using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CrystalLib.ExtensionMethods;
using CrystalLib.Toolset.UndoRedo;
using Toolset.Managers;
using Toolset.TileEngine;

namespace Toolset.Docking
{
    public partial class DockHistory : ToolWindow
    {
        #region Field Region

        private EditorTileMap _map;

        private List<string> UndoCache = new List<string>();
        private List<string> RedoCache = new List<string>();

        public EventHandler<EventArgs> UndoRedoChanged;

        public bool UndoEnabled
        {
            get { return btnUndo.Enabled; }
        }

        public bool RedoEnabled
        {
            get { return btnRedo.Enabled; }
        }

        #endregion

        #region Constructor Region

        public DockHistory()
        {
            InitializeComponent();

            Disable();

            Resize += DockHistory_Resize;
            lstHistory.ColumnWidthChanging += lstHistory_ColumnWidthChanging;
            lstHistory.ColumnWidthChanged += lstHistory_ColumnWidthChanged;

            btnUndo.Click += btnUndo_Click;
            btnRedo.Click += btnRedo_Click;
            btnDelete.Click += btnDelete_Click;

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
                if (_map != null)
                {
                    _map.UndoRedoArea.CommandDone -= UndoRedoCommandDone;
                    _map = null;
                }
                Disable();
                return;
            }

            if (_map == e.Map) return;

            _map = e.Map;
            _map.UndoRedoArea.CommandDone += UndoRedoCommandDone;

            lstHistory.Items.Clear();
            BuildList();

            Enable();
        }

        private void UndoRedoCommandDone(object sender, CommandDoneEventArgs e)
        {
            BuildList();
        }

        #endregion

        #region Form Event Handler Region

        /// <summary>
        /// Handles the Click event of the <see cref="btnUndo"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void btnUndo_Click(object sender, EventArgs e)
        {
            _map.UndoRedoArea.Undo();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnRedo"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void btnRedo_Click(object sender, EventArgs e)
        {
            _map.UndoRedoArea.Redo();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnDelete"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void btnDelete_Click(object sender, EventArgs e)
        {
            _map.UndoRedoArea.ClearHistory();
            BuildList();
        }

        /// <summary>
        /// Handles the Resize event of the <see cref="DockHistory"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DockHistory_Resize(object sender, EventArgs e)
        {
            ResizeList();
        }

        /// <summary>
        /// Handles the ColumnWidthChanging event of the <see cref="lstHistory"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ColumnWidthChangingEventArgs"/> instance containing the event data.</param>
        private void lstHistory_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Handles the ColumnWidthChanged event of the <see cref="lstHistory"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ColumnWidthChangedEventArgs"/> instance containing the event data.</param>
        private void lstHistory_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            ResizeList();
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Enables the form.
        /// </summary>
        public void Enable()
        {
            toolStripMain.Enabled = true;
            lstHistory.Enabled = true;
            lstHistory.BackColor = Color.White;
        }

        /// <summary>
        /// Disables the form.
        /// </summary>
        public void Disable()
        {
            toolStripMain.Enabled = false;
            lstHistory.Enabled = false;
            lstHistory.BackColor = SystemColors.Control;
            lstHistory.Items.Clear();
        }

        public void BuildList()
        {
            var UndoCommands = new List<string>(_map.UndoRedoArea.UndoCommands);
            var RedoCommands = new List<string>(_map.UndoRedoArea.RedoCommands);

            var count = UndoCommands.Count + RedoCommands.Count;
            var array = new ListViewItem[count];

            lstHistory.BeginUpdate();

            if (UndoCommands.Count > 0 || RedoCommands.Count > 0)
                btnDelete.Enabled = true;
            else
                btnDelete.Enabled = false;

            UndoCommands.Reverse();

            int i = 0;
            foreach (var action in UndoCommands)
            {
                var lvi = new ListViewItem(action);
                lvi.SubItems.Add(action);

                if (GetIcon(action) != -1)
                    lvi.ImageIndex = GetIcon(action);

                array[i] = lvi;
                if (lstHistory.Items.IndexOf(lvi) == UndoCommands.Count - 1)
                {
                    lvi.Selected = true;
                    lstHistory.EnsureVisible(lvi.Index);
                }
                i++;
            }
            foreach (var action in RedoCommands)
            {
                var lvi = new ListViewItem(action)
                {
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.Gray
                };
                lvi.SubItems.Add(action);

                if (GetIcon(action) != -1)
                    lvi.ImageIndex = GetIcon(action);

                array[i] = lvi;
                i++;
            }

            lstHistory.Items.Clear();
            lstHistory.Items.AddRange(array);

            btnUndo.Enabled = _map.UndoRedoArea.CanUndo;
            btnRedo.Enabled = _map.UndoRedoArea.CanRedo;

            lstHistory.EndUpdate();

            UndoRedoChanged.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Resizes the columns of the <see cref="lstHistory"/> control.
        /// </summary>
        private void ResizeList()
        {
            if (columnIcon.Width != 23)
                columnIcon.Width = 23;
            if (columnName.Width != lstHistory.Width - columnIcon.Width - 17)
                columnName.Width = lstHistory.Width - columnIcon.Width - 17;
        }

        private int GetIcon(string action)
        {
            if (action == "Brush Tool")
                return 0;
            if (action == "Terrain Tool")
                return 4;
            if (action == "Eraser Tool")
                return 1;
            if (action == "Fill Tool")
                return 2;
            if (action == "New Layer" || action == "Renamed Layer" || action == "Duplicated Layer"
                || action == "Deleted Layer" || action == "Moved Layer Up" || action == "Moved Layer Down"
                || action == "Set Layer Visibility")
                return 3;
            if (action == "Map Size Changed")
                return 5;
            return -1;
        }

        #endregion
    }
}
