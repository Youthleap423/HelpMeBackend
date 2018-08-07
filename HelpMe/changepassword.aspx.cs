using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data;
using HelpMe.BusinessAccess;
using HelpMe.Entities;
using System.Configuration;

namespace HelpMe
{
    public partial class changepassword : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Change Password - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                if (!IsPostBack)
                {
                    txtUserName.Text = ((HelpMe.Entities.Login)Session["UserData"]).LoginName;
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
                string sQry = "Update tblLogin Set LoginPassword = '" + GlobalFunctions.Encrypt(txtUserPassword.Text.Trim().Replace("'", "''")) + "' Where LoginId = " + ((HelpMe.Entities.Login)Session["UserData"]).LoginId;
                BeanHelper.DBHelper.ExecuteNonQuery(sQry);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "edit", "alert('Password changed Successfully.');", true);
                Response.Redirect("Dashboard.aspx");
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
    }
}