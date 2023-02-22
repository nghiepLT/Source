<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TTDanhgiaungvien.ascx.cs"
     Inherits="WebCus.TTDanhgiaungvien" %>

<div class="page-title">
    <h2 class="icon-title">
        <span>Dánh giá ứng viên</span>
    </h2>
</div>
<div class="TboardBox">
    <hr />
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
                            <ItemStyle CssClass="RB_L" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Họ tên ứng viên">
                            <ItemTemplate>
                                <%#Eval("HoTen")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="Ngày tạo">
                            <ItemTemplate>
                                <%#Eval("CreatedDate")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phòng ban">
                            <ItemTemplate>
                                <%#Eval("PhongBan")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Chức năng">
                            <ItemTemplate>
                                &nbsp;
                                    <%#checkchucnang(Eval("Status").ToString(),Eval("Id").ToString())%>
                                &nbsp;
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
</div>

<style>
    #ctl00_MainContent_TTThongtinUngvien_gvBanner td {
        text-align: center !important;
    }
</style>
<script>
    function ShowPopupMapLink(id) {
        WindowOpen('RenderPopupUngvien.aspx?id=' + id, 'POpi', 850, 550, 'no');
        return false;
    }
    function ShowPopupMapLink2(id) {
        WindowOpen('RenderPopupDanhGia.aspx?id=' + id, 'POpi', 800, 550, 'no');
        return false;
    }
</script>
