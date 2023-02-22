<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="TerritoryMng.ascx.cs"
    Inherits="UserMng.TerritoryMng" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="uc1" %>
<div class="page-title">
    <h2 class="icon-title">
        <span>Quản lý quận huyện</span>
    </h2>
</div>
<br/>
<script type="text/javascript">
    function validateChooseDropList(oSrc, args) {
        args.IsValid = (args.Value != '-1');
    }
            </script>
<div class="TboardBox">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="C">
                </td>
                <td class="R">
                    <asp:Button ID="btnInsertBanner" runat="server" Text="Tạo mới" CssClass="btn-1" OnClick="btnInsertBanner_Click" />
                    &nbsp;
                    <asp:Button ID="btnSaveBanner" runat="server" ValidationGroup="Edit" Text="Lưu" CssClass="btn-1" OnClick="btnSaveBanner_Click"/>
                    &nbsp;
                    <asp:Button Visible="false" ID="btnDeleteBanner" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDeleteBanner_Click"
                        OnClientClick="return ConfirmDeleteBanner();" />
                    <asp:ValidationSummary ShowSummary="false" ShowMessageBox="true" ValidationGroup="Edit"
                                    ID="ValidationSummary2" runat="server" />
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
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label1" runat="server" Text="Thuộc khu vực"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:DropDownList ID="ddl_area" runat="server">
                </asp:DropDownList>
                <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="Edit"
                    ControlToValidate="ddl_area" Display="None" ErrorMessage="Bạn chưa chọn khu vực cho quận/huyện" 
                    ClientValidationFunction="validateChooseDropList">
                </asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="lbl_name" runat="server" Text="Tên quận huyện"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txt_TerriName" CssClass="Input_text" Width="99%" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_TerriName"
                    SetFocusOnError="true" Display="None" 
                    ValidationGroup="Edit" runat="server" ErrorMessage='Bạn chưa nhập tên quận/huyện'></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label2" runat="server" Text="Thứ tự"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txt_Sort" CssClass="Input_text" Width="99%" runat="server" Text="1"></asp:TextBox>
            </td>
            <%--<th width="15%" class="RB_L">
                <asp:Label ID="Label1" runat="server" Text="Trạng thái"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>--%>
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
                        <asp:TemplateField HeaderText="Tên khu vực">
                            <ItemTemplate>
                                <%#BindAreaName(Eval("AreaID"))%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên quận/huyện">
                            <ItemTemplate>
                                <%#Eval("TerritoryName")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thứ tự">
                            <ItemTemplate>
                                <%#Eval("SortOrder")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
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
                                    CommandArgument='<%# Eval("TerritoryID") %>'
                                    runat="server" CommandName="EditItem" />
                                &nbsp;
                                <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("TerritoryID") %>' CommandName="DeleteItem"
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
