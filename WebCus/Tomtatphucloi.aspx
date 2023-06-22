<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Tomtatphucloi.aspx.cs" Inherits="WebCus.Chedo_phucloi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="AdminCss/bootstrap.css" rel="stylesheet" />
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
    <input id="userid" runat="server" style="display:none;"  />
    <div class="mydivtable">
         <div class="text-center">
             <h1>TÓM TẮT MỘT SỐ CHẾ ĐỘ PHÚC LỢI THÔNG TIN NHÂN VIÊN</h1>
         </div>
         <table class="table table-bordered">
        <thead>
            <tr>
                <th>STT
                </th>
                <th style="width:70%">NỘI DUNG TÓM TẮT
                </th>
                <th class="text-center">XÁC NHẬN
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>1
                </td>
                <td>
                    <textarea id="chedobaohiem" runat="server" rows="3"></textarea>
                </td>
                <td class="text-center">
                   <input type="checkbox" id="chkchedobaohiem" runat="server" onclick="ipchedobaohiem(this,1)" />
                </td>
            </tr>
             <tr>
                <td>2
                </td>
                <td>
                    <textarea id="chinhsachthamnien" runat="server" rows="3"></textarea>
                </td>
                <td class="text-center">
                    <input type="checkbox" id="chkchinhsachthamnien" runat="server" onclick="ipchedobaohiem(this,2)" />
                </td>
            </tr>
             <tr>
                <td>3
                </td>
                <td>
                    <textarea id="chedothuong" runat="server" rows="3"></textarea>
                </td>
                 <td class="text-center">
                   <input type="checkbox" id="chkchedothuong" runat="server" onclick="ipchedobaohiem(this,3)" />
                 </td>
            </tr>
             <tr>
                <td>4
                </td>
                <td>
                    <textarea id="phucloi" runat="server" rows="3"></textarea>
                </td>
                <td class="text-center">
                   <input type="checkbox" id="chkphucloi" runat="server" onclick="ipchedobaohiem(this,4)" />

                 </td>
            </tr>
        </tbody>
    </table>
    
    </div>
    <script src="js/jquery-1.8.2.js"></script>
  <script>
      function ipchedobaohiem(dis,status) {
          var name = $(dis).prop("checked");
          var userid = $("#ctl00_MainContent_userid").val();
          $.ajax({
              type: "POST", //POST
              url: "Tomtatphucloi.aspx/UDChedobaohiem",
              data: "{name:'" + name + "',status:'" + status + "',userid:'"+userid+"'}",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
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
      
  </script>

</asp:Content>

