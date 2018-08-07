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
    public partial class Product : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Product Info - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Product"), false);
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

        #region Function
        protected void DisplayData()
        {
            try
            {
                if (Request.QueryString["p2"] != null)
                {
                    DataTable dt = BeanHelper.ProductBean.GetData(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        string FileName1 = string.Empty;
                        string Path = string.Empty;

                        txtProductName.Text = Convert.ToString(dt.Rows[0]["ProductName"]);
                        txtDescription.Text = Convert.ToString(dt.Rows[0]["Description"]);
                        txtPoint.Text = Convert.ToString(dt.Rows[0]["Point"]);

                        FileName1 = Convert.ToString(dt.Rows[0]["ProductImage"]);
                        Path = Server.MapPath("~/images/Product/" + FileName1);
                        if (File.Exists(Path))
                            imgProduct.ImageUrl = "~/images/Product/" + FileName1;
                        else
                            imgProduct.ImageUrl = "../Content/images/icons/Noimge.jpg";

                        aImage1.HRef = "~/images/Product/" + FileName1;
                        lblProductimage.Text = FileName1;

                        if (string.IsNullOrEmpty(lblProductimage.Text))
                        {
                            divshowproduct.Style.Add("Display", "none");
                            btnProductimage.Visible = true;
                            btnClearImage.Visible = false;
                        }
                        else
                        {
                            divshowproduct.Style.Add("Display", "block");
                            btnProductimage.Visible = false;
                            btnClearImage.Visible = true;
                            fuproductimage.Visible = false;
                        }
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
                int iProductId = 0;
                if (Request.QueryString["p2"] == null)
                    iProductId = 0;
                else
                    iProductId = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                HelpMe.Entities.Product objProduct = new HelpMe.Entities.Product();

                objProduct.ProductId = iProductId;
                objProduct.ProductName = txtProductName.Text.Trim().Replace("'", "''").Trim();
                objProduct.Description = Convert.ToString(txtDescription.Text);
                objProduct.ProductImage = Convert.ToString(lblProductimage.Text);
                objProduct.Point = txtPoint.Text == "" ? Convert.ToInt32("0") : Convert.ToInt32(txtPoint.Text);

                int ReturnValue;
                BeanHelper.ProductBean.ObjProduct = objProduct;
                if (iProductId == 0)
                    ReturnValue = BeanHelper.ProductBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.I);
                else
                    ReturnValue = BeanHelper.ProductBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.U);

                if (ReturnValue > 0)
                {
                    if (iProductId == 0)
                        lblErrorMsg.Text = "Product Saved Successfully.";
                    else
                        lblErrorMsg.Text = "Product Updated Successfully.";
                }

                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Product"), false);
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
                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Product"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }
        #endregion


        protected void btnProductimage_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuproductimage.PostedFile != null)
                {
                    if (!Directory.Exists(Server.MapPath("~/images/Product/")))
                        Directory.CreateDirectory(Server.MapPath("~/images/Product/"));

                    string FileName = Path.GetFileName(fuproductimage.PostedFile.FileName);
                    fuproductimage.SaveAs(Server.MapPath("~/images/Product/" + FileName));
                    imgProduct.ImageUrl = "~/images/Product/" + FileName;
                    aImage1.HRef = "~/images/Product/" + FileName;
                    lblProductimage.Text = fuproductimage.FileName.ToString();

                    btnProductimage.Visible = false;
                    fuproductimage.Visible = false;
                    btnClearImage.Visible = true;
                    divshowproduct.Style.Add("Display", "block");
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void btnClearImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblProductimage.Text))
                {

                    string fileName = Path.GetFileName(lblProductimage.Text);
                    File.Delete(Server.MapPath("~/images/Product/") + fileName);

                    btnProductimage.Visible = true;
                    fuproductimage.Visible = true;
                    btnClearImage.Visible = false;
                    lblProductimage.Text = "";
                    divshowproduct.Style.Add("Display", "none");
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblErrorMsg.Text = ex.Message;
            }
        }


    }
}