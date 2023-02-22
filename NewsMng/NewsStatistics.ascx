<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsStatistics.ascx.cs"
    Inherits="NewsMng.NewsStatistics" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<table border="0" cellspacing="0" cellpadding="0" width='100%'>
    <tr>
        <td style="font-size: 1pt;">
            <img src="Images/Other/corner_Top_Left.gif" />
        </td>
        <td style="background-image: url('Images/Other/bar_Top.gif');">
        </td>
        <td style="font-size: 1pt;">
            <img src="Images/Other/corner_Top_right.gif" />
        </td>
    </tr>
    <tr>
        <td style="background-image: url('Images/Other/bar_Left.gif');" width='5'>
        </td>
        <td class="R">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="pdL5 L">
                        <img src="/images/Icon/bullet_Tit.gif" />
                        <b>
                            <asp:Label ID="Label4" runat="server" Text="News list"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp; </b>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="Select status" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Enable" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Disable" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:DropDownList ID="ddlSearchType" runat="server">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Title" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:DropDownList ID="ddlCategory" runat="server">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:TextBox ID="txtSearchText" onkeydown="return SetFocusOnEnter(event, 'btnSearch');"
                            runat="server" CssClass="Input_text"></asp:TextBox>
                        <b>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                Style="background: url('Images/Icon/btn_search.gif') no-repeat left; padding-bottom: 3px;
                                padding-left: 20px;" />
                        </b>
                    </td>
                </tr>
            </table>
        </td>
        <td style="background-image: url('Images/Other/bar_Right.gif');" width='5'>
        </td>
    </tr>
    <tr>
        <td style="font-size: 1pt;">
            <img src="Images/Other/corner_Bottom_Left.gif" />
        </td>
        <td style="background-image: url('Images/Other/bar_Bottom.gif');">
        </td>
        <td style="font-size: 1pt;">
            <img src="Images/Other/corner_Bottom_Right.gif" />
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td height="5px">
        </td>
    </tr>
    <tr>
        <td class="Line2">
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvUser" runat="server" Width="100%" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row"
                CellSpacing="1" CellPadding="0" BorderWidth="0px">
                <Columns>
                    <asp:TemplateField HeaderText="Num">
                        <ItemTemplate>
                            &nbsp
                            <%# Eval("Num")%>
                        </ItemTemplate>
                        <HeaderStyle Width="8%" CssClass="setup01 C" HorizontalAlign="center" />
                        <ItemStyle CssClass="setup02 C" Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate>
                            &nbsp;
                            <asp:Label ID="lnkLink" runat="server" Text='<%#Eval("Title")%>' />
                            &nbsp;
                        </ItemTemplate>
                        <HeaderStyle CssClass="setup01 L" HorizontalAlign="Center" />
                        <ItemStyle CssClass="setup02 L" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Author">
                        <ItemTemplate>
                            &nbsp;
                            <asp:Label ID="Label1212" runat="server" Text='<%# Eval("Author")%>' />
                            &nbsp;
                        </ItemTemplate>
                        <HeaderStyle CssClass="setup01 L" HorizontalAlign="Center" Width="17%" />
                        <ItemStyle CssClass="setup02 L" Width="20%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RegDate">
                        <ItemTemplate>
                            &nbsp;
                            <asp:Label ID="Label143" runat="server" Text='<%# Eval("RegDate", "{0: yyyy-MM-dd}")%>' />
                            &nbsp;
                        </ItemTemplate>
                        <HeaderStyle CssClass="setup01 L" HorizontalAlign="Center" Width="17%" />
                        <ItemStyle CssClass="setup02 L" Width="20%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Viewed">
                        <ItemTemplate>
                            &nbsp;
                            <asp:Label ID="Label1421" runat="server" Text='<%#Eval("CountView")%>' />
                            &nbsp;
                        </ItemTemplate>
                        <HeaderStyle CssClass="setup01 L" HorizontalAlign="Center" Width="17%" />
                        <ItemStyle CssClass="setup02 L" Width="20%" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="td_line2" height="1">
        </td>
    </tr>
</table>
<br />
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="200px">
        </td>
        <td class="C">
            <pqt:PQTPager ID="PQTPager1" runat="server" OnPageIndexChanged="Pager_PageIndexChanged"
                Width="410px" />
        </td>
        <td width="200px" class="R">
            <asp:TextBox ID="txtCurrentPage" runat="server" Width="30px" Style="text-align: right"
                CssClass="Input_text"></asp:TextBox>
            /<asp:Label ID="lblTotalPage" runat="server" Text=""></asp:Label>
            <asp:Label ID="Label2" runat="server" Text=" Page"></asp:Label>
            &nbsp;&nbsp;
            <asp:Button ID="btnPageMove" runat="server" Text="Move" Font-Bold="true"
                OnClick="btnPageMove_Click" OnClientClick="return CheckMovePage();" Style="background: url('Images/Icon/btn_move.gif') no-repeat left;
                padding-bottom: 3px; padding-left: 22px;" />
        </td>
    </tr>
</table>
