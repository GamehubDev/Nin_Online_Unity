using System;
using System.Collections.Generic;
using CrystalLib.TileEngine;

namespace Toolset
{
    public class TilesetLoadedEventArgs : EventArgs
    {
        readonly List<Tileset> _tilesets;

        public TilesetLoadedEventArgs(List<Tileset> tilesets)
        {
            _tilesets = tilesets;
        }

        public List<Tileset> Tilesets
        {
            get { return _tilesets; }
        }
    }
}