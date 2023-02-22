<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header01.ascx.cs" Inherits="WebCus.ASCX.Header01" %>

<script language="JavaScript" type="text/javascript">
    function ViewDiv(isView, idTd, index) {
        var divSubMenu = document.getElementById('divSubMenu' + index);
        if (divSubMenu != null) {
            var tdEvent = document.getElementById(idTd);
            var status = isView == 1 ? 'inline' : 'none';

            divSubMenu.style.display = status;
            divSubMenu.style.position = 'absolute';
            divSubMenu.style.top = tdEvent.offsetTop + 28 + 'px';
            divSubMenu.style.left = tdEvent.offsetLeft + 0 + 'px';
        }
        for (var i = 1; i <= 5; i++) {
            if (i != index) {
                var divSubMenuIndex = document.getElementById('divSubMenu' + i);
                if (divSubMenuIndex != null)
                    divSubMenuIndex.style.display = 'none';
            }
        }
    }

</script>

<table cellspacing="0" class="menudottedline" cellpadding="0" width="100%" border="0">
    <tbody>
        <tr>
            <td class="menubackgr">
                <table id="tblTopMenu" border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="TopMenu L" id="tdProduct" runat="server" onmouseover="ViewDiv(1,this.id, 1);">
                            <asp:HyperLink ID="HyperLink12" NavigateUrl="/RenderModule.aspx?smid=UnlockMng&md=UnlockControl.ascx&muid=ServiceList"
                                CssClass="TopMenuLink" runat="server">Service</asp:HyperLink>
                            <!--Sub menu Product-->
                            <div style="display: none; position: absolute" id="divSubMenu1" onmouseout="this.style.display='none'">
                                <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="SubMenu L" id="tdProductCategory" runat="server">
                                            <asp:HyperLink ID="HyperLink5" NavigateUrl="/RenderModule.aspx?smid=UnlockMng&md=UnlockControl.ascx&muid=ServiceOptionInfo"
                                                CssClass="SubMenuLink" runat="server">ServiceOptionInfo</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu L" id="tdProductList" runat="server">
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl="/RenderModule.aspx?smid=UnlockMng&md=UnlockControl.ascx&muid=ServiceGroupInfo"
                                                CssClass="SubMenuLink" runat="server">ServiceGroupInfo</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--End Sub menu Product-->
                        </td>
                        <td class="TopMenu" id="tdNews" runat="server" onmouseover="ViewDiv(1,this.id, 2);">
                            <asp:HyperLink ID="HyperLink9" NavigateUrl="#" CssClass="TopMenuLink" runat="server">Other</asp:HyperLink>
                            <!--Sub menu News-->
                            <div style="display: none; position: absolute" id="divSubMenu2" onmouseout="this.style.display='none'">
                                <table id="Table2" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="SubMenu L" >
                                            <a href="/RenderModule.aspx?smid=UnlockMng&md=UnlockControl.ascx&muid=BaseCode" class="TopMenuLink">
                                                BaseCode </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu L">
                                             <a href="/RenderModule.aspx?smid=UnlockMng&md=UnlockControl.ascx&muid=NetworkToCountry" class="TopMenuLink">
                                                Network To Country </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu L">
                                             <a href="/RenderModule.aspx?smid=UnlockMng&md=UnlockControl.ascx&muid=ModelInfo" class="TopMenuLink">
                                                Model</a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--End Sub menu News-->
                        </td>
                        <td class="TopMenu" id="td2" runat="server" onmouseover="ViewDiv(1,this.id, 2);">
                            <asp:HyperLink ID="HyperLink10" NavigateUrl="/RenderModule.aspx?smid=NewsMng&md=NewsControl.ascx&muid=NewsList&UK=Custommer&IS=1"
                                CssClass="TopMenuLink" runat="server">Customer</asp:HyperLink>
                        </td>
                        <td class="TopMenu" id="tdUser" runat="server" onmouseover="ViewDiv(1,this.id, -1);">
                            <asp:HyperLink ID="hplUserInfo" NavigateUrl="/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=UserInfo"
                                CssClass="TopMenuLink" runat="server">User</asp:HyperLink>
                        </td>
                        <td class="TopMenu" id="tdLanguage" runat="server" onmouseover="ViewDiv(1,this.id, -1);">
                            <asp:HyperLink ID="HyperLink4" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=LanguageMng"
                                CssClass="TopMenuLink" runat="server">Language</asp:HyperLink>
                        </td>
                        <td class="TopMenu L" onmouseover="ViewDiv(1,this.id, -1);">
                            <asp:HyperLink ID="HyperLink23" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=OrderList"
                                CssClass="TopMenuLink" runat="server">OrderList</asp:HyperLink>
                        </td>
                        <td class="TopMenu" width="80%" onmouseover="ViewDiv(1,this.id, -1);">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<br />
<br />
<br />
<table class="menudottedline" cellspacing="0" cellpadding="0" width="100%" border="0">
    <tbody>
        <tr class="menubackgr">
            <td class="smallgrey" width="50%">
                &nbsp;&nbsp;<strong><%=WebsiteName%>
                    Administration</strong>
            </td>
            <td class="smallgrey" align="right">
                &nbsp;Logged in as <strong><span class="small">
                    <%=UserLogin%></span></strong> &nbsp;&nbsp;
                <asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click"></asp:LinkButton>
            </td>
            <td class="smallgrey" align="right">
                &nbsp;&nbsp;&nbsp;
            </td>
            <td class="smallgrey" align="right">
                &nbsp;
            </td>
        </tr>
    </tbody>
</table>
