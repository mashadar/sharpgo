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

            System.IO.TextReader sr = System.IO.File.OpenText("game.sgf");
            string zeile;
            while(sr.Peek() != -1)
            {
                zeile = sr.ReadLine();
                BoardPosition p = parser.ParseString(zeile);
                if (p != null)
                {
                    Console.WriteLine("Setting stone at " + p.x + "x" + p.y);
                    board.SetStone(p);
                }
            }

            board.PrintToConsole();
            while ((pos = parser.ParseString(Console.ReadLine())) != null)
            {
                board.SetStone(pos);
                System.Console.Clear();
                board.PrintToConsole();
            }
        }
    }
}
