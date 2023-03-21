<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" 
    CodeBehind="TableYeyCauTuyenDung.aspx.cs" Inherits="WebCus.TableYeyCauTuyenDung" %>

<%--<%@ Register Src="~/Payment/TTYeuCauTuyenDung.ascx" TagPrefix="uc1" TagName="TTYeuCauTuyenDung" %>--%>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <%-- <uc1:TTYeuCauTuyenDung runat="server" ID="TTYeuCauTuyenDung" />--%>
<%--<link href="../AdminCss/UngVienTuyenDung.css" rel="stylesheet" />--%>
    <input id="txtIdYeuCau" runat="server" style="display:none;" />
<div class="page-title">
    <h2 class="icon-title">
        <span>Yêu cầu tuyển dụng</span>
    </h2>
</div>
 <div class="blackdoor" onclick="blackdoorclick()">
    </div>
<div class="TboardBox">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tb_button" runat="server" visible="true">
        <tbody>
            <tr>
                <td class="C"></td>
                <td class="R">
                    <asp:Button ID="btnInsertBanner" runat="server" Text="Tạo mới" CssClass="btn-1" OnClick="btnInsertBanner_Click" />
                    &nbsp;
                    <asp:Button ID="btnSaveBanner" runat="server" Text="Lưu" CssClass="btn-1" OnClick="btnSaveBanner_Click"
                        OnClientClick="return CheckValidBanner();" />
                    &nbsp;
                    <asp:Button Visible="false" ID="btnDeleteBanner" runat="server" Text="Xóa" CssClass="btn-1" OnClick="btnDeleteBanner_Click"
                        OnClientClick="return ConfirmDeleteBanner();" />
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tb_input" runat="server" visible="true">
        <tr>
            <td colspan="4" height="5px"></td>
        </tr>
        <tr>
            <td colspan="4" class="Line2"></td>
        </tr>
        <tr>
             <th width="15%" class="RB_L">
                <asp:Label ID="Label6" runat="server" Text="Trực thuộc"></asp:Label>
            </th>
             <td class="RB_L">
                <asp:DropDownList ID="dropTructhuoc" AutoPostBack="true" runat="server" CssClass="select" Width="250px" Style="margin-top: 0px" OnSelectedIndexChanged="dropTructhuoc_SelectedIndexChanged">
                      <asp:ListItem Text="Nguyên kim" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="Chính nhân" Value="2" ></asp:ListItem>
                      <asp:ListItem Text="SMC" Value="3" ></asp:ListItem> 
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label3" runat="server" Text="Phòng ban"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:DropDownList ID="dropPhongban" runat="server" CssClass="select" Width="250px" Style="margin-top: 0px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="lbl_tieude" runat="server" Text="Tiêu đề"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txtTieuDe" CssClass="Input_text" Width="99%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label4" runat="server" Text="Số lượng"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txtSoLuong" CssClass="Input_text" Width="99%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="Label2" runat="server" Text="Import File"></asp:Label>
            </th>
            <td class="RB_L">
                <div>
                    <div>
                        <a id="fileimport" runat="server" style="    background-color: #316ac5;
    color: white;
    padding: 5px 5px;
    display: inline-block;
    margin-bottom: 6px;" href="/Uploads/TuyenDung/FileMau/1.%20Phiếu%20đề%20nghị%20Tuyển%20dụng.doc">Tải file mẫu</a>
                    </div>
                    <div id="upload_show" style="display: none;">
                        <asp:FileUpload ID="filesUpload" runat="server" CssClass="btn-1" OnLoad="filesUpload_Load" />
                        <asp:Button ID="btn_upload" runat="server" Text="Upload" OnClick="Click_uploadexcel" CssClass="btn-1" />
                        <span id="spFile" runat="server">
                            <asp:HyperLink runat="server" CssClass="LinkNauha" Target="_blank" ID="lbFiles"
                                Text='File' NavigateUrl=""></asp:HyperLink></span> 
                    </div>
                    <div>
                        <input type="file" class="btn-group btn btn-info btn-xs upload" id="uploadAvatar" accept="application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint,text/plain, application/pdf, image/*" /><br />
                        <input type="hidden" id="Filess" runat="server" />
                        <a style="background-color: #005399; color: white; padding: 5px 5px; display: inline-block; cursor: pointer; margin: 5px 0px; text-decoration: none;"
                            id="filepath" runat="server"></a>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <th width="15%" class="RB_L">
                <asp:Label ID="lbl_NoiDung" runat="server" Text="Nội dung Email"></asp:Label>
            </th>
            <td class="RB_L">
                <CKEditor:CKEditorControl ID="txtNoiDung" Width='<%#20 %>' Height='<%# 200 %>' Text='<%#GetText() %>' BasePath="Include/ObjCK/ckeditor/" runat="server"></CKEditor:CKEditorControl>
            </td>
        </tr>
        <tr id="trTrangthai" runat="server">
            <th width="15%" class="RB_L">
                <asp:Label ID="Label1" runat="server" Text="Trạng thái"></asp:Label>

            </th>
            <td class="RB_L">
                <asp:DropDownList ID="dropTrangThai" runat="server" CssClass="select" Width="150px" Style="margin-top: 0px">
                    <asp:ListItem Value="1">Chưa duyệt</asp:ListItem>
                    <asp:ListItem Value="2">Duyệt</asp:ListItem>
                    <asp:ListItem Value="3">Hủy</asp:ListItem>
                    <asp:ListItem Value="4">Ẩn</asp:ListItem>
                </asp:DropDownList>
                <input type="checkbox" checked="checked" id="isSend" runat="server"   /> <span>Gửi Email</span>
            </td>

        </tr>
        <tr id="trLydo" runat="server">
            <th width="15%" class="RB_L">
                <asp:Label ID="Label5" runat="server" Text="Lý do"></asp:Label>
            </th>
            <td class="RB_L">
                <asp:TextBox ID="txtLyDo" CssClass="Input_text" Width="99%" runat="server"></asp:TextBox>

            </td>
        </tr>
    </table>

    <hr />
    <table cellpadding="0" cellspacing="0">

        <tr class="trLabelFilter1">
            <td>Trực thuộc
            </td>
            <td>Trạng thái
            </td>
            <td colspan="2">&nbsp;
            </td>
        </tr>

        <tr class="trLabelFilter1">
            <td class="RB_L">
                <asp:DropDownList ID="drop_tructhuoc" CssClass="select" Width="120px"
                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_tructhuoc_SelectedIndexChanged">
                    <asp:ListItem Text="--Tất Cả--" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Nguyên Kim" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Chính Nhân" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Smart Connec" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="RB_L">
                <asp:DropDownList AutoPostBack="true" ID="drop_trangthai" CssClass="select" Width="150px" runat="server" OnSelectedIndexChanged="drop_trangthai_SelectedIndexChanged">
                    <asp:ListItem Text="--Tất Cả--" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Duyệt" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Chưa duyệt" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Hủy" Value="3"></asp:ListItem>
                     <asp:ListItem Text="Đã ẩn" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="RB_L" style="display: none">
                <asp:Button ID="btnSearch" CssClass="btn-1" runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnExport" Visible="false" CssClass="btn-1" runat="server" Text="Xuất Excell"
                OnClientClick="ExporttoExcel('tblData','Danh_sach_don_hang'); return false;" />

                <%--<button id="Button2" class="btn-1" onclick="excellexport();">
                Xuất Excell</button>--%>
            </td>
        </tr>
    </table>
    <hr />
    <br />
       <ul class="tabul">
            <li>
                <a class="achung" runat="server" id="a1" onclick="tabactive(1)">Đang tuyển dụng <span>(</span><span id="rs1" runat="server"></span><span>) </span></a>
            </li>
            <li>
                <a class="achung" id="a2" runat="server" onclick="tabactive(2)">Đã hoàn thành <span>(</span><span id="rs2" runat="server"></span><span>) </span></a>
            </li>
        </ul>
    <table class="text-center" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="Line2"></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvBanner" runat="server" Width="100%" AutoGenerateColumns="False"
                    AlternatingRowStyle-CssClass="row-alt" RowStyle-CssClass="row" CellSpacing="1"
                    CellPadding="0" BorderWidth="0px" OnRowCommand="gvBanner_RowCommand">
                    <AlternatingRowStyle CssClass="row-alt"></AlternatingRowStyle>
                    <Columns>
                       <%-- <asp:TemplateField HeaderText="STT">
                            <ItemTemplate>
                                &nbsp
                                <%# Container.DisplayIndex+1%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="RB_L" Width="70px" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Trạng thái">
                            <ItemTemplate>
                                <%#Eval("TrangThaiTuyenDung")%>
                              <a onclick="tgTTTD(<%#Eval("IdYeuCau")%>)" class="btn-capnhattttd">Cập nhật</a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" Width="200px" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ngày tạo">
                            <ItemTemplate>
                                <%#Eval("NgayTao")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" Width="70px" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                    <%--    <asp:TemplateField HeaderText="Trực thuộc">
                            <ItemTemplate>
                                <%#string.Format("{0}", Eval("TrucThuoc").ToString() == "1" ? "Nguyên Kim" : "Chính Nhân")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Người yêu cầu">
                            <ItemTemplate>
                                <%#Eval("HoTen")%>
                            </ItemTemplate>
                            <ControlStyle Font-Bold="True" />
                            <HeaderStyle CssClass="RB_C" Width="150px" />
                            <ItemStyle CssClass="RB_L colorred" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Phòng ban">
                            <ItemTemplate>
                             <div>
                                 <%--<strong>    <%#string.Format("{0}", Eval("TrucThuoc").ToString() == "1" ? "Nguyên Kim" : "Chính Nhân")%></strong>--%>
                                   <%#checktructhuoc(int.Parse(Eval("TrucThuoc").ToString()))%>
                             </div>
                                <%#Eval("TenPhong")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tiêu đề">
                            <ItemTemplate>
                                <%#Eval("TieuDe")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Số lượng">
                            <ItemTemplate>
                                <%#Eval("Soluong")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File tuyển dụng">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" CssClass="LinkNauha img_doc" Target="_blank" ID="link"
                                    NavigateUrl='<%# string.Format("{0}", Eval("Files")) %>' ImageUrl="~/Images/wordicon.png"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trạng thái">
                            <ItemTemplate>
                                <%-- <%#string.Format("{0}", Eval("TrangThai").ToString() == "1" ? "Chưa duyệt" : "Duyệt")%>--%>
                                <%#checktrangthai(Eval("TrangThai").ToString(),int.Parse(Eval("IdYeuCau").ToString()))%>
                            </ItemTemplate>

                            <HeaderStyle CssClass="RB_C" />
                            <ItemStyle CssClass="RB_L" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ImageUrl="/images/icon/modify.gif" ToolTip="Xem/sửa"
                                    CommandArgument='<%# Eval("IdYeuCau") %>'
                                    runat="server" CommandName="EditItem" />
                                &nbsp;
                                <asp:ImageButton ID="imgDelete" ImageUrl="/images/icon/delete.gif" ToolTip="Xóa"
                                    runat="server" CommandArgument='<%# Eval("IdYeuCau") %>' CommandName="DeleteItem"
                                    OnClientClick="return confirm('Bạn thật sự muốn ẩn yêu cầu tuyển dụng này?')" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="RB_L" HorizontalAlign="center" />
                            <ItemStyle CssClass="B_C" Width="70px" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
            </td>
        </tr>

    </table>
</div>
<div id="mymodal">
    <div class="ttpv_title">
       Trạng thái yêu cầu tuyển dụng  <a onclick="closeMyModal()"><i class="fa fa-close"></i></a>
        <div>
            <span id="htuv"></span>
        </div>
    </div>
    <div class="ttpv_box"> 
        <div class="ttpv_title2">
            <ul>
                <li>
                    <input id="radDangTuyendung" runat="server" type="radio" name="tttd" /> <span>Đang tuyển dụng</span>
                </li>
                <li>
                    <input id="radDaHoanThanh" type="radio" runat="server"  name="tttd"/> <span>Đã hoàn thành</span>
                </li>
            </ul>
        </div>
    </div>
    <div style="padding: 10px 10px;">
        <asp:Button ID="Button1" runat="server" Text="Cập nhật" CssClass="btnduyet" OnClick="btnSaveBanner_Click2"
            />
    </div>
</div>
<style>
     .tabul {
            margin: 0px;
            padding: 0px;
        }

            .tabul li {
                display: inline-block;
                    margin-bottom: 0px;
            }

                .tabul li a {
                    display: inline-block;
                    padding: 3px 5px;
                    text-decoration: none !important;
                    font-weight: bold;
                    border-right: 1px solid #d3d3d3;
                    font-size: 11.4px;
                }

        .tabactive {
            color: white !important;
            color: white;
        }
    .btnduyet{
            display: block;
    width: 100%;
    padding: 5px 5px;
    border: none;
    background-color: #242424;
    color: white;
    text-transform: capitalize;
    }
    #ctl00_MainContent_TTYeuCauTuyenDung_gvBanner td {
        text-align: center !important;
    }

    .colorred {
        font-weight: bold !important;
        color: #0b65a7;
    }
     .blackdoor {
            background-color: #000000ad;
            width: 100%;
            min-height: 100%;
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 999999999;
            display: none;
        }
      #mymodal {
            position: fixed;
            left: 41%;
            top: 12%;
            background-color: white;
            z-index: 999999999;
            width: 19%;
            box-shadow: 1px 2px 3px 1px #161616;
        }
      .ttpv_title {
            background-color: #005399;
            padding: 5px 10px;
            color: white;
            font-weight: bold;
            text-align: center;
            border-radius: 3px 3px 0px 0px;
        }

        .ttpv_box {
            padding: 10px 20px;
        }
        .ttpv_title a {
            color: white;
            position: absolute;
            right: 10px;
            font-size: 14px;
            top: 8px;
        }
        .ttpv_title2{

        }
        .ttpv_title2 ul{
            margin:0px;
            padding:0px;
        }
        .ttpv_title2 li{
            display:block;
        }
        a.btn-capnhattttd {
    background-color: #005399;
    color: white;
    padding: 5px 5px;
    display: inline-block;
    margin: 5px 0px;
    text-align: center;
    font-size: 12px;
    text-decoration:none;
}
</style>
<script src="js/jquery-1.8.2.js"></script>

<script>
    var _URL = window.URL || window.webkitURL;
    $("#uploadAvatar").on('change', function () {
        var file, img;

        if ((file = this.files[0])) {
            sendFile(file);
        }
    });
    $(document).ready(function () {
        if ($("#ctl00_MainContent_filepath").html() == '') {
            $("#ctl00_MainContent_filepath").hide();
        }
        $("#mymodal").hide();
    });
    function sendFile(file) {
        var formData = new FormData();
        formData.append('file', $('#uploadAvatar')[0].files[0]);
        $.ajax({
            url: "../ImportYCTD.ashx",
            type: "POST",
            data: formData,
            success: function (status) {
                $("#ctl00_MainContent_filepath").show();
                $("#ctl00_MainContent_filepath").html(status);
                $("#ctl00_MainContent_filepath").attr("href", "/Uploads/TuyenDung/" + status);
                $("#ctl00_MainContent_Filess").val("/Uploads/TuyenDung/" + status);
            },
            processData: false,
            contentType: false,
            error: function () {
                alert("Whoops something went wrong!");
            }
        });
    }

    function tgTTTD(id) {
        $(".blackdoor").show();
        // WindowOpen('RenderPopupCapNhatTrangThai.aspx?id=' + id, 'POpi', 320, 120, 'no');
        $("#mymodal").fadeIn(100);
        //
        $("#ctl00_MainContent_txtIdYeuCau").val(id);
        $.ajax({
            type: "POST", //POST
            url: "TableYeyCauTuyenDung.aspx/GetStatus",
            data: "{IdYeuCau:'" + id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == 0 ) {
                    $("#ctl00_MainContent_radDangTuyendung").prop("checked", true);
                }
                else {
                    $("#ctl00_MainContent_radDaHoanThanh").prop("checked", true);
                }
            },
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }

    function blackdoorclick() {
        $("#mymodal").hide();
        $(".blackdoor").hide();
    }

    function tabactive(id) {
        var url = window.location.origin + window.location.pathname;
        window.location.href = url + "?type=" + id;
    }
</script>
    <script>
    function CheckValidBanner() {
        if ($("#ctl00_MainContent_txtTieuDe").val() == '') {
            alert("Vui lòng nhập tiêu đề");
            $("#ctl00_MainContent_txtTieuDe").focus();
            return false;
        }

        if ($("#ctl00_MainContent_filepath").html() == null || $("#ctl00_MainContent_filepath").html() == '') {
            alert("Vui lòng chọn file");
            return false;
        }

    }
</script>

</asp:Content>
