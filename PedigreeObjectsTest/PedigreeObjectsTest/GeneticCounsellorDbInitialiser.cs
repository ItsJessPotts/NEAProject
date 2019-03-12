using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PedigreeObjects
{
    public class GeneticCounsellorDbInitialiser : DropCreateDatabaseIfModelChanges<GeneticCounsellorDbContext> //DropCreateDatabaseAlways<GeneticCounsellorDbContext> //DropCreateDatabaseIfModelChanges<GeneticCounsellorDbContext>         



    {
        protected override void Seed(GeneticCounsellorDbContext context)
        {
            base.Seed(context);

            
        }
        public static Phenotype CreateNewPhenotype(string PhenotypeName, GeneticCounsellorDbContext context)
        {
            var p = new Phenotype();
            //derived from the traits and genotypes
            context.Phenotypes.Add(p);
            return p;
        }
        public static Trait CreateNewTrait(string TraitName, string AlleleName, Dominance InheritanceType,GeneticCounsellorDbContext context)
        {
            var t = new Trait();
            t.TraitName = TraitName;
            t.AlleleName = AlleleName;
            t.InheritanceType = InheritanceType;
            context.Traits.Add(t);
            return t;
        }
        public static Genotype CreateNewGenotype(string AlleleName, Dominance Allele1, Dominance Allele2, GeneticCounsellorDbContext context)
        {
            var g = new Genotype(AlleleName, Allele1, Allele2);
            g.AlleleName = AlleleName;
            g.Allele1 = Allele1;
            g.Allele2 = Allele2;

            context.Genotypes.Add(g);
            return g;

        }
        public static Person CreateNewPerson(string Name, Sex Sex, bool Living, string phenotypeName, GeneticCounsellorDbContext context)
        {
            var p = new Person();
            p.Name = Name;
            p.Sex = Sex;
            p.Living = Living;
            p.Phenotype = CreateNewPhenotype(phenotypeName, context);

            context.Persons.Add(p);
            context.SaveChanges();
            return p;

        }



    }
}
