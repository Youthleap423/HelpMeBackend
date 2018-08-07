<%@ Page Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="SageDB.aspx.cs" Inherits="RentTrack.SageDB" %>

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
                        <i class="fa fa-th-large"></i><span>Sage DB</span>
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
                    <asp:UpdatePanel ID="upAuthType" runat="server">
                        <ContentTemplate>
                            <div class="form-group" id="Messagesection" runat="server" visible="false">
                                <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:HiddenField ID="hf_SageDBId" runat="server" Value="0" />
                                <label class="col-sm-3 control-label">
                                    Server Name :<span style="color: Red"> *</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_ServerName" runat="server" MaxLength="100" CssClass="form-control"
                                        ToolTip="Server Name"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="Req_ServerName" runat="server" ControlToValidate="txt_ServerName"
                                        Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Authentication Type :<span style="color: Red"> *</span></label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddl_AuthType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_AuthType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="--Select Authentication Type--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="SQL Server Authentication" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Windows Authentication" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="Req_AuthType" runat="server" ControlToValidate="ddl_AuthType"
                                        Display="Static" ErrorMessage="***" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    User Name :<span style="color: Red"> *</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_UserName" runat="server" MaxLength="100" CssClass="form-control"
                                        ToolTip="User Name" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="req_txt_UserName" runat="server" ControlToValidate="txt_UserName"
                                        Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    User Password :<span style="color: Red"> *</span></label>
                                <div class="col-sm-4">
                                    <asp:HiddenField ID="hfPassword" runat="server" />
                                    <asp:TextBox ID="txt_UserPassword" runat="server" MaxLength="50" TextMode="Password"
                                        CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="Req_txt_UserPassword" runat="server" ControlToValidate="txt_UserPassword"
                                        Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Database Name :<span style="color: Red"> *</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_DatabaseName" runat="server" MaxLength="100" CssClass="form-control"
                                        ToolTip="Database Name"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="Req_DatabaseName" runat="server" ControlToValidate="txt_DatabaseName"
                                        Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">&nbsp;</label>
                                <div class="col-sm-4">
                                    <asp:Button ID="btnTestConnection" runat="server" Text="Test Connection" CssClass="btn btn-primary btn-label-left"
                                        OnClick="btnTestConnection_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">&nbsp;</label>
                                <div class="col-sm-4">
                                    <asp:Label ID="lblConnection" runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">CashBook Batch No. : <span style="color: Red">*</span></label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlCashBookBatch" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-4">
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
