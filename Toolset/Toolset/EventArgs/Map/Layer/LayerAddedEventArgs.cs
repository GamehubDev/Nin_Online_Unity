using System;
using Toolset.TileEngine;

namespace Toolset
{
    public class LayerAddedEventArgs : EventArgs
    {
        readonly EditorTileLayer _layer;

        public LayerAddedEventArgs(EditorTileLayer layer)
        {
            _layer = layer;
        }

        public EditorTileLayer Layer
        {
            get { return _layer; }
        }
    }
}