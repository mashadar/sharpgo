using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGo
{
    /// <summary>
    /// 
    /// </summary>
    class Group
    {
        /// <summary>
        /// 
        /// </summary>
        private System.Collections.Generic.LinkedList<BoardPosition> stones = 
            new System.Collections.Generic.LinkedList<BoardPosition>();

        /// <summary>
        /// 
        /// </summary>
        public System.Collections.Generic.LinkedList<BoardPosition> Stones
        {
            get
            {
                return stones;
            }
            set
            {
                stones = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool ConnectsWithGroup(Board board, BoardPosition pos)
        {
            foreach(BoardPosition stone in Stones)
            {
                if (board.DirectNeighbors(pos, stone) && pos.Contains == stone.Contains)
                {
                    if(pos.Group != this)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool BelongsToGroup(BoardPosition pos)
        {
            return pos.Group == this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public void AddToGroup(BoardPosition pos)
        {
            Stones.AddLast(pos);
            pos.Group = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public void JoinWithGroup(Board board, Group g)
        {
            foreach (BoardPosition pos in g.Stones)
            {
                Stones.AddLast(pos);
                pos.Group = this;
            }
            board.Groups.Remove(g);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public int GetFreeLiberties(Board board)
        {
            bool[,] marked = new bool[board.Size, board.Size];

            int liberties = 0;

            foreach (BoardPosition pos in Stones)
            {
                if (board.North(pos) != null)
                {
                    if (board.North(pos).Contains == BoardPositionEntry.EMTPY)
                    {
                        if (!marked[board.North(pos).x, board.North(pos).y])
                        {
                            liberties++;
                            marked[board.North(pos).x, board.North(pos).y] = true;
                        }
                    }
                }
                if (board.East(pos) != null)
                {
                    if (board.East(pos).Contains == BoardPositionEntry.EMTPY)
                    {
                        if (!marked[board.East(pos).x, board.East(pos).y])
                        {
                            liberties++;
                            marked[board.East(pos).x, board.East(pos).y] = true;
                        }
                    }
                }
                if (board.South(pos) != null)
                {
                    if (board.South(pos).Contains == BoardPositionEntry.EMTPY)
                    {
                        if (!marked[board.South(pos).x, board.South(pos).y])
                        {
                            liberties++;
                            marked[board.South(pos).x, board.South(pos).y] = true;
                        }
                    }
                }
                if (board.West(pos) != null)
                {
                    if (board.West(pos).Contains == BoardPositionEntry.EMTPY)
                    {
                        if (!marked[board.West(pos).x, board.West(pos).y])
                        {
                            liberties++;
                            marked[board.West(pos).x, board.West(pos).y] = true;
                        }
                    }
                }
            }
            return liberties;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int RemoveGroup(Board board)
        {
            int size = Stones.Count;
            BoardPositionEntry entry = BoardPositionEntry.EMTPY;

            foreach (BoardPosition pos in Stones)
            {
                entry = pos.Contains;
                board.RemoveStone(pos);
            }
            Stones.Clear();
            board.Groups.Remove(this);

            return size;
        }
    }
}
