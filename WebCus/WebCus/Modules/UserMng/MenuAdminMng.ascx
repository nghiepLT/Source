<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="MenuAdminMng.ascx.cs"
    Inherits="UserMng.MenuAdminMng" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="uc1" %>
<script type="text/javascript" src="/Include/JS/ShowImage.js"></script>
<script type="text/javascript">

    function CheckValid() {
        var obj = document.getElementById('<%=txtUrl.ClientID %>');
        if (obj.value == '') {
            alert('Please input url');
            obj.focus();
            return false;
        }

        return true;
    }
    

    function ConfirmDelete() { 
        if(<%=this.MenuID %> != 0)
        {
            return confirm('Do you want delete it');
        }
        else
        {
            alert('Please select Menu to delete');
            return false;
        }
    }
    

</script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Quản lý menu admin</span>
    </h2>
</div>
<br>
<table border="0" width="100%" cellpadding="0" cellspacing="0">
    <tbody>
        <tr>
            <td class="C">
                &nbsp;
            </td>
            <td class="R">
                <asp:Button ID="btnInsert" runat="server" Text="Insert" CssClass="btn-1" OnClick="btnInsert_Click" />
                &nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-1" OnClick="btnSave_Click"
                    OnClientClick="return CheckValid();" />
                &nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn-1" OnClick="btnDelete_Click"
                    OnClientClick="return ConfirmDelete();" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" class="Line2">
            </td>
        </tr>
        <tr>
            <td width="40%" class="T C LinkCat">
                <asp:Panel ID="Panel1" Height="385px" Width="100%" ScrollBars="Auto" runat="server">
                    <asp:TreeView ID="treeCategory" runat="server" ShowLines="true" OnSelectedNodeChanged="treeCategory_SelectedNodeChanged">
                        <SelectedNodeStyle CssClass="Link02" />
                        <NodeStyle Font-Bold="true" />
                    </asp:TreeView>
                </asp:Panel>
            </td>
            <td>
                &nbsp;
            </td>
            <td width="56%" class="T">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th width="20%" class="RB_L">
                            <asp:Label ID="Label16" runat="server" Text="KeyWord"></asp:Label>
                        </th>
                        <td class="B_L" width="30%">
                            <asp:TextBox ID="txtKeyWord" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                        <th width="20%" class="RB_L">
                            <asp:Label ID="Label17" runat="server" Text="Active"></asp:Label>
                        </th>
                        <td class="B_L" width="30%">
                            <asp:RadioButton ID="rdoMenuActive" Checked="true" Text="Yes" runat="server" GroupName="Active" />
                            <asp:RadioButton ID="rdoMenuUnActive" Text="No" runat="server" GroupName="Active" />
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="RB_L">
                            <asp:Label ID="Label2" runat="server" Text="Menu name"></asp:Label>
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtMenuName" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                        <th width="20%" class="RB_L">
                            <asp:Label ID="Label3" runat="server" Text="Sort Order"></asp:Label>
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtSortOrder" runat="server" CssClass="Input_text" Width="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="RB_L">
                            <asp:Label ID="Label5" runat="server" Text="ParentID"></asp:Label>
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtParentID" runat="server" CssClass="Input_text" Width="60px"></asp:TextBox>
                        </td>
                        <th width="20%" class="RB_L">
                            <asp:Label ID="Label6" runat="server" Text="MenuID"></asp:Label>
                        </th>
                        <td class="B_L">
                            <asp:Label ID="lblMenuID" runat="server" Text="MenuID"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="RB_L">
                            <asp:Label ID="Label22" runat="server" Text="Url"></asp:Label>
                        </th>
                        <td class="B_L" colspan="3">
                            <asp:TextBox ID="txtUrl" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="Line1">
            </td>
        </tr>
    </table>
</div>
