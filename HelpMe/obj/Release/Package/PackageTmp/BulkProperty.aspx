<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="BulkProperty.aspx.cs" Inherits="RentTrack.BulkProperty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Property</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Property</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    No. of Property : <span style="color: Red">*</span>
                                </label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control" Text="1" Width="100px"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="req_Number" runat="server" ForeColor="Red"
                                        Display="Static" ErrorMessage="***" ControlToValidate="txtNumber"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">&nbsp;</label>
                                <label class=" control-label"><span style="color: Red">*Note : </span>insert No. of Property you want to configure for same configuration. </label>
                            </div>
                            <div class="clearfix">
                            </div>

                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Property LR No : <span style="color: Red">*</span>
                                </label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPropertyLrNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqPropertyLrNo" runat="server" ForeColor="Red"
                                        Display="Static" ErrorMessage="***" ControlToValidate="txtPropertyLrNo"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Property Type : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqType" runat="server" ControlToValidate="ddlType" ForeColor="Red"
                                        Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Posted By :</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlPostedby" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPostedby_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="--Select Posted By--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Landlord" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Broker" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Employee" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqPostedby" runat="server" ControlToValidate="ddlPostedby" ForeColor="Red"
                                        Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Profile Name :</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlProfile" runat="server" CssClass="form-control" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqProfile" runat="server" ControlToValidate="ddlProfile" ForeColor="Red"
                                        Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Property for :</span><span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlPropertyFor" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPropertyFor_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="For Rent" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="For Sale" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="req_ddlPropertyFor" runat="server" ControlToValidate="ddlPropertyFor" ForeColor="Red"
                                        Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group" id="divPropertySale" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Sales Person :</span><span style="color: Red">*</span></label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSalesPerson" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Property Value : 
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPropertyValue" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1">
                                        <cc1:FilteredTextBoxExtender runat="server" ID="ftb_txtPropertyValue" TargetControlID="txtPropertyValue"
                                            ValidChars="0123456789." FilterMode="ValidChars">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <br />
                            <h4><b><u>Location Details</u></b></h4>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Location Type : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlLocationType" runat="server" ValidationGroup="LocationType" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlLocationType" runat="server" ControlToValidate="ddlLocationType" ForeColor="Red"
                                        Display="Static" InitialValue="0" ValidationGroup="LocationType" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Value : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtLocationValue" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqtxtLocationValue" runat="server" ControlToValidate="txtLocationValue" ForeColor="Red"
                                        Display="Static" ValidationGroup="LocationType" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:HiddenField ID="hfPropertyLocationTypeLrNo" runat="server" Value="" />
                                    <asp:Button ID="btnAddLocationType" Text="Add" ValidationGroup="LocationType" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddLocationType_Click" />
                                    <asp:Button ID="btnClearLocationType" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearLocationType_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group" id="MessagePropertyLocationTypeSection" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <asp:Label ID="MessagePropertyLocationType" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <asp:Repeater ID="rptPropertyLocationTypeData" runat="server" OnItemCommand="rptPropertyLocationTypeData_ItemCommand">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                            <tr class="repeaterheader">
                                                <td style="text-align: center; width: 5%"><b>No.</b>
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 45%"><b>Location Type</b>
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 30%"><b>Value</b>
                                                </td>
                                                <td style="text-align: center; width: 10%"><b>Edit</b>
                                                </td>
                                                <td style="text-align: center; width: 10%"><b>Delete</b>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="repeaterrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 45%">
                                                <asp:Label ID="lblLocationTypeName" Text='<%# Eval("LocationTypeName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyLocationTypeId" Value='<%# Eval("PropertyLocationTypeId") %>' runat="server" />
                                                <asp:HiddenField ID="hfLocationTypeId" Value='<%# Eval("LocationTypeId") %>' runat="server" />
                                                <asp:HiddenField ID="hfPropertyLocationTypeLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 30%">
                                                <asp:Label ID="lblLocationValue" runat="server" Text='<%# Eval("LocationValue") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="repeateralrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 45%">
                                                <asp:Label ID="lblLocationTypeName" Text='<%# Eval("LocationTypeName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyLocationTypeId" Value='<%# Eval("PropertyLocationTypeId") %>' runat="server" />
                                                <asp:HiddenField ID="hfLocationTypeId" Value='<%# Eval("LocationTypeId") %>' runat="server" />
                                                <asp:HiddenField ID="hfPropertyLocationTypeLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 30%">
                                                <asp:Label ID="lblLocationValue" runat="server" Text='<%# Eval("LocationValue") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="clearfix">
                            </div>
                            <br />
                            <h4><b><u>Building Parameter Details</u></b></h4>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Building Parameter : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlBuildingParameter" runat="server" ValidationGroup="BuildingParameter" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlBuildingParameter" runat="server" ControlToValidate="ddlBuildingParameter" ForeColor="Red"
                                        Display="Static" InitialValue="0" ValidationGroup="BuildingParameter" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Value : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtBuildingParameterValue" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqtxtBuildingParameterValue" runat="server" ControlToValidate="txtBuildingParameterValue" ForeColor="Red"
                                        Display="Static" ValidationGroup="BuildingParameter" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:HiddenField ID="hfPropertyBuildingParameterLrNo" runat="server" Value="" />
                                    <asp:Button ID="btnAddBuildingParameter" Text="Add" ValidationGroup="BuildingParameter" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddBuildingParameter_Click" />
                                    <asp:Button ID="btnClearBuildingParameter" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearBuildingParameter_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group" id="MessagePropertyBuildingParameterSection" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <asp:Label ID="MessagePropertyBuildingParameter" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <asp:Repeater ID="rptPropertyBuildingParameterData" runat="server" OnItemCommand="rptPropertyBuildingParameterData_ItemCommand">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                            <tr class="repeaterheader">
                                                <td style="text-align: center; width: 5%"><b>No.</b>
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 45%"><b>Building Parameter</b>
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 30%"><b>Value</b>
                                                </td>
                                                <td style="text-align: center; width: 10%"><b>Edit</b>
                                                </td>
                                                <td style="text-align: center; width: 10%"><b>Delete</b>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="repeaterrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 45%">
                                                <asp:Label ID="lblBuildingParameterName" Text='<%# Eval("BuildingParameterName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyBuildingParameterId" Value='<%# Eval("PropertyBuildingParameterId") %>' runat="server" />
                                                <asp:HiddenField ID="hfBuildingParameterId" Value='<%# Eval("BuildingParameterId") %>' runat="server" />
                                                <asp:HiddenField ID="hfPropertyBuildingParameterLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 30%">
                                                <asp:Label ID="lblBuildingParameterValue" runat="server" Text='<%# Eval("BuildingParameterValue") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="repeateralrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 45%">
                                                <asp:Label ID="lblBuildingParameterName" Text='<%# Eval("BuildingParameterName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyBuildingParameterId" Value='<%# Eval("PropertyBuildingParameterId") %>' runat="server" />
                                                <asp:HiddenField ID="hfBuildingParameterId" Value='<%# Eval("BuildingParameterId") %>' runat="server" />
                                                <asp:HiddenField ID="hfPropertyBuildingParameterLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 30%">
                                                <asp:Label ID="lblBuildingParameterValue" runat="server" Text='<%# Eval("BuildingParameterValue") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="clearfix">
                            </div>
                            <br />
                            <h4><b><u>Feature Details</u></b></h4>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Feature : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlFeature" runat="server" ValidationGroup="Feature" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlFeature" runat="server" ControlToValidate="ddlFeature" ForeColor="Red"
                                        Display="Static" InitialValue="0" ValidationGroup="Feature" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Value : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtFeatureValue" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqtxtFeatureValue" runat="server" ControlToValidate="txtFeatureValue" ForeColor="Red"
                                        Display="Static" ValidationGroup="Feature" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:HiddenField ID="hfPropertyFeatureLrNo" runat="server" Value="" />
                                    <asp:Button ID="btnAddFeature" Text="Add" ValidationGroup="Feature" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddFeature_Click" />
                                    <asp:Button ID="btnClearFeature" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearFeature_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group" id="MessagePropertyFeatureSection" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <asp:Label ID="MessagePropertyFeature" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <asp:Repeater ID="rptPropertyFeatureData" runat="server" OnItemCommand="rptPropertyFeatureData_ItemCommand">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                            <tr class="repeaterheader">
                                                <td style="text-align: center; width: 5%"><b>No.</b>
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 45%"><b>Feature</b>
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 30%"><b>Value</b>
                                                </td>
                                                <td style="text-align: center; width: 10%"><b>Edit</b>
                                                </td>
                                                <td style="text-align: center; width: 10%"><b>Delete</b>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="repeaterrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 45%">
                                                <asp:Label ID="lblFeatureName" Text='<%# Eval("FeatureName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyFeatureId" Value='<%# Eval("PropertyFeatureId") %>' runat="server" />
                                                <asp:HiddenField ID="hfFeatureId" Value='<%# Eval("FeatureId") %>' runat="server" />
                                                <asp:HiddenField ID="hfPropertyFeatureLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 30%">
                                                <asp:Label ID="lblFeatureValue" runat="server" Text='<%# Eval("FeatureValue") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="repeateralrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 45%">
                                                <asp:Label ID="lblFeatureName" Text='<%# Eval("FeatureName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyFeatureId" Value='<%# Eval("PropertyFeatureId") %>' runat="server" />
                                                <asp:HiddenField ID="hfFeatureId" Value='<%# Eval("FeatureId") %>' runat="server" />
                                                <asp:HiddenField ID="hfPropertyFeatureLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 30%">
                                                <asp:Label ID="lblFeatureValue" runat="server" Text='<%# Eval("FeatureValue") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="clearfix">
                            </div>
                            <br />
                            <h4><b><u>Amenities</u></b></h4>
                            <div class="form-group" id="MessageAmenitysection" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <asp:Label ID="MessageAmenity" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Amenity : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlAmenity" runat="server" ValidationGroup="Amenity" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqAmenity" runat="server" ControlToValidate="ddlAmenity" ForeColor="Red"
                                        Display="Static" InitialValue="0" ValidationGroup="Amenity" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:HiddenField ID="hfAmenityLrNo" runat="server" Value="" />
                                    <asp:Button ID="btnAddAmenity" Text="Add" ValidationGroup="Amenity" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddAmenity_Click" />
                                    <asp:Button ID="btnClearAmenity" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearAmenity_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <asp:Repeater ID="rptAmenityData" runat="server" OnItemCommand="rptAmenityData_ItemCommand">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                            <tr class="repeaterheader">
                                                <td style="text-align: center; width: 5%">No.
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 45%">Amenity
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 30%">Type
                                                </td>
                                                <td style="text-align: center; width: 10%">Edit
                                                </td>
                                                <td style="text-align: center; width: 10%">Delete
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="repeaterrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 45%">
                                                <asp:Label ID="lblAmenity" Text='<%# Eval("AmenityName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyAmenityId" Value='<%# Eval("PropertyAmenityId") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hfAmenityId" Value='<%# Eval("AmenityId") %>' runat="server" />
                                                <asp:HiddenField ID="hfAmenityLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 30%">
                                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                <asp:HiddenField ID="hfTypeValue" Value='<%# Eval("TypeValue") %>' runat="server" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="repeateralrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 45%">
                                                <asp:Label ID="lblAmenity" Text='<%# Eval("AmenityName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyAmenityId" Value='<%# Eval("PropertyAmenityId") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hfAmenityId" Value='<%# Eval("AmenityId") %>' runat="server" />
                                                <asp:HiddenField ID="hfAmenityLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 30%">
                                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                <asp:HiddenField ID="hfTypeValue" Value='<%# Eval("TypeValue") %>' runat="server" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="clearfix">
                            </div>
                            <h4><b><u>Charges</u></b></h4>
                            <div class="form-group" id="MessageChargesection" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <asp:Label ID="MessageCharge" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Charge Name : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlCharge" runat="server" ValidationGroup="Charge" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqCharge" runat="server" ControlToValidate="ddlCharge" ForeColor="Red"
                                        Display="Static" InitialValue="0" ValidationGroup="Charge" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Mode : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlChargeMode" runat="server" ValidationGroup="Charge" CssClass="form-control">
                                        <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Once" Value="Once"></asp:ListItem>
                                        <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqChargeMode" runat="server" ControlToValidate="ddlChargeMode" ForeColor="Red"
                                        Display="Static" InitialValue="0" ValidationGroup="Charge" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Amount : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtAmount" MaxLength="15" runat="server" ValidationGroup="Charge" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqAmount" runat="server" ForeColor="Red"
                                        Display="Static" ErrorMessage="***" ValidationGroup="Charge" ControlToValidate="txtAmount"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="comAmount" runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"
                                        Display="Static" ErrorMessage="***" ValidationGroup="Charge" ControlToValidate="txtAmount"></asp:CompareValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:HiddenField ID="hfChargeLrNo" runat="server" Value="" />
                                    <asp:Button ID="btnAddCharge" Text="Add" ValidationGroup="Charge" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddCharge_Click" />
                                    <asp:Button ID="btnClearCharge" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearCharge_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <asp:Repeater ID="rptChargeData" runat="server" OnItemCommand="rptChargeData_ItemCommand">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                            <tr class="repeaterheader">
                                                <td style="text-align: center; width: 5%">No.
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 35%">Charge
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 20%">Mode
                                                </td>
                                                <td style="padding-right: 5px; text-align: right; width: 20%">Amount
                                                </td>
                                                <td style="text-align: center; width: 10%">Edit
                                                </td>
                                                <td style="text-align: center; width: 10%">Delete
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="repeaterrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 35%">
                                                <asp:Label ID="lblCharge" Text='<%# Eval("ChargeName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyChargeId" Value='<%# Eval("PropertyChargeId") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hfChargeId" Value='<%# Eval("ChargeId") %>' runat="server" />
                                                <asp:HiddenField ID="hfChargeLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 20%">
                                                <asp:Label ID="lblMode" runat="server" Text='<%# Eval("Mode") %>'></asp:Label>
                                            </td>
                                            <td style="padding-right: 5px; text-align: right; width: 20%">
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="repeateralrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 35%">
                                                <asp:Label ID="lblCharge" Text='<%# Eval("ChargeName") %>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPropertyChargeId" Value='<%# Eval("PropertyChargeId") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hfChargeId" Value='<%# Eval("ChargeId") %>' runat="server" />
                                                <asp:HiddenField ID="hfChargeLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 20%">
                                                <asp:Label ID="lblMode" runat="server" Text='<%# Eval("Mode") %>'></asp:Label>
                                            </td>
                                            <td style="padding-right: 5px; text-align: right; width: 20%">
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="clearfix">
                            </div>
                            <h4><b><u>Documents</u></b></h4>
                            <div class="form-group" id="MessageDocumentsection" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <asp:Label ID="MessageDocument" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Document :
                                </label>
                                <div class="col-sm-3">
                                    <asp:FileUpload ID="txtDocument" runat="server" ValidationGroup="Document" CssClass="form-control"></asp:FileUpload>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqDocument" runat="server" ForeColor="Red"
                                        Display="Static" ErrorMessage="***" ValidationGroup="Document" ControlToValidate="txtDocument"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="btnAddDocument" Text="Upload" ValidationGroup="Document" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddDocument_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <asp:Repeater ID="rptDocumentData" runat="server" OnItemCommand="rptDocumentData_ItemCommand">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                            <tr class="repeaterheader">
                                                <td style="text-align: center; width: 5%">No.
                                                </td>
                                                <td style="padding-left: 5px; text-align: left; width: 85%">Document
                                                </td>
                                                <td style="text-align: center; width: 10%">Delete
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="repeaterrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 85%">
                                                <asp:LinkButton ID="lnkDocument" Text='<%# Eval("DocumentName") %>' CommandName="Download" runat="server" CausesValidation="false" CommandArgument='<%# Eval("DocumentName") %>' ToolTip="Download"></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("DocumentName") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="repeateralrow">
                                            <td style="text-align: center; width: 5%">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="padding-left: 5px; text-align: left; width: 85%">
                                                <asp:LinkButton ID="lnkDocument" Text='<%# Eval("DocumentName") %>' CommandName="Download" runat="server" CausesValidation="false" CommandArgument='<%# Eval("DocumentName") %>' ToolTip="Download"></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center; width: 10%">
                                                <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                    CommandArgument='<%# Eval("DocumentName") %>' CausesValidation="false" runat="server"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAddDocument" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-3">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-label-left"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-label-left" Visible="false"
                                OnClick="btnDelete_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning btn-label-left" CausesValidation="false"
                                OnClick="btnCancel_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning btn-label-left" CausesValidation="false"
                                OnClick="btnBack_Click" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
