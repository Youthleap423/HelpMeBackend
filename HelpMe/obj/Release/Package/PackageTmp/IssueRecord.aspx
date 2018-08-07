<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="IssueRecord.aspx.cs" Inherits="RentTrack.IssueRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Issue Recording</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Issue Recording</span>
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
                                    <asp:TextBox ID="txtIssueLrNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqIssueLrNo" runat="server" ForeColor="Red"
                                        Display="Static" ErrorMessage="***" ControlToValidate="txtIssueLrNo"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Record Date : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtRecordDate" runat="server" Width="80%" MaxLength="10" CssClass="input_date form-control dateinput"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqRecordDate" runat="server" ControlToValidate="txtRecordDate" ForeColor="Red"
                                        Display="Static" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>

                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Posted By : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlPostedby" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPostedby_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="--Select Posted By--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Employee" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Care Tacker" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="3"></asp:ListItem>
                                        <%--<asp:ListItem Text="Landlord" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Broker" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Tenant" Value="3"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqPostedby" runat="server" ControlToValidate="ddlPostedby" ForeColor="Red"
                                        Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Select Employee : <span style="color: Red">*</span></label>
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

                            <%--<div id="otherdetailsection" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Name :
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtOtherName" runat="server" Width="80%" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Mobile :
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtOtherMobile" runat="server" Width="80%" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Email :
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtOtherEmail" runat="server" Width="80%" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>--%>

                            <div class="form-group">
                                <label class="col-sm-2 control-label">Select Property : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtProperty" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:ImageButton ID="ibPropertySearch" runat="server" ImageUrl="~/Content/Images/Edit-find.png"
                                        ToolTip="Select Property" OnClick="ibPropertySearch_Click" CausesValidation="false" />
                                    <asp:HiddenField ID="hfPropertyId" runat="server" OnValueChanged="hfPropertyId_ValueChanged" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Select Category : <span style="color: Red">*</span></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqCategory" runat="server" ControlToValidate="ddlCategory" ForeColor="Red"
                                        Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
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
                            <asp:PostBackTrigger ControlID="ibPropertySearch" />
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
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(page, width, height) {
            var LeftPosition = (screen.width) ? (screen.width - width) / 2 : 0;
            var TopPosition = (screen.height) ? (screen.height - height) / 2 : 0;
            popUpObj = window.open(page, "ModalPopUp", "toolbar=no,scrollbars=no,location=no,statusbar=no,menubar=no,resizable=1,modal=yes,width=" + width + ",height=" + height + ",left=" + LeftPosition + ",top=" + TopPosition);
            popUpObj.focus();
        }

        function setDataFrom(value) {
            document.getElementById('<%=hfPropertyId.ClientID%>').value = value;
            __doPostBack('hfPropertyId', 'ValueChanged');
        }
    </script>
</asp:Content>
