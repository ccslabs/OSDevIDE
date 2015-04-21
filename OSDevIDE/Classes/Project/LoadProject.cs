using OSDevIDE.Classes.DiskIO.Reading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDevIDE.Classes.Project
{
    class LoadProject
    {
        /// <summary>
        /// Load the Current Project
        /// </summary>
        /// <param name="finfoLatest">
        /// FileInfo: Information about the location of the Current Project File
        /// </param>
        internal static void OpenProject(System.IO.FileInfo finfoLatest)
        {
            Read.LoadProject(finfoLatest.FullName);
        }

        internal static void OpenProject(string projectPath)
        {
            Read.LoadProject(projectPath);
        }
    }
}
