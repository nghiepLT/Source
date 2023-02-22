<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="MnSeoList.ascx.cs"
    Inherits="UserMng.MnSeoList" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="pqt" %>
<div class="page-title">
    <h2 class="icon-title">
        <span>Seo</span>
    </h2>
</div>
<script type="text/javascript">

    function CheckValidate() {

        var check = false;
        var objArrFile = document.getElementsByTagName('input');
        for (i = 0; i < objArrFile.length; i++) {
            if (objArrFile[i].type == 'text') {
                if (objArrFile[i].id.toString().indexOf('txtName') >= 0 && objArrFile[i].value != '')
                    check = true;
            }
        }

        if (!check) {
            alert('Nhập tiêu đề');
            return false;
        }

        return true;
    }
</script>
<div class="TboardBox">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="C">
                </td>
                <td class="R">
                    <asp:Button ID="btnInsertBanner" Visible="false" runat="server" Text="Tạo mới" CssClass="btn-1" OnClick="btnInsertBanner_Click" />
                    &nbsp;
                    <asp:Button ID="btnSaveBanner" Visible="false" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSaveBanner_Click" OnClientClick="return CheckValidate();"/>
                    &nbsp;
                    <asp:Button Visible="false" ID="btnDeleteBanner" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDeleteBanner_Click" OnClientClick="return confirm('Bạn thật sự muốn xóa?')"/>
                    &nbsp;
                    <asp:Button ID="btnUpdataBanner" runat="server" Text="Update" CssClass="btn-1" OnClick="btnUpdateBanner_Click" OnClientClick="return CheckValidate();"/>
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:5px;">
        <tr>
            <td colspan="4" class="Line1">
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <th class="RB_L" width="20%">
                Mô tả
            </th>
            <td class="B_L" style="width: 80%">
                <asp:Label ID="lbl_mota" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <pqt:TextBoxMultiLanguage ID="txt_title_tag" TextWidth="95%" Title="Meta Title" TitleWidth="20%"
        runat="server" />
    <pqt:TextBoxMultiLanguage ID="txt_key_tag" TextWidth="95%" Title="Meta Keyword" TitleWidth="20%"
                runat="server" />
    <pqt:TextBoxMultiLanguage ID="txt_des_tag" TextWidth="95%" Title="Meta Description" TitleWidth="20%"
        runat="server" />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr id="titlePage" runat="server" Visible="false">
            <th class="RB_L" width="20%">
                Mô tả
            </th>
            <td class="B_L" style="width: 80%">
                <asp:TextBox ID="txt_MoTa"  runat="server" Width="50%" Text="" CssClass="Input_text"></asp:TextBox>
            </td>
        </tr>
        <tr id="keyword" runat="server" Visible="false">
            <th class="RB_L" width="20%">
                KeyWord
            </th>
            <td class="B_L" style="width: 80%">
                <asp:TextBox ID="txtKetWord"  runat="server" Width="50%" Text="" CssClass="Input_text"></asp:TextBox>
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
                        <asp:TemplateField Visible="false" HeaderText="KeyOther">
                            <ItemTemplate>
                                <%#Eval("KeyOther")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mô tả">
                            <ItemTemplate>
                                <%#Eval("Mota")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Meta Title">
                            <ItemTemplate>
                                <span style='<%#Eval("SeoType").ToString() == "1" ? "color:red" : "" %>'>
                                <%#Eval("TitleSeo")%>
                                </span>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Meta Key">
                            <ItemTemplate>
                                <%#Eval("KeySeo")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Meta Description">
                            <ItemTemplate>
                                <%#Eval("DesSeo")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageUrl="/images/icon/modify.gif" ToolTip="Xem/sửa"
                                    CommandArgument='<%# Eval("SeoID") %>'
                                    runat="server" CommandName="EditItem" />
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
