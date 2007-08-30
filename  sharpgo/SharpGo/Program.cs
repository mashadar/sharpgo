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
            Board board = new Board(19);

            board.SetStone(3, 6, BoardPositionEntry.WHITE);
            board.SetStone(1, 6, BoardPositionEntry.WHITE);
            board.SetStone(2, 5, BoardPositionEntry.WHITE);
            board.SetStone(2, 6, BoardPositionEntry.BLACK);
            board.SetStone(2, 7, BoardPositionEntry.WHITE);

            board.PrintToConsole();

            System.Console.WriteLine("White's captured stones: " + board.CapturedBlackStones);
            System.Console.WriteLine("Black's captured stones: " + board.CapturedWhiteStones);

            System.Console.ReadKey();
        }
    }
}
