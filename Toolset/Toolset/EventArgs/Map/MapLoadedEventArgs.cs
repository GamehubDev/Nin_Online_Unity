using System;
using System.Collections.Generic;
using Toolset.TileEngine;

namespace Toolset
{
    public class MapLoadedEventArgs : EventArgs
    {
        readonly List<EditorTileMap> _maps;

        public MapLoadedEventArgs(List<EditorTileMap> maps)
        {
            _maps = maps;
        }

        public List<EditorTileMap> Maps
        {
            get { return _maps; }
        }
    }
}