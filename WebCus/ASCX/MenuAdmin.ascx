<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuAdmin.ascx.cs" Inherits="WebCus.ASCX.MenuAdmin" %>
<link href="/AdminCss/admin-menu.css" rel="stylesheet" type="text/css">
<script src="/AdminCss/admin-menu.js" type="text/javascript"></script>
<div class="left-menu-title C">
    <span style="font-weight: bold;">MENU</span>
</div>
<ul id="menu">
    <asp:Repeater ID="rptMenu" runat="server" OnItemDataBound="rptMenu_ItemDataBound">
        <ItemTemplate>
            <li><a href='<%#changerlink(Eval("Link")) %>'>
                <%#Eval("MenuName") %></a>
                <ul>
                    <asp:Repeater ID="rptSubMenu" runat="server" OnItemDataBound="rptMenu_ItemDataBound">
                        <ItemTemplate>
                            <li><a href='<%#changerlink(Eval("Link"))%>'>
                                <%#Eval("MenuName") %></a>
                                <ul runat="server">
                                    <asp:Repeater ID="rptSubMenu" runat="server">
                                        <ItemTemplate>
                                            <li><a href='<%#changerlink(Eval("Link"))%>'>
                                                <%#Eval("MenuName") %></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </li>
        </ItemTemplate>
    </asp:Repeater>
    <li>
        <a href="/sinhnhatnv.aspx">Sinh nhật NV</a>
       <%-- <%=Session["codesecret"].ToString() %>--%>
       <%-- <%=WebCus.Utility.Encrypt(Session["codesecret"].ToString()) %>
        <%=WebCus.Utility.Decrypt("7do0pgA9HjUtZlqqHVZU9NoaABXaYRpA") %>--%>
    </li>
    <li>
        <a href="/Logout.aspx">Đăng xuất</a>
    </li>
    <li id="menuSupperAdmin" runat="server"><a href="#" rel="subMenuTypeAdmin">Supper Admin</a>
        <ul id="subMenuTypeAdmin">
            <li><a href="/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=MenuAdminMng">
                Menu Admin </a></li>
            <li><a href="/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=TypeMng">
                Type </a></li>
            <li><a href="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=LanguageMng">
                Language Mng </a></li>
                 <li><a href="/baotri.aspx">
                Mode Bảo Trì </a></li>
        </ul>
    </li>
</ul>
<br />
<div style="background:#CCC;color:Red;text-align:center;">
<asp:Label ID="lbl_nameUSer" runat="server"></asp:Label>
</div>
<script type="text/javascript">
    function initMenu() {
        $('#menu ul').hide();
        $('#menu li a').click(function () {$(this).next().slideToggle('normal');});
    }
</script>
<%--<div style="padding:10px;">
    <center>
        <table border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th style="text-align:right !important;">
                    Đang online:
                </th>
                <td style="text-align:left !important;">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="text-align:right !important;">
                    Lượt truy cập:
                </th>
                <td style="text-align:left !important;">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="text-align:right !important;">
                    Hôm nay:
                </th>
                <td style="text-align:left !important;">
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </center>
</div>--%>