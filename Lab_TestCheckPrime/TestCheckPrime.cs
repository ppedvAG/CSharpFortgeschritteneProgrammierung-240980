using Labs;

namespace Lab_TestCheckPrime
{
    [TestClass]
    public class TestCheckPrime
    {
        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(11)]
        [DataRow(13)]
        [DataRow(17)]
        [DataRow(19)]
        [DataRow(23)]
        [DataRow(29)]
        [DataRow(31)]
        [DataRow(37)]
        [DataRow(41)]
        [DataRow(int.MaxValue)]
        public void CheckPrime_ReturnsTrue(int input)
        {
            // Arrange 
            var primeComponent = new PrimeComponent();

            // Act
            var result = primeComponent.CheckPrime(input);

            // Assert
            Assert.IsTrue(result, $"Expected {input} to be prime");
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(4)]
        [DataRow(123456789)]
        public void CheckPrime_ReturnsFalse(int input)
        {
            // Arrange 
            var primeComponent = new PrimeComponent();

            // Act
            var result = primeComponent.CheckPrime(input);

            // Assert
            Assert.IsFalse(result, $"Expected {input} to be not a prime");
        }
    }
}