using System;
using System.IO;
using System.Xml.Serialization;
using CrystalLib.Serialization;

namespace CrystalLib.Project
{
    [Serializable]
    public class Project
    {
        #region Field Region

        #endregion

        #region Property Region

        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        [XmlIgnore]
        public string FilePath { get; set; }
        public bool Closing { get; set; }

        /// <summary>
        /// Returns the path of the Project.xml file.
        /// </summary>
        public string ProjectPath
        {
            get
            {
                return Path.Combine(FilePath, "Project.xml");
            }
        }

        /// <summary>
        /// Returns the path of the Settings directory.
        /// </summary>
        public string SettingsPath
        {
            get
            {
                return Path.Combine(FilePath, @"settings");
            }
        }

        /// <summary>
        /// Returns the path of the Maps directory.
        /// </summary>
        public string MapPath
        {
            get
            {
                return Path.Combine(FilePath, @"maps");
            }
        }

        /// <summary>
        /// Returns the path of the Tilesets directory.
        /// </summary>
        public string TilesetPath
        {
            get
            {
                return Path.Combine(FilePath, @"tilesets");
            }
        }

        /// <summary>
        /// Returns the path of the Terrain directory.
        /// </summary>
        public string TerrainPath
        {
            get
            {
                return Path.Combine(FilePath, @"terrain");
            }
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="name">Name of the project.</param>
        /// <param name="author">Author(s) of the project.</param>
        /// <param name="description">Description of the project.</param>
        /// <param name="path">Path to the project directory.</param>
        public Project(string name, string author, string description, string path)
        {
            Name = name;
            Author = author;
            Description = description;
            FilePath = path;
        }

        #endregion

        #region Serialization Region

        /// <summary>
        /// Serializes the <see cref="Project"/> object to an XML file.
        /// </summary>
        public void SaveToXml(string path)
        {
            Serializer.SerializeToXml(this, path);
        }

        /// <summary>
        /// Deserializes a <see cref="Project"/> object from an XML file.
        /// </summary>
        /// <param name="path">Location of the XML file.</param>
        /// <returns>Deserialized <see cref="Project"/> object.</returns>
        public static Project LoadFromXml(string path)
        {
            return Serializer.DeserializeFromXml<Project>(path);
        }

        #endregion

        #region Method Region

        /// <summary>
        /// Checks if all project paths actually exist. If not, they are created.
        /// </summary>
        public void CheckDirectories()
        {
            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);

            if (!Directory.Exists(SettingsPath))
                Directory.CreateDirectory(SettingsPath);

            if (!Directory.Exists(MapPath))
                Directory.CreateDirectory(MapPath);

            if (!Directory.Exists(TilesetPath))
                Directory.CreateDirectory(TilesetPath);

            if (!Directory.Exists(TerrainPath))
                Directory.CreateDirectory(TerrainPath);
        }

        #endregion
    }
}