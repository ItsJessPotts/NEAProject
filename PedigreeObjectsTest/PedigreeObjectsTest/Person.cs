using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }// clarity's sake
        public Sex Sex { get; set; } //male or female = circle or square
        public bool Living { get; set; } //dead or alive
        //public List<Genotype> Genotypes { get; set; }
        public virtual Phenotype Phenotype { get; set; }//is colourblind, heamophiliac, Has cystic fibrosis etc  
        public int PhenotypeID { get; set; }
        //public object Gametes { get; set; }// 'A, a, B, b'or ' A,b
        public virtual Person Mother { get; set; }
        public virtual int? MotherID { get; set; }
        public virtual Person Father { get; set; }
        public virtual int? FatherID { get; set; }


        //no constructor

        public override string ToString()
        {
            string summary = Name + " " + Sex.ToString();
            return summary;
        }

        public void AddGenotypeToPerson(Genotype genotype)
        {            
            this.Phenotype.TraitGenotypes.Add(genotype);            
        }
        public void AddPhenotypeToPerson(Phenotype phenotype)
        {
            this.Phenotype = phenotype;
        }
        public void AddTraitToPerson(Trait trait)
        {
            this.Phenotype.Traits.Add(trait);
        }
        public void AddMotherToPerson(Person mother)
        {
            if (mother.Sex == Sex.Female)
            {
                this.Mother = mother;
            }            
        }
        public bool CanAddMother(Person mother)
        {
            return mother.Sex == Sex.Female;            
        }
        public void AddFatherToPerson(Person father)
        {
            if (father.Sex == Sex.Male)
            {
                this.Father = father;
            }           
        }
        public bool CanAddFather(Person father)
        {
            return father.Sex == Sex.Male;
        }






    }

}