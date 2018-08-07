<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="HelpMe.index" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sign In | HelpMe</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="Thu, 19 Nov 1900 08:52:00 GMT" />
    <!--Loading bootstrap css-->
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,800italic,400,700,800" />
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Oswald:400,700,300" />
    <link type="text/css" rel="stylesheet" href="Content/vendors/jquery-ui-1.10.3.custom/css/ui-lightness/jquery-ui-1.10.3.custom.html" />
    <link type="text/css" rel="stylesheet" href="Content/vendors/font-awesome/css/font-awesome.min.css" />
    <link type="text/css" rel="stylesheet" href="Content/vendors/bootstrap/css/bootstrap.min.css" />
    <!--Loading style vendors-->
    <link type="text/css" rel="stylesheet" href="Content/vendors/animate.css/animate.css" />
    <link type="text/css" rel="stylesheet" href="Content/vendors/iCheck/skins/all.css" />
    <!--Loading style-->
    <link type="text/css" rel="stylesheet" href="Content/css/themes/style1/pink-blue.css" class="default-style" />
    <link type="text/css" rel="stylesheet" href="Content/css/themes/style1/pink-blue.css" id="theme-change" class="style-change color-change" />
    <link type="text/css" rel="stylesheet" href="Content/css/style-responsive.css" />
    <link rel="shortcut icon" href="Content/images/icons/favicon.png" />
</head>
<body id="signin-page">
    <div class="page-form">
        <form id="form1" class="form" runat="server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <div class="header-content">
                <img class="img-responsive" src="Content/images/icons/User.png" style="margin:0 auto; height:90px" />                
            </div>
            <div class="body-content">
                <div class="note note-danger" id="divmsg" runat="server" visible="false">
                    <h5 class="box-heading">
                        <asp:Label ID="Message" runat="server"></asp:Label></h5>
                </div>

                <div class="form-group">
                    <div class="input-icon right">
                        <i class="fa fa-user"></i>
                        <asp:TextBox ID="UserName" runat="server" Text="Admin" CssClass="form-control" placeholder="User Name"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-icon right">
                        <i class="fa fa-key"></i>
                        <asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group pull-left">
                    <div class="checkbox-list">
                        <label>
                            <asp:CheckBox ID="cbRemember" runat="server" />
                            &nbsp;Remember Password
                        </label>
                    </div>
                </div>
                <div class="form-group pull-right">
                    <asp:LinkButton ID="btnLogin" runat="server" Text="Sign in" OnClick="btnLogin_Click" class="btn btn-success">
                        Log In &nbsp;<i class="fa fa-chevron-circle-right"></i>
                    </asp:LinkButton>
                </div>
                <div class="clearfix">
                </div>
                <div class="forget-password">
                    <h4>Forgotten your Password?</h4>
                    <p>no worries, click <a href='#' class='btn-forgot-pwd'>here</a> to reset your password.</p>
                </div>
            </div>
        </form>
    </div>
    <script src="Content/js/jquery-1.10.2.min.js"></script>
    <script src="Content/js/jquery-migrate-1.2.1.min.js"></script>
    <script src="Content/js/jquery-ui.js"></script>
    <!--loading bootstrap js-->
    <script src="Content/vendors/bootstrap/js/bootstrap.min.js"></script>
    <script src="Content/vendors/bootstrap-hover-dropdown/bootstrap-hover-dropdown.js"></script>
    <script src="Content/js/html5shiv.js"></script>
    <script src="Content/js/respond.min.js"></script>
    <script src="Content/vendors/iCheck/icheck.min.js"></script>
    <script src="Content/vendors/iCheck/custom.min.js"></script>
    <script>//BEGIN CHECKBOX & RADIO
        $('input[type="checkbox"]').iCheck({
            checkboxClass: 'icheckbox_minimal-grey',
            increaseArea: '20%' // optional
        });
        $('input[type="radio"]').iCheck({
            radioClass: 'iradio_minimal-grey',
            increaseArea: '20%' // optional
        });
        //END CHECKBOX & RADIO</script>
</body>
</html>
