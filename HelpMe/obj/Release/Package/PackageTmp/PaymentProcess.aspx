<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="PaymentProcess.aspx.cs" Inherits="RentTrack.PaymentProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Lease Payment Process</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Lease Payment Process</span>
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
                                    <asp:HiddenField ID="hfLeasePaymentId" runat="server" />
                                    <asp:HiddenField ID="hfPropertyId" runat="server" />
                                    <asp:HiddenField ID="hfProfileId" runat="server" />
                                    <asp:HiddenField ID="hfLeaseSignUpId" runat="server" />
                                    <asp:HiddenField ID="hfPaymentStatus" runat="server" />
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Tenant : </label>
                                        <div class="col-sm-3">
                                            <literal id="tenantname" runat="server"></literal>
                                        </div>
                                        <div class="col-sm-1">&nbsp;</div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Property No. : </label>
                                        <div class="col-sm-3">
                                            <literal id="propertyname" runat="server"></literal>
                                        </div>
                                        <div class="col-sm-1">&nbsp;</div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Due Date : </label>
                                        <div class="col-sm-3">
                                            <literal id="duedatetext" runat="server"></literal>
                                        </div>
                                        <div class="col-sm-1">&nbsp;</div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Lease SignUp No. : </label>
                                        <div class="col-sm-3">
                                            <literal id="leaseSignUpNo" runat="server"></literal>
                                        </div>
                                        <div class="col-sm-1">&nbsp;</div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Received Date : </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtReceiveDate" runat="server" Width="70%" CssClass="form-control dateinput"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqFromDate" runat="server" ControlToValidate="txtReceiveDate"
                                                ForeColor="Red" Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Comments : </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <h2>Charges</h2>
                                    <div id="changecharges" runat="server">
                                        <div class="form-group" id="MessageChargesection" runat="server" visible="false">
                                            <div class="col-sm-12">
                                                <asp:Label ID="MessageCharge" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Charge : </label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCharge" runat="server" ValidationGroup="Charge" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:RequiredFieldValidator ID="reqCharge" runat="server" ControlToValidate="ddlCharge"
                                                    ForeColor="Red" Display="Static" InitialValue="0" ValidationGroup="Charge" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Amount : </label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtAmount" MaxLength="15" runat="server" ValidationGroup="Charge" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:RequiredFieldValidator ID="reqAmount" runat="server" ForeColor="Red"
                                                    Display="Static" ErrorMessage="***" ValidationGroup="Charge" ControlToValidate="txtAmount"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="comAmount" runat="server" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"
                                                    Display="Static" ErrorMessage="***" ValidationGroup="Charge" ControlToValidate="txtAmount"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-offset-2 col-sm-2">
                                                <asp:HiddenField ID="hfChargeLrNo" runat="server" Value="" />
                                                <asp:Button ID="btnAddCharge" Text="Add" ValidationGroup="Charge" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddCharge_Click" />
                                                <asp:Button ID="btnClearCharge" Text="Clear" runat="server" CausesValidation="false" CssClass="btn btn-warning btn-label-left" OnClick="btnClearCharge_Click" />
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Repeater ID="rptChargeData" runat="server" OnItemCommand="rptChargeData_ItemCommand" OnItemDataBound="rptChargeData_ItemDataBound">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%">No.
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 50%">Charge
                                                        </td>
                                                        <td style="padding-right: 5px; text-align: right; width: 25%">Amount
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
                                                    <td style="padding-left: 5px; text-align: left; width: 50%">
                                                        <asp:Label ID="lblCharge" Text='<%# Eval("ChargeName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfLeasePayment_ChargeId" Value='<%# Eval("LeasePayment_ChargeId") %>'
                                                            runat="server" />
                                                        <asp:HiddenField ID="hfChargeId" Value='<%# Eval("ChargeId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfChargeLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-right: 5px; text-align: right; width: 25%">
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
                                                    <td style="padding-left: 5px; text-align: left; width: 50%">
                                                        <asp:Label ID="lblCharge" Text='<%# Eval("ChargeName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfLeasePayment_ChargeId" Value='<%# Eval("LeasePayment_ChargeId") %>'
                                                            runat="server" />
                                                        <asp:HiddenField ID="hfChargeId" Value='<%# Eval("ChargeId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfChargeLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-right: 5px; text-align: right; width: 25%">
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnSubmit" Text="Save & Process" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnSubmit_Click" />
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
