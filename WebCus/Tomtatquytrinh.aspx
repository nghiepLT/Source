<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Tomtatquytrinh.aspx.cs" Inherits="WebCus.Tomtatquytrinh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="AdminCss/bootstrap.css" rel="stylesheet" />
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
   
    <div class="mydivtable">
         <div class="text-center">
             <h1>MỘT SỐ QUY TRÌNH QUY ĐỊNH HƯỚNG DẪN NHÂN VIÊN MỚI</h1>
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
                    <textarea id="giolamviec" runat="server" rows="3"></textarea>
                </td>
                <td class="text-center">
                   <input type="checkbox" id="chkgiolamviec" runat="server" onclick="ipGiolamviec(this)" />
                </td>
            </tr>
             <tr>
                <td>2
                </td>
                <td>
                    <textarea id="chamcong" runat="server" rows="3"></textarea>
                </td>
                <td class="text-center">
                    <input type="checkbox" id="chkChamcong" runat="server" onclick="ipChamcong(this)" />
                </td>
            </tr>
             <tr>
                <td>3
                </td>
                <td>
                    <textarea id="lamphep" runat="server" rows="3"></textarea>
                </td>
                 <td class="text-center">
                   <input type="checkbox" id="chkLamphep" runat="server" onclick="ipLamphep(this)" />
                 </td>
            </tr>
             <tr>
                <td>4
                </td>
                <td>
                    <textarea id="dirangoai" runat="server" rows="3"></textarea>
                </td>
                <td class="text-center">
                   <input type="checkbox" id="chkDirangoai" runat="server" onclick="ipDirangoai(this)" />

                 </td>
            </tr>
             <tr>
                <td>5
                </td>
                <td>
                    <textarea id="trangphuc" runat="server" rows="3"></textarea>
                </td>
                 <td class="text-center">
                     <input type="checkbox" id="chkTrangphuc" runat="server" onclick="ipTrangphuc(this)" />
                 </td>
            </tr>
             <tr>
                <td>6
                </td>
                 <td >
                    <textarea id="sinhhoat" runat="server" rows="3"></textarea>
                </td>
                <td class="text-center">
                     <input type="checkbox" id="chkSinhhoat" runat="server" onclick="ipSinhhoat(this)" />
                 </td>
            </tr>
             <tr>
                <td>7
                </td>
                 <td>
                    <textarea id="traodoi" runat="server" rows="3"></textarea>
                </td>
                <td class="text-center">
                      <input type="checkbox" id="chkTraodoi" runat="server" onclick="ipTraodoi(this)" />
                 </td>
            </tr>
              <tr>
                <td>8
                </td>
                <td>
                    <textarea id="thoigian" runat="server" rows="3"></textarea>
                </td>
                 <td class="text-center">
                     <input type="checkbox" id="chkThoigian" runat="server" onclick="ipThoigian(this)" />
                  </td>
            </tr>
        </tbody>
    </table>
    
    </div>
    <script src="js/jquery-1.8.2.js"></script>
  <script>
      function ipGiolamviec(dis) {
          var name = $(dis).prop("checked");
          $.ajax({
              type: "POST", //POST
              url: "Tomtatquytrinh.aspx/UDGiolamviec",
              data: "{name:'"+name+"'}",
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
      function ipChamcong(dis) {
          var name = $(dis).prop("checked");
          $.ajax({
              type: "POST", //POST
              url: "Tomtatquytrinh.aspx/UDChamCong",
              data: "{name:'" + name + "'}",
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
      function ipLamphep(dis) {
          var name = $(dis).prop("checked");
          $.ajax({
              type: "POST", //POST
              url: "Tomtatquytrinh.aspx/UDLamphep",
              data: "{name:'" + name + "'}",
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
      function ipDirangoai(dis) {
          var name = $(dis).prop("checked");
          $.ajax({
              type: "POST", //POST
              url: "Tomtatquytrinh.aspx/UDDirangoai",
              data: "{name:'" + name + "'}",
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
      //ipTrangphuc
      function ipTrangphuc(dis) {
          var name = $(dis).prop("checked");
          $.ajax({
              type: "POST", //POST
              url: "Tomtatquytrinh.aspx/UDTrangphuc",
              data: "{name:'" + name + "'}",
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
      //ipSinhhoat
      function ipSinhhoat(dis) {
          var name = $(dis).prop("checked");
          $.ajax({
              type: "POST", //POST
              url: "Tomtatquytrinh.aspx/UDSinhhoat",
              data: "{name:'" + name + "'}",
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
      //ipTraodoi
      function ipTraodoi(dis) {
          var name = $(dis).prop("checked");
          $.ajax({
              type: "POST", //POST
              url: "Tomtatquytrinh.aspx/UDTraodoi",
              data: "{name:'" + name + "'}",
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
      //ipThoigian
      function ipThoigian(dis) {
          var name = $(dis).prop("checked");
          $.ajax({
              type: "POST", //POST
              url: "Tomtatquytrinh.aspx/UDThoigian",
              data: "{name:'" + name + "'}",
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

