<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="MemberLogin.aspx.cs" Inherits="WebCus.MemberLogin" %>

<%@ Register Src="../ASCX/Content.ascx" TagName="Content" TagPrefix="uc1" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <title>Đăng nhập</title>
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

        .btn_login button:hover {
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
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="container">
        <div class="wrap">
            <div class="clearfix" style="float: none;">
                <h3><%=this.ClientLanguageMsg("lnglogin")%></h3>
                <div class="col-md-6 col-lg-6 col-xs-12 col-sm-12">
                    <div class="form-group">
                        <span class="fa fa-user "></span>
                        <asp:TextBox ID="txtUserID" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <span class="fa fa-lock "></span>
                        <asp:TextBox ID="txtPass" CssClass="form-control" TextMode="Password" Text="Password" runat="server">
                        </asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label></label>
                        <a href="/quen-mat-khau"><%=this.ClientLanguageMsg("lngLostPass") %> ? </a>
                    </div>
                   
                    <div class="form-group">
                        <asp:LinkButton ID="btnLogin" CssClass=" btn-new btn-info" OnClick="btnLogin_Click" runat="server">
                                        <%=this.ClientLanguageMsg("lnglogin") %>
                        </asp:LinkButton>
                    </div>
                     <div class="form-group">
                        <label></label>
                        <asp:Label ID="lblAlert" runat="server" Style="color: Red;" Text="" Visible="false"></asp:Label>
                    </div>

                    <%--             <div class="form-group" style="margin-top:20px;">
                    <h4>
                        Bạn có thể đăng nhập qua</h4>
                    <p>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                            <img src="/Styles/LocNhung/images/login-face.png" />
                        </asp:LinkButton>
                    </p>
                    <p>
                        <a href="#">
                            <img src="/Styles/LocNhung/images/login-google.png" /></a></p>
                </div>--%>
                </div>
                <div class="col-md-6 col-lg-6 col-xs-12 col-sm-12">
                    <div class="form-group">
                            <h4><%=this.ClientLanguageMsg("lngNotHasAccount") %></h4>
                        </div>
                    <div class="form-group btn_login">
                        <asp:LinkButton ID="btn_reg" OnClick="btnReg_Click" CssClass=" btn-new" runat="server">
                                        <%=this.ClientLanguageMsg("lngReg") %>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
