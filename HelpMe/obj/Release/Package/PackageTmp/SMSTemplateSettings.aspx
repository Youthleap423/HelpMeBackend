<%@ Page Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true"
    CodeBehind="SMSTemplateSettings.aspx.cs" Inherits="RentTrack.SMSTemplateSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>SMS Template</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>SMS Template</span>
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
                    <asp:UpdatePanel ID="uppanle" runat="server">
                        <ContentTemplate>
                            <div class="form-group" id="Messagesection" runat="server" visible="false">
                                <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validate_group"
                                ShowMessageBox="false" ShowSummary="false" />
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    * SMS Type</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlSMSTemplateType" runat="server" OnSelectedIndexChanged="ddlSMSTemplateType_SelectedIndexChanged"
                                        AutoPostBack="true" CssClass="form-control">
                                        <asp:ListItem Text="--Select SMS Template Type--" Value="0" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Text="Lease Signup / Lease Renewal" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Lease Signup - Send Feenote Reminder" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Lease Signup - Send Rent Payment Reminder" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Lease Signup - Send Rent Payment Delay" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Lease Termination" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Maintenance - Issue Allocation/Progress" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Schedule Visit" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Sales Activity" Value="8"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="Req_ddlSMSTemplateType" runat="server" ControlToValidate="ddlSMSTemplateType"
                                        CssClass="error-message" Display="Dynamic" ErrorMessage="SMS Template Type is required."
                                        ValidationGroup="validate_group" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    * Add Parameter</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlParameter" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:Button ID="btnAddParameter" runat="server" Text="Add Parameter" CssClass="btn btn-primary btn-label-left"
                                        OnClick="btnAddParameter_Click" CausesValidation="True" />
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-sm-12 control-label">
                                        *Note : Parameter added with @@ and @@ is representing values at the Booking Time.
                                    </label>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    * SMS Template</label>
                                <div class="col-sm-10">
                                    <CKEditor:CKEditorControl ID="txt_SMSTemplate" BasePath="~//ckeditor/" runat="server"></CKEditor:CKEditorControl>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>

                            <div class="clearfix">
                                &nbsp;
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-12 control-label">
                                </label>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2">
                                    &nbsp;
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save Settings" CssClass="btn btn-primary btn-label-left" OnClick="btnSubmit_Click"
                                        CausesValidation="True" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
