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
            Parser parser = new Parser();
            BoardPosition pos;

            while ((pos = parser.ParseString(Console.ReadLine())) != null)
            {
                board.SetStone(pos);
                System.Console.Clear();
                board.PrintToConsole();
            }

            System.Console.ReadKey();
        }
    }
}
