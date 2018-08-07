<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="LeaseTerminate.aspx.cs" Inherits="RentTrack.LeaseTerminate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Lease Terminate</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Lease Terminate</span>
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
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Lease Date : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLeaseSignUpDate" runat="server" Width="50%" MaxLength="10" CssClass="input_date form-control dateinput" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            From Date : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="50%" MaxLength="10" CssClass="input_date form-control dateinputset" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField ID="agreementdays" runat="server" ClientIDMode="Static" />
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            To Date : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="50%" MaxLength="10" CssClass="input_date form-control dateinput todateset" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
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
                                            <asp:TextBox ID="txtDueDate" runat="server" MaxLength="10" CssClass="input_date form-control" Text="0" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <h4><b><u>Charges</u></b></h4>
                                    <div class="form-group">
                                        <asp:Repeater ID="rptChargeData" runat="server">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%">No.
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 45%">Charge
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 30%">Mode
                                                        </td>
                                                        <td style="padding-right: 5px; text-align: right; width: 20%">Amount
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="repeaterrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 45%">
                                                        <asp:Label ID="lblCharge" Text='<%# Eval("ChargeName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfLeaseSignUp_ChargeId" Value='<%# Eval("LeaseSignUp_ChargeId") %>'
                                                            runat="server" />
                                                        <asp:HiddenField ID="hfChargeId" Value='<%# Eval("ChargeId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfChargeLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 30%">
                                                        <asp:Label ID="lblMode" runat="server" Text='<%# Eval("Mode") %>'></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 5px; text-align: right; width: 20%">
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="repeateralrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 45%">
                                                        <asp:Label ID="lblCharge" Text='<%# Eval("ChargeName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfLeaseSignUp_ChargeId" Value='<%# Eval("LeaseSignUp_ChargeId") %>'
                                                            runat="server" />
                                                        <asp:HiddenField ID="hfChargeId" Value='<%# Eval("ChargeId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfChargeLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 30%">
                                                        <asp:Label ID="lblMode" runat="server" Text='<%# Eval("Mode") %>'></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 5px; text-align: right; width: 20%">
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
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
                                        <asp:Repeater ID="rptTenantData" runat="server">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%"><b>No.</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 55%"><b>Tenant</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 40%"><b>Value</b>
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="repeaterrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 55%">
                                                        <asp:Label ID="lblTenantName" Text='<%# Eval("TenantName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyTenantId" Value='<%# Eval("PropertyTenantId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfTenantId" Value='<%# Eval("TenantId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyTenantLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 40%">
                                                        <asp:Label ID="lblTenantValue" runat="server" Text='<%# Eval("TenantValue") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="repeateralrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 55%">
                                                        <asp:Label ID="lblTenantName" Text='<%# Eval("TenantName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyTenantId" Value='<%# Eval("PropertyTenantId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfTenantId" Value='<%# Eval("TenantId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyTenantLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 40%">
                                                        <asp:Label ID="lblTenantValue" runat="server" Text='<%# Eval("TenantValue") %>'></asp:Label>
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
                                        <asp:Repeater ID="rptBrokerData" runat="server">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%"><b>No.</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 50%"><b>Date</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 45%"><b>Percentage</b>
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="repeaterrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 50%">
                                                        <asp:Label ID="lblBrokerDate" runat="server" Text='<%# Eval("BrokerDate") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyBrokerId" Value='<%# Eval("PropertyBrokerId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyBrokerLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 45%">
                                                        <asp:Label ID="lblBrokerPercentage" runat="server" Text='<%# Eval("BrokerPercentage") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="repeateralrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 50%">
                                                        <asp:Label ID="lblBrokerDate" runat="server" Text='<%# Eval("BrokerDate") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyBrokerId" Value='<%# Eval("PropertyBrokerId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyBrokerLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 45%">
                                                        <asp:Label ID="lblBrokerPercentage" runat="server" Text='<%# Eval("BrokerPercentage") %>'></asp:Label>
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
                                        <asp:Repeater ID="rptLawyerData" runat="server">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%"><b>No.</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 50%"><b>Date</b>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 45%"><b>Value</b>
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="repeaterrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 50%">
                                                        <asp:Label ID="lblLawyerDate" runat="server" Text='<%# Eval("LawyerDate") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyLawyerId" Value='<%# Eval("PropertyLawyerId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyLawyerLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 45%">
                                                        <asp:Label ID="lblLawyerPercentage" runat="server" Text='<%# Eval("LawyerPercentage") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="repeateralrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 50%">
                                                        <asp:Label ID="lblLawyerDate" runat="server" Text='<%# Eval("LawyerDate") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfPropertyLawyerId" Value='<%# Eval("PropertyLawyerId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyLawyerLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 45%">
                                                        <asp:Label ID="lblLawyerPercentage" runat="server" Text='<%# Eval("LawyerPercentage") %>'></asp:Label>
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
                                        <asp:Repeater ID="rptDocumentData" runat="server" OnItemCommand="rptDocumentData_ItemCommand">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%">No.
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 95%">Document
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="repeaterrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 95%">
                                                        <asp:LinkButton ID="lnkDocument" Text='<%# Eval("DocumentName") %>' CommandName="Download" runat="server" CausesValidation="false" CommandArgument='<%# Eval("DocumentName") %>' ToolTip="Download"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="repeateralrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 95%">
                                                        <asp:LinkButton ID="lnkDocument" Text='<%# Eval("DocumentName") %>' CommandName="Download" runat="server" CausesValidation="false" CommandArgument='<%# Eval("DocumentName") %>' ToolTip="Download"></asp:LinkButton>
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
                                            <asp:DropDownList ID="ddlSendFeeNote" runat="server" CssClass="form-control" ReadOnly="true">
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
                                            <asp:TextBox ID="txtSendFeeNote_Email" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            &nbsp;
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            Send Rent Reminder by Email : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="ddlSendRentRemindEmail" runat="server" CssClass="form-control" ReadOnly="true">
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
                                            <asp:TextBox ID="txt_SendRentRemindEmail" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            &nbsp;
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            Send Rent Reminder by SMS : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="ddlSendRentRemindSMS" runat="server" CssClass="form-control" ReadOnly="true">
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
                                            <asp:TextBox ID="txt_SendRentRemindSMS" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            &nbsp;
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            How many Reminders to be sent : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txt_ReminderCount" runat="server" MaxLength="10" Text="0" CssClass="input_date form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            How many days before due date : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txt_ReminderDueDays" runat="server" MaxLength="10" Text="0" CssClass="input_date form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            Send Notice After Rent Payment Delay by Email : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="ddlSendRentDelayEmail" runat="server" CssClass="form-control" ReadOnly="true">
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
                                            <asp:TextBox ID="txt_SendRentDelayEmail" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            &nbsp;
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            Send Notice After Rent Payment Delay by SMS : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="ddlSendRentDelaySMS" runat="server" CssClass="form-control" ReadOnly="true">
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
                                            <asp:TextBox ID="txt_SendRentDelaySMS" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 control-label">
                                            &nbsp;
                                        </label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            How many days after due date : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txt_DelayCount" runat="server" MaxLength="10" Text="0" CssClass="input_date form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">
                                            How many Notices to be sent : 
                                        </label>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txt_DelayDueDays" runat="server" MaxLength="10" Text="0" CssClass="input_date form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Comments</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <div class="col-sm-offset-2 col-sm-3">
                                    <asp:Button ID="btnSave" Text="Terminate" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnSave_Click" />
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
