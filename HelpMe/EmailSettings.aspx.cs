using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using HelpMe.BusinessAccess;
using HelpMe.Shared.Utilities;

namespace HelpMe
{
    public partial class EmailSettings : System.Web.UI.Page
    {
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.ValidateSession())
                {
                    Response.Redirect("~/index.aspx", false);
                    return;
                }

                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Email Settings - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("Email Settings"), false);
                }

                if (!IsPostBack)
                {
                    FillCombo();

                    if (Request.QueryString["p2"] != null)
                        DisplayData();
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion

        #region Click Events
      
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int ESettingID = 0;
                if (Request.QueryString["p2"] == null)
                    ESettingID = 0;
                else
                    ESettingID = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                HelpMe.Entities.EmailSetting objSettings = new HelpMe.Entities.EmailSetting();
                objSettings.EmailSettingsId = ESettingID;
                if (ddlUserName.SelectedIndex == 0)
                    objSettings.LoginId = 0;
                else
                    objSettings.LoginId = Int32.Parse(ddlUserName.SelectedValue.ToString());

                objSettings.SMTPServer = txtSMTPServer.Text.Trim().Replace("'", "''").Trim();
                objSettings.SMTPUserName = txtSMTPUserName.Text.Trim().Replace("'", "''").Trim();
                objSettings.SMTPPassword = txtSMTPpwd.Text.Trim().Replace("'", "''").Trim();
                objSettings.SMTPPort = int.Parse(txtSMTPport.Text.Trim().Replace("'", "''").Trim());

                if (chkSSL.Checked == true)
                {
                    objSettings.EnableSSL = true;
                }
                else
                {
                    objSettings.EnableSSL = false;
                }

                int ReturnValue;
                BeanHelper.EmailSettingsBean.EmailSetting = objSettings;
                if (ESettingID == 0)
                    ReturnValue = BeanHelper.EmailSettingsBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.I);
                else
                    ReturnValue = BeanHelper.EmailSettingsBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.U);

                if (ReturnValue > 0)
                {
                    if (ESettingID == 0)
                        lblErrorMsg.Text = "Email Settings Saved Successfully.";
                    else
                        lblErrorMsg.Text = "Email Settings Updated Successfully.";
                }

                Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("Email Settings"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("Email Settings"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion

        #region Other Functions
        protected void FillCombo()
        {
            try
            {
                // For User
                DataTable dt = BeanHelper.DBHelper.FillTable("Select  LoginId, LoginName from tblLogin");
                ddlUserName.DataTextField = "LoginName";
                ddlUserName.DataValueField = "LoginId";
                ddlUserName.DataSource = dt;
                ddlUserName.DataBind();
                ddlUserName.Items.Insert(0, "-- Select User --");

            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion

        #region Display Data
        protected void DisplayData()
        {
            try
            {
                if (Request.QueryString["p2"] != null)
                {
                    DataTable dt = BeanHelper.EmailSettingsBean.GetEmailSettings(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        ddlUserName.SelectedValue = Convert.ToString(dt.Rows[0]["LoginId"]);

                        txtSMTPServer.Text = Convert.ToString(dt.Rows[0]["SMTPServer"]);
                        txtSMTPUserName.Text = Convert.ToString(dt.Rows[0]["SMTPUserName"]);
                        txtSMTPpwd.Text = Convert.ToString(dt.Rows[0]["SMTPPassword"]);
                        txtSMTPport.Text = Convert.ToString(dt.Rows[0]["SMTPPort"]);

                        if (Convert.ToBoolean(dt.Rows[0]["EnableSSL"]) == true)
                            chkSSL.Checked = true;
                        else
                            chkSSL.Checked = false;

                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion        
    }
}