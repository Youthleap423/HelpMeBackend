<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="SearchJobPost.aspx.cs" Inherits="HelpMe.SearchJobPost" %>

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
                                    <div class="col-md-12 alert alert-danger" id="Messagesection" runat="server" visible="false">
                                        <asp:Label ID="Message" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-6">
                                        <div class="input-group input-group-sm mbs">
                                            <asp:DropDownList ID="ddlIsHire" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlIsHire_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="New Job Post" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Jobs Awarded but not Finished" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Finished Jobs - Payment Pending" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Finished Jobs - Payment Done" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
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
                                                    <asp:Label ID="lblPaynow" runat="server" Text="Pay Now"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgPayNow" runat="server" ToolTip='Pay Now' OnClick="imgPayNow_Click"
                                                        Text="Pay Now" CommandArgument='<%#Eval("JobPostId")%>' ImageUrl="~/Content/images/icons/paynow.png"></asp:ImageButton>
                                                    <asp:HiddenField ID="hfIsHire" runat="server" Value='<%#Eval("IsHire")%>' />
                                                    <asp:HiddenField ID="hfIsStripePaymentDone" runat="server" Value='<%#Eval("IsStripePaymentDone")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="JobTitle">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="JobTitle" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Job Title"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("JobTitle")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="JobDescription">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="JobDescription" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Job Description"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("JobDescription")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HelpSeekerName">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="HelpSeekerName" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Help Seeker Name"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("HelpSeekerName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HelpSeeker_StripeAccountId">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="HelpSeeker_StripeAccountId" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Help Seeker Stripe Id"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("HelpSeeker_StripeAccountId")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HelperName">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="HelperName" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Helper Name"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("HelperName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Helper_StripeAccountId">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="Helper_StripeAccountId" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Helper Stripe Id"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Helper_StripeAccountId")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CategoryName">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="CategoryName" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Category Name"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("CategoryName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="JobAmount">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="JobAmount" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Job Amount"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("JobAmount")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="JobHour">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="JobHour" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Job Hour"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("JobHour")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TotalPayment">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="TotalPayment" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Total Payment"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("TotalPayment")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RefundAmount">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="RefundAmount" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Refund Amount"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("RefundAmount")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="StripeDeductionAmount">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="StripeDeductionAmount" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Stripe Deduction"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("StripeDeductionAmount")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DeductionAmount">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="DeductionAmount" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Deduction Amount"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("DeductionAmount")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HelperAmount">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="HelperAmount" runat="server" ToolTip="Sort" OnClick="lnkSort_Click" Text="Helper Amount"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("HelperAmount")%>
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
