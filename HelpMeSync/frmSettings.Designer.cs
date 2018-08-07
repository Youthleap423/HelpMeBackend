namespace HelpMeSync
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.nmdScheduleMins = new System.Windows.Forms.NumericUpDown();
            this.label35 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.ddlDatabase = new System.Windows.Forms.ComboBox();
            this.ddlAuthentication = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.pnlSave = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ssBar = new System.Windows.Forms.StatusStrip();
            this.lblMessageBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSMSSchedulerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmdScheduleMins)).BeginInit();
            this.pnlSave.SuspendLayout();
            this.ssBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.txtSMSSchedulerName);
            this.pnlMain.Controls.Add(this.label22);
            this.pnlMain.Controls.Add(this.nmdScheduleMins);
            this.pnlMain.Controls.Add(this.label35);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.label13);
            this.pnlMain.Controls.Add(this.label15);
            this.pnlMain.Controls.Add(this.label17);
            this.pnlMain.Controls.Add(this.label20);
            this.pnlMain.Controls.Add(this.txtServer);
            this.pnlMain.Controls.Add(this.ddlDatabase);
            this.pnlMain.Controls.Add(this.ddlAuthentication);
            this.pnlMain.Controls.Add(this.label12);
            this.pnlMain.Controls.Add(this.label14);
            this.pnlMain.Controls.Add(this.label16);
            this.pnlMain.Controls.Add(this.txtPassword);
            this.pnlMain.Controls.Add(this.label18);
            this.pnlMain.Controls.Add(this.txtUserId);
            this.pnlMain.Controls.Add(this.label19);
            this.pnlMain.Controls.Add(this.btnTestConnection);
            this.pnlMain.Location = new System.Drawing.Point(7, 7);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(527, 343);
            this.pnlMain.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(184, 290);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 13);
            this.label22.TabIndex = 8;
            this.label22.Text = "Minute(s)";
            // 
            // nmdScheduleMins
            // 
            this.nmdScheduleMins.Location = new System.Drawing.Point(116, 286);
            this.nmdScheduleMins.Name = "nmdScheduleMins";
            this.nmdScheduleMins.Size = new System.Drawing.Size(59, 20);
            this.nmdScheduleMins.TabIndex = 7;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(8, 290);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(102, 13);
            this.label35.TabIndex = 85;
            this.label35.Text = "Scheduler Interval :";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(99, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 16);
            this.label10.TabIndex = 31;
            this.label10.Text = "*";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(99, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 28;
            this.label11.Text = "*";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(99, 158);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 16);
            this.label13.TabIndex = 34;
            this.label13.Text = "*";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(99, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 16);
            this.label15.TabIndex = 25;
            this.label15.Text = "*";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(99, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(16, 16);
            this.label17.TabIndex = 23;
            this.label17.Text = "*";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(116, 18);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(117, 13);
            this.label20.TabIndex = 36;
            this.label20.Text = "Database Settings :";
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.Location = new System.Drawing.Point(116, 48);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(227, 21);
            this.txtServer.TabIndex = 0;
            // 
            // ddlDatabase
            // 
            this.ddlDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDatabase.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ddlDatabase.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDatabase.FormattingEnabled = true;
            this.ddlDatabase.Items.AddRange(new object[] {
            "SQL Server Authentication",
            "Windows Authentication"});
            this.ddlDatabase.Location = new System.Drawing.Point(116, 156);
            this.ddlDatabase.Name = "ddlDatabase";
            this.ddlDatabase.Size = new System.Drawing.Size(227, 21);
            this.ddlDatabase.Sorted = true;
            this.ddlDatabase.TabIndex = 4;
            this.ddlDatabase.Enter += new System.EventHandler(this.ddlDatabase_Enter);
            // 
            // ddlAuthentication
            // 
            this.ddlAuthentication.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlAuthentication.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlAuthentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAuthentication.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ddlAuthentication.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlAuthentication.FormattingEnabled = true;
            this.ddlAuthentication.Items.AddRange(new object[] {
            "SQL Server Authentication",
            "Windows Authentication"});
            this.ddlAuthentication.Location = new System.Drawing.Point(116, 75);
            this.ddlAuthentication.Name = "ddlAuthentication";
            this.ddlAuthentication.Size = new System.Drawing.Size(227, 21);
            this.ddlAuthentication.Sorted = true;
            this.ddlAuthentication.TabIndex = 1;
            this.ddlAuthentication.SelectedIndexChanged += new System.EventHandler(this.ddlAuthentication_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(20, 79);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Authentication :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(55, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "User Id :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(28, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(76, 13);
            this.label16.TabIndex = 24;
            this.label16.Text = "Server Name :";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(116, 129);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(227, 21);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.LostFocus += new System.EventHandler(this.txtPassword_LostFocus);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(44, 133);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 32;
            this.label18.Text = "Password :";
            // 
            // txtUserId
            // 
            this.txtUserId.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserId.Location = new System.Drawing.Point(116, 102);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(227, 21);
            this.txtUserId.TabIndex = 2;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(44, 160);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 13);
            this.label19.TabIndex = 35;
            this.label19.Text = "Database :";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestConnection.Location = new System.Drawing.Point(116, 183);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(131, 33);
            this.btnTestConnection.TabIndex = 5;
            this.btnTestConnection.Text = "&Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // pnlSave
            // 
            this.pnlSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlSave.Controls.Add(this.btnClose);
            this.pnlSave.Controls.Add(this.btnSave);
            this.pnlSave.Location = new System.Drawing.Point(7, 356);
            this.pnlSave.Name = "pnlSave";
            this.pnlSave.Size = new System.Drawing.Size(527, 48);
            this.pnlSave.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(445, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 31);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(367, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 31);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ssBar
            // 
            this.ssBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessageBar});
            this.ssBar.Location = new System.Drawing.Point(0, 410);
            this.ssBar.Name = "ssBar";
            this.ssBar.Size = new System.Drawing.Size(540, 22);
            this.ssBar.TabIndex = 2;
            this.ssBar.Text = "statusStrip1";
            // 
            // lblMessageBar
            // 
            this.lblMessageBar.Name = "lblMessageBar";
            this.lblMessageBar.Size = new System.Drawing.Size(0, 17);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "Scheduler Name :";
            // 
            // txtSMSSchedulerName
            // 
            this.txtSMSSchedulerName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMSSchedulerName.Location = new System.Drawing.Point(116, 259);
            this.txtSMSSchedulerName.Name = "txtSMSSchedulerName";
            this.txtSMSSchedulerName.Size = new System.Drawing.Size(227, 21);
            this.txtSMSSchedulerName.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(116, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 90;
            this.label2.Text = "Scheduler Settings :";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 432);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ssBar);
            this.Controls.Add(this.pnlSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmdScheduleMins)).EndInit();
            this.pnlSave.ResumeLayout(false);
            this.ssBar.ResumeLayout(false);
            this.ssBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Panel pnlSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.ComboBox ddlDatabase;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox ddlAuthentication;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.StatusStrip ssBar;
        private System.Windows.Forms.ToolStripStatusLabel lblMessageBar;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown nmdScheduleMins;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSMSSchedulerName;
    }
}