using System;
using System.Collections.Generic;
using System.Text;

namespace CheCoxshall.Battleship.Core
{
    public static class CellExtensions
    {
        public static int ToBoardIndex(this Cell cell) => (cell.Row * 10) + cell.Col;

        public static IEnumerable<Cell> Range(this Cell cell, int length, bool horizontal)
        {
            yield return cell;
            for (int i = 1; i < length; i++)
                yield return new Cell(Convert.ToChar(cell.Col + (horizontal ? 97 + i : 97)), cell.Row + (horizontal ? 1 : 1 + i)); 
        }
    }
}
