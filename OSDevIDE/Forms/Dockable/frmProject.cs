using OSDevIDE.Classes.Core;
using OSDevIDE.Classes.DiskIO.Reading;
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

namespace OSDevIDE.Forms.Dockable
{
    public partial class frmProject : DockContent
    {

        internal delegate void LogEventHandler(OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes EventType, string status);
        internal event LogEventHandler LogEvent;
        internal delegate void OpenDocumentEventHandler(OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes EventType, string filePath);
        internal event OpenDocumentEventHandler OpenDocumentEvent;

        delegate void SetLabelTextCallback(Label lbl, string text);
        CoreUserIdle cui = new CoreUserIdle(true); // Start monitoring User Activity

        public frmProject()
        {
            InitializeComponent();
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;


        }

        void fsw_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            if (LogEvent != null) LogEvent(Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information, "File Renamed from " + e.OldFullPath + " to " + e.FullPath);
        }

        void fsw_Error(object sender, System.IO.ErrorEventArgs e)
        {
            if (LogEvent != null) LogEvent(Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information, "File Error " + e.GetException().Message);
        }

        void fsw_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            if (LogEvent != null) LogEvent(Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information, "File Created " + e.FullPath);
        }

        void fsw_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            if (LogEvent != null) LogEvent(Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information, "File Changed " + e.FullPath);
        }

        void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentProjectName")
            {
                if (LogEvent != null) LogEvent(Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information, "Current Project Name " + Properties.Settings.Default.CurrentProjectName);
                SetLabelText(lblProjectName, Properties.Settings.Default.CurrentProjectName);
                timerSeconds.Enabled = true;
                timerSeconds.Interval = 1000;
                timerSeconds.Start();
                fsw.Path = Path.Combine(Properties.Settings.Default.CurrentProjectPath, Properties.Settings.Default.CurrentProjectName);

                AddNodes();
            }
        }

        private void AddNodes()
        {
            if (LogEvent != null) LogEvent(Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information, "Adding Nodes ");
            ListDirectory(tvProjectTree, Path.Combine(Properties.Settings.Default.ApplicationFolderPath, Properties.Settings.Default.CurrentProjectName));
            tvProjectTree.ExpandAll();
            fsw.Changed += fsw_Changed;
            fsw.Created += fsw_Created;
            fsw.Error += fsw_Error;
            fsw.Renamed += fsw_Renamed;
        }


        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    childDirectoryNode.ImageIndex = 2;
                    childDirectoryNode.SelectedImageIndex = 1;
                    currentNode.Nodes.Add(childDirectoryNode);

                    stack.Push(childDirectoryNode);
                }
                foreach (var file in directoryInfo.GetFiles())
                {
                    TreeNode tn = new TreeNode(file.Name);
                    tn.ImageIndex = 3;
                    tn.SelectedImageIndex = 3;
                    tn.Tag = "file";

                    currentNode.Nodes.Add(tn);
                }
            }

            treeView.Nodes.Add(node);
        }


        private void SetLabelText(Label lbl, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl.InvokeRequired)
            {
                SetLabelTextCallback d = new SetLabelTextCallback(SetLabelText);
                this.Invoke(d, new object[] { lbl, text });
            }
            else
            {
                lbl.Text = text;
            }

        }

        private void timerSeconds_Tick(object sender, EventArgs e)
        {

            TimeSpan aTime = TimeSpan.FromSeconds(cui.ActiveSeconds);
            SetLabelText(lblDevelopmentTime, aTime.ToString());

            TimeSpan iTime = TimeSpan.FromSeconds(cui.IdleSeconds);
            SetLabelText(lblIdleTime, iTime.ToString());

        }

        private void tvProjectTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            try
            {
                if ((string)e.Node.Tag == "file")
                {

                    // display the file if left click!
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        
                        // Liberator\Liberator.osp
                        string fullPath = Path.Combine(Properties.Settings.Default.ApplicationFolderPath, e.Node.FullPath);
                        if (LogEvent != null) LogEvent(Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information, "Opening File: " + fullPath);
                        if (OpenDocumentEvent != null) OpenDocumentEvent(Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information, fullPath);
                    }
                    else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {

                        // show the file's context menu
                    }

                }
            }
            catch (Exception) // Probably e.Node.Tag contains a DirectoryInfo
            {
                // expand the directory if it is not expanded and if left click

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {

                    e.Node.Expand();
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {

                    // Show the Folder's Context Menu - Dependent upon which folder has been clicked on.
                }
            }
        }

        /// <summary>
        /// Insert's the basic template for the two stage bootloader - via a wizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stageBootloaderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
