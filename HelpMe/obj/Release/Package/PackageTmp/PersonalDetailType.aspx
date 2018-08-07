<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="PersonalDetailType.aspx.cs" Inherits="RentTrack.PersonalDetailType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Personal Detail Type</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Personal Detail Type</span>
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
                            <div class="form-group" id="Messagesection" runat="server" visible="false">
                                <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Personal Detail Type : <span style="color: Red">*</span></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="reqTitle" runat="server" ForeColor="Red"
                                        Display="Static" ErrorMessage="***" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Text Mode : <span style="color: Red">*</span></label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlTextMode" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select Text Mode--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="SingleLine" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="MultiLine" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <asp:RequiredFieldValidator ID="Req_TextMode" runat="server" ControlToValidate="ddlTextMode"
                                        Display="Static" ErrorMessage="***" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Detail Type : </label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlDetailType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--N/A--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Contact No." Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Email Id" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Description :
                                </label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
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
                        </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
