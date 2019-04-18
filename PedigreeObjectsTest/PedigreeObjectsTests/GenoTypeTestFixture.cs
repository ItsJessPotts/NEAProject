using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedigreeObjects;


namespace PedigreeObjectsTests
{
    public class GenoTypeTestFixture
    {
        public GenotypeRepository genotypeRepository  { get; set; }
        public RealRandomNumberGenerator rng { get; set; }

        public void SetUp()
        {
            GeneticCounsellorDbContext context = null;
            using (context = new GeneticCounsellorDbContext("Jess1"))
            {

                var traitRepository = new TraitRepository(context);
                var personRepository = new PersonRepository(context);
                genotypeRepository = new GenotypeRepository(context);
                rng = new RealRandomNumberGenerator();

                string personFilename = @"C:\Users\potts\Desktop\PersonsSeedData.txt";
                string traitFilename = @"C:\Users\potts\Desktop\TraitSeedData.txt";

            }
        }

    }
}
