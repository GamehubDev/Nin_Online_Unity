// Copyright © Sven Groot (Ookii.org) 2009
// BSD license; see license.txt for details.
using System;
using System.ComponentModel.Design;

namespace CrystalLib.Toolset.Dialogs
{
    class TaskDialogDesigner : ComponentDesigner
    {
        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                verbs.Add(new DesignerVerb(Properties.Resources.Preview, new EventHandler(Preview)));
                return verbs;
            }
        }

        private void Preview(object sender, EventArgs e)
        {
            ((TaskDialog)Component).ShowDialog();
        }
    }
}