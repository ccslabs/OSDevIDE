using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDevIDE.Classes.Project
{
    class ProjectClass
    {

        internal string ProjectName { get; set; }
        internal string ApplicationName { get; set; }
        internal string ProjectSaveLocation { get; set; }
        internal DateTime ProjectCreatedOn { get; set; }

        internal List<ProjectTimings> Timeings = new List<ProjectTimings>();
    }

    class ProjectTimings
    {
        internal long SecondsWorking { get; set; }
        internal string InMethod { get; set; }
        internal string InFile { get; set; }
    }

}
