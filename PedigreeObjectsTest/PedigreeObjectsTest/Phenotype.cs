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
        public string Environment { get; set; } //Array of environmental influences that could be effecting the phenotype
        public virtual List<Genotype> TraitGenotypes { get; set; } = new List<Genotype>(); // The actual genotypes possessed: Aa, BB, Cc, Dd, EE
        public string PhenotypeName {
            get {
                return GeneratePhenotypeName();
            }
        } //name of phenotype


        public string GeneratePhenotypeName()
        {
            string name = "";
            foreach (var genotype in TraitGenotypes)
            {
               
                foreach (var trait in Traits)
                {
                    //if (trait.AlleleName == genotype.AlleleName)
                    {
                        //if (trait.InheritanceType == Dominance.Recessive)
                        {
                            //if Allele1 == recessive AND Allele2 == recessive
                            //Then
                            //phenotype = "Has" + traitName
                            //if Alelle1 ==  Dominant And Allele2 == Dominant
                            //Then
                            //Phenotype = "Not" + traitName
                            //if Allele1 == Dominant AND Allele2 == recessive
                            //Then
                            //Phenotype = "Carrier of " + traitName

                        }
                    }
                }

                name = name + genotype.AlleleName;
            }
            return name;
        }

        public override string ToString()
        {
            string summary = "Phenotype: " + PhenotypeName;
            return summary;
        }

        public string CalculatePhenotypicRatio(Genotype parent1, Genotype parent2, int NumberOfPhenotypeOptions) //Aa Bb
        {
            int isHeterozygous = 0;
            int isRecessive = 0;
            string formattedRatio = "";

            int[] choice = new int[] { 0, 1, 2, 3 };
            Dominance resultingAllele1;
            Dominance resultingAllele2;

            foreach (int option in choice)
            {

                switch (option)
                {
                    case 0:
                        resultingAllele1 = parent2.Allele1; //A
                        resultingAllele2 = parent1.Allele1; //B

                        break;
                    case 1:
                        resultingAllele1 = parent2.Allele2; //b
                        resultingAllele2 = parent1.Allele1; //A
                        isHeterozygous++;
                        break;

                    case 2:
                        resultingAllele1 = parent2.Allele1; //B
                        resultingAllele2 = parent1.Allele2; //a
                        isHeterozygous++;

                        break;
                    case 3:
                        resultingAllele1 = parent2.Allele2; //b
                        resultingAllele2 = parent1.Allele2; //a
                        isRecessive++;
                        break;
                    default:
                        throw new Exception("Invalid Choice");
                }

            }
            if (NumberOfPhenotypeOptions == 2) //normal monohyprid cross= has Trait/doesn't
            {
                var gcd = GCD(4 - isRecessive, isRecessive);
                formattedRatio = string.Format("{0}:{1}", (isRecessive / gcd).ToString(), (4 - isRecessive / gcd).ToString());
            }
            if (NumberOfPhenotypeOptions == 3)//CoDominant = has trait/doesn't/mixed
            {
                var gcd = GCD(GCD(4 - isRecessive, isRecessive), isHeterozygous); ;
                formattedRatio = string.Format("{0}:{1}:{2}", (isRecessive / gcd).ToString(), (4 - isRecessive / gcd).ToString(), (isHeterozygous / gcd).ToString());//not working
            }
            return formattedRatio;
        }


        public int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            if (a == 0)
                return b;
            else
                return a;
        }




    }

}
