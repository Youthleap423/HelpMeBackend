<%@ Page Language="C#" MasterPageFile="~/Admin/SiteLinks.Master" AutoEventWireup="true"
    CodeBehind="Company.aspx.cs" Inherits="RentTrack.Admin.Company" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Company Master</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div id="pnl_add" runat="server" class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Company Master</span>
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
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validate_group"
                        ShowMessageBox="false" ShowSummary="false" />
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Company Name :<span style="color: Red"> *</span></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_CompanyName" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="Company Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Req_txt_CompanyName" runat="server" ControlToValidate="txt_CompanyName"
                                Display="Dynamic" ErrorMessage="Company Name is required." ValidationGroup="validate_group"
                                CssClass="help-block"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Contact Person :<span style="color: Red"> *</span></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_ContactPerson" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="ContactPerson"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Req_txt_ContactPerson" runat="server" ControlToValidate="txt_ContactPerson"
                                Display="Dynamic" ErrorMessage="Contact Person is required." ValidationGroup="validate_group"
                                CssClass="help-block"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Address :</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_Address" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="Address"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="act_txt_Address" runat="server" TargetControlID="txt_Address"
                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                                ServiceMethod="GetAddress">
                            </cc1:AutoCompleteExtender>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Town :</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_Town" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="Town"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="act_txt_Town" runat="server" TargetControlID="txt_Town"
                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                                ServiceMethod="GetTown">
                            </cc1:AutoCompleteExtender>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            PO Box :</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_POBox" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="PO Box"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            PO Box Code :</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_POBoxCode" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="PO Box Code"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mobile No. :<span style="color: Red"> *</span></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_MobileNo" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="Mobile No."></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Req_txt_MobileNo" runat="server" ControlToValidate="txt_MobileNo"
                                Display="Dynamic" ErrorMessage="Mobile No is required." ValidationGroup="validate_group"
                                CssClass="help-block"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Contact No. :</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_ContactNo" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="Contact No."></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Email Id :</label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_EmailId" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="Email Id"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Other Notes :</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txt_Description" runat="server" MaxLength="300" TextMode="MultiLine"
                                CssClass="form-control" ToolTip="Description"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>


                    <div class="form-group">
                        <asp:HiddenField ID="hf_SageDBId" runat="server" Value="0" />
                        <label class="col-sm-2 control-label">
                            Server Name :<span style="color: Red"> *</span></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_ServerName" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="Server Name" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="Req_ServerName" runat="server" ControlToValidate="txt_ServerName"
                                Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Authentication Type :<span style="color: Red"> *</span></label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddl_AuthType" runat="server" CssClass="form-control" ReadOnly="true">
                                <asp:ListItem Text="--Select Authentication Type--" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="SQL Server Authentication" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Windows Authentication" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="Req_AuthType" runat="server" ControlToValidate="ddl_AuthType"
                                Display="Static" ErrorMessage="***" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            User Name :<span style="color: Red"> *</span></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_UserName" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="User Name" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="req_txt_UserName" runat="server" ControlToValidate="txt_UserName"
                                Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            User Password :<span style="color: Red"> *</span></label>
                        <div class="col-sm-3">
                            <asp:HiddenField ID="hfPassword" runat="server" />
                            <asp:TextBox ID="txt_UserPassword" runat="server" MaxLength="50" TextMode="Password"
                                CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="Req_txt_UserPassword" runat="server" ControlToValidate="txt_UserPassword"
                                Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Database Name :<span style="color: Red"> *</span></label>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_DatabaseName" runat="server" MaxLength="100" CssClass="form-control"
                                ToolTip="Database Name"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="Req_DatabaseName" runat="server" ControlToValidate="txt_DatabaseName"
                                Display="Static" ErrorMessage="***" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>


                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-2">
                            <asp:HiddenField ID="hd_primaryid" runat="server" />
                            <asp:Button ID="btn_submit" runat="server" Text="Save" CssClass="btn btn-primary btn-label-left"
                                OnClick="btn_submit_Click" ValidationGroup="validate_group" CausesValidation="True" />
                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CssClass="btn btn-warning btn-label-left"
                                OnClick="btn_cancel_Click" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>
        <div id="pnl_list" runat="server" class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-search"></i><span>Company Master</span>
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
                        <label class="col-sm-2 control-label">
                            Company Name :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_CompanyNameSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Contact Person :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_ContactPersonSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mobile No. :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_MobileNoSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Estate/Village :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_AddressSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Town :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_TownSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            PO Box :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_POBoxSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            PO Box Code :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_POBoxCodeSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Contact No. :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_ContactNoSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Email Id :</label>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_EmailIdSearch" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
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
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Total Company(s) :</span>
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
                            <asp:Button ID="btn_add" runat="server" Text="Add Company Master" OnClick="btn_add_Click"
                                CssClass="btn btn-primary btn-label-left" />
                            <asp:Button ID="btn_export" runat="server" Text="Export to Excel" OnClick="btn_export_Click"
                                CssClass="btn btn-warning btn-label-left" />
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="content" style="width: 100%; overflow: auto">
                        <asp:GridView ID="grd_main" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="false" GridLines="None" OnPageIndexChanging="grd_main_PageIndexChanging"
                            EmptyDataText="No Company(s) Found." Width="100%" RowStyle-Wrap="true" HeaderStyle-Wrap="false"
                            FooterStyle-Wrap="false" AlternatingRowStyle-Wrap="true" SelectedRowStyle-Wrap="false"
                            SortedAscendingHeaderStyle-Wrap="false" SortedDescendingHeaderStyle-Wrap="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <span style="font-weight: bold">Actions</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_update" runat="server" ToolTip="Click here to Modify record"
                                            CommandArgument='<%#Eval("CompanyId")%>' OnClick="btn_click"><img src="Content/img/icons/actions/edit.png" alt="" /></asp:LinkButton>
                                        <asp:LinkButton ID="lnk_delete" runat="server" ToolTip="Click here to Delete record"
                                            CommandArgument='<%#Eval("CompanyId")%>' OnClick="btn_click" OnCompanyClick="javascript:return confirm('Are you sure want to delete this record?');"><img
                                            src="Content/img/icons/actions/delete.png" alt="" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="CompanyName" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Company Name"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("CompanyName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="ContactPerson" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Contact Person"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("ContactPerson")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="MobileNo" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Mobile No."></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("MobileNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="EmailId" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Email Id"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("EmailId")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="Address" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Address"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Address")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="Town" runat="server" ToolTip="Sort" OnClick="lnksort_Click" Text="Town"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Town")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="POBox" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="POBox"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("POBox")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="POBoxCode" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Code"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("POBoxCode")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="ContactNo" runat="server" ToolTip="Sort" OnClick="lnksort_Click"
                                            Text="Contact No."></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("ContactNo")%>
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
                                            Company Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("CompanyName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Contact Person
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("ContactPerson")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Mobile No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("MobileNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Email Id
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("EmailId")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Address
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Address")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Town
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Town")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            PO Box
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("POBox")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            POBox Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("POBoxCode")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Contact No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("ContactNo")%>
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
