using System.ComponentModel;
using CrystalLib.ResourceEngine;
using SFML.Graphics;
using SFML.Window;

namespace Toolset.Controls.Scroll
{
    public partial class ScrollTexture : ScrollSFML
    {
        #region Property Region

        private Texture _texture;

        /// <summary>
        /// <see cref="SFML.Graphics.Texture"/> object used by the <see cref="Sprite"/> object.
        /// </summary>
        public Texture Texture
        {
            get { return _texture; }
            set
            {
                if (value == _texture) return;
                _texture = value;
                OnPropertyChanged("Texture");
            }
        }

        private Sprite _sprite;

        /// <summary>
        /// <see cref="SFML.Graphics.Sprite"/> object rendered to the <see cref="SFML.Window.Window"/> object.
        /// </summary>
        public Sprite Sprite
        {
            get { return _sprite; }
            set
            {
                if (value == _sprite) return;
                _sprite = value;
                OnPropertyChanged("Sprite");
            }
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollTexture"/> control.
        /// </summary>
        public ScrollTexture()
        {
            InitializeComponent();

            PropertyChanged += ScrollableTexture_PropertyChanged;
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
                if (Texture != null) Texture.Dispose();
                if (Sprite != null) Sprite.Dispose();

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the PropertyChanged event of the <see cref="ScrollTexture"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ScrollableTexture_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Texture")
            {
                if (Texture != null)
                {
                    Sprite = new Sprite(Texture)
                    {
                        TextureRect = new IntRect(0, 0, (int)Texture.Size.X, (int)Texture.Size.Y),
                        Position = new Vector2f(0, 0)
                    };
                    ObjectWidth = (int)Texture.Size.X;
                    ObjectHeight = (int)Texture.Size.Y;
                    XOffset = 0;
                    YOffset = 0;
                }
                else
                {
                    Sprite = null;
                    ObjectWidth = 0;
                    ObjectHeight = 0;
                    XOffset = 0;
                    YOffset = 0;
                }
                Render();
            }

            /*if (e.PropertyName == "XOffset" || e.PropertyName == "YOffset")
            {
                Render();
            }*/
        }

        #endregion

        #region Method Region

        /// <summary>
        /// Loads an image file in to the <see cref="Texture"/> object.
        /// </summary>
        /// <param name="path">Path to a valid image file.</param>
        public void LoadTexture(string path)
        {
            Texture = ResourceManager.Instance.LoadTexture(path);
        }

        #endregion

        #region SFML Region

        /// <summary>
        /// Draws the <see cref="Sprite"/> object to the <see cref="RenderWindow"/> object.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            if (Sprite != null)
                RenderWindow.Draw(Sprite);
        }

        #endregion
    }
}