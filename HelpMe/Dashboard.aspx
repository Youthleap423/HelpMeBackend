<%@ Page Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HelpMe.Dashboard" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--BEGIN TITLE & BREADCRUMB PAGE-->
    <div id="title-breadcrumb-option-demo" class="page-title-breadcrumb">
        <div class="page-header pull-left">
            <div class="page-title">Dashboard</div>
        </div>
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="hidden"><a href="#">Dashboard</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Dashboard</li>
        </ol>
        <%--<div class="btn btn-blue reportrange"><i class="fa fa-calendar"></i>&nbsp;<span></span>&nbsp;report&nbsp;<i class="fa fa-angle-down"></i><input type="hidden" name="datestart" /><input type="hidden" name="endstart" /></div>--%>
        <div class="clearfix"></div>
    </div>
    <!--END TITLE & BREADCRUMB PAGE-->
    <!--BEGIN CONTENT-->

    <div class="page-content">
        <div id="tab-general">
            <div id="sum_box" class="row mbl" style="overflow: auto;">
                <%--<div class="col-md-3" style="background-color: white; overflow: auto;">
                    <div class="form-group">
                        <div class="text-center mbl">
                            <img class="img-circle" style="border: 5px solid #fff; box-shadow: 0 2px 3px rgba(0,0,0,0.25);" src="Content/images/icons/defaultuser.png" />
                        </div>
                    </div>
                    <table class="table table-striped table-hover">
                        <tbody>
                            <tr>
                                <td width="35%">Name</td>
                                <td id="tdName" runat="server"></td>
                            </tr>
                            <tr>
                                <td width="35%">PetType</td>
                                <td id="tdPetType" runat="server"></td>
                            </tr>
                            <tr>
                                <td width="35%">Department</td>
                                <td id="tdDepartment" runat="server"></td>
                            </tr>
                            <tr>
                                <td width="35%">Office</td>
                                <td id="tdOffice" runat="server"></td>
                            </tr>
                            <tr>
                                <td width="35%">User Name</td>
                                <td id="tdUserName" runat="server"></td>
                            </tr>
                            <tr>
                                <td width="35%">Email Id</td>
                                <td><u><a id="aEmailId" runat="server"></a></u></td>
                            </tr>
                            <tr>
                                <td width="35%">Signature</td>
                                <td>
                                    <asp:FileUpload ID="fuSignature" ValidationGroup="validateLetter" runat="server" />
                                    <asp:LinkButton ID="lnkUploadSignature" CssClass="btn btn-success dropdown-toggle" OnClick="lnkUploadSignature_Click" runat="server">Upload</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" width="100%">
                                    <asp:Image ID="imgSignature" runat="server" AlternateText="No Signature found." Width="365px" Height="150px" />
                                </td>
                            </tr>
                            <tr>
                                <td width="35%">Initials</td>
                                <td>
                                    <asp:FileUpload ID="fuInitials" ValidationGroup="validateLetter" runat="server" />
                                    <asp:LinkButton ID="lnkUploadInitials" CssClass="btn btn-success dropdown-toggle" OnClick="lnkUploadInitials_Click" runat="server">Upload</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" width="100%">
                                    <asp:Image ID="imgInitials" runat="server" AlternateText="No Initials found." Width="365px" Height="150px" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-lg-9">
                    <div class="col-sm-6 col-md-12">
                        <ul class="nav ul-edit nav-tabs responsive hidden-xs hidden-sm">
                            <asp:Button ID="btnLetter" OnClick="btnLetter_Click" runat="server" Text="Letter" CssClass="btn btn-success dropdown-toggle" />
                            <asp:Button ID="btnFile" OnClick="btnFile_Click" runat="server" Text="File" CssClass="btn btn-success dropdown-toggle" />
                        </ul>
                        <div class="tab-content pan mtl mbn responsive hidden-xs hidden-sm" style="background: transparent; border: 0; box-shadow: none !important">
                            <div class="tab-pane fade active in" id="divTabLetter" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="width: 100%; overflow: auto">
                                            <div class="col-md-12">
                                                <table border="0" class="table table-hover table-bordered" style="text-align: center; width: 100%;">
                                                    <tr style="background-color: #F5F5F5;">
                                                        <td>
                                                            <label class="control-label" for="selCountry" style="font-size: 14px; font-weight: bold;">Letter Inward</label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="panel panel-body col-md-4" id="divPendingLetter" runat="server">
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="panel">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <span class="mbm text" style="font-size: 14px; font-weight: bold;">Employee Vs. Priority</span>
                                                                    <div style="overflow: auto; height: 270px;">
                                                                        <asp:TreeView ID="trEmpPriority" runat="server" CssClass="jstree jstree-12 jstree-default jstree-default-responsive">
                                                                        </asp:TreeView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="panel">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <span class="mbm text" style="font-size: 14px; font-weight: bold;">Emp. + Dept. Vs. Priority</span>
                                                                    <div style="overflow: auto; height: 270px;">
                                                                        <asp:TreeView ID="trEmpDeptPriority" runat="server" CssClass="jstree jstree-12 jstree-default jstree-default-responsive">
                                                                        </asp:TreeView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <table border="0" class="table table-hover table-bordered" style="text-align: center; width: 100%;">
                                                    <tr style="background-color: #F5F5F5;">
                                                        <td>
                                                            <label class="control-label" for="selCountry" style="font-size: 14px; font-weight: bold;">Letter Outward</label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="panel panel-body col-md-4" id="divPendingLetter_Outward" runat="server">
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="panel">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <span class="mbm text" style="font-size: 14px; font-weight: bold;">Employee Vs. Priority</span>
                                                                    <div style="overflow: auto; height: 270px;">
                                                                        <asp:TreeView ID="trEmpPriorityOut" runat="server" CssClass="jstree jstree-12 jstree-default jstree-default-responsive">
                                                                        </asp:TreeView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="panel">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <span class="mbm text" style="font-size: 14px; font-weight: bold;">Emp. + Dept. Vs. Priority</span>
                                                                    <div style="overflow: auto; height: 270px;">
                                                                        <asp:TreeView ID="trEmpDeptPriorityOut" runat="server" CssClass="jstree jstree-12 jstree-default jstree-default-responsive">
                                                                        </asp:TreeView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade active in" id="divTabFile" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-md-12" id="Messagesection" runat="server" visible="false">
                                        <asp:Label ID="Message" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group input-group-sm mbs">
                                            <span>Total Record(s) :</span>
                                            <asp:Label ID="lblRecord" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group input-group-sm mbs">
                                            <asp:TextBox ID="txtSearch" CssClass="form-control" placeholder="Search text here . . ." runat="server"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search" CssClass="btn btn-success dropdown-toggle" />
                                                <asp:Button ID="btnCancelsearch" OnClick="btnCancelsearch_Click" runat="server" Text="Clear" CssClass="btn btn-success dropdown-toggle" />
                                                <asp:Button ID="btnExportExcel" OnClick="btnExportExcel_Click" runat="server" Text="Excel" CssClass="btn btn-success dropdown-toggle" />
                                                <asp:Button ID="btnExportPDF" OnClick="btnExportPDF_Click" runat="server" Text="PDF" CssClass="btn btn-success dropdown-toggle" />
                                            </span>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Open Files" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Archived Files" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="All Files" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div style="width: 100%; overflow: auto">
                                            <asp:GridView ID="grdFile" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false"
                                                PagerSettings-Mode="NumericFirstLast" AllowSorting="True" EmptyDataText="No Record(s) Found."
                                                Width="100%" OnPageIndexChanging="grdFile_PageIndexChanging" OnRowDataBound="grdFile_RowDataBound" AllowPaging="True" PagerStyle-CssClass="pagination">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            History
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgHistory" runat="server" ToolTip='File Movement History' OnClick="imgHistory_Click"
                                                                Text="History" CommandArgument='<%#Eval("FileId")%>' ImageUrl="~/Content/images/icons/history.png"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Move
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgMove" runat="server" ToolTip='Move File' OnClick="imgMove_Click"
                                                                Text="Move" CommandArgument='<%#Eval("FileId")%>' ImageUrl="~/Content/images/icons/move.png"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Remarks
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgRemarks" runat="server" ToolTip='Remarks' OnClick="imgRemarks_Click"
                                                                Text="Remarks" CommandArgument='<%#Eval("FileId")%>' ImageUrl="~/Content/images/icons/remarks.png"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="FileNo" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="File No"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbFileName" runat="server" ToolTip='<%#Eval("FileNo")%>' OnClick="lbFileName_Click"
                                                                Text='<%#Eval("FileNo")%>' CommandArgument='<%#Eval("FileId")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="FileName" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="File Name"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("FileName")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="FileDescription" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="File Description"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("FileDescription")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="CreatedByDepartment" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="Created By Department"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("CreatedByDepartment")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="CreatedByEmployee" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="Created By Employee"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("CreatedByEmployee")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="CreatedOnDisp" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="Created On"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("CreatedOnDisp")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="CurrentDepartment" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="Current Department"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("CurrentDepartment")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="CurrentEmployee" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="Current Employee"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("CurrentEmployee")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="LastRemarks" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="Last Remarks"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("LastRemarks")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="LastMovedOn" runat="server" ToolTip="Sort" OnClick="lnkSort_Click"
                                                                Text="Last Moved On"></asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("LastMovedOn")%>
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
                </div>--%>
            </div>
        </div>
    </div>
    <!--END CONTENT-->
</asp:Content>
