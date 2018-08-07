<%@ Page Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="RecordNoSettings.aspx.cs" Inherits="RentTrack.RecordNoSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>User</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Record No. Settings</span>
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
                            <asp:UpdatePanel ID="upAuthType" runat="server">
                                <ContentTemplate>
                                    <div class="form-group" id="Messagesection" runat="server" visible="false">
                                        <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Record Type : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlRecordType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRecordType_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="Req_ddlRecordType" runat="server" ControlToValidate="ddlRecordType"
                                                Display="Static" ErrorMessage="***" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Prefix : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtPrefix" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="Prefix" OnTextChanged="txt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="Req_txtPrefix" runat="server" ControlToValidate="txtPrefix"
                                                Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Starting No. : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtStartingNo" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="Starting No." OnTextChanged="txt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="req_txtStartingNo" runat="server" ControlToValidate="txtStartingNo"
                                                Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Length : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLength" runat="server" MaxLength="50"
                                                CssClass="form-control" ToolTip="Length" OnTextChanged="txt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="Req_txtLength" runat="server" ControlToValidate="txtLength"
                                                Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Layout : <span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLayout" runat="server" MaxLength="100" CssClass="form-control"
                                                ToolTip="Layout" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="Req_DatabaseName" runat="server" ControlToValidate="txtLayout"
                                                Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <div class="col-sm-offset-2 col-sm-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-label-left"
                                        OnClick="btnSave_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-primary btn-label-left"
                                        OnClick="btnDelete_Click" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning btn-label-left" CausesValidation="false"
                                        OnClick="btnBack_Click" />
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
