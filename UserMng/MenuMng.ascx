<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="MenuMng.ascx.cs"
    Inherits="UserMng.MenuMng" %>
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
    
    function ShowImage(event, isShow) {

        if (isShow == 0) {
            document.getElementById("divImageProduct").style.display = 'none';
            return false;
        } else {
            PopupArea(event, "divImageProduct");
            return false;
        }

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

    function change_url() { 
        var url = $('#ctl00_MainContent_ctl00_ctl00_dl_map_item').val();
        if(url != "#")
            $('#ctl00_MainContent_ctl00_ctl00_txtUrl').val(url);

        var item_text = $("#ctl00_MainContent_ctl00_ctl00_dl_map_item option:selected").text();
        var n = item_text.lastIndexOf(">");
        if(n>0)
            n=n+2;
        var text=item_text.substring(n);
        $('#ctl00_MainContent_ctl00_ctl00_txtMenuName').val(text);
        $('#ctl00_MainContent_ctl00_ctl00_txtMenuName2').val(text);
        $('#ctl00_MainContent_ctl00_ctl00_txtMenuName3').val(text);
    }
    
    

</script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Quản lý MENU</span>
    </h2>
</div>
<br>
<table border="0" width="100%" cellpadding="0" cellspacing="0">
    <tbody>
        <tr>
            <td class="C">&nbsp;
            </td>
            <td class="R">
                <asp:Button ID="btnInsert" runat="server" Text="Làm mới" CssClass="btn-1" OnClick="btnInsert_Click" />
                &nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Cập nhật" CssClass="btn-1" OnClick="btnSave_Click"
                    OnClientClick="return CheckValid();" />
                &nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Xoá" CssClass="btn-1" OnClick="btnDelete_Click"
                    OnClientClick="return ConfirmDelete();" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" class="Line2"></td>
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
            <td>&nbsp;
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
                           <%-- <asp:TextBox ID="txtParentID" runat="server" CssClass="Input_text" Width="60px"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddl_parent" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th width="20%" class="RB_L">Link nội bộ 
                        </th>
                        <td class="B_L">
                            <asp:RadioButton ID="rdoMenu_NoiBo_Active" Text="Yes" runat="server" GroupName="NoiBo" />
                            <asp:RadioButton ID="rdoMenu_NoiBo_UnActive" Text="No" Checked="true" runat="server" GroupName="NoiBo" />
                        </td>
                    </tr>
                    <tr>
                        <th class="RB_L">Loại
                        </th>
                        <td class="B_L" colspan="3">
                            <asp:DropDownList ID="dl_type" runat="server" OnSelectedIndexChanged="dl_type_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="1">Trang chủ</asp:ListItem>
                                <asp:ListItem Value="2">Nhóm sản phẩm</asp:ListItem>
                                <asp:ListItem Value="3">Trang nội dung</asp:ListItem>
                                <asp:ListItem Value="4">Blog</asp:ListItem>
                                <asp:ListItem Value="5">Liên hệ</asp:ListItem>
                                <asp:ListItem Value="6">Tất cả sản phẩm</asp:ListItem>
                                <asp:ListItem Value="7">Sản phẩm</asp:ListItem>
                                <asp:ListItem Value="8">Tìm kiếm</asp:ListItem>
                                <asp:ListItem Value="9">Địa chỉ web</asp:ListItem>
                                  <asp:ListItem Value="15">Tour</asp:ListItem>
                                   <asp:ListItem Value="14">Khách sạn</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="dl_map_item" Width="120px" onchange="change_url()" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th class="RB_L">Url
                        </th>
                        <td class="B_L" colspan="3">
                            <asp:TextBox ID="txtUrl" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                      <tr id="div_img" runat="server">
                        <th class="RB_L">Hình Ảnh
                        </th>
                        <td class="B_L" colspan="3">
                         <table>
                                <tr>
                                    <td onmouseover="return ShowImage(event, 1);" onmouseout="return ShowImage(event, 0);">
                                        <asp:Label ID="lblImage" runat="server" Text="" ForeColor="BlueViolet" Style="cursor: pointer"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                           <asp:FileUpload ID="fileImage" runat="server" CssClass="Input_text" Width="90%" />
                            <asp:Button ID="btnDeleteImg" runat="server" Text="Xóa ảnh" CssClass="btn-1" OnClick="btnDeleteImg_Click" />
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="RB_L">Name 2
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtMenuName2" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                        <th width="20%" class="RB_L">Name 3
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtMenuName3" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="RB_L">Option 1
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtOption1" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                        <th width="20%" class="RB_L">Option 2
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtOption2" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="RB_L">Option 3
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtOption3" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                        <th width="20%" class="RB_L">Option 4
                        </th>
                        <td class="B_L">
                            <asp:TextBox ID="txtOption4" runat="server" CssClass="Input_text" Width="95%"></asp:TextBox>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="Line1"></td>
        </tr>
    </table>

     <div id="divImageProduct" style="display: none; background-color: Gray; padding: 2px">
        <div style="padding: 3px; background-color: White">
            <asp:Image ID="imgProduct" runat="server" Width="200px" />
        </div>
    </div>
</div>
