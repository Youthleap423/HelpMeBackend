<%@ Page Language="C#" MasterPageFile="~/Admin/SiteLinks.Master" AutoEventWireup="true"
    CodeBehind="EmailSettings.aspx.cs" Inherits="RentTrack.Admin.EmailSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Email Settings</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Email Settings</span>
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
                    <%--<h4 class="page-header">Email Settings</h4>--%>
                    <form id="defaultForm" method="post" class="form-horizontal">
                    <fieldset>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validate_group"
                            ShowMessageBox="false" ShowSummary="false" />
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                SMTP User Name :<span style="color: Red"> *</span></label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSMTPUserName" runat="server" MaxLength="100" CssClass="form-control"
                                    ToolTip="Email Id"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Req_txtSMTPUserName" runat="server" ControlToValidate="txtSMTPUserName"
                                    Display="Dynamic" ErrorMessage="User Name is required." ValidationGroup="validate_group"
                                    CssClass="has-error"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                SMTP Password :<span style="color: Red"> *</span></label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSMTPPassword" runat="server" TextMode="Password" MaxLength="20"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="req_txtSMTPPassword" runat="server" ControlToValidate="txtSMTPPassword"
                                    Display="Dynamic" ErrorMessage="Password is required." ValidationGroup="validate_group"
                                    CssClass="help-block"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                SMTP Server Name :<span style="color: Red"> *</span></label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSMTPServer" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Req_txtSMTPServer" runat="server" ControlToValidate="txtSMTPServer"
                                    Display="Dynamic" ErrorMessage="SMTP Server Name is required." ValidationGroup="validate_group"
                                    CssClass="help-block"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                SMTP Port No. :<span style="color: Red"> *</span></label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSMTPPort" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Req_txtSMTPPort" runat="server" ControlToValidate="txtSMTPPort"
                                    Display="Dynamic" ErrorMessage="SMTP Port No. is required." ValidationGroup="validate_group"
                                    CssClass="help-block"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Enable SSL : </span>
                            </label>
                            <div class="col-sm-3">
                                <asp:CheckBox ID="cbEnableSSL" runat="server" />
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Save Settings" CssClass="btn btn-primary btn-label-left"
                                    OnClick="btnSubmit_Click" ValidationGroup="validate_group" CausesValidation="True" />
                                <asp:Button ID="btnTestEmail" runat="server" Text="Test Mail" CssClass="btn btn-warning btn-label-left"
                                    OnClick="btnTestEmail_Click" ValidationGroup="validate_group" CausesValidation="True" />
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
