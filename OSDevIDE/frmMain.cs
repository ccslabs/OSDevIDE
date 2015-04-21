using OSDevIDE.Classes.Core;
using OSDevIDE.Forms.Dialogues;
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
            string callingMethod = stackTrace.GetFrame(2).GetMethod().Name; // Display if in Debug Mode - which Method called me
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
            frmMainLog("Getting Docking Layout -> " + persistString);
            if (persistString == typeof(frmProject).ToString())
            {
                projectToolStripMenuItem.Checked = true;
                projectForm.Disposed += projectForm_Disposed;
                return projectForm;
            }
            else if (persistString == typeof(frmOutput).ToString())
            {
                logginToolStripMenuItem.Checked = true;
                outputForm.Disposed += outputForm_Disposed;
                return outputForm;
            }
            else if (persistString == typeof(frmStartup).ToString())
            {
                startupToolStripMenuItem.Checked = true;
                startupForm.Disposed += startupForm_Disposed;
                return startupForm;
            }
            else
            {
                return null;
            }
        }

        #region Dockable Forms Disposing
        void startupForm_Disposed(object sender, EventArgs e)
        {
            startupToolStripMenuItem.Checked = false;
        }

        void outputForm_Disposed(object sender, EventArgs e)
        {
            logginToolStripMenuItem.Checked = false;
        }

        private void projectForm_Disposed(object sender, EventArgs e)
        {
            projectToolStripMenuItem.Checked = false;
        }
        #endregion

        #region Menu -> Project
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Menu -> Windows -> Standard
        private void startupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateFormDisplay(startupToolStripMenuItem, startupForm, DockState.Document);
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateFormDisplay(projectToolStripMenuItem, projectForm, DockState.DockRight);
        }

        private void logginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateFormDisplay(logginToolStripMenuItem, outputForm, DockState.DockBottom);
        }

        // http://stackoverflow.com/questions/29757509/c-sharp-refactoring-to-nearly-identical-methods
        private void UpdateFormDisplay<TForm>(ToolStripMenuItem menuItem, TForm frmName, DockState dockState) where TForm : DockContent, new()
        {
            if (menuItem.Checked)
            {
                menuItem.Checked = false;
                if (!frmName.IsDisposed) frmName.Hide();
            }
            else
            {
                if (frmName.IsDisposed)
                    frmName = new TForm();
                frmName.Show(dockPanel, dockState);
                menuItem.Checked = true;
            }
        }

        #endregion

        #region New Project
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            CreateNewProject();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewProject();
        }

        private void CreateNewProject()
        {
            frmMainLog("User Creating New Project");
            frmCreateNewProject createNewProjectForm = new frmCreateNewProject();
            createNewProjectForm.LogEvent += createNewProjectForm_LogEvent;
            DialogResult dr = createNewProjectForm.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Load the Project File
                // Populate ProjectClass
                // Create the Default folders
                // Populate the Project Window
                // Commit To GitHub <Free> or <PaidFor> User decides.

            }
            else
                frmMainLog("User Canceled the New Project");

        }

        void createNewProjectForm_LogEvent(Classes.Enumerations.LoggingEnumerations.LogEventTypes EventType, string status)
        {
            frmMainLog(status, EventType);
        }
        #endregion

       
    }
}
