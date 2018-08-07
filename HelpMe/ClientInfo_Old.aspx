<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ClientInfo_Old.aspx.cs" Inherits="HelpMe.ClientInfo_Old" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">Client Information</div>
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
                                    <label class="col-md-2 control-label" for="inputFirstName">First Name <span class="require">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtfirstname" Style="text-transform: capitalize;" CssClass="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtfirstname" ControlToValidate="txtfirstname" SetFocusOnError="true" ValidationGroup="validateSubmit"
                                            runat="server" ErrorMessage="First Name required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputLastName">Last Name <span class="require">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtlastname" Style="text-transform: capitalize;" CssClass="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtlastname" ControlToValidate="txtlastname" SetFocusOnError="true" ValidationGroup="validateSubmit"
                                            runat="server" ErrorMessage="Last Name required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtAddress1">Address1 </label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtAddress1" Style="text-transform: capitalize;" TextMode="MultiLine" CssClass="form-control" placeholder="Address1" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtAddress2">Address2 </label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtAddress2" Style="text-transform: capitalize;" TextMode="MultiLine" CssClass="form-control" placeholder="Address2" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtCountry">Country </label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtState">State </label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtCity">City </label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtPOBox">PO Box </label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPOBox" MaxLength="6" CssClass="form-control" placeholder="PO Box No" runat="server"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPOBox" FilterMode="ValidChars" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtPhoneNo">Phone No </label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPhoneNo" MaxLength="10" CssClass="form-control" placeholder="Mobile No" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <%--<asp:RequiredFieldValidator ID="reqMobile" ControlToValidate="txtPhoneNo" SetFocusOnError="true" ValidationGroup="validateSubmit"
                                            runat="server" ErrorMessage="Mobile required" class="require"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhoneNo" SetFocusOnError="true" ErrorMessage="Enter Valid Mobile No" class="require" ValidationGroup="validateSubmit"
                                            ValidationExpression="[7-9][0-9]{9}$"></asp:RegularExpressionValidator>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtPhoneNo" FilterMode="ValidChars" ValidChars="0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtEmailId">Email Id<span class="require">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtEmailId" CssClass="form-control" placeholder="Email Id" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtEmailId" ControlToValidate="txtEmailId" SetFocusOnError="true" ValidationGroup="validateSubmit"
                                            runat="server" ErrorMessage="Email Id required" class="require"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" SetFocusOnError="true" ValidationGroup="validateSubmit" ErrorMessage="Enter Valid EmailId" runat="server" Display="Dynamic" class="require" ControlToValidate="txtEmailId" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmailId" SetFocusOnError="true" ValidationGroup="validateSubmit" Display="Dynamic"
                                            runat="server" ErrorMessage="Email Id required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtPassword">Password<span class="require">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtpassword" CssClass="form-control" placeholder="Password" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtpassword" SetFocusOnError="true" ValidationGroup="validateSubmit"
                                            runat="server" ErrorMessage="Password required" class="require"></asp:RequiredFieldValidator>
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
                               <%-- <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtusername">User Name<span class="require">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtusername" Style="text-transform: capitalize;" CssClass="form-control" placeholder="User Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtusername" SetFocusOnError="true" ValidationGroup="validateSubmit"
                                            runat="server" ErrorMessage="User Name required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>--%>
                               
                               <%--  <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtuProfilePic">ProfilePic<span class="require">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtProfilePic" Style="text-transform: capitalize;" CssClass="form-control" placeholder="User Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtProfilePic" SetFocusOnError="true" ValidationGroup="validateSubmit"
                                            runat="server" ErrorMessage="User Profile Pic required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                               <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputLatitude">Latitude</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtLatitude"  CssClass="form-control" placeholder="Latitude" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                      
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputLongitude">Longitude</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtLongitude"  CssClass="form-control" placeholder="Longitude" runat="server"></asp:TextBox>
                                    </div>
                                   
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputAltitude">Altitude</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtAltitude"  CssClass="form-control" placeholder="Altitude" runat="server"></asp:TextBox>
                                    </div>
                                </div>
--%>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputtxtGender">Gender</label>
                                    <div class="col-md-6">
                                        <asp:RadioButtonList ID="rdogender" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Value="1">Male</asp:ListItem>
                                            <asp:ListItem Value="2">FeMale</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="selIsClientProfile"></label>
                                    <div class="col-md-6">
                                        <div class="checkbox-list">
                                            <label class="checkbox-inline">
                                                <asp:CheckBox ID="ChkIsClientProfile" runat="server" Checked="true" />
                                                &nbsp;Is ClientProfile
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="selGender"></label>
                                    <div class="col-md-6">
                                        <div class="checkbox-list">
                                            <label class="checkbox-inline">
                                                <asp:CheckBox ID="ChkIsActive" runat="server" Checked="true" />
                                                &nbsp;Is Active  
                                            </label>
                                        </div>
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
