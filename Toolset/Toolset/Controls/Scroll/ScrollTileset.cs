using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CrystalLib.Project;
using CrystalLib.ResourceEngine;
using CrystalLib.TileEngine;
using SFML.Graphics;
using SFML.Window;
using Toolset.Managers;

namespace Toolset.Controls.Scroll
{
    public partial class ScrollTileset : ScrollTexture
    {
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

        public Tileset Tileset;

        private Texture _tileTexture;
        private Sprite _tileSprite;

        private Texture _highlightTexture;
        private List<Sprite> _highlightSprites = new List<Sprite>();

        private bool _isDrag;

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollTileset"/> object.
        /// </summary>
        public ScrollTileset()
        {
            InitializeComponent();

            CheckeredBackground = true;

            TilesetManager.Instance.SelectionChanged += SelectionChanged;
            TilesetManager.Instance.TilesetSelected += TilesetSelected;

            Load += ScrollableTileset_Load;
            MouseDown += ScrollableTileset_MouseDown;
            MouseMove += ScrollableTileset_MouseMove;
            MouseUp += ScrollableTileset_MouseUp;

            PropertyChanged += ScrollableTileset_PropertyChanged;
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
                if (Tileset != null) Tileset = null;
                if (_tileTexture != null) _tileTexture.Dispose();
                if (_tileSprite != null) _tileSprite.Dispose();
                if (_highlightTexture != null) _highlightTexture.Dispose();
                foreach (var sprite in _highlightSprites)
                {
                    if (sprite != null) sprite.Dispose();
                }
                if (_highlightSprites != null) _highlightSprites = null;

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the PropertyChanged event of the <see cref="ScrollTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ScrollableTileset_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ObjectWidth" || e.PropertyName == "ObjectHeight")
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

                TilesetManager.Instance.SetSelectionObjectSize(ObjectWidth, ObjectHeight);
            }

            if (e.PropertyName == "TileGrid")
            {
                Render();
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _highlightSprites.Clear();

            var tileWidth = 32;
            var tileHeight = 32;

            if (Tileset != null)
            {
                tileWidth = Tileset.TileWidth;
                tileHeight = Tileset.TileHeight;
            }

            foreach (var point in e.Selection.Points)
            {
                _highlightSprites.Add(new Sprite
                {
                    Texture = _highlightTexture,
                    TextureRect = new IntRect(0, 0, tileWidth, tileHeight),
                    Position = new Vector2f(point.X * tileWidth, point.Y * tileHeight)
                });
            }

            Render();
        }

        /// <summary>
        /// Handles the TilesetSelected event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetChangedEventArgs"/> instance containing the event data.</param>
        private void TilesetSelected(object sender, TilesetSelectedEventArgs e)
        {
            if (e.Tileset == null) return;
            Tileset = e.Tileset;
            TilesetManager.Instance.SetSelectionTileSize(e.Tileset.TileWidth, e.Tileset.TileHeight);
            LoadTexture(e.Tileset.Image);
        }

        /// <summary>
        /// Handles the Load event of the <see cref="ScrollTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableTileset_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _highlightTexture = ResourceManager.Instance.LoadTexture(Application.StartupPath + @"\content\textures\highlight.png");
            _highlightTexture.Repeated = true;

            _tileTexture = ResourceManager.Instance.LoadTexture(Application.StartupPath + @"\content\textures\grid.png");
            _tileTexture.Repeated = true;
        }

        /// <summary>
        /// Handles the MouseDown event of the <see cref="ScrollTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableTileset_MouseDown(object sender, MouseEventArgs e)
        {
            if (Tileset == null) return;

            if (e.Button != MouseButtons.Left) return;

            _isDrag = true;

            var tileWidth = Tileset.TileWidth;
            var tileHeight = Tileset.TileHeight;

            if (ModifierKeys == Keys.Control)
            {
                TilesetManager.Instance.ToggleSelection(new Point(MouseXOffset / tileWidth, MouseYOffset / tileHeight));
                return;
            }

            TilesetManager.Instance.SetSelection(new Point(MouseXOffset / tileWidth, MouseYOffset / tileHeight), new Point((MouseXOffset / tileWidth), (MouseYOffset / tileHeight)));
        }

        /// <summary>
        /// Handles the MouseMove event of the <see cref="ScrollTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableTileset_MouseMove(object sender, MouseEventArgs e)
        {
            if (Tileset == null) return;

            if (e.Button != MouseButtons.Left) return;
            if (ModifierKeys == Keys.Control) return;
            if (!_isDrag) return;

            var tileWidth = Tileset.TileWidth;
            var tileHeight = Tileset.TileHeight;

            TilesetManager.Instance.SetSelection(new Point((MouseXOffset / tileWidth), (MouseYOffset / tileHeight)));
        }

        /// <summary>
        /// Handles the MouseUp event of the <see cref="ScrollTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableTileset_MouseUp(object sender, MouseEventArgs e)
        {
            _isDrag = false;
        }

        #endregion

        #region SFML Region

        /// <summary>
        /// Draws the <see cref="Sprite"/> object to the <see cref="RenderWindow"/> object.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            if (TileGrid)
            {
                if (_tileSprite != null)
                    RenderWindow.Draw(_tileSprite);
            }

            foreach (var sprite in _highlightSprites.Where(sprite => sprite != null))
            {
                RenderWindow.Draw(sprite);
            }
        }

        #endregion
    }
}