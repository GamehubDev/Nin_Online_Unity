using System;

namespace Toolset
{
    public class UnsavedChangesEventArgs : EventArgs
    {
        readonly bool _unsavedChanges;

        public UnsavedChangesEventArgs(bool unsavedChanges)
        {
            _unsavedChanges = unsavedChanges;
        }

        public bool UnsavedChanges 
        {
            get { return _unsavedChanges; }
        }
    }
}