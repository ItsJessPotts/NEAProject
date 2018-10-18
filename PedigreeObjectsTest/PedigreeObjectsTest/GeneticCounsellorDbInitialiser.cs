using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PedigreeObjects
{
    public class GeneticCounsellorDbInitialiser: DropCreateDatabaseIfModelChanges<GeneticCounsellorDbContext> //DropCreateDatabaseAlways<GeneticCounsellorDbContext>
    {
        protected override void Seed(GeneticCounsellorDbContext context)
        {
            base.Seed(context);
            var JA = new Person("John Adams", Sex.Male, true); 
            context.Persons.Add(JA); //Repeat with other seed data

            context.SaveChanges();
            

            var AA = new Genotype("A",Dominance.Dominant,Dominance.Recessive);
            context.Genotypes.Add(AA);
            context.SaveChanges();

            var c = new Trait("Colourblindness", 'c', Dominance.Recessive);
            context.Traits.Add(c);
            context.SaveChanges();

            var cb = new Phenotype("Colourblind");
            context.Phenotypes.Add(cb);
            context.SaveChanges();

            JA.Phenotype = cb;
            context.SaveChanges();

        }
            //        John Adams, Male,true, A A
            //  Eliza Hamilton,Female,true,A a
            //Samuel Adams, Male,true, a a
            //  Paul Revere,Male,true,A a
            //Crispis Attucks, Male,false, A A
            //  Abigail Adams,Female,true,a a

    }
}
