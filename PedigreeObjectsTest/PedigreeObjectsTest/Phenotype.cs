using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Phenotype//NEED TO: Test Phenotypic Ratio
    {
        private IRandomNumberGenerator RNG { get; set; }//For Ratio calculation
        public string[] Traits { get; set; } //Array of traits possessed by the person
        public string[] Environment { get; set; } //Array of environmental influences that could be effecting the phenotype
        public Genotype[] TraitGenotypes { get; set; } // The actual genotypes possessed: Aa, BB, Cc, Dd, EE

        public Phenotype(IRandomNumberGenerator rng, string[] traits, string[] environment, Genotype[] traitGenotypes)
        {
            IRandomNumberGenerator RNG = rng;
            string[] Trait = traits;
            string[] Environment = environment;
            Genotype[] TraitGenotypes = traitGenotypes;
        }

        public string CalculatePhenotypicRatio(Genotype parent1, Genotype parent2, int NumberOfPhenotypeOptions) //Aa Bb
        {
            var choice = RNG.Next(0, 4);
            Dominance resultingAllele1;
            Dominance resultingAllele2;
            int isRecessive = 0;
            string formattedRatio = "";
                               
            switch (choice)
            {                
                case 0:
                    resultingAllele1 = parent2.Allele1; //A
                    resultingAllele2 = parent1.Allele1; //B
                    
                    break;
                case 1:
                    resultingAllele1 = parent2.Allele2; //b
                    resultingAllele2 = parent1.Allele1; //A
                    
                    break;

                case 2:
                    resultingAllele1 = parent2.Allele1; //B
                    resultingAllele2 = parent1.Allele2; //a
                    

                    break;
                case 3:
                    resultingAllele1 = parent2.Allele2; //b
                    resultingAllele2 = parent1.Allele2; //a
                    isRecessive ++;
                    break;
                default:
                    throw new Exception("Invalid Choice");
            }
            if (NumberOfPhenotypeOptions == 2) //normal monohyprid cross= has Trait/doesn't
            {                
                var gcd = GCD(4-isRecessive,isRecessive);
                formattedRatio = string.Format("{0}:{1}", (4-isRecessive / gcd).ToString(), (isRecessive / gcd).ToString());
            }
            return formattedRatio;
            
           
        }
        public int GCD(int a, int b)

        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            if (a == 0)
                return b;
            else
                return a;
        }




    }

}
