<%@ Page Language="C#" Title="Image Processing" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" Inherits="HelpMe.Administrator.Subscription" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">Subscription Information</div>
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
                                    <label class="col-md-2 control-label" for="inputFirstName">First Name</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtFirstName" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputLastName">Last Name</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtLastName" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputPackageName">Package Name</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPackageName" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="Package Name" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputCreditPost">Credit Post</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtCreditPost" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="Credit Post" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputCreditPoint">Credit Point</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtCreditPoint" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="Credit Point" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputPaymentAmount">Payment Amount</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPaymentAmount" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="Payment Amount" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputPaymentTime">Payment Time</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPaymentTime" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="Payment Time" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputPaymentId">Payment Id</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPaymentId" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="Payment Id" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputPaymentStatus">Payment Status</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPaymentStatus" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="Payment Status" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputPaymentResponse">Payment Response</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPaymentResponse" ReadOnly="true" Style="text-transform: capitalize;" MaxLength="500" CssClass="form-control" placeholder="Payment Response" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-actions">
                                    <div class="col-md-offset-2 col-md-10">
                                        <%--<asp:LinkButton ID="lnkSubmit" CssClass="btn btn-primary" ValidationGroup="validateSubmit" runat="server" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>&nbsp;--%>
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
