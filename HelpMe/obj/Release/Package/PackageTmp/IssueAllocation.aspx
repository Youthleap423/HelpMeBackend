<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="IssueAllocation.aspx.cs" Inherits="RentTrack.IssueAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Issue Allocation</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Issue Allocation</span>
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
                            <script type="text/javascript">
                                Sys.Application.add_load(function () { BindDateEvents(); });
                            </script>
                            <div class="row clearfix" id="Messagesection" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-2 control-label">Select Issue : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlIssue" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlIssue" runat="server" ControlToValidate="ddlIssue" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Resolve By Date : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtResolveByDate" runat="server" Width="80%" MaxLength="10" CssClass="input_date form-control dateinput"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqResolveByDate" runat="server" ControlToValidate="txtResolveByDate" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Assigned To : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAssignedTo_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="--Select Assigned To--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Employee" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Care Tacker" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlAssignedTo" runat="server" ControlToValidate="ddlAssignedTo" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Select Employee : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlAssignedProfile" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlAssignedProfile" runat="server" ControlToValidate="ddlAssignedProfile" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Is Chargeble : </label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlCharge" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Text="--Select Charge--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlCharge" runat="server" ControlToValidate="ddlCharge" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Amount :
                                </label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtCharge" runat="server" Width="80%" MaxLength="5" CssClass="input_date form-control" Text="0"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Email To :</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlEmailTo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEmailTo_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="--Select Email To--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Landlord" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Broker" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Tenant" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Employee" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlEmailTo" runat="server" ControlToValidate="ddlEmailTo" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Select Email Profile : </label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlEmailToProfile" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlEmailToProfile" runat="server" ControlToValidate="ddlEmailToProfile" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
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
