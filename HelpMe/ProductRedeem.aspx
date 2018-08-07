<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ProductRedeem.aspx.cs" Inherits="HelpMe.ProductRedeem" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">Product Redeem Information</div>
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
                                    <label class="col-md-2 control-label" for="inputddlClient">Client Name <span class="require">*</span></label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtfirstname" ControlToValidate="ddlClient" SetFocusOnError="true" ValidationGroup="validateSubmit" Display="Dynamic"
                                            runat="server" ErrorMessage="Client Name required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputddlProduct">Product Name </label>
                                    <div class="col-md-6">
                                       <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                     <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlProduct" SetFocusOnError="true" ValidationGroup="validateSubmit" Display="Dynamic"
                                            runat="server" ErrorMessage="Product Name required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inpuRedeem">Redeem Point </label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtRedeemPoint" MaxLength="10" CssClass="form-control" placeholder="Redeem Point" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtRedeemPoint" FilterMode="ValidChars" ValidChars="0123456789">
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
