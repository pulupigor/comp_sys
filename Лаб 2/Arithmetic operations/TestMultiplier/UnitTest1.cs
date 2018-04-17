using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arithmetic_operations;

namespace TestMultiplier
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMultiplier()
        {

            Assert.AreEqual(Multiplication.ShiftingRight(2, -3), -6);
        }
    }
}
