using System.Collections.Generic;
using System.Xml.Serialization;
using CrystalLib.Serialization;

namespace CrystalLib.Project
{
    public class ProjectSettings
    {
        #region Property Region

        public string SelectedFile { get; set; }
        public List<string> OpenFiles { get; set; }
        public List<string> ExpandedNodes { get; set; }

        [XmlIgnore]
        public bool Ignore { get; set; }

        #endregion

        #region Serialization Region

        /// <summary>
        /// Serializes the <see cref="ProjectSettings"/> object to an XML file.
        /// </summary>
        public void SaveToXml(string path)
        {
            Serializer.SerializeToXml(this, path);
        }

        /// <summary>
        /// Deserializes a <see cref="ProjectSettings"/> object from an XML file.
        /// </summary>
        /// <param name="path">Location of the XML file.</param>
        /// <returns>Deserialized <see cref="ProjectSettings"/> object.</returns>
        public static ProjectSettings LoadFromXml(string path)
        {
            return Serializer.DeserializeFromXml<ProjectSettings>(path);
        }

        #endregion
    }
}