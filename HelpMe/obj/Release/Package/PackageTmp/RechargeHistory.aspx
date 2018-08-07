<%@ Page Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true"
    CodeBehind="RechargeHistory.aspx.cs" Inherits="RentTrack.RechargeHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Recharge History</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-search"></i><span>Recharge History</span>
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
                    <%--<h4 class="page-header">Search Recharge History</h4>--%>
                    <form id="defaultForm1" method="post" class="form-horizontal">
                        <fieldset>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Credit Note No. :</label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txt_CreditNoteNoSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    &nbsp;</label>
                                <div class="col-sm-2">
                                    <asp:Button ID="btn_search" runat="server" Text="Search" CssClass="btn btn-primary btn-label-left"
                                        OnClick="btn_search_Click" />
                                    <asp:Button ID="btn_Cancelsearch" runat="server" Text="Clear" CssClass="btn btn-warning btn-label-left"
                                        OnClick="btn_Cancelsearch_Click" />
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                        </fieldset>
                    </form>
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Total Recharge History(s) :</span>
                        <asp:Label ID="lbl_total" runat="server" class="control-label"></asp:Label>
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
                    <div class="form-group">
                        <div class="col-sm-3">
                            <asp:Button ID="btn_export" runat="server" Text="Export to Excel" OnClick="btn_export_Click"
                                CssClass="btn btn-warning btn-label-left" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="content" style="width: 100%; overflow: auto">
                        <asp:GridView ID="grd_main" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="false" GridLines="None" OnPageIndexChanging="grd_main_PageIndexChanging"
                            EmptyDataText="No Recharge History(s) Found." Width="100%" RowStyle-Wrap="true"
                            HeaderStyle-Wrap="false" FooterStyle-Wrap="false" AlternatingRowStyle-Wrap="true"
                            SelectedRowStyle-Wrap="false" SortedAscendingHeaderStyle-Wrap="false" SortedDescendingHeaderStyle-Wrap="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="CreditNoteNo" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Credit Note No."></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("CreditNoteNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="TransactionDate" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Transaction Date"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("TransactionDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="Quantity" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Quantity"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Quantity")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="RatePerSMS" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Rate / Quantity"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("RatePerSMS")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="GrossAmount" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Gross Amount"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("GrossAmount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="TaxAmount" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Tax Amount"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("TaxAmount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="TotalAmount" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Total Amount"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("TotalAmount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle HorizontalAlign="Right" />
                        </asp:GridView>
                        <div style="display: none;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" GridLines="None"
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Credit Note No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("CreditNoteNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Transaction Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("TransactionDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Quantity
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Quantity")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Rate / Quantity
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("RatePerSMS")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Gross Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("GrossAmount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Tax Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("TaxAmount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Total Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("TotalAmount")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
