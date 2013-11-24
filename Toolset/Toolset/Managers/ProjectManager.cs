using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CrystalLib.Project;
using CrystalLib.Toolset.Dialogs;
using Toolset.Dialogs;

namespace Toolset.Managers
{
    public class ProjectManager
    {
        #region Events Region

        /// <summary>
        /// Event raised when the <see cref="Project"/> object is loaded.
        /// </summary>
        public event EventHandler<ProjectLoadedEventArgs> ProjectLoaded;

        /// <summary>
        /// Event raised when the <see cref="Project"/> object is destroyed.
        /// </summary>
        public event EventHandler<ProjectClosedEventArgs> ProjectClosed;

        /// <summary>
        /// Event raised when the <see cref="Project"/> object is changed.
        /// </summary>
        public event EventHandler<ProjectChangedEventArgs> ProjectChanged;

        /// <summary>
        /// Event raised when the <see cref="Settings"/> object is loaded.
        /// </summary>
        public event EventHandler<SettingsLoadedEventArgs> SettingsLoaded;

        #endregion

        #region Properties Region

        /// <summary>
        /// Returns the singleton instance of the <see cref="ProjectManager"/> class.
        /// </summary>
        public static readonly ProjectManager Instance = new ProjectManager();

        /// <summary>
        /// Stores the one instance of the <see cref="Project"/> class.
        /// </summary>
        public Project Project { get; set; }

        public ProjectSettings Settings { get; set; }        

        #endregion

        #region Project Management Region

        /// <summary>
        /// Updates the <see cref="Project"/> object with new data, 
        /// triggers the <see cref="ProjectChanged"/> event and saves the project.
        /// </summary>
        /// <param name="name">Name of the project.</param>
        /// <param name="author">Author of the project.</param>
        /// <param name="description">Description of the project.</param>
        private void UpdateProject(string name, string author, string description)
        {
            Project.Name = name;
            Project.Author = author;
            Project.Description = description;

            ProjectChanged.Invoke(this, new ProjectChangedEventArgs(Project));
            SaveProject();
        }

        /// <summary>
        /// Handles the <see cref="DialogProject"/> form, checks all the directories and builds the <see cref="Project"/> object.
        /// </summary>
        public void NewProject()
        {
            using (var dialog = new DialogProject())
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                if (Project != null)
                {
                    var dialogResult = MessageBox.Show(@"Creating a new project will close the current one. Continue?", @"New Project", MessageBoxButtons.YesNo);
                    if (dialogResult != DialogResult.Yes) return;
                    CloseProject();
                }

                var name = dialog.ProjectName;
                var author = dialog.Author;
                var description = dialog.Description;
                var path = dialog.FilePath;

                Project = new Project(name, author, description, path);

                if (ProjectLoaded != null)
                    ProjectLoaded.Invoke(this, new ProjectLoadedEventArgs(Project));

                Settings = new ProjectSettings();

                if (SettingsLoaded != null)
                    SettingsLoaded.Invoke(this, new SettingsLoadedEventArgs(Settings));

                SaveProject();

                Console.WriteLine(@"Project {0} created.", name);
            }
        }

        /// <summary>
        /// Serializes the <see cref="Project"/> object.
        /// </summary>
        public void SaveProject()
        {
            Project.CheckDirectories();
            Project.SaveToXml(Project.ProjectPath);
            Settings.SaveToXml(Path.Combine(Project.SettingsPath, "settings.xml"));
            Console.WriteLine(@"Project {0} saved.", Project.Name);
        }

        /// <summary>
        /// Deserializes the <see cref="Project"/> object.
        /// </summary>
        public void LoadProject()
        {
            using (var folderDialog = new OpenFileDialog())
            {
                folderDialog.FileName = "Project.xml";
                folderDialog.DefaultExt = ".xml";

                var folderResult = folderDialog.ShowDialog();
                if (folderResult != DialogResult.OK) return;

                if (Project != null)
                {
                    var dialogResult = MessageBox.Show(@"Opening a project will close the current one. Continue?", @"Open Project", MessageBoxButtons.YesNo);
                    if (dialogResult != DialogResult.Yes) return;
                    if (!CloseProject()) return;
                }

                string path = folderDialog.FileName;

                if (!File.Exists(path))
                {
                    MessageBox.Show(@"This is not a valid Crystal Toolset Project.", @"Open Project");
                    return;
                }

                Project = Project.LoadFromXml(path);
                Project.FilePath = Path.GetDirectoryName(path);
                Project.CheckDirectories();

                Console.WriteLine(@"Project {0} loaded.", Project.Name);

                if (ProjectLoaded != null)
                    ProjectLoaded.Invoke(this, new ProjectLoadedEventArgs(Project));

                var settingsPath = Path.Combine(Project.SettingsPath, "settings.xml");

                Settings = File.Exists(settingsPath) ? ProjectSettings.LoadFromXml(settingsPath) : new ProjectSettings();

                Settings.Ignore = true;

                if (SettingsLoaded != null)
                    SettingsLoaded.Invoke(this, new SettingsLoadedEventArgs(Settings));

                Settings.Ignore = false;
            }
        }

        /// <summary>
        /// Destroys the <see cref="Project"/> object.
        /// </summary>
        public bool CloseProject()
        {
            if (Project != null)
            {
                List<string> files = new List<string>();
                foreach (var map in MapManager.Instance.Maps)
                {
                    if (map.UnsavedChanges)
                    {
                        files.Add(map.Name);
                    }
                }

                if (files.Count > 0)
                {
                    using (var closeDialog = new DialogClose(files))
                    {
                        var closeResult = closeDialog.ShowDialog();
                        if (closeResult == DialogResult.Cancel) return false;
                        if (closeResult == DialogResult.Yes)
                            MapManager.Instance.SaveMaps();
                        if (closeResult == DialogResult.No)
                        {
                            foreach (var map in MapManager.Instance.Maps)
                            {
                                map.UnsavedChanges = false;
                            }
                        }
                    }
                }

                Console.WriteLine(@"Project {0} closed.", Project.Name);
            }

            if (Settings != null)
                Settings.Ignore = true;

            if (Project != null)
                Project.Closing = true;

            if (ProjectClosed != null)
                ProjectClosed.Invoke(this, new ProjectClosedEventArgs(Project));

            if (Settings != null && Project != null)
                Settings.SaveToXml(Path.Combine(Project.SettingsPath, "settings.xml"));

            Project = null;
            Settings = null;

            return true;
        }

        /// <summary>
        /// Renames the <see cref="Project"/> object.
        /// </summary>
        public void RenameProject()
        {
            if (Project == null) return;

            using (var dialog = new DialogRename(Project.Name))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                Console.WriteLine(@"Project {0} renamed to {1}", Project.Name, dialog.NewName);

                UpdateProject(dialog.NewName, Project.Author, Project.Description);
            }
        }

        /// <summary>
        /// Handles the <see cref="DialogProject"/> form and updates the <see cref="Project"/> object with the new data.
        /// </summary>
        public void EditProject()
        {
            if (Project == null) return;

            using (var dialog = new DialogProject(Project.Name, Project.Author, Project.Description, Project.FilePath))
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK) return;

                var name = dialog.ProjectName;
                var author = dialog.Author;
                var description = dialog.Description;

                UpdateProject(name, author, description);

                Console.WriteLine(@"Project {0} edited.", Project.Name);
            }
        }

        #endregion

        #region Play Region

        /// <summary>
        /// Runs a test client.
        /// </summary>
        /// <param name="parent">Parent form.</param>
        public void Play(Form parent)
        {
            var worker = new BackgroundWorker();

            int width = parent.DesktopBounds.Width;
            int height = parent.DesktopBounds.Height;
            int x = parent.DesktopBounds.X;
            int y = parent.DesktopBounds.Y;

            var x2 = (uint)((x + (width / 2)) - (800 / 2));
            var y2 = (uint)((y + (height / 2)) - (600 / 2));

            parent.Enabled = false;

            worker.DoWork += delegate
            {
                var game = new GameClient.Game(Project.FilePath, x2, y2, 800, 600);

                while (game.IsOpen)
                {
                    game.Render();
                }
            };

            worker.RunWorkerCompleted += delegate 
            { 
                parent.Enabled = true; 
            };

            worker.RunWorkerAsync();
        }

        #endregion
    }
}