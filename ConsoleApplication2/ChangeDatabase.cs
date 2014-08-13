using System;
using System.ServiceModel;
using Microsoft.ComplexEventProcessing;
using Microsoft.ComplexEventProcessing.Linq;
using Microsoft.ComplexEventProcessing.ManagementService;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;


namespace ConsoleApplication2
{
    class ChangeDatabase<T> where T : ConsoleApplication2.ChangeEvent
    {

        private string TABLENAME = "";
        private string CONNECTION_STRING = "";
        private string SQL_QUERY = "";
        private string HBASE_ROW_KEY = "";
        private static RequestManager.RequestHolder requestHolder;

        public ChangeDatabase(string tableName, string key, string connectionString, string sqlQuery){
            this.TABLENAME = tableName;
            this.HBASE_ROW_KEY = key;
            this.CONNECTION_STRING = connectionString;
            this.SQL_QUERY = sqlQuery;
        }

     
        public void begin(Application myApp, int capacity, string processName)
        {

                string conn_string = this.CONNECTION_STRING;
                string sql_query = this.SQL_QUERY;

                System.Reactive.Linq.IQbservable<T> qSource2 = myApp.DefineObservable<T>(() => new ObservablePoller<T>(conn_string, sql_query, 5000));
                var streamable = qSource2.ToPointStreamable(x => PointEvent.CreateInsert(DateTime.Now, x), AdvanceTimeSettings.IncreasingStartTime);

                requestHolder = new RequestManager.RequestHolder(this.TABLENAME, capacity);
                requestHolder.rowBody = new RequestManager.RowBody();
                requestHolder.rowBody.Row = new List<RequestManager.RowElement>();

                // DEFINE a simple observer SINK (writes the value to the server console)
                string hbase_row_key = this.HBASE_ROW_KEY.Clone().ToString();
                var mySink2 = myApp.DefineObserver(() => Observer.Create<T>(x => Sink<T>(x, hbase_row_key)));


                // Compose a QUERY over the source (able to filter out results here if desired)
            
                var myQuery2 = from e in streamable
                               select e;
                 
                // BIND the query to the sink and RUN it
                using (IDisposable proc = myQuery2.Bind<T>(mySink2).Run(processName))
                {
                    
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("MyStreamInsightServer is running, press Enter to stop the server");
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine(" ");
                    Console.ReadLine();
                }
                Console.ReadLine();
        }

        
        private static int hitCount = 0;

        private static void Sink<T> (T x, string hbase_row_key)
        {
            Console.WriteLine("Sink hit count: " + hitCount++);
            ChangeEvent rowevent = (ChangeEvent)(object)x;
            PropertyInfo[] props = rowevent.GetType().GetProperties();

            List<RequestManager.CellElement> cells = new List<RequestManager.CellElement>();
            string key = "";

            foreach (PropertyInfo pi in props)
            {
                //skip the PrimaryKey which acts as key in HBase
                if (pi.Name == hbase_row_key)
                {
                    Console.WriteLine("key: " + pi.GetValue(rowevent, null).ToString());
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
                string val = pi.GetValue(rowevent, null) == null ? "NULL" : pi.GetValue(rowevent, null).ToString();

                Console.WriteLine("col: " + pi.Name);
                Console.WriteLine("val: " + val);

                val = RequestManager.ToBase64(val);
                
                RequestManager.CellElement cell = new RequestManager.CellElement { column = col, value = val };
                cells.Add(cell);
            };

            RequestManager.RowElement row = new RequestManager.RowElement() { key = key, Cell = cells };
            requestHolder.addRow(row);
          
        }


    }
}
