using CrystalLib.Serialization;

namespace Toolset.Preferences
{
    public class ToolsetPreferences
    {
        #region Property Region

        public int MaxHistorySize { get; set; }
        public bool MapGrid { get; set; }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolsetPreferences"/> class.
        /// </summary>
        public ToolsetPreferences()
        {
            MaxHistorySize = 40;
            MapGrid = true;
        }

        #endregion

        #region Serialization Region

        /// <summary>
        /// Serializes the <see cref="ToolsetPreferences"/> object to an XML file.
        /// </summary>
        public void SaveToXml(string path)
        {
            Serializer.SerializeToXml(this, path);
        }

        /// <summary>
        /// Deserializes a <see cref="ToolsetPreferences"/> object from an XML file.
        /// </summary>
        /// <param name="path">Location of the XML file.</param>
        /// <returns>Deserialized <see cref="ToolsetPreferences"/> object.</returns>
        public static ToolsetPreferences LoadFromXml(string path)
        {
            return Serializer.DeserializeFromXml<ToolsetPreferences>(path);
        }

        #endregion
    }
}
