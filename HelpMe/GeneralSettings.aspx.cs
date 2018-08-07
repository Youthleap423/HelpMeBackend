using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using HelpMe.Shared.Utilities;
using HelpMe.Helpers;
using HelpMe.BusinessAccess;
using System.Data;
using System.IO;

namespace HelpMe
{
    public partial class GeneralSettings : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - General Settings - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("General Settings"), false);
                }

                if (!IsPostBack)
                {
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
                int GeneralSettingId = 0;
                if (Request.QueryString["p2"] == null)
                    GeneralSettingId = 0;
                else
                    GeneralSettingId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                HelpMe.Entities.GeneralSettings objGeneralSettings = new HelpMe.Entities.GeneralSettings();

                objGeneralSettings.GeneralSettingId = GeneralSettingId;
                objGeneralSettings.CreditPoint = Convert.ToString(txtCreditPost.Text);
                objGeneralSettings.ShareApp = Convert.ToString(txtshareapp.Text);
                objGeneralSettings.SharePost = Convert.ToString(txtsharepost.Text);
                objGeneralSettings.AppMode = Convert.ToString(cblAppMode.SelectedValue);

                int ReturnValue;
                BeanHelper.GeneralSettingsBean.objGeneralSettings = objGeneralSettings;
                if (GeneralSettingId == 0)
                    ReturnValue = BeanHelper.GeneralSettingsBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.I);
                else
                    ReturnValue = BeanHelper.GeneralSettingsBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.U);

                if (ReturnValue > 0)
                {
                    if (GeneralSettingId == 0)
                        lblErrorMsg.Text = "General Settings Saved Successfully.";
                    else
                        lblErrorMsg.Text = "General Settings Updated Successfully.";
                }

                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("General Settings"), false);
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
                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("General Settings"), false);
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
                    DataTable dt = BeanHelper.GeneralSettingsBean.GetDataGeneralSettings(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        txtCreditPost.Text = Convert.ToString(dt.Rows[0]["CreditPoint"]);
                        txtshareapp.Text = Convert.ToString(dt.Rows[0]["ShareApp"]);
                        txtsharepost.Text = Convert.ToString(dt.Rows[0]["SharePost"]);
                        cblAppMode.SelectedValue = Convert.ToString(dt.Rows[0]["AppMode"]);
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