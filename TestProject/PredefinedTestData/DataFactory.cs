using LinqSamples.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.PredefinedTestData
{
    public class DataFactory
    {
        public static Car[] TestData => new[]
        {
            new Car
            {
                Id = 0,
                Manufacturer = "Volkswagen",
                Model = "Golf",
                Fuel = "Diesel",
                TopSpeed = 200,
                Color = KnownColor.BlueViolet,
                Type = "Hatchback"
            },
            new Car
            {
                Id = 1,
                Manufacturer = "Volkswagen",
                Model = "Passat",
                Fuel = "Diesel",
                TopSpeed = 220,
                Color = KnownColor.RebeccaPurple,
                Type = "Sedan"
            },
            new Car
            {
                Id = 2,
                Manufacturer = "Volkswagen",
                Model = "Passat",
                Fuel = "Electric",
                TopSpeed = 320,
                Color = KnownColor.RosyBrown,
                Type = "Coupe"
            }
        };
    }
}
