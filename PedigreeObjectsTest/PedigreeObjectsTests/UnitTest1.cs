using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PedigreeObjects;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
        [TestMethod]//NEED TO: Add more tests
        public void TestCalculatePhenotypicRatio1()
        {
            var rng = new PredictableRandomNumberGenerator();
            var gt = new Genotype('C', Dominance.Dominant, Dominance.Recessive,rng); //Cc
            var otherGt = new Genotype('C', Dominance.Recessive, Dominance.Recessive,rng);//cc
            Phenotype p = new Phenotype(null, null,null);// TO DO: fill in
            string phenotypicRatio = p.CalculatePhenotypicRatio(otherGt, gt, 2); //Aa Bb
            Assert.AreEqual("1:3", phenotypicRatio);
        }
        [TestMethod]//TO DO: Test Person Functions
        public void TestPersonToString()
        {
            var j = new Person("Jess Potts", Sex.Female, true);
            string jess = j.ToString();
            Assert.AreEqual("Jess Potts Female", jess);
        }
        [TestMethod]
        public void TestAddPerson()
        {
            var j = new Person("Jess Potts", Sex.Female, true);
            var e = new Person("Erica Korner", Sex.Female, true);
            var p = new Person("paul Korner", Sex.Female, true);
            var rep = new PersonRepository();
            rep.AddPerson(j);
            rep.AddPerson(e);
            var result = rep.ListPersons();

            Assert.IsTrue(result.Contains(j));
            Assert.IsTrue(result.Contains(e));
            Assert.IsFalse(result.Contains(p));
        }
        [TestMethod]
        public void TestReturnPerson()
        {
            var j = new Person("Jess Potts", Sex.Female, true);
            var e = new Person("Erica Korner", Sex.Female, true);
            var p = new Person("Paul Korner", Sex.Female, true);
            var rep = new PersonRepository();
            rep.AddPerson(j);
            rep.AddPerson(e);

            var testList = new List<Person>();
            testList.Add(j);
            testList.Add(e);
            var result = rep.ListPersons();
                       
            CollectionAssert.AreEqual(testList,result);
            
        }
        [TestMethod]
        public void TestReadingSeedData()
        {
            string[] lines = new string[1000];
            using (StreamReader reader = new StreamReader("PersonsSeedData.txt"))
            {
                int i = 0;
                while (!reader.EndOfStream)
                {
                    lines[i] = reader.ReadLine();
                }
                Assert.AreEqual("Abigail Adams,Female,true", lines[i]);
            }
            var expectedCollection = new string[1000];
           
        }
        [TestMethod]
        public void TestTurnRecordsFileIntoPersonRepository()
        {
            PersonRepository personRepository = new PersonRepository();
            string[] records = new string[1000];
            records = PersonRepository.ReadFile(records);
            PersonRepository myPersonRepository = PersonRepository.TurnRecordsFileIntoPersonRepository(records, personRepository);

            var ListofPersons = new List<Person>();
            ListofPersons = myPersonRepository.ListPersons();
            var TestPerson = new Person("Abigail Adams",Sex.Female, true);

            Assert.AreEqual(ListofPersons[0], TestPerson);
        }






    }
}
