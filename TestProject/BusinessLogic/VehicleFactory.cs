using LinqSamples.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.UnitsUnderTest
{
    internal class VehicleFactory
    {
        private readonly IVehicleService _vehicleService;
        private static int CurrentOrderId = 0;

        public VehicleFactory(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public VehicleOrder CreateOrder(string customerName)
        {
            if (!string.IsNullOrEmpty(customerName))
            {
                return new VehicleOrder(CurrentOrderId++, customerName);
            }

            return null;
        }

        public Car[] CreateCarFleet()
        {
            return new[]
            {
                _vehicleService.CreateCar("Toyota", "Camry"),
                _vehicleService.CreateCar("Ford", "Mustang"),
                _vehicleService.CreateCar("Nissan", "Altima")
            };
        }
    }

    public record VehicleOrder(int OrderId, string CustomerName);
}
