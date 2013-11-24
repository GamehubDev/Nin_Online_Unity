using System;
using CrystalLib.TileEngine;

namespace Toolset
{
    public class TilesetChangedEventArgs : EventArgs
    {
        readonly Tileset _oldTileset;
        readonly Tileset _newTileset;

        public TilesetChangedEventArgs(Tileset oldTileset, Tileset newTileset)
        {
            _oldTileset = oldTileset;
            _newTileset = newTileset;
        }

        public Tileset NewTileset
        {
            get { return _newTileset; }
        }

        public Tileset OldTileset
        {
            get { return _oldTileset; }
        }
    }
}