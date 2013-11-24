using System;
using System.Collections.Generic;
using Toolset.TileEngine;

namespace Toolset
{
    public class LayerOrderChangedEventArgs : EventArgs
    {
        readonly List<EditorTileLayer> _layer;

        public LayerOrderChangedEventArgs(List<EditorTileLayer> layer)
        {
            _layer = layer;
        }

        public List<EditorTileLayer> Layers
        {
            get { return _layer; }
        }
    }
}