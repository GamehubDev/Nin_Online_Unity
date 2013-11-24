using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalLib.Project;
using CrystalLib.TileEngine;
using Toolset.Dialogs;

namespace Toolset.Managers
{
    class TilesetManager
    {
        #region Field Region

        private Project _project;
        private List<Tileset> _tilesets = new List<Tileset>();
        private PointSelection _selection = new PointSelection();

        #endregion

        #region Property Region

        /// <summary>
        /// Returns the singleton instance of the <see cref="TilesetManager"/> class.
        /// </summary>
        public static readonly TilesetManager Instance = new TilesetManager();

        /// <summary>
        /// Stores the instances of the <see cref="Tileset"/> objects.
        /// </summary>
        public List<Tileset> Tilesets
        {
            get { return _tilesets; }
            set { _tilesets = value; }
        }

        /// <summary>
        /// Returns a list of all <see cref="Tileset"/> names.
        /// </summary>
        public List<string> TilesetNames
        {
            get
            {
                return Tilesets.Select(tileset => tileset.Name).ToList();
            }
        }

        /// <summary>
        /// Stores the array of currently selected tiles.
        /// </summary>
        public PointSelection Selection
        {
            get { return _selection; }
            set { _selection = value; }
        }

        /// <summary>
        /// Stores the currently selected tileset.
        /// </summary>
        public Tileset Tileset { get; set; }

        #endregion

        #region Events Region

        /// <summary>
        /// Event raised when the <see cref="Tilesets"/> object is populated.
        /// </summary>
        public event EventHandler<TilesetLoadedEventArgs> TilesetLoaded;

        /// <summary>
        /// Event raised when a <see cref="Tileset"/> is added.
        /// </summary>
        public event EventHandler<TilesetAddedEventArgs> TilesetAdded;

        /// <summary>
        /// Event raised when a <see cref="Tileset"/> is deleted.
        /// </summary>
        public event EventHandler<TilesetDeletedEventArgs> TilesetDeleted;

        /// <summary>
        /// Event raised when a <see cref="Tileset"/> object is changed.
        /// </summary>
        public event EventHandler<TilesetChangedEventArgs> TilesetChanged;

        /// <summary>
        /// Event raised when the selected <see cref="Tileset"/> object is changed.
        /// </summary>
        public event EventHandler<TilesetSelectedEventArgs> TilesetSelected;

        /// <summary>
        /// Event raised when the <see cref="Selection"/> object is changed.
        /// </summary>
        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="TilesetManager"/> class.
        /// </summary>
        public TilesetManager()
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
            LoadTilesets(e.Project.TilesetPath);
        }

        /// <summary>
        /// Handles the ProjectClosed event of the <see cref="ProjectManager"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProjectClosedEventArgs"/> instance containing the event data.</param>
        private void ProjectClosed(object sender, ProjectClosedEventArgs e)
        {
            Tilesets.Clear();
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

        #region Tileset Management Region

        /// <summary>
        /// Updates the <see cref="Tileset"/> object with new data,
        /// triggers the <see cref="TilesetChangedEventArgs"/> event and saves the tileset.
        /// </summary>
        /// <param name="id">ID of the tileset to edit.</param>
        /// <param name="name">Name of the tileset.</param>
        /// <param name="path">Path to the tileset texture.</param>
        /// <param name="tileWidth">Width of the individual tiles.</param>
        /// <param name="tileHeight">Height of the individual tiles.</param>
        private void UpdateTileset(int id, string name, string path, int tileWidth, int tileHeight)
        {
            var tileset = GetTileset(id);
            if (tileset == null) return;

            var oldTileset = tileset.Clone();
            var contPath = path;

            if (tileset.Name != name)
            {
                string oldPath = Path.Combine(_project.TilesetPath, tileset.Name + @".xml");
                string newPath = Path.Combine(_project.TilesetPath, name + @".xml");
                File.Move(oldPath, newPath);

                oldPath = tileset.Image;
                string newName = name + Path.GetExtension(tileset.Image);
                newPath = Path.Combine(ProjectManager.Instance.Project.TilesetPath, newName);
                File.Move(oldPath, newPath);
                path = newPath;
            }

            if (tileset.Image != path && tileset.Image != contPath)
            {
                string newName = name + Path.GetExtension(path);
                string newPath = Path.Combine(ProjectManager.Instance.Project.TilesetPath, newName);
                File.Delete(tileset.Image);
                File.Copy(path, newPath);
                path = newPath;
            }

            tileset.Name = name;
            tileset.Image = path;
            tileset.TileWidth = tileWidth;
            tileset.TileHeight = tileHeight;

            if (TilesetChanged != null)
                TilesetChanged.Invoke(this, new TilesetChangedEventArgs(oldTileset, tileset));

            SaveTileset(id);
        }

        /// <summary>
        /// Handles the <see cref="DialogTileset"/> form, checks all the directories and builds the <see cref="Tileset"/> object.
        /// </summary>
        public void NewTileset()
        {
            using (var dialog = new DialogTileset())
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var name = dialog.TilesetName;
                var path = dialog.FilePath;
                int width = dialog.TileWidth;
                int height = dialog.TileHeight;

                if (CheckTileset(name))
                {
                    MessageBox.Show(@"A tileset with this name already exists.", @"New Tileset");
                    return;
                }

                var index = 1;
                while (true)
                {
                    var exit = true;
                    foreach (var tmp in Tilesets)
                    {
                        if (tmp.ID == index)
                            exit = false;
                    }

                    if (exit)
                        break;

                    index++;
                }

                string newName = name + Path.GetExtension(path);
                string newPath = Path.Combine(ProjectManager.Instance.Project.TilesetPath, newName);
                File.Copy(path, newPath);

                path = newPath;

                var tileset = new Tileset(index, name, path, width, height);

                Tilesets.Add(tileset);

                SaveTileset(index);

                if (TilesetAdded != null)
                    TilesetAdded.Invoke(this, new TilesetAddedEventArgs(tileset));

                SelectTileset(index);
            }
        }

        /// <summary>
        /// Serializes <see cref="Tilesets"/> list.
        /// </summary>
        public void SaveTilesets()
        {
            foreach (var tileset in Tilesets)
            {
                SaveTileset(tileset.ID);
            }
        }

        /// <summary>
        /// Deserializes <see cref="Tilesets"/> list from directory.
        /// </summary>
        /// <param name="path">Directory containing the xml files.</param>
        public void LoadTilesets(string path)
        {
            Tilesets.Clear();

            if (!Directory.Exists(path)) return;

            var files = Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var str in files)
            {
                if (!File.Exists(str)) continue;
                var tileset = LoadTileset(str);
                Tilesets.Add(tileset);
            }

            if (TilesetLoaded != null)
                TilesetLoaded.Invoke(this, new TilesetLoadedEventArgs(Tilesets));
        }

        /// <summary>
        /// Serializes the <see cref="Tileset"/> object to xml.
        /// </summary>
        /// <param name="id">ID of the tileset.</param>
        public void SaveTileset(int id)
        {
            var tileset = GetTileset(id);
            if (tileset == null) return;
            tileset.Image = Path.GetFileName(tileset.Image);
            tileset.SaveToXml(Path.Combine(ProjectManager.Instance.Project.TilesetPath, tileset.Name + @".xml"));
            tileset.Image = Path.Combine(_project.TilesetPath, tileset.Image);
        }

        /// <summary>
        /// Deserializes a <see cref="Tileset"/> object from xml.
        /// </summary>
        /// <param name="path">Path to the xml file.</param>
        public Tileset LoadTileset(string path)
        {
            if (!File.Exists(path)) return null;
            var tmp = Tileset.LoadFromXml(path);
            tmp.Image = Path.Combine(_project.TilesetPath, tmp.Image);
            return tmp;
        }

        /// <summary>
        /// Deletes the <see cref="Tileset"/> object and deletes the associated xml file.
        /// </summary>
        /// <param name="id">ID of the tileset.</param>
        public void DeleteTileset(int id)
        {
            var tileset = GetTileset(id);
            if (tileset == null) return;

            DialogResult dialogResult = MessageBox.Show(@"This will permanently delete the " + tileset.Name + @" tileset and remove all related tiles from all maps. Continue?", @"Delete Tileset", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;

            string fileName = Path.Combine(ProjectManager.Instance.Project.TilesetPath, tileset.Name + @".xml");

            if (File.Exists(fileName))
                File.Delete(fileName);

            if (File.Exists(tileset.Image))
                File.Delete(tileset.Image);

            string name = tileset.Name;

            Tilesets.Remove(tileset);

            if (TilesetDeleted != null)
                TilesetDeleted.Invoke(this, new TilesetDeletedEventArgs(name, id));
        }

        /// <summary>
        /// Renames the <see cref="Tileset"/> object.
        /// </summary>
        public void RenameTileset(int id)
        {
            var tileset = GetTileset(id);
            if (tileset == null) return;

            using (var dialog = new DialogRename(tileset.Name))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                if (tileset.Name != dialog.NewName)
                {
                    if (CheckTileset(dialog.NewName))
                    {
                        MessageBox.Show(@"A tileset with this name already exists.", @"Rename Tileset");
                        return;
                    }
                }

                UpdateTileset(id, dialog.NewName, tileset.Image, tileset.TileWidth, tileset.TileHeight);
            }
        }

        /// <summary>
        /// Handles the <see cref="DialogTileset"/> form and updates the <see cref="Tileset"/> object with the new data.
        /// </summary>
        public void EditTileset(int id)
        {
            var tileset = GetTileset(id);

            if (tileset == null) return;

            using (var dialog = new DialogTileset(tileset.Name, tileset.Image, tileset.TileWidth, tileset.TileHeight))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var name = dialog.TilesetName;
                var path = dialog.FilePath;
                int tileWidth = dialog.TileWidth;
                int tileHeight = dialog.TileHeight;

                if (tileset.Name.ToLower() != name.ToLower())
                {
                    if (CheckTileset(name))
                    {
                        MessageBox.Show(@"A tileset with this name already exists.", @"Edit Tileset");
                        return;
                    }
                }

                UpdateTileset(id, name, path, tileWidth, tileHeight);
            }
        }

        /// <summary>
        /// Checks if a <see cref="Tileset"/> object already exists.
        /// </summary>
        /// <param name="name">The name of the <see cref="Tileset"/> object.</param>
        /// <returns>Returns true if tileset already exists.</returns>
        public bool CheckTileset(string name)
        {
            return _tilesets.Any(tileset => tileset.Name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Returns a <see cref="Tileset"/> object from the list.
        /// </summary>
        /// <param name="id">ID of the <see cref="Tileset"/> object.</param>
        /// <returns><see cref="Tileset"/> object.</returns>
        public Tileset GetTileset(int id)
        {
            return _tilesets.FirstOrDefault(tileset => tileset.ID == id);
        }

        /// <summary>
        /// Returns a <see cref="Tileset"/> object from the list.
        /// </summary>
        /// <param name="name">Name of the <see cref="Tileset"/> object.</param>
        /// <returns><see cref="Tileset"/> object.</returns>
        public Tileset GetTileset(string name)
        {
            return _tilesets.FirstOrDefault(tileset => tileset.Name == name);
        }

        /// <summary>
        /// Gets the ID of a <see cref="Tileset"/> object from the list.
        /// </summary>
        /// <param name="name">Name of the <see cref="Tileset"/> object.</param>
        /// <returns>ID of the <see cref="Tileset"/> object.</returns>
        public int GetTilesetID(string name)
        {
            return (from tileset in _tilesets where tileset.Name == name select tileset.ID).FirstOrDefault();
        }

        /// <summary>
        /// Triggers the <see cref="TilesetSelected"/> event.
        /// </summary>
        /// <param name="id">ID of the tileset selected.</param>
        public void SelectTileset(int id)
        {
            var tileset = GetTileset(id);
            if (tileset == null) return;

            Tileset = tileset;

            if (TilesetSelected != null)
                TilesetSelected.Invoke(this, new TilesetSelectedEventArgs(tileset));

            SetSelection(new Point(0, 0), new Point(0, 0));
        }

        /// <summary>
        /// Forces the TilesetSelected event to fire.
        /// </summary>
        public void RefreshTileset()
        {
            if (_project.Closing) return;
            if (TilesetSelected != null)
                TilesetSelected.Invoke(this, new TilesetSelectedEventArgs(Tileset));
        }

        #endregion

        #region Tile Selection Management

        /// <summary>
        /// Toggles the highlight of a specific point.
        /// </summary>
        /// <param name="point">Point to toggle.</param>
        public void ToggleSelection(Point point)
        {
            Selection.TogglePoint(point);

            if (SelectionChanged != null)
                SelectionChanged.Invoke(this, new SelectionChangedEventArgs(Selection));
        }

        /// <summary>
        /// Sets the <see cref="PointSelection.Start"/> and <see cref="PointSelection.End"/> properties of the <see cref="Selection"/> object.
        /// </summary>
        /// <param name="start">Starting point.</param>
        /// <param name="end">Ending point.</param>
        public void SetSelection(Point start, Point end)
        {
            Selection.Start = start;
            Selection.End = end;

            if (SelectionChanged != null)
                SelectionChanged.Invoke(this, new SelectionChangedEventArgs(Selection));
        }

        /// <summary>
        /// Sets the <see cref="PointSelection.End"/> property of the <see cref="Selection"/> object.
        /// </summary>
        /// <param name="end">Ending point.</param>
        public void SetSelection(Point end)
        {
            Selection.End = end;

            if (SelectionChanged != null)
                SelectionChanged.Invoke(this, new SelectionChangedEventArgs(Selection));
        }

        /// <summary>
        /// Sets the size of the underlying object of the <see cref="Selection"/> object.
        /// </summary>
        /// <param name="x">Width of the underlying object.</param>
        /// <param name="y">Height of the underlying object.</param>
        public void SetSelectionObjectSize(int x, int y)
        {
            Selection.SetObjectSize(x, y);

            if (SelectionChanged != null)
                SelectionChanged.Invoke(this, new SelectionChangedEventArgs(Selection));
        }

        /// <summary>
        /// Sets the size of the tiles of the <see cref="Selection"/> object.
        /// </summary>
        /// <param name="x">Width of the tiles.</param>
        /// <param name="y">Height of the tiles.</param>
        public void SetSelectionTileSize(int x, int y)
        {
            Selection.SetTileSize(x, y);

            if (SelectionChanged != null)
                SelectionChanged.Invoke(this, new SelectionChangedEventArgs(Selection));
        }

        /// <summary>
        /// Forces the SelectionChanged event to fire.
        /// </summary>
        public void RefreshSelection()
        {
            if (SelectionChanged != null)
                SelectionChanged.Invoke(this, new SelectionChangedEventArgs(Selection));
        }

        #endregion
    }
}
