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

        private static void GeneticCounsellor()//TO DO: Finish menu
        {
            bool geneticCounsellorScreen = true;

            while (geneticCounsellorScreen == true)
            {
                Console.WriteLine("1) Add Person");
                Console.WriteLine("2) List all Persons");
                Console.WriteLine("3) Create trait");
                Console.WriteLine("4) List all traits");

                int geneticCounsellorMenuOption = MenuUserInputInt(4);

                switch (geneticCounsellorMenuOption)
                {
                    case 0:
                     geneticCounsellorScreen = true;
                        break;
                    case 1:
                        AddPersonScreen();
                        break;
                    case 2:
                        ListAllPersons();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        throw new Exception("Invalid Menu input");
                }

            }
                
        }
        private static void AddPersonScreen()//TO DO: figure out how to get user inputs of booleans and sex
        {
            Console.WriteLine("Name (first and last): ");
            string inputName = Console.ReadLine();
            Console.WriteLine("Sex (Male or Female): ");
            string inputSex = Console.ReadLine();
            Console.WriteLine("Living (true or false): ");
            string inputLiving = Console.ReadLine();

            var person = new Person(inputName,Sex.Male,false); //inputSex , inputLiving
            PersonRepository.AddPerson(person);
        }
        private static void ListAllPersons()
        {
            ListAllPersons();
        }

        private static int MenuUserInputInt(int max)
        {
           
            try
            {
                Console.WriteLine("Please select an option:");
                int menuOption = Convert.ToInt32(Console.ReadLine());
                if (menuOption > max)
                {
                    throw new Exception();
                }
                if (int.TryParse(Console.ReadLine(), out menuOption))
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
