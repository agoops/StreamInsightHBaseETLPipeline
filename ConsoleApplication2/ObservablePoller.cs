using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication2;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;

namespace ConsoleApplication2
{
    sealed class ObservablePoller<SourceEvent> : IObservable<SourceEvent>, IDisposable
    {
        private bool _done;
        private readonly List<IObserver<SourceEvent>> _observers;
        private readonly Random _random;
        private readonly object _sync;
        private readonly Timer _timer;
        private readonly int _timerPeriod;
        private DateTime mostRecentTime;

        private string SQL_COMMAND = "";
        private string CONNECTION_STRING = "";


        public ObservablePoller(string connString, string sqlCommand, int timerPeriod)
        {
            _done = false;
            _observers = new List<IObserver<SourceEvent>>();
            _random = new Random();
            _sync = new object();
            _timer = new Timer(Poll);
            _timerPeriod = timerPeriod;
            mostRecentTime = DateTime.Parse("1975-01-01 12:00:00.000");
            Schedule();
        }

        private void Poll(object _)
        {
            IEnumerable<SourceEvent> results = GetEvents(mostRecentTime);

            foreach (SourceEvent e in results)
            {
                //Console.WriteLine("Observable: " + e.Product);
                this.mostRecentTime = e.TransactionTime;

                var sqlFormattedDate = mostRecentTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Console.WriteLine(sqlFormattedDate);
                OnNext(e);

            }
            Schedule();
        }


        public IDisposable Subscribe(IObserver<SourceEvent> observer)
        {
            lock (_sync)
            {
                _observers.Add(observer);
            }
            return new Subscription(this, observer);
        }

        public void OnNext(SourceEvent value)
        {
            lock (_sync)
            {
                if (!_done)
                {
                    foreach (var observer in _observers)
                    {
                        observer.OnNext(value);
                    }
                }
            }
        }

        public void OnError(Exception e)
        {
            lock (_sync)
            {
                foreach (var observer in _observers)
                {
                    observer.OnError(e);
                }
                _done = true;
            }
        }

        public void OnCompleted()
        {
            lock (_sync)
            {
                foreach (var observer in _observers)
                {
                    observer.OnCompleted();
                }
                _done = true;
            }
        }

        void IDisposable.Dispose()
        {
            _timer.Dispose();
        }

        private void Schedule()
        {
            lock (_sync)
            {
                if (!_done)
                {
                    _timer.Change(_timerPeriod, Timeout.Infinite);
                }
            }
        }

        public IEnumerable<SourceEvent> GetEvents(DateTime mostRecentTransactionTime)
        {
            Console.WriteLine("GetEvents() called");

            //define connection string
            string connString = this.CONNECTION_STRING;

            //create enumerable to hold results
            IEnumerable<SourceEvent> result;

            //define dataconext object which is used later for translating results to objects
            DataContext dc = new DataContext(connString);

            //initiate and open connection
            SqlConnection conn = (SqlConnection)dc.Connection;
            conn.Open();

            //return all events stored in the SQL Server table
            string mostRecentTransactionString = mostRecentTransactionTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
//            string sqlCommand = @"SELECT
//                                    tm.tran_end_time [TransactionTime]
//                                    ,[SaleID]
//                                    ,[Product]
//                                    ,[SaleDate]
//                                    ,CAST( StatusID AS int) as StatusID
//                                    ,[SalePrice]
//                                    
//                                    ,[__$operation] [Operation]
//                                FROM [CaptureChanges].[cdc].[dbo_SalesHistory_CT] cdc left join [CaptureChanges].[cdc].[lsn_time_mapping] tm
//	                            on cdc.[__$start_lsn] = tm.start_lsn
//                                WHERE cdc.__$operation <> 3 AND tm.tran_end_time > '" + mostRecentTransactionString + "'";

            string sqlCommand = this.SQL_COMMAND + "'" + mostRecentTransactionString + "'";

            SqlCommand command = new SqlCommand(sqlCommand, conn);

            //get the database results and set the connection to close after results are read
            SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            //use "translate" to flip the reader stream to an Enumerable of my custom object type
            result = dc.Translate<SourceEvent>(dataReader);

        
            return result;
        }

        private sealed class Subscription : IDisposable
        {
            private readonly ObservablePoller<SourceEvent> _subject;
            private IObserver<SourceEvent> _observer;

            public Subscription(ObservablePoller<SourceEvent> subject, IObserver<SourceEvent> observer)
            {
                _subject = subject;
                _observer = observer;
            }

            public void Dispose()
            {
                IObserver<SourceEvent> observer = _observer;
                if (null != observer)
                {
                    lock (_subject._sync)
                    {
                        _subject._observers.Remove(observer);
                    }
                    _observer = null;
                }
            }
        }
    }
}
