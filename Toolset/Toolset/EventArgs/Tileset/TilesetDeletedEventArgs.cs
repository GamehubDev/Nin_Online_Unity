using System;

namespace Toolset
{
    public class TilesetDeletedEventArgs : EventArgs
    {
        readonly string _name;
        readonly int _id;

        public TilesetDeletedEventArgs(string name, int id)
        {
            _name = name;
            _id = id;
        }

        public string Name
        {
            get { return _name; }
        }

        public int ID
        {
            get { return _id; }
        }
    }
}