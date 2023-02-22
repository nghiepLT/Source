<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupMemberResetPass.aspx.cs"
    Inherits="WebCus.PopupMemberResetPass" %>

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
                var obj = document.getElementById('<%=btnRecovery.ClientID %>');
                obj.click();
                return false;
            }

        }
        
    </script>
<body>
    <form id="form1" runat="server">
    
    <div>
        <div class="css_bgCateTitle">
            Quên mật khẩu
        </div>
        <div class="css_hr_border">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRecovery" Style="padding: 10px;">
                <center>
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="right" style="padding-bottom: 10px" class="color_02">
                                Tên đăng nhập:
                            </td>
                            <td align="left" style="padding-bottom: 10px">
                                <asp:TextBox ID="txt_loginID" CssClass="txtBox_border" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="lblMsgLogin" runat="server" Text="*" Style="color: Red;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-bottom: 10px" class="color_02">
                                Email:
                            </td>
                            <td align="left" style="padding-bottom: 10px">
                                <asp:TextBox ID="txtEmail" CssClass="txtBox_border" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="lblMsg" runat="server" Text="*" Style="color: Red;"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEmail"
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                                    SetFocusOnError="true" ValidationGroup="reg" ErrorMessage='Email không hợp lệ. '>
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-bottom: 10px" class="color_02">
                                Mã xác nhận:
                            </td>
                            <td align="left" style="padding-bottom: 10px">
                                <asp:TextBox ID="txtVerifyCode" CssClass="txtBox_border" runat="server" Width="200px"></asp:TextBox>
                                <asp:Label ID="lblMsgCode" runat="server" Text="*" Style="color: Red;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Image ID="Image2" runat="server" ImageUrl="/ImageValidator.aspx?code=0" Width="80px"
                                    Height="20px" Style="padding-right: 10px; margin: 0;" />
                                <asp:ImageButton ID="btnChangeImg" runat="server" ImageUrl="/Images/icon/refresh_icon.gif"
                                    OnClientClick="return reloadImg();" Style="" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="padding-top: 5px;">
                                <asp:ValidationSummary ShowSummary="false" ShowMessageBox="false" ValidationGroup="reg"
                                    ID="ValidationSummary2" runat="server" />
                                <asp:LinkButton ID="btnRecovery" Width="80px" OnClick="btnCreate_Click" ValidationGroup="reg"
                                    CssClass="css_btnCart" runat="server">
                                        Xác nhận
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
