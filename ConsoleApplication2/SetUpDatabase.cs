using System;
using System.ServiceModel;
using Microsoft.ComplexEventProcessing;
using Microsoft.ComplexEventProcessing.Linq;
using Microsoft.ComplexEventProcessing.ManagementService;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.ComponentModel.Design;
using System.Data.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ConsoleApplication2
{

    


    class SetUpDatabase<SourceEvent>
    {
        private string TABLENAME = "";
        private string CONNECTION_STRING = "";
        private string SQL_QUERY = "";
        private string HBASE_ROW_KEY = "";
        private static RequestManager.RequestHolder requestHolder;

        public SetUpDatabase(string tableName, string key, string connectionString, string sqlQuery){
            this.TABLENAME = tableName;
            this.HBASE_ROW_KEY = key;
            this.CONNECTION_STRING = connectionString;
            this.SQL_QUERY = sqlQuery;
        }

        public void begin(Application myApp, int capacity, string processName)
        {
            string conn_string = this.CONNECTION_STRING;
            string sql_query = this.SQL_QUERY;
            System.Linq.IQueryable<SourceEvent> qSource = myApp.DefineEnumerable<SourceEvent>(() => GetEvents(conn_string, sql_query));

            IQStreamable<SourceEvent> tSource = qSource.ToPointStreamable<SourceEvent, SourceEvent>(
                          e => CreatePoint(e), AdvanceTimeSettings.IncreasingStartTime);

            //tSource.Deploy("serverSource");
            requestHolder = new RequestManager.RequestHolder(this.TABLENAME, capacity);
            requestHolder.rowBody = new RequestManager.RowBody();
            requestHolder.rowBody.Row = new List<RequestManager.RowElement>();



            // DEFINE a simple observer SINK (writes the value to the server console)
            //var mySink = myApp.DefineObserver(() => Observer.Create<SourceDatabaseEvent>(x => Console.WriteLine("sinkserver: " + x.Product + " SaleDate:" + x.SaleDate + " TransTime:" + x.TransactionTime)));
            string hbase_row_key = this.HBASE_ROW_KEY;
            var mySink = myApp.DefineObserver(() => Observer.Create<SourceEvent>(x => Sink<SourceEvent>(x, hbase_row_key)));

            // DEPLOY the sink to the server for clients to use
            //mySink.Deploy("serverSink");

            // Compose a QUERY over the source (return every even-numbered event)
            var myQuery = from e in tSource
                          select e;
            
            // BIND the query to the sink and RUN it
            using (IDisposable proc = myQuery.Bind<SourceEvent>(mySink).Run(processName))
            {
                // Wait for the user stops the server
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("MyStreamInsightServer is running, press Enter to stop the server");
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine(" ");
                Console.ReadLine();
            }
        }

        private static IEnumerable<SourceEvent> GetEvents(string connString, string sqlCommand)
        {
            Console.WriteLine("GetEvents() called");

            //SQL Connection and Query Setup

            //string connString = connString;
            //string sqlCommand = SQL_COMMANDS.ACCOUNT_BASE;

            //string connString = SQL_COMMANDS.CAPTURE_CHANGES_CONN;
            //string sqlCommand = SQL_COMMANDS.SALES_HISTORY;


            //create enumerable to hold results
            IEnumerable<SourceEvent> result;

            //define dataconext object which is used later for translating results to objects
            DataContext dc = new DataContext(connString);

            //initiate and open connection
            SqlConnection conn = (SqlConnection)dc.Connection;
            conn.Open();

           

            SqlCommand command = new SqlCommand(sqlCommand, conn);

            //get the database results and set the connection to close after results are read
            SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            //use "translate" to flip the reader stream to an Enumerable of my custom object type
            result = dc.Translate<SourceEvent>(dataReader);
            //Console.WriteLine("numResults: " + result.Count());
          
            return result;
        }

        private static PointEvent<SourceEvent> CreatePoint(SourceEvent e)
        {
            return PointEvent.CreateInsert<SourceEvent>(DateTime.Now, e);
        }

        
      

        private static void Sink<SourceEvent>(SourceEvent x, string HBASE_ROW_KEY)
        {
 
            SourceEvent rowevent = x;
            PropertyInfo[] props = rowevent.GetType().GetProperties();

            List<RequestManager.CellElement> cells = new List<RequestManager.CellElement>();
            string key = "";

            foreach (PropertyInfo pi in props)
            {
                //skip the PrimaryKey which acts as key in HBase
                if (pi.Name == HBASE_ROW_KEY)
                {
                    key = RequestManager.ToBase64(pi.GetValue(rowevent, null).ToString());
                    continue;
                }

                // if have Byte[], need to convert to string
                if (pi.PropertyType == typeof(Byte[]))
                {
                    string byteCol = RequestManager.ToBase64("cf:" + pi.Name);
                    string byteVal = RequestManager.ToBase64(pi.GetValue(rowevent, null) == null ? "NULL" : BitConverter.ToString((Byte[])pi.GetValue(rowevent, null)));
                    RequestManager.CellElement byteCell = new RequestManager.CellElement { column = byteCol, value = byteVal };
                    cells.Add(byteCell);
                    continue;
                }

                //set up cellelement with column and value
                string col = RequestManager.ToBase64("cf:" + pi.Name);
                string val = RequestManager.ToBase64(pi.GetValue(rowevent, null) == null ? "NULL" : pi.GetValue(rowevent, null).ToString());
                RequestManager.CellElement cell = new RequestManager.CellElement { column = col, value = val };
                cells.Add(cell);
            };

            RequestManager.RowElement row = new RequestManager.RowElement() { key = key, Cell = cells };

            requestHolder.addRow(row);

         


        }

    }
}
