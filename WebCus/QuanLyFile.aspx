<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="QuanLyFile.aspx.cs" Inherits="WebCus.QuanLyFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="AdminCss/bootstrap.css" rel="stylesheet" />
    <div class="page-title">
        <h2 class="icon-title">
            <span>Quản lý file</span>
        </h2>
    </div>
    <div>
        <table class="table table-bordered">
            <tr>
                <th>
                    Mẫu
                </th>
                <th>
                    Đường dẫn
                </th>
            </tr>
            <tr>
                <td>
                    1. Phiếu đề nghị Tuyển dụng
                </td>
                <td>
                    <a onclick="importfile()">Phiếu đề nghị Tuyển dụng.doc</a>
                      <input style="display:none;" type="file" class="btn-group btn btn-info btn-xs upload" id="uploadAvatar" accept="application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint,text/plain, application/pdf, image/*" /><br />
                                <input type="hidden" id="Filess" runat="server" />
                </td>
            </tr>
        </table>
    </div>
     <script src="js/jquery-1.8.2.js"></script>
    <script>
        var _URL = window.URL || window.webkitURL;
        $("#uploadAvatar").on('change', function () {
            var file, img;

            if ((file = this.files[0])) {
                sendFile(file);
            }
        });
        function importfile() {
            $("#uploadAvatar").trigger("click");
        }
        function sendFile(file) {
            var formData = new FormData();
            formData.append('file', $('#uploadAvatar')[0].files[0]);
            $.ajax({
                url: "../UploadYCTD.ashx",
                type: "POST",
                data: formData,
                success: function (status) {
                    alert("Import Thành Công!");
                   // $("#ctl00_ContentPlaceHolder1_Filess").val(status);
                },
                processData: false,
                contentType: false,
                error: function () {
                    alert("Whoops something went wrong!");
                }
            });
        }
    </script>
</asp:Content>
