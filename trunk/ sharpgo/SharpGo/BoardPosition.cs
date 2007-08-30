using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGo
{
    /// <summary>
    /// 
    /// </summary>
    public enum BoardPositionEntry
    {
        EMTPY,
        WHITE,
        BLACK,
        WEAK_KO,
        GRAY,
        GRAY_WHITE,
        GRAY_BLACK,
    }

    /// <summary>
    /// 
    /// </summary>
    class BoardPosition
    {
        /// <summary>
        /// 
        /// </summary>
        private int _x = 0;

        /// <summary>
        /// 
        /// </summary>
        private int _y = 0;
        /// <summary>
        /// 
        /// </summary>
        private BoardPositionEntry contains = BoardPositionEntry.EMTPY;

        /// <summary>
        /// 
        /// </summary>
        public int x
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BoardPositionEntry Contains
        {
            get
            {
                return contains;
            }
            set
            {
                contains = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BoardPosition()
        {
            x = 0;
            y = 0;
            Contains = BoardPositionEntry.EMTPY;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="entry"></param>
        public BoardPosition(int x, int y, BoardPositionEntry entry)
        {
            this.x = x;
            this.y = y;
            this.contains = entry;
        }
    }
}
