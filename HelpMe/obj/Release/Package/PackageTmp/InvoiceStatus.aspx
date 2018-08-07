<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="InvoiceStatus.aspx.cs" Inherits="RentTrack.InvoiceStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Invoice Generation</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span id="TitleCaption" runat="server">Invoice Generation</span>
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
                                        <div class="col-sm-12">
                                            <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Select Month : </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqMonth" runat="server" ControlToValidate="ddlMonth"
                                                ForeColor="Red" Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Invoice Status : </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlInvoiceStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Processed" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Select Year : </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqYear" runat="server" ControlToValidate="ddlYear"
                                                ForeColor="Red" Display="Static" InitialValue="0" ErrorMessage="***"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">Data Type : </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlDataType" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Summary" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Detail" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            &nbsp;    
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">&nbsp;</label>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnSearch" Text="Search Record" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnProcess" Text="Process Record" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnProcess_Click" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="grddata" runat="server" AllowPaging="false" CssClass="gridstyle" AutoGenerateColumns="false"
                                                PagerSettings-Mode="NumericFirstLast" AllowSorting="true" EmptyDataText="No Data Found"
                                                Width="100%" OnPageIndexChanging="grddata_PageIndexChanging" OnRowDataBound="grddata_RowDataBound"
                                                OnSorting="grddata_Sorting">
                                                <Columns>
                                                </Columns>
                                                <HeaderStyle CssClass="gridheader" />
                                                <PagerStyle CssClass="gridpager" />
                                                <RowStyle CssClass="gridrow" />
                                                <AlternatingRowStyle CssClass="gridalrow" />
                                                <EmptyDataRowStyle CssClass="gridemptydata" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
