using System;
using Toolset.TileEngine;

namespace Toolset
{
    public class MapChangedEventArgs : EventArgs
    {
        readonly EditorTileMap _oldMap;
        readonly EditorTileMap _newMap;

        public MapChangedEventArgs(EditorTileMap oldMap, EditorTileMap newMap)
        {
            _oldMap = oldMap;
            _newMap = newMap;
        }

        public EditorTileMap OldMap
        {
            get { return _oldMap; }
        }

        public EditorTileMap NewMap
        {
            get { return _newMap; }
        }
    }
}