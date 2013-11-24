// Copyright (c) Sven Groot (Ookii.org) 2006
// See license.txt for details
using System;
using System.Collections.Generic;
using System.Text;

namespace CrystalLib.Toolset.Dialogs
{
    class VistaFileDialogEvents : CrystalLib.Toolset.Dialogs.Interop.IFileDialogEvents, CrystalLib.Toolset.Dialogs.Interop.IFileDialogControlEvents
    {
        const uint S_OK = 0;
        const uint S_FALSE = 1;
        const uint E_NOTIMPL = 0x80004001;

        private VistaFileDialog _dialog;

        public VistaFileDialogEvents(VistaFileDialog dialog)
        {
            if( dialog == null )
                throw new ArgumentNullException("dialog");

            _dialog = dialog;
        }

        #region IFileDialogEvents Members

        public Interop.HRESULT OnFileOk(CrystalLib.Toolset.Dialogs.Interop.IFileDialog pfd)
        {
            if( _dialog.DoFileOk(pfd) )
                return CrystalLib.Toolset.Dialogs.Interop.HRESULT.S_OK;
            else
                return CrystalLib.Toolset.Dialogs.Interop.HRESULT.S_FALSE;
        }

        public Interop.HRESULT OnFolderChanging(CrystalLib.Toolset.Dialogs.Interop.IFileDialog pfd, CrystalLib.Toolset.Dialogs.Interop.IShellItem psiFolder)
        {
            return CrystalLib.Toolset.Dialogs.Interop.HRESULT.S_OK;
        }

        public void OnFolderChange(CrystalLib.Toolset.Dialogs.Interop.IFileDialog pfd)
        {
        }

        public void OnSelectionChange(CrystalLib.Toolset.Dialogs.Interop.IFileDialog pfd)
        {
        }

        public void OnShareViolation(CrystalLib.Toolset.Dialogs.Interop.IFileDialog pfd, CrystalLib.Toolset.Dialogs.Interop.IShellItem psi, out NativeMethods.FDE_SHAREVIOLATION_RESPONSE pResponse)
        {
            pResponse = NativeMethods.FDE_SHAREVIOLATION_RESPONSE.FDESVR_DEFAULT;
        }

        public void OnTypeChange(CrystalLib.Toolset.Dialogs.Interop.IFileDialog pfd)
        {
        }

        public void OnOverwrite(CrystalLib.Toolset.Dialogs.Interop.IFileDialog pfd, CrystalLib.Toolset.Dialogs.Interop.IShellItem psi, out NativeMethods.FDE_OVERWRITE_RESPONSE pResponse)
        {
            pResponse = NativeMethods.FDE_OVERWRITE_RESPONSE.FDEOR_DEFAULT;
        }

        #endregion

        #region IFileDialogControlEvents Members

        public void OnItemSelected(CrystalLib.Toolset.Dialogs.Interop.IFileDialogCustomize pfdc, int dwIDCtl, int dwIDItem)
        {
        }

        public void OnButtonClicked(CrystalLib.Toolset.Dialogs.Interop.IFileDialogCustomize pfdc, int dwIDCtl)
        {
            if( dwIDCtl == VistaFileDialog.HelpButtonId )
                _dialog.DoHelpRequest();
        }

        public void OnCheckButtonToggled(CrystalLib.Toolset.Dialogs.Interop.IFileDialogCustomize pfdc, int dwIDCtl, bool bChecked)
        {
        }

        public void OnControlActivating(CrystalLib.Toolset.Dialogs.Interop.IFileDialogCustomize pfdc, int dwIDCtl)
        {
        }

        #endregion


    }
}
