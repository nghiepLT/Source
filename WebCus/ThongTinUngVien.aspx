<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ThongTinUngVien.aspx.cs" Inherits="WebCus.ThongTinUngVien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.3.0/css/all.min.css" />
    <link href="../AdminCss/UngVienTuyenDung.css?v=1" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.0/css/font-awesome.css" />
    <input id="noidung" runat="server" style="display: none;" />
    <input type="file" style="display: none;" class="btn-group btn btn-info btn-xs upload" id="uploadAvatar" accept="application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint,text/plain, application/pdf, image/*" /><br />
    <input type="file" style="display: none;" class="btn-group btn btn-info btn-xs upload" id="uploadTMNV" accept="application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint,text/plain, application/pdf, image/*" /><br />
    <input id="ipungvien" runat="server" style="display: none" />
    <div class="page-title">
        <h2 class="icon-title">
            <span>Quản lý thông tin ứng viên</span>
        </h2>
    </div>
    <div class="blackdoor" onclick="blackdoorclick()">
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
        <table style="margin-bottom: 30px;" width="100%" border="0" cellspacing="0" cellpadding="0" id="tb_input" runat="server" visible="true">
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
        <%-- <hr />--%>
        <table cellpadding="0" cellspacing="0" style="display: none;">

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
                        <asp:ListItem Text="Tất cả" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Nguyên Kim" Value="1"></asp:ListItem>
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
                        <asp:ListItem Text="--Đạt" Value="1"></asp:ListItem>
                        <asp:ListItem Text="--Không đạt" Value="2"></asp:ListItem>
                        <asp:ListItem Text="--UV Suy nghĩ thêm" Value="3"></asp:ListItem>
                        <asp:ListItem Text="--Từ chối phỏng vấn" Value="4"></asp:ListItem>
                        <asp:ListItem Text="--Đang thử việc" Value="5"></asp:ListItem>
                        <asp:ListItem Text="--NV chính thức" Value="6"></asp:ListItem>
                        <asp:ListItem Text="--Không phù hợp" Value="7"></asp:ListItem>
                        <asp:ListItem Text="--Từ chối nhận việc" Value="8"></asp:ListItem>
                        <asp:ListItem Text="--Đã nghỉ việc" Value="9"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="RB_L">
                    <asp:Button ID="btnSearch" CssClass="btn-1" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <hr />
        <ul class="tabul">
            <li>
                <a class="achung" runat="server" id="a1" onclick="tabactive(1)">Đang thử việc <span>(</span><span id="rs1" runat="server"></span><span>) </span></a>
            </li>
            <li>
                <a class="achung" id="a2" runat="server" onclick="tabactive(2)">Không đạt <span>(</span><span id="rs2" runat="server"></span><span>) </span></a>
            </li>
            <li>
                <a class="achung" id="a3" runat="server" onclick="tabactive(3)">UV Suy nghĩ thêm <span>(</span><span id="rs3" runat="server"></span><span>) </span></a>
            </li>
            <li>
                <a class="achung" id="a4" runat="server" onclick="tabactive(4)">Từ chối phỏng vấn <span>(</span><span id="rs4" runat="server"></span><span>) </span></a>
            </li>
            <li>
                <a class="achung" id="a5" runat="server" onclick="tabactive(5)">NV chính thức <span>(</span><span id="rs5" runat="server"></span><span>) </span></a>
            </li>
            <li>
                <a class="achung" id="a6" runat="server" onclick="tabactive(6)">Không phù hợp <span>(</span><span id="rs6" runat="server"></span><span>) </span></a>
            </li>
            <li>
                <a class="achung" id="a7" runat="server" onclick="tabactive(7)">Từ chối nhận việc <span>(</span><span id="rs7" runat="server"></span><span>) </span></a>
            </li>
            <li>
                <a class="achung" id="a8" runat="server" onclick="tabactive(8)">Đã nghỉ việc <span>(</span><span id="rs8" runat="server"></span><span>) </span></a>
            </li>
        </ul>
        <div id="myfixtable" class="outer-container">
            <div class="inner-container">
                <table class="text-center" width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="Line2"></td>
                    </tr>
                    <tr>
                        <td id="tab1">
                            <div style="overflow: scroll; height: 330px;" class="scrolling">
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
                                        <asp:TemplateField HeaderText="Ngày tạo">
                                            <ItemTemplate>
                                                <%#Eval("CreatedDate")%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="RB_C ngaytaouv" />
                                            <ItemStyle CssClass="RB_L" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Yêu cầu tuyển dụng">
                                            <ItemTemplate>
                                                <span class="hotenuv"><%#Eval("TieuDe")%></span>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="RB_C" />
                                            <ItemStyle CssClass="RB_L " />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Họ tên">
                                            <ItemTemplate>
                                                <span class="hotenuv hoten"><%#Eval("HoTen")%></span>
                                                <div style="font-weight: bold; color: red; margin-top: 10px;">Trạng thái : <%#GetTrangThaiUngVien(Guid.Parse(Eval("Id").ToString()))%></div>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="RB_C stickyheader" />
                                            <ItemStyle CssClass="RB_L stickytd pd-15 mb-15" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ngày vào làm">
                                            <ItemTemplate>
                                                <div class="">
                                                    <%#GetNgayVL(Guid.Parse(Eval("Id").ToString()))%>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="RB_C" />
                                            <ItemStyle CssClass="RB_L" />
                                        </asp:TemplateField>
                                        <%--   <asp:TemplateField HeaderText="Trạng thái">
                                        <ItemTemplate>
                                            <span style="color: #ff0000; text-decoration: underline" class="hotenuv"><%#Eval("TrangThai")%></span>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L " />
                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Trực thuộc">
                                            <ItemTemplate>
                                                <div class="thongtinbox">
                                                    <%#CheckTrucThuc(int.Parse(Eval("TrucThuoc").ToString()))%>
                                                </div>
                                                <strong style="color: red">[ <%#Eval("Phongban")%> ]</strong>
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
                                            <HeaderStyle CssClass="RB_C ttuvcl" />
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
                                                    <%-- <a href="/Uploads/TuyenDung/ThuMoiNhanViec/<%#Eval("TMNVPath")%> ">
                                               File--%>
                                                    <%#CheckGTNV(Eval("TMNVPath").ToString())%>
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
                        <td style="display: none;" id="tab2">
                            <div style="overflow: scroll; height: 330px;" class="scrolling">
                                <asp:GridView ID="gvBanner2" runat="server" CssClass="longdiv" AutoGenerateColumns="False"
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
                                        <asp:TemplateField HeaderText="Ngày tạo">
                                            <ItemTemplate>
                                                <%#Eval("CreatedDate")%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="RB_C ngaytaouv headerfixed" />
                                            <ItemStyle CssClass="RB_L" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Yêu cầu tuyển dụng">
                                            <ItemTemplate>
                                                <span class="hotenuv"><%#Eval("TieuDe")%></span>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="RB_C" />
                                            <ItemStyle CssClass="RB_L " />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Họ tên">
                                            <ItemTemplate>
                                                <span class="hotenuv hoten"><%#Eval("HoTen")%></span>
                                                <div style="font-weight: bold; color: red; margin-top: 10px;">Trạng thái : <%#GetTrangThaiUngVien(Guid.Parse(Eval("Id").ToString()))%></div>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="RB_C stickyheader " />
                                            <ItemStyle CssClass="RB_L stickytd pd-15" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ngày vào làm">
                                            <ItemTemplate>
                                                <div class="">
                                                    <%#GetNgayVL(Guid.Parse(Eval("Id").ToString()))%>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="RB_C" />
                                            <ItemStyle CssClass="RB_L" />
                                        </asp:TemplateField>
                                        <%--   <asp:TemplateField HeaderText="Trạng thái">
                                        <ItemTemplate>
                                            <span style="color: #ff0000; text-decoration: underline" class="hotenuv"><%#Eval("TrangThai")%></span>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="RB_C" />
                                        <ItemStyle CssClass="RB_L " />
                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Trực thuộc">
                                            <ItemTemplate>
                                                <div class="thongtinbox">
                                                    <%#CheckTrucThuc(int.Parse(Eval("TrucThuoc").ToString()))%>
                                                </div>
                                                <strong style="color: red">[ <%#Eval("Phongban")%> ]</strong>
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
                                            <HeaderStyle CssClass="RB_C ttuvcl" />
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
                                                    <%-- <a href="/Uploads/TuyenDung/ThuMoiNhanViec/<%#Eval("TMNVPath")%> ">
                                               File--%>
                                                    <%#CheckGTNV(Eval("TMNVPath").ToString())%>
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

    </div>

    <div id="mymodal">
        <div class="ttpv_title">
            Trạng thái ứng viên  <a onclick="closeMyModal()"><i class="fa fa-close"></i></a>
            <div>
                <span id="htuv"></span>
            </div>
        </div>
        <div class="ttpv_box">

            <div class="ttpv_title2">
                1. Thông tin phỏng vấn
            </div>
            <ul>
                <li>
                    <input id="ip1" type="radio" name="ttpv" runat="server" />
                    <span onclick="clickip('ip1')">- Đạt</span>
                </li>
                <li>
                    <input id="ip2" type="radio" name="ttpv" runat="server" />
                    <span onclick="clickip('ip2')">- Không đạt</span>
                </li>
                <li>
                    <input id="ip3" type="radio" name="ttpv" runat="server" />
                    <span onclick="clickip('ip3')">- UV Suy nghĩ thêm</span>
                </li>
                <li>
                    <input id="ip4" type="radio" name="ttpv" runat="server" />
                    <span onclick="clickip('ip4')">- Từ chối phỏng vấn</span>
                </li>
            </ul>
            <div class="ttpv_title2">
                2. Thông tin nhận việc
            </div>
            <ul>
                <li>
                    <input onclick="clk(5)" id="ip5" type="radio" name="ttnv" runat="server" />
                    <span onclick="clickip('ip5')">- Đang thử việc</span>
                </li>
                <li>
                    <input onclick="clk(6)" id="ip6" type="radio" name="ttnv" runat="server" />
                    <span onclick="clickip('ip6')">- NV chính thức</span>
                </li>
                <li>
                    <input onclick="clk(7)" id="ip7" type="radio" name="ttnv" runat="server" />
                    <span onclick="clickip('ip7')">- Không phù hợp</span>
                </li>
                <li>
                    <input onclick="clk(8)" id="ip8" type="radio" name="ttnv" runat="server" />
                    <span onclick="clickip('ip8')">- Từ chối nhận việc</span>
                </li>
                <li>
                    <input onclick="clk(9)" id="ip9" type="radio" name="ttnv" runat="server" />
                    <span onclick="clickip('ip9')">- Đã nghỉ việc</span>
                </li>
            </ul>
            <div>
                <asp:Button ID="Button1" runat="server" Text="Cập nhật" CssClass="btnduyet" OnClick="btnSaveBanner_Click2"
                    OnClientClick="return CheckValidBanner();" />
            </div>
        </div>

    </div>
    <style>
        #ctl00_MainContent_TTThongtinUngvien_gvBanner td {
            text-align: center !important;
        }

        .btnCapnhat {
            margin-left: 5px;
            background-color: #005399;
            color: white;
            font-size: 12px;
            padding: 5px 13px;
            cursor: pointer;
            text-decoration: none !important;
        }

            .btnCapnhat:hover {
                background-color: #083f6d;
                color: white;
            }

        .tabul {
            margin: 0px;
            padding: 0px;
        }

            .tabul li {
                display: inline-block;
                    margin-bottom: 0px;
            }

                .tabul li a {
                    display: inline-block;
                    padding: 3px 5px;
                    text-decoration: none !important;
                    font-weight: bold;
                    border-right: 1px solid #d3d3d3;
                    font-size: 11.4px;
                }

        .tabactive {
            color: white !important;
            color: white;
        }

        #mymodal {
            position: fixed;
            left: 41%;
            top: 12%;
            background-color: white;
            z-index: 999999999;
            width: 19%;
            box-shadow: 1px 2px 3px 1px #161616;
        }

        .ttpv_title {
            background-color: #005399;
            padding: 5px 10px;
            color: white;
            font-weight: bold;
            text-align: center;
            border-radius: 3px 3px 0px 0px;
        }

        .ttpv_box {
            padding: 10px 20px;
        }

        .ttpv_title2 {
            margin-bottom: 5px;
            font-weight: bold;
            text-transform: capitalize;
        }

        .ttpv_title a {
            color: white;
            position: absolute;
            right: 10px;
            font-size: 14px;
            top: 8px;
        }

        .ttpv_title i {
        }

        .ttpv_box span {
            cursor: pointer;
            position: relative;
            top: -1.9px;
        }

        .ttpv_box li:hover span {
            color: #005399;
        }

        .ttpv_box input {
            cursor: pointer;
        }

        .blackdoor {
            background-color: #000000ad;
            width: 100%;
            min-height: 100%;
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 999999999;
            display: none;
        }
    </style>
    <script src="js/jquery-1.8.2.js"></script>
    <script>
        $(document).ready(function () {
            var lstRt = $("#ctl00_MainContent_gvBanner th");
            lstRt.each(function () {
                $(this).addClass("headerfixed");
            });
            $("#mymodal").hide();
        });
        function ShowPopupMapLink(id) {
            WindowOpen('RenderPopupUngvien.aspx?id=' + id, 'POpi', 850, 550, 'no');
            return false;
        }
        function ShowPopupMapLink2(id) {
            WindowOpen('RenderPopupDanhGia.aspx?id=' + id, 'POpi', 900, 550, 'no');
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
        function ShowPopupNgayVaolam(id) {
            WindowOpen('RenderPopupCapnhatngaylam.aspx?id=' + id, 'POpi', 320, 320, 'no');
        }
        var _id;
        function importf(id) {
            _id = id;
            $("#uploadAvatar").trigger("click");
        }
        function blackdoorclick() {
            $("#mymodal").hide();
            $(".blackdoor").hide();
        }
        function capnhattrangthai(id, hoten, st, st2) {
            $(".blackdoor").show();
            // WindowOpen('RenderPopupCapNhatTrangThai.aspx?id=' + id, 'POpi', 320, 120, 'no');
            $("#mymodal").fadeIn(100);
            $("#htuv").html(hoten);

            //
            $("#ctl00_MainContent_ip1").removeAttr("checked");
            $("#ctl00_MainContent_ip2").removeAttr("checked");
            $("#ctl00_MainContent_ip3").removeAttr("checked");
            $("#ctl00_MainContent_ip4").removeAttr("checked");
            $("#ctl00_MainContent_ip5").removeAttr("checked");
            $("#ctl00_MainContent_ip6").removeAttr("checked");
            $("#ctl00_MainContent_ip7").removeAttr("checked");
            $("#ctl00_MainContent_ip8").removeAttr("checked");
            $("#ctl00_MainContent_ip9").removeAttr("checked");

            if (st != "") {
                if (st == 1) {
                    $("#ctl00_MainContent_ip1").prop("checked", true);
                }
                if (st == 2) {
                    $("#ctl00_MainContent_ip2").prop("checked", true);
                }
                if (st == 3) {
                    $("#ctl00_MainContent_ip3").prop("checked", true);
                }
                if (st == 4) {
                    $("#ctl00_MainContent_ip4").prop("checked", true);
                }
                if (st2 == 5) {
                    $("#ctl00_MainContent_ip5").prop("checked", true);
                }
                if (st2 == 6) {
                    $("#ctl00_MainContent_ip6").prop("checked", true);
                }
                if (st2 == 7) {
                    $("#ctl00_MainContent_ip7").prop("checked", true);
                }
                if (st2 == 8) {
                    $("#ctl00_MainContent_ip8").prop("checked", true);
                }
                if (st2 == 9) {
                    $("#ctl00_MainContent_ip9").prop("checked", true);
                }
            }
            $("#ctl00_MainContent_ipungvien").val(id);
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
                                location.reload();
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
        //Thu moi nhan viec
        $("#uploadTMNV").on('change', function () {
            var file, img;

            if ((file = this.files[0])) {
                sendFileTMNV(file);
            }
        });
        function sendFileTMNV(file) {
            var formData = new FormData();
            formData.append('file', $('#uploadTMNV')[0].files[0]);
            $.ajax({
                url: "../UploadImages.ashx",
                type: "POST",
                data: formData,
                success: function (status) {
                    //Gọi ajax đọc file
                    $.ajax({
                        type: "POST", //POST
                        url: "ThongTinUngVien.aspx/UploadFileTMNV",
                        data: "{path:'" + status + "',id:'" + _idTMNV + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == true) {
                                alert("Import dữ liệu thành công!");
                                location.reload();
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
        function Boqua(id) {
            $.ajax({
                type: "POST", //POST
                url: "ThongTinUngVien.aspx/NextPV",
                data: "{id:'" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    window.location.reload();
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
        var _idTMNV;
        function importTMNV(id) {
            _idTMNV = id;
            $("#uploadTMNV").trigger("click");
        }

        function tabactive(id) {
            //$(".achung").removeClass("tabactive");
            //if (id == 1) {
            //    $("#a1").addClass("tabactive");
            //    $("#tab1").show();
            //    $("#tab2").hide();
            //}
            //if (id == 2) {
            //    $("#a2").addClass("tabactive");
            //    $("#tab1").hide();
            //    $("#tab2").show();
            //}
            var url = window.location.origin + window.location.pathname;
            window.location.href = url + "?type=" + id;
        }

        function closeMyModal() {
            $("#mymodal").toggle();
            $(".blackdoor").hide();
        }
        function clickip(id) {
            var check = $("#ctl00_MainContent_" + id).attr("checked");
            if (check == "checked") {
                $("#ctl00_MainContent_" + id).attr("checked", false);
            }
            else {
                $("#ctl00_MainContent_" + id).attr("checked", true);
            }
            var rm = id.replace("ip", "");
            if (rm > 4) {
                $("#ctl00_MainContent_ip1").prop("checked", true);
            }
            else {
                $("#ctl00_MainContent_ip5").removeAttr("checked");
                $("#ctl00_MainContent_ip6").removeAttr("checked");
                $("#ctl00_MainContent_ip7").removeAttr("checked");
                $("#ctl00_MainContent_ip8").removeAttr("checked");
                $("#ctl00_MainContent_ip9").removeAttr("checked");
            }
            //$("#ctl00_MainContent_" + id).prop("checked", true);
        }

        function clk(id) {

        }

        $('input:radio').click(function () {
            $("#ctl00_MainContent_ip1").prop("checked", true);
        });



        function CheckValidBanner() {
            if ($("#ctl00_MainContent_txthoTen").val() == '') {
                alert("Vui lòng nhập họ tên ứng viên");
                return false;
            }
            //Kiem tra trung ten

           
        }
    </script>

</asp:Content>
