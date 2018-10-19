using System;
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

        public Trait(string traitName, char alleleName, Dominance inheritanceType)
        {
            this.TraitName = traitName;
            this.AlleleName = alleleName;
            this.InheritanceType = inheritanceType;
        }
        public Trait()
        {

        }
     

        public override string ToString()
        {
            string summary = "Trait: " + TraitName + " " + InheritanceType.ToString();
            return summary;
        }


    }
}
