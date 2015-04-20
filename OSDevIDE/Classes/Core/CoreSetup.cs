using OSDevIDE.Forms.Dockable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDevIDE.Classes.Core
{
    class CoreSetup
    {


        /// <summary>
        /// Is this the First Time the Application Has been run since installing?
        /// </summary>
        /// <returns>
        /// Bool: True if Yes
        /// Bool: False if No
        /// </returns>
        /// <remarks>
        /// We just check to see if the Application's Folder in the User's Documents Folder exists
        /// </remarks>
        internal bool FirtsRun()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.ApplicationFolderPath))
            {
                return true;
            }
            return false;
        }

    }
}
