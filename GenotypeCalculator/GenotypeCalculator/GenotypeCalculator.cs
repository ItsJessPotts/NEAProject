using System.Collections.Generic;

namespace GenotypeCalculator
{
    public enum Dominance
    {
        Dominant,
        Recessive
    }
    public class GenotypeCalculator
    {
        public List<string> Genotypes { get; set; }

        public GenotypeCalculator()
        {
            Genotypes = Calculate(Dominance.Dominant, Dominance.Recessive);
        }

        public List<string> Calculate(Dominance allele1, Dominance allele2)
        {
            if (allele1 != allele2)
                return new List<string>() { "CC", "Cc", "CC", "cc", "Cc", "cc" };
            else if (allele1 == Dominance.Dominant && allele2 == Dominance.Dominant)
                return new List<string>() { "CC", "CC", "CC", "CC", "CC", "CC" };
            else if (allele1 == Dominance.Recessive && allele2 == Dominance.Recessive)
                return new List<string>() { "cc", "cc", "cc", "cc", "cc", "cc" };
            else
                return new List<string>();
        }

        public override string ToString()
        {
            string result = null;
            foreach (var genotype in Genotypes)
            {
                result += genotype + " ";
            }

            return result;
        }
    }
}