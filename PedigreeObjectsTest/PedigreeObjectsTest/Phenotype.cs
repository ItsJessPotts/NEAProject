using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Phenotype//NEED TO: Test Phenotypic Ratio or implement CoDominance
    {
        public int? PhenotypeID { get; set; }

        public virtual List<Trait> Traits { get; set; } = new List<Trait>(); //Array of traits possessed by the person
        public virtual List<Genotype> TraitGenotypes { get; set; } = new List<Genotype>(); // The actual genotypes possessed: Aa, BB, Cc, Dd, EE
        public string PhenotypeName { get; set; } //Phenotype Name
            
       
        public void GeneratePhenotypeName()
        {
            string name = "";
            foreach (var genotype in TraitGenotypes)
            {

                foreach (var trait in Traits)
                {
                    
                    if (trait.AlleleName.ToUpper() == genotype.AlleleName.ToUpper())
                    {
                        if (trait.InheritanceType == Dominance.Recessive)
                        {
                            
                            if (genotype.Allele1 == Dominance.Recessive && genotype.Allele2 == Dominance.Recessive)
                            {
                                name += "Has " + trait.TraitName + " ";
                            }
                            if (genotype.Allele1 == Dominance.Dominant && genotype.Allele2 == Dominance.Dominant)
                            {
                                name += "Does not have " + trait.TraitName + " ";
                            }
                            if (genotype.Allele1 == Dominance.Dominant && genotype.Allele2 == Dominance.Recessive)
                            {
                                name += "Carrier of " + trait.TraitName + " ";
                            }
                        }
                        if (trait.InheritanceType == Dominance.Dominant)
                        {
                            if (genotype.Allele1 == Dominance.Recessive && genotype.Allele2 == Dominance.Recessive)
                            {
                                name += "Does not have " + trait.TraitName + " ";
                            }
                            if (genotype.Allele1 == Dominance.Dominant && genotype.Allele2 == Dominance.Dominant)
                            {
                                name += "Has " + trait.TraitName + " ";
                            }
                            if (genotype.Allele1 == Dominance.Dominant && genotype.Allele2 == Dominance.Recessive)
                            {
                                name += "Has " + trait.TraitName + " ";
                            }
                        }
                    }
                }

                
            }
            PhenotypeName = name;
        }

        public override string ToString()
        {
            string summary = PhenotypeName;
            return summary;
        }

        




    }

}
