using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalLib.Project;
using CrystalLib.TileEngine;
using Toolset.Dialogs;

namespace Toolset.Managers
{
    class TerrainManager
    {
        #region Field Region

        private Project _project;
        private List<TerrainTile> _terrain = new List<TerrainTile>();
        private TerrainTile _selectedTerrain;

        #endregion

        #region Property Region

        /// <summary>
        /// Returns the singleton instance of the <see cref="TerrainManager"/> class.
        /// </summary>
        public static readonly TerrainManager Instance = new TerrainManager();

        /// <summary>
        /// Stores the instances of the <see cref="Terrain"/> objects.
        /// </summary>
        public List<TerrainTile> Terrain
        {
            get { return _terrain; }
            set { _terrain = value; }
        }

        /// <summary>
        /// Returns a list of all <see cref="Terrain"/> names.
        /// </summary>
        public List<string> TerrainNames
        {
            get
            {
                return Terrain.Select(terrain => terrain.Name).ToList();
            }
        }

        public TerrainTile SelectedTerrain
        {
            get { return _selectedTerrain; }
            set { _selectedTerrain = value; }
        }

        #endregion

        #region Events Region

        /// <summary>
        /// Event raised when the <see cref="TerrainTile"/> object is populated.
        /// </summary>
        public event EventHandler<TerrainLoadedEventArgs> TerrainLoaded;

        /// <summary>
        /// Event raised when a new <see cref="TerrainTile"/> object is added.
        /// </summary>
        public event EventHandler<TerrainAddedEventArgs> TerrainAdded;

        /// <summary>
        /// Event raised when a <see cref="TerrainTile"/> object is deleted.
        /// </summary>
        public event EventHandler<TerrainDeletedEventArgs> TerrainDeleted;

        /// <summary>
        /// Event raised when a <see cref="TerrainTile"/> object is changed.
        /// </summary>
        public event EventHandler<TerrainChangedEventArgs> TerrainChanged;

        /// <summary>
        /// Event raised when a <see cref="TerrainTile"/> object is selected.
        /// </summary>
        public event EventHandler<TerrainSelectedEventArgs> TerrainSelected;

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainManager"/> class.
        /// </summary>
        public TerrainManager()
        {
            ProjectManager.Instance.ProjectLoaded += ProjectLoaded;
            ProjectManager.Instance.ProjectClosed += ProjectClosed;
            ProjectManager.Instance.ProjectChanged += ProjectChanged;
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
            LoadTerrainTiles(_project.TerrainPath);
        }

        /// <summary>
        /// Handles the ProjectClosed event of the <see cref="ProjectManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectClosedEventArgs"/> instance containing the event data.</param>
        private void ProjectClosed(object sender, ProjectClosedEventArgs e)
        {
            Terrain.Clear();
        }

        /// <summary>
        /// Handles the ProjectChanged event of the <see cref="ProjectManager"/> object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectChangedEventArgs"/> instance containing the event data.</param>
        private void ProjectChanged(object sender, ProjectChangedEventArgs e)
        {

        }

        #endregion

        #region Terrain Management Region

        /// <summary>
        /// Updates the <see cref="TerrainTile"/> object with new data,
        /// triggers the <see cref="TerrainChangedEventArgs"/> event and saves the tileset.
        /// </summary>
        /// <param name="id">ID of the terrain to edit.</param>
        /// <param name="name">Name of the terrain.</param>
        /// <param name="type"><see cref="TerrainType"/> enum object.</param>
        /// <param name="tileset">Tileset the terrain texture comes from.</param>
        /// <param name="x">X co-ordinate of the srcRect.</param>
        /// <param name="y">Y co-ordinate of the srcRect.</param>
        /// <param name="width">Width of the srcRect.</param>
        /// <param name="height">Height of the srcRect.</param>
        private void UpdateTerrain(int id, string name, TerrainType type, int tileset, int x, int y, int width, int height)
        {
            var terrain = GetTerrain(id);
            if (terrain == null) return;

            var oldTerrain = terrain.Clone();

            if (terrain.Name != name)
            {
                string oldPath = Path.Combine(_project.TerrainPath, terrain.Name + @".xml");
                string newPath = Path.Combine(_project.TerrainPath, name + @".xml");
                File.Move(oldPath, newPath);

                oldPath = Path.Combine(_project.TerrainPath, terrain.Name + @".png");
                newPath = Path.Combine(_project.TerrainPath, name + @".png");
                File.Move(oldPath, newPath);
            }

            terrain.Name = name;
            terrain.Type = type;
            terrain.Tileset = tileset;
            terrain.X = x;
            terrain.Y = y;
            terrain.Width = width;
            terrain.Height = height;

            if (TerrainChanged != null)
                TerrainChanged.Invoke(this, new TerrainChangedEventArgs(oldTerrain, terrain));

            SaveTerrain(id);
        }

        /// <summary>
        /// Handles the <see cref="DialogTerrain"/> form, checks all the directories and builds the <see cref="Terrain"/> object.
        /// </summary>
        public void NewTerrain()
        {
            using (var dialog = new DialogTerrain())
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var name = dialog.TerrainName;
                var type = dialog.TerrainType;
                var tileset = dialog.TilesetID;
                var x = dialog.SelectionX;
                var y = dialog.SelectionY;
                var width = dialog.SelectionWidth;
                var height = dialog.SelectionHeight;

                if (CheckTerrain(name))
                {
                    MessageBox.Show(@"Terrain with this name already exists.", @"New Terrain");
                    return;
                }

                var index = 1;
                while (true)
                {
                    var exit = true;
                    foreach (var tmp in Terrain)
                    {
                        if (tmp.ID == index)
                            exit = false;
                    }

                    if (exit)
                        break;

                    index++;
                }

                var terrain = new TerrainTile(index, name, type, tileset, x, y, width, height);

                Terrain.Add(terrain);

                SaveTerrain(index);

                if (TerrainAdded != null)
                    TerrainAdded.Invoke(this, new TerrainAddedEventArgs(terrain));
            }
        }

        /// <summary>
        /// Serializes <see cref="Terrain"/> list.
        /// </summary>
        public void SaveTerrainTiles()
        {
            foreach (var terrain in Terrain)
            {
                SaveTerrain(terrain.ID);
            }
        }

        /// <summary>
        /// Deserializes <see cref="Terrain"/> list from directory.
        /// </summary>
        /// <param name="path">Directory containing the xml files.</param>
        public void LoadTerrainTiles(string path)
        {
            Terrain.Clear();

            if (!Directory.Exists(path)) return;

            var files = Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var str in files)
            {
                if (!File.Exists(str)) continue;
                var terrain = TerrainTile.LoadFromXml(str);
                Terrain.Add(terrain);
            }

            if (TerrainLoaded != null)
                TerrainLoaded.Invoke(this, new TerrainLoadedEventArgs(Terrain));
        }

        /// <summary>
        /// Serializes the <see cref="Terrain"/> object to xml.
        /// </summary>
        /// <param name="id">ID of the tileset.</param>
        public void SaveTerrain(int id)
        {
            var terrain = GetTerrain(id);
            if (terrain == null) return;
            terrain.SaveToXml(Path.Combine(ProjectManager.Instance.Project.TerrainPath, terrain.Name + @".xml"));
        }

        /// <summary>
        /// Deserializes a <see cref="Terrain"/> object from xml.
        /// </summary>
        /// <param name="path">Path to the xml file.</param>
        public TerrainTile LoadTerrain(string path)
        {
            return !File.Exists(path) ? null : TerrainTile.LoadFromXml(path);
        }

        /// <summary>
        /// Deletes the <see cref="TerrainTile"/> object and deletes the associated xml file.
        /// </summary>
        /// <param name="id">ID of the tileset.</param>
        public void DeleteTerrain(int id)
        {
            var terrain = GetTerrain(id);
            if (terrain == null) return;

            DialogResult dialogResult = MessageBox.Show(@"This will permanently delete the " + terrain.Name + @" terrain tile. Continue?", @"Delete Terrain", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;

            string fileName = Path.Combine(ProjectManager.Instance.Project.TerrainPath, terrain.Name + @".xml");
            if (File.Exists(fileName))
                File.Delete(fileName);

            fileName = Path.Combine(ProjectManager.Instance.Project.TerrainPath, terrain.Name + @".png");
            if (File.Exists(fileName))
                File.Delete(fileName);

            string name = terrain.Name;

            Terrain.Remove(terrain);

            if (TerrainDeleted != null)
                TerrainDeleted.Invoke(this, new TerrainDeletedEventArgs(name, id));
        }

        /// <summary>
        /// Renames the <see cref="TerrainTile"/> object.
        /// </summary>
        public void RenameTerrain(int id)
        {
            var terrain = GetTerrain(id);
            if (terrain == null) return;

            using (var dialog = new DialogRename(terrain.Name))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                if (terrain.Name.ToLower() != dialog.NewName.ToLower())
                {
                    if (CheckTerrain(dialog.NewName))
                    {
                        MessageBox.Show(@"A terrain tile with this name already exists.", @"Rename Terrain");
                        return;
                    }
                }

                UpdateTerrain(id, dialog.NewName, terrain.Type, terrain.Tileset, terrain.X, terrain.Y, terrain.Width, terrain.Height);
            }
        }

        /// <summary>
        /// Handles the <see cref="DialogTerrain"/> form and updates the <see cref="Terrain"/> object with the new data.
        /// </summary>
        public void EditTerrain(int id)
        {
            var terrain = GetTerrain(id);

            if (terrain == null) return;

            using (var dialog = new DialogTerrain(terrain.Name, terrain.Type, terrain.Tileset, terrain.X, terrain.Y, terrain.Width, terrain.Height))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var name = dialog.TerrainName;
                var type = dialog.TerrainType;
                var tileset = dialog.TilesetID;
                var x = dialog.SelectionX;
                var y = dialog.SelectionY;
                var width = dialog.SelectionWidth;
                var height = dialog.SelectionHeight;

                if (terrain.Name.ToLower() != name.ToLower())
                {
                    if (CheckTerrain(name))
                    {
                        MessageBox.Show(@"A terrain tile with this name already exists.", @"Edit Terrain");
                        return;
                    }
                }

                UpdateTerrain(id, name, type, tileset, x, y, width, height);
            }
        }

        /// <summary>
        /// Handles the <see cref="DialogTerrain"/> form and duplicates the passed Terrain object.
        /// </summary>
        /// <param name="id">ID of the terrain to duplicate</param>
        public void DuplicateTerrain(int id)
        {
            var terrain = GetTerrain(id);
            if (terrain == null) return;

            using (var dialog = new DialogTerrain(terrain.Name + " Copy", terrain.Type, terrain.Tileset, terrain.X, terrain.Y, terrain.Width, terrain.Height))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var name = dialog.TerrainName;
                var type = dialog.TerrainType;
                var tileset = dialog.TilesetID;
                var x = dialog.SelectionX;
                var y = dialog.SelectionY;
                var width = dialog.SelectionWidth;
                var height = dialog.SelectionHeight;

                if (CheckTerrain(name))
                {
                    MessageBox.Show(@"A terrain tile with this name already exists.", @"Duplicate Terrain");
                    return;
                }

                var index = 1;
                while (true)
                {
                    var exit = true;
                    foreach (var tmp in Terrain)
                    {
                        if (tmp.ID == index)
                            exit = false;
                    }

                    if (exit)
                        break;

                    index++;
                }

                var newTerrain = new TerrainTile(index, name, type, tileset, x, y, width, height);

                Terrain.Add(newTerrain);

                SaveTerrain(newTerrain.ID);

                if (TerrainAdded != null)
                    TerrainAdded.Invoke(this, new TerrainAddedEventArgs(newTerrain));
            }
        }

        /// <summary>
        /// Checks if a <see cref="Terrain"/> object already exists.
        /// </summary>
        /// <param name="name">The name of the <see cref="Terrain"/> object.</param>
        /// <returns>Returns true if tileset already exists.</returns>
        public bool CheckTerrain(string name)
        {
            return _terrain.Any(terrain => terrain.Name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Returns a <see cref="Terrain"/> object from the list.
        /// </summary>
        /// <param name="id">ID of the <see cref="Terrain"/> object.</param>
        /// <returns><see cref="Terrain"/> object.</returns>
        public TerrainTile GetTerrain(int id)
        {
            return _terrain.FirstOrDefault(terrain => terrain.ID == id);
        }

        /// <summary>
        /// Returns a <see cref="Terrain"/> object from the list.
        /// </summary>
        /// <param name="name">Name of the <see cref="Terrain"/> object.</param>
        /// <returns><see cref="Terrain"/> object.</returns>
        public TerrainTile GetTerrain(string name)
        {
            return _terrain.FirstOrDefault(terrain => terrain.Name == name);
        }

        /// <summary>
        /// Gets the ID of a <see cref="Terrain"/> object from the list.
        /// </summary>
        /// <param name="name">Name of the <see cref="Terrain"/> object.</param>
        /// <returns>ID of the <see cref="Terrain"/> object.</returns>
        public int GetTerrainID(string name)
        {
            return (from terrain in _terrain where terrain.Name == name select terrain.ID).FirstOrDefault();
        }

        /// <summary>
        /// Forces a selection of the terrain ID passed.
        /// </summary>
        /// <param name="id">ID of the terrain to select.</param>
        public void SelectTerrain(int id)
        {
            var terrain = GetTerrain(id);
            if (terrain == null) return;

            SelectedTerrain = terrain;

            if (TerrainSelected != null)
                TerrainSelected.Invoke(this, new TerrainSelectedEventArgs(terrain));
        }

        #endregion
    }
}
