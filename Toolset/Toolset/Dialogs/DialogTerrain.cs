using System;
using System.Windows.Forms;
using CrystalLib.ResourceEngine;
using CrystalLib.TileEngine;
using Toolset.Managers;

namespace Toolset.Dialogs
{
    public partial class DialogTerrain : Form
    {
        #region Property Region

        public string TerrainName { get; set; }
        public TerrainType TerrainType { get; set; }
        public int TilesetID { get; set; }
        public int SelectionX { get; set; }
        public int SelectionY { get; set; }
        public int SelectionWidth { get; set; }
        public int SelectionHeight { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogTerrain"/> form.
        /// </summary>
        public DialogTerrain()
        {
            InitializeComponent();

            cmbTileset.Items.Clear();
            cmbType.Items.Clear();

            cmbTileset.BeginUpdate();
            cmbType.BeginUpdate();

            var tilesetList = TilesetManager.Instance.TilesetNames;
            tilesetList.Sort();
            foreach (var str in tilesetList)
            {
                cmbTileset.Items.Add(str);
            }

            var typeList = Enum.GetValues(typeof(TerrainType));
            foreach (var str in typeList)
            {
                cmbType.Items.Add(str);
            }

            cmbTileset.EndUpdate();
            cmbType.EndUpdate();

            if (cmbTileset.Items.Count > 0)
            {
                cmbTileset.SelectedIndex = 0;
                SetTileset();
            }
            if (cmbType.Items.Count > 0)
            {
                cmbType.SelectedIndex = 0;
                SetTerrainSize();
            }

            btnOK.Click += btnOK_Click;

            cmbTileset.SelectedIndexChanged += cmbTileset_SelectedIndexChanged;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
        }

        /// <summary>
        /// Initialzes a new instance of the <see cref="DialogTerrain"/> form.
        /// Effectively puts the dialog in 'edit mode'.
        /// Populates the field with the corresponding data.
        /// </summary>
        /// <param name="name">Terrain name.</param>
        /// <param name="type">Type of the terrain.</param>
        /// <param name="tileset">ID of the tileset.</param>
        /// <param name="x">X co-ordinate of the terrain.</param>
        /// <param name="y">Y co-ordinate of the terrain.</param>
        /// <param name="width">Width of the terrain.</param>
        /// <param name="height">Height of the terrain.</param>
        public DialogTerrain(string name, TerrainType type, int tileset, int x, int y, int width, int height)
        {
            InitializeComponent();

            cmbTileset.Items.Clear();
            cmbType.Items.Clear();

            var tilesetList = TilesetManager.Instance.TilesetNames;
            tilesetList.Sort();
            foreach (var str in tilesetList)
            {
                cmbTileset.Items.Add(str);
            }

            var typeList = Enum.GetValues(typeof(TerrainType));
            foreach (var str in typeList)
            {
                cmbType.Items.Add(str);
            }

            if (cmbTileset.Items.Count > 0)
                SetTileset();
            if (cmbType.Items.Count > 0)
                SetTerrainSize();

            btnOK.Click += btnOK_Click;

            cmbTileset.SelectedIndexChanged += cmbTileset_SelectedIndexChanged;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;

            txtName.Text = name;
            for (int i = 0; i < cmbType.Items.Count; i++)
            {
                if (cmbType.Items[i].ToString() == type.ToString())
                {
                    cmbType.SelectedIndex = i;
                    SetTerrainSize();
                }
            }

            var tilesetName = TilesetManager.Instance.GetTileset(tileset).Name;
            for (int i = 0; i < cmbTileset.Items.Count; i++)
            {
                if (cmbTileset.Items[i].ToString() == tilesetName)
                {
                    cmbTileset.SelectedIndex = i;
                    SetTileset();
                }
            }

            viewTileset.SelectionX = x;
            viewTileset.SelectionY = y;
            viewTileset.SelectionWidth = width;
            viewTileset.SelectionHeight = height;

            Load += delegate { viewTileset.EnsureVisible(); };

            Text = @"Edit Terrain";
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

            TerrainName = txtName.Text;
            TerrainType = (TerrainType)cmbType.SelectedItem;
            TilesetID = TilesetManager.Instance.GetTilesetID(cmbTileset.SelectedItem.ToString());
            SelectionX = viewTileset.SelectionX;
            SelectionY = viewTileset.SelectionY;
            SelectionWidth = viewTileset.SelectionWidth;
            SelectionHeight = viewTileset.SelectionHeight;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the <see cref="cmbTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param> 
        private void cmbTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTileset();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the <see cref="cmbType"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param> 
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTerrainSize();
        }

        #endregion

        #region Method Region

        /// <summary>
        /// Builds the <see cref="viewTileset"/> control based on the <see cref="cmbTileset"/> selected index.
        /// </summary>
        private void SetTileset()
        {
            Tileset tileset = null;
            if (cmbTileset.SelectedItem != null)
                tileset = TilesetManager.Instance.GetTileset(cmbTileset.SelectedItem.ToString());
            if (tileset == null)
            {
                viewTileset.ObjectWidth = 0;
                viewTileset.ObjectHeight = 0;
                viewTileset.TileWidth = 0;
                viewTileset.TileHeight = 0;
                viewTileset.Texture = null;
                return;
            }

            var image = ResourceManager.Instance.LoadTexture(tileset.Image);

            viewTileset.ObjectWidth = (int)image.Size.X;
            viewTileset.ObjectHeight = (int)image.Size.Y;
            viewTileset.TileWidth = tileset.TileWidth;
            viewTileset.TileHeight = tileset.TileHeight;

            viewTileset.LoadTexture(tileset.Image);
        }

        /// <summary>
        /// Sets the selection size of the <see cref="viewTileset"/> control.
        /// </summary>
        private void SetTerrainSize()
        {
            if (cmbType.SelectedItem == null) return;
            var type = (TerrainType)cmbType.SelectedItem;
            switch (type)
            {
                case TerrainType.RM2K_Ground:
                    viewTileset.SelectionWidth = 96;
                    viewTileset.SelectionHeight = 128;
                    break;
                case TerrainType.RMVX_Ground:
                    viewTileset.SelectionWidth = 64;
                    viewTileset.SelectionHeight = 96;
                    break;
                case TerrainType.RMVX_Cliff:
                    viewTileset.SelectionWidth = 64;
                    viewTileset.SelectionHeight = 64;
                    break;
            }
        }

        /// <summary>
        /// Validates the input areas of the <see cref="DialogTerrain"/> form.
        /// </summary>
        /// <returns>Returns false if validation fails, true if validation succeedes.</returns>
        private bool ValidateForm()
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(@"Please enter a terrain name.", Text);
                return false;
            }

            return true;
        }

        #endregion
    }
}
