using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class TraitRepository
    {
        private GeneticCounsellorDbContext Db { get; set; }

        public TraitRepository(GeneticCounsellorDbContext db)
        {
            Db = db;
        }

        public Trait AddTrait(string traitName, string alleleName, Dominance inheritanceType)
        {
            var t = new Trait();
            t.TraitName = traitName;
            t.AlleleName = alleleName.ToUpper();
            t.InheritanceType = inheritanceType;
            Db.Traits.Add(t);
            Db.SaveChanges();
            return t;
        }
        public List<Trait> ListTraits()
        {
            return Db.Traits.ToList();
        }
        public void DeleteTrait(Trait t)
        {
            Db.Traits.Remove(t);
            Db.SaveChanges();
        }



    }   
}
