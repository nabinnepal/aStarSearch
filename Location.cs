using System;
using System.Collections.Generic;
using System.Text;

namespace AStarSearchImplementation
{
    public class Location
    {
        #region Private Fields
        private int x, y; 
        #endregion

        #region Constructor
        public Location(int xx, int yy)
        {
            x = xx;
            y = yy;
        } 
        #endregion

        #region Properties
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return ((Location)obj).X == x && ((Location)obj).Y == y;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 1000 * x + y;
        } 
        #endregion
    }
}
