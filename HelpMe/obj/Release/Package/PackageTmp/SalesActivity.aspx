<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="SalesActivity.aspx.cs" Inherits="RentTrack.SalesActivity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Sales Activity</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Sales Activity</span>
                    </div>
                    <div class="box-icons">
                        <a class="collapse-link"><i class="fa fa-chevron-up"></i></a><a class="expand-link">
                            <i class="fa fa-expand"></i></a><a class="close-link"><i class="fa fa-times"></i>
                            </a>
                    </div>
                    <div class="row clearfix" id="Messagesection" runat="server">
                        <div class="col-sm-12">
                            <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                        </div>
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
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Employee :<span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                ForeColor="Red" Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Property :<span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlProperty" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqProperty" runat="server" ControlToValidate="ddlProperty"
                                                ForeColor="Red" Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Activity Type :<span style="color: Red">*</span></label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqActivityType" runat="server" ControlToValidate="ddlActivityType"
                                                ForeColor="Red" Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Activity Date & Time :</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtActivityDate" runat="server" MaxLength="10" Width="150px" CssClass="input_date form-control dateinput"></asp:TextBox>
                                            <asp:TextBox ID="txtActivityTime" runat="server" Width="100px" CssClass="form-control timeinput"></asp:TextBox>
                                            <asp:MaskedEditExtender ID="med_txtActivityTime" runat="server" AcceptAMPM="false"
                                                Mask="99:99" TargetControlID="txtActivityTime" MessageValidatorTip="true" MaskType="Time"
                                                InputDirection="RightToLeft" ErrorTooltipEnabled="True">
                                            </asp:MaskedEditExtender>
                                            <asp:MaskedEditValidator ID="mev_txtActivityTime" runat="server" ControlExtender="med_txtActivityTime"
                                                ControlToValidate="txtActivityTime" IsValidEmpty="false" MaximumValue="23:59"
                                                MinimumValue="00:00" EmptyValueMessage="Enter Time" MaximumValueMessage="23:59"
                                                InvalidValueBlurredMessage="Time is Invalid" MinimumValueMessage="Time must be grater than 00:00"
                                                EmptyValueBlurredText="*" ToolTip="Enter time between 00:00 to 23:59"></asp:MaskedEditValidator>
                                        </div>                                        
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Deal Complete :
                                        </label>
                                        <div class="col-sm-3">
                                            <asp:CheckBox ID="cbIsDealComplete" runat="server" Text=""></asp:CheckBox>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Brief Remarks : </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtBriefRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Remarks : </label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control"></asp:TextBox>
                                        </div>
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
