<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="BannerAdvMng.ascx.cs"
    Inherits="UserMng.BannerAdvMng" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="uc1" %>
<script type="text/javascript" src="/Include/JS/ShowImage.js"></script>
<script type="text/javascript">

    function CheckValidBanner() {
        var obj = document.getElementById('<%=txtUrl.ClientID %>');
        if (obj.value == '') {
            alert('Please input url');
            obj.focus();
            return false;
        }

        return true;
    }
    

    function ConfirmDeleteBanner() { 
        if(<%=this.BannerID %> != 0)
        {
            return confirm('Do you want delete it');
        }
        else
        {
            alert('Please select Banner to delete');
            return false;
        }
    }
    
    function ShowImage(event, isShow) {

        if (isShow == 0) {
            document.getElementById("divImageView").style.display = 'none';
            return false;
        } else {
            PopupArea(event, "divImageView");
            return false;
        }

    }

</script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Quản lý banner/logo</span>
    </h2>
</div>
<br>
<div class="TboardBox">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="C">
                </td>
                <td class="R">
                    <asp:Button ID="btnInsertBanner" runat="server" Text="Tạo mới" CssClass="btn-1" OnClick="btnInsertBanner_Click" />
                    &nbsp;
                    <asp:Button ID="btnSaveBanner" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSaveBanner_Click"
                        OnClientClick="return CheckValidBanner();" />
                    &nbsp;
                    <asp:Button ID="btnDeleteBanner" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDeleteBanner_Click"
                        OnClientClick="return ConfirmDeleteBanner();" /></b>
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="4" height="5px">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="Line2">
            </td>
        </tr>
        <tr style="display: none;">
            <th width="20%" class="RB_L">
                <asp:Label ID="Label16" runat="server" Text="BannerID"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:Label ID="lblBannerID" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label17" runat="server" Text="Trạng thái"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:RadioButton ID="rdoBannerActive" Checked="true" Text="Yes" runat="server" />
                <asp:RadioButton ID="rdoBannerUnActive" Text="No" runat="server" />
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label2" runat="server" Text="Hình ảnh"></asp:Label>
            </th>
            <td class="RB_L">
                <table onmouseover="return ShowImage(event, 1);" onmouseout="return ShowImage(event, 0);">
                    <tr>
                        <td>
                            <asp:Label ID="LinkImg" runat="server" Text="" ForeColor="BlueViolet" Style="cursor: pointer"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:FileUpload ID="fileImage" runat="server" Width="10px" Height="20px" />
            </td>
            <th width="20%" class="RB_L" style="display: none;">
                <asp:Label ID="Label5" runat="server" Text="Position"></asp:Label>
            </th>
            <td class="RB_L" width="30%" style="display: none;">
                <asp:DropDownList ID="ddlPosition" runat="server" Enabled="false">
                    <asp:ListItem Text="Top" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Left" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Right" Value="3" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label22" runat="server" Text="Url"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txtUrl" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
            </td>
            </tr>
        <tr>
            <th width="20%" class="RB_L">
                <asp:Label ID="lblSortOrder" runat="server" Text="Thứ tự"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txtSortOrder" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <uc1:TextBoxMultiLanguage ID="tbmlName" runat="server" TitleWidth="20%" Title="Tên"
        TextWidth="95%" />
    <div id="divImageView" style="display: none; background-color: Gray; padding: 2px">
        <div style="padding: 3px; background-color: White">
            <asp:Image ID="imgBanner" runat="server" Width="100px" />
        </div>
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="4" class="Line1">
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
                <asp:GridView ID="gvBanner" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px" OnRowCommand="gvBanner_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="STT">
                            <ItemTemplate>
                                &nbsp
                                <%# Container.DisplayIndex+1%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hình ảnh">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkLinkImg" runat="server" CommandArgument='<%# Eval("BannerID") %>'
                                    CommandName="EditItem">
                                    <img onmouseover="return ShowImage(event, 1, this.id);" id="tdLink" runat="server"
                                        onmouseout="return ShowImage(event, 0, this.id);" src='<%#GetImagePath(Eval("FileID")) %>'
                                        style="width: 60px; height: 45px;" />
                                </asp:LinkButton>
                                <div id="divImage" runat="server" style="display: none; background-color: Gray; padding: 2px">
                                    <div style="padding: 3px; background-color: White">
                                        <asp:Image ID="imgProduct" ImageUrl='<%#GetImagePath(Eval("FileID")) %>' runat="server"
                                            CommandArgument='<%# Eval("BannerID") %>' CommandName="EditItem" />
                                    </div>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_C" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên">
                            <ItemTemplate>
                                &nbsp;
                                <asp:LinkButton ID="lnkLink" runat="server" Text='<%#Eval("Name")%>' CommandName="EditItem"  CommandArgument='<%# Eval("BannerID") %>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Url">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("Url")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageUrl="/images/icon/modify.gif" ToolTip="Xem/sửa"
                                    CommandArgument='<%# Eval("BannerID") %>' runat="server" CommandName="EditItem" />
                                &nbsp;
                                <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("BannerID") %>' CommandName="DeleteItem"
                                    OnClientClick="return confirm('Bạn thật sự muốn xóa?')" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="B_C" Width="70px" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="Line1" height="1">
            </td>
        </tr>
    </table>
</div>
