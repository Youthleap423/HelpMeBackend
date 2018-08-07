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
    public partial class AboutUs : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - About Us - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("AboutUs"), false);
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
                int iAboutUsId = 0;
                string SQry = string.Empty;

                if (Request.QueryString["p2"] == null)
                    iAboutUsId = 0;
                else
                    iAboutUsId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                if (iAboutUsId == 0)
                    SQry = @"INSERT INTO [dbo].[tblAboutUs]([Remarks])VALUES ('" + Convert.ToString(txtremarks.Text) + "')";
                else
                    SQry = @"update tblAboutUs set Remarks='" + Convert.ToString(txtremarks.Text) + "',CreatedOn=(getdate()) where AboutUsId=" + iAboutUsId + "";

                BeanHelper.DBHelper.ExecuteNonQuery(SQry);


                if (iAboutUsId == 0)
                    lblErrorMsg.Text = "About Us Saved Successfully.";
                else
                    lblErrorMsg.Text = "About Us Updated Successfully.";

                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("AboutUs"), false);
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
                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("AboutUs"), false);
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
                    Int64 iAboutUsId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());
                    DataTable dt = BeanHelper.DBHelper.FillTable(@"Select AboutUsId, Remarks, CreatedOn, EndDate from [dbo].[tblAboutUs] where AboutUsId=" + iAboutUsId + " and  EndDate is null");
                    if (dt.Rows.Count > 0)
                    {
                        txtremarks.Text = Convert.ToString(dt.Rows[0]["Remarks"]);
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