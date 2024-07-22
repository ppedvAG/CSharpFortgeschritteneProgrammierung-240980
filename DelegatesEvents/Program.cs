

namespace DelegatesEvents
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Delegate Sample");
            DelegateSample();

            Console.WriteLine("\n\nAction und Funcs");
            ActionSample();

            Console.WriteLine("\n\nEvents");
            EventSample();
        }

        #region Delegates

        // delegates sind sozusagen "function pointers",
        // also Zeiger welche auf eine Methode im Speicher zeigen
        public delegate void Hello(string name);

        private static void DelegateSample()
        {
            var hello = new Hello(HalloDE);

            hello("Tobias"); // Ausfuehrung des Delegates

            // Mit += koennen wir weitere function pointers hinzufuegen
            hello += HalloDE;
            hello("Max");

            hello += HelloEN;
            hello += HelloEN;
            hello("Felix");

            hello -= HalloDE;
            hello -= HalloDE;
            hello("Jutta");

            hello -= HelloEN;
            hello -= HelloEN;
            //hello("anybody?"); // hello ist null

            // Immer ein Null-Check vor Ausfuehrung des Delegates durchfuehren
            if (hello != null)
            {
                hello("Stefan");
            }

            // Kuerzere Schreibweise fuer null-Check
            hello?.Invoke("Lukas"); // Null-Propagation

            // Nichts passiert
            hello -= HalloDE;
        }

        private static void HalloDE(string name)
        {
            Console.WriteLine("Hallo, mein Name ist " + name);
        }

        private static void HelloEN(string name)
        {
            Console.WriteLine("Howdy, my name is " + name);
        }

        #endregion

        #region Action und Func
        private static void ActionSample()
        {
            var printNumber = new Action<int, int>(PrintRandomNumber);
            printNumber += PrintRandomNumber;
            printNumber?.Invoke(100, 0);

            var addNumbers = new Func<int, int, int>(AddNumbers);
            addNumbers += AddNumbers;
            var result = addNumbers?.Invoke(5, 6);
            Console.WriteLine($"5 + 6 = {result}");

            // Praktische Beispiele
            List<int> numbers = [1,2,3,4];
            var result2 = numbers.Find(IsEven);
            Console.WriteLine($"The first even number is {result2}");

            Console.WriteLine("More even numbers...");
            var evenNumbers = numbers.Where(IsEven);
            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }

            bool IsEven(int number) => number % 2 == 0;
        }

        private static void PrintRandomNumber(int max = 10, int min = 0)
        {
            Console.WriteLine($"My lucky number today is {Random.Shared.Next(min, max)}");
        }

        private static int AddNumbers(int a, int b)
        {
            return a + b;
        }

        #endregion

        #region Events

        private static void EventSample()
        {
            var demo = new EventsDemo();

            demo.OnRainStarting += (sender, args) =>
            {
                Console.WriteLine("It's raining");
            };
            demo.OnRainEnding += (sender, args) =>
            {
                Console.WriteLine($"It's not raining anymore. The amount was {args.Amount}");
            };
            
            demo.StartSample();

            var instance = new Component();
            instance.OnStart += () => Console.WriteLine("Process started");
            instance.OnEnd += () => Console.WriteLine("Process finished");
            instance.OnProgress += (p) => Console.WriteLine($"Process progress: {p}%");
            instance.Run();
        }

        #endregion
    }

    /// <summary>
    /// Komponente, die eine beliebige Arbeit verrichtet und Fortschritt laufend zurueck gibt
    /// </summary>
    public class Component
    {
        public event Action OnStart;

        public event Action OnEnd;

        public event Action<int> OnProgress;

        public void Run()
        {
            OnStart?.Invoke();
            for (int i = 0; i < 100; i += 20)
            {
                Thread.Sleep(100);
                OnProgress?.Invoke(i);
            }
            OnEnd?.Invoke();
        }
    }
}
