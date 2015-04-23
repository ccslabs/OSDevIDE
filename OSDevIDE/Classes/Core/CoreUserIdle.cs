using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OSDevIDE.Classes.Core
{
    class CoreUserIdle
    {

        private Timer timerIdle = new Timer();
        internal bool IsIdle = true;
        private long vIdleSeconds = 0;
        private long vActiveSeconds = 0;

        internal long ActiveSeconds { get; set; }
        
        internal long IdleSeconds { get; set; }


        internal CoreUserIdle(bool Start)
        {
            if (Start && !timerIdle.Enabled) // Timer told to start but is not running
            {
                timerIdle.Interval = 1000;
                timerIdle.Tick += timerIdle_Tick;
                timerIdle.Enabled = true;
                timerIdle.Start();
            }

            else if (!Start && timerIdle.Enabled) // Timer told to start and is running
            {
                timerIdle.Stop();
                timerIdle.Enabled = false;
            }

            // Ignore Timer being told to stop when already stopped
            // Ignore Timer told to start and is already running
        }

        void timerIdle_Tick(object sender, EventArgs e)
        {
            if (IsIdle)
                vIdleSeconds++;
            else
                vActiveSeconds++;

            ActiveSeconds = vActiveSeconds;
            IdleSeconds = vIdleSeconds;
        }


    }
}
