using HardyWeinbergTest;
using PedigreeObjects;
using System;
using System.Text;

namespace ConsoleUI
{
    class Program
    {       
        static void Main()
        {
            MainMenuScreen();                                  
        }

        private static void MainMenuScreen()
        {
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("Welcome");
            Console.WriteLine("1) Genetic Counsellor");
            Console.WriteLine("2) Hardy Weinberg Calculator");
            Console.WriteLine("Select 0 to return to a previous menu at any screen");
            Console.WriteLine("____________________________________________________");
            int mainMenuChoice = MenuUserInputInt(2);

            switch (mainMenuChoice)
            {
                case 0:
                    MainMenuScreen();
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

        private static void HardyWeinbergCalculator()
        {

            HardyWeinberg.Main();
        }

        private static void GeneticCounsellor()
        {
            
            
            var genotypeRepository = new GenotypeRepository(); 
            var traitRepository = new TraitRepository();
            var personRepository = new PersonRepository();

            string personFilename = "PersonsSeedData.txt";
            string traitFilename = "TraitSeedData.txt";

            personRepository.LoadFile(personFilename);
            traitRepository.LoadFile(traitFilename);

            geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename);
                                                        
        }

        private static void geneticCounsellorScreen(GenotypeRepository genotypeRepository, TraitRepository traitRepository, PersonRepository personRepository, string personFilename, string traitFilename)
        {
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("1) Add Person");
            Console.WriteLine("2) List all Persons");
            Console.WriteLine("3) Create trait");
            Console.WriteLine("4) List all traits");
            Console.WriteLine("____________________________________________________");

            int geneticCounsellorMenuOption = MenuUserInputInt(4);

            switch (geneticCounsellorMenuOption)
            {
                case 0:
                    MainMenuScreen();
                    break;
                case 1:
                    AddPersonScreen(personRepository, personFilename);

                    break;
                case 2:
                    ListAllPersonsChoice(personRepository);
                    int ListAllPersonsScreenOption = MenuUserInputInt(2);

                    switch (ListAllPersonsScreenOption)
                    {
                        case 0:
                            geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename);
                            break;
                        case 1:
                            Person SelectedPerson = FindPersonByIndex(personRepository);
                            PersonScreen(SelectedPerson, personRepository);

                            break;
                        default:
                            throw new Exception("Invalid Menu input");
                    }

                    break;
                case 3:
                    CreateTraitScreen(traitRepository);

                    break;
                case 4:
                    ListAllTraitsChoice(genotypeRepository, traitRepository);

                    break;
                default:
                    throw new Exception("Invalid Menu input");
            }
        }

        private static void ListAllPersonsChoice(PersonRepository personRepository)
        {
            StringBuilder sb = new StringBuilder();
            ListAllPersonsScreen(sb, personRepository);
            string s = sb.ToString();
            Console.Write(s);
            Console.WriteLine("____________________________________________________");
           
        }

        private static void ListAllTraitsChoice(GenotypeRepository genotypeRepository,TraitRepository traitRepository)
        {
            StringBuilder sb2 = new StringBuilder();
            ListAllTraitsScreen(sb2, genotypeRepository, traitRepository);
            string s2 = sb2.ToString();
            Console.Write(s2);
            Console.WriteLine("____________________________________________________");
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
        
        private static void AddPersonScreen(PersonRepository personRepository,string personFilename)
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
            personRepository.WritePersonToTexfile(person,personFilename);

            
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
                Console.WriteLine("____________________________________________________");
                foreach (var person in ListOfPersons)
                {
                    i++;
                    sb.AppendLine(i + " " + person.ToString());
                }


            }
            
            {
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("1) Select a Person");
                Console.WriteLine("2) Delete a Person");
                Console.WriteLine("-----------------------------------------------------");
            }
            


        }

        private static Person FindPersonByIndex(PersonRepository personRepository)
        {
            Console.Write("Enter Index No. :");
            try
            {
                var ListOfPersons = personRepository.ListPersons();
                int index = Convert.ToInt32(Console.ReadLine());
                Person SelectedPerson = ListOfPersons[index - 1];
                return SelectedPerson;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Not a valid index");
                return null;
            }
            catch (FormatException)
            {
                Console.WriteLine("Not a number");
                return null;
            }
            
        }

      
        private static void PersonScreen(Person SelectedPerson, PersonRepository personRepository)//To Do: Add options for methods
        {
            Console.WriteLine("Name: " + SelectedPerson.Name);
            Console.WriteLine("Sex: " + SelectedPerson.Sex);
            Console.WriteLine("Traits: " + SelectedPerson.Traits);
            Console.WriteLine("Genotypes: " + SelectedPerson.Genotypes);

            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1) Edit Person");
            Console.WriteLine("2) Combine Genotype with other person");
            Console.WriteLine("-----------------------------------------------------");
                        
            PersonScreenMenu(personRepository,SelectedPerson);
                      
        }

        private static void PersonScreenMenu(PersonRepository personRepository, Person SelectedPerson)
        {
            int PersonMenuChoice = MenuUserInputInt(2);
            switch (PersonMenuChoice)
            {
                case 0:
                    StringBuilder sb = new StringBuilder();
                    ListAllPersonsScreen(sb, personRepository);
                    break;
                case 1:
                    EditPersonScreen();
                    break;
                case 2:
                    CombineGenotypesScreen(SelectedPerson, personRepository);
                    break;
                default:
                    throw new Exception("Invalid Menu input");
            }
        }

        private static void CombineGenotypesScreen(Person selectedPerson, PersonRepository personRepository)//TO DO: Change Seed Data to be compatible with added Genotypes
        {
            selectedPerson.Genotypes.ListGenotypes();
            Console.WriteLine("Please select a person to combine genotypes with:");
            Person otherPerson = FindPersonByIndex(personRepository);
            otherPerson.Genotypes.ListGenotypes();
        }

        private static void EditPersonScreen()
        {
            throw new NotImplementedException();
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
