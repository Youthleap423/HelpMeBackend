using HelpMe.BusinessAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelpMe.Shared.Utilities;
using HelpMe.Helpers;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace HelpMe
{
    public partial class SearchClient : System.Web.UI.Page
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
                Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Client - Version : " + ConfigurationManager.AppSettings["Version"].ToString();

                if (!IsPostBack)
                {
                    txtSearch.Focus();

                    ddlStatus.Visible = false;
                    chkArchived.Visible = false;
                    btnAdd.Visible = false;
                    btnAddData.Visible = false;

                    ViewState["SelectedView"] = "vwClientGet";
                    ViewState["ReturnPage"] = "~/ClientInfo.aspx?p1=2";
                    ViewState["AddPage"] = "~/ClientInfo.aspx?p1=2";
                    ViewState["DefaultCondition"] = string.Empty;
                    ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                    ViewState["OrderBy"] = " Order By [ClientId] ASC";
                    ViewState["SelectedField"] = @"ClientId,FirstName,LastName,Gender,GenderDisp,Address1,Address2,City,POBox,State,Country,PhoneNo,EmailId,Password,Status,IsActive,CreatedOn,EndDate,AcTokenId,RegisteredBy,CityName,
                                                    StateName,CountryName,Latitude,Longitude,Altitude,CreditPoint,ProfilePic,IsClientProfile,Rating,Points,HelpMe,Offered,Radious,BirthDate,IsBankInformation,BusinessTaxId,PersonalIdNumber,
                                                    BankAccountNumber,RoutingNumber,LegalDocument,StripeAccountId,IsStripeConnectVerified,PaymentMethod,PaymentMethodDisp";
                    TitleCaption.Text = "Client";
                    ViewState["PDFColWidth"] = new float[] { 7, 7, 7, 7, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };

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
                    }
                    else
                    {
                        btnExportExcel.Visible = false;
                        btnExportPDF.Visible = false;
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
                    if (string.IsNullOrEmpty(ConversionHelper.ConvertToString(((HiddenField)e.Row.Cells[0].Controls[3]).Value)))
                        button.Visible = false;
                    else
                        button.Visible = true;

                    ImageButton button1 = e.Row.Cells[1].Controls[1] as ImageButton;
                    if (ConversionHelper.ConvertToString(((HiddenField)e.Row.Cells[1].Controls[3]).Value) == "0")
                        button1.ImageUrl = "~/Content/images/icons/no.png";
                    else
                        button1.ImageUrl = "~/Content/images/icons/yes.png";
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
                    if (ConversionHelper.ConvertToString(ddlStatus.SelectedValue) != "2")
                        sWhere = " IsFree = " + ConversionHelper.ConvertToString(ddlStatus.SelectedValue);
                }


                if (!string.IsNullOrEmpty(strwhere.ToString()))
                    sWhere = sWhere + " And ( " + strwhere.ToString() + " )";

                if (string.IsNullOrEmpty(ViewState["DefaultCondition"].ToString()))
                    mainstrwhere = ViewState["SelectedView"].ToString() + " Where " + sWhere;
                else
                    mainstrwhere = ViewState["SelectedView"].ToString() + " Where " + ViewState["DefaultCondition"].ToString() + " And " + sWhere;

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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ViewState["AddPage"].ToString(), false);
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
        
        protected void imgLegalDocument_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                if (img != null)
                {
                    if (!string.IsNullOrEmpty(ConversionHelper.ConvertToString(img.CommandArgument)))
                    {
                        string sFilePath = "webservice//ClientDocument//" + ConversionHelper.ConvertToString(img.CommandArgument) + ".png";
                        string path = Server.MapPath(sFilePath);
                        FileInfo file = new FileInfo(path);
                        if (File.Exists(path))
                        {
                            Response.ClearContent();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = ".png"; Response.TransmitFile(file.FullName); Response.End();
                        }
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
        protected void imgVerifyStripeConnect_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton img = (ImageButton)sender;
                if (img != null)
                {
                    if (!string.IsNullOrEmpty(ConversionHelper.ConvertToString(img.CommandArgument)))
                    {
                        BeanHelper.DBHelper.ExecuteNonQuery("Update tblClient Set IsStripeConnectVerified = 1 Where ClientId = " + ConversionHelper.ConvertToString(img.CommandArgument));
                        btnSearch_Click(null, null);
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

        //protected void grddata_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        string section = string.Empty;

        //        try
        //        {
        //            section = HtmlSerializer.HtmlToObject(Request.QueryString["p2"]).ToString();
        //        }
        //        catch (Exception)
        //        {
        //            Response.Redirect("~/Dashboard.aspx", false);
        //        }

        //        int rowIndex = Convert.ToInt32(e.CommandArgument);
        //        int Id = Convert.ToInt32(grddata.DataKeys[rowIndex].Values[0]);
        //        int ReturnValue;

        //        if (section == "Client")
        //        {
        //            HelpMe.Entities.Client objClient = new HelpMe.Entities.Client();

        //            objClient.ClientId = Id;

        //            BeanHelper.ClientBean.ObjClient = objClient;
        //            ReturnValue = BeanHelper.ClientBean.ExecuteData(HelpMe.CommonEnums.HelpMeOperations.D);
        //            FindSearchAndBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Messagesection.Visible = true;
        //        Message.Text = ex.Message;
        //        LogManager.Log(ex);
        //    }
        //}
    }
}