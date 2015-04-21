
using FastColoredTextBoxNS;
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
        delegate void SetFCTBMessageCallBack(string text, Style style);


        TextStyle infoStyle = new TextStyle(Brushes.CornflowerBlue, null, FontStyle.Regular);
        TextStyle warningStyle = new TextStyle(Brushes.Yellow, null, FontStyle.Regular);
        TextStyle errorStyle = new TextStyle(Brushes.PaleVioletRed, null, FontStyle.Regular);
        TextStyle successStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);

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
            string version = "OS Development IDE Version: " + coreGets.GetExecutableVersion();
            SetFCTBMessage(version, infoStyle);
            this.Text = this.Text + "   [" + version + "]";
        }


        internal void OutputLog(LoggingEnumerations.LogEventTypes logEnum, string Message)
        {
            Style style = infoStyle;
            StackTrace stackTrace = new StackTrace();
            string callingMethod = stackTrace.GetFrame(2).GetMethod().Name; // Display if in Debug Mode - which Method called me

            switch (logEnum)
            {
                case LoggingEnumerations.LogEventTypes.Success:
                    style = successStyle;
                    break;
                case LoggingEnumerations.LogEventTypes.Warning:
                    style = warningStyle;
                    break;
                case LoggingEnumerations.LogEventTypes.Failure:
                    style = errorStyle;
                    break;
                default:
                    style = infoStyle;
                    break;
            }

#if DEBUG
            Message = "[" + callingMethod + "]" + Message;
#endif
            Message = DateTime.Now + "\t" + Message;
            SetFCTBMessage(Message, style);


        }

        private void SetFCTBMessage(string Message, Style style)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.fctb.InvokeRequired)
            {
                SetFCTBMessageCallBack d = new SetFCTBMessageCallBack(SetFCTBMessage);
                this.Invoke(d, new object[] { Message, style });
            }
            else
            {
                fctb.BeginUpdate();
                fctb.Selection.BeginUpdate();

                //add text with predefined style
                fctb.InsertText(Message + Environment.NewLine, style);

                fctb.GoEnd();//scroll to end of the textMessage
                //
                fctb.Selection.EndUpdate();
                fctb.EndUpdate();

                //FIX: May need to return focus to the previously focused object
                fctb.Focus();
            }
        }

    }
}
