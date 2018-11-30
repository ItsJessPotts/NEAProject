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
            Console.WriteLine("Enter 0 to return to a previous menu at any screen");
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
            GeneticCounsellorDbContext Db = null;
            using (Db = new GeneticCounsellorDbContext("Jess1"))
            {

                var traitRepository = new TraitRepository(Db);
                var personRepository = new PersonRepository(Db);
                var genotypeRepository = new GenotypeRepository(Db);
                var rng = new RealRandomNumberGenerator(); 

                string personFilename = @"C:\Users\potts\Desktop\PersonsSeedData.txt";
                string traitFilename = @"C:\Users\potts\Desktop\TraitSeedData.txt";

                geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename, rng);
            }
                                                        
        }

        private static void geneticCounsellorScreen( GenotypeRepository genotypeRepository,TraitRepository traitRepository, PersonRepository personRepository, string personFilename, string traitFilename,RealRandomNumberGenerator rng)
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
                    AddPersonScreen(personRepository, genotypeRepository);
                    geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename,rng);
                    break;
                case 2:
                    ListAllPersonsChoice(personRepository);
                    int ListAllPersonsScreenOption = MenuUserInputInt(2);

                    switch (ListAllPersonsScreenOption)
                    {
                        case 0:
                            geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename,rng);
                            break;
                        case 1:
                            Person SelectedPerson = FindPersonByIndex(personRepository);
                            PersonScreen(SelectedPerson, personRepository, genotypeRepository,rng);

                            break;
                        default:
                            throw new Exception("Invalid Menu input");
                    }

                    break;
                case 3:
                    CreateTraitScreen(traitRepository);

                    break;
                case 4:
                    ListAllTraitsChoice(traitRepository);

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

        private static void ListAllTraitsChoice(TraitRepository traitRepository)
        {
            StringBuilder sb2 = new StringBuilder();
            ListAllTraitsScreen(sb2,traitRepository);
            string s2 = sb2.ToString();
            Console.Write(s2);
            Console.WriteLine("____________________________________________________");
        }

        private static void ListAllTraitsScreen(StringBuilder sb2, TraitRepository traitRepository)
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

           
            traitRepository.AddTrait(inputName, inputAlelleName, inputInheritanceType);
            

        }
        
        private static void AddPersonScreen(PersonRepository personRepository, GenotypeRepository genotypeRepository)
        {
            try
            {
                Console.WriteLine("Name (first and last): ");
                string inputName = Console.ReadLine();
                if (inputName == "")
                {
                    throw new Exception();
                }
                Console.WriteLine("Sex ( Male or Female ): ");
                string inputtedSex = Console.ReadLine();
                Sex inputSex = (Sex)Enum.Parse(typeof(Sex), inputtedSex, true);
                if (inputSex == Sex.Female || inputSex == Sex.Male)
                {
                    Console.WriteLine("Living (true or false): ");
                    string acceptableTrueString = "true";
                    string acceptableFalseString = "False";
                    string inputtedLiving = Console.ReadLine();
                    if (inputtedLiving == acceptableTrueString || inputtedLiving == acceptableFalseString)
                    {
                        bool inputLiving = (bool)Convert.ToBoolean(inputtedLiving);
                        personRepository.AddPerson(inputName, inputSex, inputLiving, "unaffected");
                    }
                }                
                else
                {
                    throw new Exception();
                }                
            }
            catch (Exception)
            {
                Console.WriteLine("Input was not valid, please try again.");
                AddPersonScreen(personRepository, genotypeRepository);
            }
                                  
            

           



        }
        private static void ListAllPersonsScreen(StringBuilder sb, PersonRepository personRepository)
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
            Console.WriteLine("_________________________________________________");
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

      
        private static void PersonScreen(Person SelectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            
            Console.WriteLine("Name: " + SelectedPerson.Name);
            Console.WriteLine("Sex: " + SelectedPerson.Sex);
            
            string traitOutput = "";
            foreach (var traits in SelectedPerson.Phenotype.Traits)
            {
                traitOutput = traitOutput + traits.ToString() + ",";
            }
            Console.WriteLine("Traits: " + traitOutput);
            
            string outputGenotypes = "";
            foreach (var genotype in SelectedPerson.Phenotype.TraitGenotypes)
            {
                outputGenotypes = outputGenotypes + genotype.ToString() + ',';               
            }
            Console.WriteLine("Genotypes: " + outputGenotypes);

            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1) Edit Person");//TO DO
            Console.WriteLine("2) Combine Genotype with other person");
            Console.WriteLine("-----------------------------------------------------");
                        
            PersonScreenMenu(personRepository,SelectedPerson,genotypeRepository,rng);
                      
        }

        private static void PersonScreenMenu(PersonRepository personRepository, Person SelectedPerson, GenotypeRepository genotypeRepository,RealRandomNumberGenerator rng)
        {
            int PersonMenuChoice = MenuUserInputInt(2);
            switch (PersonMenuChoice)
            {
                case 0:
                    StringBuilder sb = new StringBuilder();
                    ListAllPersonsScreen(sb, personRepository);
                    break;
                case 1:
                    EditPersonScreen(personRepository, SelectedPerson, genotypeRepository, rng);
                    break;
                case 2: //Combine Genotypes
                    Genotype resultingGenotype = CombineGenotypesScreen(SelectedPerson, personRepository, genotypeRepository,rng);
                    Console.WriteLine(resultingGenotype.ToString()+" is the most likely genotype combination in offspring between these two persons");
                    Console.ReadKey();
                    break;
                default:
                    throw new Exception("Invalid Menu input");
            }
        }

        private static Genotype CombineGenotypesScreen(Person selectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository,RealRandomNumberGenerator rng)//TO DO: Change Seed Data to be compatible with added Genotypes
        {
            Genotype firstSelectedPerson = GetSelectedPersonsGenotype(selectedPerson); //Selected Genotypes            
            Console.WriteLine("Follow Instructions below to select a person to combine genotypes with:"); //Ensure only males and females are compatible 
            //var otherPersonIndex = Console.ReadLine();
            Person otherPerson = FindPersonByIndex(personRepository);
            Genotype otherSelectedPerson = GetSelectedPersonsGenotype(otherPerson);
            Genotype resultingGenotype = firstSelectedPerson.CombineGenotypes(otherSelectedPerson,genotypeRepository,rng);
            return resultingGenotype;
            
        }

        private static Genotype GetSelectedPersonsGenotype(Person selectedPerson)
        {
            var listOfSelectedPersonGenotypes = selectedPerson.Phenotype.TraitGenotypes;
            int number = 1;
            
            foreach (var genotype in selectedPerson.Phenotype.TraitGenotypes)
            {
                Console.WriteLine(number + " " + genotype.ToString());
                number++;
            }
            
            Console.WriteLine("Please input the index of genotype you want to combine");
            int index = Convert.ToInt32(Console.ReadLine());
            Genotype selectedPersonGenotype = listOfSelectedPersonGenotypes[index - 1];
            return selectedPersonGenotype;

        }

        private static void EditPersonScreen(PersonRepository personRepository, Person SelectedPerson, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            int EditPersonMenuChoice = MenuUserInputInt(4);
            switch (EditPersonMenuChoice)
            {
                case 0:
                    PersonScreenMenu(personRepository, SelectedPerson, genotypeRepository, rng);
                    break;
                case 1:
                    ChangeName();
                    break;
                case 2:
                    ChangeSex();
                    break;
                case 3:
                    changeLiving();
                    break;
                case 4:
                    AddGenotype();
                    break;
                case 5:
                    changeGenotype();
                    break;
                case 6:
                    AddPhenotype();
                case 7:
                    changePhenotype();
                    break;
      
            }
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
