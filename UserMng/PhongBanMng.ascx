<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="PhongBanMng.ascx.cs"
    Inherits="UserMng.PhongBanMng" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="uc1" %>
<div class="page-title">
    <h2 class="icon-title">
        <span>Quản lý Phòng Ban</span>
    </h2>
</div>
<div style="display:none;">
 <asp:FileUpload ID="filesUpload" runat="server" CssClass="btn-1" />
            <asp:Button ID="btn_upload" runat="server" Text="Upload" OnClick="Click_upload" CssClass="btn-1" />
            <asp:GridView ID="grvData" runat="server">
        </asp:GridView>
<br/>
</div>
<div class="TboardBox">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tb_button" runat="server" visible="true">
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
                    <asp:Button Visible="false" ID="btnDeleteBanner" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDeleteBanner_Click"
                        OnClientClick="return ConfirmDeleteBanner();" />
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tb_input" runat="server" visible="true">
        <tr>
            <td colspan="4" height="5px">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="Line2">
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label2" runat="server" Text="Type"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:DropDownList ID="dr_typephong" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="dr_typephong_SelectedIndexChanged">
                     <asp:ListItem Text="--Chọn Loại--" Value="0" Selected="True" ></asp:ListItem>
                     <asp:ListItem Text="Phòng Ban-Bộ Phận" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Nhóm" Value="2"></asp:ListItem>
                </asp:DropDownList>
                
            </td>
        </tr>
         <tr id="tr_key" runat="server">
            <th width="15%" class="RB_L">
                <asp:Label ID="Label4" runat="server" Text="KEYID"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txt_keyid" CssClass="Input_text" Width="99%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="lbl_name" runat="server" Text="Tên"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txttenPhong" CssClass="Input_text" Width="99%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label1" runat="server" Text="Trực Thuộc"></asp:Label>
            </th>
            <td class="RB_L">
               <asp:DropDownList ID="dr_Cty" runat="server">
                     <asp:ListItem Text="Nguyên Kim" Selected="True" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Chính Nhân" Value="2"></asp:ListItem>
                     <asp:ListItem Text="SMC" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr_thuocphong" runat="server">
            <th width="15%" class="RB_L">
                <asp:Label ID="Label3" runat="server" Text="Thuộc Phòng"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:DropDownList ID="dr_phongban" runat="server" >                     
                </asp:DropDownList>
            </td>
        </tr>
    </table>
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
                        <asp:TemplateField HeaderText="Tên Bộ Phận">
                            <ItemTemplate>
                                <%#Eval("TenPhong")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trực Thuộc">
                            <ItemTemplate>
                                <%#Eval("TrucThuoc")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Thứ tự">
                            <ItemTemplate>
                                <%#Eval("SortOrder")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>--%>
                       <%-- <asp:TemplateField HeaderText="Trạng thái">
                            <ItemTemplate>
                                <img src='/admincss/images/icon/<%# Get_Image_Status(Eval("Status"))%>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_C" Width="80px" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageUrl="/images/icon/modify.gif" ToolTip="Xem/sửa"
                                    CommandArgument='<%# Eval("IDPhong") %>'
                                    runat="server" CommandName="EditItem" />
                                &nbsp;
                                <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("IDPhong") %>' CommandName="DeleteItem"
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
