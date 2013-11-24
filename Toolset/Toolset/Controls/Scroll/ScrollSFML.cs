using System;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalLib.ResourceEngine;
using SFML.Graphics;
using SFML.Window;
using View = SFML.Graphics.View;

namespace Toolset.Controls.Scroll
{
    public partial class ScrollSFML : ScrollBlank
    {
        #region Property Region

        [CategoryAttribute("Global Settings"),
        ReadOnlyAttribute(false),
        DefaultValueAttribute(true)]
        public bool CheckeredBackground { get; set; }

        public RenderWindow RenderWindow { get; set; }
        public View View { get; set; }
        public IntRect Camera { get; set; }

        private Texture _checkeredTexture;
        private Sprite _checkeredSprite;
        
        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollSFML"/> control.
        /// </summary>
        public ScrollSFML()
        {
            InitializeComponent();

            PropertyChanged += ScrollableSFML_PropertyChanged;
            Load += ScrollableSFML_Load;
            Resize += ScrollableSFML_Resize;
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
                if (RenderWindow != null) RenderWindow.Dispose();
                if (View != null) View.Dispose();
                if (_checkeredTexture != null) _checkeredTexture.Dispose();
                if (_checkeredSprite != null) _checkeredSprite.Dispose();

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the PropertyChanged event of the <see cref="ScrollSFML"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ScrollableSFML_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "XOffset" || e.PropertyName == "YOffset")
                RefreshView();

            if (e.PropertyName == "ObjectWidth" || e.PropertyName == "ObjectHeight")
            {
                if (CheckeredBackground)
                {
                    if (_checkeredSprite == null) SetCheckered();
                    _checkeredSprite.TextureRect = new IntRect(0, 0, ObjectWidth, ObjectHeight);
                    Render();
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the <see cref="ScrollSFML"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableSFML_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            if (RenderWindow != null) return;
            RenderWindow = new RenderWindow(Handle);

            SetView();

            SetCheckered();

            Render();
        }
        
        /// <summary>
        /// Handles the Resize event of the <see cref="ScrollSFML"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollableSFML_Resize(object sender, EventArgs e)
        {
            if (RenderWindow != null)
            {
                SetView();
                RefreshView();
            }
        }

        /// <summary>
        /// Overrides the OnPaint event of the <see cref="ScrollSFML"/> control.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Render();
        }

        #endregion

        #region SFML Region

        /// <summary>
        /// Builds the checkered background texture & sprite.
        /// </summary>
        private void SetCheckered()
        {
            if (CheckeredBackground)
            {
                if (_checkeredTexture == null || _checkeredSprite == null)
                {
                    _checkeredTexture = ResourceManager.Instance.LoadTexture(Application.StartupPath + @"\content\textures\transparency.png");
                    _checkeredTexture.Repeated = true;
                    _checkeredSprite = new Sprite(_checkeredTexture);
                    _checkeredSprite.TextureRect = new IntRect(0, 0, 0, 0);
                }
            }
        }

        /// <summary>
        /// Creates the <see cref="View"/> object and assigns it to the <see cref="RenderWindow"/> object.
        /// </summary>
        public void SetView()
        {
            View = new View(new FloatRect((XOffset * -1), (YOffset * -1), ClientSize.Width, ClientSize.Height));
            RenderWindow.SetView(View);
            SetCamera();
        }

        /// <summary>
        /// Re-positions the <see cref="View"/> and re-renders the scene.
        /// </summary>
        public void RefreshView()
        {
            if (RenderWindow == null) return;
            if (View == null) return;

            View.Center = new Vector2f(XOffset + ((int)View.Size.X / 2), YOffset + ((int)View.Size.Y / 2));
            RenderWindow.SetView(View);
            SetCamera();
            Render();
        }

        /// <summary>
        /// Builds the Camera object.
        /// </summary>
        public void SetCamera()
        {
            var left = (int)View.Center.X - ((int)View.Size.X / 2);
            var top = (int)View.Center.Y - ((int)View.Size.Y / 2);
            var right = left + (int)View.Size.X;
            var bottom = top + (int)View.Size.Y;

            Camera = new IntRect(left, top, left + right, top + bottom);
        }

        /// <summary>
        /// Renders the scene.
        /// </summary>
        public void Render()
        {
            if (RenderWindow == null) return;

            if (IsDisposed) return;

            CheckHandle();
            BeginDraw();
            DrawTransparency();
            Draw();
            EndDraw();
        }

        /// <summary>
        /// Clears the <see cref="RenderWindow"/> object.
        /// </summary>
        private void BeginDraw()
        {
            RenderWindow.Clear(new Color(191, 191, 191));
        }

        /// <summary>
        /// Draws the <see cref="_checkeredSprite"/> object.
        /// </summary>
        private void DrawTransparency()
        {
            if (RenderWindow == null) return;

            if (_checkeredSprite != null)
                RenderWindow.Draw(_checkeredSprite);
        }

        /// <summary>
        /// When overridden allows the derived class to draw to the <see cref="RenderWindow"/> object.
        /// </summary>
        public virtual void Draw()
        {

        }

        /// <summary>
        /// Displays the <see cref="RenderWindow"/> object.
        /// </summary>
        private void EndDraw()
        {
            RenderWindow.Display();
        }

        /// <summary>
        /// Checks to see if the <see cref="Handle"/> property matches that of the <see cref="RenderWindow"/> object.
        /// If they differ it'll re-create the <see cref="RenderWindow"/> object.
        /// </summary>
        private void CheckHandle()
        {
            if (RenderWindow.SystemHandle == Handle)
                return;

            if (RenderWindow != null)
                RenderWindow = null;

            RenderWindow = new RenderWindow(Handle);
        }

        #endregion
    }
}