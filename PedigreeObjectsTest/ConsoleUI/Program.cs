using PedigreeObjects;
using HardyWeinbergTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleUI
{
    class Program
    {
       

        static void Main()//To Do: Create Seed Data
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
        private static void PersonsSeedData()
        {
            string[] lines = new string[1000];
            using (StreamReader reader = new StreamReader("PersonsSeedData.txt"))
            {
                int i = 0;
                while (!reader.EndOfStream)
                {
                    lines[i] = reader.ReadLine();
                }
            }
            foreach (var line in lines)
            {
                Console.WriteLine(line);
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
            var traitRepository = new TraitRepository();

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
                        CreateTraitScreen(traitRepository);
                        break;
                    case 4:
                        StringBuilder sb2 = new StringBuilder();
                        ListAllTraitsScreen(sb2,genotypeRepository, traitRepository);
                        string s2 = sb2.ToString();
                        Console.Write(s2);
                        break;
                    default:
                        throw new Exception("Invalid Menu input");
                }

            }
                
        }

        private static void ListAllTraitsScreen(StringBuilder sb2, GenotypeRepository genotypeRepository, TraitRepository traitRepository)
        {
            var ListOfTraits = traitRepository.ListTraits();

            if (ListOfTraits.Count == 0)
            {
                sb2.AppendLine("There are no Traits in this system, please add one.");
                sb2.AppendLine("____________________________________________________");
            }
            else
            {
                foreach (var t in ListOfTraits)
                {
                    sb2.AppendLine(t.ToString());
                }
            }
        }

        private static void CreateTraitScreen(TraitRepository traitRepository)//TO DO: Find way to input Dominance 
        {
            Console.WriteLine("Name of trait:");// Colourblindness
            string inputName = Console.ReadLine();
            //Console.WriteLine("Type of inheritance (currently Dominant or Recessive only)"); //Reccessive
            //Dominance inputInheritanceType = Console.ReadLine(); //HELP
            Console.WriteLine("What letter should represent it?");// C
            char inputAlelleName = Convert.ToChar(Console.ReadLine());

            var trait = new Trait(inputName, inputAlelleName, Dominance.Dominant);
            traitRepository.AddTrait(trait);
            

        }
        
        private static void AddPersonScreen(PersonRepository personRepository)//TO DO: figure out how to get user inputs of enum sex
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
        private static void ListAllPersonsScreen(StringBuilder sb, PersonRepository personRepository)// Implement ability to find person 
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

            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1) Select a Person");
            Console.WriteLine("2) Delete a Person");
            
          

        }
        private static void PersonScreen(Person SelectedPerson)//To Do: Add options for methods
        {
            Console.WriteLine("Name: ",SelectedPerson.Name);
            Console.WriteLine("Sex: ",SelectedPerson.Sex);
            Console.WriteLine("Traits: ",SelectedPerson.Traits);
            Console.WriteLine("Genotypes: ", SelectedPerson.Genotypes);
        }
        
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
