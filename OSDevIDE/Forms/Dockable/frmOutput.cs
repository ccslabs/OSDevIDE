using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using FastColoredTextBoxNS;

namespace OSDevIDE.Forms.Dockable
{
    public partial class frmOutput : DockContent
    {
        FastColoredTextBox fctb = new FastColoredTextBox();

        public frmOutput()
        {
            InitializeComponent();
            fctb.Dock = DockStyle.Fill;
            fctb.ReadOnly = true;
            fctb.BorderStyle = BorderStyle.None;
            fctb.BackColor = Color.Black;
            fctb.CaretVisible = false;
            fctb.WordWrap = true;
            panelContainer.Controls.Add(fctb);

        }
    }
}
