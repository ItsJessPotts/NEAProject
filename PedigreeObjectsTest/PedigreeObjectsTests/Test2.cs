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
            var fullListOfgenotypes = new List<Genotype>();
            fullListOfgenotypes.Add(pHet);
            p.TraitGenotypes = fullListOfgenotypes;
            p.GeneratePhenotypeName();
            Assert.AreEqual(p.PhenotypeName, "Haemphilia carrier");
        }
    }
}
