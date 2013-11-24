using System;
using CrystalLib.Project;

namespace Toolset
{
    public class ProjectLoadedEventArgs : EventArgs
    {
        readonly Project _project;

        public ProjectLoadedEventArgs(Project project)
        {
            _project = project;
        }

        public Project Project
        {
            get { return _project; }
        }
    }
}