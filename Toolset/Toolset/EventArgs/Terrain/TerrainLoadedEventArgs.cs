using System;
using System.Collections.Generic;
using CrystalLib.TileEngine;

namespace Toolset
{
    public class TerrainLoadedEventArgs : EventArgs
    {
        readonly List<TerrainTile> _terrain;

        public TerrainLoadedEventArgs(List<TerrainTile> terrain)
        {
            _terrain = terrain;
        }

        public List<TerrainTile> Terrain
        {
            get { return _terrain; }
        }
    }
}