<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="GeneralSettings.aspx.cs" Inherits="HelpMe.GeneralSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">General Settings </div>
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
                                    <label class="col-md-2 control-label" for="inputCreditPost">App Mode</label>
                                    <div class="col-md-6">
                                        <asp:RadioButtonList ID="cblAppMode" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Testing" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Live" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputCreditPost">Credit Post</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtCreditPost" MaxLength="10" CssClass="form-control" placeholder="Credit Post" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <cc1:FilteredTextBoxExtender ID="FtxtCreditPost" runat="server" TargetControlID="txtCreditPost" FilterMode="ValidChars" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputCreditPoint">Share Application</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtshareapp" MaxLength="10" CssClass="form-control" placeholder="Share Application Point" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <cc1:FilteredTextBoxExtender ID="FtxtCreditPoint" runat="server" TargetControlID="txtshareapp" FilterMode="ValidChars" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputCreditPoint">Share Post</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtsharepost" MaxLength="10" CssClass="form-control" placeholder="Share Post" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtsharepost" FilterMode="ValidChars" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>

                                <div class="form-actions">
                                    <div class="col-md-offset-2 col-md-10">
                                        <asp:LinkButton ID="lnkSubmit" CssClass="btn btn-primary" ValidationGroup="validateSubmit" runat="server" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>&nbsp;
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
