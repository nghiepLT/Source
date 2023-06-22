<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Khaosathoinhap.aspx.cs" Inherits="WebCus.Khaosathoinhap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="AdminCss/bootstrap.css" rel="stylesheet" />
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" /> 
    <input id="type" runat="server" style="display:none;"/>
       <input id="step" runat="server" style="display:none;" />
    <div class="khaosathoinhap_box">
         <div class="khaosathoinhap_tab">
            <ul>
                <li id="li7ngay" runat="server">
                    <a id="atab1" onclick="tabclick(1)" runat="server" class="khaosathoinhap_tab_active achung">
                        Trong 7 ngày
                    </a>
                </li>
                 <li  >
                    <a id="atab2" onclick="tabclick(2)" class="achung" runat="server">
                        Trong 14 ngày
                    </a>
                </li>
                 <li  >
                    <a id="atab3" onclick="tabclick(3)" class="achung" runat="server">
                        Sau 2 tháng
                    </a>
                </li>
                 
            </ul>
        </div>
        <table class="table table-bordered">
            <tr>
                <th class="text-center">
                    <img src="Images/logo_form.png" class="img-responsive" />
                </th>
                <th>
                    <span>BẢNG KHẢO SÁT HỘI NHẬP BAN ĐẦU (<span id="songay" runat="server">7 ngày</span>)</span>
                </th>
            </tr>
            <tr>
                <th>
                    Họ tên: <input style="text-transform:uppercase;width:76%" type="text" id="hoten" class="dotted" runat="server"/>
                </th>
                <th>
                     Vị trí:  <input type="text" id="vitri" class="dotted" runat="server"/>
                </th>
            </tr>
        </table>
        <div>
            Ngày nhận việc: Ngày <strong style="color:red" id="spngaynhanviec" runat="server"></strong> tháng <strong style="color:red" id="spthangnhanviec" runat="server"></strong> năm <strong style="color:red" id="spnamnhanviec" runat="server"></strong>
        </div>
        <div>
            Thời gian ghi nhận khảo sát: Ngày <strong  id="spngayhientai" runat="server"></strong> tháng <strong  id="spthanghientai" runat="server"></strong> năm <strong  id="spnamghientai" runat="server"></strong>
        </div>
       <div>
           <strong style="color:red;font-style:italic;margin:5px 0px;display:inline-block">Đánh dấu vào các ô ý kiến đánh giá mà bạn lựa chọn</strong>
       </div>
        <div id="tab1" class="tabchung">    
            <table class="table table-bordered">
                <thead> 
                    <tr>
                        <th rowspan="2">STT
                        </th>
                        <th rowspan="2">Ý KIẾN
                        </th>
                        <th colspan="5" class="text-center">Ý KIẾN ĐÁNH GIÁ	 
                        </th>
                    </tr>
                    <tr>
                        <th>Còn ít thông tin
                        </th>
                        <th>Ngắn gọn
                        </th>
                        <th>Khá đầy đủ
                        </th>
                        <th>Vừa đủ
                        </th>
                        <th>Quá nhiều thông tin
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>I.
                        </td>
                        <td>
                            <strong>Hướng dẫn ngày đầu nhận việc của phòng HCNS</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radHuongdanngaydau1" class="dotted ks_radio" name="radyk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radHuongdanngaydau2" class="dotted ks_radio" name="radyk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radHuongdanngaydau3" class="dotted ks_radio" name="radyk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radHuongdanngaydau4" class="dotted ks_radio" name="radyk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radHuongdanngaydau5" class="dotted ks_radio" name="radyk1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.1
                        </td>
                        <td>- Tóm tắt nội quy làm việc
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTomtatnoiquylamviec1" class="dotted ks_radio" name="radyk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTomtatnoiquylamviec2" class="dotted ks_radio" name="radyk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radTomtatnoiquylamviec3" class="dotted ks_radio" name="radyk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTomtatnoiquylamviec4" class="dotted ks_radio" name="radyk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTomtatnoiquylamviec5" class="dotted ks_radio" name="radyk2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.2
                        </td>
                        <td>- Tóm tắt chính sách phúc lợi
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTomtatchinhsachphutloi1" class="dotted ks_radio" name="radyk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTomtatchinhsachphutloi2" class="dotted ks_radio" name="radyk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radTomtatchinhsachphutloi3" class="dotted ks_radio" name="radyk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTomtatchinhsachphutloi4" class="dotted ks_radio" name="radyk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTomtatchinhsachphutloi5" class="dotted ks_radio" name="radyk3" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.3
                        </td>
                        <td>- Biên nhận bằng gốc
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radBiennhanbanggoc1" class="dotted ks_radio" name="radyk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radBiennhanbanggoc2" class="dotted ks_radio" name="radyk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radBiennhanbanggoc3" class="dotted ks_radio" name="radyk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radBiennhanbanggoc4" class="dotted ks_radio" name="radyk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radBiennhanbanggoc5" class="dotted ks_radio" name="radyk4" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.4
                        </td>
                        <td>- Cam kết thực hiện trách nhiệm lái xe
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCamket1" class="dotted ks_radio" name="radyk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCamket2" class="dotted ks_radio" name="radyk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radCamket3" class="dotted ks_radio" name="radyk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCamket4" class="dotted ks_radio" name="radyk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCamket5" class="dotted ks_radio" name="radyk5" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.5
                        </td>
                        <td>- Hướng dẫn ghi lịch trình hoạt động của lái xe
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radHuongdan1" class="dotted ks_radio" name="radyk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radHuongdan2" class="dotted ks_radio" name="radyk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radHuongdan3" class="dotted ks_radio" name="radyk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radHuongdan4" class="dotted ks_radio" name="radyk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radHuongdan5" class="dotted ks_radio" name="radyk6" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>II
                        </td>
                        <td><strong>Những thông tin ngày đầu tiên nhận việc của phòng HCNS</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNhungthongtinngaydau1" class="dotted ks_radio" name="radyk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNhungthongtinngaydau2" class="dotted ks_radio" name="radyk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radNhungthongtinngaydau3" class="dotted ks_radio" name="radyk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNhungthongtinngaydau4" class="dotted ks_radio" name="radyk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNhungthongtinngaydau5" class="dotted ks_radio" name="radyk7" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>III
                        </td>
                        <td><strong>Nội dung hướng dẫn hội nhập qua mail</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidunghuongdan1" class="dotted ks_radio" name="radyk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidunghuongdan2" class="dotted ks_radio" name="radyk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radNoidunghuongdan3" class="dotted ks_radio" name="radyk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidunghuongdan4" class="dotted ks_radio" name="radyk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidunghuongdan5" class="dotted ks_radio" name="radyk8" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>III.1
                        </td>
                        <td><strong>- File hướng dẫn hội nhập</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilehuongdanhoinhap1" class="dotted ks_radio" name="radyk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilehuongdanhoinhap2" class="dotted ks_radio" name="radyk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radFilehuongdanhoinhap3" class="dotted ks_radio" name="radyk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilehuongdanhoinhap4" class="dotted ks_radio" name="radyk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilehuongdanhoinhap5" class="dotted ks_radio" name="radyk9" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>III.2
                        </td>
                        <td><strong>- File Nội quy công ty</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilenoiquycongty1" class="dotted ks_radio" name="radyk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilenoiquycongty2" class="dotted ks_radio" name="radyk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radFilenoiquycongty3" class="dotted ks_radio" name="radyk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilenoiquycongty4" class="dotted ks_radio" name="radyk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilenoiquycongty5" class="dotted ks_radio" name="radyk10" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>III.3
                        </td>
                        <td><strong>- File sơ đồ nhân sự công ty</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilehosonhansu1" class="dotted ks_radio" name="radyk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilehosonhansu2" class="dotted ks_radio" name="radyk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radFilehosonhansu3" class="dotted ks_radio" name="radyk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilehosonhansu4" class="dotted ks_radio" name="radyk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radFilehosonhansu5" class="dotted ks_radio" name="radyk11" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><strong>ĐÁNH GIÁ & CẢM NHẬN</strong></td>
                        <td><strong>Rất không ấn tượng</strong></td>
                        <td><strong>Ít ấn tượng</strong></td>
                        <td><strong>Không ý kiến</strong></td>
                        <td><strong>Bình thường</strong></td>
                        <td><strong>Ấn tượng</strong></td>
                    </tr>
                    <tr>
                        <td>1</td>
                        <td><strong>Ngày đầu nhận việc</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNgaydaunhanviec1" class="dotted ks_radio" name="radyk12" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNgaydaunhanviec2" class="dotted ks_radio" name="radyk12" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radNgaydaunhanviec3" class="dotted ks_radio" name="radyk12" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNgaydaunhanviec4" class="dotted ks_radio" name="radyk12" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNgaydaunhanviec5" class="dotted ks_radio" name="radyk12" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td><strong>Trao đổi với Phòng NS</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTraodoivoiphongnhansu1" class="dotted ks_radio" name="radyk13" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTraodoivoiphongnhansu2" class="dotted ks_radio" name="radyk13" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radTraodoivoiphongnhansu3" class="dotted ks_radio" name="radyk13" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTraodoivoiphongnhansu4" class="dotted ks_radio" name="radyk13" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTraodoivoiphongnhansu5" class="dotted ks_radio" name="radyk13" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td><strong>Tiếp xúc với nhân sự của bộ phận</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTiepxucvoinhansucuabophan1" class="dotted ks_radio" name="radyk14" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTiepxucvoinhansucuabophan2" class="dotted ks_radio" name="radyk14" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radTiepxucvoinhansucuabophan3" class="dotted ks_radio" name="radyk14" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTiepxucvoinhansucuabophan4" class="dotted ks_radio" name="radyk14" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTiepxucvoinhansucuabophan5" class="dotted ks_radio" name="radyk14" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>4</td>
                        <td><strong>Tiếp xúc với công việc tại bộ phận</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTiepxucvoicongviectaibophan1" class="dotted ks_radio" name="radyk15" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTiepxucvoicongviectaibophan2" class="dotted ks_radio" name="radyk15" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radTiepxucvoicongviectaibophan3" class="dotted ks_radio" name="radyk15" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTiepxucvoicongviectaibophan4" class="dotted ks_radio" name="radyk15" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radTiepxucvoicongviectaibophan5" class="dotted ks_radio" name="radyk15" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <strong>GÓP Ý CỦA NHÂN VIÊN THỬ VIỆC</strong>
                        </td>
                        <td colspan="5"><strong>Ghi ý kiến cá nhân				
                        </strong></td>
                    </tr>
                    <tr>
                        <td>1</td>
                        <td>- Đối với phòng HCNS
                        </td>
                        <td colspan="5">
                            <textarea id="doivoiphongnhansu" runat="server" style="pointer-events: all;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>- Đối với bộ phận làm việc trực tiếp

                        </td>
                        <td colspan="5">
                            <textarea id="doivoibophanlamviectructiep" runat="server" style="pointer-events: all;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td>- Đối với công ty

                        </td>
                        <td colspan="5">
                            <textarea id="doivoicongty" runat="server" style="pointer-events: all;"></textarea>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="tab2" class="tabchung">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th rowspan="2">STT
                        </th>
                        <th rowspan="2">NỘI DUNG
                        </th>
                        <th colspan="5" class="text-center">Ý KIẾN ĐÁNH GIÁ	 
                        </th>
                    </tr>
                    <tr>
                        <th>Nhiều khó khăn
                        </th>
                        <th>Nhiều thao tác
                        </th>
                        <th>Bình thường
                        </th>
                        <th>Dễ dàng
                        </th>
                        <th>Phức tạp
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>I.
                        </td>
                        <td>
                            <strong>Phần mềm quản lý nhân sự</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radPhanmemquanlynhansu1" class="dotted ks_radio" name="radt2yk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radPhanmemquanlynhansu2" class="dotted ks_radio" name="radt2yk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radPhanmemquanlynhansu3" class="dotted ks_radio" name="radt2yk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radPhanmemquanlynhansu4" class="dotted ks_radio" name="radt2yk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radPhanmemquanlynhansu5" class="dotted ks_radio" name="radt2yk1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.1
                        </td>
                        <td>- Sử dụng phần mềm đăng ký phép 
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyphep1" class="dotted ks_radio" name="radt2yk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyphep2" class="dotted ks_radio" name="radt2yk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radSudungphanmemdangkyphep3" class="dotted ks_radio" name="radt2yk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyphep4" class="dotted ks_radio" name="radt2yk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyphep5" class="dotted ks_radio" name="radt2yk2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.2
                        </td>
                        <td>- Sử dụng phần mềm đăng ký tăng ca
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkytangca1" class="dotted ks_radio" name="radt2yk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkytangca2" class="dotted ks_radio" name="radt2yk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radSudungphanmemdangkytangca3" class="dotted ks_radio" name="radt2yk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkytangca4" class="dotted ks_radio" name="radt2yk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkytangca5" class="dotted ks_radio" name="radt2yk3" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.3
                        </td>
                        <td>- Sử dụng phần mềm đăng ký ghi công việc
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkycongviec1" class="dotted ks_radio" name="radt2yk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkycongviec2" class="dotted ks_radio" name="radt2yk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radSudungphanmemdangkycongviec3" class="dotted ks_radio" name="radt2yk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkycongviec4" class="dotted ks_radio" name="radt2yk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkycongviec5" class="dotted ks_radio" name="radt2yk4" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>+ Sử dụng phần mềm đăng ký ghi công việc (công tác)
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkycongviecct1" class="dotted ks_radio" name="radt2yk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkycongviecct2" class="dotted ks_radio" name="radt2yk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radSudungphanmemdangkycongviecct3" class="dotted ks_radio" name="radt2yk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkycongviecct4" class="dotted ks_radio" name="radt2yk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkycongviecct5" class="dotted ks_radio" name="radt2yk5" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>+ Sử dụng phần mềm đăng ký ghi công làm việc
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyghiconglamviec1" class="dotted ks_radio" name="radt2yk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyghiconglamviec2" class="dotted ks_radio" name="radt2yk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radSudungphanmemdangkyghiconglamviec3" class="dotted ks_radio" name="radt2yk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyghiconglamviec4" class="dotted ks_radio" name="radt2yk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyghiconglamviec5" class="dotted ks_radio" name="radt2yk6" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.4
                        </td>
                        <td>- Sử dụng phần mềm đăng ký đi trễ về sớm
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyvetre1" class="dotted ks_radio" name="radt2yk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyvetre2" class="dotted ks_radio" name="radt2yk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radSudungphanmemdangkyvetre3" class="dotted ks_radio" name="radt2yk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyvetre4" class="dotted ks_radio" name="radt2yk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemdangkyvetre5" class="dotted ks_radio" name="radt2yk7" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>I.5
                        </td>
                        <td>- Sử dụng phần mềm kiểm tra chấm công
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemkiemtrachamcong1" class="dotted ks_radio" name="radt2yk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemkiemtrachamcong2" class="dotted ks_radio" name="radt2yk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radSudungphanmemkiemtrachamcong3" class="dotted ks_radio" name="radt2yk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemkiemtrachamcong4" class="dotted ks_radio" name="radt2yk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudungphanmemkiemtrachamcong5" class="dotted ks_radio" name="radt2yk8" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>II
                        </td>
                        <td><strong>Phần mềm đào tạo công ty</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radPhanmemdaotaocongty1" class="dotted ks_radio" name="radt2yk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radPhanmemdaotaocongty2" class="dotted ks_radio" name="radt2yk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radPhanmemdaotaocongty3" class="dotted ks_radio" name="radt2yk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radPhanmemdaotaocongty4" class="dotted ks_radio" name="radt2yk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radPhanmemdaotaocongty5" class="dotted ks_radio" name="radt2yk9" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>II.1
                        </td>
                        <td>- Nội dung quy định chung
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidungquydinhchung1" class="dotted ks_radio" name="radt2yk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidungquydinhchung2" class="dotted ks_radio" name="radt2yk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radNoidungquydinhchung3" class="dotted ks_radio" name="radt2yk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidungquydinhchung4" class="dotted ks_radio" name="radt2yk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidungquydinhchung5" class="dotted ks_radio" name="radt2yk10" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>II.2
                        </td>
                        <td>- Nội dung chuyên môn công việc
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidungchuyenmoncongviec1" class="dotted ks_radio" name="radt2yk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidungchuyenmoncongviec2" class="dotted ks_radio" name="radt2yk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radNoidungchuyenmoncongviec3" class="dotted ks_radio" name="radt2yk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidungchuyenmoncongviec4" class="dotted ks_radio" name="radt2yk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNoidungchuyenmoncongviec5" class="dotted ks_radio" name="radt2yk11" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>
                            <strong>GÓP Ý CỦA NHÂN VIÊN</strong>
                        </td>
                        <td colspan="5">
                    </tr>
                    <tr>
                        <td>1</td>
                        <td>- Đối với phần mềm nhân sự
                        </td>
                        <td colspan="5">
                            <textarea id="doivoipmnhansu14" runat="server" style="pointer-events: all;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>- Đối với phần mềm đào tạo 
                        </td>
                        <td colspan="5">
                            <textarea id="doivoiphanmemdaotao" runat="server" style="pointer-events: all;"></textarea>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="tab3" class="tabchung">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>STT
                        </th>
                        <th>Ý KIẾN
                        </th>
                        <th>Hoàn toàn đồng ý
                        </th>
                        <th>Đồng ý
                        </th>
                        <th>Bình thường
                        </th>
                        <th>Không đồng ý
                        </th>
                        <th>Hoàn toàn không đồng ý
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1

                        </td>
                        <td>
                            <strong>Tôi cảm thấy vô cùng hài lòng với công việc hiện tại</strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthayvocunghailong1" class="dotted ks_radio" name="radt3yk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthayvocunghailong2" class="dotted ks_radio" name="radt3yk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToicamthayvocunghailong3" class="dotted ks_radio" name="radt3yk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthayvocunghailong4" class="dotted ks_radio" name="radt3yk1" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthayvocunghailong5" class="dotted ks_radio" name="radt3yk1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>2
                        </td>
                        <td>
                            <strong>Tôi cảm thấy công việc thú vị
                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthaycongviecthuvi1" class="dotted ks_radio" name="radt3yk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthaycongviecthuvi2" class="dotted ks_radio" name="radt3yk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToicamthaycongviecthuvi3" class="dotted ks_radio" name="radt3yk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthaycongviecthuvi4" class="dotted ks_radio" name="radt3yk2" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthaycongviecthuvi5" class="dotted ks_radio" name="radt3yk2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>3
                        </td>
                        <td>
                            <strong>Tôi biết chính xác công việc tôi muốn làm

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToivietchinhxaccongviectoimuonlam1" class="dotted ks_radio" name="radt3yk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToivietchinhxaccongviectoimuonlam2" class="dotted ks_radio" name="radt3yk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToivietchinhxaccongviectoimuonlam3" class="dotted ks_radio" name="radt3yk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToivietchinhxaccongviectoimuonlam4" class="dotted ks_radio" name="radt3yk3" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToivietchinhxaccongviectoimuonlam5" class="dotted ks_radio" name="radt3yk3" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>4
                        </td>
                        <td>
                            <strong>Tôi sẵn sàng cố gắng hết sức để hoàn thành công việc

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToisansangcoganghetsuc1" class="dotted ks_radio" name="radt3yk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToisansangcoganghetsuc2" class="dotted ks_radio" name="radt3yk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToisansangcoganghetsuc3" class="dotted ks_radio" name="radt3yk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToisansangcoganghetsuc4" class="dotted ks_radio" name="radt3yk4" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToisansangcoganghetsuc5" class="dotted ks_radio" name="radt3yk4" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>5
                        </td>
                        <td>
                            <strong>Công việc của tôi không quá thách thức </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCongvieccuatoikhongquathachthuc1" class="dotted ks_radio" name="radt3yk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCongvieccuatoikhongquathachthuc2" class="dotted ks_radio" name="radt3yk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radCongvieccuatoikhongquathachthuc3" class="dotted ks_radio" name="radt3yk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCongvieccuatoikhongquathachthuc4" class="dotted ks_radio" name="radt3yk5" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCongvieccuatoikhongquathachthuc5" class="dotted ks_radio" name="radt3yk5" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>6
                        </td>
                        <td>
                            <strong>Tôi được tự do quyết định cách làm việc
                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToiduoctudoquyetdinh1" class="dotted ks_radio" name="radt3yk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToiduoctudoquyetdinh2" class="dotted ks_radio" name="radt3yk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToiduoctudoquyetdinh3" class="dotted ks_radio" name="radt3yk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToiduoctudoquyetdinh4" class="dotted ks_radio" name="radt3yk6" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToiduoctudoquyetdinh5" class="dotted ks_radio" name="radt3yk6" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>7
                        </td>
                        <td>
                            <strong>Tôi có nhiều cơ hội để học hỏi khi làm việc
                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToiconhieucohoidehochoi1" class="dotted ks_radio" name="radt3yk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToiconhieucohoidehochoi2" class="dotted ks_radio" name="radt3yk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToiconhieucohoidehochoi3" class="dotted ks_radio" name="radt3yk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToiconhieucohoidehochoi4" class="dotted ks_radio" name="radt3yk7" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToiconhieucohoidehochoi5" class="dotted ks_radio" name="radt3yk7" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>8
                        </td>
                        <td>
                            <strong>Cơ sở hạ tầng/thiết bị/công cụ của công ty thật tuyệt vời

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCosohatangthietbi1" class="dotted ks_radio" name="radt3yk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCosohatangthietbi2" class="dotted ks_radio" name="radt3yk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radCosohatangthietbi3" class="dotted ks_radio" name="radt3yk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCosohatangthietbi4" class="dotted ks_radio" name="radt3yk8" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radCosohatangthietbi5" class="dotted ks_radio" name="radt3yk8" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>9
                        </td>
                        <td>
                            <strong>Tôi không nhận được đủ sự hỗ trợ từ sếp trực tiếp của tôi
                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToikhongnhanduocdusuhotro1" class="dotted ks_radio" name="radt3yk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToikhongnhanduocdusuhotro2" class="dotted ks_radio" name="radt3yk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToikhongnhanduocdusuhotro3" class="dotted ks_radio" name="radt3yk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToikhongnhanduocdusuhotro4" class="dotted ks_radio" name="radt3yk9" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToikhongnhanduocdusuhotro5" class="dotted ks_radio" name="radt3yk9" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>10
                        </td>
                        <td>
                            <strong>Tôi thích làm việc với sếp trực tiếp của tôi

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToithichlamviecvoiseptructiep1" class="dotted ks_radio" name="radt3yk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToithichlamviecvoiseptructiep2" class="dotted ks_radio" name="radt3yk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToithichlamviecvoiseptructiep3" class="dotted ks_radio" name="radt3yk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToithichlamviecvoiseptructiep4" class="dotted ks_radio" name="radt3yk10" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToithichlamviecvoiseptructiep5" class="dotted ks_radio" name="radt3yk10" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>11
                        </td>
                        <td>
                            <strong>Sự đóng góp của tôi được ghi nhận đầy đủ

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudonggopcuatoi1" class="dotted ks_radio" name="radt3yk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudonggopcuatoi2" class="dotted ks_radio" name="radt3yk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radSudonggopcuatoi3" class="dotted ks_radio" name="radt3yk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudonggopcuatoi4" class="dotted ks_radio" name="radt3yk11" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSudonggopcuatoi5" class="dotted ks_radio" name="radt3yk11" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>12
                        </td>
                        <td>
                            <strong>Những kinh nghiệm tôi học được hiện tại cực kỳ hữu ích để phát triển sự nghiệp tương lai

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNhungkinhnghiemtoihocduoc1" class="dotted ks_radio" name="radt3yk12" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNhungkinhnghiemtoihocduoc2" class="dotted ks_radio" name="radt3yk12" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radNhungkinhnghiemtoihocduoc3" class="dotted ks_radio" name="radt3yk12" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNhungkinhnghiemtoihocduoc4" class="dotted ks_radio" name="radt3yk12" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radNhungkinhnghiemtoihocduoc5" class="dotted ks_radio" name="radt3yk12" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>13
                        </td>
                        <td>
                            <strong>Tôi thấy khó khăn để theo kịp các yêu cầu công việc

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToithaykhokhandetheokip1" class="dotted ks_radio" name="radt3yk13" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToithaykhokhandetheokip2" class="dotted ks_radio" name="radt3yk13" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToithaykhokhandetheokip3" class="dotted ks_radio" name="radt3yk13" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToithaykhokhandetheokip4" class="dotted ks_radio" name="radt3yk13" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToithaykhokhandetheokip5" class="dotted ks_radio" name="radt3yk13" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>14
                        </td>
                        <td>
                            <strong>Tôi không gặp bất kỳ khó khăn nào trong việc cân bằng công việc và cuộc sống cá nhân

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToikhonggapbatkykhokhannao1" class="dotted ks_radio" name="radt3yk14" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToikhonggapbatkykhokhannao2" class="dotted ks_radio" name="radt3yk14" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToikhonggapbatkykhokhannao3" class="dotted ks_radio" name="radt3yk14" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToikhonggapbatkykhokhannao4" class="dotted ks_radio" name="radt3yk14" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToikhonggapbatkykhokhannao5" class="dotted ks_radio" name="radt3yk14" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>15
                        </td>
                        <td>
                            <strong>Tôi hòa nhập dễ dàng với đồng nghiệp

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToihoanhapdedang1" class="dotted ks_radio" name="radt3yk15" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToihoanhapdedang2" class="dotted ks_radio" name="radt3yk15" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToihoanhapdedang3" class="dotted ks_radio" name="radt3yk15" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToihoanhapdedang4" class="dotted ks_radio" name="radt3yk15" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToihoanhapdedang5" class="dotted ks_radio" name="radt3yk15" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>16
                        </td>
                        <td>
                            <strong>Tôi nghĩ đây là nơi tuyệt vời để làm việc

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToinghidaylanoituyetvoi1" class="dotted ks_radio" name="radt3yk16" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToinghidaylanoituyetvoi2" class="dotted ks_radio" name="radt3yk16" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToinghidaylanoituyetvoi3" class="dotted ks_radio" name="radt3yk16" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToinghidaylanoituyetvoi4" class="dotted ks_radio" name="radt3yk16" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToinghidaylanoituyetvoi5" class="dotted ks_radio" name="radt3yk16" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>17
                        </td>
                        <td>
                            <strong>Tôi tin rằng mình sẽ có tương lai tươi sáng khi làm việc tại đây

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToitinrangminhsecotuonglai1" class="dotted ks_radio" name="radt3yk17" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToitinrangminhsecotuonglai2" class="dotted ks_radio" name="radt3yk17" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToitinrangminhsecotuonglai3" class="dotted ks_radio" name="radt3yk17" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToitinrangminhsecotuonglai4" class="dotted ks_radio" name="radt3yk17" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToitinrangminhsecotuonglai5" class="dotted ks_radio" name="radt3yk17" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>18
                        </td>
                        <td>
                            <strong>Tôi sẽ tiếp tục làm việc tại đây

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToisetieptuclamviectaiday1" class="dotted ks_radio" name="radt3yk18" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToisetieptuclamviectaiday2" class="dotted ks_radio" name="radt3yk18" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToisetieptuclamviectaiday3" class="dotted ks_radio" name="radt3yk18" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToisetieptuclamviectaiday4" class="dotted ks_radio" name="radt3yk18" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToisetieptuclamviectaiday5" class="dotted ks_radio" name="radt3yk18" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>19
                        </td>
                        <td>
                            <strong>Tôi cảm thấy không hài lòng với giá trị của công ty và cách thức vận hành công việc kinh doanh

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthaykhonghailongvoigiatricuacongty1" class="dotted ks_radio" name="radt3yk19" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthaykhonghailongvoigiatricuacongty2" class="dotted ks_radio" name="radt3yk19" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radToicamthaykhonghailongvoigiatricuacongty3" class="dotted ks_radio" name="radt3yk19" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthaykhonghailongvoigiatricuacongty4" class="dotted ks_radio" name="radt3yk19" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radToicamthaykhonghailongvoigiatricuacongty5" class="dotted ks_radio" name="radt3yk19" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>20
                        </td>
                        <td>
                            <strong>Sản phẩm/Dịch vụ mà công ty cung cấp cực kỳ tuyệt vời

                            </strong>
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSanphamdichvu1" class="dotted ks_radio" name="radt3yk20" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSanphamdichvu2" class="dotted ks_radio" name="radt3yk20" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input  type="radio" id="radSanphamdichvu3" class="dotted ks_radio" name="radt3yk20" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSanphamdichvu4" class="dotted ks_radio" name="radt3yk20" runat="server" />
                        </td>
                        <td class="tdposition">
                            <input type="radio" id="radSanphamdichvu5" class="dotted ks_radio" name="radt3yk20" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="text-right">
            <asp:Button ID="btnSaveBanner" runat="server" Text="Lưu thông tin" CssClass="btn-1 btn_luuthongtin" OnClick="btnSaveBanner_Click"
                OnClientClick="return CheckValidBanner();" />
        </div>
    </div>
    <script> 
       
        $(".tabchung").hide(); 
       
        $(document).ready(function () {
            if ($("#ctl00_MainContent_atab2").is(":visible")) {
                $(".achung").removeClass("khaosathoinhap_tab_active");
                $("#atab" + 2).addClass("khaosathoinhap_tab_active");
                //
                $(".tabchung").hide();
                $("#tab2").show();
            }
            if ($("#ctl00_MainContent_atab3").is(":visible")) {
                $(".achung").removeClass("khaosathoinhap_tab_active");
                $("#atab" + 3).addClass("khaosathoinhap_tab_active");
                //
                $(".tabchung").hide();
                $("#tab3").show(); 
            }
            $("#tab1").show();
            if ($("#ctl00_MainContent_type").val() == 1)
            {
                tabclick(1);
            }
            if ($("#ctl00_MainContent_type").val() == 2)
            {
                tabclick(2);
            }
            if ($("#ctl00_MainContent_type").val() == 3) {
                tabclick(3);
            }
            //
            if ($("#ctl00_MainContent_type").val() == 1) {
                if ($("#ctl00_MainContent_step").val() == 1) {
                    $("#ctl00_MainContent_atab2").hide();
                    $("#ctl00_MainContent_atab3").hide();
                    tabclick(1);
                    $("#ctl00_MainContent_atab" + 1).addClass("khaosathoinhap_tab_active");
                }
                if ($("#ctl00_MainContent_step").val() == 2) { 
                    $("#ctl00_MainContent_atab3").hide();
                    tabclick(2);
                    $("#ctl00_MainContent_atab" + 2).addClass("khaosathoinhap_tab_active");
                }
                if ($("#ctl00_MainContent_step").val() == 3) { 
                    tabclick(3);
                    $("#ctl00_MainContent_atab" + 3).addClass("khaosathoinhap_tab_active");
                }
            }
        });
        function tabclick(id) {
            $(".achung").removeClass("khaosathoinhap_tab_active");
            $("#ctl00_MainContent_atab" + id).addClass("khaosathoinhap_tab_active");
            //atab1
            $(".tabchung").hide();
            $("#tab" + id).show();
        }
    </script>
</asp:Content>
