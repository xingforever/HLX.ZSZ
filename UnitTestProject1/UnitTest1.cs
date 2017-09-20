using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cla;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(2, Class1.Add(1, 1));
            Assert.AreEqual(1, Class1.Add(1, 0));
            Assert.AreEqual(4, Class1.Add(0, 4));
        }
    }
}
