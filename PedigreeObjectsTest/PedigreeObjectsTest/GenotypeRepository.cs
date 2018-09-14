using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class GenotypeRepository //Referred to as traits on the user interface
                                    //Stored in Repos as Homozygous
    {
        private List<Genotype> Genotypes = new List<Genotype>();

        public void AddGenotype(Genotype genotype)
        {
            Genotypes.Add(genotype);
        }
        public List<Genotype> ListGenotypes()
        {
            return Genotypes;
        }
    }
}
