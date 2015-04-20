using OSDevIDE.Forms.Dockable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;

      
        frmProject projectForm = new frmProject();
        frmOutput outputForm = new frmOutput();
        frmStartup startupForm = new frmStartup();

        public frmMain()
        {
            InitializeComponent();
            dockPanel.Dock = DockStyle.Fill;
            dockPanel.ShowDocumentIcon = true;
            dockPanel.BackColor = Color.DarkGray;

            this.Controls.Add(dockPanel);

            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            if (File.Exists("C:\\layout.xml"))
                dockPanel.LoadFromXml("C:\\layout.xml", m_deserializeDockContent);
           

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
          if(m_bSaveLayout)
          {
              dockPanel.SaveAsXml("C:\\layout.xml");
          }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(frmProject).ToString())
                return projectForm;
            else if (persistString == typeof(frmOutput).ToString())
                return outputForm;
            else if (persistString == typeof(frmStartup).ToString())
                return startupForm;
           
            else
            {
                projectForm.Show(dockPanel, DockState.DockRight);
                outputForm.Show(dockPanel, DockState.DockBottom);
                startupForm.Show(dockPanel, DockState.Document);
            
                return null;
            }
        }

    }
}
