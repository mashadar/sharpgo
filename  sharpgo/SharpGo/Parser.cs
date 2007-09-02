using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGo
{
    class Parser
    {
        /// <summary>
        /// 
        /// </summary>
        private string letters = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public BoardPosition ParseString(string cmd)
        {
            BoardPosition pos = new BoardPosition();

            if (cmd == "exit")
                return null;

            if (cmd.Substring(0, 3) == ";B[")
                pos.Contains = BoardPositionEntry.BLACK;
            else if (cmd.Substring(0, 3) == ";W[")
                pos.Contains = BoardPositionEntry.WHITE;
            else return null;

            char sx = cmd[3];
            char sy = cmd[4];

            int x = 0;
            int y = 0;

            for (int i = 0; i < 26; i++)
            {
                if (letters[i] == sx)
                    x = i;
                if (letters[i] == sy)
                    y = i;
            }

            pos.x = x;
            pos.y = y;

            return pos;
        }
    }
}
