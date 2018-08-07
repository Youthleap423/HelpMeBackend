namespace HelpMeSync
{
    partial class frmMDI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDI));
            this.ssBar = new System.Windows.Forms.StatusStrip();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSynchronization = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEmailSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFCM = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTop.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssBar
            // 
            this.ssBar.Location = new System.Drawing.Point(0, 498);
            this.ssBar.Name = "ssBar";
            this.ssBar.Size = new System.Drawing.Size(725, 22);
            this.ssBar.TabIndex = 3;
            this.ssBar.Text = "statusStrip1";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.menuStrip1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(725, 24);
            this.pnlTop.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConfiguration});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(725, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuConfiguration
            // 
            this.mnuConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSynchronization,
            this.mnuEmailSettings,
            this.mnuFCM,
            this.mnuSep1,
            this.mnuExit});
            this.mnuConfiguration.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F);
            this.mnuConfiguration.Name = "mnuConfiguration";
            this.mnuConfiguration.Size = new System.Drawing.Size(94, 20);
            this.mnuConfiguration.Text = "Configuration";
            // 
            // mnuSynchronization
            // 
            this.mnuSynchronization.Name = "mnuSynchronization";
            this.mnuSynchronization.Size = new System.Drawing.Size(152, 22);
            this.mnuSynchronization.Text = "Settings";
            this.mnuSynchronization.Click += new System.EventHandler(this.mnuSynchronization_Click);
            // 
            // mnuEmailSettings
            // 
            this.mnuEmailSettings.Name = "mnuEmailSettings";
            this.mnuEmailSettings.Size = new System.Drawing.Size(152, 22);
            this.mnuEmailSettings.Text = "Email Settings";
            this.mnuEmailSettings.Click += new System.EventHandler(this.mnuEmailSettings_Click);
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            this.mnuSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(152, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            //// 
            //// mnuFCM
            //// 
            //this.mnuFCM.Name = "mnuFCM";
            //this.mnuFCM.Size = new System.Drawing.Size(152, 22);
            //this.mnuFCM.Text = "FCM";
            //this.mnuFCM.Click += new System.EventHandler(this.mnuFCM_Click);
            // 
            // frmMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 520);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.ssBar);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMDI";
            this.Text = "HelpMe Sync™ [ HelpMe Sync Management ]";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.StatusStrip ssBar;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuSynchronization;
        private System.Windows.Forms.ToolStripSeparator mnuSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuEmailSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuFCM;
        //private System.Windows.Forms.ToolStripMenuItem pricingMethodToolStripMenuItem;
    }
}