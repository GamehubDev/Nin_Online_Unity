using System;
using CrystalLib.TileEngine;
using Toolset.Managers;

namespace Toolset.Docking
{
    public partial class DockTileset : ToolWindow
    {
        #region Field Region

        private Tileset Tileset;

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DockTileset"/> form.
        /// </summary>
        public DockTileset()
        {
            InitializeComponent();

            Disable();

            cmbTilesets.SelectedIndexChanged += cmbTilesets_SelectedIndexChanged;

            btnNew.Click += btnNew_Click;
            btnRename.Click += btnRename_Click;
            btnProperties.Click += btnProperties_Click;
            btnDelete.Click += btnDelete_Click;
            btnGrid.Click += btnGrid_Click;

            Resize += DockTileset_Resize;

            ProjectManager.Instance.ProjectLoaded += ProjectLoaded;
            ProjectManager.Instance.ProjectClosed += ProjectClosed;

            TilesetManager.Instance.TilesetLoaded += TilesetLoaded;
            TilesetManager.Instance.TilesetAdded += TilesetAdded;
            TilesetManager.Instance.TilesetChanged += TilesetChanged;
            TilesetManager.Instance.TilesetDeleted += TilesetDeleted;
            TilesetManager.Instance.TilesetSelected += TilesetSelected;
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the ProjectLoaded event of the <see cref="ProjectManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectLoadedEventArgs"/> instance containing the event data.</param>
        private void ProjectLoaded(object sender, ProjectLoadedEventArgs e)
        {
            Enable();
        }

        /// <summary>
        /// Handles the ProjectClosed event of the <see cref="ProjectManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectClosedEventArgs"/> instance containing the event data.</param>
        private void ProjectClosed(object sender, ProjectClosedEventArgs e)
        {
            Disable();
        }

        /// <summary>
        /// Handles the TilesetLoaded event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetLoadedEventArgs"/> instance containing the event data.</param>
        private void TilesetLoaded(object sender, TilesetLoadedEventArgs e)
        {
            foreach (var tileset in e.Tilesets)
            {
                cmbTilesets.Items.Add(tileset.Name);
            }

            if (cmbTilesets.Items.Count > 0)
                cmbTilesets.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the TilesetAdded event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetAddedEventArgs"/> instance containing the event data.</param>
        private void TilesetAdded(object sender, TilesetAddedEventArgs e)
        {
            cmbTilesets.Items.Add(e.Tileset.Name);
            TilesetManager.Instance.SelectTileset(e.Tileset.ID);
        }

        /// <summary>
        /// Handles the TilesetDeleted event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetDeletedEventArgs"/> instance containing the event data.</param>
        private void TilesetDeleted(object sender, TilesetDeletedEventArgs e)
        {
            cmbTilesets.Items.Remove(e.Name);

            viewTexture.Texture = null;

            if (cmbTilesets.Items.Count > 0)
                cmbTilesets.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the TilesetChanged event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetChangedEventArgs"/> instance containing the event data.</param>
        private void TilesetChanged(object sender, TilesetChangedEventArgs e)
        {
            for (int i = 0; i < cmbTilesets.Items.Count; i++)
            {
                if (cmbTilesets.Items[i].ToString() == e.OldTileset.Name)
                    cmbTilesets.Items[i] = e.NewTileset.Name;
            }

            cmbTilesets.Sorted = true;
        }

        /// <summary>
        /// Handles the TilesetSelected event of the <see cref="TilesetManager"/> object;
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetSelectedEventArgs"/> instance containing the event data.</param>
        private void TilesetSelected(object sender, TilesetSelectedEventArgs e)
        {
            if (e.Tileset == null)
            {
                viewTexture.Texture = null;
                return;
            }

            for (int i = 0; i < cmbTilesets.Items.Count; i++)
            {
                if (cmbTilesets.Items[i].ToString() == e.Tileset.Name)
                    cmbTilesets.SelectedIndex = i;
            }

            Tileset = e.Tileset;
        }

        #endregion

        #region Toolstrip Event Handler Region

        /// <summary>
        /// Handles the Click event of the <see cref="btnNew"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            TilesetManager.Instance.NewTileset();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnProperties"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnProperties_Click(object sender, EventArgs e)
        {
            TilesetManager.Instance.EditTileset(Tileset.ID);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnRename"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnRename_Click(object sender, EventArgs e)
        {
            TilesetManager.Instance.RenameTileset(Tileset.ID);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnDelete"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            TilesetManager.Instance.DeleteTileset(Tileset.ID);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnGrid"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnGrid_Click(object sender, EventArgs e)
        {
            viewTexture.TileGrid = !viewTexture.TileGrid;
            btnGrid.Checked = viewTexture.TileGrid;
        }

        #endregion

        #region Form Event Handler Region

        /// <summary>
        /// Handles the SelectedIndexChanged event of the <see cref="cmbTilesets"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmbTilesets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTilesets.SelectedIndex < 0)
                return;

            var name = cmbTilesets.SelectedItem.ToString();

            if (String.IsNullOrEmpty(name)) return;

            TilesetManager.Instance.SelectTileset(TilesetManager.Instance.GetTilesetID(name));
        }

        /// <summary>
        /// Handles the Resize event of the <see cref="DockTileset"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DockTileset_Resize(object sender, EventArgs e)
        {
            if (cmbTilesets != null)
                cmbTilesets.Refresh();
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Enables the controls.
        /// </summary>
        private void Enable()
        {
            toolStripMain.Enabled = true;
            cmbTilesets.Enabled = true;
            viewTexture.Enabled = true;

            BuildMenu();
        }

        /// <summary>
        /// Disables the controls.
        /// </summary>
        private void Disable()
        {
            toolStripMain.Enabled = false;
            cmbTilesets.Enabled = false;

            ClearList();

            viewTexture.Texture = null;
            viewTexture.Enabled = false;
        }

        /// <summary>
        /// Enables/Disables specific buttons on the <see cref="toolStripMain"/> control.
        /// </summary>
        private void BuildMenu()
        {
            if (cmbTilesets.Items.Count == 0)
            {
                btnRename.Enabled = false;
                btnDelete.Enabled = false;
                btnGrid.Enabled = false;
                btnProperties.Enabled = false;
            }
            else
            {
                btnRename.Enabled = true;
                btnDelete.Enabled = true;
                btnProperties.Enabled = true;
                btnGrid.Enabled = true;
            }
        }

        /// <summary>
        /// Clears the items in the <see cref="cmbTilesets"/> control.
        /// </summary>
        private void ClearList()
        {
            cmbTilesets.Items.Clear();
        }

        #endregion
    }
}