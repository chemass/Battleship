using System;

namespace CheCoxshall.Battleship.Core
{
    public struct Cell
    {
        public int Col { get; private set; }
        public int Row { get; private set; }

        public Cell(char col, int row)
        {
            if (!char.IsLower(col))
                col = char.ToLower(col);
            if (col < 97 || col > 106)
                throw new ArgumentOutOfRangeException("Col must be in the range A-J");
            if (row < 1 || row > 10)
                throw new ArgumentOutOfRangeException("Row must be in the range 1-10");

            Col = col - 97;
            Row = row - 1;
        }
    }
}