using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpMe.Helpers;
using HelpMe.BusinessAccess;
using HelpMe.Entities;
using HelpMe.Shared.Utilities;
using System.Security.Principal;

namespace HelpMe
{
    public partial class index : System.Web.UI.Page
    {
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Form.DefaultButton = this.btnLogin.UniqueID.ToString();

                if (!IsPostBack)
                {
                    if (Request.Cookies["UName"] != null)
                    {
                        UserName.Text = Request.Cookies["UName"].Value;
                        if (Request.Cookies["PWD"] != null)
                            Password.Attributes.Add("value", Request.Cookies["PWD"].Value);

                        if (Request.Cookies["UName"] != null && Request.Cookies["PWD"] != null)
                            cbRemember.Checked = true;
                    }
                    UserName.Focus();
                }
            }
            catch (Exception ex)
            {
                divmsg.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        #endregion

        #region "Click Events"
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Message.Text = string.Empty;

                if (!string.IsNullOrEmpty(UserName.Text.Trim()))//&& !string.IsNullOrEmpty(Password.Text.Trim())
                {
                    if (CheckValidUser())
                    {
                        Setcookies();
                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    {
                        divmsg.Visible = true;
                        Message.Text = "Invalid User";
                        UserName.Focus();
                    }
                }
                else
                {
                    divmsg.Visible = true;
                    Message.Text = "Login Name & Password can not be blank";
                    UserName.Focus();
                }
            }
            catch (Exception ex)
            {
                divmsg.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        #endregion

        #region "Functions"
        private bool CheckValidUser()
        {
            try
            {
                DataTable data = BeanHelper.LoginBean.GetData(" LM.LoginName = '" + UserName.Text.Trim() + "' And LM.LoginPassword = '" + GlobalFunctions.Encrypt(Password.Text) + "'");
                if (data.Rows.Count > 0)
                {
                    HelpMe.Entities.Login objUserMaster = new HelpMe.Entities.Login();
                    objUserMaster.LoginId = ConversionHelper.ConvertToInt32(data.Rows[0]["LoginId"]);
                    objUserMaster.LoginName = ConversionHelper.ConvertToString(data.Rows[0]["LoginName"]);

                    objUserMaster.UserRoleId = ConversionHelper.ConvertToInt32(data.Rows[0]["UserRoleId"]);
                    objUserMaster.UserRoleName = ConversionHelper.ConvertToString(data.Rows[0]["UserRoleName"]);

                    Session["UserData"] = objUserMaster;

                    //SetAllDirectories();

                    //                    string sMachineName = Environment.MachineName;
                    //                    string sDomainName = WindowsIdentity.GetCurrent().Name;
                    //                    string sPCUserName = System.Environment.UserName.ToString();
                    //                    int iActiveLoginId = ConversionHelper.ConvertToInt32(data.Rows[0]["LoginId"]);
                    //                    string sLoggedOnQry = @"Insert Into tblLoginDetails (MachineName, DomainName, UserName, LoginId) 
                    //                                            Values ('" + sMachineName + "', '" + sDomainName + "', '" + sPCUserName + "', " + iActiveLoginId + ")";
                    //                    BeanHelper.DBHelper.ExecuteNonQuery(sLoggedOnQry);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                divmsg.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
                return false;
            }
        }
        //private void SetAllDirectories()
        //{
        //    try
        //    {
        //        if (!Directory.Exists(Server.MapPath(string.Empty) + "/HelpMeDocs"))
        //            Directory.CreateDirectory(Server.MapPath(string.Empty) + "/HelpMeDocs");

        //        if (!Directory.Exists(Server.MapPath(string.Empty) + "/HelpMeFile"))
        //            Directory.CreateDirectory(Server.MapPath(string.Empty) + "/HelpMeFile");

        //        if (!Directory.Exists(Server.MapPath(string.Empty) + "/UserSignature"))
        //            Directory.CreateDirectory(Server.MapPath(string.Empty) + "/UserSignature");

        //        if (!Directory.Exists(Server.MapPath(string.Empty) + "/UserInitials"))
        //            Directory.CreateDirectory(Server.MapPath(string.Empty) + "/UserInitials");

        //        Session["HelpMeDocsPath"] = Server.MapPath(string.Empty) + "/HelpMeDocs/";
        //        Session["HelpMeFilePath"] = Server.MapPath(string.Empty) + "/HelpMeFile/";
        //        Session["UserSignaturePath"] = Server.MapPath(string.Empty) + "/UserSignature/";
        //        Session["UserInitialsPath"] = Server.MapPath(string.Empty) + "/UserInitials/";
        //    }
        //    catch (Exception ex)
        //    {
        //        divmsg.Visible = true;
        //        Message.Text = ex.Message;
        //        LogManager.Log(ex);
        //    }
        //}
        #endregion

        #region "Remember password"
        public void Setcookies()
        {
            try
            {
                if (this.cbRemember.Checked == true)
                {
                    Response.Cookies["UName"].Value = UserName.Text;
                    Response.Cookies["PWD"].Value = Password.Text;
                    Response.Cookies["UName"].Expires = DateTime.Now.AddMonths(2);
                    Response.Cookies["PWD"].Expires = DateTime.Now.AddMonths(2);
                }
                else
                {
                    Response.Cookies["UName"].Expires = DateTime.Now.AddMonths(-1);
                    Response.Cookies["PWD"].Expires = DateTime.Now.AddMonths(-1);
                }
            }
            catch (Exception ex)
            {
                divmsg.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        #endregion
    }
}