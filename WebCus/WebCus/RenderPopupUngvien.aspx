<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="RenderPopupUngvien.aspx.cs" Inherits="WebCus.RenderPopupUngvien" %>

<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css" integrity="sha512-MV7K8+y+gLIBoVD59lQIYicR65iaqukzvf/nwasF0nqhPay5w/9lJmVM2hMDcnK1OnMGCdVK+iQrJ7lzPJQd1w==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Anton&family=Lobster&family=Roboto:wght@300;500;700;900&display=swap" rel="stylesheet">
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
    <style>
        body {
            background-color: #ffffff !important;
        }
    </style>
    <link href="AdminCss/admin.css" rel="stylesheet" />
    <div class="duyetungvien_box">
        <div class="duyetungvien_child">
            <div class="tieudeh1">
                <h1>Duyệt ứng viên <i class="fas fa-user" style="position: relative; top: -3px; left: 7px;"></i></h1>
            </div>
            <div class="duyetungvien_childdetail">
                <div class="tmpv_title">
                    <strong>THƯ MỜI PHỎNG VẤN</strong> <i class="fas"></i>
                </div>
                <div class="dutuyenungvien_childchild">
                    <div>
                        <span>Tên ứng viên: </span><strong id="tenuv" runat="server"></strong>
                    </div>
                    <div>
                        <span>Email: </span><strong id="emailuv" runat="server"></strong>
                    </div>
                    <div>
                        <span>SĐT: </span><strong id="sodtuv" runat="server"></strong>
                    </div>
                    <div>
                        <span>Địa chỉ: </span><strong id="diachiuv" runat="server"></strong>
                    </div>
                    <div>
                        Trước tiên, Công Ty TNHH Vi tính Nguyên Kim xin gửi tới bạn lời chúc sức khoẻ, an khang và thành đạt. 
                    </div>
                    <div>
                        Sau Quá trình sàn lọc hồ sơ, phòng nhân sự trân trọng mời bạn đến Công ty tham dự buổi phỏng vấn: 
                    </div>
                    <div>
                        <span>- Vị trí dự tuyển: </span><strong id="vitriutuv" runat="server"></strong>
                    </div>
                    <div>
                        - Địa điểm phỏng vấn: <strong>245B Trần Quang Khải, Phường Tân Định, Quận 1, TP.HCM.</strong>
                    </div>
                    <div>
                        - Thời gian: Lúc
                    <input style="width: 50px;" class="ipngay" runat="server" id="uvluc" placeholder="..." />
                        thứ
                    <input style="width: 30px" class="ipngay" runat="server" id="uvthu" placeholder="..." />
                        ngày
                    <input style="width: 30px" class="ipngay" runat="server" id="uvngay" placeholder="..." />
                        tháng
                    <input style="width: 30px" class="ipngay" runat="server" id="uvmonth" placeholder="..." />
                        năm
                    <input style="width: 40px" class="ipngay" runat="server" id="uvyear" placeholder="..." />
                    </div>
                    <div>
                        <span style="color: red">Bạn vui lòng liên hệ tiếp tân điền vào Phiếu thông tin và Bảng khảo sát khi đến tham gia Phỏng vấn</span> hoặc <span style="color: red">điền thông tin vào file đính kèm và gửi lại qua email.    </span>
                    </div>
                    <div>
                        Rất mong bạn thu xếp thời gian, nếu có sự trở ngại về mặt thời gian và vị trí ứng tuyển, bạn cũng có thể thông báo với chúng tôi theo thông tin bên dưới.
                    </div>
                    <div>
                        Xin chân thành cảm ơn và trân trọng kính chào!
                    </div>
                    <div style="font-style: italic; margin-top: 30px;">
                        Công ty Vi Tính Nguyên Kim hiện là công ty hàng đầu trong việc phân phối sỉ sản phẩm CNTT máy tính, máy in, linh kiện, camera, server, laptop … chính hãng. 
                    <br />
                        Khi được tuyển vào Công ty Vi Tính Nguyên Kim các bạn sẽ có dịp trải nghiệm môi trường làm việc thân thiện và chuyên nghiệp. 
                    <br />
                        Có nhiều cơ hội thăng tiến và thu nhập cao tùy theo hiệu quả và sự đóng góp của mỗi cá nhân.
                    <br />
                        Nhiều chính sách phúc lợi hấp dẫn dành cho người lao động:
                    <br />
                        Thưởng thâm niên 3 năm, 5 năm.
                    <br />
                        Khám sức khỏe định kỳ
                    <br />
                        Cơm trưa, đồng phục, nghỉ thư giản giữa giờ
                    <br />
                        Du lịch hàng năm
                    <br />
                        BHXH, BHYT, BHTN, BH 24/24, Bảo hiểm cao cấp
                    <br />
                        Lương thưởng tháng 13, các ngày Lễ trong năm, 12 ngày phép…

                    </div>
                </div>
            </div>
            <div class="thumoipvright">
                <div class="tgpv_title">Thông tin thời gian phỏng vấn</div>
                <div>
                    <label class="title_right">Ngày</label>
                    <input type="date" class="inputhour" onchange="slngay()" id="sldate" />
                </div>
                <div>
                    <label class="title_right">Giờ</label>
                    <input placeholder="hh:mm" class="inputhour" id="slhour" runat="server" onkeyup="slhour()" />
                </div>
                <div class="guithumpv">
                    <input type="checkbox" checked="checked" />
                    <label>Gửi thư mời phỏng vấn</label>
                </div>
                <div class="btnduyet">
                    <asp:button id="btnSaveBanner" runat="server" text="Duyệt" cssclass="btnduyet" onclick="btnSaveBanner_Click"
                        onclientclick="return CheckValidBanner();" />
                </div>
            </div>
            <div class="clearfix">
            </div>
        </div>

    </div>
    <script src="js/jquery-1.4.2.min.js"></script>
    <script>
        <%-- function ToggleSecondPopup()
        {
            <%= txtDateFrom.ClientID %>.ShowPopup();
        }--%>


        // 
        function slngay() {
            var newdate = new Date($("#sldate").val());
            $("#ctl00_ContentPlaceHolder1_uvyear").val(newdate.getFullYear());
            $("#ctl00_ContentPlaceHolder1_uvmonth").val(newdate.getMonth()+1);
            $("#ctl00_ContentPlaceHolder1_uvngay").val(newdate.getDate());
            // ctl00_ContentPlaceHolder1_slhour
            $("#ctl00_ContentPlaceHolder1_uvluc").val($("#ctl00_ContentPlaceHolder1_slhour").val());
            //ctl00_ContentPlaceHolder1_uvthu
            $("#ctl00_ContentPlaceHolder1_uvthu").val(newdate.getDay() + 1);
        }
        function slhour() {
            $("#ctl00_ContentPlaceHolder1_uvluc").val($("#ctl00_ContentPlaceHolder1_slhour").val());
        }
        function CheckValidBanner() {
            if ($("#sldate").val() == "") {
                alert("Vui lòng nhập thông tin");
                $("#sldate").focus();
                return false;
                e.preventDefault();
            }
            
            if ($("#ctl00_ContentPlaceHolder1_slhour").val()=='') {
                alert("Vui lòng nhập thông tin");
                $("#ctl00_ContentPlaceHolder1_slhour").focus();
                return false;
                e.preventDefault();
            } 
        }
    </script>
</asp:Content>
