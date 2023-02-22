<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TTableDuyet.ascx.cs"
    Inherits="WebCus.TTableDuyet" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script src="../js/select_tag_js/jquery.min.js" type="text/javascript"></script>
<script src="../js/select_tag_js/selectize.js" type="text/javascript"></script>
<script src="../js/select_tag_js/index.js" type="text/javascript"></script>
<link href="../js/select_tag_js/selectize.default.css" rel="stylesheet" type="text/css" />

<script language="JAVASCRIPT" type="text/javascript">
  
 function CheckAddValue() {
  
     var Usertype = document.getElementById('<%=ddlmaphep.ClientID %>');
        var idselect = Usertype.options[Usertype.selectedIndex].value;
        if (idselect == "0") {
            alert('Chọn Loại Phép');
            Usertype.focus();
            return false;
        }
       
    }

    function verify() {
        msg = "Bạn chắc chắn muốn xóa không ? thao tác không thể phục hồi !";
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

     function ToggleSecondPopup4()
    {
       <%= rad_ngaynghibuoi.ClientID %>.ShowPopup();
    }
    function ToggleSecondPopup5()
    {
       <%= rad_ngay.ClientID %>.ShowPopup();
    }
     function ToggleSecondPopup2()
    {
       <%= rad_tungay.ClientID %>.ShowPopup();
    }
  function ToggleSecondPopup3()
    {
       <%= rad_denngay.ClientID %>.ShowPopup();
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
  
         var ghichutext = document.getElementById('<%=txt_ghichunghu.ClientID %>');
        if (ghichutext.value == "") {
            alert('Ghi Chú Lý Do Không Duyệt');
            ghichutext.focus();
            return false;
        }
        return true;
       
}

</script>

<asp:HiddenField ID="hdn_Transaction_Detail_ID" Value='' runat="server" />
<asp:HiddenField ID="hdn_macv" Value='' runat="server" />
<div class="page-title">
    <h2 class="icon-title">
        <span>Duyệt ngày phép</span>
       
    </h2>

</div>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="tbn_action" runat="server" style="display:none;">
    <tbody>
        <tr>
            <td class="C">
            </td>
            <td class="R">
                <asp:Button ID="btnInsert" runat="server" Text="Tạo mới " CssClass="btn-1"
                    OnClick="btnInsert_Click" />
                &nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSave_Click"
                    OnClientClick="return CheckValidate();" />
                &nbsp;
              
            </td>
        </tr>
    </tbody>
</table>
<div class="page-title" style="font-size:15px;"> 
 <asp:Literal ID="Literal1" runat="server"></asp:Literal> 

 </div>


<div class="TboardBox" id="tbttkt" runat="server">
 <legend class="page-title" style="font-size:15px;" >Thống kê ngày nghĩ nhân viên : <asp:Label ID="lbltennv" style="color:Red;" runat="server"></asp:Label> </legend>
     <div><span style="width:20%;float:left;">Số ngày nghĩ trong năm <b style="color:Red"><%=DateTime.Now.Year %>:</b></span>  <asp:Label  style="color:Red;font-weight:bold;" ID="lbl_nghinamnv" runat="server"></asp:Label></div>
      <div><span style="width:20%;float:left;">Ngày nghĩ trong <b>tháng <%=DateTime.Now.Month %></b>:</span>  <asp:Label  style="color:Red;font-weight:bold;" ID="lbl_nghithangnv" runat="server"></asp:Label></div>
       <div><span style="width:20%;float:left;">Tổng số lần <b>đi trễ</b> trong <b>tháng <%=DateTime.Now.Month %></b> :</span>  <asp:Label  style="color:Red;font-weight:bold;" ID="lbl_ditrenv" runat="server"></asp:Label></div>
         <div><span style="width:20%;float:left;">Tổng số lần <b>về sớm</b> trong <b>tháng <%=DateTime.Now.Month %></b> :</span>  <asp:Label  style="color:Red;font-weight:bold;" ID="lbl_vesomnv" runat="server"></asp:Label></div>
        <div><span style="width:20%;float:left;">Số lần <b>c.tác ngoài</b> trong <b>tháng <%=DateTime.Now.Month %></b> :</span>  <asp:Label  style="color:Red;font-weight:bold;" ID="lbl_congtacngoainv" runat="server"></asp:Label></div>
        
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="4" class="Line2">
           
            </td>
        </tr>
        <tr id="tr_macv" runat="server">
         <th width="20%" class="RB_L">
                <asp:Label ID="Label2" runat="server" Text="Loại Phép"></asp:Label>
            </th>
            <td class="B_L" width="30%">
                
            
				<asp:DropDownList ID="ddlmaphep" runat="server" CssClass="demo-default" 
                    Width="93%" onselectedindexchanged="ddlmaphep_SelectedIndexChanged" AutoPostBack="true">
                 <asp:ListItem Text="--Chọn Loại Phép--" Value="0"></asp:ListItem>
                 <asp:ListItem Text="Nghỉ Buổi" Value="1"></asp:ListItem>
                 <asp:ListItem Text="Nghỉ Ngày" Value="2"></asp:ListItem>
                 <asp:ListItem Text="Đi Trễ" Value="3"></asp:ListItem>
                 <asp:ListItem Text="Về Sớm" Value="4"></asp:ListItem>
                 <%--<asp:ListItem Text="Quên Quẹt Thẻ" Value="5"></asp:ListItem>--%>
                  <asp:ListItem Text="Công Tác Ngoài" Value="6"></asp:ListItem>
                </asp:DropDownList>
                
            </td>
           <th width="20%" class="RB_L" id="th_buoi" runat="server" >
                <asp:Label ID="Label1" runat="server" Text="Buổi Nghỉ"></asp:Label>
               
            </th>
            <th width="20%" class="RB_L" id="th_songay" runat="server">
                <asp:Label ID="Label6" runat="server" Text="Số Ngày Nghỉ"></asp:Label>
            </th>
            <th width="20%" class="RB_L" id="th_ngay" runat="server" >
                <asp:Label ID="Label3" runat="server" Text="Ngày"></asp:Label>
            </th>
            <td class="B_L" width="30%" id="td_buoi" runat="server" >
            Ngày <radCln:RadDatePicker ID="rad_ngaynghibuoi" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup4()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
            <asp:DropDownList ID="dr_buoinghi" runat="server" CssClass="demo-default" 
                    Width="50%" >
                 <asp:ListItem Text="--Chọn Buổi--" Value="0"></asp:ListItem>
                 <asp:ListItem Text="Buổi Sáng" Value="Buổi Sáng"></asp:ListItem>
                 <asp:ListItem Text="Buổi Chiều" Value="Buổi Chiều"></asp:ListItem>
             <asp:ListItem Text="Cả Ngày" Value="Cả Ngày"></asp:ListItem>
                </asp:DropDownList>
                 
            </td>
            <td class="B_L" width="30%" id="td_Ngaynghi" runat="server">
                <asp:TextBox ID="txt_songaynghi" runat="server" CssClass="Input_text" Width="22%"></asp:TextBox>
                 Từ <radCln:RadDatePicker ID="rad_tungay" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup2()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
                đến
                <radCln:RadDatePicker ID="rad_denngay" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup3()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
            </td>
          <td class="B_L" width="30%" id="td_ngay" runat="server">
          <radCln:RadDatePicker ID="rad_ngay" CssClass="datePicker" Width="100px" AllowEmpty="false"
                    MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                    <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup5()" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <PopupButton Visible="False"></PopupButton>
                </radCln:RadDatePicker>
          </td>
             
        </tr>
        
        <tr id="tr_tenkh" runat="server">
          <th width="20%" class="RB_L">
                <asp:Label ID="Label12" runat="server" Text="Lý Do-Ng.Thay Thế"></asp:Label>
            </th>
            <td class="B_L" width="30%">
                <asp:TextBox ID="txtlydonghi" runat="server" CssClass="Input_text" Width="90%" TextMode="MultiLine"></asp:TextBox>
            </td>
              <th width="20%" class="RB_L">
                <asp:Label ID="Label10" runat="server" Text="Ghi chú or lý do không duyệt"></asp:Label>
            </th>
            <td class="B_L" width="30%">
               <asp:TextBox ID="txt_ghichunghu" runat="server" CssClass="Input_text" Width="90%" TextMode="MultiLine"></asp:TextBox>
            </td>
          
          
        </tr>
        
    </table>
    <fieldset id="div_button_active" runat="server">
    <legend class="page-title" style="font-size:15px;" >Cập nhật trạng thái</legend>
        <table cellpadding="0" cellspacing="0">
            <tr class="trLabelFilter1">
              <td class="pd5">
                    <asp:Button ID="btnxoa" Width="120px" CommandArgument="0" CssClass="btn-1" Visible="false"
                        Text="Xóa" runat="server" OnClick="btnUpdate_Status_Click" OnClientClick="return verify();">
                    </asp:Button>
                </td>
                <td class="pd5">
                    <asp:Button ID="btn_khongduyet" Width="120px" CommandArgument="2" CssClass="btn-1"
                        Text="Không Duyệt" runat="server" OnClick="btnUpdate_Status_Click" OnClientClick="return CheckFinishValue();">
                    </asp:Button>
                </td>
               <td class="pd5">
                    <asp:Button ID="btn_duyetphep" Width="120px" CommandArgument="1" CssClass="btn-1"
                        Text="Duyệt Phép" runat="server" OnClick="btnUpdate_Status_Click">
                    </asp:Button>
                </td>
                
            </tr>
        </table>
    </fieldset>
</div>
<hr />
<div id="tbgrid_data" runat="server">
<table cellpadding="0" cellspacing="0">
    <tr class="trLabelFilter1" style="display: none;">
        <td>
            upload sp
            <asp:FileUpload ID="Upload" runat="server" />
            <asp:Button ID="Button3" runat="server" Text="Upload" OnClick="Click_upload" />
        </td>
    </tr>
    <tr class="trLabelFilter1">
        <td>
            Từ ngày
        </td>
        <td>
            Đến ngày
        </td>
          <td>
            Trạng Thái
        </td>
        <td>
            Loại Phép
        </td>
        <td>
            Tìm theo
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr class="trLabelFilter1">
        
        <td>
            <radCln:RadDatePicker ID="txtDateFrom" CssClass="datePicker" Width="100px" AllowEmpty="false"
                MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup()" DateFormat="dd/MM/yyyy">
                
                </DateInput>
                <PopupButton Visible="False"></PopupButton>
            </radCln:RadDatePicker>
        </td>
        <td>
            <radCln:RadDatePicker ID="txtDateTo" CssClass="datePicker" Width="100px" AllowEmpty="false"
                MinDate="1911-01-01" runat="server" MaxDate="2199-12-16" Calendar-BackColor="#CCCCCC">
                <DateInput DisplayPromptChar="_" PromptChar=" " onclick="ToggleSecondPopup1()" DateFormat="dd/MM/yyyy">
                </DateInput>
                <PopupButton Visible="False"></PopupButton>
            </radCln:RadDatePicker>
        </td>
        <td>
            <asp:DropDownList ID="drop_trangthai" CssClass="select" Width="120px" runat="server">
                <asp:ListItem Text="Tất Cả" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Chưa Duyệt" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Đã Duyệt" Value="1"></asp:ListItem>  
                 <asp:ListItem Text="Không Duyệt" Value="2"></asp:ListItem>                             
            </asp:DropDownList>
        </td>
        <td>
        
            <asp:DropDownList ID="drop_loaipheps" CssClass="select" Width="150px" runat="server">
                <asp:ListItem Text="Tất Cả" Value="-1"></asp:ListItem>
               <asp:ListItem Text="Nghỉ Buổi" Value="1"></asp:ListItem>
                 <asp:ListItem Text="Nghỉ Ngày" Value="2"></asp:ListItem>
                 <asp:ListItem Text="Đi Trễ" Value="3"></asp:ListItem>
                 <asp:ListItem Text="Về Sớm" Value="4"></asp:ListItem>
                 <asp:ListItem Text="Quên Quẹt Thẻ" Value="5"></asp:ListItem>
                  <asp:ListItem Text="Công Tác Ngoài" Value="6"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="dropSearchtype" CssClass="select" Width="150px" runat="server"
                OnSelectedIndexChanged="dropSearchtype_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Tất Cả" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Phòng Ban" Value="1"></asp:ListItem>
                <asp:ListItem Text="Tên Nhân Viên" Value="2"></asp:ListItem>                              
            </asp:DropDownList>
        </td>
        <td>
         <asp:DropDownList ID="dr_cty" CssClass="select" Width="150px" runat="server" Visible="false"
                AutoPostBack="true" onselectedindexchanged="dr_cty_SelectedIndexChanged"> 
                                <asp:ListItem Text="--Chọn Cty--" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Nguyên Kim" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Chính Nhân" Value="2"></asp:ListItem>
                                <asp:ListItem Text="SMC" Value="3"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="dr_dspb" CssClass="select" Width="150px" runat="server" 
                onselectedindexchanged="dr_dspb_SelectedIndexChanged" AutoPostBack="true"> 
                                
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" CssClass="Input_text" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnSearch" CssClass="btn-1" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnExport" Visible="false" CssClass="btn-1" runat="server" Text="Xuất Excell"
                OnClientClick="ExporttoExcel('tblData','Danh_sach_don_hang'); return false;" />
            <button id="Button2" class="btn-1" onclick="excellexport();" style="display: none;">
                Xuất Excell</button>
        </td>
    </tr>
</table>
<hr />
<div>
    <asp:Label ID="lbl_Total_Count" Font-Bold="true" runat="server" Text=""></asp:Label>
    <asp:Button ID="Button4" runat="server" Text="Cập Nhật Danh Sách" CssClass="btn-1"
        OnClick="btn_refrestData" />
       <br />  <asp:Label ID="lbl_satus" runat="server" style="color:Green;font-weight:bold;"></asp:Label>
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
                                <input id='<%#Eval("IDPhep") %>' name="chkChoose" onclick="AddValue(this.id);"
                                    type='checkbox' />
                            </ItemTemplate>
                            <ItemStyle Wrap="True" Width="20px" CssClass="RB_C"></ItemStyle>
                            <HeaderStyle CssClass="RB_C" />
                        </asp:TemplateField>
                    </Columns>
                    <%--<Columns>
                        <asp:TemplateField HeaderText="Key Ưu Tiên">
                            <HeaderTemplate>
                                Ưu Tiên
                                <asp:LinkButton ID="btn_sort_ID" Visible="false" CssClass="Link_Header" runat="server"
                                    CommandName="SortItem" CommandArgument="1-1">
                                           ID Keywork
                                           
                                </asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style='font-weight: bold; color: <%#string.Format("{0}", Eval("UuTienKeyWork").ToString()=="0" ? "Green" : "Red")%>'>
                                    <asp:Label ID="Label4" CssClass="text002" Text='<%#string.Format("{0}", Eval("UuTienKeyWork").ToString()=="0" ? "NO" : "YES")%>'
                                        runat="server"></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle Width="20px" CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>                               
                               Nhân Viên
                            </HeaderTemplate>
                            <ItemTemplate>
                                
                                <%#GetNameNhanVien(Eval("IDNhanVien"))%>
                                
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="TuNgay">
                            <HeaderTemplate>
                                Từ Ngày                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("TuNgay", "{0:dd.MM.yyyy}")%>                               
                                <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("TinhTrangKeyWork").ToString()=="0" ? "Red" : (Eval("TinhTrangKeyWork").ToString()=="1" ? "Blue" : (Eval("TinhTrangKeyWork").ToString()=="2" ? "Green" : "Gray")))%>'>
                                    <asp:Label ID="Label42" CssClass="text002" Text='<%#string.Format("{0}", Eval("TinhTrangKeyWork").ToString()=="0" ? "Chưa Xử Lý" :(Eval("TinhTrangKeyWork").ToString()=="1" ? "Đang Xử Lý" : (Eval("TinhTrangKeyWork").ToString()=="2" ? "Đã Hoàn Thành" : "Hủy")))%>'
                                        runat="server"></asp:Label>                                      
                                </div>--%>                               
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle Width="40px" CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="DenNgay">
                            <HeaderTemplate>
                              
                                Đến Ngày
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                               
                                <%#Eval("DenNgay", "{0:dd.MM.yyyy}")%>
                              
                                <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("TinhTrangKeyWork").ToString()=="0" ? "Red" : (Eval("TinhTrangKeyWork").ToString()=="1" ? "Blue" : (Eval("TinhTrangKeyWork").ToString()=="2" ? "Green" : "Gray")))%>'>
                                    <asp:Label ID="Label42" CssClass="text002" Text='<%#string.Format("{0}", Eval("TinhTrangKeyWork").ToString()=="0" ? "Chưa Xử Lý" :(Eval("TinhTrangKeyWork").ToString()=="1" ? "Đang Xử Lý" : (Eval("TinhTrangKeyWork").ToString()=="2" ? "Đã Hoàn Thành" : "Hủy")))%>'
                                        runat="server"></asp:Label>
                                      
                                </div>--%>
                                
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle Width="40px" CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="SoNgayNghi">
                            <HeaderTemplate>                               
                                Số Ngày Nghỉ
                            </HeaderTemplate>
                            <ItemTemplate>
                               
                                <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("TinhTrangKeyWork").ToString()=="0" ? "Red" : (Eval("TinhTrangKeyWork").ToString()=="1" ? "Blue" : (Eval("TinhTrangKeyWork").ToString()=="2" ? "Green" : "Gray")))%>'>
                                    <asp:Label ID="Label42" CssClass="text002" Text='<%#string.Format("{0}", Eval("TinhTrangKeyWork").ToString()=="0" ? "Chưa Xử Lý" :(Eval("TinhTrangKeyWork").ToString()=="1" ? "Đang Xử Lý" : (Eval("TinhTrangKeyWork").ToString()=="2" ? "Đã Hoàn Thành" : "Hủy")))%>'
                                        runat="server"></asp:Label>                                      
                                </div>--%>
                                 <%#Eval("SoNgayNghi")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle Width="10px" CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Người duyệt or xác nhận                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                
                                <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("TrangThaiPhep").ToString()=="0" ? "Red" : "Green")%>'>
                                    <%#string.Format("{0}", Eval("TrangThaiPhep").ToString() == "0" ? "Chưa Duyệt" : "Đã Duyệt")%>                                                                             
                                </div>--%>
                              <%#GetNameNhanVien(Eval("IDNguoiDuyet"))%> 

                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Trạng Thái                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                 <%#Binstatus(Eval("TrangThaiPhep"), Eval("TrangThaiNhom"), Eval("TrangThaiBoPhan"))%>
                                <%--<div style='font-weight: bold; color: <%#string.Format("{0}", Eval("TrangThaiPhep").ToString()=="0" ? "Red" :(Eval("TrangThaiPhep").ToString()=="2" ? "Orange" : "Green"))%>'>
                                    <%#string.Format("{0}", Eval("TrangThaiPhep").ToString() == "0" ? "Chưa Duyệt" : (Eval("TrangThaiPhep").ToString() == "2" ? "Không Duyệt" : "Đã Duyệt"))%>                                                                             
                                </div>--%>
                               
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" Width="20px" />
                        </asp:TemplateField>
                    </Columns>
                    <%--<Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                               
                               Người Duyệt
                            </HeaderTemplate>
                            <ItemTemplate>
                                
                                
                                <%#Eval("IDNguoiDuyet")%>
                                
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" Width="40px" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                               Loại Phép                               
                            </HeaderTemplate>
                            <ItemTemplate>                                
                               <span style="color:Blue;font-weight:bold"><%#string.Format("{0}", Eval("LoaiPhep").ToString() == "1" ? "Phép Buổi" : (Eval("LoaiPhep").ToString() == "2" ? "Phép Ngày" : (Eval("LoaiPhep").ToString() == "3" ? "Đi Trễ" : (Eval("LoaiPhep").ToString() == "4" ? "Về Sớm" : (Eval("LoaiPhep").ToString() == "5" ? "Quên QThẻ" : "C.Tác Ngoài")))))%></span> 
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" Width="10px" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                              
                                Lý Do Nghỉ
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                             
                                
                                 <%#Eval("LyDoVang")%>
                                
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="TenSP">
                            <HeaderTemplate>
                               Ngày lập Phép
                                <br />
                                Ngày Duyệt Phép
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("NgayLapPhep", "{0:dd-MM-yyyy}")%>
                                <br />
                                <%#Eval("NgayDuyetPhep", "{0:dd-MM-yyyy}")%>
                            </ItemTemplate>
                            <ItemStyle Width="50px" CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <%--<Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Ngày Tạo
                                <br />
                                Ngày Hoàn Thành
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span style="color:Blue;"><%# Eval("NgayTaoKeyWork","{0:dd-MM-yyyy}") %></span>              
                                <br />
                                <span style="color:Red;" ><%# Eval("NgayTaoKeyWork","{0:HH:mm tt}") %></span>                                   
                                <br />
                                <span style="color:Green;" >  <%#string.Format("{0}", Eval("NgayHoanThanhKeyWork", "{0:dd-MM-yyyy HH:mm tt}").ToString() == "01-01-2003 00:00 SA" ? "Chưa Hoàn Thành" : Eval("NgayHoanThanhKeyWork", "{0:dd-MM-yyyy}" + "<br>"+"{0:HH:mm tt}"))%>
                               </span>
                            </ItemTemplate>
                            <ItemStyle Width="80px" CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <Columns>
                        <asp:TemplateField HeaderText="DanhGiaKH">
                            <HeaderTemplate>
                                Ghi Chú Duyệt Phép
                            </HeaderTemplate>
                            <ItemTemplate>
                                   <%#Eval("GhiChu")%>
                            </ItemTemplate>
                            <ItemStyle Width="30px" CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <%--<Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                Người Tạo case
                                <br />
                                Nhân Viên Xử Lý
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label91" CssClass="text002" Text='<%# Eval("NguoiTaoKeyWork") %>'
                                    runat="server"></asp:Label>
                                <br />
                                <hr />
                                <%#Eval("IDKeyWork")%>
                               
                            </ItemTemplate>
                            <ItemStyle Width="60px" CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <%--<Columns>
                        <asp:TemplateField HeaderText="GhiChuKey">
                            <HeaderTemplate>
                                Ghi chú
                            </HeaderTemplate>
                            <ItemTemplate>
                                <img src="Styles/images/note.png" style='display: <%#Eval("GhiChuKeyWork").ToString() !=""? "":"none" %>'
                                    title='<%# Eval("GhiChuKeyWork")%>' width="50px" />
                                <%# Eval("GhiChuKeyWork")%>
                            </ItemTemplate>
                            <ItemStyle Width="50px" CssClass="RB_C" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <%--<Columns>
                        <asp:TemplateField HeaderText="GhiChuKey">
                            <HeaderTemplate>
                                Ghi chú Xử Lý
                            </HeaderTemplate>
                            <ItemTemplate>
                                
                                <%# Eval("GhiChuXuLyKeyWork")%>
                            </ItemTemplate>
                            <ItemStyle Width="50px" CssClass="RB_C" />
                            <HeaderStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageUrl="/images/icon/modify.gif" ToolTip="Xem/sửa"
                                    CommandArgument='<%# Eval("IDPhep") %>' runat="server" CommandName="capnhatphep" />
                                <%--  &nbsp;
                                <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
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
<!-- Table block 'end' -->
