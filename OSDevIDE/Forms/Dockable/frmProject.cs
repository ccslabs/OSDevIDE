using OSDevIDE.Classes.Core;
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

        void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentProjectName")
            {
                SetLabelText(lblProjectName, Properties.Settings.Default.CurrentProjectName);
                timerSeconds.Enabled = true;
                timerSeconds.Interval = 1000;
                timerSeconds.Start();
            }
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
