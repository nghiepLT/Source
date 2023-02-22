<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="FileUploadMng.ascx.cs"
    Inherits="UserMng.FileUploadMng" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="uc1" %>
<div class="page-title">
    <h2 class="icon-title">
        <span>Quản lý Upload File</span>
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
                    <asp:Button Visible="false" ID="btnDeleteBanner" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDeleteBanner_Click"
                        OnClientClick="return ConfirmDeleteBanner();" />
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
            <th width="15%" class="RB_L">
                <asp:Label ID="Label16" runat="server" Text="FileID"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:Label ID="lblFileID" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="lblFile01" runat="server" Text="Upload File"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:FileUpload ID="fileUrl01" runat="server" Width="70%" Height="20px" />
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="lblFile02" runat="server" Text="Upload File"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:FileUpload ID="fileUrl02" runat="server" Width="70%" Height="20px" />
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label2" runat="server" Text="Tiêu đề 1"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txt_title1" runat="server"></asp:TextBox>
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label3" runat="server" Text="Tiêu đề 2"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txt_title2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="lblFile03" runat="server" Text="Upload File"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:FileUpload ID="fileUrl03" runat="server" Width="70%" Height="20px" />
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="lblFile04" runat="server" Text="Upload File"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:FileUpload ID="fileUrl04" runat="server" Width="70%" Height="20px" />
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label4" runat="server" Text="Tiêu đề 3"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txt_title3" runat="server"></asp:TextBox>
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label5" runat="server" Text="Tiêu đề 4"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txt_title4" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_key" runat="server" visible="false">
            <th width="15%" class="RB_L">
                <asp:Label ID="Label1" runat="server" Text="Key Word"></asp:Label>
            </th>
            <td class="RB_L" colspan="3">
                <asp:TextBox ID="txtKeyWord" CssClass="Input_text" Width="95%" runat="server"></asp:TextBox>
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
                        <asp:TemplateField HeaderText="Tiêu đề">
                            <ItemTemplate>
                                <%#Eval("Title")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_C" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <%#Eval("FileName")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_C" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Link Url Download">
                            <ItemTemplate>
                                <%#BindLinkUrl(Eval("UploadFileID"))%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("UploadFileID") %>' CommandName="DeleteItem"
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
