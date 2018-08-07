<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Package.aspx.cs" Inherits="HelpMe.Package" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">Package Information</div>
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
                                    <label class="col-md-2 control-label" for="inputPackageName">Package Name <span class="require">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPackageName" Style="text-transform: capitalize;" CssClass="form-control" placeholder="Package Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtPackageName" ControlToValidate="txtPackageName" SetFocusOnError="true" ValidationGroup="validateSubmit" Display="Dynamic"
                                            runat="server" ErrorMessage="Package Name required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtDescription">Description </label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtDescription" Style="text-transform: capitalize;" TextMode="MultiLine" CssClass="form-control" placeholder="Description" runat="server"></asp:TextBox>
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
                                    <label class="col-md-2 control-label" for="inputCreditPoint">Credit Point</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtCreditPoint" MaxLength="10" CssClass="form-control" placeholder="Credit Point" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <cc1:FilteredTextBoxExtender ID="FtxtCreditPoint" runat="server" TargetControlID="txtCreditPoint" FilterMode="ValidChars" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputAmount">Amount</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtAmount" MaxLength="10" CssClass="form-control" placeholder="Amount" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <cc1:FilteredTextBoxExtender ID="FtxtAmount" runat="server" TargetControlID="txtAmount" FilterMode="ValidChars" ValidChars="0123456789.">
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
