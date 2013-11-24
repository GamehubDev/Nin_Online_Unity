using System;
using CrystalLib.TileEngine;

namespace Toolset
{
    public class TilesetAddedEventArgs : EventArgs
    {
        readonly Tileset _tileset;

        public TilesetAddedEventArgs(Tileset tileset)
        {
            _tileset = tileset;
        }

        public Tileset Tileset
        {
            get { return _tileset; }
        }
    }
}