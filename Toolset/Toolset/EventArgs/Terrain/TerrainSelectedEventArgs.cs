using System;
using CrystalLib.TileEngine;

namespace Toolset
{
    public class TerrainSelectedEventArgs : EventArgs
    {
        readonly TerrainTile _terrain;

        public TerrainSelectedEventArgs(TerrainTile terrain)
        {
            _terrain = terrain;
        }

        public TerrainTile Terrain
        {
            get { return _terrain; }
        }
    }
}