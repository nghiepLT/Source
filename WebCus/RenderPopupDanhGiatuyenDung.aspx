<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RenderPopupDanhGiatuyenDung.aspx.cs" Inherits="WebCus.RenderPopupDanhGiatuyenDung" %>

<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="AdminCss/bootstrap.css" rel="stylesheet" />
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
    <input id="type" runat="server" style="display:none;" />
    <input id="iduser" runat="server" style="display:none;" />
    <div class="mycontainer">
        <div class="row">

            <div class="col-sm-12">
                <div class="full_box">
                    <div class="headertitle_box">
                        <div class="headertitle_box_left">
                            <img src="Images/logo_form.png" class="img-responsive" />
                        </div>
                        <div class="headertitle_box_right">
                            <div class="headertitle_box_right_title">
                                PHIẾU ĐÁNH GIÁ THỬ VIỆC
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div>
                        <div class="table_1">
                            <table class="table">
                                <tr>
                                    <td style="width: 50%">Họ tên nhân viên: <span id="hotennhanvien" runat="server">Nguyễn Công Thành</span>
                                    </td>
                                    <td>Thời gian thử việc: Từ <span id="thoigiantu" runat="server">14/09/2022</span> đến <span id="thoigianden" runat="server">14/11/2022</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mã số NV: <span id="masonhanvien" runat="server">ENGN0922100 Giới tính: 0 Nam;  0 Nữ</span>
                                    </td>
                                    <td>Địa điểm làm việc: <span id="diadiemlamviec" runat="server">245B Trần Quang Khải, Q1</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vị trí làm việc: <span id="vitrilamviec" runat="server">NV Lái xe</span>
                                    </td>
                                    <td>Người đánh giá: <span id="nguoidanhgia" runat="server">Mr. Minh</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Bộ phận/ Phòng: <span id="bophanphong" runat="server">Giao nhận</span>
                                    </td>
                                    <td>Chức vụ: <span id="chucvu" runat="server">TBP. Giao nhận</span>
                                    </td>
                                </tr>
                            </table>
                            <table class="table" id="tb1">
                                <tr>
                                    <td colspan="7">Đánh giá quá trình làm việc trong thời gian thử việc: Tốt 80-100%, Khá 70-<80%, TB 50-<70%, Yếu<50%
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nội dung đánh giá
                                    </td>
                                    <td>% H.thành
                                    </td>
                                    <td>Công việc được giao 
                                    </td>
                                    <td>Tốt
                                    </td>
                                    <td>Khá
                                    </td>
                                    <td>TB
                                    </td>
                                    <td>Yếu
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tinh thần trách nhiệm với công việc
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="TinhThanTrachNhiemPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="TinhThanTrachNhiemCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radTinhthan" type="radio" class="ip" id="TinhThanTrachNhiemDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radTinhthan" type="radio" class="ip" id="TinhThanTrachNhiemDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radTinhthan" type="radio" class="ip" id="TinhThanTrachNhiemDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radTinhthan" type="radio" class="ip" id="TinhThanTrachNhiemDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mức độ hoàn thành công việc
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="MucDoHoanthanhPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="MucDoHoanthanhCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radMucDo" type="radio" class="ip" id="MucDoHoanthanhCongViec1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radMucDo" type="radio" class="ip" id="MucDoHoanthanhCongViec2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radMucDo" type="radio" class="ip" id="MucDoHoanthanhCongViec3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radMucDo" type="radio" class="ip" id="MucDoHoanthanhCongViec4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Thời gian hoàn thành công việc
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="ThoiGianHoanThanhPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="ThoiGianHoanThanhCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radThoiGian" type="radio" class="ip" id="ThoiGianHoanThanhDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radThoiGian" type="radio" class="ip" id="ThoiGianHoanThanhDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radThoiGian" type="radio" class="ip" id="ThoiGianHoanThanhDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radThoiGian" type="radio" class="ip" id="ThoiGianHoanThanhDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Sự hiểu biết về công việc 
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="SuHieuBietPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="SuHieuBietCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radSuHieuBiet" type="radio" class="ip" id="SuHieuBietDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radSuHieuBiet" type="radio" class="ip" id="SuHieuBietDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radSuHieuBiet" type="radio" class="ip" id="SuHieuBietDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radSuHieuBiet" type="radio" class="ip" id="SuHieuBietDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kỹ năng chuyên môn
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="KyNangChuyenMonPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="KyNangChuyenMonCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKyNang" type="radio" class="ip" id="KyNangChuyenMonDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKyNang" type="radio" class="ip" id="KyNangChuyenMonDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKyNang" type="radio" class="ip" id="KyNangChuyenMonDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKyNang" type="radio" class="ip" id="KyNangChuyenMonDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Sự chủ động trong công việc
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="SuChuDongPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="SuChuDongCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radsuChuDong" type="radio" class="ip" id="SuChuDongDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radsuChuDong" type="radio" class="ip" id="SuChuDongDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radsuChuDong" type="radio" class="ip" id="SuChuDongDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radsuChuDong" type="radio" class="ip" id="SuChuDongDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Khả năng làm việc độc lập 
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="KhaNangLamViecPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="KhaNangLamViecCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKhaNangLam" type="radio" class="ip" id="KhaNangLamViecDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKhaNangLam" type="radio" class="ip" id="KhaNangLamViecDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKhaNangLam" type="radio" class="ip" id="KhaNangLamViecDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKhaNangLam" type="radio" class="ip" id="KhaNangLamViecDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tinh thần hỗ trợ đồng nghiệp
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="TinhThanhoTroPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="TinhThanhoTroCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radTinhThanHoTro" type="radio" class="ip" id="TinhThanhoTroDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radTinhThanHoTro" type="radio" class="ip" id="TinhThanhoTroDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radTinhThanHoTro" type="radio" class="ip" id="TinhThanhoTroDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radTinhThanHoTro" type="radio" class="ip" id="TinhThanhoTroDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Học hỏi, cầu tiến
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="HocHoiPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="HocHoiTroCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radHocHoi" type="radio" class="ip" id="HocHoiTroDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radHocHoi" type="radio" class="ip" id="HocHoiTroDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radHocHoi" type="radio" class="ip" id="HocHoiTroDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radHocHoi" type="radio" class="ip" id="HocHoiTroDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Giao tiếp với khách hàng, đối tác
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="GiaoTiepVoiKHPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="GiaoTiepVoiKHCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radGiaoTiepVoiKH" type="radio" class="ip" id="GiaoTiepVoiKHDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radGiaoTiepVoiKH" type="radio" class="ip" id="GiaoTiepVoiKHDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radGiaoTiepVoiKH" type="radio" class="ip" id="GiaoTiepVoiKHDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radGiaoTiepVoiKH" type="radio" class="ip" id="GiaoTiepVoiKHDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mối quan hệ đồng nghiệp
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="MoiQuanHeDNPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="MoiQuanHeDNCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radMoiQuanHe" type="radio" class="ip" id="MoiQuanHeDNDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radMoiQuanHe" type="radio" class="ip" id="MoiQuanHeDNDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radMoiQuanHe" type="radio" class="ip" id="MoiQuanHeDNDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radMoiQuanHe" type="radio" class="ip" id="MoiQuanHeDNDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Xử lý tình huống, giải quyết vấn đề
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="XuLyTinhHuongPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="XuLyTinhHuongCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radXulytinhhuong" type="radio" class="ip" id="XuLyTinhHuongDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radXulytinhhuong" type="radio" class="ip" id="XuLyTinhHuongDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radXulytinhhuong" type="radio" class="ip" id="XuLyTinhHuongDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radXulytinhhuong" type="radio" class="ip" id="XuLyTinhHuongDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Khả năng sáng tạo
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="KhaNangSangTaoPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="KhaNangSangTaoCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKhanangsangtao" type="radio" class="ip" id="KhaNangSangTaoDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKhanangsangtao" type="radio" class="ip" id="KhaNangSangTaoDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKhanangsangtao" type="radio" class="ip" id="KhaNangSangTaoDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radKhanangsangtao" type="radio" class="ip" id="KhaNangSangTaoDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Chấp hành mệnh lệnh cấp quản lý
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="ChapHanhMenhLenhPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="ChapHanhMenhLenhCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radChapHanhMenhLenh" type="radio" class="ip" id="ChapHanhMenhLenhDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radChapHanhMenhLenh" type="radio" class="ip" id="ChapHanhMenhLenhDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radChapHanhMenhLenh" type="radio" class="ip" id="ChapHanhMenhLenhDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radChapHanhMenhLenh" type="radio" class="ip" id="ChapHanhMenhLenhDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Đạo đức
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="DaoDucPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="DaoDucCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDaoDuc" type="radio" class="ip" id="DaoDucDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDaoDuc" type="radio" class="ip" id="DaoDucDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDaoDuc" type="radio" class="ip" id="DaoDucDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDaoDuc" type="radio" class="ip" id="DaoDucDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Hiểu rõ và tuân thủ nội qui, quy định của công ty
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="HieuRoPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="HieuRoCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radHieuRo" type="radio" class="ip" id="HieuRoDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radHieuRo" type="radio" class="ip" id="HieuRoDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radHieuRo" type="radio" class="ip" id="HieuRoDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radHieuRo" type="radio" class="ip" id="HieuRoDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Đảm bảo ngày công (P. HCNS)
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="DamBaoNgayCongPercent" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="DamBaoNgayCongCongViec" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDambaongaycong" type="radio" class="ip" id="DamBaoNgayCongDanhGia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDambaongaycong" type="radio" class="ip" id="DamBaoNgayCongDanhGia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDambaongaycong" type="radio" class="ip" id="DamBaoNgayCongDanhGia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDambaongaycong" type="radio" class="ip" id="DamBaoNgayCongDanhGia4" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td>Đánh giá chung
                                    </td>
                                    <td>
                                        <input name="radDanhgiachung" type="radio" class="ip" id="DanhgiachungDanhgia1" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDanhgiachung" type="radio" class="ip" id="DanhgiachungDanhgia2" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDanhgiachung" type="radio" class="ip" id="DanhgiachungDanhgia3" runat="server" />
                                    </td>
                                    <td>
                                        <input name="radDanhgiachung" type="radio" class="ip" id="DanhgiachungDanhgia4" runat="server" />
                                    </td>
                                </tr>
                            </table>

                            <table class="table" id="tb2" >
                                <tr>
                                    <td style="width: 50%">Nhận xét của người đánh giá:
                                    </td>
                                    <td>Đề xuất của người đánh giá:
                                    </td>
                                </tr>
                                <tr>
                                    <td> <input class="ip dotted w100" id="Nhanxetnguoidanhgia" runat="server" /></td>
                                    <td> <input name="radDiLamDungGio" type="radio" class="ip" id="radKhongtiepnhan" runat="server" /> Không tiếp nhận;
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><input onchange="taithuviec()" name="radDiLamDungGio" type="radio" class="ip" id="radTaithuviec" runat="server" /> Tái thử việc - Từ <input class="ip dotted w30" id="TaithuviecTu" runat="server" /> đến <input class="ip dotted w30" id="TaithuviecDen" runat="server" />    
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><input onchange="tiepnhanct()" name="radDiLamDungGio" type="radio" class="ip" id="radTiepnhanchinhthuc" runat="server" /> Tiếp nhận chính thức - Từ <input class="ip dotted w30" id="TiepnhanchinhthucTu" runat="server" /> đến <input class="ip dotted w30" id="TiepnhanchinhthucDen" runat="server" />    
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ký tên  <input class="ip dotted w50" id="Text1" runat="server" />
                                    </td>
                                    <td>Khác:<input class="ip dotted w50" id="Text2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ngày đánh giá:<input class="ip dotted w50" id="Text3" runat="server" />                    
                                    </td>
                                    <td></td>
                                </tr>
                                
                               
                            </table>
                            <table class="table" id="tb3">
                                <tr>
                                    <td><strong>Trưởng Phòng Chuyên Môn</strong>
                                    </td>
                                    <td>Nhận xét:
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ký tên: 
                                        <input class="ip dotted w80" id="truongphongkyten" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w100" id="truongphongnhanxet" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>Họ tên:
                                        <input class="ip dotted w80" id="TruongPhongHoTen" runat="server" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Ngày:
                                        <input class="ip dotted w80" id="Truongphongngay" runat="server" />
                                    </td>
                                    <td></td>
                                </tr>
                                
                            </table>
                            <table class="table" id="tb4">
                                <tr>
                                    <td><strong>Phòng Hành Chính Nhân Sự:</strong>
                                    </td>
                                    <td>Nhận xét: 
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ký tên:<input class="ip dotted w80" id="Hanhchanhkyten" runat="server" />
                                    </td>
                                    <td>
                                        <input class="ip dotted w80" id="Hanhchanhnhanxet" runat="server" /></td>
                                </tr>

                                <tr>
                                    <td>Ngày:
                                        <input class="ip dotted w80" id="Hanhchanhngay" runat="server" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            <table class="table" id="tb5">
                                 <tr>
                                    <td><strong>Ban Giám Đốc:</strong></td>
                                    <td>
                                        <input name="radBangiamdoc" type="radio" class="ip" id="Bangiamdocdongytiepnhan" runat="server" />Đồng ý tiếp nhận  - Từ <input class="ip dotted w30" id="BangiamdocdongytiepnhanTu" runat="server" /> đến <input class="ip dotted w30" id="BangiamdocdongytiepnhanDen" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nhận xét:
                                    </td>
                                    <td>
                                        <input name="radBangiamdoc" type="radio" class="ip" id="Bangiamdocykienkhac" runat="server" />
                                        Ý kiến khác
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input class="ip dotted w100" id="Bangiamdocnhanxet" runat="server" />
                                    </td>
                                    <td>Lương chính thức: 
                                        <input class="ip dotted w50" id="Bangiamdocluong" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ký tên:    
                                        <input class="ip dotted w80" id="BangiamdocKyten" runat="server" />
                                    </td>
                                    <td>Khác:
                                        <input class="ip dotted w80" id="Bangiamdockhac" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Họ tên: 
                                        <input class="ip dotted w80" id="Bangiamdochoten" runat="server" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Ngày: 
                                        <input class="ip dotted w80" id="Bangiamdocngay" runat="server" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                        <div class="text-right btn_luuthongtinbox">
                            <asp:button id="btnSaveBanner" runat="server" text="Lưu thông tin" cssclass="btn-1 btn_luuthongtin btn btn-sm btn-danger" onclick="btnSaveBanner_Click"
                                onclientclick="return CheckValidBanner();" />
                            <%--<asp:button id="btnDuyet" runat="server" text="Duyệt" cssclass="btn-1 btn_luuthongtin btn btn-sm btn-primary" onclick="btnDuyet_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <style>
      input[type="radio"]{
          cursor:pointer;
      }
        td{
            cursor:pointer;
        }
        body {
            background-image: none !important;
        }

        .spanleft {
        }

        .w100 {
            width: 100% !important;
        }

        .w90 {
            width: 90% !important;
        }

        .w80 {
            width: 80% !important;
        }

        .w70 {
            width: 70% !important;
        }

        .w60 {
            width: 60% !important;
        }

        .mycontainer {
            width: 95%;
            margin: 0px auto;
        }

        .dotted {
            border-bottom: 1px dotted !important;
            width: 59%;
            position: relative;
            top: -2px;
        }
    </style>
    <script src="js/jquery-1.8.2.js"></script>
    <script>
        function taithuviec() {
            $("#ctl00_ContentPlaceHolder1_TiepnhanchinhthucTu").val('');
            $("#ctl00_ContentPlaceHolder1_TiepnhanchinhthucDen").val('');
        }
        function tiepnhanct() {
            $("#ctl00_ContentPlaceHolder1_TaithuviecTu").val('');
            $("#ctl00_ContentPlaceHolder1_TaithuviecDen").val('');
        }
        function CheckValidBanner() {
            
            if ($("#ctl00_ContentPlaceHolder1_radTaithuviec").prop("checked") == true) {
                if ($("#ctl00_ContentPlaceHolder1_TaithuviecTu").val() == '') {
                    {
                        alert("Vui lòng nhập thông tin");
                        $("#ctl00_ContentPlaceHolder1_TaithuviecTu").focus();
                        return false;
                    }

                }
                if ($("#ctl00_ContentPlaceHolder1_TaithuviecDen").val() == '') {
                    {
                        alert("Vui lòng nhập thông tin");
                        $("#ctl00_ContentPlaceHolder1_TaithuviecDen").focus();
                        return false;
                    }

                }
            }
            if ($("#ctl00_ContentPlaceHolder1_radTiepnhanchinhthuc").prop("checked") == true) {
                if ($("#ctl00_ContentPlaceHolder1_TiepnhanchinhthucTu").val() == '') {
                    {
                        alert("Vui lòng nhập thông tin");
                        $("#ctl00_ContentPlaceHolder1_TiepnhanchinhthucTu").focus();
                        return false;
                    }

                }
                if ($("#ctl00_ContentPlaceHolder1_TiepnhanchinhthucDen").val() == '') {
                    {
                        alert("Vui lòng nhập thông tin");
                        $("#ctl00_ContentPlaceHolder1_TiepnhanchinhthucDen").focus();
                        return false;
                    }

                }
            }


            //Nếu chưa chọn đánh giá
            if ($("#ctl00_ContentPlaceHolder1_radKhongtiepnhan").prop("checked") == false && $("#ctl00_ContentPlaceHolder1_radTaithuviec").prop("checked") == false && $("#ctl00_ContentPlaceHolder1_radTiepnhanchinhthuc").prop("checked") == false) {
                alert("Vui lòng chọn đề xuất của người đánh giá");
                return false;
            }
        }
        $("td").click(function() {
            var f = $(this).find("input");
            f.prop("checked", true);
        });

        $(document).ready(function () {
            if ($("#ctl00_ContentPlaceHolder1_type").val() == 1) {
                type1();
            }
            if ($("#ctl00_ContentPlaceHolder1_type").val() == 2) {
                type2();
            }
            if ($("#ctl00_ContentPlaceHolder1_type").val() == 3) {
                type3();
            }
            if ($("#ctl00_ContentPlaceHolder1_type").val() == 4) {
                type4();
            }
        });
        function type1() {
            $("#tb3").addClass("tabledisable");
            $("#tb4").addClass("tabledisable");
            $("#tb5").addClass("tabledisable");
        }
        function type2() {
            $("#tb1").addClass("tabledisable");
            $("#tb2").addClass("tabledisable");
            $("#tb4").addClass("tabledisable");
            $("#tb5").addClass("tabledisable");
        }
        function type3() {
            $("#tb1").addClass("tabledisable");
            $("#tb3").addClass("tabledisable");
            $("#tb2").addClass("tabledisable");
            $("#tb5").addClass("tabledisable");
        }
        function type4() {
            $("#tb1").addClass("tabledisable");
            $("#tb3").addClass("tabledisable");
            $("#tb2").addClass("tabledisable");
            $("#tb4").addClass("tabledisable");
        }
    </script>
    <style>
        .tabledisable{
            background-color:#dddddd;
            pointer-events:none;
        }
    </style>
</asp:Content>
