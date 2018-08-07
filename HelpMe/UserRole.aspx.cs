using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using HelpMe.BusinessAccess;
using HelpMe.Helpers;
using HelpMe.Shared.Utilities;

namespace HelpMe
{
    public partial class UserRole : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - User Role - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("UserRole"), false);
                }

                if (!IsPostBack)
                {
                    FillUserRights();
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
        #endregion

        #region Click Events
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int UserRoleId = 0;
                if (Request.QueryString["p2"] == null)
                    UserRoleId = 0;
                else
                    UserRoleId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                if (BeanHelper.UserRoleBean.IsUserRoleExist(UserRoleId, txtUserRoleName.Text.Replace("'", string.Empty).Trim()))
                {
                    dvMsg.Visible = true;
                    lblMsg.Text = "User Role already Exists.";
                    txtUserRoleName.Focus();
                    return;
                }

                HelpMe.Entities.UserRole objUserRole = new HelpMe.Entities.UserRole();
                objUserRole.UserRoleId = UserRoleId;
                objUserRole.UserRoleName = txtUserRoleName.Text.Replace("'", string.Empty).Trim();
                objUserRole.UserRoleDesc = txtUserRoleDesc.Text.Replace("'", string.Empty).Trim();

                int ReturnValue;
                BeanHelper.UserRoleBean.UserRole = objUserRole;
                if (UserRoleId == 0)
                    ReturnValue = BeanHelper.UserRoleBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.I);
                else
                    ReturnValue = BeanHelper.UserRoleBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.U);

                if (ReturnValue > 0)
                {
                    UpdateUserAccess(ReturnValue);

                    if (UserRoleId == 0)
                        lblMsg.Text = "User Role Saved Successfully.";
                    else
                        lblMsg.Text = "User Role Updated Successfully.";
                }

                Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("UserRole"), false);
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
                Response.Redirect("~/Search.aspx?p1=1&p2=" + HtmlSerializer.ObjectToHTML("UserRole"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void UpdateUserAccess(int UserRoleId)
        {
            try
            {
                BeanHelper.DBHelper.ExecuteNonQuery("Delete From tblUserRights Where UserRoleId = " + UserRoleId.ToString());

                string stblUserRights = string.Empty;
                foreach (RepeaterItem rptItem in rpt_Forms.Items)
                {
                    stblUserRights = @"Insert Into tblUserRights (UserRoleId, MenuName, IsView, IsSave, IsUpdate, IsDelete, IsPrint)
                                    Values (" + UserRoleId.ToString() + @", '" +
                                                Convert.ToString(((HiddenField)rptItem.FindControl("hfMenuName")).Value) + @"', " +
                                                Convert.ToString(((CheckBox)rptItem.FindControl("cbView")).Checked == true ? 1 : 0) + @", " +
                                                Convert.ToString(((CheckBox)rptItem.FindControl("cbSave")).Checked == true ? 1 : 0) + @", " +
                                                Convert.ToString(((CheckBox)rptItem.FindControl("cbUpdate")).Checked == true ? 1 : 0) + @", " +
                                                Convert.ToString(((CheckBox)rptItem.FindControl("cbDelete")).Checked == true ? 1 : 0) + @", " +
                                                Convert.ToString(((CheckBox)rptItem.FindControl("cbPrint")).Checked == true ? 1 : 0) + @")";
                    BeanHelper.DBHelper.ExecuteNonQuery(stblUserRights);
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
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
                    DataTable dt = BeanHelper.UserRoleBean.GetDataRole(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        txtUserRoleName.Text = Convert.ToString(dt.Rows[0]["UserRoleName"]);
                        txtUserRoleDesc.Text = Convert.ToString(dt.Rows[0]["UserRoleDesc"]);
                        ChkIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);

                        FillUserRights();

                        string sQry = @"Select URR.UserRoleId, URR.MenuName, URR.IsView, URR.IsSave, URR.IsUpdate, URR.IsDelete, URR.IsPrint 
                                From tblUserRights URR 
                                    Inner Join tblDefaultForms DF On URR.MenuName = DF.MenuName
                                Where URR.UserRoleId = " + ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()) + @" 
                                Order By URR.MenuName ";
                        DataTable dttblUserRights = BeanHelper.DBHelper.FillTable(sQry);
                        if (dttblUserRights != null)
                        {
                            foreach (RepeaterItem rptItem in rpt_Forms.Items)
                            {
                                dttblUserRights.DefaultView.RowFilter = "MenuName = '" + Convert.ToString(((HiddenField)rptItem.FindControl("hfMenuName")).Value) + "'";
                                if (dttblUserRights.DefaultView.ToTable() != null && dttblUserRights.DefaultView.ToTable().Rows.Count > 0)
                                {
                                    ((CheckBox)rptItem.FindControl("cbView")).Checked = Convert.ToInt32(dttblUserRights.DefaultView.ToTable().Rows[0]["IsView"]) == 1 ? true : false;
                                    ((CheckBox)rptItem.FindControl("cbSave")).Checked = Convert.ToInt32(dttblUserRights.DefaultView.ToTable().Rows[0]["IsSave"]) == 1 ? true : false;
                                    ((CheckBox)rptItem.FindControl("cbUpdate")).Checked = Convert.ToInt32(dttblUserRights.DefaultView.ToTable().Rows[0]["IsUpdate"]) == 1 ? true : false;
                                    ((CheckBox)rptItem.FindControl("cbDelete")).Checked = Convert.ToInt32(dttblUserRights.DefaultView.ToTable().Rows[0]["IsDelete"]) == 1 ? true : false;
                                    ((CheckBox)rptItem.FindControl("cbPrint")).Checked = Convert.ToInt32(dttblUserRights.DefaultView.ToTable().Rows[0]["IsPrint"]) == 1 ? true : false;
                                }
                                dttblUserRights.DefaultView.RowFilter = string.Empty;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void FillUserRights()
        {
            try
            {
                DataTable dt = BeanHelper.DBHelper.FillTable("Select MenuName, FormName From tblDefaultForms Order By FormId");
                rpt_Forms.DataSource = dt;
                rpt_Forms.DataBind();
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        #endregion

        #region Control Events
        protected void chkFormAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem rptItem in rpt_Forms.Items)
                {
                    if (rptItem.FindControl("chkForm") != null)
                    {
                        ((CheckBox)rptItem.FindControl("chkForm")).Checked = ((CheckBox)sender).Checked;
                    }
                    if (rptItem.FindControl("cbView") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbView")).Checked = ((CheckBox)sender).Checked;
                    }
                    if (rptItem.FindControl("cbSave") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbSave")).Checked = ((CheckBox)sender).Checked;
                    }
                    if (rptItem.FindControl("cbUpdate") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbUpdate")).Checked = ((CheckBox)sender).Checked;
                    }
                    if (rptItem.FindControl("cbDelete") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbDelete")).Checked = ((CheckBox)sender).Checked;
                    }
                    if (rptItem.FindControl("cbPrint") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbPrint")).Checked = ((CheckBox)sender).Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void cbView_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem rptItem in rpt_Forms.Items)
                {
                    if (rptItem.FindControl("cbView") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbView")).Checked = ((CheckBox)sender).Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void cbSave_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem rptItem in rpt_Forms.Items)
                {
                    if (rptItem.FindControl("cbSave") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbSave")).Checked = ((CheckBox)sender).Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void cbUpdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem rptItem in rpt_Forms.Items)
                {
                    if (rptItem.FindControl("cbUpdate") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbUpdate")).Checked = ((CheckBox)sender).Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void cbDelete_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem rptItem in rpt_Forms.Items)
                {
                    if (rptItem.FindControl("cbDelete") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbDelete")).Checked = ((CheckBox)sender).Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void cbPrint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem rptItem in rpt_Forms.Items)
                {
                    if (rptItem.FindControl("cbPrint") != null)
                    {
                        ((CheckBox)rptItem.FindControl("cbPrint")).Checked = ((CheckBox)sender).Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void chkForm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    try
                    {
                        if (((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbView") != null)
                        {
                            ((CheckBox)((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbView")).Checked = ((CheckBox)sender).Checked;
                        }
                        if (((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbSave") != null)
                        {
                            ((CheckBox)((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbSave")).Checked = ((CheckBox)sender).Checked;
                        }
                        if (((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbUpdate") != null)
                        {
                            ((CheckBox)((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbUpdate")).Checked = ((CheckBox)sender).Checked;
                        }
                        if (((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbDelete") != null)
                        {
                            ((CheckBox)((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbDelete")).Checked = ((CheckBox)sender).Checked;
                        }
                        if (((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbPrint") != null)
                        {
                            ((CheckBox)((RepeaterItem)((CheckBox)sender).Parent).FindControl("cbPrint")).Checked = ((CheckBox)sender).Checked;
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        #endregion
    }
}