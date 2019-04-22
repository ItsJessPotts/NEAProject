using HardyWeinbergTest;
using PedigreeObjects;
using System;
using System.Collections.Generic;
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
            GeneticCounsellorDbContext context = null;
            using (context = new GeneticCounsellorDbContext("Jess1"))
            {

                var traitRepository = new TraitRepository(context);
                var personRepository = new PersonRepository(context);
                var genotypeRepository = new GenotypeRepository(context);
                var rng = new RealRandomNumberGenerator(); 

                string personFilename = @"C:\Users\potts\Desktop\PersonsSeedData.txt";
                string traitFilename = @"C:\Users\potts\Desktop\TraitSeedData.txt";

                geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename, rng, context);
            }
                                                        
        }

        private static void geneticCounsellorScreen( GenotypeRepository genotypeRepository,TraitRepository traitRepository, PersonRepository personRepository, string personFilename, string traitFilename,RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
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
                    geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename,rng, context);
                    break;
                case 2:
                    ListAllPersonsChoice(personRepository,genotypeRepository);
                    int ListAllPersonsScreenOption = MenuUserInputInt(3);

                    switch (ListAllPersonsScreenOption)
                    {
                        case 0:
                            geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename,rng, context);
                            break;
                        case 1:
                            Person selectedPerson = FindPersonByIndex(personRepository);
                            PersonScreen(traitRepository,selectedPerson, personRepository, genotypeRepository,rng, context); //SELECTS A PERSON
                            break;
                        case 2:
                            DeletePerson(personRepository,genotypeRepository);                            
                            break;
                        case 3:
                            AddPersonScreen(personRepository, genotypeRepository);
                            geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename, rng, context);
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
                    int ListAllTraitsScreenOption = MenuUserInputInt(1);
                    switch (ListAllTraitsScreenOption)
                    {
                        case 0:
                            geneticCounsellorScreen(genotypeRepository, traitRepository, personRepository, personFilename, traitFilename, rng, context);
                            break;
                        case 1:
                            CreateTraitScreen(traitRepository);
                            break;
                        case 2:
                            Trait selectedTraitToDelete = FindTraitByIndex(traitRepository); //DELETES A TRAIT
                            traitRepository.DeleteTrait(selectedTraitToDelete);
                            Console.WriteLine("#######################");
                            Console.WriteLine("Trait has been deleted.");
                            Console.WriteLine("#######################");
                            break;
                    }
                    break;
                case 5:
                    FamilyTreeScreen(personRepository, genotypeRepository);
                    break;
                
                default:
                    throw new Exception("Invalid Menu input");
            }
        }

        private static void DeletePerson(PersonRepository personRepository, GenotypeRepository genotypeRepository)
        {
            Console.WriteLine("Are you sure you wish to delete this person? Yes/No");
            string answer = Console.ReadLine();
            if (answer == "Yes" || answer == "yes")
            {
                Person selectedPersonToDelete = FindPersonByIndex(personRepository); //DELETES A PERSON
                personRepository.DeletePerson(selectedPersonToDelete);
                Console.WriteLine("#########################");
                Console.WriteLine("Person has been deleted.");
                Console.WriteLine("#########################");
            }
            else
            {
                ListAllPersonsChoice(personRepository,genotypeRepository);    
            }
            
        }

        private static Trait FindTraitByIndex(TraitRepository traitRepository)
        {
            Console.WriteLine("_________________________________________________");
            Console.Write("Enter a Trait Index No. :");
            try
            {
                var ListOftraits = traitRepository.ListTraits();
                int index = Convert.ToInt32(Console.ReadLine());
                Trait SelectedTrait = ListOftraits[index - 1];
                return SelectedTrait;
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

        private static void FamilyTreeScreen(PersonRepository personRepository, GenotypeRepository genotypeRepository)
        {
            StringBuilder sb = new StringBuilder();
            ListAllPersonsScreen(sb,personRepository,genotypeRepository);
            string s = sb.ToString();
            Console.Write(s);
            Person SelectedPerson = FindPersonByIndex(personRepository);
            Console.WriteLine("How many generations should this system display?");
            int numberOfGenerations = Convert.ToInt32(Console.ReadLine());
            DrawGeneration(SelectedPerson, numberOfGenerations);
            

        }

        private static void DrawGeneration(Person selectedPerson, int numberOfGenerations)
        {
            
            var pedTree = new List<List<Person>>();
            for (int i = 0; i < numberOfGenerations; i++)
            {
                pedTree.Add(new List<Person>()); //initialising the list
            }
            pedTree[0].Add(selectedPerson);

            for (int i = 0; i < numberOfGenerations-1; i++)
            {
                var layer = pedTree[i];
                foreach (Person p in layer)
                {
                    Person father = p.Father;
                    Person mother = p.Mother;
                    pedTree[i + 1].Add(mother);
                    pedTree[i + 1].Add(father);
                }
            }
            int layerNumber = -1;
            foreach (var layer in pedTree)
            {
                layerNumber++;
                OutputBoxLine(layerNumber,layer,numberOfGenerations);
                OutputNames(pedTree, numberOfGenerations, layerNumber, layer);
                Console.WriteLine();
                OutputBoxLine(layerNumber, layer,numberOfGenerations);
                Console.WriteLine();
                OutputArrow(layerNumber, layer, numberOfGenerations);                
            }            
        }

        private static void OutputArrow(int layerNumber, List<Person> layer, int numberOfGenerations )
        {
            foreach (Person p in layer)
            {
                string LeftGap = CalculateLeftGap(layerNumber, layer, numberOfGenerations);
                string arrow = "         |          ";
                if (layerNumber <= numberOfGenerations)
                {
                    Console.Write(LeftGap + arrow);
                }
                else
                {
                    Console.Write("");
                }                            
            }
            Console.WriteLine();
        }

        private static void OutputNames(List<List<Person>> pedTree,  int numberOfGenerations, int layerNumber, List<Person> layer)
        {
            foreach (Person p in layer)
            {
                string LeftGap = CalculateLeftGap(layerNumber, layer,  numberOfGenerations);
                string Person = CalculateNamePadding(p);
                Console.Write(LeftGap + Person);
            }
        }

        private static void OutputBoxLine(int layerNumber, List<Person> layer, int numberOfGenerations)
        {
            foreach (Person p in layer)
            {
                string LeftGap = CalculateLeftGap(layerNumber, layer, numberOfGenerations);
                string line = "--------------------";//20 lines
                Console.Write(LeftGap + line);
            }
            Console.WriteLine();
                      
        }

        private static string CalculateNamePadding(Person p)
        {
            string name = "";
            if (p == null)
            {
                name = "unknown";
            }
            else
            {
                name = p.Name;                
            }
            string paddedName = name + "                  ";
            int constant = 18;
            string Name18 = paddedName.Substring(0, constant);
            string NameBox = "|" + Name18 + "|";
            return NameBox;
        }

        private static string CalculateLeftGap(int layerNumber, List<Person> layer, int NumberOfGenerations)
        {            
            int width = 210;
            int numberOfNames = layer.Count;
            int numberOfGaps = numberOfNames + 1;
            int NameLength = 20;
            string FinishedGap = "";
            string gap = " ";
            if (layerNumber == 0)
            {
                int GapLength = ((width - NameLength) / numberOfGaps);
                for (int i = 0; i < GapLength; i++)
                {
                    FinishedGap = FinishedGap + gap;
                }
                return FinishedGap;
            }
            else //(layerNumber >= 1)
            {
                int GapLength = (width - (numberOfNames * NameLength)) / numberOfGaps;
                for (int i = 0; i < GapLength; i++)
                {
                    FinishedGap = FinishedGap + gap;
                }
                return FinishedGap;
            }            
        }

        private static void ListAllPersonsChoice(PersonRepository personRepository,GenotypeRepository genotypeRepository)
        {
            StringBuilder sb = new StringBuilder();
            ListAllPersonsScreen(sb, personRepository,genotypeRepository);
            string s = sb.ToString();
            Console.Write(s);
            Console.WriteLine("____________________________________________________");
            
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("1) Select a Person");
                Console.WriteLine("2) Delete a Person");
                Console.WriteLine("3) Add a person");
                Console.WriteLine("-----------------------------------------------------");
            

        }

        private static void ListAllTraitsChoice(TraitRepository traitRepository)
        {
            StringBuilder sb2 = new StringBuilder();
            ListAllTraitsScreen(sb2,traitRepository);
            string s2 = sb2.ToString();
            Console.Write(s2);
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1) Add a trait");
            Console.WriteLine("2) Delete a trait");
            Console.WriteLine("-----------------------------------------------------");
        }

        private static void ListAllTraitsScreen(StringBuilder sb2, TraitRepository traitRepository)
        {
            Console.Clear();
            var ListOfTraits = traitRepository.ListTraits();
            int i = 0;
            if (ListOfTraits.Count == 0)
            {
                sb2.AppendLine("There are no Traits in this system, please add one.");
                sb2.AppendLine("____________________________________________________");
            }
            else
            {
                foreach (var t in ListOfTraits)
                {
                    i++;
                    sb2.AppendLine(i+ ") " + t.ToString());
                }
            }
        }

        private static void CreateTraitScreen(TraitRepository traitRepository) 
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Name of trait:");// Colourblindness
                string inputName = Console.ReadLine();
                Console.WriteLine("Type of inheritance (currently Dominant or Recessive only)"); //Reccessive
                Dominance inputInheritanceType = (Dominance)Enum.Parse(typeof(Dominance), Console.ReadLine(), true);
                Console.WriteLine("What letter should represent it?");// C
                string inputAlelleName = Console.ReadLine();
                traitRepository.AddTrait(inputName, inputAlelleName, inputInheritanceType);
                Console.Clear();
                StringBuilder sb2 = new StringBuilder();
                ListAllTraitsScreen(sb2, traitRepository);
                string s2 = sb2.ToString();
                Console.Write(s2);

            }
            catch (Exception)
            {

                Console.WriteLine("Input not recognised- Please try again");
                CreateTraitScreen(traitRepository);
            }
            
            
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
                if (inputSex == Sex.Female || inputSex == Sex.Male || inputSex == Sex.Unknown)
                {
                    Console.WriteLine("Living (true or false): ");
                    string acceptableTrueString = "true";
                    string acceptableFalseString = "false";
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
                Console.ReadLine();
                
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
                //return personRepository.FindPersonByID(index);
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
      
        private static void PersonScreen(TraitRepository traitRepository, Person SelectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
        {
            Console.Clear();
            
            Console.WriteLine("Name: " + SelectedPerson.Name);
            Console.WriteLine("Sex: " + SelectedPerson.Sex);
            Console.WriteLine("Living Status:" + SelectedPerson.Living);
            
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
           
                        
            PersonScreenMenu(traitRepository, personRepository,SelectedPerson,genotypeRepository,rng, context);
                      
        }

        private static void PersonScreenMenu(TraitRepository traitRepository, PersonRepository personRepository, Person selectedPerson, GenotypeRepository genotypeRepository,RealRandomNumberGenerator rng, GeneticCounsellorDbContext context )
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine("1) Edit Person");
            Console.WriteLine("2) Combine Genotype with other person");
            Console.WriteLine("3) Update Phenotype");
            Console.WriteLine("4) Calculate Parental Genotypes");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            int PersonMenuChoice = MenuUserInputInt(4);
            switch (PersonMenuChoice)
            {
                case 0:
                    Console.Clear();
                    StringBuilder sb = new StringBuilder(); //return to last menu
                    ListAllPersonsScreen(sb, personRepository,genotypeRepository);
                    break;
                case 1://Edit Person
                    EditPersonScreen(traitRepository, personRepository, selectedPerson, genotypeRepository, rng, context);
                    break;
                case 2: //Combine Genotypes
                    CombineGenotypesScreen(selectedPerson, personRepository, genotypeRepository,rng, traitRepository, context);
                                                           
                    break;
                case 3: //update Phenotype
                    UpdatePhenotype(selectedPerson);
                    break;
                case 4:
                    CalculateParentalGenotypes(selectedPerson, traitRepository, personRepository,genotypeRepository, rng, context);
                    break;
                default:
                    throw new Exception("Invalid Menu input");
            }
        }

        private static void CalculateParentalGenotypes(Person selectedPerson, TraitRepository traitRepository, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
        {
            Genotype genotypeToTrace = GetSelectedPersonsGenotype(selectedPerson, traitRepository, personRepository, genotypeRepository, rng, context);
            List<Genotype> parentalGenotypes = genotypeToTrace.CalculateParentalGenotypes(genotypeRepository, rng);
            Console.WriteLine("Here are the potential parental genotypes:");
            Console.WriteLine("====================================================");
            foreach (var genotype in parentalGenotypes)
            {
                Console.WriteLine(genotype);
            }
        }

        private static void CombineGenotypesScreen(Person selectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository,RealRandomNumberGenerator rng, TraitRepository traitRepository, GeneticCounsellorDbContext context)//TO DO: Change Seed Data to be compatible with added Genotypes
        {
            Console.Clear();
            Genotype firstSelectedPerson = GetSelectedPersonsGenotype(selectedPerson, traitRepository, personRepository, genotypeRepository, rng, context); //Selected Genotypes            
             //Ensure only males and females are compatible 
            StringBuilder sb = new StringBuilder();
            ListAllPersonsScreen(sb, personRepository, genotypeRepository);
            string s = sb.ToString();
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Please select a person to combine genotypes with:");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.Write(s);          
            Person otherPerson = FindPersonByIndex(personRepository);
            Genotype otherSelectedPerson = GetSelectedPersonsGenotype(otherPerson, traitRepository, personRepository, genotypeRepository, rng, context);
            Genotype resultingGenotype = firstSelectedPerson.MostLikelyGenotype(otherSelectedPerson,genotypeRepository,rng);
            Console.WriteLine("______________________________________________________________________________________________________________");
            Console.WriteLine(resultingGenotype.ToString() + " is the most likely genotype combination in offspring between these two persons");                      
            Console.WriteLine("______________________________________________________________________________________________________________");

        }
        

        private static Genotype GetSelectedPersonsGenotype(Person selectedPerson, TraitRepository traitRepository, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
        {
            var listOfSelectedPersonGenotypes = selectedPerson.Phenotype.TraitGenotypes;            
            if (selectedPerson.Phenotype.TraitGenotypes.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("This person does not have any genotypes");          
                PersonScreen(traitRepository, selectedPerson, personRepository,genotypeRepository , rng, context);
            }
            int number = 1;
            foreach (var genotype in selectedPerson.Phenotype.TraitGenotypes)
            {
                Console.WriteLine(number + ") " + genotype.ToString());
                number = number + 1;
            }
            try
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Please input the index of genotype you want to select: ");
               
                int index = Convert.ToInt32(Console.ReadLine());

                Genotype selectedPersonGenotype = listOfSelectedPersonGenotypes[index - 1];
                return selectedPersonGenotype;                
            }
            catch (Exception)
            {
                Console.WriteLine("There is no genotype located at that index. Please try again");
                return GetSelectedPersonsGenotype(selectedPerson, traitRepository, personRepository, genotypeRepository,rng, context);               
               
            }
                                             
        }

        private static void EditPersonScreen(TraitRepository traitRepository, PersonRepository personRepository, Person selectedPerson, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
        {
            Console.WriteLine("1) Change Name");
            Console.WriteLine("2) Change Sex");
            Console.WriteLine("3) Change Living");
            Console.WriteLine("4) Add an exsisting Genotype from database");
            Console.WriteLine("5) Delete a Genotype from Person ");
            Console.WriteLine("6) Add an exsisting Trait from database");
            Console.WriteLine("7) Delete a Trait from person");
            Console.WriteLine("7) Add Mother from exsisting list of Persons");
            Console.WriteLine("8) Add Father from exsisting list of Persons");
           
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            int EditPersonMenuChoice = MenuUserInputInt(9);
            switch (EditPersonMenuChoice)
            {
                case 0:
                    PersonScreenMenu(traitRepository, personRepository, selectedPerson, genotypeRepository, rng, context);
                    break;
                case 1:
                    ChangeName(traitRepository,selectedPerson, personRepository, genotypeRepository, rng, context); 
                    break;
                case 2:
                    ChangeSex(traitRepository,selectedPerson, personRepository, genotypeRepository, rng, context);
                    break;
                case 3:
                    changeLiving(traitRepository,selectedPerson, personRepository, genotypeRepository, rng, context);
                    break;
                case 4:
                    StringBuilder sb = new StringBuilder();
                    ListAllPersonsScreen(sb, personRepository,genotypeRepository);
                    string s = sb.ToString();
                    Console.Write(s);                    
                    AddExsistingGenotype(traitRepository, selectedPerson, genotypeRepository, personRepository, rng, context);
                    break;
                case 5:
                    DeleteGenotype(selectedPerson, traitRepository, personRepository, genotypeRepository, rng, context);
                    break;
                case 6:
                    AddExsistingTrait(selectedPerson, traitRepository,  personRepository, genotypeRepository, rng, context);
                    break;
                case 7:
                    DeleteTrait(selectedPerson, traitRepository, personRepository, genotypeRepository, rng, context);
                    break;
                case 8:
                    AddMother(traitRepository, selectedPerson, personRepository, genotypeRepository, rng, context);
                    break;
                case 9:
                    AddFather(traitRepository,selectedPerson, personRepository, genotypeRepository, rng, context);
                    break;                
            }
        }

        private static void DeleteTrait(Person selectedPerson, TraitRepository traitRepository, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
        {
            Trait traitToBeDeleted = GetSelectedPersonsTrait(selectedPerson, traitRepository, personRepository, genotypeRepository, rng, context);
            selectedPerson.Phenotype.Traits.Remove(traitToBeDeleted);
            context.SaveChanges();
        }

        private static Trait GetSelectedPersonsTrait(Person selectedPerson, TraitRepository traitRepository, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
        {
            var listOfSelectedPersonTraits = selectedPerson.Phenotype.Traits;
            if (selectedPerson.Phenotype.Traits.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("This person does not have any Traits");
                PersonScreen(traitRepository, selectedPerson, personRepository, genotypeRepository, rng, context);
                PersonScreenMenu(traitRepository, personRepository, selectedPerson, genotypeRepository, rng, context);
            }
            int number = 1;
            foreach (var trait in selectedPerson.Phenotype.Traits)
            {
                Console.WriteLine(number + ") " + trait.ToString());
                number = number + 1;
            }
            try
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Please input the index of trait you want to select: ");

                int index = Convert.ToInt32(Console.ReadLine());

                Trait selectedPersonTrait = listOfSelectedPersonTraits[index - 1];
                return selectedPersonTrait;
            }
            catch (Exception)
            {
                Console.WriteLine("There is no Trait located at that index. Please try again");
                return GetSelectedPersonsTrait(selectedPerson, traitRepository, personRepository, genotypeRepository, rng, context);

            }
        }

        private static void AddExsistingTrait(Person selectedPerson, TraitRepository traitRepository, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
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
            Trait chosenTrait = allTraits[traitIndex];
            selectedPerson.AddTraitToPerson(chosenTrait);
            Console.WriteLine("Select which genotype to add to person:");
            chosenTrait.GenerateGenotypesForATrait(chosenTrait.AlleleName, genotypeRepository);
            AddExsistingGenotype(traitRepository,selectedPerson,genotypeRepository,personRepository,rng, context);
            context.SaveChanges();
            PersonScreen(traitRepository, selectedPerson, personRepository, genotypeRepository, rng, context);
        }

        private static void AddMother(TraitRepository traitRepository, Person selectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
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
                context.SaveChanges();
                PersonScreen(traitRepository,selectedPerson, personRepository, genotypeRepository, rng, context);

            }
            else
            {
                Console.WriteLine("Only biological females can be entered as biological mothers");
            }            
            PersonScreen(traitRepository ,selectedPerson,personRepository, genotypeRepository, rng, context); //returns user to last menu
        }

        private static void AddFather(TraitRepository traitRepository, Person selectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
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
                context.SaveChanges();
                PersonScreen(traitRepository, selectedPerson, personRepository, genotypeRepository, rng, context);
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

        private static void DeleteGenotype(Person SelectedPerson, TraitRepository traitRepository, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
        {
            Genotype genotypeToBeDeleted = GetSelectedPersonsGenotype(SelectedPerson, traitRepository, personRepository, genotypeRepository, rng, context);
            SelectedPerson.Phenotype.TraitGenotypes.Remove(genotypeToBeDeleted);
            context.SaveChanges();

           // context.Genotypes.Remove(); HOW TO REMOVE FROM THE DATABASE
        }

        private static void AddExsistingGenotype(TraitRepository traitRepository, Person SelectedPerson, GenotypeRepository genotypeRepository, PersonRepository personRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
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
            context.SaveChanges();
            PersonScreen(traitRepository, SelectedPerson, personRepository, genotypeRepository, rng, context); //returns user to last menu

        }

        private static void changeLiving(TraitRepository traitRepository, Person SelectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
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
            context.SaveChanges();
            PersonScreen(traitRepository,SelectedPerson, personRepository, genotypeRepository, rng, context); //returns user to last menu
        }

        private static void ChangeSex(TraitRepository traitRepository, Person SelectedPerson, PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
        {
            Console.WriteLine("Please input a new sex (Male, Female or Unknown)");
            string inputtedSex = Console.ReadLine();
            Sex newSex = (Sex)Enum.Parse(typeof(Sex), inputtedSex, true);

            SelectedPerson.Sex = newSex;
            context.SaveChanges();
            PersonScreen(traitRepository, SelectedPerson,personRepository, genotypeRepository, rng, context); //returns user to last menu
        }

        private static void ChangeName(TraitRepository traitRepository,Person SelectedPerson,PersonRepository personRepository, GenotypeRepository genotypeRepository, RealRandomNumberGenerator rng, GeneticCounsellorDbContext context)
        {
            
            Console.WriteLine("Please input a new full name");
            string newName = Console.ReadLine();
            if (newName == "")
            {
                Console.WriteLine("It is against the Geneva convention for a person to be denied a name");
            }
          
            SelectedPerson.Name = newName;
            context.SaveChanges();
            PersonScreen(traitRepository,SelectedPerson, personRepository, genotypeRepository, rng, context);//returns user to last menu           
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
