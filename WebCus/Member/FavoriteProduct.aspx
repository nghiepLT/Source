<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FavoriteProduct.aspx.cs" Inherits="WebCus.FavoriteProduct" ValidateRequest="false" %>

<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="cc1" %>
<%@ Register Src="/ASCX/Content.ascx" TagName="Content" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <title>Sản phẩm yêu thích</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="cl05">
        <h1>
            Sản phẩm yêu thích
        </h1>
    </div>
    <div class="clearfix" style="margin-top: 10px;">
        <asp:DataGrid ID="GridCart" CssClass="infotable" runat="server"
            AutoGenerateColumns="False" AllowSorting="True" 
            onitemcommand="GridCart_ItemCommand">
            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <ItemStyle CssClass="CartBody" />
            <HeaderStyle CssClass="CartHeader" />
            <Columns>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        STT
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#Container.ItemIndex + 1%>
                    </ItemTemplate>
                    <HeaderStyle CssClass="css_b" Width="5%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        Sản phẩm
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#BindProName(Eval("ProductID"))%>
                    </ItemTemplate>
                    <HeaderStyle CssClass="css_b" HorizontalAlign="Center"/>
                    <ItemStyle  CssClass="css_link03"/>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        Giá
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#BindProPrice(Eval("ProductID"))%>
                    </ItemTemplate>
                    <HeaderStyle CssClass="css_b" HorizontalAlign="Center" Width="100px"/>
                    <ItemStyle HorizontalAlign="Right"/>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("CartID") %>'
                                    CommandName="DeleteItem" OnClientClick="return confirm('Bạn thật sự muốn xóa?')" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="css_b" HorizontalAlign="Center" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <!--  -->
</asp:Content>
