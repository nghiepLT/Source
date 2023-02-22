<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="WebCus.ASCX.Header" %>

<script language="JavaScript" type="text/javascript">
    function ViewDiv(isView, idTd, index) {
        var divSubMenu = document.getElementById('divSubMenu' + index);
        if (divSubMenu != null) {
            var tdEvent = document.getElementById(idTd);
            var status = isView == 1 ? 'inline' : 'none';

            divSubMenu.style.display = status;
            divSubMenu.style.position = 'absolute';
            divSubMenu.style.top = tdEvent.offsetTop + 16 + 'px';
            divSubMenu.style.left = tdEvent.offsetLeft + 0 + 'px';
        }
        for (var i = 1; i <= 2; i++) {
            if (i != index) {
                var divSubMenuIndex = document.getElementById('divSubMenu' + i);
                if (divSubMenuIndex!=null)
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
                        <td class="TopMenu L" id="tdProduct" runat="server" onmouseover="ViewDiv(1,thiss.id, 1);">
                            <asp:HyperLink ID="HyperLink6" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=ProductList"
                                CssClass="TopMenuLink" runat="server">Product</asp:HyperLink>
                            <!--Sub menu Product-->
                            <div style="position: absolute" id="divSubMenu1" onmouseout="this.style.display='none'">
                                <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="SubMenu L" id="tdProductList" runat="server" >
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=ProductList"
                                                CssClass="SubMenuLink" runat="server">ProductList</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu L" id="tdProductCategory" runat="server" >
                                            <asp:HyperLink ID="HyperLink5" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=CategoryInfo"
                                                CssClass="SubMenuLink" runat="server">Category</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu" id="tdManufacturer" runat="server" >
                                            <asp:HyperLink ID="HyperLink71" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=ManufacturerList"
                                                CssClass="SubMenuLink" runat="server">Manufacturer</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu" id="tdStockStatus" runat="server" >
                                            <asp:HyperLink ID="HyperLink2" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=StockStatusMng"
                                                CssClass="SubMenuLink" runat="server">StockStatus</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu" id="tdTaxClass" runat="server" >
                                            <asp:HyperLink ID="HyperLink3" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=TaxClassMng"
                                                CssClass="SubMenuLink" runat="server">TaxClass</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu" id="tdWeightClass" runat="server" >
                                            <asp:HyperLink ID="HyperLink51" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=WeightClassMng"
                                                CssClass="SubMenuLink" runat="server">WeightClass</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--End Sub menu Product-->
                        </td>
                        <td class="TopMenu" id="tdNews" runat="server" onmouseover="ViewDiv(1,this.id, 2);">
                            <asp:HyperLink ID="HyperLink9" NavigateUrl="/RenderModule.aspx?smid=NewsMng&md=NewsControl.ascx&muid=NewsList"
                                CssClass="TopMenuLink" runat="server">Content</asp:HyperLink>
                            <!--Sub menu News-->
                            <div style="position: absolute; display:none;" id="divSubMenu2" onmouseout="this.style.display='none'">
                                <table id="Table2" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="SubMenu" id="tdNewsList" runat="server" >
                                            <asp:HyperLink ID="HyperLink7" NavigateUrl="/RenderModule.aspx?smid=NewsMng&md=NewsControl.ascx&muid=NewsList"
                                                 runat="server">Content</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu" id="tdNewsCategory" runat="server" >
                                            <asp:HyperLink ID="HyperLink8" NavigateUrl="/RenderModule.aspx?smid=NewsMng&md=NewsControl.ascx&muid=NewsCategoryInfo"
                                                runat="server">Category</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubMenu" id="tdNewsSource" runat="server" >
                                            <asp:HyperLink ID="HyperLink11" NavigateUrl="/RenderModule.aspx?smid=NewsMng&md=NewsControl.ascx&muid=NewsSource"
                                                runat="server">NewsSource</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--End Sub menu News-->
                        </td>
                        <td class="TopMenu" id="tdUser" runat="server" onmouseover="ViewDiv(1,this.id, 3);">
                            <asp:HyperLink ID="hplUserInfo" NavigateUrl="/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid=UserInfo"
                                CssClass="TopMenuLink" runat="server">User</asp:HyperLink>
                        </td>
                        <td class="TopMenu" id="tdLanguage" style="display: none;" runat="server" onmouseover="ViewDiv(1,this.id, 3);">
                            <asp:HyperLink ID="HyperLink4" NavigateUrl="/RenderModule.aspx?smid=ProductMng&md=ProductControl.ascx&muid=LanguageMng"
                                CssClass="TopMenuLink" runat="server">Language</asp:HyperLink>
                        </td>
                        <td class="TopMenu" width="90%" onmouseover="ViewDiv(1,this.id, 3);">
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
