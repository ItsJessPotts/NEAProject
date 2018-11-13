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
            
            var ja = CreateNewPerson("John Adams", Sex.Male, true, context); 
            
            var ea = CreateNewPerson("Eliza Hamilton", Sex.Female, true, context);
            
            var sa = CreateNewPerson("Samuel Adams", Sex.Male, true, context);
      
            var pr = CreateNewPerson("Paul Revere", Sex.Male, true, context);
          
            var ca = CreateNewPerson("Crispus Attucks", Sex.Male, false,context);
        
            var aa = CreateNewPerson("Abigail Adams",Sex.Female,true,context);
            
            context.SaveChanges();

 
            var aDom = CreateNewGenotype('A',Dominance.Dominant,Dominance.Dominant, context);
      
            var Aa = CreateNewGenotype('A', Dominance.Dominant, Dominance.Recessive, context);
            
            var aRec = CreateNewGenotype('a', Dominance.Recessive, Dominance.Recessive, context);
            
            context.SaveChanges();

           
            var c = CreateNewTrait("Colourblindness", 'c', Dominance.Recessive, context);
    
            var f = CreateNewTrait("Cystic Fibrosis", 'f', Dominance.Recessive, context);
         
            var p = CreateNewTrait("haemophilia", 'p', Dominance.Recessive, context);
           
            var H = CreateNewTrait("Huntington's Disease", 'H', Dominance.Dominant, context);
            
            var M = CreateNewTrait("Marfan's Syndrom", 'M', Dominance.Dominant, context);
            
            var T = CreateNewTrait("Tuberous Sclerosis", 'T', Dominance.Dominant, context);
            
            context.SaveChanges();

            
            
            var cb = CreateNewPhenotype("Colourblind",context);
            var cf = CreateNewPhenotype("Cystic Fibrosis", context);
            var hp =CreateNewPhenotype("Heamophiliac", context);
            var ht =CreateNewPhenotype("Huntingtons", context);
            var mf =CreateNewPhenotype("Marfans", context);
            var ts =CreateNewPhenotype("Tuberous Sclerosis", context);
            
                                   
            context.SaveChanges();


            ja.Phenotype = cb;
            ea.Phenotype = cf;
            sa.Phenotype = hp;


            context.SaveChanges();

        }
        public static Phenotype CreateNewPhenotype(string PhenotypeName, GeneticCounsellorDbContext context)
        {
            var p = new Phenotype();
            p.PhenotypeName = PhenotypeName;
            context.Phenotypes.Add(p);
            return p;
        }
        public static Trait CreateNewTrait(string TraitName, char AlleleName, Dominance InheritanceType,GeneticCounsellorDbContext context)
        {
            var t = new Trait();
            t.TraitName = TraitName;
            t.AlleleName = AlleleName;
            t.InheritanceType = InheritanceType;
            context.Traits.Add(t);
            return t;
        }
        public static Genotype CreateNewGenotype(char AlleleName, Dominance Allele1, Dominance Allele2, GeneticCounsellorDbContext context)
        {
            var g = new Genotype();
            g.AlleleName = AlleleName;
            g.Allele1 = Allele1;
            g.Allele2 = Allele2;

            context.Genotypes.Add(g);
            return g;

        }
        public static Person CreateNewPerson(string Name, Sex Sex, bool Living, GeneticCounsellorDbContext context)
        {
            var p = new Person();
            p.Name = Name;
            p.Sex = Sex;
            p.Living = Living;
            p.Phenotype = CreateNewPhenotype("unaffected", context);

            context.Persons.Add(p);
            context.SaveChanges();
            return p;

        }



    }
}
