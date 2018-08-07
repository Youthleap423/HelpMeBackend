using System;
using System.Collections;
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
    public partial class JobPostInfo : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Job Post Info - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                try
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("~/Search.aspx?p1=2&p2=" + HtmlSerializer.ObjectToHTML("Jobpost"), false);
                }

                if (!IsPostBack)
                {
                    if (Request.QueryString["p2"] != null)
                    {
                        TitleCaption.Text = "Job Post Information";
                        ArrayList coldata = new ArrayList();
                        ArrayList coltype = new ArrayList();

                        #region Job Post Offer

                        ViewState["SearchCondition_JobPostOffer"] = "vwJobPostOfferGet Where JobPostId = " + ConversionHelper.ConvertToString(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]));
                        ViewState["OrderBy_JobPostOffer"] = " Order By [JobPostOfferId] ASC";
                        ViewState["SelectedField_JobPostOffer"] = "JobPostOfferId,JobPostId,ClientId, FirstName, LastName, EmailId, JobTitle, JobDescription, OfferAmount";
                        DataTable dtJobPostOffer = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_JobPostOffer"].ToString() + ViewState["OrderBy_JobPostOffer"].ToString(), ViewState["SelectedField_JobPostOffer"].ToString());

                        coldata = new ArrayList();
                        coltype = new ArrayList();
                        for (int i = 0; i < dtJobPostOffer.Columns.Count; i++)
                        {
                            coldata.Add(dtJobPostOffer.Columns[i].ColumnName);
                            coltype.Add(dtJobPostOffer.Columns[i].DataType.Name);
                        }
                        ViewState["Columns_EduInfo"] = coldata;
                        ViewState["ColumnsType_EduInfo"] = coltype;

                        GlobalFunctions.SetPaging(ref grdJobPostOffer);
                        grdJobPostOffer.DataSource = dtJobPostOffer;
                        grdJobPostOffer.DataBind();
                        lblRecord_JobPostOffer.Text = "Job Post Offer Info. : " + (dtJobPostOffer.Rows.Count.ToString()) + " Record(s)";
                        ViewState["PDFColWidth_EduInfo"] = new float[] { 17, 17, 17, 17, 16, 16 };
                        #endregion

                        #region Job Post View

                        ViewState["SearchCondition_JobPostView"] = "vwJobPostViewGet Where JobPostId = " + ConversionHelper.ConvertToString(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]));
                        ViewState["OrderBy_JobPostView"] = " Order By [JobPostViewId] ASC";
                        ViewState["SelectedField_JobPostView"] = "JobPostViewId,JobPostId,ClientId, FirstName, LastName, CreatedOn";
                        DataTable dtJobPostView = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_JobPostView"].ToString() + ViewState["OrderBy_JobPostView"].ToString(), ViewState["SelectedField_JobPostView"].ToString());

                        coldata = new ArrayList();
                        coltype = new ArrayList();
                        for (int i = 0; i < dtJobPostView.Columns.Count; i++)
                        {
                            coldata.Add(dtJobPostView.Columns[i].ColumnName);
                            coltype.Add(dtJobPostView.Columns[i].DataType.Name);
                        }
                        ViewState["Columns_EduInfo"] = coldata;
                        ViewState["ColumnsType_EduInfo"] = coltype;

                        GlobalFunctions.SetPaging(ref grdJobPostView);
                        grdJobPostView.DataSource = dtJobPostView;
                        grdJobPostView.DataBind();
                        lblRecord_JobPostView.Text = "Job Post View Info. : " + (dtJobPostView.Rows.Count.ToString()) + " Record(s)";
                        ViewState["PDFColWidth_EduInfo"] = new float[] { 17, 17, 17, 17, 16, 16 };
                       
                        #endregion
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

        protected void btnExportExcel_JobPostOffer_Click(object sender, EventArgs e)
        {

        }

        protected void btnExportPDF_JobPostOffer_Click(object sender, EventArgs e)
        {

        }

        protected void grdJobPostOffer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdJobPostOffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnExportExcel_JobPostView_Click(object sender, EventArgs e)
        {

        }

        protected void btnExportPDF_JobPostView_Click(object sender, EventArgs e)
        {

        }

        protected void grdJobPostView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdJobPostView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        #endregion
    }
}