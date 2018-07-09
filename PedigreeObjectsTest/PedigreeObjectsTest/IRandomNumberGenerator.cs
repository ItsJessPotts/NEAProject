using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedigreeObjects
{
    public interface IRandomNumberGenerator
    {
        int Next(int min, int max);


    }
    public class RealRandomNumberGenerator : IRandomNumberGenerator
    {
        private Random RNG { get; set; }
        public RealRandomNumberGenerator()
        {
            RNG = new Random();   
        }
        public int Next(int min, int max)
        {
            return RNG.Next(min, max);
        }
        
    }
    public class PredictableRandomNumberGenerator : IRandomNumberGenerator
    {
   
        private int Value { get; set; }
        public int Next(int min, int max)
        {
            Value +=1;
            if (Value >= max)
            {
                Value = min;
            }
            if (Value <= min)
            {
                Value = min;
            }
            return Value;
           
        }
    }
}
