<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="historycheckinout.ascx.cs"
    Inherits="WebCus.historycheckinout" %>
<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script src="../js/select_tag_js/jquery.min.js" type="text/javascript"></script>
<script src="../js/select_tag_js/selectize.js" type="text/javascript"></script>
<script src="../js/select_tag_js/index.js" type="text/javascript"></script>
<link href="../js/select_tag_js/selectize.default.css" rel="stylesheet" type="text/css" />
<link href="/js/datepicker/jquery-ui.css" rel="stylesheet" type="text/css" />
<script src="/js/datepicker/jquery-1.12.4.js" type="text/javascript"></script>
<script src="/js/datepicker/jquery-ui.js" type="text/javascript"></script>
<script src="/js/datepicker/jquery.ui.datepicker-vi-VN.js" type="text/javascript"></script>

<script language="JAVASCRIPT" type="text/javascript">
      
    function ToggleSecondPopup()
    {
       <%= txtDateFrom.ClientID %>.ShowPopup();
    }

    function ToggleSecondPopup1()
    {
        <%= txtDateTo.ClientID %>.ShowPopup();
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
 function isNumberKey(evt)
       {
          var charCode = (evt.which) ? evt.which : evt.keyCode;
          if (charCode != 46 && charCode > 31 
            && (charCode < 48 || charCode > 57))
             return false;

          return true;
       }
</script>
<asp:HiddenField ID="hdn_Transaction_Detail_ID" Value='' runat="server" />
<asp:HiddenField ID="hdn_macv" Value='' runat="server" />
<div class="page-title">
    <h2 class="icon-title">
        <span>Danh sách nhân viên vào ra</span>
    </h2>
</div>
<div class="page-title" style="font-size: 15px;">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</div>
<table cellpadding="0" cellspacing="0" id="tbl_search" runat="server">    
    <tr class="trLabelFilter1">
        <td>
            Từ ngày
        </td>
        <td>
            Đến ngày
        </td>
        <td style="display: none;">
            Trạng Thái
        </td>
        <td style="display: none;">
            Loại Phép
        </td>
        <td >
            Tìm theo
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr class="trLabelFilter1">
        <td>
            
         <%--   <asp:TextBox ID="txt_tungay" runat="server" Width="80px"></asp:TextBox>
            <asp:TextBox ID="txt_denngay" runat="server" Width="80px"></asp:TextBox>

            <script type="text/javascript">

                $(function () {
                    var fromValue = $("[id$=txt_tungay]").datepicker({ dateFormat: 'dd/mm/yy', showButtonPanel: true, defaultDate: new Date(), changeYear: true, yearRange: '1990:2100', changeMonth: true, showWeek: true }).datepicker('setDate',-7);
                    var toValue = $("[id$=txt_denngay]").datepicker({ dateFormat: 'dd/mm/yy', showButtonPanel: true, defaultDate: new Date(), changeYear: true, yearRange: '1990:2100', changeMonth: true, showWeek: true }).datepicker('setDate', new Date());

                  
                });

                $(function () {
                    $("[id$=txt_denngay]").datepicker({ dateFormat: 'dd/mm/yy' });
                    $("[id$=txt_tungay]").datepicker({ dateFormat: 'dd/mm/yy' }).bind("change", function () {
                        var minValue = $(this).val();
                        minValue = $.datepicker.parseDate("dd/mm/yy", minValue);
                       // minValue.setDate(minValue.getDate() + 1);
                        minValue.setDate(minValue.getDate());
                        $("[id$=txt_denngay]").datepicker("option", "minDate", minValue);
                    })
                });

            </script>--%>
            <%--<ajaxToolkit:CalendarExtender DefaultView="Days" ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>--%>
                
                <radCln:RadDatePicker ID="txtDateFrom" Width="100px" runat="server" > 
                
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
        <td style="display: none;">
            <asp:DropDownList ID="drop_trangthai" CssClass="select" Width="120px" runat="server">
                <asp:ListItem Text="Tất Cả" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Chưa Duyệt" Value="0"></asp:ListItem>
                <asp:ListItem Text="Đã Duyệt" Value="1"></asp:ListItem>
                <asp:ListItem Text="Không Duyệt" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="display: none;">
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
        <td id="td_searchname" runat="server" visible="false">
            <asp:DropDownList ID="dropSearchtype" CssClass="select" Width="150px" runat="server"
                OnSelectedIndexChanged="dropSearchtype_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Tất Cả" Value="-1"></asp:ListItem>
                <%--<asp:ListItem Text="Phòng Ban" Value="1"></asp:ListItem>--%>
                <asp:ListItem Text="Tên Nhân Viên" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="dr_dspb" CssClass="select" Width="150px" runat="server">
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
<div id="div_lbl" runat="server">
    <asp:Label ID="lbl_Total_Count" Font-Bold="true" runat="server" Text=""></asp:Label>
    <asp:Button ID="Button4" runat="server" Text="Cập Nhật" CssClass="btn-1"
        OnClick="btn_refrestData" />
        <asp:Label ID="lbt_totalnhanvien" Font-Bold="true" runat="server" Text=""></asp:Label>
    <div id="div_ngaynghithang" runat="server">
        <%--Tổng lượt vào - ra năm <%=DateTime.Now.Year %> :
        <asp:Label ID="lblngaynghi" Style="color: Red;" runat="server"></asp:Label> |--%>
        Trong tháng
        <asp:Label ID="lbl_thang" runat="server"></asp:Label>
        :
        <asp:Label ID="lbl_sngaynghi" Style="color: Red;" runat="server"></asp:Label>
        <br />
        Tổng lượt <asp:Label ID="lbl_tungay" Style="color: blue;" runat="server"></asp:Label> đến <asp:Label ID="lbl_denngay" Style="color: blue;" runat="server"></asp:Label> :    <asp:Label ID="lbl_songaynghitim" Style="color: Red;" runat="server"></asp:Label>
    </div>
</div>
<br />
<div class="TboardBox" id="tblData" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tbdta">
        <tr>
            <td class="Line2">
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvList" runat="server" CssClass="infotable" AutoGenerateColumns="false"
                    AlternatingRowStyle-CssClass="row" RowStyle-CssClass="row-alt"
                    OnRowDataBound="gvList_RowDataBound" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="checkallid" ItemStyle-CssClass="C" Visible="false">
                            <HeaderTemplate>
                                Check ALL
                                <br />
                                <input id="ckhAll" type="checkbox" onclick="CheckAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input id='<%#Eval("IDCheck") %>' name="chkChoose" onclick="AddValue(this.id);" type='checkbox' />
                            </ItemTemplate>
                            <ItemStyle Wrap="True" Width="20px" CssClass="RB_C"></ItemStyle>
                            <HeaderStyle CssClass="RB_C" />
                        </asp:TemplateField>
                    </Columns>                    
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                              Mã Nhân Viên
                            </HeaderTemplate>
                            <ItemTemplate>
                              <span style="color:Blue;">  <%#Eval("BarCodeUser")%></span>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" Width="20px" />
                        </asp:TemplateField>
                    </Columns>
                     <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                              Tên Nhân Viên
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("NameUser")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="RB_L" />
                            <HeaderStyle CssClass="RB_L" Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="TuNgay">
                            <HeaderTemplate>
                                Ngày
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span style="color:Green;"><%#Eval("DateCheck", "{0:dd.MM.yyyy hh:mm:ss tt}")%>  </span>                              
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle Width="30px" CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="DenNgay">
                            <HeaderTemplate>
                                Thời gian
                            </HeaderTemplate>
                            <ItemTemplate>                            
                               <%#Gettime(Eval("TimesOut"), Eval("TimesIn"))%>                                                          
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle Width="80px" CssClass="RB_L" />
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="SoNgayNghi">
                            <HeaderTemplate>
                               Lý do
                            </HeaderTemplate>
                            <ItemTemplate>                                
                                <%#Eval("LyDoCheck")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" />
                            <ItemStyle Width="10px" CssClass="RB_L" />
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
