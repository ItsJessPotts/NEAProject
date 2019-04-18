using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PedigreeObjects;
using System.Linq;

namespace PedigreeObjectsTests
{
    [TestClass]
    public class Test2
    {

        [TestMethod]
        public void CcShouldReturn6ParentsWithAny()
        {
            //Arrange
            var f = new GenoTypeTestFixture();
            f.SetUp();


            var g = new Genotype("C", Dominance.Dominant, Dominance.Recessive);
            //var gr = new GenotypeRepository();

            //Act
            var result = g.CalculateParentalGenotypes(f.genotypeRepository, f.rng);


            //Assert            
            //Assert.IsTrue(result.Any.Where(g => g);

        }
        [TestMethod]
        public void CanCompareGenotypes()
        {
            var a = new Genotype("A", Dominance.Dominant, Dominance.Recessive);
            var a2 = new Genotype("A", Dominance.Dominant, Dominance.Recessive);
            int result;

            if (a.Allele1 == a2.Allele1 && a.Allele2 == a2.Allele2 && a.AlleleName == a2.AlleleName)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }

            Assert.AreEqual(1, result);
        }
        
    }
}
