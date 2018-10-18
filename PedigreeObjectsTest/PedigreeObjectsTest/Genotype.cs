using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    //The pairs of alelles that express a specific phenotype: Aa Or Bb or Cc
    public class Genotype
    {
        private IRandomNumberGenerator RNG { get; set; }
        private char AlleleName { get; set; } //eg C for colourblindness or A for asthma
        public Dominance Allele1 { get; set; } //If dominant- inputted here
        public Dominance Allele2 { get; set; }

        public Genotype(char alleleName, Dominance allele1, Dominance allele2, IRandomNumberGenerator rng)
        {
            AlleleName = alleleName;
            if (allele1 == Dominance.Recessive && allele2 == Dominance.Dominant)
            {
                Allele1 = allele2;
                Allele2 = allele1;
            }
            else
            {
                Allele1 = allele1;
                Allele2 = allele2;
            }
            RNG = rng;
        }

        public override string ToString()
        {
            return AlleleAsString(Allele1) + AlleleAsString(Allele2);
        }
        private string AlleleAsString(Dominance allele)
        {
            if (allele == Dominance.Dominant)
            {
                return AlleleName.ToString();
            }
            else
            {
                return AlleleName.ToString().ToLower();
            }
        }
        public Genotype CombineGenotypes(Genotype other)
        {//TO DO: CHECK THAT OTHER GENOTYPE HAS THE SAME NAME
            
            var choice = RNG.Next(0, 4);
            Dominance resultingAllele1;
            Dominance resultingAllele2;
            switch (choice)
            {
                case 0:
                    resultingAllele1 = this.Allele1;
                    resultingAllele2 = other.Allele1;
                    break;
                case 1:
                    resultingAllele1 = this.Allele2;
                    resultingAllele2 = other.Allele1;
                    break;
                case 2:
                    resultingAllele1 = this.Allele1;
                    resultingAllele2 = other.Allele2;
                    break;
                case 3:
                    resultingAllele1 = this.Allele2;
                    resultingAllele2 = other.Allele2;
                    break;
                default:
                    throw new Exception("Invalid Choice");
            }
            //Create Genotype given the two alelles
            var gt = new Genotype(this.AlleleName, resultingAllele1, resultingAllele2,RNG);
            return gt;
        }


        // THESE METHODS BELOW MAY NOT BE SUITED TO THIS CLASS..

        public int CalculateGenotypicRatio(Genotype gt)
        {
            return 0;
        } 
        
        public GenotypeRepository CalculateParentalGenotypes()
        {
            return null;
        }
        

    }

}
