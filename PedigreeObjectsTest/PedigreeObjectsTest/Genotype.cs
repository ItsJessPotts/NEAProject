using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects 
{
    //The pairs of alelles that express a specific phenotype: Aa Or Bb or Cc
    public class Genotype
    {
        public int GenotypeID { get; set; }        
        public string AlleleName { get; set; } //eg C for colourblindness or A for asthma (one letter) //TO DO:turn back to char
        public Dominance Allele1 { get; set; } //If dominant- inputted here
        public Dominance Allele2 { get; set; }
        public virtual List<Phenotype> PeopleWithGenotype { get; set; } = new List<Phenotype>();

        public Genotype()
        {

        }
       
        public Genotype(string AlleleName, Dominance Allele1, Dominance Allele2)
        {
            
            this.AlleleName = AlleleName;
            this.Allele1 = Allele1;
            this.Allele2 = Allele2;
                       
        }
        public override string ToString()
        {
            return  AlleleAsString(Allele1) + AlleleAsString(Allele2);
        }
        private string AlleleAsString(Dominance allele)
        {
            if (allele == Dominance.Dominant)
            {
                return AlleleName.ToString();
            }
            else
            {
                return AlleleName.ToString().ToLower();
            }
        }
        public Genotype CombineGenotypes(Genotype other, GenotypeRepository genotypeRepository, IRandomNumberGenerator RNG)
        {
            
            var choice = RNG.Next(0, 4);
            Dominance resultingAllele1;
            Dominance resultingAllele2;
            switch (choice)
            {
                case 0:
                    resultingAllele1 = this.Allele1;
                    resultingAllele2 = other.Allele1;
                    break;
                case 1:
                    resultingAllele1 = this.Allele2;
                    resultingAllele2 = other.Allele1;
                    break;
                case 2:
                    resultingAllele1 = this.Allele1;
                    resultingAllele2 = other.Allele2;
                    break;
                case 3:
                    resultingAllele1 = this.Allele2;
                    resultingAllele2 = other.Allele2;
                    break;
                default:
                    throw new Exception("Invalid Choice");
            }

            var gt = new Genotype(this.AlleleName, resultingAllele1, resultingAllele2);
            return gt;
        }

                
        public List<Genotype> CalculateParentalGenotypes(Genotype g, GenotypeRepository genotypeRepository, IRandomNumberGenerator RNG)
        {
            List<Genotype> PossibleParentalGenotypes = new List<Genotype>();
            string letter = g.AlleleName;            
            Genotype hetG = new Genotype(g.AlleleName, Dominance.Dominant, Dominance.Recessive);
            Genotype domG = new Genotype(g.AlleleName, Dominance.Dominant, Dominance.Dominant);
            Genotype recG = new Genotype(g.AlleleName, Dominance.Recessive, Dominance.Recessive);
            Genotype resultOfHetGAndDomG = hetG.CombineGenotypes(domG, genotypeRepository, RNG);
            Genotype resultOfHetGAndRecG = hetG.CombineGenotypes(recG, genotypeRepository, RNG);
            Genotype resultOfHetGAndHetG = hetG.CombineGenotypes(hetG,genotypeRepository, RNG);
            Genotype resultOfDomGAndDomG = domG.CombineGenotypes(domG, genotypeRepository, RNG);
            Genotype resultOfDomGAndRecG = domG.CombineGenotypes(recG, genotypeRepository, RNG);
            Genotype resultOfRecGAndRecG = recG.CombineGenotypes(recG, genotypeRepository, RNG);

            if (g.ToString() == resultOfDomGAndDomG.ToString())
            {
                PossibleParentalGenotypes.Add(domG);
                PossibleParentalGenotypes.Add(domG);

            }
            if (g.ToString() == resultOfDomGAndRecG.ToString())
            {
                PossibleParentalGenotypes.Add(domG);
                PossibleParentalGenotypes.Add(recG);
            }
            if (g.ToString() == resultOfHetGAndDomG.ToString())
            {
                PossibleParentalGenotypes.Add(domG);
                PossibleParentalGenotypes.Add(hetG);
            }
            if (g.ToString() == resultOfHetGAndHetG.ToString())
            {
                PossibleParentalGenotypes.Add(hetG);
                PossibleParentalGenotypes.Add(hetG);
            }
            if (g.ToString() == resultOfHetGAndRecG.ToString())
            {
                PossibleParentalGenotypes.Add(recG);
                PossibleParentalGenotypes.Add(hetG);
            }
            if (g.ToString() == resultOfRecGAndRecG.ToString())
            {
                PossibleParentalGenotypes.Add(recG);
                PossibleParentalGenotypes.Add(recG);
            }

            return PossibleParentalGenotypes;
           
            
        }
        

    }

}
