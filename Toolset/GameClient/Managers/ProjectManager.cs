using System.IO;
using CrystalLib.Project;

namespace GameClient.Managers
{
    public class ProjectManager
    {
        /// <summary>
        /// Returns the singleton instance of the <see cref="ProjectManager"/> class.
        /// </summary>
        public static readonly ProjectManager Instance = new ProjectManager();

        /// <summary>
        /// Stores the one instance of the <see cref="Project"/> class.
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// Deserializes the <see cref="Project"/> object.
        /// </summary>
        public bool LoadProject(string path)
        {
            Project = Project.LoadFromXml(Path.Combine(path, "Project.xml"));

            Project.FilePath = Path.GetDirectoryName(path);
            Project.CheckDirectories();

            return true;
        }
    }
}