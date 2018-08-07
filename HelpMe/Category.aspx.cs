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
    public partial class Category : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Category Info - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Category"), false);
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

        #region Click Events
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int categoryID = 0;
                if (Request.QueryString["p2"] == null)
                    categoryID = 0;
                else
                    categoryID = int.Parse(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString());

                HelpMe.Entities.Category objCategory = new HelpMe.Entities.Category();

                objCategory.CategoryId = categoryID;
                objCategory.CategoryName = txtCategoryName.Text.Trim().Replace("'", "''").Trim();
                objCategory.CategoryPoints = Convert.ToInt32(txtCategoryPoints.Text.Trim().Replace("'", "''").Trim());
                objCategory.Icon1 = lblPhoto1.Text;
                objCategory.Icon2 = lblPhoto2.Text;
                objCategory.ColorCode = "#" + txtColorCode.Text.Trim().ToString();
               
                
                objCategory.IsActive = chkIsActive.Checked == true ? 1 : 0;
                objCategory.IsFree = chkIsFree.Checked == true ? 1 : 0;

                int ReturnValue;
                BeanHelper.CategoryBean.objCategory = objCategory;
                if (categoryID == 0)
                    ReturnValue = BeanHelper.CategoryBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.I);
                else
                    ReturnValue = BeanHelper.CategoryBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.U);

                if (ReturnValue > 0)
                {
                    if (categoryID == 0)
                        lblMsg.Text = "Category Saved Successfully.";
                    else
                        lblMsg.Text = "Category Updated Successfully.";
                }

                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Category"), false);
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
                Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Category"), false);
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUploadImage1_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUpload1.PostedFile != null)
                {
                    if (!Directory.Exists(Server.MapPath("~/images/")))
                        Directory.CreateDirectory(Server.MapPath("~/images/"));

                    string FileName = Path.GetFileName(fileUpload1.PostedFile.FileName);
                    fileUpload1.SaveAs(Server.MapPath("~/images/" + FileName));
                    imgPicture1.ImageUrl = "~/images/" + FileName;
                    aImage1.HRef = "~/images/" + FileName;
                    lblPhoto1.Text = fileUpload1.FileName.ToString();

                    btnUploadImage1.Visible = false;
                    fileUpload1.Visible = false;
                    btnClearImage1.Visible = true;
                    aImage1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnClearImage1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblPhoto1.Text))
                {
                    if (!Directory.Exists(Server.MapPath("~/images/Category/")))
                        Directory.CreateDirectory(Server.MapPath("~/images/Category/"));

                    string fileName = Path.GetFileName(lblPhoto1.Text);
                    File.Delete(Server.MapPath("~/images/Category/") + fileName);

                    imgPicture1.ImageUrl = string.Empty;
                    lblPhoto1.Text = string.Empty;
                    btnUploadImage1.Visible = true;
                    fileUpload1.Visible = true;
                    btnClearImage1.Visible = false;
                    aImage1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUploadImage2_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUpload2.PostedFile != null)
                {

                    if (!Directory.Exists(Server.MapPath("~/images/")))
                        Directory.CreateDirectory(Server.MapPath("~/images/"));

                    string FileName = Path.GetFileName(fileUpload2.PostedFile.FileName);
                    fileUpload2.SaveAs(Server.MapPath("~/images/" + FileName));
                    imgPicture2.ImageUrl = "~/images/" + FileName;
                    aImage2.HRef = "~/images/" + FileName;
                    lblPhoto2.Text = fileUpload2.FileName.ToString();

                    btnUploadImage2.Visible = false;
                    fileUpload2.Visible = false;
                    btnClearImage2.Visible = true;
                    aImage2.Visible = true;
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnClearImage2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblPhoto2.Text))
                {
                    if (!Directory.Exists(Server.MapPath("~/images/Category/")))
                        Directory.CreateDirectory(Server.MapPath("~/images/Category/"));

                    string fileName = Path.GetFileName(lblPhoto2.Text);
                    File.Delete(Server.MapPath("~/images/Category/") + fileName);

                    imgPicture2.ImageUrl = string.Empty;
                    lblPhoto2.Text = string.Empty;
                    btnUploadImage2.Visible = true;
                    fileUpload2.Visible = true;
                    btnClearImage2.Visible = false;
                    aImage2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                dvMsg.Visible = true;
                lblMsg.Text = ex.Message;
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
                    DataTable dt = BeanHelper.CategoryBean.GetDataCategory(ConversionHelper.ConvertToInt32(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString()));
                    if (dt.Rows.Count > 0)
                    {
                        string FileName1 = string.Empty;
                        string Path = string.Empty;
                        string FileName2 = string.Empty;

                        txtCategoryName.Text = Convert.ToString(dt.Rows[0]["CategoryName"]);
                        txtCategoryPoints.Text = Convert.ToString(dt.Rows[0]["CategoryPoints"]);
                        txtColorCode.Text =  Convert.ToString(dt.Rows[0]["ColorCode"]).Substring(1,6);

                        FileName1 = Convert.ToString(dt.Rows[0]["Icon1"]);
                        Path = Server.MapPath("~/images/" + FileName1);
                        if (File.Exists(Path))
                            imgPicture1.ImageUrl = "~/images/" + FileName1;
                        else
                            imgPicture1.ImageUrl = "../Content/images/icons/Noimge.jpg";

                        aImage1.HRef = "~/images/" + FileName1;
                        lblPhoto1.Text = FileName1;

                        if (string.IsNullOrEmpty(lblPhoto1.Text))
                        {
                            btnUploadImage1.Visible = true;
                            btnClearImage1.Visible = false;
                            aImage1.Visible = false;
                        }
                        else
                        {
                            btnUploadImage1.Visible = false;
                            btnClearImage1.Visible = true;
                            aImage1.Visible = true;
                        }

                        // For Icon 2

                         FileName2 = Convert.ToString(dt.Rows[0]["Icon2"]);
                         lblPhoto2.Text = FileName2;

                        if (string.IsNullOrEmpty(lblPhoto2.Text))
                        {
                            btnUploadImage2.Visible = true;
                            btnClearImage2.Visible = false;
                            aImage2.Visible = false;
                        }
                        else
                        {
                            btnUploadImage2.Visible = false;
                            btnClearImage2.Visible = true;
                            aImage2.Visible = true;
                        }

                        Path = Server.MapPath("~/images/" + FileName2);
                        if (File.Exists(Path))
                            imgPicture2.ImageUrl = "~/images/" + FileName2;
                        else
                        {
                            imgPicture2.ImageUrl = "../Content/images/icons/Noimge.jpg";
                          
                        }
                        aImage2.HRef = "~/images/" + FileName2;

                        if (Convert.ToInt32(dt.Rows[0]["IsActive"]) == 1)
                            chkIsActive.Checked = true;
                        else
                            chkIsActive.Checked = false;

                        if (Convert.ToInt32(dt.Rows[0]["IsFree"]) == 1)
                            chkIsFree.Checked = true;
                        else
                            chkIsFree.Checked = false;

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
    }
}