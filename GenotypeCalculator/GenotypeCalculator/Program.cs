using System;

namespace GenotypeCalculator
{
    public class Program
    {
        static void Main()
        {
            var g = new GenotypeCalculator();
            Console.WriteLine(g.ToString());

            var f = new FamilyTree();
            f.DrawBlocks();
            Console.ReadLine();
        }
    }
}