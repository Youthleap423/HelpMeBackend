<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLinks.Master" AutoEventWireup="true" CodeBehind="Lawyer.aspx.cs" Inherits="RentTrack.Lawyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="breadcrumb" class="col-xs-12">
            <ol class="breadcrumb">
                <li><a href="Dashboard.aspx">Home</a></li>
                <li>Lawyer</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-th-large"></i><span>Lawyer Master</span>
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
                                        <label class="col-sm-3 control-label">
                                            Lawyer Type :<span style="color: Red"> *</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlLawyerType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLawyerType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="--Select Type--" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Company" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Individual" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="Req_LawyerType" runat="server" ControlToValidate="ddlLawyerType"
                                                Display="Static" ErrorMessage="***" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div id="divPersonalDetailShow" runat="server" visible="true">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                First Name : <span style="color: Red">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Middle Name :</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Last Name : <span style="color: Red">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtLastname" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div id="divCompanyDetailShow" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Company Name : <span style="color: Red">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>

                                    <div id="divPersonalDetails" runat="server" visible="true">
                                        <br />
                                        <h4><b><u>Personal Details</u></b></h4>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Detail Type : <span style="color: Red">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlPersonalDetailType" runat="server" ValidationGroup="PersonalDetailType" CssClass="form-control" OnSelectedIndexChanged="ddlPersonalDetailType_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:RequiredFieldValidator ID="reqddlPersonalDetailType" runat="server" ControlToValidate="ddlPersonalDetailType" ForeColor="Red"
                                                    Display="Static" InitialValue="0" ValidationGroup="PersonalDetailType" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Value : <span style="color: Red">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtPersonalDetailValue" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:RequiredFieldValidator ID="reqtxtPersonalDetailValue" runat="server" ControlToValidate="txtPersonalDetailValue" ForeColor="Red"
                                                    Display="Static" ValidationGroup="PersonalDetailType" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:HiddenField ID="hfLawyerDetailsLrNo" runat="server" Value="" />
                                                <asp:Button ID="btnAddLawyerDetails" Text="Add" ValidationGroup="PersonalDetailType" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddLawyerDetails_Click" />
                                                <asp:Button ID="btnClearLawyerDetails" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearLawyerDetails_Click" />
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group" id="MessageLawyerDetailsSection" runat="server" visible="false">
                                            <div class="col-sm-12">
                                                <asp:Label ID="MessageLawyerDetails" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group">
                                            <asp:Repeater ID="rptLawyerDetailsData" runat="server" OnItemCommand="rptLawyerDetailsData_ItemCommand">
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                        <tr class="repeaterheader">
                                                            <td style="text-align: center; width: 5%"><b>No.</b>
                                                            </td>
                                                            <td style="padding-left: 5px; text-align: left; width: 45%"><b>Detail Type</b>
                                                            </td>
                                                            <td style="padding-left: 5px; text-align: left; width: 30%"><b>Value</b>
                                                            </td>
                                                            <td style="text-align: center; width: 10%"><b>Edit</b>
                                                            </td>
                                                            <td style="text-align: center; width: 10%"><b>Delete</b>
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="repeaterrow">
                                                        <td style="text-align: center; width: 5%">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 45%">
                                                            <asp:Label ID="lblPersonalDetailType" Text='<%# Eval("PersonalDetailTypeName") %>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfLawyerDetailsId" Value='<%# Eval("LawyerDetailsId") %>' runat="server" />
                                                            <asp:HiddenField ID="hfPersonalDetailTypeId" Value='<%# Eval("PersonalDetailTypeId") %>' runat="server" />
                                                            <asp:HiddenField ID="hfLawyerDetailsLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 30%">
                                                            <asp:Label ID="lblPersonalDetailValue" runat="server" Text='<%# Eval("PersonalDetailValue") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center; width: 10%">
                                                            <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                                CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                                        </td>
                                                        <td style="text-align: center; width: 10%">
                                                            <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                                CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                                ToolTip="Delete" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="repeateralrow">
                                                        <td style="text-align: center; width: 5%">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 45%">
                                                            <asp:Label ID="lblPersonalDetailType" Text='<%# Eval("PersonalDetailTypeName") %>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfLawyerDetailsId" Value='<%# Eval("LawyerDetailsId") %>' runat="server" />
                                                            <asp:HiddenField ID="hfPersonalDetailTypeId" Value='<%# Eval("PersonalDetailTypeId") %>' runat="server" />
                                                            <asp:HiddenField ID="hfLawyerDetailsLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 30%">
                                                            <asp:Label ID="lblPersonalDetailValue" runat="server" Text='<%# Eval("PersonalDetailValue") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center; width: 10%">
                                                            <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                                CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                                        </td>
                                                        <td style="text-align: center; width: 10%">
                                                            <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                                CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                                ToolTip="Delete" />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="clearfix">
                                        </div>

                                    </div>

                                    <div id="divCompanyDetails" runat="server" visible="false">
                                        <br />
                                        <h4><b><u>Company Details</u></b></h4>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Detail Type : <span style="color: Red">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlCompanyDetailType" runat="server" ValidationGroup="CompanyPersonalDetailType" CssClass="form-control" OnSelectedIndexChanged="ddlCompanyDetailType_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:RequiredFieldValidator ID="reqddlCompanyDetailType" runat="server" ControlToValidate="ddlCompanyDetailType" ForeColor="Red"
                                                    Display="Static" InitialValue="0" ValidationGroup="CompanyPersonalDetailType" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Value : <span style="color: Red">*</span></label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtCompanyDetailValue" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:RequiredFieldValidator ID="reqtxtCompanyDetailValue" runat="server" ControlToValidate="txtCompanyDetailValue" ForeColor="Red"
                                                    Display="Static" ValidationGroup="CompanyPersonalDetailType" ErrorMessage="***"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:HiddenField ID="hfLawyerCompanyDetailsLrNo" runat="server" Value="" />
                                                <asp:Button ID="btnAddLawyerCompanyDetails" Text="Add" ValidationGroup="CompanyPersonalDetailType" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddLawyerCompanyDetails_Click" />
                                                <asp:Button ID="btnClearLawyerCompanyDetails" CausesValidation="false" Text="Clear" runat="server" CssClass="btn btn-warning btn-label-left" OnClick="btnClearLawyerCompanyDetails_Click" />
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group" id="MessageLawyerCompanyDetailsSection" runat="server" visible="false">
                                            <div class="col-sm-12">
                                                <asp:Label ID="MessageLawyerCompanyDetails" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group">
                                            <asp:Repeater ID="rptLawyerCompanyDetailsData" runat="server" OnItemCommand="rptLawyerCompanyDetailsData_ItemCommand">
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                        <tr class="repeaterheader">
                                                            <td style="text-align: center; width: 5%"><b>No.</b>
                                                            </td>
                                                            <td style="padding-left: 5px; text-align: left; width: 45%"><b>Detail Type</b>
                                                            </td>
                                                            <td style="padding-left: 5px; text-align: left; width: 30%"><b>Value</b>
                                                            </td>
                                                            <td style="text-align: center; width: 10%"><b>Edit</b>
                                                            </td>
                                                            <td style="text-align: center; width: 10%"><b>Delete</b>
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="repeaterrow">
                                                        <td style="text-align: center; width: 5%">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 45%">
                                                            <asp:Label ID="lblPersonalDetailType" Text='<%# Eval("PersonalDetailTypeName") %>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfLawyerCompanyDetailsId" Value='<%# Eval("LawyerCompanyDetailsId") %>' runat="server" />
                                                            <asp:HiddenField ID="hfPersonalDetailTypeId" Value='<%# Eval("PersonalDetailTypeId") %>' runat="server" />
                                                            <asp:HiddenField ID="hfLawyerCompanyDetailsLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 30%">
                                                            <asp:Label ID="lblPersonalDetailValue" runat="server" Text='<%# Eval("PersonalDetailValue") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center; width: 10%">
                                                            <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                                CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                                        </td>
                                                        <td style="text-align: center; width: 10%">
                                                            <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                                CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                                ToolTip="Delete" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="repeateralrow">
                                                        <td style="text-align: center; width: 5%">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 45%">
                                                            <asp:Label ID="lblPersonalDetailType" Text='<%# Eval("PersonalDetailTypeName") %>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfLawyerCompanyDetailsId" Value='<%# Eval("LawyerCompanyDetailsId") %>' runat="server" />
                                                            <asp:HiddenField ID="hfPersonalDetailTypeId" Value='<%# Eval("PersonalDetailTypeId") %>' runat="server" />
                                                            <asp:HiddenField ID="hfLawyerCompanyDetailsLrNo" Value='<%# Eval("iLrNo") %>' runat="server" />
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 30%">
                                                            <asp:Label ID="lblPersonalDetailValue" runat="server" Text='<%# Eval("PersonalDetailValue") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center; width: 10%">
                                                            <asp:ImageButton ID="ibEdit" ImageUrl="~/Content/Images/Edit.gif" CommandName="Edit"
                                                                CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server" ToolTip="Edit" />
                                                        </td>
                                                        <td style="text-align: center; width: 10%">
                                                            <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                                CommandArgument='<%# Eval("iLrNo") %>' CausesValidation="false" runat="server"
                                                                ToolTip="Delete" />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <h4><b><u>Documents</u></b></h4>
                                    <div class="form-group" id="MessageDocumentsection" runat="server" visible="false">
                                        <div class="col-sm-12">
                                            <asp:Label ID="MessageDocument" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">
                                            Document :
                                        </label>
                                        <div class="col-sm-4">
                                            <asp:FileUpload ID="txtDocument" runat="server" ValidationGroup="Document" CssClass="form-control"></asp:FileUpload>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:RequiredFieldValidator ID="reqDocument" runat="server" ForeColor="Red"
                                                Display="Static" ErrorMessage="***" ValidationGroup="Document" ControlToValidate="txtDocument"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btnAddDocument" Text="Upload" ValidationGroup="Document" runat="server" CssClass="btn btn-primary btn-label-left" OnClick="btnAddDocument_Click" />
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="form-group">
                                        <asp:Repeater ID="rptDocumentData" runat="server" OnItemCommand="rptDocumentData_ItemCommand">
                                            <HeaderTemplate>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="text-align: center;">
                                                    <tr class="repeaterheader">
                                                        <td style="text-align: center; width: 5%">No.
                                                        </td>
                                                        <td style="padding-left: 5px; text-align: left; width: 85%">Document
                                                        </td>
                                                        <td style="text-align: center; width: 10%">Delete
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="repeaterrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 85%">
                                                        <asp:LinkButton ID="lnkDocument" Text='<%# Eval("DocumentName") %>' CommandName="Download" runat="server" CausesValidation="false" CommandArgument='<%# Eval("DocumentName") %>' ToolTip="Download"></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center; width: 10%">
                                                        <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                            CommandArgument='<%# Eval("DocumentName") %>' CausesValidation="false" runat="server"
                                                            ToolTip="Delete" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="repeateralrow">
                                                    <td style="text-align: center; width: 5%">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>
                                                    <td style="padding-left: 5px; text-align: left; width: 85%">
                                                        <asp:LinkButton ID="lnkDocument" Text='<%# Eval("DocumentName") %>' CommandName="Download" runat="server" CausesValidation="false" CommandArgument='<%# Eval("DocumentName") %>' ToolTip="Download"></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center; width: 10%">
                                                        <asp:ImageButton ID="ibDelete" ImageUrl="~/Content/Images/delete.gif" CommandName="Delete"
                                                            CommandArgument='<%# Eval("DocumentName") %>' CausesValidation="false" runat="server"
                                                            ToolTip="Delete" />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnAddDocument" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="clearfix">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-label-left"
                                        OnClick="btnSave_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-label-left" Visible="false"
                                        OnClick="btnDelete_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning btn-label-left" CausesValidation="false"
                                        OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning btn-label-left" CausesValidation="false"
                                        OnClick="btnBack_Click" />
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
