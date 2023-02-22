<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupMemberLogin.aspx.cs"
    Inherits="WebCus.PopupMemberLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<link href="/default.css" rel="stylesheet" type="text/css" />
<link href="/Styles/BaTam/css/fancybox_popup.css" rel="stylesheet" type="text/css" />
<link href="/Styles/BaTam/css/reset.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function closeFancyBox() {
        parent.$.fancybox.close();
    }
    </script>
    <script type="text/javascript">
        function SetFocus(e) {
            var keynum;
            if (window.event) // IE
                keynum = e.keyCode;
            else if (e.which) // Netscape/Firefox/Opera
                keynum = e.which;
            if (keynum == 13) {
                var obj = document.getElementById('<%=btnLogin.ClientID %>');
                obj.click();
                return false;
            }

        }
    </script>
<body>
    <form id="form1" runat="server">
    
    
    <div>
        <div class="css_bgCateTitle">
            Đăng nhập
        </div>
        <div class="css_hr_border">
            <div class="maximg_prodetail css_description clearfix defaulLink" style="padding: 10px;">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">
                    <div class="maximg_content contenttext">
                        <center>
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="right" style="padding-bottom: 10px" class="color_02">
                                        Tên đăng nhập:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtUserID" CssClass="txtBox_border" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="color_02">
                                        <%=this.ClientLanguageMsg("lngPass")%>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPass" CssClass="txtBox_border" TextMode="Password" Text="Password"
                                            runat="server" Width="200px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="css_titleLink01">
                                        <a href="/MemberPopup/PopupMemberResetPass.aspx">Quên mật khẩu </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="padding-top: 20px;">
                                        <p>
                                            <asp:Label ID="lblAlert" runat="server" Style="color: Red;" Text=""></asp:Label>
                                        </p>
                                        <asp:LinkButton ID="btnLogin" Width="80px" OnClick="btnLogin_Click"
                                            CssClass="css_btnCart" runat="server">
                                        Đăng nhập
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btn_reg" PostBackUrl="/MemberPopup/PopupMemberCreate.aspx" Width="80px" CssClass="css_btnCart"
                                            runat="server">
                                        Đăng ký
                                        </asp:LinkButton> <%--OnClick="btnReg_Click"--%>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
