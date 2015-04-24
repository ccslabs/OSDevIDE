using OSDevIDE.Classes.Core;
using OSDevIDE.Classes.DiskIO.Writing;
using OSDevIDE.Classes.Enumerations;
using OSDevIDE.Classes.Project;
using OSDevIDE.Forms.Dialogues;
using OSDevIDE.Forms.Dockable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

        CoreUserIdle cui = new CoreUserIdle(false);

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


        #region Needing Re-factored and placed in the correct
        private void Start()
        {
            CoreSetup cs = new CoreSetup();
            frmMainLog("Starting Up...");

            if (cs.FirtsRun())
            {
                FirstRunSetup();
            }
            else
            {
                StandardRun();
            }
        }

        private void StandardRun()
        {
            frmMainLog("This program has been setup properly", LoggingEnumerations.LogEventTypes.Success);
            if (string.IsNullOrEmpty(Properties.Settings.Default.CurrentProjectPath))
            {
                // Ok so we don't know what the last Project was - let's see if we can find any
                ArrayList alProjects = FindPreviousProjects();

                LoadSuspectedCurrentProject(alProjects);
            }
            else
            {
                LoadReportedCurrentProject();
            }
        }

        private void LoadReportedCurrentProject()
        {
            frmMainLog("Loading Reported Current Project", LoggingEnumerations.LogEventTypes.Success);
            LoadProject.OpenProject(Properties.Settings.Default.CurrentProjectPath);
        }

        private void LoadSuspectedCurrentProject(ArrayList alProjects)
        {
            if (alProjects.Count > 0)
            {
                // Ok we have projects but which one is the Current project?
                // probably the last written to?
                FileInfo finfoLatest = null;

                foreach (FileInfo finfo in alProjects)
                {
                    if (finfoLatest == null)
                        finfoLatest = finfo;

                    if (finfo.LastWriteTime > finfoLatest.LastWriteTime)
                        finfoLatest = finfo;
                }

                // finfoLatest should now be the Current Project !!
                // so save the information to Properties so that the rest of the app can get the information easily
                frmMainLog("Loading Suspected Current Project", LoggingEnumerations.LogEventTypes.Warning);
                LoadProject.OpenProject(finfoLatest);

            }
            else
            {
                frmMainLog("There are no projects currently available.");
            }
        }

        private ArrayList FindPreviousProjects()
        {
            ArrayList alProjects = new ArrayList();
            string[] dirs = Directory.GetDirectories(Properties.Settings.Default.ApplicationFolderPath);
            if (dirs.Count() > 0) // Ok we have some folders here - any of them Project Folders?
            {
                foreach (string dir in dirs)
                {
                    string[] files = Directory.GetFiles(dir);
                    foreach (string file in files)
                    {
                        // .osp files are project files
                        FileInfo finfo = new FileInfo(file);
                        if (finfo.Extension.ToLowerInvariant().Contains("osp")) //TODO: Associate .osp files with this application
                        {
                            alProjects.Add(finfo);
                        }
                    }
                }
            }
            else
            {
                frmMainLog("There are no projects currently available.");
            }
            return alProjects;
        }

        private void FirstRunSetup()
        {
            frmMainLog("This program has not been setup since being installed", LoggingEnumerations.LogEventTypes.Warning);
            Properties.Settings.Default.ApplicationFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OSIDE");
            Properties.Settings.Default.DockingLayoutFilePath = Path.Combine(Properties.Settings.Default.ApplicationFolderPath, "DockingLayout.xml");
            if (!Directory.Exists(Properties.Settings.Default.ApplicationFolderPath))
                Directory.CreateDirectory(Properties.Settings.Default.ApplicationFolderPath);
            frmMainLog("Initial Setup Complete", LoggingEnumerations.LogEventTypes.Success);
        }
        #endregion


        //TODO: Clean this up
        private void frmMainLog(string Message, LoggingEnumerations.LogEventTypes le = LoggingEnumerations.LogEventTypes.Information)
        {
            StackTrace stackTrace = new StackTrace();
            string callingMethod = stackTrace.GetFrame(2).GetMethod().Name; // Display if in Debug Mode - which Method called me
            if (!callingMethod.Contains("<.ctor>b__0"))
                outputForm.OutputLog(le, "[" + callingMethod + "]\t" + Message);
            else
                outputForm.OutputLog(le, "\t" + Message);
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
                projectForm.LogEvent += projectForm_LogEvent;
                projectForm.OpenDocumentEvent += projectForm_OpenDocumentEvent;
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

        void projectForm_OpenDocumentEvent(LoggingEnumerations.LogEventTypes EventType, string path)
        {
            frmDocument documentForm = new frmDocument(path);           
            documentForm.Show(dockPanel, DockState.Document);
        }

       
        void projectForm_LogEvent(LoggingEnumerations.LogEventTypes EventType, string status)
        {
            frmMainLog(status, EventType);
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
                frmMainLog("Creating Project Directories");
                Write.CreateDefaultFolders();
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
