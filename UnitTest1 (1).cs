using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using пр37;

namespace SalaryApp
{
    [TestClass]
    public class SalaryCalculatorTests
    {
        [TestMethod]
        public void GetSalaryByPosition_Assistant_Returns3880()
        {
            // Arrange
            string position = "Ассистент";

            // Act
            decimal result = SalaryCalculator.GetSalaryByPosition(position);

            // Assert
            Assert.AreEqual(3880m, result);
        }

        [TestMethod]
        public void GetSalaryByPosition_Professor_Returns6003()
        {
            // Arrange
            string position = "Профессор";

            // Act
            decimal result = SalaryCalculator.GetSalaryByPosition(position);

            // Assert
            Assert.AreEqual(6003m, result);
        }

        [TestMethod]
        public void GetSalaryByPosition_InvalidPosition_ReturnsZero()
        {
            // Arrange
            string position = "Директор";

            // Act
            decimal result = SalaryCalculator.GetSalaryByPosition(position);

            // Assert
            Assert.AreEqual(0m, result);
        }

        [TestMethod]
        public void CalculateAllowances_OnlyDocent_Returns40Percent()
        {
            // Arrange
            decimal salary = 3880m;

            // Act
            decimal result = SalaryCalculator.CalculateAllowances(
                salary, true, false, false, false, false, false, false, false, false);

            // Assert
            Assert.AreEqual(1552m, result);
        }

        [TestMethod]
        public void CalculateAllowances_WithDocentAndMethodLit_ReturnsCorrectSum()
        {
            // Arrange
            decimal salary = 3880m;

            // Act
            decimal result = SalaryCalculator.CalculateAllowances(
                salary, true, false, false, false, false, false, false, false, true);

            // Assert
            Assert.AreEqual(1652m, result);
        }

        [TestMethod]
        public void CalculateAll_ValidInput_ReturnsCorrectToPay()
        {
            // Arrange
            decimal salary = 3880m;
            decimal allowances = 1552m;

            // Act
            var result = SalaryCalculator.CalculateAll(salary, allowances);

            // Assert
            Assert.AreEqual(5432m, result.accrual);
            Assert.AreEqual(814.8m, result.ural);
            Assert.AreEqual(812.084m, result.tax);
            Assert.AreEqual(5434.716m, result.toPay);
        }

        [TestMethod]
        public void CalculateAll_ZeroSalary_ReturnsZero()
        {
            // Arrange
            decimal salary = 0m;
            decimal allowances = 0m;

            // Act
            var result = SalaryCalculator.CalculateAll(salary, allowances);

            // Assert
            Assert.AreEqual(0m, result.accrual);
            Assert.AreEqual(0m, result.ural);
            Assert.AreEqual(0m, result.tax);
            Assert.AreEqual(0m, result.toPay);
        }
    }
}