<%@ Page Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="UserRole.aspx.cs" Inherits="RentTrack.UserRole" %>

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
        <div class="col-xs-12 col-sm-12">
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
                    <form id="defaultForm" method="post" class="form-horizontal">
                        <fieldset>
                            <div class="form-group" id="Messagesection" runat="server" visible="false">
                                <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    User Role Name :<span style="color: Red"> *</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_UserRoleName" runat="server" MaxLength="100" CssClass="form-control"
                                        ToolTip="User Role"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="req_txt_UserRoleName" runat="server" ControlToValidate="txt_UserRoleName"
                                        Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Active :</label>
                                <div class="col-sm-4">
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
                                <div class="col-sm-offset-3 col-sm-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-label-left"
                                        OnClick="btnSave_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-label-left" Visible="false"
                                        OnClick="btnDelete_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning btn-label-left" CausesValidation="false"
                                        OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning btn-label-left" CausesValidation="false"
                                        OnClick="btnBack_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                        </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>