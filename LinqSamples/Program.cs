using System.Diagnostics;
using System.Linq;

namespace LinqSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("10 Beispiele mit Yield Return");
            var sampleList = YieldSampleForLoop<Foo>(10);

            // Hier mal debuggen, dass wir sehen, dass noch nichts passiert ist
            var actualList = sampleList.ToList();

            foreach (var item in actualList)
            {
                Console.WriteLine($"{item.Id}: {item.Name}");
            }
        }

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
    }
}
