using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Person
    {
        public string Name { get; set; }// clarity's sake
        public Sex Sex { get; set; } //male or female
        public bool Living { get; set; } //dead or alive
        public object Phenotype { get; set; }//is colourblind, heamophiliac, Has cystic fibrosis etc
        public object Genotype { get; set; }// Aa,aa,AA
        public object Gametes { get; set; }// 'A, a, B, b'or ' A,b
        
                            
    }
}