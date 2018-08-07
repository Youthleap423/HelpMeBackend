<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="IssueProgress.aspx.cs" Inherits="RentTrack.IssueProgress" %>

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
                                <label class="col-sm-2 control-label">
                                    Issue LR No : <span style="color: Red">*</span>
                                </label>
                                <div class="col-sm-3">
                                    <asp:Label ID="lblIssueLrNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-sm-1">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Progress Date : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtDate" runat="server" Width="80%" MaxLength="10" CssClass="input_date form-control dateinput"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqtxtDate" runat="server" ControlToValidate="txtDate" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Stage : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlStage" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlStage" runat="server" ControlToValidate="ddlStage" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Is Completed : </label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlCompleted" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Text="--Select Status--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqddlCompleted" runat="server" ControlToValidate="ddlCompleted" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Completed Date :
                                </label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtCompletedDate" runat="server" Width="80%" MaxLength="10" CssClass="input_date form-control dateinput"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqtxtCompletedDate" runat="server" ControlToValidate="txtCompletedDate" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Description</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
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
