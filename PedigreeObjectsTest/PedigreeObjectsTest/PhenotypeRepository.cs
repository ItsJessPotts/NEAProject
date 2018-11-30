using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class PhenotypeRepository
    {
        private GeneticCounsellorDbContext Db { get; set; }

        public PhenotypeRepository(GeneticCounsellorDbContext db)
        {
            Db = db;
        }

        public Phenotype AddPhenotype(string phenotypeName)
        {
            var p = new Phenotype();
           //p.GenotypeName
            Db.Phenotypes.Add(p);
            Db.SaveChanges();
            return p;
        }

        public List<Phenotype> ListPhenotypes()
        {
            return Db.Phenotypes.ToList();
        }
    }
}
