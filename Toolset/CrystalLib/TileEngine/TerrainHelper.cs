namespace CrystalLib.TileEngine
{
    public class TerrainHelper
    {
        #region Field Region

        private TerrainTile _terrain;

        private Point[] autoInner;
        private Point[] autoNW;
        private Point[] autoNE;
        private Point[] autoSW;
        private Point[] autoSE;

        #endregion

        #region Constructor Region

        public TerrainHelper(TerrainTile terrain)
        {
            _terrain = terrain;
        }

        #endregion

        #region Caching Logic

        public void CachePoints(TerrainType type)
        {
            if (type == TerrainType.RMVX_Ground)
                CachePoints_RMVX_Ground();
            if (type == TerrainType.RMVX_Cliff)
                CachePoints_RMVX_Cliff();
        }

        public Point GetPoint(TerrainType type, char letter)
        {
            if (type == TerrainType.RMVX_Ground)
                return (GetPoint_RMVX_Ground(letter));
            if (type == TerrainType.RMVX_Cliff)
                return (GetPoint_RMVX_Cliff(letter));

            return null;
        }

        private bool TileMatch(int original, int compare)
        {
            return original == compare;
        }

        #region RMVX Ground

        public void CachePoints_RMVX_Ground()
        {
            autoInner = new Point[4];
            autoNW = new Point[4];
            autoNE = new Point[4];
            autoSW = new Point[4];
            autoSE = new Point[4];

            //Inner tiles (Top right subtile region)
            // NW - a
            autoInner[0] = new Point(32, 0);
            // NE - b
            autoInner[1] = new Point(48, 0);
            // SW - c
            autoInner[2] = new Point(32, 16);
            // SE - d
            autoInner[3] = new Point(48, 16);
            // Outer Tiles - NW [bottom subtile region]
            // NW - e
            autoNW[0] = new Point(0, 32);
            // NE - f
            autoNW[1] = new Point(16, 32);
            // SW - g
            autoNW[2] = new Point(0, 48);
            // SE - h
            autoNW[3] = new Point(16, 48);
            // Outer Tiles - NE [bottom subtile region]
            // NW - i
            autoNE[0] = new Point(32, 32);
            // NE - j
            autoNE[1] = new Point(48, 32);
            // SW - k
            autoNE[2] = new Point(32, 48);
            // SE - l
            autoNE[3] = new Point(48, 48);
            // Outer Tiles - SW [bottom subtile region]
            // NW - m
            autoSW[0] = new Point(0, 64);
            // NE - n
            autoSW[1] = new Point(16, 64);
            // SW - o
            autoSW[2] = new Point(0, 80);
            // SE - p
            autoSW[3] = new Point(16, 80);
            // Outer Tiles - SE [bottom subtile region]
            // NW - q
            autoSE[0] = new Point(32, 64);
            // NE - r
            autoSE[1] = new Point(48, 64);
            // SW - s
            autoSE[2] = new Point(32, 80);
            // SE - t
            autoSE[3] = new Point(48, 80);
        }

        public Point GetPoint_RMVX_Ground(char letter)
        {
            var point = new Point();
            switch (letter)
            {
                case 'a':
                    point = autoInner[0];
                    break;
                case 'b':
                    point = autoInner[1];
                    break;
                case 'c':
                    point = autoInner[2];
                    break;
                case 'd':
                    point = autoInner[3];
                    break;
                case 'e':
                    point = autoNW[0];
                    break;
                case 'f':
                    point = autoNW[1];
                    break;
                case 'g':
                    point = autoNW[2];
                    break;
                case 'h':
                    point = autoNW[3];
                    break;
                case 'i':
                    point = autoNE[0];
                    break;
                case 'j':
                    point = autoNE[1];
                    break;
                case 'k':
                    point = autoNE[2];
                    break;
                case 'l':
                    point = autoNE[3];
                    break;
                case 'm':
                    point = autoSW[0];
                    break;
                case 'n':
                    point = autoSW[1];
                    break;
                case 'o':
                    point = autoSW[2];
                    break;
                case 'p':
                    point = autoSW[3];
                    break;
                case 'q':
                    point = autoSE[0];
                    break;
                case 'r':
                    point = autoSE[1];
                    break;
                case 's':
                    point = autoSE[2];
                    break;
                case 't':
                    point = autoSE[3];
                    break;
            }

            return point;
        }

        public Point CalculateNW_RMVX_Ground(int tile, int NW, int N, int W)
        {
            var tmpTile = new bool[3];
            var situation = TerrainState.Fill;

            if (TileMatch(tile, NW)) tmpTile[0] = true;
            if (TileMatch(tile, N)) tmpTile[1] = true;
            if (TileMatch(tile, W)) tmpTile[2] = true;

            if (!tmpTile[1] && !tmpTile[2]) situation = TerrainState.Inner;
            if (!tmpTile[1] && tmpTile[2]) situation = TerrainState.Horizontal;
            if (tmpTile[1] && !tmpTile[2]) situation = TerrainState.Vertical;
            if (!tmpTile[0] && tmpTile[1] && tmpTile[2]) situation = TerrainState.Outer;
            if (tmpTile[0] && tmpTile[1] && tmpTile[2]) situation = TerrainState.Fill;

            switch (situation)
            {
                case TerrainState.Inner:
                    return GetPoint_RMVX_Ground('e');
                case TerrainState.Outer:
                    return GetPoint_RMVX_Ground('a');
                case TerrainState.Horizontal:
                    return GetPoint_RMVX_Ground('i');
                case TerrainState.Vertical:
                    return GetPoint_RMVX_Ground('m');
                case TerrainState.Fill:
                    return GetPoint_RMVX_Ground('q');
            }

            return null;
        }

        public Point CalculateNE_RMVX_Ground(int tile, int N, int NE, int E)
        {
            var tmpTile = new bool[3];
            var situation = TerrainState.Fill;

            if (TileMatch(tile, N)) tmpTile[0] = true;
            if (TileMatch(tile, NE)) tmpTile[1] = true;
            if (TileMatch(tile, E)) tmpTile[2] = true;

            if (!tmpTile[0] && !tmpTile[2]) situation = TerrainState.Inner;
            if (!tmpTile[0] && tmpTile[2]) situation = TerrainState.Horizontal;
            if (tmpTile[0] && !tmpTile[2]) situation = TerrainState.Vertical;
            if (tmpTile[0] && !tmpTile[1] && tmpTile[2]) situation = TerrainState.Outer;
            if (tmpTile[0] && tmpTile[1] && tmpTile[2]) situation = TerrainState.Fill;

            switch (situation)
            {
                case TerrainState.Inner:
                    return GetPoint_RMVX_Ground('j');
                case TerrainState.Outer:
                    return GetPoint_RMVX_Ground('b');
                case TerrainState.Horizontal:
                    return GetPoint_RMVX_Ground('f');
                case TerrainState.Vertical:
                    return GetPoint_RMVX_Ground('r');
                case TerrainState.Fill:
                    return GetPoint_RMVX_Ground('n');
            }

            return null;
        }

        public Point CalculateSW_RMVX_Ground(int tile, int W, int SW, int S)
        {
            var tmpTile = new bool[3];
            var situation = TerrainState.Fill;

            if (TileMatch(tile, W)) tmpTile[0] = true;
            if (TileMatch(tile, SW)) tmpTile[1] = true;
            if (TileMatch(tile, S)) tmpTile[2] = true;

            if (!tmpTile[0] && !tmpTile[2]) situation = TerrainState.Inner;
            if (tmpTile[0] && !tmpTile[2]) situation = TerrainState.Horizontal;
            if (!tmpTile[0] && tmpTile[2]) situation = TerrainState.Vertical;
            if (tmpTile[0] && !tmpTile[1] && tmpTile[2]) situation = TerrainState.Outer;
            if (tmpTile[0] && tmpTile[1] && tmpTile[2]) situation = TerrainState.Fill;

            switch (situation)
            {
                case TerrainState.Inner:
                    return GetPoint_RMVX_Ground('o');
                case TerrainState.Outer:
                    return GetPoint_RMVX_Ground('c');
                case TerrainState.Horizontal:
                    return GetPoint_RMVX_Ground('s');
                case TerrainState.Vertical:
                    return GetPoint_RMVX_Ground('g');
                case TerrainState.Fill:
                    return GetPoint_RMVX_Ground('k');
            }

            return null;
        }

        public Point CalculateSE_RMVX_Ground(int tile, int S, int SE, int E)
        {
            var tmpTile = new bool[3];
            var situation = TerrainState.Fill;

            if (TileMatch(tile, S)) tmpTile[0] = true;
            if (TileMatch(tile, SE)) tmpTile[1] = true;
            if (TileMatch(tile, E)) tmpTile[2] = true;

            if (!tmpTile[0] && !tmpTile[2]) situation = TerrainState.Inner;
            if (!tmpTile[0] && tmpTile[2]) situation = TerrainState.Horizontal;
            if (tmpTile[0] && !tmpTile[2]) situation = TerrainState.Vertical;
            if (tmpTile[0] && !tmpTile[1] && tmpTile[2]) situation = TerrainState.Outer;
            if (tmpTile[0] && tmpTile[1] && tmpTile[2]) situation = TerrainState.Fill;

            switch (situation)
            {
                case TerrainState.Inner:
                    return GetPoint_RMVX_Ground('t');
                case TerrainState.Outer:
                    return GetPoint_RMVX_Ground('d');
                case TerrainState.Horizontal:
                    return GetPoint_RMVX_Ground('p');
                case TerrainState.Vertical:
                    return GetPoint_RMVX_Ground('l');
                case TerrainState.Fill:
                    return GetPoint_RMVX_Ground('h');
            }

            return null;
        }

        #endregion

        #region RMVX Cliff

        public void CachePoints_RMVX_Cliff()
        {
            autoInner = new Point[4];
            autoNW = new Point[4];
            autoNE = new Point[4];
            autoSW = new Point[4];
            autoSE = new Point[4];

            // Outer Tiles - NW
            // NW - a
            autoNW[0] = new Point(0, 0);
            // NE - b
            autoNW[1] = new Point(16, 0);
            // SW - c
            autoNW[2] = new Point(0, 16);
            // SE - d
            autoNW[3] = new Point(16, 16);
            // Outer Tiles - NE
            // NW - e
            autoNE[0] = new Point(32, 0);
            // NE - f
            autoNE[1] = new Point(48, 0);
            // SW - g
            autoNE[2] = new Point(32, 16);
            // SE - h
            autoNE[3] = new Point(48, 16);
            // Outer Tiles - SW
            // NW - i
            autoSW[0] = new Point(0, 32);
            // NE - j
            autoSW[1] = new Point(16, 32);
            // SW - k
            autoSW[2] = new Point(0, 48);
            // SE - l
            autoSW[3] = new Point(16, 48);
            // Outer Tiles - SE
            // NW - m
            autoSE[0] = new Point(32, 32);
            // NE - n
            autoSE[1] = new Point(48, 32);
            // SW - o
            autoSE[2] = new Point(32, 48);
            // SE - p
            autoSE[3] = new Point(48, 48);
        }

        public Point GetPoint_RMVX_Cliff(char letter)
        {
            var point = new Point();
            switch (letter)
            {
                case 'a':
                    point = autoNW[0];
                    break;
                case 'b':
                    point = autoNW[1];
                    break;
                case 'c':
                    point = autoNW[2];
                    break;
                case 'd':
                    point = autoNW[3];
                    break;
                case 'e':
                    point = autoNE[0];
                    break;
                case 'f':
                    point = autoNE[1];
                    break;
                case 'g':
                    point = autoNE[2];
                    break;
                case 'h':
                    point = autoNE[3];
                    break;
                case 'i':
                    point = autoSW[0];
                    break;
                case 'j':
                    point = autoSW[1];
                    break;
                case 'k':
                    point = autoSW[2];
                    break;
                case 'l':
                    point = autoSW[3];
                    break;
                case 'm':
                    point = autoSE[0];
                    break;
                case 'n':
                    point = autoSE[1];
                    break;
                case 'o':
                    point = autoSE[2];
                    break;
                case 'p':
                    point = autoSE[3];
                    break;
            }

            return point;
        }

        public Point CalculateNW_RMVX_Cliff(int tile, int NW, int N, int W)
        {
            var tmpTile = new bool[3];
            var situation = TerrainState.Fill;

            if (TileMatch(tile, NW)) tmpTile[0] = true;
            if (TileMatch(tile, N)) tmpTile[1] = true;
            if (TileMatch(tile, W)) tmpTile[2] = true;

            if (!tmpTile[1] && tmpTile[2]) situation = TerrainState.Horizontal;
            if (tmpTile[1] && !tmpTile[2]) situation = TerrainState.Vertical;
            if (tmpTile[0] && tmpTile[1] && tmpTile[2]) situation = TerrainState.Fill;
            if (!tmpTile[1] && !tmpTile[2]) situation = TerrainState.Inner;

            switch (situation)
            {
                case TerrainState.Inner:
                    return GetPoint_RMVX_Cliff('a');
                case TerrainState.Horizontal:
                    return GetPoint_RMVX_Cliff('e');
                case TerrainState.Vertical:
                    return GetPoint_RMVX_Cliff('i');
                case TerrainState.Fill:
                    return GetPoint_RMVX_Cliff('m');
            }

            return null;
        }

        public Point CalculateNE_RMVX_Cliff(int tile, int N, int NE, int E)
        {
            var tmpTile = new bool[3];
            var situation = TerrainState.Fill;

            if (TileMatch(tile, N)) tmpTile[0] = true;
            if (TileMatch(tile, NE)) tmpTile[1] = true;
            if (TileMatch(tile, E)) tmpTile[2] = true;

            if (!tmpTile[0] && tmpTile[2]) situation = TerrainState.Horizontal;
            if (tmpTile[0] && !tmpTile[2]) situation = TerrainState.Vertical;
            if (tmpTile[0] && tmpTile[1] && tmpTile[2]) situation = TerrainState.Fill;
            if (!tmpTile[0] && !tmpTile[2]) situation = TerrainState.Inner;

            switch (situation)
            {
                case TerrainState.Inner:
                    return GetPoint_RMVX_Cliff('f');
                case TerrainState.Horizontal:
                    return GetPoint_RMVX_Cliff('b');
                case TerrainState.Vertical:
                    return GetPoint_RMVX_Cliff('n');
                case TerrainState.Fill:
                    return GetPoint_RMVX_Cliff('j');
            }

            return null;
        }

        public Point CalculateSW_RMVX_Cliff(int tile, int W, int SW, int S)
        {
            var tmpTile = new bool[3];
            var situation = TerrainState.Fill;

            if (TileMatch(tile, W)) tmpTile[0] = true;
            if (TileMatch(tile, SW)) tmpTile[1] = true;
            if (TileMatch(tile, S)) tmpTile[2] = true;

            if (tmpTile[0] && !tmpTile[2]) situation = TerrainState.Horizontal;
            if (!tmpTile[0] && tmpTile[2]) situation = TerrainState.Vertical;
            if (tmpTile[0] && tmpTile[1] && tmpTile[2]) situation = TerrainState.Fill;
            if (!tmpTile[0] && !tmpTile[2]) situation = TerrainState.Inner;

            switch (situation)
            {
                case TerrainState.Inner:
                    return GetPoint_RMVX_Cliff('k');
                case TerrainState.Horizontal:
                    return GetPoint_RMVX_Cliff('o');
                case TerrainState.Vertical:
                    return GetPoint_RMVX_Cliff('c');
                case TerrainState.Fill:
                    return GetPoint_RMVX_Cliff('g');
            }

            return null;
        }

        public Point CalculateSE_RMVX_Cliff(int tile, int S, int SE, int E)
        {
            var tmpTile = new bool[3];
            var situation = TerrainState.Fill;

            if (TileMatch(tile, S)) tmpTile[0] = true;
            if (TileMatch(tile, SE)) tmpTile[1] = true;
            if (TileMatch(tile, E)) tmpTile[2] = true;

            if (!tmpTile[0] && tmpTile[2]) situation = TerrainState.Horizontal;
            if (tmpTile[0] && !tmpTile[2]) situation = TerrainState.Vertical;
            if (tmpTile[0] && tmpTile[1] && tmpTile[2]) situation = TerrainState.Fill;
            if (!tmpTile[0] && !tmpTile[2]) situation = TerrainState.Inner;

            switch (situation)
            {
                case TerrainState.Inner:
                    return GetPoint_RMVX_Cliff('p');
                case TerrainState.Horizontal:
                    return GetPoint_RMVX_Cliff('l');
                case TerrainState.Vertical:
                    return GetPoint_RMVX_Cliff('h');
                case TerrainState.Fill:
                    return GetPoint_RMVX_Cliff('d');
            }

            return null;
        }

        #endregion

        #endregion
    }
}
