using System;
using System.Windows.Forms;

namespace Toolset.Dialogs
{
    public partial class DialogMap : Form
    {
        #region Property Region

        public string MapName { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogMap"/> form.
        /// </summary>
        public DialogMap()
        {
            InitializeComponent();

            spinWidth.ValueChanged += delegate { CalculateMapSize(); };
            spinHeight.ValueChanged += delegate { CalculateMapSize(); };
            spinTileWidth.ValueChanged += delegate { CalculateMapSize(); };
            spinTileHeight.ValueChanged += delegate { CalculateMapSize(); };

            btnOK.Click += btnOK_Click;

            CalculateMapSize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogMap"/> form.
        /// Effectively puts the form in 'edit mode'.
        /// Populates the field with corresponding data.
        /// </summary>
        /// <param name="name">Name of the map.</param>
        /// <param name="width">Width of the map.</param>
        /// <param name="height">Height of the map.</param>
        /// <param name="tilewidth">Width of the tiles.</param>
        /// <param name="tileheight">Height of the tiles.</param>
        public DialogMap(string name, int width, int height, int tilewidth, int tileheight)
        {
            InitializeComponent();

            spinWidth.ValueChanged += delegate { CalculateMapSize(); };
            spinHeight.ValueChanged += delegate { CalculateMapSize(); };
            spinTileWidth.ValueChanged += delegate { CalculateMapSize(); };
            spinTileHeight.ValueChanged += delegate { CalculateMapSize(); };

            btnOK.Click += btnOK_Click;

            txtName.Text = name;
            spinWidth.Value = width;
            spinHeight.Value = height;
            spinTileWidth.Value = tilewidth;
            spinTileHeight.Value = tileheight;

            spinWidth.Enabled = false;
            spinHeight.Enabled = false;
            spinTileWidth.Enabled = false;
            spinTileHeight.Enabled = false;

            CalculateMapSize();

            Text = @"Edit Map";
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the Click event of the <see cref="btnYes"/> control.
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

            MapName = txtName.Text;
            MapWidth = (int)spinWidth.Value;
            MapHeight = (int)spinHeight.Value;
            TileWidth = (int)spinTileWidth.Value;
            TileHeight = (int)spinTileHeight.Value;
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Calculates the size of the map in pixels.
        /// </summary>
        private void CalculateMapSize()
        {
            var width = ((int)spinWidth.Value + 1) * (int)spinTileWidth.Value;
            var height = ((int)spinHeight.Value + 1) * (int)spinTileHeight.Value;
            lblSize.Text = width + @" x " + height;
        }

        /// <summary>
        /// Validates the input areas of the <see cref="DialogMap"/> form.
        /// </summary>
        /// <returns>Returns false if validation fails, true if validation succeedes.</returns>
        private bool ValidateForm()
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(@"Please enter a map name.", Text);
                return false;
            }

            return true;
        }

        #endregion
    }
}
