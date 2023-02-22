<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupMemberEdit.aspx.cs"
    Inherits="WebCus.PopupMemberEdit" %>

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
            var obj = document.getElementById('<%=btnEditProfile.ClientID %>');
            obj.click();
            return false;
        }

    }
</script>

<script type="text/javascript">
    function validatePassConfig(oSrc, args) {
        var a = $('#<%=txt_passnew.ClientID %>').val();
        var b = args.Value;

        args.IsValid = (a == b);
    }
</script>
<body>
    <form id="form1" runat="server">
    
    <!--  -->
    <div>
        <div class="css_bgCateTitle">
            Thông tin thành viên
        </div>
        <div class="css_hr_border">
            <div id="div_info">
                <div class="maximg_prodetail css_description clearfix defaulLink" style="padding: 10px;">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnEditProfile">
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
                                        <asp:Label ID="lbl_tendangnhap" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" style="padding-top: 5px; padding-right: 5px;">
                                        Mật khẩu cũ:
                                    </td>
                                    <td align="left" style="padding-bottom: 3px;">
                                        <asp:TextBox ID="txt_passold" MaxLength="20" CssClass="txtBox_border" Width="250px"
                                            TextMode="Password" runat="server"></asp:TextBox>
                                        <span style="color: red;">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_passold"
                                            SetFocusOnError="true" Display="None" ValidationGroup="Pass" runat="server" ErrorMessage='Chưa nhập mật khấu!'></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" style="padding-top: 5px; padding-right: 5px;">
                                        Mật khẩu mới:
                                    </td>
                                    <td align="left" style="padding-bottom: 3px;">
                                        <asp:TextBox ID="txt_passnew" MaxLength="20" CssClass="txtBox_border" Width="250px"
                                            TextMode="Password" runat="server"></asp:TextBox>
                                        <span style="color: red;">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_passnew"
                                            SetFocusOnError="true" Display="None" ValidationGroup="Pass" runat="server" ErrorMessage='Chưa nhập mật khấu mới!'></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txt_passnew"
                                            ValidationExpression="^[a-zA-Z0-9_]{6,20}$" runat="server" Display="None" SetFocusOnError="true"
                                            ValidationGroup="Pass" ErrorMessage='Mật khẩu Không được nhỏ hơn 6 ký tự và lớn hơn 20 ký tự'>
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                        Xác nhận lại mật khẩu:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_passnewconfirm" MaxLength="50" CssClass="txtBox_border" Width="250px"
                                            TextMode="Password" runat="server"></asp:TextBox>
                                        <span style="color: red;">*</span>
                                        <asp:CustomValidator ID="CustomValidator1" Display="None" runat="server" ValidationGroup="Pass"
                                            ControlToValidate="txt_passnewconfirm" ErrorMessage="Mật khẩu xác nhận không đúng"
                                            ClientValidationFunction="validatePassConfig"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                        Email:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
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
                                        Mã xác nhận:
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
                                        <%--<cc1:PQTButton ID="" runat="server" ValidationGroup=""
                                    OnClick="" Height="26px" Text="Cập nhật"></cc1:PQTButton>--%>
                                        <asp:LinkButton ID="btnEditProfile" Width="80px" OnClick="btnCreate_Click" ValidationGroup="reg"
                                            CssClass="css_btnCart" runat="server">
                                        Cập nhật
                                        </asp:LinkButton>
                                        <%--<asp:LinkButton ></asp:LinkButton>--%>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    
    </form>
</body>
</html>
