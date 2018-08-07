<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="HelpMe.Product" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">Product Information</div>
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
                                    <label class="col-md-2 control-label" for="inputProductName">Product Name <span class="require">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtProductName" Style="text-transform: capitalize;" CssClass="form-control" placeholder="Product Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtfirstname" ControlToValidate="txtProductName" SetFocusOnError="true" ValidationGroup="validateSubmit" Display="Dynamic"
                                            runat="server" ErrorMessage="Product Name required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtDescription">Description </label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtDescription" Style="text-transform: capitalize;" TextMode="MultiLine" CssClass="form-control" placeholder="Description" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtPhoneNo">Point </label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPoint" MaxLength="10" CssClass="form-control" placeholder="Point" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtPoint" FilterMode="ValidChars" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtDescription">Product Image </label>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblProductimage" runat="server" Visible="false"></asp:Label>
                                        <asp:FileUpload ID="fuproductimage" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Select Correct Image"
                                            ControlToValidate="fuproductimage" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$" SetFocusOnError="true" Display="Dynamic" class="require"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-3">
                                          <asp:Button ID="btnProductimage" runat="server" Text="Set Image" CssClass="btn btn-primary" OnClick="btnProductimage_Click" />
                                        <asp:Button ID="btnClearImage" runat="server" Text="Clear Image" CssClass="btn btn-primary" OnClick="btnClearImage_Click" Visible="false" />
                                    </div>
                                </div>
                                <div class="form-group" id="divshowproduct" runat="server" style="display: none">
                                    <div class="col-md-offset-2">
                                        <a id="aImage1" runat="server" target="_blank" href="#" data-lightbox="image-2" data-title="Image 2" class="mix-zoom">
                                            <asp:Image ID="imgProduct" runat="server" Style="height: 152px;" BorderColor="#CCCCFF" BorderStyle="Ridge" />
                                        </a>
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
