using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PedigreeObjects;
using System.Collections.Generic;
using System.Linq;

namespace PedigreeObjectsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructor1()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Dominant, Dominance.Recessive,rng);
            Assert.AreEqual("Cc", gt.ToString());
        }
        [TestMethod]
        public void TestConstructor2()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Dominant, Dominance.Dominant,rng);
            Assert.AreEqual("CC", gt.ToString());
        }
        [TestMethod]
        public void TestConstructor3()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Recessive, Dominance.Dominant,rng);
            Assert.AreEqual("Cc", gt.ToString());
        }
        [TestMethod]
        public void TestConstructor4()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Recessive, Dominance.Recessive,rng);
            Assert.AreEqual("cc", gt.ToString());
        }
        [TestMethod]
        public void TestCombineGenotype1()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Dominant, Dominance.Dominant,rng);
            var otherGt = new Genotype('C', Dominance.Recessive, Dominance.Recessive,rng);
            var resultingGenotype = gt.CombineGenotypes(otherGt);
            TestGenotypeCombinationRatio(gt, otherGt, "Cc", 1.0m);
        }
        [TestMethod]
        public void TestCombineGenotype2()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Recessive, Dominance.Recessive,rng);
            var otherGt = new Genotype('C', Dominance.Recessive, Dominance.Recessive,rng);
            var resultingGenotype = gt.CombineGenotypes(otherGt);
            TestGenotypeCombinationRatio(gt, otherGt, "cc", 1.0m);
        }
        [TestMethod]
        public void TestCombineGenotype3()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Recessive, Dominance.Recessive,rng);
            var otherGt = new Genotype('C', Dominance.Dominant, Dominance.Dominant,rng);
            var resultingGenotype = gt.CombineGenotypes(otherGt);
            TestGenotypeCombinationRatio(gt, otherGt, "Cc", 1.0m);
        }
        [TestMethod]
        public void TestCombineGenotype4()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Dominant, Dominance.Recessive,rng);
            var otherGt = new Genotype('C', Dominance.Recessive, Dominance.Recessive,rng);
            TestGenotypeCombinationRatio(gt, otherGt, "Cc", 0.50m);
            TestGenotypeCombinationRatio(gt, otherGt, "cc", 0.50m);
           
        }
        [TestMethod]
        public void TestCombineGenotype5()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Dominant, Dominance.Recessive,rng);
            var otherGt = new Genotype('C', Dominance.Dominant, Dominance.Recessive,rng);
            TestGenotypeCombinationRatio(gt, otherGt,"CC",0.25m);
            TestGenotypeCombinationRatio(gt, otherGt, "Cc", 0.50m);
            TestGenotypeCombinationRatio(gt, otherGt, "cc", 0.25m);
        }

        private static void TestGenotypeCombinationRatio(Genotype gt, Genotype otherGt, string outcome, decimal frequency)
        {
            var list = new List<Genotype>();
            for (int i = 0; i < 1000; i++)
            {
                var resultingGenotype = gt.CombineGenotypes(otherGt);
                list.Add(resultingGenotype);
            }
            decimal actual = list.Count(g => g.ToString() == outcome)/1000m;
            
            Assert.AreEqual(frequency,actual);
        }

    }
}
