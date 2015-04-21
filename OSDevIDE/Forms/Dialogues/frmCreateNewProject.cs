﻿using OSDevIDE.Classes.DiskIO.Writing;
using OSDevIDE.Classes.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSDevIDE.Forms.Dialogues
{
    public partial class frmCreateNewProject : Form
    {


        internal delegate void LogEventHandler(OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes EventType, string status);
        internal event LogEventHandler LogEvent;

        public frmCreateNewProject()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (LogEvent != null) LogEvent(OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes.Information, "User Canceled New Project ");
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbProjectName.Text))
            {
                MessageBox.Show("We Need a Name for your project!");
            }
            else
            {
                if (string.IsNullOrEmpty(tbApplicationName.Text))
                {
                    tbApplicationName.Text = tbProjectName.Text;
                }
                else
                {
                    if (string.IsNullOrEmpty(tbSaveLocation.Text))
                    {
                        MessageBox.Show("There must be a location set to save the project to!");
                    }
                    else
                    {
                        // Everything has been validated - Let's Create The Project
                        // the Properties.Settings.Default.DefaultProjectFolder and
                        ProjectClass pc = new ProjectClass();
                        pc.ApplicationName = tbApplicationName.Text;
                        pc.ProjectCreatedOn = DateTime.Now;
                        pc.ProjectName = tbProjectName.Text;
                        pc.ProjectSaveLocation = tbSaveLocation.Text;

                        Write writeProjectToDisk = new Write();

                        // Write the project file to the standard location
                        if (writeProjectToDisk.SaveProjectApplicationFolder(pc))
                        {
                            LogEvent(OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes.Success, "Project " + pc.ProjectName + " successfully created");
                        }
                        else
                        {
                            //TODO: We should get the reason why the failure occurred
                            LogEvent(OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes.Failure, "Failed to Create Project " + pc.ProjectName);
                            MessageBox.Show("Failed to Create Project");
                        }

                        if (!string.IsNullOrEmpty(Properties.Settings.Default.DefaultProjectFolder) && Properties.Settings.Default.DefaultProjectFolder != Properties.Settings.Default.ApplicationFolderPath)
                        {
                            if (writeProjectToDisk.SaveProjectDefaultFolder(pc))
                            {
                                LogEvent(OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes.Success, "Project " + pc.ProjectName + " successfully created");
                            }
                            else
                            {
                                //TODO: We should get the reason why the failure occurred
                                LogEvent(OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes.Failure, "Failed to Create Project " + pc.ProjectName);
                                MessageBox.Show("Failed to Create Project");
                            }
                        }
                    }
                }
            }
        }

        private void tbProjectName_TextChanged(object sender, EventArgs e)
        {
            tbApplicationName.Text = tbProjectName.Text;
        }

        private void tbSaveLocation_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultProjectFolder = tbSaveLocation.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = FBD.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                tbSaveLocation.Text = FBD.SelectedPath;
            }
            else
            {
                tbSaveLocation.Text = Properties.Settings.Default.ApplicationFolderPath;
            }
        }
    }
}
