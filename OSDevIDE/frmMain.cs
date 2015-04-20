using OSDevIDE.Forms.Dockable;
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

namespace OSDevIDE
{
    public partial class frmMain : Form
    {
        DockPanel dockPanel = new DockPanel();

        frmSolutionExplorer solutionExplorer = new frmSolutionExplorer();
        frmOutput output = new frmOutput();

        public frmMain()
        {
            InitializeComponent();
            dockPanel.Dock = DockStyle.Fill;
            
            dockPanel.ShowDocumentIcon = true;
            dockPanel.BackColor = Color.AliceBlue;
            toolStripContainer1.ContentPanel.Controls.Add(dockPanel);
            
            solutionExplorer.Show(dockPanel,DockState.DockRight);
            output.Show(dockPanel, DockState.DockBottom);

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
          //  dockPanel.SaveAsXml()
        }
    }
}
