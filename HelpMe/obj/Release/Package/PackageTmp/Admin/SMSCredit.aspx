<%@ Page Language="C#" MasterPageFile="~/Admin/SiteLinks.Master" AutoEventWireup="true"
    CodeBehind="SMSCredit.aspx.cs" Inherits="RentTrack.Admin.SMSCredit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $('#<%=txt_TransactionDate.ClientID%>').datepicker({ dateFormat: "dd/mm/yy", onSelect: function () { } });
        }
    </script>
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>SMS Credit</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div id="pnl_add" runat="server" class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>SMS Credit</span>
                    </div>
                    <div class="box-icons">
                        <a class="collapse-link"><i class="fa fa-chevron-up"></i></a><a class="expand-link">
                            <i class="fa fa-expand"></i></a><a class="close-link"><i class="fa fa-times"></i>
                            </a>
                    </div>
                    <div class="no-move">
                    </div>
                </div>
                <div class="box-content">
                    <%--<h4 class="page-header">SMS Credit</h4>--%>
                    <form id="defaultForm" method="post" class="form-horizontal">
                        <fieldset>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validate_group"
                                ShowMessageBox="false" ShowSummary="false" />
                            <asp:UpdatePanel ID="upUserType" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Credit Note No. :<span style="color: Red"> *</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_CreditNoteNo" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="Credit Note No." ReadOnly="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Req_txt_CreditNoteNo" runat="server" ControlToValidate="txt_CreditNoteNo"
                                                Display="Dynamic" ErrorMessage="Credit Note No. is required." ValidationGroup="validate_group"
                                                CssClass="help-block"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Transaction Date :</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_TransactionDate" runat="server" MaxLength="10" CssClass="form-control"
                                                ToolTip="Transaction Date"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Req_txt_TransactionDate" runat="server" ControlToValidate="txt_TransactionDate"
                                                Display="Dynamic" ErrorMessage="Transaction Date is required." ValidationGroup="validate_group"
                                                CssClass="help-block"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender runat="server" ID="ftb_txt_TransactionDate" TargetControlID="txt_TransactionDate"
                                                ValidChars="0123456789/" FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Company Name :</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Req_Company" runat="server" ControlToValidate="ddlCompany"
                                                Display="Dynamic" ErrorMessage="Company is required." ValidationGroup="validate_group"
                                                InitialValue="0" CssClass="help-block"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Quantity :<span style="color: Red"> *</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_Quantity" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="Quantity" Text="0.00" OnTextChanged="SetTotal" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Req_txt_Quantity" runat="server" ControlToValidate="txt_Quantity"
                                                Display="Dynamic" ErrorMessage="Quantity is required." ValidationGroup="validate_group"
                                                CssClass="help-block"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender runat="server" ID="fte_txt_Quantity" TargetControlID="txt_Quantity"
                                                ValidChars="0123456789." FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Rate / Quantity :</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_RatePerSMS" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="Rate" Text="0.00" OnTextChanged="SetTotal" AutoPostBack="true">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Req_txt_RatePerSMS" runat="server" ControlToValidate="txt_RatePerSMS"
                                                Display="Dynamic" ErrorMessage="Rate is required." ValidationGroup="validate_group"
                                                CssClass="help-block"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender runat="server" ID="fte_txt_RatePerSMS" TargetControlID="txt_RatePerSMS"
                                                ValidChars="0123456789." FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Gross Amount :</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_GrossAmount" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="TotalAmount" ReadOnly="true" Text="0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Tax (%) :</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_TaxPercent" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="Rate" Text="0.00" OnTextChanged="SetTotal" AutoPostBack="true"> </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Req_txt_TaxPercent" runat="server" ControlToValidate="txt_TaxPercent"
                                                Display="Dynamic" ErrorMessage="Rate is required." ValidationGroup="validate_group"
                                                CssClass="help-block"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender runat="server" ID="fte_txt_TaxPercent" TargetControlID="txt_TaxPercent"
                                                ValidChars="0123456789." FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Total Amount :</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_TotalAmount" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="TotalAmount" ReadOnly="true" Text="0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Other Notes :</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txt_Description" runat="server" MaxLength="300" TextMode="MultiLine"
                                                CssClass="form-control" ToolTip="Description"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <div class="col-sm-offset-2 col-sm-2">
                                    <asp:HiddenField ID="hd_primaryid" runat="server" />
                                    <asp:Button ID="btn_submit" runat="server" Text="Save" CssClass="btn btn-primary btn-label-left"
                                        OnClick="btn_submit_Click" ValidationGroup="validate_group" CausesValidation="True" />
                                    <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CssClass="btn btn-warning btn-label-left"
                                        OnClick="btn_cancel_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                        </fieldset>
                    </form>
                </div>
            </div>
        </div>
        <div id="pnl_list" runat="server" class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-search"></i><span>SMS Credit</span>
                    </div>
                    <div class="box-icons">
                        <a class="collapse-link"><i class="fa fa-chevron-up"></i></a><a class="expand-link">
                            <i class="fa fa-expand"></i></a><a class="close-link"><i class="fa fa-times"></i>
                            </a>
                    </div>
                    <div class="no-move">
                    </div>
                </div>
                <div class="box-content">
                    <%--<h4 class="page-header">Search SMS Credit</h4>--%>
                    <form id="defaultForm1" method="post" class="form-horizontal">
                        <fieldset>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Company Name :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txt_CompanyNameSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Credit Note No. :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txt_CreditNoteNoSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    &nbsp;</label>
                                <div class="col-sm-2">
                                    <asp:Button ID="btn_search" runat="server" Text="Search" CssClass="btn btn-primary btn-label-left"
                                        OnClick="btn_search_Click" />
                                    <asp:Button ID="btn_Cancelsearch" runat="server" Text="Clear" CssClass="btn btn-warning btn-label-left"
                                        OnClick="btn_Cancelsearch_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                        </fieldset>
                    </form>
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Total SMS Credit(s) :</span>
                        <asp:Label ID="lbl_total" runat="server" class="control-label"></asp:Label>
                    </div>
                    <div class="box-icons">
                        <a class="collapse-link"><i class="fa fa-chevron-up"></i></a><a class="expand-link">
                            <i class="fa fa-expand"></i></a><a class="close-link"><i class="fa fa-times"></i>
                            </a>
                    </div>
                    <div class="no-move">
                    </div>
                </div>
                <div class="box-content">
                    <div class="form-group">
                        <div class="col-sm-3">
                            <asp:Button ID="btn_add" runat="server" Text="Add SMS Credit" OnClick="btn_add_Click"
                                CssClass="btn btn-primary btn-label-left" />
                            <asp:Button ID="btn_export" runat="server" Text="Export to Excel" OnClick="btn_export_Click"
                                CssClass="btn btn-warning btn-label-left" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="content" style="width: 100%; overflow: auto">
                        <asp:GridView ID="grd_main" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="false" GridLines="None" OnPageIndexChanging="grd_main_PageIndexChanging"
                            EmptyDataText="No SMSCredit(s) Found." Width="100%" RowStyle-Wrap="true" HeaderStyle-Wrap="false"
                            FooterStyle-Wrap="false" AlternatingRowStyle-Wrap="true" SelectedRowStyle-Wrap="false"
                            SortedAscendingHeaderStyle-Wrap="false" SortedDescendingHeaderStyle-Wrap="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span style="font-weight: bold">Actions</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_update" runat="server" ToolTip="Click here to Modify record"
                                            CommandArgument='<%#Eval("SMSCreditId")%>' OnClick="btn_click"><img src="Content/img/icons/actions/edit.png" alt="" /></asp:LinkButton>
                                        <asp:LinkButton ID="lnk_delete" runat="server" ToolTip="Click here to Delete record"
                                            CommandArgument='<%#Eval("SMSCreditId")%>' OnClick="btn_click" OnSMSCreditClick="javascript:return confirm('Are you sure want to delete this record?');"><img
                                            src="Content/img/icons/actions/delete.png" alt="" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="CreditNoteNo" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Credit Note No."></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("CreditNoteNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="TransactionDate" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Transaction Date"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("TransactionDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="CompanyName" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Company Name"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("CompanyName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="Quantity" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Quantity"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Quantity")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="RatePerSMS" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Rate / Quantity"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("RatePerSMS")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="GrossAmount" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Gross Amount"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("GrossAmount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="TaxAmount" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Tax Amount"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("TaxAmount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="TotalAmount" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Total Amount"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("TotalAmount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle HorizontalAlign="Right" />
                        </asp:GridView>
                        <div style="display: none;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" GridLines="None"
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Credit Note No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("CreditNoteNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Transaction Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("TransactionDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Company Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("CompanyName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Quantity
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Quantity")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Rate / Quantity
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("RatePerSMS")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Gross Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("GrossAmount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Tax Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("TaxAmount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Total Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("TotalAmount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
