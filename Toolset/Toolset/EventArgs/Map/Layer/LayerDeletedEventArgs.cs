using System;
using Toolset.TileEngine;

namespace Toolset
{
    public class LayerSelectedEventArgs : EventArgs
    {
        readonly EditorTileLayer _layer;

        public LayerSelectedEventArgs(EditorTileLayer layer)
        {
            _layer = layer;
        }

        public EditorTileLayer Layer
        {
            get { return _layer; }
        }
    }
}