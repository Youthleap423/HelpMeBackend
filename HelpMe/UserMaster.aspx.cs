using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using HelpMe.BusinessAccess;
using HelpMe.Shared.Utilities;

namespace HelpMe
{
    public partial class UserMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.ValidateSession())
                {
                    Response.Redirect("~/index.aspx", false);
                    return;
                }
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - User Setup - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("User"), false);
                }

                if (!IsPostBack)
                {
                    FillCombo();
                    if (Request.QueryString["p2"] != null)
                    {
                        DisplayData();
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void FillCombo()
        {
            try
            {
                ddlUserRole.DataSource = BeanHelper.UserRoleBean.GetDropDown(string.Empty);
                ddlUserRole.DataTextField = "Text";
                ddlUserRole.DataValueField = "Value";
                ddlUserRole.DataBind();
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void DisplayData()
        {
            try
            {
                if (Request.QueryString["p2"] != null)
                {
                    DataTable dt = BeanHelper.LoginBean.GetDataUser(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        txtUserName.Text = Convert.ToString(dt.Rows[0]["LoginName"]);
                        txtPassword.Text = GlobalFunctions.Decrypt(Convert.ToString(dt.Rows[0]["LoginPassword"]));
                        txtPassword.Attributes.Add("value", GlobalFunctions.Decrypt(Convert.ToString(dt.Rows[0]["LoginPassword"])));
                        ddlUserRole.SelectedValue = Convert.ToString(dt.Rows[0]["UserRoleId"]);
                        ChkIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int LoginId = 0;
                if (Request.QueryString["p2"] == null)
                    LoginId = 0;
                else
                    LoginId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                if (BeanHelper.LoginBean.IsUserExist(LoginId, txtUserName.Text.Replace("'", string.Empty).Trim()))
                {
                    dvMsg.Visible = true;
                    lblMsg.Text = "User already Exists.";
                    txtUserName.Focus();
                    return;
                }

                HelpMe.Entities.Login objLogin = new HelpMe.Entities.Login();
                objLogin.LoginId = LoginId;
                objLogin.LoginName = txtUserName.Text.Trim().Replace("'", "''").Trim();
                objLogin.LoginPassword = GlobalFunctions.Encrypt(txtPassword.Text.Trim());
                objLogin.UserRoleId = Convert.ToInt32(ddlUserRole.SelectedValue);
                objLogin.IsActive = ChkIsActive.Checked == true ? 1 : 0;
                
                int ReturnValue;
                BeanHelper.LoginBean.LoginObject = objLogin;
                if (LoginId == 0)
                    ReturnValue = BeanHelper.LoginBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.I);
                else
                    ReturnValue = BeanHelper.LoginBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.U);

                if (ReturnValue > 0)
                {
                    if (LoginId == 0)
                        lblMsg.Text = "User Saved Successfully.";
                    else
                        lblMsg.Text = "User Updated Successfully.";
                }

                Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("User"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("User"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
    }
}