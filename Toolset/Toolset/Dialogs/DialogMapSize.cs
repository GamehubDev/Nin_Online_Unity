using System;
using System.Windows.Forms;
using Toolset.Enums;

namespace Toolset.Dialogs
{
    public partial class DialogMapSize : Form
    {
        #region Property Region

        private int oldMapWidth { get; set; }
        private int oldMapHeight { get; set; }

        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        public AnchorPoints AnchorPoint { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogMapSize"/> form.
        /// </summary>
        /// <param name="width">Width of the map.</param>
        /// <param name="height">Height of the map.</param>
        /// <param name="tilewidth">Width of the individual tiles.</param>
        /// <param name="tileheight">Height of the individual tiles.</param>
        public DialogMapSize(int width, int height, int tilewidth, int tileheight)
        {
            InitializeComponent();

            spinWidth.ValueChanged += delegate { CalculateMapSize(); };
            spinHeight.ValueChanged += delegate { CalculateMapSize(); };
            spinTileWidth.ValueChanged += delegate { CalculateMapSize(); };
            spinTileHeight.ValueChanged += delegate { CalculateMapSize(); };

            btnOK.Click += btnOK_Click;

            oldMapWidth = width;
            oldMapHeight = height;

            MapWidth = width;
            MapHeight = height;
            TileWidth = tilewidth;
            TileHeight = tileheight;

            lblWidth.Text = @"Width: " + width;
            lblHeight.Text = @"Height: " + height;
            lblTileWidth.Text = @"Tile Width: " + tilewidth;
            lblTileHeight.Text = @"Tile Height: " + tileheight;

            spinWidth.Value = width;
            spinHeight.Value = height;
            spinTileWidth.Value = tilewidth;
            spinTileHeight.Value = tileheight;

            CalculateCurrentMapSize(width, height, tilewidth, tileheight);
            CalculateMapSize();
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the Click event of the <see cref="btnOK"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                DialogResult = DialogResult.None;
                return;
            }

            MapWidth = (int)spinWidth.Value;
            MapHeight = (int)spinHeight.Value;
            TileWidth = (int)spinTileWidth.Value;
            TileHeight = (int)spinTileHeight.Value;
            AnchorPoint = anchorSelection.AnchorPoint;
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Validates the input areas of the <see cref="DialogMap"/> form.
        /// </summary>
        /// <returns>Returns false if validation fails, true if validation succeedes.</returns>
        private bool ValidateForm()
        {
            if ((MapWidth < oldMapWidth) || (MapHeight < oldMapHeight))
            {
                var result = MessageBox.Show(@"The new map size is smaller than the current map size; some clipping will occur.", @"Edit Map Size", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel) return false;
            }

            return true;
        }

        /// <summary>
        /// Calculates the size of the map in its current state (values passed on initialisation)
        /// </summary>
        /// <param name="width">Width of the map.</param>
        /// <param name="height">Height of the map.</param>
        /// <param name="tilewidth">Width of the individual tiles.</param>
        /// <param name="tileheight">Height of the individual tiles.</param>
        private void CalculateCurrentMapSize(int width, int height, int tilewidth, int tileheight)
        {
            var width2 = (width + 1) * tilewidth;
            var height2 = (height + 1) * tileheight;
            lblSize.Text = width2 + @" x " + height2;
        }

        /// <summary>
        /// Calculates the size of the map based on the inputted values.
        /// </summary>
        private void CalculateMapSize()
        {
            var width = ((int)spinWidth.Value + 1) * (int)spinTileWidth.Value;
            var height = ((int)spinHeight.Value + 1) * (int)spinTileHeight.Value;
            lblNewSize.Text = width + @" x " + height;

            MapWidth = (int)spinWidth.Value;
            MapHeight = (int)spinHeight.Value;
            TileWidth = (int)spinTileWidth.Value;
            TileHeight = (int)spinTileHeight.Value;

            anchorSelection.xOffset = MapWidth - oldMapWidth;
            anchorSelection.yOffset = MapHeight - oldMapHeight;
        }

        #endregion
    }
}
