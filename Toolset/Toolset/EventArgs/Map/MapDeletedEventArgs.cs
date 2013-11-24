using System;

namespace Toolset
{
    public class MapDeletedEventArgs : EventArgs
    {
        readonly string _name;

        public MapDeletedEventArgs(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}