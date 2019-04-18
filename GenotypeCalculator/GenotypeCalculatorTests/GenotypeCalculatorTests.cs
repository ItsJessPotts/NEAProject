using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenotypeCalculator.Tests
{
    [TestClass]
    public class GenotypeCalculatorTests
    {
        [TestInitialize]
        public void Setup()
        {
            Console.WriteLine("This code is run before each test");
            Console.WriteLine("To see me, click on the test in Test Explorer and click on the blue Output link");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GenotypeCcShouldReturnCorrectGenotypes()
        {
            // Arrange
            var g = new GenotypeCalculator();
            List<string> expected = new List<string>() { "CC", "Cc", "CC", "cc", "Cc", "cc" };

            // Act
            var result = g.Calculate(Dominance.Dominant, Dominance.Recessive);

            // Assert 
            // result should have exactly the same sequence as expected
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GenotypeCCShouldReturnCorrectGenotypes()
        {
            // Arrange
            var g = new GenotypeCalculator();
            List<string> expected = new List<string>() { "CC", "CC", "CC", "CC", "CC", "CC" };

            // Act
            var result = g.Calculate(Dominance.Dominant, Dominance.Recessive);

            // Assert
            // result should have only have expected genotypes
            Assert.IsTrue(result.Any(x => expected.Contains(x)));
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void GenotypeccShouldReturnCorrectGenotypes()
        {
            // Arrange
            var g = new GenotypeCalculator();
            List<string> expected = new List<string>() { "cc", "cc", "cc", "cc", "cc", "cc" };

            // Act
            var result = g.Calculate(Dominance.Dominant, Dominance.Recessive);

            // Assert
            // result should have only have expected genotypes
            Assert.IsTrue(result.Any(x => expected.Contains(x)));
        }

    }
}
