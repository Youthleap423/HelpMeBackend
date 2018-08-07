<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="EmailSettings.aspx.cs" Inherits="HelpMe.EmailSettings" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--BEGIN CONTENT-->
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">Email Settings</div>
                        <div class="tools">
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="form-horizontal form-separated" role="form">
                            <div class="form-body">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div id="dvMsg" runat="server" visible="false" class="note note-danger">
                                            <h5 class="box-heading">Error</h5>
                                            <p>
                                                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputUser">User<span class="require">*</span></label>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputSMTP">SMTP Server</label>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtSMTPServer" CssClass="form-control" placeholder="SMTP Server" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputSMTPUserName">SMTP User Name</label>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtSMTPUserName" CssClass="form-control" placeholder="SMTP User Name" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputSMTPPassword">SMTP Password</label>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtSMTPpwd" CssClass="form-control" placeholder="***" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputSMTPPort">SMTP Port</label>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtSMTPport" CssClass="form-control" placeholder="SMTP Port" runat="server"></asp:TextBox>
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtSMTPport" FilterMode="ValidChars" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputFirstName"></label>
                                    <div class="col-md-7">
                                        <asp:CheckBox ID="chkSSL" runat="server" Text="Enable SSL" CssClass="checkbox-inline " />
                                    </div>
                                </div>
                                <%--<div class="form-group">
                                    <div class="col-md-offset-2">
                                        <asp:LinkButton ID="lnkTestMail" CssClass="btn btn-default" runat="server" OnClick="lnkTestMail_Click">Test Mail</asp:LinkButton>
                                    </div>
                                </div>--%>
                                <div class="form-actions">
                                    <div class="col-md-offset-2 col-md-10">
                                        <asp:LinkButton ID="lnkSubmit" CssClass="btn btn-primary" runat="server" ValidationGroup="validateSubmit" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkCancel" CssClass="btn btn-default" CausesValidation="false" runat="server" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
