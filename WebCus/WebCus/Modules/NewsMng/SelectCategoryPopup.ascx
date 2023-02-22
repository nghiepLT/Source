<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectCategoryPopup.ascx.cs" Inherits="NewsMng.SelectCategoryPopup" %>

<script type="text/javascript">

    function CheckValidate() {



        return true;
    }

    function OnCheck(obj) { 
        alert(obj)
    }
    
</script>


<table border="0" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td>
            <img src="Images/Other/corner_Top_Left.gif" />
        </td>
        <td style="background-image: url('Images/Other/bar_Top.gif');">
        </td>
        <td>
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
                            <asp:Label ID="Label4" runat="server" Text="Category info"></asp:Label>
                        </b>
                    </td>
                    <td class="R">
                    </td>
                </tr>
            </table>
        </td>
        <td style="background-image: url('Images/Other/bar_Right.gif');" width='5'>
        </td>
    </tr>
    <tr>
        <td>
            <img src="Images/Other/corner_Bottom_Left.gif" />
        </td>
        <td style="background-image: url('Images/Other/bar_Bottom.gif');">
        </td>
        <td>
            <img src="Images/Other/corner_Bottom_Right.gif" />
        </td>
    </tr>
</table>
<br />
<table width="100%">
    <tr>
        <td width="100%" class="T">
            <table border="0" cellspacing="0" cellpadding="0" width='100%'>
                <tr>
                    <td>
                        <img src="Images/Other/corner_Top_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Top.gif');">
                    </td>
                    <td>
                        <img src="Images/Other/corner_Top_right.gif" />
                    </td>
                </tr>
                <tr>
                    <td style="background-image: url('Images/Other/bar_Left.gif');" width='5'>
                    </td>
                    <td class="R">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="B_L">
                                    <asp:Label ID="lblTitleCategoryTree" runat="server" Text="ProductCategory" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="RB_L">
                                    <asp:Panel ID="Panel1" Height="285px" Width="100%" ScrollBars="Auto" runat="server">
                                        <asp:TreeView ID="treeCategory" runat="server" ShowLines="true"
                                            ShowCheckBoxes="All" 
                                            ontreenodecheckchanged="treeCategory_TreeNodeCheckChanged">
                                            <SelectedNodeStyle CssClass="Link02" />
                                        </asp:TreeView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td class="C">
                                    <b>
                                        <asp:Button ID="btnSelect" runat="server" Text="Select" Style="background: url('Images/Icon/save.gif') no-repeat left;
                                            padding-bottom: 3px; padding-left: 25px; padding-right: 15px" OnClick="btnSelect_Click" OnClientClick="return CheckValidate();" />
                                    </b><b>
                                        <asp:Button ID="LinkButton5" runat="server" Text="Close" Style="background: url('Images/Icon/close.gif') no-repeat left;
                                            padding-bottom: 3px; padding-left: 25px;" OnClientClick="window.close();" />
                                    </b>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="background-image: url('Images/Other/bar_Right.gif');" width='5'>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="Images/Other/corner_Bottom_Left.gif" />
                    </td>
                    <td style="background-image: url('Images/Other/bar_Bottom.gif');">
                    </td>
                    <td>
                        <img src="Images/Other/corner_Bottom_Right.gif" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
