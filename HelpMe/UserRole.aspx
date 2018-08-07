<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="UserRole.aspx.cs" Inherits="HelpMe.UserRole" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--BEGIN CONTENT-->
    <div class="page-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet box portlet-grey">
                    <div class="portlet-header">
                        <div class="caption">User Role</div>
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
                                    <label class="col-md-2 control-label" for="inputFirstName">User Role Name<span class="require">*</span></label>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtUserRoleName" CssClass="form-control" placeholder="User Role Name" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RequiredFieldValidator ID="reqtxtUserRoleName" ControlToValidate="txtUserRoleName" SetFocusOnError="true"
                                            runat="server" ErrorMessage="User Role Name required" class="require"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="inputFirstName">User Role Description</label>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtUserRoleDesc" CssClass="form-control" placeholder="User Role Description" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label" for="selGender"></label>
                                    <div class="col-md-7">
                                        <div>
                                            <label>
                                                <asp:CheckBox ID="ChkIsActive" runat="server" Checked="true" />
                                                &nbsp;Is Active  
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="portlet portlet-white">
                                            <div class="portlet-body pan">
                                                <asp:Repeater ID="rpt_Forms" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="table table-hover table-striped table-bordered table-advanced tablesorter mbn">
                                                            <thead>
                                                                <tr>
                                                                    <th width="30%">
                                                                        <div>
                                                                            <label>
                                                                                <asp:CheckBox ID="chkFormAll" runat="server" OnCheckedChanged="chkFormAll_CheckedChanged"
                                                                                    AutoPostBack="true" />
                                                                                &nbsp;Form Name
                                                                            </label>
                                                                        </div>
                                                                    </th>
                                                                    <th width="14%">
                                                                        <div>
                                                                            <label>
                                                                                <asp:CheckBox ID="cbView" runat="server" OnCheckedChanged="cbView_CheckedChanged"
                                                                                    AutoPostBack="true" />
                                                                                &nbsp;View
                                                                            </label>
                                                                        </div>
                                                                    </th>
                                                                    <th width="14%">
                                                                        <div>
                                                                            <label>
                                                                                <asp:CheckBox ID="cbSave" runat="server" OnCheckedChanged="cbSave_CheckedChanged"
                                                                                    AutoPostBack="true" />
                                                                                &nbsp;Save
                                                                            </label>
                                                                        </div>
                                                                    </th>
                                                                    <th width="14%">
                                                                        <div>
                                                                            <label>
                                                                                <asp:CheckBox ID="cbUpdate" runat="server" OnCheckedChanged="cbUpdate_CheckedChanged"
                                                                                    AutoPostBack="true" />
                                                                                &nbsp;Update
                                                                            </label>
                                                                        </div>
                                                                    </th>
                                                                    <th width="14%">
                                                                        <div>
                                                                            <label>
                                                                                <asp:CheckBox ID="cbDelete" runat="server" OnCheckedChanged="cbDelete_CheckedChanged"
                                                                                    AutoPostBack="true" />
                                                                                &nbsp;Delete
                                                                            </label>
                                                                        </div>
                                                                    </th>
                                                                    <th width="14%">
                                                                        <div>
                                                                            <label>
                                                                                <asp:CheckBox ID="cbPrint" runat="server" OnCheckedChanged="cbPrint_CheckedChanged"
                                                                                    AutoPostBack="true" />
                                                                                &nbsp;Print
                                                                            </label>
                                                                        </div>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <div>
                                                                    <label>
                                                                        <asp:CheckBox ID="chkForm" runat="server" OnCheckedChanged="chkForm_CheckedChanged"
                                                                            AutoPostBack="true" />
                                                                        &nbsp;<%#Eval("FormName")%>
                                                                    </label>
                                                                </div>
                                                                <asp:HiddenField ID="hfMenuName" runat="server" Value='<%#Eval("MenuName")%>' />
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <label>
                                                                        <asp:CheckBox ID="cbView" runat="server" />
                                                                        &nbsp;View
                                                                    </label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <label>
                                                                        <asp:CheckBox ID="cbSave" runat="server" />
                                                                        &nbsp;Save
                                                                    </label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <label>
                                                                        <asp:CheckBox ID="cbUpdate" runat="server" />
                                                                        &nbsp;Update
                                                                    </label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <label>
                                                                        <asp:CheckBox ID="cbDelete" runat="server" />
                                                                        &nbsp;Delete
                                                                    </label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <label>
                                                                        <asp:CheckBox ID="cbPrint" runat="server" />
                                                                        &nbsp;Print
                                                                    </label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <div class="col-md-offset-2 col-md-10">
                                        <asp:LinkButton ID="lnkSubmit" CssClass="btn btn-primary" runat="server" OnClick="lnkSubmit_Click">Submit</asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkCancel" CssClass="btn btn-default" CausesValidation="false" runat="server" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--END CONTENT-->
</asp:Content>
