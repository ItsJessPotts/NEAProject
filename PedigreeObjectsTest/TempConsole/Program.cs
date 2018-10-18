using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedigreeObjects;


namespace TempConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new GeneticCounsellorDbContext("Jess1");
            foreach (var person in context.Persons)
            {
                Console.Write(person.ToString());//John Adams  
                Console.WriteLine(" has phenotype " + person.Phenotype);
            }
            foreach (var trait in context.Traits)
            {
                Console.WriteLine(trait.ToString());//Colourblindness
            }
            foreach (var phenotype in context.Phenotypes)
            {
                Console.WriteLine(phenotype.ToString());//Colourblind
            }
            foreach (var Genotype in context.Genotypes)
            {
                Console.WriteLine(Genotype.ToString()); //AA
            }
            Console.ReadKey();
        }
    }
}
