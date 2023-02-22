<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ThongTinUngVien.aspx.cs" Inherits="WebCus.ThongTinUngVien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.0/css/font-awesome.css" />
    <input id="noidung" runat="server" style="display: none;" />
    <input type="file" style="display: none;" class="btn-group btn btn-info btn-xs upload" id="uploadAvatar" accept="application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint,text/plain, application/pdf, image/*" /><br />
    <div class="page-title">
        <h2 class="icon-title">
            <span>Quản lý thông tin ứng viên</span>
        </h2>
    </div>
    <div class="TboardBox">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tb_button" runat="server" visible="true">
            <tbody>
                <tr>
                    <td class="C"></td>
                    <td class="R">
                        <asp:Button ID="btnInsertBanner" runat="server" Text="Tạo mới" CssClass="btn-1" OnClick="btnInsertBanner_Click" />
                        &nbsp;
                    <asp:Button ID="btnSaveBanner" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSaveBanner_Click"
                        OnClientClick="return CheckValidBanner();" />
                        &nbsp;
                   
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
                    <asp:Label ID="Label3" runat="server" Text="Yêu cầu tuyển dụng"></asp:Label>
                </th>
                <td class="RB_L">
                    <asp:DropDownList ID="dropYeuCauTuyenDung" runat="server" CssClass="select" Width="350px" Style="margin-top: 0px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="15%" class="RB_L">
                    <asp:Label ID="lbl_tieude" runat="server" Text="Họ tên ứng viên"></asp:Label>
                </th>
                <td class="RB_L">
                    <asp:TextBox ID="txthoTen" CssClass="Input_text" Width="99%" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <hr />
        <table cellpadding="0" cellspacing="0">

            <tr class="trLabelFilter1">
                <td>Họ tên ứng viên</td>
                <td>Trực thuộc
                </td>
                <td>Yêu cầu tuyển dụng
                </td>
                <td>Trạng thái
                </td>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr class="trLabelFilter1">
                <td class="RB_L">
                    <asp:TextBox ID="txtTenUngVien" runat="server"></asp:TextBox>
                </td>
                <td class="RB_L">
                    <asp:DropDownList ID="drop_tructhuoc" CssClass="select" Width="120px"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_tructhuoc_SelectedIndexChanged">
                        <asp:ListItem Text="Nguyên Kim" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Chính Nhân" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Smart Connec" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="RB_L">
                    <asp:DropDownList AutoPostBack="true" ID="drop_yeucautuyendung" CssClass="select" Width="150px" runat="server" OnSelectedIndexChanged="drop_yeucautuyendung_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

                <td class="RB_L" style="display: none;">

                    <asp:DropDownList ID="drop_trangthai" CssClass="select" Width="120px"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_trangthai_SelectedIndexChanged">
                        <asp:ListItem Text="--Tất Cả--" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="RB_L" style="display: none;">
                    <asp:DropDownList AutoPostBack="true" ID="dropPhongban" CssClass="select" Width="150px" runat="server">
                        <asp:ListItem Text="--Tất Cả--" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td class="RB_L">
                     <asp:DropDownList ID="dropTrangThai" CssClass="select" Width="120px"
                         runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropTrangThai_SelectedIndexChanged">
                         <asp:ListItem Text="Tất cả" Value="0" Selected="True"></asp:ListItem>
                         <asp:ListItem Text="Chờ nhập thông tin" Value="1"></asp:ListItem>
                         <asp:ListItem Text="Chờ phỏng vấn" Value="2"></asp:ListItem>
                         <asp:ListItem Text="Chờ đánh giá" Value="3"></asp:ListItem>
                         <asp:ListItem Text="Chờ gửi thư mời nhận việc" Value="3"></asp:ListItem>
                         <asp:ListItem Text="Đánh giá chưa đạt" Value="4"></asp:ListItem>
                         <asp:ListItem Text="Chờ đồng bộ" Value="5"></asp:ListItem>
                         <asp:ListItem Text="Chờ khảo sát" Value="6"></asp:ListItem>
                         <asp:ListItem Text="Chờ đánh giá thử việc" Value="7"></asp:ListItem>
                         <asp:ListItem Text="Chờ Gửi mail chúc mừng" Value="8"></asp:ListItem>
                     </asp:DropDownList>
                </td>
                <td class="RB_L">
                    <asp:Button ID="btnSearch" CssClass="btn-1" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <hr />
        <div id="myfixtable">
            <table class="text-center" width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="Line2"></td>
                </tr>
                <tr>

                    <td>
                        <div style="overflow: scroll" class="scrolling">
                            <asp:GridView ID="gvBanner" runat="server" CssClass="longdiv" AutoGenerateColumns="False"
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
                                    <asp:TemplateField HeaderText="Họ tên">
                                        <ItemTemplate>
                                            <span class="hotenuv"><%#Eval("HoTen")%></span>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L " />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trạng thái">
                                        <ItemTemplate>
                                            <span style="color: #ff0000; text-decoration: underline" class="hotenuv"><%#Eval("TrangThai")%></span>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L " />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ngày tạo">
                                        <ItemTemplate>
                                            <%#Eval("CreatedDate")%>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C ngaytaouv" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trực thuộc">
                                        <ItemTemplate>
                                            <div class="thongtinbox">
                                                <%#CheckTrucThuc(int.Parse(Eval("TrucThuoc").ToString()))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Phòng ban">
                                        <ItemTemplate>
                                            <%#Eval("Phongban")%>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Thông tin ứng viên">
                                        <ItemTemplate>
                                            <div class="thongtinbox">
                                                <%#CheckChucnangNhapThongtin(Guid.Parse(Eval("Id").ToString()))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phỏng vấn ứng viên">
                                        <ItemTemplate>
                                            <div class="thongtinbox">
                                                <%#CheckChucnangPhongvan(Guid.Parse(Eval("Id").ToString()))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Đánh giá ứng viên">
                                        <ItemTemplate>
                                            <div class="thongtinbox">
                                                <%#CheckChucnangDanhGia(Guid.Parse(Eval("Id").ToString()))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Thư mời nhận việc">
                                        <ItemTemplate>
                                            <div class="thongtinbox">
                                                <%#CheckChucnangMoinhanviec(Guid.Parse(Eval("Id").ToString()))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Đồng bộ dữ liệu">
                                        <ItemTemplate>
                                            <asp:Button ID="imgDongBo" Text="Đồng bộ" ToolTip="Xóa" CssClass="btn-1"
                                                runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="DongBoItem"
                                                OnClientClick="return confirm('Bạn thật sự muốn đồng bộ nhân viên này?')" Visible='<%#CheckStatus(Eval("GuiThumoi").ToString())%>' />
                                            <div class="thongtinbox">
                                                <%#CheckChucnangDongbo(Guid.Parse(Eval("Id").ToString()))%>
                                            </div>
                                        </ItemTemplate>

                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quy trình, phúc lợi, khảo sát">
                                        <ItemTemplate>
                                            <div class="thongtinbox">
                                                <%#Checkchucnangquytrinh(Guid.Parse(Eval("Id").ToString()))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Đánh giá thử việc">
                                        <ItemTemplate>
                                            <div class="thongtinbox">
                                                <%#Checkchucnangdanhgiathuviec(Guid.Parse(Eval("Id").ToString()))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gửi mail chúc mừng">
                                        <ItemTemplate>
                                            <div class="thongtinbox">
                                                <%#Checkchucnangchucmung(Guid.Parse(Eval("Id").ToString()))%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Trạng thái">
                            <ItemTemplate>
                                &nbsp;
                                    <%#checktrangthai(Eval("Status").ToString(),Eval("Id").ToString())%>
                                &nbsp;
                            </ItemTemplate>
                              <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                                runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="DeleteItem"
                                                OnClientClick="return confirm('Bạn thật sự muốn xóa?')" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                                        <ItemStyle CssClass="B_C" Width="70px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>

            </table>
        </div>

    </div>

    <style>
        #ctl00_MainContent_TTThongtinUngvien_gvBanner td {
            text-align: center !important;
        }
    </style>
    <script src="js/jquery-1.8.2.js"></script>
    <script>
        function ShowPopupMapLink(id) {
            WindowOpen('RenderPopupUngvien.aspx?id=' + id, 'POpi', 850, 550, 'no');
            return false;
        }
        function ShowPopupMapLink2(id) {
            WindowOpen('RenderPopupDanhGia.aspx?id=' + id, 'POpi', 850, 550, 'no');
            return false;
        }
        function ShowPopupMapLink3(id) {
            WindowOpen('RenderPopupThumoinhanviec.aspx?id=' + id, 'POpi', 800, 550, 'no');
            return false;
        }
        function ShowPopupMapLink4(id) {
            //
            WindowOpen('RenderPopupDanhGiatuyenDung.aspx?id=' + id, 'POpi', 800, 550, 'no');
            return false;
        }
        function ShowPopupMapLink5(id) {
            //
            WindowOpen('RenderPopupThuChucMung.aspx?id=' + id, 'POpi', 630, 480, 'no');
            return false;
        }
        function ShowPopupMapLink6(id, type, userid) {
            //
            WindowOpen('RenderPopupNguoiDanhGiaThuViec.aspx?id=' + id + "&type=" + type + "&userid=" + userid, 'POpi', 630, 220, 'no');
            return false;
        }
        function ShowPopupMapLink7(id, type, userid) {
            //
            WindowOpen('RenderPopupNguoiDanhGia.aspx?id=' + id + "&type=" + type + "&userid=" + userid, 'POpi', 630, 220, 'no');
            return false;
        }
        function ShowPopupMapLinkDanhGia1(id, type, userid) {
            //
            WindowOpen('RenderPopupNguoiDanhGia.aspx?id=' + id + "&type=" + type + "&userid=" + userid, 'POpi', 630, 220, 'no');
            return false;
        }
        function ShowPopupMapLinkDanhGia2(id, type, userid) {
            //
            WindowOpen('RenderPopupNguoiDanhGia.aspx?id=' + id + "&type=" + type + "&userid=" + userid, 'POpi', 630, 220, 'no');
            return false;
        }
        var _id;
        function importf(id) {
            _id = id;
            $("#uploadAvatar").trigger("click");
        }

        var _URL = window.URL || window.webkitURL;
        $("#uploadAvatar").on('change', function () {
            var file, img;

            if ((file = this.files[0])) {
                sendFile(file);

            }
        });
        function sendFile(file) {
            console.log("sendfike")
            var formData = new FormData();
            formData.append('file', $('#uploadAvatar')[0].files[0]);
            $.ajax({
                url: "../UploadUngVien.ashx",
                type: "POST",
                data: formData,
                success: function (status) {
                    //Gọi ajax đọc file
                    $.ajax({
                        type: "POST", //POST
                        url: "ThongTinUngVien.aspx/UploadFileUngvien",
                        data: "{path:'" + status + "',id:'" + _id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == true) {
                                alert("Import dữ liệu thành công!");
                            }
                            else {
                                alert("Tên ứng viên không hợp lệ");
                            }
                        },
                        failure: function (response) {
                            alert(response.d);
                        },
                        error: function (response) {
                            alert(response.d);
                        }
                    });
                },
                processData: false,
                contentType: false,
                error: function () {
                    alert("Whoops something went wrong!");
                }
            });
        }
    </script>
</asp:Content>
