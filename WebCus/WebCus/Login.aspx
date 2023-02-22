<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebCus.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::HR System-NKCN::</title>
    <link href="App_Themes/Gray/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <%--<script type="text/javascript">
         window.location.href = "baotri.html";
    </script>--%>
    <script type="text/javascript">
        window.oncontextmenu = function () {
            return false;
        }
        $(document).ready(function () {
            $(document).keydown(function (event) {
                if (event.keyCode == 13) {
                    alert("ok");
                    return false;
                }
                else if (event.keyCode == 123) {
                    return false;
                }
                else if ((event.ctrlKey && event.shiftKey && event.keyCode == 73) || (event.ctrlKey && event.shiftKey && event.keyCode == 74)) {
                    return false;
                }
                else return;

            });
        });        
    </script>
    <%--<script src="js/image_snow/it.snow.js"></script>--%>
    <%--<script type="text/javascript">
        document.write('<style>body{padding-bottom:20px}#e_itexpress_left{display:none;position:fixed;z-index:9999;top:0;left:0}#e_itexpress_right{display:none;position:fixed;z-index:9999;top:0;right:0}#e_itexpress_footer{display:none;position:fixed;z-index:9999;bottom:-50px;left:0;width:100%;height:104px;background:url(js/image_snow/ft.png) repeat-x bottom left}#e_itexpress_bottom_left{display:none;position:fixed;z-index:9999;bottom:20px;left:20px}@media (min-width: 992px){#e_itexpress_left,#e_itexpress_right,#e_itexpress_footer,#e_itexpress_bottom_left{display:block}}</style><img id="e_itexpress_left" src=""/><img id="e_itexpress_right" src=""/><div id="e_itexpress_footer"></div><img id="e_itexpress_bottom_left" src="js/image_snow/bottomleft.png"/><div style="position:fixed;z-index:9999;bottom:3px;right:3px; font-size:12px;color:#8D8D8D;">by Nhân Nguyễn</div>');
        var no = 50; var hidesnowtime = 0; var snowdistance = 'pageheight'; var ie4up = (document.all) ? 1 : 0; var ns6up = (document.getElementById && !document.all) ? 1 : 0; function iecompattest() { return (document.compatMode && document.compatMode != 'BackCompat') ? document.documentElement : document.body } var dx, xp, yp; var am, stx, sty; var i, doc_width = 800, doc_height = 600; if (ns6up) { doc_width = self.innerWidth; doc_height = self.innerHeight } else if (ie4up) { doc_width = iecompattest().clientWidth; doc_height = iecompattest().clientHeight } dx = new Array(); xp = new Array(); yp = new Array(); am = new Array(); stx = new Array(); sty = new Array(); for (i = 0; i < no; ++i) { dx[i] = 0; xp[i] = Math.random() * (doc_width - 50); yp[i] = Math.random() * doc_height; am[i] = Math.random() * 20; stx[i] = 0.02 + Math.random() / 10; sty[i] = 0.7 + Math.random(); if (ie4up || ns6up) { document.write('<div id="dot' + i + '" style="POSITION:absolute;Z-INDEX:' + i + ';VISIBILITY:visible;TOP:15px;LEFT:15px;"><span style="font-size:18px;color:#fff">*</span></div>') } }
        function snowIE_NS6() { doc_width = ns6up ? window.innerWidth - 10 : iecompattest().clientWidth - 10; doc_height = (window.innerHeight && snowdistance == 'windowheight') ? window.innerHeight : (ie4up && snowdistance == 'windowheight') ? iecompattest().clientHeight : (ie4up && !window.opera && snowdistance == 'pageheight') ? iecompattest().scrollHeight : iecompattest().offsetHeight; for (i = 0; i < no; ++i) { yp[i] += sty[i]; if (yp[i] > doc_height - 50) { xp[i] = Math.random() * (doc_width - am[i] - 30); yp[i] = 0; stx[i] = 0.02 + Math.random() / 10; sty[i] = 0.7 + Math.random() } dx[i] += stx[i]; document.getElementById('dot' + i).style.top = yp[i] + 'px'; document.getElementById('dot' + i).style.left = xp[i] + am[i] * Math.sin(dx[i]) + 'px' } snowtimer = setTimeout('snowIE_NS6()', 10) } function hidesnow() { if (window.snowtimer) { clearTimeout(snowtimer) } for (i = 0; i < no; i++) document.getElementById('dot' + i).style.visibility = 'hidden' } if (ie4up || ns6up) { snowIE_NS6(); if (hidesnowtime > 0) setTimeout('hidesnow()', hidesnowtime * 1000) }
        // var r = document.createElement("script"); r.type = "text/javascript"; r.async = true; r.src = n + "//itexpress.vn/js/popup_newtab_time.js";
    </script>--%>
    <style type="text/css">
        .BoxLogin
        {
            /*    background:rgba(4, 4, 4, 0.49);
                
            background: url( 'admincss/bkg_main.gif' );*/
        }
        .VIEW_table2, .VIEW_table
        {
            background: none !important;
            border-color: rgba(255, 255, 255, 0) !important;
        }
        
        .btn_login
        {
            background: #3498db;
            background-image: -webkit-linear-gradient(top, #3498db, #2980b9);
            background-image: -moz-linear-gradient(top, #3498db, #2980b9);
            background-image: -ms-linear-gradient(top, #3498db, #2980b9);
            background-image: -o-linear-gradient(top, #3498db, #2980b9);
            background-image: linear-gradient(to bottom, #3498db, #2980b9);
            -webkit-border-radius: 28;
            -moz-border-radius: 28;
            border-radius: 28px;
            font-family: Arial;
            color: #ffffff;
            font-size: 20px;
            padding: 5px 45px;
            text-decoration: none;
        }
        
        .btn_login:hover
        {
            background: #3cb0fd;
            background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
            background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
            text-decoration: none;
            cursor: pointer;
        }
        .tb5_input
        {
            border: 2px solid #456879;
            border-radius: 10px;
            height: 22px;
            width: 230px;
        }
        #hexagon
        {
            width: 434px;
            height: 200px;
            background: rgba(4, 4, 4, 0.49);
            position: relative;
        }
        #hexagon:before
        {
            content: "";
            position: absolute;
            top: -100px;
            left: 0;
            width: 0;
            height: 0;
            border-left: 218px solid transparent;
            border-right: 218px solid transparent;
            border-bottom: 100px solid rgba(4, 4, 4, 0.49);
        }
        #hexagon:after
        {
            content: "";
            position: absolute;
            bottom: -100px;
            left: 0;
            width: 0;
            height: 0;
            border-left: 218px solid transparent;
            border-right: 218px solid transparent;
            border-top: 100px solid rgba(4, 4, 4, 0.49);
        }
        
        .bgbody
        {
            background: url('Styles/Images/bg_new.jpg') no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }
    </style>
</head>
<body class="bgbody">
    <form id="form1" runat="server">
    <div align="center" style="padding-top: 20px">
        <table border="0" style="border-collapse: collapse" width="100%" bordercolor="#C0C0C0"
            id="table2" height="520px">
            <tr>
                <td class="C">
                    <table id="Table1" border="0" bordercolor="#000000" style="border-collapse: collapse"
                        width="100%">
                        <tr>
                            <td>
                            </td>
                            <td id="hexagon" valign="bottom" width="400px">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="VIEW_table pd5">
                                    <tr>
                                        <td class="VIEW_table2 pd5">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <%--<tr>
                                                    <td class="pdB5 L">
                                                        <img src="AdminCss/logo.png" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="L pdT10">
                                                        <asp:Button ID="Button1" runat="server" BackColor="InactiveCaptionText" BorderStyle="Solid"
                                                            BorderWidth="1px" Text="Check In Out"  CssClass="btnSelect" 
                                                            onclick="Button1_Click" />
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="BoxLogin" height="220px">
                                                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="#C00000"></asp:Label>
                                                        <asp:Login ID="Login1" runat="server" Font-Names="Arial" ForeColor="White" LoginButtonText="Sign In"
                                                            TitleText="" OnLoggingIn="Login1_LoggingIn">
                                                            <LoginButtonStyle BackColor="InactiveCaptionText" BorderStyle="Solid" BorderWidth="1px" />
                                                            <LayoutTemplate>
                                                                <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                                                                    <tr>
                                                                        <td style="height: 130px">
                                                                            <table border="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td class="R pdR10" width="130px">
                                                                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Font-Bold="True"
                                                                                            Font-Size="9pt" Style="color: #fff;">User Name:</asp:Label>
                                                                                    </td>
                                                                                    <td style="height: 30px">
                                                                                        <asp:TextBox ID="UserName" runat="server" Width="150px" CssClass="tb5_input" autocomplete="off"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="R pdR10">
                                                                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Font-Bold="True"
                                                                                            Font-Size="9pt" Style="color: #fff;">Password:</asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left; height: 30px;">
                                                                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px" CssClass="tb5_input"
                                                                                            autocomplete="off"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" colspan="2" style="color: red">
                                                                                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td class="L pdT10">
                                                                                        <asp:Button ID="LoginButton" runat="server" BackColor="InactiveCaptionText" BorderStyle="Solid"
                                                                                            BorderWidth="1px" CommandName="Login" Text="Sign In" ValidationGroup="Login1"
                                                                                            CssClass="btn_login" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </LayoutTemplate>
                                                        </asp:Login>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <div class="pd10">
                        <span style="font-size: 9pt; color: #fff; font-family: Arial">CMS HR since 2018 by Nhân Nguyễn</span>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
