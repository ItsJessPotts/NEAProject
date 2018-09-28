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
        public static ReadFile(string[] records)
        {
            try
            {

                using (StreamReader reader = new StreamReader("PersonsSeedData.txt"))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        records[i] = reader.ReadLine();
                        i++;
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
        public void LoadFile(string[] records) 
        {
            
            foreach (var line in records)
            {
                var item = line.Split(',');
                string name = item[0];
                Sex sex = (Sex)Enum.Parse(typeof(Sex),item[1],true);                
                bool living = Convert.ToBoolean(item[2]);
                var person = new Person(name, sex, living); //inputSex , inputLiving
                personRepository.AddPerson(person);
                                
            }
            
        }
    }
}
