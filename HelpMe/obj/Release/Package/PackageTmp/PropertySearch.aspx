<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="PropertySearch.aspx.cs" Inherits="RentTrack.PropertySearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Search</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box" id="Messagesection" runat="server" visible="false">
                <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
            </div>
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-search"></i><span id="TitleCaption" runat="server"></span>
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
                    <form id="defaultForm1" method="post" class="form-horizontal">
                        <fieldset>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <div class="col-sm-12 content" style="overflow: auto; height: 350px;">
                                        <asp:Repeater ID="rptSearch" runat="server" OnItemDataBound="rptSearch_ItemDataBound">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <td width="40%"><span><b>Column Name</b></span></td>
                                                        <td width="60%"><span><b>Search By</b></span></td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="40%">
                                                        <asp:Label ID="lblFieldName" runat="server" class="control-label" Text='<%#Eval("FieldName")%>'></asp:Label>
                                                        <asp:Label ID="lblColumnName" runat="server" class="control-label" Text='<%#Eval("ColumnName")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td width="60%">
                                                        <asp:TextBox ID="txtValue" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:CheckBoxList ID="cblValue" runat="server" Visible="false">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="col-sm-12">
                                        &nbsp;
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_search" runat="server" Text="Search Property" OnClick="btn_search_Click"
                                            CssClass="btn btn-Primary btn-label-left" />
                                        <asp:Button ID="btn_ClearSearch" runat="server" Text="Clear Search" CssClass="btn btn-warning btn-label-left"
                                            OnClick="btn_ClearSearch_Click" />
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="col-sm-12 content" style="overflow: auto; height: 250px;">
                                        <asp:Repeater ID="rptColumns" runat="server" OnItemDataBound="rptColumns_ItemDataBound">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <td width="40%"><span><b>Column Name</b></span></td>
                                                        <td width="60%"><span><b>Show/Hide</b></span></td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="40%">
                                                        <asp:Label ID="lblFieldName" runat="server" class="control-label" Text='<%#Eval("FieldName")%>'></asp:Label>
                                                        <asp:Label ID="lblColumnName" runat="server" class="control-label" Text='<%#Eval("ColumnName")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td width="60%">
                                                        <asp:DropDownList ID="ddlValue" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="Hide" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Show" Value="1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="col-sm-12">
                                        &nbsp;
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnColumnSettings" runat="server" Text="Save Column Settings" OnClick="btnColumnSettings_Click"
                                            CssClass="btn btn-Primary btn-label-left" />
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <%--<div class="form-group">
                                    <asp:UpdatePanel ID="upSMSTemplate" runat="server">
                                        <ContentTemplate>
                                            <label class="col-sm-2 control-label">
                                                <asp:DropDownList ID="ddlAndOr" runat="server" CssClass="form-control" Enabled="false">
                                                    <asp:ListItem Text="And" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Or" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="--Search By--" Value="0" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Equals" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Does not equal" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Begins with" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Ends with" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Is like" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Is not like" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtSearchValue" runat="server" CssClass="form-control"></asp:TextBox>
                                                <div class="col-sm-12 content" style="height: 80px; overflow: auto; overflow-y: scroll;"
                                                    id="divSearchValue" runat="server" visible="false">
                                                    <asp:Repeater ID="rptSearch" runat="server">
                                                        <HeaderTemplate>
                                                            <table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td width="20%">
                                                                    <asp:CheckBox ID="cbView" runat="server" />
                                                                    <asp:HiddenField ID="hfId" runat="server" Value='<%#Eval("ID")%>' />
                                                                </td>
                                                                <td width="80%">
                                                                    <asp:Label ID="lblName" runat="server" class="control-label" Text='<%#Eval("Name")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btn_AddCriteria" runat="server" Text="Add Criteria" CssClass="btn btn-Primary btn-label-left"
                                            OnClick="btn_AddCriteria_Click" />
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="form-group" id="ddlCriteria" runat="server">
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txt_SearchCriteria" runat="server" CssClass="form-control" ReadOnly="true"
                                            Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click"
                                            CssClass="btn btn-Primary btn-label-left" />
                                        <asp:Button ID="btn_ClearSearch" runat="server" Text="Remove Filter" CssClass="btn btn-warning btn-label-left"
                                            OnClick="btn_ClearSearch_Click" />
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>--%>
                            </div>
                            <div class="col-sm-8">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_add" runat="server" Text="Add Record" OnClick="btn_add_Click" CssClass="btn btn-primary btn-label-left" />
                                        <asp:Button ID="btn_ExportExcel" runat="server" Text="Export to Excel" OnClick="btn_ExportExcel_Click"
                                            CssClass="btn btn-warning btn-label-left" />
                                        <asp:Button ID="btn_ExportPDF" runat="server" Text="Export to PDF" OnClick="btn_ExportPDF_Click"
                                            CssClass="btn btn-warning btn-label-left" />
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="content" style="overflow: auto;">
                                    <asp:GridView ID="grddata" runat="server" AutoGenerateColumns="false"
                                        PagerSettings-Mode="NumericFirstLast" AllowSorting="true" EmptyDataText="No Data Found"
                                        Width="100%" OnPageIndexChanging="grddata_PageIndexChanging" OnRowDataBound="grddata_RowDataBound"
                                        OnSorting="grddata_Sorting" OnRowCommand="grddata_RowCommand">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="clearfix">
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
