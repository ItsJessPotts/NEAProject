namespace GenotypeCalculator
{
    public class Block : Grid
    {
        public const int BlockWidth = 13;
        public const int BlockHeight = 5;
        public Block(string name, string gender, string genotype) : base(BlockWidth, BlockHeight)
        {
            Place(name, Width / 2, 1);
            Place(gender, Width / 2, 2);
            Place(genotype, Width / 2, 3);
            DrawBorder();
        }
    }
}
