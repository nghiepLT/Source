<%@ Page Title="" Language="C#" MasterPageFile="~/checker.Master" AutoEventWireup="true" CodeBehind="updatechecker.aspx.cs" Inherits="WebCus.updatechecker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
 <link href="/Styles/font-awesome-4.4.0/css/font-awesome.css" rel="stylesheet" type="text/css" />
 <%--<link href="/Styles/materialize.min.css" rel="stylesheet" type="text/css" />--%>
  <link href="/js/autocomplete/jquery-ui.css" rel="stylesheet" type="text/css" />
 <script src="/js/autocomplete/jquery-ui.min.js" type="text/javascript"></script>
 <%--<script src="/Styles/js/materialize.min.js" type="text/javascript"></script>--%>
 
    <style type="text/css">
        .container .wrap {
            width: 100%;
            max-width: 730px;
            padding: 15px;
            margin: 20px auto 50px;
            border: solid #ddd;
            font-size: 14px;
            border-width: 1px 1px 3px;
        }

        .wrap h3 {
            font-weight: 500 !important;
            border-bottom: 2px solid #D7D7D7 !important;
            padding-bottom: 10px;
            text-transform: uppercase;
            background:none !important;
            font-size:24px !important;
        }

        .wrap h4 {
            text-transform: uppercase;
            text-align:center;
        }

        .form-group {
            margin: 20px 0;
            position: relative;
        }

        .form-control {
            background-color: #EDEDED;
            text-indent: 30px;
            font-family: FontAwesome;
            height: 38px;
            padding: 0;
            border: 0;
            border-radius: 3px;
        }

        .btn-new {
            padding: 6px 30px;
            font-size: 17px !important;
            width: 60%;
        }

        .btn_login {
            text-align: center;
        }

        .btn_login a {
            background: #F39300;
            color: #fff;
        }

        .btn_login button:hover {
            color: #fff;
        }

        .fa {
            position: absolute;
            margin-left: 10px;
            margin-top: 10px;
            font-size: 16px;
            color: #b0b0b0;
        }
        .col-xs-1, .col-sm-1, .col-md-1, .col-lg-1, .col-xs-2, .col-sm-2, .col-md-2, .col-lg-2, .col-xs-3, .col-sm-3, .col-md-3, .col-lg-3, .col-xs-4, .col-sm-4, .col-md-4, .col-lg-4, .col-xs-5, .col-sm-5, .col-md-5, .col-lg-5, .col-xs-6, .col-sm-6, .col-md-6, .col-lg-6, .col-xs-7, .col-sm-7, .col-md-7, .col-lg-7, .col-xs-8, .col-sm-8, .col-md-8, .col-lg-8, .col-xs-9, .col-sm-9, .col-md-9, .col-lg-9, .col-xs-10, .col-sm-10, .col-md-10, .col-lg-10, .col-xs-11, .col-sm-11, .col-md-11, .col-lg-11, .col-xs-12, .col-sm-12, .col-md-12, .col-lg-12 {
  position: relative;
  min-height: 1px;
 /* padding-right: 15px;
  padding-left: 15px;*/
  padding-right: 5px;
  padding-left: 5px;
}
h3{margin:auto;}
        .btn-new {margin:5px;}
        .modal {bottom:auto !important;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <script type="text/javascript">
     
      function SetFocus(e) {
          var keynum;
          if (window.event) // IE
              keynum = e.keyCode;
          else if (e.which) // Netscape/Firefox/Opera
              keynum = e.which;
          if (keynum == 13) {
              var obj = document.getElementById('<%=btnLogin.ClientID %>');
              obj.click();
              return false;
          }
      }
      $(document).ready(function () {
          // $('.datepicker').timepicker();
          //          $('.datepicker').timepicker.datepicker(
          //       {
          //           format: 'dd/mm/yyyy',
          //           autoClose: true,
          //           i18n: {
          //               months: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"],
          //               monthsShort: ["Th1", "Th2", "Th3", "Th4", "Th5", "Th6", "Th7", "Th8", "Th9", "Th10", "Th11", "Th12"],
          //               weekdays: ["Chủ Nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7"],
          //               weekdaysShort: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
          //               weekdaysAbbrev: ["CN", "2", "3", "4", "5", "6", "7"],
          //               cancel: 'Cancel',
          //               clear: 'Clear',
          //               today: 'To day',
          //               done: 'OK'
          //           }
          //       }
          //       );

          $("#<%=txtUserID.ClientID %>").autocomplete({
              source: function (request, response) {
                  $.ajax({                      
                      url: '<%=ResolveUrl("~/Service.asmx/GetUser") %>',
                      data: "{ 'prefix': '" + request.term + "'}",
                      dataType: "json",
                      type: "POST",
                      contentType: "application/json; charset=utf-8",
                      success: function (data) {
                          response($.map(data.d, function (item) {
                              return {
                                  label: item.split('-')[0],
                                  val: item.split('-')[1],
                                  iduser: item.split('-')[2]
                              }
                          }))
                      },
                      error: function (response) {
                          alert(response.responseText);
                      },
                      failure: function (response) {
                          alert(response.responseText);
                      }
                  });
              },
              select: function (e, i) {
                  // alert(i.item.val);
                  $("#<%=HiddenField.ClientID %>").val(i.item.label);
                  $("#<%=HiddenField1.ClientID %>").val(i.item.val);
                  $("#<%=HiddenField2.ClientID %>").val(i.item.iduser);
              },
              minLength: 1
          });
      }); 
          
    </script>
     
    <div class="container">
        <div class="wrap">
        <div class="clearfix" style="float: none;" id="div_login" runat="server">
        <h3>            <img src="Images/sys-admin.png" width="50px"/>Xác Thực Quản Lý</h3>
        <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12">
                    <div class="form-group">
                        <span class="fa fa-user "></span>
                        <asp:TextBox ID="txtUsername" placeholder="Nhập tài khoản" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <span class="fa fa-lock "></span>
                        <asp:TextBox ID="txtPass" placeholder="Nhập mật khẩu" CssClass="form-control" TextMode="Password" Text="Password" runat="server">
                        </asp:TextBox>
                    </div>  
                    <div class="form-group" style="text-align:center;">
                        <label></label>
                        <asp:Label ID="lbl_thongbao" runat="server" Font-Bold="true" Text="" Visible="false"></asp:Label>
                    </div>
                    <div class="form-group" style="text-align:center;">
                     <asp:LinkButton ID="LinkButton4" CssClass=" btn-new btn-info" OnClick="btnLogin_Click" runat="server">
                                       Đăng Nhập
                        </asp:LinkButton>
                    </div>       
                 </div>

        </div>
        <div class="clearfix" style="float: none;" id="div_update" runat="server">
                <h3><img src="/Styles/Images/checkbarcode.png" />Cập Nhật Nhân Viên Nghỉ Phép              
                </h3> 
                 <%--<div class="col-md-12 col-lg-12 col-xs-12 col-sm-12" style="position: absolute;top: 22%;">
                   <div class="col-sm-6 form-group"> 
                            <p>
                                <label>
                                    <input  name="check_enter" type="radio" value="1">
                                    <span style="white-space: pre-wrap;">Check mã và xác nhận</span>
                                </label>
                            </p>
                            </div>
                            <div class="col-sm-6 form-group"> 
                            <p>
                                <label>
                                    <input  name="check_enter" type="radio" value="0">
                                    <span style="white-space: pre-wrap;">Xác nhận sau khi quét</span>
                                </label>
                            </p>                          
                        
                        </div>
                        </div>--%>
                 <div class="col-md-4 col-lg-4 col-xs-4 col-sm-4" style="display:none;">
                   <div class="col-sm-6 form-group">
                        <span class="fa fa-clock-o "></span>
                        <asp:TextBox ID="txt_fromtimes" placeholder="Từ" AutoCompleteType="Disabled" autocomplete="off" CssClass="datepicker form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-6 form-group">
                        <span class="fa fa-clock-o"></span>
                        <asp:TextBox ID="txt_totimes" placeholder="Đến" AutoCompleteType="Disabled" autocomplete="off" CssClass="datepicker form-control" runat="server"></asp:TextBox>
                    </div>
                   </div>
                   <div class="col-md-6 col-lg-6 col-xs-6 col-sm-6">
                   <div class="form-group">
                        <span class="fa fa-user "></span>
                        <asp:TextBox ID="txtUserID" placeholder="Tên Nhân Viên" CssClass="form-control" runat="server"></asp:TextBox>
                         <asp:HiddenField ID="HiddenField" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                    </div>
                   </div>    
                    <div class="col-md-6 col-lg-6 col-xs-6 col-sm-6">
                   <div class="form-group">
                        <span class="fa fa-edit "></span>
                        <asp:TextBox ID="txt_lydo" placeholder="Thông tin/Lý do" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                   </div>                    
                       
                <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12">
                    <%--<div class="form-group">
                        <span class="fa fa-user "></span>
                        <asp:TextBox ID="txtUserID" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <span class="fa fa-lock "></span>
                        <asp:TextBox ID="txtPass" CssClass="form-control" TextMode="Password" Text="Password" runat="server">
                        </asp:TextBox>
                    </div>     --%>               
                    <div class="form-group" style="text-align: center;">
                        <asp:LinkButton ID="btnLogin" CssClass=" btn-new btn-danger" oncommand="LinkButton3_Command" CommandName="nghibuoisang" runat="server" OnClientClick="return confirm('Xác nhận thông tin ?')">
                                       Nghỉ Buổi Sáng
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1" CssClass=" btn-new btn-info" oncommand="LinkButton3_Command" CommandName="nghibuoichieu" runat="server" OnClientClick="return confirm('Xác nhận thông tin ?')">
                                       Nghỉ Buổi Chiều
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" CssClass=" btn-new btn-primary" oncommand="LinkButton3_Command" CommandName="nghingay" runat="server" OnClientClick="return confirm('Xác nhận thông tin ?')">
                                       Nghỉ Ngày
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" Visible="false" CssClass=" btn-new btn-success" 
                            CommandName="nghikhac" runat="server" 
                            OnClientClick="return confirm('Xác nhận thông tin ?')" 
                            oncommand="LinkButton3_Command">
                                      Khác
                        </asp:LinkButton>
                      <%--  <asp:LinkButton ID="LinkButton4" CssClass=" btn-new btn-info" OnClick="btnLogin_Click" runat="server">
                                       Option 5
                        </asp:LinkButton>--%>
                    </div>
                     <div class="form-group" style="text-align:center;">
                        <label></label>
                        <asp:Label ID="lblAlert" runat="server" Font-Bold="true" Text="" Visible="false"></asp:Label>
                    </div>
                </div>  
                           
            </div>
        </div>
    </div>
</asp:Content>
