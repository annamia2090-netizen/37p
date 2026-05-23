using System;
using пр37;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SalaryTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetSalaryByPosition()
        {
            decimal result = SalaryCalculator.GetSalaryByPosition("Ассистент");
            Assert.AreEqual(3880m, result);
        }

        [TestMethod]
        public void TestCalculateAll()
        {
            var result = SalaryCalculator.CalculateAll(3880m, 1552m);
            Assert.AreEqual(5434.716m, result.toPay);
        }
    }
}
