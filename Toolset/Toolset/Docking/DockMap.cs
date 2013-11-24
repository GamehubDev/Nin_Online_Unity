using System;
using System.Diagnostics;
using System.Windows.Forms;
using CrystalLib.TileEngine;
using CrystalLib.Toolset.Docking;
using Toolset.Managers;
using Toolset.TileEngine;

namespace Toolset.Docking
{
    public partial class DockMap : DockContent
    {
        #region Property Region

        /// <summary>
        /// <see cref="TileMap"/> object controlled by the <see cref="DockMap"/> form.
        /// </summary>
        public EditorTileMap Map
        {
            get { return viewMap.Map; }
            set { viewMap.Map = value; }
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DockMap"/> form.
        /// </summary>
        public DockMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DockMap"/> form.
        /// </summary>
        /// <param name="map"><see cref="TileMap"/> to edit.</param>
        public DockMap(EditorTileMap map)
        {
            InitializeComponent();

            MapManager.Instance.MapChanged += MapChanged;
            MapManager.Instance.MapDeleted += MapDeleted;

            map.UnsavedChangesChanged += UnsavedChangesChanged;

            FormClosing += DockMap_FormClosing;

            viewMap.SetMap(map);

            this.Name = @"Map" + Map.Name;
            this.Text = Map.Name;
            this.TabText = Map.Name;

            txtName.Text = Map.Name;
            mnuTabSave.Text = @"Save " + Map.Name;

            mnuProperties.Click += mnuProperties_Click;
            mnuSize.Click += mnuSize_Click;
            mnuExport.Click += mnuExport_Click;
            mnuGrid.Click += mnuGrid_Click;

            mnuTab.Opening += mnuTab_Opening;
            mnuTabSave.Click += mnuTabSave_Click;
            mnuTabClose.Click += mnuTabClose_Click;
            mnuTabFolder.Click += mnuTabFolder_Click;
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the MapChanged event of the <see cref="MapManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapChangedEventArgs"/> instance containing the event data.</param>
        private void MapChanged(object sender, MapChangedEventArgs e)
        {
            if (Map == null) return;
            if (e.NewMap.Name != Map.Name) return;

            if (this.Name != "Map" + e.NewMap.Name)
                this.Name = "Map" + e.NewMap.Name;

            if (this.Text != e.NewMap.Name)
                this.Text = e.NewMap.Name;

            if (this.TabText != e.NewMap.Name)
            {
                this.TabText = e.NewMap.Name;
                if (e.NewMap.UnsavedChanges)
                    this.TabText += @"*";
            }

            if (txtName.Text != e.NewMap.Name)
                txtName.Text = e.NewMap.Name;
        }

        /// <summary>
        /// Handles the MapDeleted event of the <see cref="MapManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapDeletedEventArgs"/> instance containing the event data.</param>
        private void MapDeleted(object sender, MapDeletedEventArgs e)
        {
            if (Map != null)
            {
                if (e.Name != Map.Name) 
                    return;
            }
            Close();
        }

        /// <summary>
        /// Handles the UnsavedChangesChanged event of the <see cref="Map"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnsavedChangesEventArgs"/> instance containing the event data.</param>
        private void UnsavedChangesChanged(object sender, UnsavedChangesEventArgs e)
        {
            if (Map == null) return;
            if (e.UnsavedChanges)
                this.TabText = Map.Name + @"*";
            else
                this.TabText = Map.Name;
        }

        #endregion

        #region Form Event Handler Region

        /// <summary>
        /// Handles the Click event of the <see cref="mnuProperties"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuProperties_Click(object sender, EventArgs e)
        {
            MapManager.Instance.EditMap(MapManager.Instance.GetMapID(this.Text));
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuSize"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuSize_Click(object sender, EventArgs e)
        {
            MapManager.Instance.EditMapSize(MapManager.Instance.GetMapID(this.Text));
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuExport"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuExport_Click(object sender, EventArgs e)
        {
            //MapManager.Instance.ExportAsImage(MapManager.Instance.GetMapID(this.Text));
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuGrid"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuGrid_Click(object sender, EventArgs e)
        {
            viewMap.TileGrid = !viewMap.TileGrid;
            mnuGrid.Checked = viewMap.TileGrid;
        }

        /// <summary>
        /// Handles the Opening event of the <see cref="mnuTab"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuTab_Opening(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuTabSave"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuTabSave_Click(object sender, EventArgs e)
        {
            if (Map == null) return;
            MapManager.Instance.SaveMap(Map.ID);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuTabClose"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuTabClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuTabFolder"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuTabFolder_Click(object sender, EventArgs e)
        {
            var folder = ProjectManager.Instance.Project.MapPath;
            Process.Start(folder);
        }

        /// <summary>
        /// Handles the FormClosing event of the <see cref="DockMap"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void DockMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;

            if (Map == null) return;
            if (!Map.Close())
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        #endregion
    }
}