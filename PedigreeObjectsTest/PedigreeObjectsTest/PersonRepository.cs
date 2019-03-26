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
        private GeneticCounsellorDbContext Db { get; set; }

        public PersonRepository(GeneticCounsellorDbContext db)
        {
            Db = db;
        }
        public void DeletePerson(Person p)
        {
            Db.Persons.Remove(p);
            Db.SaveChanges();
        }
               
        public Person AddPerson(string name, Sex sex, bool living, string phenotypeName)
        {
            var p = new Person();
            p.Name = name;
            p.Sex = sex;
            p.Living = living;
            var ph = new Phenotype();
            //derived from the traits and genotypes
            Db.Phenotypes.Add(ph);
            p.Phenotype = ph;

            Db.Persons.Add(p);
            
            Db.SaveChanges();
            return p;
        }
        public List<Person> ListPersons()
        {
            return Db.Persons.ToList();
        }
        public Person FindPersonByID(int Id) //Select * FROM persons WHERE personId = index
        {
            return Db.Persons.Where(p => p.PersonID == Id).FirstOrDefault();        
        }
        public List<Person> FindPersonsByTrait(Trait trait)
        {

            //IQueryable<Person> personQuery =
            //from p in Db.Persons
            //from t in Db.Traits
            return null;
               
        }





    }
}
