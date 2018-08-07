<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="HelpMe.Notification" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content">

        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">
                            <asp:Label ID="TitleCaption" runat="server" Text="Search"></asp:Label>
                        </div>
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
                                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="selType">Type</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlType" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Client" Selected="True" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="divClinet" class="form-group" visible="false" runat="server">
                                    <label class="col-md-2 control-label" for="selName">Client</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                             
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputMessage">Title <span class="require">*</span></label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtTitle"  CssClass="form-control" placeholder="Title" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="validatesubmit" ControlToValidate="txtTitle" SetFocusOnError="true" Display="Dynamic"
                                            runat="server" ErrorMessage="Title is required." class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputMessage">Message <span class="require">*</span></label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtMessage" TextMode="MultiLine" CssClass="form-control" placeholder="Message" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:RequiredFieldValidator ID="reqtxtMessage" ValidationGroup="validatesubmit" ControlToValidate="txtMessage" SetFocusOnError="true" Display="Dynamic"
                                            runat="server" ErrorMessage="Message is required." class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputKeyParameter">Key Parameter</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtKeyParameter1" CssClass="form-control" placeholder="Key Parameter1" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtKeyParameter2" CssClass="form-control" placeholder="Key Parameter2" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputKeyParameter"></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtKeyParameter3" CssClass="form-control" placeholder="Key Parameter3" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtKeyParameter4" CssClass="form-control" placeholder="Key Parameter4" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputKeyParameter"></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtKeyParameter5" CssClass="form-control" placeholder="Key Parameter5" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtKeyParameter6" CssClass="form-control" placeholder="Key Parameter6" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <div class="col-md-offset-2 col-md-10">
                                        <asp:LinkButton ID="lnkSubmit" CssClass="btn btn-primary" ValidationGroup="validatesubmit" runat="server" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>&nbsp;
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
