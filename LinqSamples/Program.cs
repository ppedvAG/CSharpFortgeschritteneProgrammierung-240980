using LinqSamples.Data;
using LinqSamples.Extensions;
using System.Diagnostics;
using System.Text;

namespace LinqSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("10 Beispiele mit Yield Return");
            var sampleList = YieldSampleForLoop<Foo>(10);

            // Hier mal debuggen, dass wir sehen, dass noch nichts passiert ist
            List<Foo> actualList = sampleList.ToList();
            foreach (var item in actualList)
            {
                Console.WriteLine($"{item.Id}: {item.Name}");
            }

            Console.WriteLine("\n\n200 Fahrzeuge erstellen");
            LinqSamples();

            ExtensionMethodsSamples();
        }

        private static void LinqSamples()
        {
            var cars = Enumerable
                .Range(0, 200)
                .Select(i => CarGenerator.GenerateVehicle(i))
                .ToList();

            cars.Take(10).ToList().ForEach(c => Console.WriteLine(c));
            var avarageSpeed = cars.Average(c => c.TopSpeed);
            var maxSpeed = cars.Max(c => c.TopSpeed);
            var minSpeed = cars.Min(c => c.TopSpeed);
            Console.WriteLine($"\nAverage Speed: {avarageSpeed}\nMax Speed: {maxSpeed}\nMin Speed: {minSpeed}");

            Console.WriteLine($"Erstes Fahrzeug: {cars.First()}");
            Console.WriteLine($"Letztes Fahrzeug: {cars.Last()}");

            Console.WriteLine("\n\n10 Fahrzeuge sortieren nach TopSpeed und Model");
            var sortedCars = cars
                .OrderBy(c => c.TopSpeed)
                .ThenBy(c => c.Model)
                .ToList();
            sortedCars.Take(10).ToList().ForEach(c => Console.WriteLine(c));

            Console.WriteLine("\n\nAlle Fahrzeuge mit einem gelben Farbton.");
            cars.Where(c => c.Color.ToString().Contains("Yellow"))
                .ToList()
                .ForEach(c => Console.WriteLine(c));

            var countOrangeCars = cars.Count(c => c.Color == System.Drawing.KnownColor.Orange);
            Console.WriteLine($"Anzahl Orange-Fahrzeuge: {countOrangeCars}");

            Console.WriteLine("\n\nAutos nach Hersteller gruppiert");
            cars.GroupBy(c => c.Manufacturer)
                .Select(g => new { Manufacturer = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToList()
                .ForEach(group => Console.WriteLine($"{group.Manufacturer}: {group.Count}"));

            // Das Produkt von X bis Y
            Console.WriteLine($"\n\n1*2*3*...*10 = {Enumerable.Range(1, 10).Aggregate(1, (agg, i) => agg * i)}");

            var sb = cars.Aggregate(new StringBuilder(), (sb, car) => sb.AppendLine($"Der {car.Color} {car.Manufacturer} faehrt max. {car.TopSpeed} km/h"));
            Console.WriteLine(sb.ToString());
        }

        private static void ExtensionMethodsSamples()
        {
            var number = 12345;
            Console.WriteLine($"\n\nQuersumme von {number}: {number.Quersumme()}");
        }

        #region Yield Sample
        // Dieses Beispiel erzeugt drei Instanzen wenn das IEnumerable aufgeloest wird
        public static IEnumerable<Foo> YieldSampleSimple(int howMuch)
        {
            yield return new Foo { Id = 0, Name = "Foo0" };
            yield return new Foo { Id = 1, Name = "Foo1" };
            yield return new Foo { Id = 2, Name = "Foo2" };
        }

        public static IEnumerable<T> YieldSampleForLoop<T>(int howMuch)
            where T : IHasIdAndName, new()
        {
            var faker = new Bogus.Faker();
            for (int i = 0; i < howMuch; i++)
            {
                var instance = new T();
                instance.Id = i;
                instance.Name = faker.Name.LastName();
                yield return instance;
            }
        }

        // Statt .ToString() ueberladen besser dieses Attribut verwenden
        [DebuggerDisplay("{Id}: {Name}")]
        public class Foo : IHasIdAndName
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public interface IHasIdAndName
        {
            int Id { get; set; }
            public string Name { get; set; }
        } 
        #endregion
    }
}
