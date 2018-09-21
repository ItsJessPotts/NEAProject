using System;
using System.Collections.Generic;
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
        }
        public List<Trait> ListTraits()
        {
            return Traits;
        }
    }
   
}
