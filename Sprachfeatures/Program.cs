namespace Sprachfeatures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine("TuplesSample()");
            TuplesSample();

            Console.WriteLine("\nLocalFunctionSample()");
            LocalFunctionSample();

            // Wir koennen hier auf Color zugreifen weil das using aus Usings.cs kommt
            Console.WriteLine($"\n My favorite color is {Color.Green}");
        }

        private static void TuplesSample()
        {
            var max = new PersonUsingTuples
            {
                Vorname = "Max",
                Nachname = "Mustermann",
                ZweiterName = "Moritz"
            };
            //max.Vorname = "Max"; // weil init erlaubt nur einmalige Zuweisung

            // Wurde frueher so gemacht und wirklich schlecht lesbar! Was soll Item 1 sein?
            var tuples = max.VollerNameMitPrehistoricTuples();
            Console.WriteLine(tuples.Item1);
            Console.WriteLine(tuples.Item2);
            Console.WriteLine(tuples.Item3);

            // Mit named tuples muessen wir nicht mehr die generischen Namen "Item1", "Item2", "Item3" verwenden
            var namedTuples = max.VollerNameWithNamedTuples();
            Console.WriteLine(namedTuples.Vorname);
            Console.WriteLine(namedTuples.ZweiterName);
            Console.WriteLine(namedTuples.Nachname);

            PersonUsingTuples martina = new()
            {
                Vorname = "Martina",
                Nachname = "Mustermann",
                ZweiterName = "Mia"
            };

            // Jetzt koennen wir mit dekonstruierende Zuweiseung sinnvolle Variablen Namen verwenden und Tuples sinnvoll verwenden
            var (vorname, zweiterName, nachname) = martina.VollerName();
            Console.WriteLine(vorname);
            Console.WriteLine(zweiterName);
            Console.WriteLine(nachname);
        }

        private static void LocalFunctionSample()
        {
            WertVerdoppeln(5_345);

            var result = Fibonacci(5);
            Console.WriteLine($"Fibonacci(5) = {result}");
        }

        public static void WertVerdoppeln(int wert = 123)
        {
            // Actions oder Delegates um "lokale Funktionen" zu definieren
            Action doubleValue = () => wert *= 2;
            doubleValue();
            Console.WriteLine(wert);

            // Jetzt koennen sogar lokale Funktionen wie hier definiert werden.
            // Sie ist nur im Scope dieser Methode verfuegbar, also nur innerhalb von "WertVerdoppeln"
            void localFunc()
            {
                wert *= 2;
            }
            localFunc();
            Console.WriteLine(wert);
        }

        public static int Fibonacci(int startValue)
        {
            return FibonacciRecursive(startValue).currentValue;

            static (int currentValue, int previousValue) FibonacciRecursive(int i)
            {
                if (i == 0)
                {
                    return (1, 0);
                }
                else
                {
                    var (current, previous) = FibonacciRecursive(i - 1);
                    return (current + previous, current);
                }
            }
        }
    }
}
