using System;
using CrystalLib.TileEngine;

namespace Toolset
{
    public class TerrainChangedEventArgs : EventArgs
    {
        readonly TerrainTile _oldTerrain;
        readonly TerrainTile _newTerrain;

        public TerrainChangedEventArgs(TerrainTile oldTerrain, TerrainTile newTerrain)
        {
            _oldTerrain = oldTerrain;
            _newTerrain = newTerrain;
        }

        public TerrainTile NewTerrain
        {
            get { return _newTerrain; }
        }

        public TerrainTile OldTerrain
        {
            get { return _oldTerrain; }
        }
    }
}