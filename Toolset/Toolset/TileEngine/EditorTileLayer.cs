using System;
using System.ComponentModel;
using CrystalLib.Toolset.UndoRedo;
using SFML.Graphics;

namespace Toolset.TileEngine
{
    public class EditorTileLayer : INotifyPropertyChanged
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

        private UndoRedo<string> _name = new UndoRedo<string>();
        public string Name
        {
            get { return _name.Value; }
            set
            {
                if (value == _name.Value) return;
                _name.Value = value;
                OnPropertyChanged("Name");
            }
        }

        private UndoRedo<int> _opacity = new UndoRedo<int>();
        public int Opacity
        {
            get { return _opacity.Value; }
            set
            {
                if (value == _opacity.Value) return;
                _opacity.Value = value;
                OnPropertyChanged("Opacity");
            }
        }

        private UndoRedo<bool> _visible = new UndoRedo<bool>();
        public bool Visible
        {
            get { return _visible.Value; }
            set
            {
                if (value == _visible.Value) return;
                _visible.Value = value;
                OnPropertyChanged("Visible");
            }
        }

        public EditorTile[,] Tiles { get; set; }

        public Sprite[,] Sprites { get; set; }

        public TerrainCache[,] TerrainCache { get; set; }

        #endregion

        #region Constructor Region

        public EditorTileLayer(string name, int opacity, bool visible)
        {
            Name = name;
            Opacity = opacity;
            Visible = visible;

            HookEvents();
        }

        #endregion

        #region Serialization Region

        /// <summary>
        /// Clones the <see cref="EditorTileLayer"/> object.
        /// </summary>
        /// <returns>Copy of the object.</returns>
        public EditorTileLayer Clone()
        {
            var newLayer = new EditorTileLayer(Name, Opacity, Visible);
            newLayer.Tiles = new EditorTile[Tiles.GetLength(0), Tiles.GetLength(1)];

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    var tile = Tiles[x, y];
                    newLayer.Tiles[x, y] = new EditorTile(tile.X, tile.Y, tile.Tileset, tile.SrcX, tile.SrcY, tile.Terrain, newLayer);
                }
            }

            return newLayer;
        }

        #endregion

        #region Undo/Redo Events

        private void HookEvents()
        {
            _name.Changed += delegate { OnPropertyChanged("Name"); };
            _opacity.Changed += delegate { OnPropertyChanged("Opacity"); };
            _visible.Changed += delegate { OnPropertyChanged("Visibile"); };
        }

        #endregion
    }
}
