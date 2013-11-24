using System;
using System.IO;
using System.Windows.Forms;

namespace Toolset.Dialogs
{
    public partial class DialogTileset : Form
    {
        #region Property Region

        public string TilesetName { get; set; }
        public string FilePath { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogTileset"/> form.
        /// </summary>
        public DialogTileset()
        {
            InitializeComponent();

            txtLocation.Text = Application.StartupPath;

            btnOK.Click += btnOK_Click;
            btnBrowse.Click += btnBrowse_Click;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogTileset"/> form.
        /// Effectively puts the dialog in 'edit mode'.
        /// Populates the fields with the corresponding data.
        /// </summary>
        /// <param name="name">Tileset name.</param>
        /// <param name="path">Path for the tileset texture.</param>
        /// <param name="tileWidth">Width of the individual tiles.</param>
        /// <param name="tileHeight">Height of the individual tiles.</param>
        public DialogTileset(string name, string path, int tileWidth, int tileHeight)
        {
            InitializeComponent();

            txtLocation.Text = Application.StartupPath;

            btnOK.Click += btnOK_Click;
            btnBrowse.Click += btnBrowse_Click;

            txtName.Text = name;
            txtLocation.Text = path;
            spinTileWidth.Value = tileWidth;
            spinTileHeight.Value = tileHeight;

            Text = @"Edit Tileset";
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

            TilesetName = txtName.Text;
            FilePath = txtLocation.Text;
            TileWidth = (int)spinTileWidth.Value;
            TileHeight = (int)spinTileHeight.Value;
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnBrowse"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = @"Select image to import";
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.Filter = @"Image Files|*.png;*.bmp;*.tga;*.jpg;*.gif;*.psd;*.hdr;*.pic";

                var result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtLocation.Text = dialog.FileName;

                    if (txtName.Text == @"New Tileset")
                        txtName.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
                }
            }
        }

        #endregion

        #region Method Region

        /// <summary>
        /// Validates the input areas of the <see cref="DialogTileset"/> form.
        /// </summary>
        /// <returns>Returns false if validation fails, true if validation succeedes.</returns>
        private bool ValidateForm()
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(@"Please enter a tileset name.", Text);
                return false;
            }

            if (!File.Exists(txtLocation.Text))
            {
                MessageBox.Show(@"Please enter a path to a valid image file.", Text);
                return false;
            }

            return true;
        }

        #endregion
    }
}
