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
            var personRepository = new PersonRepository(context);
            var ja = personRepository.AddPerson("John Adams", Sex.Male, true); 
            context.Persons.Add(ja); //Repeat with other seed data
            var ea = personRepository.AddPerson("Eliza Hamilton", Sex.Female, true);
            context.Persons.Add(ea);
            var sa = personRepository.AddPerson("Samuel Adams", Sex.Male, true);
            context.Persons.Add(sa);
            var pr = personRepository.AddPerson("Paul Revere", Sex.Male, true);
            context.Persons.Add(pr);
            var ca = personRepository.AddPerson("Crispus Attucks", Sex.Male, false);
            context.Persons.Add(ca);
            var aa = personRepository.AddPerson("Abigail Adams",Sex.Female,true);
            context.Persons.Add(aa);
            context.SaveChanges();

            var genotypeRepository = new GenotypeRepository(context);
            var aDom = genotypeRepository.AddGenotype('A',Dominance.Dominant,Dominance.Dominant);
            context.Genotypes.Add(aDom);
            var Aa = genotypeRepository.AddGenotype('A', Dominance.Dominant, Dominance.Recessive);
            context.Genotypes.Add(Aa);
            var aRec = genotypeRepository.AddGenotype('a', Dominance.Recessive, Dominance.Recessive);
            context.Genotypes.Add(aRec);
            context.SaveChanges();

            var traitRepository = new TraitRepository(context);
            var c = traitRepository.AddTrait("Colourblindness", 'c', Dominance.Recessive);
            context.Traits.Add(c);
            var f = traitRepository.AddTrait("Cystic Fibrosis", 'f', Dominance.Recessive);
            context.Traits.Add(f);
            var p = traitRepository.AddTrait("haemophilia", 'p', Dominance.Recessive);
            context.Traits.Add(p);
            var H = traitRepository.AddTrait("Huntington's Disease", 'H', Dominance.Dominant);
            context.Traits.Add(H);
            var M = traitRepository.AddTrait("Marfan's Syndrom", 'M', Dominance.Dominant);
            context.Traits.Add(M);
            var T = traitRepository.AddTrait("Tuberous Sclerosis", 'T', Dominance.Dominant);
            context.Traits.Add(T);
            context.SaveChanges();

            var phenotypeRepository = new PhenotypeRepository(context);
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
