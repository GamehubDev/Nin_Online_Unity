using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CrystalLib.ResourceEngine;
using CrystalLib.TileEngine;
using CrystalLib.Toolset.UndoRedo;
using SFML.Graphics;
using SFML.Window;
using Toolset.Managers;
using Toolset.TileEngine;

namespace Toolset.Controls.Scroll
{
    public partial class ScrollMap : ScrollSFML
    {
        #region Field Region

        private Tileset _tileset;
        private Texture _tilesetTexture;
        private List<Sprite> _tilesetSprites;
        private bool _showSelection;
        private PointSelection _selection;

        private Texture _tileTexture;
        private Sprite _tileSprite;

        private Texture _highlightTexture;
        private Sprite _highlightSprite;
        private List<Sprite> _highlightSprites;

        private bool _isDrag;
        private int _lastTileX;
        private int _lastTileY;

        #endregion

        #region Property Region

        [Category("Global Settings"),
        ReadOnlyAttribute(false),
        DefaultValueAttribute(true)]
        private bool _tileGrid;

        /// <summary>
        /// Flag to show the tile grid graphic or not.
        /// </summary>
        public bool TileGrid
        {
            get { return _tileGrid; }
            set
            {
                if (_tileGrid == value) return;
                _tileGrid = value;
                OnPropertyChanged("TileGrid");
            }
        }

        private EditorTileMap _map;

        /// <summary>
        /// <see cref="EditorTileMap"/> object controlled by the <see cref="ScrollMap"/> control.
        /// </summary>
        public EditorTileMap Map
        {
            get { return _map; }
            set
            {
                if (value == _map) return;
                _map = value;
                OnPropertyChanged("Map");
            }
        }

        private int _tileX;

        /// <summary>
        /// MouseX / TileWidth.
        /// </summary>
        public int TileX
        {
            get { return _tileX; }
            set
            {
                if (value == _tileX) return;
                _tileX = value;
                OnPropertyChanged("TileX");
            }
        }

        private int _tileY;

        /// <summary>
        /// MouseY / TileWidth.
        /// </summary>
        public int TileY
        {
            get { return _tileY; }
            set
            {
                if (value == _tileY) return;
                _tileY = value;
                OnPropertyChanged("TileY");
            }
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollMap"/> control.
        /// </summary>
        public ScrollMap()
        {
            InitializeComponent();

            CheckeredBackground = true;

            Load += ScrollableMap_Load;
            MouseEnter += ScrollableMap_MouseEnter;
            MouseLeave += ScrollableMap_MouseLeave;
            MouseDown += ScrollableMap_MouseDown;
            MouseMove += ScrollableMap_MouseMove;
            MouseUp += ScrollableMap_MouseUp;

            PropertyChanged += ScrollableMap_PropertyChanged;

            MapManager.Instance.MapChanged += MapChanged;

            TilesetManager.Instance.TilesetSelected += TilesetSelected;
            TilesetManager.Instance.SelectionChanged += SelectionChanged;
        }

        #endregion

        #region Dispose Region

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (Map != null) Map = null;

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Event Handler Region

        private void UndoRedoCommandDone(object sender, CommandDoneEventArgs e)
        {
            if (e.CommandDoneType == CommandDoneType.Redo || e.CommandDoneType == CommandDoneType.Undo)
                Render();
        }

        /// <summary>
        /// Handles the PropertyChanged event of the <see cref="ScrollMap"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ScrollableMap_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ObjectWidth" || e.PropertyName == "ObjectHeight")
            {
                BuildTileSprite();
                Render();
            }

            if (e.PropertyName == "MouseXOffset" || e.PropertyName == "MouseYOffset")
            {
                if (Map != null)
                {
                    TileX = MouseXOffset / Map.TileWidth;
                    TileY = MouseYOffset / Map.TileHeight;
                }
            }

            if (e.PropertyName == "TileGrid")
            {
                BuildTileSprite();
                Render();
            }

            if (e.PropertyName == "TileX" || e.PropertyName == "TileY")
            {
                if (Map != null)
                {
                    if (_highlightSprite != null)
                    {
                        _highlightSprite.Position = new Vector2f(TileX * Map.TileWidth, TileY * Map.TileHeight);
                        if (TileX > _map.Width - 1 || TileY > _map.Height - 1)
                            _highlightSprite.Color = new Color(255, 255, 255, 0);
                        else
                            _highlightSprite.Color = new Color(255, 255, 255, 255);
                    }
                }

                BuildTilesetSprites();
            }

            if (e.PropertyName == "Map")
            {
                if (_map != null)
                    _map.SizeChanged += MapSizeChanged;
            }
        }

        /// <summary>
        /// Handles the Load event of the <see cref="ScrollMap"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableMap_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _highlightTexture = ResourceManager.Instance.LoadTexture(Application.StartupPath + @"\content\textures\highlight.png");
            _highlightTexture.Repeated = true;
            _highlightSprite = new Sprite
            {
                Texture = _highlightTexture,
                TextureRect = new IntRect(0, 0, 32, 32),
                Position = new Vector2f(0, 0)
            };

            _tileTexture = ResourceManager.Instance.LoadTexture(Application.StartupPath + @"\content\textures\grid.png");
            _tileTexture.Repeated = true;

            BuildTileSprite();
        }

        /// <summary>
        /// Handles the MouseEnter event of the <see cref="ScrollMap"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableMap_MouseEnter(object sender, EventArgs e)
        {
            _showSelection = true;
            Render();
        }

        /// <summary>
        /// Handles the MouseLeave event of the <see cref="ScrollMap"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableMap_MouseLeave(object sender, EventArgs e)
        {
            _showSelection = false;
            Render();
        }

        /// <summary>
        /// Handles the MouseDown event of the <see cref="ScrollMap"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (_selection == null) return;
            if (_tileset == null) return;
            if (_map == null) return;
            if (_map.SelectedLayer == null) return;

            _isDrag = false;
            if (_map.UndoRedoArea.IsCommandStarted)
                _map.UndoRedoArea.Commit();

            if (e.Button == MouseButtons.Left && MapManager.Instance.SelectedTool == EditorTool.Brush)
            {
                _isDrag = true;

                _map.UndoRedoArea.Start("Brush Tool");
                _map.SetTiles(_tileset, _map.SelectedLayer, _selection, TileX, TileY);
                Render();
            }
            if (e.Button == MouseButtons.Left && MapManager.Instance.SelectedTool == EditorTool.Terrain)
            {
                _isDrag = true;

                _map.UndoRedoArea.Start("Terrain Tool");
                _map.SetTerrain(_map.SelectedLayer, TerrainManager.Instance.SelectedTerrain, TileX, TileY);
                Render();
            }
            if (((MapManager.Instance.SelectedTool == EditorTool.Brush || MapManager.Instance.SelectedTool == EditorTool.Terrain) && e.Button == MouseButtons.Right) || 
                MapManager.Instance.SelectedTool == EditorTool.Eraser)
            {
                _isDrag = true;

                _map.UndoRedoArea.Start("Eraser Tool");
                _map.RemoveTile(_map.SelectedLayer, TileX, TileY);
                Render();
            }
            if (MapManager.Instance.SelectedTool == EditorTool.Fill)
            {
                Map.Fill(_tileset, _selection, TileX, TileY);
                Render();
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the <see cref="ScrollMap"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selection == null) return;
            if (_tileset == null) return;
            if (_map == null) return;
            if (_map.SelectedLayer == null) return;

            if (e.Button == MouseButtons.Left && MapManager.Instance.SelectedTool == EditorTool.Brush)
            {
                if (!_isDrag) return;
                if (_lastTileX == TileX && _lastTileY == TileY) return;
                _lastTileX = TileX;
                _lastTileY = TileY;

                _map.SetTiles(_tileset, _map.SelectedLayer, _selection, TileX, TileY);
                Render();
            }
            if (e.Button == MouseButtons.Left && MapManager.Instance.SelectedTool == EditorTool.Terrain)
            {
                if (!_isDrag) return;
                if (_lastTileX == TileX && _lastTileY == TileY) return;
                _lastTileX = TileX;
                _lastTileY = TileY;

                _map.SetTerrain(_map.SelectedLayer, TerrainManager.Instance.SelectedTerrain, TileX, TileY);
                Render();
            }
            if (((MapManager.Instance.SelectedTool == EditorTool.Brush || MapManager.Instance.SelectedTool == EditorTool.Terrain) && e.Button == MouseButtons.Right) || 
                MapManager.Instance.SelectedTool == EditorTool.Eraser)
            {
                if (!_isDrag) return;
                if (_lastTileX == TileX && _lastTileY == TileY) return;
                _lastTileX = TileX;
                _lastTileY = TileY;

                _map.RemoveTile(_map.SelectedLayer, TileX, TileY);
                Render();
            }
            if (MapManager.Instance.SelectedTool == EditorTool.Fill)
            {
                if (_lastTileX == TileX && _lastTileY == TileY) return;
                _lastTileX = TileX;
                _lastTileY = TileY;

                var points = Map.FillCache(_tileset, _selection, TileX, TileY);
                _highlightSprites = new List<Sprite>();

                if (points != null)
                {
                    foreach (var point in points)
                    {
                        _highlightSprites.Add(new Sprite
                        {
                            Texture = _highlightTexture,
                            TextureRect = new IntRect(0, 0, Map.TileWidth, Map.TileHeight),
                            Position = new Vector2f(point.X * Map.TileWidth, point.Y * Map.TileHeight)
                        });
                    }
                }
                Render();
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the <see cref="ScrollMap"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableMap_MouseUp(object sender, MouseEventArgs e)
        {
            _isDrag = false;
            if (_map.UndoRedoArea.IsCommandStarted)
                _map.UndoRedoArea.Commit();
        }

        /// <summary>
        /// Handles the MapChanged event of the <see cref="MapManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapChangedEventArgs"/> instance containing the event data.</param>
        private void MapChanged(object sender, MapChangedEventArgs e)
        {
            if (Map == null) return;
            if (e.NewMap.Name != Map.Name) return;

            if (ObjectWidth != (e.NewMap.Width * e.NewMap.TileWidth))
                ObjectWidth = (e.NewMap.Width * e.NewMap.TileWidth);

            if (ObjectHeight != (e.NewMap.Height * e.NewMap.TileHeight))
                ObjectHeight = (e.NewMap.Height * e.NewMap.TileHeight);

            Render();
        }

        /// <summary>
        /// Handles the SizeChanged event of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MapSizeChanged(object sender, EventArgs e)
        {
            if (Map == null) return;

            if (ObjectWidth != (Map.Width * Map.TileWidth))
                ObjectWidth = (Map.Width * Map.TileWidth);

            if (ObjectHeight != (Map.Height * Map.TileHeight))
                ObjectHeight = (Map.Height * Map.TileHeight);

            Render();
        }

        /// <summary>
        /// Handles the TilesetSelected event of the <see cref="TilesetManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetSelectedEventArgs"/> instance containing the event data.</param>
        private void TilesetSelected(object sender, TilesetSelectedEventArgs e)
        {
            if (e.Tileset == null) return;

            _tileset = e.Tileset;

            _tilesetTexture = ResourceManager.Instance.LoadTexture(_tileset.Image);
            BuildTilesetSprites();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the <see cref="TilesetManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selection = e.Selection;
            BuildTilesetSprites();
        }

        /// <summary>
        /// Handles the LayerAdded event of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerAddedEventArgs"/> instance containing the event data.</param>
        private void LayerAdded(object sender, LayerAddedEventArgs e)
        {
            Render();
        }

        /// <summary>
        /// Handles the LayerChanged event of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerChangedEventArgs"/> instance containing the event data.</param>
        private void LayerChanged(object sender, LayerChangedEventArgs e)
        {
            Render();
        }

        /// <summary>
        /// Handles the LayerDeleted event of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerDeletedEventArgs"/> instance containing the event data.</param>
        private void LayerDeleted(object sender, LayerDeletedEventArgs e)
        {
            Render();
        }

        /// <summary>
        /// Handles the LayerOrderChanged event of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerOrderChangedEventArgs"/> instance containing the event data.</param>
        private void LayerOrderChanged(object sender, LayerOrderChangedEventArgs e)
        {
            Render();
        }

        #endregion

        #region Method Region

        /// <summary>
        /// Sets the <see cref="Map"/> object.
        /// </summary>
        /// <param name="map"><see cref="EditorTileMap"/> object to set the <see cref="Map"/> object to.</param>
        public void SetMap(EditorTileMap map)
        {
            if (Map != null)
            {
                Map.LayerAdded -= LayerAdded;
                Map.LayerChanged -= LayerChanged;
                Map.LayerDeleted -= LayerDeleted;
                Map.LayerOrderChanged -= LayerOrderChanged;
                Map.UndoRedoArea.CommandDone -= UndoRedoCommandDone;
            }

            Map = map;

            if (Map != null)
            {
                ObjectWidth = Map.Width * Map.TileWidth;
                ObjectHeight = Map.Height * Map.TileHeight;
                XOffset = 0;
                YOffset = 0;

                Map.LayerAdded += LayerAdded;
                Map.LayerChanged += LayerChanged;
                Map.LayerDeleted += LayerDeleted;
                Map.LayerOrderChanged += LayerOrderChanged;
                Map.UndoRedoArea.CommandDone += UndoRedoCommandDone;

                //Map.CacheAllTiles();
            }
            else
            {
                ObjectWidth = 0;
                ObjectHeight = 0;
                XOffset = 0;
                YOffset = 0;
            }
        }

        #endregion

        #region SFML Region

        /// <summary>
        /// Builds the <see cref="_tilesetSprites"/> array based on the <see cref="_selection"/> object.
        /// </summary>
        private void BuildTilesetSprites()
        {
            if (_map == null) return;
            if (_tilesetTexture == null) return;
            if (_tileset == null) return;

            _tilesetSprites = new List<Sprite>();

            if (TileX > _map.Width - 1 || TileY > _map.Height - 1)
            {
                Render();
                return;
            }

            if (_selection == null)
            {
                _tilesetSprites.Add(new Sprite
                {
                    Texture = _tilesetTexture,
                    TextureRect = new IntRect(0, 0, _tileset.TileWidth, _tileset.TileHeight),
                    Position = new Vector2f(TileX * _tileset.TileWidth, TileY * _tileset.TileHeight)
                });
            }
            else
            {
                foreach (var point in _selection.Points)
                {
                    var x = TileX + point.X - _selection.Offset.X;
                    var y = TileY + point.Y - _selection.Offset.Y;

                    if (x > _map.Width - 1 || y > _map.Height - 1) continue;

                    _tilesetSprites.Add(new Sprite
                    {
                        Texture = _tilesetTexture,
                        TextureRect = new IntRect(point.X * _tileset.TileWidth, point.Y * _tileset.TileHeight, _tileset.TileWidth, _tileset.TileHeight),
                        Position = new Vector2f(x * _tileset.TileWidth, y * _tileset.TileHeight),
                        Color = new Color(255, 255, 255, 200)
                    });
                }
            }

            Render();
        }

        /// <summary>
        /// Builds the <see cref="_tileSprite"/> object.
        /// </summary>
        private void BuildTileSprite()
        {
            if (_tileTexture != null)
            {
                _tileSprite = new Sprite
                {
                    Texture = _tileTexture,
                    Position = new Vector2f(0, 0),
                    TextureRect = new IntRect(0, 0, ObjectWidth, ObjectHeight)
                };
            }
        }

        /// <summary>
        /// Draws the map.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            if (_map != null)
                _map.RenderMap(RenderWindow, Camera);

            if (TileGrid)
            {
                if (_tileSprite != null)
                    RenderWindow.Draw(_tileSprite);
            }

            if (_showSelection)
            {
                if (MapManager.Instance.SelectedTool == EditorTool.Brush)
                {
                    if (_tilesetSprites != null)
                    {
                        foreach (var sprite in _tilesetSprites.Where(sprite => sprite != null))
                        {
                            RenderWindow.Draw(sprite);
                        }
                    }
                }
                if (MapManager.Instance.SelectedTool == EditorTool.Eraser || MapManager.Instance.SelectedTool == EditorTool.Terrain)
                {
                    if (_highlightSprite != null)
                        RenderWindow.Draw(_highlightSprite);
                }
                if (MapManager.Instance.SelectedTool == EditorTool.Fill)
                {
                    if (_highlightSprites != null)
                    {
                        foreach (var sprite in _highlightSprites.Where(sprite => sprite != null).Where(sprite => IsInView((int)sprite.Position.X, (int)sprite.Position.Y)))
                        {
                            RenderWindow.Draw(sprite);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a tile is within the view.
        /// </summary>
        /// <param name="x">X co-ordinate of the tile.</param>
        /// <param name="y">Y co-ordinate of the tile.</param>
        /// <returns>True if the tile is in the view, false of not.</returns>
        public bool IsInView(int x, int y)
        {
            var width = Map.TileWidth;
            var height = Map.TileHeight;

            if (x + width >= Camera.Left && x - width <= Camera.Left + Camera.Width)
            {
                if (y + height >= Camera.Top && y - height <= Camera.Top + Camera.Height)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}