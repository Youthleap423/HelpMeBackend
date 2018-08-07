<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="RentTrack.index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Login : Rent Track Management</title>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link rel="shortcut icon" href="Content/img/favicon.ico" />
    <link href="Content/plugins/bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="Content/css/font-awesome.css" rel="stylesheet" />
    <link href="Content/css/Righteous.css" rel="stylesheet" />
    <link href="Content/css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="container-fluid">
            <div id="page-login" class="row">
                <div class="col-xs-12 col-md-4 col-md-offset-4 col-sm-6 col-sm-offset-3">
                    <div class="Logobox">
                        <img src="Content/img/logo.gif" alt="logo" />
                    </div>
                    <div class="box">
                        <div class="box-content">
                            <div class="form-group" id="Messagesection" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <asp:Label ID="Message" runat="server" CssClass="form-control" ViewStateMode="Disabled"></asp:Label>
                                </div>
                            </div>

                            <div class="text-center">
                                <h3 class="page-header">
                                    <img src="Content/img/icons/lock-closed.png" alt="" />Login : Rent Track Management</h3>
                            </div>
                            <div class="form-group">
                                <label class="control-label">
                                    User Name :</label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="UserName"
                                    TargetControlID="UserName">
                                </asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="form-group">
                                <label class="control-label">
                                    Password :</label>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="Login"
                                    TargetControlID="Password">
                                </asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="cbRemember" runat="server" />
                                <span>Remember me</span>
                            </div>
                            <div class="text-center">
                                <asp:Button ID="btnLogin" runat="server" Text="Sign in" OnClick="btnLogin_Click"
                                    CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
