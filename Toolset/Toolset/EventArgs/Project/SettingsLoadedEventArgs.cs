using System;
using CrystalLib.Project;

namespace Toolset
{
    public class SettingsLoadedEventArgs : EventArgs
    {
        readonly ProjectSettings _settings;

        public SettingsLoadedEventArgs(ProjectSettings settings)
        {
            _settings = settings;
        }

        public ProjectSettings Settings
        {
            get { return _settings; }
        }
    }
}