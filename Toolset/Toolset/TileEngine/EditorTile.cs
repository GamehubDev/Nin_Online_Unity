using System.ComponentModel;
using CrystalLib.Toolset.UndoRedo;

namespace Toolset.TileEngine
{
    public class EditorTile : INotifyPropertyChanged
    {
        #region Property Changed Region

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Property Region

        private UndoRedo<int> _x = new UndoRedo<int>();
        public int X
        {
            get { return _x.Value; }
            set 
            {
                if (value == _x.Value) return;
                _x.Value = value;
                OnPropertyChanged("X");
            }
        }

        private UndoRedo<int> _y = new UndoRedo<int>();
        public int Y
        {
            get { return _y.Value; }
            set
            {
                if (value == _y.Value) return;
                _y.Value = value;
                OnPropertyChanged("Y");
            }
        }

        private UndoRedo<int> _tileset = new UndoRedo<int>();
        public int Tileset
        {
            get { return _tileset.Value; }
            set
            {
                if (value == _tileset.Value) return;
                _tileset.Value = value;
                OnPropertyChanged("Tileset");
            }
        }

        private UndoRedo<int> _srcX = new UndoRedo<int>();
        public int SrcX
        {
            get { return _srcX.Value; }
            set
            {
                if (value == _srcX.Value) return;
                _srcX.Value = value;
                OnPropertyChanged("SrcX");
            }
        }

        private UndoRedo<int> _srcY = new UndoRedo<int>();
        public int SrcY
        {
            get { return _srcY.Value; }
            set
            {
                if (value == _srcY.Value) return;
                _srcY.Value = value;
                OnPropertyChanged("SrcY");
            }
        }

        private UndoRedo<int> _terrain = new UndoRedo<int>();
        public int Terrain
        {
            get { return _terrain.Value; }
            set
            {
                if (value == _terrain.Value) return;
                _terrain.Value = value;
                OnPropertyChanged("Terrain");
            }
        }

        public EditorTileLayer Layer { get; set; }

        #endregion

        #region Constructor Region

        public EditorTile()
        {
            HookEvents();
        }

        public EditorTile(int x, int y, int tileset, int srcX, int srcY, int terrain)
        {
            X = x;
            Y = y;
            Tileset = tileset;
            SrcX = srcX;
            SrcY = srcY;
            Terrain = terrain;

            HookEvents();
        }

        public EditorTile(int x, int y, int tileset, int srcX, int srcY, int terrain, EditorTileLayer layer)
        {
            X = x;
            Y = y;
            Tileset = tileset;
            SrcX = srcX;
            SrcY = srcY;
            Terrain = terrain;
            Layer = layer;

            HookEvents();
        }

        #endregion

        #region Undo/Redo Events

        private void HookEvents()
        {
            _x.Changed += delegate { OnPropertyChanged("X"); };
            _y.Changed += delegate { OnPropertyChanged("Y"); };
            _tileset.Changed += delegate { OnPropertyChanged("Tileset"); };
            _srcX.Changed += delegate { OnPropertyChanged("SrcX"); };
            _srcY.Changed += delegate { OnPropertyChanged("SrcY"); };
            _terrain.Changed += delegate { OnPropertyChanged("Terrain"); };
        }

        #endregion
    }
}
