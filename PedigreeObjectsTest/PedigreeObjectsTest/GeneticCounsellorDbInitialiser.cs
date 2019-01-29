using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PedigreeObjects
{
    public class GeneticCounsellorDbInitialiser:DropCreateDatabaseAlways<GeneticCounsellorDbContext> //DropCreateDatabaseIfModelChanges<GeneticCounsellorDbContext>
    {
        protected override void Seed(GeneticCounsellorDbContext context)
        {
            base.Seed(context);

            //var cb = CreateNewPhenotype("Colourblind", context);
            //var cf = CreateNewPhenotype("Cystic Fibrosis", context);
            //var hp = CreateNewPhenotype("Heamophiliac", context);
            //var ht = CreateNewPhenotype("Huntingtons", context);
            //var mf = CreateNewPhenotype("Marfans", context);
            //var ts = CreateNewPhenotype("Tuberous Sclerosis", context);

            var ja = CreateNewPerson("John Adams", Sex.Male, true, "Colourblind", context); 
            
            var ea = CreateNewPerson("Eliza Hamilton", Sex.Female, true, "Cystic Fibrosis", context);
            
            var sa = CreateNewPerson("Samuel Adams", Sex.Male, true, "Heamophiliac", context);
      
            var pr = CreateNewPerson("Paul Revere", Sex.Male, true, "Huntingtons",context);
          
            var ca = CreateNewPerson("Crispus Attucks", Sex.Male, false, "Marfans",context);
        
            var aa = CreateNewPerson("Abigail Adams",Sex.Female,true, "Tuberous Sclerosis", context);

            var jc = CreateNewPerson("James Cook", Sex.Male, true, "Heamophiliac", context);

            var ed = CreateNewPerson("Enid Dundy", Sex.Female, true, "Hemophiliac", context);

            var jh = CreateNewPerson("Jonathan Hamitlon", Sex.Male, true, "Hemophiliac", context);



            context.SaveChanges();

            //The Gene Pool
            var cDom = CreateNewGenotype("C",Dominance.Dominant,Dominance.Dominant, context);
      
            var cHet = CreateNewGenotype("C", Dominance.Dominant, Dominance.Recessive, context);
            
            var cRec = CreateNewGenotype("c", Dominance.Recessive, Dominance.Recessive, context);

            var fDom = CreateNewGenotype("F", Dominance.Dominant, Dominance.Dominant, context);

            var pDom = CreateNewGenotype("P", Dominance.Dominant, Dominance.Dominant, context);

            var pRec = CreateNewGenotype("p", Dominance.Recessive, Dominance.Recessive, context);

            context.SaveChanges();

           
            var c = CreateNewTrait("Colourblindness", 'C', Dominance.Recessive, context);
    
            var f = CreateNewTrait("Cystic Fibrosis", 'F', Dominance.Recessive, context);
         
            var p = CreateNewTrait("Haemophilia", 'P', Dominance.Recessive, context);
           
            var H = CreateNewTrait("Huntington's Disease", 'H', Dominance.Dominant, context);
            
            var M = CreateNewTrait("Marfan's Syndrom", 'M', Dominance.Dominant, context);
            
            var T = CreateNewTrait("Tuberous Sclerosis", 'T', Dominance.Dominant, context);
            
            context.SaveChanges();

            ja.Phenotype.Traits.Add(c);
            ja.Phenotype.Traits.Add(f);
            ja.Phenotype.TraitGenotypes.Add(cRec);
            ja.Phenotype.TraitGenotypes.Add(fDom);
            ja.AddMotherToPerson(ea);
            ja.AddFatherToPerson(sa);

            ea.Phenotype.Traits.Add(p);
            ea.Phenotype.Traits.Add(c);
            ea.Phenotype.TraitGenotypes.Add(pDom);
            ea.Phenotype.TraitGenotypes.Add(cHet);
            ea.AddMotherToPerson(ed);
            ea.AddFatherToPerson(jh);

            ed.Phenotype.Traits.Add(p);
            ed.Phenotype.Traits.Add(c);
            ed.Phenotype.TraitGenotypes.Add(cHet);
            ed.Phenotype.TraitGenotypes.Add(pDom);

            sa.Phenotype.Traits.Add(p);
            sa.Phenotype.Traits.Add(c);
            sa.Phenotype.TraitGenotypes.Add(pRec);
            sa.Phenotype.TraitGenotypes.Add(cHet);

            context.SaveChanges();



        }
        public static Phenotype CreateNewPhenotype(string PhenotypeName, GeneticCounsellorDbContext context)
        {
            var p = new Phenotype();
            //derived from the traits and genotypes
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
        public static Genotype CreateNewGenotype(string AlleleName, Dominance Allele1, Dominance Allele2, GeneticCounsellorDbContext context)
        {
            var g = new Genotype();
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
