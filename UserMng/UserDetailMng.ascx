<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="UserDetailMng.ascx.cs"
    Inherits="UserMng.UserDetailMng" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="TextBoxMultiLanguage.ascx" TagName="TextBoxMultiLanguage" TagPrefix="uc1" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>

<script type="text/javascript" src="/Include/JS/ShowImage.js"></script>
<script type="text/javascript">

    function ShowImage(event, isShow) {

        if (isShow == 0) {
            document.getElementById("divImageView").style.display = 'none';
            return false;
        } else {
            PopupArea(event, "divImageView");
            return false;
        }

    }

    function ShowImageFrame(event, isShow) {

        if (isShow == 0) {
            document.getElementById("divImageFrame").style.display = 'none';
            return false;
        } else {
            PopupArea(event, "divImageFrame");
            return false;
        }

    }
</script>
<div class="page-title">
    <h2 class="icon-title">
        <span>Thông tin User</span>
    </h2>
</div>
<div class="TboardBox">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="C">
                </td>
                <td class="R">
                    <asp:Button ID="btnInsertBanner" runat="server" Text="Reset" CssClass="btn-1" OnClick="btnInsertBanner_Click" />
                    &nbsp;
                    <asp:Button ID="btnSaveBanner" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSaveBanner_Click"/>
                    &nbsp;
                    <asp:Button ID="btnDeleteBanner" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDeleteBanner_Click"
                        OnClientClick="return confirm('Bạn thật sự muốn xóa?')" />
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2" height="5px">
            </td>
        </tr>
        <tr>
            <td colspan="2" class="Line2">
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>   
            <th width="15%" class="RB_L">
                <asp:Label ID="Label1" runat="server" Text="Họ tên thành viên"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lbl_fullname" runat="server"></asp:Label>
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lblEmail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label2" runat="server" Text="Tên đăng nhập"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lbl_loginid" runat="server"></asp:Label>
            </td>
            
            <th width="15%" class="RB_L">
                <asp:Label ID="Label8" runat="server" Text="Ngày sinh"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lbl_brithday" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="display:none;">
            <th width="15%" class="RB_L">
                <asp:Label ID="Label4" runat="server" Text="Số điện thoại"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lbl_phone" runat="server"></asp:Label>
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label5" runat="server" Text="YahooID"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lblYahooID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label6" runat="server" Text="Giới tính"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lbl_gioitinh" runat="server"></asp:Label>
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label9" runat="server" Text="Ngày đăng ký"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lblRegDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label11" runat="server" Text="Bình luận"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lbl_comment" runat="server"></asp:Label>
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label10" runat="server" Text="Số like"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lbl_like" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="display:none;">
            <th width="15%" class="RB_L">
                <asp:Label ID="Label7" runat="server" Text="Địa chỉ"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lblAddress" runat="server"></asp:Label>
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label13" runat="server" Text="Bài viết"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lbl_baiviet" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="display:none;">
            <th width="15%" class="RB_L">
                <asp:Label ID="Label12" runat="server" Text="Ý tưởng"></asp:Label>
            </th>
            <td class="B_L">
                <asp:Label ID="lbl_chucdanh" runat="server"></asp:Label>
            </td>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label14" runat="server" Text="YT Kim cương"></asp:Label>
            </th>
            <td class="B_L">
                <asp:RadioButton ID="rbt_KCYes" GroupName="kc" runat="server" />Yes
                &nbsp;
                <asp:RadioButton ID="rbt_KCNo" GroupName="kc" runat="server" />No
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
                                <%#Eval("NUM")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Họ tên">
                            <ItemTemplate>
                                <%#Eval("UserName")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên đăng nhập">
                            <ItemTemplate>
                                <%#Eval("LoginID")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <%#Eval("Email")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="YahooID">
                            <ItemTemplate>
                                <%#Eval("YahooID")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Số điện thoại">
                            <ItemTemplate>
                                <%#Eval("Tel")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Địa chỉ">
                            <ItemTemplate>
                                <%#Eval("Address")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ngày sinh">
                            <ItemTemplate>
                                <%#Eval("Brithday", "{0:dd/MM/yyyy}")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Giới tính">
                            <ItemTemplate>
                                <%#this.StrGender(Eval("Gender"))%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageUrl="/images/icon/modify.gif" ToolTip="View Detail"
                                    CommandArgument='<%# Eval("UserID") %>' runat="server" CommandName="EditItem" />
                                &nbsp;
                                <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("UserID") %>' CommandName="DeleteItem"
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
    <div class="page clear">
        <center>
            <pqt:PQTPager ID="PQTPager1" FirstPageImageUrl="/img/pager/first.png" PreviousButtonImageUrl="/img/pager/prev.png"
                NextButtonImageUrl="/img/pager/next.png" LastPageImageUrl="/img/pager/last.png"
                runat="server" OnPageIndexChanged="Pager_PageIndexChanged" />
        </center>
    </div>
</div>
<div id="divImageView" style="display: none; background-color: Gray; padding: 2px">
    <div style="padding: 3px; background-color: White">
        <asp:Image ID="imgPaint" runat="server" Width="600px" />
    </div>
</div>
<div id="divImageFrame" style="display: none; background-color: Gray; padding: 2px">
    <div style="padding: 3px; background-color: White">
        <asp:Image ID="imgFrame" runat="server" Width="600px" />
    </div>
</div>
