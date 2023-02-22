<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ThongTinUngVienDuTuyen.aspx.cs"
    Inherits="WebCus.ThongTinUngVienDuTuyen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="AdminCss/bootstrap.css" rel="stylesheet" />
    <link href="AdminCss/UngVienTuyenDung.css" rel="stylesheet" />
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
                <%-- <div class="menuleft">
                <ul>
                <li>
                    <a>
                       1. THÔNG TIN CÁ NHÂN:
                    </a>
                </li>
                <li>
                    <a>
                      2. GIA ĐÌNH:
                    </a>
                </li>
                 <li>
                    <a>
                     3. TÊN VÀ SỐ ĐT CỦA NGƯỜI LIÊN HỆ TRƯỜNG HỢP KHẨN CẤP
                    </a>
                </li>
                 <li>
                    <a>
                     4. THÔNG TIN KHÁC

                    </a>
                </li>
                  <li>
                    <a>
                   5. QUÁ TRÌNH ĐÀO TẠO (Chỉ ghi văn bằng cao nhất)
                    </a>
                </li>
                 <li>
                    <a>
                   6. QUÁ TRÌNH LÀM VIỆC (Nêu 2 nơi làm sau cùng - theo trình tự gần nhất)

                    </a>
                </li>
            </ul>
          </div>--%>
            </div>
            <div class="col-sm-8">
                <div class="full_box">
                    <div class="headertitle_box">
                        <div class="headertitle_box_left">
                            <img src="Images/logo_form.png" class="img-responsive" />
                        </div>
                        <div class="headertitle_box_right">
                            <div class="headertitle_box_right_title">
                                THÔNG TIN ỨNG VIÊN DỰ TUYỂN
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="chungtoicamket">
                        <div class="chungtoicamket_img">

                            <input type="file" class="hidden" />
                        </div>
                        <div class="chungtoicamket_title">
                            (Chúng tôi cam kết
thông tin ứng viên cung cấp sẽ được bảo mật)

                        </div>
                    </div>

                    <div class="table_1">
                        <table class="table">
                            <tr>
                                <td class="tdgray">Ứng cử vào vị trí
                                </td>
                                <td class="tdgray">Nguồn tin tuyển dụng
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="vitriungtuyen" runat="server" required />
                                </td>
                                <td rowspan="3">
                                    <ul>
                                        <li>
                                            <span class="sp1">
                                                <input style="pointer-events: none;" id="chkInternet" runat="server" class="cb" type="checkbox" data-type="1" />
                                                Mạng Internet/Báo: </span>
                                            <input onkeyup="internetup()" class="ip dotted" id="lcinternet" runat="server" />
                                        </li>
                                        <li>
                                            <span class="sp1">
                                                <input style="pointer-events: none;" id="chkdvvk" runat="server" class="cb" type="checkbox" data-type="2" />
                                                DVVL : </span>
                                            <input onkeyup="dvvlup()" class="ip dotted" id="lcdvvl" runat="server" />
                                        </li>
                                        <li>
                                            <span class="sp1">
                                                <input style="pointer-events: none;" id="chkgioitb" runat="server" class="cb" type="checkbox" data-type="3" />
                                                Giới thiệu bởi : </span>
                                            <input onkeyup="gioitbup()" class="ip dotted" id="lcgioithieuboi" runat="server" />
                                        </li>
                                        <li>
                                            <span class="sp1">
                                                <input style="pointer-events: none;" id="chkkhach" runat="server" class="cb" type="checkbox" data-type="4" />
                                                Khác : </span>
                                            <input onkeyup="khacup()" class="ip dotted" id="lckhac" runat="server" />
                                        </li>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdgray">Vị trí mong muốn khác
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="vitrimongmuon" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdgray">
                                    <div class="title_table">
                                        Thời gian có thể bắt đầu làm việc kể từ khi được thư mời nhận việc
                                    </div> 
                                </td>
                                <td class="tdgray">
                                    <div class="title_table">
                                        Mức lương đề nghị cho vị trí dự tuyển (VNĐ - Net/ Gross)
                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <input class="ips" id="thoigianbatdau" runat="server" required iptype="date" />
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <input class="ips currency" id="mucluongdenghi" runat="server" required />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="ttcn_title">
                        1. THÔNG TIN CÁ NHÂN:
                    </div>

                    <div class="table_1">
                        <table class="table">
                            <tr>
                                <td colspan="2" class="tdgray">Họ và tên ứng viên
                                </td>
                                <td colspan="2" class="tdgray">Ngày/Tháng/Năm sinh
                                </td>
                                <td class="tdgray">Nơi sinh
                                </td>
                                <td class="tdgray">Giới tính
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input class="ips" id="HoTen" runat="server" required />
                                </td>
                                <td colspan="2">
                                    <input class="ips datej" iptype="date" id="NgaySinh" runat="server" required />
                                </td>
                                <td>
                                    <input class="ips" id="NoiSinh" runat="server" required />
                                </td>
                                <td>
                                    <input class="ips" id="GioiTinh" runat="server" required />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tdgray">Địa chỉ e-mail
                                </td>
                                <td colspan="2" class="tdgray">ĐT di động 
                                </td>
                                <td class="tdgray">ĐT cơ quan	
                                </td>
                                <td class="tdgray">ĐT nhà
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input class="ips" id="Email" runat="server" required />
                                </td>
                                <td colspan="2">
                                    <input class="ips" id="SoDt" runat="server" required />
                                </td>
                                <td>
                                    <input class="ips" />
                                </td>
                                <td>
                                    <input class="ips" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdgray">Dân tộc
                                </td>
                                <td class="tdgray">Tôn giáo
                                </td>
                                <td class="tdgray" colspan="2">Số CMND
                                </td>
                                <td class="tdgray">Ngày cấp
                                </td>
                                <td class="tdgray">Nơi cấp
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="Dantoc" runat="server" />
                                </td>
                                <td>
                                    <input class="ips" id="Tongiao" runat="server" />
                                </td>
                                <td colspan="2">
                                    <input class="ips" id="CMND" runat="server" required />
                                </td>
                                <td>
                                    <input class="ips " id="NgayCMND"  runat="server" required />
                                </td>
                                <td>
                                    <input class="ips" id="NoiCapCMND" runat="server" required />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="tdgray">Địa chỉ đang cư ngụ/ địa chỉ liên lạc
                                </td>
                                <td colspan="3" class="tdgray">Địa chỉ thường trú
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <input class="ips" id="DCTamTru" runat="server" />
                                </td>
                                <td colspan="3">
                                    <input class="ips" id="DCThuongTru" runat="server" />
                                </td>
                            </tr>

                        </table>
                    </div>

                    <div class="ttcn_title">
                        2. GIA ĐÌNH:
                    </div>

                    <div class="table_1">
                        <table class="table">
                            <tr>
                                <td>Tình trạng hôn nhân 
                                </td>
                                <td>
                                    <input type="radio" name="radHonnhan" id="radLapGiaDinh" runat="server" />
                                    Lập gia đình
                                </td>
                                <td>
                                    <input type="radio" name="radHonnhan" id="radDocThan" runat="server" />
                                    Độc thân
                                </td>
                            </tr>
                            <tr>
                                <td class="tdgray">Họ tên vợ/ chồng
                                </td>
                                <td class="tdgray">Năm sinh
                                </td>
                                <td class="tdgray">Công tác tại
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="hotenvochong" runat="server" />
                                </td>
                                <td>
                                    <input type="text" class="ips" id="namsinhvochong" runat="server"  />
                                </td>
                                <td>
                                    <input class="ips" id="congtactai" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdgray">Họ tên các con
                                </td>
                                <td class="tdgray">Năm sinh
                                </td>
                                <td class="tdgray">Học tại trường
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="hotencon1" runat="server" />
                                </td>
                                <td>
                                    <input type="text" class="ips" id="namsinhcon1" runat="server"  />
                                </td>
                                <td>
                                    <input class="ips" id="hoctaitruong1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="hotencon2" runat="server" />
                                </td>
                                <td>
                                    <input type="text" class="ips" id="namsinhcon2" runat="server"  />
                                </td>
                                <td>
                                    <input class="ips" id="hoctaitruong2" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="hotencon3" runat="server" />
                                </td>
                                <td>
                                    <input type="text" class="ips" id="namsinhcon3" runat="server"  />
                                </td>
                                <td>
                                    <input class="ips" id="hoctaitruong3" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="ttcn_title">
                        3. TÊN VÀ SỐ ĐT CỦA NGƯỜI LIÊN HỆ TRƯỜNG HỢP KHẨN CẤP
                    </div>
                    <div class="table_1">
                        <table class="table">
                            <tr>
                                <td class="tdgray">Họ và tên
                                </td>
                                <td class="tdgray">Mối quan hệ
                                </td>
                                <td class="tdgray">ĐT liên lạc
                                </td>
                                <td class="tdgray">Địa chỉ cư ngụ
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="tennlh" runat="server" />
                                </td>
                                <td>
                                    <input class="ips" id="moiqh" runat="server" />
                                </td>
                                <td>
                                    <input class="ips" id="dtnlh" runat="server" />
                                </td>
                                <td>
                                    <input class="ips" id="diachinlh" runat="server" />
                                </td>
                            </tr>
                        </table>

                    </div>

                    <div class="ttcn_title">
                        4. THÔNG TIN KHÁC
                    </div>

                    <div class="table_1">
                        <table class="table">
                            <tr>
                                <td class="tdgray">Bạn từng bị thương tật, giải phẫu hay tai nạn không? (Chi tiết nếu có)
                                </td>
                                <td>
                                    <input type="radio" name="radThuongTat" />
                                    <span>Có</span>
                                </td>
                                <td>
                                    <input type="radio" checked="checked" name="radThuongTat" />
                                    <span>Không</span>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="ttcn_title">
                        5. QUÁ TRÌNH ĐÀO TẠO (Chỉ ghi văn bằng cao nhất)  
                    </div>
                    <div class="table_1">
                        <table class="table">
                            <tr>
                                <td colspan="4" rowspan="2">
                                    <asp:radiobuttonlist id="RadioButtonList1"  runat="server" height="54px"  width="325px" repeatcolumns="3" autopostback="True" onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" Visible="False">
                                    <asp:ListItem Value="PTTH">PTTH</asp:ListItem>
                                    <asp:ListItem>Trung cấp</asp:ListItem>
                                    <asp:ListItem>Cao đẳng</asp:ListItem>
                                    <asp:ListItem>Đại học</asp:ListItem>
                                    <asp:ListItem>Cao học</asp:ListItem>
                               <asp:ListItem>Tiến sĩ</asp:ListItem>
                                </asp:radiobuttonlist>
                                   <div class="quatrinhdaotaobox">
                                       <ul>
                                           <li>
                                               <span>PTTH</span>
                                               <input id="radVB1" name="qtdt1" type="radio" onclick="choosevanbang('PTTH')" />
                                           </li>
                                           <li>
                                               <span>Cao đẳng</span>
                                               <input id="radVB2" name="qtdt1" type="radio" onclick="choosevanbang('Cao đẳng')" />
                                           </li>
                                           <li>
                                               <span>Cao học</span>
                                               <input id="radVB3" name="qtdt1" type="radio" onclick="choosevanbang('Cao học')"/>
                                           </li>
                                       </ul>
                                       <ul>
                                           <li>
                                               <span>Trung cấp</span>
                                               <input id="radVB4" name="qtdt1" type="radio" onclick="choosevanbang('Trung cấp')"/>
                                           </li>
                                           <li>
                                               <span>Đại học</span>
                                               <input id="radVB5" name="qtdt1" type="radio" onclick="choosevanbang('Đại học')"/>
                                           </li>
                                           <li>
                                               <span>Tiến sĩ</span>
                                               <input id="radVB6" name="qtdt1" type="radio" onclick="choosevanbang('Tiến sĩ')"/>
                                           </li>
                                       </ul>
                                   </div>
                                    <div style="margin-top: 10px;">
                                        <strong class="sp1" style="width: 20%">Trường học:   </strong>
                                        <input class="ip dotted" style="width: 75%" id="tentruong" runat="server" />
                                    </div>
                                </td>
                                <td class="tdgray">Năm tốt nghiệp</td>
                                <td class="tdgray">Văn bằng</td>
                            </tr>
                            <tr>
                                <td style="width: 20%; height: 77px;">
                                    <input class="ips" id="namtotnghiep" runat="server"  /></td>
                                <td style="width: 20%; height: 77px;">
                                    <input class="ips" id="vanbang" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width: 50%">
                                    <strong class="sp1" style="width: 20%">Ngành học:   </strong>
                                    <input class="ip dotted" style="width: 75%" id="nganhhoc" runat="server">
                                </td> 
                                <td colspan="3">
                                    <strong class="sp1" style="width: 20%">Xếp loại:   </strong>
                                    <input class="ip dotted" style="width: 75%" id="xeploai" runat="server">
                                </td>
                            </tr>
                            <tr>
                                <td class="tdgray">Văn bằng/chứng chỉ khác
                                </td>
                                <td class="tdgray">Ngành học</td>
                                <td class="tdgray">Thời gian đào tạo</td>
                                <td class="tdgray">Năm tốt nghiệp</td>
                                <td class="tdgray">Xếp loại</td>
                                <td class="tdgray">Nơi cấp</td>
                            </tr>

                            <tr>
                                <td>
                                    <input class="ips" id="vpcc1" runat="server" />
                                </td>
                                <td>
                                    <input class="ips" id="nganhhoc1" runat="server" />
                                </td>
                                <td>
                                    <input class="ips " id="thoigiandaotao1" runat="server"  />
                                </td>
                                <td>
                                    <input class="ips" id="namtotnghiep1" runat="server"  />
                                </td>
                                <td>
                                    <input class="ips" id="xeploai1" runat="server" />
                                </td>
                                <td>
                                    <input class="ips" id="noicap1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="vpcc2" runat="server" />
                                </td>
                                <td>
                                    <input class="ips" id="nganhhoc2" runat="server" />
                                </td>
                                <td>
                                    <input class="ips " id="thoigiandaotao2" runat="server"  />
                                </td>
                                <td>
                                    <input class="ips" id="namtotnghiep2" runat="server"  />
                                </td>
                                <td>
                                    <input class="ips" id="xeploai2" runat="server" />
                                </td>
                                <td>
                                    <input class="ips" id="noicap2" runat="server" />
                                </td>
                            </tr>
                            <%-- TRÌNH ĐỘ NGOẠI NGỮ --%>
                            <tr>
                                <td colspan="6" class="tdnn_title">TRÌNH ĐỘ NGOẠI NGỮ : 1: Giỏi;  2: Khá;  3: T.Bình;  4: Yếu
                                </td>
                            </tr>
                            <tr>
                                <td class="tdgray">
                                    <strong>Ngoại ngữ</strong>
                                </td>
                                <td class="tdgray">
                                    <strong>Nghe</strong>
                                </td>
                                <td class="tdgray">
                                    <strong>Nói</strong>
                                </td>
                                <td class="tdgray">
                                    <strong>Đọc</strong>
                                </td>
                                <td class="tdgray">
                                    <strong>Viết</strong>
                                </td>
                                <td class="tdgray">
                                    <strong>Ghi chú</strong>
                                </td>
                            </tr>
                            <%--Anh--%>
                            <tr>
                                <td>Anh
                                </td>
                                <td>
                                    <input class="ips" id="NgheAnh" runat="server" /></td>
                                <td>
                                    <input class="ips" id="NoiAnh" runat="server" /></td>
                                <td>
                                    <input class="ips" id="DocAnh" runat="server" /></td>
                                <td>
                                    <input class="ips" id="VietAnh" runat="server" /></td>
                                <td>
                                    <input class="ips" id="GhiChuAnh" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Pháp
                                </td>
                                <td>
                                    <input class="ips" /></td>
                                <td>
                                    <input class="ips" /></td>
                                <td>
                                    <input class="ips" /></td>
                                <td>
                                    <input class="ips" /></td>
                                <td>
                                    <input class="ips" /></td>
                            </tr>
                            <tr>
                                <td>Hoa
                                </td>
                                <td>
                                    <input class="ips" /></td>
                                <td>
                                    <input class="ips" /></td>
                                <td>
                                    <input class="ips" /></td>
                                <td>
                                    <input class="ips" /></td>
                                <td>
                                    <input class="ips" /></td>
                            </tr>
                            <tr>
                                <td class="tdgray">Các khả năng/kỹ năng khác</td>
                                <td class="tdgray">Giỏi</td>
                                <td class="tdgray">Khá</td>
                                <td class="tdgray">TB</td>
                                <td class="tdgray">Yếu</td>
                                <td class="tdgray">Ghi chú</td>
                            </tr>
                            <tr>
                                <td>Vi tính</td>
                                <td>
                                    <input type="hidden" id="knViTinh" runat="server" />
                                    <input id="radViTinh1" runat="server" onchange="rcViTinh(this)" value="Giỏi" class="ips" type="radio" name="radViTinh" />
                                </td>
                                <td>
                                    <input id="radViTinh2" runat="server" onchange="rcViTinh(this)" value="Khá" class="ips" type="radio" name="radViTinh" /></td>
                                <td>
                                    <input id="radViTinh3" runat="server" onchange="rcViTinh(this)" value="TB" class="ips" type="radio" name="radViTinh" /></td>
                                <td>
                                    <input id="radViTinh4" runat="server" onchange="rcViTinh(this)" value="Yếu" class="ips" type="radio" name="radViTinh" /></td>
                                <td>
                                    <input class="ips" id="ViTinhGhiChu" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Lãnh đạo/ quản lý</td>
                                <td>
                                    <input type="hidden" id="knLanhDao" runat="server" />
                                    <input id="radLanhDao1" runat="server" onchange="rcLanhDao(this)" value="Giỏi" class="ips" type="radio" name="radLanhDao" /></td>
                                <td>
                                    <input id="radLanhDao2" runat="server" onchange="rcLanhDao(this)" value="Khá" class="ips" type="radio" name="radLanhDao" /></td>
                                <td>
                                    <input id="radLanhDao3" runat="server" onchange="rcLanhDao(this)" value="TB" class="ips" type="radio" name="radLanhDao" /></td>
                                <td>
                                    <input id="radLanhDao4" runat="server" onchange="rcLanhDao(this)" value="Yếu" class="ips" type="radio" name="radLanhDao" /></td>
                                <td>
                                    <input class="ips" id="LanhDaoGhiChu" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Giải quyết vấn đề/ Thương lượng/ Thuyết phục</td>
                                <td>
                                    <input type="hidden" id="knGiaiQuyet" runat="server" />
                                    <input id="radGaiQuyet1" runat="server" onchange="rcGiaiQuyet(this)" value="Giỏi" class="ips" type="radio" name="radGiaiQuyet" /></td>
                                <td>
                                    <input id="radGaiQuyet2" runat="server" onchange="rcGiaiQuyet(this)" value="Khá" class="ips" type="radio" name="radGiaiQuyet" /></td>
                                <td>
                                    <input id="radGaiQuyet3" runat="server" onchange="rcGiaiQuyet(this)" value="TB" class="ips" type="radio" name="radGiaiQuyet" /></td>
                                <td>
                                    <input id="radGaiQuyet4" runat="server" onchange="rcGiaiQuyet(this)" value="Yếu" class="ips" type="radio" name="radGiaiQuyet" /></td>
                                <td>
                                    <input class="ips" id="GiaiQuyetGhiChu" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Trình bày/ Diễn thuyết</td>
                                <td>
                                    <input type="hidden" id="knTrinhBay" runat="server" />
                                    <input id="radTrinhBay1" runat="server" onchange="rcTrinhBay(this)" value="Giỏi" class="ips" type="radio" name="radTrinhBay" /></td>
                                <td>
                                    <input id="radTrinhBay2" runat="server" onchange="rcTrinhBay(this)" value="Khá" class="ips" type="radio" name="radTrinhBay" /></td>
                                <td>
                                    <input id="radTrinhBay3" runat="server" onchange="rcTrinhBay(this)" value="TB" class="ips" type="radio" name="radTrinhBay" /></td>
                                <td>
                                    <input id="radTrinhBay4" runat="server" onchange="rcTrinhBay(this)" value="Yếu" class="ips" type="radio" name="radTrinhBay" /></td>
                                <td>
                                    <input class="ips" id="TrinhBayGhiChu" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Làm việc độc lập/ áp lực công việc</td>
                                <td>
                                    <input type="hidden" id="knLamViec" runat="server" />
                                    <input id="radLamViec1" runat="server" onchange="rcLamViec(this)" value="Giỏi" class="ips" type="radio" name="radLamViec" /></td>
                                <td>
                                    <input id="radLamViec2" runat="server" onchange="rcLamViec(this)" value="Khá" class="ips" type="radio" name="radLamViec" /></td>
                                <td>
                                    <input id="radLamViec3" runat="server" onchange="rcLamViec(this)" value="TB" class="ips" type="radio" name="radLamViec" /></td>
                                <td>
                                    <input id="radLamViec4" runat="server" onchange="rcLamViec(this)" value="Yếu" class="ips" type="radio" name="radLamViec" /></td>
                                <td>
                                    <input class="ips" id="LamViecGhiChu" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Sinh hoạt tập thể/ Hài hước</td>
                                <td>
                                    <input type="hidden" id="knSinhHoat" runat="server" />
                                    <input id="radSinhHoat1" runat="server" onchange="rcSinhHoat(this)" value="Giỏi" class="ips" type="radio" name="radSinhHoat" /></td>
                                <td>
                                    <input id="radSinhHoat2" runat="server" onchange="rcSinhHoat(this)" value="Khá" class="ips" type="radio" name="radSinhHoat" /></td>
                                <td>
                                    <input id="radSinhHoat3" runat="server" onchange="rcSinhHoat(this)" value="TB" class="ips" type="radio" name="radSinhHoat" /></td>
                                <td>
                                    <input id="radSinhHoat4" runat="server" onchange="rcSinhHoat(this)" value="Yếu" class="ips" type="radio" name="radSinhHoat" /></td>
                                <td>
                                    <input class="ips" id="knSinhHoatGhiChu" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Hoạt động thể thao</td>
                                <td>
                                    <input type="hidden" id="knHoatDong" runat="server" />
                                    <input id="radHoatDong1" runat="server" onchange="rcHoatDong(this)" value="Giỏi" class="ips" type="radio" name="radHoatDong" /></td>
                                <td>
                                    <input id="radHoatDong2" runat="server" onchange="rcHoatDong(this)" value="Khá" class="ips" type="radio" name="radHoatDong" /></td>
                                <td>
                                    <input id="radHoatDong3" runat="server" onchange="rcHoatDong(this)" value="TB" class="ips" type="radio" name="radHoatDong" /></td>
                                <td>
                                    <input id="radHoatDong4" runat="server" onchange="rcHoatDong(this)" value="Yếu" class="ips" type="radio" name="radHoatDong" /></td>
                                <td>
                                    <input class="ips" id="knHoatDongGhiChu" runat="server" /></td>
                            </tr>
                            <%-- TÍNH CÁCH CÁ NHÂN--%>
                            <tr>
                                <td colspan="2">TÍNH CÁCH CÁ NHÂN
                                </td>
                                <td colspan="4">NĂNG LỰC VƯỢT TRỘI KHÁC
                                </td>
                            </tr>
                            <tr>
                                <td class="tdgray">Điểm yếu
                                </td>
                                <td class="tdgray">Điểm mạnh
                                </td>
                                <td colspan="4" rowspan="3">

                                    <input class="ips" id="nangnlucvuottroi" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="ips" id="diemyeu" runat="server" />
                                </td>
                                <td>
                                    <input class="ips" id="diemmanh" runat="server" />

                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="ttcn_title">
                        6. QUÁ TRÌNH LÀM VIỆC (Nêu 2 nơi làm sau cùng - theo trình tự gần nhất)
                    </div>
                    <div class="table_1">
                        <table class="table">
                            <%--   Công ty 1--%>
                            <tr>
                                <td>
                                    <strong>TÊN CÔNG TY 1: </strong>
                                    <div>
                                        <input class="ips" id="tencongty1" runat="server" />
                                    </div>
                                    Địa chỉ:
                            <div>
                                <input class="ips" id="diachicongty1" runat="server" />
                            </div>
                                </td>
                                <td colspan="2">Ngành nghề/ lĩnh vực Cty hoạt động :
                             <div>
                                 <input class="ips" id="linhvuchd1" runat="server" />
                             </div>
                                </td>
                                <td style="width: 30%">Thời gian làm việc:
                             <div>
                                 Từ:<input class="ips " id="congtytu1"  runat="server" />
                                 Đến:
                                 <input class="ips " id="congtyden1"  runat="server" />
                             </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Chức vụ/Công việc mới vào:
                             <div>
                                 <input class="ips" id="chuvumoivao1" runat="server" />
                             </div>
                                </td>
                                <td colspan="2">Chức vụ/ Công việc sau cùng:
                             <div>
                                 <input class="ips" id="chucvusaucung1" runat="server" />
                             </div>
                                </td>
                                <td rowspan="4">Nhiệm vụ và trách nhiệm chính:
                            <div>
                                <input class="ips" id="nhiemvuchinh1" runat="server" />
                            </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Lương khởi điểm:  
                             <div>
                                 <input class="ips " id="luongkhoidiem1" runat="server" />
                             </div>
                                </td>
                                <td colspan="2">Lương sau cùng: 
                             <div>
                                 <input class="ips " id="luongsaucung1" runat="server" />
                             </div>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="3">Họ tên người quản lý trực tiếp:
                             <div>
                                 <input class="ips" id="nqlhoten1" runat="server" />
                             </div>
                                    <div style="float: left; width: 40%; margin-right: 10%">
                                        Chức vụ:  
                             <div>
                                 <input class="ips" id="nqlchucvu1" runat="server" />
                             </div>
                                    </div>
                                    <div style="float: left; width: 40%">
                                        Điện thoại:  
                             <div>
                                 <input class="ips" id="nqldienthoai" runat="server" />
                             </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">Lý do nghỉ việc: 
                             <div>
                                 <input class="ips" id="lydonghiviec1" runat="server" />
                             </div>
                                </td>
                            </tr>
                            <%--   Công ty 2--%>
                            <tr>
                                <td>
                                    <strong>TÊN CÔNG TY 2: </strong>
                                    <div>
                                        <input class="ips" id="tencongty2" runat="server" />
                                    </div>
                                    Địa chỉ:
                            <div>
                                <input class="ips" id="diachicongty21" runat="server" />
                            </div>
                                </td>
                                <td colspan="2">Ngành nghề/ lĩnh vực Cty hoạt động :
                             <div>
                                 <input class="ips" id="linhvuchoatdong2" runat="server" />
                             </div>
                                </td>
                                <td style="width: 30%">Thời gian làm việc:
                             <div>
                                 Từ:<input class="ips " id="congtytu2" runat="server"  />
                                 Đến:
                                 <input class="ips " id="congtyden2" runat="server"  />
                             </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Chức vụ/Công việc mới vào:
                             <div>
                                 <input class="ips" id="chucvumoivao2" runat="server" />
                             </div>
                                </td>
                                <td colspan="2">Chức vụ/ Công việc sau cùng:
                             <div>
                                 <input class="ips" id="chucvusaucung2" runat="server" />
                             </div>
                                </td>
                                <td rowspan="4">Nhiệm vụ và trách nhiệm chính:
                            <div>
                                <input class="ips" id="nhiemvuchinh21" runat="server" />

                            </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Lương khởi điểm:  
                             <div>
                                 <input class="ips " id="luongkhoidiem2" runat="server" />
                             </div>
                                </td>
                                <td colspan="2">Lương sau cùng: 
                             <div>
                                 <input class="ips " id="luongsaucung2" runat="server" />
                             </div>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="3">Họ tên người quản lý trực tiếp:
                             <div>
                                 <input class="ips" id="nqlhoten2" runat="server" />
                             </div>
                                    <div style="float: left; width: 40%; margin-right: 10%">
                                        Chức vụ:  
                             <div>
                                 <input class="ips" id="nqlchucvu2" runat="server" />
                             </div>
                                    </div>
                                    <div style="float: left; width: 40%">
                                        Điện thoại:  
                             <div>
                                 <input class="ips" id="nqldienthoai2" runat="server" />
                             </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">Lý do nghỉ việc: 
                             <div>
                                 <input class="ips" id="lydonghiviec2" runat="server" />
                             </div>
                                </td>
                            </tr>
                           
                            <%--Quyền lợi tại nơi làm việc sau cùng:--%>
                            <tr>
                                <td class="tdgray">
                                    <strong>Quyền lợi tại nơi làm việc sau cùng:</strong>
                                </td>
                                <td class="tdgray">Có
                                </td>
                                <td class="tdgray">Không
                                </td>
                                <td class="tdgray">Bằng tiền
                                </td>
                            </tr>
                            <tr>
                                <td>Được sử dụng xe công ty
                                </td>
                                <td>
                                    <input type="hidden" id="radSuDung" runat="server">
                                    <input onchange="radSuDung(this)" value="1" type="radio" name="radSD" class="ips" id="radSuDung1" runat="server" />
                                </td>
                                <td>
                                    <input onchange="radSuDung(this)" value="2" type="radio" name="radSD" class="ips" id="radSuDung2" runat="server" />
                                </td>
                                <td>
                                    <input onchange="radSuDung(this)" value="3" type="radio" name="radSD" class="ips" id="radSuDung3" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Phụ cấp đi lại
                                </td>
                                <td>
                                    <input type="hidden" id="radPhuCap" runat="server">
                                    <input onchange="radPhuCap(this)" value="1" type="radio" name="radSD2" class="ips" id="radPhuCap1" runat="server" />
                                </td>
                                <td>
                                    <input onchange="radPhuCap(this)" value="2" type="radio" name="radSD2" class="ips" id="radPhuCap2" runat="server" />
                                </td>
                                <td>
                                    <input onchange="radPhuCap(this)" value="3" type="radio" name="radSD2" class="ips" id="radPhuCap3" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Điện thoại di động
                                </td>
                                <td>
                                    <input type="hidden" id="radDienThoaiDiDong" runat="server">
                                    <input value="1" onchange="radDienThoaiDiDong(this)" type="radio" name="radSD3" class="ips" id="radDienThoaiDiDong1" runat="server" />
                                </td>
                                <td>
                                    <input value="2" onchange="radDienThoaiDiDong(this)" type="radio" name="radSD3" class="ips" id="radDienThoaiDiDong2" runat="server" />
                                </td>
                                <td>
                                    <input value="3" onchange="radDienThoaiDiDong(this)" type="radio" name="radSD3" class="ips" id="radDienThoaiDiDong3" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Tiền thưởng/ hoa hồng/ khuyến khích
                                </td>
                                <td>
                                    <input type="hidden" id="radTienThuong" runat="server">
                                    <input value="1" onchange="radTienThuong(this)" type="radio" name="radSD4" class="ips" id="radTienThuong1" runat="server" />
                                </td>
                                <td>
                                    <input value="2" onchange="radTienThuong(this)" type="radio" name="radSD4" class="ips" id="radTienThuong2" runat="server" />
                                </td>
                                <td>
                                    <input value="3" onchange="radTienThuong(this)" type="radio" name="radSD4" class="ips" id="radTienThuong3" runat="server" />
                                </td>

                            </tr>
                            <tr>
                                <td>Tiền vay hoặc phụ cấp
                                </td>
                                <td>
                                    <input type="hidden" id="radtienVay" runat="server">
                                    <input value="1" onchange="radtienVay(this)" type="radio" name="radSD5" class="ips" id="radtienVay1" runat="server" />
                                </td>
                                <td>
                                    <input value="2" onchange="radtienVay(this)" type="radio" name="radSD5" class="ips" id="radtienVay2" runat="server" />
                                </td>
                                <td>
                                    <input value="3" onchange="radtienVay(this)" type="radio" name="radSD5" class="ips" id="radtienVay3" runat="server" />
                                </td>

                            </tr>
                            <%--Mục tiêu phát triển nghề nghiệp (mô tả ngắn gọn Vị trí mong muốn đạt được, môi trường làm việc):--%>

                            <tr>
                                <td colspan="2">
                                    <strong>Mục tiêu phát triển nghề nghiệp (mô tả ngắn gọn Vị trí mong muốn đạt được, môi trường làm việc):

                                    </strong>
                                </td>
                                <td colspan="2">
                                    <strong>Vì sao bạn muốn làm việc cho chúng tôi? Đề nghị gì đối với Công ty nếu được tuyển dụng ?
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input class="ips" id="MuctieuPhatTrien" runat="server" />
                                </td>
                                <td colspan="2">
                                    <input class="ips" id="ViSaoBanMuon" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="text-right btn_luuthongtinbox">
                        <asp:button id="btnSaveBanner" runat="server" text="Lưu thông tin" cssclass="btn-1 btn_luuthongtin btn btn-sm btn-danger" onclick="btnSaveBanner_Click"
                            onclientclick="return CheckValidBanner();" />
                    </div>
                </div>

            </div>
            <div class="col-sm-2">
            </div>
        </div> 
    </div>
    <script src="js/jquery-1.11.1.js"></script>
    <script>
        function rcViTinh(dis) {
            $("#ctl00_ContentPlaceHolder1_knViTinh").val($(dis).val());
        }
        function rcLanhDao(dis) {
            $("#ctl00_ContentPlaceHolder1_knLanhDao").val($(dis).val());
        }
        function rcGiaiQuyet(dis) {
            $("#ctl00_ContentPlaceHolder1_knGiaiQuyet").val($(dis).val());
        }
        function rcTrinhBay(dis) {
            $("#ctl00_ContentPlaceHolder1_knTrinhBay").val($(dis).val());
        }
        function rcLamViec(dis) {
            $("#ctl00_ContentPlaceHolder1_knLamViec").val($(dis).val());
        }
        function rcSinhHoat(dis) {
            $("#ctl00_ContentPlaceHolder1_knSinhHoat").val($(dis).val());
        }
        function rcHoatDong(dis) {
            $("#ctl00_ContentPlaceHolder1_knHoatDong").val($(dis).val());
        }
        //
        function radSuDung(dis) {
            $("#ctl00_ContentPlaceHolder1_radSuDung").val($(dis).val());
        }
        function radPhuCap(dis) {
            $("#ctl00_ContentPlaceHolder1_radPhuCap").val($(dis).val());
        }
        function radDienThoaiDiDong(dis) {
            $("#ctl00_ContentPlaceHolder1_radDienThoaiDiDong").val($(dis).val());
        }
        function radTienThuong(dis) {
            $("#ctl00_ContentPlaceHolder1_radTienThuong").val($(dis).val());
        }
        function radtienVay(dis) {
            $("#ctl00_ContentPlaceHolder1_radtienVay").val($(dis).val());
        }

        function internetup() {
            if ($("#ctl00_ContentPlaceHolder1_lcinternet").val() != "") {
                $("#ctl00_ContentPlaceHolder1_chkInternet").prop("checked", true);
            }
            else {
                $("#ctl00_ContentPlaceHolder1_chkInternet").prop("checked", false);
            }
        }

        function dvvlup() {
            if ($("#ctl00_ContentPlaceHolder1_lcdvvl").val() != "") {
                $("#ctl00_ContentPlaceHolder1_chkdvvk").prop("checked", true);
            }
            else {
                $("#ctl00_ContentPlaceHolder1_chkdvvk").prop("checked", false);
            }
        }
        function gioitbup() {
            if ($("#ctl00_ContentPlaceHolder1_lcgioithieuboi").val() != "") {
                $("#ctl00_ContentPlaceHolder1_chkgioitb").prop("checked", true);
            }
            else {
                $("#ctl00_ContentPlaceHolder1_chkgioitb").prop("checked", false);
            }
        }

        function khacup() {
            if ($("#ctl00_ContentPlaceHolder1_lckhac").val() != "") {
                $("#ctl00_ContentPlaceHolder1_chkkhach").prop("checked", true);
            }
            else {
                $("#ctl00_ContentPlaceHolder1_chkkhach").prop("checked", false);
            }
        }
        //$("input").change(function () {
        //    if ($(this).attr("iptype") == "date") { 
        //        var d = new Date(($(this).val())); 
        //        if ( d == "Invalid Date") {
        //            alert("Vui lòng nhập đúng định dạng ngày!")
        //        }   
        //    }
        //});

    </script>
    <style>
        body {
            background-color: #d7d7d7;
            font-family: 'Times New Roman' !important;
        }
    </style>
    <script src="js/accounting.js"></script>

    <script>
        function isNumeric(num) {
            return !isNaN(num)
        }
        var str = "";
        function choosevanbang(value) {
            $("#ctl00_ContentPlaceHolder1_vanbang").val(value);
        }
        $(document).ready(function () {
            //$("#ctl00_ContentPlaceHolder1_mucluongdenghi").val(accounting.formatNumber($("#ctl00_ContentPlaceHolder1_mucluongdenghi").val()));
            //$("#ctl00_ContentPlaceHolder1_luongkhoidiem1").val(accounting.formatNumber($("#ctl00_ContentPlaceHolder1_luongkhoidiem1").val()));
            //$("#ctl00_ContentPlaceHolder1_luongkhoidiem2").val(accounting.formatNumber($("#ctl00_ContentPlaceHolder1_luongkhoidiem2").val()));
            //$("#ctl00_ContentPlaceHolder1_luongsaucung1").val(accounting.formatNumber($("#ctl00_ContentPlaceHolder1_luongsaucung1").val()));
            //$("#ctl00_ContentPlaceHolder1_luongsaucung2").val(accounting.formatNumber($("#ctl00_ContentPlaceHolder1_luongsaucung2").val()));

            //Set văn bằng lúc đầu
            if ($("#ctl00_ContentPlaceHolder1_vanbang").val() == 'PTTH')
            {
                $("#radVB1").prop("checked", true);
            }
            if ($("#ctl00_ContentPlaceHolder1_vanbang").val() == 'Cao đẳng') {
                $("#radVB2").prop("checked", true);
            }
            if ($("#ctl00_ContentPlaceHolder1_vanbang").val() == 'Cao học') {
                $("#radVB3").prop("checked", true);
            }
            if ($("#ctl00_ContentPlaceHolder1_vanbang").val() == 'Trung cấp') {
                $("#radVB4").prop("checked", true);
            }
            if ($("#ctl00_ContentPlaceHolder1_vanbang").val() == 'Đại học') {
                $("#radVB5").prop("checked", true);
            }
            if ($("#ctl00_ContentPlaceHolder1_vanbang").val() == 'Tiến sĩ') {
                $("#radVB6").prop("checked", true);
            }
            //Nguồn tuyển dụng
            if ($("#ctl00_ContentPlaceHolder1_lcinternet").val() != "") {
                $("#ctl00_ContentPlaceHolder1_chkInternet").prop("checked", true);
            }
            if ($("#ctl00_ContentPlaceHolder1_lcdvvl").val() != "") {
                $("#ctl00_ContentPlaceHolder1_chkdvvk").prop("checked", true);
            }
            if ($("#ctl00_ContentPlaceHolder1_lcgioithieuboi").val() != "") {
                $("#ctl00_ContentPlaceHolder1_chkgioitb").prop("checked", true);
            }
            if ($("#ctl00_ContentPlaceHolder1_lckhac").val() != "") {
                $("#ctl00_ContentPlaceHolder1_chkkhach").prop("checked", true);
            }
        });
    </script>


    <script>
        //var input = document.querySelectorAll('.datej');

        //var dateInputMask = function dateInputMask(elm) {
        //    elm.addEventListener('keyup', function (e) {
        //        if (e.keyCode < 47 || e.keyCode > 57) {
        //            e.preventDefault();
        //        }

        //        var len = elm.value.length;

        //        // If we're at a particular place, let the user type the slash
        //        // i.e., 12/12/1212
        //        if (len !== 1 || len !== 3) {
        //            if (e.keyCode == 47) {
        //                e.preventDefault();
        //            }
        //        }

        //        // If they don't add the slash, do it for them...
        //        if (len === 2) {
        //            elm.value += '/';
        //        }

        //        // If they don't add the slash, do it for them...
        //        if (len === 5) {
        //            elm.value += '/';
        //        }
        //    });
        //};
        //input.forEach(myFunction);
        //function myFunction(item, index) { 
        //    dateInputMask(item);
        //}
        function dateIsValid(dateStr) {
            const regex = /^\d{2}\/\d{2}\/\d{4}$/;

            if (dateStr.match(regex) === null) {
                return false;
            }

            const [day, month, year] = dateStr.split('/');

            // 👇️ format Date string as `yyyy-mm-dd`
            const isoFormattedStr = `${year}-${month}-${day}`;

            const date = new Date(isoFormattedStr);

            const timestamp = date.getTime();

            if (typeof timestamp !== 'number' || Number.isNaN(timestamp)) {
                return false;
            }

            return date.toISOString().startsWith(isoFormattedStr);
        }

        function checkDateValidate(id) {
           
            if ($("#" + id).val() != '')
            {
                var d = new Date(($("#" + id).val()));
                if (!dateIsValid( $("#" + id).val())) {
                    alert("Vui lòng nhập đúng định dạng ngày!");
                    $("#" + id).focus();
                    return false;
                }
            }
        }

        function CheckValidBanner() {
            var dd = checkDateValidate("ctl00_ContentPlaceHolder1_NgaySinh");
            if(dd==false)
                return false;
            dd = checkDateValidate("ctl00_ContentPlaceHolder1_NgayCMND");
            if (dd == false)
                return false;
            ////Thời gian làm việc:
            //dd = checkDateValidate("ctl00_ContentPlaceHolder1_congtytu1");
            //if (dd == false)
            //    return false;
            //dd = checkDateValidate("ctl00_ContentPlaceHolder1_congtyden1");
            //if (dd == false)
            //    return false;
            //dd = checkDateValidate("ctl00_ContentPlaceHolder1_congtytu2");
            //if (dd == false)
            //    return false;
            //dd = checkDateValidate("ctl00_ContentPlaceHolder1_congtyden2");
            //if (dd == false)
            //    return false;
            //Nếu check Lập gia đình, thì check nhập ho tên vơ chồng
            if ($("#ctl00_ContentPlaceHolder1_radLapGiaDinh").prop("checked")) {
                if ($("#ctl00_ContentPlaceHolder1_hotenvochong").val() == '') {
                    alert("Vui lòng nhập họ tên vợ chồng");
                    $("#ctl00_ContentPlaceHolder1_hotenvochong").focus();
                    return false;
                }
            }
            //Năm sinh vợ chồng
            if (!isNumeric($("#ctl00_ContentPlaceHolder1_namsinhvochong").val())) {
                alert("Vui lòng nhập đúng định dạng!");
                $("#ctl00_ContentPlaceHolder1_namsinhvochong").focus();
                return false;
            }
            if (!isNumeric($("#ctl00_ContentPlaceHolder1_namsinhcon1").val())) {
                alert("Vui lòng nhập đúng định dạng!");
                $("#ctl00_ContentPlaceHolder1_namsinhcon1").focus();
                return false;
            }
            if (!isNumeric($("#ctl00_ContentPlaceHolder1_namsinhcon1").val())) {
                alert("Vui lòng nhập đúng định dạng!");
                $("#ctl00_ContentPlaceHolder1_namsinhcon2").focus();
                return false;
            }
            if (!isNumeric($("#ctl00_ContentPlaceHolder1_namsinhcon3").val())) {
                alert("Vui lòng nhập đúng định dạng!");
                $("#ctl00_ContentPlaceHolder1_namsinhcon3").focus();
                return false;
            }
            if (!isNumeric($("#ctl00_ContentPlaceHolder1_namtotnghiep").val())) {
                alert("Vui lòng nhập đúng định dạng!");
                $("#ctl00_ContentPlaceHolder1_namtotnghiep").focus();
                return false;
            }
            //Năm tốt nghiệp
            if (!isNumeric($("#ctl00_ContentPlaceHolder1_namtotnghiep1").val())) {
                alert("Vui lòng nhập đúng định dạng!");
                $("#ctl00_ContentPlaceHolder1_namtotnghiep1").focus();
                return false;
            }
        }
    </script>
</asp:Content>
