<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ThietLapGuiMail.aspx.cs" Inherits="WebCus.ThietLapGuiMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
    <div class="page-title">
        <h2 class="icon-title">
            <span>Thiết lập Email</span>
        </h2>
    </div>
    <div class="TboardBox">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tb_button" runat="server" visible="true">
            <tbody>
                <tr>
                    <td class="C"></td>
                    <td class="R">
                        <asp:Button ID="btnSaveBanner" runat="server" Text="Thêm" CssClass="btn-1" OnClick="btnSaveBanner_Click"
                            OnClientClick="return CheckValidBanner();" />
                        &nbsp;  &nbsp;
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tb_input" runat="server" visible="true">
            <tr>
                <td colspan="4" height="5px"></td>
            </tr>
            <tr>
                <td colspan="4" class="Line2"></td>
            </tr>


            <tr>
                <th width="15%" class="RB_L">
                    <asp:Label ID="lbl_tieude" runat="server" Text="Email"></asp:Label>
                </th>
                <td class="RB_L"> 
                    <input type="text" id="txtEmail" class="Input_text" style="width:99%" onkeyup="autogetname()" runat="server" />
                    <ul id="ulcontents">

                    </ul>
                </td>
            </tr>
        </table>
        <hr />
        <br />
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
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <%#Eval("Email")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="RB_L" />
                                <ItemStyle CssClass="RB_L" />
                            </asp:TemplateField>
                         
                          <%--  <asp:CheckBoxField DataField="Status" HeaderText="An hien" SortExpression="Status" />--%>
                            <asp:TemplateField HeaderText="Ẩn hiện">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkview" Checked='<%# Eval("Status") %>' CommandName='<%# Eval("IDMail") %>'   runat="server" AutoPostBack="true" OnCheckedChanged="chkview_CheckedChanged" />
                                </ItemTemplate>
                                   <HeaderStyle CssClass="RB_L" />
                                <ItemStyle CssClass="RB_L" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                        runat="server" CommandArgument='<%# Eval("IDMail") %>' CommandName="DeleteItem"
                                        OnClientClick="return confirm('Bạn thật sự muốn ẩn Email này?')" />
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

        </table>
    </div>
    
    <script>
        $(document).ready(function () {
            $.ajax({
                type: "POST", //POST
                url: "ThietLapGuiMail.aspx/Autugetname",
                data: "{key:'" + $("#txtEmail").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    var getsplit = msg.d;
                    var getss = getsplit.split(',');
                  
                    arraynv = [];
                    for (i = 0; i < getss.length; i++) {
                        if (getss[i] != '' && getss[i] != null) {
                            //html += "<li>";
                            //html += "<a>";
                            //html += getss[i];
                            //html += "</a>";
                            //html += "<li>";
                            arraynv.push(getss[i])
                        }

                    }
                    console.log(arraynv);
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });
        var arraynv = [];
        function autogetname() {
            $("#ulcontents").show();
            //alert($("#txtEmail").val());
            var html = "";
            if ($("#ctl00_MainContent_txtEmail").val() != "") {
                for (i = 0; i < arraynv.length; i++) {
                    if (arraynv[i].toLowerCase().includes($("#ctl00_MainContent_txtEmail").val().toLowerCase())) {
                        html += "<li>";
                        html += "<a onclick=\"selectitem('"+arraynv[i].split('-')[1]+"')\">";
                        html += arraynv[i];
                        html += "</a>";
                        html += "<li>";
                    }
                }
            }
            $("#ulcontents").html(html);
        }

        function selectitem(value) {
            $("#ctl00_MainContent_txtEmail").val(value);
            $("#ulcontents").hide();
        }
    </script> 
</asp:Content>
