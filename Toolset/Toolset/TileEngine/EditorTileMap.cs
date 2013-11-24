using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalLib.ResourceEngine;
using CrystalLib.TileEngine;
using CrystalLib.Toolset.UndoRedo;
using CrystalLib.Toolset.UndoRedo.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using Toolset.Dialogs;
using Toolset.Enums;
using Toolset.Managers;

namespace Toolset.TileEngine
{
    public class EditorTileMap : INotifyPropertyChanged
    {
        #region Field Region

        private bool _building;
        private bool _dontCache;

        #endregion

        #region Property Changed Region

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Property Region

        private int _id;
        public int ID
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private UndoRedo<int> _width = new UndoRedo<int>();
        public int Width
        {
            get { return _width.Value; }
            set
            {
                if (value == _width.Value) return;
                _width.Value = value;
                OnPropertyChanged("Width");
            }
        }

        private UndoRedo<int> _height = new UndoRedo<int>();
        public int Height
        {
            get { return _height.Value; }
            set
            {
                if (value == _height.Value) return;
                _height.Value = value;
                OnPropertyChanged("Height");
            }
        }

        private UndoRedo<int> _tileWidth = new UndoRedo<int>();
        public int TileWidth
        {
            get { return _tileWidth.Value; }
            set
            {
                if (value == _tileWidth.Value) return;
                _tileWidth.Value = value;
                OnPropertyChanged("TileWidth");
            }
        }

        private UndoRedo<int> _tileHeight = new UndoRedo<int>();
        public int TileHeight
        {
            get { return _tileHeight.Value; }
            set
            {
                if (value == _tileHeight.Value) return;
                _tileHeight.Value = value;
                OnPropertyChanged("TileHeight");
            }
        }

        private bool _unsavedChanges;
        public bool UnsavedChanges
        {
            get { return _unsavedChanges; }
            set
            {
                if (value == _unsavedChanges) return;
                _unsavedChanges = value;
                OnPropertyChanged("UnsavedChanges");
            }
        }

        public UndoRedoList<EditorTileLayer> Layers { get; set; }

        public int ConcreteWidth { get; set; }
        public int ConcreteHeight { get; set; }
        public List<TileLayer> ConcreteLayers { get; set; }

        public EditorTileLayer SelectedLayer { get; set; }

        public List<string> LayerNames
        {
            get
            {
                return Layers.Select(layer => layer.Name).ToList();
            }
        }

        public UndoRedoArea UndoRedoArea { get; set; }

        #endregion

        #region Event Region

        /// <summary>
        /// Event raised when a new <see cref="TileLayer"/> object is added.
        /// </summary>
        public event EventHandler<LayerAddedEventArgs> LayerAdded;

        /// <summary>
        /// Event raised when a <see cref="TileLayer"/> object is changed.
        /// </summary>
        public event EventHandler<LayerChangedEventArgs> LayerChanged;

        /// <summary>
        /// Event raised when the order of the <see cref="TileLayer"/> list is changed.
        /// </summary>
        public event EventHandler<LayerOrderChangedEventArgs> LayerOrderChanged;

        /// <summary>
        /// Event raised when a <see cref="TileLayer"/> object is deleted.
        /// </summary>
        public event EventHandler<LayerDeletedEventArgs> LayerDeleted;

        /// <summary>
        /// Event raised when the currently selected <see cref="TileLayer"/> object is changed.
        /// </summary>
        public event EventHandler<LayerSelectedEventArgs> LayerSelected;

        /// <summary>
        /// Event raised when the <see cref="UnsavedChanges"/> object is changed.
        /// </summary>
        public event EventHandler<UnsavedChangesEventArgs> UnsavedChangesChanged;

        /// <summary>
        /// Event raised when the map size is changed.
        /// </summary>
        public event EventHandler SizeChanged;

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorTileMap"/> class.
        /// </summary>
        public EditorTileMap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorTileMap"/> class.
        /// </summary>
        /// <param name="map"><see cref="TileMap"/> object.</param>
        public EditorTileMap(TileMap map)
        {
            UndoRedoArea = new UndoRedoArea(map.Name) { MaxHistorySize = 100 };

            BuildFromTileMap(map);

            MapManager.Instance.MapChanged += MapChanged;

            LayerAdded += EditorMap_LayerAdded;

            PropertyChanged += EditorMap_PropertyChanged;

            HookEvents();
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the PropertyChanged event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void EditorMap_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UnsavedChanges")
            {
                if (UnsavedChangesChanged != null)
                    UnsavedChangesChanged.Invoke(this, new UnsavedChangesEventArgs(UnsavedChanges));
            }

            /*
             * 
             * Due to the nature of the undo/redo data types we can't actually
             * hold the history of the 2D array layer.tiles itself.
             * 
             * If we were to simply re-build the array every time the map size was changed
             * then we'd be able to undo/redo the map size without the actual map
             * content re-populating accordingly.
             * 
             * As such we bypass this by ONLY re-sizing outwards. If we only
             * ever make the array larger when neccassary then we keep the
             * data we actually need -- the EditorTile objects with their complete
             * undo/redo stack.
             * 
             * When we use a specific anchor point, we don't replace the object,
             * we simply swap the values through direct per-property access. This means
             * the entire undo/redo history of the EditorTileMap object is untouched and
             * can be undone/redone by the user.
             * 
             * If we make the map smaller, we'll keep the array the same size whilst
             * 0ing out the properties (except for X, Y and Layer) so that if you then
             * re-size the map larger it doesn't suddenly bring back a huge swathe of tiles
             * you thought you'd cleared out.
             * 
             * As far as I can tell this is a very dirty way of handling things, but I honestly
             * can't think of any other way to keep the undo/redo stack intact whilst re-sizing
             * the map... At least it's only needed in the toolset and not the actual game client. :/
             * 
             */

            if (e.PropertyName == "Width")
            {
                if (!_building)
                {
                    CacheAllTiles();
                    SizeChanged.Invoke(this, new EventArgs());
                }
            }

            if (e.PropertyName == "Height")
            {
                if (!_building)
                {
                    CacheAllTiles();
                    SizeChanged.Invoke(this, new EventArgs());
                }
            }

            /*if (e.PropertyName == "Width" || e.PropertyName == "Height")
            {
                if (!_building)
                {
                    _building = true;

                    if (!(UndoRedoArea.IsCommandStarted))
                        UndoRedoManager.Start("Resizing map");

                    foreach (var layer in Layers)
                    {
                        if (Width > layer.Tiles.GetLength(0) || Height > layer.Tiles.GetLength(1))
                        {
                            EditorTile[,] tmpArray = layer.Tiles;

                            var newWidth = Math.Max(Width, layer.Tiles.GetLength(0));
                            var newHeight = Math.Max(Height, layer.Tiles.GetLength(1));

                            layer.Tiles = new EditorTile[newWidth, newHeight];

                            layer.Sprites = new Sprite[Width, Height];
                            layer.TerrainCache = new TerrainCache[Width, Height];

                            for (int x = 0; x < newWidth; x++)
                            {
                                for (int y = 0; y < newHeight; y++)
                                {
                                    if (x >= tmpArray.GetLength(0) || y >= tmpArray.GetLength(1))
                                    {
                                        var tile = new EditorTile(x, y, 0, 0, 0, 0, layer);
                                        layer.Tiles[x, y] = tile;
                                        layer.Tiles[x, y].PropertyChanged += Tile_PropertyChanged;
                                    }
                                    else
                                    {
                                        layer.Tiles[x, y] = tmpArray[x, y];
                                        layer.Tiles[x, y].X = x;
                                        layer.Tiles[x, y].Y = y;
                                    }
                                }
                            }
                        }

                        if (Width < layer.Tiles.GetLength(0) || Height < layer.Tiles.GetLength(1))
                        {
                            var arrWidth = layer.Tiles.GetLength(0);
                            var arrHeight = layer.Tiles.GetLength(1);
                            var newWidth = Math.Min(Width, arrWidth);
                            var newHeight = Math.Min(Height, arrHeight);

                            for (int x = 0; x < arrWidth; x++)
                            {
                                for (int y = 0; y < arrHeight; y++)
                                {
                                    if (x > newWidth || y > newHeight)
                                    {
                                        layer.Tiles[x, y].SrcX = 0;
                                        layer.Tiles[x, y].SrcY = 0;
                                        layer.Tiles[x, y].Terrain = 0;
                                        layer.Tiles[x, y].Tileset = 0;
                                    }
                                }
                            }
                        }
                    }

                    if (UndoRedoManager.IsCommandStarted)
                        UndoRedoManager.Commit();

                    _building = false;

                    CacheAllTiles();
                    SizeChanged.Invoke(this, new EventArgs());
                }
            }*/
        }

        /// <summary>
        /// Handles the PropertyChanged event of the layers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Layer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var layer = (EditorTileLayer)sender;

            if (LayerOrderChanged != null)
                LayerOrderChanged.Invoke(this, new LayerOrderChangedEventArgs(Layers.ToList()));
        }

        /// <summary>
        /// Handles the PropertyChanged event of the tiles.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Tile_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_building || _dontCache) return;

            var tile = (EditorTile)sender;

            if (tile.Layer != null)
            {
                if (tile.Terrain > 0)
                    CacheTerrainTile(tile.Layer, tile);
                else
                    CacheTile(tile.Layer, tile);

                CacheSurroundingTerrain(tile.Layer, tile);

                UnsavedChanges = true;
            }
        }

        /// <summary>
        /// Event handler for the MapChanged event of the <see cref="MapManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapChangedEventArgs"/> instance containing the event data.</param>
        private void MapChanged(object sender, MapChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Event handler for the LayerAdded event of the <see cref="EditorTileMap"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerAddedEventArgs"/> instance containing the event data.</param>
        private void EditorMap_LayerAdded(object sender, LayerAddedEventArgs e)
        {
            e.Layer.Tiles = new EditorTile[Width, Height];
            e.Layer.Sprites = new Sprite[Width, Height];
            e.Layer.TerrainCache = new TerrainCache[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var tile = new EditorTile(x, y, 0, 0, 0, 0, e.Layer);
                    e.Layer.Tiles[x, y] = tile;
                    e.Layer.Tiles[x, y].PropertyChanged += Tile_PropertyChanged;
                }
            }

            e.Layer.PropertyChanged += Layer_PropertyChanged;
        }

        #endregion

        #region Layer Management Region

        /// <summary>
        /// Checks if a <see cref="TileLayer"/> object already exists.
        /// </summary>
        /// <param name="name">The name of the <see cref="TileLayer"/> object.</param>
        /// <returns>Returns true if layer already exists.</returns>
        public bool CheckLayer(string name)
        {
            return Layers.Any(layer => layer.Name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Returns a <see cref="TileLayer"/> object based on name.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        /// <returns><see cref="TileLayer"/> object found with that name.</returns>
        public EditorTileLayer GetLayer(string name)
        {
            return Layers.FirstOrDefault(layer => layer.Name == name);
        }

        /// <summary>
        /// Sets the <see cref="SelectedLayer"/> property and invokes the <see cref="LayerSelected"/> event.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        public void SelectLayer(string name)
        {
            var layer = GetLayer(name);
            if (layer == null) return;

            SelectedLayer = layer;

            if (LayerSelected != null)
                LayerSelected.Invoke(this, new LayerSelectedEventArgs(layer));
        }

        /// <summary>
        /// Creates a new layer.
        /// </summary>
        public void NewLayer()
        {
            using (var dialog = new DialogLayer())
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var name = dialog.LayerName;
                var opacity = dialog.LayerOpacity;
                var visible = dialog.LayerVisible;

                if (CheckLayer(name))
                {
                    MessageBox.Show(@"A layer with this name already exists.", @"New Layer");
                    return;
                }

                using (UndoRedoArea.Start("New Layer"))
                {
                    var layer = new EditorTileLayer(name, opacity, visible);

                    Layers.Add(layer);

                    if (LayerAdded != null)
                        LayerAdded.Invoke(this, new LayerAddedEventArgs(layer));

                    SelectLayer(layer.Name);

                    UnsavedChanges = true;

                    UndoRedoArea.Commit();
                }
            }
        }

        /// <summary>
        /// Renames the currently selected layer.
        /// </summary>
        public void RenameLayer()
        {
            using (var dialog = new DialogRename(SelectedLayer.Name))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                if (SelectedLayer.Name != dialog.NewName)
                {
                    if (CheckLayer(dialog.NewName))
                    {
                        MessageBox.Show(@"A layer with this name already exists.", @"Rename Layer");
                        return;
                    }
                }

                using (UndoRedoArea.Start("Renamed Layer"))
                {
                    var oldLayer = SelectedLayer.Clone();

                    SelectedLayer.Name = dialog.NewName;

                    if (LayerChanged != null)
                        LayerChanged.Invoke(this, new LayerChangedEventArgs(oldLayer, SelectedLayer));

                    UnsavedChanges = true;

                    UndoRedoArea.Commit();
                }
            }
        }

        /// <summary>
        /// Duplicates the currently selected layer.
        /// </summary>
        public void DuplicateLayer()
        {
            using (UndoRedoArea.Start("Duplicated Layer"))
            {
                var newLayer = SelectedLayer.Clone();
                newLayer.Name += " Copy";

                using (var dialog = new DialogLayer(newLayer.Name, newLayer.Opacity, newLayer.Visible))
                {
                    var result = dialog.ShowDialog();
                    if (result != DialogResult.OK) return;

                    var name = dialog.LayerName;
                    var opacity = dialog.LayerOpacity;
                    var visible = dialog.LayerVisible;

                    if (CheckLayer(name))
                    {
                        MessageBox.Show(@"A layer with this name already exists.", @"Duplicate Layer");
                        return;
                    }

                    newLayer.Name = name;
                    newLayer.Opacity = opacity;
                    newLayer.Visible = visible;

                    var index = Layers.IndexOf(SelectedLayer);
                    Layers.Insert(index + 1, newLayer);

                    if (LayerAdded != null)
                        LayerAdded.Invoke(this, new LayerAddedEventArgs(newLayer));

                    SelectLayer(newLayer.Name);

                    UnsavedChanges = true;
                }

                UndoRedoArea.Commit();
            }
        }

        /// <summary>
        /// Deletes the currently selected layer.
        /// </summary>
        public void DeleteLayer()
        {
            DialogResult result = MessageBox.Show(@"Are you sure you want to delete " + SelectedLayer.Name + @"?", @"DeleteLayer", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            var index = Layers.IndexOf(SelectedLayer);
            if (index > 0)
                index--;
            else
                index = 0;

            var newLayer = Layers[index];

            using (UndoRedoArea.Start("Deleted Layer"))
            {
                if (LayerDeleted != null)
                    LayerDeleted.Invoke(this, new LayerDeletedEventArgs(SelectedLayer.Clone()));

                Layers.Remove(SelectedLayer);

                SelectLayer(newLayer.Name);

                UnsavedChanges = true;

                UndoRedoArea.Commit();
            }
        }

        /// <summary>
        /// Moves the currently selected layer up in the array.
        /// </summary>
        public void MoveLayerUp()
        {
            var index = Layers.IndexOf(SelectedLayer);

            if (index <= 0) return;
            if (Layers[index - 1] == null) return;

            using (UndoRedoArea.Start("Moved Layer Up"))
            {
                var tmp = Layers[index - 1];
                Layers[index - 1] = SelectedLayer;
                Layers[index] = tmp;

                if (LayerOrderChanged != null)
                    LayerOrderChanged.Invoke(this, new LayerOrderChangedEventArgs(Layers.ToList()));

                UnsavedChanges = true;

                UndoRedoArea.Commit();
            }
        }

        /// <summary>
        /// Moves the currently selected layer down in the array.
        /// </summary>
        public void MoveLayerDown()
        {
            var index = Layers.IndexOf(SelectedLayer);

            if (index >= Layers.Count - 1) return;
            if (Layers[index + 1] == null) return;

            using (UndoRedoArea.Start("Moved Layer Down"))
            {
                var tmp = Layers[index + 1];
                Layers[index + 1] = SelectedLayer;
                Layers[index] = tmp;

                if (LayerOrderChanged != null)
                    LayerOrderChanged.Invoke(this, new LayerOrderChangedEventArgs(Layers.ToList()));

                UnsavedChanges = true;

                UndoRedoArea.Commit();
            }
        }

        /// <summary>
        /// Sets layer's visibility.
        /// </summary>
        /// <param name="layerName">Name of the layer.</param>
        /// <param name="visible">Visibility of the layer.</param>
        public void SetLayerVisible(string layerName, bool visible)
        {
            var layer = GetLayer(layerName);
            if (layer == null) return;
            if (layer.Visible == visible) return;

            using (UndoRedoArea.Start("Set Layer Visibility"))
            {
                var oldLayer = layer.Clone();

                layer.Visible = visible;

                if (LayerChanged != null)
                    LayerChanged.Invoke(this, new LayerChangedEventArgs(oldLayer, layer));

                UnsavedChanges = true;

                UndoRedoArea.Commit();
            }
        }

        #endregion

        #region Map Editor Region

        /// <summary>
        /// Re-sizes the map based on the anchor and size.
        /// </summary>
        /// <param name="width">New width of the map.</param>
        /// <param name="height">New height of the map.</param>
        /// <param name="tilewidth">New width of the tiles.</param>
        /// <param name="tileheight">New height of the tiles.</param>
        /// <param name="anchor">Anchor point to resize around.</param>
        public void ResizeMap(int width, int height, int tilewidth, int tileheight, AnchorPoints anchor)
        {
            if (tilewidth != TileWidth) TileWidth = tilewidth;
            if (tileheight != TileHeight) TileHeight = tileheight;

            if (Width == width && Height == height) return;

            using (UndoRedoArea.Start("Map Size Changed"))
            {
                /*
                 * The actual re-sizing the array (when it's actually resized) is in the
                 * property changed events for the "Width" and "Height" properties.
                 * 
                 * Here we'll simply move the tiles around based on the anchor point passed
                 * by the user.
                 * 
                 * We cannot destroy objects, so we'll be copying over data between tiles
                 * directly in to the tile properties. This allows us to retain the undo/redo
                 * stack of each tile without moving the objects themselves.
                 * 
                 * After that, setting the Width and Height properties will cause the array
                 * itself to resize (or not, depending) and pass through the changes
                 * to the ScrollMap controls accordingly.
                 * 
                 * If the new width is less than the old one, then we'll move the tiles
                 * before changing the size.
                 * 
                 * If the new width is more than the old one, then we'll change the size
                 * and then move the tiles.
                 * 
                 */

                var oldWidth = Width;
                var oldHeight = Height;

                var difference = 0;

                _building = true;

                if (width < oldWidth)
                {
                    switch (anchor)
                    {
                        case AnchorPoints.Right:
                        case AnchorPoints.UpRight:
                        case AnchorPoints.DownRight:
                            difference = oldWidth - width;
                            foreach (var layer in Layers)
                            {
                                for (int x = 0; x < oldWidth; x++)
                                {
                                    for (int y = 0; y < oldHeight; y++)
                                    {
                                        if (x < oldWidth - difference)
                                        {
                                            var tile = layer.Tiles[x + difference, y];
                                            layer.Tiles[x, y].SrcX = tile.SrcX;
                                            layer.Tiles[x, y].SrcY = tile.SrcY;
                                            layer.Tiles[x, y].Terrain = tile.Terrain;
                                            layer.Tiles[x, y].Tileset = tile.Tileset;
                                        }
                                    }
                                }
                            }
                            break;
                        case AnchorPoints.Center: case AnchorPoints.Up: case AnchorPoints.Down:
                            difference = (oldWidth / 2) - (width / 2);
                            foreach (var layer in Layers)
                            {
                                for (int x = 0; x < oldWidth; x++)
                                {
                                    for (int y = 0; y < oldHeight; y++)
                                    {
                                        if (x < oldWidth - (oldWidth - width))
                                        {
                                            var tile = layer.Tiles[x + difference, y];
                                            layer.Tiles[x, y].SrcX = tile.SrcX;
                                            layer.Tiles[x, y].SrcY = tile.SrcY;
                                            layer.Tiles[x, y].Terrain = tile.Terrain;
                                            layer.Tiles[x, y].Tileset = tile.Tileset;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }

                if (height < oldHeight)
                {
                    switch (anchor)
                    {
                        case AnchorPoints.Down:
                        case AnchorPoints.DownRight:
                        case AnchorPoints.DownLeft:
                            difference = oldHeight - height;
                            foreach (var layer in Layers)
                            {
                                for (int x = 0; x < oldWidth; x++)
                                {
                                    for (int y = 0; y < oldHeight; y++)
                                    {
                                        if (y < oldHeight - difference)
                                        {
                                            var tile = layer.Tiles[x, y + difference];
                                            layer.Tiles[x, y].SrcX = tile.SrcX;
                                            layer.Tiles[x, y].SrcY = tile.SrcY;
                                            layer.Tiles[x, y].Terrain = tile.Terrain;
                                            layer.Tiles[x, y].Tileset = tile.Tileset;
                                        }
                                    }
                                }
                            }
                            break;
                        case AnchorPoints.Center:
                        case AnchorPoints.Left:
                        case AnchorPoints.Right:
                            difference = (oldHeight / 2) - (height / 2);
                            foreach (var layer in Layers)
                            {
                                for (int x = 0; x < oldWidth; x++)
                                {
                                    for (int y = 0; y < oldHeight; y++)
                                    {
                                        if (y < oldHeight - (oldHeight - height))
                                        {
                                            var tile = layer.Tiles[x, y + difference];
                                            layer.Tiles[x, y].SrcX = tile.SrcX;
                                            layer.Tiles[x, y].SrcY = tile.SrcY;
                                            layer.Tiles[x, y].Terrain = tile.Terrain;
                                            layer.Tiles[x, y].Tileset = tile.Tileset;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }

                Width = width;
                Height = height;

                // resize

                foreach (var layer in Layers)
                {
                    if (width > layer.Tiles.GetLength(0) || height > layer.Tiles.GetLength(1))
                    {
                        EditorTile[,] tmpArray = layer.Tiles;

                        var newwidth = Math.Max(width, layer.Tiles.GetLength(0));
                        var newheight = Math.Max(height, layer.Tiles.GetLength(1));

                        layer.Tiles = new EditorTile[newwidth, newheight];

                        layer.Sprites = new Sprite[width, height];
                        layer.TerrainCache = new TerrainCache[width, height];

                        for (int x = 0; x < newwidth; x++)
                        {
                            for (int y = 0; y < newheight; y++)
                            {
                                if (x >= tmpArray.GetLength(0) || y >= tmpArray.GetLength(1))
                                {
                                    var tile = new EditorTile(x, y, 0, 0, 0, 0, layer);
                                    layer.Tiles[x, y] = tile;
                                    layer.Tiles[x, y].PropertyChanged += Tile_PropertyChanged;
                                }
                                else
                                {
                                    layer.Tiles[x, y] = tmpArray[x, y];
                                    layer.Tiles[x, y].X = x;
                                    layer.Tiles[x, y].Y = y;
                                }
                            }
                        }
                    }

                    if (width < layer.Tiles.GetLength(0) || height < layer.Tiles.GetLength(1))
                    {
                        var arrwidth = layer.Tiles.GetLength(0);
                        var arrheight = layer.Tiles.GetLength(1);
                        var newwidth = Math.Min(width, arrwidth);
                        var newheight = Math.Min(height, arrheight);

                        for (int x = 0; x < arrwidth; x++)
                        {
                            for (int y = 0; y < arrheight; y++)
                            {
                                if (x > newwidth || y > newheight)
                                {
                                    layer.Tiles[x, y].SrcX = 0;
                                    layer.Tiles[x, y].SrcY = 0;
                                    layer.Tiles[x, y].Terrain = 0;
                                    layer.Tiles[x, y].Tileset = 0;
                                }
                            }
                        }
                    }
                }

                // end resize

                if (width > oldWidth)
                {
                    switch (anchor)
                    {
                        case AnchorPoints.Right:
                        case AnchorPoints.UpRight:
                        case AnchorPoints.DownRight:
                            difference = width - oldWidth;
                            foreach (var layer in Layers)
                            {
                                for (int x = width - 1; x > 0; x--)
                                {
                                    for (int y = 0; y < Height; y++)
                                    {
                                        if (x - difference >= 0)
                                        {
                                            var tile = layer.Tiles[x - difference, y];
                                            layer.Tiles[x, y].SrcX = tile.SrcX;
                                            layer.Tiles[x, y].SrcY = tile.SrcY;
                                            layer.Tiles[x, y].Terrain = tile.Terrain;
                                            layer.Tiles[x, y].Tileset = tile.Tileset;
                                        }
                                    }
                                }
                                for (int x = 0; x < difference; x++)
                                {
                                    for (int y = 0; y < Height; y++)
                                    {
                                        layer.Tiles[x, y].SrcX = 0;
                                        layer.Tiles[x, y].SrcY = 0;
                                        layer.Tiles[x, y].Terrain = 0;
                                        layer.Tiles[x, y].Tileset = 0;
                                    }
                                }
                            }
                            break;
                        case AnchorPoints.Center:
                        case AnchorPoints.Up:
                        case AnchorPoints.Down:
                            difference = (width / 2) - (oldWidth / 2);
                            foreach (var layer in Layers)
                            {
                                for (int x = width - 1; x > 0; x--)
                                {
                                    for (int y = 0; y < Height; y++)
                                    {
                                        if (x - difference >= 0)
                                        {
                                            var tile = layer.Tiles[x - difference, y];
                                            layer.Tiles[x, y].SrcX = tile.SrcX;
                                            layer.Tiles[x, y].SrcY = tile.SrcY;
                                            layer.Tiles[x, y].Terrain = tile.Terrain;
                                            layer.Tiles[x, y].Tileset = tile.Tileset;
                                        }
                                    }
                                }
                                for (int x = 0; x < difference; x++)
                                {
                                    for (int y = 0; y < Height; y++)
                                    {
                                        layer.Tiles[x, y].SrcX = 0;
                                        layer.Tiles[x, y].SrcY = 0;
                                        layer.Tiles[x, y].Terrain = 0;
                                        layer.Tiles[x, y].Tileset = 0;
                                    }
                                }
                            }
                            break;
                    }
                }

                if (height > oldHeight)
                {
                    switch (anchor)
                    {
                        case AnchorPoints.Down:
                        case AnchorPoints.DownRight:
                        case AnchorPoints.DownLeft:
                            difference = height - oldHeight;
                            foreach (var layer in Layers)
                            {
                                for (int x = 0; x < Width; x++)
                                {
                                    for (int y = height - 1; y > 0; y--)
                                    {
                                        if (y - difference >= 0)
                                        {
                                            var tile = layer.Tiles[x, y - difference];
                                            layer.Tiles[x, y].SrcX = tile.SrcX;
                                            layer.Tiles[x, y].SrcY = tile.SrcY;
                                            layer.Tiles[x, y].Terrain = tile.Terrain;
                                            layer.Tiles[x, y].Tileset = tile.Tileset;
                                        }
                                    }
                                }
                                for (int x = 0; x < Width; x++)
                                {
                                    for (int y = 0; y < difference; y++)
                                    {
                                        layer.Tiles[x, y].SrcX = 0;
                                        layer.Tiles[x, y].SrcY = 0;
                                        layer.Tiles[x, y].Terrain = 0;
                                        layer.Tiles[x, y].Tileset = 0;
                                    }
                                }
                            }
                            break;
                        case AnchorPoints.Center:
                        case AnchorPoints.Left:
                        case AnchorPoints.Right:
                            difference = (height / 2) - (oldHeight / 2);
                            foreach (var layer in Layers)
                            {
                                for (int x = 0; x < Width; x++)
                                {
                                    for (int y = height - 1; y > 0; y--)
                                    {
                                        if (y - difference >= 0)
                                        {
                                            var tile = layer.Tiles[x, y - difference];
                                            layer.Tiles[x, y].SrcX = tile.SrcX;
                                            layer.Tiles[x, y].SrcY = tile.SrcY;
                                            layer.Tiles[x, y].Terrain = tile.Terrain;
                                            layer.Tiles[x, y].Tileset = tile.Tileset;
                                        }
                                    }
                                }
                                for (int x = 0; x < Width; x++)
                                {
                                    for (int y = 0; y < difference; y++)
                                    {
                                        layer.Tiles[x, y].SrcX = 0;
                                        layer.Tiles[x, y].SrcY = 0;
                                        layer.Tiles[x, y].Terrain = 0;
                                        layer.Tiles[x, y].Tileset = 0;
                                    }
                                }
                            }
                            break;
                    }
                }

                UnsavedChanges = true;

                _building = false;

                CacheAllTiles();
                SizeChanged.Invoke(this, new EventArgs());

                UndoRedoArea.Commit();
            };
        }

        /// <summary>
        /// Caches all tiles.
        /// </summary>
        public void CacheAllTiles()
        {
            if (_dontCache) return;
            Console.WriteLine(@"Caching map {0}", Name);
            foreach (var layer in Layers)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (layer.Tiles[x, y] == null)
                        {
                            layer.Sprites[x, y] = null;
                            layer.TerrainCache[x, y] = null;
                            continue;
                        }

                        if (layer.Tiles[x, y].Terrain == 0)
                            CacheTile(layer, layer.Tiles[x, y]);
                        else
                            CacheTerrainTile(layer, layer.Tiles[x, y]);
                    }
                }
            }
        }

        /// <summary>
        /// Caches an individual tile's sprite.
        /// </summary>
        /// <param name="layer">Layer of the tile.</param>
        /// <param name="tile">Tile to cache.</param>
        private void CacheTile(EditorTileLayer layer, EditorTile tile)
        {
            layer.TerrainCache[tile.X, tile.Y] = null;

            var tileset = TilesetManager.Instance.GetTileset(tile.Tileset);
            if (tileset == null)
            {
                layer.Sprites[tile.X, tile.Y] = null;
                return;
            }

            layer.Sprites[tile.X, tile.Y] = new Sprite
            {
                Texture = ResourceManager.Instance.LoadTexture(tileset.Image),
                Position = new Vector2f(tile.X * tileset.TileWidth, tile.Y * tileset.TileHeight),
                TextureRect = new IntRect(tile.SrcX * tileset.TileWidth, tile.SrcY * tileset.TileHeight, tileset.TileWidth, tileset.TileHeight)
            };
        }

        private void CacheSurroundingTerrain(EditorTileLayer layer, EditorTile tile)
        {
            var tiles = new List<EditorTile>();

            if (!(tile.X - 1 < 0 || tile.Y - 1 < 0))
                tiles.Add(layer.Tiles[tile.X - 1, tile.Y - 1]);
            if (!(tile.Y - 1 < 0))
                tiles.Add(layer.Tiles[tile.X, tile.Y - 1]);
            if (!(tile.X + 1 > Width - 1 || tile.Y - 1 < 0))
                tiles.Add(layer.Tiles[tile.X + 1, tile.Y - 1]);
            if (!(tile.X + 1 > Width - 1))
                tiles.Add(layer.Tiles[tile.X + 1, tile.Y]);
            if (!(tile.X + 1 > Width - 1 || tile.Y + 1 > Height - 1))
                tiles.Add(layer.Tiles[tile.X + 1, tile.Y + 1]);
            if (!(tile.Y + 1 > Height - 1))
                tiles.Add(layer.Tiles[tile.X, tile.Y + 1]);
            if (!(tile.X - 1 < 0 || tile.Y + 1 > Height - 1))
                tiles.Add(layer.Tiles[tile.X - 1, tile.Y + 1]);
            if (!(tile.X - 1 < 0))
                tiles.Add(layer.Tiles[tile.X - 1, tile.Y]);

            foreach (var item in tiles)
            {
                if (item.Terrain > 0)
                    CacheTerrainTile(layer, item);
            }
        }

        /// <summary>
        /// Caches an individual terrain tile's sprite.
        /// </summary>
        /// <param name="layer">Layer of the tile.</param>
        /// <param name="tile">Tile to cache.</param>
        private void CacheTerrainTile(EditorTileLayer layer, EditorTile tile)
        {
            layer.Sprites[tile.X, tile.Y] = null;

            var terrain = TerrainManager.Instance.GetTerrain(tile.Terrain);
            if (terrain == null)
            {
                layer.TerrainCache[tile.X, tile.Y] = null;
                return;
            }

            layer.TerrainCache[tile.X, tile.Y] = new TerrainCache();

            EditorTile NW; EditorTile N; EditorTile NE; EditorTile E; EditorTile SE; EditorTile S; EditorTile SW; EditorTile W;

            EditorTile forcedTile;
            if (!UndoRedoManager.IsCommandStarted && !UndoRedoArea.IsCommandStarted)
            {
                using (UndoRedoManager.Start("Creating fake tile"))
                {
                    forcedTile = new EditorTile(0, 0, 0, 0, 0, tile.Terrain);
                    UndoRedoManager.Commit();
                }
            }
            else
            {
                forcedTile = new EditorTile(0, 0, 0, 0, 0, tile.Terrain);
            }

            if (!(tile.X - 1 < 0 || tile.Y - 1 < 0))
                NW = layer.Tiles[tile.X - 1, tile.Y - 1];
            else
                NW = forcedTile;
            if (!(tile.Y - 1 < 0))
                N = layer.Tiles[tile.X, tile.Y - 1];
            else
                N = forcedTile;
            if (!(tile.X + 1 > Width - 1 || tile.Y - 1 < 0))
                NE = layer.Tiles[tile.X + 1, tile.Y - 1];
            else
                NE = forcedTile;
            if (!(tile.X + 1 > Width - 1))
                E = layer.Tiles[tile.X + 1, tile.Y];
            else
                E = forcedTile;
            if (!(tile.X + 1 > Width - 1 || tile.Y + 1 > Height - 1))
                SE = layer.Tiles[tile.X + 1, tile.Y + 1];
            else
                SE = forcedTile;
            if (!(tile.Y + 1 > Height - 1))
                S = layer.Tiles[tile.X, tile.Y + 1];
            else
                S = forcedTile;
            if (!(tile.X - 1 < 0 || tile.Y + 1 > Height - 1))
                SW = layer.Tiles[tile.X - 1, tile.Y + 1];
            else
                SW = forcedTile;
            if (!(tile.X - 1 < 0))
                W = layer.Tiles[tile.X - 1, tile.Y];
            else
                W = forcedTile;

            layer.TerrainCache[tile.X, tile.Y].Cache(tile.X, tile.Y, tile.Terrain, NW.Terrain, N.Terrain, NE.Terrain, E.Terrain, SE.Terrain, S.Terrain, SW.Terrain, W.Terrain);
        }

        /// <summary>
        /// Sets the selection of tiles to the map.
        /// </summary>
        /// <param name="tileset">Tileset of the tiles.</param>
        /// <param name="layer">Layer to set the tiles to.</param>
        /// <param name="selection">Selection of tiles to place.</param>
        /// <param name="x">X co-ordinate to place to.</param>
        /// <param name="y">Y co-orindate to place to.</param>
        public void SetTiles(Tileset tileset, EditorTileLayer layer, PointSelection selection, int x, int y)
        {
            foreach (var point in selection.Points)
            {
                var x2 = (x - selection.Offset.X) + point.X;
                var y2 = (y - selection.Offset.Y) + point.Y;

                if (x2 < 0 || y2 < 0 || x2 > Width - 1 || y2 > Height - 1) continue;

                SetTile(tileset, layer, x2, y2, point.X, point.Y);
            }
        }

        /// <summary>
        /// Sets an individual tile to the map.
        /// </summary>
        /// <param name="tileset">Tileset of the tile.</param>
        /// <param name="layer">Layer to set the tile to.</param>
        /// <param name="destX">X co-ordinate to place to.</param>
        /// <param name="destY">Y co-ordinate to place to.</param>
        /// <param name="srcX">X co-ordinate of the tile on the tileset.</param>
        /// <param name="srcY">Y co-ordinate of the tile on the tileset.</param>
        private void SetTile(Tileset tileset, EditorTileLayer layer, int destX, int destY, int srcX, int srcY)
        {
            layer.Tiles[destX, destY].Tileset = tileset.ID;
            layer.Tiles[destX, destY].X = destX;
            layer.Tiles[destX, destY].Y = destY;
            layer.Tiles[destX, destY].SrcX = srcX;
            layer.Tiles[destX, destY].SrcY = srcY;
            layer.Tiles[destX, destY].Terrain = 0;
        }

        /// <summary>
        /// Sets a terrain tile
        /// </summary>
        /// <param name="layer">Layer to set the terrain to</param>
        /// <param name="terrain">TerrainTile to set down</param>
        /// <param name="destX">X co-ordinate to place to</param>
        /// <param name="destY">Y co-ordinate to place to</param>
        public void SetTerrain(EditorTileLayer layer, TerrainTile terrain, int destX, int destY)
        {
            if (destX < 0 || destY < 0 || destX > Width - 1 || destY > Height - 1) return;

            layer.Tiles[destX, destY].Tileset = 0;
            layer.Tiles[destX, destY].X = destX;
            layer.Tiles[destX, destY].Y = destY;
            layer.Tiles[destX, destY].SrcX = 0;
            layer.Tiles[destX, destY].SrcY = 0;
            layer.Tiles[destX, destY].Terrain = terrain.ID;
        }

        /// <summary>
        /// Removes a tile from the map.
        /// </summary>
        /// <param name="layer">Layer the tile is on.</param>
        /// <param name="destX">X co-ordinate of the tile.</param>
        /// <param name="destY">Y co-ordinate of the tile.</param>
        public void RemoveTile(EditorTileLayer layer, int destX, int destY)
        {
            if (destX < 0 || destY < 0 || destX > Width - 1 || destY > Height - 1) return;
            layer.Tiles[destX, destY].Tileset = 0;
            layer.Tiles[destX, destY].SrcX = 0;
            layer.Tiles[destX, destY].SrcY = 0;
            layer.Tiles[destX, destY].Terrain = 0;

            layer.Sprites[destX, destY] = null;
            layer.TerrainCache[destX, destY] = null;
        }

        #endregion

        #region Flood Fill Region

        private EditorTile[,] _tileMap;
        private List<Point> _tilesToCache;

        private FillTile[,] _cacheMap;
        private List<Point> _tileCache;

        private bool[,] _tileChecked;

        /// <summary>
        /// Runs a flood-fill algorithm with the passed co-ordinates of the root.
        /// Returns an enumerable list of points based on the results.
        /// </summary>
        /// <param name="tileset">Tileset to replace with.</param>
        /// <param name="selection">Tile selection to replace with.</param>
        /// <param name="x">X co-ordinate of the root tile.</param>
        /// <param name="y">Y co-ordinate of the root tile.</param>
        /// <returns>An enumerable list of points.</returns>
        public List<Point> FillCache(Tileset tileset, PointSelection selection, int x, int y)
        {
            if (x < 0 || x > Width - 1 || y < 0 || y > Height - 1) return null;

            _tileCache = new List<Point>();

            _cacheMap = new FillTile[Width, Height];
            for (var x2 = 0; x2 < Width; x2++)
            {
                for (var y2 = 0; y2 < Height; y2++)
                {
                    var tile = SelectedLayer.Tiles[x2, y2];
                    _cacheMap[x2, y2] = new FillTile(tile.Tileset, tile.SrcX, tile.SrcY, tile.Terrain);
                }
            }

            _tileChecked = new bool[Width, Height];

            var point = selection.GetTopLeftMostPoint();
            var replacementTile = new FillTile(tileset.ID, point.X, point.Y, 0);
            var currTile = GetFillCacheTile(x, y, replacementTile);
            var originalFillTile = new FillTile(currTile.Tileset, currTile.X, currTile.Y, currTile.Terrain);

            FillCacheTiles(originalFillTile, replacementTile, x, y);

            return _tileCache;
        }

        /// <summary>
        /// Begins the actual flood-fill algorithm. Should only be called from the <see cref="FillCache"/> method.
        /// </summary>
        /// <param name="originalTile">Root tile to check.</param>
        /// <param name="replacementTile">Tile to replace all tiles with.</param>
        /// <param name="destx">Root X co-ordinate.</param>
        /// <param name="desty">Root Y co-ordinate.</param>
        private void FillCacheTiles(FillTile originalTile, FillTile replacementTile, int destx, int desty)
        {
            var open = new Queue<Point>();
            open.Enqueue(new Point(destx, desty));

            while (open.Count > 0)
            {
                var current = open.Dequeue();
                var x = current.X;
                var y = current.Y;

                CheckCacheFillTile(open, x, y, originalTile, replacementTile);

                if (x > 0)
                    CheckCacheFillTile(open, x - 1, y, originalTile, replacementTile);
                if (x < Width - 1)
                    CheckCacheFillTile(open, x + 1, y, originalTile, replacementTile);  
                if (y > 0)
                    CheckCacheFillTile(open, x, y - 1, originalTile, replacementTile);
                if (y < Height - 1)
                    CheckCacheFillTile(open, x, y + 1, originalTile, replacementTile);
            }
        }

        /// <summary>
        /// Checks a specific point in the check queue.
        /// </summary>
        /// <param name="open">Queue the point is in.</param>
        /// <param name="x">X co-ordinate of the point.</param>
        /// <param name="y">Y co-ordinate of the point.</param>
        /// <param name="originalTile">Tile to replace.</param>
        /// <param name="replacementTile">Tile to replace it with.</param>
        private void CheckCacheFillTile(Queue<Point> open, int x, int y, FillTile originalTile, FillTile replacementTile)
        {
            if (_tileChecked[x, y]) return;
            var currTile = GetFillCacheTile(x, y, replacementTile);
            if (CompareCacheTile(currTile, originalTile))
            {
                currTile.Tileset = replacementTile.Tileset;
                currTile.X = replacementTile.X;
                currTile.Y = replacementTile.Y;
                currTile.Terrain = replacementTile.Terrain;
                _tileCache.Add(new Point(x, y));

                _tileChecked[x, y] = true;
                open.Enqueue(new Point(x, y));
            }
        }

        /// <summary>
        /// If a cache map tile doesn't exist, it'll create a new fill tile.
        /// </summary>
        /// <param name="x">X co-ordinate of the tile.</param>
        /// <param name="y">Y co-ordinate of the tile.</param>
        /// <param name="fillTile">Tile to replace with.</param>
        /// <returns>Returns a generated FillTile object.</returns>
        private FillTile GetFillCacheTile(int x, int y, FillTile fillTile)
        {
            var tile = _cacheMap[x, y] ?? new FillTile(fillTile.Tileset, x, y, 0);
            return tile;
        }

        /// <summary>
        /// Compares two FillTile objects.
        /// </summary>
        /// <param name="targetTile">Object to compare against.</param>
        /// <param name="replacementTile">Object to compare to.</param>
        /// <returns>True if tiles match, false if not.</returns>
        public bool CompareCacheTile(FillTile targetTile, FillTile replacementTile)
        {
            return targetTile.Tileset == replacementTile.Tileset && targetTile.X == replacementTile.X && targetTile.Y == replacementTile.Y && targetTile.Terrain == replacementTile.Terrain;
        }

        public void Fill(Tileset tileset, PointSelection selection, int x, int y)
        {
            if (x < 0 || x > Width - 1 || y < 0 || y > Height - 1) return;

            using (UndoRedoArea.Start("Fill Tool"))
            {
                _tileChecked = new bool[Width, Height];
                _tileMap = SelectedLayer.Tiles;
                _tilesToCache = new List<Point>();

                var tile = selection.GetTopLeftMostPoint();
                var replacementTile = new FillTile(tileset.ID, tile.X, tile.Y, 0);
                var currTile = GetFillTile(x, y, replacementTile);
                var originalFillTile = new FillTile(currTile.Tileset, currTile.SrcX, currTile.SrcY, currTile.Terrain);

                FillTiles(currTile, originalFillTile, replacementTile);

                UndoRedoArea.Commit();
            }
        }

        private void FillTiles(EditorTile tile, FillTile originalTile, FillTile replacementTile)
        {
            var open = new Queue<Point>();
            open.Enqueue(new Point(tile.X, tile.Y));

            while (open.Count > 0)
            {
                var current = open.Dequeue();
                var x = current.X;
                var y = current.Y;

                CheckFillTile(open, x, y, originalTile, replacementTile);

                if (x > 0)
                    CheckFillTile(open, x - 1, y, originalTile, replacementTile);
                if (x < Width - 1)
                    CheckFillTile(open, x + 1, y, originalTile, replacementTile);
                if (y > 0)
                    CheckFillTile(open, x, y - 1, originalTile, replacementTile);
                if (y < Height - 1)
                    CheckFillTile(open, x, y + 1, originalTile, replacementTile);
            }

            SelectedLayer.Tiles = _tileMap;

            foreach (var tmp in SelectedLayer.Tiles)
            {
                tmp.PropertyChanged += Tile_PropertyChanged;
            }

            foreach (var point in _tilesToCache)
            {
                CacheTile(SelectedLayer, SelectedLayer.Tiles[point.X, point.Y]);
            }
        }

        private void CheckFillTile(Queue<Point> open, int x, int y, FillTile originalTile, FillTile replacementTile)
        {
            if (_tileChecked[x, y]) return;
            var currTile = GetFillTile(x, y, replacementTile);
            if (CompareTile(currTile, originalTile))
            {
                currTile.Tileset = replacementTile.Tileset;
                currTile.SrcX = replacementTile.X;
                currTile.SrcY = replacementTile.Y;
                currTile.X = x;
                currTile.Y = y;
                currTile.Terrain = replacementTile.Terrain;

                _tilesToCache.Add(new Point(x, y));

                _tileChecked[x, y] = true;
                open.Enqueue(new Point(x, y));
            }
        }

        private EditorTile GetFillTile(int x, int y, FillTile fillTile)
        {
            var tile = _tileMap[x, y] ?? new EditorTile(fillTile.Tileset, x, y, fillTile.X, fillTile.Y, 0);
            return tile;
        }

        public bool CompareTile(EditorTile targetTile, FillTile replacementTile)
        {
            return targetTile.Tileset == replacementTile.Tileset && targetTile.SrcX == replacementTile.X && targetTile.SrcY == replacementTile.Y && targetTile.Terrain == replacementTile.Terrain;
        }

        #endregion

        #region SFML Region

        /// <summary>
        /// Renders the map to a <see cref="RenderWindow"/> object.
        /// </summary>
        /// <param name="rw"><see cref="RenderWindow"/> object to render to.</param>
        /// <param name="Camera">Doesn't render anything outside of this rect.</param>
        public void RenderMap(RenderWindow rw, IntRect Camera)
        {
            Console.WriteLine(@"Rendering map {0}", Name);
            var left = (Camera.Left / TileWidth) - 1;
            var top = (Camera.Top / TileHeight) - 1;

            if (left < 0) left = 0;
            if (top < 0) top = 0;

            var right = left + (Camera.Width / TileWidth) + 1;
            var bottom = top + (Camera.Height / TileHeight) + 1;

            if (right > Width) right = Width;
            if (bottom > Height) bottom = Height;

            foreach (var layer in Layers)
            {
                if (!layer.Visible) continue;

                var sprites = layer.Sprites;

                for (var x = left; x < right; x++)
                {
                    for (var y = top; y < bottom; y++)
                    {
                        if (x > sprites.GetLength(0) - 1 || y > sprites.GetLength(1) - 1) continue;
                        if (sprites[x, y] != null)
                            rw.Draw(sprites[x, y]);
                    }
                }

                var terrain = layer.TerrainCache;

                for (var x = left; x < right; x++)
                {
                    for (var y = top; y < bottom; y++)
                    {
                        if (x > terrain.GetLength(0) - 1 || y > terrain.GetLength(1) - 1) continue;
                        if (terrain[x, y] == null) continue;

                        terrain[x, y].Render(rw);
                    }
                }
            }
        }

        #endregion

        #region Serialization Region

        public bool Close()
        {
            if (!UnsavedChanges)
            {
                UndoRedoArea.ClearHistory();
                return true;
            }

            DialogResult result = MessageBox.Show(@"Any unsaved changes will be lost. Save changes to " + Name + @"?", @"Close Map", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Cancel)
                return false;

            if (result == DialogResult.Yes)
            {
                using (UndoRedoManager.Start("Closing map"))
                {
                    SaveToConcrete();
                    UndoRedoManager.Commit();
                }
                SaveToXml(Path.Combine(ProjectManager.Instance.Project.MapPath, Name + @".xml"));
                UndoRedoArea.ClearHistory();
                return true;
            }

            if (result == DialogResult.No)
            {
                using (UndoRedoManager.Start("Closing map"))
                {
                    RevertToConcrete();
                    UndoRedoManager.Commit();
                }
                SaveToXml(Path.Combine(ProjectManager.Instance.Project.MapPath, Name + @".xml"));
                UndoRedoArea.ClearHistory();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Converts the 2D Tile array in to an enumerable Tile List.
        /// Saves the <see cref="EditorTileMap"/> object to XML.
        /// </summary>
        /// <param name="path">Path to save to.</param>
        public void SaveToXml(string path)
        {
            var map = new TileMap
            {
                ID = ID,
                Name = Name,
                Width = ConcreteWidth,
                Height = ConcreteHeight,
                TileWidth = TileWidth,
                TileHeight = TileHeight,
                Layers = ConcreteLayers
            };

            map.SaveToXml(path);
            UnsavedChanges = false;
        }

        private void BuildFromTileMap(TileMap map)
        {
            using (UndoRedoManager.Start("Initializing map " + map.Name))
            {
                _building = true;
                _dontCache = true;

                ID = map.ID;
                Name = map.Name;
                Width = map.Width;
                Height = map.Height;
                TileWidth = map.TileWidth;
                TileHeight = map.TileHeight;

                ConcreteWidth = Width;
                ConcreteHeight = Height;
                ConcreteLayers = map.Layers;
                RevertToConcrete();

                _building = false;
                _dontCache = false;

                UndoRedoManager.Commit();
            }
        }

        public EditorTileMap Clone()
        {
            using (UndoRedoManager.Start("Cloning map"))
            {
                var newMap = new EditorTileMap
                {
                    ID = ID,
                    Name = Name,
                    Width = Width,
                    Height = Height,
                    TileWidth = TileWidth,
                    TileHeight = TileHeight
                };

                return newMap;
            }
        }

        public void RevertToConcrete()
        {
            _building = true;
            _dontCache = true;

            Layers = new UndoRedoList<EditorTileLayer>();

            Width = ConcreteWidth;
            Height = ConcreteHeight;

            foreach (var layer in ConcreteLayers)
            {
                var newLayer = new EditorTileLayer(layer.Name, layer.Opacity, layer.Visible)
                {
                    Tiles = new EditorTile[Width,Height],
                    Sprites = new Sprite[Width,Height],
                    TerrainCache = new TerrainCache[Width,Height]
                };

                for (var x = 0; x < Width; x++)
                {
                    for (var y = 0; y < Height; y++)
                    {
                        newLayer.Tiles[x, y] = new EditorTile(x, y, 0, 0, 0, 0, newLayer);
                        newLayer.Tiles[x, y].PropertyChanged += Tile_PropertyChanged;
                    }
                }

                foreach (var tile in layer.Tiles)
                {
                    var newTile = newLayer.Tiles[tile.X, tile.Y];
                    newTile.X = tile.X;
                    newTile.Y = tile.Y;
                    newTile.Tileset = tile.Tileset;
                    newTile.SrcX = tile.SrcX;
                    newTile.SrcY = tile.SrcY;
                    newTile.Terrain = tile.Terrain;
                }

                Layers.Add(newLayer);
            }

            _building = false;
            _dontCache = false;

            CacheAllTiles();
        }

        public void SaveToConcrete()
        {
            ConcreteLayers = new List<TileLayer>();

            ConcreteWidth = Width;
            ConcreteHeight = Height;

            foreach (var layer in Layers)
            {
                var newLayer = new TileLayer(layer.Name, layer.Opacity, layer.Visible);

                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (x > layer.Tiles.GetLength(0) - 1 || y > layer.Tiles.GetLength(1) - 1) continue;
                        var tile = layer.Tiles[x, y];
                        if (tile == null) continue;

                        if (!(tile.Tileset == 0 && tile.SrcX == 0 && tile.SrcY == 0 && tile.Terrain == 0))
                        {
                            newLayer.Tiles.Add(new Tile(tile.X, tile.Y, tile.Tileset, tile.SrcX, tile.SrcY, tile.Terrain));
                        }
                    }
                }

                ConcreteLayers.Add(newLayer);
            }
        }

        #endregion

        #region Undo/Redo Events

        private void HookEvents()
        {
            _width.Changed += delegate { OnPropertyChanged("Width"); };
            _height.Changed += delegate { OnPropertyChanged("Height"); };
            _tileWidth.Changed += delegate { OnPropertyChanged("TileWidth"); };
            _tileHeight.Changed += delegate { OnPropertyChanged("TileHeight"); };
        }

        #endregion
    }
}