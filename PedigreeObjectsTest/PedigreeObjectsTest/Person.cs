﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }// clarity's sake
        public Sex Sex { get; set; } //male or female = circle or square
        public bool Living { get; set; } //dead or alive
        //public List<Genotype> Genotypes { get; set; }
        public Phenotype Phenotype { get; set; }//is colourblind, heamophiliac, Has cystic fibrosis etc  
        public int? PhenotypeID { get; set; } 
        //public object Gametes { get; set; }// 'A, a, B, b'or ' A,b        
        public List<Trait> Traits { get; set; }// colourblindness, heamophilia, blue eyes,

       //no constructor
       
        public override string ToString()
        {
            string summary = Name + " " + Sex.ToString();
            return summary;
        }

        public void AddGenotypeToPerson(Genotype genotype)
        {            
            this.Phenotype.TraitGenotypes.Add(genotype);            
        }
        public void AddPhenotypeToPerson(Phenotype phenotype)
        {
            this.Phenotype = phenotype;
        }
        public void AddTraitToPerson(Trait trait)
        {
            this.Traits.Add(trait);
        }
        





    }
    
}