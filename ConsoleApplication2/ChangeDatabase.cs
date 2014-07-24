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


namespace ConsoleApplication2
{
    class ChangeDatabase
    {
        public class ChangeDatabaseEvent
        {
            //public string Start_Lsn { get; set; }
            //public string End_Lsn { get; set; }
            //public string Seqval { get; set; }
            //public int Operation { get; set; }
            //public string Update_Mask {get; set; }
            public int SaleID { get; set; }
            public string Product { get; set; }
            public DateTime SaleDate { get; set; }
            public DateTime TransactionTime { get; set; }
            public int Operation { get; set; }
            public int StatusID { get; set; }
            public decimal SalePrice { get; set; }

        }

        public static IEnumerable<ChangeDatabaseEvent> GetEvents(DateTime mostRecentTransactionTime)
        {
            Console.WriteLine("GetEvents() called");

            //define connection string
            string connString = "Data Source=BRSMBVSQLDEV1;Initial Catalog=CaptureChanges;Persist Security Info=True;User ID=t-ankigu;Password=password1";

            //create enumerable to hold results
            IEnumerable<ChangeDatabaseEvent> result;

            //define dataconext object which is used later for translating results to objects
            DataContext dc = new DataContext(connString);

            //initiate and open connection
            SqlConnection conn = (SqlConnection)dc.Connection;
            conn.Open();

            //return all events stored in the SQL Server table
            string mostRecentTransactionString = mostRecentTransactionTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string sqlCommand = @"SELECT
                                    tm.tran_end_time [TransactionTime]
                                    ,[SaleID]
                                    ,[Product]
                                    ,[SaleDate]
                                    ,CAST( StatusID AS int) as StatusID
                                    ,[SalePrice]
                                    
                                    ,[__$operation] [Operation]
                                FROM [CaptureChanges].[cdc].[dbo_SalesHistory_CT] cdc left join [CaptureChanges].[cdc].[lsn_time_mapping] tm
	                            on cdc.[__$start_lsn] = tm.start_lsn
                                WHERE cdc.__$operation <> 3 AND tm.tran_end_time > '" + mostRecentTransactionString + "'";

            SqlCommand command = new SqlCommand(sqlCommand, conn);

            //get the database results and set the connection to close after results are read
            SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            //use "translate" to flip the reader stream to an Enumerable of my custom object type
            result = dc.Translate<ChangeDatabaseEvent>(dataReader);

            /*
             * Bug? : When the following block is uncommented and results are printed
             * to console, the sink output does not show.
             * */

            //var count = result.ToList<ChangeDatabaseEvent>().Count;
            //Console.WriteLine(count);
            //foreach (ChangeDatabaseEvent e in result)
            //{
            //    //Console.WriteLine(e.Product + " " + e.SaleDate);
            //    Console.WriteLine(e.SaleID);
            //    ++count;
            //}
            //Console.WriteLine("Source count: " + count);
            return result;
        }
        public static void begin(Application myApp)
        {
                            
                System.Linq.IQueryable<ChangeDatabaseEvent> qSource = myApp.DefineEnumerable<ChangeDatabaseEvent>(() => GetEvents(DateTime.MinValue));
                //qSource.AsEnumerable<ChangeDatabaseEvent>().Concat(GetEvents());
                //qSource.Concat(GetEvents());
                var qSource2 = myApp.DefineObservable(() => new ObservablePoller(5000));


                IQStreamable<ChangeDatabaseEvent> tSource = qSource.ToPointStreamable<ChangeDatabaseEvent, ChangeDatabaseEvent>(
                              e => CreatePoint(e), AdvanceTimeSettings.IncreasingStartTime);
                
                //tSource.Deploy("serverSource");


                var streamable = qSource2.ToPointStreamable(x => PointEvent.CreateInsert(DateTime.Now, x), AdvanceTimeSettings.StrictlyIncreasingStartTime);
                
                // DEFINE a simple observer SINK (writes the value to the server console)
                //var mySink = myApp.DefineObserver(() => Observer.Create<ChangeDatabaseEvent>(x => Console.WriteLine("sinkserver: " + x.Product + " SaleDate:" + x.SaleDate + " TransTime:" + x.TransactionTime)));
                var mySink = myApp.DefineObserver(() => Observer.Create<ChangeDatabaseEvent>(x =>Sink(x)));
                var mySink2 = myApp.DefineObserver(() => Observer.Create<ChangeDatabaseEvent>(x =>Sink(x)));
                // DEPLOY the sink to the server for clients to use
                //mySink.Deploy("serverSink");

                // Compose a QUERY over the source (return every even-numbered event)
                var myQuery = from e in tSource
                              select e;

                var myQuery2 = from e in streamable
                               select e;
                 
                // BIND the query to the sink and RUN it
                using (IDisposable proc = myQuery.Bind<ChangeDatabaseEvent>(mySink).Run("serverProcess"))
                {
                    // Wait for the user stops the server
                    //IDisposable proc2 = myQuery2.ToObservable().Subscribe(Console.WriteLine);
                    myQuery2.Bind(mySink2).Run("Server2");
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("MyStreamInsightServer is running, press Enter to stop the server");
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine(" ");
                    Console.ReadLine();
                }
                Console.ReadLine();
        }

        private static PointEvent<ChangeDatabaseEvent> CreatePoint(ChangeDatabaseEvent e)
        {
            //Console.WriteLine("source " + e.Product + " " + e.SaleDate);
            //Console.WriteLine(e.SaleID);
            return PointEvent.CreateInsert<ChangeDatabaseEvent>(DateTime.Now, e);
        }

        

        private static void Sink(ChangeDatabaseEvent x)
        {
            Console.WriteLine(@"sinkserver: "
                + x.SaleID + " "
                + x.Product
                + " SaleDate:" + x.SaleDate
                + " TransTime:" + x.TransactionTime
                + " Operation: " + x.Operation
                + " StatusID: " + x.StatusID
                + " SalePrice: " + x.SalePrice
                );


            ////DELETE
            //if (x.Operation == 1)
            //{

            //}
            ////INSERT or UPDATE(new values)
            //else if (x.Operation == 2 || x.Operation == 4)
            //{
            //    List<Scripts.CellElement> cells = new List<Scripts.CellElement>();
            //    cells.Add(new Scripts.CellElement() { column = sinkProduct, value = x.Product.ToString() });
            //    cells.Add(new Scripts.CellElement() { column = sinkSaleDate, value = x.SaleDate.ToString() });
            //    cells.Add(new Scripts.CellElement() { column = sinkStatusID, value = x.StatusID.ToString() });

            //    cells.Add(new Scripts.CellElement() { column = sinkSalePrice, value = x.SalePrice.ToString() });

            //    Scripts.RowElement row = new Scripts.RowElement() { key = x.SaleID.ToString(), Cell = cells };

            //    List<Scripts.RowElement> allRows = new List<Scripts.RowElement>();
            //    allRows.Add(row);

            //    Scripts.RowBody rowBody = new Scripts.RowBody() { Row = allRows };
            //    Scripts.putRowBody(x.SaleID.ToString(), rowBody);
            //}
          
        }


    }
}
