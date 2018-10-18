using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class GeneticCounsellorDbContext: DbContext
    {
        public GeneticCounsellorDbContext(string databaseName):base(databaseName)
        {
            Database.SetInitializer(new GeneticCounsellorDbInitialiser());
        }
        public DbSet<Person> Persons { get; set; } //Defining the types that have a table
        public DbSet<Genotype> Genotypes { get; set; }
        public DbSet<Trait> Traits { get; set; }
        public DbSet<Phenotype> Phenotypes { get; set; }
    }
}
