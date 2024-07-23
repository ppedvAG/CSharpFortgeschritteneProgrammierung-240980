using Bogus.DataSets;
using Bogus;
using System.Drawing;
using Serialization.Models;
using System.Xml.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using Bogus.Bson;

namespace Serialization
{
    internal class Program
    {
        public static readonly Faker _faker = new Faker();
        public const int ReservedSystemColorNameCount = 27;
        public static readonly int ColorCount = Enum.GetValues(typeof(KnownColor)).Length;

        static async Task Main(string[] args)
        {
            var carList = Enumerable.Range(0, 10).Select(GenerateVehicle).ToArray();

            //XmlSample(carList, "cars.xml");

            Console.WriteLine("\n\nSample System.Text.Json");
            JsonSample(carList);

            Console.WriteLine("\n\nSample Newtonsoft.Json");
            NewtonsoftJsonSample(carList);
        }

        private static void XmlSample<T>(T[] carList, string fileName) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T[]));

            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, carList);
            }

            using (var reader = new StreamReader(fileName))
            {
                var result = serializer.Deserialize(reader) as T[];

                foreach (var car in result)
                {
                    Console.WriteLine(car.ToString());
                }
            }
        }

        private static async Task JsonSample<T>(T[] carList)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = System.Text.Json.JsonSerializer.Serialize(carList, options);
            await File.WriteAllTextAsync("cars.json", json);

            var fileContent = await File.ReadAllTextAsync("cars.json");
            var result = System.Text.Json.JsonSerializer.Deserialize<T[]>(fileContent);
            foreach (var car in result)
            {
                Console.WriteLine(car.ToString());
            }
        }

        private static async Task NewtonsoftJsonSample<T>(T[] carList)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects // Vererbung ermoeglichen
            };

            var json = JsonConvert.SerializeObject(carList, settings);
            await File.WriteAllTextAsync("cars_newtonsoft.json", json);

            var fileContent = await File.ReadAllTextAsync("cars_newtonsoft.json");
            var result = JsonConvert.DeserializeObject<T[]>(fileContent);
            foreach (var car in result)
            {
                Console.WriteLine(car.ToString());
            }
        }

        public static Car GenerateVehicle(int id)
        {
            var topSpeed = Random.Shared.Next(10, 25) * 10;
            var color = (KnownColor)Random.Shared.Next(ReservedSystemColorNameCount, ColorCount);
            return new Car
            {
                Id = id,
                Manufacturer = _faker.Vehicle.Manufacturer(),
                Model = _faker.Vehicle.Model(),
                Type = _faker.Vehicle.Type(),
                Fuel = _faker.Vehicle.Fuel(),
                TopSpeed = topSpeed,
                Color = color
            };
        }
    }
}
