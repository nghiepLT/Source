<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RenderPopupNguoiDanhGiaThuViec.aspx.cs" Inherits="WebCus.RenderPopupNguoiDanhGiaThuViec" %>

<%@ Register Assembly="PQT.Controls" Namespace="PQT.Controls" TagPrefix="pqt" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="AdminCss/bootstrap.css" rel="stylesheet" />
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
    <input id="type" runat="server" style="display:none;" />
    <input id="path" runat="server" style="display:none;" />
    <input id="nguoidanhgia" runat="server" style="display:none;" />
    <input id="gioitinh" runat="server" style="display:none;" />
    <input id="iduser" runat="server" style="display:none;" />
     <input id="idungvien" runat="server" style="display:none;" />
    <div class="container">
        <div style="margin-bottom:10px;">
            <span>Email:</span>
            <input onkeyup="changeemail()" style="border:none;border-bottom:1px solid #e2e2e2;width:50%;outline:none;" type="text" id="email" runat="server" />
           <%-- <input type="text" id="txtEmail" class="Input_text" style="width:99%" onkeyup="autogetname()" runat="server" />--%>
                    <ul id="ulcontents">

                    </ul>
        </div>
        <div>
            <span style="margin-bottom:10px;display:inline-block">Nội dung gửi:</span>
            <textarea rows="4" style="border: 1px solid #eee; pointer-events: all;padding:10px 10px;" id="noidung" runat="server"></textarea>
        </div>
        <div class="text-right" style="margin-top:10px;">
             <asp:button id="btnSaveBanner" runat="server" text="Gửi" cssclass="btn-1 btn_luuthongtin btn btn-sm btn-danger" onclick="btnSaveBanner_Click"
                            onclientclick="return CheckValidBanner();" />
        </div>
    </div>
       <script src="js/jquery-1.8.2.js"></script>
    <script>
        var arraynv = [];
        $(document).ready(function () {
            $.ajax({
                type: "POST", //POST
                url: "ThietLapGuiMail.aspx/Autugetname",
                data: "{key:'" + $("#txtEmail").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    var getsplit = msg.d;
                    var getss = getsplit.split(',');

                    arraynv = [];
                    for (i = 0; i < getss.length; i++) {
                        if (getss[i] != '' && getss[i] != null) {
                            //html += "<li>";
                            //html += "<a>";
                            //html += getss[i];
                            //html += "</a>";
                            //html += "<li>";
                            arraynv.push(getss[i])
                        }

                    }
                    console.log(arraynv);
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });
        function autogetname() {
            $("#ulcontents").show();
            //alert($("#txtEmail").val());
            var html = "";
            if ($("#ctl00_ContentPlaceHolder1_email").val() != "") {
                for (i = 0; i < arraynv.length; i++) {
                    if (arraynv[i].toLowerCase().includes($("#ctl00_ContentPlaceHolder1_email").val().toLowerCase())) {
                        html += "<li>";
                        html += "<a onclick=\"selectitem('" + arraynv[i].split('-')[1] + "')\">";
                        html += arraynv[i];
                        html += "</a>";
                        html += "<li>";
                    }
                }
            }
            $("#ulcontents").html(html);
        }
        function changeemail() {
            autogetname();
            $.ajax({
                type: "POST", //POST
                url: "RenderPopupNguoiDanhGiaThuViec.aspx/changeemail",
                data: "{email:'" + $("#ctl00_ContentPlaceHolder1_email").val() + "',id:'" + $("#ctl00_ContentPlaceHolder1_idungvien").val() + "',type:'"+$("#ctl00_ContentPlaceHolder1_type").val()+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#ctl00_ContentPlaceHolder1_noidung").val(msg.d.split('|')[0]);
                    //ctl00_ContentPlaceHolder1_path
                    $("#ctl00_ContentPlaceHolder1_path").val(msg.d.split('|')[1]); 
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
        function selectitem(value) { 
            $("#ctl00_ContentPlaceHolder1_email").val(value).change();
            $("#ulcontents").hide();
            $.ajax({
                type: "POST", //POST
                url: "RenderPopupNguoiDanhGiaThuViec.aspx/changeemail",
                data: "{email:'" + $("#ctl00_ContentPlaceHolder1_email").val() + "',id:'" + $("#ctl00_ContentPlaceHolder1_idungvien").val() + "',type:'" + $("#ctl00_ContentPlaceHolder1_type").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#ctl00_ContentPlaceHolder1_noidung").val(msg.d.split('|')[0]);
                    //ctl00_ContentPlaceHolder1_path
                    $("#ctl00_ContentPlaceHolder1_path").val(msg.d.split('|')[1]);
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
    </script>
</asp:Content>
