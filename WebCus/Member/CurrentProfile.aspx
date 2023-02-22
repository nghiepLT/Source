<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="CurrentProfile.aspx.cs" Inherits="WebCus.CurrentProfile" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <title>Thông tin thành viên</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function SetFocus(e) {
            var keynum;
            if (window.event) // IE
                keynum = e.keyCode;
            else if (e.which) // Netscape/Firefox/Opera
                keynum = e.which;
            if (keynum == 13) {
                var obj = document.getElementById('<%=btnSubmit.ClientID %>');
                obj.click();
                return false;
            }

        }
        
    
    </script>
    <div class="clearfix">
        <div class="css_rel">
            <div class="css_title">
                <h1>
                    Thông tin thành viên
                </h1>
            </div>
            <div class="css_hrBt">
            </div>
        </div>
        <div class="clearfix css_mrTopHome">
            <div class="maximg_content css_description clearfix defaulLink maximg_video" style="padding: 0 10px 10px;">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSubmit">
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
                                    <asp:Label ID="lbl_loginID" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Mật khẩu cũ:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPasswordold" MaxLength="20" CssClass="txtBox_border" Width="250px"
                                        TextMode="Password" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPasswordold"
                                        ValidationExpression="^[a-zA-Z0-9_]{6,20}$" runat="server" Display="None" SetFocusOnError="true"
                                        ValidationGroup="reg" ErrorMessage='Mật khẩu Không được nhỏ hơn 6 ký tự và lớn hơn 20 ký tự'>
                                    </asp:RegularExpressionValidator>
                                    <asp:Label ID="lbl_Mass_passOld" Style="color: red;" runat="server" Text="*"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Mật khẩu mới:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPasswordNew" MaxLength="20" CssClass="txtBox_border" Width="250px"
                                        TextMode="Password" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPasswordNew"
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
                                    <asp:Label ID="lbl_mspass" Style="color: red;" runat="server" Text="*"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Nick Yahoo:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtYahoo" MaxLength="100" CssClass="txtBox_border" Width="250px"
                                        runat="server"></asp:TextBox>
                                    <span>@yahoo.com</span>
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtYahoo"
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                                    Display="None" SetFocusOnError="true" ValidationGroup="reg" ErrorMessage='Yahoo không hợp lệ. Vd: abc@yahoo.com hoặc abcd@yahoo.com.vn'>
                                </asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Email:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lbl_email" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Số điện thoại:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTel" MaxLength="50" Width="250px" CssClass="txtBox_border" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTel"
                                        SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server" ErrorMessage='Chưa nhập số điện thoại!'></asp:RequiredFieldValidator>
                                    <span style="color: red;">*</span>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAddress"
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
                                <td align="right" style="padding-bottom: 10px; padding-right: 5px;" class="color_02">
                                    Mã xác nhận:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtVerifyCode" CssClass="txtBox_border" Width="250px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lbl_codeimage" Style="color: red;" runat="server" Text="*"></asp:Label>
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
                                    <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn03"
                                        ValidationGroup="reg">
                                    Gửi
                                    </asp:LinkButton>
                                    <%--<cc1:PQTButton ID="btnSubmit" runat="server" ValidationGroup="reg" OnClientClick="return CheckValid();"
                                    OnClick="btnSubmit_Click" LanguageRef="lngReg" Height="26px"></cc1:PQTButton>--%>
                                    <%--<asp:LinkButton ></asp:LinkButton>--%>
                                </td>
                            </tr>
                        </table>
                    </center>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
