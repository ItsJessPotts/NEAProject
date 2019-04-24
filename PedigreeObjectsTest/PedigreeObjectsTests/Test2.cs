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

        [TestMethod]
        public void TestCombineGenotypes()
        {
            var f = new GenoTypeTestFixture();
            f.SetUp();
            var a = new Genotype("A", Dominance.Dominant, Dominance.Recessive);
            var a2 = new Genotype("A", Dominance.Dominant, Dominance.Dominant);
            var expected = new Genotype("A", Dominance.Dominant, Dominance.Dominant);

             var result = a.MostLikelyGenotype(a2, f.genotypeRepository,f.rng);

            Assert.AreEqual(expected.ToString(),result.ToString());
        }
        [TestMethod]
        public void TestGenerateGenericGenotypesForAGenotype()
        {
            var f = new GenoTypeTestFixture();
            f.SetUp();

            var expected = new List<Genotype>();
            Genotype tDom = new Genotype("T", Dominance.Dominant, Dominance.Dominant);
            Genotype tHet = new Genotype("T", Dominance.Dominant, Dominance.Recessive);
            Genotype tRec = new Genotype("T", Dominance.Recessive, Dominance.Recessive);
            Genotype tHet2 = new Genotype("T", Dominance.Recessive, Dominance.Dominant);

            expected.Add(tHet);
            expected.Add(tDom);
            expected.Add(tRec);
            expected.Add(tHet2);

            Genotype tgenotype = new Genotype("T", Dominance.Recessive, Dominance.Recessive);

            List<Genotype> genericGenotypes = tgenotype.CreateGenericGenotypes();

                      
            Assert.AreEqual(expected[0].ToString(),genericGenotypes[0].ToString());
            Assert.AreEqual(expected[1].ToString(), genericGenotypes[1].ToString());
            Assert.AreEqual(expected[2].ToString(), genericGenotypes[2].ToString());
            Assert.AreEqual(expected[3].ToString(), genericGenotypes[3].ToString());

        }
        
    }
}
