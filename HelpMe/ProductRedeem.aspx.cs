using HelpMe.BusinessAccess;
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
using HelpMe.Shared.Utilities;
using HelpMe.Helpers;
using System.IO;

namespace HelpMe
{
    public partial class ProductRedeem : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Product Redeem Info - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Product Redeem"), false);
                }

                if (!IsPostBack)
                {
                    FillProduct();
                    FillClient();
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

        #region Function
        protected void DisplayData()
        {
            try
            {
                if (Request.QueryString["p2"] != null)
                {
                    DataTable dt = BeanHelper.ProductRedeemBean.GetData(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {

                        ddlClient.SelectedValue = Convert.ToString(dt.Rows[0]["ClientId"]);
                        ddlProduct.SelectedValue = Convert.ToString(dt.Rows[0]["ProductId"]);
                        txtRedeemPoint.Text = Convert.ToString(dt.Rows[0]["RedeemPoint"]);
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

        #region Product
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int ProductRedeemId = 0;
                if (Request.QueryString["p2"] == null)
                    ProductRedeemId = 0;
                else
                    ProductRedeemId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                HelpMe.Entities.ProductRedeem objobjProduct = new HelpMe.Entities.ProductRedeem();

                objobjProduct.ProductId = Convert.ToInt64(ddlProduct.SelectedValue);
                objobjProduct.ClientId = Convert.ToInt64(ddlClient.SelectedValue);
                objobjProduct.RedeemPoint = Convert.ToInt32(txtRedeemPoint.Text);

                int ReturnValue;
                BeanHelper.ProductRedeemBean.ObjProductRedeem = objobjProduct;
                if (ProductRedeemId == 0)
                    ReturnValue = BeanHelper.ProductRedeemBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.I);
                else
                    ReturnValue = BeanHelper.ProductRedeemBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.U);

                if (ReturnValue > 0)
                {
                    if (ProductRedeemId == 0)
                        lblErrorMsg.Text = "Product Redeem Saved Successfully.";
                    else
                        lblErrorMsg.Text = "Product Redeem Updated Successfully.";
                }

                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Product Redeem"), false);
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
                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Product Redeem"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion


        protected void FillClient()
        {
            try
            {
                DataTable dt = BeanHelper.DBHelper.FillTable(@"Select ClientId, FirstName +' '+ LastName as ClientName  from tblClient where   EndDate is null order by FirstName asc");
                ddlClient.DataTextField = "ClientName";
                ddlClient.DataValueField = "ClientId";
                ddlClient.Items.Insert(0, "-- Select Client --");
                if (dt.Rows.Count > 0)
                    ddlClient.DataSource = dt;
                ddlClient.DataBind();
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void FillProduct()
        {
            try
            {
                ddlProduct.DataSource = BeanHelper.DBHelper.FillTable(@"Select *  from tblProduct where  EndDate is null  order by ProductName");
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.Items.Insert(0, "-- Select Product --");
                ddlProduct.DataBind();
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }

    }
}