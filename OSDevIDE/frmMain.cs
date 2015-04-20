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

        public frmMain()
        {
            InitializeComponent();
            dockPanel.Dock = DockStyle.Fill;
            dockPanel.BackColor = Color.AliceBlue;
            toolStripContainer1.ContentPanel.Controls.Add(dockPanel);
        }
    }
}
