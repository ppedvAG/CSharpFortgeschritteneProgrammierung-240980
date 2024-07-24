using Reflection;
using System.Drawing;

namespace LinqSamples.Data
{
    [OurAwesomeCar("Happy", 37)]
    public class Car
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public string Fuel { get; set; }

        public int TopSpeed { get; set; }

        public KnownColor Color { get; set; }

        public override string ToString()
        {
            return $"{Manufacturer} - {Model} - {Type} - {Fuel} - TopSpeed: {TopSpeed} - Color: {Color}";
        }
    }

    public class Foo
    {

    }

    public interface IBar
    {

    }
}
