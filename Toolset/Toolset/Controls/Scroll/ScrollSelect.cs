using System;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalLib.ResourceEngine;
using SFML.Graphics;
using SFML.Window;

namespace Toolset.Controls.Scroll
{
    public partial class ScrollSelect : ScrollTexture
    {
        #region Field Region

        private Texture _hlTexture;
        private Sprite _hlSprite;

        #endregion

        #region Property Region

        private int _tileWidth;
        public int TileWidth
        {
            get { return _tileWidth; }
            set
            {
                if (value == _tileWidth) return;
                _tileWidth = value;
                OnPropertyChanged("TileWidth");
            }
        }

        private int _tileHeight;
        public int TileHeight
        {
            get { return _tileHeight; }
            set
            {
                if (value == _tileHeight) return;
                _tileHeight = value;
                OnPropertyChanged("TileHeight");
            }
        }

        private int _selectionWidth;
        public int SelectionWidth
        {
            get { return _selectionWidth; }
            set
            {
                if (value == _selectionWidth) return;
                _selectionWidth = value;
                OnPropertyChanged("SelectionWidth");
            }
        }

        private int _selectionHeight;
        public int SelectionHeight
        {
            get { return _selectionHeight; }
            set
            {
                if (value == _selectionHeight) return;
                _selectionHeight = value;
                OnPropertyChanged("SelectionHeight");
            }
        }

        private int _selectionX;
        public int SelectionX
        {
            get { return _selectionX; }
            set
            {
                if (value == _selectionX) return;
                _selectionX = value;
                OnPropertyChanged("SelectionX");
            }
        }

        private int _selectionY;
        public int SelectionY
        {
            get { return _selectionY; }
            set
            {
                if (value == _selectionY) return;
                _selectionY = value;
                OnPropertyChanged("SelectionY");
            }
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollTileset"/> object.
        /// </summary>
        public ScrollSelect()
        {
            InitializeComponent();

            CheckeredBackground = true;

            Load += ScrollSelect_Load;

            MouseDown += ScrollSelect_MouseDown;
            MouseMove += ScrollSelect_MouseMove;

            PropertyChanged += ScrollSelect_PropertyChanged;
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
                if (_hlTexture != null) _hlTexture.Dispose();
                if (_hlSprite != null) _hlSprite.Dispose();

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
        private void ScrollSelect_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectionX" || e.PropertyName == "SelectionY" || e.PropertyName == "TileWidth" || e.PropertyName == "TileHeight"
                || e.PropertyName == "SelectionWidth" || e.PropertyName == "SelectionHeight")
            {
                UpdateSprite();
            }
        }

        private void ScrollSelect_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            _hlTexture = ResourceManager.Instance.LoadTexture(Application.StartupPath + @"\content\textures\highlight.png");
            _hlTexture.Repeated = true;
            _hlSprite = new Sprite(_hlTexture);
            UpdateSprite();
        }

        /// <summary>
        /// Handles the MouseDown event of the <see cref="ScrollTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollSelect_MouseDown(object sender, MouseEventArgs e)
        {
            SelectionX = MouseXOffset / TileWidth;
            SelectionY = MouseYOffset / TileHeight;
        }

        /// <summary>
        /// Handles the MouseMove event of the <see cref="ScrollTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollSelect_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SelectionX = MouseXOffset / TileWidth;
                SelectionY = MouseYOffset / TileHeight;
            }
        }

        #endregion

        #region Method Region

        public void EnsureVisible()
        {
            var xMax = (Width - ObjectWidth - 17) * -1;
            var yMax = (Height - ObjectHeight - 17) * -1;

            var right = (SelectionX * TileWidth) + SelectionWidth;
            var bottom = (SelectionY * TileHeight) + SelectionHeight;

            var xoffset = 0;
            var yoffset = 0;

            if (right > Width - 17)
                xoffset = right - Width + 50 + 17;
            if (bottom > Height - 17)
                yoffset = bottom - Height + 50 + 17;

            if (xoffset > xMax) xoffset = xMax;
            if (yoffset > yMax) yoffset = yMax;

            if (xoffset < 0) xoffset = 0;
            if (yoffset < 0) yoffset = 0;

            XOffset = xoffset;
            YOffset = yoffset;
        }

        #endregion

        #region SFML Region

        private void UpdateSprite()
        {
            if (_hlSprite == null) return;

            _hlSprite.TextureRect = new IntRect(0, 0, SelectionWidth, SelectionHeight);
            _hlSprite.Position = new Vector2f(SelectionX * TileWidth, SelectionY * TileHeight);
            Render();
        }

        /// <summary>
        /// Draws the <see cref="Sprite"/> object to the <see cref="RenderWindow"/> object.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            if (_hlSprite != null)
                RenderWindow.Draw(_hlSprite);
        }

        #endregion
    }
}