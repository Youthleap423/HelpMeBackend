<%@ Page Language="C#" MasterPageFile="~/Admin/SiteLinks.Master" AutoEventWireup="true"
    CodeBehind="UserMaster.aspx.cs" Inherits="RentTrack.Admin.UserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>User Master</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div id="pnl_add" runat="server" class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>User Master</span>
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
                    <%--<h4 class="page-header">User Master</h4>--%>
                    <form id="defaultForm" method="post" class="form-horizontal">
                        <fieldset>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validate_group"
                                ShowMessageBox="false" ShowSummary="false" />
                            <asp:UpdatePanel ID="upUserType" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            User Type :<span style="color: Red"> *</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlUserType" runat="server" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"
                                                AutoPostBack="true" CssClass="form-control">
                                                <asp:ListItem Text="--Select User Type--" Value="-1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Admin Panel" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Company Admin" Value="6"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Req_UserType" runat="server" ControlToValidate="ddlUserType"
                                                Display="Dynamic" ErrorMessage="User Type is required." ValidationGroup="validate_group"
                                                InitialValue="-1" CssClass="help-block"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group" id="divCompany" runat="server" visible="false">
                                        <label class="col-sm-2 control-label">
                                            Company Name :</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            User Name :<span style="color: Red"> *</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_UserName" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="User Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="req_txt_UserName" runat="server" ControlToValidate="txt_UserName"
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
                                            <asp:TextBox ID="txt_UserPassword" runat="server" MaxLength="50" TextMode="Password"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Req_txt_UserPassword" runat="server" ControlToValidate="txt_UserPassword"
                                                Display="Dynamic" ErrorMessage="Password is required." ValidationGroup="validate_group"
                                                CssClass="help-block"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            User Role :<span style="color: Red"> *</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlUserRole" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Req_UserRole" runat="server" ControlToValidate="ddlUserRole"
                                                Display="Dynamic" ErrorMessage="User Role is required." ValidationGroup="validate_group"
                                                InitialValue="0" CssClass="help-block"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Mobile No. :<span style="color: Red"> *</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Req_txtMobileNo" runat="server" ControlToValidate="txtMobileNo"
                                                Display="Dynamic" ErrorMessage="Mobile No is required." ValidationGroup="validate_group"
                                                CssClass="help-block"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Email Id :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtEmailId" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Active :</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
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
                        <i class="fa fa-search"></i><span>User Master</span>
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
                    <%--<h4 class="page-header">Search User Master</h4>--%>
                    <form id="defaultForm1" method="post" class="form-horizontal">
                        <fieldset>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    User Name</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txt_UserSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
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
                        <i class="fa fa-th-large"></i><span>Total User(s) :</span>
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
                            <asp:Button ID="btn_add" runat="server" Text="Add User" OnClick="btn_add_Click" CssClass="btn btn-primary btn-label-left" />
                            <asp:Button ID="btn_export" runat="server" Text="Export to Excel" OnClick="btn_export_Click"
                                CssClass="btn btn-warning btn-label-left" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="content">
                        <asp:GridView ID="grd_main" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="false" GridLines="None" OnPageIndexChanging="grd_main_PageIndexChanging"
                            EmptyDataText="No User(s) Found." Width="100%" RowStyle-Wrap="true" HeaderStyle-Wrap="false"
                            FooterStyle-Wrap="false" AlternatingRowStyle-Wrap="true" SelectedRowStyle-Wrap="false"
                            SortedAscendingHeaderStyle-Wrap="false" SortedDescendingHeaderStyle-Wrap="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span style="font-weight: bold">Actions</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_update" runat="server" ToolTip="Click here to Modify record"
                                            CommandArgument='<%#Eval("UserId")%>' OnClick="btn_click"><img src="Content/img/icons/actions/edit.png" alt="" /></asp:LinkButton>
                                        <asp:LinkButton ID="lnk_delete" runat="server" ToolTip="Click here to Delete record"
                                            CommandArgument='<%#Eval("UserId")%>' OnClick="btn_click" OnClientClick="javascript:return confirm('Are you sure want to delete this record?');"><img
                                            src="Content/img/icons/actions/delete.png" alt="" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="UserName" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="User Name"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("User Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="UserRoleName" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="User Role"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("User Role")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="UserTypeName" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="User Type"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("User Type")%>
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
                                            User Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("User Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            User Role
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("User Role")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            User Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("User Type")%>
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
