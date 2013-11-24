using System;
using CrystalLib.TileEngine;

namespace Toolset
{
    public class TerrainAddedEventArgs : EventArgs
    {
        readonly TerrainTile _terrainTile;

        public TerrainAddedEventArgs(TerrainTile terrainTile)
        {
            _terrainTile = terrainTile;
        }

        public TerrainTile TerrainTile
        {
            get { return _terrainTile; }
        }
    }
}