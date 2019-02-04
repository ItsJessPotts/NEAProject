﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Trait 
    {
        public int TraitID { get; set; }
        public string TraitName { get; set; }// Colourblindness
        public char AlleleName { get; set; }//eg C for colourblindness or A for asthma
        public Dominance InheritanceType { get; set; }// Dominant/ Recessive/ Autosomal/ Dihybrid/ Epistasis   // Maybe make it an enum
        public virtual List<Phenotype> PhenotypesWithTrait { get; set; } = new List<Phenotype>(); //People whom have the trait

        
        public override string ToString()
        {
            string summary =  TraitName + " " + InheritanceType.ToString();
            return summary;
        }




    }
}
