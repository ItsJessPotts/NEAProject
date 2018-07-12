using PedigreeObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static PersonRepository PersonRepository = new PersonRepository(); 
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("1) Genetic Counsellor");
            Console.WriteLine("2) Hardy Weinberg Calculator");
            int mainMenuChoice = MenuUserInputInt(2);

            switch (mainMenuChoice)
            {
                case 1:
                    GeneticCounsellor();
                    break;
                case 2:
                    HardyWeinbergCalculator();
                    break;
                default:
                    throw new Exception("Invalid Menu input");
            }
        }

        private static void HardyWeinbergCalculator()
        {
            throw new NotImplementedException();
        }

        private static void GeneticCounsellor()
        {
            Console.WriteLine("1) Add Alelle");
            Console.WriteLine("2) List all alelles");
            Console.WriteLine("3) Add Person");
            Console.WriteLine("4) List all Persons");

            int geneticCounsellorMenuOption = MenuUserInputInt(4);
        }
        private static void AddPersonScreen()
        {
            //input Name and Sex
            var person = new Person(null, Sex.Male, false);
            PersonRepository.AddPerson(person);
        }

        private static int MenuUserInputInt(int max)//TO DO: Guard against anything over the max or non integers.
        {
            Console.WriteLine("Please select an option:");
            int menuoption = Convert.ToInt32(Console.ReadLine());
            return menuoption;
        }
    }
}
