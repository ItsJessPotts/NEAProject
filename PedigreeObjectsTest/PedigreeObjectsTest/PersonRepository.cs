using System;
using System.Collections.Generic;
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
    }
}
