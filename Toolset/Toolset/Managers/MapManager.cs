using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalLib.Project;
using CrystalLib.TileEngine;
using CrystalLib.Toolset.UndoRedo;
using Toolset.Dialogs;
using Toolset.TileEngine;

namespace Toolset.Managers
{
    class MapManager
    {
        #region Field Region

        private Project _project;
        private List<EditorTileMap> _maps = new List<EditorTileMap>();

        #endregion

        #region Property Manager

        /// <summary>
        /// Returns the singleton instance of the <see cref="MapManager"/> class.
        /// </summary>
        public static readonly MapManager Instance = new MapManager();

        public EditorTool SelectedTool;

        /// <summary>
        /// Stores the instances of the <see cref="TileMap"/> objects.
        /// </summary>
        public List<EditorTileMap> Maps
        {
            get { return _maps; }
            set { _maps = value; }
        }

        /// <summary>
        /// Returns a list of all <see cref="TileMap"/> names.
        /// </summary>
        public List<string> MapNames
        {
            get
            {
                return Maps.Select(map => map.Name).ToList();
            }
        }

        #endregion

        #region Events Region

        /// <summary>
        /// Event raised when the <see cref="Maps"/> object is populated.
        /// </summary>
        public event EventHandler<MapLoadedEventArgs> MapLoaded;

        /// <summary>
        /// Event raised when a <see cref="TileMap"/> is added.
        /// </summary>
        public event EventHandler<MapAddedEventArgs> MapAdded;

        /// <summary>
        /// Event raised when a <see cref="TileMap"/> is deleted.
        /// </summary>
        public event EventHandler<MapDeletedEventArgs> MapDeleted;

        /// <summary>
        /// Event raised when a <see cref="TileMap"/> object is changed.
        /// </summary>
        public event EventHandler<MapChangedEventArgs> MapChanged;

        /// <summary>
        /// Event raised when the selected <see cref="TileMap"/> object is changed.
        /// </summary>
        public event EventHandler<MapSelectedEventArgs> MapSelected;

        /// <summary>
        /// Event raised when the selected tool is changed.
        /// </summary>
        public event EventHandler<ToolSelectedEventArgs> ToolSelected;

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="MapManager"/> class.
        /// </summary>
        public MapManager()
        {
            SelectedTool = EditorTool.Brush;

            ProjectManager.Instance.ProjectLoaded += ProjectLoaded;
            ProjectManager.Instance.ProjectClosed += ProjectClosed;
            ProjectManager.Instance.SettingsLoaded += SettingsLoaded;

            TerrainManager.Instance.TerrainSelected += TerrainSelected;
            TerrainManager.Instance.TerrainDeleted += TerrainDeleted;

            TilesetManager.Instance.SelectionChanged += SelectionChanged;
            TilesetManager.Instance.TilesetDeleted += TilesetDeleted;
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
            _project = e.Project;
            LoadMaps(e.Project.MapPath);
        }

        /// <summary>
        /// Handles the ProjectClosed event of the <see cref="ProjectManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectClosedEventArgs"/> instance containing the event data.</param>
        private void ProjectClosed(object sender, ProjectClosedEventArgs e)
        {
            Maps.Clear();
        }

        /// <summary>
        /// Handles the SettingsLoaded event of the <see cref="ProjectManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SettingsLoadedEventArgs"/> instance containing the event data.</param>
        private void SettingsLoaded(object sender, SettingsLoadedEventArgs e)
        {
            if (e.Settings.OpenFiles == null) return;

            foreach (var str in e.Settings.OpenFiles)
            {
                SelectMap(GetMapID(str));
            }

            if (!String.IsNullOrEmpty(e.Settings.SelectedFile))
                SelectMap(GetMapID(e.Settings.SelectedFile));
        }

        /// <summary>
        /// Handles the TerrainSelected event of the <see cref="TerrainManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainSelectedEventArgs"/> instance containing the event data.</param>
        private void TerrainSelected(object sender, TerrainSelectedEventArgs e)
        {
            if (SelectedTool == EditorTool.Brush)
                SelectTool(EditorTool.Terrain);
        }

        /// <summary>
        /// Handles the TerrainDeleted event of the <see cref="TerrainManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TerrainDeletedEventArgs"/> instance containing the event data.</param>
        private void TerrainDeleted(object sender, TerrainDeletedEventArgs e)
        {
            using (UndoRedoManager.Start("Deleting terrain"))
            {
                foreach (var map in Maps)
                {
                    foreach (var layer in map.ConcreteLayers)
                    {
                        foreach (var tile in layer.Tiles)
                        {
                            if (tile.Terrain == e.ID)
                            {
                                tile.SrcX = 0;
                                tile.SrcY = 0;
                                tile.Terrain = 0;
                                tile.Tileset = 0;
                            }
                        }
                    }
                    foreach (var layer in map.Layers)
                    {
                        foreach (var tile in layer.Tiles)
                        {
                            if (tile.Terrain == e.ID)
                            {
                                tile.SrcX = 0;
                                tile.SrcY = 0;
                                tile.Terrain = 0;
                                tile.Tileset = 0;
                            }
                        }
                    }
                    map.CacheAllTiles();
                }
                UndoRedoManager.Commit();
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the <see cref="TilesetManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedTool == EditorTool.Terrain)
                SelectTool(EditorTool.Brush);
        }

        /// <summary>
        /// Handles the TilesetDeleted event of the <see cref="TilesetManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TilesetDeletedEventArgs"/> instance containing the event data.</param>
        private void TilesetDeleted(object sender, TilesetDeletedEventArgs e)
        {
            using (UndoRedoManager.Start("Deleting tileset"))
            {
                foreach (var map in Maps)
                {
                    foreach (var layer in map.ConcreteLayers)
                    {
                        foreach (var tile in layer.Tiles)
                        {
                            if (tile.Tileset == e.ID)
                            {
                                tile.SrcX = 0;
                                tile.SrcY = 0;
                                tile.Terrain = 0;
                                tile.Tileset = 0;
                            }
                        }
                    }
                    foreach (var layer in map.Layers)
                    {
                        foreach (var tile in layer.Tiles)
                        {
                            if (tile.Tileset == e.ID)
                            {
                                tile.SrcX = 0;
                                tile.SrcY = 0;
                                tile.Terrain = 0;
                                tile.Tileset = 0;
                            }
                        }
                    }
                    map.CacheAllTiles();
                }
                UndoRedoManager.Commit();
            }
        }

        #endregion

        #region Map Management Region

        /// <summary>
        /// Updates the <see cref="TileMap"/> object with new data,
        /// triggers the <see cref="MapChangedEventArgs"/> event and saves the map.
        /// </summary>
        /// <param name="id">ID of the map.</param>
        /// <param name="name">Name of the map.</param>
        /// <param name="width">Width of the map.</param>
        /// <param name="height">Height of the map.</param>
        /// <param name="tileWidth">Width of the tiles.</param>
        /// <param name="tileHeight">Height of the tiles.</param>
        private void UpdateMap(int id, string name, int width, int height, int tileWidth, int tileHeight)
        {
            var map = GetMap(id);
            if (map == null) return;

            if (map.Name == name && map.Width == width && map.Height == height & map.TileWidth == tileWidth && map.TileHeight == tileHeight)
                return;

            var oldMap = map.Clone();

            if (map.Name != name)
            {
                string oldPath = Path.Combine(_project.MapPath, map.Name + @".xml");
                string newPath = Path.Combine(_project.MapPath, name + @".xml");
                File.Move(oldPath, newPath);
            }

            map.Name = name;
            /*map.Width = width;
            map.Height = height;
            map.TileWidth = tileWidth;
            map.TileHeight = tileHeight;*/

            map.UnsavedChanges = true;

            if (MapChanged != null)
                MapChanged.Invoke(this, new MapChangedEventArgs(oldMap, map));
        }

        /// <summary>
        /// Handles the <see cref="DialogMap"/> form, checks all the directories and builds the <see cref="TileMap"/> object.
        /// </summary>
        public void NewMap()
        {
            using (var dialog = new DialogMap())
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var name = dialog.MapName;
                var width = dialog.MapWidth;
                var height = dialog.MapHeight;
                var tileWidth = dialog.TileWidth;
                var tileHeight = dialog.TileHeight;

                if (CheckMap(name))
                {
                    MessageBox.Show(@"A map with this name already exists.", @"New Map");
                    return;
                }

                var index = 1;
                while (true)
                {
                    var exit = true;
                    foreach (var tmp in Maps)
                    {
                        if (tmp.ID == index)
                            exit = false;
                    }

                    if (exit)
                        break;

                    index++;
                }

                var map = new TileMap(index, name, width, height, tileWidth, tileHeight);
                map.Layers.Add(new TileLayer("Default", 100, true));

                var editormap = new EditorTileMap(map);

                Maps.Add(editormap);

                Console.WriteLine(@"Map {0} created.", editormap.Name);

                SaveMap(index);

                if (MapAdded != null)
                    MapAdded.Invoke(this, new MapAddedEventArgs(editormap));

                SelectMap(index);
            }
        }

        /// <summary>
        /// Serializes <see cref="Maps"/> list.
        /// </summary>
        public void SaveMaps()
        {
            foreach (var map in Maps)
            {
                SaveMap(map.ID);
            }
            Console.WriteLine(@"All maps saved.");
        }

        /// <summary>
        /// Deserializes <see cref="Maps"/> list from directory.
        /// </summary>
        /// <param name="path">Directory containing the xml files.</param>
        public void LoadMaps(string path)
        {
            Maps.Clear();

            if (!Directory.Exists(path)) return;

            var files = Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var str in files)
            {
                if (!File.Exists(str)) continue;

                var map = TileMap.LoadFromXml(str);
                var editormap = new EditorTileMap(map);

                Maps.Add(editormap);
            }

            if (MapLoaded != null)
                MapLoaded.Invoke(this, new MapLoadedEventArgs(Maps));

            Console.WriteLine(@"All maps loaded.");
        }

        /// <summary>
        /// Serializes the <see cref="TileMap"/> object to xml.
        /// </summary>
        /// <param name="id">ID of the map.</param>
        public void SaveMap(int id)
        {
            var map = GetMap(id);
            if (map == null) return;
            using (UndoRedoManager.Start("Saving map"))
            {
                map.SaveToConcrete();
                UndoRedoManager.Commit();
            }
            map.SaveToXml(Path.Combine(ProjectManager.Instance.Project.MapPath, map.Name + @".xml"));
            Console.WriteLine(@"Map {0} saved.", map.Name);
        }

        /// <summary>
        /// Deserializes a <see cref="TileMap"/> object from xml.
        /// </summary>
        /// <param name="path">Path to the xml file.</param>
        public TileMap LoadMap(string path)
        {
            return !File.Exists(path) ? null : TileMap.LoadFromXml(path);
        }

        /// <summary>
        /// Deletes the <see cref="TileMap"/> object and deletes the associated xml file.
        /// </summary>
        /// <param name="id">ID of the map.</param>
        public void DeleteMap(int id)
        {
            var map = GetMap(id);
            if (map == null) return;

            DialogResult dialogResult = MessageBox.Show(@"This will permanently delete the " + map.Name + @" map. Continue?", @"Delete Map", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;

            string fileName = Path.Combine(ProjectManager.Instance.Project.MapPath, map.Name + @".xml");

            if (File.Exists(fileName))
                File.Delete(fileName);

            string name = map.Name;

            if (MapDeleted != null)
                MapDeleted.Invoke(this, new MapDeletedEventArgs(name));

            Maps.Remove(map);

            Console.WriteLine(@"Map {0} deleted.", name);
        }

        /// <summary>
        /// Renames the <see cref="TileMap"/> object.
        /// </summary>
        public void RenameMap(int id)
        {
            var map = GetMap(id);
            if (map == null) return;

            using (var dialog = new DialogRename(map.Name))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                if (map.Name == dialog.NewName) return;

                if (CheckMap(dialog.NewName))
                {
                    MessageBox.Show(@"A map with this name already exists.", @"Rename Map");
                    return;
                }

                Console.WriteLine(@"Map {0} renamed to {1}.", map.Name, dialog.NewName);

                UpdateMap(id, dialog.NewName, map.Width, map.Height, map.TileWidth, map.TileHeight);
            }
        }

        /// <summary>
        /// Handles the <see cref="DialogMap"/> form and updates the <see cref="TileMap"/> object with the new data.
        /// </summary>
        public void EditMap(int id)
        {
            var map = GetMap(id);
            if (map == null) return;

            using (var dialog = new DialogMap(map.Name, map.Width, map.Height, map.TileWidth, map.TileHeight))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var name = dialog.MapName;
                var width = dialog.MapWidth;
                var height = dialog.MapHeight;
                var tileWidth = dialog.TileWidth;
                var tileHeight = dialog.TileHeight;

                if (map.Name.ToLower() != name.ToLower())
                {
                    if (CheckMap(name))
                    {
                        MessageBox.Show(@"A map with this name already exists.", @"Edit Map");
                        return;
                    }
                }

                Console.WriteLine(@"Map {0} edited.", map.Name);

                UpdateMap(id, name, width, height, tileWidth, tileHeight);
            }
        }

        /// <summary>
        /// Handles the <see cref="DialogMapSize"/> form and updates the <see cref="TileMap"/> object with the new data.
        /// </summary>
        /// <param name="id"></param>
        public void EditMapSize(int id)
        {
            var map = GetMap(id);
            if (map == null) return;

            using (var dialog = new DialogMapSize(map.Width, map.Height, map.TileWidth, map.TileHeight))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var width = dialog.MapWidth;
                var height = dialog.MapHeight;
                var tileWidth = dialog.TileWidth;
                var tileHeight = dialog.TileHeight;
                var anchor = dialog.AnchorPoint;

                map.ResizeMap(width, height, tileWidth, tileHeight, anchor);
            }
        }

        /// <summary>
        /// Checks if a <see cref="TileMap"/> object already exists.
        /// </summary>
        /// <param name="name">The name of the <see cref="TileMap"/> object.</param>
        /// <returns>Returns true if map already exists.</returns>
        public bool CheckMap(string name)
        {
            return _maps.Any(map => map.Name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Returns a <see cref="TileMap"/> object from the list.
        /// </summary>
        /// <param name="id">ID of the <see cref="TileMap"/> object.</param>
        /// <returns><see cref="TileMap"/> object.</returns>
        public EditorTileMap GetMap(int id)
        {
            return _maps.FirstOrDefault(map => map.ID == id);
        }

        /// <summary>
        /// Returns a <see cref="TileMap"/> object from the list.
        /// </summary>
        /// <param name="name">Name of the <see cref="TileMap"/> object.</param>
        /// <returns><see cref="TileMap"/> object.</returns>
        public EditorTileMap GetMap(string name)
        {
            return _maps.FirstOrDefault(map => map.Name == name);
        }

        /// <summary>
        /// Gets the ID of a <see cref="TileMap"/> object from the list.
        /// </summary>
        /// <param name="name">Name of the <see cref="TileMap"/> object.</param>
        /// <returns>ID of the <see cref="TileMap"/> object.</returns>
        public int GetMapID(string name)
        {
            return (from map in _maps where map.Name == name select map.ID).FirstOrDefault();
        }

        /// <summary>
        /// Triggers the <see cref="MapSelected"/> event.
        /// </summary>
        /// <param name="id">Id of the map selected.</param>
        public void SelectMap(int id)
        {
            var map = GetMap(id);

            if (MapSelected != null)
                MapSelected.Invoke(this, new MapSelectedEventArgs(map));

            TilesetManager.Instance.RefreshTileset();
            TilesetManager.Instance.RefreshSelection();
        }

        public void SelectTool(EditorTool tool)
        {
            SelectedTool = tool;

            if (ToolSelected != null)
                ToolSelected.Invoke(this, new ToolSelectedEventArgs(SelectedTool));
        }

        #endregion
    }
}