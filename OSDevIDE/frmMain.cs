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
      
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;

      
        frmProject projectForm = new frmProject();
        frmOutput outputForm = new frmOutput();
        frmStartup startupForm = new frmStartup();

        public frmMain()
        {
            InitializeComponent();          
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            
            //TODO: Change the Hard coded layout.xml location
            if (File.Exists("C:\\layout.xml"))
                dockPanel.LoadFromXml("C:\\layout.xml", m_deserializeDockContent);


            Task tStartUp = new Task(() => Start());
            tStartUp.Start();
        }

        private void Start()
        {
            frmMainLog("Starting Up");
        }

        //TODO: Clean this up
        private void frmMainLog(string Message, OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes le = Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information)
        {
            outputForm.OutputLog(le, Message);
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

        #region Menu -> Windows -> Standard
        private void startupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startupToolStripMenuItem.Checked)
            {
                startupToolStripMenuItem.Checked = false;
                startupForm.Hide();
            }
            else
            {
                startupForm.Show(dockPanel, DockState.Document);
                startupToolStripMenuItem.Checked = true;
            }
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectToolStripMenuItem.Checked)
            {
                logginToolStripMenuItem.Checked = false;
                projectForm.Hide();
            }
            else
            {
                projectForm.Show(dockPanel, DockState.DockRight);
                projectToolStripMenuItem.Checked = true;
            }

        }

        private void logginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logginToolStripMenuItem.Checked)
            {
                logginToolStripMenuItem.Checked = false;
                outputForm.Hide();
            }
            else
            {
                outputForm.Show(dockPanel, DockState.DockBottom);
                logginToolStripMenuItem.Checked = true;
            }
        }
        #endregion
        

    }
}
