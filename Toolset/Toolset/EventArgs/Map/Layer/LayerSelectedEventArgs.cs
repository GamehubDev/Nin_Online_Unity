using System;
using Toolset.TileEngine;

namespace Toolset
{
    public class LayerDeletedEventArgs : EventArgs
    {
        readonly EditorTileLayer _layer;

        public LayerDeletedEventArgs(EditorTileLayer layer)
        {
            _layer = layer;
        }

        public EditorTileLayer Layer
        {
            get { return _layer; }
        }
    }
}