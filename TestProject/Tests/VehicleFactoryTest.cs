using LinqSamples.Data;
using Moq;
using System.Text.Json;
using TestProject.UnitsUnderTest;

namespace TestProject.Tests
{
    public record OrderDataTestArg(string Name, int Days);

    [TestClass]
    public class VehicleFactoryTest
    {
        // Dieses Property wird vom Framework automatisch injiziert. Es muss exakt so heiﬂen und von diesem Typ sein.
        public TestContext TestContext { get; set; }

        private static IEnumerable<object[]> CreateOrderParameters
        {
            get
            {
                yield return new object[] { new OrderDataTestArg("Max Mustermann", 10) };
                yield return new object[] { new OrderDataTestArg("John Doe", 20) };
            }
        }

        [TestMethod]
        [DynamicData(nameof(CreateOrderParameters), DynamicDataSourceType.Property)]
        public void CreateOrder_ReturnsValidOrder(OrderDataTestArg order)
        {
            // Arrange
            var expectedDate = DateTime.Now.AddDays(order.Days);
            var mockService = Mock.Of<IVehicleService>();
            var factory = new VehicleFactory(mockService);

            // Act
            var result = factory.CreateOrder(order.Name, order.Days);

            // Assert
            Assert.IsNotNull(result, "Expected a valid order record.");
            Assert.AreEqual(order.Name, result.CustomerName);
            Assert.AreEqual(expectedDate.Day, result.DueDate.Value.Day);

            ExportTestResults(result);
        }

        [TestMethod]
        public void CreateOrder_ReturnsNull()
        {
            // Arrange
            var mockService = Mock.Of<IVehicleService>();
            var factory = new VehicleFactory(mockService);

            // Act
            var result = factory.CreateOrder("");

            // Assert
            Assert.IsNull(result, "Expected no order record.");

            ExportTestResults(result);
        }

        [TestMethod]
        public void CreateCarFleet_Returns3Vehicles()
        {
            // Arrange
            var expectedModelName = "Test Model";
            var mockService = new Mock<IVehicleService>();
            mockService
                .Setup(m => m.CreateCar(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Car
                {
                    // TestName gibt den Namen der aktuellen Testmethode zurueck
                    Manufacturer = TestContext.TestName,
                    Model = expectedModelName,
                    Color = System.Drawing.KnownColor.Transparent
                });
            var factory = new VehicleFactory(mockService.Object);

            // Act
            var result = factory.CreateCarFleet();

            // Assert
            Assert.IsTrue(result.Any(), "Expected any vehicles");
            Assert.AreEqual(3, result.Count(), "Expected 3 vehicles");
            Assert.IsNotNull(result[0], "Expected first vehicle not null");
            Assert.IsTrue(result.All(v => v.Model == expectedModelName), "Expected all model names to be " + expectedModelName);

            ExportTestResults(result);
        }

        private void ExportTestResults<T>(T result)
        {
            // Wir koennen unsere Testergbnisse in eine Datei speichern und fuer jeden Test ausgeben lassen
            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
            var path = Path.Combine(TestContext.TestRunDirectory, TestContext.TestName + ".json");
            File.WriteAllText(path, json);
        }
    }
}