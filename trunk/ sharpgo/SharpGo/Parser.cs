using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGo
{
    class Parser
    {
        public BoardPosition ParseString(string cmd)
        {
            BoardPosition pos = new BoardPosition();

            if (cmd == "exit")
                return null;x

            if (cmd[0].ToString() == "b")
                pos.Contains = BoardPositionEntry.BLACK;
            else if (cmd[0].ToString() == "w")
                pos.Contains = BoardPositionEntry.WHITE;

            if (pos.Contains == BoardPositionEntry.EMTPY)
                cmd = cmd.Substring(1, cmd.Length - 1);

            int x = int.Parse(cmd.Substring(1, 2));
            int y = int.Parse(cmd.Substring(3, 2));

            pos.x = x;
            pos.y = y;

            return pos;
        }
    }
}
