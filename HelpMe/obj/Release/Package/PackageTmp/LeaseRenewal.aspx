<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="LeaseRenewal.aspx.cs" Inherits="RentTrack.LeaseRenewal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Lease Renewal</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Lease Renewal</span>
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
                    <form id="defaultForm" method="post" class="form-horizontal">
                        <fieldset>
                            <asp:UpdatePanel ID="uppanle" runat="server">
                                <ContentTemplate>
                                    <script type="text/javascript">
                                        Sys.Application.add_load(function () { BindDateEvents(); });
                                    </script>
                                    <div class="form-group" id="Messagesection" runat="server" visible="false">
                                        <div class="col-sm-12">
                                            <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Lease LR No : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLeaseSignUpNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField ID="currentOldLeaseSignUpId" runat="server" />
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqPropertyLrNo" runat="server" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ControlToValidate="txtLeaseSignUpNo"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Lease Date : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLeaseSignUpDate" runat="server" Width="50%" MaxLength="10" CssClass="input_date form-control dateinput"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqLeaseSignUpDate" runat="server" ControlToValidate="txtLeaseSignUpDate" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            From Date : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="50%" MaxLength="10" CssClass="input_date form-control dateinputset"></asp:TextBox>
                                            <asp:HiddenField ID="agreementdays" runat="server" ClientIDMode="Static" />
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqFromDate" runat="server" ControlToValidate="txtFromDate" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            To Date : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="50%" MaxLength="10" CssClass="input_date form-control dateinput todateset"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqToDate" runat="server" ControlToValidate="txtToDate" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Property :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtProperty" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                            <asp:HiddenField ID="hfPropertyId" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Payment due on : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txtDueDate" runat="server" MaxLength="10" CssClass="input_date form-control" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">(i.e. 1, 2, 3 ... 29, 30)</label>
                                            <asp:RequiredFieldValidator ID="reqDueDate" runat="server" ControlToValidate="txtDueDate" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="ftb_txtDueDate" runat="server" TargetControlID="txtDueDate" ValidChars="0123456789" FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RangeValidator ID="regDueDate" runat="server" ForeColor="Red"
                                                Display="Static" MinimumValue="1" MaximumValue="30" Type="Integer" ErrorMessage="Enter value from 1 to 30" ControlToValidate="txtDueDate"></asp:RangeValidator>
                                        </div>
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
                                            Charge Mode : <span style="color: Red">*</span></label>
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
                                            Charge Amount : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtAmount" MaxLength="15" runat="server" ValidationGroup="Charge" CssClass="form-control" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <cc1:FilteredTextBoxExtender ID="ftb_txtAmount" runat="server" TargetControlID="txtAmount" ValidChars="0123456789." FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
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
                                                        <asp:HiddenField ID="hfLeaseSignUp_ChargeId" Value='<%# Eval("LeaseSignUp_ChargeId") %>'
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
                                                        <asp:HiddenField ID="hfLeaseSignUp_ChargeId" Value='<%# Eval("LeaseSignUp_ChargeId") %>'
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
                                    <br />
                                    <h4><b><u>Tenant Details</u></b></h4>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Tenant Name : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlTenant" runat="server" ValidationGroup="Tenant" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqddlTenant" runat="server" ControlToValidate="ddlTenant" ForeColor="Red"
                                                Display="Static" InitialValue="0" ValidationGroup="Tenant" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Tenant % : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtTenantValue" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <cc1:FilteredTextBoxExtender ID="ftb_txtTenantValue" runat="server" TargetControlID="txtTenantValue" ValidChars="0123456789." FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="reqtxtTenantValue" runat="server" ControlToValidate="txtTenantValue" ForeColor="Red"
                                                Display="Static" InitialValue="0" ValidationGroup="Tenant" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:HiddenField ID="hfPropertyTenantLrNo" runat="server" Value="" />
                                            <asp:Button ID="btnAddTenant" Text="Add" ValidationGroup="Tenant" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddTenant_Click" />
                                            <asp:Button ID="btnClearTenant" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearTenant_Click" />
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group" id="MessagePropertyTenantSection" runat="server" visible="false">
                                        <div class="col-sm-12">
                                            <asp:Label ID="MessagePropertyTenant" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <asp:Repeater ID="rptTenantData" runat="server" OnItemCommand="rptTenantData_ItemCommand">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%"><b>No.</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 45%"><b>Tenant</b>
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
                                                        <asp:Label ID="lblTenantName" Text='<%# Eval("TenantName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyTenantId" Value='<%# Eval("PropertyTenantId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfTenantId" Value='<%# Eval("TenantId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyTenantLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 30%">
                                                        <asp:Label ID="lblTenantValue" runat="server" Text='<%# Eval("TenantValue") %>'></asp:Label>
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
                                                        <asp:Label ID="lblTenantName" Text='<%# Eval("TenantName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyTenantId" Value='<%# Eval("PropertyTenantId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfTenantId" Value='<%# Eval("TenantId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyTenantLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 30%">
                                                        <asp:Label ID="lblTenantValue" runat="server" Text='<%# Eval("TenantValue") %>'></asp:Label>
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
                                    <br />
                                    <div class="clearfix">
                                    </div>
                                    <h4><b><u>Broker Details</u></b></h4>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Broker :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlBroker" runat="server" ValidationGroup="Broker" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            Broker Payment :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtBrokerValue" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <cc1:FilteredTextBoxExtender ID="ftb_txtBrokerValue" runat="server" TargetControlID="txtBrokerValue" ValidChars="0123456789." FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="req_txtBrokerValue" runat="server" ControlToValidate="txtBrokerValue" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmptxtBrokerValue" runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ControlToValidate="txtBrokerValue"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Payment Date : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtBrokerDate" runat="server" Width="50%" MaxLength="10" CssClass="input_date form-control dateinput"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqLeaseBrokerDate" runat="server" ControlToValidate="txtBrokerDate" ForeColor="Red"
                                                Display="Static" ValidationGroup="Broker" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            Payment % : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtBrokerPercentage" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <cc1:FilteredTextBoxExtender ID="ftb_txtBrokerPercentage" runat="server" TargetControlID="txtBrokerPercentage" ValidChars="0123456789." FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="reqtxtBrokerPercentage" runat="server" ControlToValidate="txtBrokerPercentage" ForeColor="Red"
                                                Display="Static" ValidationGroup="Broker" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmptxtBrokerPercentage" runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ValidationGroup="Broker" ControlToValidate="txtBrokerPercentage"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:HiddenField ID="hfPropertyBrokerLrNo" runat="server" Value="" />
                                            <asp:Button ID="btnAddBroker" Text="Add" ValidationGroup="Broker" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddBroker_Click" />
                                            <asp:Button ID="btnClearBroker" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearBroker_Click" />
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group" id="MessagePropertyBrokerSection" runat="server" visible="false">
                                        <div class="col-sm-12">
                                            <asp:Label ID="MessagePropertyBroker" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <asp:Repeater ID="rptBrokerData" runat="server" OnItemCommand="rptBrokerData_ItemCommand">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%"><b>No.</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 40%"><b>Date</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 35%"><b>Percentage</b>
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
                                                    <td style="padding-left: 5px; text-align: left; width: 40%">
                                                        <asp:Label ID="lblBrokerDate" runat="server" Text='<%# Eval("BrokerDate") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyBrokerId" Value='<%# Eval("PropertyBrokerId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyBrokerLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 35%">
                                                        <asp:Label ID="lblBrokerPercentage" runat="server" Text='<%# Eval("BrokerPercentage") %>'></asp:Label>
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
                                                    <td style="padding-left: 5px; text-align: left; width: 40%">
                                                        <asp:Label ID="lblBrokerDate" runat="server" Text='<%# Eval("BrokerDate") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyBrokerId" Value='<%# Eval("PropertyBrokerId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyBrokerLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 35%">
                                                        <asp:Label ID="lblBrokerPercentage" runat="server" Text='<%# Eval("BrokerPercentage") %>'></asp:Label>
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
                                    <br />
                                    <div class="clearfix">
                                    </div>
                                    <h4><b><u>Lawyer Details</u></b></h4>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Lawyer :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlLawyer" runat="server" ValidationGroup="Lawyer" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            Lawyer Payment :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLawyerValue" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <cc1:FilteredTextBoxExtender ID="ftb_txtLawyerValue" runat="server" TargetControlID="txtLawyerValue" ValidChars="0123456789." FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="req_txtLawyerValue" runat="server" ControlToValidate="txtLawyerValue" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmptxtLawyerValue" runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ControlToValidate="txtLawyerValue"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Payment Date : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLawyerDate" runat="server" Width="50%" MaxLength="10" CssClass="input_date form-control dateinput"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqLeaseLawyerDate" runat="server" ControlToValidate="txtLawyerDate" ForeColor="Red"
                                                Display="Static" ValidationGroup="Lawyer" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            Payment % : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLawyerPercentage" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqtxtLawyerPercentage" runat="server" ControlToValidate="txtLawyerPercentage" ForeColor="Red"
                                                Display="Static" InitialValue="0" ValidationGroup="Lawyer" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="ftb_txtLawyerPercentage" runat="server" TargetControlID="txtLawyerPercentage" ValidChars="0123456789." FilterMode="ValidChars">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:CompareValidator ID="cmptxtLawyerPercentage" runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ValidationGroup="Lawyer" ControlToValidate="txtLawyerPercentage"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:HiddenField ID="hfPropertyLawyerLrNo" runat="server" Value="" />
                                            <asp:Button ID="btnAddLawyer" Text="Add" ValidationGroup="Lawyer" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddLawyer_Click" />
                                            <asp:Button ID="btnClearLawyer" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearLawyer_Click" />
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group" id="MessagePropertyLawyerSection" runat="server" visible="false">
                                        <div class="col-sm-12">
                                            <asp:Label ID="MessagePropertyLawyer" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <asp:Repeater ID="rptLawyerData" runat="server" OnItemCommand="rptLawyerData_ItemCommand">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%"><b>No.</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 40%"><b>Date</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 35%"><b>Value</b>
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
                                                    <td style="padding-left: 5px; text-align: left; width: 40%">
                                                        <asp:Label ID="lblLawyerDate" runat="server" Text='<%# Eval("LawyerDate") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyLawyerId" Value='<%# Eval("PropertyLawyerId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyLawyerLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 35%">
                                                        <asp:Label ID="lblLawyerPercentage" runat="server" Text='<%# Eval("LawyerPercentage") %>'></asp:Label>
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
                                                    <td style="padding-left: 5px; text-align: left; width: 40%">
                                                        <asp:Label ID="lblLawyerDate" runat="server" Text='<%# Eval("LawyerDate") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyLawyerId" Value='<%# Eval("PropertyLawyerId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyLawyerLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 35%">
                                                        <asp:Label ID="lblLawyerPercentage" runat="server" Text='<%# Eval("LawyerPercentage") %>'></asp:Label>
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
                                            Document : </label>
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
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            Send Fee Note by Email : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="ddlSendFeeNote" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSendFeeNote_SelectedIndexChanged">
                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="divSendFeeNote" runat="server" visible="false">
                                        <label class="col-sm-2 control-label">
                                            Receiver Email Id(s) :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSendFeeNote_Email" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            (i.e. user1@gmail.com; user2@gmail.com)
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            Send Rent Reminder by Email : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="ddlSendRentRemindEmail" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSendRentRemindEmail_SelectedIndexChanged">
                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="divSendRentRemindEmail" runat="server" visible="false">
                                        <label class="col-sm-2 control-label">
                                            Receiver Email Id(s) :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_SendRentRemindEmail" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            (i.e. user1@gmail.com; user2@gmail.com)
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            Send Rent Reminder by SMS : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="ddlSendRentRemindSMS" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSendRentRemindSMS_SelectedIndexChanged">
                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="divSendRentRemindSMS" runat="server" visible="false">
                                        <label class="col-sm-2 control-label">
                                            Receiver Contact No(s) :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_SendRentRemindSMS" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            (i.e. 701701701; 721721721)
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            How many Reminders to be sent : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txt_ReminderCount" runat="server" MaxLength="10" Text="0" CssClass="input_date form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:RequiredFieldValidator ID="req_ReminderCount" runat="server" ControlToValidate="txt_ReminderCount" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="range_ReminderCount" runat="server" ForeColor="Red"
                                                Display="Static" MinimumValue="0" MaximumValue="15" Type="Integer" ErrorMessage="Enter value from 0 to 15" ControlToValidate="txt_ReminderCount"></asp:RangeValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            How many days before due date : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txt_ReminderDueDays" runat="server" MaxLength="10" Text="0" CssClass="input_date form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:RequiredFieldValidator ID="req_ReminderDueDays" runat="server" ControlToValidate="txt_ReminderDueDays" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="range_ReminderDueDays" runat="server" ForeColor="Red"
                                                Display="Static" MinimumValue="0" MaximumValue="15" Type="Integer" ErrorMessage="Enter value from 0 to 15" ControlToValidate="txt_ReminderDueDays"></asp:RangeValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            Send Notice After Rent Payment Delay by Email : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="ddlSendRentDelayEmail" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSendRentDelayEmail_SelectedIndexChanged">
                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="divSendRentDelayEmail" runat="server" visible="false">
                                        <label class="col-sm-2 control-label">
                                            Receiver Email Id(s) :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_SendRentDelayEmail" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            (i.e. user1@gmail.com; user2@gmail.com)
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            Send Notice After Rent Payment Delay by SMS : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="ddlSendRentDelaySMS" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSendRentDelaySMS_SelectedIndexChanged">
                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="divSendRentDelaySMS" runat="server" visible="false">
                                        <label class="col-sm-2 control-label">
                                            Receiver Contact No(s) :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_SendRentDelaySMS" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            (i.e. 701701701; 721721721)
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            How many days after due date : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txt_DelayCount" runat="server" MaxLength="10" Text="0" CssClass="input_date form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:RequiredFieldValidator ID="req_DelayCount" runat="server" ControlToValidate="txt_DelayCount" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="range_DelayCount" runat="server" ForeColor="Red"
                                                Display="Static" MinimumValue="0" MaximumValue="15" Type="Integer" ErrorMessage="Enter value from 0 to 15" ControlToValidate="txt_DelayCount"></asp:RangeValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            How many Notices to be sent : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txt_DelayDueDays" runat="server" MaxLength="10" Text="0" CssClass="input_date form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:RequiredFieldValidator ID="req_DelayDueDays" runat="server" ControlToValidate="txt_DelayDueDays" ForeColor="Red"
                                                Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="range_DelayDueDays" runat="server" ForeColor="Red"
                                                Display="Static" MinimumValue="0" MaximumValue="15" Type="Integer" ErrorMessage="Enter value from 0 to 15" ControlToValidate="txt_DelayDueDays"></asp:RangeValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Comments</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnAddDocument" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <div class="col-sm-offset-2 col-sm-3">
                                    <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnCancel_Click" CausesValidation="false" />
                                    <asp:Button ID="btnBack" Text="Back" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnBack_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                        </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
