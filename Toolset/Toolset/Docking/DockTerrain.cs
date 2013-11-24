using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CrystalLib.TileEngine;
using Toolset.Controls.Sorting;
using Toolset.Managers;

namespace Toolset.Docking
{
    public partial class DockTerrain : ToolWindow
    {
        #region Field Region

        private int _selectedID;
        private List<BackgroundWorker> threadArray = new List<BackgroundWorker>();

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DockTerrain"/> form.
        /// </summary>
        public DockTerrain()
        {
            InitializeComponent();

            Disable();

            ProjectManager.Instance.ProjectLoaded += ProjectLoaded;
            ProjectManager.Instance.ProjectClosed += ProjectClosed;

            TerrainManager.Instance.TerrainAdded += TerrainAdded;
            TerrainManager.Instance.TerrainDeleted += TerrainDeleted;
            TerrainManager.Instance.TerrainLoaded += TerrainLoaded;
            TerrainManager.Instance.TerrainChanged += TerrainChanged;
            TerrainManager.Instance.TerrainSelected += TerrainSelected;

            TilesetManager.Instance.TilesetLoaded += TilesetLoaded;

            btnNew.Click += btnNew_Click;
            btnRename.Click += btnRename_Click;
            btnProperties.Click += btnProperties_Click;
            btnDuplicate.Click += btnDuplicate_Click;
            btnDelete.Click += btnDelete_Click;

            mnuRename.Click += btnRename_Click;
            mnuProperties.Click += btnProperties_Click;
            mnuDuplicate.Click += btnDuplicate_Click;
            mnuDelete.Click += btnDelete_Click;

            lstTerrain.MouseClick += lstTerrain_MouseClick;
            lstTerrain.SelectedIndexChanged += lstTerrain_SelectedIndexChanged;

            lstTerrain.ListViewItemSorter = new ListViewSorter();
            lstTerrain.LargeImageList = new ImageList
            {
                ImageSize = new Size(32, 32),
                ColorDepth = ColorDepth.Depth32Bit
            };
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
        /// Handles the TerrainAdded event of the <see cref="TerrainManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainAddedEventArgs"/> instance containing the event data.</param>
        private void TerrainAdded(object sender, TerrainAddedEventArgs e)
        {
            var tile = CreateTerrainItem(e.TerrainTile);

            var bmpItem = CreateIconImage(e.TerrainTile, true);
            lstTerrain.LargeImageList.Images.Add(e.TerrainTile.Name, bmpItem);

            lstTerrain.Items.Add(tile);
            lstTerrain.Sort();

            foreach (ListViewItem item in lstTerrain.Items)
            {
                if (item.Text == e.TerrainTile.Name)
                {
                    item.Selected = true;
                    lstTerrain.EnsureVisible(item.Index);
                }
            }
        }

        /// <summary>
        /// Handles the TerrainDeleted event of the <see cref="TerrainManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainAddedEventArgs"/> instance containing the event data.</param>
        private void TerrainDeleted(object sender, TerrainDeletedEventArgs e)
        {
            if (lstTerrain.LargeImageList.Images.ContainsKey(e.Name))
                lstTerrain.LargeImageList.Images.RemoveByKey(e.Name);

            foreach (ListViewItem item in lstTerrain.Items)
            {
                if (item.Text == e.Name)
                {
                    var index = 0;
                    if (item.Index > 0)
                        index = item.Index - 1;
                    lstTerrain.Items.Remove(item);

                    if (lstTerrain.Items.Count > 0)
                    {
                        lstTerrain.Items[index].Selected = true;
                        lstTerrain.EnsureVisible(index);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the TerrainLoaded event of the <see cref="TerrainManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainLoadedEventArgs"/> instance containing the event data.</param>
        private void TerrainLoaded(object sender, TerrainLoadedEventArgs e)
        {
            lstTerrain.BeginUpdate();

            lstTerrain.Items.Clear();

            foreach (var terrain in e.Terrain)
            {
                lstTerrain.Items.Add(CreateTerrainItem(terrain));
            }

            lstTerrain.Sort();

            if (lstTerrain.Items.Count > 0)
            {
                lstTerrain.Items[0].Selected = true;
                lstTerrain.EnsureVisible(0);
            }

            lstTerrain.EndUpdate();
        }

        /// <summary>
        /// Handles the TerrainChanged event of the <see cref="TerrainManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainChangedEventArgs"/> instance containing the event data.</param>
        private void TerrainChanged(object sender, TerrainChangedEventArgs e)
        {
            if (lstTerrain.LargeImageList.Images.ContainsKey(e.OldTerrain.Name))
                lstTerrain.LargeImageList.Images.RemoveByKey(e.OldTerrain.Name);

            foreach (ListViewItem item in lstTerrain.Items)
            {
                if (item.Text == e.OldTerrain.Name)
                {
                    item.Name = e.NewTerrain.Name;
                    item.Text = e.NewTerrain.Name;
                    item.ToolTipText = e.NewTerrain.Name;
                    item.ImageKey = e.NewTerrain.Name;
                }
            }

            var bmpItem = CreateIconImage(e.NewTerrain, true);
            lstTerrain.LargeImageList.Images.Add(e.NewTerrain.Name, bmpItem);

            lstTerrain.Sort();
        }

        /// <summary>
        /// Handles the TerrainSelected event of the <see cref="TerrainManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainSelectedEventArgs"/> instance containing the event data.</param>
        private void TerrainSelected(object sender, TerrainSelectedEventArgs e)
        {
            var name = e.Terrain.Name;

            foreach (ListViewItem item in lstTerrain.Items)
            {
                if (item.Text == name)
                {
                    item.Selected = true;
                    item.EnsureVisible();
                }
            }
        }

        /// <summary>
        /// Handles the TilsetLoaded event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetLoadedEventArgs"/> instance containing the event data.</param>
        private void TilesetLoaded(object sender, TilesetLoadedEventArgs e)
        {
            lstTerrain.LargeImageList.Images.Clear();
            ThreadPool.QueueUserWorkItem(Icons_Init);
        }

        #endregion

        #region Form Event Handler Region

        /// <summary>
        /// Handles the MouseClick event of the <see cref="lstTerrain"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void lstTerrain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            if (lstTerrain.FocusedItem.Bounds.Contains(e.Location))
            {
                contextTerrain.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the <see cref="lstTerrain"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lstTerrain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTerrain.SelectedItems.Count == 0)
            {
                _selectedID = 0;
                btnRename.Enabled = false;
                btnProperties.Enabled = false;
                btnDuplicate.Enabled = false;
                btnDelete.Enabled = false;

                mnuRename.Enabled = false;
                mnuProperties.Enabled = false;
                mnuDuplicate.Enabled = false;
                mnuDelete.Enabled = false;
            }
            else
            {
                foreach (ListViewItem item in lstTerrain.Items)
                {
                    if (item.Selected)
                    {
                        var id = TerrainManager.Instance.GetTerrainID(item.Text);
                        TerrainManager.Instance.SelectTerrain(id);
                        _selectedID = id;
                    }
                }

                btnRename.Enabled = true;
                btnProperties.Enabled = true;
                btnDuplicate.Enabled = true;
                btnDelete.Enabled = true;

                mnuRename.Enabled = true;
                mnuProperties.Enabled = true;
                mnuDuplicate.Enabled = true;
                mnuDelete.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnNew"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            TerrainManager.Instance.NewTerrain();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnRename"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRename_Click(object sender, EventArgs e)
        {
            TerrainManager.Instance.RenameTerrain(_selectedID);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnProperties"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnProperties_Click(object sender, EventArgs e)
        {
            TerrainManager.Instance.EditTerrain(_selectedID);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnDuplicate"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            TerrainManager.Instance.DuplicateTerrain(_selectedID);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnDelete"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            TerrainManager.Instance.DeleteTerrain(_selectedID);
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Threaded generation of the terrain icons shown in the list.
        /// </summary>
        /// <param name="obj"></param>
        private void Icons_Init(object obj)
        {
            var terrain = TerrainManager.Instance.Terrain;
            terrain.Sort(new TerrainSorter());

            try
            {
                foreach (var tile in terrain)
                {
                    var bmpItem = CreateIconImage(tile);
                    var strItem = tile.Name;
                    Invoke((MethodInvoker)(() => lstTerrain.LargeImageList.Images.Add(strItem, bmpItem)));
                }
            }
            catch { }
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        private void Enable()
        {
            toolStripMain.Enabled = true;
            lstTerrain.Enabled = true;
        }

        /// <summary>
        /// Disables the controls.
        /// </summary>
        private void Disable()
        {
            toolStripMain.Enabled = false;

            btnRename.Enabled = false;
            btnProperties.Enabled = false;
            btnDuplicate.Enabled = false;
            btnDelete.Enabled = false;

            mnuRename.Enabled = false;
            mnuProperties.Enabled = false;
            mnuDuplicate.Enabled = false;
            mnuDelete.Enabled = false;

            lstTerrain.Enabled = false;
            lstTerrain.Items.Clear();
        }

        /// <summary>
        /// Creates a <see cref="ListViewItem"/> based on the <see cref="TerrainTile"/> object passed.
        /// </summary>
        /// <param name="terrain"><see cref="TerrainTile"/> to pull the properties from.</param>
        /// <returns>New <see cref="ListViewItem"/> based on the properties passed in the <see cref="TerrainTile"/> object.</returns>
        private ListViewItem CreateTerrainItem(TerrainTile terrain)
        {
            var newItem = new ListViewItem
            {
                Name = terrain.Name,
                Text = terrain.Name,
                ImageKey = terrain.Name,
                ToolTipText = terrain.Name
            };

            return newItem;
        }

        /// <summary>
        /// Checks if a cached Terrain icon exists. If not, creates one and saves it for future use.
        /// </summary>
        /// <param name="terrain"><see cref="TerrainTile"/> to pull the properties from.</param>
        /// <param name="forced">Force a new icon to be generated, even if a cached version exists.</param>
        /// <returns>A <see cref="Bitmap"/> object with the icon data.</returns>
        private Bitmap CreateIconImage(TerrainTile terrain, bool forced = false)
        {
            Bitmap cropped;

            if (terrain.Tileset == 0) return null;
             
            var filepath = Path.Combine(ProjectManager.Instance.Project.TerrainPath, terrain.Name + @".png");
            if (!File.Exists(filepath) || forced)
            {
                var tileset = TilesetManager.Instance.GetTileset(terrain.Tileset);
                if (tileset == null) return null;

                var path = tileset.Image;
                if (!File.Exists(path)) return null;

                var original = new Bitmap(path);
                var srcRect = new Rectangle(terrain.X * tileset.TileWidth, terrain.Y * tileset.TileHeight, tileset.TileWidth, tileset.TileHeight);
                cropped = original.Clone(srcRect, original.PixelFormat);
                cropped.Save(filepath);
            }
            else
            {
                cropped = new Bitmap(filepath);
            }

            return cropped;
        }

        #endregion
    }
}