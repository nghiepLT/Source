<%@ Page Title="" Language="C#" MasterPageFile="~/checker.Master" AutoEventWireup="true" CodeBehind="inoutchecker.aspx.cs" Inherits="WebCus.inoutchecker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
 <link href="/Styles/font-awesome-4.4.0/css/font-awesome.css" rel="stylesheet" type="text/css" />
 <link href="/Styles/materialize.min.css" rel="stylesheet" type="text/css" />
 <script src="/Styles/js/materialize.min.js" type="text/javascript"></script>
    <style type="text/css">
        .container .wrap {
            width: 100%;
            max-width: 730px; 
            /*max-width: 1030px; */
            padding: 15px;
            margin: 20px auto 50px;
            border: solid #ddd;
            font-size: 14px;
            border-width: 1px 1px 3px;
        }

        .wrap h3 {
            font-weight: bold !important;
            border-bottom: 2px solid #D7D7D7 !important;
            padding-bottom: 10px;
            text-transform: uppercase;
            background:none !important;
            font-size:24px !important;
        }

        .wrap h4 {
            text-transform: uppercase;
            text-align:center;
            font-size: x-large;    
    font-weight: bold;
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
            padding:6px 10px;
            font-size: 15px !important;
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
h3{margin:auto;
       font-weight: bold !important;
    text-shadow: 1px 3px #CD5;
   }
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
          // $("#img").hide();          
          $('.datepicker').timepicker();
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
          //      
          setInterval(function () {
              var stig = "color:#" + Math.floor(Math.random() * 4096).toString(16) + ";";
              $("h3").attr('style', stig);
              //http://192.168.245.245/cgi-bin/snapshot.cgi?user=admin&password=Nkcn@2020@@
              //var stringimg = "http://192.168.245.245/cgi-bin/snapshot.cgi?channel=" + Math.floor(Math.random() * (5 - 0 + 1)) + 0;
              var linkcam = '<%=linkcam %>';
              var stringimg = linkcam + "?" + Math.floor(Math.random() * (5 - 0 + 1)) + 0;
              // var stringimg = "http://192.168.245.245";   
              //              $("#img").show();
              // UrlExists(stringimg, function (status) {
              //  if (status === 200) {
              //      $("#img").attr("src", stringimg);
              //  }
              //  else if (status === 500) {
              //      alert('Ip camera not found !');
              //  }
              // });
              if (linkcam == "0")
                  $("#lblip").text('Ip camera not found');
              else
                  $("#img").attr("src", stringimg);
          }, 5000);
          function UrlExists(url, cb) {
              jQuery.ajax({
                  url: url,
                  dataType: 'text',
                  type: 'GET',
                  complete: function (xhr) {
                      if (typeof cb === 'function')
                          cb.apply(this, [xhr.status]);
                  },
                  error: function (XMLHttpRequest, textStatus, errorThrown) {
                      if (XMLHttpRequest.readyState == 4) {
                          // HTTP error (can be checked by XMLHttpRequest.status and XMLHttpRequest.statusText)
                          // alert("error");
                          $("#lblip").text('Ip camera not found');
                      }
                      else if (XMLHttpRequest.readyState == 0) {
                          // Network error (i.e. connection refused, access denied due to CORS, etc.)
                          // alert("error!");
                          $("#lblip").text('Ip camera not found');
                      }
                      else {
                          // something weird is happening
                          alert("error!!");
                      }
                  }
              });
          }
      });      
    </script>
    <div class="container">
        <div class="wrap">
            <div class="clearfix" style="float: none;">
                <h3><img alt="QuetBarcode" src="/Styles/Images/checkbarcode.png" />Quét Mã Nhân Viên - Vào - Ra               
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
                   
                   <div class="col-md-9 col-lg-9 col-xs-9 col-sm-9" style = "border-right: 1px solid #c9c9cc;">
                   <h4 style="color: Green;">Quét Ra</h4>
                    <div class="col-md-6 col-lg-6 col-xs-6 col-sm-6">
                   <div class="form-group">
                        <span class="fa fa-edit "></span>
                        <asp:TextBox ID="txt_lydo" placeholder="Thông tin/Lý do" AutoCompleteType="Disabled" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                   </div>                    
                    <div class="col-md-6 col-lg-6 col-xs-6 col-sm-6">
                   <div class="form-group">
                        <span class="fa fa-user "></span>
                        <asp:TextBox ID="txtUserID" placeholder="ID/Mã nhân viên" AutoCompleteType="Disabled" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                   </div>  
                   <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12">
                   <div class="form-group" style="text-align: center;">
                        <asp:LinkButton ID="btnLogin" CssClass=" btn-new btn-danger" oncommand="LinkButton3_Command" CommandName="gapkhachhang" runat="server" OnClientClick="return confirm('Xác nhận thông tin ?')">
                                       Gặp Khách Hàng
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1" CssClass=" btn-new btn-info" oncommand="LinkButton3_Command" CommandName="gapdoitac" runat="server" OnClientClick="return confirm('Xác nhận thông tin ?')">
                                       Gặp Đối Tác
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" CssClass=" btn-new btn-primary" oncommand="LinkButton3_Command" CommandName="giaovanthu" runat="server" OnClientClick="return confirm('Xác nhận thông tin ?')">
                                       Giao Văn Thư
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" CssClass=" btn-new btn-success" 
                            CommandName="khac" runat="server" 
                            OnClientClick="return confirm('Xác nhận thông tin ?')" 
                            oncommand="LinkButton3_Command">
                                      Khác
                        </asp:LinkButton>                      
                    </div>
                    <div class="form-group" style="text-align:center;">
                        <label></label>
                        <asp:Label ID="lblAlert" runat="server" Font-Bold="true" Text="" Visible="false"></asp:Label>
                    </div>
                     </div>
                   </div>
                   <div class="col-md-3 col-lg-3 col-xs-3 col-sm-3">    
                     <h4 style="color: Red;">Quét vào</h4>     
                      <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12">          
                   <div class="form-group">
                        <span class="fa fa-user "></span>
                        <asp:TextBox ID="txt_idcheckvao" ClientIDMode="Static" placeholder="ID/Mã nhân viên" AutoCompleteType="Disabled" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                   <script type="text/javascript" language="javascript">                      
                       var wage = document.getElementById("txt_idcheckvao");
                        wage.addEventListener("keydown", function (e) {
                            if (e.keyCode === 13) {
                                var btn = document.getElementById('<%=LinkButton4.ClientID%>');
                              // alert("enter");
                               btn.click();
                           }
                       });    
                   </script>
                    </div>  
                    </div> 
                    <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12">   
                    <div class="" style="text-align:center;">   
                   <asp:LinkButton ID="LinkButton4"  CssClass=" btn-new btn-primary" style="z-index:-9999;position:absolute;" runat="server" onclick="btn_takepicture_Click">
                                                                
                        </asp:LinkButton>
                    <img alt="camip" id="img"  Width="150" src="Styles/Images/giphy.gif" />  
                       
                   <%--<asp:Image ID="img" runat="server" ClientIDMode="Static" Width="150" ImageUrl = "http://192.168.117.208/cgi-bin/snapshot.cgi"/>--%>
                  <label id="lblip"></label>
                  </div>                   
                    </div>             
                   </div>                                 
                           
            </div>
        </div>
    </div>
</asp:Content>
