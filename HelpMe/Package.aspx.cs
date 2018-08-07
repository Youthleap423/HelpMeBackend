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
    public partial class Package : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Package Info - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Package"), false);
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
                int PackageId = 0;
                if (Request.QueryString["p2"] == null)
                    PackageId = 0;
                else
                    PackageId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                HelpMe.Entities.Package objPackage = new HelpMe.Entities.Package();

                objPackage.PackageId = PackageId;
                objPackage.PackageName = txtPackageName.Text.Trim().Replace("'", "''").Trim();
                objPackage.Description = Convert.ToString(txtDescription.Text);
                objPackage.CreditPost = Convert.ToInt32(txtCreditPost.Text);
                objPackage.CreditPoint = Convert.ToInt32(txtCreditPoint.Text);
                objPackage.Amount = Convert.ToDecimal(txtAmount.Text);

                int ReturnValue;
                BeanHelper.PackageBean.objPackage = objPackage;
                if (PackageId == 0)
                    ReturnValue = BeanHelper.PackageBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.I);
                else
                    ReturnValue = BeanHelper.PackageBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.U);

                if (ReturnValue > 0)
                {
                    if (PackageId == 0)
                        lblErrorMsg.Text = "Package Saved Successfully.";
                    else
                        lblErrorMsg.Text = "Package Updated Successfully.";
                }

                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Package"), false);
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
                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Package"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion

        #region Other Functions
        #endregion

        #region Display Data
        protected void DisplayData()
        {
            try
            {
                if (Request.QueryString["p2"] != null)
                {
                    DataTable dt = BeanHelper.PackageBean.GetDataPackage(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        txtPackageName.Text = Convert.ToString(dt.Rows[0]["PackageName"]);
                        txtDescription.Text = Convert.ToString(dt.Rows[0]["Description"]);
                        txtCreditPost.Text = Convert.ToString(dt.Rows[0]["CreditPost"]);
                        txtCreditPoint.Text = Convert.ToString(dt.Rows[0]["CreditPoint"]);
                        txtAmount.Text = Convert.ToString(dt.Rows[0]["Amount"]);                                                
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