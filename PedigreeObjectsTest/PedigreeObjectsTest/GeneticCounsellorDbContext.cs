﻿using System;
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder) //2 different relationships as a person
                                                                             // can have one mother and one father
        {                                                                    // but a person can be a parent to 
                                                                             // multiple people
            modelBuilder.Entity<Person>()
                .HasOptional(m => m.Mother)
                .WithMany()
                .HasForeignKey(m => m.MotherID);

            modelBuilder.Entity<Person>()
               .HasOptional(f => f.Father)
               .WithMany()
               .HasForeignKey(f => f.FatherID);
        }
    }
}
