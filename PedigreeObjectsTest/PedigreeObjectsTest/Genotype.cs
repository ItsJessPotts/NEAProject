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
        public string AlleleName { get; set; } //eg C for colourblindness or A for asthma (one letter) 
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
        public Genotype CombineGenotypes(Genotype other, GenotypeRepository genotypeRepository, IRandomNumberGenerator RNG) //########FIX THIS#########
        {                        
            var choice = RNG.Next(0, 4);
            var g = new Genotype();
            g.AlleleName = AlleleName;

            if (choice == 0)
            {
                g.Allele1 = this.Allele1;
                g.Allele2 = other.Allele1;
            }
            if (choice == 1)
            {
                g.Allele1 = this.Allele2;
                g.Allele2 = other.Allele1;
                
            }
            if (choice == 2)
            {
                g.Allele1 = this.Allele1;
                g.Allele2 = other.Allele2;
                
            }
            if (choice == 3)
            {
                g.Allele1 = this.Allele2;
                g.Allele2 = other.Allele2;

            }
                 
            return g;

        }

        public Genotype MostLikelyGenotype(Genotype other, GenotypeRepository genotypeRepository, IRandomNumberGenerator RNG)
        {
            string letter = AlleleName;
            List<Genotype> GenericGenotypes = CreateGenericGenotypes(AlleleName);
            Genotype hetG = GenericGenotypes[0];
            Genotype domG = GenericGenotypes[1];
            Genotype recG = GenericGenotypes[2];
            Genotype hetG2 = GenericGenotypes[3];

            int hetGTally = 0;
            int domGTally = 0;
            int recGTally = 0;
            int hetG2Tally = 0;


            for (int i = 0; i < 100; i++)// CANNOT COMPARE GENOTYPES
            {
                Genotype g = CombineGenotypes(other, genotypeRepository, RNG);
                if (g.ToString() == hetG.ToString())
                {
                    hetGTally++;
                }
                if (g.ToString() == domG.ToString())
                {
                    domGTally++;
                }
                if (g.ToString() == recG.ToString())
                {
                    recGTally++;
                }
                if (g.ToString() == hetG2.ToString())
                {
                    hetG2Tally++;
                }
                


            }
            if (domGTally > recGTally || domGTally > hetGTally || hetGTally > hetG2Tally)
            {
                return domG;
            }
            if (recGTally > domGTally || recGTally > domGTally || hetGTally > hetG2Tally)
            {
                return recG;
            }
            if (hetGTally > domGTally || hetGTally > recGTally || hetGTally > hetG2Tally)
            {
                return hetG;
            }
            if (hetG2Tally > domGTally || hetG2Tally > recGTally || hetG2Tally > hetGTally)
            {
                return hetG2;
            }
            {
                return other;
            }
        }    

            public List<Genotype> CreateGenericGenotypes(string AlleleName)
            {

                    Genotype hetG = new Genotype(AlleleName, Dominance.Dominant, Dominance.Recessive);
                    Genotype domG = new Genotype(AlleleName, Dominance.Dominant, Dominance.Dominant);
                    Genotype recG = new Genotype(AlleleName, Dominance.Recessive, Dominance.Recessive);
                    Genotype hetG2 = new Genotype(AlleleName,Dominance.Recessive, Dominance.Dominant);

                    List<Genotype> GenericGenotypes = new List<Genotype>();
                    GenericGenotypes.Add(hetG);
                    GenericGenotypes.Add(domG);
                    GenericGenotypes.Add(recG);
                    GenericGenotypes.Add(hetG2);
                    return GenericGenotypes;


            }


            public List<Genotype> CalculateParentalGenotypes(GenotypeRepository genotypeRepository, IRandomNumberGenerator RNG)
            {
                    List<Genotype> PossibleParentalGenotypes = new List<Genotype>();
                    string letter = AlleleName;
                    List<Genotype> GenericGenotypes = CreateGenericGenotypes(AlleleName);
                    Genotype hetG = GenericGenotypes[0];
                    Genotype domG = GenericGenotypes[1];
                    Genotype recG = GenericGenotypes[2];
                    Genotype hetG2 = GenericGenotypes[3];

                    Genotype resultOfHetGAndDomG = hetG.CombineGenotypes(domG, genotypeRepository, RNG);
                    Genotype resultOfHetGAndRecG = hetG.CombineGenotypes(recG, genotypeRepository, RNG);
                    Genotype resultOfHetGAndHetG = hetG.CombineGenotypes(hetG, genotypeRepository, RNG);

                    Genotype resultOfDomGAndDomG = domG.CombineGenotypes(domG, genotypeRepository, RNG);
                    Genotype resultOfDomGAndRecG = domG.CombineGenotypes(recG, genotypeRepository, RNG);

                    Genotype resultOfRecGAndRecG = recG.CombineGenotypes(recG, genotypeRepository, RNG);

                    Genotype resultOfhetG2AndRecG = hetG2.CombineGenotypes(recG, genotypeRepository, RNG);
                    Genotype resultOfhetG2AndDomG = hetG2.CombineGenotypes(domG, genotypeRepository, RNG);
                    Genotype resultOfhetG2AndHetG = hetG2.CombineGenotypes(hetG, genotypeRepository, RNG);
                    Genotype resultOfhetG2AndhetG2 = hetG2.CombineGenotypes(hetG2, genotypeRepository, RNG);



                        if (ToString() == resultOfDomGAndDomG.ToString()) //AA X AA
                        {
                            PossibleParentalGenotypes.Add(domG);
                            PossibleParentalGenotypes.Add(domG);

                        }
                        if (ToString() == resultOfDomGAndRecG.ToString()) // AA X aa
                        {
                            PossibleParentalGenotypes.Add(domG);
                            PossibleParentalGenotypes.Add(recG);
                        }
                        if (ToString() == resultOfHetGAndDomG.ToString()) // Aa X AA
                        {
                            PossibleParentalGenotypes.Add(domG);
                            PossibleParentalGenotypes.Add(hetG);
                        }
                        if (ToString() == resultOfHetGAndHetG.ToString()) // Aa X Aa
                        {
                            PossibleParentalGenotypes.Add(hetG);
                            PossibleParentalGenotypes.Add(hetG);
                        }
                        if (ToString() == resultOfHetGAndRecG.ToString())//Aa X aa
                        {
                            PossibleParentalGenotypes.Add(recG);
                            PossibleParentalGenotypes.Add(hetG);
                        }
                        if (ToString() == resultOfRecGAndRecG.ToString())// aa X aa
                        {
                            PossibleParentalGenotypes.Add(recG);
                            PossibleParentalGenotypes.Add(recG);
                        }
                        if (ToString() == resultOfhetG2AndRecG.ToString())// aA X aa
                        {
                            PossibleParentalGenotypes.Add(hetG); //not HG2 as HG2 is actually the same as hetG as aA = Aa
                            PossibleParentalGenotypes.Add(recG);
                        }
                        if (ToString() == resultOfhetG2AndDomG.ToString()) //aA X AA
                        {
                            PossibleParentalGenotypes.Add(hetG);
                            PossibleParentalGenotypes.Add(domG);
                        }
                        if (ToString() == resultOfhetG2AndHetG.ToString()) //aA X Aa
                        {
                            PossibleParentalGenotypes.Add(hetG);
                            PossibleParentalGenotypes.Add(hetG);
                        }
                        if (ToString() == resultOfhetG2AndhetG2.ToString()) //aA X aA
                        {
                            PossibleParentalGenotypes.Add(hetG);
                            PossibleParentalGenotypes.Add(hetG);
                        }


                return PossibleParentalGenotypes;
                
            }
        

    }

}
