using System.Collections.Generic;
using System.Linq;

namespace CrystalLib.TileEngine
{
    public class Point
    {
        /// <summary>
        /// X co-ordinate of the point.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y co-ordinate of the point.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        public Point()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="x">X co-ordinate of the point.</param>
        /// <param name="y">Y co-ordinate of the point.</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class PointSelection
    {
        #region Field Region

        /// <summary>
        /// Width of the underlying object in pixels.
        /// </summary>
        private int _objectWidth;

        /// <summary>
        /// Height of the underlying object in pixels.
        /// </summary>
        private int _objectHeight;

        /// <summary>
        /// Width of the individual tiles.
        /// </summary>
        private int _tileWidth;

        /// <summary>
        /// Height of the individual tiles.
        /// </summary>
        private int _tileHeight;

        #endregion

        #region Property Region

        private Point _start = new Point();

        /// <summary>
        /// Starting point of the selection area.
        /// </summary>
        public Point Start
        {
            get
            {
                return _start;
            }
            set 
            {
                _start = value;
                CalculatePoints();
            }
        }

        private Point _end = new Point();

        /// <summary>
        /// Ending point of the selection area.
        /// </summary>
        public Point End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
                CalculatePoints();
            }
        }

        private List<Point> _points = new List<Point>();

        /// <summary>
        /// List of points which appear between the <see cref="Start"/> and <see cref="End"/> points.
        /// </summary>
        public List<Point> Points
        {
            get { return _points; }
        }

        private Point _offset;

        /// <summary>
        /// Returns the offset of the selection area compared to the tileset.
        /// </summary>
        public Point Offset
        {
            get { return _offset; }
        }

        #endregion

        #region Method Region

        /// <summary>
        /// Toggles the inclusion of a specific point.
        /// </summary>
        /// <param name="point">Point to toggle.</param>
        public void TogglePoint(Point point)
        {
            if (point.X > (_objectWidth / _tileWidth) - 1) return;
            if (point.Y > (_objectHeight / _tileHeight) - 1) return;

            if (!ContainsPoint(point))
            {
                _points.Add(point);
                CalculateOffset();
                return;
            }

            foreach (var tmp in _points.ToList())
            {
                if (tmp.X == point.X && tmp.Y == point.Y)
                    _points.Remove(tmp);
            }

            CalculateOffset();
        }

        /// <summary>
        /// Calculates the furthest X and Y co-ordinates.
        /// </summary>
        private void CalculateOffset()
        {
            Offset.X = (_objectWidth / _tileWidth) - 1;
            Offset.Y = (_objectHeight / _tileHeight) - 1;

            foreach (var tmp in _points.ToList())
            {
                if (tmp.X < Offset.X)
                    Offset.X = tmp.X;
                if (tmp.Y < Offset.Y)
                    Offset.Y = tmp.Y;
            }
        }

        /// <summary>
        /// Checks if a point is in the <see cref="Points"/> list.
        /// </summary>
        /// <param name="point">Point to check for.</param>
        /// <returns>Returns true if the point is found, false if not.</returns>
        public bool ContainsPoint(Point point)
        {
            foreach (Point tmp in _points)
            {
                if (tmp.X == point.X && tmp.Y == point.Y) 
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Calculates which points appear between the <see cref="Start"/> and <see cref="End"/> points.
        /// </summary>
        public void CalculatePoints()
        {
            _points.Clear();

            int x1; int x2; int y1; int y2;

            if (Start.X < End.X)
            {
                x1 = Start.X;
                x2 = End.X;
            }
            else
            {
                x1 = End.X;
                x2 = Start.X;
            }
            if (Start.Y < End.Y)
            {
                y1 = Start.Y;
                y2 = End.Y;
            }
            else
            {
                y1 = End.Y;
                y2 = Start.Y;
            }

            if (x1 < 0) x1 = 0;
            if (y1 < 0) y1 = 0;
            if (x2 < 0) x2 = 0;
            if (y2 < 0) y2 = 0;
            if (x1 > (_objectWidth / _tileWidth) - 1) x1 = (_objectWidth / _tileWidth) - 1;
            if (y1 > (_objectHeight / _tileHeight) - 1) y1 = (_objectHeight / _tileHeight) - 1;
            if (x2 > (_objectWidth / _tileWidth) - 1) x2 = (_objectWidth / _tileWidth) - 1;
            if (y2 > (_objectHeight / _tileHeight) - 1) y2 = (_objectHeight / _tileHeight) - 1;

            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    _points.Add(new Point(x, y));
                }
            }

            _offset = new Point(x1, y1);
        }

        /// <summary>
        /// Sets the size of the underlying object.
        /// </summary>
        /// <param name="x">Width of the underlying object in pixels.</param>
        /// <param name="y">Height of the underlying object in pixels.</param>
        public void SetObjectSize(int x, int y)
        {
            _objectWidth = x;
            _objectHeight = y;

            CalculatePoints();
        }

        /// <summary>
        /// Sets the size of the tiles.
        /// </summary>
        /// <param name="x">Width of the tiles.</param>
        /// <param name="y">Height of the tiles.</param>
        public void SetTileSize(int x, int y)
        {
            _tileWidth = x;
            _tileHeight = y;

            CalculatePoints();
        }

        public Point GetTopLeftMostPoint()
        {
            int x = (_objectWidth / _tileWidth) - 1;
            int y = (_objectHeight / _tileHeight) - 1;

            foreach (var tmp in _points.ToList())
            {
                if ((tmp.X < x && tmp.Y < y))
                {
                    x = tmp.X;
                    y = tmp.Y;
                }
            }

            return new Point(x, y);
        }

        #endregion
    }
}