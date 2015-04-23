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

        delegate void SetLabelTextCallback(Label lbl, string text);
        CoreUserIdle cui = new CoreUserIdle(true); // Start monitoring User Activity

        public frmProject()
        {
            InitializeComponent();
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
           

        }

        void fsw_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void fsw_Error(object sender, System.IO.ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        void fsw_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        void fsw_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentProjectName")
            {
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
            ListDirectory(tvProjectTree, Path.Combine(Properties.Settings.Default.ApplicationFolderPath, Properties.Settings.Default.CurrentProjectName));
            tvProjectTree.ExpandAll();
            fsw.Changed += fsw_Changed;
            fsw.Created += fsw_Created;
            fsw.Error += fsw_Error;
            fsw.Renamed += fsw_Renamed;
        }


        private static void ListDirectory(TreeView treeView, string path)
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
    }
}
