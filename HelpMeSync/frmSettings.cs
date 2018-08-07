using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32.TaskScheduler;
using HelpMe.BusinessAccess;

namespace HelpMeSync
{
    public partial class frmSettings : Form
    {
        #region Variables
        HelpMeDatabaseConfiguration.Configurations.Configuration cng = new HelpMeDatabaseConfiguration.Configurations.Configuration();
        #endregion

        #region Page Events
        void frmSettings_Load(object sender, System.EventArgs e)
        {
            #region Database Settings
            string FilePath = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "DBConfig.HelpMe";

            HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity dbSecurity = null;
            if (File.Exists(FilePath))
            {
                dbSecurity = new HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity();
                dbSecurity.DecryptFiletoString();

                txtServer.Text = dbSecurity.ServerName;
                ddlAuthentication.Text = dbSecurity.Authentication + " " + "Authentication";
                txtUserId.Text = dbSecurity.UserName;
                txtPassword.Text = dbSecurity.Password;
                ddlDatabase_Enter(null, null);
                ddlDatabase.Text = dbSecurity.DatabaseName;
            }
            #endregion
        }
        public frmSettings()
        {
            InitializeComponent();

            #region Database_Settings
            ddlAuthentication.SelectedIndex = 0;
            #endregion
        }
        #endregion

        #region Database_Control_Events
        private void ddlDatabase_Enter(object sender, EventArgs e)
        {
            try
            {
                ddlDatabase.DataSource = new List<HelpMeDatabaseConfiguration.Entities.Database>();
                if (ddlAuthentication.Text == "Windows Authentication")
                {
                    HelpMeDatabaseConfiguration.Common.Authentication = HelpMeDatabaseConfiguration.DatabaseAuthentication.WindowsAuthentication;
                    HelpMeDatabaseConfiguration.Common.ServerName = txtServer.Text;
                    List<HelpMeDatabaseConfiguration.Entities.Database> lst = new List<HelpMeDatabaseConfiguration.Entities.Database>();
                    lst = cng.GetDatabases();
                    ddlDatabase.DataSource = lst;
                    ddlDatabase.DisplayMember = "DatabaseName";
                    ddlDatabase.ValueMember = "DatabaseIndex";
                }
                else
                {
                    HelpMeDatabaseConfiguration.Common.Authentication = HelpMeDatabaseConfiguration.DatabaseAuthentication.SQLServerAuthentication;
                    HelpMeDatabaseConfiguration.Common.ServerName = txtServer.Text;
                    HelpMeDatabaseConfiguration.Common.UserName = txtUserId.Text;
                    HelpMeDatabaseConfiguration.Common.Password = txtPassword.Text;
                    List<HelpMeDatabaseConfiguration.Entities.Database> lst = new List<HelpMeDatabaseConfiguration.Entities.Database>();
                    lst = cng.GetDatabases();
                    ddlDatabase.DataSource = lst;
                    ddlDatabase.DisplayMember = "DatabaseName";
                    ddlDatabase.ValueMember = "DatabaseIndex";
                }
            }
            catch (Exception ex)
            {
                ApplicationHelper.ErrorHelper.ShowError(ex, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod().ToString(), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName, ex.Message, ApplicationHelper.CommonHelper.MessageTitle);
            }
        }
        private void ddlAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAuthentication.Text == "Windows Authentication")
            {
                txtPassword.Enabled = false;
                txtUserId.Enabled = false;
            }
            else
            {
                txtPassword.Enabled = true;
                txtUserId.Enabled = true;
            }
        }
        private void txtPassword_LostFocus(object sender, System.EventArgs e)
        {
            ddlDatabase_Enter(sender, e);
        }
        #endregion

        #region Click Events
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            #region Variables
            this.btnTestConnection.Enabled = false;
            string strConnTest = string.Empty;
            bool isAuthenticated;
            #endregion

            #region Database Settings
            isAuthenticated = true;
            if (string.IsNullOrEmpty(ddlDatabase.Text))
            {
                lblMessageBar.Text = "Quick Doc Database Settings Not Found.";
            }
            else
            {
                if (ddlAuthentication.Text == "Windows Authentication")
                    strConnTest = "Server=" + txtServer.Text + ";Database=" + ddlDatabase.Text + ";integrated security=true;";
                else
                    strConnTest = "Server=" + txtServer.Text + ";Database=" + ddlDatabase.Text + ";UID=" + txtUserId.Text.Trim() + ";Password=" + txtPassword.Text + ";";

                try
                {
                    SqlConnection cnTemp = new SqlConnection(strConnTest);
                    cnTemp.Open();
                    cnTemp.Close();
                }
                catch
                {
                    isAuthenticated = false;
                }

                if (isAuthenticated)
                    lblMessageBar.Text = "Quick Doc Database Settings Authenticated Successfully.";
                else
                    lblMessageBar.Text = "Quick Doc Database Settings Authentication Failed. Please verify the database information provided.";
            }
            #endregion

            this.btnTestConnection.Enabled = true;
        }
        #endregion

        #region Save Settings
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Variables
            this.btnSave.Enabled = false;

            string strConnTest = string.Empty;
            bool isAuthenticated;
            #endregion

            #region Database Settings
            string FilePath = string.Empty;
            FilePath = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "DBConfig.HelpMe";
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            #region Local DB
            isAuthenticated = false;
            if (!string.IsNullOrEmpty(ddlDatabase.Text))
            {
                if (ddlAuthentication.Text == "Windows Authentication")
                    strConnTest = "Server=" + txtServer.Text + ";Database=" + ddlDatabase.Text + ";integrated security=true;";
                else
                    strConnTest = "Server=" + txtServer.Text + ";Database=" + ddlDatabase.Text + ";UID=" + txtUserId.Text.Trim() + ";Password=" + txtPassword.Text + ";";

                try
                {
                    SqlConnection cnTemp = new SqlConnection(strConnTest);
                    cnTemp.Open();
                    cnTemp.Close();
                    isAuthenticated = true;
                }
                catch { }
            }
            if (!isAuthenticated)
            {
                lblMessageBar.Text = "Quick Doc Database Settings are not Authenticated. Please verify the database information provided.";
                return;
            }
            #endregion

            string strConn_local = string.Empty;
            if (ddlAuthentication.Text == "Windows Authentication")
                strConn_local = "Authentication:windows#Server:" + txtServer.Text + "#Database:" + ddlDatabase.Text;
            else
                strConn_local = "Authentication:server#Server:" + txtServer.Text + "#Database:" + ddlDatabase.Text + "#UserID:" + txtUserId.Text + "#Password:" + txtPassword.Text;

            string strConn = strConn_local + "~Provider:System.Data.SqlClient";

            HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity dbs = new HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity();
            FilePath = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "DBConfig.HelpMe";
            dbs.EncryptStringtoFile(strConn, FilePath);
            lblMessageBar.Text = "Quick Doc Database Settings Saved Successfully.";

            SetScheduler();

            this.btnSave.Enabled = true;
            #endregion
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SetScheduler()
        {
            try
            {
                string sSMSSchedulerName = txtSMSSchedulerName.Text.Trim();

                string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                TaskService ts1 = new TaskService();
                Task t = ts1.FindTask(sSMSSchedulerName, false);

                if (t != null)
                {
                    ts1.RootFolder.DeleteTask(sSMSSchedulerName);
                }

                using (TaskService ts = new TaskService())
                {
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "Does something";
                    td.RegistrationInfo.Author = user;

                    // Create a trigger that will fire the task at this time every other day
                    DailyTrigger dt = new DailyTrigger(2);
                    dt.Repetition.Interval = new TimeSpan(0, Convert.ToInt32(nmdScheduleMins.Value), 0);
                    dt.Repetition.StopAtDurationEnd = false;
                    dt.StartBoundary = System.DateTime.Now;
                    dt.DaysInterval = 1;
                    td.Triggers.Add(dt);

                    // Create an action that will launch Notepad whenever the trigger fires
                    td.Actions.Add(new ExecAction("\"" + Application.StartupPath + "\\HelpMeSync.exe\"", "HelpMeSync", null));

                    //GenericSecurityDescriptor sd = SecurityDescriptor.;
                    ts.RootFolder.RegisterTaskDefinition(sSMSSchedulerName, td);
                }
                MessageBox.Show("Scheduled Task created successfully.", ApplicationHelper.CommonHelper.MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ApplicationHelper.CommonHelper.MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}