
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
    public partial class frmOutput : DockContent
    {

        CoreGets coreGets = new CoreGets();

       

        //internal delegate void LogEventHandlerHandler(LogEventTypes EventType, string status);
        //internal event LogEventHandlerHandler LogEvent;

        public frmOutput()
        {
            InitializeComponent();
            Task tStart = new Task(() => Start());
            tStart.Start();

        }


        private void Start()
        {
            fctb.AppendText("OS Development IDE Version: " + coreGets.GetExecutableVersion());
        }
    }
}
