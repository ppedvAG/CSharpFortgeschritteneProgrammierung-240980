using LinqSamples.Data;

namespace TestProject.UnitsUnderTest
{
    public class VehicleService : IVehicleService
    {
        public Car CreateCar(string make, string model)
        {
            return new Car
            {
                Manufacturer = make,
                Model = model
            };
        }
    }
}
