<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="HelpMe.Search" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">
                            <asp:Label ID="TitleCaption" runat="server" Text="Search"></asp:Label>
                        </div>
                        <div class="tools">
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="form-horizontal form-separated" role="form">
                            <div class="form-body">
                                <div class="form-group">
                                    <div class="col-md-12" id="Messagesection" runat="server" visible="false">
                                        <asp:Label ID="Message" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-6">
                                        <div class="input-group input-group-sm mbs">
                                            <span class="input-group-btn">
                                                <asp:Button ID="btnAddData" OnClick="btnAdd_Click" runat="server" Text=" + " CssClass="btn btn-success dropdown-toggle" />
                                                <asp:Button ID="btnAdd" OnClick="btnAdd_Click" runat="server" Text="Add Data" CssClass="btn btn-success dropdown-toggle" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                        <div class="input-group input-group-sm mbs">
                                            <asp:CheckBox ID="chkArchived" Checked="false" runat="server" Text="Archived" CssClass="checkbox" />
                                        </div>
                                    </div>
                                    <div class="col-lg-5">
                                        <div class="input-group input-group-sm mbs">
                                            <asp:TextBox ID="txtSearch" CssClass="form-control" placeholder="Search text here . . ." runat="server"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search" CssClass="btn btn-success dropdown-toggle" />
                                                <asp:Button ID="btnCancelsearch" OnClick="btnCancelsearch_Click" runat="server" Text="Clear" CssClass="btn btn-success dropdown-toggle" />
                                                <asp:Button ID="btnExportExcel" OnClick="btnExportExcel_Click" runat="server" Text="Excel" CssClass="btn btn-success dropdown-toggle" />
                                                <asp:Button ID="btnExportPDF" OnClick="btnExportPDF_Click" runat="server" Text="PDF" CssClass="btn btn-success dropdown-toggle" />
                                            </span>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <div class="input-group input-group-sm mbs">
                                        <asp:Label ID="lblRecord" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grddata" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false"
                                        PagerSettings-Mode="NumericFirstLast" AllowSorting="true" EmptyDataText="No Data Found"
                                        Width="100%" OnPageIndexChanging="grddata_PageIndexChanging" OnRowDataBound="grddata_RowDataBound"
                                        OnSorting="grddata_Sorting" PagerStyle-CssClass="pagination">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>