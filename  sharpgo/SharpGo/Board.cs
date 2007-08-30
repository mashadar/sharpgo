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
        public readonly int MaxLiberties = (2 * (MaxBoardSize * MaxBoardSize + 1) / 3);
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
                throw e;
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
                throw e;
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
                throw e;
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
                throw e;
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
                    SetStone(x, y, BoardPositionEntry.EMTPY);
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
            board[x, y] = new BoardPosition(x, y, entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public void SetStone(BoardPosition pos)
        {
            if (pos.x < 0 || pos.x >= size)
                throw new System.Exception("Position out of range");
            if (pos.y < 0 || pos.y >= size)
                throw new System.Exception("Position out of range");
            board[pos.x, pos.y] = pos;
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
                    if (pos.Contains == BoardPositionEntry.BLACK)
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
