namespace AsyncAwait
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Einfaches Beispiel: Zwei Dateien herunterladen
            //SequentialSample();

            // Async: Zwei Dateien parallel herunterladen
            await ParallelAsyncSample();
        }

        private static void SequentialSample()
        {
            Task.Delay(10000).Wait();
            Console.WriteLine("Download file 1");

            Task.Delay(10000).Wait();
            Console.WriteLine("Download file 2");

            Console.WriteLine("Start data input: Enter name");
            var input = Console.ReadLine();
            Console.WriteLine($"Hello {input}");

            Console.ReadLine();
        }

        private static async Task ParallelAsyncSample()
        {
            // An dieser Stelle werden zwangslauefig keine neuen Threads angelegt sondern sog. Coroutines verwendet
            Download1();

            Download2();

            Console.WriteLine("Start data input: Enter name");
            var input = Console.ReadLine();
            Console.WriteLine($"Hello {input}");

            Console.ReadLine();
        }

        private static async Task Download2()
        {
            await Task.Delay(10000);
            Console.WriteLine("Download file 2 -- Thread ID: " + Thread.CurrentThread.ManagedThreadId);
        }

        private static async Task Download1()
        {
            await Task.Delay(10000);
            Console.WriteLine("Download file 1 -- Thread ID: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
