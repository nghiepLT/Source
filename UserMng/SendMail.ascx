<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="SendMail.ascx.cs" Inherits="UserMng.SendMail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    function CheckAllItem(objcheckAll) {
        var obj = document.getElementsByName('chkItem');

        for (var i = 0; i < obj.length; i++) {
            var chkItem = obj[i];
            chkItem.checked = objcheckAll.checked;
        }
    }

    function getChkbox() {
        var strValueIDChecked = '';

        var arrChkItem = document.getElementsByName('chkItem');

        for (var i = 0; i < arrChkItem.length; i++) {
            var chkItem = arrChkItem[i];
            if (chkItem.checked == true) {
                strValueIDChecked += chkItem.value + ',';
            }

        }

        if (strValueIDChecked == '') {
            alert("Vui lòng chọn email");
            return false;
        }
        else {
            var hdnIDs = document.getElementById('<%=hdnIDs.ClientID %>');
            hdnIDs.value = strValueIDChecked;
            return true;
        }
    }
</script>

<asp:HiddenField ID="hdnIDs" Value="" runat="server" />

<div class="page-title">
    <h2 class="icon-title">
        <span>Gửi Mail</span>
    </h2>
</div>
<br>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tbody>
        <tr>
            <td class="C"></td>
            <td class="R">
                <asp:Button ID="btnSend" runat="server" Text="Thêm mới" CssClass="btn-1" OnClick="btnSend_Click" OnClientClick="return getChkbox();" />
            </td>
        </tr>
    </tbody>
</table>
<div class="TboardBox">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="4" class="Line2"></td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label5" runat="server" Text="Tiêu đề"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="Input_text" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                <asp:Label ID="Label1" runat="server" Text="Nội dung"></asp:Label>
            </th>
            <td class="B_L">
                <asp:TextBox ID="txtContent" runat="server" CssClass="Input_text" Width="100%" Height="200" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="15px"></td>
        </tr>
        <tr>
            <td class="Line2"></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvUser" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <HeaderTemplate>
                                <input id="chkAll" type="checkbox" onclick="CheckAllItem(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id="chkItem" style='display: <%# String.IsNullOrEmpty(Eval("UserID").ToString())?"none":""%>;'
                                    type="checkbox" name="chkItem" value='<%# Eval("UserID")%>' class="chkbox" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" CssClass="RB_C" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_C" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UserID">
                            <ItemTemplate>
                                &nbsp
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UserID") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="8%" CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText1" runat="server" Text='<%# Eval("UserName")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Email")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Số điện thoại">
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblResourceText2" runat="server" Text='<%# Eval("Tel")%>' />
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="Center" />
                            <ItemStyle CssClass="B_L" Width="20%" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="Line1" height="1"></td>
        </tr>
    </table>
</div>
