﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardyWeinbergTest
{
    public class HardyWeinberg
    {
        public static void Main()
        {
            Console.WriteLine("======================================================================================");
            Console.WriteLine(" Hardy Weinberg Calculator ");

            double population = AskUserForPopulation();
            double NoOfRecessiveSnails = AskUserForNoOfRecessiveSnails();
            double FrequencyOfRecessiveAlelles = CalculateFrequencyOfRecessiveAlelle(NoOfRecessiveSnails, population);
            double FrequencyOfDominantAlelles = CalculateFrequencyOfDominantAlelle(FrequencyOfRecessiveAlelles);
            double FrequencyOfRecessiveSnails = CalculateFrequencyOfRecessiveSnails(FrequencyOfRecessiveAlelles);
            double FrequencyOfDominantSnails = CalculateFrequencyOfDominantSnails(FrequencyOfDominantAlelles);
            double FrequencyOfHeterozygousSnails = CalculateFrequencyOfHeterozygousSnails(FrequencyOfRecessiveAlelles,FrequencyOfDominantAlelles);
            double DominantChancesResult = FrequencyOfDominantSnails + FrequencyOfDominantSnails * 100;
            double RecessiveChancesResult = FrequencyOfRecessiveSnails + FrequencyOfRecessiveSnails * 100;
            double HeterozygouseChancesResult = FrequencyOfHeterozygousSnails + FrequencyOfHeterozygousSnails * 100;


            Console.WriteLine("AA = "+ "\t  " + Math.Round(DominantChancesResult) +"% chance of homozygous dominant.");
            Console.WriteLine("aa = " + "\t  " + Math.Round(RecessiveChancesResult) + "% chance of being homozygous recessive.");
            Console.WriteLine("Aa = " + "\t  " + Math.Round(HeterozygouseChancesResult) + "% chance of being a carrier.");
           
            Console.ReadKey();
            
        }
        
        public static double AskUserForPopulation()
        {
            Console.WriteLine("Please input the total number individuals:");
            double population = Convert.ToDouble(Console.ReadLine());
            return population;
        }
        public static double AskUserForNoOfRecessiveSnails()
        {
            Console.WriteLine("Please input the number of recessive individuals:");
            double NoOfRecessiveSnails = Convert.ToDouble(Console.ReadLine());
            return NoOfRecessiveSnails;
        }
        public static double CalculateFrequencyOfRecessiveAlelle(double NoOfRecessiveSnails,double population)
        {
            double FrequencyOfRecessiveAlelles = Math.Sqrt(NoOfRecessiveSnails / population);
            return FrequencyOfRecessiveAlelles;
        }
        public static double CalculateFrequencyOfDominantAlelle(double FrequencyOfRecessiveAlelles)
        {
            double FrequencyOfDominantAlelles = 1 - FrequencyOfRecessiveAlelles;
            return FrequencyOfDominantAlelles;
        }
        public static double CalculateFrequencyOfRecessiveSnails(double FrequencyOfRecessiveAlelles)
        {
            double FrequencyOfRecessiveSnails = FrequencyOfRecessiveAlelles * FrequencyOfRecessiveAlelles;
            return FrequencyOfRecessiveSnails;
        }
        public static double CalculateFrequencyOfDominantSnails(double FrequencyOfRecessiveAlelles)
        {
            double FrequencyOfDominantSnails = FrequencyOfRecessiveAlelles * FrequencyOfRecessiveAlelles;
            return FrequencyOfDominantSnails;
        }
        public static double CalculateFrequencyOfHeterozygousSnails(double FrequencyOfRecessiveAlelles, double FrequencyOfDominantAlelles)
        {
            double FrequencyOfHeterozygousSnails = 2 * FrequencyOfDominantAlelles * FrequencyOfRecessiveAlelles;
            return FrequencyOfHeterozygousSnails;
        }

    }
}
