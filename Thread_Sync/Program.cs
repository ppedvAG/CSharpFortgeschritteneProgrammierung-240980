
namespace Thread_Sync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            // Sample 1 - Threads werden alle parallel abgearbeitet
            //SpawnNewThreads(DoWork);

            // Sample 2 - Mit Lock werden die Threads sequentiell abgearbeitet
            //SpawnNewThreads(DoWorkUsingLock);

            // Sample 3 - Exceptions fangen um andere Threads nicht zu blockieren
            //SpawnNewThreads(DoWorkUsingMonitor);

            // Sample 4 - Wenn auf ein schreibenden Thread gewartet werden soll
            //SpawnNewThreadsWithManualReset();

            // Sample 5 - Wenn schreibende Threads sequentiell abgearbeitet werden sollen
            //SpawnNewThreadsWithAutoReset();

            // Sample 6 - Wenn schreibende Threads sequentiell abgearbeitet werden sollen
            //SpawnNewThreadsWithMutex();

            // Sample 7 - Wenn mehrere Threads schreiben duerfen, wird ein Semaphore verwendet
            SpawnNewThreadsWithSemaphore();

            Console.ReadKey();
        }

        private static void SpawnNewThreads(Action action, int numberOfThreads = 10)
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                new Thread(() => action()).Start();
            }
        }

        #region Sample 1

        private static void DoWork()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started");
            Thread.Sleep(2000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished");
        } 

        #endregion

        #region Sample 2 - lock

        private static object _locker = new object();

        private static void DoWorkUsingLock()
        {
            lock (_locker)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started");
                Thread.Sleep(2000);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished");
            }
        } 

        #endregion

        #region Sample 3 - Monitor

        private static void DoWorkUsingMonitor()
        {
            try
            {
                Monitor.Enter(_locker);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started");
                Thread.Sleep(2000);

                if (Random.Shared.Next(0, 2) == 0)
                {
                    throw new Exception();
                }

                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished");
            }
            catch (Exception)
            {
                // Exception loggen
            }
            finally
            {
                Monitor.Exit(_locker);
            }
        }

        #endregion

        #region Sample 4 - ManualResetEvent

        static readonly ManualResetEvent _mre = new ManualResetEvent(false); // false == non-signaled

        private static void SpawnNewThreadsWithManualReset()
        {
            new Thread(WriteSomething).Start();

            SpawnNewThreads(ReadThatSomething);
        }

        private static void ReadThatSomething()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} reading...");

            // Der lock wird hier sozusagen abgefragt
            _mre.WaitOne(); 

            Thread.Sleep(1000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} reading completed.");
        }

        private static void WriteSomething(object? obj)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
            _mre.Reset();
            Thread.Sleep(5000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing completed.");
            _mre.Set();
        }

        #endregion

        #region Sample 5 - AutoResetEvent

        static readonly AutoResetEvent _are = new AutoResetEvent(true); // true damit der erste Thread den Anfang machen kann

        private static void SpawnNewThreadsWithAutoReset()
        {
            SpawnNewThreads(WriteSomethingDifferent, 5);

            // Auf keinen Fall sollte Set() vom Main Thread gesetzt werden
            //Thread.Sleep(1000);
            //_are.Set();
        }

        private static void WriteSomethingDifferent()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting...");
            _are.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
            Thread.Sleep(5000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing completed.");
            _are.Set();
        }

        #endregion

        #region Sample 6 - Mutex

        static readonly Mutex _mutex = new Mutex(true); // true damit der erste Thread den Anfang machen kann

        private static void SpawnNewThreadsWithMutex()
        {
            SpawnNewThreads(WriteSomethingWithMutex, 5);

            // Unterschied zum AutoResetEvent ist, dass Release eine ApplicationException wirft
            // "Object synchronization method was called from an unsynchronized block of code"
            _mutex.ReleaseMutex();
        }

        private static void WriteSomethingWithMutex()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting...");
            _mutex.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
            Thread.Sleep(5000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing completed.");
            _mutex.ReleaseMutex();
        }

        #endregion

        #region Sample 7 - Semaphore

        static readonly Semaphore _semaphore = new Semaphore(initialCount: 1, maximumCount: 3);

        private static void SpawnNewThreadsWithSemaphore()
        {
            SpawnNewThreads(WriteSomethingWithSemaphore, 5);

            // Release() stoert hier nicht
            Thread.Sleep(5000);
            _semaphore.Release();
        }

        private static void WriteSomethingWithSemaphore()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting...");
            _semaphore.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
            Thread.Sleep(5000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing completed.");
            _semaphore.Release();
        }
        #endregion
    }
}
