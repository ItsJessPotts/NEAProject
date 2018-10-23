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
            var ja = new Person("John Adams", Sex.Male, true); 
            context.Persons.Add(ja); //Repeat with other seed data
            var ea = new Person("Eliza Hamilton", Sex.Female, true);
            context.Persons.Add(ea);
            var sa = new Person("Samuel Adams", Sex.Male, true);
            context.Persons.Add(sa);
            var pr = new Person("Paul Revere", Sex.Male, true);
            context.Persons.Add(pr);
            var ca = new Person("Crispus Attucks", Sex.Male, false);
            context.Persons.Add(ca);
            var aa = new Person("Abigail Adams",Sex.Female,true);
            context.Persons.Add(aa);
            context.SaveChanges();
            

            var aDom = new Genotype('A',Dominance.Dominant,Dominance.Dominant);
            context.Genotypes.Add(aDom);
            var Aa = new Genotype('A', Dominance.Dominant, Dominance.Recessive);
            context.Genotypes.Add(Aa);
            var aRec = new Genotype('a', Dominance.Recessive, Dominance.Recessive);
            context.Genotypes.Add(aRec);
            context.SaveChanges();

            var c = new Trait("Colourblindness", 'c', Dominance.Recessive);
            context.Traits.Add(c);
            var f = new Trait("Cystic Fibrosis", 'f', Dominance.Recessive);
            context.Traits.Add(f);
            var p = new Trait("haemophilia", 'p', Dominance.Recessive);
            context.Traits.Add(p);
            var H = new Trait("Huntington's Disease", 'H', Dominance.Dominant);
            context.Traits.Add(H);
            var M = new Trait("Marfan's Syndrom", 'M', Dominance.Dominant);
            context.Traits.Add(M);
            var T = new Trait("Tuberous Sclerosis", 'T', Dominance.Dominant);
            context.Traits.Add(T);
            context.SaveChanges();

            var cb = new Phenotype("Colourblind");
            context.Phenotypes.Add(cb);
            var cf = new Phenotype("Cystic Fibrosis");
            context.Phenotypes.Add(cf);
            var hp = new Phenotype("Heamophiliac");
            context.Phenotypes.Add(hp);
            var hd = new Phenotype("Huntingtons");
            context.Phenotypes.Add(hd);
            var ms = new Phenotype("Marfans");
            context.Phenotypes.Add(ms);
            var ts = new Phenotype("Tuberous Sclerosis");
            context.Phenotypes.Add(ts);
            context.SaveChanges();

            ja.Phenotype = cb;
            ea.Phenotype = cf;
            context.SaveChanges();

        }
            
            
         
    }
}
