<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ThongKeKhaoSat.aspx.cs" Inherits="WebCus.ThongKeKhaoSat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title">
        <h2 class="icon-title">
            <span>Thống kê khảo sát</span>
        </h2>
    </div>
    <table cellpadding="0" cellspacing="0" id="tbl_ctcontrol" runat="server">

        <tr class="trLabelFilter1">
            <td>Tình trạng khảo sát
            </td>

            <td colspan="2">&nbsp;
            </td>
        </tr>
        <tr class="trLabelFilter1">
            <td class="RB_L">
                <asp:DropDownList ID="dropTructhuoc" AutoPostBack="true" runat="server" CssClass="select" Width="230px" Style="margin-top: 0px">
                    <asp:ListItem Text="Tất cả" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Chưa khảo sát" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Đã khảo sát" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>

            <td class="RB_L"></td>
            <td class="RB_L"></td>
            <td class="RB_L" style="display: none;">
                <asp:DropDownList ID="drop_yeucautuyendung" CssClass="select" Width="120px" runat="server">
                </asp:DropDownList>
            </td>

            <td class="RB_L">
                <asp:Button ID="btnSearch" CssClass="btn-1" runat="server" Text="Xem" OnClick="btnSearch_Click" />
                &nbsp;&nbsp;&nbsp; 
            </td>
        </tr>
    </table> 
    <table class="text-center" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="Line2"></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvBanner" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px" OnRowCommand="gvBanner_RowCommand">
                    <AlternatingRowStyle CssClass="row-alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="STT">
                            <ItemTemplate>
                                &nbsp
                                <%# Container.DisplayIndex+1%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Họ tên">
                            <ItemTemplate>
                                <%#Eval("HoTenUngVien")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Số ngày làm">
                            <ItemTemplate>
                                <%#Eval("SoNgayLam")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="Khảo sát 7 ngày">
                            <ItemTemplate>
                                   <%#Get7Ngay(DateTime.Parse(Eval("Ks7Ngay").ToString()))%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Khảo sát 14 ngày">
                            <ItemTemplate>
                                   <%#Get14Ngay(DateTime.Parse(Eval("Ks14Ngay").ToString()))%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Khảo sát 2 tháng">
                            <ItemTemplate>
                                   <%#Get2Thang(DateTime.Parse(Eval("Ks2Thang").ToString()))%>
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

    </script>
</asp:Content>
