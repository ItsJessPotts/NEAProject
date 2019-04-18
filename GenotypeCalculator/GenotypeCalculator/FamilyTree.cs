using System;
using System.Collections.Generic;
using System.Text;

namespace GenotypeCalculator
{
    public class FamilyTree
    {
        private Grid _grid;

        public FamilyTree()
        {
            _grid = new Grid(200,100);
        }

        public void DrawBlocks()
        {
            Block adam = new Block("Adam", "M", "Cc");
            Block abby = new Block("Abigail", "F", "Cc");

            Block child1 = new Block("Beth", "F", "CC");
            Block child2 = new Block("Josh", "M", "Cc");
            Block child3 = new Block("Chloe", "F", "cc");

            _grid.DrawBorder();
            _grid.Place(adam, 40, 30);
            _grid.Place(abby, 120, 30);
            _grid.Place(child1, 40, 60);
            _grid.Place(child2, 80, 60);
            _grid.Place(child3, 120, 60);

            _grid.Print();
        }
    }
}
