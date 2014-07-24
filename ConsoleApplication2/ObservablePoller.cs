using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication2;
using System.Data;

namespace ConsoleApplication2
{
    sealed class ObservablePoller : IObservable<ChangeDatabase.ChangeDatabaseEvent>, IDisposable
    {
        private bool _done;
        private readonly List<IObserver<ChangeDatabase.ChangeDatabaseEvent>> _observers;
        private readonly Random _random;
        private readonly object _sync;
        private readonly Timer _timer;
        private readonly int _timerPeriod;
        private DateTime mostRecentTime;

        /// <summary>
        /// Random observable subject. It produces an integer in regular time periods.
        /// </summary>
        /// <param name="timerPeriod">Timer period (in milliseconds)</param>
        public ObservablePoller(int timerPeriod)
        {
            _done = false;
            _observers = new List<IObserver<ChangeDatabase.ChangeDatabaseEvent>>();
            _random = new Random();
            _sync = new object();
            _timer = new Timer(Poll);
            _timerPeriod = timerPeriod;
            mostRecentTime = DateTime.Parse("1975-01-01 12:00:00.000");
            Schedule();
        }

        private void Poll(object _)
        {
            IEnumerable<ChangeDatabase.ChangeDatabaseEvent> results = ChangeDatabase.GetEvents(mostRecentTime);

            foreach (ChangeDatabase.ChangeDatabaseEvent cde in results)
            {
                Console.WriteLine("Observable: " + cde.Product);
                this.mostRecentTime = cde.TransactionTime;

                var sqlFormattedDate = mostRecentTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Console.WriteLine(sqlFormattedDate);
                OnNext(cde);

            }
            Schedule();
        }


        public IDisposable Subscribe(IObserver<ChangeDatabase.ChangeDatabaseEvent> observer)
        {
            lock (_sync)
            {
                _observers.Add(observer);
            }
            return new Subscription(this, observer);
        }

        public void OnNext(ChangeDatabase.ChangeDatabaseEvent value)
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

        

        private sealed class Subscription : IDisposable
        {
            private readonly ObservablePoller _subject;
            private IObserver<ChangeDatabase.ChangeDatabaseEvent> _observer;

            public Subscription(ObservablePoller subject, IObserver<ChangeDatabase.ChangeDatabaseEvent> observer)
            {
                _subject = subject;
                _observer = observer;
            }

            public void Dispose()
            {
                IObserver<ChangeDatabase.ChangeDatabaseEvent> observer = _observer;
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
