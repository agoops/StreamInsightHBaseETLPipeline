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
    //sealed
    class ObservablePoller<T> : IObservable<T>, IDisposable where T : ChangeEvent
    {
        private bool _done;
        private readonly List<IObserver<T>> _observers;
        private readonly Random _random;
        private readonly object _sync;
        private readonly Timer _timer;
        private readonly int _timerPeriod;
        private DateTime mostRecentTime;

        private string SQL_COMMAND = "";
        private string CONNECTION_STRING = "";


        public ObservablePoller(string connString, string sqlCommand, int timerPeriod)
        {
            this.SQL_COMMAND = sqlCommand;
            this.CONNECTION_STRING = connString;

            _done = false;
            _observers = new List<IObserver<T>>();
            _random = new Random();
            _sync = new object();
            _timer = new Timer(Poll);
            _timerPeriod = timerPeriod;
            mostRecentTime = DateTime.Parse("1975-01-01 12:00:00.000");
            Schedule();
        }

        private void Poll(object _)
        {
            IEnumerable<T> results = GetEvents(mostRecentTime);

            foreach (T e in results)
            {
                this.mostRecentTime = e.getTransactionTime();
                //e.getTransactionTime();
                //this.mostRecentTime = e.getTransactionTime();
                //var sqlFormattedDate = mostRecentTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //Console.WriteLine(sqlFormattedDate);

                OnNext(e);

            }
            Schedule();
        }


        public IDisposable Subscribe(IObserver<T> observer)
        {
            lock (_sync)
            {
                _observers.Add(observer);
            }
            return new Subscription(this, observer);
        }

        public void OnNext(T value)
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

        public IEnumerable<T> GetEvents(DateTime mostRecentTransactionTime)
        {
            Console.WriteLine("GetEvents() called");

            //define connection string
            string connString = this.CONNECTION_STRING;

            //create enumerable to hold results
            IEnumerable<T> result;

            //define dataconext object which is used later for translating results to objects
            DataContext dc = new DataContext(connString);

            //initiate and open connection
            SqlConnection conn = (SqlConnection)dc.Connection;
            conn.Open();

            //return all events stored in the SQL Server table
            string mostRecentTransactionString = mostRecentTransactionTime.ToString("yyyy-MM-dd HH:mm:ss.fff");


            string sqlCommand = this.SQL_COMMAND + "'" + mostRecentTransactionString + "'";

            SqlCommand command = new SqlCommand(sqlCommand, conn);

            //get the database results and set the connection to close after results are read
            SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            //use "translate" to flip the reader stream to an Enumerable of my custom object type
            result = dc.Translate<T>(dataReader);
            

        
            return result;
        }

        private sealed class Subscription : IDisposable
        {
            private readonly ObservablePoller<T> _subject;
            private IObserver<T> _observer;

            public Subscription(ObservablePoller<T> subject, IObserver<T> observer)
            {
                _subject = subject;
                _observer = observer;
            }

            public void Dispose()
            {
                IObserver<T> observer = _observer;
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
