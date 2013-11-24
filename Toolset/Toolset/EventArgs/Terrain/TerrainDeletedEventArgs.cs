using System;

namespace Toolset
{
    public class TerrainDeletedEventArgs : EventArgs
    {
        readonly string _name;
        readonly int _id;

        public TerrainDeletedEventArgs(string name, int id)
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