<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectProperty.aspx.cs" Inherits="RentTrack.SelectProperty" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Property</title>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link rel="shortcut icon" href="Content/img/favicon.ico" />
    <link href="Content/plugins/bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="Content/plugins/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
    <link href="Content/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Content/css/Righteous.css" rel="stylesheet" type="text/css" />
    <link href="Content/plugins/fancybox/jquery.fancybox.css" rel="stylesheet" />
    <link href="Content/plugins/fullcalendar/fullcalendar.css" rel="stylesheet" />
    <link href="Content/plugins/xcharts/xcharts.min.css" rel="stylesheet" />
    <link href="Content/plugins/select2/select2.css" rel="stylesheet" />
    <link href="Content/css/style.css" rel="stylesheet" />
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
		<script src="http://getbootstrap.com/docs-assets/js/html5shiv.js"></script>
		<script src="http://getbootstrap.com/docs-assets/js/respond.min.js"></script>
	<![endif]-->
</head>
<body>
    <form id="defaultForm1" class="form-horizontal" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
        </cc1:ToolkitScriptManager>
        <div id="main" class="container-fluid">
            <div class="row">
                <div class="col-xs-12 col-sm-12">
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
                            <div class="box" id="Messagesection" runat="server" visible="false">
                                <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <div class="col-sm-12 content propertyselection" style="overflow: auto; height: 350px;">
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
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="col-sm-8">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_ExportExcel" runat="server" Text="Export to Excel" OnClick="btn_ExportExcel_Click"
                                            CssClass="btn btn-warning btn-label-left" />
                                        <asp:Button ID="btn_ExportPDF" runat="server" Text="Export to PDF" OnClick="btn_ExportPDF_Click"
                                            CssClass="btn btn-warning btn-label-left" />
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="content" style="overflow: auto;">
                                    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grddata" runat="server" AutoGenerateColumns="false"
                                                PagerSettings-Mode="NumericFirstLast" AllowSorting="true" EmptyDataText="No Data Found"
                                                Width="100%" OnPageIndexChanging="grddata_PageIndexChanging" OnRowDataBound="grddata_RowDataBound"
                                                OnSorting="grddata_Sorting" OnSelectedIndexChanging="grddata_SelectedIndexChanging">
                                                <Columns>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="grddata" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="Content/plugins/jquery/jquery-2.1.0.min.js" type="text/javascript"></script>
    <script src="Content/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="Content/plugins/bootstrap/bootstrap.min.js" type="text/javascript"></script>
    <script src="Content/plugins/justified-gallery/jquery.justifiedgallery.min.js" type="text/javascript"></script>
    <script src="Content/js/devoops.js" type="text/javascript"></script>
    <script src="Content/js/nav.js" type="text/javascript"></script>
    <script src="Content/js/date.format.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Create Wysiwig editor for textare
            // Add slider for change test input length
            FormLayoutExampleInputLength($(".slider-style"));
            // Add tooltip to form-controls
            $('.form-control').tooltip();
            WinMove();
        });
    </script>
</body>
</html>
