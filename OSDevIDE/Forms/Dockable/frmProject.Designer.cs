namespace OSDevIDE.Forms.Dockable
{
    partial class frmProject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProject));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIdleTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDevelopmentTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tvProjectTree = new System.Windows.Forms.TreeView();
            this.ilTreeviewImages = new System.Windows.Forms.ImageList(this.components);
            this.timerSeconds = new System.Windows.Forms.Timer(this.components);
            this.fsw = new System.IO.FileSystemWatcher();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsw)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.lblIdleTime);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblDevelopmentTime);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblProjectName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel1.Size = new System.Drawing.Size(334, 78);
            this.panel1.TabIndex = 0;
            // 
            // lblIdleTime
            // 
            this.lblIdleTime.AutoSize = true;
            this.lblIdleTime.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::OSDevIDE.Properties.Settings.Default, "LblValueForecolour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblIdleTime.ForeColor = global::OSDevIDE.Properties.Settings.Default.LblValueForecolour;
            this.lblIdleTime.Location = new System.Drawing.Point(71, 56);
            this.lblIdleTime.Name = "lblIdleTime";
            this.lblIdleTime.Size = new System.Drawing.Size(16, 13);
            this.lblIdleTime.TabIndex = 5;
            this.lblIdleTime.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::OSDevIDE.Properties.Settings.Default, "LblTitleForecolour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label3.ForeColor = global::OSDevIDE.Properties.Settings.Default.LblTitleForecolour;
            this.label3.Location = new System.Drawing.Point(12, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Idle Time:";
            // 
            // lblDevelopmentTime
            // 
            this.lblDevelopmentTime.AutoSize = true;
            this.lblDevelopmentTime.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::OSDevIDE.Properties.Settings.Default, "LblValueForecolour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblDevelopmentTime.ForeColor = global::OSDevIDE.Properties.Settings.Default.LblValueForecolour;
            this.lblDevelopmentTime.Location = new System.Drawing.Point(117, 32);
            this.lblDevelopmentTime.Name = "lblDevelopmentTime";
            this.lblDevelopmentTime.Size = new System.Drawing.Size(16, 13);
            this.lblDevelopmentTime.TabIndex = 3;
            this.lblDevelopmentTime.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::OSDevIDE.Properties.Settings.Default, "LblTitleForecolour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label4.ForeColor = global::OSDevIDE.Properties.Settings.Default.LblTitleForecolour;
            this.label4.Location = new System.Drawing.Point(12, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Development Time:";
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::OSDevIDE.Properties.Settings.Default, "LblValueForecolour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblProjectName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::OSDevIDE.Properties.Settings.Default, "CurrentProjectName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblProjectName.ForeColor = global::OSDevIDE.Properties.Settings.Default.LblValueForecolour;
            this.lblProjectName.Location = new System.Drawing.Point(92, 9);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(16, 13);
            this.lblProjectName.TabIndex = 1;
            this.lblProjectName.Text = global::OSDevIDE.Properties.Settings.Default.CurrentProjectName;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::OSDevIDE.Properties.Settings.Default, "LblTitleForecolour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label1.ForeColor = global::OSDevIDE.Properties.Settings.Default.LblTitleForecolour;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Name:";
            // 
            // tvProjectTree
            // 
            this.tvProjectTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvProjectTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProjectTree.ImageIndex = 0;
            this.tvProjectTree.ImageList = this.ilTreeviewImages;
            this.tvProjectTree.Location = new System.Drawing.Point(0, 79);
            this.tvProjectTree.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tvProjectTree.Name = "tvProjectTree";
            this.tvProjectTree.SelectedImageIndex = 0;
            this.tvProjectTree.Size = new System.Drawing.Size(334, 433);
            this.tvProjectTree.TabIndex = 1;
            // 
            // ilTreeviewImages
            // 
            this.ilTreeviewImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTreeviewImages.ImageStream")));
            this.ilTreeviewImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTreeviewImages.Images.SetKeyName(0, "flowchart.ico");
            this.ilTreeviewImages.Images.SetKeyName(1, "162.ico");
            this.ilTreeviewImages.Images.SetKeyName(2, "Closed.ico");
            this.ilTreeviewImages.Images.SetKeyName(3, "basic1-051_file_binary_code-128.ico");
            // 
            // timerSeconds
            // 
            this.timerSeconds.Interval = 1000;
            this.timerSeconds.Tick += new System.EventHandler(this.timerSeconds_Tick);
            // 
            // fsw
            // 
            this.fsw.EnableRaisingEvents = true;
            this.fsw.IncludeSubdirectories = true;
            this.fsw.SynchronizingObject = this;
            // 
            // frmProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 518);
            this.Controls.Add(this.tvProjectTree);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmProject";
            this.Text = "Project";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvProjectTree;
        private System.Windows.Forms.ImageList ilTreeviewImages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Label lblDevelopmentTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblIdleTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerSeconds;
        private System.IO.FileSystemWatcher fsw;
    }
}