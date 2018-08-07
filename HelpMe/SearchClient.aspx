<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="SearchClient.aspx.cs" Inherits="HelpMe.SearchClient" EnableEventValidation="false" ValidateRequest="false" %>

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
                                        PagerStyle-CssClass="pagination">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblLegalDocument" runat="server" Text="Legal Document"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgLegalDocument" runat="server" ToolTip='Legal Document' OnClick="imgLegalDocument_Click"
                                                        Text="Legal Document" CommandArgument='<%#Eval("ClientId")%>' ImageUrl="~/Content/images/icons/legaldocument.png" Width="24px" Height="24px"></asp:ImageButton>
                                                    <asp:HiddenField ID="hfLegalDocument" runat="server" Value='<%#Eval("LegalDocument")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblVerifyStripeAcc" runat="server" Text="Verify Stripe A/c"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgVerifyStripeConnect" runat="server" ToolTip='Legal Document' OnClick="imgVerifyStripeConnect_Click"
                                                        Text="Verify Stripe Connect" CommandArgument='<%#Eval("ClientId")%>' ImageUrl="~/Content/images/icons/no.png"></asp:ImageButton>
                                                    <asp:HiddenField ID="hfIsStripeConnectVerified" runat="server" Value='<%#Eval("IsStripeConnectVerified")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="StripeAccountId">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="StripeAccountId" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Stripe Account Id"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("StripeAccountId")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PaymentMethodDisp">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="PaymentMethodDisp" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Payment Method"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("PaymentMethodDisp")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FirstName">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="FirstName" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="First Name"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("FirstName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LastName">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="LastName" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Last Name"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("LastName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GenderDisp">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="GenderDisp" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Gender"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("GenderDisp")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address1">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="Address1" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Address1"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Address1")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address2">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="Address2" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Address2"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Address2")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CountryName">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="CountryName" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="CountryName"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("CountryName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="StateName">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="StateName" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="StateName"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("StateName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CityName">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="CityName" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="CityName"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("CityName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="POBox">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="POBox" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="PO Box"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("POBox")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PhoneNo">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="PhoneNo" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Phone No"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("PhoneNo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmailId">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="EmailId" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Email Id"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("EmailId")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rating">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="Rating" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Rating"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Rating")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Points">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="Points" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Points"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Points")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HelpMe">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="HelpMe" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="HelpMe"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("HelpMe")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offered">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="Offered" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Offered"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Offered")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="Status" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Status"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Status")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
