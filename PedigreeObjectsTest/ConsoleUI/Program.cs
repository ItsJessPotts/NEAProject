using PedigreeObjects;
using HardyWeinbergTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
       

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

            HardyWeinberg.Main();
        }

        private static void GeneticCounsellor()
        {
            bool geneticCounsellorScreen = true;
            var personRepository = new PersonRepository();
            var genotypeRepository = new GenotypeRepository();

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
                        AddPersonScreen(personRepository);
                        break;
                    case 2:
                        StringBuilder sb = new StringBuilder();
                        ListAllPersonsScreen(sb, personRepository);
                        string s = sb.ToString();
                        Console.Write(s);
                        break;
                    case 3:
                        CreateTraitScreen();
                        break;
                    case 4:
                        StringBuilder sb2 = new StringBuilder();
                        ListAllTraitsScreen(sb2,genotypeRepository);
                        string s2 = sb2.ToString();
                        Console.Write(s2);
                        break;
                    default:
                        throw new Exception("Invalid Menu input");
                }

            }
                
        }

        private static void ListAllTraitsScreen(StringBuilder sb2, GenotypeRepository genotypeRepository)
        {
            var listOfGenotypes = genotypeRepository.ListGenotypes();

            if (listOfGenotypes.Count == 0)
            {
                sb2.AppendLine("There are no Persons in this system, please add one.");
                sb2.AppendLine("____________________________________________________");
            }
            else
            {
                foreach (var g in listOfGenotypes)
                {
                    sb2.AppendLine(g.ToString());
                }
            }
        }

        private static void CreateTraitScreen()//TO DO: ACTUALLY SWITCH TO TRAIT CLASS- EXPLAIN 
        {
            Console.WriteLine("Name of trait:");// Colourblindness
            string inputName = Console.ReadLine();
            Console.WriteLine("Dominant or Recessive:"); //Reccessive
            string inputDominance = Console.ReadLine();
            Console.WriteLine("What letter should represent it?");// C
            string inputAlelleName = Console.ReadLine();

            //var genotype = new Genotype(inputAlelleName, null, null, null);

        }
        
        private static void AddPersonScreen(PersonRepository personRepository)//TO DO: figure out how to get user inputs of booleans and sex
        {
            
            Console.WriteLine("Name (first and last): ");
            string inputName = Console.ReadLine();
            Console.WriteLine("Sex ( Male(0) or Female(1) ): ");
            string inputtedSex = Console.ReadLine();
            Sex inputSex = (Sex)Convert.ToInt32(inputtedSex);
            Console.WriteLine("Living (true or false): ");
            string inputtedLiving = Console.ReadLine();
            bool inputLiving = (bool)Convert.ToBoolean(inputtedLiving);

            var person = new Person(inputName,inputSex,inputLiving); //inputSex , inputLiving
            personRepository.AddPerson(person);
        }
        private static void ListAllPersonsScreen(StringBuilder sb, PersonRepository personRepository)
        {
            
            var listOfPersons = personRepository.ListPersons();

            if (listOfPersons.Count == 0)
            {
                sb.AppendLine("There are no Persons in this system, please add one.");
                sb.AppendLine("____________________________________________________");                
            }
            else
            {
                foreach (var person in listOfPersons)
                {
                    sb.AppendLine(person.ToString());
                }             
            }
          

        }
        //private static void ListAllPersonsScreen()
        //{
        //    PersonRepository personRepository = new PersonRepository();
        //    var listOfPersons = personRepository.ListPersons();

        //    if (listOfPersons.Count == 0)
        //    {
        //        Console.WriteLine("There are no Persons in this system, please add one.");
        //        Console.WriteLine("____________________________________________________");
        //        AddPersonScreen();
        //    }
        //    else
        //    {
        //        foreach (var person in listOfPersons)
        //        {
        //            Console.WriteLine(person.ToString());
        //        }
        //    }


        //}

        private static int MenuUserInputInt(int max)
        {
           
            try
            {
                Console.WriteLine("Please select an option:");
                string menuOption = Console.ReadLine();
                int menuOptionInt = Convert.ToInt32(menuOption); 
                int number;
                if (menuOptionInt > max)
                {
                    throw new Exception();
                }
                if (int.TryParse(menuOption, out number)== false)
                {
                    throw new Exception();
                }
                else
                {
                    return menuOptionInt;
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
