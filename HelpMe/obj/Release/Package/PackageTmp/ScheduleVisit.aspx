<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="ScheduleVisit.aspx.cs" Inherits="RentTrack.ScheduleVisit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Schedule Visit</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Schedule Visit</span>
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
                                    <div class="row clearfix" id="Messagesection" runat="server" visible="false">
                                        <div class="col-sm-12">
                                            <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">First Name : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqFirst" runat="server" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Middle Name : </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Last Name : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLastname" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqLast" runat="server" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ControlToValidate="txtLastname"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Mobile : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqMobile" runat="server" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ControlToValidate="txtMobile"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Email : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqEmail" runat="server" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="regEmail" runat="server" ForeColor="Red" ControlToValidate="txtEmail"
                                                Display="Static" ErrorMessage="**" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Employee :<span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                ForeColor="Red" Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Visit Date & Time :</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtVisitDate" runat="server" MaxLength="10" Width="50%" CssClass="input_date form-control dateinput"></asp:TextBox>
                                            <asp:TextBox ID="txtVisitTime" runat="server" Width="30%" CssClass="form-control timeinput"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Comments : </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <h2>Properties</h2>
                                    <div class="form-group" id="MessagePropertysection" runat="server" visible="false">
                                        <div class="col-sm-12">
                                            <asp:Label ID="MessageProperty" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Property : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlProperty" runat="server" ValidationGroup="Property" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqProperty" runat="server" ControlToValidate="ddlProperty"
                                                ForeColor="Red" Display="Static" InitialValue="0" ValidationGroup="Property" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Status : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlStatus" runat="server" ValidationGroup="Property" CssClass="form-control">
                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Cancel" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Postpone" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Confirm" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqStatus" runat="server" ControlToValidate="ddlStatus"
                                                ForeColor="Red" Display="Static" InitialValue="0" ValidationGroup="Property" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-offset-2 col-sm-2">
                                            <asp:HiddenField ID="hfPropertyLrNo" runat="server" Value="" />
                                            <asp:Button ID="btnAddProperty" Text="Add Property" ValidationGroup="Property" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddProperty_Click" />
                                            <asp:Button ID="btnClearProperty" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearProperty_Click" />
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <asp:Repeater ID="rptPropertyData" runat="server" OnItemCommand="rptPropertyData_ItemCommand">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%">No.
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 30%">Property
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 20%">Type
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
                                                    <td style="padding-left: 5px; text-align: left; width: 30%">
                                                        <asp:Label ID="lblProperty" Text='<%# Eval("PropertyName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfScheduleVisitPropertyId" Value='<%# Eval("ScheduleVisitPropertyId") %>'
                                                            runat="server" />
                                                        <asp:HiddenField ID="hfPropertyId" Value='<%# Eval("PropertyId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 20%">
                                                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfTypeValue" Value='<%# Eval("StatusValue") %>' runat="server" />
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
                                                    <td style="padding-left: 5px; text-align: left; width: 30%">
                                                        <asp:Label ID="lblProperty" Text='<%# Eval("PropertyName") %>' runat="server"></asp:Label>
                                                        <asp:HiddenField ID="hfScheduleVisitPropertyId" Value='<%# Eval("ScheduleVisitPropertyId") %>'
                                                            runat="server" />
                                                        <asp:HiddenField ID="hfPropertyId" Value='<%# Eval("PropertyId") %>' runat="server" />
                                                        <asp:HiddenField ID="hfPropertyLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 25%">
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 25%">
                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 20%">
                                                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfTypeValue" Value='<%# Eval("StatusValue") %>' runat="server" />
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
                                <div class="col-sm-offset-2 col-sm-3">
                                    <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-label-left" Visible="false"
                                        OnClick="btnDelete_Click" />
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
