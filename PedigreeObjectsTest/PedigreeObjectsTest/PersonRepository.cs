using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class PersonRepository
    {
        private List<Person> Persons = new List<Person>();

        public void AddPerson(Person person)
        {
            Persons.Add(person);
        }
        public List<Person> ListPersons()
        {
            return Persons;
        }
        public void LoadFile(string personFilename)
        {
            try
            {

                using (StreamReader reader = new StreamReader(personFilename))
                {
                    
                    
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                                                
                         var item = line.Split(',');
                         string name = item[0];
                         Sex sex = (Sex)Enum.Parse(typeof(Sex), item[1], true);
                         bool living = Convert.ToBoolean(item[2]);
                        var rng = new PredictableRandomNumberGenerator();
                        var genotypeLetters = item[3].Split(' ');
                        Dominance allele1 = Dominance.Unknown;
                        Dominance allele2 = Dominance.Unknown;
                        if (genotypeLetters[0].ToString().ToUpper() == genotypeLetters[0].ToString())
                        {
                            allele1 = Dominance.Dominant;
                        }
                        else
                        {
                            allele1 = Dominance.Recessive;
                        }
                        if (genotypeLetters[1].ToString().ToUpper() == genotypeLetters[1].ToString())
                        {
                            allele2 = Dominance.Dominant;
                        }
                        else
                        {
                            allele2 = Dominance.Recessive;
                        }
                        char[] alleleName = genotypeLetters[0].ToCharArray();
                        Genotype genotype = new Genotype(alleleName[0], allele1, allele2,rng);   //TO DO: FIX ME!!

                        var genotypes = new GenotypeRepository();
                        genotypes.AddGenotype(genotype);
                        var person = new Person(name, sex, living, genotypes); //inputSex , inputLiving
                        Persons.Add(person);                                                    
                    }
                }

               
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unable to locate 'PersonsSeedData.txt'");
                Console.ReadKey();
                throw;
            }
        }
        

        public void WritePersonToTexfile(Person person, string personFilename)
        {
            
            using (StreamWriter writer = File.AppendText(personFilename))
            {
                writer.WriteLine(person.ToString());
                
            }
            

        }

    }
}
