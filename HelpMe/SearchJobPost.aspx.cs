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
using HelpMe.BusinessAccess;
using Stripe;

namespace HelpMe
{
    public partial class SearchJobPost : System.Web.UI.Page
    {
        #region Variables
        static int shortway = 0;
        #endregion

        #region Page_Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string section;
                if (!GlobalFunctions.ValidateSession())
                {
                    Response.Redirect("~/index.aspx", false);
                    return;
                }
                try
                {
                    section = HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                }
                catch (Exception)
                {
                    Response.Redirect("~/Dashboard.aspx", false);
                }

                this.Form.DefaultButton = this.btnSearch.UniqueID.ToString();
                section = HtmlSerializer.HtmlToObject(Request.QueryString["p2"].ToString()).ToString();
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Job Post - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                if (!IsPostBack)
                {
                    txtSearch.Focus();

                    ddlStatus.Visible = false;
                    btnCancelsearch.Visible = false;
                    btnExportExcel.Visible = false;
                    btnExportPDF.Visible = false;
                    btnSearch.Visible = false;
                    ddlIsHire.Visible = true;

                    ViewState["SelectedView"] = "vwJobPostGet";
                    ViewState["ReturnPage"] = "~/SearchJobPost.aspx?p1=2";
                    ViewState["SearchCondition"] = ViewState["SelectedView"].ToString() + " Where IsHire = 0";
                    ViewState["OrderBy"] = " Order By [JobPostId] ASC";
                    ViewState["SelectedField"] = @"JobPostId,ClientId,JobTitle,JobDescription,JobPhoto,IsFree,CategoryId,JobPostingPoints,JobPostingAmount,Latitude,Longitude,Altitude,JobTimeFlag,JobHour,JobDoneTime,JobAmount,JobAmountFlag,PaymentTime,PaymentId,PaymentStatus,
                                                    PaymentResponse,CreatedOn,EndDate,Latitude_1,Longitude_1,Altitude_1,FirstName,LastName,CategoryName,EmailId,IsHire,TotalPayment,RefundAmount,DeductionAmount,HelperAmount,StripeDeductionAmount,IsStripePaymentDone,HelpSeeker_StripeAccountId,Helper_StripeAccountId,HelpSeekerName,HelperName";
                    TitleCaption.Text = "Job Post";
                    ViewState["PDFColWidth"] = new float[] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };

                    DataTable data = BeanHelper.SearchBean.GetData(ViewState["SearchCondition"].ToString() + ViewState["OrderBy"].ToString(), ViewState["SelectedField"].ToString());
                    ArrayList coldata = new ArrayList();
                    ArrayList coltype = new ArrayList();

                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        if (data.Columns[i].ColumnName != "IdEnc")
                        {
                            coldata.Add(data.Columns[i].ColumnName);
                            coltype.Add(data.Columns[i].DataType.Name);
                        }
                    }

                    ViewState["Columns"] = coldata;
                    ViewState["ColumnsType"] = coltype;

                    grddata.AllowPaging = true;
                    grddata.PageSize = 10;
                    grddata.DataSource = data;
                    grddata.DataBind();
                    txtSearch.Focus();

                    lblRecord.Text = (data.Rows.Count.ToString()) + " Record(s) found.";
                    if (data.Rows.Count > 0)
                    {
                        btnExportExcel.Visible = true;
                        btnExportPDF.Visible = true;
                        btnSearch.Visible = true;
                        btnCancelsearch.Visible = true;
                    }
                    else
                    {
                        btnExportExcel.Visible = false;
                        btnExportPDF.Visible = false;
                        btnSearch.Visible = false;
                        btnCancelsearch.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        #endregion

        #region Grid Events
        protected void grddata_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grddata.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void grddata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gridView = (GridView)sender;
                if (ViewState["SortCol"] != null)
                {
                    int cellIndex = -1;
                    foreach (DataControlField field in gridView.Columns)
                    {
                        if (field.SortExpression == ViewState["SortCol"].ToString())
                        {
                            cellIndex = gridView.Columns.IndexOf(field);
                            break;
                        }
                    }

                    if (cellIndex > -1)
                    {
                        if (e.Row.RowType == DataControlRowType.Header)
                            e.Row.Cells[cellIndex].CssClass += (ViewState["SortDir"].ToString() == "ASC" ? " sortascheader" : " sortdescheader");
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton button = e.Row.Cells[0].Controls[1] as ImageButton;
                    if (ConversionHelper.ConvertToString(((HiddenField)e.Row.Cells[0].Controls[3]).Value) != "2")
                        button.Visible = false;
                    else
                    {
                        if (ConversionHelper.ConvertToString(((HiddenField)e.Row.Cells[0].Controls[5]).Value) == "1")
                            button.Visible = false;
                        else
                            button.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void lnkSort_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                if (lnk != null)
                {
                    if (shortway % 2 == 0)
                        ViewState["lastsorton"] = Convert.ToString(lnk.ID) + " ASC";
                    else
                        ViewState["lastsorton"] = Convert.ToString(lnk.ID) + " DESC";

                    BindGrid();

                    #region Sorting
                    try
                    {
                        System.Web.UI.WebControls.DataControlFieldHeaderCell thSort = (System.Web.UI.WebControls.DataControlFieldHeaderCell)lnk.Parent;
                        if (thSort != null)
                        {
                            try
                            {
                                int iCount = ((System.Web.UI.WebControls.TableRow)(((GridViewRow)thSort.Parent))).Cells.Count;
                                for (int i = 0; i < iCount; i++)
                                {
                                    System.Web.UI.WebControls.DataControlFieldHeaderCell thSort_Row = (System.Web.UI.WebControls.DataControlFieldHeaderCell)((System.Web.UI.WebControls.TableRow)(((GridViewRow)thSort.Parent))).Cells[i];
                                    (((System.Web.UI.WebControls.DataControlFieldCell)(thSort_Row)).ContainingField).HeaderStyle.CssClass = string.Empty;
                                }
                            }
                            catch { }

                            if (shortway % 2 == 0)
                                (((System.Web.UI.WebControls.DataControlFieldCell)(thSort)).ContainingField).HeaderStyle.CssClass = "sorting_asc";
                            else
                                (((System.Web.UI.WebControls.DataControlFieldCell)(thSort)).ContainingField).HeaderStyle.CssClass = "sorting_desc";
                        }
                    }
                    catch { }
                    #endregion

                    shortway++;
                }
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        #endregion

        #region Other_Procedures
        protected void FindSearchAndBind()
        {
            try
            {
                StringBuilder strwhere = new StringBuilder();
                string searchtext = txtSearch.Text.Replace("'", string.Empty).Trim();
                string mainstrwhere = string.Empty;
                string section = string.Empty;
                try
                {
                    section = HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
                }
                catch (Exception)
                {
                    Response.Redirect("~/Dashboard.aspx", false);
                }

                if (!string.IsNullOrEmpty(searchtext) && (ViewState["Columns"] != null) && (ViewState["ColumnsType"] != null))
                {
                    ArrayList coldata = (ArrayList)ViewState["Columns"];
                    ArrayList coltype = (ArrayList)ViewState["ColumnsType"];

                    for (int i = 0; i < coldata.Count; i++)
                    {
                        if (((string)coltype[i]) == "DateTime")
                        {
                            if (strwhere.Length == 0)
                            {
                                strwhere.Append("Convert(Varchar(10), [" + ((string)coldata[i]) + "], 103) Like '%" + searchtext + "%'");
                                strwhere.Append(" Or Convert(Varchar(8), [" + ((string)coldata[i]) + "], 108) Like '%" + searchtext + "%'");
                            }
                            else
                            {
                                strwhere.Append(" Or Convert(Varchar(10), [" + ((string)coldata[i]) + "], 103) Like '%" + searchtext + "%'");
                                strwhere.Append(" Or Convert(Varchar(8), [" + ((string)coldata[i]) + "], 108) Like '%" + searchtext + "%'");
                            }
                        }
                        else
                        {
                            if (strwhere.Length == 0)
                                strwhere.Append("[" + ((string)coldata[i]) + "] Like '%" + searchtext + "%'");
                            else
                                strwhere.Append(" Or [" + ((string)coldata[i]) + "] Like '%" + searchtext + "%'");
                        }
                    }
                }
                string sWhere = "1 = 1";
                if (section == "UserRole" || section == "Client")
                {
                    if (ConversionHelper.ConvertToString(ddlStatus.SelectedValue) != "2")
                        sWhere = " IsActive = " + ConversionHelper.ConvertToString(ddlStatus.SelectedValue);
                }
                if (section == "Job Post")
                {
                    if (ConversionHelper.ConvertToString(ddlIsHire.SelectedValue) == "0")
                        sWhere = " IsHire = 0";
                    else if (ConversionHelper.ConvertToString(ddlIsHire.SelectedValue) == "1")
                        sWhere = " IsHire = 1";
                    else if (ConversionHelper.ConvertToString(ddlIsHire.SelectedValue) == "2")
                        sWhere = " IsHire = 2 And IsStripePaymentDone = 0";
                    else if (ConversionHelper.ConvertToString(ddlIsHire.SelectedValue) == "3")
                        sWhere = " IsHire = 2 And IsStripePaymentDone = 1";
                }

                if (!string.IsNullOrEmpty(strwhere.ToString()))
                    sWhere = sWhere + " And ( " + strwhere.ToString() + " )";

                mainstrwhere = ViewState["SelectedView"].ToString() + " Where" + sWhere;
                ViewState["SearchCondition"] = mainstrwhere.ToString();

                BindGrid();
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void BindGrid()
        {
            try
            {
                DataTable data = BeanHelper.SearchBean.GetData(ViewState["SearchCondition"].ToString() + ViewState["OrderBy"].ToString(), ViewState["SelectedField"].ToString());
                DataColumn Col = data.Columns.Add("IdEnc", System.Type.GetType("System.String"));
                Col.SetOrdinal(1);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    data.Rows[i][1] = HtmlSerializer.ObjectToHTML(data.Rows[i][0].ToString());
                }
                grddata.DataSource = data;
                grddata.DataBind();

                lblRecord.Text = (data.Rows.Count.ToString()) + " Record(s) found.";
                if (data.Rows.Count > 0)
                {
                    btnExportExcel.Visible = true;
                    btnExportPDF.Visible = true;
                }
                else
                {
                    btnExportExcel.Visible = false;
                    btnExportPDF.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        #endregion

        #region Click_Events
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FindSearchAndBind();
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FindSearchAndBind();
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void btnCancelsearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                FindSearchAndBind();
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable objDataTable = new DataTable();
                string SelectedColumns = string.Empty;
                for (int i = 0; i < grddata.Columns.Count; i++)
                {
                    if (SelectedColumns == string.Empty)
                        SelectedColumns = SelectedColumns + "[" + grddata.Columns[i].HeaderText + "]";
                    else
                        SelectedColumns = SelectedColumns + "," + "[" + grddata.Columns[i].HeaderText + "]";
                }
                SelectedColumns = SelectedColumns.Replace(",[Delete]", " ");
                SelectedColumns = SelectedColumns.Replace(",[Is Submit]", " ");
                objDataTable = BeanHelper.SearchBean.GetData(ViewState["SearchCondition"].ToString() + ViewState["OrderBy"].ToString(), SelectedColumns);
                ExcelWrite objExcelWrite = new ExcelWrite(TitleCaption.Text);
                objExcelWrite.ExportExcel(objDataTable);
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable objDataTable = new DataTable();
                string SelectedColumns = string.Empty;
                for (int i = 0; i < grddata.Columns.Count; i++)
                {
                    if (SelectedColumns == string.Empty)
                        SelectedColumns = SelectedColumns + "[" + grddata.Columns[i].HeaderText + "]";
                    else
                        SelectedColumns = SelectedColumns + "," + "[" + grddata.Columns[i].HeaderText + "]";
                }
                SelectedColumns = SelectedColumns.Replace(",[Delete]", " ");

                objDataTable = BeanHelper.SearchBean.GetData(ViewState["SearchCondition"].ToString() + ViewState["OrderBy"].ToString(), SelectedColumns);

                PDFWrite objPDFWrite = new PDFWrite();
                objPDFWrite.ExportPDF(objDataTable, (float[])ViewState["PDFColWidth"]);
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }

        protected void ddlIsHire_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FindSearchAndBind();
                ddlIsHire.Focus();
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void imgPayNow_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Messagesection.Visible = false;
                Message.Text = string.Empty;

                ImageButton img = (ImageButton)sender;
                if (img != null)
                {
                    string sJobPostId = ConversionHelper.ConvertToString(img.CommandArgument);
                    if (!string.IsNullOrEmpty(sJobPostId))
                    {
                        BeanHelper.DBHelper.ExecuteNonQuery("Update tblJobPost Set IsStripePaymentDone = 1 Where JobPostId = " + sJobPostId);
                        ddlIsHire_SelectedIndexChanged(null, null);

                        #region Stripe Payment
                        //DataTable dtJobPost = BeanHelper.DBHelper.FillTable("Select * From vwJobPostGet Where JobPostId = " + sJobPostId);
                        //if (dtJobPost != null && dtJobPost.Rows.Count > 0)
                        //{
                        //    if (Convert.ToDouble(dtJobPost.Rows[0]["HelperAmount"]) > 0 && string.IsNullOrEmpty(ConversionHelper.ConvertToString(dtJobPost.Rows[0]["Helper_StripeAccountId"])))
                        //    {
                        //        Messagesection.Visible = true;
                        //        Message.Text = "Helper stripe Account not connected, you can not perform Payment/Transfer";
                        //        return;
                        //    }

                        //    if (Convert.ToDouble(dtJobPost.Rows[0]["RefundAmount"]) > 0 && string.IsNullOrEmpty(ConversionHelper.ConvertToString(dtJobPost.Rows[0]["HelpSeeker_StripeAccountId"])))
                        //    {
                        //        Messagesection.Visible = true;
                        //        Message.Text = "Help Seeker stripe Account not connected, you can not perform Refund";
                        //        return;
                        //    }

                        //    bool blnPaymentDone = false;
                        //    bool blnRefundDone = false;

                        //    string Stripe_ApiKey = "sk_live_laT7frDDnFvoFRMNpO5efIIn";      // "sk_test_eLa53cuPkNlgl7RwI6Z9GIiM";
                        //    StripeConfiguration.SetApiKey(Stripe_ApiKey);

                        //    #region Payout
                        //    double HelperAmount = Convert.ToDouble(dtJobPost.Rows[0]["HelperAmount"]);
                        //    double TransferAmount = 0;
                        //    try
                        //    {
                        //        if (HelperAmount == 0)
                        //            blnPaymentDone = true;
                        //        else
                        //        {
                        //            DataTable dtJobPostPayment = BeanHelper.DBHelper.FillTable("Select * From tblJobPostPayment Where Amount > (TransferAmount + RefundAmount) And JobPostId = " + sJobPostId);
                        //            if (dtJobPostPayment != null && dtJobPostPayment.Rows.Count > 0)
                        //            {
                        //                foreach (DataRow drJobPostPayment in dtJobPostPayment.Rows)
                        //                {
                        //                    if (HelperAmount == 0)
                        //                        break;

                        //                    if ((HelperAmount * 100) > ConversionHelper.ConvertToDouble(drJobPostPayment["Amount"]))
                        //                    {
                        //                        TransferAmount = ConversionHelper.ConvertToDouble(drJobPostPayment["Amount"]);
                        //                        HelperAmount = ((HelperAmount * 100) - TransferAmount) / 100;
                        //                    }
                        //                    else
                        //                    {
                        //                        TransferAmount = (HelperAmount * 100);
                        //                        HelperAmount = 0;
                        //                    }

                        //                    var objstripeTransfer = new StripeTransferCreateOptions();
                        //                    objstripeTransfer.Amount = ConversionHelper.ConvertToInt32(TransferAmount);
                        //                    objstripeTransfer.Currency = "cad";
                        //                    objstripeTransfer.SourceTransaction = ConversionHelper.ConvertToString(drJobPostPayment["ChargeId"]);
                        //                    objstripeTransfer.Destination = ConversionHelper.ConvertToString(dtJobPost.Rows[0]["Helper_StripeAccountId"]);

                        //                    var TransferService = new StripeTransferService();
                        //                    var Transfer = TransferService.Create(objstripeTransfer);

                        //                    string sTransferQry = "Update tblJobPostPayment Set TransferAmount = TransferAmount + " + TransferAmount.ToString() + " Where JobPostId = " + sJobPostId + " And ChargeId = '" + ConversionHelper.ConvertToString(drJobPostPayment["ChargeId"]) + "'";
                        //                    BeanHelper.DBHelper.ExecuteNonQuery(sTransferQry);
                        //                }
                        //            }

                        //            //var payoutOptions = new StripeChargeCreateOptions();
                        //            //payoutOptions.Amount = Convert.ToInt32(HelperAmount * 100);
                        //            //payoutOptions.Currency = "cad";
                        //            //payoutOptions.SourceTokenOrExistingSourceId = string.Empty;
                        //            //payoutOptions.Destination = ConversionHelper.ConvertToString(dtJobPost.Rows[0]["Helper_StripeAccountId"]);

                        //            //var payoutService = new StripeChargeService();
                        //            //var payout = payoutService.Create(payoutOptions);
                        //            blnPaymentDone = true;
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Messagesection.Visible = true;
                        //        Message.Text = ex.Message;
                        //        blnPaymentDone = false;
                        //    }
                        //    #endregion

                        //    #region Refund
                        //    double RefundAmount = Convert.ToDouble(dtJobPost.Rows[0]["RefundAmount"]);
                        //    TransferAmount = 0;
                        //    try
                        //    {
                        //        if (RefundAmount == 0)
                        //            blnRefundDone = true;
                        //        else
                        //        {
                        //            if (blnPaymentDone)
                        //            {
                        //                DataTable dtJobPostPayment = BeanHelper.DBHelper.FillTable("Select * From tblJobPostPayment Where Amount > (TransferAmount + RefundAmount) And JobPostId = " + sJobPostId);
                        //                if (dtJobPostPayment != null && dtJobPostPayment.Rows.Count > 0)
                        //                {
                        //                    foreach (DataRow drJobPostPayment in dtJobPostPayment.Rows)
                        //                    {
                        //                        if (RefundAmount == 0)
                        //                            break;

                        //                        if ((RefundAmount * 100) > ConversionHelper.ConvertToDouble(drJobPostPayment["Amount"]))
                        //                        {
                        //                            TransferAmount = ConversionHelper.ConvertToDouble(drJobPostPayment["Amount"]);
                        //                            RefundAmount = ((RefundAmount * 100) - TransferAmount) / 100;
                        //                        }
                        //                        else
                        //                        {
                        //                            TransferAmount = (RefundAmount * 100);
                        //                            RefundAmount = 0;
                        //                        }

                        //                        var objstripeTransfer = new StripeTransferCreateOptions();
                        //                        objstripeTransfer.Amount = ConversionHelper.ConvertToInt32(TransferAmount);
                        //                        objstripeTransfer.Currency = "cad";
                        //                        objstripeTransfer.SourceTransaction = ConversionHelper.ConvertToString(drJobPostPayment["ChargeId"]);
                        //                        objstripeTransfer.Destination = ConversionHelper.ConvertToString(dtJobPost.Rows[0]["HelpSeeker_StripeAccountId"]);

                        //                        var TransferService = new StripeTransferService();
                        //                        var Transfer = TransferService.Create(objstripeTransfer);

                        //                        string sTransferQry = "Update tblJobPostPayment Set RefundAmount = RefundAmount + " + TransferAmount.ToString() + " Where JobPostId = " + sJobPostId + " And ChargeId = '" + ConversionHelper.ConvertToString(drJobPostPayment["ChargeId"]) + "'";
                        //                        BeanHelper.DBHelper.ExecuteNonQuery(sTransferQry);
                        //                    }
                        //                }

                        //                blnRefundDone = true;
                        //                //var refundOptions = new StripeRefundCreateOptions();
                        //                //refundOptions.Amount = (HelperAmount * 100);
                        //                //refundOptions.Reason = "Partial payment refund";
                        //                ////payoutOptions.SourceTokenOrExistingSourceId =
                        //                //refundOptions.Destination = sHelpSeeker_StripeAccountId;

                        //                //var refundService = new StripeRefundService();
                        //                //var refund = refundService.Create(refundOptions);
                        //            }
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Messagesection.Visible = true;
                        //        Message.Text = ex.Message;
                        //        blnRefundDone = false;
                        //    }

                        //    #endregion

                        //    if (blnRefundDone && blnPaymentDone)
                        //    {
                        //        BeanHelper.DBHelper.ExecuteNonQuery("Update tblJobPost Set IsStripePaymentDone = 1 Where JobPostId = " + sJobPostId);
                        //        ddlIsHire_SelectedIndexChanged(null, null);
                        //    }
                        //}
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        #endregion
    }
}