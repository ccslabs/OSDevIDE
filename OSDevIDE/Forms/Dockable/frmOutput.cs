
using OSDevIDE.Classes.Core;
using OSDevIDE.Classes.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        delegate void SetFCTBMessageCallBack(string text);


        //internal delegate void LogEventHandlerHandler(LogEventTypes EventType, string status);
        //internal event LogEventHandlerHandler LogEvent;

        public frmOutput()
        {
            InitializeComponent();
            fctb.ForeColor = Color.CornflowerBlue;
            Task tStart = new Task(() => Start());
            tStart.Start();

        }


        private void Start()
        {
            SetFCTBMessage("OS Development IDE Version: " + coreGets.GetExecutableVersion());
        }


        internal void OutputLog(LoggingEnumerations.LogEventTypes logEnum, string Message)
        {
            StackTrace stackTrace = new StackTrace();
            string callingMethod = stackTrace.GetFrame(1).GetMethod().Name; // Display if in Debug Mode - which Method called me
          
            switch (logEnum)
            {
                case LoggingEnumerations.LogEventTypes.Success:
                    fctb.ForeColor = Color.Green;
                    break;
                case LoggingEnumerations.LogEventTypes.Warning:
                    fctb.ForeColor = Color.Orange;
                    break;
                case LoggingEnumerations.LogEventTypes.Failure:
                    fctb.ForeColor = Color.Red;
                    break;
                default:
                    fctb.ForeColor = Color.CornflowerBlue;
                    break;
            }

#if DEBUG
            Message = "[" + callingMethod + "]\t" + Message;
#endif
            Message = DateTime.Now + "\t" + Message;
            SetFCTBMessage(Message);


        }

        private void SetFCTBMessage(string Message)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.fctb.InvokeRequired)
            {
                SetFCTBMessageCallBack d = new SetFCTBMessageCallBack(SetFCTBMessage);
                this.Invoke(d, new object[] { Message });
            }
            else
            {
                this.fctb.AppendText(Message + Environment.NewLine);
            }
        }

    }
}
