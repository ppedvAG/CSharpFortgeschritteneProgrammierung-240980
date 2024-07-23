namespace Lab_Konto
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            CreateThreads();

            CreateTasks();

            Console.WriteLine("Fertig");
            Console.Read();
        }

        private static void CreateThreads()
        {
            var konto = new KontoWithLock() { Type = "Threads" };
            for (int i = 0; i < 50; i++)
            {
                new Thread(() => Run(konto)).Start();
            }
        }

        private static void CreateTasks()
        {
            var konto = new KontoWithMutex() { Type = "Tasks" };
            for (int i = 0; i < 50; i++)
            {
                Task.Factory.StartNew(Run, konto);
            }
        }

        static void Run(object arg) //Random Einzahlungen und Auszahlungen ausführen
        {
            var konto = (IKonto)arg;
            for (int i = 0; i < 10; i++)
            {
                int betrag = Random.Shared.Next(0, 10) * 10;

                if (Random.Shared.Next() % 2 == 0)
                    konto.Deposite(betrag);
                else
                    konto.Disburse(betrag);
            }
        }
    }
}