<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RenderPopupThuChucMung.aspx.cs" Inherits="WebCus.RenderPopupThuChucMung" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css" integrity="sha512-MV7K8+y+gLIBoVD59lQIYicR65iaqukzvf/nwasF0nqhPay5w/9lJmVM2hMDcnK1OnMGCdVK+iQrJ7lzPJQd1w==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Anton&family=Lobster&family=Roboto:wght@300;500;700;900&display=swap" rel="stylesheet">
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />

    <div>
        <div>
            Chào <span class="tennv" id="nv1" runat="server"></span>,
        </div>
        <div>
            Nhân viên <span id="nhanvien" runat="server"></span>
        </div>
        <div class="heightdiv">
        </div>
        <div>
            Chúc mừng <span class="tennv" id="nv2" runat="server"></span> đã chính thức tham gia vào đội ngũ nhân viên của Công ty TNHH Vi Tính Nguyên Kim.
        </div>
        <div class="heightdiv">
        </div>
        <div>
            Theo đánh giá của Ban Giám đốc, <span class="tennv" id="nv3" runat="server"></span> trong thời gian thử việc đã có nhiều nỗ lực hoàn thành những công việc được giao và đạt được những thành công nhất định.
        </div>
        <div class="heightdiv">
        </div>
        <div>
            Ban Giám đốc công ty mong muốn <span class="tennv" id="nv4" runat="server"></span> sẽ phát huy tiềm năng của mình trong thời gian tới tại Công ty TNHH Vi Tính Nguyên Kim.
        </div>
        <div class="heightdiv">
        </div>
        <div>
            Hy vọng những đóng góp của cá nhân Chị sẽ đem lại thu nhập cho chính Chị cũng như đội ngũ nhân viên Công ty TNHH Vi Tính Nguyên Kim. 
        </div>
        <div class="heightdiv">
        </div>
        <div>
            Theo quyết định của Ban Giám đốc, thời gian tiếp nhận chính thức của <span class="tennv" id="nv5" runat="server"></span> bắt đầu từ  <input class="ips w20" id="batdautu" runat="server" />
        </div>
        <div class="heightdiv">
        </div>
        <div class="bangiamdoctitle">
            TM BAN GIÁM ĐỐC
        </div>
        <div class="bangiamdoctitle">
            PHÒNG HCNS
        </div>
        <div class="heightdiv">
        </div>
        <div>
            @mời <span class="tennv" id="nv6" runat="server"></span>liên hệ P. HCNS vào lúc 17h hôm nay <input class="ips w20" id="homnay" runat="server" /> để làm thủ tục nhân viên chính thức (BHXH, Đồng phục, ký HĐLĐ, làm thẻ ngân hàng <input class="ips w20" id="thenganhang" runat="server" />).
        </div>

        <div class="btnGuimail">
            <asp:button id="btnSaveBanner" runat="server" text="Gửi" cssclass="btnduyet" onclick="btnSaveBanner_Click"
                onclientclick="return CheckValidBanner();" />
        </div>
    </div>
    <style>
        .tennv {
            font-weight: bold;
        }

        .heightdiv {
            height: 10px;
        }

        .bangiamdoctitle {
            text-align: right;
            font-weight: bold;
            margin: 5px 0px;
        }
        .btnGuimail{
            text-align:right;
            margin-top:10px;
        }
        .btnduyet{
            display:inline-block!important;
            width:100px;
            cursor:pointer;
        }
        .ips {
    border: none;
    border-bottom: 1px dotted;
    outline: none;
    width: 100%;
    margin-bottom: 10px;
}
    </style>
</asp:Content>
