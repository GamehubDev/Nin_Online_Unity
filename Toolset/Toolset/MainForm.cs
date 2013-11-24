using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CrystalLib.Toolset.Docking;
using Toolset.Dialogs;
using Toolset.Docking;
using Toolset.Managers;
using Toolset.TileEngine;

namespace Toolset
{
    public partial class MainForm : Form
    {
        #region Field Region

        // Dockable form objects
        private readonly DockTileset _dockTileset;
        private readonly DockProject _dockProject;
        private readonly DockLayers _dockLayers;
        private readonly DockHistory _dockHistory;
        private readonly DockConsole _dockConsole;
        private readonly DockTerrain _dockTerrain;

        // DockPanelSuite serialization
        private readonly DeserializeDockContent _deserializeDockContent;

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> form.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            DisableToolset();

            _dockTileset = new DockTileset();
            _dockProject = new DockProject();
            _dockLayers = new DockLayers();
            _dockHistory = new DockHistory();
            _dockConsole = new DockConsole();
            _dockTerrain = new DockTerrain();

            _dockTileset.VisibleChanged += delegate { BuildViewMenu(); };
            _dockProject.VisibleChanged += delegate { BuildViewMenu(); };
            _dockLayers.VisibleChanged += delegate { BuildViewMenu(); };
            _dockHistory.VisibleChanged += delegate { BuildViewMenu(); };
            _dockConsole.VisibleChanged += delegate { BuildViewMenu(); };
            _dockTerrain.VisibleChanged += delegate { BuildViewMenu(); };

            _deserializeDockContent = GetDockContentFromString;

            Load += MainForm_Load;
            FormClosing += MainForm_FormClosing;

            mnuFile.DropDownOpening += mnuFile_DropDownOpening;
            mnuNewGame.Click += mnuNewGame_Click;
            mnuNewMap.Click += mnuNewMap_Click;
            mnuNewTileset.Click += mnuNewTileset_Click;
            mnuNewTerrain.Click += mnuNewTerrain_Click;
            mnuOpen.Click += mnuOpen_Click;
            mnuSave.Click += mnuSave_Click;
            mnuSaveAll.Click += mnuSaveAll_Click;
            mnuClose.Click += mnuClose_Click;
            mnuCloseProject.Click += mnuClose_Click;
            mnuPlay.Click += mnuPlay_Click;
            mnuExit.Click += mnuExit_Click;

            mnuUndo.Click += _dockHistory.btnUndo_Click;
            mnuRedo.Click += _dockHistory.btnRedo_Click;

            mnuBrush.Click += mnuBrush_Click;
            mnuTerrain.Click += mnuTerrain_Click;
            mnuEraser.Click += mnuEraser_Click;
            mnuFill.Click += mnuFill_Click;
            mnuRectangle.Click += mnuRectangle_Click;
            mnuEyedropper.Click += mnuEyedropper_Click;

            mnuViewTileset.Click += mnuViewTileset_Click;
            mnuViewProject.Click += mnuViewProject_Click;
            mnuViewLayers.Click += mnuViewLayers_Click;
            mnuViewHistory.Click += mnuViewHistory_Click;
            mnuViewConsole.Click += mnuViewConsole_Click;
            mnuViewTerrain.Click += mnuViewTerrain_Click;

            mnuAbout.Click += mnuAbout_Click;

            btnNewGame.Click += mnuNewGame_Click;
            btnNewMap.Click += mnuNewMap_Click;
            btnNewTileset.Click += mnuNewTileset_Click;
            btnNewTerrain.Click += mnuNewTerrain_Click;
            btnOpen.Click += mnuOpen_Click;
            btnSave.Click += mnuSave_Click;

            btnUndo.Click += _dockHistory.btnUndo_Click;
            btnRedo.Click += _dockHistory.btnRedo_Click;

            btnBrush.Click += mnuBrush_Click;
            btnTerrain.Click += mnuTerrain_Click;
            btnEraser.Click += mnuEraser_Click;
            btnFill.Click += mnuFill_Click;
            btnRectangle.Click += mnuRectangle_Click;
            btnEyedropper.Click += mnuEyedropper_Click;

            btnPlay.Click += mnuPlay_Click;
            
            dockPanel.ContentAdded += dockPanel_ContentAdded;
            dockPanel.ContentRemoved += dockPanel_ContentRemoved;
            dockPanel.ActiveDocumentChanged += dockPanel_ActiveDocumentChanged;

            ProjectManager.Instance.ProjectLoaded += ProjectLoaded;
            ProjectManager.Instance.ProjectClosed += ProjectClosed;
            ProjectManager.Instance.ProjectChanged += ProjectChanged;
            
            MapManager.Instance.MapSelected += MapSelected;
            MapManager.Instance.ToolSelected += ToolSelected;

            _dockHistory.UndoRedoChanged += UndoRedoChanged;
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the ProjectLoaded event of the <see cref="ProjectManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectLoadedEventArgs"/> instance containing the event data.</param>
        private void ProjectLoaded(object sender, ProjectLoadedEventArgs e)
        {
            Text = @"Crystal Toolset - " + e.Project.Name;
            EnableToolset();
        }

        /// <summary>
        /// Handles the ProjectClosed event of the <see cref="ProjectManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectClosedEventArgs"/> instance containing the event data.</param>
        private void ProjectClosed(object sender, ProjectClosedEventArgs e)
        {
            Text = @"Crystal Toolset";
            DisableToolset();
        }

        /// <summary>
        /// Handles the ProjectChanged event of the <see cref="ProjectManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectChangedEventArgs"/> instance containing the event data.</param>
        private void ProjectChanged(object sender, ProjectChangedEventArgs e)
        {
            Text = @"Crystal Toolset - " + e.Project.Name;
        }

        /// <summary>
        /// Handles the MapSelected event of the <see cref="MapManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapSelectedEventArgs"/> instance containing the event data.</param>
        private void MapSelected(object sender, MapSelectedEventArgs e)
        {
            if (e.Map == null) return;

            var doc = FindDocument(e.Map.Name);
            if (doc != null)
            {
                doc.DockHandler.Show();
                return;
            }

            var dockMap = new DockMap(e.Map);
            dockMap.Show(dockPanel);
        }

        /// <summary>
        /// Handles the ToolSelected event of the <see cref="MapManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ToolSelectedEventArgs"/> instance containing the event data.</param>
        private void ToolSelected(object sender, ToolSelectedEventArgs e)
        {
            if (e.Tool == EditorTool.Brush)
            {
                if (mnuBrush.Checked) return;
                UncheckAllTools();
                mnuBrush.Checked = true;
                btnBrush.Checked = true;
            }

            if (e.Tool == EditorTool.Terrain)
            {
                if (mnuTerrain.Checked) return;
                UncheckAllTools();
                mnuTerrain.Checked = true;
                btnTerrain.Checked = true;
            }

            if (e.Tool == EditorTool.Eraser)
            {
                if (mnuEraser.Checked) return;
                UncheckAllTools();
                mnuEraser.Checked = true;
                btnEraser.Checked = true;
            }

            if (e.Tool == EditorTool.Fill)
            {
                if (mnuFill.Checked) return;
                UncheckAllTools();
                mnuFill.Checked = true;
                btnFill.Checked = true;
            }

            if (e.Tool == EditorTool.Rectangle)
            {
                if (mnuRectangle.Checked) return;
                UncheckAllTools();
                mnuRectangle.Checked = true;
                btnRectangle.Checked = true;
            }

            if (e.Tool == EditorTool.Eyedropper)
            {
                if (mnuEyedropper.Checked) return;
                UncheckAllTools();
                mnuEyedropper.Checked = true;
                btnEyedropper.Checked = true;
            }
        }

        private void UndoRedoChanged(object sender, EventArgs e)
        {
            btnUndo.Enabled = _dockHistory.UndoEnabled;
            btnRedo.Enabled = _dockHistory.RedoEnabled;

            mnuUndo.Enabled = _dockHistory.UndoEnabled;
            mnuRedo.Enabled = _dockHistory.RedoEnabled;
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Handles the Load event of the <see cref="MainForm"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            var bounds = DesktopBounds;

            if (Properties.Settings.Default.Location.X == 0 && Properties.Settings.Default.Location.Y == 0)
                Properties.Settings.Default.Location = bounds.Location;

            if (Properties.Settings.Default.Size.Width == 0 && Properties.Settings.Default.Size.Height == 0)
                Properties.Settings.Default.Size = bounds.Size;

            DesktopBounds = new Rectangle(Properties.Settings.Default.Location, Properties.Settings.Default.Size);

            if (Properties.Settings.Default.Maximised)
                WindowState = FormWindowState.Maximized;

            if (!SerializeDockPanel())
            {
                dockPanel.DockLeftPortion = 300;
                dockPanel.DockRightPortion = 300;
                dockPanel.DockBottomPortion = 300;
                _dockTileset.Show(dockPanel);
                _dockProject.Show(dockPanel);
                _dockLayers.Show(dockPanel);
                _dockHistory.Show(dockPanel);
                _dockConsole.Show(dockPanel);
                _dockTerrain.Show(dockPanel);
            }

            BuildViewMenu();
        }

        /// <summary>
        /// Handles the FormClosing event of the <see cref="MainForm"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = !ProjectManager.Instance.CloseProject();
            }

            DeserializeDockPanel();

            var bounds = WindowState != FormWindowState.Normal ? RestoreBounds : DesktopBounds;

            Properties.Settings.Default.Location = bounds.Location;
            Properties.Settings.Default.Size = bounds.Size;
            Properties.Settings.Default.Maximised = WindowState == FormWindowState.Maximized;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Sets the Checked property of the View menu subitems.
        /// </summary>
        public void BuildViewMenu()
        {
            mnuViewTileset.Checked = _dockTileset.Visible;
            mnuViewProject.Checked = _dockProject.Visible;
            mnuViewLayers.Checked = _dockLayers.Visible;
            mnuViewHistory.Checked = _dockHistory.Visible;
            mnuViewConsole.Checked = _dockConsole.Visible;
            mnuViewTerrain.Checked = _dockTerrain.Visible;
        }

        /// <summary>
        /// Enables all project-specific options in the <see cref="MainForm"/> form.
        /// </summary>
        private void EnableToolset()
        {
            mnuNewMap.Enabled = true;
            mnuNewTileset.Enabled = true;
            mnuNewTerrain.Enabled = true;
            mnuCloseProject.Enabled = true;
            mnuSave.Enabled = true;
            mnuPlay.Enabled = true;

            btnNew.Enabled = true;
            btnNewMap.Enabled = true;
            btnNewTileset.Enabled = true;
            btnNewTerrain.Enabled = true;
            btnSave.Enabled = true;

            mnuBrush.Enabled = true;
            mnuTerrain.Enabled = true;
            mnuEraser.Enabled = true;
            mnuFill.Enabled = true;

            btnBrush.Enabled = true;
            btnTerrain.Enabled = true;
            btnEraser.Enabled = true;
            btnFill.Enabled = true;

            mnuBrush.Checked = true;
            btnBrush.Checked = true;

            btnPlay.Enabled = true;
        }

        /// <summary>
        /// Disables all project-specific options in the <see cref="MainForm"/> form.
        /// </summary>
        private void DisableToolset()
        {
            CloseAllMaps();

            mnuNewMap.Enabled = false;
            mnuNewTileset.Enabled = false;
            mnuNewTerrain.Enabled = false;
            mnuCloseProject.Enabled = false;
            mnuSave.Enabled = false;
            mnuExport.Enabled = false;
            mnuExportMap.Enabled = false;
            mnuPlay.Enabled = false;

            mnuPreferences.Enabled = false;
            mnuUndo.Enabled = false;
            mnuRedo.Enabled = false;
            mnuCopy.Enabled = false;
            mnuCut.Enabled = false;
            mnuPaste.Enabled = false;

            mnuBrush.Enabled = false;
            mnuTerrain.Enabled = false;
            mnuEraser.Enabled = false;
            mnuFill.Enabled = false;
            mnuRectangle.Enabled = false;
            mnuEyedropper.Enabled = false;

            mnuBrush.Checked = false;
            mnuEraser.Checked = false;
            mnuFill.Checked = false;
            mnuRectangle.Checked = false;
            mnuEyedropper.Checked = false;

            mnuDatabase.Enabled = false;
            mnuScripts.Enabled = false;
            mnuSound.Enabled = false;
            mnuTextures.Enabled = false;

            btnNew.Enabled = false;
            btnNewMap.Enabled = false;
            btnNewTileset.Enabled = false;
            btnNewTerrain.Enabled = false;
            btnSave.Enabled = false;

            btnUndo.Enabled = false;
            btnRedo.Enabled = false;
            btnCopy.Enabled = false;
            btnCut.Enabled = false;
            btnPaste.Enabled = false;

            btnBrush.Enabled = false;
            btnTerrain.Enabled = false;
            btnEraser.Enabled = false;
            btnFill.Enabled = false;
            btnRectangle.Enabled = false;
            btnEyedropper.Enabled = false;

            btnBrush.Checked = false;
            btnEraser.Checked = false;
            btnFill.Checked = false;
            btnRectangle.Checked = false;
            btnEyedropper.Checked = false;

            btnDatabase.Enabled = false;
            btnScripts.Enabled = false;
            btnSound.Enabled = false;
            btnTextures.Enabled = false;

            btnPlay.Enabled = false;
        }

        private void UncheckAllTools()
        {
            mnuBrush.Checked = false;
            mnuTerrain.Checked = false;
            mnuEraser.Checked = false;
            mnuFill.Checked = false;
            mnuRectangle.Checked = false;
            mnuEyedropper.Checked = false;

            btnBrush.Checked = false;
            btnTerrain.Checked = false;
            btnEraser.Checked = false;
            btnFill.Checked = false;
            btnRectangle.Checked = false;
            btnEyedropper.Checked = false;
        }

        #endregion

        #region Menu Event Handler Region

        /// <summary>
        /// Handles the DropDownOpening event of the <see cref="mnuFile"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuFile_DropDownOpening(object sender, EventArgs e)
        {
            mnuClose.Enabled = dockPanel.ActiveDocument != null;
            if (dockPanel.ActiveDocument != null)
            {
                var name = dockPanel.ActiveDocument.DockHandler.TabText;
                if (name.Substring(name.Length - 1, 1) == "*")
                    name = name.Substring(0, name.Length - 1);
                mnuSave.Text = @"Save " + name;
                mnuSave.Enabled = true;
            }
            else
            {
                mnuSave.Text = @"Save";
                mnuSave.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuNewGame"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuNewGame_Click(object sender, EventArgs e)
        {
            ProjectManager.Instance.NewProject();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuNewMap"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuNewMap_Click(object sender, EventArgs e)
        {
            MapManager.Instance.NewMap();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuNewTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuNewTileset_Click(object sender, EventArgs e)
        {
            TilesetManager.Instance.NewTileset();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuNewTerrain"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuNewTerrain_Click(object sender, EventArgs e)
        {
            TerrainManager.Instance.NewTerrain();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuOpen"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            ProjectManager.Instance.LoadProject();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuSave"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuSave_Click(object sender, EventArgs e)
        {
            var name = GetOpenMap();
            if (name == null) return;

            var map = MapManager.Instance.GetMap(name);
            if (map == null) return;

            if (map.UnsavedChanges)
                MapManager.Instance.SaveMap(map.ID);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuSaveAll"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuSaveAll_Click(object sender, EventArgs e)
        {
            MapManager.Instance.SaveMaps();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuClose"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuClose_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveDocument != null)
                dockPanel.ActiveDocument.DockHandler.Close();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuCloseProject"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuCloseProject_Click(object sender, EventArgs e)
        {
            ProjectManager.Instance.CloseProject();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuPlay"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuPlay_Click(object sender, EventArgs e)
        {
            ProjectManager.Instance.Play(this);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuExit"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (ProjectManager.Instance.CloseProject())
                Close();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuViewTileset"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuViewTileset_Click(object sender, EventArgs e)
        {
            if (_dockTileset.Visible)
                _dockTileset.Hide();
            else
                _dockTileset.Show(dockPanel);

            BuildViewMenu();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuViewProject"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuViewProject_Click(object sender, EventArgs e)
        {
            if (_dockProject.Visible)
                _dockProject.Hide();
            else
                _dockProject.Show(dockPanel);

            BuildViewMenu();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuViewLayers"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuViewLayers_Click(object sender, EventArgs e)
        {
            if (_dockLayers.Visible)
                _dockLayers.Hide();
            else
                _dockLayers.Show(dockPanel);

            BuildViewMenu();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuViewHistory"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuViewHistory_Click(object sender, EventArgs e)
        {
            if (_dockHistory.Visible)
                _dockHistory.Hide();
            else
                _dockHistory.Show(dockPanel);

            BuildViewMenu();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuViewConsole"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuViewConsole_Click(object sender, EventArgs e)
        {
            if (_dockConsole.Visible)
                _dockConsole.Hide();
            else
                _dockConsole.Show(dockPanel);

            BuildViewMenu();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuViewTerrain"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuViewTerrain_Click(object sender, EventArgs e)
        {
            if (_dockTerrain.Visible)
                _dockTerrain.Hide();
            else
                _dockTerrain.Show(dockPanel);

            BuildViewMenu();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuAbout"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            using (var dlg = new DialogAbout())
            {
                dlg.ShowDialog(this);
            }
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuBrush"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuBrush_Click(object sender, EventArgs e)
        {
            MapManager.Instance.SelectTool(EditorTool.Brush);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuTerrain"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuTerrain_Click(object sender, EventArgs e)
        {
            MapManager.Instance.SelectTool(EditorTool.Terrain);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuEraser"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuEraser_Click(object sender, EventArgs e)
        {
            MapManager.Instance.SelectTool(EditorTool.Eraser);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuFill"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuFill_Click(object sender, EventArgs e)
        {
            MapManager.Instance.SelectTool(EditorTool.Fill);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuRectangle"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuRectangle_Click(object sender, EventArgs e)
        {
            MapManager.Instance.SelectTool(EditorTool.Rectangle);
        }

        /// <summary>
        /// Handles the Click event of the <see cref="mnuEyedropper"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuEyedropper_Click(object sender, EventArgs e)
        {
            MapManager.Instance.SelectTool(EditorTool.Eyedropper);
        }

        #endregion

        #region Dock Panel Serialization Region

        /// <summary>
        /// Serializes the <see cref="dockPanel"/> control.
        /// </summary>
        /// <returns>Returns true if successful, false if unsuccessful.</returns>
        private bool SerializeDockPanel()
        {
            var configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "dockpanel.config");
            if (File.Exists(configFile))
            {
                dockPanel.LoadFromXml(configFile, _deserializeDockContent);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deserializes the <see cref="dockPanel"/> control.
        /// </summary>
        private void DeserializeDockPanel()
        {
            var configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "dockpanel.config");
            dockPanel.SaveAsXml(configFile);
        }

        /// <summary>
        /// Returns control references based on the content strings passed as a parameter.
        /// </summary>
        /// <param name="content">Class name string.</param>
        /// <returns>Reference to an object if it exists.</returns>
        private IDockContent GetDockContentFromString(string content)
        {
            if (content == typeof(DockTileset).ToString())
                return _dockTileset;
            if (content == typeof(DockProject).ToString())
                return _dockProject;
            if (content == typeof(DockLayers).ToString())
                return _dockLayers;
            if (content == typeof(DockHistory).ToString())
                return _dockHistory;
            if (content == typeof(DockConsole).ToString())
                return _dockConsole;
            if (content == typeof(DockTerrain).ToString())
                return _dockTerrain;

            return null;
        }

        #endregion

        #region Dock Panel Management Region

        /// <summary>
        /// Handles the ContentAdded event of the <see cref="dockPanel"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DockContentEventArgs"/> instance containing the event data.</param>
        private void dockPanel_ContentAdded(object sender, DockContentEventArgs e)
        {
            CacheOpenMaps();
        }

        /// <summary>
        /// Handles the ContentRemoved event of the <see cref="dockPanel"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DockContentEventArgs"/> instance containing the event data.</param>
        private void dockPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            CacheOpenMaps();
        }

        /// <summary>
        /// Handles the ActiveDocumentChanged event of the <see cref="dockPanel"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            CacheOpenMaps();

            if (dockPanel.ActiveDocument != null)
            {
                var doc = dockPanel.ActiveDocument as DockMap;
                if (doc == null)
                {
                    MapManager.Instance.SelectMap(0);
                    return;
                }
                var name = doc.Text;
                var ID = MapManager.Instance.GetMapID(name);
                MapManager.Instance.SelectMap(ID);
            }
            else
            {
                MapManager.Instance.SelectMap(0);
            }
        }

        /// <summary>
        /// Loops through all opened documents in the <see cref="dockPanel"/> control and compiles a list of all open maps.
        /// </summary>
        private void CacheOpenMaps()
        {
            if (ProjectManager.Instance.Project == null) return;
            if (ProjectManager.Instance.Settings.Ignore) return;

            var openMaps = new List<string>();

            ProjectManager.Instance.Settings.SelectedFile = null;

            for (int i = 0; i < dockPanel.Contents.Count; i++)
            {
                var map = dockPanel.Contents[i] as DockMap;
                if (map == null) continue;
                openMaps.Add(map.Text);

                if (dockPanel.ActiveDocument != null)
                {
                    var active = dockPanel.ActiveDocument as DockMap;
                    if (active == null) continue;
                    if (active.Text != "")
                        ProjectManager.Instance.Settings.SelectedFile = active.Text;
                }
            }

            ProjectManager.Instance.Settings.OpenFiles = openMaps;
        }

        /// <summary>
        /// Finds an open document by name.
        /// </summary>
        /// <param name="text">Name of the document.</param>
        /// <returns>Returns the document found.</returns>
        private IDockContent FindDocument(string text)
        {
            foreach (IDockContent content in dockPanel.Documents)
            {
                var doc = content as DockMap;
                if (doc == null) continue;
                if (doc.Text == text) return content;
            }
            return null;
        }

        /// <summary>
        /// Closes all open maps.
        /// </summary>
        private void CloseAllMaps()
        {
            for (int index = dockPanel.Contents.Count - 1; index >= 0; index--)
            {
                var map = dockPanel.Contents[index] as DockMap;
                if (map == null) continue;
                var content = dockPanel.Contents[index];
                content.DockHandler.Close();
            }
        }

        private string GetOpenMap()
        {
            if (dockPanel.ActiveDocument != null)
            {
                var active = dockPanel.ActiveDocument as DockMap;
                if (active != null)
                {
                    if (active.Text != "")
                    {
                        return active.Text;
                    }
                }
            }
            return null;
        }

        #endregion
    }
}