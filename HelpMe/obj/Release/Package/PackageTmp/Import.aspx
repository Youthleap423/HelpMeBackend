<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="RentTrack.Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Import Module</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Download CSV Template</span>
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
                    <div class="form-group" id="Messagesection" runat="server" visible="false">
                        <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Template Type : <span style="color: Red">*</span></label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlTemplateType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--Select Template--" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Country" Value="1"></asp:ListItem>
                                <asp:ListItem Text="State" Value="2"></asp:ListItem>
                                <asp:ListItem Text="City" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Zone" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Area" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Activity Type" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Property Type" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Location Type" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Building Parameter" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Amenity" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Feature" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Charge" Value="12"></asp:ListItem>
                                <asp:ListItem Text="Maintenance Status" Value="13"></asp:ListItem>
                                <asp:ListItem Text="Maintenance Category" Value="14"></asp:ListItem>
                                <asp:ListItem Text="Personal Detail Type" Value="15"></asp:ListItem>
                                <asp:ListItem Text="Company Detail Type" Value="16"></asp:ListItem>
                                <asp:ListItem Text="Landlord" Value="17"></asp:ListItem>
                                <asp:ListItem Text="Broker" Value="18"></asp:ListItem>
                                <asp:ListItem Text="Employee" Value="19"></asp:ListItem>
                                <asp:ListItem Text="Tenant" Value="20"></asp:ListItem>
                                <asp:ListItem Text="Lawyer" Value="21"></asp:ListItem>
                                <asp:ListItem Text="Property" Value="22"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="btnCSV" runat="server" Text="Download Template" CssClass="btn btn-primary btn-label-left" CausesValidation="false"
                                OnClick="btnCSV_Click" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Import CSV</span>
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
                    <div class="form-group" id="Messagesection_Import" runat="server" visible="false">
                        <asp:Label ID="Message_Import" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Template Type : <span style="color: Red">*</span></label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlTemplateType_Import" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--Select Template--" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Country" Value="1"></asp:ListItem>
                                <asp:ListItem Text="State" Value="2"></asp:ListItem>
                                <asp:ListItem Text="City" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Zone" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Area" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Activity Type" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Property Type" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Location Type" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Building Parameter" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Amenity" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Feature" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Charge" Value="12"></asp:ListItem>
                                <asp:ListItem Text="Maintenance Status" Value="13"></asp:ListItem>
                                <asp:ListItem Text="Maintenance Category" Value="14"></asp:ListItem>
                                <asp:ListItem Text="Personal Detail Type" Value="15"></asp:ListItem>
                                <asp:ListItem Text="Company Detail Type" Value="16"></asp:ListItem>
                                <asp:ListItem Text="Landlord" Value="17"></asp:ListItem>
                                <asp:ListItem Text="Broker" Value="18"></asp:ListItem>
                                <asp:ListItem Text="Employee" Value="19"></asp:ListItem>
                                <asp:ListItem Text="Tenant" Value="20"></asp:ListItem>
                                <asp:ListItem Text="Lawyer" Value="21"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>

                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Import File : <span style="color: Red">*</span></label>
                        <div class="col-sm-3">
                            <asp:FileUpload ID="txtDocument" runat="server" ValidationGroup="Document" CssClass="form-control"></asp:FileUpload>
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="btnImport" runat="server" Text="Import CSV" CssClass="btn btn-primary btn-label-left" CausesValidation="false"
                                OnClick="btnImport_Click" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
