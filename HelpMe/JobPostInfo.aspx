<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="JobPostInfo.aspx.cs" Inherits="HelpMe.JobPostInfo" %>
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
                                    <div class="col-md-12">
                                        <div id="dvMsg" runat="server" visible="false" class="note note-danger">
                                            <h5 class="box-heading">Error</h5>
                                            <p>
                                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        <div class="input-group input-group-sm mbs">
                                            <asp:Label ID="lblRecord_JobPostOffer" runat="server" Text=""></asp:Label>
                                            <span class="input-group-btn">
                                                <asp:Button ID="btnExportExcel_JobPostOffer" OnClick="btnExportExcel_JobPostOffer_Click" runat="server" Text="Excel" CssClass="btn btn-success dropdown-toggle" />
                                                <asp:Button ID="btnExportPDF_JobPostOffer" OnClick="btnExportPDF_JobPostOffer_Click" runat="server" Text="PDF" CssClass="btn btn-success dropdown-toggle" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12" style="width: 100%; overflow: auto">
                                        <asp:GridView ID="grdJobPostOffer" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false"
                                            PagerSettings-Mode="NumericFirstLast" AllowSorting="true" EmptyDataText="No Data Found"
                                            Width="100%" OnPageIndexChanging="grdJobPostOffer_PageIndexChanging" OnRowDataBound="grdJobPostOffer_RowDataBound" PagerStyle-CssClass="pagination">
                                            <Columns>
                                                <asp:TemplateField HeaderText="First Name">
                                                    <ItemTemplate>
                                                        <%#Eval("FirstName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Name">
                                                    <ItemTemplate>
                                                        <%#Eval("LastName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <%#Eval("EmailId")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job Title">
                                                    <ItemTemplate>
                                                        <%#Eval("JobTitle")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <%#Eval("JobDescription")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <%#Eval("OfferAmount")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        <div class="input-group input-group-sm mbs">
                                            <asp:Label ID="lblRecord_JobPostView" runat="server" Text=""></asp:Label>
                                            <span class="input-group-btn">
                                                <asp:Button ID="btnExportExcel_JobPostView" OnClick="btnExportExcel_JobPostView_Click" runat="server" Text="Excel" CssClass="btn btn-success dropdown-toggle" />
                                                <asp:Button ID="btnExportPDF_JobPostView" OnClick="btnExportPDF_JobPostView_Click" runat="server" Text="PDF" CssClass="btn btn-success dropdown-toggle" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12" style="width: 100%; overflow: auto">
                                        <asp:GridView ID="grdJobPostView" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false"
                                            PagerSettings-Mode="NumericFirstLast" AllowSorting="true" EmptyDataText="No Data Found"
                                            Width="100%" OnPageIndexChanging="grdJobPostView_PageIndexChanging" OnRowDataBound="grdJobPostView_RowDataBound" PagerStyle-CssClass="pagination">
                                            <Columns>
                                                <asp:TemplateField HeaderText="First Name">
                                                    <ItemTemplate>
                                                        <%#Eval("FirstName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Name">
                                                    <ItemTemplate>
                                                        <%#Eval("LastName")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created On">
                                                    <ItemTemplate>
                                                        <%#Eval("CreatedOn")%>
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
    </div>
</asp:Content>
