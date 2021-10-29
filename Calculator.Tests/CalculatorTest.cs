using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorNS;

namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void Add()
        {
            // Arrange
            string statement = "1 + 1 + 4";
            string statement2 = "11.1 + 23";
            decimal expectedResult = 6m;
            decimal expectedResult2 = 34.1m;

            // Act
            decimal result = CalculatorNS.Calculator.Calculate(statement);
            decimal result2 = CalculatorNS.Calculator.Calculate(statement2);

            // Assert
            Assert.AreEqual(result, expectedResult);
            Assert.AreEqual(result2, expectedResult2);
        }

        [TestMethod]
        public void Minus()
        {
            // Arrange
            string statement = "5 - 1 - 2";
            decimal expectedResult = 2;

            // Act
            decimal result = CalculatorNS.Calculator.Calculate(statement);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void Multiply()
        {
            // Arrange
            string statement = "1 * 1 * 2";
            decimal expectedResult = 2;

            // Act
            decimal result = CalculatorNS.Calculator.Calculate(statement);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void Divide()
        {
            // Arrange
            string statement = "10 / 4 / 5";
            decimal expectedResult = 0.5m;

            // Act
            decimal result = CalculatorNS.Calculator.Calculate(statement);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void CombinationOfSimpleOperators()
        {
            // Arrange
            string statement = "1 + 1 * 3 / 3 - 1 + 5";
            decimal expectedResult = 6;

            // Act
            decimal result = CalculatorNS.Calculator.Calculate(statement);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void ExpressionWithBracket()
        {
            // Arrange
            string statement = "( 11.5 + 15.4 ) + 10.1";
            string statement2 = "23 - ( 29.3 - 12.5 )";
            string statement3 = "( 1 / 2 ) - 1 + 1";
            decimal expectedResult = 37m;
            decimal expectedResult2 = 6.2m;
            decimal expectedResult3 = 0.5m;

            // Act
            decimal result = CalculatorNS.Calculator.Calculate(statement);
            decimal result2 = CalculatorNS.Calculator.Calculate(statement2);
            decimal result3 = CalculatorNS.Calculator.Calculate(statement3);

            // Assert
            Assert.AreEqual(result, expectedResult);
            Assert.AreEqual(result2, expectedResult2);
            Assert.AreEqual(result3, expectedResult3);
        }

        [TestMethod]
        public void ExpressionWithNestedBracket()
        {
            // Arrange
            string statement = "10 - ( 2 + 3 * ( 7 - 5 ) )";
            decimal expectedResult = 2;

            // Act
            decimal result = CalculatorNS.Calculator.Calculate(statement);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }
    }
}
