using System;
using CrystalLib.Project;

namespace Toolset
{
    public class ProjectClosedEventArgs : EventArgs
    {
        readonly Project _project;

        public ProjectClosedEventArgs(Project project)
        {
            _project = project;
        }

        public Project Project
        {
            get { return _project; }
        }
    }
}