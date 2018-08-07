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
using HelpMe.Helpers;
using HelpMe.Shared.Utilities;

namespace HelpMe
{
    public partial class ClientInfo : System.Web.UI.Page
    {
        #region Variables
        static int shortway_Subscription = 0;
        static int shortway_ProductRedeem = 0;
        #endregion

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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Client Information " + ConfigurationManager.AppSettings["Version"].ToString();

                if (!IsPostBack)
                {
                    TitleCaption.Text = "Client Information";
                    ArrayList coldata = new ArrayList();
                    ArrayList coltype = new ArrayList();

                    #region Subscription
                    ViewState["SearchCondition_SubscriptionGet"] = "[vwSubscriptionGet] Where ClientId = " + ConversionHelper.ConvertToString(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]));
                    ViewState["OrderBy_SubscriptionGet"] = " Order By [SubscriptionId] ASC";
                    ViewState["SelectedField_SubscriptionGet"] = "[SubscriptionId],[PackageName],[FirstName],[LastName],[CreditPost],[CreditPoint],[PaymentAmount],[PaymentTime],[PaymentId],[PaymentStatus],[PaymentResponse]";
                    DataTable dtSubscriptionGet = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_SubscriptionGet"].ToString() + ViewState["OrderBy_SubscriptionGet"].ToString(), ViewState["SelectedField_SubscriptionGet"].ToString());

                    coldata = new ArrayList();
                    coltype = new ArrayList();
                    for (int i = 0; i < dtSubscriptionGet.Columns.Count; i++)
                    {
                        coldata.Add(dtSubscriptionGet.Columns[i].ColumnName);
                        coltype.Add(dtSubscriptionGet.Columns[i].DataType.Name);
                    }
                    ViewState["Columns_SubscriptionGet"] = coldata;
                    ViewState["ColumnsType_SubscriptionGet"] = coltype;

                    GlobalFunctions.SetPaging(ref grdSubscriptionGet);
                    grdSubscriptionGet.DataSource = dtSubscriptionGet;
                    grdSubscriptionGet.DataBind();
                    lblRecord_Subscription.Text = "Subscription Info. : " + (dtSubscriptionGet.Rows.Count.ToString()) + " Record(s)";
                    ViewState["PDFColWidth_Subscription"] = new float[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                    #endregion

                    #region ProductRedeem
                    ViewState["SearchCondition_ProductRedeem"] = "vwProductRedeemGet Where ClientId = " + ConversionHelper.ConvertToString(HtmlSerializer.HtmlToObject(Request.QueryString["p2"]));
                    ViewState["OrderBy_ProductRedeem"] = " Order By [ProductRedeemId] ASC";
                    ViewState["SelectedField_ProductRedeem"] = "[ProductRedeemId],[FirstName],[LastName],[ProductName],[RedeemPoint]";
                 
                    DataTable dtProductRedeem = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_ProductRedeem"].ToString() + ViewState["OrderBy_ProductRedeem"].ToString(), ViewState["SelectedField_ProductRedeem"].ToString());
                    coldata = new ArrayList();
                    coltype = new ArrayList();
                    for (int i = 0; i < dtProductRedeem.Columns.Count; i++)
                    {
                        coldata.Add(dtProductRedeem.Columns[i].ColumnName);
                        coltype.Add(dtProductRedeem.Columns[i].DataType.Name);
                    }
                    ViewState["Columns_ProductRedeem"] = coldata;
                    ViewState["ColumnsType_ProductRedeem"] = coltype;

                    GlobalFunctions.SetPaging(ref grdProductRedeemGet);
                    grdProductRedeemGet.DataSource = dtProductRedeem;
                    grdProductRedeemGet.DataBind();
                    lblRecord_ProductRedeemGet.Text = "Product Redeem Info. : " + (dtProductRedeem.Rows.Count.ToString()) + " Record(s)";
                    ViewState["PDFColWidth_ProductRedeem"] = new float[] { 40, 20, 20, 20 };
                    #endregion
                                        
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

        #region Product Redeem
        protected void grdProductRedeemGet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdProductRedeemGet.PageIndex = e.NewPageIndex;
                BindGrid_ProductRedeem();
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void grdProductRedeemGet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gridView = (GridView)sender;
                if (ViewState["SortCol_ProductRedeem"] != null)
                {
                    int cellIndex = -1;
                    foreach (DataControlField field in gridView.Columns)
                    {
                        if (field.SortExpression == ViewState["SortCol_ProductRedeem"].ToString())
                        {
                            cellIndex = gridView.Columns.IndexOf(field);
                            break;
                        }
                    }

                    if (cellIndex > -1)
                    {
                        if (e.Row.RowType == DataControlRowType.Header)
                            e.Row.Cells[cellIndex].CssClass += (ViewState["SortDir_ProductRedeem"].ToString() == "ASC" ? " sortascheader" : " sortdescheader");
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
        protected void lnkSort_ProductRedeemGet_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                if (lnk != null)
                {
                    if (shortway_ProductRedeem % 2 == 0)
                        ViewState["lastsorton_ProductRedeem"] = Convert.ToString(lnk.ID) + " ASC";
                    else
                        ViewState["lastsorton_ProductRedeem"] = Convert.ToString(lnk.ID) + " DESC";

                    BindGrid_ProductRedeem();

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

                            if (shortway_ProductRedeem % 2 == 0)
                                (((System.Web.UI.WebControls.DataControlFieldCell)(thSort)).ContainingField).HeaderStyle.CssClass = "sorting_asc";
                            else
                                (((System.Web.UI.WebControls.DataControlFieldCell)(thSort)).ContainingField).HeaderStyle.CssClass = "sorting_desc";
                        }
                    }
                    catch { }
                    #endregion

                    shortway_ProductRedeem++;
                }
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void BindGrid_ProductRedeem()
        {
            try
            {
                DataTable data = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_ProductRedeem"].ToString() + ViewState["OrderBy_ProductRedeem"].ToString(), ViewState["SelectedField_ProductRedeem"].ToString());
                grdProductRedeemGet.DataSource = data;
                grdProductRedeemGet.DataBind();

                lblRecord_ProductRedeemGet.Text = "ProductRedeem Info. : " + (data.Rows.Count.ToString()) + " Record(s)";
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }

        protected void btnExportExcel_ProductRedeem_Click(object sender, EventArgs e)
        {
            try
            {
                string SelectedColumns = string.Empty;
                for (int i = 0; i < grdProductRedeemGet.Columns.Count; i++)
                {
                    if (!string.IsNullOrEmpty(grdProductRedeemGet.Columns[i].HeaderText))
                    {
                        if (SelectedColumns == string.Empty)
                            SelectedColumns = SelectedColumns + "[" + grdProductRedeemGet.Columns[i].HeaderText + "]";
                        else
                            SelectedColumns = SelectedColumns + "," + "[" + grdProductRedeemGet.Columns[i].HeaderText + "]";
                    }
                }
                DataTable objDataTable = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_ProductRedeem"].ToString() + ViewState["OrderBy_ProductRedeem"].ToString(), SelectedColumns);
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
        protected void btnExportPDF_ProductRedeem_Click(object sender, EventArgs e)
        {
            try
            {
                string SelectedColumns = string.Empty;
                for (int i = 0; i < grdProductRedeemGet.Columns.Count; i++)
                {
                    if (!string.IsNullOrEmpty(grdProductRedeemGet.Columns[i].HeaderText))
                    {
                        if (SelectedColumns == string.Empty)
                            SelectedColumns = SelectedColumns + "[" + grdProductRedeemGet.Columns[i].HeaderText + "]";
                        else
                            SelectedColumns = SelectedColumns + "," + "[" + grdProductRedeemGet.Columns[i].HeaderText + "]";
                    }
                }
                DataTable objDataTable = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_ProductRedeem"].ToString() + ViewState["OrderBy_ProductRedeem"].ToString(), SelectedColumns);

                PDFWrite objPDFWrite = new PDFWrite();
                objPDFWrite.ExportPDF(objDataTable, (float[])ViewState["PDFColWidth_ProductRedeem"]);
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        #endregion

        #region Subscription
        protected void grdSubscriptionGet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdSubscriptionGet.PageIndex = e.NewPageIndex;
                BindGrid_Subscription();
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void grdSubscriptionGet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gridView = (GridView)sender;
                if (ViewState["SortCol_Subscription"] != null)
                {
                    int cellIndex = -1;
                    foreach (DataControlField field in gridView.Columns)
                    {
                        if (field.SortExpression == ViewState["SortCol_Subscription"].ToString())
                        {
                            cellIndex = gridView.Columns.IndexOf(field);
                            break;
                        }
                    }

                    if (cellIndex > -1)
                    {
                        if (e.Row.RowType == DataControlRowType.Header)
                            e.Row.Cells[cellIndex].CssClass += (ViewState["SortDir_Subscription"].ToString() == "ASC" ? " sortascheader" : " sortdescheader");
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
        protected void lnkSort_Subscription_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                if (lnk != null)
                {
                    if (shortway_Subscription % 2 == 0)
                        ViewState["lastsorton_Subscription"] = Convert.ToString(lnk.ID) + " ASC";
                    else
                        ViewState["lastsorton_Subscription"] = Convert.ToString(lnk.ID) + " DESC";

                    BindGrid_Subscription();

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

                            if (shortway_Subscription % 2 == 0)
                                (((System.Web.UI.WebControls.DataControlFieldCell)(thSort)).ContainingField).HeaderStyle.CssClass = "sorting_asc";
                            else
                                (((System.Web.UI.WebControls.DataControlFieldCell)(thSort)).ContainingField).HeaderStyle.CssClass = "sorting_desc";
                        }
                    }
                    catch { }
                    #endregion

                    shortway_Subscription++;
                }
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }
        protected void BindGrid_Subscription()
        {
            try
            {
                DataTable data = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_SubscriptionGet"].ToString() + ViewState["OrderBy_SubscriptionGet"].ToString(), ViewState["SelectedField_SubscriptionGet"].ToString());
                grdSubscriptionGet.DataSource = data;
                grdSubscriptionGet.DataBind();

                lblRecord_Subscription.Text = "Subscription Info. : " + (data.Rows.Count.ToString()) + " Record(s)";
            }
            catch (Exception ex)
            {
                Messagesection.Visible = true;
                Message.Text = ex.Message;
                LogManager.Log(ex);
            }
        }

        protected void btnExportExcel_Subscription_Click(object sender, EventArgs e)
        {
            try
            {
                string SelectedColumns = string.Empty;
                for (int i = 0; i < grdSubscriptionGet.Columns.Count; i++)
                {
                    if (!string.IsNullOrEmpty(grdSubscriptionGet.Columns[i].HeaderText))
                    {
                        if (SelectedColumns == string.Empty)
                            SelectedColumns = SelectedColumns + "[" + grdSubscriptionGet.Columns[i].HeaderText + "]";
                        else
                            SelectedColumns = SelectedColumns + "," + "[" + grdSubscriptionGet.Columns[i].HeaderText + "]";
                    }
                }
                DataTable objDataTable = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_SubscriptionGet"].ToString() + ViewState["OrderBy_SubscriptionGet"].ToString(), SelectedColumns);
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
        protected void btnExportPDF_Subscription_Click(object sender, EventArgs e)
        {
            try
            {
                string SelectedColumns = string.Empty;
                for (int i = 0; i < grdSubscriptionGet.Columns.Count; i++)
                {
                    if (!string.IsNullOrEmpty(grdSubscriptionGet.Columns[i].HeaderText))
                    {
                        if (SelectedColumns == string.Empty)
                            SelectedColumns = SelectedColumns + "[" + grdSubscriptionGet.Columns[i].HeaderText + "]";
                        else
                            SelectedColumns = SelectedColumns + "," + "[" + grdSubscriptionGet.Columns[i].HeaderText + "]";
                    }
                }
                DataTable objDataTable = BeanHelper.SearchBean.GetData(ViewState["SearchCondition_SubscriptionGet"].ToString() + ViewState["OrderBy_SubscriptionGet"].ToString(), SelectedColumns);

                PDFWrite objPDFWrite = new PDFWrite();
                objPDFWrite.ExportPDF(objDataTable, (float[])ViewState["PDFColWidth_Subscription"]);
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