using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Trait
    {
        private string TraitName { get; set; }// Colourblindness
        private char AlleleName { get; set; }//eg C for colourblindness or A for asthma
        private Dominance InheritanceType { get; set; }// Dominant/ Recessive/ Autosomal/ Dihybrid/ Epistasis   // Maybe make it an enum

        public Trait(string traitName, char alleleName, Dominance inheritanceType)
        {
            this.TraitName = traitName;
            this.AlleleName = alleleName;
            this.InheritanceType = inheritanceType;
        }

        public override string ToString()
        {
            string summary = TraitName + " " + InheritanceType.ToString();
            return summary;
        }


    }
}
