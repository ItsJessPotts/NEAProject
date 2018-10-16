﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Person
    {
        public string Name { get; set; }// clarity's sake
        public Sex Sex { get; set; } //male or female = circle or square
        public bool Living { get; set; } //dead or alive
        public GenotypeRepository Genotypes { get; set; }
        public Phenotype Phenotype { get; set; }//is colourblind, heamophiliac, Has cystic fibrosis etc        
        public object Gametes { get; set; }// 'A, a, B, b'or ' A,b        
        public TraitRepository Traits { get; set; }// colourblindness, heamophilia, blue eyes,

        public Person(string name, Sex sex, bool living, GenotypeRepository genotypes)
        {
            this.Name = name;
            this.Sex = sex;
            this.Living = living;
            this.Genotypes = genotypes;
        }
        public override string ToString()
        {
            string summary = Name + " " + Sex.ToString();
            return summary;
        }
      

    }
    
}