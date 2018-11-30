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
        
        private GeneticCounsellorDbContext Db { get; set; }

        public GenotypeRepository(GeneticCounsellorDbContext db)
        {
            Db = db;
        }
        


        public Genotype AddGenotype(string alleleName, Dominance allele1, Dominance allele2)
        {
            var g = new Genotype();
            g.AlleleName = alleleName;
            if (allele1 == Dominance.Recessive && allele2 == Dominance.Dominant)
            {
                g.Allele1 = allele2;
                g.Allele2 = allele1;
            }
            else
            {
                g.Allele1 = allele1;
                g.Allele2 = allele2;
            }
            
            Db.Genotypes.Add(g);
            Db.SaveChanges();
            return g;
        }
        public List<Genotype> ListGenotypes()
        {
            return Db.Genotypes.ToList();
        }
        public List<Genotype> FindGenotypeByAlleleName(string AlelleName) //Find all Genotypes relating to AlleleName eg show all genotypes for C
        {
            IQueryable<Genotype> genotypeQuery =
                from genotype in Db.Genotypes
                where genotype.AlleleName == AlelleName
                select genotype;
                return genotypeQuery.ToList();
        }
    }
}
