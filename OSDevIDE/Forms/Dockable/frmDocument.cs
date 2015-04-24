using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace OSDevIDE.Forms.Dockable
{
    public partial class frmDocument : DockContent
    {

        public frmDocument(string DocumentPath)
        {
            FileInfo finfo = new FileInfo(DocumentPath);
            InitializeComponent();
            this.Text = finfo.Name;

            fctbDocument.LineInserted += fctbDocument_LineInserted;
            fctbDocument.LineRemoved += fctbDocument_LineRemoved;
            fctbDocument.SelectionChanged += fctbDocument_SelectionChanged;
            fctbDocument.SelectionChangedDelayed += fctbDocument_SelectionChangedDelayed;
            fctbDocument.TextChanged += fctbDocument_TextChanged;
            fctbDocument.TextChangedDelayed += fctbDocument_TextChangedDelayed;
            fctbDocument.TextChanging += fctbDocument_TextChanging;
            fctbDocument.UndoRedoStateChanged += fctbDocument_UndoRedoStateChanged;
            fctbDocument.VisualMarkerClick += fctbDocument_VisualMarkerClick;
            fctbDocument.OpenBindingFile(DocumentPath, Encoding.UTF8);
        }

        void fctbDocument_VisualMarkerClick(object sender, FastColoredTextBoxNS.VisualMarkerEventArgs e)
        {
           // throw new NotImplementedException();
        }

        void fctbDocument_UndoRedoStateChanged(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        void fctbDocument_TextChanging(object sender, FastColoredTextBoxNS.TextChangingEventArgs e)
        {
           // throw new NotImplementedException();
        }

        void fctbDocument_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        void fctbDocument_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        void fctbDocument_SelectionChangedDelayed(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        void fctbDocument_SelectionChanged(object sender, EventArgs e)
        {
          //  throw new NotImplementedException();
        }

        void fctbDocument_LineRemoved(object sender, FastColoredTextBoxNS.LineRemovedEventArgs e)
        {
          //  throw new NotImplementedException();
        }

        void fctbDocument_LineInserted(object sender, FastColoredTextBoxNS.LineInsertedEventArgs e)
        {
           // throw new NotImplementedException();
        }
    }
}
