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
            bool MainMenuScreenChoice = true;
            while (MainMenuScreenChoice == true)
            {
                Console.WriteLine("____________________________________________________");
                Console.WriteLine("Welcome");
                Console.WriteLine("1) Genetic Counsellor");
                Console.WriteLine("2) Hardy Weinberg Calculator");
                Console.WriteLine("3) Quit");
                Console.WriteLine("Enter 0 to return to a previous menu at any screen");
                Console.WriteLine("____________________________________________________");
                int mainMenuChoice = MenuUserInputInt(3);

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
                    case 3:
                        Environment.Exit(0);
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
            Console.Clear();
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("1) Add Person");
            Console.WriteLine("2) List all Persons");
            Console.WriteLine("3) Create trait");
            Console.WriteLine("4) List all traits");
            Console.WriteLine("5) Create family tree");
            Console.WriteLine("____________________________________________________");

            int geneticCounsellorMenuOption = MenuUserInputInt(5);

            switch (geneticCounsellorMenuOption)
            {
                case 0:
                    Console.Clear();
                    MainMenuScreen();
                    break;
                case 1:
                    AddPersonScreen(personRepository, genotypeRepository);
                    geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename,rng);
                    break;
                case 2:
                    ListAllPersonsChoice(personRepository,genotypeRepository);
                    int ListAllPersonsScreenOption = MenuUserInputInt(2);

                    switch (ListAllPersonsScreenOption)
                    {
                        case 0:
                            geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename,rng);
                            break;
                        case 1:
                            Person selectedPerson = FindPersonByIndex(personRepository);
                            PersonScreen(traitRepository,selectedPerson, personRepository, genotypeRepository,rng); //SELECTS A PERSON
                            break;
                        case 2:
                            Person selectedPersonToDelete = FindPersonByIndex(personRepository); //DELETES A PERSON
                            personRepository.DeletePerson(selectedPersonToDelete); 
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
                case 5:
                    FamilyTreeScreen(personRepository, genotypeRepository);
                    break;
                
                default:
                    throw new Exception("Invalid Menu input");
            }
        }

        private static void FamilyTreeScreen(PersonRepository personRepository, GenotypeRepository genotypeRepository)
        {
            StringBuilder sb = new StringBuilder();
            ListAllPersonsScreen(sb,personRepository,genotypeRepository);
            Person SelectedPerson = FindPersonByIndex(personRepository);
            Console.WriteLine(SelectedPerson.Mother + "            " + SelectedPerson.Father);
            Console.WriteLine("    |                                           |");
            Console.WriteLine("    |                                           |");
            Console.WriteLine("    |                                           |");
            Console.WriteLine("    |                                           |");
            Console.WriteLine("    --------------------|------------------------");
            Console.WriteLine("                        |                     ");
            Console.WriteLine("                        |                     ");
            Console.WriteLine("                        |                     ");
            Console.WriteLine("               " +SelectedPerson);


            
        }

        private static void ListAllPersonsChoice(PersonRepository personRepository,GenotypeRepository genotypeRepository)
        {
            StringBuilder sb = new StringBuilder();
            ListAllPersonsScreen(sb, personRepository,genotypeRepository);
            string s = sb.ToString();
            Console.Write(s);
            Console.WriteLine("____________________________________________________");
            {
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("1) Select a Person");
                Console.WriteLine("2) Delete a Person");
                Console.WriteLine("-----------------------------------------------------");
            }

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
            Console.Clear();
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
            Console.Clear();
            Console.WriteLine("Name of trait:");// Colourblindness
            string inputName = Console.ReadLine();
            Console.WriteLine("Type of inheritance (currently Dominant or Recessive only)"); //Reccessive
            Dominance inputInheritanceType = (Dominance)Enum.Parse(typeof(Dominance),Console.ReadLine(),true); 
            Console.WriteLine("What letter should represent it?");// C
            string inputAlelleName = Console.ReadLine();

           
            traitRepository.AddTrait(inputName, inputAlelleName, inputInheritanceType);
            
        }
        
        private static void AddPersonScreen(PersonRepository personRepository, GenotypeRepository genotypeRepository)
        {
            Console.Clear();
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

        private static void ListAllPersonsScreen(StringBuilder sb, PersonRepository personRepository, GenotypeRepository genotypeRepository)
        {
            Console.Clear();
            var ListOfPersons = personRepository.ListPersons();
            int i = 0;

            if (ListOfPersons.Count == 0)
            {
                sb.AppendLine("There are no Persons in this system, please add one.");
                sb.AppendLine("____________________________________________________");
                Console.ReadKey();
                AddPersonScreen(personRepository, genotypeRepository);
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
            
        }

        private static Person FindPersonByIndex(PersonRepository personRepository)
        {
            Console.WriteLine("_________________________________________________");
            Console.Write("Enter a Person Index No. :");
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
      
        private static void PersonScreen(TraitRepository traitRepository, Person SelectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            Console.Clear();
            
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
            try
            {
                UpdatePhenotype(SelectedPerson);
            }
            catch (Exception)
            {

                Console.WriteLine("Phenotype: N/A");
            }
            try
            {
                Console.WriteLine("Mother: " + SelectedPerson.Mother.Name); //recursive viewing of the whole family
                Console.WriteLine("Father: " + SelectedPerson.Father.Name);
            }
            catch (Exception)
            {

                Console.WriteLine("Incomplete parents inputted.");
            }
           
                        
            PersonScreenMenu(traitRepository, personRepository,SelectedPerson,genotypeRepository,rng);
                      
        }

        private static void PersonScreenMenu(TraitRepository traitRepository, PersonRepository personRepository, Person selectedPerson, GenotypeRepository genotypeRepository,RealRandomNumberGenerator rng )
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1) Edit Person");
            Console.WriteLine("2) Combine Genotype with other person");
            Console.WriteLine("3) Update Phenotype");
            Console.WriteLine("-----------------------------------------------------");
            int PersonMenuChoice = MenuUserInputInt(3);
            switch (PersonMenuChoice)
            {
                case 0:
                    Console.Clear();
                    StringBuilder sb = new StringBuilder(); //return to last menu
                    ListAllPersonsScreen(sb, personRepository,genotypeRepository);
                    break;
                case 1://Edit Person
                    EditPersonScreen(traitRepository, personRepository, selectedPerson, genotypeRepository, rng);
                    break;
                case 2: //Combine Genotypes
                    Genotype resultingGenotype = CombineGenotypesScreen(selectedPerson, personRepository, genotypeRepository,rng);
                    Console.WriteLine(resultingGenotype.ToString()+" is the most likely genotype combination in offspring between these two persons");
                    Console.ReadKey();
                    break;
                case 3: //update Phenotype
                    UpdatePhenotype(selectedPerson);
                    break;
                default:
                    throw new Exception("Invalid Menu input");
            }
        }

        private static Genotype CombineGenotypesScreen(Person selectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository,RealRandomNumberGenerator rng)//TO DO: Change Seed Data to be compatible with added Genotypes
        {
            Genotype firstSelectedPerson = GetSelectedPersonsGenotype(selectedPerson); //Selected Genotypes            
            Console.WriteLine("Follow Instructions below to select a person to combine genotypes with:"); //Ensure only males and females are compatible 
            StringBuilder sb = new StringBuilder();
            ListAllPersonsScreen(sb, personRepository, genotypeRepository);
            string s = sb.ToString();
            Console.Write(s);
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

        private static void EditPersonScreen(TraitRepository traitRepository, PersonRepository personRepository, Person selectedPerson, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            Console.WriteLine("1) Change Name");
            Console.WriteLine("2) Change Sex");
            Console.WriteLine("3) Change Living");
            Console.WriteLine("4) Add an exsisting Genotype from database");
            Console.WriteLine("5) Change an exsisting Genotype ");
            Console.WriteLine("6) Add an exsisting Trait from database");
            Console.WriteLine("7) Add Mother from exsisting list of Persons");
            Console.WriteLine("8) Add Father from exsisting list of Persons");
           
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            int EditPersonMenuChoice = MenuUserInputInt(9);
            switch (EditPersonMenuChoice)
            {
                case 0:
                    PersonScreenMenu(traitRepository, personRepository, selectedPerson, genotypeRepository, rng);
                    break;
                case 1:
                    ChangeName(traitRepository,selectedPerson, personRepository, genotypeRepository, rng); 
                    break;
                case 2:
                    ChangeSex(traitRepository,selectedPerson, personRepository, genotypeRepository, rng);
                    break;
                case 3:
                    changeLiving(traitRepository,selectedPerson, personRepository, genotypeRepository, rng);
                    break;
                case 4:
                    StringBuilder sb = new StringBuilder();
                    ListAllPersonsScreen(sb, personRepository,genotypeRepository);
                    string s = sb.ToString();
                    Console.Write(s);                    
                    AddExsistingGenotype(traitRepository, selectedPerson, genotypeRepository, personRepository, rng);
                    break;
                case 5:
                    ChangeGenotype(selectedPerson);
                    break;
                case 6:
                    AddExsistingTrait(selectedPerson, traitRepository,  personRepository, genotypeRepository, rng);
                    break;
                case 7:
                    AddMother(traitRepository, selectedPerson, personRepository, genotypeRepository, rng);
                    break;
                case 8:
                    AddFather(traitRepository,selectedPerson, personRepository, genotypeRepository, rng);
                    break;                
            }
        }

        private static void AddExsistingTrait(Person selectedPerson, TraitRepository traitRepository, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            Console.Clear();
            Console.WriteLine("Already exsisting Traits:");
            int num = 0;
            foreach (var t in traitRepository.ListTraits())
            {
                Console.WriteLine(num + ") " + t);
                num++;
            }
            Console.WriteLine("Select a trait from the list:");
            int traitIndex = Convert.ToInt32(Console.ReadLine());
            var allTraits = traitRepository.ListTraits();
            selectedPerson.AddTraitToPerson(allTraits[traitIndex]);
            PersonScreen(traitRepository, selectedPerson, personRepository, genotypeRepository, rng);
        }

        private static void AddMother(TraitRepository traitRepository, Person selectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            StringBuilder sb = new StringBuilder();
            ListAllPersonsScreen(sb, personRepository,genotypeRepository);
            string s = sb.ToString();
            Console.Write(s);
            Person personToBeMother = FindPersonByIndex(personRepository);
            bool can = selectedPerson.CanAddMother(personToBeMother);
            if (can)
            {
                selectedPerson.AddMotherToPerson(personToBeMother);
                PersonScreen(traitRepository,selectedPerson, personRepository, genotypeRepository, rng);

            }
            else
            {
                Console.WriteLine("Only biological females can be entered as biological mothers");
            }            
            PersonScreen(traitRepository ,selectedPerson,personRepository, genotypeRepository, rng); //returns user to last menu
        }

        private static void AddFather(TraitRepository traitRepository, Person selectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            StringBuilder sb = new StringBuilder();
            ListAllPersonsScreen(sb, personRepository,genotypeRepository);
            string s = sb.ToString();
            Console.Write(s);
            Person personToBeFather = FindPersonByIndex(personRepository);
            bool can =selectedPerson.CanAddFather(personToBeFather);
            if (can)
            {
                selectedPerson.AddFatherToPerson(personToBeFather);
                PersonScreen(traitRepository, selectedPerson, personRepository, genotypeRepository, rng);
            }
            else
            {
                Console.WriteLine("Only biological males can be entered as biological fathers.");
            }
            

        }
        
        private static void UpdatePhenotype(Person SelectedPerson)
        {
            SelectedPerson.Phenotype.GeneratePhenotypeName();
            Console.WriteLine("Phenotype:" + SelectedPerson.Phenotype.PhenotypeName);
        }

        private static void ChangeGenotype(Person SelectedPerson)
        {
         
        }

        private static void AddExsistingGenotype(TraitRepository traitRepository, Person SelectedPerson, GenotypeRepository genotypeRepository, PersonRepository personRepository, RealRandomNumberGenerator rng)//GeneticCounsellorDbContext Db
        {
            Console.Clear();
            Console.WriteLine("Already exsisting Genotypes:");
            int num = 0;
            foreach (var g in genotypeRepository.ListGenotypes())
            {
                Console.WriteLine(num + ") " + g );
                num++;
            }
            Console.WriteLine("Select a genotype from list:");
            int genotypeIndex = Convert.ToInt32(Console.ReadLine());
            var Allgenotypes = genotypeRepository.ListGenotypes();
            SelectedPerson.AddGenotypeToPerson(Allgenotypes[genotypeIndex]);
            PersonScreen(traitRepository, SelectedPerson, personRepository, genotypeRepository, rng); //returns user to last menu

        }

        private static void changeLiving(TraitRepository traitRepository, Person SelectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            Console.WriteLine("Please input a new Living status (True, False)");            
            string acceptableTrueString = "true";
            string acceptableFalseString = "False";
            string inputtedLiving = Console.ReadLine();
            if (inputtedLiving == acceptableTrueString || inputtedLiving == acceptableFalseString)
            {
                bool inputLiving = (bool)Convert.ToBoolean(inputtedLiving);
                SelectedPerson.Living = inputLiving;
            }
            PersonScreen(traitRepository,SelectedPerson, personRepository, genotypeRepository, rng); //returns user to last menu
        }

        private static void ChangeSex(TraitRepository traitRepository, Person SelectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            Console.WriteLine("Please input a new sex (Male, Female or Unknown)");
            string inputtedSex = Console.ReadLine();
            Sex newSex = (Sex)Enum.Parse(typeof(Sex), inputtedSex, true);

            SelectedPerson.Sex = newSex;
            PersonScreen(traitRepository, SelectedPerson,personRepository, genotypeRepository, rng); //returns user to last menu
        }

        private static void ChangeName(TraitRepository traitRepository,Person SelectedPerson,PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng)
        {
            
            Console.WriteLine("Please input a new full name");
            string newName = Console.ReadLine();
            if (newName == "")
            {
                Console.WriteLine("It is against the Geneva convention for a person to be denied a name");
            }
          
            SelectedPerson.Name = newName;
            PersonScreen(traitRepository,SelectedPerson, personRepository, genotypeRepository, rng);           
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
