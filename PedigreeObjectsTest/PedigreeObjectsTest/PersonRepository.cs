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

               
        public Person AddPerson(string name, Sex sex, bool living, string phenotypeName)
        {
            var p = new Person();
            p.Name = name;
            p.Sex = sex;
            p.Living = living;
            p.Phenotype = new Phenotype();
            p.Phenotype.PhenotypeName = phenotypeName;

            Db.Persons.Add(p);
            Db.SaveChanges();
            return p;
        }
        public List<Person> ListPersons()
        {
            return Db.Persons.ToList();
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
