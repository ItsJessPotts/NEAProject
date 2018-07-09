using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Phenotype//NEED TO: Implement Phenotypic Ratio and Punnet Square, As well as see if Genotypes can be manipulated outside of class.
    {
        private IRandomNumberGenerator RNG { get; set; }//For Ratio calculation
        public string[] Traits { get; set; } //Array of traits possessed by the person
        public string[] Environment { get; set; } //Array of environmental influences that could be effecting the phenotype
        public Genotype[] TraitGenotypes { get; set; } // The actual genotypes possessed: Aa, BB, Cc, Dd, EE

        public int CalculatePhenotypicRatio(Genotype other, Genotype owner)
        {
            return 0;
        }
        public void CalculatePunnetSquare(Genotype other, Genotype owner)
        {
            
        }
       

    }

}
