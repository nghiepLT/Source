<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="NewsLetter.ascx.cs"
    Inherits="UserMng.NewsLetter" %>
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

<script type="text/javascript">
    function CheckAllItem(objcheckAll) {
        var obj = document.getElementsByName('chkItem');

        for (var i = 0; i < obj.length; i++) {
            var chkItem = obj[i];
            chkItem.checked = objcheckAll.checked;
        }
    }

    function CheckActive() {

        var strValueIDChecked = '';

        var arrChkItem = document.getElementsByName('chkItem');

        for (var i = 0; i < arrChkItem.length; i++) {
            var chkItem = arrChkItem[i];
            if (chkItem.checked == true) {
                strValueIDChecked += chkItem.value + '|';
            }

        }

        if (strValueIDChecked == '') {
            alert("Vui lòng chọn tin cần cập nhật cho hiển thị tin");
            return false;
        }
        else {
            var hdnIDs = document.getElementById('<%=hdnIDs.ClientID %>');
            hdnIDs.value = strValueIDChecked;
            return true;

        }

    } 

    function CheckAndConfirmDelete() {

        var strValueIDChecked = '';

        var arrChkItem = document.getElementsByName('chkItem');

        for (var i = 0; i < arrChkItem.length; i++) {
            var chkItem = arrChkItem[i];
            if (chkItem.checked == true) {
                strValueIDChecked += chkItem.value + '|';
            }

        }

        if (strValueIDChecked == '') {
            alert("Vui lòng chọn sản phẩm cần xóa");
            return false;
        }
        else {
            if (confirm('Bạn có chắc xóa các sản phẩm này không?')) {
                var hdnIDs = document.getElementById('<%=hdnIDs.ClientID %>');
                hdnIDs.value = strValueIDChecked;
                return true;
            }
            else
                return false;

        }
    }
</script>
<asp:HiddenField ID="hdnIDs" Value="" runat="server" />
<div class="page-title">
    <h2 class="icon-title">
        <span>NewsLetter</span>
    </h2>
</div>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td class="L">
                Tìm kiếm:
                <asp:DropDownList ID="ddlStatus" runat="server" Visible="false">
                    <asp:ListItem Text="Chọn trạng thái" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Đã xuất" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Chưa xuất" Value="0"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="ddlGender" runat="server" Visible="false">
                    <asp:ListItem Text="Chọn giới tính" Value=""></asp:ListItem>
                    <asp:ListItem Text="Nam" Value="Nam"></asp:ListItem>
                    <asp:ListItem Text="Nữ" Value="Nữ"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:TextBox ID="txtSearchText" runat="server" CssClass="Input_text"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Tìm" OnClick="btnSearch_Click" CssClass="btn-1" />
            </td>
            <td class="R">
                <asp:Button ID="btnActive" runat="server" Visible="false" OnClientClick="return CheckActive();"
                    Text="Cập nhật trạng thái chưa xuất" OnClick="btnActive_Click" CssClass="btn-1" ></asp:Button>&nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" OnClientClick="return CheckAndConfirmDelete();"
                    Text="Xóa" OnClick="btnDelete_Click" CssClass="btn-1"></asp:Button>&nbsp;&nbsp;
                <asp:Button ID="btnInsert" runat="server" Visible="false" Text="Xuất file excel" CssClass="btn-1" OnClick="btnInsert_Click" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
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
                        <asp:TemplateField HeaderText="#">
                            <HeaderTemplate>
                                <input id="chkAll" type="checkbox" onclick="CheckAllItem(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id="chkItem" type="checkbox" name="chkItem" value='<%# Eval("NewLetterID")%>' />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="STT">
                            <ItemTemplate>
                                &nbsp
                                <%# numpage() + Container.DisplayIndex + 1%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <%#Eval("Email")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Giới tính" Visible="false">
                            <ItemTemplate>
                                <%#Eval("Gender")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng thái" Visible="false">
                            <ItemTemplate>
                                <%#this.StrStatus(Eval("Status"))%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ngày tạo">
                            <ItemTemplate>
                                <%#Eval("AddDate","{0:dd/MM/yyyy}")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C"/>
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <%--<asp:ImageButton ID="imgEdit" ImageUrl="/images/icon/modify.gif" ToolTip="View Detail"
                                    CommandArgument='<%# Eval("NewLetterID") %>' runat="server" CommandName="EditItem" />
                                &nbsp;--%>
                                <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("NewLetterID") %>' CommandName="DeleteItem"
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
