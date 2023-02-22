<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="RenderPopupDanhGia.aspx.cs" Inherits="WebCus.RenderPopupDanhGia" %>

<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="userid" runat="server" style="display:none;" />
    <input id="type" runat="server" style="display:none" />
    <link href="AdminCss/bootstrap.css" rel="stylesheet" />
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
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
                                PHIẾU ĐÁNH GIÁ PHỎNG VẤN
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div>
                        <div class="table_1" id="tablenpv">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <td colspan="5">
                                            <span class="spanleft">Họ tên ứng viên:</span>
                                            <input class="ip dotted w70" id="HoTenUngVien" runat="server" required />
                                        </td>
                                        <td>
                                            <span class="spanleft"></span>Mức lương hiện tại:   
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="Mucluonghientai" required runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">Vị trí dự tuyển:
                                            <input class="ip dotted" id="vitriungtuyen" runat="server"  required/>
                                        </td>
                                        <td colspan="2">Phòng/BP:
                                            <input class="ip dotted" id="phongban" runat="server" />
                                        </td>
                                        <td>Mức lương đề nghị:
                                        </td>
                                        <td>
                                            <input class="ip dotted w100 currency" id="Mucluongdenghi" required runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="3"><strong>Các đợt PV</strong></td>
                                        <td colspan="2"><strong>Phỏng vấn lần 1	</strong>
                                        </td>
                                        <td colspan="2"><strong>Phỏng vấn lần 2	</strong>
                                        </td>
                                        <td colspan="2"><strong>Phỏng vấn lần 3</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Ngày
                                            <input class="ip dotted w70 js-date" id="NgayPV" runat="server" required />
                                        </td>
                                        <td colspan="2">Ngày
                                            <input class="ip dotted w70" id="Text6" runat="server" />
                                        </td>
                                        <td colspan="2">Ngày
                                            <input class="ip dotted w70" id="Text7" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Người PV:
                                            <input class="ip dotted w100" id="NguoiPV" runat="server" required />
                                        </td>
                                        <td colspan="2">Người PV:
                                            <input class="ip dotted w100" id="Text9" runat="server" />
                                        </td>
                                        <td colspan="2">Người PV:
                                            <input class="ip dotted w100" id="Text10" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Điểm
                                        </td>
                                        <td colspan="2"><5 :  Kém     
                                        </td>
                                        <td colspan="2">5 đến 6 : Trung bình  
                                        </td>
                                        <td>7 đến 8 : Khá
                                        </td>
                                        <td>9 đến 10 : Giỏi
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Nội dung</strong>
                                        </td>
                                        <td><strong>Điểm</strong>
                                        </td>
                                        <td><strong>Nhận xét</strong>
                                        </td>
                                        <td><strong>Điểm</strong>
                                        </td>
                                        <td><strong>Nhận xét</strong>
                                        </td>
                                        <td><strong>Điểm</strong>
                                        </td>
                                        <td><strong>Nhận xét</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <div>
                                                Kiến thức:
                                            </div>
                                            <div>
                                                -Chuyên ngành
                                            </div>
                                            <div>
                                                -Kỹ năng mềm
                                            </div>
                                            <div>
                                                -Khác
                                            </div>

                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemkienthuc1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetkienthuc1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemkienthuc2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetkienthuc2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemkienthuc3" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetkienthuc3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                Chuyên môn:
                                            </div>
                                            <div>
                                                -ĐH, CĐ, TC
                                            </div>
                                            <div>
                                                -Khác
                                            </div>
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemchuyenmon1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetchuyenmon1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemchuyenmon2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetchuyenmon2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemchuyenmon3" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetchuyenmon3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                Kinh nghiệm:
                                            </div>
                                            <div>
                                                -T/g làm việc
                                            </div>
                                            <div>
                                                -Vị trí ứng tuyển
                                            </div>
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemkinhnghiem1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetkinhnghiem1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemkinhnghiem2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetkinhnghiem2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemkinhnghiem3" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetkinhnghiem3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                Thành tích:
                                            </div>
                                            <div>
                                                -Chuyên môn
                                            </div>
                                            <div>
                                                -Khác
                                            </div>
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemthanhtich1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetthanhtich1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemthanhtich2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetthanhtich2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemthanhtich3" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetthanhtich3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                Định hướng:
                                            </div>
                                            <div>
                                                -Công việc
                                            </div>
                                            <div>
                                                -Thăng tiến
                                            </div>
                                            <div>
                                                -Khác
                                            </div>
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemdinhhuong1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetdinhhuong1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemdinhhuong2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetdinhhuong2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemdinhhuong3" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxetdinhhuong3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                Tiêu chí ưu tiên
                                            </div>
                                            <div>
                                                -Công việc
                                            </div>
                                            <div>
                                                -Môi trường
                                            </div>
                                            <div>
                                                -Nơi Làm Việc
                                            </div>
                                            <div>
                                                -Thu Nhập
                                            </div>
                                            <div>
                                                -Chính Sách 
                                            </div>
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemtieuchi1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxettieuchi1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemtieuchi2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxettieuchi2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemtieuchi3" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxettieuchi3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                Tính cách
                                            </div>
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemtinhcach1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxettinhcach1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemtinhcach2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxettinhcach2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemtinhcach3" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="nhanxettinhcach3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                Khả năng hội nhập
                                            </div>
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemkhanang1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="noidungkhanang1" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemkhanang2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="noidungkhanang2" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="diemkhanang3" runat="server" />
                                        </td>
                                        <td>
                                            <input class="ip dotted w100" id="noidungkhanang3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2">
                                            <div>
                                                Tổng điểm
                                            </div>
                                        </td>
                                        <td rowspan="2">
                                            <input class="ip dotted w100" id="tongdiem1" runat="server" />
                                        </td>
                                        <td>
                                            <span>Đạt:</span>
                                            <input onchange="kqdat1()" name="kqdat1" type="radio" class="ip" id="radDat1" runat="server" />
                                        </td>
                                      <td rowspan="2">
                                            <input class="ip dotted w100" id="tongdiem2" runat="server" />
                                        </td>
                                        <td>
                                            <span>Đạt:</span>
                                            <input onchange="kqdat2()" name="kqdat2" type="radio" class="ip" id="radDat2" runat="server" />
                                        </td>
                                           <td rowspan="2">
                                            <input class="ip dotted w100" id="tongdiem3" runat="server" />
                                        </td>
                                        <td>
                                            <span>Đạt:</span>
                                            <input name="kqdat3" type="radio" class="ip" id="radDat" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span>Không:</span>
                                            <input onchange="kqdat1()" name="kqdat1" type="radio" class="ip" id="radKhongDat1" runat="server" />
                                        </td>
                                      
                                        <td>
                                            <span>Không:</span>
                                            <input onchange="kqdat2()" name="kqdat2" type="radio" class="ip" id="radKhongDat2" runat="server" />

                                        </td> 
                                        <td>
                                            <span>Không:</span>
                                            <input name="kqdat3" type="radio" class="ip" id="radKhongDat3" runat="server" />

                                        </td>
                                    </tr>
                                    <tr id="qd1">
                                        <td rowspan="4">Quyết định của người PV</td>
                                        <td colspan="2">
                                            <span>Không phù hợp:</span>
                                            <input name="qd1" type="radio" class="ip" id="radKhongPhuhop" runat="server" />
                                        </td>
                                        <td colspan="2">
                                            <span>Chuyên môn chưa đạt:</span>
                                            <input name="qd2" type="radio" class="ip" id="radChuyenMonChuaDat" runat="server" />
                                        </td>
                                        <td colspan="2">
                                            <span>Ứng viên suy nghĩ thêm:</span>
                                            <input name="qd3" type="radio" class="ip" id="radUngVienSuyNghiThem" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <span>So sánh ứng viên khác:</span>
                                            <input name="qd1" type="radio" class="ip" id="radSoSanhUngVienKhac" runat="server" />
                                        </td>
                                        <td colspan="2">
                                            <span>So sánh ứng viên khác:</span>
                                            <input name="qd2" type="radio" class="ip  " id="radSoSanhUngVienKhac2" runat="server" />
                                        </td>
                                        <td colspan="2">
                                            <span>Xem xét thêm:</span>
                                            <input name="qd3" type="radio" class="ip  " id="radXemxetthem" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <span>PV lần 2</span>
                                            <input name="qd1" type="radio" class="ip  " id="radPVlan2" runat="server" />
                                        </td>
                                        <td colspan="2">
                                            <span>PV lần 3</span>
                                            <input name="qd2" type="radio" class="ip  " id="radPVlan3" runat="server" />
                                        </td>
                                        <td colspan="2">
                                            <span>PV lại</span>
                                            <input name="qd3" type="radio" class="ip  " id="radPVLai" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <span>Khác </span>
                                            <input name="qd1" type="radio" class="ip  " id="radKhac1" runat="server" />
                                        </td>
                                        <td colspan="2">
                                            <span>Khác </span>
                                            <input name="qd2" type="radio" class="ip  " id="radKhac2" runat="server" />
                                        </td>
                                        <td colspan="2">
                                            <span>Đạt</span>
                                            <input name="qd3" type="radio" class="ip  " id="radDat3" runat="server" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="table_1" id="tabduyet">
                            <table class="table">
                                <tr>
                                    <td colspan="2"><strong>DUYỆT TUYỂN DỤNG:</strong></td>
                                </tr>
                                <tr>
                                    <td>Vị trí công việc : 
                                        <input class="ip dotted w70" id="vitricongviec" runat="server" />
                                    </td>
                                    <td>Báo cáo cho: 
                                        <input class="ip dotted w70" id="baocaocho" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Bộ phận  : 
                                        <input class="ip dotted w70" id="bophan" runat="server" />
                                    </td>
                                    <td>Ngày nhận việc: 
                                        <input class="ip dotted w70 " id="ngaynhanviec" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Lương thử việc:
                                        <input class="ip dotted w70 currency" id="luongthuviec" runat="server" />
                                    </td>
                                    <td>Thời gian thử việc: 
                                        <input class="ip dotted w70" id="thoigianthuviec" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Lương chính :
                                        <input class="ip dotted w70 currency" id="luongchinh" runat="server" />
                                    </td>
                                    <td>Phụ cấp: 
                                        <input class="ip dotted w70" id="phucap" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">Thỏa thuận khác :
                                        <input class="ip dotted w80" id="thoathuankhac" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ký duyệt:
                                        <input class="ip dotted w70" id="kyduyet" runat="server" />
                                    </td>
                                    <td>Ngày duyệt: 
                                        <input class="ip dotted w70" id="ngayduyet" runat="server" />
                                    </td>
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
    <input id="dottuyendung" runat="server" style="display:none;"  />
    <style>
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
        .backdisable{
            background-color:#eee!important;
            pointer-events:none;
        }
    </style>
    <script src="js/jquery-1.11.1.js"></script>
      <script src="js/accounting.js"></script>
   
    <script>
        var input = document.querySelectorAll('.js-date');

        var dateInputMask = function dateInputMask(elm) {
            elm.addEventListener('keyup', function (e) {
                if (e.keyCode < 47 || e.keyCode > 57) {
                    e.preventDefault();
                }

                var len = elm.value.length;

                // If we're at a particular place, let the user type the slash
                // i.e., 12/12/1212
                if (len !== 1 || len !== 3) {
                    if (e.keyCode == 47) {
                        e.preventDefault();
                    }
                }

                // If they don't add the slash, do it for them...
                if (len === 2) {
                    elm.value += '/';
                }

                // If they don't add the slash, do it for them...
                if (len === 5) {
                    elm.value += '/';
                }
            });
        };
        input.forEach(myFunction);
        function myFunction(item, index) { 
            dateInputMask(item);
        }
    </script>


    <script>
        function checkDateValidate(id) {

            if ($("#" + id).val() != '') {
                var d = new Date(($("#" + id).val()));
                if (!dateIsValid($("#" + id).val())) {
                    alert("Vui lòng nhập đúng định dạng ngày!");
                    $("#" + id).focus();
                    return false;
                }
            }
        }
        function dateIsValid(dateStr) {
            const regex = /^\d{2}\/\d{2}\/\d{4}$/;

            if (dateStr.match(regex) === null) {
                return false;
            }

            const [day, month, year] = dateStr.split('/');

            // 👇️ format Date string as `yyyy-mm-dd`
            const isoFormattedStr = `${year}-${month}-${day}`;

            const date = new Date(isoFormattedStr);

            const timestamp = date.getTime();

            if (typeof timestamp !== 'number' || Number.isNaN(timestamp)) {
                return false;
            }

            return date.toISOString().startsWith(isoFormattedStr);
        }
        function CheckValidBanner() {
            var dd = checkDateValidate("ctl00_ContentPlaceHolder1_NgayPV");
            if (dd == false)
                return false; 

            var dd = checkDateValidate("ctl00_ContentPlaceHolder1_Text6");
            if (dd == false)
                return false; 
            //ctl00_ContentPlaceHolder1_radDat1
            if(!$("#ctl00_ContentPlaceHolder1_radDat1").prop("checked") && !$("#ctl00_ContentPlaceHolder1_radKhongDat1").prop("checked")){
               alert("Vui lòng đánh giá kết quả")
                return false; 
            }

            //check validate pv lần 2 
            if($("#ctl00_ContentPlaceHolder1_dottuyendung").val()==1){
                if($("#ctl00_ContentPlaceHolder1_NgayPV").val()==''){
                    alert("Vui lòng nhập thông tin");
                    $("#ctl00_ContentPlaceHolder1_NgayPV").focus();
                    return false;
                }
                
            }
            if($("#ctl00_ContentPlaceHolder1_dottuyendung").val()==2){
                if($("#ctl00_ContentPlaceHolder1_Text6").val()==''){
                    alert("Vui lòng nhập thông tin");
                    $("#ctl00_ContentPlaceHolder1_Text6").focus();
                    return false;
                }
                
            }
        }

        $(document).ready(function(){
            kqdat1();
            kqdat2();
            //disable background
            if($("#ctl00_ContentPlaceHolder1_dottuyendung").val()=="1"){
                disableLan2();
                disableLan3();
            }
            if($("#ctl00_ContentPlaceHolder1_dottuyendung").val()=="2"){
                disableLan1();
                disableLan3();
            }
            if($("#ctl00_ContentPlaceHolder1_dottuyendung").val()=="3"){
                disableLan2();
                disableLan1();
            }
        });
        function disableLan1(){
            return;
            $("#ctl00_ContentPlaceHolder1_NgayPV").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_NguoiPV").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemkienthuc1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemchuyenmon1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemkinhnghiem1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemthanhtich1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemdinhhuong1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemtieuchi1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemtinhcach1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemkhanang1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_tongdiem1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radKhongPhuhop").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radPVlan2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radKhac1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetkienthuc1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetchuyenmon1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetkinhnghiem1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetthanhtich1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetdinhhuong1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxettieuchi1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxettinhcach1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_noidungkhanang1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radDat1").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radKhongDat1").parent().addClass("backdisable");
        }
        function disableLan2(){
            return;
            $("#ctl00_ContentPlaceHolder1_Text6").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_Text9").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemkienthuc2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemchuyenmon2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemkinhnghiem2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemthanhtich2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemdinhhuong2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemtieuchi2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemtinhcach2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemkhanang2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radDat2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radKhongDat2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetkienthuc2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetchuyenmon2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetkinhnghiem2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetthanhtich2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetdinhhuong2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxettieuchi2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxettinhcach2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_noidungkhanang2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_tongdiem2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_tongdiem2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radChuyenMonChuaDat").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac2").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radPVlan3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radKhac2").parent().addClass("backdisable");
        }
        
        function disableLan3(){
            return;
            $("#ctl00_ContentPlaceHolder1_Text7").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_Text10").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemkienthuc3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemchuyenmon3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemkinhnghiem3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemthanhtich3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemdinhhuong3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemtieuchi3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemtinhcach3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_diemkhanang3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_tongdiem3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetkienthuc3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetchuyenmon3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetkinhnghiem3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetthanhtich3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxetdinhhuong3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxettieuchi3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_nhanxettinhcach3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_noidungkhanang3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radDat3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radKhongDat3").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radUngVienSuyNghiThem").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radXemxetthem").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radPVLai").parent().addClass("backdisable");
            $("#ctl00_ContentPlaceHolder1_radDat3").parent().addClass("backdisable");
        }
        function kqdat1(){
            return;
            if($("#ctl00_ContentPlaceHolder1_radDat1").prop("checked")){
                $("#ctl00_ContentPlaceHolder1_radKhongPhuhop").addClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac").addClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radPVlan2").addClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radKhac1").addClass("radDisable");
                //
                $("#ctl00_ContentPlaceHolder1_radKhongPhuhop").prop('checked',false);
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac").prop('checked',false);
                $("#ctl00_ContentPlaceHolder1_radPVlan2").prop('checked',false);
                $("#ctl00_ContentPlaceHolder1_radKhac1").prop('checked',false);
                //.parent().addClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radKhongPhuhop").parent().addClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac").parent().addClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radPVlan2").parent().addClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radKhac1").parent().addClass("backdisable");
            }
            if($("#ctl00_ContentPlaceHolder1_radKhongDat1").prop("checked")){
                $("#ctl00_ContentPlaceHolder1_radKhongPhuhop").removeClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac").removeClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radPVlan2").removeClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radKhac1").removeClass("radDisable");
                //
                $("#ctl00_ContentPlaceHolder1_radKhongPhuhop").parent().removeClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac").parent().removeClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radPVlan2").parent().removeClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radKhac1").parent().removeClass("backdisable");
            }
        }
        function kqdat2(){
            return;
            if($("#ctl00_ContentPlaceHolder1_radDat2").prop("checked")){ 
                $("#ctl00_ContentPlaceHolder1_radChuyenMonChuaDat").addClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac2").addClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radPVlan3").addClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radKhac2").addClass("radDisable");
                //
                $("#ctl00_ContentPlaceHolder1_radChuyenMonChuaDat").prop('checked',false);
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac2").prop('checked',false);
                $("#ctl00_ContentPlaceHolder1_radPVlan3").prop('checked',false);
                $("#ctl00_ContentPlaceHolder1_radKhac2").prop('checked',false);
                //.parent().addClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radChuyenMonChuaDat").parent().addClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac2").parent().addClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radPVlan3").parent().addClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radKhac2").parent().addClass("backdisable");
            }
            if($("#ctl00_ContentPlaceHolder1_radKhongDat2").prop("checked")){
                $("#ctl00_ContentPlaceHolder1_radChuyenMonChuaDat").removeClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac2").removeClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radPVlan3").removeClass("radDisable");
                $("#ctl00_ContentPlaceHolder1_radKhac2").removeClass("radDisable");
                //
                $("#ctl00_ContentPlaceHolder1_radChuyenMonChuaDat").parent().removeClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radSoSanhUngVienKhac2").parent().removeClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radPVlan3").parent().removeClass("backdisable");
                $("#ctl00_ContentPlaceHolder1_radKhac2").parent().removeClass("backdisable");
            }
        }
        //disableDuyetTD();
        function disableDuyetTD(){
             $("#tabduyet").addClass("tabduyet");
        }
        $(document).ready(function(){
            if($("#ctl00_ContentPlaceHolder1_type").val()==1){
                disableDuyetTD();
            }
            if($("#ctl00_ContentPlaceHolder1_type").val()==2){
                $("#tablenpv").addClass("tabduyet");
            }
        });
    </script>
    <style>
        .radDisable{
            pointer-events:none;
        }
        .tabduyet{
             pointer-events:none;
             background-color:#dddddd;
        }
    </style>

    <script>
        $('input:radio').click(function () {
            var check=$(this).attr("checked"); 
            if(check=="checked"){
                $(this).attr("checked",false);
            }
            else{
                $(this).attr("checked",true);
            } 
        });
    </script>
</asp:Content>
