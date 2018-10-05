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
        static void Main()
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
            
            var genotypeRepository = new GenotypeRepository(); //creating Repositories
            var traitRepository = new TraitRepository();
            var personRepository = new PersonRepository();

            while (geneticCounsellorScreen == true)
            {
                Console.WriteLine("1) Add Person");
                Console.WriteLine("2) List all Persons");
                Console.WriteLine("3) Create trait");
                Console.WriteLine("4) List all traits");

               
                string personFilename = "PersonsSeedData.txt";
                string traitFilename = "TraitSeedData.txt";


                personRepository.LoadFile(personFilename);
                traitRepository.LoadFile(traitFilename);
                


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
                        ListAllPersonsScreen(sb,personRepository);
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

        private static void CreateTraitScreen(TraitRepository traitRepository) 
        {
            Console.WriteLine("Name of trait:");// Colourblindness
            string inputName = Console.ReadLine();
            Console.WriteLine("Type of inheritance (currently Dominant or Recessive only)"); //Reccessive
            Dominance inputInheritanceType = (Dominance)Enum.Parse(typeof(Dominance),Console.ReadLine(),true); 
            Console.WriteLine("What letter should represent it?");// C
            char inputAlelleName = Convert.ToChar(Console.ReadLine());

            var trait = new Trait(inputName, inputAlelleName, inputInheritanceType);
            traitRepository.AddTrait(trait);
            

        }
        
        private static void AddPersonScreen(PersonRepository personRepository)
        {
            
            Console.WriteLine("Name (first and last): ");
            string inputName = Console.ReadLine();
            Console.WriteLine("Sex ( Male or Female ): ");
            string inputtedSex = Console.ReadLine();
            Sex inputSex = (Sex)Enum.Parse(typeof(Sex),inputtedSex,true);
            Console.WriteLine("Living (true or false): ");
            string inputtedLiving = Console.ReadLine();
            bool inputLiving = (bool)Convert.ToBoolean(inputtedLiving);

            var person = new Person(inputName,inputSex,inputLiving); 
            personRepository.AddPerson(person);
            
        }
        private static void ListAllPersonsScreen(StringBuilder sb, PersonRepository personRepository)// Implement ability to load file into list of persons.
        {
            var ListOfPersons = personRepository.ListPersons();
            int i = 0;
                        
            if (ListOfPersons.Count == 0)
            {
                sb.AppendLine("There are no Persons in this system, please add one.");
                sb.AppendLine("____________________________________________________");                
            }
            else
            {
                foreach (var person in ListOfPersons)
                {
                    i++;
                    sb.AppendLine(i + " " + person.ToString()); 
                }
                
            }
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1) Select a Person");
            Console.WriteLine("2) Delete a Person");
            Console.WriteLine("-----------------------------------------------------");

            //int ListAllPersonsScreenOption = MenuUserInputInt(2); 

            //switch (ListAllPersonsScreenOption)
            //{
            //    case 1:
            //        FindPersonByIndex();
            //        break;
            //    default:
            //        throw new Exception("Invalid Menu input"); 
            //}

        }

        private static void FindPersonByIndex()
        {
            throw new NotImplementedException();
        }


        //static void FindRecordByIndex(string [] records)
        //{
        //    Console.Write("Enter Index No. :");
        //    try
        //    {
        //        int index = Convert.ToInt32(Console.ReadLine());                          From Student Records
        //        WriteRecordToConsole(records[index]);
        //    }
        //    catch (IndexOutOfRangeException)
        //    {
        //        Console.WriteLine("Not a valid index");
        //    }
        //    catch (FormatException)
        //    {
        //        Console.WriteLine("Not a number");
        //    }
        //}


        //static void WriteRecordToConsole(string record)
        //{
        //    var formatted = record.Replace(",", "\t");
        //    Console.WriteLine(formatted);
        //}

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
