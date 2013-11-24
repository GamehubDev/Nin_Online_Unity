using System;
using Toolset.TileEngine;

namespace Toolset
{
    public class LayerChangedEventArgs : EventArgs
    {
        readonly EditorTileLayer _oldLayer;
        readonly EditorTileLayer _newLayer;

        public LayerChangedEventArgs(EditorTileLayer oldLayer, EditorTileLayer newLayer)
        {
            _oldLayer = oldLayer;
            _newLayer = newLayer;
        }

        public EditorTileLayer OldLayer
        {
            get { return _oldLayer; }
        }

        public EditorTileLayer NewLayer
        {
            get { return _newLayer; }
        }
    }
}