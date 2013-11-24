using System;
using Toolset.TileEngine;

namespace Toolset
{
    public class MapAddedEventArgs : EventArgs
    {
        readonly EditorTileMap _map;

        public MapAddedEventArgs(EditorTileMap map)
        {
            _map = map;
        }

        public EditorTileMap Map
        {
            get { return _map; }
        }
    }
}