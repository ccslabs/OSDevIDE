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
    public partial class frmBootloaderWizard : Form
    {

        internal string StageOneMessage { get; set; }
        internal string StageTwoMessage { get; set; }

        public frmBootloaderWizard()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (!string.IsNullOrEmpty(tbStageOneMessage.Text))
                StageOneMessage = tbStageOneMessage.Text;
            if (!string.IsNullOrEmpty(tbStageTwoMessage.Text))
                StageTwoMessage = tbStageTwoMessage.Text;
            this.Close();
        }
    }
}
