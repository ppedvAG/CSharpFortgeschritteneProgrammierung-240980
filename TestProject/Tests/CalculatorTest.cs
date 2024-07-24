using TestProject.BusinessLogic;

namespace TestProject.Tests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void Add_ReturnsSum()
        {
            // Arrange
            var expectedOparand1 = 1;
            var expectedOperand2 = 2;
            var expectedResult = 3;
            var calculator = new Calculator();

            // Act
            var result = calculator.Add(expectedOparand1, expectedOperand2);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void CrossTotal_ReturnsCrossTotal()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.CrossTotal(12345);

            // Assert
            Assert.AreEqual(15, result);
        }
    }
}