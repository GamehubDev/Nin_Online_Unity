using System;
using Toolset.TileEngine;

namespace Toolset
{
    public class ToolSelectedEventArgs : EventArgs
    {
        readonly EditorTool _tool;

        public ToolSelectedEventArgs(EditorTool tool)
        {
            _tool = tool;
        }

        public EditorTool Tool
        {
            get { return _tool; }
        }
    }
}