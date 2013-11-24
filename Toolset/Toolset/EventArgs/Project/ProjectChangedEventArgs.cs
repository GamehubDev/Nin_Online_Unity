using System;
using CrystalLib.Project;

namespace Toolset
{
    public class ProjectChangedEventArgs : EventArgs
    {
        readonly Project _project;

        public ProjectChangedEventArgs(Project project)
        {
            _project = project;
        }

        public Project Project
        {
            get { return _project; }
        }
    }
}