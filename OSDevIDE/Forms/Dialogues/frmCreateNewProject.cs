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


        internal delegate void LogEventHandlerHandler(OSDevIDE.Classes.Enumerations.LoggingEnumerations.LogEventTypes EventType, string status);
        internal event LogEventHandlerHandler LogEvent;

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
            if(string.IsNullOrEmpty(tbProjectName.Text))
            {
                MessageBox.Show("We Need a Name for your project!");
            }
            else
            {
                if(string.IsNullOrEmpty(tbApplicationName.Text))
                {
                    tbApplicationName.Text = tbProjectName.Text;
                }
                else
                {
                    if(string.IsNullOrEmpty(tbSaveLocation.Text))
                    {
                        MessageBox.Show("There must be a location set to save the project to!");
                    }
                    else
                    {
                        // Everything has been validated - Let's Create The Project
                        // Project File is saved in two locations.
                        // The Properties.Settings.Default.ApplicationFolderPath and
                        // the Properties.Settings.Default.DefaultProjectFolder



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
            if(dr == System.Windows.Forms.DialogResult.OK)
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
