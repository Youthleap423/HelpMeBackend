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
using HelpMe.BusinessAccess;
using Microsoft.Win32.TaskScheduler;

namespace HelpMeSync
{
    public partial class frmEmailSettings : Form
    {
        #region Variables
        bool IsValidEmailSetting = false;
        #endregion

        #region Page Events
        public frmEmailSettings()
        {
            InitializeComponent();

            #region Email_Settings
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + "SignCabProfilesettings.xml"))
            {
                ds.ReadXml(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + "SignCabProfilesettings.xml");
                try
                {
                    LoadData();
                }
                catch
                {
                    GenerateDataTable();
                }
            }
            else
            {
                GenerateDataTable();
            }
            #endregion
        }
        #endregion
       
        #region Save Settings
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            
            #region Email Settings
            if (string.IsNullOrEmpty(txtLoginId.Text.Trim()) && string.IsNullOrEmpty(txtEmailPassword.Text.Trim()) &&
                string.IsNullOrEmpty(txtPortNo.Text.Trim()) && string.IsNullOrEmpty(txtSMTPServer.Text.Trim()) &&
                string.IsNullOrEmpty(txtSender.Text.Trim()))
            {
                if (MessageBox.Show("Email settings are not configured.\r\nDo you wish to continue ?", ApplicationHelper.CommonHelper.MessageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    IsValidEmailSetting = false;
                    SaveData_EmailSettings();
                }
            }
            else
            {
                TestMessage();

                if (IsValidEmailSetting)
                {
                    SaveData_EmailSettings();
                    MessageBox.Show("Test email sent successfully.\r\nThe email settings have been saved.", ApplicationHelper.CommonHelper.MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (MessageBox.Show("The email settings could not be validated.\r\nDo you wish to continue ?", ApplicationHelper.CommonHelper.MessageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SaveData_EmailSettings();
                    }
                }
            }
            #endregion

            this.btnSave.Enabled = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region EMail_Settings
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        void GenerateDataTable()
        {
            dt.Columns.Add("Id");
            dt.Columns.Add("pass");
            dt.Columns.Add("port");
            dt.Columns.Add("server");
            dt.Columns.Add("sender");
            dt.Columns.Add("receiver");
            dt.Columns.Add("ssl");
            dt.Columns.Add("async");
            dt.Columns.Add("isvalid");

            dt.AcceptChanges();

            dt.TableName = "EmailSettings";
            ds.Tables.Add(dt);
        }
        void LoadData()
        {
            HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity obj = new HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity();

            txtLoginId.Text = ds.Tables[0].Rows[0]["Id"].ToString();
            txtEmailPassword.Text = obj.DecryptString(ds.Tables[0].Rows[0]["pass"].ToString());
            txtPortNo.Text = ds.Tables[0].Rows[0]["port"].ToString();
            txtSMTPServer.Text = ds.Tables[0].Rows[0]["server"].ToString();
            txtSender.Text = ds.Tables[0].Rows[0]["sender"].ToString();
            txtReceiver.Text = ds.Tables[0].Rows[0]["receiver"].ToString();
            chkSSL.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ssl"]);
            chkAsync.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["async"]);
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isvalid"].ToString()))
            {
                IsValidEmailSetting = Convert.ToBoolean(ds.Tables[0].Rows[0]["isvalid"]);
            }
        }
        void TestMessage()
        {
            ApplicationHelper.EmailHelper.LoginId = txtLoginId.Text.Trim();
            ApplicationHelper.EmailHelper.Password = txtEmailPassword.Text.Trim();
            ApplicationHelper.EmailHelper.Port = string.IsNullOrEmpty(txtPortNo.Text.Trim()) == true ? 0 : Convert.ToInt32(txtPortNo.Text.Trim());
            ApplicationHelper.EmailHelper.SendFrom = txtSender.Text.Trim();
            ApplicationHelper.EmailHelper.Server = txtSMTPServer.Text.Trim();
            ApplicationHelper.EmailHelper.SSL = chkSSL.Checked;
            ApplicationHelper.EmailHelper.Async = chkAsync.Checked;
            ApplicationHelper.EmailHelper.SendTo = txtReceiver.Text.Trim();
            ApplicationHelper.EmailHelper.Subject = "POS Test Email...";

            string str = ApplicationHelper.EmailHelper.SendTestEMail();
            if (str.ToUpper() == "Succeed".ToUpper())
                IsValidEmailSetting = true;
            else
                IsValidEmailSetting = false;
        }
        void SaveData_EmailSettings()
        {
            ds.Tables[0].Clear();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + "SignCabProfilesettings.xml"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + "SignCabProfilesettings.xml");
            }
            HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity obj = new HelpMeDatabaseConfiguration.Configurations.DatabaseSecurity();

            DataRow dr = ds.Tables[0].NewRow();
            dr["Id"] = txtLoginId.Text;
            dr["pass"] = obj.EncryptString(txtEmailPassword.Text);
            dr["port"] = txtPortNo.Text;
            dr["server"] = txtSMTPServer.Text;
            dr["sender"] = txtSender.Text;
            dr["receiver"] = txtReceiver.Text;
            dr["ssl"] = chkSSL.Checked;
            dr["async"] = chkAsync.Checked;
            dr["isvalid"] = IsValidEmailSetting;
            ds.Tables[0].Rows.Add(dr);
            ds.Tables[0].AcceptChanges();
            ds.WriteXml(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + System.IO.Path.DirectorySeparatorChar + "SignCabProfilesettings.xml");
        }
        #endregion
    }
}