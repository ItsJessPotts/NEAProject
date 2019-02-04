using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PedigreeObjects;

namespace PedigreeObjectsTests
{
    [TestClass]
    public class Test2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var p = new Phenotype();
            var pHet = new Genotype("P",Dominance.Dominant, Dominance.Recessive);           
            var fullListgenotypes = new List<Genotype>();
            fullListgenotypes.Add(pHet);
            p.TraitGenotypes = fullListgenotypes;
            p.GeneratePhenotypeName();
            Assert.AreEqual(p.PhenotypeName, "Phenotype: Haemphilia carrier");
        }
    }
}
