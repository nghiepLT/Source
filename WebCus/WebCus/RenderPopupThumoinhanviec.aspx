<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="RenderPopupThumoinhanviec.aspx.cs" Inherits="WebCus.RenderPopupThumoinhanviec" %>


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
    <input id="ngaynhanviec" runat="server" style="display:none" />
    <div class="duyetungvien_box">
        <div class="duyetungvien_child">
            <div class="tieudeh1">
                <h1>THƯ MỜI NHẬN VIỆC <i class="fas fa-user" style="position: relative; top: -3px; left: 7px;"></i></h1>
            </div>
            <div class="duyetungvien_childdetail">
                <div class="tmpv_title">
                    <strong>THƯ MỜI NHẬN VIỆC</strong> <i class="fas"></i>
                </div>
                <div class="dutuyenungvien_childchild">
                    <div>
                        <span>Chào Anh/Chị : </span><strong id="tenuv" runat="server"></strong>
                    </div>
                    <div>
                        <span>Điện thoại: </span><strong id="sodtuv" runat="server"></strong>
                    </div>
                    <div>
                        Sau khi tham gia các vòng phỏng vấn, phòng HCNS trân trọng thông báo Anh/Chị <strong id="tenuv2" runat="server"></strong>đã trúng tuyển vị trí Nhân viên  <strong id="vitriutuv" runat="server"></strong>
                    </div>
                    <div>
                        Khi gia nhập Công ty TNHH Vi Tính Nguyên Kim, Anh/Chị sẽ được hưởng đầy đủ các phúc lợi, quyền lợi, chính sách như các nhân viên khác. 
                    </div>
                    <div>
                        Phòng HCNS trân trọng mời Anh/Chị <strong id="tenuv3" runat="server"></strong>có mặt tại công ty <strong
                            style="color: #0070C0">địa chỉ 245B Trần Quang Khải, phường Tân Định, quận 1, TP.HCM</strong>
                        <strong style="color: red">vào lúc
                            <input class="ipngay" runat="server" id="uvluc" placeholder="..." />
                            ngày
                            <input style="width: 100px; text-align: left" class="ipngay" runat="server" id="ngaythangnam" placeholder="..." /></strong>
                    </div>

                    <div>
                        Trường hợp Anh/Chị không thể thu xếp đến nhận việc đúng thời gian, xin vui lòng liên hệ với chúng tôi theo số điện thoại <strong>0938 808 660</strong> để xác nhận lại.
                    </div>

                </div>
            </div>
            <div class="thumoipvright">
                <div class="tgpv_title">Thông tin thời gian nhận việc</div>
                <div>
                    <label class="title_right">Ngày</label>
                    <input type="date" class="inputhour" onchange="slngay()" id="sldate" />
                </div>
                <div>
                    <label class="title_right">Giờ</label>
                    <input placeholder="hh:mm" class="inputhour" id="slhour" runat="server" onkeyup="slhour()" />
                </div>
                <div id="upload_show" style="display:none;">
                    <asp:fileupload id="filesUpload" runat="server" cssclass="btn-1 btnw btnmail_import" ondatabinding="filesUpload_DataBinding" onload="filesUpload_Load" />

                </div>
                <div style="display:none;">
                    <asp:button id="btn_upload" runat="server" text="Upload" onclick="Click_uploadexcel" cssclass="btn-1 btnmail_export" />
                    <span id="spFile" runat="server">
                        <asp:hyperlink runat="server" cssclass="LinkNauha" target="_blank" id="lbFiles"
                            text='File' navigateurl=""></asp:hyperlink>
                    </span>
                </div>
                <div>
                   <input type="file" class="btn-group btn btn-info btn-xs upload" id="uploadAvatar" accept="application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint,text/plain, application/pdf, image/*" /><br />
                                <input type="hidden" id="Filess" runat="server" />
                </div>
                <div>
                    <asp:button id="btnSaveBanner" runat="server" text="Duyệt" cssclass="btnduyet" onclick="btnSaveBanner_Click"
                        onclientclick="return CheckValidBanner();" />
                </div>
            </div>
            <div class="clearfix">
            </div>
        </div>

    </div>
    <script src="js/jquery-1.8.2.js"></script>
    <script>
        
        // 
        function slngay() {
            var newdate = new Date($("#sldate").val());
            $("#ctl00_ContentPlaceHolder1_uvyear").val(newdate.getFullYear());
            $("#ctl00_ContentPlaceHolder1_uvmonth").val(newdate.getMonth());
            $("#ctl00_ContentPlaceHolder1_uvngay").val(newdate.getDate());
            // ctl00_ContentPlaceHolder1_slhour
            $("#ctl00_ContentPlaceHolder1_uvluc").val($("#ctl00_ContentPlaceHolder1_slhour").val());
            //ctl00_ContentPlaceHolder1_uvthu
            $("#ctl00_ContentPlaceHolder1_uvthu").val(newdate.getDay() + 1);
            $("#ctl00_ContentPlaceHolder1_ngaythangnam").val(newdate.getDate() + "\/" + (parseInt(newdate.getMonth()) + 1) + "\/" + newdate.getFullYear());
        }

        function slhour() {
            $("#ctl00_ContentPlaceHolder1_uvluc").val($("#ctl00_ContentPlaceHolder1_slhour").val());
        }

        function upfile() {
            $.ajax({
                type: "POST", //POST
                url: "UploadImages.ashx",
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (msg) {
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }

        var _URL = window.URL || window.webkitURL;
        $("#uploadAvatar").on('change', function () {
            var file, img;

            if ((file = this.files[0])) {
                //img = new Image();
                sendFile(file);
                //img.onerror = function () {
                //    alert("Not a valid file:" + file.type);
                //};
                //img.src = _URL.createObjectURL(file);
            }
        });
        function sendFile(file) {
            console.log("sendfike")
            var formData = new FormData();
            formData.append('file', $('#uploadAvatar')[0].files[0]);
            $.ajax({
                url: "../UploadImages.ashx",
                type: "POST",
                data: formData,
                success: function (status) {
                  $("#ctl00_ContentPlaceHolder1_Filess").val(status);
                },
                processData: false,
                contentType: false,
                error: function () {
                    alert("Whoops something went wrong!");
                }
            });
        }

        function CheckValidBanner() {
            if ($("#sldate").val() == '') {
                alert("Vui lòng nhập thời gian!");
                return false;
            }
            if ($("#ctl00_ContentPlaceHolder1_slhour").val() == '') {
                alert("Vui lòng nhập thời gian!");
                return false;
            }
            if ($("#ctl00_ContentPlaceHolder1_Filess").val() == '') {
                alert("Vui lòng chọn file!");
                return false;
            }
            $("#ctl00_ContentPlaceHolder1_ngaynhanviec").val($("#sldate").val().replace("-", "/").replace("-", "/"));
        }
    </script>
    <style>
        .ips {
            border: none;
            border-bottom: 1px dotted;
            outline: none;
            width: 100%;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>
