<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsSource.ascx.cs" Inherits="NewsMng.NewsSource" %>

<script type="text/javascript">
    function Checkvalidate()
    {
         
    }
     function ConfirmDelete() 
    {
        var NewsSourceID = document.getElementById('<%=lblNewsSourceID.ClientID %>');
        if(NewsSourceID.innerHTML!='')
        {
            return confirm('Do you want delete it');
        }
        else
        {
            alert('Select source to delete');
            return false;
        }
    }
</script>

<table border="0" cellspacing="0" cellpadding="0" width='100%'>
    <tr>
        <td width="48%" valign="top">
            <table border="0" cellspacing="0" cellpadding="0" width='100%'>
                <tr>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Top_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Top.gif');">
                    </td>
                    <td style="font-size:1pt;">
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
                                        <asp:Label ID="Label4" runat="server" Text="News Source"></asp:Label>
                                    </b>
                                </td>
                                <td class="R">
                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="pdR10 R" height="22px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="background-image: url('Images/Other/bar_Right.gif');" width='5'>
                    </td>
                </tr>
                <tr>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Bottom_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Bottom.gif');">
                    </td>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Bottom_Right.gif" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="Line2">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" Height="165px" ScrollBars="Vertical" runat="server">
                        <asp:GridView ID="gvUser" runat="server" Width="100%" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row"
                            CellSpacing="1" CellPadding="0" BorderWidth="0px" 
                            onrowcommand="gvUser_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Num">
                                    <ItemTemplate>
                                        &nbsp
                                        <%# Container.DisplayIndex + 1%>
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%" CssClass="setup01 C" HorizontalAlign="center" />
                                    <ItemStyle CssClass="setup02 C" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:LinkButton ID="lnkLink" runat="server" Text='<%# Eval("NewsSourceName")%>' CommandArgument='<%# Eval("NewsSourceID") + "," + Eval("LanguageID") %>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 C" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="setup02 L" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Language">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("LangName")%>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 C" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="setup02 C" Width="20%" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="td_line2" height="1">
                    </td>
                </tr>
            </table>
        </td>
        <td width="4%"></td>
        <td width="48%" valign="top">
            <table border="0" cellspacing="0" cellpadding="0" width='100%'>
                <tr>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Top_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Top.gif');">
                    </td>
                    <td style="font-size:1pt;">
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
                                        <asp:Label ID="Label20" runat="server" Text="Detail"></asp:Label>
                                    </b>
                                </td>
                                <td class="R">
                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="pdR10 R" height="22px">
                                                <b>
                                                    <asp:Button ID="btnInsert" runat="server" Text="Tạo mới" Style="background: url('Images/Icon/btn_modify.gif') no-repeat left;
                                                        padding-bottom: 3px; padding-left: 25px;" onclick="btnInsert_Click" />
                                                    <asp:Button ID="btnDelete" runat="server" Text="Xóa" Style="background: url('Images/Icon/delete.gif') no-repeat left;
                                                        padding-bottom: 3px; padding-left: 25px; padding-right: 15px" 
                                                    onclick="btnDelete_Click" OnClientClick="return ConfirmDelete() ;" />
                                                    <asp:LinkButton Height="16px" ID="btnSave" runat="server" Text="Save" Style="background: url('Images/Icon/save.gif') no-repeat left;
                                                        padding-bottom: 3px; padding-left: 25px; padding-right: 15px"
                                                        OnClientClick="return Checkvalidate();" onclick="btnsave_click" />
                                                </b>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="background-image: url('Images/Other/bar_Right.gif');" width='5'>
                    </td>
                </tr>
                <tr>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Bottom_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Bottom.gif');">
                    </td>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Bottom_Right.gif" />
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellspacing="0" cellpadding="0" width='100%'>
                <tr>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Top_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Top.gif');">
                    </td>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Top_right.gif" />
                    </td>
                </tr>
                <tr>
                    <td style="background-image: url('Images/Other/bar_Left.gif');" width='5'>
                    </td>
                    <td class="R">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th class="RB_L" width="20%">
                                    <asp:Label ID="Label19" runat="server" Text="NewsSourceID" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="B_L">
                                    <asp:Label ID="lblNewsSourceID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <th class="RB_L" width="20%">
                                    <asp:Label ID="Label16" runat="server" Text="Language" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="B_L">
                                    <asp:DropDownList ID="ddlLanguage" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th class="RB_L" width="20%">
                                    <asp:Label ID="Label2" runat="server" Text="SourceName" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="B_L" colspan="2" style="width: 50%">
                                    <asp:TextBox ID="txtSourceName" CssClass="Input_text" Width="99%" runat="server"></asp:TextBox>
                                </td>
                                <td class="B_L" width="30%">
                                    &nbsp;</td>
                            </tr>
                            </table>
                    </td>
                    <td style="background-image: url('Images/Other/bar_Right.gif');" width='5'>
                    </td>
                </tr>
                <tr>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Bottom_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Bottom.gif');">
                    </td>
                    <td style="font-size:1pt;">
                        <img src="Images/Other/corner_Bottom_Right.gif" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
