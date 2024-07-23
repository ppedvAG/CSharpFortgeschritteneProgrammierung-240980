namespace Multitasking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadKey(); // Diese Methode wird im Hintergrund ausgeführt

            TaskSamples();

            TaskWaitSample();

            Console.ReadKey();
        }

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

    }
}
