<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuPage.ascx.cs" Inherits="WebCus.ASCX.MenuPage" %>
<script type="text/javascript">
    $(document).ready(function () {

        $('ul#menutoppage li').has('li.active').addClass(' active');
        //$('ul#menutoppage li.parents').has('li.mega-menu-column').removeClass('parents').addClass('parent');
//        if ($('ul#menutoppage li.parents').has('ul.mega-menu')) {
//            $('ul.mega-menu').removeClass('mega-menu').addClass('megas');
        //        }
     //   $('ul#menutoppage li.parent ul li').addClass('mega-menu-column');
     //   $('ul#menutoppage li.parents ul li a.nav-header').removeClass('nav-header').addClass('nav-headers');
    });
    
</script>


<ul class="menu collapse clearfix" id="menutoppage">
    <asp:Repeater ID="rpt_menu" runat="server" OnItemDataBound="rptMenu_ItemDataBound">
        <ItemTemplate>
       
            <li id='menu_parent' runat="server" class='<%#this.Page.Request.RawUrl.ToString().Trim() == Eval("Alias_URL").ToString().Trim() ? "active" : "none" %>'>
                <a   href='<%#Eval("Alias_URL") %>'><%#Eval("Name_Lang") %></a>
               
                <ul id="menuSub" runat="server">
                    <asp:Repeater ID="rptSubMenu" runat="server" OnItemDataBound="rptMenu_ItemDataBound_sub">
                        <ItemTemplate>
                            <li id="menu_parent_sub" runat="server" class='<%# this.Page.Request.RawUrl.ToString().Trim() == Eval("Alias_URL").ToString().Trim() ? "active" : "none" %>'>
                               <a   href='<%#Eval("Alias_URL") %>'><%#Eval("Name_Lang") %></a>
                                <ul id="menuSub_sub" runat="server">
                              
                                <asp:Repeater ID="rptSubMenu_sub" runat="server">
                                    <ItemTemplate>
                                         <li class='<%# this.Page.Request.RawUrl.ToString().Trim() == Eval("Alias_URL").ToString().Trim() ? "active" : "none" %>'>
                                         <a href='<%#Eval("Alias_URL") %>'><%#Eval("Name_Lang") %></a>
                                       </li>
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
  
</ul>
