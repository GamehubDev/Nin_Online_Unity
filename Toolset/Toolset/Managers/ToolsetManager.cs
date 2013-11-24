using System.IO;
using System.Windows.Forms;
using CrystalLib.Serialization;
using Toolset.Preferences;

namespace Toolset.Managers
{
    class ToolsetManager
    {
        #region Properties Region

        private ToolsetPreferences _preferences = new ToolsetPreferences();
        public ToolsetPreferences Preferences
        {
            get { return _preferences; }
            set { _preferences = value; }
        }

        /// <summary>
        /// Returns the singleton instance of the <see cref="ToolsetManager"/> class.
        /// </summary>
        public static readonly ToolsetManager Instance = new ToolsetManager();

        #endregion

        #region Method Region

        public void Init()
        {
            var path = Path.Combine(Application.StartupPath, "settings.xml");
            if (!File.Exists(path))
                SaveSettings();
            else
                LoadSettings();
        }

        #endregion

        #region Serialization Region

        public void SaveSettings()
        {
            var path = Path.Combine(Application.StartupPath, "settings.xml");
            Serializer.SerializeToXml(_preferences, path);
        }

        public void LoadSettings()
        {
            var path = Path.Combine(Application.StartupPath, "settings.xml");
            Serializer.DeserializeFromBinary<ToolsetPreferences>(path);
        }

        #endregion
    }
}
