using HelpMe.BusinessAccess;
using HelpMe.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpMe.Helpers;
using System.Security.Principal;

namespace HelpMe
{
    public partial class AdminMasterPage : System.Web.UI.MasterPage
    {
        #region Page_Events
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.ValidateSession())
                {
                    Response.Redirect("index.aspx", false);
                    return;
                }

                if (!IsPostBack)
                {
                    lblUser.Text = ((HelpMe.Entities.Login)Session["UserData"]).LoginName;

                    string sIsActive = string.Empty;
                    string section = string.Empty;
                    if (Request.QueryString["p1"] != null)
                        section = Request.QueryString["p1"].ToString();

                    DataTable dt = BeanHelper.UserRightsBean.GetData("UR.IsView = 1 And UR.UserRoleId = " + ((HelpMe.Entities.Login)Session["UserData"]).UserRoleId);
                    string strMenu = string.Empty;
                    DataView dv = new DataView(dt);
                    DataView dvSub = new DataView(dt);
                    dv.RowFilter = "UnderFormId = 0";
                    for (int i = 0; i < dv.ToTable().Rows.Count; i++)
                    {
                        sIsActive = string.Empty;
                        if (section == Convert.ToString(dv.ToTable().Rows[i]["IsPage"]))
                        {
                            sIsActive = " class='active'";
                        }

                        strMenu = strMenu + @"<li" + sIsActive + @">
    <a href='#'>
        <i class='fa fa-laptop fa-fw'><div class='icon-bg bg-pink'></div></i>
        <span class='menu-title'>" + Convert.ToString(dv.ToTable().Rows[i]["FormName"]) + @"</span><span class='fa arrow'></span>
    </a>";

                        dvSub.RowFilter = "UnderFormId = " + Convert.ToString(dv.ToTable().Rows[i]["FormId"]);
                        for (int j = 0; j < dvSub.ToTable().Rows.Count; j++)
                        {
                            strMenu = strMenu + @"<ul class='nav nav-second-level'>
    <li>
        <a href='" + Convert.ToString(dvSub.ToTable().Rows[j]["WebURL"]) + "'><i class='fa fa-rocket'></i><span class='submenu-title'>" + Convert.ToString(dvSub.ToTable().Rows[j]["FormName"]) + @"</span></a>
    </li>
</ul>";
                        }
                        strMenu = strMenu + "</li>";
                    }

                    ltmenu.Text = strMenu;
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
            }
        }
        #endregion

        #region Click_Events
        protected void lnk_logout_Click(object sender, EventArgs e)
        {
            try
            {
                //                string sMachineName = Environment.MachineName;
                //                string sDomainName = WindowsIdentity.GetCurrent().Name;
                //                string sPCUserName = System.Environment.UserName.ToString();
                //                Int64 iActiveLoginId = ((HelpMe.Entities.Login)Session["UserData"]).LoginId;

                //                string sLoggedOnQry = @"Update tblLoginDetails Set LoggedOut = GetDate() 
                //                                        Where LoginDetailsId = (Select Max(LoginDetailsId) From tblLoginDetails Where LoginId = " + (iActiveLoginId.ToString()) + " And LoggedOut Is Null)";
                //                BeanHelper.DBHelper.ExecuteNonQuery(sLoggedOnQry);

                Session.Abandon();
                Response.Redirect("index.aspx", false);
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
            }
        }
        #endregion
    }
}