using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Toolset.Controls.Sorting;
using Toolset.Managers;

namespace Toolset.Docking
{
    public partial class DockProject : ToolWindow
    {
        #region Field Region

        private string _projectName;
        private bool _isCaching;

        private TreeNode projectNode;
        private TreeNode mapsNode;
        private TreeNode tilesetsNode;
        private TreeNode terrainNode;

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DockProject"/> form.
        /// </summary>
        public DockProject()
        {
            InitializeComponent();

            Disable();

            Load += DockProject_Load;

            treeProject.Sorted = true;
            treeProject.DoubleClick += treeProject_DoubleClick;
            treeProject.MouseDown += treeProject_MouseDown;
            treeProject.AfterExpand += treeProject_AfterExpand;
            treeProject.AfterCollapse += treeProject_AfterExpand;

            btnNewMap.Click += btnNewMap_Click;
            btnNewTileset.Click += btnNewTileset_Click;
            btnNewTerrain.Click += btnNewTerrain_Click;
            btnOpen.Click += btnOpen_Click;
            btnRename.Click += btnRename_Click;
            btnProperties.Click += btnProperties_Click;
            btnDelete.Click += btnDelete_Click;

            mnuOpen.Click += btnOpen_Click;
            mnuRename.Click += btnRename_Click;
            mnuProperties.Click += btnProperties_Click;
            mnuDelete.Click += btnDelete_Click;

            ProjectManager.Instance.ProjectLoaded += ProjectLoaded;
            ProjectManager.Instance.ProjectClosed += ProjectClosed;
            ProjectManager.Instance.ProjectChanged += ProjectChanged;
            ProjectManager.Instance.SettingsLoaded += SettingsLoaded;

            TilesetManager.Instance.TilesetLoaded += TilesetLoaded;
            TilesetManager.Instance.TilesetAdded += TilesetAdded;
            TilesetManager.Instance.TilesetDeleted += TilesetDeleted;
            TilesetManager.Instance.TilesetChanged += TilesetChanged;

            MapManager.Instance.MapLoaded += MapLoaded;
            MapManager.Instance.MapAdded += MapAdded;
            MapManager.Instance.MapDeleted += MapDeleted;
            MapManager.Instance.MapChanged += MapChanged;

            TerrainManager.Instance.TerrainLoaded += TerrainLoaded;
            TerrainManager.Instance.TerrainAdded += TerrainAdded;
            TerrainManager.Instance.TerrainDeleted += TerrainDeleted;
            TerrainManager.Instance.TerrainChanged += TerrainChanged;

            treeProject.TreeViewNodeSorter = new TreeNodeSorter();

            BuildNodes();
        }

        #endregion

        #region Form Event Handler Region

        /// <summary>
        /// Handles the Load event of the <see cref="DockProject"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DockProject_Load(object sender, EventArgs e)
        {
            
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
            _projectName = e.Project.Name;
            Enable();
            BuildTreeView();
        }

        /// <summary>
        /// Handles the ProjectClosed event of the <see cref="ProjectManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectClosedEventArgs"/> instance containing the event data.</param>
        private void ProjectClosed(object sender, ProjectClosedEventArgs e)
        {
            _projectName = null;
            Disable();
            _isCaching = true;
        }

        /// <summary>
        /// Handles the ProjectChanged event of the <see cref="ProjectManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectChangedEventArgs"/> instance containing the event data.</param>
        private void ProjectChanged(object sender, ProjectChangedEventArgs e)
        {
            _projectName = e.Project.Name;
            projectNode.Text = _projectName;
        }

        /// <summary>
        /// Handles the SettingsLoaded event of the <see cref="ProjectManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SettingsLoadedEventArgs"/> instance containing the event data.</param>
        private void SettingsLoaded(object sender, SettingsLoadedEventArgs e)
        {
            ExpandNodes(e.Settings.ExpandedNodes);
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
                tilesetsNode.Nodes.Add(new TreeNode
                {
                    Name = "Tileset" + tileset.Name,
                    Text = tileset.Name,
                    SelectedImageIndex = 3,
                    ImageIndex = 3
                });
            }

            SortNodes();
        }

        /// <summary>
        /// Handles the TilesetAdded event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetAddedEventArgs"/> instance containing the event data.</param>
        private void TilesetAdded(object sender, TilesetAddedEventArgs e)
        {
            tilesetsNode.Nodes.Add(new TreeNode
            {
                Name = "Tileset" + e.Tileset.Name,
                Text = e.Tileset.Name,
                SelectedImageIndex = 3,
                ImageIndex = 3
            });

            SortNodes("Tileset" + e.Tileset.Name);
        }

        /// <summary>
        /// Handles the TilesetDeleted event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetDeletedEventArgs"/> instance containing the event data.</param>
        private void TilesetDeleted(object sender, TilesetDeletedEventArgs e)
        {
            string selectednode = null;

            if (treeProject.SelectedNode.Name == "Tileset" + e.Name)
                selectednode = tilesetsNode.Name;

            foreach (TreeNode node in tilesetsNode.Nodes)
            {
                if (node != null)
                {
                    if (node.Text == e.Name)
                        tilesetsNode.Nodes.Remove(node);
                }
            }

            SortNodes(selectednode);
        }

        /// <summary>
        /// Handles the TilesetChanged event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetChangedEventArgs"/> instance containing the event data.</param>
        private void TilesetChanged(object sender, TilesetChangedEventArgs e)
        {
            foreach (TreeNode node in tilesetsNode.Nodes)
            {
                if (node.Text == e.OldTileset.Name)
                {
                    node.Name = "Tileset" + e.NewTileset.Name;
                    node.Text = e.NewTileset.Name;
                }
            }

            if (treeProject.SelectedNode.Name == "Tileset" + e.OldTileset.Name)
                SortNodes("Tileset" + e.NewTileset.Name);
            else
                SortNodes();
        }

        /// <summary>
        /// Handles the MapLoaded event of the <see cref="MapManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapLoadedEventArgs"/> instance containing the event data.</param>
        private void MapLoaded(object sender, MapLoadedEventArgs e)
        {
            foreach (var map in e.Maps)
            {
                mapsNode.Nodes.Add(new TreeNode
                {
                    Name = "Map" + map.Name,
                    Text = map.Name,
                    SelectedImageIndex = 2,
                    ImageIndex = 2
                });
            }

            SortNodes();
        }

        /// <summary>
        /// Handles the MapAdded event of the <see cref="MapManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapAddedEventArgs"/> instance containing the event data.</param>
        private void MapAdded(object sender, MapAddedEventArgs e)
        {
            mapsNode.Nodes.Add(new TreeNode
            {
                Name = "Map" + e.Map.Name,
                Text = e.Map.Name,
                SelectedImageIndex = 2,
                ImageIndex = 2
            });

            SortNodes("Map" + e.Map.Name);
        }

        /// <summary>
        /// Handles the MapDeleted event of the <see cref="MapManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapDeletedEventArgs"/> instance containing the event data.</param>
        private void MapDeleted(object sender, MapDeletedEventArgs e)
        {
            string selectednode = null;

            if (treeProject.SelectedNode.Name == "Map" + e.Name)
                selectednode = (mapsNode.Name);

            foreach (TreeNode node in mapsNode.Nodes)
            {
                if (node != null)
                {
                    if (node.Text == e.Name)
                        mapsNode.Nodes.Remove(node);
                }
            }

            SortNodes(selectednode);
        }

        /// <summary>
        /// Handles the MapChanged event of the <see cref="MapManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MapChangedEventArgs"/> instance containing the event data.</param>
        private void MapChanged(object sender, MapChangedEventArgs e)
        {
            foreach (TreeNode node in mapsNode.Nodes)
            {
                if (node.Text == e.OldMap.Name)
                {
                    node.Name = "Map" + e.NewMap.Name;
                    node.Text = e.NewMap.Name;
                }
            }

            if (treeProject.SelectedNode.Name == "Map" + e.OldMap.Name)
                SortNodes("Map" + e.NewMap.Name);
            else
                SortNodes();
        }

        /// <summary>
        /// Handles the TerrainLoaded event of the <see cref="TerrainManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainLoadedEventArgs"/> instance containing the event data.</param>
        private void TerrainLoaded(object sender, TerrainLoadedEventArgs e)
        {
            foreach (var terrain in e.Terrain)
            {
                terrainNode.Nodes.Add(new TreeNode
                {
                    Name = "Terrain" + terrain.Name,
                    Text = terrain.Name,
                    SelectedImageIndex = 4,
                    ImageIndex = 4
                });
            }

            SortNodes();
        }

        /// <summary>
        /// Handles the TerrainAdded event of the <see cref="TilesetManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainAddedEventArgs"/> instance containing the event data.</param>
        private void TerrainAdded(object sender, TerrainAddedEventArgs e)
        {
            terrainNode.Nodes.Add(new TreeNode
            {
                Name = "Terrain" + e.TerrainTile.Name,
                Text = e.TerrainTile.Name,
                SelectedImageIndex = 4,
                ImageIndex = 4
            });

            SortNodes("Terrain" + e.TerrainTile.Name);
        }

        /// <summary>
        /// Handles the TerrainDeleted event of the <see cref="TerrainManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainDeletedEventArgs"/> instance containing the event data.</param>
        private void TerrainDeleted(object sender, TerrainDeletedEventArgs e)
        {
            string selectednode = null;

            if (treeProject.SelectedNode.Name == "Terrain" + e.Name)
                selectednode = terrainNode.Name;

            foreach (TreeNode node in terrainNode.Nodes)
            {
                if (node != null)
                {
                    if (node.Text == e.Name)
                        terrainNode.Nodes.Remove(node);
                }
            }

            SortNodes(selectednode);
        }

        /// <summary>
        /// Handles the TerrainChanged event of the <see cref="TerrainManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainChangedEventArgs"/> instance containing the event data.</param>
        private void TerrainChanged(object sender, TerrainChangedEventArgs e)
        {
            foreach (TreeNode node in terrainNode.Nodes)
            {
                if (node.Text == e.OldTerrain.Name)
                {
                    node.Name = "Terrain" + e.NewTerrain.Name;
                    node.Text = e.NewTerrain.Name;
                }
            }

            if (treeProject.SelectedNode.Name == "Terrain" + e.OldTerrain.Name)
                SortNodes("Terrain" + e.NewTerrain.Name);
            else
                SortNodes();
        }

        #endregion

        #region Toolstrip Event Handler Region

        /// <summary>
        /// Handles the Click event of the <see cref="btnNewMap"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNewMap_Click(object sender, EventArgs e)
        {
            MapManager.Instance.NewMap();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnNewTileset"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNewTileset_Click(object sender, EventArgs e)
        {
            TilesetManager.Instance.NewTileset();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnNewTerrain"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNewTerrain_Click(object sender, EventArgs e)
        {
            TerrainManager.Instance.NewTerrain();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnOpen"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenNode();
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnRename"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnRename_Click(object sender, EventArgs e)
        {
            if (treeProject.SelectedNode == null) return;

            if (IsProjectSelected())
                ProjectManager.Instance.RenameProject();
            if (IsTilesetSelected())
                TilesetManager.Instance.RenameTileset(TilesetManager.Instance.GetTilesetID(treeProject.SelectedNode.Text));
            if (IsMapSelected())
                MapManager.Instance.RenameMap(MapManager.Instance.GetMapID(treeProject.SelectedNode.Text));
            if (IsTerrainSelected())
                TerrainManager.Instance.RenameTerrain(TerrainManager.Instance.GetTerrainID(treeProject.SelectedNode.Text));
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnProperties"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnProperties_Click(object sender, EventArgs e)
        {
            if (treeProject.SelectedNode == null) return;

            if (IsProjectSelected())
                ProjectManager.Instance.EditProject();
            if (IsTilesetSelected())
                TilesetManager.Instance.EditTileset(TilesetManager.Instance.GetTilesetID(treeProject.SelectedNode.Text));
            if (IsMapSelected())
                MapManager.Instance.EditMap(MapManager.Instance.GetMapID(treeProject.SelectedNode.Text));
            if (IsTerrainSelected())
                TerrainManager.Instance.EditTerrain(TerrainManager.Instance.GetTerrainID(treeProject.SelectedNode.Text));
        }

        /// <summary>
        /// Handles the Click event of the <see cref="btnDelete"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (treeProject.SelectedNode == null) return;

            if (IsTilesetSelected())
                TilesetManager.Instance.DeleteTileset(TilesetManager.Instance.GetTilesetID(treeProject.SelectedNode.Text));
            if (IsMapSelected())
                MapManager.Instance.DeleteMap(MapManager.Instance.GetMapID(treeProject.SelectedNode.Text));
            if (IsTerrainSelected())
                TerrainManager.Instance.DeleteTerrain(TerrainManager.Instance.GetTerrainID(treeProject.SelectedNode.Text));
        }

        #endregion

        #region Form Management Region

        /// <summary>
        /// Calls various 'open' functions depending on node type.
        /// </summary>
        private void OpenNode()
        {
            if (treeProject.SelectedNode == null) return;

            if (IsTilesetSelected())
                TilesetManager.Instance.SelectTileset(TilesetManager.Instance.GetTilesetID(treeProject.SelectedNode.Text));
            if (IsMapSelected())
                MapManager.Instance.SelectMap(MapManager.Instance.GetMapID(treeProject.SelectedNode.Text));
            if (IsTerrainSelected())
                TerrainManager.Instance.SelectTerrain(TerrainManager.Instance.GetTerrainID(treeProject.SelectedNode.Text));
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        private void Enable()
        {
            toolStripMain.Enabled = true;
            treeProject.Enabled = true;
            treeProject.BackColor = Color.White;
        }

        /// <summary>
        /// Disables the controls.
        /// </summary>
        private void Disable()
        {
            toolStripMain.Enabled = false;
            treeProject.Enabled = false;
            treeProject.BackColor = SystemColors.Control;

            if (mapsNode != null)
                mapsNode.Nodes.Clear();

            if (tilesetsNode != null)
                tilesetsNode.Nodes.Clear();

            if (terrainNode != null)
                terrainNode.Nodes.Clear();

            Clear();
        }

        /// <summary>
        /// Clears the nodes on the <see cref="treeProject"/> control.
        /// </summary>
        private void Clear()
        {
            treeProject.Nodes.Clear();
        }

        /// <summary>
        /// Enables/Disables specific buttons on the <see cref="toolStripMain"/> control.
        /// </summary>
        private void BuildMenu()
        {
            btnDelete.Enabled = false;
            btnRename.Enabled = false;
            btnOpen.Enabled = false;
            btnProperties.Enabled = false;

            mnuDelete.Enabled = false;
            mnuOpen.Enabled = false;
            mnuProperties.Enabled = false;
            mnuRename.Enabled = false;

            if (IsProjectSelected())
            {
                btnRename.Enabled = true;
                btnProperties.Enabled = true;

                mnuRename.Enabled = true;
                mnuProperties.Enabled = true;
            }

            if (IsTilesetSelected() || IsMapSelected() || IsTerrainSelected())
            {
                btnRename.Enabled = true;
                btnProperties.Enabled = true;
                btnOpen.Enabled = true;
                btnDelete.Enabled = true;

                mnuRename.Enabled = true;
                mnuProperties.Enabled = true;
                mnuOpen.Enabled = true;
                mnuDelete.Enabled = true;
            }
        }

        #endregion

        #region Tree View Management Region

        /// <summary>
        /// Sorts the nodes and selects the node with the name passed through.
        /// </summary>
        /// <param name="SelectedNode">The name of the selected node.</param>
        private void SortNodes(string SelectedNode = null)
        {
            if (SelectedNode == null)
            {
                if (treeProject.SelectedNode != null)
                    SelectedNode = treeProject.SelectedNode.Name;
            }

            treeProject.Sort();

            if (SelectedNode == null)
            {
                treeProject.SelectedNode = projectNode;
                return;
            }

            foreach (TreeNode root in treeProject.Nodes)
            {
                SelectNode(root, SelectedNode);
            }
        }

        /// <summary>
        /// Recursively checks through for our selected node and selects it.
        /// </summary>
        /// <param name="root">Root node to check.</param>
        /// <param name="name">Name of the node to select.</param>
        private void SelectNode(TreeNode root, string name)
        {
            if (root.Name == name)
            {
                treeProject.SelectedNode = root;
                return;
            }

            foreach (TreeNode childnode in root.Nodes)
            {
                if (childnode.Name == name)
                {
                    treeProject.SelectedNode = childnode;
                    return;
                }
                SelectNode(childnode, name);
            }
        }

        /// <summary>
        /// Builds the core nodes.
        /// </summary>
        private void BuildNodes()
        {
            projectNode = new TreeNode
            {
                Name = "Project",
                Text = @"Project Name",
                SelectedImageIndex = 0,
                ImageIndex = 0
            };

            mapsNode = new TreeNode
            {
                Name = "Maps",
                Text = @"Maps",
                SelectedImageIndex = 1,
                ImageIndex = 1
            };

            tilesetsNode = new TreeNode
            {
                Name = "Tilesets",
                Text = @"Tilesets",
                SelectedImageIndex = 1,
                ImageIndex = 1
            };

            terrainNode = new TreeNode
            {
                Name = "Terrain",
                Text = @"Terrain",
                SelectedImageIndex = 1,
                ImageIndex = 1
            };

            projectNode.Nodes.Add(mapsNode);
            projectNode.Nodes.Add(tilesetsNode);
            projectNode.Nodes.Add(terrainNode);
        }

        /// <summary>
        /// Updates the <see cref="projectNode"/> and deserializes the node states.
        /// </summary>
        public void BuildTreeView()
        {
            treeProject.BeginUpdate();

            projectNode.Text = _projectName;

            Clear();

            treeProject.Nodes.Add(projectNode);
            treeProject.SelectedNode = projectNode;

            treeProject.EndUpdate();
        }

        /// <summary>
        /// Checks if the currently selected node in the <see cref="treeProject"/> control is that root project node.
        /// </summary>
        /// <returns>True of node is root project node.</returns>
        private bool IsProjectSelected()
        {
            if (treeProject.SelectedNode == null) return false;
            return treeProject.SelectedNode.Name == @"Project";
        }

        /// <summary>
        /// Checks if the currently selected node in the <see cref="treeProject"/> control is that 'Maps'.
        /// </summary>
        /// <returns>True of node is 'maps' node.</returns>
        private bool IsMapSelected()
        {
            if (treeProject.SelectedNode == null) return false;

            if (treeProject.SelectedNode.Parent == null)
                return false;

            return treeProject.SelectedNode.Parent.Name == @"Maps";
        }

        /// <summary>
        /// Checks if the currently selected node in the <see cref="treeProject"/> control is that 'Tilesets'.
        /// </summary>
        /// <returns>True of node is 'tilesets' node.</returns>
        private bool IsTilesetSelected()
        {
            if (treeProject.SelectedNode == null) return false;

            if (treeProject.SelectedNode.Parent == null)
                return false;

            return treeProject.SelectedNode.Parent.Name == @"Tilesets";
        }

        /// <summary>
        /// Checks if the currently selected node in the <see cref="treeProject"/> control is that 'Terrain'.
        /// </summary>
        /// <returns>True of node is 'terrain' node.</returns>
        private bool IsTerrainSelected()
        {
            if (treeProject.SelectedNode == null) return false;

            if (treeProject.SelectedNode.Parent == null)
                return false;

            return treeProject.SelectedNode.Parent.Name == @"Terrain";
        }

        /// <summary>
        /// Checks if a node is expanded.
        /// </summary>
        /// <param name="name">Text to compare to the Name property of the node.</param>
        /// <param name="isRoot">Flag to check root project node.</param>
        /// <returns>True if node is expanded, false if not.</returns>
        private bool IsNodeExpanded(string name, bool isRoot)
        {
            if (isRoot)
            {
                foreach (TreeNode node in treeProject.Nodes)
                {
                    if (node.Name == name)
                        return node.IsExpanded;
                }
            }
            else
            {
                foreach (TreeNode node in treeProject.Nodes[0].Nodes)
                {
                    if (node.Name == name)
                        return node.IsExpanded;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets a node's expanded property to true.
        /// </summary>
        /// <param name="name">Text to compare to the Name property of the node.</param>
        /// <param name="isRoot">Flag to set root project node.</param>
        private void SetNodeExpanded(string name, bool isRoot)
        {
            if (isRoot)
            {
                foreach (TreeNode node in treeProject.Nodes)
                {
                    if (node.Name == name)
                        node.Expand();
                }
            }
            else
            {
                foreach (TreeNode node in treeProject.Nodes[0].Nodes)
                {
                    if (node.Name == name)
                        node.Expand();
                }
            }
        }

        /// <summary>
        /// Expands all nodes in the <see cref="treeProject"/> control based on the <see cref="NodeStates"/> object.
        /// </summary>
        private void ExpandNodes(List<string> NodeStates)
        {
            if (NodeStates == null) return;

            if (NodeStates.Count == 0) return;

            _isCaching = true;

            if (NodeStates.Contains("Project"))
                SetNodeExpanded("Project", true);

            if (NodeStates.Contains("Maps"))
                SetNodeExpanded("Maps", false);

            if (NodeStates.Contains("Tilesets"))
                SetNodeExpanded("Tilesets", false);

            if (NodeStates.Contains("Terrain"))
                SetNodeExpanded("Terrain", false);

            if (treeProject.SelectedNode != null)
                treeProject.SelectedNode.EnsureVisible();

            _isCaching = false;
        }

        /// <summary>
        /// Serializes the expanded state of all nodes in the <see cref="treeProject"/> to the <see cref="NodeStates"/> object.
        /// </summary>
        private void CacheNodeStates()
        {
            ProjectManager.Instance.Settings.ExpandedNodes = new List<String>();

            if (IsNodeExpanded("Project", true))
                ProjectManager.Instance.Settings.ExpandedNodes.Add("Project");

            if (IsNodeExpanded("Maps", false))
                ProjectManager.Instance.Settings.ExpandedNodes.Add("Maps");

            if (IsNodeExpanded("Tilesets", false))
                ProjectManager.Instance.Settings.ExpandedNodes.Add("Tilesets");

            if (IsNodeExpanded("Terrain", false))
                ProjectManager.Instance.Settings.ExpandedNodes.Add("Terrain");
        }

        #endregion

        #region Tree View Event Handler Region

        /// <summary>
        /// Handles the DoubleClick event of the <see cref="treeProject"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void treeProject_DoubleClick(object sender, EventArgs e)
        {
            OpenNode();
        }

        /// <summary>
        /// Handles the MouseDown event of the <see cref="treeProject"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        void treeProject_MouseDown(object sender, MouseEventArgs e)
        {
            var point = new Point(e.X, e.Y);
            var node = treeProject.GetNodeAt(point);

            if (node == null) return;

            treeProject.SelectedNode = node;
            BuildMenu();

            if (e.Button != MouseButtons.Right) return;

            if (node.Name == @"Project")
                contextProject.Show(Cursor.Position);

            if (node.Parent != null)
            {
                if (node.Parent.Name == @"Maps" || node.Parent.Name == @"Tilesets" || node.Parent.Name == @"Terrain")
                    contextProject.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// Handles the AfterExpand event of the <see cref="treeProject"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        void treeProject_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (_isCaching) return;
            CacheNodeStates();
        }

        #endregion
    }
}