<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="RegisAccount.aspx.cs" Inherits="WebCus.RegisAccount" %>

<%@ Register Src="../ASCX/Content.ascx" TagName="Content" TagPrefix="uc1" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <title>Đăng ký</title>
       <link href="/Styles/font-awesome-4.4.0/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .container .wrap {
            width: 100%;
            max-width: 730px;
            padding: 15px;
            margin: 20px auto 50px;
            border: solid #ddd;
            font-size: 14px;
            border-width: 1px 1px 3px;
        }

        .wrap h3 {
            font-weight: 500 !important;
            border-bottom: 2px solid #D7D7D7 !important;
            padding-bottom: 10px;
            text-transform: uppercase;
            background:none !important;
            font-size:24px !important;
        }
        .wrap h4 {
            text-transform: uppercase;
            text-align:center;
        }

        .form-group {
            margin: 20px 0;
            position: relative;
        }

        .form-control {
            background-color: #EDEDED;
            text-indent: 40px;
            font-family: FontAwesome;
            height: 38px;
            padding: 0;
            border: 0;
            border-radius: 3px;
        }

        .btn-new {
            padding: 6px 30px;
            font-size: 17px !important;
            width: 60%;
        }

        .btn_login {
            text-align: center;
        }

        .btn_login a {
            background: #F39300;
            color: #fff;
        }

        .btn_login a:hover {
            color: #fff;
        }

        .fa {
            position: absolute;
            margin-left: 10px;
            margin-top: 10px;
            font-size: 16px;
            color: #b0b0b0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
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
    <script type="text/javascript">

        function validatePassConfig(oSrc, args) {
            var a = $('#<%#txtPassword.ClientID %>').val();
            var b = args.Value;

            args.IsValid = (a == b);
        }
    </script>
      <div class="container">
        <div class="wrap">

            <div class="clearfix" style="float: none;">
                <h3><%=this.ClientLanguageMsg("lngRegAccount")%></h3>
                <div class="col-md-6 col-lg-6 col-xs-12 col-sm-12">
                    <div class="form-group">
                        <span class="fa fa-user "></span>
                        <asp:TextBox ID="txtUserName" CssClass="form-control" required runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtUserName"
                            SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <span class="fa fa-user "></span>
                        <asp:TextBox ID="txt_loginid" CssClass="form-control"
                            runat="server" required></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_loginid"
                            SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txt_loginid"
                            ValidationExpression="^[a-zA-Z0-9_]{6,20}$" runat="server" Display="None" SetFocusOnError="true"
                            ValidationGroup="reg" ErrorMessage='Tên đăng nhập Không được nhỏ hơn 6 ký tự và lớn hơn 20 ký tự'>
                        </asp:RegularExpressionValidator>

                    </div>
                    
                    <div class="form-group">
                        <span class="fa fa-lock "></span>
                        <asp:TextBox ID="txtPassword" required CssClass="form-control"
                            TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPassword"
                            SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPassword"
                            ValidationExpression="^[a-zA-Z0-9_]{6,20}$" runat="server" Display="None" SetFocusOnError="true"
                            ValidationGroup="reg">
                        </asp:RegularExpressionValidator>

                    </div>

                    <div class="form-group">
                        <span class="fa fa-lock "></span>
                        <asp:TextBox ID="txtPasswordConfirm" CssClass="form-control"
                            TextMode="Password" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="reg"
                            SetFocusOnError="true" ControlToCompare="txtPassword"
                            ControlToValidate="txtPasswordConfirm"></asp:CompareValidator>
                    </div>

                    <div class="form-group">
                        <span class="fa fa-envelope-o "></span>
                        <asp:TextBox ID="txtEmail" CssClass="form-control"
                            runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEmail"
                            SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server"
                            Display="None" SetFocusOnError="true" ValidationGroup="reg">
                        </asp:RegularExpressionValidator>
                    </div>

                    <div class="form-group">
                        <span class="fa fa-phone "></span>
                        <asp:TextBox ID="txtTel" required CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtTel"
                            SetFocusOnError="true" Display="none" ValidationGroup="reg" runat="server"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group ">
                        <span class="fa fa-home "></span>
                        <asp:TextBox ID="txtAddress" CssClass="form-control"
                            runat="server"></asp:TextBox>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtAddress"
                    SetFocusOnError="true" Display="None" ValidationGroup="reg" runat="server" ></asp:RequiredFieldValidator>--%>

                    </div>
                    <%-- 
            <div class="form-group ">
                <label><span style="color: red;">*</span> <%=this.ClientLanguageMsg("lngGender")%>:</label>
                <asp:RadioButton ID="rdoGender_Male" runat="server" Text="Nam" GroupName="Gender"
                    Checked="true" />
                &nbsp;
                <asp:RadioButton ID="rdoGender_Female" runat="server" Text="Nữ" GroupName="Gender" />

            </div>

            <div class="form-group ">
                <label><span style="color: red;">*</span><%=this.ClientLanguageMsg("lngVerify")%>:</label>
                <asp:TextBox ID="txtVerifyCode" CssClass="form-control"
                    runat="server"></asp:TextBox>

            </div>

            <div class="form-group   ">
                <label></label>
                <asp:Image ID="Image2" runat="server" ImageUrl="/ImageValidator.aspx?code=0" Width="80px"
                    Height="20px" Style="padding-right: 10px; margin: 0;" />
                <asp:ImageButton ID="btnChangeImg" runat="server" ImageUrl="/Images/icon/refresh_icon.gif"
                    OnClientClick="return reloadImg();" Style="" />

            </div>
                    --%>
                    <div class="form-group" style="text-align: center;">

                        <asp:ValidationSummary ShowSummary="false" ShowMessageBox="true" ValidationGroup="reg"
                            ID="ValidationSummary2" runat="server" />
                        <asp:LinkButton ID="btnSubmitReg" ValidationGroup="reg" OnClick="btnSubmit_Click" CssClass="btn-new  btn-info" runat="server">
                                        <%=this.ClientLanguageMsg("lngReg")%>
                        </asp:LinkButton>

                    </div>
                </div>
                .
                <div class="col-md-6 col-lg-6 col-xs-12 col-sm-12">
                    <div class="form-group">
                        <h4><%=this.ClientLanguageMsg("lngHasAccount") %></h4>
                    </div>
                    <div class="form-group btn_login">
                        <a class=" btn-new" href="/dang-nhap"><%=this.ClientLanguageMsg("lnglogin") %></a>
                    </div>
                </div>

            </div>

        </div>


    </div>
  
</asp:Content>
