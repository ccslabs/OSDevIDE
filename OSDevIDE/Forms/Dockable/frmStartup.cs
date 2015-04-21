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
    public partial class frmStartup : DockContent
    {

        delegate void SetLabelTextCallback(Label lbl, string text);

        public frmStartup()
        {
            InitializeComponent();
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;

        }

        void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentProjectName")
               SetLabelText(lblCurrentProjectName, Properties.Settings.Default.CurrentProjectName);
            if (e.PropertyName == "CurrentProjectCreationDate")
               SetLabelText(lblCreationDate, Properties.Settings.Default.CurrentProjectCreationDate);
        }

        private void PopulateStartPage()
        {
            SetLabelText(lblCurrentProjectName, Properties.Settings.Default.CurrentProjectName);
            SetLabelText(lblCreationDate, Properties.Settings.Default.CurrentProjectCreationDate);
        }

        /// <summary>
        /// Ensures the Middle GroupBox gbTeamDetails stay centered between the other two GroupBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flpBottom_SizeChanged(object sender, EventArgs e)
        {
            //TODO: This does not work as expected
            int pWidth = flpBottom.Width - (flpBottom.Padding.Left + flpBottom.Padding.Right);
            int gbWidth = gbTeamDetails.Width;  // All the GroupBoxes should be the same width
            int pSpacing = (Math.Max(gbTeamDetails.Left, gbCurrentProject.Right) - Math.Min(gbTeamDetails.Left, gbCurrentProject.Right)) * 2;   // Total Spacing between the GroupBoxes
            pWidth = pWidth - pSpacing; // True usable width of the FlowLayOutPanel

            gbCurrentProject.Size = new Size((pWidth / 3) - 5, gbCurrentProject.Height);
            gbTeamDetails.Size = new Size((pWidth / 3) - 5, gbCurrentProject.Height);
            gbYourDetails.Size = new Size((pWidth / 3) - 5, gbCurrentProject.Height);

        }

        private void frmStartup_Load(object sender, EventArgs e)
        {
            PopulateStartPage();
        }

        private void SetLabelText(Label lbl, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl.InvokeRequired)
            {
                SetLabelTextCallback d = new SetLabelTextCallback(SetLabelText);
                this.Invoke(d, new object[] {lbl, text });
            }
            else
            {
                lbl.Text = text;
            }

        }
    }
}
