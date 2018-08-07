using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using HelpMe.BusinessAccess;
using HelpMe.Shared.Utilities;

namespace HelpMe.Administrator
{
    public partial class Subscription : System.Web.UI.Page
    {
        #region Page_Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.ValidateSession())
                {
                    Response.Redirect("~/index.aspx", false);
                    return;
                }
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Subscription Info - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Subscription"), false);
                }

                if (!IsPostBack)
                {
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

        #region Function
        protected void DisplayData()
        {
            try
            {
                if (Request.QueryString["p2"] != null)
                {
                    DataTable dt = BeanHelper.SubscriptionBean.GetData(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        txtFirstName.Text = Convert.ToString(dt.Rows[0]["FirstName"]);
                        txtLastName.Text = Convert.ToString(dt.Rows[0]["LastName"]);
                        txtPackageName.Text = Convert.ToString(dt.Rows[0]["PackageName"]);
                        txtCreditPost.Text = Convert.ToString(dt.Rows[0]["CreditPost"]);
                        txtCreditPoint.Text = Convert.ToString(dt.Rows[0]["CreditPoint"]);
                        txtPaymentAmount.Text = Convert.ToString(dt.Rows[0]["PaymentAmount"]);
                        txtPaymentTime.Text = Convert.ToString(dt.Rows[0]["PaymentTime"]);
                        txtPaymentId.Text = Convert.ToString(dt.Rows[0]["PaymentId"]);
                        txtPaymentStatus.Text = Convert.ToString(dt.Rows[0]["PaymentStatus"]);
                        txtPaymentResponse.Text = Convert.ToString(dt.Rows[0]["PaymentResponse"]);

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

        #region Subscription

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Subscription"), false);
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