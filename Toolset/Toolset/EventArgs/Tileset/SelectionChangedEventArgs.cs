using System;
using CrystalLib.TileEngine;

namespace Toolset
{
    public class SelectionChangedEventArgs : EventArgs
    {
        readonly PointSelection _selection;

        public SelectionChangedEventArgs(PointSelection selection)
        {
            _selection = selection;
        }

        public PointSelection Selection
        {
            get { return _selection; }
        }
    }
}