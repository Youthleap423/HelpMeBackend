<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SiteLinks.Master" AutoEventWireup="true"
    CodeBehind="changepassword.aspx.cs" Inherits="RentTrack.Admin.changepassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Change Password</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Change Password</span>
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
                    <%--<h4 class="page-header">Change Password</h4>--%>
                    <form id="defaultForm" method="post" class="form-horizontal">
                    <fieldset>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="validate_group"
                            ShowMessageBox="false" ShowSummary="false" />
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                User Name :<span style="color: Red"> *</span></label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtUserName" runat="server" MaxLength="100" CssClass="form-control"
                                    ToolTip="User Name" ReadOnly="true"></asp:TextBox>
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
                                Confirm Password :<span style="color: Red"> *</span></label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="20"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Req_txtConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                    Display="Dynamic" ErrorMessage="Password is required." ValidationGroup="validate_group"
                                    CssClass="help-block"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cv_txtConfirmPassword" runat="server" ControlToCompare="txtUserPassword"
                                    ControlToValidate="txtConfirmPassword" Display="Dynamic" ErrorMessage="Password not match."
                                    ValidationGroup="validate_group" CssClass="help-block"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Change Password" CssClass="btn btn-primary btn-label-left"
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
    </div>
</asp:Content>
