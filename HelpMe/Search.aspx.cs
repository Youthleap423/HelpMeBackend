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

namespace HelpMe
{
    public partial class Search : System.Web.UI.Page
    {
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

                #region Switch Case
                switch (section)
                {
                    #region Administration
                    case "UserRole":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - User Role - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "User":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - User Setup - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "Email Settings":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Email Settings - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "General Settings":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - General Settings - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;

                    case "Category":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Category - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "Product":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Product - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "Package":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Package - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "Client":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Client - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "Job Post":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Job Post - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "Subscription":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Subscription - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "Product Redeem":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Product Redeem - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "Notification":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - Notification - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                    case "AboutUs":
                        Page.Title = ConfigurationManager.AppSettings["ProjectTitle"].ToString() + " - Search - About Us - Version : " + ConfigurationManager.AppSettings["Version"].ToString();
                        break;
                        
                    #endregion
                }
                #endregion

                if (!IsPostBack)
                {
                    txtSearch.Focus();

                    #region  Page Selection
                    switch (section)
                    {
                        #region Administration
                        case "UserRole":
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "vwUserRoleGet";
                            ViewState["ReturnPage"] = "~/UserRole.aspx?p1=1";
                            ViewState["AddPage"] = "~/UserRole.aspx?p1=1";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [User Role] ASC";
                            ViewState["SelectedField"] = "[UserRoleId],[User Role],[Description],[Status]";
                            TitleCaption.Text = "User Role";
                            ViewState["PDFColWidth"] = new float[] { 40, 30, 30 };
                            break;
                        case "User":
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "vwLoginGet";
                            ViewState["ReturnPage"] = "~/UserMaster.aspx?p1=1";
                            ViewState["AddPage"] = "~/UserMaster.aspx?p1=1";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString() + " Where IsActive = " + ConversionHelper.ConvertToString(ddlStatus.SelectedValue);
                            ViewState["OrderBy"] = " Order By [User Name] ASC";
                            ViewState["SelectedField"] = "LoginId,[User Name],[User Role],[Status]";
                            TitleCaption.Text = "User Setup";
                            ViewState["PDFColWidth"] = new float[] { 40, 30, 30 };
                            break;
                        case "Email Settings":
                            ddlStatus.Visible = false;
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "vwEmailSettingsGet";
                            ViewState["ReturnPage"] = "~/EmailSettings.aspx?p1=1";
                            ViewState["AddPage"] = "~/EmailSettings.aspx?p1=1";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [LoginName] ASC";
                            ViewState["SelectedField"] = "[EmailSettingsId],[LoginId],[LoginName],[SMTPServer],[SMTPPort],[EnableSSL]";
                            TitleCaption.Text = "Email Settings";
                            ViewState["PDFColWidth"] = new float[] { 40, 30, 30 };
                            break;
                        case "General Settings":
                            ddlStatus.Visible = false;
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "[vwGeneralSettings]";
                            ViewState["ReturnPage"] = "~/GeneralSettings.aspx?p1=1";
                            ViewState["AddPage"] = "~/GeneralSettings.aspx?p1=1";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [GeneralSettingId] ASC";
                            ViewState["SelectedField"] = "GeneralSettingId, CreditPoint, ShareApp, SharePost, AppModeDisp";
                            TitleCaption.Text = "General Settings";
                            ViewState["PDFColWidth"] = new float[] { 40, 30, 30 };
                            break;


                        case "Category":
                            ddlStatus.Visible = false;
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "vwCategoryGet";
                            ViewState["ReturnPage"] = "~/Category.aspx?p1=2";
                            ViewState["AddPage"] = "~/Category.aspx?p1=2";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [CategoryId] ASC";
                            ViewState["SelectedField"] = "[CategoryId],[Category Name],[CategoryPoints],[ColorCode],[Status],[Is Free]";
                            TitleCaption.Text = "Category";
                            ViewState["PDFColWidth"] = new float[] { 40, 15, 15, 15, 15 };
                            break;
                        case "Product":
                            ddlStatus.Visible = false;
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "vwProductGet";
                            ViewState["ReturnPage"] = "~/Product.aspx?p1=2";
                            ViewState["AddPage"] = "~/Product.aspx?p1=2";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [ProductId] ASC";
                            ViewState["SelectedField"] = "[ProductId],[ProductName],[Point],[ProductImage],[Description]";
                            TitleCaption.Text = "Product";
                            ViewState["PDFColWidth"] = new float[] { 40, 20, 20, 20 };
                            break;
                        case "Package":
                            ddlStatus.Visible = false;
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "vwPackageGet";
                            ViewState["ReturnPage"] = "~/Package.aspx?p1=2";
                            ViewState["AddPage"] = "~/Package.aspx?p1=2";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [PackageId] ASC";
                            ViewState["SelectedField"] = "[PackageId],[PackageName],[CreditPost],[CreditPoint],[Amount],[Description]";
                            TitleCaption.Text = "Package";
                            ViewState["PDFColWidth"] = new float[] { 40, 15, 15, 15, 15 };
                            break;
                        case "Client":
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
                            ViewState["SelectedField"] = "[ClientId],[FirstName],[LastName],[GenderDisp],[Address1],[Address2],[CityName],[POBox],[StateName],[CountryName],[PhoneNo],[EmailId],[Rating],[Points],[HelpMe],[Offered],[Status]";
                            TitleCaption.Text = "Client";
                            ViewState["PDFColWidth"] = new float[] { 7, 7, 7, 7, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
                            break;
                        case "Job Post":
                            ddlStatus.Visible = true;
                            chkArchived.Visible = false;
                            btnAdd.Visible = false;
                            btnAddData.Visible = false;                            
                            ViewState["SelectedView"] = "vwJobPostGet";
                            ViewState["ReturnPage"] = "~/SearchJobPost.aspx?p1=2";
                            ViewState["AddPage"] = "~/SearchJobPost.aspx?p1=2";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [JobPostId] ASC";
                            ViewState["SelectedField"] = "[JobPostId],[JobTitle],[JobDescription],[FirstName],[LastName],[CategoryName],[JobPostingPoints],[JobPostingAmount],[JobAmount],[JobHour],[JobAmount],[PaymentTime],[PaymentId],[PaymentStatus],[PaymentResponse],[IsFree],[EmailId],[JobActualHour],[TotalPayment],[RefundAmount],[DeductionAmount],[HelperAmount],[StripeDeductionAmount]";
                            TitleCaption.Text = "Job Post";
                            ViewState["PDFColWidth"] = new float[] { 7, 7, 7, 7, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
                            break;
                        case "Subscription":
                            chkArchived.Visible = false;
                            btnAdd.Visible = false;
                            btnAddData.Visible = false;
                            ddlStatus.Visible = false;
                            ViewState["SelectedView"] = "vwSubscriptionGet";
                            ViewState["ReturnPage"] = "~/Subscription.aspx?p1=2";
                            ViewState["AddPage"] = "~/Subscription.aspx?p1=2";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [SubscriptionId] ASC";
                            ViewState["SelectedField"] = "[SubscriptionId],[PackageName],[FirstName],[LastName],[CreditPost],[CreditPoint],[PaymentAmount],[PaymentTime],[PaymentId],[PaymentStatus],[PaymentResponse]";
                            TitleCaption.Text = "Subscription";
                            ViewState["PDFColWidth"] = new float[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                            break;
                        case "Product Redeem":
                            ddlStatus.Visible = false;
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "vwProductRedeemGet";
                            ViewState["ReturnPage"] = "~/ProductRedeem.aspx?p1=2";
                            ViewState["AddPage"] = "~/ProductRedeem.aspx?p1=2";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [ProductRedeemId] ASC";
                            ViewState["SelectedField"] = "[ProductRedeemId],[FirstName],[LastName],[ProductName],[RedeemPoint]";
                            TitleCaption.Text = "Product Redeem";
                            ViewState["PDFColWidth"] = new float[] { 40, 20, 20, 20 };
                            break;
                        case "Notification":
                            ddlStatus.Visible = false;
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "vwNotificationGet";
                            ViewState["ReturnPage"] = "~/Notification.aspx?p1=2";
                            ViewState["AddPage"] = "~/Notification.aspx?p1=2";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [NotificationId] ASC";
                            ViewState["SelectedField"] = "[NotificationId],[FirstName],[LastName],[AppHeading],[Title],[Remarks]";
                            TitleCaption.Text = "Notification";
                            ViewState["PDFColWidth"] = new float[] { 40, 15, 15, 15, 15 };
                            break;
                        case "AboutUs":
                            ddlStatus.Visible = false;
                            chkArchived.Visible = false;
                            ViewState["SelectedView"] = "vwAboutUsGet";
                            ViewState["ReturnPage"] = "~/AboutUs.aspx?p1=2";
                            ViewState["AddPage"] = "~/AboutUs.aspx?p1=2";
                            ViewState["DefaultCondition"] = string.Empty;
                            ViewState["SearchCondition"] = ViewState["SelectedView"].ToString();
                            ViewState["OrderBy"] = " Order By [AboutUsId] ASC";
                            ViewState["SelectedField"] = "[AboutUsId],[Remarks]";
                            TitleCaption.Text = "About Us";
                            ViewState["PDFColWidth"] = new float[] { 40, 15, 15, 15, 15 };
                            break;
                        #endregion
                    }
                    #endregion

                    DataTable data = BeanHelper.SearchBean.GetData(ViewState["SearchCondition"].ToString() + ViewState["OrderBy"].ToString(), ViewState["SelectedField"].ToString());

                    // Convert Id to HTMLObject
                    DataColumn Col = data.Columns.Add("IdEnc", System.Type.GetType("System.String"));
                    Col.SetOrdinal(1);
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        data.Rows[i][1] = HtmlSerializer.ObjectToHTML(data.Rows[i][0].ToString());
                    }

                    ArrayList coldata = new ArrayList();
                    ArrayList coltype = new ArrayList();

                    ArrayList datakeycol = new ArrayList();

                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        if (data.Columns[i].ColumnName != "IdEnc")
                        {
                            coldata.Add(data.Columns[i].ColumnName);
                            coltype.Add(data.Columns[i].DataType.Name);
                        }
                        if (data.Columns[i].ColumnName.ToUpper().IndexOf("ID") != -1)
                        {
                            if (i < 2) datakeycol.Add(data.Columns[i].ColumnName);
                        }
                    }

                    int intHyperField = 1;
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        if (data.Columns[i].ColumnName.ToUpper().IndexOf("ID") == -1 || data.Columns[i].ColumnName.ToUpper() == "EMAILID")
                        {
                            if (intHyperField == 1)
                            {
                                HyperLinkField hlfHyperMain = new HyperLinkField();
                                hlfHyperMain.HeaderText = data.Columns[i].ColumnName;
                                hlfHyperMain.DataTextField = data.Columns[i].ColumnName;
                                hlfHyperMain.SortExpression = data.Columns[i].ColumnName;
                                hlfHyperMain.ItemStyle.CssClass = "bluelink";
                                hlfHyperMain.DataNavigateUrlFields = (String[])datakeycol.ToArray(typeof(string));
                                hlfHyperMain.DataNavigateUrlFormatString = ViewState["ReturnPage"] + "&p2={1}";
                                intHyperField = intHyperField + 1;
                                grddata.Columns.Add(hlfHyperMain);
                            }
                            else
                            {
                                BoundField boundField = new BoundField();
                                boundField.DataField = data.Columns[i].ColumnName;
                                boundField.HeaderText = data.Columns[i].ColumnName;
                                boundField.SortExpression = data.Columns[i].ColumnName;
                                boundField.HtmlEncode = true;
                                if (data.Columns[i].DataType.Name == "Decimal")
                                    boundField.DataFormatString = "{0:n2}";
                                else if (data.Columns[i].DataType.Name == "DateTime")
                                    boundField.DataFormatString = "{0:dd/MM/yyyy hh:mm:ss:tt}";
                                intHyperField = intHyperField + 1;
                                grddata.Columns.Add(boundField);
                            }
                        }
                    }

                    //switch (section)
                    //{
                    //    case "Tender":
                    //        ButtonField btn = new ButtonField();
                    //        //btn.Text = "Yes";
                    //        btn.Text = "<i class='glyphicon glyphicon-ok-circle' ></i>";
                    //        btn.CommandName = "StatusYes";
                    //        btn.ButtonType = ButtonType.Link;
                    //        btn.HeaderText = "Is Submit";

                    //        grddata.Columns.Add(btn);
                    //        ViewState["IsSubmitColumnIndex"] = grddata.Columns.Count - 1;
                    //        break;

                    //}
                    //if (section == "Client" || section == "Package" || section == "File Upload")
                    //{
                    //    ButtonField btn = new ButtonField();
                    //    btn.Text = "Delete";
                    //    btn.CommandName = "DeleteRecord";
                    //    btn.ButtonType = ButtonType.Link;
                    //    btn.HeaderText = "Delete";
                    //    grddata.Columns.Add(btn);
                    //    ViewState["deletebuttonColumnIndex"] = grddata.Columns.Count - 1;
                    //}

                    grddata.DataKeyNames = (string[])datakeycol.ToArray(typeof(string));

                    ViewState["Columns"] = coldata;
                    ViewState["ColumnsType"] = coltype;

                    GlobalFunctions.SetPaging(ref grddata);

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
                    string section = HtmlSerializer.HtmlToObject(Request.QueryString["p2"].ToString()).ToString();
                    if (section == "IssueAllocation")
                    {
                        DateTimeFormatInfo dtfrom = new DateTimeFormatInfo();
                        dtfrom.ShortDatePattern = "dd/MM/yyyy";
                        dtfrom.DateSeparator = "/";

                        DataRowView drvCurrent = (DataRowView)e.Row.DataItem;
                        if (Convert.ToDateTime(drvCurrent["Resolve By Date"].ToString(), dtfrom) < DateTime.Now)
                        {
                            e.Row.BackColor = Color.FromArgb(255, 182, 193);
                            e.Row.ForeColor = Color.White;
                        }
                    }
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (ViewState["IsSubmitColumnIndex"] != null)
                    {
                        int deletebuttonColumnIndex = (int)ViewState["IsSubmitColumnIndex"];
                        LinkButton button = e.Row.Cells[deletebuttonColumnIndex].Controls[0] as LinkButton;
                        if (e.Row.Cells[2].Text != null && e.Row.Cells[2].Text == "Yes")
                        {
                            button.CommandName = "StatusNo";
                            //button.Text = e.Row.Cells[2].Text;
                            button.Text = "<i class='glyphicon glyphicon-ok-circle' ></i>";
                            button.CssClass = "btn1 btn-success";

                        }
                        else
                        {
                            button.CommandName = "StatusYes";
                            //button.Text = e.Row.Cells[2].Text;
                            button.Text = "<i class='glyphicon glyphicon-remove' ></i>";
                            button.CssClass = "btn1 btn-danger";

                        }
                    }
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (ViewState["deletebuttonColumnIndex"] != null)
                    {
                        int deletebuttonColumnIndex = (int)ViewState["deletebuttonColumnIndex"];
                        LinkButton button = e.Row.Cells[deletebuttonColumnIndex].Controls[0] as LinkButton;
                        if (button != null && button.CommandName == "DeleteRecord")
                        {
                            button.OnClientClick = "if (!confirm('Are you sure you want to delete this record?')) return false;";
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
        protected void grddata_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["SortCol"] != null)
                {
                    if (ViewState["SortCol"].ToString() == e.SortExpression)
                    {
                        if (ViewState["SortDir"].ToString() == "ASC")
                            ViewState["SortDir"] = "DESC";
                        else
                            ViewState["SortDir"] = "ASC";
                    }
                    else
                    {
                        ViewState["SortCol"] = e.SortExpression;
                        ViewState["SortDir"] = "ASC";
                    }
                }
                else
                {
                    ViewState["SortCol"] = e.SortExpression;
                    ViewState["SortDir"] = "ASC";
                }

                ViewState["OrderBy"] = " Order By [" + ViewState["SortCol"].ToString() + "] " + ViewState["SortDir"].ToString();
                BindGrid();
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