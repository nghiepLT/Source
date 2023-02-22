<%@ Page Language="C#" AutoEventWireup="true"MasterPageFile="~/Site1.Master"  CodeBehind="RenderPopupThumoinhanviec2.aspx.cs" Inherits="WebCus.RenderPopupThumoinhanviec2" %>


<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="AdminCss/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css" integrity="sha512-MV7K8+y+gLIBoVD59lQIYicR65iaqukzvf/nwasF0nqhPay5w/9lJmVM2hMDcnK1OnMGCdVK+iQrJ7lzPJQd1w==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Anton&family=Lobster&family=Roboto:wght@300;500;700;900&display=swap" rel="stylesheet">
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
    <style>
        body {
            background-color: #ffffff !important;
            background-image:none;
        }
    </style>
    <%--<link href="AdminCss/admin.css" rel="stylesheet" />--%>
    <div class="thumoinhanviec_box">
       <table class="table table-bordered">
           <tr>
               <td colspan="2" class="text-center"> <img style="width:100px;margin:0px auto" src="Images/logo_form.png" class="img-responsive" /></td>
               <td>
                   THỎA THUẬN THỬ VIỆC
               </td>
               <td>
                   <div>
                       Mã hiệu: 06.01-BM/HCN/NK
                   </div>
                   <div>
                       Lần ban hành/sửa đổi: 1/0
                   </div>
               </td>
           </tr>
           <tr>
               <td>
                   Ngày hiệu lực
               </td>
                <td>
                   26/04/2017
               </td>
               <td>
                   Bộ phận chịu trách nhiệm
               </td>
               <td>
                   Hành chính Nhân sự
               </td>
           </tr>
       </table>
        <div class="text-right">
            TP.HCM, ngày .. tháng .. năm 2022
        </div>
        <div class="tmnv_title">
            THƯ MỜI NHẬN VIỆC
        </div>
        <div class="tmnv_kinhgui">
            Kính gửi: Anh <span id="kinhguianh" runat="server"></span> (ĐT: <span id="sodt" runat="server"></span>)
        </div>
        <div class="ctchanthanh">
            Chúng tôi chân thành cảm ơn sự quan tâm của Anh đối với công ty cũng như chức danh công việc mà Anh đã dự tuyển. Chúng tôi trân trọng thông báo Anh đã trúng tuyển trong đợt phỏng vấn vừa qua. Chúng tôi chào đón anh gia nhập và làm việc tại Công ty TNHH Vi tính Nguyên Kim theo những thông tin sau:
        </div>
        <div class="tmnv_padding">

        </div>
        <div class="lvtai">
            -Làm việc tại	: 245B Trần Quang Khải, phường Tân Định, quận 1, TP.HCM
        </div>
        <div class="lvtai">
            -Chức danh công việc	: <span id="chucdanh" runat="server"></span>
        </div>
         <div class="lvtai">
            -Bộ phận/ Phòng	: <span id="bophanphong" runat="server"></span>
        </div>
        <div class="lvtai">
            -Báo cáo trực tiếp cho	: BGĐ, Quản lý
        </div>
         <div class="lvtai">
            -Ngày nhận việc	: <input class="ip dotted w40" id="ngaynhanviec" runat="server" />		Thời gian thử việc:  <input class="ip dotted w10" id="thoigianthuviec" runat="server" />	 tháng
        </div>
         <div class="lvtai">
           Lương và các chế độ khác như sau: 
        </div>
        <div class="lvtai">
           -	Lương chính thức: Mức lương <input class="ip dotted w30" id="mucluong" runat="server" /> đồng/ tháng (Lương bao gồm phần đóng 10.5% BHXH + BHYT + BHTN + Bằng cấp của người lao động). 
        </div>
        <div class="lvtai">
           -	Lương thử việc	: <input class="ip dotted w4" id="luongthuviec" runat="server" />%.
        </div>
          <div class="lvtai">
        -	Thỏa thuận khác	: <input class="ip dotted w80" id="thoathuankhac" runat="server" />
        </div>
         <div class="lvtai">
        -	Các chế độ khác	: Theo Nội quy lao động và chế độ phúc lợi của công ty.
        </div>
        <div class="lvtai">
            Đề nghị anh bổ sung 02 bộ hồ sơ nhân sự trước hoặc trong ngày <input class="ip dotted w30" id="thoigianbosung" runat="server" />: 
        </div>
        <div class="denghiac">
            1.	Thư xin việc, SYLL (bản chính) 
        </div>
        <div class="denghiac">
            2.	CNMD/CCCD (photo sao y)
        </div>
        <div class="denghiac">
            3.	Hộ khẩu (photo sao y)
        </div>
          <div class="denghiac">
            4.	Bằng gốc (bằng <input class="ip dotted w10" id="banggoc" runat="server" />)
        </div>
         <div class="denghiac">
           5.	Bằng cấp, chứng chỉ các loại (photo sao y) 
        </div>
        <div class="denghiac">
           6.	Giấy khám sức khỏe (bản chính)
        </div>
        <div class="denghiac">
           7.	5 ảnh 3x4cm. 
        </div>
        <div>
            <strong>Tất cả hồ sơ phải được cơ quan có thẩm quyền ở địa phương xác nhận chưa quá 3 tháng.</strong>
        </div>
        <div>
            Mời Anh liên hệ với bộ phận hành chính của công ty để nhận việc. Xin vui lòng phản hồi cho chúng tôi trong vòng 24 tiếng từ khi nhận được thư này. Rất mong anh sẽ sớm hòa nhập và thực hiện tốt công việc của mình, đóng góp vào thành công chung của công ty.
        </div>
           <asp:Button ID="btnSaveBanner" runat="server" Text="Duyệt" CssClass="btnduyet" OnClick="btnSaveBanner_Click"
                        OnClientClick="return CheckValidBanner();" />
    </div>
    <style>
        .dotted {
    border-bottom: 1px dotted !important;
    width: 59%;
    position: relative;
    top: -7px;
    border: none;
    outline: none;
}
    </style>
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
    </script>
</asp:Content>
