<%@ Page  Language="C#" MasterPageFile="~/Admin/SiteLinks.Master" AutoEventWireup="true"
    CodeBehind="SMSSettings.aspx.cs" Inherits="RentTrack.Admin.SMSSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>SMS Settings</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div id="pnl_add" runat="server" class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>SMS Settings</span>
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
                    <%--<h4 class="page-header">SMS Settings</h4>--%>
                    <form id="defaultForm" method="post" class="form-horizontal">
                    <fieldset>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="validate_group"
                            ShowMessageBox="false" ShowSummary="false" />
                        <asp:UpdatePanel ID="upUserType" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Company Name :</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                            AutoPostBack="true">
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
                                        User Name :<span style="color: Red"> *</span></label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtUserName" runat="server" MaxLength="100" CssClass="form-control"
                                            ToolTip="User Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="req_txtUserName" runat="server" ControlToValidate="txtUserName"
                                            Display="Dynamic" ErrorMessage="User Name is required." ValidationGroup="validate_group"
                                            CssClass="help-block"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        User Password :<span style="color: Red"> *</span></label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" MaxLength="20"
                                            CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="req_txtUserPassword" runat="server" ControlToValidate="txtUserPassword"
                                            Display="Dynamic" ErrorMessage="Password is required." ValidationGroup="validate_group"
                                            CssClass="help-block"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Sender Id :</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSenderId" runat="server" MaxLength="100" CssClass="form-control"
                                            ToolTip="Sender Id"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        SMS Credit :
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtTotalSMS" runat="server" MaxLength="20" CssClass="form-control"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Save Settings" CssClass="btn btn-primary btn-label-left"
                                    OnClick="btnSubmit_Click" ValidationGroup="validate_group" CausesValidation="True" />
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
                        <i class="fa fa-search"></i><span>User Role</span>
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
                    <%--<h4 class="page-header">Search User Role</h4>--%>
                    <form id="defaultForm1" method="post" class="form-horizontal">
                    <fieldset>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Search By :</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_SearchBy" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
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
                        <i class="fa fa-th-large"></i><span>Total SMS Setting(s) :</span>
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
                            <asp:Button ID="btn_export" runat="server" Text="Export to Excel" OnClick="btn_export_Click"
                                CssClass="btn btn-warning btn-label-left" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="content">
                        <asp:GridView ID="grd_main" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="false" GridLines="None" OnPageIndexChanging="grd_main_PageIndexChanging"
                            EmptyDataText="No SMS Setting(s) Found." Width="100%" RowStyle-Wrap="true" HeaderStyle-Wrap="false"
                            FooterStyle-Wrap="false" AlternatingRowStyle-Wrap="true" SelectedRowStyle-Wrap="false"
                            SortedAscendingHeaderStyle-Wrap="false" SortedDescendingHeaderStyle-Wrap="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span style="font-weight: bold">Actions</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_update" runat="server" ToolTip="Click here to Modify record"
                                            CommandArgument='<%#Eval("CompanyId")%>' OnClick="btn_click"><img src="Content/img/icons/actions/edit.png" alt="" /></asp:LinkButton>
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
                                        <asp:LinkButton ID="UserName" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="User Name"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("UserName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="TotalSMS" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="SMS Credit"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("TotalSMS")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="SenderId" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Sender Id"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("SenderId")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="headercss" />
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle CssClass="pagination" HorizontalAlign="Right" />
                        </asp:GridView>
                        <div style="display: none;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" GridLines="None"
                                Width="100%">
                                <Columns>
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
                                            User Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("UserName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SMS Credit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("TotalSMS")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Sender Id
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("SenderId")%>
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
