<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupMemberCreate.aspx.cs"
    Inherits="WebCus.PopupMemberCreate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
                var obj = document.getElementById('<%=btnSubmitReg.ClientID %>');
                obj.click();
                return false;
            }
        }
        
    </script>
    <%--<script type="text/javascript">
        
        function validatePassConfig(oSrc, args) {
            var a = $('#<%=txtPassword.ClientID %>').val();
            var b = args.Value;

            args.IsValid = (a == b);
        }
    </script>--%>
    </head>
<body>
    <form id="form1" runat="server">
    
    
    <div>
        <div class="css_bgCateTitle">
            Đăng ký thành viên
        </div>
        <div class="css_hr_border">
            <div class="maximg_prodetail css_description clearfix defaulLink" style="padding: 10px;">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSubmitReg">
                    <center>
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Họ và tên:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUserName" MaxLength="50" Width="250px" CssClass="txtBox_border"
                                        runat="server"></asp:TextBox>
                                    <span style="color: red;">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtUserName"
                                        SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server" ErrorMessage='Chưa nhập họ và tên!'></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Tên đăng nhập:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txt_loginid" MaxLength="21" Width="250px" CssClass="txtBox_border"
                                        runat="server"></asp:TextBox>
                                    <span style="color: red;">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_loginid"
                                        SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server" ErrorMessage='Chưa nhập tên đăng nhập!'></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txt_loginid"
                                        ValidationExpression="^[a-zA-Z0-9_]{6,20}$" runat="server" Display="None" SetFocusOnError="true"
                                        ValidationGroup="reg" ErrorMessage='Tên đăng nhập Không được nhỏ hơn 6 ký tự và lớn hơn 20 ký tự'>
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Mật khẩu:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPassword" MaxLength="20" CssClass="txtBox_border" Width="250px"
                                        TextMode="Password" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPassword"
                                        SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server" ErrorMessage='Chưa nhập mật khấu!'></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPassword"
                                        ValidationExpression="^[a-zA-Z0-9_]{6,20}$" runat="server" Display="None" SetFocusOnError="true"
                                        ValidationGroup="reg" ErrorMessage='Mật khẩu Không được nhỏ hơn 6 ký tự và lớn hơn 20 ký tự'>
                                    </asp:RegularExpressionValidator>
                                    <span style="color: red;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Xác nhận lại mật khẩu:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPasswordConfirm" MaxLength="50" CssClass="txtBox_border" Width="250px"
                                        TextMode="Password" runat="server"></asp:TextBox>
                                    <span style="color: red;">*</span>
                                    <%--<asp:CustomValidator ID="CustomValidator1" Display="None" runat="server" ValidationGroup="reg"
                                        ControlToValidate="txtPasswordConfirm" ErrorMessage="Mật khẩu xác nhận không đúng"
                                        ClientValidationFunction="validatePassConfig"></asp:CustomValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Email:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmail" MaxLength="50" Width="250px" CssClass="txtBox_border"
                                        runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEmail"
                                        SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server" ErrorMessage='Chưa nhập địa chỉ email!'></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEmail"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                                        Display="None" SetFocusOnError="true" ValidationGroup="reg" ErrorMessage='Email không hợp lệ'>
                                    </asp:RegularExpressionValidator>
                                    <span style="color: red;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Số điện thoại:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTel" MaxLength="50" Width="250px" CssClass="txtBox_border" runat="server"></asp:TextBox>
                                    <span style="color: red;">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtTel"
                                        SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server" ErrorMessage='Chưa nhập số điện thoại!'></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Địa chỉ:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAddress" MaxLength="400" CssClass="txtBox_border" Width="250px"
                                        runat="server"></asp:TextBox>
                                    <span style="color: red;">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtAddress"
                                        SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server" ErrorMessage='Chưa nhập địa chỉ!'></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Giới tính:
                                </td>
                                <td align="left">
                                    <asp:RadioButton ID="rdoGender_Male" runat="server" Text="Nam" GroupName="Gender"
                                        Checked="true" />
                                    &nbsp;
                                    <asp:RadioButton ID="rdoGender_Female" runat="server" Text="Nữ" GroupName="Gender" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" style="padding-top: 5px; padding-right: 5px;">
                                    <%=this.ClientLanguageMsg("lngVerify")%>:
                                </td>
                                <td align="left" style="padding-bottom: 3px;">
                                    <asp:TextBox ID="txtVerifyCode" CssClass="txtBox_border" Height="20px" Width="300px"
                                        runat="server"></asp:TextBox>
                                    <span style="color: #fff;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Image ID="Image2" runat="server" ImageUrl="/ImageValidator.aspx?code=0" Width="80px"
                                        Height="20px" Style="padding-right: 10px; margin: 0;" />
                                    <asp:ImageButton ID="btnChangeImg" runat="server" ImageUrl="/Images/icon/refresh_icon.gif"
                                        OnClientClick="return reloadImg();" Style="" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" style="padding-top: 10px;">
                                    <asp:ValidationSummary ShowSummary="false" ShowMessageBox="true" ValidationGroup="reg"
                                        ID="ValidationSummary2" runat="server" />
                                    <asp:LinkButton ID="btnSubmitReg" Width="80px" ValidationGroup="reg" OnClick="btnSubmit_Click"
                                        CssClass="css_btnCart" runat="server">
                                        Đăng ký
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </center>
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
