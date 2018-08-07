<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="InspectionFeedback.aspx.cs" Inherits="RentTrack.InspectionFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Inspection Feedback</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Inspection Feedback</span>
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
                                    <div class="form-group" id="Messagesection" runat="server" visible="false">
                                        <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Select Request :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlInspection" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Select Employee : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlProfile" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqLandloard" runat="server" ControlToValidate="ddlProfile" ForeColor="Red"
                                                Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Select Property : <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlProperty" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqProperty" runat="server" ControlToValidate="ddlProperty" ForeColor="Red"
                                                Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Feedback Date :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtVisitDate" runat="server" Width="80%" MaxLength="10" CssClass="input_date form-control dateinput"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Comments :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
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
                        </fieldset>
                    </form>
                </div>
                <div class="clearfix">
                </div>

            </div>
        </div>
    </div>
</asp:Content>
