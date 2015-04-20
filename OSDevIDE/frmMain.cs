using OSDevIDE.Classes.Core;
using OSDevIDE.Forms.Dockable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            
            // SETUP Some Debugging Properties
#if DEBUG
            Properties.Settings.Default.ApplicationFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OSIDE");
            Properties.Settings.Default.DockingLayoutFilePath = Path.Combine(Properties.Settings.Default.ApplicationFolderPath, "DockingLayout.xml");
#endif


            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
           
            if (File.Exists(Properties.Settings.Default.DockingLayoutFilePath))
                dockPanel.LoadFromXml(Properties.Settings.Default.DockingLayoutFilePath, m_deserializeDockContent);


            Task tStartUp = new Task(() => Start());
            tStartUp.Start();
        }

        private void Start()
        {
            CoreSetup cs = new CoreSetup();
            frmMainLog("Starting Up...");

            if (cs.FirtsRun())
            {
                frmMainLog("This program has not been setup since being installed", Classes.Enumerations.LoggingEnumerations.LogEventTypes.Warning);
                Properties.Settings.Default.ApplicationFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OSIDE");
                Properties.Settings.Default.DockingLayoutFilePath = Path.Combine(Properties.Settings.Default.ApplicationFolderPath, "DockingLayout.xml");
                if (!Directory.Exists(Properties.Settings.Default.ApplicationFolderPath))
                    Directory.CreateDirectory(Properties.Settings.Default.ApplicationFolderPath);
                frmMainLog("Initial Setup Complete", Classes.Enumerations.LoggingEnumerations.LogEventTypes.Success);
            }
            else
            {
                frmMainLog("This program has been setup properly");
            }
        }

        //TODO: Clean this up
        private void frmMainLog(string Message, OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes le = Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information)
        {
            StackTrace stackTrace = new StackTrace();
            string callingMethod = stackTrace.GetFrame(1).GetMethod().Name; // Display if in Debug Mode - which Method called me
            outputForm.OutputLog(le, "[" + callingMethod + "]\t" + Message);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();

            if (m_bSaveLayout)
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.DockingLayoutFilePath))
                    dockPanel.SaveAsXml(Properties.Settings.Default.DockingLayoutFilePath);
                else
                    frmMainLog("Cannot Save Docking Layout File; the DockingLayOutFilePath Setting has not be populated", Classes.Enumerations.LoggingEnumerations.LogEventTypes.Failure);
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            frmMainLog("Getting Docking Layout");
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
