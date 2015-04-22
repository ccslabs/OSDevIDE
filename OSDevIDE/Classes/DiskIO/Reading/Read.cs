using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDevIDE.Classes.DiskIO.Reading
{
    class Read
    {

        //private void delegate SetProperty(Properties.Settings DefSDettings, string value);

        /// <summary>
        /// Load the Project File and place information in the Properties.Settings
        /// </summary>
        /// <param name="pathToProjectFile"></param>
        internal static bool LoadProject(string pathToProjectFile)
        {
            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                fs = new FileStream(pathToProjectFile, FileMode.Open, FileAccess.Read, FileShare.None);
                sr = new StreamReader(fs);

                // This is what we are currently writing out to the Project File.
                //sw.WriteLine(projectClass.ApplicationName);
                //sw.WriteLine(projectClass.ProjectName);
                //sw.WriteLine(projectClass.ProjectSaveLocation);
                //sw.WriteLine(projectClass.ProjectCreatedOn.ToString());

                sr.ReadLine(); // Application Name - not used just now
                Properties.Settings.Default.CurrentProjectName = sr.ReadLine();
                Properties.Settings.Default.CurrentProjectPath = sr.ReadLine();
                Properties.Settings.Default.CurrentProjectCreationDate = sr.ReadLine();

                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload(); // Maybe required to refresh the values on StartPage

                sr.Close();
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
                return false;
            }
        }
    }
}
