using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Multitasking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadKey(); // Diese Methode wird im Hintergrund ausgeführt

            //TaskSamples();

            //TaskWaitSample();

            //TaskAbortSample();

            //TaskExceptionSample();

            ConcurrentCollectionSample();

            Console.ReadKey();
        }

        #region Sample Tasks

        private static void TaskSamples()
        {
            var task = new Task(RandomNumber);
            task.Start();

            Task.Factory.StartNew(RandomNumber);

            Task.Run(RandomNumber);

            var taskWithArg = new Task(RandomNumberMax, 100);
            taskWithArg.Start();

            var taskWithReturn = new Task<int>(CreateRandomNumber);
            taskWithReturn.Start();

            if (taskWithReturn.IsCompleted)
            {
                Console.WriteLine($"Random number {taskWithReturn.Result} from thread {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        public static void RandomNumber()
        {
            Thread.Sleep(2000);
            int number = new Random().Next(0, 100);
            Console.WriteLine($"Random number {number} from thread {Thread.CurrentThread.ManagedThreadId}");
        }

        public static void RandomNumberMax(object max)
        {
            Thread.Sleep(2000);
            int number = new Random().Next(0, (int)max);
            Console.WriteLine($"Random number {number} from thread {Thread.CurrentThread.ManagedThreadId}");
        }

        public static int CreateRandomNumber()
        {
            Thread.Sleep(2000);
            int number = new Random().Next(0, 100);
            return number;
        }

        #endregion

        #region Sample Task Wait All/Any

        private static void TaskWaitSample()
        {
            var task = new Task(() => Thread.Sleep(2000));
            task.Start();

            // Warte hier auf den Task und blockiere den Main Thread
            task.Wait();

            var spawnSomeTasks = CreateTasks();

            // Warten bis alle Tasks abgearbeitet wurden
            Task.WaitAll(spawnSomeTasks.ToArray());

            // Warten bis mindestens ein Task abgearbeitet wurde
            Task.WaitAny(spawnSomeTasks.ToArray());
        } 

        private static IEnumerable<Task> CreateTasks(int numberOfTasks = 10)
        {
            for (int i = 0; i < numberOfTasks; i++)
            {
                yield return new Task(() => Thread.Sleep(2000));
            }
        }

        #endregion

        #region Sample Task Cancellation

        private static void TaskAbortSample()
        {
            var cts = new CancellationTokenSource(); // Tokenproduktion, also Quelle der Tokens
            var token = cts.Token; // Token abholen

            var task = new Task(RunCancellableTasks, token);

            task.Start();

            Thread.Sleep(500);

            // Cancel wird immer auf der Source durchgefuehrt
            cts.Cancel();
        }

        private static void RunCancellableTasks(object arg)
        {
            if (arg is CancellationToken token)
            {
                for (int i = 0; i < 20; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"Running task {i} with Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(100);
                }
            }
        }

        #endregion

        #region Sample Catch AggregateException

        private static void TaskExceptionSample()
        {
            try
            {
                var task = Task.Run(() => RunTaskThrowsException(0));
                Task.WaitAll(task);
            }
            catch (AggregateException aggregates)
            {
                foreach (var exception in aggregates.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.StackTrace);
                    Console.WriteLine("============================");
                }
            }
        }

        private static void RunTaskThrowsException(int i)
        {
            Thread.Sleep(2000);
            throw new NotImplementedException("Not implemented #" + i);
        }

        #endregion

        #region Sample Concurrent Collection

        private static void ConcurrentCollectionSample()
        {
            // NuGet: System.ServiceModel.Primitives wird benoetigt
            var list = new SynchronizedCollection<int>(); // Wie List aber mit einem Lock-Block um jede Methode herum
            list.Add(42);
            Console.WriteLine("Listeintrag " + list[0]);

            var bag = new ConcurrentBag<int>(); // "Haufen von Objekten" welche wir mit Linq verarbeiten konnen
            bag.Add(42);

            Console.WriteLine("BagEintrag " + bag.ToArray()[0]);

            var dict = new ConcurrentDictionary<string, int>(); // Wie Dictionary, jedoch mit einem Lock-Block um jede Methode herum
            int UpdateDictEntry(string key, int value) => value; // Local Function um Wert zu aktualisieren
            
            dict.AddOrUpdate("Hugo", 42, UpdateDictEntry);

            dict.TryAdd("Klaus", 96); // Statt dem normalem Add beim Dictionary

            if (dict.TryGetValue("Hugo", out int hugoValue))
            {
                Console.WriteLine("Hugo hat " + hugoValue + " Punkte");
            }
            else
            {
                Console.WriteLine("Fehler den Wert fuer Hugo zu bekommen");
            }

        }

        #endregion
    }
}
