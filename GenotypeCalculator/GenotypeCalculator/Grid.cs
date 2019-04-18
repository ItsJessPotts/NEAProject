using System;

namespace GenotypeCalculator
{
    public class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        // Define a Grid of width x height chars
        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            _chars = new char[width, height];
        }

        private char[,] _chars;
        public char[,] Chars
        {
            get { return _chars; }
        }

        // Place char on grid if if x and y are ok as follows:
        // e.g. x=8, y=8 on a 18 x 12 grid
        // +012345678901234567+
        // 0+---x--->         |
        // 1|                 |
        // 2|                 |
        // 3|                 |
        // 4y                 |
        // 5|                 |
        // 6|                 |
        // 7|                 |
        // 8V       *         |
        // 9                  |
        // 0                  |
        // 1                  |
        // +------------------+
        public void Place(char c, int x, int y)
        {
            if (0 <= x && x < Width && 0 <= y && y < Height)
            {
                _chars[x, y] = c;
            }
        }

        // Place a string centred on x, y
        // e.g. x=8, y=8 on a 18 x 12 grid
        // +012345678901234567+
        // 0+---x--->         |
        // 1|                 |
        // 2|                 |
        // 3|                 |
        // 4y                 |
        // 5|                 |
        // 6|                 |
        // 7|                 |
        // 8V     string      |
        // 9                  |
        // 0                  |
        // 1                  |
        // +------------------+
        public void Place(string s, int xc, int yc)
        {
            var l = s.Length;
            var startx = xc - l / 2;
            var y = yc;
            for (int i = 0, x = startx; i < l; i++, x++)
            {
                Place(s[i], x, y);
            }
        }

        // Place a Grid centred on x, y
        // e.g. Place a 10 x 3 grid at x=8, y=8 on a 18 x 12 grid
        // +012345678901234567+
        // 0+---x--->         |
        // 1|                 |
        // 2|                 |
        // 3|                 |
        // 4y                 |
        // 5|                 |
        // 6|   +01234567890+ |
        // 7|   0  string   | |
        // 8V   1  string   | |
        // 9    2  string   | |
        // 0    +-----------+ |
        // 1                  |
        // +------------------+
        public void Place(Grid g, int xc, int yc)
        {
            var x1 = xc - g.Width / 2;
            var y1 = yc - g.Height / 2;
            for (int i = 0, x = x1; i < g.Width; i++, x++)
            {
                for (int j = 0, y = y1; j < g.Height; j++, y++)
                {
                    Place(g.Chars[i, j], x, y);
                }
            }
        }
        public void DrawBorder()
        {
            var y1 = 0;
            for (int x = 1; x < Width - 1; x++)
            {
                Place('-', x, y1);
            }

            y1 = Height - 1;
            for (int x = 0; x < Width - 1; x++)
            {
                Place('-', x, y1);
            }
            var x1 = 0;
            for (int y = 1; y < Height - 1; y++)
            {
                Place('|', x1, y);
            }

            x1 = Width - 1;
            for (int y = 1; y < Height - 1; y++)
            {
                Place('|', x1, y);
            }
            Place('+', 0, 0);
            Place('+', Width - 1, 0);
            Place('+', 0, Height - 1);
            Place('+', Width - 1, Height - 1);

        }

        public void Print()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    Console.Write(_chars[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
