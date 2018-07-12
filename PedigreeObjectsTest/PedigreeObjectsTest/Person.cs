using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Person //NEED TO: Create Constructor
    {
        public string Name { get; set; }// clarity's sake
        public Sex Sex { get; set; } //male or female = circle or square
        public bool Living { get; set; } //dead or alive
        public Phenotype Phenotype { get; set; }//is colourblind, heamophiliac, Has cystic fibrosis etc
        public Genotype Genotype { get; set; }// Aa,aa,AA
        public object Gametes { get; set; }// 'A, a, B, b'or ' A,b

        public Person(string name, Sex sex, bool living)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            string summary = Name + Sex.ToString();
            return summary;
        }

    }
    
}