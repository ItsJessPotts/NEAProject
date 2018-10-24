using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class PhenotypeRepository
    {
        private List<Phenotype> Phenotypes = new List<Phenotype>();

        public void AddPhenotype(Phenotype phenotype)
        {
            Phenotypes.Add(phenotype);
            db.Phenotype.InsertOnSubmit();
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Make some adjustments.
                // ...
                // Try again.
                db.SubmitChanges();
            }
        }
        public List<Phenotype> ListPhenotypes()
        {
            return Phenotypes;
        }
    }
}
