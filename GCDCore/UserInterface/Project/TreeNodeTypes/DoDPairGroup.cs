﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GCDCore.Project;

namespace GCDCore.UserInterface.Project.TreeNodeTypes
{
    public class DoDPairGroup : TreeNodeGroup
    {
        public readonly Surface NewSurface;
        public readonly Surface OldSurface;

        public DoDPairGroup(TreeNodeCollection parentNodes, Surface newSurf, Surface oldSurf, string name, IContainer container)
            : base(parentNodes, name, string.Empty, string.Empty, null, container)
        {
            ContextMenuStrip.Items[0].Text = "Add Change Detection With These New And Old Surfaces";

            NewSurface = newSurf;
            OldSurface = oldSurf;

            LoadChildNodes();
        }

        public override void LoadChildNodes()
        {
            Nodes.Clear();

            foreach (DoDBase dod in ProjectManager.Project.DoDs.Values.Where(x => x.NewSurface == NewSurface && x.OldSurface == OldSurface))
            {
                DoDGroup dodGroup = new DoDGroup(Nodes, dod, ContextMenuStrip.Container);
            }

            if (Nodes.Count > 0)
                Expand();
        }

        public override void OnAdd(object sender, EventArgs e)
        {
            ChangeDetection.frmDoDProperties frm = new ChangeDetection.frmDoDProperties(NewSurface, OldSurface);
            EditTreeItem(frm);
        }
    }
}