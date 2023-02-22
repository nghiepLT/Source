<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupNewsReleated.ascx.cs"
    Inherits="NewsMng.PopupNewsReleated" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>

<script type="text/javascript">

    function ShowCategory() {
        WindowOpen('RenderPopup.aspx?smid=ProductMng&renderPage=SelectCategoryPopup.ascx', 'Category', 400, 450, 'no');
        return false;
    }
    
     function CheckMovePage() {
        var txtCurrentPage = document.getElementById("<%=txtCurrentPage.ClientID %>");
        var totalPage = <%=TotalPage %>;

        if (!PageMoveValid(txtCurrentPage, totalPage))
        {
	        alert('Invalid page number');
	        txtCurrentPage.focus();
	        return false;
        }
        return true;
    }
    
     function CheckAllItem(objcheckAll)
    {
        var obj = document.getElementsByName('chkItem');
        
        for(var i=0; i< obj.length; i++)
        {
            var chkItem = obj[i];
            chkItem.checked = objcheckAll.checked;
        }
    }
    
    function GetValueChecked()
    {
        var strValueIDChecked = '';
        var obj = document.getElementsByName('chkItem');
        
        for(var i=0; i< obj.length; i++)
        {
            var chkItem = obj[i];
            if(chkItem.checked == true)
            {
                strValueIDChecked += chkItem.value + ',';
            }
        }
        if(strValueIDChecked == '')
        {
            alert("Vui lòng chọn tin tức");
            return false;
        }
        else
        {
                var hdnIDs = document.getElementById('<%=hdnIDs.ClientID %>');
                hdnIDs.value  = strValueIDChecked;
                return true;   
        
        }
    }
    
    function selectedTabOnLoad(){}
    
</script>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <asp:HiddenField ID="hdnIDs" Value="" runat="server" />
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
                    <td class="R" style="height: 26px;">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="pdL5 L">
                                    <img src="/images/Icon/bullet_Tit.gif" />
                                    <b>
                                        <asp:Label ID="Label4" runat="server" Text="News list"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp; </b>
                                    <asp:DropDownList ID="ddlStatus" runat="server" Style="display: none;">
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
                                </td>
                                <td class="pdL10">
                                    &nbsp;
                                    <asp:ImageButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" ImageUrl="/Images/Icon/btn_search.gif" />
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
                            CellSpacing="1" CellPadding="0" BorderWidth="0px" OnRowCommand="gvUser_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Chọn">
                                    <HeaderTemplate>
                                        <input id="chkAll" type="checkbox" onclick="CheckAllItem(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input id="chkItem" type="checkbox" name="chkItem" value='<%# Eval("NewsID")%>' style='display:<%# IsView(Eval("NewsID"))%>;'/>
                                        
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" CssClass="setup01 C" HorizontalAlign="center" />
                                    <ItemStyle CssClass="setup02 C" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stt">
                                    <ItemTemplate>
                                        &nbsp
                                        <%# Eval("Num")%>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" CssClass="setup01 C" HorizontalAlign="center" />
                                    <ItemStyle CssClass="setup02 C" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tiêu đề">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:LinkButton ID="lnkLink" runat="server" Text='<%#Eval("Title")%>' CommandArgument='<%# Eval("NewsID") %>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 L" HorizontalAlign="Center" />
                                    <ItemStyle CssClass="setup02 L" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tác giả">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:Label ID="Label1212" runat="server" Text='<%# Eval("Author")%>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 L" HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle CssClass="setup02 L" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ngày tạo">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:Label ID="Label143" runat="server" Text='<%# Eval("RegDate", "{0: yyyy-MM-dd}")%>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 L" HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle CssClass="setup02 L" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Trạng thái">
                                    <ItemTemplate>
                                        &nbsp;
                                        <asp:Label ID="Label1421" runat="server" Text='<%#GetStatusName(Eval("NewsStatus"))%>' />
                                        &nbsp;
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="setup01 L" HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle CssClass="setup02 L" Width="60px" />
                                </asp:TemplateField>--%>
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
        </td>
        <td width="80px">
            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="pdR10 R T" height="30px">
                        &nbsp;&nbsp; <b>
                            <asp:Button ID="btnSelect" runat="server" Text="Chọn" Style="background: url('Images/Icon/value_regist.gif') no-repeat left;
                                padding-bottom: 3px; padding-left: 25px;" OnClick="btnSelect_Click" OnClientClick="return GetValueChecked();" />
                        </b>
                    </td>
                </tr>
                <tr>
                    <td class="pdR10 R T" height="272px">
                        &nbsp;&nbsp; <b>
                            <asp:Button ID="btnClose" runat="server" Text="Đóng" Style="background: url('Images/Icon/forward_level.gif') no-repeat left;
                                padding-bottom: 3px; padding-left: 25px;" OnClick="btnClose_Click" />
                        </b>
                    </td>
                </tr>
            </table>
        </td>
        <td width="340px" class="T">
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
                                <td class="pdL5 L" style="height: 26px;">
                                    <img src="/images/Icon/bullet_Tit.gif" />
                                    <b>
                                        <asp:Label ID="Label1" runat="server" Text="Tin liên quan"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp; </b>
                                    <%--<asp:DropDownList ID="DropDownList3" runat="server">
                                    </asp:DropDownList>--%>
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
                        <div id="divReleatedNews1">
                            <asp:GridView ID="gvReleated" runat="server" Width="100%" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row"
                                CellSpacing="1" CellPadding="0" BorderWidth="0px" OnRowCommand="gvReleated_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Tiêu đề">
                                        <ItemTemplate>
                                            &nbsp;
                                            <asp:LinkButton ID="lnkLink" runat="server" Text='<%#Eval("Title")%>' CommandArgument='<%# Eval("ReleatedID") %>' />
                                            &nbsp;
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="setup01 L" HorizontalAlign="Center" />
                                        <ItemStyle CssClass="setup02 L" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Xóa">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="/Images/Icon/delete_s.gif"
                                                Visible='<%# IsVisible(Eval("ReleatedID")) %>' CommandArgument='<%# Eval("ReleatedID") %>'
                                                OnClientClick="return confirm('Bạn thật sự muốn bỏ tin này?');" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="setup01 C" HorizontalAlign="Center" />
                                        <ItemStyle CssClass="setup02 C" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
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
