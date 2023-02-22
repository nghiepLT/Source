<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TTNhanvien.ascx.cs"
    Inherits="WebCus.TTNhanvien" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script src="../js/select_tag_js/jquery.min.js" type="text/javascript"></script>
<script src="../js/select_tag_js/selectize.js" type="text/javascript"></script>
<script src="../js/select_tag_js/index.js" type="text/javascript"></script>
<link href="../js/select_tag_js/selectize.default.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Include/JS/ShowImage.js"></script>

<style type="text/css">
    .RB_Ls
    {
        text-align: center !important;
    }
</style>
<script language="JAVASCRIPT" type="text/javascript">
$(document).ready(function(){
$('#hideshow').show();
    $('#hideshow').on('click', function(event) {        
         $('#upload_show').toggle("slide", { direction: "right" }, 100);
         $('#hideshow').hide();
    });
});
 function verifyupdate() {
        msg = "Cập nhật thông tin nhân viên này ?";
        return confirm(msg);
    } 
    function verify() {
        msg = "Bạn chắc chắn muốn xóa ? thao tác này không thể phục hồi !";
        return confirm(msg);
    } 
     function verify2() {
        msg = "Bạn chắc chắn muốn hủy phiếu này không ? thao tác không thể phục hồi !";
        return confirm(msg);
    } 
    function ToggleSecondPopup()
    {
       <%= txtDateFrom.ClientID %>.ShowPopup();
    }

    function ToggleSecondPopup1()
    {
        <%= txtDateTo.ClientID %>.ShowPopup();
    }
    

     function TogglePopupngaysinh()
    {
        <%= rad_ngaysinh.ClientID %>.ShowPopup();
    }
    
      function Togglengaycapcmnd()
    {
        <%= rad_ngaycapcmnd.ClientID %>.ShowPopup();
    }
    function CheckValidate() {
   
        return true;
    }
   
</script>
<script language="javascript" type="text/javascript">
    function CheckAll(chkCheckAll) {
        var arrObj = document.getElementsByName("chkChoose");
        var result = '';
        var count = 0;
        for (i = 0; i < arrObj.length; i++) {

            arrObj[i].checked = chkCheckAll.checked;

            if (arrObj[i].checked) {
                result += arrObj[i].id + ';';
                count++;
            }
        }
        var hdn_Transaction_Detail_ID = document.getElementById('<%=hdn_Transaction_Detail_ID.ClientID %>');
        hdn_Transaction_Detail_ID.value = result;
    }

    function AddValue() {
        var arrCheck = document.getElementsByName('chkChoose');
        var result = '';
        var count = 0;
        for (i = 0; i < arrCheck.length; i++) {
            var item = arrCheck[i];
            if (item.checked) {
                result += item.id + ';';
                count++;
            }
        }
        var hdn_Transaction_Detail_ID = document.getElementById('<%=hdn_Transaction_Detail_ID.ClientID %>');
        hdn_Transaction_Detail_ID.value = result;
    }

    function CheckValid() {
        var hdn_Transaction_Detail_ID = document.getElementById('<%=hdn_Transaction_Detail_ID.ClientID %>');
        if (hdn_Transaction_Detail_ID.value == '') {
            alert('Chọn item Đơn hàng');
            return false;
        }
        return true;
    }
    function CheckAndConfirmDelete() {

        var strValueIDChecked = '';

        var arrChkItem = document.getElementsByName('chkChoose');

        for (var i = 0; i < arrChkItem.length; i++) {
            var chkItem = arrChkItem[i];
            if (chkItem.checked == true) {
                strValueIDChecked += chkItem.value + '|';
            }

        }

        if (strValueIDChecked == '') {
            alert("Vui lòng chọn sản phẩm cần xóa");
            return false;
        }
        else {
            if (confirm('Bạn có chắc xóa các sản phẩm này không?')) {
                var hdn_Transaction_Detail_ID = document.getElementById('<%=hdn_Transaction_Detail_ID.ClientID %>');
                hdn_Transaction_Detail_ID.value = strValueIDChecked;
                return true;
            }
            else
                return false;

        }
    }

   
     function ShowPopupAddKey(IDUser) {
     WindowOpen('RenderPopup.aspx?smid=UserMng&renderPage=UserControl.ascx&md=PoinKm&id='+ <%=UserMemberID %>,'PoinKm', 500, 500, 'yes');
        return false;
    }
   function CheckFinishValue() {
   
        return true;
}

  function ShowImage(event, isShow) {

        if (isShow == 0) {
            document.getElementById("divImageProduct").style.display = 'none';
            return false;
        } else {
            PopupArea(event, "divImageProduct");
            return false;
        }

    }
  function ShowImage_nguoiThan(event, isShow) {

        if (isShow == 0) {
            document.getElementById("divImageNguoiThan").style.display = 'none';
            return false;
        } else {
            PopupArea(event, "divImageNguoiThan");
            return false;
        }

    }
    function ValidateKeypress(numcheck, e) {
                var keynum, keychar, numcheck;
                if (window.event) {//IE
                    keynum = e.keyCode;
                }
                else if (e.which) {// Netscape/Firefox/Opera
                    keynum = e.which;
                }
                if (keynum == 8 || keynum == 127 || keynum == null || keynum == 9 || keynum == 0 || keynum == 13) return true;
                keychar = String.fromCharCode(keynum);
                var result = numcheck.test(keychar);
                return result;
            }
</script>
<asp:HiddenField ID="hdn_Transaction_Detail_ID" Value='' runat="server" />
<asp:HiddenField ID="hdn_macv" Value='' runat="server" />
<div class="page-title">
    <h2 class="icon-title">
        <span>Danh Sách Nhân Viên</span>
    </h2>
</div>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="tbn_action" runat="server">
    <tbody>
        <tr>
            <td class="C">
            </td>
            <td class="R">
                <asp:Button ID="btnInsert" runat="server" Text="Tạo mới " CssClass="btn-1" OnClick="btnInsert_Click" />
                &nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSave_Click"
                    OnClientClick="return CheckValidate();" />
                &nbsp;
                <asp:Button ID="btndelete" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDeleteKey"
                    OnClientClick="return CheckValidate();" Visible="false" />
            </td>
        </tr>
    </tbody>
</table>
<div class="page-title" style="font-size: 15px;">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</div>
<div class="TboardBox" id="tbttkt" runat="server">
<fieldset id="div_button_active" runat="server">
        <legend class="page-title" style="font-size: 15px;">Cập nhật Thông Tin</legend>
        <table cellpadding="0" cellspacing="0">
            <tr class="trLabelFilter1">
                <td class="pd5">
                    <asp:Button ID="btnxoa" Width="120px" CommandArgument="0" CssClass="btn-1" style="background:repeat-x scroll 0 top #ff0023 !important;border-color:#ff0023 #ff0023 #ff0023 #ff0023;text-shadow:0 -1px 2px #fff" Text="Xóa Nhân Viên"
                        runat="server" OnClick="btnUpdate_Status_Click" OnClientClick="return verify();">
                    </asp:Button>
                </td>
                <td class="pd5">
                    <asp:Button ID="btn_capnhatttnhanvien" Width="120px" CssClass="btn-1" Text="Nhân viên"
                        runat="server" OnClick="btncnnv_Click"></asp:Button>
                    <%--<asp:Button ID="btn_showcnttnv" Width="200px" CssClass="btn-1" Text="Cập nhật thông tin nhân viên"
                        runat="server" OnClick="btnSave_Click" OnClientClick="return verifyupdate();">
                    </asp:Button>--%>
                </td>
                <td class="pd5">
                    <asp:Button ID="btn_capnhatttungvien" Width="120px" CssClass="btn-1" Text="Tuyển Dụng"
                        runat="server" OnClick="btncnuv_Click" />
                </td>
                <td class="pd5">
                    <asp:Button ID="btn_capnhatttnhansu" Width="120px" CssClass="btn-1" Text="Nhân sự"
                        runat="server" OnClick="btncnns_Click" />
                </td>
                  <td class="pd5">
                    <asp:Button ID="Button6" Width="120px" CssClass="btn-1" Text="Người Thân"
                        runat="server" OnClick="btncnnt_Click" />
                </td>
                  <td class="pd5">
                    <asp:Button ID="Button7" Width="150px" CssClass="btn-1" Text="Quá Trình Làm việc"
                        runat="server" OnClick="btncnqtlv_Click" />
                </td>
                <td class="pd5">
                    <asp:Button ID="btn_duyetphep" Width="120px" CommandArgument="1" CssClass="btn-1"
                        Visible="false" Text="Duyệt Phép" runat="server" OnClick="btnUpdate_Status_Click"
                        OnClientClick="return CheckFinishValue();"></asp:Button>
                </td>
                <td class="pd5">
                    <asp:Button ID="btn_khongduyet" Width="120px" CommandArgument="2" CssClass="btn-1"
                        Visible="false" Text="Không Duyệt" runat="server" OnClick="btnUpdate_Status_Click"
                        OnClientClick="return CheckFinishValue();"></asp:Button>
                </td>
            </tr>
        </table>
    </fieldset>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tbttNhanvien"
        runat="server">
        <tr>
            <td colspan="4" class="Line2" >
          
            </td>
        </tr>
         <tr>
            <td colspan="4" style="margin-top:10px; text-align:center; background:url(images/common/bkg-btn-blue.gif) repeat-x scroll 0 top #3A8FCE;color:#fff;padding:10px;" >
            THÔNG TIN NHÂN VIÊN
            </td>
        </tr>
        <tr id="tr_hoten" runat="server">
            <th width="20%" class="RB_L">
                Họ tên
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_hoten" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Giới tính
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_gioitinh" runat="server" CssClass="demo-default" Width="50%">
                    <asp:ListItem Text="--Chọn giới tính--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Nam" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Nữ" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr1" runat="server">
            <th width="20%" class="RB_L">
                Ngày Sinh
            </th>
            <td class="B_L" width="30%">
                <radCln:RadDatePicker ID="rad_ngaysinh" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="TogglePopupngaysinh()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
            </td>
            <th width="20%" class="RB_L">
                Nơi Sinh
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_noisinh" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr2" runat="server">
            <th width="20%" class="RB_L">
                Số CMND
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_socmnd" runat="server" CssClass="Input_text" Width="90%" onkeypress="return ValidateKeypress(/\d/,event);"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Ngày Cấp CMND
            </th>
            <td class="B_L" width="30%">
                <radCln:RadDatePicker ID="rad_ngaycapcmnd" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1900-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengaycapcmnd()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                Nơi Cấp :  <asp:TextBox ID="txt_noicapcmnd" runat="server" CssClass="Input_text" Width="44.5%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr3" runat="server">
            <th width="20%" class="RB_L">
                Đ/c Tạm Trú
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_dctamtru" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Đ/c Thường Trú
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_dcthuongtru" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr4" runat="server">
            <th width="20%" class="RB_L">
                Email
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_email" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Số Điện Thoại
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_sdt" runat="server" CssClass="Input_text" Width="90%" onkeypress="return ValidateKeypress(/\d/,event);"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr5" runat="server">
            <th width="20%" class="RB_L">
                Trình Độ
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_trinhdonhanvien" runat="server" CssClass="demo-default"
                    Width="92.5%">
                    <asp:ListItem Text="--Chọn Trình Độ--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="THPT" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Trung cấp" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Cao Đẳng" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Đại Học" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Sau Đại Học" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="20%" class="RB_L">
                Chuyên Môn
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_chuyenmon" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr6" runat="server">
            <th width="20%" class="RB_L">
                Kinh Nghiệm
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_kinhnghiem" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Mã số thuế
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_masothue" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr7" runat="server">
            <th width="20%" class="RB_L">
                TK Ngân hàng
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_tknganhang" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Số TK Ngân Hàng
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txtsotknganhang" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr10" runat="server">
            <th width="20%" class="RB_L">
                Dân tộc
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_dantoc" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Tôn giáo
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_tongiao" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr8" runat="server">
            <th width="20%" class="RB_L">
                Nguyên Quán
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_nguyenQuan" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
             <th class="RB_L" width="15%">
           Tình trạng hôn nhân
            </th>
             <td  class="B_L">
                <asp:TextBox ID="txt_tinhtranghonnhan" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
           
        </tr>
        <tr>
            <th class="RB_L" width="15%">
                <asp:Label ID="Label16" runat="server" Text="Ảnh NV"></asp:Label>
            </th>
            <td class="B_L">
                <table onmouseover="return ShowImage(event, 1);" onmouseout="return ShowImage(event, 0);">
                    <tr>
                        <td>
                            <asp:Label ID="lblImage" runat="server" Text="" ForeColor="BlueViolet" Style="cursor: pointer"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:FileUpload ID="fileImage" runat="server" Height="20px" CssClass="Input_text" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="fileImage"
                    ErrorMessage="Không phải file ảnh" ValidationExpression="^([0-9a-zA-Z_\-~ :\\])+(.jpg|.JPG|.jpeg|.JPEG|.bmp|.BMP|.gif|.GIF|.png|.PNG)$">
                </asp:RegularExpressionValidator>
            </td>
            <th width="20%" class="RB_L">
                Ghi chú
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_ghichunhanvien" runat="server" CssClass="Input_text" Width="90%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="B_L" colspan="4" style="margin-top:10px; text-align:center;">
                <asp:Button ID="btn_capnhatnhanvien" Width="100px" CssClass="btn-1" Text="Cập nhật"
                    runat="server" OnClick="btnSave_Click" OnClientClick="return verifyupdate();" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tbttUngVien" runat="server">
        <tr>
            <td colspan="4" class="Line2">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="margin-top:10px; text-align:center; background:url(images/common/bkg-btn-blue.gif) repeat-x scroll 0 top #3A8FCE;color:#fff;padding:10px;" >
            THÔNG TIN PHỎNG VẤN
            </td>
        </tr>
        <tr id="tr9" runat="server">
            <th width="20%" class="RB_L">
                Ngày PV lần 1
            </th>
            <td class="B_L" width="30%">
                <radCln:RadDatePicker ID="rad_ngaypvl1" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengaypvl1()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengaypvl1()
                    {
        <%= rad_ngaypvl1.ClientID %>.ShowPopup();
                      }

                </script>
                Kết Quả
                <asp:DropDownList ID="dr_kql1" runat="server" CssClass="demo-default" Width="50%">
                    <asp:ListItem Text="--Chọn kết quả--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Đạt" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Không đạt" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Lưu Hồ Sơ" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="20%" class="RB_L">
                Ngày PV lần 2
            </th>
            <td class="B_L" width="30%">
                <radCln:RadDatePicker ID="rad_ngaypvl2" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengaypvl2()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengaypvl2()
                    {
        <%= rad_ngaypvl2.ClientID %>.ShowPopup();
                      }

                </script>
                Kết Quả
                <asp:DropDownList ID="dr_kql2" runat="server" CssClass="demo-default" Width="50%">
                    <asp:ListItem Text="--Chọn kết quả--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Đạt" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Không đạt" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Lưu Hồ Sơ" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr12" runat="server">
            <th width="20%" class="RB_L">
                Ngày PV lần 3
            </th>
            <td class="B_L" width="30%">
                <radCln:RadDatePicker ID="rad_ngaypvl3" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengaypvl3()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengaypvl3()
                    {
        <%= rad_ngaypvl3.ClientID %>.ShowPopup();
                      }

                </script>
                Kết Quả
                <asp:DropDownList ID="dr_kql3" runat="server" CssClass="demo-default" Width="50%">
                    <asp:ListItem Text="--Chọn kết quả--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Đạt" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Không đạt" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Lưu Hồ Sơ" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="20%" class="RB_L">
                Ngày PV lại
            </th>
            <td class="B_L" width="30%">
                <radCln:RadDatePicker ID="rad_ngaypvlai" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengaypvlai()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengaypvlai()
                    {
        <%= rad_ngaypvlai.ClientID %>.ShowPopup();
                      }

                </script>
                Kết Quả
                <asp:DropDownList ID="dr_kqlai" runat="server" CssClass="demo-default" Width="50%">
                    <asp:ListItem Text="--Chọn kết quả--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Đạt" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Không đạt" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Lưu Hồ Sơ" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="tr14" runat="server">
            <th width="20%" class="RB_L">
                Ngày Duyệt HS
            </th>
            <td class="B_L" width="30%">
                <radCln:RadDatePicker ID="rad_ngayduyeths" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengayduyeths()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengayduyeths()
                    {
        <%= rad_ngayduyeths.ClientID %>.ShowPopup();
                      }

                </script>
            </td>
            <th width="20%" class="RB_L">
                Ngày Vào Làm
            </th>
            <td class="B_L" width="30%">
                 <radCln:RadDatePicker ID="rad_ngayvaolam" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengayvaolam()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengayvaolam()
                    {
        <%= rad_ngayvaolam.ClientID %>.ShowPopup();
                      }

                </script>
            </td>
        </tr>
        <tr id="tr15" runat="server">
            <th width="20%" class="RB_L">
                Chấp nhận Cv
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_chapnhancv" runat="server" CssClass="demo-default" Width="92.5%">
                    <asp:ListItem Text="--Chọn Trạng Thái--" Value="0" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Chấp nhận" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Không" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="20%" class="RB_L">
                Ghi chú PV
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_ghichuphongvan" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr16" runat="server">
            <th width="20%" class="RB_L">
                Có Thể Gọi Lại
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_goilai" runat="server" CssClass="demo-default" Width="92.5%">
                    <asp:ListItem Text="--Chọn Trạng Thái--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="20%" class="RB_L">
                Ghi chú gọi lại
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_ghichugoilai" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr17" runat="server">
            <th width="20%" class="RB_L">
                Vị trí tuyển dụng
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_vitrituyendung" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Nguồn Tuyển Dụng
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_nguontuyendung" runat="server" CssClass="demo-default" Width="92.5%">
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr id="tr18" runat="server" >
            <td class="B_L" colspan="4" style="margin-top:10px; text-align:center;">
                <asp:Button ID="Button1" Width="100px" CssClass="btn-1" Text="Cập nhật" runat="server"
                    OnClick="btnSaveUV_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tbttNhansu" runat="server">
        <tr>
            <td colspan="4" class="Line2">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="margin-top:10px; text-align:center; background:url(images/common/bkg-btn-blue.gif) repeat-x scroll 0 top #3A8FCE;color:#fff;padding:10px;" >
            THÔNG TIN NHÂN SỰ
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
                Trực Thuộc
            </th>
            <td class="B_L" width="30%">
              <asp:DropDownList ID="dr_tructhuoccty" runat="server" CssClass="demo-default" 
                    Width="92.5%" AutoPostBack="true" 
                    onselectedindexchanged="dr_tructhuoccty_SelectedIndexChanged">
                    <asp:ListItem Text="--Chọn Cty--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Nguyên Kim" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Chính Nhân" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Smart Connec" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="20%" class="RB_L">
                Mã Nhân Viên
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_manv" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <th width="20%" class="RB_L">
                Vị Trí
            </th>
            <td class="B_L" width="30%">
              <asp:DropDownList ID="dr_vitri" runat="server" CssClass="demo-default" 
                    Width="92.5%">
                    <asp:ListItem Text="--Chọn Vị Trí--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Giám Đốc" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Phó GĐ" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Trợ lý GĐ" Value="8"></asp:ListItem>
                    <asp:ListItem Text="Trưởng Phòng" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Phó Phòng" Value="2"></asp:ListItem>
                     <asp:ListItem Text="Trưởng Bộ Phận" Value="9"></asp:ListItem>
                     <asp:ListItem Text="Phó Bộ Phận" Value="10"></asp:ListItem>
                      <asp:ListItem Text="Trưởng Nhóm" Value="3"></asp:ListItem>
                     <asp:ListItem Text="Phó Nhóm" Value="11"></asp:ListItem>                   
                    <asp:ListItem Text="Nhân Viên" Value="4"></asp:ListItem>
                     <asp:ListItem Text="NV Thực Tập" Value="7"></asp:ListItem>
                    
                </asp:DropDownList>
            </td>
            <th width="20%" class="RB_L">
                Phòng Ban
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_phongban" runat="server" CssClass="demo-default" 
                    Width="92.5%">
                   
                </asp:DropDownList>
            </td>
        </tr>      
        <tr>
            <th width="20%" class="RB_L">
               Ngày Ký Hợp Đồng
            </th>
            <td class="B_L" width="30%">
               <radCln:RadDatePicker ID="rad_ngaykyhd" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengaykhd()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengaykhd()
                    {
        <%= rad_ngaykyhd.ClientID %>.ShowPopup();
                      }

                </script>
            </td>
            <th width="20%" class="RB_L">
                Thâm niên
            </th>
            <td class="B_L" width="30%">
                 <asp:TextBox ID="txt_thamnien" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
               Ngày Tham Gia BHXH
            </th>
            <td class="B_L" width="30%">
              <asp:TextBox ID="txtngaybhxh" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Ngày Tham Gia BHYT
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txtngaybhyt" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
               Ngày Tham Gia BHTN
            </th>
            <td class="B_L" width="30%">
              <asp:TextBox ID="txtngaybhtn" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Ngày Tham Gia BHCC
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txtngaybhcc" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
               Số Sổ BH
            </th>
            <td class="B_L" width="30%">
              <asp:TextBox ID="txtsosobh" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Ngày Vào Làm
            </th>
            <td class="B_L" width="30%">
                 <radCln:RadDatePicker ID="rad_ngayvaonhanviec" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengayvaonhanviec()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengayvaonhanviec()
                    {
        <%= rad_ngayvaonhanviec.ClientID %>.ShowPopup();
                      }

                </script>
                  Loại NV : 
            <asp:DropDownList ID="dr_loainv" runat="server" CssClass="demo-default" 
                    Width="49%">
                    <asp:ListItem Text="--Chọn Loại NV--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="NV Chính Thức" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Thử Việc" Value="2"></asp:ListItem>
                      <asp:ListItem Text="Nghĩ Việc" Value="3"></asp:ListItem>
                  
                </asp:DropDownList>
            </td>
          
        </tr>
        <tr>
            <td class="B_L" colspan="4" style="margin-top:10px; text-align:center;">
                <asp:Button ID="Button5" Width="100px" CssClass="btn-1" Text="Cập nhật" runat="server"
                    OnClick="btnSaveNS_Click" OnClientClick="return verifyupdate();"/>            
                
           </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tablettnt" runat="server">
        <tr>
            <td colspan="4" class="Line2">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="margin-top:10px; text-align:center; background:url(images/common/bkg-btn-blue.gif) repeat-x scroll 0 top #3A8FCE;color:#fff;padding:10px;" >
            THÔNG TIN NGƯỜI THÂN
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
              Họ Tên
            </th>
            <td class="B_L" width="30%">
              <asp:TextBox ID="txt_hotennguoithan" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Số ĐT
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_soDTnt" runat="server" CssClass="Input_text" Width="90%" onkeypress="return ValidateKeypress(/\d/,event);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
               Địa Chỉ
            </th>
            <td class="B_L" width="30%">
              <asp:TextBox ID="txt_diachiNt" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Mối Quan hệ
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_moiquanhe" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
               Nghề Nghiệp
            </th>
            <td class="B_L" width="30%">
              <asp:TextBox ID="txt_ngheNghiepnt" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Ghi chú
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_ghichunt" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
               Ngày sinh
            </th>
            <td class="B_L" width="30%">
                <radCln:RadDatePicker ID="rad_ngaysinhnt" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengaysinhnguoithan()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengaysinhnguoithan()
                    {
        <%= rad_ngaysinhnt.ClientID %>.ShowPopup();
                      }

                </script>
            </td>
            <th width="20%" class="RB_L">
                Giới Tính
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_gioitinhnguoithan" runat="server" CssClass="demo-default" 
                    Width="92.5%">
                    <asp:ListItem Text="--Chọn giới tính--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Nam" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Nữ" Value="2"></asp:ListItem>                    
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
              Hình Ảnh
            </th>
            <td class="B_L"  width="30%">
                <table onmouseover="return ShowImage_nguoiThan(event, 1);" onmouseout="return ShowImage_nguoiThan(event, 0);">
                    <tr>
                        <td>
                            <asp:Label ID="lbl_Imagettngthan" runat="server" Text="" ForeColor="BlueViolet" Style="cursor: pointer"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:FileUpload ID="FileUpload1" runat="server" Height="20px" CssClass="Input_text" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FileUpload1"
                    ErrorMessage="Không phải file ảnh" ValidationExpression="^([0-9a-zA-Z_\-~ :\\])+(.jpg|.JPG|.jpeg|.JPEG|.bmp|.BMP|.gif|.GIF|.png|.PNG)$">
                </asp:RegularExpressionValidator>
            </td>
            <th width="20%" class="RB_L">
                Quê quán
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_quequannt" runat="server" CssClass="Input_text" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="B_L" colspan="4" style="margin-top:10px; text-align:center;">
                <asp:Button ID="btn_capnhanguoithan" Width="100px" CssClass="btn-1" Text="Cập nhật" runat="server"
                    OnClick="btnSaveNT_Click" OnClientClick="return verifyupdate();"/>            
                 <asp:Button ID="Button9" Width="100px" CssClass="btn-1" Text="Thêm Mới" runat="server"
                    OnClick="btnaddNT_Click"/> 
           </td>
        </tr>
        <tr>
            <th width="100%" class="RB_L" colspan="4" style="text-align:center;background:#5a85a5;color:#fff">
               Danh Sách Người Thân
            </th>
        </tr>
        <tr> 
            <td class="B_L" colspan="4">
             <asp:GridView ID="gr_dsnguoithan" runat="server" CssClass="infotable" AutoGenerateColumns="false"
                    AlternatingRowStyle-CssClass="row" RowStyle-CssClass="row-alt" OnRowCommand="gvList_RowCommand2"
                    OnRowDataBound="gr_dsnguoithan_RowDataBound" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="checkallid" ItemStyle-CssClass="C" Visible="false">
                            <HeaderTemplate>
                                Check ALL
                                <br />
                                <input id="ckhAll" type="checkbox" onclick="CheckAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id='<%#Eval("Id") %>' name="chkChoose" onclick="AddValue(this.id);"
                                    type='checkbox' />
                            </ItemTemplate>
                            <ItemStyle Wrap="True" CssClass="RB_C"></ItemStyle>
                            <HeaderStyle CssClass="RB_C" />
                        </asp:TemplateField>
                    </Columns>
                    
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                H.Ảnh
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a class="fancybox" rel="<%#Eval("IdNhanVien")%>" href='<%#GetUSerImageUrl(Eval("Image"))%>'>
                                    <img src="<%#GetUSerImageUrl(Eval("Image"))%>" width="50px" height="60px" alt="<%#Eval("TenNguoiThan")%>" />
                                </a>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L RB_Ls" />
                            <HeaderStyle CssClass="RB_L RB_Ls" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Họ tên
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("TenNguoiThan")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Giới tính
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#string.Format("{0}", Eval("GioiTinh").ToString() == "1" ? "Nam" : "Nữ")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Ngày sinh
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("NgaySinh", "{0:dd-MM-yyyy}")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle CssClass="RB_L" Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="DenNgay">
                            <HeaderTemplate>
                                Địa Chỉ
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("DiaChiNguoiThan")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="SoNgayNghi">
                            <HeaderTemplate>
                                Nguyên Quán
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("QueQuan")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                               Số ĐT
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("SoDTNguoiThan")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Đ/c Thường Trú
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("DiaChiNguoiThan")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Quan Hệ
                            </HeaderTemplate>
                            <ItemTemplate>                                
                                <%#Eval("MoiQuanHe")%>
                                
                               
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Nghề Nghiệp
                            </HeaderTemplate>
                            <ItemTemplate>                                
                                <%#Eval("NgheNghiep")%>
                                
                               
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Ghi Chú
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("GhiChu")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    
                    
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="Btn_editNT" ImageUrl="/images/icon/modify.gif" ToolTip="Xem/sửa"
                                    CommandArgument='<%# Eval("Id") %>' runat="server" CommandName="capnhatnguoithan" />
                                &nbsp;
                                <asp:ImageButton ID="imgDelete_NT" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="Deletenguoithan"
                                    OnClientClick="return confirm('Bạn thật sự muốn xóa?')" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="B_C" Width="10px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
           
        </tr>
        
        </table>
        
   <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tableqtlv" runat="server">
        <tr>
            <td colspan="4" class="Line2">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="margin-top:10px; text-align:center; background:url(images/common/bkg-btn-blue.gif) repeat-x scroll 0 top #3A8FCE;color:#fff;padding:10px;" >
            QUÁ TRÌNH LÀM VIỆC
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
               Ngày Thực Hiện
            </th>
            <td class="B_L" width="30%">
                <radCln:RadDatePicker ID="rad_ngaythuchien" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="Togglengaythuchien()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                <script type="text/javascript">
                  function Togglengaythuchien()
                    {
        <%= rad_ngaythuchien.ClientID %>.ShowPopup();
                      }

                </script>
            </td>
            <th width="20%" class="RB_L">
               Loại 
            </th>
            <td class="B_L" width="30%">
                <asp:DropDownList ID="dr_loaikytluatkhenthuong" runat="server" CssClass="demo-default" 
                    Width="92.5%">
                    <asp:ListItem Text="--Chọn Loại--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Khen Thưởng" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Kỷ Luật" Value="2"></asp:ListItem>     
                  
                    <asp:ListItem Text="Đào Tạo Nội Bộ" Value="3"></asp:ListItem>     
                    <asp:ListItem Text="Đào Tạo Bên Ngoài" Value="4"></asp:ListItem>  
                    <asp:ListItem Text="Thay Đỗi Vị Trí" Value="5"></asp:ListItem>                 
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="20%" class="RB_L">
              Lý Do
            </th>
            <td class="B_L" width="30%">
              <asp:TextBox ID="txt_lyDokhenthuong" runat="server" CssClass="Input_text" Width="90%" TextMode="MultiLine"></asp:TextBox>
            </td>
            <th width="20%" class="RB_L">
                Ghi chú
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txt_ghichukhenthuong" runat="server" CssClass="Input_text" Width="90%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="B_L" colspan="4" style="margin-top:10px; text-align:center;">
                <asp:Button ID="Button_capnhatkl" Width="100px" CssClass="btn-1" Text="Cập nhật" runat="server"
                    OnClick="btnSaveQTLV_Click" OnClientClick="return verifyupdate();"/>            
                  <asp:Button ID="Button8" Width="100px" CssClass="btn-1" Text="Thêm Mới" runat="server"
                    OnClick="btnAdd_qtlv_Click"/> 
           </td>
        </tr>
          <tr>
            <th width="100%" class="RB_L" colspan="4" style="text-align:center;background:#5a85a5;color:#fff">
              Chi Tiết Quá Trình Làm Việc
            </th>
        </tr>
        <tr> 
            <td class="B_L" colspan="4">
             <asp:GridView ID="gr_kyluatkhenthuong" runat="server" CssClass="infotable" AutoGenerateColumns="false"
                    AlternatingRowStyle-CssClass="row" RowStyle-CssClass="row-alt" OnRowCommand="gvList_RowCommand3"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="checkallid" ItemStyle-CssClass="C" Visible="false">
                            <HeaderTemplate>
                                Check ALL
                                <br />
                                <input id="ckhAll" type="checkbox" onclick="CheckAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id='<%#Eval("Id") %>' name="chkChoose" onclick="AddValue(this.id);"
                                    type='checkbox' />
                            </ItemTemplate>
                            <ItemStyle Wrap="True" CssClass="RB_C"></ItemStyle>
                            <HeaderStyle CssClass="RB_C" />
                        </asp:TemplateField>
                    </Columns>
                    
                    <%--<Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                H.Ảnh
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a class="fancybox" rel="<%#Eval("IdNhanVien")%>" href='<%#GetUSerImageUrl(Eval("Image"))%>'>
                                    <img src="<%#GetUSerImageUrl(Eval("Image"))%>" width="50px" height="60px" alt="<%#Eval("TenNguoiThan")%>" />
                                </a>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L RB_Ls" />
                            <HeaderStyle CssClass="RB_L RB_Ls" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Loại
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#GetNameLoai(Eval("Loai"))%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <%--<Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Ngày
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#string.Format("{0}", Eval("Gioitinh").ToString()=="1" ? "Nam" : "Nữ")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Ngày
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("NgayThucHien", "{0:dd-MM-yyyy}")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle CssClass="RB_L" Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="DenNgay">
                            <HeaderTemplate>
                              Lý Do
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("LyDo")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="SoNgayNghi">
                            <HeaderTemplate>
                                Ghi Chú
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("GhiChu")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="Btn_editNT" ImageUrl="/images/icon/modify.gif" ToolTip="Xem/sửa"
                                    CommandArgument='<%# Eval("Id") %>' runat="server" CommandName="capnhatqtlv" />
                                &nbsp;
                                <asp:ImageButton ID="imgDelete_NT" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="DeleteQtlv"
                                    OnClientClick="return confirm('Bạn thật sự muốn xóa?')" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="B_C" Width="10px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
           
        </tr>
        </table>
</div>
<hr />
<table cellpadding="0" cellspacing="0">
    <tr class="trLabelFilter1" style="display:none;">
        <td>
            upload sp
            <asp:FileUpload ID="Upload" runat="server" />
            <asp:Button ID="Button3" runat="server" Text="Upload" OnClick="Click_upload" />
        </td>
    </tr>
    <tr class="trLabelFilter1">
       
        <td style="display:none;">
            Từ ngày
        </td>
        <td style="display:none;">
            Đến ngày
        </td>
        <td>
            Trực thuộc
        </td>
        <td>
            Loại Nhân Viên
        </td>
        <td>
            Tìm theo
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr class="trLabelFilter1">
        <td style="display:none;">
            <radCln:RadDatePicker ID="txtDateFrom" CssClass="datePicker" Width="100px" AllowEmpty="false"
                MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup()" DateFormat="dd/MM/yyyy">
                
                </DateInput>
                <PopupButton Visible="False"></PopupButton>
            </radCln:RadDatePicker>
        </td>
        <td style="display:none;">
            <radCln:RadDatePicker ID="txtDateTo" CssClass="datePicker" Width="100px" AllowEmpty="false"
                MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup1()" DateFormat="dd/MM/yyyy">
                </DateInput>
                <PopupButton Visible="False"></PopupButton>
            </radCln:RadDatePicker>
        </td>
        <td>
            <asp:DropDownList ID="drop_tructhuoc" CssClass="select" Width="120px" 
                runat="server" onselectedindexchanged="drop_tructhuoc_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="--Tất Cả--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Nguyên Kim" Value="1"></asp:ListItem>
                <asp:ListItem Text="Chính Nhân" Value="2"></asp:ListItem>
                  <asp:ListItem Text="Smart Connec" Value="3"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="drop_loainv" CssClass="select" Width="150px" runat="server">
                <asp:ListItem Text="--Tất Cả--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Nhân Viên Chính Thức" Value="1"></asp:ListItem>
                <asp:ListItem Text="Nhân viên thử việc" Value="2"></asp:ListItem>
                 <asp:ListItem Text="Nhân Viên Nghĩ " Value="3"></asp:ListItem>                
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="dropSearchtype" CssClass="select" Width="150px" runat="server"
                OnSelectedIndexChanged="dropSearchtype_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Chọn tìm theo" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Phòng Ban" Value="1"></asp:ListItem>
                <asp:ListItem Text="Tên Nhân Viên" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="dr_dspb" CssClass="select" Width="150px" runat="server" 
                onselectedindexchanged="dr_dspb_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" CssClass="Input_text" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnSearch" CssClass="btn-1" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnExport" Visible="false" CssClass="btn-1" runat="server" Text="Xuất Excell"
                OnClientClick="ExporttoExcel('tblData','Danh_sach_don_hang'); return false;" />
            <asp:Button ID="btn_export" Visible="true" CssClass="btn-1" runat="server" 
                Text="Xuất DS" onclick="btn_export_Click"/>
            <%--<button id="Button2" class="btn-1" onclick="excellexport();">
                Xuất Excell</button>--%>
        </td>
    </tr>
</table>
<hr />
<div>

    <asp:Label ID="lbl_Total_Count" Font-Bold="true" runat="server" Text=""></asp:Label>
    <asp:Button ID="Button4" runat="server" Text="Cập Nhật Danh Sách Nhân Viên" CssClass="btn-1"
        OnClick="btn_refrestData" />
        <input type='button' id='hideshow' class="btn-1" value='Upload DS Nhân Viên' style="cursor:pointer;float:right;"/>
         <div style="float:right;display:none" id="upload_show">
                <asp:FileUpload ID="filesUpload" runat="server"  CssClass="btn-1" />
            <asp:Button ID="btn_upload" runat="server" Text="Upload" OnClick="Click_uploadexcel" CssClass="btn-1" />
            </div>
</div>
<br />
 
<div class="TboardBox" id="tblData">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tbdta">
        <tr>
            <td class="Line2">
            </td>
        </tr>
        <tr>
            <td>
       
                <asp:GridView ID="gvList" runat="server" CssClass="infotable" AutoGenerateColumns="false"
                    AlternatingRowStyle-CssClass="row" RowStyle-CssClass="row-alt" OnRowCommand="gvList_RowCommand1"
                    OnRowDataBound="gvList_RowDataBound" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="checkallid" ItemStyle-CssClass="C" Visible="false">
                            <HeaderTemplate>
                                Check ALL
                                <br />
                                <input id="ckhAll" type="checkbox" onclick="CheckAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id='<%#Eval("IdNhanVien") %>' name="chkChoose" onclick="AddValue(this.id);"
                                    type='checkbox' />
                            </ItemTemplate>
                            <ItemStyle Wrap="True" CssClass="RB_C"></ItemStyle>
                            <HeaderStyle CssClass="RB_C" />
                        </asp:TemplateField>
                    </Columns>
                    
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                H.Ảnh
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a class="fancybox" rel="<%#Eval("IdNhanVien")%>" href='<%#GetUSerImageUrl(Eval("Image"))%>'>
                                    <img src="<%#GetUSerImageUrl(Eval("Image"))%>" width="50px" height="60px" alt="<%#Eval("HoTen")%>" />
                                </a>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L RB_Ls" Width="60px"/>
                            <HeaderStyle CssClass="RB_L RB_Ls" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Mã NV
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("MaNV")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="80px" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                     <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Phòng Ban
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("PhongBan")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="80px" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Chức vụ
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("ViTri")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="80px" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Họ tên
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("HoTen")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="150px"/>
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Giới tính
                            </HeaderTemplate>
                            <ItemTemplate>
                               <%-- <%#string.Format("{0}", Eval("GioiTinh").ToString()=="1" ? "Nam" : "Nữ")%>--%>
                               <%#Eval("GioiTinh") %>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="80px"/>
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Ngày sinh
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("NgaySinh", "{0:dd-MM-yyyy}")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L"/>
                            <ItemStyle CssClass="RB_L" Width="110px"/>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="DenNgay">
                            <HeaderTemplate>
                                số cmnd
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("CMND")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle CssClass="RB_L"  Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="SoNgayNghi">
                            <HeaderTemplate>
                                Nguyên Quán
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("NguyenQuan")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle CssClass="RB_L"  Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                    <%--<Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Đ/c tạm Trú
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("DCTamTru")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="180px" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <%--<Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Đ/c Thường Trú
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("DCThuongTru")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="180px"/>
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>--%>
                   
                    <%--<Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Email
                            </HeaderTemplate>
                            <ItemTemplate>
                                
                                <%#Eval("Email")%>
                                
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="80px"/>
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>--%>

                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                SĐT
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("TrangThaiPhep").ToString()=="0" ? "Red" :(Eval("TrangThaiPhep").ToString()=="2" ? "Orange" : "Green"))%>'>
                                    <%#string.Format("{0}", Eval("TrangThaiPhep").ToString() == "0" ? "Chưa Duyệt" : (Eval("TrangThaiPhep").ToString() == "2" ? "Không Duyệt" : "Đã Duyệt"))%>                                                                             
                                </div>--%>
                               
                                <%#Eval("SoDt")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="80px"/>
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Ghi Chú
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("GhiChuNV")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L"  Width="80px" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>                   
                    
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageUrl="/images/icon/modify.gif" ToolTip="Xem/sửa"
                                    CommandArgument='<%# Eval("IdNhanVien") %>' runat="server" CommandName="capnhat" />
                                &nbsp;
                                <%--<asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("IDMaCV") %>' CommandName="DeleteItem"
                                    OnClientClick="return confirm('Bạn thật sự muốn xóa?')" />--%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="B_C" Width="10px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            
            </td>
        </tr>
        <tr>
            <td class="Line1" height="1">
            </td>
        </tr>
    </table>
</div>
<br />
<div class="panigation" id="pager_div" runat="server">
    <center>
        <div>
            <asp:LinkButton ID="lnkFirst" Style="padding: 1px 3px; margin: 1px; background: #ccc;
                border: solid 1px #666; font: 8pt tahoma;" OnClick="lnkFirst_Click" runat="server">
                    <<
            </asp:LinkButton>
            <asp:LinkButton ID="lnkPrevious" Style="padding: 1px 3px; margin: 1px; background: #ccc;
                border: solid 1px #666; font: 8pt tahoma;" OnClick="lnkPrevious_Click" runat="server">
                    <
            </asp:LinkButton>
            <asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand1"
                OnItemDataBound="RepeaterPaging_ItemDataBound">
                <ItemTemplate>
                    <asp:LinkButton ID="btnPage" Style="padding: 1px 3px; margin: 1px; border: solid 1px #666;
                        font: 8pt tahoma;" CommandName="Page" CommandArgument='<%#Eval("PageIndex") %>'
                        runat="server"><%# Eval("PageText")%>

                    </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
            <asp:LinkButton ID="lnkNext" Style="padding: 1px 3px; margin: 1px; background: #ccc;
                border: solid 1px #666; font: 8pt tahoma;" OnClick="lnkNext_Click" runat="server">
                    >
            </asp:LinkButton>
            <asp:LinkButton ID="lnkLast" Style="padding: 1px 3px; margin: 1px; background: #ccc;
                border: solid 1px #666; font: 8pt tahoma;" OnClick="lnkLast_Click" runat="server">
                    >>
            </asp:LinkButton>
        </div>
    </center>
</div>
<div id="divImageProduct" style="display: none; background-color: Gray; padding: 2px">
    <div style="padding: 3px; background-color: White">
        <asp:Image ID="imgProduct" runat="server" />
    </div>
</div>
<div id="divImageNguoiThan" style="display: none; background-color: Gray; padding: 2px">
    <div style="padding: 3px; background-color: White">
        <asp:Image ID="Image_nguoithan" runat="server" />
    </div>
</div>
<script type="text/javascript" language="javascript">

    function Merce_Cell() {

        var myTable = document.getElementById('<%=gvList.ClientID %>');

        var rows = myTable.getElementsByTagName('tr');
        var numRows = rows.length;
        var numRowSpan = 1;
        var index_merger_col = 1; // Cot thu 2

        var index_merger_col_3 = 2;
        var index_merger_col_4 = 3;
        var index_merger_col_5 = 8;
        //var index_merger_col_6 = 9;


        for (var j = 1; j < (numRows - 1); j++) {
            if (numRowSpan <= 1) {
                var currentRow = myTable.getElementsByTagName('tr')[j];

                var currentCell = currentRow.getElementsByTagName('td')[index_merger_col];
                var currentCellData = currentCell.childNodes[0].data;

                var currentCell_3 = currentRow.getElementsByTagName('td')[index_merger_col_3];
                var currentCell_4 = currentRow.getElementsByTagName('td')[index_merger_col_4];
                var currentCell_5 = currentRow.getElementsByTagName('td')[index_merger_col_5];
                // var currentCell_6 = currentRow.getElementsByTagName('td')[index_merger_col_6];

            }
            if (j < numRows - 1) {
                if (myTable.getElementsByTagName('tr')[j + 1]) {
                    var nextRow = myTable.getElementsByTagName('tr')[j + 1];
                    var nextCell = nextRow.getElementsByTagName('td')[index_merger_col];
                    var nextCellData = nextCell.childNodes[0].data;

                    var nextCell_3 = nextRow.getElementsByTagName('td')[index_merger_col_3];
                    var nextCell_4 = nextRow.getElementsByTagName('td')[index_merger_col_4];
                    var nextCell_5 = nextRow.getElementsByTagName('td')[index_merger_col_5];
                    //  var nextCell_6 = nextRow.getElementsByTagName('td')[index_merger_col_6];

                    // compare the current cell and the next cell             
                    if (currentCellData == nextCellData) {
                        numRowSpan += 1;
                        currentCell.rowSpan = numRowSpan;
                        currentCell_3.rowSpan = numRowSpan;
                        currentCell_4.rowSpan = numRowSpan;
                        currentCell_5.rowSpan = numRowSpan;
                        //   currentCell_6.rowSpan = numRowSpan;

                        nextCell.parentNode.deleteCell(index_merger_col); //nextCell.style.display = 'none';   //disappear the next cell    
                        nextCell_3.parentNode.deleteCell(index_merger_col_3 - 1);
                        nextCell_4.parentNode.deleteCell(index_merger_col_4 - 2);
                        nextCell_5.parentNode.deleteCell(index_merger_col_5 - 3);
                        //   nextCell_6.parentNode.deleteCell(index_merger_col_6 - 4);


                    } else {
                        numRowSpan = 1;
                    }

                }
            }
        }
    }
    //    Merce_Cell();
</script>
<script type="text/javascript" language="javascript">
    function excellexport() {
        //getting values of current time for generating the file name

        var dt = new Date();
        var day = dt.getDate();
        var month = dt.getMonth() + 1;
        var year = dt.getFullYear();
        var hour = dt.getHours();
        var mins = dt.getMinutes();
        var postfix = day + "." + month + "." + year + "_" + hour + "." + mins;
        //creating a temporary HTML link element (they support setting file names)
        var a = document.createElement('a');
        //getting data from our div that contains the HTML table
        //            var data_type = 'data:application/vnd.ms-excel';
        var data_type = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
        var table_div = document.getElementById('tblData');


        var table_html = table_div.outerHTML.replace(/ /g, '%20').replace(/<A[^>]*>|<\/A>/g, "").replace(/<img[^>]*>/gi, "").replace(/<input[^>]*>|<\/input>/gi, "");


        a.href = data_type + ', ' + table_html;
        //setting the file name
        a.download = 'DSDonhang_' + postfix + '.xls';
        //triggering the function
        a.click();
        //just in case, prevent default behaviour
        e.preventDefault();
    }
    function fnExcelReport() {
        var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
        var textRange; var j = 0;
        tab = document.getElementById('tbdta'); // id of table
        for (j = 0; j < tab.rows.length; j++) {
            tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
            //tab_text=tab_text+"</tr>";
        }

        tab_text = tab_text + "</table>";
        tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, ""); //remove if u want links in your table
        tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
        tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
        {
            txtArea1.document.open("txt/html", "replace");
            txtArea1.document.write(tab_text);
            txtArea1.document.close();
            txtArea1.focus();
            sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
        }
        else                 //other browser not tested on IE 11
            sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

        return (sa);
    }

   
</script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
<script src="/js/fancybox/lib/jquery.mousewheel-3.0.6.pack.js" type="text/javascript"></script>
<script src="/js/fancybox/source/jquery.fancybox.js" type="text/javascript"></script>
<link href="/js/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {

        $(".fancybox").fancybox({
            openEffect: 'none',
            closeEffect: 'none',
            width: 600,
            height: 800,
            'hideOnOverlayClick': false,
            'hideOnContentClick': false,
            onStart: function (event, ui) { $(this).parent().appendTo($("form:first")) }
        });

    });
</script>
<!-- Table block 'end'-->
