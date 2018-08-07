<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="HelpMe.UserMaster" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--BEGIN CONTENT-->
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">User Setup</div>
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
                                    <label class="col-md-2 control-label" for="inputFirstName">User Name <span class="require">*</span></label>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtUserName" CssClass="form-control" placeholder="User Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtUserName" ControlToValidate="txtUserName" SetFocusOnError="true"
                                            runat="server" ErrorMessage="User Name required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputFirstName">Password <span class="require">*</span></label>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Password" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtPassword" ControlToValidate="txtPassword" SetFocusOnError="true"
                                            runat="server" ErrorMessage="Password required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputFirstName">Confirm Password <span class="require">*</span></label>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtCPassword" CssClass="form-control" TextMode="Password" placeholder="Confirm Password" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtCPassword" ControlToValidate="txtCPassword" SetFocusOnError="true"
                                            runat="server" ErrorMessage="Confirm Password required" class="require"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txtCPassword" ControlToCompare="txtPassword" Type="Integer" ErrorMessage="Password not Match" class="require"></asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="selCountry">User Role <span class="require">*</span></label>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlUserRole" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqddlUserRole" ControlToValidate="ddlUserRole" SetFocusOnError="true"
                                            runat="server" InitialValue="0" ErrorMessage="User Role required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="selGender"></label>
                                    <div class="col-md-7">
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
                                        <asp:LinkButton ID="lnkSubmit" CssClass="btn btn-primary" runat="server" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>&nbsp;
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
    <!--END CONTENT-->
</asp:Content>
