using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PedigreeObjects;

namespace PedigreeObjectsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructor1()
        {
            var gt = new Genotype('C',Dominance.Dominant,Dominance.Recessive );
            Assert.AreEqual("Cc", gt.ToString());
        }
        [TestMethod]
        public void TestConstructor2()
        {
            var gt = new Genotype('C', Dominance.Dominant, Dominance.Dominant);
            Assert.AreEqual("CC", gt.ToString());
        }
        [TestMethod]
        public void TestConstructor3()
        {
            var gt = new Genotype('C', Dominance.Recessive, Dominance.Dominant);
            Assert.AreEqual("Cc", gt.ToString());
        }
        [TestMethod]
        public void TestConstructor4()
        {
            var gt = new Genotype('C', Dominance.Recessive, Dominance.Recessive);
            Assert.AreEqual("cc", gt.ToString());
        }


    }
}
