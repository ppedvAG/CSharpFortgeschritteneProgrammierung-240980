using Bogus;
using System.Drawing;

namespace LinqSamples.Data
{
    public class CarGenerator
    {
        public static readonly Faker _faker = new Faker();
        public const int ReservedSystemColorNameCount = 27;
        public static readonly int ColorCount = Enum.GetValues(typeof(KnownColor)).Length;

        // Statischer Konstruktur der nur einmal ausgeführt wird
        static CarGenerator()
        {
            // Wir setzen einen fixen seed um immer wieder die selben Ausgangsdaten zu erzeugen (Vergleichbarkeit)
            Randomizer.Seed = new Random(37);
        }

        public static Car GenerateVehicle(int id)
        {
            var topSpeed = _faker.Random.Number(10, 25) * 10;
            var color = (KnownColor)_faker.Random.Number(ReservedSystemColorNameCount, ColorCount);
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
