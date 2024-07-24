using LinqSamples.Data;

namespace TestProject.UnitsUnderTest
{
    public interface IVehicleService
    {
        Car CreateCar(string make, string model);
    }
}