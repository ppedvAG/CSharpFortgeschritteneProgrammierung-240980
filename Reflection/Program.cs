using LinqSamples.Data;
using System.Reflection;
using System.Text;

namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welche Properties hat die Klasse Car?");

            var car = new Car
            {
                Manufacturer = "VW",
                Model = "Passat",
                Type = "SUV",
                Fuel = "Diesel",
                TopSpeed = 200
            };

            string properties = GetPropertyDescription(car);
            Console.WriteLine(properties);

            Console.WriteLine("\n\nListe von Typen in unserer Assembly:");
            var types = Assembly.GetAssembly(typeof(Car)).GetTypes();
            types.ToList().ForEach(Console.WriteLine);

            Console.WriteLine($"\nListe von Typen mit dem Attribute {nameof(OurAwesomeCarAttribute)}");
            var customObjectsWithAttribute = types.Where(t => t.GetCustomAttribute<OurAwesomeCarAttribute>() != null).ToList();
            customObjectsWithAttribute.ForEach(Console.WriteLine);

            var attr = customObjectsWithAttribute.First().GetCustomAttribute<OurAwesomeCarAttribute>();
            Console.WriteLine($"\n{attr.YourMood} {attr.LuckyNumber}");
        }

        private static string GetPropertyDescription<T>(T obj)
        {
            // besser typeof(T) arbeiten weil obj potentiell null sein kann
            //var type = obj.GetType();

            return typeof(T)
                .GetProperties()
                .Aggregate(new StringBuilder(), (sb, prop) => sb.AppendLine($"{prop.PropertyType.Name} {prop.Name} {prop.GetValue(obj)}"))
                .ToString();
        }
    }
}
