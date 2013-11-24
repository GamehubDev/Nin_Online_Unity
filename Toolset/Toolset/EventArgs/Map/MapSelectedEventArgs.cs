using System;
using Toolset.TileEngine;

namespace Toolset
{
    public class MapSelectedEventArgs : EventArgs
    {
        readonly EditorTileMap _map;

        public MapSelectedEventArgs(EditorTileMap map)
        {
            _map = map;
        }

        public EditorTileMap Map
        {
            get { return _map; }
        }
    }
}