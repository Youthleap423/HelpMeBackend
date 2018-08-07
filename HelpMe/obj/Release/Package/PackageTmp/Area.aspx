<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="Area.aspx.cs" Inherits="RentTrack.Area" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Area</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Area Master</span>
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
                            <asp:UpdatePanel ID="upUserType" runat="server">
                                <ContentTemplate>
                                    <div class="form-group" id="Messagesection" runat="server" visible="false">
                                        <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Country : <span style="color: Red">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqGroup" runat="server" ControlToValidate="ddlGroup" ForeColor="Red"
                                                Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            State/County : <span style="color: Red">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlGroup1" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlGroup1_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqGroup1" runat="server" ControlToValidate="ddlGroup1" ForeColor="Red"
                                                Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            City : <span style="color: Red">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlGroup2" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlGroup2_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqGroup2" runat="server" ControlToValidate="ddlGroup2" ForeColor="Red"
                                                Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Zone/Town : <span style="color: Red">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlGroup3" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqGroup3" runat="server" ControlToValidate="ddlGroup3"
                                                ForeColor="Red" Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Area : <span style="color: Red">*</span></label>
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
                                        <label class="col-sm-2 control-label">
                                            Description :
                                        </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-offset-2 col-sm-4">
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
