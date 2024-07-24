using TestProject.BusinessLogic;

namespace TestProject.Tests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        [DataRow(1, 2, 3)]
        [DataRow(2, 3, 5)]
        [DataRow(3, 4, 7)]
        [DataRow(4, 5, 9)]
        public void Add_ReturnsSum(int expectedOparand1, int expectedOperand2, int expectedResult)
        {
            // Arrange
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