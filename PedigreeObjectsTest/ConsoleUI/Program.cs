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
            bool MainMenuScreen = true;
            while (MainMenuScreen == true)
            {
                Console.WriteLine("Welcome");
                Console.WriteLine("1) Genetic Counsellor");
                Console.WriteLine("2) Hardy Weinberg Calculator");
                int mainMenuChoice = MenuUserInputInt(2);

                switch (mainMenuChoice)
                {
                    case 0:
                        MainMenuScreen = true;
                        break;
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
            
        }

        private static void HardyWeinbergCalculator()
        {
            throw new NotImplementedException();
        }

        private static void GeneticCounsellor()
        {            
            Console.WriteLine("1) Add Person");
            Console.WriteLine("2) List all Persons");

            int geneticCounsellorMenuOption = MenuUserInputInt(2);
        }
        private static void AddPersonScreen()
        {
            //input Name and Sex
            var person = new Person(null, Sex.Male, false);
            PersonRepository.AddPerson(person);
        }

        private static int MenuUserInputInt(int max)//TO DO: Guard against non integers.
        {

            
            try
            {
                Console.WriteLine("Please select an option:");
                int menuOption = Convert.ToInt32(Console.ReadLine());
                if (menuOption > max)
                {
                    throw new Exception();
                }
                else
                {
                    return menuOption;
                }
                             
            }
            catch (Exception)
            {
                Console.WriteLine("Input was not valid");
                return 0;
            }
           
             
            
        }
    }
}
