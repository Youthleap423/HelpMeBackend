<%@ Page Language="C#" MasterPageFile="~/Admin/SiteLinks.Master" AutoEventWireup="true"
    CodeBehind="UserRole.aspx.cs" Inherits="RentTrack.Admin.UserRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>User Role</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div id="pnl_add" runat="server" class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>User Role</span>
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
                    <%--<h4 class="page-header">User Role</h4>--%>
                    <form id="defaultForm" method="post" class="form-horizontal">
                        <fieldset>
                            <div class="form-group" id="Messagesection" runat="server" visible="false">
                                <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validate_group"
                                ShowMessageBox="false" ShowSummary="false" />
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    User Role Name :<span style="color: Red"> *</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txt_UserRoleName" runat="server" MaxLength="100" CssClass="form-control"
                                        ToolTip="Email Id"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="req_txt_UserRoleName" runat="server" ControlToValidate="txt_UserRoleName"
                                        Display="Dynamic" ErrorMessage="User Role Name is required." ValidationGroup="validate_group"
                                        CssClass="help-block"></asp:RequiredFieldValidator>
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
                            <div class="content">
                                <asp:Repeater ID="rpt_Forms" runat="server">
                                    <HeaderTemplate>
                                        <table>
                                            <tr class="title">
                                                <th width="20%">
                                                    <asp:CheckBox ID="chkFormAll" runat="server" Text=" Form Name" OnCheckedChanged="chkFormAll_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </th>
                                                <th width="16%">
                                                    <asp:CheckBox ID="cbView" runat="server" Text=" View" OnCheckedChanged="cbView_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </th>
                                                <th width="16%">
                                                    <asp:CheckBox ID="cbSave" runat="server" Text=" Save" OnCheckedChanged="cbSave_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </th>
                                                <th width="16%">
                                                    <asp:CheckBox ID="cbUpdate" runat="server" Text=" Update" OnCheckedChanged="cbUpdate_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </th>
                                                <th width="16%">
                                                    <asp:CheckBox ID="cbDelete" runat="server" Text=" Delete" OnCheckedChanged="cbDelete_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </th>
                                                <th width="16%">
                                                    <asp:CheckBox ID="cbExport" runat="server" Text=" Export" OnCheckedChanged="cbExport_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkForm" runat="server" Text=' <%#Eval("FormName")%>' OnCheckedChanged="chkForm_CheckedChanged"
                                                    AutoPostBack="true" />
                                                <asp:HiddenField ID="hfFormId" runat="server" Value='<%#Eval("FormId")%>' />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbView" runat="server" />
                                                <asp:Label ID="lblView" runat="server" Text="View"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbSave" runat="server" />
                                                <asp:Label ID="lblSave" runat="server" Text="Save"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbUpdate" runat="server" />
                                                <asp:Label ID="lblUpdate" runat="server" Text="Update"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbDelete" runat="server" />
                                                <asp:Label ID="lblDelete" runat="server" Text="Delete"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbExport" runat="server" />
                                                <asp:Label ID="lblExport" runat="server" Text="Export"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="clearfix">
                            </div>
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
                                    User Role Name :</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txt_UserRoleSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
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
                        <i class="fa fa-th-large"></i><span>Total User Role(s) :</span>
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
                            <asp:Button ID="btn_add" runat="server" Text="Add User Role" OnClick="btn_add_Click"
                                CssClass="btn btn-primary btn-label-left" />
                            <asp:Button ID="btn_export" runat="server" Text="Export to Excel" OnClick="btn_export_Click"
                                CssClass="btn btn-warning btn-label-left" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="content">
                        <asp:GridView ID="grd_main" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="false" GridLines="None" OnPageIndexChanging="grd_main_PageIndexChanging"
                            EmptyDataText="No User Role(s) Found." Width="100%" RowStyle-Wrap="true" HeaderStyle-Wrap="false"
                            FooterStyle-Wrap="false" AlternatingRowStyle-Wrap="true" SelectedRowStyle-Wrap="false"
                            SortedAscendingHeaderStyle-Wrap="false" SortedDescendingHeaderStyle-Wrap="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span style="font-weight: bold">Actions</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_update" runat="server" ToolTip="Click here to Modify record"
                                            CommandArgument='<%#Eval("UserRoleId")%>' OnClick="btn_click"><img src="Content/img/icons/actions/edit.png" alt="" /></asp:LinkButton>
                                        <asp:LinkButton ID="lnk_delete" runat="server" ToolTip="Click here to Delete record"
                                            CommandArgument='<%#Eval("UserRoleId")%>' OnClick="btn_click" OnClientClick="javascript:return confirm('Are you sure want to delete this record?');"><img
                                            src="Content/img/icons/actions/delete.png" alt="" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="UserRoleName" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="User Role Name"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("User Role")%>
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
                                            User Role Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("User Role")%>
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
