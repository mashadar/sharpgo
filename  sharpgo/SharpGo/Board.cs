﻿using System;
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
        ///     The board's size
        /// </summary>
        private int size = 19;

        /// <summary>
        ///     2-Dimensional Array containing the placed stones
        /// </summary>
        private BoardPosition[,] board;

        /// <summary>
        ///     List of groups
        /// </summary>
        private System.Collections.Generic.List<Group> groups =
            new System.Collections.Generic.List<Group>();

        /// <summary>
        ///     
        /// </summary>
        private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        ///     Number of captured black stones
        /// </summary>
        private int black_stones_captured = 0;

        /// <summary>
        ///     Number of captured white stones
        /// </summary>
        private int white_stones_captured = 0;

        /// <summary>
        ///     This one gets incremented every time a move is made
        /// </summary>
        private int move_number = 0;

        /// <summary>
        ///     This is the maximum boardsize up to which computation time
        ///     is reasonable
        /// </summary>
        public const int MaxBoardSize = 19;

        /// <summary>
        ///     A group of n stones can have at most 2(n + 1) liberties.
        ///     From this follows that an upper bound on the number
        ///     of liberties of a group on a board of size N^2 is
        ///     2/3 (N^2 + 1)
        /// </summary>
        public const int MaxLiberties = (2 * (MaxBoardSize * MaxBoardSize + 1) / 3);
        #endregion

        #region Accessors
        /// <summary>
        ///     Returns the board's size
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
        ///     Number of captured black stones
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
        ///     Number of captured white stones
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
        ///     Current movenumber
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
        ///     Get/Set groups
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
        ///     Returns the stone south of the given position
        /// </summary>
        /// <param name="pos">
        ///     The stone to go from
        /// </param>
        /// <returns>
        ///     The stone south of the given position
        /// </returns>
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
        ///     Returns the stone west of the given position
        /// </summary>
        /// <param name="pos">
        ///     The stone to go from
        /// </param>
        /// <returns>
        ///     The stone west of the given position
        /// </returns>
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
        ///     Returns the stone north of the given position
        /// </summary>
        /// <param name="pos">
        ///     The stone to go from
        /// </param>
        /// <returns>
        ///     The stone north of the given position
        /// </returns>
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
        ///     Returns the stone east of the given position
        /// </summary>
        /// <param name="pos">
        ///     The stone to go from
        /// </param>
        /// <returns>
        ///     The stone east of the given position
        /// </returns>
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
        ///     Returns the stone south-west of the given position
        /// </summary>
        /// <param name="pos">
        ///     The stone to go from
        /// </param>
        /// <returns>
        ///     The stone south-west of the given position
        /// </returns>
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
        ///     Returns the stone south-east of the given position
        /// </summary>
        /// <param name="pos">
        ///     The stone to go from
        /// </param>
        /// <returns>
        ///     The stone south-east of the given position
        /// </returns>
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
        ///     Returns the stone north-west of the given position
        /// </summary>
        /// <param name="pos">
        ///     The stone to go from
        /// </param>
        /// <returns>
        ///     The stone north-west of the given position
        /// </returns>
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
        ///     Returns the stone north-east of the given position
        /// </summary>
        /// <param name="pos">
        ///     The stone to go from
        /// </param>
        /// <returns>
        ///     The stone north-east of the given position
        /// </returns>
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
        ///     Checks if the two given stones are
        ///     direct neighbors
        /// </summary>
        /// <param name="pos1">
        ///     The first stone to check
        /// </param>
        /// <param name="pos2">
        ///     The second stone to check
        /// </param>
        /// <returns>
        ///     True if they are neighbours
        /// </returns>
        public bool DirectNeighbors(BoardPosition pos1, BoardPosition pos2)
        { 
            return (pos1 == South(pos2)) ||
                (pos1 == West(pos2)) ||
                (pos1 == North(pos2)) ||
                (pos1 == East(pos2));
        }

        /// <summary>
        ///     Checks if the two given stones are
        ///     diagonal neighbors
        /// </summary>
        /// <param name="pos1">
        ///     The first stone to check
        /// </param>
        /// <param name="pos2">
        ///     The second stone to check
        /// </param>
        /// <returns>
        ///     True if they are diagonal neighbours
        /// </returns>
        public bool DiagonalNeighbors(BoardPosition pos1, BoardPosition pos2)
        {
            return (pos1 == SouthWest(pos2)) ||
                (pos1 == SouthEast(pos2)) ||
                (pos1 == NorthWest(pos2)) ||
                (pos1 == NorthEast(pos2));
        }

        /// <summary>
        ///     Constructor
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
        ///     Constructor
        /// </summary>
        /// <param name="s">
        ///     The board's size
        /// </param>
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
        ///     Returns the stone at position (x, y)
        /// </summary>
        /// <param name="x">
        ///     x-Coordinate
        /// </param>
        /// <param name="y">
        ///     y-Coordinate
        /// </param>
        /// <returns>
        ///     Returns the stone at the given position.
        ///     An exception is thrown if the coordinates
        ///     are out of bound.
        /// </returns>
        public BoardPosition GetStone(int x, int y)
        {
            if(x < 0 || x >= size)
                throw new System.Exception("Position out of range");
            if (y < 0 || y >= size)
                throw new System.Exception("Position out of range");
            return board[x, y];
        }

        /// <summary>
        ///     Places a new stone on the board at the
        ///     given position. This method then checks
        ///     if the new stone connects some groups
        ///     and if it kills any surrounding groups.
        /// </summary>
        /// <param name="x">
        ///     The new stone's x-Coordinate
        /// </param>
        /// <param name="y">
        ///     The new stone's y-Coordinate
        /// </param>
        /// <param name="entry">
        ///     The new stone's color
        /// </param>
        public void SetStone(int x, int y, BoardPositionEntry entry)
        {
            if(x < 0 || x >= size)
                throw new System.Exception("Position out of range");
            if (y < 0 || y >= size)
                throw new System.Exception("Position out of range");

            if (board[x, y].Contains == BoardPositionEntry.EMTPY)
                board[x, y].Contains = entry;

            ConnectToGroups(board[x, y]);
            if (!CheckForFreeLiberties(board[x, y]))
            {
                System.Console.WriteLine("This move is not allowed!");
            }

            CheckSurroundingGroupsForDeath(board[x, y]);
            MoveNumber++;
        }

        /// <summary>
        ///     Checks if the given point
        ///     and the surrounding one's 
        ///     connect to a new bigger group.
        /// </summary>
        /// <param name="pos">
        ///     The position to check from
        /// </param>
        /// <param name="p1">
        /// 
        /// </param>
        /// <param name="p2">
        /// 
        /// </param>
        /// <param name="p3">
        /// 
        /// </param>
        /// <param name="p4">
        /// 
        /// </param>
        /// <param name="flag">
        ///     This flag is set to true if the stone connects
        ///     to a new group. Otherwise, the stone itself
        ///     creates a new group.
        /// </param>
        protected void ConnectToGroupsHelper(ref BoardPosition pos,
            ref BoardPosition p1, ref BoardPosition p2, ref BoardPosition p3,
            ref BoardPosition p4, ref bool flag)
        {
            if (p1 != null)
            {
                if (p1.Contains == pos.Contains)
                {
                    p1.Group.AddToGroup(pos);
                    if (p2 != null)
                    {
                        if (p2.Contains == pos.Contains
                            && p2.Group != pos.Group)
                        {
                            p1.Group.JoinWithGroup(this, p2.Group);
                        }
                    }
                    if (p3 != null)
                    {
                        if (p3.Contains == pos.Contains
                            && p3.Group != pos.Group)
                        {
                            p1.Group.JoinWithGroup(this, p3.Group);
                        }
                    }
                    if (p4 != null)
                    {
                        if (p4.Contains == pos.Contains
                            && p4.Group != pos.Group)
                        {
                            p1.Group.JoinWithGroup(this, p4.Group);
                        }
                    }
                    flag = true;
                }
            } 
        }

        /// <summary>
        ///     Checks if the new placed stone
        ///     connects 2 groups or itself is connected
        ///     to a group.
        ///     Otherwise, the stone creates a new group.
        ///     In the opening, many groups will be created,
        ///     but later they'll all converge into 
        ///     few big groups.
        /// </summary>
        public void ConnectToGroups(BoardPosition pos)
        {
            bool flag = false;

            BoardPosition west = West(pos);
            BoardPosition east = East(pos);
            BoardPosition north = North(pos);
            BoardPosition south = South(pos);

            ConnectToGroupsHelper(ref pos, ref west, ref east, 
                ref north, ref south, ref flag);
            ConnectToGroupsHelper(ref pos, ref east, ref west, 
                ref north, ref south, ref flag);
            ConnectToGroupsHelper(ref pos, ref north, ref west, 
                ref east, ref south, ref flag);
            ConnectToGroupsHelper(ref pos, ref south, ref west, 
                ref east, ref north, ref flag);

            if (!flag)
            {
                pos.Group = new Group();
                pos.Group.AddToGroup(pos);
                Groups.Add(pos.Group);
            }
        }

        /// <summary>
        ///     Checks if the group is still alive
        /// </summary>
        /// <param name="pos">
        ///     A stone belonging to that group
        /// </param>
        /// <returns>
        ///     Returns true if the group is still alive. Otherwise, false
        /// </returns>
        public bool CheckForFreeLiberties(BoardPosition pos)
        {
            if (pos != null && pos.Group != null)
            {
                if (pos.Group.GetFreeLiberties(this) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///     A little helper method to check if a group is dead.
        ///     The free liberties are checked and if they are = 0,
        ///     then the group is removed and the captured stones
        ///     counter increases
        /// </summary>
        /// <param name="pos">
        ///     A stone of the group to check for death
        /// </param>
        protected void CheckSurroundingGroupsForDeathHelper(ref BoardPosition pos)
        {
            if (pos == null)
                return;

            BoardPositionEntry entry = pos.Contains;
            int size = 0;
            if (pos != null)
            {
                if (!CheckForFreeLiberties(pos))
                    size = pos.Group.RemoveGroup(this);
            }
            if (entry == BoardPositionEntry.BLACK)
                CapturedBlackStones += size;
            else if (entry == BoardPositionEntry.WHITE)
                CapturedWhiteStones += size;
        }

        /// <summary>
        ///     Checks if the new placed stone kills any 
        ///     surrounding groups
        /// </summary>
        /// <param name="pos">
        ///     The new placed stones
        /// </param>
        public void CheckSurroundingGroupsForDeath(BoardPosition pos)
        {
            BoardPosition west = West(pos);
            BoardPosition east = East(pos);
            BoardPosition north = North(pos);
            BoardPosition south = South(pos);

            CheckSurroundingGroupsForDeathHelper(ref west);
            CheckSurroundingGroupsForDeathHelper(ref east);
            CheckSurroundingGroupsForDeathHelper(ref north);
            CheckSurroundingGroupsForDeathHelper(ref south);
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
        ///     Removes the stone at the given position
        /// </summary>
        /// <param name="pos">
        ///     The stone to remove
        /// </param>
        public void RemoveStone(BoardPosition pos)
        {
            board[pos.x, pos.y].Contains = BoardPositionEntry.EMTPY;
            board[pos.x, pos.y].Group = null;
        }

        /// <summary>
        ///     Flips the board horizontally
        /// </summary>
        public void FlipBoardHorizontally()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    BoardPositionEntry tmp = board[x, y].Contains;
                    board[x, y].Contains = board[x, Size - y - 1].Contains;
                    board[x, Size - y - 1].Contains = tmp;
                }
            }
        }

        /// <summary>
        ///     Prints the board and the placed stones to the console
        /// </summary>
        public void PrintToConsole()
        {
            for (int y = -1; y <= Size; y++)
            {
                for (int x = -1; x <= Size; x++)
                {
                    if ((x == -1 && y >= 0 && y < Size) ||
                        (x == Size && y >= 0 && y < Size))
                    {
                        System.Console.Write(string.Format(" {0:00}", y + 1));
                        continue;
                    }
                    if ((y == -1 && x == -1) ||
                        (y == Size && x == -1))
                    {
                        System.Console.Write("   ");
                        continue;
                    }
                    if ((y == -1 && x >= 0 && x < Size) ||
                        (y == Size && x >= 0 && x < Size))
                    {
                        System.Console.Write(" " + letters[x]);
                        continue;
                    }
                    if (x < 0 || y < 0)
                        continue;
                    BoardPosition pos;
                    try
                    {
                        pos = GetStone(x, y);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    if(pos == null)
                        System.Console.Write(" .");
                    else if (pos.Contains == BoardPositionEntry.BLACK)
                        System.Console.Write(" X");
                    else if (pos.Contains == BoardPositionEntry.WHITE)
                        System.Console.Write(" O");
                    else
                        System.Console.Write(" .");
                }
                System.Console.WriteLine("");
            }
            System.Console.WriteLine("Move Nr. " + MoveNumber);
            System.Console.WriteLine("White (O) has captured " + CapturedBlackStones + " stones");
            System.Console.WriteLine("Black (X) has captured " + CapturedWhiteStones + " stones");
        }
        #endregion
    }
}
