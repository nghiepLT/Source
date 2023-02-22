<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="PasswordRecovery.aspx.cs" Inherits="WebCus.PasswordRecovery" %>

<%@ Register Src="../ASCX/Content.ascx" TagName="Content" TagPrefix="uc1" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <title>Quên mật khẩu</title>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
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
     <div class="container">
        <div class="col-md-6 col-md-offset-3 col-xs-12">
            <h1 style="text-align: center; margin-bottom: 20px;">Quên mật khẩu</h1>

             <div class="form-group">
                <label>   Tên đăng nhập:</label>
                 <asp:TextBox ID="txt_loginID"  CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:Label ID="lblMsgLogin" runat="server" Text="*" Style="color: Red;" Visible="false"></asp:Label>
             </div>

             <div class="form-group">
                <label>    Email:</label>
                 <asp:TextBox ID="txtEmail"  CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:Label ID="lblMsg" runat="server" Text="*" Style="color: Red;" Visible="false"></asp:Label>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEmail"
                     ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                     SetFocusOnError="true" ValidationGroup="reg" ErrorMessage='Email không hợp lệ. '>
                 </asp:RegularExpressionValidator>
             </div>

             <div class="form-group">
                 <label>Mã xác nhận:</label>
                 <asp:TextBox ID="txtVerifyCode"  CssClass="form-control" runat="server"></asp:TextBox>
             </div>

            
             <div class="form-group">
                 <label></label>
                 <asp:Image ID="Image2" runat="server" ImageUrl="/ImageValidator.aspx?code=0" Width="80px"
                     Height="20px" Style="padding-right: 10px; margin: 0;" />
                 <asp:ImageButton ID="btnChangeImg" runat="server" ImageUrl="/Images/icon/refresh_icon.gif"
                     OnClientClick="return reloadImg();" Style="" />
                 <asp:Label ID="lblMsgCode" runat="server" Text="*" Style="color: Red;" Visible="false"></asp:Label>
             </div>


            <div class="form-group">
                <asp:ValidationSummary ShowSummary="false" ShowMessageBox="false" ValidationGroup="reg"
                    ID="ValidationSummary2" runat="server" />
                <%--<cc1:PQTButton ID="" Style="cursor: pointer;" OnClick="btnCreate_Click" CssClass="btn_bg"
                                runat="server"  Text="Gửi"></cc1:PQTButton>--%>
                <asp:LinkButton ID="btnRecovery" OnClick="btnCreate_Click" ValidationGroup="reg" CssClass="btn btn-info" runat="server">
                                        Xác nhận
                </asp:LinkButton>
             </div>

        </div>
    </div>
</asp:Content>
