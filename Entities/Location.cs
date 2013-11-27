namespace AStarSearchImplementation
{
    public class Location
    {
        #region Private Fields
        private int _x, _y; 
        #endregion

        #region Constructor
        public Location(int x, int y)
        {
            _x = x;
            _y = y;
        } 
        #endregion

        #region Properties
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
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
            return ((Location)obj).X == _x && ((Location)obj).Y == _y;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 1000 * _x + _y;
        } 
        #endregion
    }
}
