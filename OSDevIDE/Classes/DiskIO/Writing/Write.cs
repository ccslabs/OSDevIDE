using OSDevIDE.Classes.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDevIDE.Classes.DiskIO.Writing
{
    class Write
    {

        internal bool SaveProjectApplicationFolder(ProjectClass projectClass)
        {
            string ApplicationFolderPath = Properties.Settings.Default.ApplicationFolderPath;   // Standard Save location (Always Save Here)
            string path = "";

            // This is always saved here no matter where the User said the project is saved to (saved to both locations if necessary)
            path = Path.Combine(ApplicationFolderPath, projectClass.ProjectName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, projectClass.ProjectName + ".osp");
            return SaveProjectTo(projectClass,path);
        }

        internal bool SaveProjectDefaultFolder(ProjectClass projectClass)
        {
            string DefaultProjectFolder = Properties.Settings.Default.DefaultProjectFolder;     // Alternative Save Location
            string path = "";

            // Only write this out if the DefaultProjectFolder Actually contains something !
          
                path = Path.Combine(DefaultProjectFolder, projectClass.ProjectName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, projectClass.ProjectName + ".osp");
               return SaveProjectTo(projectClass, path);           
        }

        private bool SaveProjectTo(ProjectClass projectClass, string saveFileToLocation)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {

                fs = new FileStream(saveFileToLocation, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                sw = new StreamWriter(fs);

                sw.WriteLine(projectClass.ApplicationName);
                sw.WriteLine(projectClass.ProjectName);
                sw.WriteLine(projectClass.ProjectSaveLocation);
                sw.WriteLine(projectClass.ProjectCreatedOn.ToString());

                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
            return false;
        }

    }
}
