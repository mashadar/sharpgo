using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGo
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            

            board.SetStone(4, 7, BoardPositionEntry.BLACK);
            board.SetStone(4, 6, BoardPositionEntry.BLACK);
            board.SetStone(14, 12, BoardPositionEntry.WHITE);

            board.PrintToConsole();
        }
    }
}
