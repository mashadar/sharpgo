using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGo
{
    /// <summary>
    /// 
    /// </summary>
    class Board
    {
        #region Members
        /// <summary>
        /// The board's size
        /// </summary>
        private int size = 19;

        /// <summary>
        /// 
        /// </summary>
        private BoardPosition[,] board;

        /// <summary>
        /// 
        /// </summary>
        private System.Collections.Generic.List<Group> groups =
            new System.Collections.Generic.List<Group>();

        /// <summary>
        /// 
        /// </summary>
        private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Number of captured black stones
        /// </summary>
        private int black_stones_captured = 0;

        /// <summary>
        /// Number of captured white stones
        /// </summary>
        private int white_stones_captured = 0;

        /// <summary>
        /// This one gets incremented every time a move is made
        /// </summary>
        private int move_number = 0;

        /// <summary>
        /// This is the maximum boardsize up to which computation time
        /// is reasonable
        /// </summary>
        public const int MaxBoardSize = 19;

        /// <summary>
        /// A group of n stones can have at most 2(n + 1) liberties.
        /// From this follows that an upper bound on the number
        /// of liberties of a group on a board of size N^2 is
        /// 2/3 (N^2 + 1)
        /// </summary>
        public const int MaxLiberties = (2 * (MaxBoardSize * MaxBoardSize + 1) / 3);
        #endregion

        #region Accessors
        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CapturedBlackStones
        {
            get
            {
                return black_stones_captured;
            }
            set
            {
                black_stones_captured = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CapturedWhiteStones
        {
            get
            {
                return white_stones_captured;
            }
            set
            {
                white_stones_captured = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MoveNumber
        {
            get
            {
                return move_number;
            }
            set
            {
                move_number = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Collections.Generic.List<Group> Groups
        {
            get
            {
                return groups;
            }
            set
            {
                groups = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the stone south of the given position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public BoardPosition South(BoardPosition pos)
        {
            try
            {
                return GetStone(pos.x, pos.y + 1);
            }
            catch(System.Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the stone west of the given position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public BoardPosition West(BoardPosition pos)
        {
            try
            {
                return GetStone(pos.x - 1, pos.y);
            }
            catch(System.Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the stone north of the given position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public BoardPosition North(BoardPosition pos)
        {
            try
            {
                return GetStone(pos.x, pos.y - 1);
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the stone east of the given position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public BoardPosition East(BoardPosition pos)
        {
            try
            {
                return GetStone(pos.x + 1, pos.y);
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public BoardPosition SouthWest(BoardPosition pos)
        {
            try
            {
                return GetStone(pos.x - 1, pos.y + 1);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public BoardPosition SouthEast(BoardPosition pos)
        {
            try
            {
                return GetStone(pos.x + 1, pos.y + 1);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public BoardPosition NorthWest(BoardPosition pos)
        {
            try
            {
                return GetStone(pos.x - 1, pos.y - 1);
               }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public BoardPosition NorthEast(BoardPosition pos)
        {
            try
            {
                return GetStone(pos.x + 1, pos.y - 1);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public bool DirectNeighbors(BoardPosition pos1, BoardPosition pos2)
        { 
            return (pos1 == South(pos2)) ||
                (pos1 == West(pos2)) ||
                (pos1 == North(pos2)) ||
                (pos1 == East(pos2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public bool DiagonalNeighbors(BoardPosition pos1, BoardPosition pos2)
        {
            return (pos1 == SouthWest(pos2)) ||
                (pos1 == SouthEast(pos2)) ||
                (pos1 == NorthWest(pos2)) ||
                (pos1 == NorthEast(pos2));
        }

        /// <summary>
        /// 
        /// </summary>
        public Board()
        {
            board = new BoardPosition[size, size];
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    board[x, y] = new BoardPosition(x, y, BoardPositionEntry.EMTPY);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public Board(int s)
        {
            Size = s;
            board = new BoardPosition[size, size];
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    board[x, y] = new BoardPosition(x, y, BoardPositionEntry.EMTPY);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public BoardPosition GetStone(int x, int y)
        {
            if(x < 0 || x >= size)
                throw new System.Exception("Position out of range");
            if (y < 0 || y >= size)
                throw new System.Exception("Position out of range");
            return board[x, y];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="entry"></param>
        public void SetStone(int x, int y, BoardPositionEntry entry)
        {
            if(x < 0 || x >= size)
                throw new System.Exception("Position out of range");
            if (y < 0 || y >= size)
                throw new System.Exception("Position out of range");
            if (board[x, y].Contains != entry)
                board[x, y].Contains = entry;

            ConnectToGroups(board[x, y]);
            if (!CheckForFreeLiberties(board[x, y]))
            {
                System.Console.WriteLine("This move is not allowed!");
            }

            CheckSurroundingGroupsForDeath(board[x, y]);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ConnectToGroups(BoardPosition pos)
        {
            bool flag = false;

            BoardPosition west = West(pos);
            BoardPosition east = East(pos);
            BoardPosition north = North(pos);
            BoardPosition south = South(pos);

            if (west != null)
            {
                if (west.Contains == pos.Contains)
                {
                    west.Group.AddToGroup(pos);
                    if (east.Contains == pos.Contains
                        && east.Group != pos.Group)
                    {
                        west.Group.JoinWithGroup(this, east.Group);
                    }
                    if (north.Contains == pos.Contains
                        && north.Group != pos.Group)
                    {
                        west.Group.JoinWithGroup(this, north.Group);
                    }
                    if (south.Contains == pos.Contains
                        && south.Group != pos.Group)
                    {
                        west.Group.JoinWithGroup(this, south.Group);
                    }
                    flag = true;
                }
            } 

            if (east != null)
            {
                if (east.Contains == pos.Contains)
                {
                    east.Group.AddToGroup(pos);
                    if (west.Contains == pos.Contains
                        && west.Group != pos.Group)
                    {
                        east.Group.JoinWithGroup(this, west.Group);
                    }
                    if (north.Contains == pos.Contains
                        && north.Group != pos.Group)
                    {
                        east.Group.JoinWithGroup(this, north.Group);
                    }
                    if (south.Contains == pos.Contains
                        && south.Group != pos.Group)
                    {
                        east.Group.JoinWithGroup(this, south.Group);
                    }
                    flag = true;
                }
            }

            if (north != null)
            {
                if (north.Contains == pos.Contains)
                {
                    north.Group.AddToGroup(pos);
                    if (west.Contains == pos.Contains
                        && west.Group != pos.Group)
                    {
                        north.Group.JoinWithGroup(this, west.Group);
                    }
                    if (east.Contains == pos.Contains
                        && east.Group != pos.Group)
                    {
                        north.Group.JoinWithGroup(this, east.Group);
                    }
                    if (south.Contains == pos.Contains
                        && south.Group != pos.Group)
                    {
                        north.Group.JoinWithGroup(this, south.Group);
                    }
                    flag = true;
                }
            }

            if (south != null)
            {
                if (south.Contains == pos.Contains)
                {
                    south.Group.AddToGroup(pos);
                    if (west.Contains == pos.Contains
                        && west.Group != pos.Group)
                    {
                        south.Group.JoinWithGroup(this, west.Group);
                    }
                    if (east.Contains == pos.Contains
                        && east.Group != pos.Group)
                    {
                        south.Group.JoinWithGroup(this, east.Group);
                    }
                    if (north.Contains == pos.Contains
                        && north.Group != pos.Group)
                    {
                        south.Group.JoinWithGroup(this, north.Group);
                    }
                    flag = true;
                }
            }

            if (!flag)
            {
                pos.Group = new Group();
                pos.Group.AddToGroup(pos);
                Groups.Add(pos.Group);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool CheckForFreeLiberties(BoardPosition pos)
        {
            if (pos.Group.GetFreeLiberties(this) == 0)
            {
                pos.Group.RemoveGroup(this);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public void CheckSurroundingGroupsForDeath(BoardPosition pos)
        {
            BoardPosition west = West(pos);
            BoardPosition east = East(pos);
            BoardPosition north = North(pos);
            BoardPosition south = South(pos);

            if (west != null)
            {
                if (!CheckForFreeLiberties(west))
                    west.Group.RemoveGroup(this);
            }

            if (east != null)
            {
                if (!CheckForFreeLiberties(west))
                    east.Group.RemoveGroup(this);
            }

            if (north != null)
            {
                if (!CheckForFreeLiberties(west))
                    north.Group.RemoveGroup(this);
            }

            if (south != null)
            {
                if (!CheckForFreeLiberties(west))
                    south.Group.RemoveGroup(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public void SetStone(BoardPosition pos)
        {
            SetStone(pos.x, pos.y, pos.Contains);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public void RemoveStone(BoardPosition pos)
        {
            board[pos.x, pos.y].Contains = BoardPositionEntry.EMTPY;
            board[pos.x, pos.y].Group = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void PrintToConsole()
        {
            for (int y = -2; y <= Size; y++)
            {
                for (int x = -2; x <= Size; x++)
                {
                    if (x == -2 && y >= 0 && y <= Size)
                    {
                        System.Console.Write(string.Format("{0:00}", y));
                        continue;
                    }
                    if (y == -2 && x == -2)
                    {
                        System.Console.Write("  ");
                        continue;
                    }
                    if (y == -1 && x == -2)
                    {
                        System.Console.Write("  ");
                        continue;
                    }
                    if (y == -2 && x >= 0 && x < Size)
                    {
                        System.Console.Write(letters[x]);
                        continue;
                    }
                    if (x == -1 || x == Size)
                    {
                        System.Console.Write(" |");
                        continue;
                    }
                    if (y == -1 || y == Size)
                    {
                        System.Console.Write("-");
                        continue;
                    }
                    if (x < 0 || y < 0)
                        continue;
                    BoardPosition pos = GetStone(x, y);
                    if(pos == null)
                        System.Console.Write(".");
                    else if (pos.Contains == BoardPositionEntry.BLACK)
                        System.Console.Write("X");
                    else if (pos.Contains == BoardPositionEntry.WHITE)
                        System.Console.Write("O");
                    else
                        System.Console.Write(".");
                }
                System.Console.WriteLine("");
            }
        }
        #endregion
    }
}
