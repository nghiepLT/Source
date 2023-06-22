<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TTThongketuyendung.ascx.cs"
    Inherits="WebCus.TTThongketuyendung" %>

<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script src="../js/select_tag_js/jquery.min.js" type="text/javascript"></script>
<script src="../js/select_tag_js/selectize.js" type="text/javascript"></script>
<script src="../js/select_tag_js/index.js" type="text/javascript"></script>
<link href="../js/select_tag_js/selectize.default.css" rel="stylesheet" type="text/css" />
<div class="page-title">
    <h2 class="icon-title">
        <span>Thống Kê tuyển dụng</span>
    </h2>
</div>
<table cellpadding="0" cellspacing="0" id="tbl_ctcontrol" runat="server">
    <tr class="trLabelFilter1" style="display: none;">
        <td>upload sp
            <asp:FileUpload ID="Upload" runat="server" />
            <asp:Button ID="Button3" runat="server" Text="Upload" OnClick="Click_upload" />
        </td>
    </tr>
    <tr class="trLabelFilter1">
        <td>Loại thống kê
        </td>
       
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr class="trLabelFilter1">
        <td class="RB_L">
            <asp:DropDownList ID="dropTructhuoc" AutoPostBack="true" runat="server" CssClass="select" Width="230px" Style="margin-top: 0px" OnSelectedIndexChanged="dropTructhuoc_SelectedIndexChanged">
                <asp:ListItem Text="Thống kê theo Phòng ban" Value="1" ></asp:ListItem>
                 <asp:ListItem Text="Thống kê theo nguồn tuyển dụng" Value="2" ></asp:ListItem>
            </asp:DropDownList>
        </td>
         
        <td class="RB_L">
            <radCln:RadDatePicker ID="txtDateFrom" CssClass="datePicker" Width="100px" AllowEmpty="false"
                MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup()" DateFormat="dd/MM/yyyy">
                </DateInput>
                <PopupButton Visible="False"></PopupButton>
            </radCln:RadDatePicker>
        </td>
        <td class="RB_L">
            <radCln:RadDatePicker ID="txtDateTo" CssClass="datePicker" Width="100px" AllowEmpty="false"
                MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup1()" DateFormat="dd/MM/yyyy">
                </DateInput>
                <PopupButton Visible="False"></PopupButton>
            </radCln:RadDatePicker>
        </td>
        <td class="RB_L" style="display:none;">
            <asp:DropDownList ID="drop_yeucautuyendung" CssClass="select" Width="120px" runat="server">
            </asp:DropDownList>
        </td>

        <td class="RB_L">
            <asp:Button ID="btnSearch" CssClass="btn-1" runat="server" Text="Xem" OnClick="btnSearch_Click" />
            &nbsp;&nbsp;&nbsp; 
        </td>
    </tr>
</table>
<hr />
<table class="text-center" width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td class="Line2"></td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvBanner" runat="server" Width="100%" AutoGenerateColumns="False"
                AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                CellPadding="0" BorderWidth="0px">
                <AlternatingRowStyle CssClass="row-alt"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="STT">
                        <ItemTemplate>
                            &nbsp
                                <%# Container.DisplayIndex+1%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                        <ItemStyle CssClass="RB_L" Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phòng ban">
                        <ItemTemplate>
                            <%#Eval("PhongBan")%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="RB_C" />
                        <ItemStyle CssClass="RB_L" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Số lượng tuyển dụng">
                        <ItemTemplate>
                            <%#Eval("SLTuyenDung")%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="RB_C" />
                        <ItemStyle CssClass="RB_L" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Đang tuyển dụng">
                        <ItemTemplate>
                            <%#Eval("Dangtuyendung")%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="RB_C" />
                        <ItemStyle CssClass="RB_L" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:GridView>
        </td>
    </tr>

</table>
<table class="text-center" width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td class="Line2"></td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvBanner2" runat="server" Width="100%" AutoGenerateColumns="False"
                AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                CellPadding="0" BorderWidth="0px">
                <AlternatingRowStyle CssClass="row-alt"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="STT">
                        <ItemTemplate>
                            &nbsp
                                <%# Container.DisplayIndex+1%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                        <ItemStyle CssClass="RB_L" Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phòng ban">
                        <ItemTemplate>
                            <%#Eval("PhongBan")%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="RB_C" />
                        <ItemStyle CssClass="RB_L" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Số lượng tuyển dụng">
                        <ItemTemplate>
                            <%#Eval("SLTuyenDung")%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="RB_C" />
                        <ItemStyle CssClass="RB_L" />
                    </asp:TemplateField>
                   
                </Columns>
                <RowStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:GridView>
        </td>
    </tr>

</table>
<script>
    function ToggleSecondPopup()
    {
        <%= txtDateFrom.ClientID %>.ShowPopup();
    }
    function ToggleSecondPopup1()
    {
        <%= txtDateTo.ClientID %>.ShowPopup();
    }
</script>
