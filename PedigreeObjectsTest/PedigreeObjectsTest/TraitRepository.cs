using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class TraitRepository
    {
        private List<Trait> Traits = new List<Trait>();

        public void AddTrait(Trait trait)
        {
            Traits.Add(trait);
            db.Trait.InsertOnSubmit(trait);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Make some adjustments.
                // ...
                // Try again.
                db.SubmitChanges();
            }
        }
        public List<Trait> ListTraits()
        {
            return Traits;
        }
        public void LoadFile(string traitFilename)
        {
            try
            {

                using (StreamReader reader = new StreamReader(traitFilename))
                {


                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        var item = line.Split(',');
                        string traitName = item[0];
                        char alleleName = Convert.ToChar(item[1]);
                        Dominance inheritanceType = (Dominance)Enum.Parse(typeof(Dominance), item[2], true);
                        var trait = new Trait(traitName, alleleName, inheritanceType);
                        Traits.Add(trait);

                    }
                }


            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unable to locate 'TraitSeedData.txt'");
                Console.ReadKey();
                throw;
            }
        }
        
    }   
}
