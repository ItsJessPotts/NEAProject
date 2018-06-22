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
        private char AlleleName { get; set; } //eg C for colourblindness or A for asthma
        private Dominance Allele1 { get; set; } //If dominant- inputted here
        private Dominance Allele2 { get; set; }

        public Genotype(char alleleName, Dominance allele1, Dominance allele2)
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
        {
            throw new NotImplementedException();
        }

    }

}
