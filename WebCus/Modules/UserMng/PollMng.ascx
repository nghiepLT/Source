<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="PollMng.ascx.cs"
    Inherits="UserMng.PollMng" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">

    function CheckValidPoll() {
        var obj = document.getElementById('<%=txtQuestion.ClientID %>');
        if (obj.value == '') {
            alert('Please input question');
            obj.focus();
            return false;
        }

        return true;
    }
    
    
    function CheckValidPollOption() {
        if(<%=this.PollID %> != 0)
        {
            var obj = document.getElementById('<%=txtAnswer.ClientID %>');
            if (obj.value == '') {
                alert('Please input answer');
                obj.focus();
                return false;
            }
            return true;
        }
        else
        {
            alert('Please select poll');
            return false;
        }
        

    }

    function ConfirmDeletePoll() { 
        if(<%=this.PollID %> != 0)
        {
            return confirm('Do you want delete it');
        }
        else
        {
            alert('Please select poll to delete');
            return false;
        }
    }
    
    function ConfirmDeletePollOption() { 
        if(<%=this.PollOptionID %> != 0)
        {
            return confirm('Do you want delete it');
        }
        else
        {
            alert('Please select poll option to delete');
            return false;
        }
    }

</script>

<table width="100%">
    <tr>
        <td width="40%" class="T">
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
                                        <asp:Label ID="Label4" runat="server" Text="Poll list"></asp:Label>
                                    </b>
                                </td>
                                <td class="R">
                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="pdR10 R">
                                                <b>
                                                    <asp:Button ID="btnInsertPoll" runat="server" Text="Insert" Style="background: url('Images/Icon/btn_modify.gif') no-repeat left;
                                                        padding-bottom: 0px; padding-left: 19px;" OnClick="btnInsertPoll_Click" />
                                                </b>&nbsp; <b>
                                                    <asp:Button ID="btnSavePoll" runat="server" Text="Save" Style="background: url('Images/Icon/save.gif') no-repeat left;
                                                        padding-bottom: 0px; padding-left: 22px;" OnClick="btnSavePoll_Click" OnClientClick="return CheckValidPoll();" />
                                                </b>&nbsp; <b>
                                                    <asp:Button ID="btnDeletePoll" runat="server" Text="Delete" Style="background: url('Images/Icon/btn_delete.gif') no-repeat left;
                                                        padding-bottom: 0px; padding-left: 19px;" OnClick="btnDeletePoll_Click" OnClientClick="return ConfirmDeletePoll();" /></b>
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
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="4" height="10px">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="Line2">
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="setup01 pdL5 L">
                        <asp:Label ID="Label16" runat="server" Text="PollID"></asp:Label>
                    </td>
                    <td class="setup02 pdL5 " width="30%">
                        <asp:Label ID="lblPollID" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="20%" class="setup01 pdL5 L">
                        <asp:Label ID="Label17" runat="server" Text="Active"></asp:Label>
                    </td>
                    <td class="setup02 pdL5 " width="30%">
                        <asp:RadioButton ID="rdoPollActive" Checked="true" Text="Yes" runat="server" />
                        <asp:RadioButton ID="rdoPollUnActive" Text="No" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="setup01 pdL5 L">
                        <asp:Label ID="Label22" runat="server" Text="Question"></asp:Label>
                    </td>
                    <td class="setup02 pdL5 " colspan="3">
                        <asp:TextBox ID="txtQuestion" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" height="10px">
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
                        <asp:GridView ID="gvPoll" runat="server" Width="100%" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row"
                            CellSpacing="1" CellPadding="0" BorderWidth="0px" OnRowCommand="gvPoll_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Question">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:LinkButton ID="lnkLink" runat="server" Text='<%#Eval("Question")%>' CommandArgument='<%# Eval("PollID") %>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 C" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="setup02 L" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("Active")%>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 C" HorizontalAlign="Center" />
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
        </td>
        <td>
        </td>
        <td width="57%" class="T">
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
                                        <asp:Label ID="Label14" runat="server" Text="Poll option"></asp:Label>
                                    </b>
                                </td>
                                <td class="R">
                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td class="pdR10 R">
                                                <b>
                                                    <asp:Button ID="btnInsertPollOption" runat="server" Text="Insert"
                                                        Style="background: url('Images/Icon/btn_modify.gif') no-repeat left; padding-bottom: 0px;
                                                        padding-left: 19px;" OnClick="btnInsertPollOption_Click" />
                                                </b>&nbsp; <b>
                                                    <asp:Button ID="btnSavePollOption" runat="server" Text="Save" Style="background: url('Images/Icon/save.gif') no-repeat left;
                                                        padding-bottom: 0px; padding-left: 22px;" OnClick="btnSavePollOption_Click" OnClientClick="return CheckValidPollOption();"/>
                                                </b>&nbsp; <b>
                                                    <asp:Button ID="btnDeletePollOption" runat="server" Text="Delete"
                                                        Style="background: url('Images/Icon/btn_delete.gif') no-repeat left; padding-bottom: 0px;
                                                        padding-left: 19px;" OnClick="btnDeletePollOption_Click" OnClientClick="return ConfirmDeletePollOption();"/></b> </b>
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
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="4" height="10px">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="Line2">
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="setup01 pdL5 L">
                        <asp:Label ID="Label19" runat="server" Text="PollOptionID"></asp:Label>
                    </td>
                    <td class="setup02 pdL5 " width="30%">
                        <asp:Label ID="lblPollOptionID" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="20%" class="setup01 pdL5 L">
                        <asp:Label ID="Label20" runat="server" Text="Vote"></asp:Label>
                    </td>
                    <td class="setup02 pdL5 " width="30%">
                        <asp:TextBox ID="txtVote" runat="server" Text="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="setup01 pdL5 L">
                        <asp:Label ID="Label23" runat="server" Text="Answer"></asp:Label>
                    </td>
                    <td class="setup02 pdL5 " colspan="3">
                        <asp:TextBox ID="txtAnswer" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" height="10px">
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
                        <asp:GridView ID="gvPollOption" runat="server" Width="100%" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row"
                            CellSpacing="1" CellPadding="0" BorderWidth="0px" OnRowCommand="gvPollOption_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Answer">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:LinkButton ID="lnkLink" runat="server" Text='<%# Eval("Answer")%>' CommandArgument='<%# Eval("PollOptionID") %>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 C" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="setup02 L" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vote">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("Votes")%>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 C" HorizontalAlign="Center" />
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
        </td>
    </tr>
</table>
