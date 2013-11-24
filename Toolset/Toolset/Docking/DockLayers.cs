using System;
using System.Drawing;
using System.Windows.Forms;
using CrystalLib.Toolset.UndoRedo;
using Toolset.Managers;
using Toolset.TileEngine;

namespace Toolset.Docking
{
    public partial class DockLayers : ToolWindow
    {
        #region Field Region

        private EditorTileMap _map;

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DockLayers"/> form.
        /// </summary>
        public DockLayers()
        {
            InitializeComponent();

            Disable();

            Resize += DockLayers_Resize;
            lstLayers.ColumnWidthChanging += lstLayers_ColumnWidthChanging;
            lstLayers.ColumnWidthChanged += lstLayers_ColumnWidthChanged;

            cmbOpacity.DropDownControl = pnlPopup;

            btnNew.Click += btnNew_Click;
            btnRename.Click += btnRename_Click;
            btnDelete.Click += btnDelete_Click;
            btnUp.Click += btnUp_Click;
            btnDown.Click += btnDown_Click;
            btnDuplicate.Click += btnDuplicate_Click;
            btnShowHide.Click += btnShowHide_Click;

            mnuRename.Click += btnRename_Click;
            mnuDelete.Click += btnDelete_Click;
            mnuUp.Click += btnUp_Click;
            mnuDown.Click += btnDown_Click;
            mnuDuplicate.Click += btnDuplicate_Click;            

            lstLayers.SelectedIndexChanged += lstLayers_SelectedIndexChanged;
            lstLayers.ItemCheck += lstLayers_ItemCheck;
            lstLayers.MouseClick += lstLayers_MouseClick;

            ProjectManager.Instance.ProjectClosed += ProjectClosed;
            MapManager.Instance.MapSelected += MapSelected;
        }

        #endregion

        #region Event Handler Region

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
        /// Handles the MapSelected event of the <see cref="MapManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapSelectedEventArgs"/> instance containing the event data.</param>
        private void MapSelected(object sender, MapSelectedEventArgs e)
        {
            if (e.Map == null)
            {
                _map = null;
                Disable();
                return;
            }

            if (_map == e.Map) return;

            if (_map != null)
            {
                _map.LayerAdded -= LayerAdded;
                _map.LayerChanged -= LayerChanged;
                _map.LayerOrderChanged -= LayerOrderChanged;
                _map.LayerDeleted -= LayerDeleted;
                _map.LayerSelected -= LayerSelected;
                _map.UndoRedoArea.CommandDone -= UndoRedoCommandDone;
            }

            _map = e.Map;

            _map.LayerAdded += LayerAdded;
            _map.LayerChanged += LayerChanged;
            _map.LayerOrderChanged += LayerOrderChanged;
            _map.LayerDeleted += LayerDeleted;
            _map.LayerSelected += LayerSelected;
            _map.UndoRedoArea.CommandDone += UndoRedoCommandDone;

            BuildList();
            Enable();
        }

        /// <summary>
        /// Handles the LayerAdded event of the <see cref="EditorTileMap"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerAddedEventArgs"/> instance containing the event data.</param>
        private void LayerAdded(object sender, LayerAddedEventArgs e)
        {
            BuildList();
        }

        /// <summary>
        /// Handles the LayerChanged event of the <see cref="EditorTileMap"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerChangedEventArgs"/> instance containing the event data.</param>
        private void LayerChanged(object sender, LayerChangedEventArgs e)
        {
            BuildList();
        }

        /// <summary>
        /// Handles the LayerOrderChanged event of the <see cref="EditorTileMap"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerOrderChangedEventArgs"/> instance containing the event data.</param>
        private void LayerOrderChanged(object sender, LayerOrderChangedEventArgs e)
        {
            BuildList();
        }

        /// <summary>
        /// Handles the LayerDeleted event of the <see cref="EditorTileMap"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerDeletedEventArgs"/> instance containing the event data.</param>
        private void LayerDeleted(object sender, LayerDeletedEventArgs e)
        {
            for (int i = 0; i < lstLayers.Items.Count; i++)
            {
                if (lstLayers.Items[i].Text == e.Layer.Name)
                    lstLayers.Items.RemoveAt(i);
            }
        }

        /// <summary>
        /// Handles the LayerSelected event of the <see cref="EditorTileMap"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LayerSelectedEventArgs"/> instance containing the event data.</param>
        private void LayerSelected(object sender, LayerSelectedEventArgs e)
        {
            if (GetSelectedItem() != e.Layer.Name)
                SetSelectedItem(e.Layer.Name);
        }

        private void UndoRedoCommandDone(object sender, CommandDoneEventArgs e)
        {
            if (e.CommandDoneType == CommandDoneType.Redo || e.CommandDoneType == CommandDoneType.Undo)
                BuildList();
        }

        #endregion

        #region Toolstrip Event Handler Region

        /// <summary>
        /// Handles the Click event of the <see cref="btnNew"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            _map.NewLayer();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnRename"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnRename_Click(object sender, EventArgs e)
        {
            _map.RenameLayer();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnDelete"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            _map.DeleteLayer();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnUp"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            // Reverse because of the reversed list view.
            _map.MoveLayerDown();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnDown"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            // Reverse because of the reversed list view.
            _map.MoveLayerUp();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnDuplicate"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            _map.DuplicateLayer();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnShowHide"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnShowHide_Click(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region Form Event Handler Region

        /// <summary>
        /// Handles the Resize event of the <see cref="DockLayers"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DockLayers_Resize(object sender, EventArgs e)
        {
            ResizeList();
        }

        /// <summary>
        /// Handles the ColumnWidthChanging event of the <see cref="lstLayers"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ColumnWidthChangingEventArgs"/> instance containing the event data.</param>
        private void lstLayers_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Handles the ColumnWidthChanged event of the <see cref="lstLayers"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ColumnWidthChangedEventArgs"/> instance containing the event data.</param>
        private void lstLayers_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {            
            ResizeList();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the <see cref="lstLayers"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lstLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            _map.SelectLayer(GetSelectedItem());
        }

        /// <summary>
        /// Handles the ItemCheck event of the <see cref="lstLayers"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemCheckEventArgs"/> instance containing the event data.</param>
        private void lstLayers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var value = e.NewValue == CheckState.Checked;
            _map.SetLayerVisible(lstLayers.Items[e.Index].Text, value);
        }

        /// <summary>
        /// Handles the MouseClick event of the <see cref="lstLayers"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void lstLayers_MouseClick(object sender, MouseEventArgs e)
        {
            if (GetSelectedIndex() == -1)
                SetSelectedItem(_map.SelectedLayer.Name);

            if (e.Button != MouseButtons.Right) return;

            if (lstLayers.FocusedItem.Bounds.Contains(e.Location))
            {
                contextLayers.Show(Cursor.Position);
            }
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Enables the form.
        /// </summary>
        public void Enable()
        {
            toolStripMain.Enabled = true;
            lstLayers.Enabled = true;
            lstLayers.BackColor = Color.White;
            //cmbOpacity.Enabled = true;
        }

        /// <summary>
        /// Disables the form.
        /// </summary>
        public void Disable()
        {
            toolStripMain.Enabled = false;
            lstLayers.Enabled = false;
            lstLayers.BackColor = SystemColors.Control;
            cmbOpacity.Enabled = false;

            lstLayers.Items.Clear();
        }

        /// <summary>
        /// Builds the list of layers.
        /// </summary>
        private void BuildList()
        {
            lstLayers.Items.Clear();

            for (int i = _map.Layers.Count - 1; i >= 0; i--)
            {
                var layer = _map.Layers[i];
                var lvi = new ListViewItem(layer.Name);
                lvi.SubItems.Add(layer.Name);
                lstLayers.Items.Add(lvi);
                lstLayers.Items[lstLayers.Items.Count - 1].Checked = layer.Visible;
            }

            btnDelete.Enabled = lstLayers.Items.Count > 1;
            mnuDelete.Enabled = btnDelete.Enabled;

            if (_map.SelectedLayer != null)
                SetSelectedItem(_map.SelectedLayer.Name);

            if (GetSelectedIndex() == -1)
            {
                lstLayers.Items[lstLayers.Items.Count - 1].Selected = true;
                lstLayers.EnsureVisible(lstLayers.Items.Count - 1);
            }
        }

        /// <summary>
        /// Gets the currently selected item.
        /// </summary>
        /// <returns>Currently selected item.</returns>
        private string GetSelectedItem()
        {
            return GetSelectedIndex() == -1 ? "" : lstLayers.Items[GetSelectedIndex()].Text;
        }

        /// <summary>
        /// Sets the currently selected item.
        /// </summary>
        /// <param name="text">Item to select.</param>
        public void SetSelectedItem(string text)
        {
            for (var i = 0; i < lstLayers.Items.Count; i++)
            {
                if (lstLayers.Items[i].Text == text)
                {
                    lstLayers.Items[i].Selected = true;
                    lstLayers.EnsureVisible(lstLayers.Items[i].Index);
                }
            }
        }

        /// <summary>
        /// Gets the currently selected index.
        /// </summary>
        /// <returns>Index of the currently selected item.</returns>
        private int GetSelectedIndex()
        {
            for (var i = 0; i < lstLayers.Items.Count; i++)
            {
                if (lstLayers.Items[i].Selected)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Resizes the columns of the <see cref="lstLayers"/> control.
        /// </summary>
        private void ResizeList()
        {
            if (columnIcon.Width != 23)
                columnIcon.Width = 23;
            if (columnName.Width != lstLayers.Width - columnIcon.Width - 17)
                columnName.Width = lstLayers.Width - columnIcon.Width - 17;
        }

        #endregion
    }
}