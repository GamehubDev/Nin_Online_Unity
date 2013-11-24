using System;
using CrystalLib.TileEngine;

namespace Toolset
{
    public class TilesetSelectedEventArgs : EventArgs
    {
        readonly Tileset _tileset;

        public TilesetSelectedEventArgs(Tileset tileset)
        {
            _tileset = tileset;
        }

        public Tileset Tileset
        {
            get { return _tileset; }
        }
    }
}