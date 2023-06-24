using Newtonsoft.Json;
using PQT.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using UserMng.DAC;
using PQT.DAC.ViewModel;
using System.Web.Services;

namespace WebCus
{
    public partial class RenderPopupDanhGia : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        protected Guid IDNTD
        {
            get
            {
                if (ViewState["g_IDNTD"] != null)
                    return Guid.Parse(ViewState["g_IDNTD"].ToString());
                return new Guid();
            }
            set
            {
                ViewState["g_IDNTD"] = value;
            }
        }
        public int UserMemberID
        {
            get
            {
                if (Session["g_UserMemberID"] != null)
                    return Convert.ToInt32(Session["g_UserMemberID"]);
                return 0;
            }
            set
            {
                Session["g_UserMemberID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
                this.idungvien.Value = (Guid.Parse(Request.QueryString["id"].ToString())).ToString();
                //Logo
                var idUser = this.UserMemberID;
                this.userid.Value = idUser.ToString();
                UngVien uv = blc_user.GetUngVienByID(GuiId);
                YeuCauTuyenDung getyctd = blc_user.GetYeuCauTD_ByID(uv.IdYeuCau.Value);
                if (getyctd != null)
                {
                    if (getyctd.TrucThuoc == 1)
                    {
                        this.logomain.Src = "/Images/logo_form.png";
                    }
                    if (getyctd.TrucThuoc == 2)
                    {
                        this.logomain.Src = "/Images/Logo/chinhnhan_logo.png";
                    }
                    if (getyctd.TrucThuoc == 3)
                    {
                        this.logomain.Src = "/Images/Logo/smc_logo.jpg";
                    }
                }
                //
           
                if (Request.QueryString["type"] != null)
                {
                    var key = Utility.Decrypt(Request.QueryString["type"]);
                    var splkey = key.Split('-');
                    this.userid.Value = splkey[0];
                    this.type.Value = splkey[1];

                    if (this.type.Value == "2")
                    {
                        YeuCauTuyenDung yctd = blc_user.GetYeuCauTD_ByID(uv.IdYeuCau.Value);
                        PhongBan pb = blc_user.GetPhongBan_ByID(yctd.IDPhongBan.Value);
                        this.ngayduyet.Value = DateTime.Now.ToShortDateString();
                        this.thoigianthuviec.Value = "2 tháng";
                        this.phongban.Value = pb.TenPhong;
                    }
                }
                var guiid = Guid.Parse(Request.QueryString["id"].ToString());
                //Kiểm tra đánh giá 
                
                Ungvien_Trangthai uvtt = blc_user.GetUngvien_TrangthaiById(guiid);
                if (uvtt.DanhGia == 0)
                {
                    ThongTinViTri ttvt = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);
                    this.Mucluonghientai.Value = ttvt.MucLuong;
                    this.Mucluongdenghi.Value = ttvt.MucLuong;
                    if (this.userid.Value == "")
                        this.userid.Value = UserMemberID.ToString();
                    TUser tuser = blc_user.GetUser_ByIDAll(int.Parse(this.userid.Value));
                    if (tuser != null)
                        this.NguoiPV.Value = tuser.UserName;
                }
                this.IDNTD = GuiId;
                this.HoTenUngVien.Value = uv.HoTen;
                if (uv.UngCuViTri != null)
                {
                    this.NgayPV.SelectedDate = uv.NgayPhongVan.Value;
                    this.vitriungtuyen.Value = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri).ViTriungTuyen;
                    this.vitricongviec.Value= JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri).ViTriungTuyen;
                    this.phongban.Value = uv.PhongBan;
                    this.bophan.Value= uv.PhongBan;
                    DanhGiaTuyenDung dgtd = blc_user.CheckDanhGiatuyenDung(this.IDNTD);
                    var dgtds = blc_user.GetDanhGiaTuyenDung(guiid);
                    if (dgtds == null)
                    {
                        this.dottuyendung.Value = "1";
                        return;
                    }
                    if (dgtds.Quyetdinhlan1 == "PV lần 2" && uvtt.NgayGoiPV2.HasValue)
                    {
                        this.dottuyendung.Value = "2";
                    }
                    else
                    {
                        this.dottuyendung.Value = "1";
                    }

                    if (dgtd != null)
                    {
                        //Lần 1
                        this.DanhGiaID = dgtd.DanhGiaID;
                        this.vitriungtuyen.Value = dgtd.Vitriungtuyen;
                        this.Mucluonghientai.Value = dgtd.Mucluonghientai.ToString();
                        this.Mucluongdenghi.Value = dgtd.Mucluongdenghi.ToString();
                        this.NgayPV.SelectedDate = dgtd.NgayPV.Value;
                        this.NguoiPV.Value = dgtd.NguoiPV != null ? dgtd.NguoiPV.ToString() : ""; 
                        //Đánh giá tuyển dụng
                        if (!string.IsNullOrEmpty(dgtd.KienThucChuyenNganh))
                        {
                            this.diemkienthuc1.Value = dgtd.KienThucChuyenNganh.Split(',')[0];
                           this.nhanxetkienthuc1.Value = dgtd.KienThucChuyenNganh.Split(',')[1];
                        }
                        //Chuyên môn
                        if (!string.IsNullOrEmpty(dgtd.ChuyenMon))
                        {
                            this.diemchuyenmon1.Value = dgtd.ChuyenMon.Split(',')[0];
                            this.nhanxetchuyenmon1.Value = dgtd.ChuyenMon.Split(',')[1];
                        }
                        //Kinh nghiệm
                        if (!string.IsNullOrEmpty(dgtd.KinhNghiem))
                        {
                            this.diemkinhnghiem1.Value = dgtd.KinhNghiem.Split(',')[0];
                            this.nhanxetkinhnghiem1.Value = dgtd.KinhNghiem.Split(',')[1];
                        }
                        //thành tích
                        if (!string.IsNullOrEmpty(dgtd.ThanhTich))
                        {
                            this.diemthanhtich1.Value = dgtd.ThanhTich.Split(',')[0];
                            this.nhanxetthanhtich1.Value = dgtd.ThanhTich.Split(',')[1];
                        }
                        //định hướng
                        if (!string.IsNullOrEmpty(dgtd.DinhHuong))
                        {
                            this.diemdinhhuong1.Value = dgtd.DinhHuong.Split(',')[0];
                            this.nhanxetdinhhuong1.Value = dgtd.DinhHuong.Split(',')[1];
                        }
                        //tieu chí
                        if (!string.IsNullOrEmpty(dgtd.TieuChi))
                        {
                            this.diemtieuchi1.Value = dgtd.TieuChi.Split(',')[0];
                            this.nhanxettieuchi1.Value = dgtd.TieuChi.Split(',')[1];
                        }
                        //tính cách
                        if (!string.IsNullOrEmpty(dgtd.TinhCach))
                        {
                            this.diemtinhcach1.Value = dgtd.TinhCach.Split(',')[0];
                            this.nhanxettinhcach1.Value = dgtd.TinhCach.Split(',')[1];
                        }
                        //Khả năng
                        if (!string.IsNullOrEmpty(dgtd.KhaNangHoiNhap))
                        {
                            this.diemkhanang1.Value = dgtd.KhaNangHoiNhap.Split(',')[0];
                            this.noidungkhanang1.Value = dgtd.KhaNangHoiNhap.Split(',')[1];
                        }
                        //Tổng điểm
                        this.tongdiem1.Value = dgtd.TongDiem;
                        if (dgtd.Ketqualan1 == 1)
                            this.radDat1.Checked = true;
                        else
                            this.radKhongDat1.Checked = true;
                        this.baocaocho.Value = dgtd.Baocaocho;
                        this.ngaynhanviec.Value = dgtd.Ngaynhanviec.HasValue ? dgtd.Ngaynhanviec.Value.ToShortDateString() : "";
                        this.luongthuviec.Value = dgtd.Luongthuviec;
                        this.thoigianthuviec.Value = dgtd.Thoigianthuviec != null ? dgtd.Thoigianthuviec : "";
                        this.luongchinh.Value = dgtd.Luongchinh;
                        this.phucap.Value = dgtd.Phucap;
                        this.thoathuankhac.Value = dgtd.Thoathuankhac;
                        this.kyduyet.Value = dgtd.Kyduyet;
                        // this.ngayduyet.Value = dgtd.Ngayduyet.HasValue ? dgtd.Ngayduyet.Value.ToShortDateString() : "";
                        //
                        if (dgtd.Quyetdinhlan1 == "Không phù hợp")
                        {
                            this.radKhongPhuhop.Checked = true;
                        }
                        if (dgtd.Quyetdinhlan1 == "So sánh ứng viên khác")
                        {
                            this.radSoSanhUngVienKhac.Checked = true;
                        }
                        if (dgtd.Quyetdinhlan1 == "PV lần 2")
                        {
                            this.radPVlan2.Checked = true;
                        }
                        if (dgtd.Quyetdinhlan1 == "Khác")
                        {
                            this.radKhac1.Checked = true;
                        }
                        //Kiểm tra có lan 2 ko
                        var dgtd2 = blc_user.CheckDanhGiatuyenDung2(this.IDNTD);
                       
                        if (dgtd2 != null)
                        { 
                            this.DanhGiaID = dgtd2.DanhGiaID;
                            //this.vitriungtuyen.Value = dgtd2.Vitriungtuyen;
                            //this.Mucluonghientai.Value = dgtd2.Mucluonghientai.ToString();
                            //this.Mucluongdenghi.Value = dgtd2.Mucluongdenghi.ToString();
                            if (dgtd2.NgayPV.HasValue)
                                this.Text6.SelectedDate = dgtd2.NgayPV.Value;
                            this.Text9.Value = dgtd2.NguoiPV != null ? dgtd2.NguoiPV.ToString() : "";

                            //Đánh giá tuyển dụng
                            if (!string.IsNullOrEmpty(dgtd.KienThucChuyenNganh))
                            {
                                this.diemkienthuc2.Value = dgtd2.KienThucChuyenNganh.Split(',')[0];
                                this.nhanxetkienthuc2.Value = dgtd2.KienThucChuyenNganh.Split(',')[1];
                            }
                            //Chuyên môn
                            if (!string.IsNullOrEmpty(dgtd2.ChuyenMon))
                            {
                                this.diemchuyenmon2.Value = dgtd2.ChuyenMon.Split(',')[0];
                                this.nhanxetchuyenmon2.Value = dgtd2.ChuyenMon.Split(',')[1];
                            }
                            //Kinh nghiệm
                            if (!string.IsNullOrEmpty(dgtd.KinhNghiem))
                            {
                                this.diemkinhnghiem2.Value = dgtd2.KinhNghiem.Split(',')[0];
                                this.nhanxetkinhnghiem2.Value = dgtd2.KinhNghiem.Split(',')[1];
                            }
                            //thành tích
                            if (!string.IsNullOrEmpty(dgtd2.ThanhTich))
                            {
                                this.diemthanhtich2.Value = dgtd2.ThanhTich.Split(',')[0];
                                this.nhanxetthanhtich2.Value = dgtd2.ThanhTich.Split(',')[1];
                            }
                            //định hướng
                            if (!string.IsNullOrEmpty(dgtd2.DinhHuong))
                            {
                                this.diemdinhhuong2.Value = dgtd2.DinhHuong.Split(',')[0];
                                this.nhanxetdinhhuong2.Value = dgtd2.DinhHuong.Split(',')[1];
                            }
                            //tieu chí
                            if (!string.IsNullOrEmpty(dgtd.TieuChi))
                            {
                                this.diemtieuchi2.Value = dgtd2.TieuChi.Split(',')[0];
                                this.nhanxettieuchi2.Value = dgtd2.TieuChi.Split(',')[1];
                            }
                            //tính cách
                            if (!string.IsNullOrEmpty(dgtd.TinhCach))
                            {
                                this.diemtinhcach2.Value = dgtd2.TinhCach.Split(',')[0];
                                this.nhanxettinhcach2.Value = dgtd2.TinhCach.Split(',')[1];
                            }
                            //Khả năng
                            if (!string.IsNullOrEmpty(dgtd.KhaNangHoiNhap))
                            {
                                this.diemkhanang2.Value = dgtd2.KhaNangHoiNhap.Split(',')[0];
                                this.noidungkhanang2.Value = dgtd2.KhaNangHoiNhap.Split(',')[1];
                            }
                            //Tổng điểm
                            this.tongdiem2.Value = dgtd2.TongDiem;
                            if (dgtd2.Ketqualan2 == 1)
                                this.radDat2.Checked = true;
                            else
                                this.radKhongDat2.Checked = true;
                            this.baocaocho.Value = dgtd2.Baocaocho;
                            this.ngaynhanviec.Value = dgtd2.Ngaynhanviec.HasValue ? dgtd2.Ngaynhanviec.Value.ToShortDateString() : "";
                            this.luongthuviec.Value = dgtd2.Luongthuviec;
                            this.thoigianthuviec.Value = dgtd2.Thoigianthuviec != null ? dgtd2.Thoigianthuviec : "";
                            this.luongchinh.Value = dgtd2.Luongchinh;
                            this.phucap.Value = dgtd2.Phucap;
                            this.thoathuankhac.Value = dgtd2.Thoathuankhac;
                            this.kyduyet.Value = dgtd2.Kyduyet;
                            this.ngayduyet.Value = dgtd2.Ngayduyet.HasValue ? dgtd2.Ngayduyet.Value.ToShortDateString() : "";
                            //
                            if (dgtd2.Quyetdinhlan2 == "Chuyên môn chưa đạt")
                            {
                                this.radChuyenMonChuaDat.Checked = true;
                            }
                            if (dgtd2.Quyetdinhlan2 == "So sánh ứng viên khác")
                            {
                                this.radSoSanhUngVienKhac2.Checked = true;
                            }
                            if (dgtd2.Quyetdinhlan2 == "PV lần 3")
                            {
                                this.radPVlan3.Checked = true;
                            }
                            if (dgtd2.Quyetdinhlan2 == "Khác")
                            {
                                this.radKhac2.Checked = true;
                            }
                        }
                        else
                        {
                            if(uvtt.NgayGoiPV2!=null)
                            this.Text6.SelectedDate = uvtt.NgayGoiPV2.Value;
                        }
                        //
                        var dgtd3 = blc_user.CheckDanhGiatuyenDung3(this.IDNTD);
                        if (dgtd3 != null)
                        {
                            this.DanhGiaID = dgtd3.DanhGiaID;
                            //this.vitriungtuyen.Value = dgtd3.Vitriungtuyen;
                            //this.Mucluonghientai.Value = dgtd3.Mucluonghientai.ToString();
                            //this.Mucluongdenghi.Value = dgtd3.Mucluongdenghi.ToString();
                            this.Text7.SelectedDate = dgtd3.NgayPV.Value;
                            this.Text10.Value = dgtd3.NguoiPV != null ? dgtd3.NguoiPV.ToString() : "";

                            //Đánh giá tuyển dụng
                            if (!string.IsNullOrEmpty(dgtd.KienThucChuyenNganh))
                            {
                                this.diemkienthuc3.Value = dgtd3.KienThucChuyenNganh.Split(',')[0];
                                this.nhanxetkienthuc3.Value = dgtd3.KienThucChuyenNganh.Split(',')[1];
                            }
                            //Chuyên môn
                            if (!string.IsNullOrEmpty(dgtd2.ChuyenMon))
                            {
                                this.diemchuyenmon3.Value = dgtd3.ChuyenMon.Split(',')[0];
                                this.nhanxetchuyenmon3.Value = dgtd3.ChuyenMon.Split(',')[1];
                            }
                            //Kinh nghiệm
                            if (!string.IsNullOrEmpty(dgtd3.KinhNghiem))
                            {
                                this.diemkinhnghiem3.Value = dgtd3.KinhNghiem.Split(',')[0];
                                this.nhanxetkinhnghiem3.Value = dgtd3.KinhNghiem.Split(',')[1];
                            }
                            //thành tích
                            if (!string.IsNullOrEmpty(dgtd3.ThanhTich))
                            {
                                this.diemthanhtich3.Value = dgtd3.ThanhTich.Split(',')[0];
                                this.nhanxetthanhtich3.Value = dgtd3.ThanhTich.Split(',')[1];
                            }
                            //định hướng
                            if (!string.IsNullOrEmpty(dgtd3.DinhHuong))
                            {
                                this.diemdinhhuong3.Value = dgtd3.DinhHuong.Split(',')[0];
                                this.nhanxetdinhhuong3.Value = dgtd3.DinhHuong.Split(',')[1];
                            }
                            //tieu chí
                            if (!string.IsNullOrEmpty(dgtd3.TieuChi))
                            {
                                this.diemtieuchi3.Value = dgtd3.TieuChi.Split(',')[0];
                                this.nhanxettieuchi3.Value = dgtd3.TieuChi.Split(',')[1];
                            }
                            //tính cách
                            if (!string.IsNullOrEmpty(dgtd3.TinhCach))
                            {
                                this.diemtinhcach3.Value = dgtd3.TinhCach.Split(',')[0];
                                this.nhanxettinhcach3.Value = dgtd3.TinhCach.Split(',')[1];
                            }
                            //Khả năng
                            if (!string.IsNullOrEmpty(dgtd3.KhaNangHoiNhap))
                            {
                                this.diemkhanang3.Value = dgtd3.KhaNangHoiNhap.Split(',')[0];
                                this.noidungkhanang3.Value = dgtd3.KhaNangHoiNhap.Split(',')[1];
                            }
                            //Tổng điểm
                            this.tongdiem3.Value = dgtd3.TongDiem;
                            if (dgtd3.Ketqualan3 == 1)
                                this.radDat.Checked = true;
                            else
                                this.radKhongDat3.Checked = true;
                            this.baocaocho.Value = dgtd3.Baocaocho;
                            this.ngaynhanviec.Value = dgtd3.Ngaynhanviec.HasValue ? dgtd3.Ngaynhanviec.Value.ToShortDateString() : "";
                            this.luongthuviec.Value = dgtd3.Luongthuviec;
                            this.thoigianthuviec.Value = dgtd3.Thoigianthuviec != null ? dgtd3.Thoigianthuviec : "";
                            this.luongchinh.Value = dgtd3.Luongchinh;
                            this.phucap.Value = dgtd3.Phucap;
                            this.thoathuankhac.Value = dgtd3.Thoathuankhac;
                            this.kyduyet.Value = dgtd3.Kyduyet;
                            this.ngayduyet.Value = dgtd3.Ngayduyet.HasValue ? dgtd3.Ngayduyet.Value.ToShortDateString() : "";
                            //
                            if (dgtd3.Quyetdinhlan3 == "Ứng viên suy nghĩ thêm")
                            {
                                this.radUngVienSuyNghiThem.Checked = true;
                            }
                            if (dgtd3.Quyetdinhlan3 == "Xem xét thêm")
                            {
                                this.radXemxetthem.Checked = true;
                            }
                            if (dgtd3.Quyetdinhlan3 == "PV lại")
                            {
                                this.radPVLai.Checked = true;
                            }
                            if (dgtd3.Quyetdinhlan3 == "Đạt")
                            {
                                this.radDat3.Checked = true;
                            }
                        }
                    }
                }
                
            }
        }
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            int type = 0;
            DanhGiaTuyenDung dgtd = blc_user.CheckDanhGiatuyenDung(this.IDNTD);
            if (dgtd == null)
                   dgtd = new DanhGiaTuyenDung();
            //2
            DanhGiaTuyenDung dgtd2 = blc_user.CheckDanhGiatuyenDung2(this.IDNTD);
            if (Page.Request.Form["ctl00$ContentPlaceHolder1$Text6$dateInput_TextBox"].ToString() != "" && Page.Request.Form["ctl00$ContentPlaceHolder1$Text6$dateInput_TextBox"].ToString()!="01/01/1911")
            {
                if (dgtd2 == null)
                    dgtd2 = new DanhGiaTuyenDung();
            }
            DanhGiaTuyenDung dgtd3 = blc_user.CheckDanhGiatuyenDung3(this.IDNTD);
            //pvlan3
            if (Page.Request.Form["ctl00$ContentPlaceHolder1$Text7$dateInput_TextBox"].ToString() != "" && Page.Request.Form["ctl00$ContentPlaceHolder1$Text7$dateInput_TextBox"].ToString() != "01/01/1911")
            {
                if (dgtd3 == null)
                    dgtd3 = new DanhGiaTuyenDung();
            }

            dgtd.Vitriungtuyen = Page.Request.Form["ctl00$ContentPlaceHolder1$vitriungtuyen"].ToString();
            dgtd.Phongban = this.phongban.Value;
             
            if( !string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$Mucluonghientai"]))
                dgtd.Mucluonghientai = Page.Request.Form["ctl00$ContentPlaceHolder1$Mucluonghientai"].ToString();
            if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$Mucluongdenghi"]))
                dgtd.Mucluongdenghi = Page.Request.Form["ctl00$ContentPlaceHolder1$Mucluongdenghi"].ToString();

            //Đánh giá  
            if (type == 1 || 1<2)
            {
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$NgayPV$dateInput_TextBox"]))
                    dgtd.NgayPV = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$NgayPV$dateInput_TextBox"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                dgtd.NguoiPV = Page.Request.Form["ctl00$ContentPlaceHolder1$NguoiPV"].ToString();
                dgtd.KienThucChuyenNganh = Page.Request.Form["ctl00$ContentPlaceHolder1$diemkienthuc1"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetkienthuc1"].ToString();
                dgtd.ChuyenMon = Page.Request.Form["ctl00$ContentPlaceHolder1$diemchuyenmon1"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetchuyenmon1"].ToString();
                dgtd.KinhNghiem = Page.Request.Form["ctl00$ContentPlaceHolder1$diemkinhnghiem1"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetkinhnghiem1"].ToString();
                dgtd.ThanhTich = Page.Request.Form["ctl00$ContentPlaceHolder1$diemthanhtich1"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetthanhtich1"].ToString();
                dgtd.DinhHuong = Page.Request.Form["ctl00$ContentPlaceHolder1$diemdinhhuong1"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetdinhhuong1"].ToString();
                dgtd.TieuChi = Page.Request.Form["ctl00$ContentPlaceHolder1$diemtieuchi1"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxettieuchi1"].ToString();
                dgtd.TinhCach = Page.Request.Form["ctl00$ContentPlaceHolder1$diemtinhcach1"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxettinhcach1"].ToString();
                dgtd.KhaNangHoiNhap = Page.Request.Form["ctl00$ContentPlaceHolder1$diemkhanang1"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$noidungkhanang1"].ToString();
                dgtd.TongDiem = Page.Request.Form["ctl00$ContentPlaceHolder1$tongdiem1"].ToString();
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$kqdat1"].ToString() == "radDat1")
                {
                    dgtd.Ketqualan1 = 1;
                    //Cập nhật lại trạng thái ứng viên
                    blc_user.UpdateStatus(this.IDNTD, 1, 5);
                }
                else
                {
                    dgtd.Ketqualan1 = 0;
                    blc_user.UpdateStatus(this.IDNTD,2,0);
                }
                dgtd.Baocaocho = Page.Request.Form["ctl00$ContentPlaceHolder1$baocaocho"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$ngaynhanviec"]))
                {
                    dgtd.Ngaynhanviec = dgtd.NgayPV = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$ngaynhanviec"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                   
                }
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$luongthuviec"]))
                    dgtd.Luongthuviec = Page.Request.Form["ctl00$ContentPlaceHolder1$luongthuviec"].ToString();
                dgtd.Thoigianthuviec = Page.Request.Form["ctl00$ContentPlaceHolder1$thoigianthuviec"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$luongchinh"]))
                    dgtd.Luongchinh = Page.Request.Form["ctl00$ContentPlaceHolder1$luongchinh"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$phucap"]))
                    dgtd.Phucap = Page.Request.Form["ctl00$ContentPlaceHolder1$phucap"].ToString();
                dgtd.Thoathuankhac = Page.Request.Form["ctl00$ContentPlaceHolder1$thoathuankhac"].ToString();
                dgtd.Kyduyet = Page.Request.Form["ctl00$ContentPlaceHolder1$kyduyet"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$ngayduyet"]))
                    dgtd.Ngayduyet = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$ngayduyet"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);

                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd1"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd1"].ToString() == "radKhongPhuhop")
                        dgtd.Quyetdinhlan1 = "Không phù hợp";
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd1"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd1"].ToString() == "radSoSanhUngVienKhac")
                        dgtd.Quyetdinhlan1 = "So sánh ứng viên khác";
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd1"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd1"].ToString() == "radPVlan2")
                        dgtd.Quyetdinhlan1 = "PV lần 2";
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd1"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd1"].ToString() == "radKhac1")
                        dgtd.Quyetdinhlan1 = "Khác";
                }
            }
            if (type == 2 || dgtd2!=null)
            {
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$Text6$dateInput_TextBox"]))
                    dgtd2.NgayPV = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$Text6$dateInput_TextBox"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                dgtd2.NguoiPV = Page.Request.Form["ctl00$ContentPlaceHolder1$Text9"].ToString();
                dgtd2.KienThucChuyenNganh = Page.Request.Form["ctl00$ContentPlaceHolder1$diemkienthuc2"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetkienthuc2"].ToString();
                dgtd2.ChuyenMon = Page.Request.Form["ctl00$ContentPlaceHolder1$diemchuyenmon2"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetchuyenmon2"].ToString();
                dgtd2.KinhNghiem = Page.Request.Form["ctl00$ContentPlaceHolder1$diemkinhnghiem2"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetkinhnghiem2"].ToString();
                dgtd2.ThanhTich = Page.Request.Form["ctl00$ContentPlaceHolder1$diemthanhtich2"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetthanhtich2"].ToString();
                dgtd2.DinhHuong = Page.Request.Form["ctl00$ContentPlaceHolder1$diemdinhhuong2"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetdinhhuong2"].ToString();
                dgtd2.TieuChi = Page.Request.Form["ctl00$ContentPlaceHolder1$diemtieuchi2"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxettieuchi2"].ToString();
                dgtd2.TinhCach = Page.Request.Form["ctl00$ContentPlaceHolder1$diemtinhcach2"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxettinhcach2"].ToString();
                dgtd2.KhaNangHoiNhap = Page.Request.Form["ctl00$ContentPlaceHolder1$diemkhanang2"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$noidungkhanang2"].ToString();
                dgtd2.TongDiem = Page.Request.Form["ctl00$ContentPlaceHolder1$tongdiem2"].ToString();
                //if (Page.Request.Form["ctl00$ContentPlaceHolder1$kqdat2"].ToString() == "radDat2")
                //    dgtd2.Ketqualan2 = 1;
                //else
                //    dgtd2.Ketqualan2 = 0;

                if (Page.Request.Form["ctl00$ContentPlaceHolder1$kqdat2"].ToString() == "radDat2")
                {
                    dgtd2.Ketqualan2 = 1;
                    //Cập nhật lại trạng thái ứng viên
                    blc_user.UpdateStatus(this.IDNTD, 1, 5);
                }
                else
                {
                    dgtd2.Ketqualan2 = 0;
                    blc_user.UpdateStatus(this.IDNTD, 2, 0);
                }

                dgtd2.Baocaocho = Page.Request.Form["ctl00$ContentPlaceHolder1$baocaocho"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$ngaynhanviec"]))
                    dgtd2.Ngaynhanviec = dgtd.NgayPV = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$ngaynhanviec"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$luongthuviec"]))
                    dgtd2.Luongthuviec = Page.Request.Form["ctl00$ContentPlaceHolder1$luongthuviec"].ToString();
                dgtd2.Thoigianthuviec = Page.Request.Form["ctl00$ContentPlaceHolder1$thoigianthuviec"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$luongchinh"]))
                    dgtd2.Luongchinh = Page.Request.Form["ctl00$ContentPlaceHolder1$luongchinh"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$phucap"]))
                    dgtd2.Phucap = Page.Request.Form["ctl00$ContentPlaceHolder1$phucap"].ToString();
                dgtd2.Thoathuankhac = Page.Request.Form["ctl00$ContentPlaceHolder1$thoathuankhac"].ToString();
                dgtd2.Kyduyet = Page.Request.Form["ctl00$ContentPlaceHolder1$kyduyet"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$ngayduyet"]))
                    dgtd2.Ngayduyet = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$ngayduyet"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);

                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd2"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd2"].ToString() == "radChuyenMonChuaDat")
                        dgtd2.Quyetdinhlan2 = "Chuyên môn chưa đạt";
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd2"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd2"].ToString() == "radSoSanhUngVienKhac2")
                        dgtd2.Quyetdinhlan2 = "So sánh ứng viên khác";
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd2"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd2"].ToString() == "radPVlan3")
                        dgtd2.Quyetdinhlan2 = "PV lần 3";
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd2"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd2"].ToString() == "radKhac2")
                        dgtd2.Quyetdinhlan2 = "Khác";
                }
            }

            //
            if (type == 3 || dgtd3!=null)
            {
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$Text7$dateInput_TextBox"]))
                    dgtd3.NgayPV = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$Text7$dateInput_TextBox"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                dgtd3.NguoiPV = Page.Request.Form["ctl00$ContentPlaceHolder1$Text10"].ToString();
                dgtd3.KienThucChuyenNganh = Page.Request.Form["ctl00$ContentPlaceHolder1$diemkienthuc3"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetkienthuc3"].ToString();
                dgtd3.ChuyenMon = Page.Request.Form["ctl00$ContentPlaceHolder1$diemchuyenmon3"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetchuyenmon3"].ToString();
                dgtd3.KinhNghiem = Page.Request.Form["ctl00$ContentPlaceHolder1$diemkinhnghiem3"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetkinhnghiem3"].ToString();
                dgtd3.ThanhTich = Page.Request.Form["ctl00$ContentPlaceHolder1$diemthanhtich3"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetthanhtich3"].ToString();
                dgtd3.DinhHuong = Page.Request.Form["ctl00$ContentPlaceHolder1$diemdinhhuong3"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxetdinhhuong3"].ToString();
                dgtd3.TieuChi = Page.Request.Form["ctl00$ContentPlaceHolder1$diemtieuchi3"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxettieuchi3"].ToString();
                dgtd3.TinhCach = Page.Request.Form["ctl00$ContentPlaceHolder1$diemtinhcach3"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$nhanxettinhcach3"].ToString();
                dgtd3.KhaNangHoiNhap = Page.Request.Form["ctl00$ContentPlaceHolder1$diemkhanang3"].ToString() + "," + Page.Request.Form["ctl00$ContentPlaceHolder1$noidungkhanang3"].ToString();
                dgtd3.TongDiem = Page.Request.Form["ctl00$ContentPlaceHolder1$tongdiem3"].ToString();
                //if (Page.Request.Form["ctl00$ContentPlaceHolder1$kqdat3"].ToString() == "radDat")
                //    dgtd3.Ketqualan3 = 1;
                //else
                //    dgtd3.Ketqualan3 = 0;

                if (Page.Request.Form["ctl00$ContentPlaceHolder1$kqdat3"].ToString() == "radDat")
                {
                    dgtd.Ketqualan3 = 1;
                    //Cập nhật lại trạng thái ứng viên
                    blc_user.UpdateStatus(this.IDNTD, 1, 5);
                }
                else
                {
                    dgtd.Ketqualan3 = 0;
                    blc_user.UpdateStatus(this.IDNTD, 2, 0);
                }
                dgtd3.Baocaocho = Page.Request.Form["ctl00$ContentPlaceHolder1$baocaocho"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$ngaynhanviec"]))
                    dgtd3.Ngaynhanviec = dgtd.NgayPV = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$ngaynhanviec"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$luongthuviec"]))
                    dgtd3.Luongthuviec = Page.Request.Form["ctl00$ContentPlaceHolder1$luongthuviec"].ToString();
                dgtd3.Thoigianthuviec = Page.Request.Form["ctl00$ContentPlaceHolder1$thoigianthuviec"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$luongchinh"]))
                    dgtd3.Luongchinh = Page.Request.Form["ctl00$ContentPlaceHolder1$luongchinh"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$phucap"]))
                    dgtd3.Phucap = Page.Request.Form["ctl00$ContentPlaceHolder1$phucap"].ToString();
                dgtd3.Thoathuankhac = Page.Request.Form["ctl00$ContentPlaceHolder1$thoathuankhac"].ToString();
                dgtd3.Kyduyet = Page.Request.Form["ctl00$ContentPlaceHolder1$kyduyet"].ToString();
                if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$ngayduyet"]))
                    dgtd3.Ngayduyet = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$ngayduyet"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);

                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd2"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd3"].ToString() == "radUngVienSuyNghiThem")
                        dgtd3.Quyetdinhlan3 = "Ứng viên suy nghĩ thêm";
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd3"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd3"].ToString() == "radXemxetthem")
                        dgtd3.Quyetdinhlan3 = "Xem xét thêm";
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd3"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd3"].ToString() == "radPVLai")
                        dgtd3.Quyetdinhlan3 = "PV lại";
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd3"] != null)
                {
                    if (Page.Request.Form["ctl00$ContentPlaceHolder1$qd3"].ToString() == "radDat3")
                        dgtd3.Quyetdinhlan3 = "Đạt";
                }
            }
            if (this.type.Value == "")
                this.type.Value = "0";
            if (this.userid.Value == "")
                this.userid.Value = UserMemberID.ToString();
            blc_user.CapnhatDanhGia(dgtd, this.IDNTD,1);
            if (dgtd2 != null)
            {
                blc_user.CapnhatDanhGia(dgtd2, this.IDNTD,2);
            }
            if (dgtd3 != null)
            {
                blc_user.CapnhatDanhGia(dgtd3, this.IDNTD,3);
            }
            blc_user.CapnhatnguoiDanhGia(int.Parse(this.userid.Value),this.IDNTD,int.Parse(this.type.Value));
            Alert.Show("Cập nhật thành công");
            string close = @"<script type='text/javascript'>
                             window.opener.location.reload(true);
                                self.close();
                                </script>";
            base.Response.Write(close);
        }
        protected int DanhGiaID
        {
            get
            {
                if (ViewState["g_DanhGiaID"] != null)
                    return int.Parse(ViewState["g_DanhGiaID"].ToString());
                return   0;
            }
            set
            {
                ViewState["g_DanhGiaID"] = value;
            }
        }
      
        protected void btnDuyet_Click(object sender, EventArgs e)
        {
            blc_user.DuyetDanhGia(this.IDNTD);
            string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
            base.Response.Write(close);
        }

        [WebMethod]
        public static string Login(string username, string password, Guid idungvien)
        {
            UserMngOther_BLC blc_user = new UserMngOther_BLC();
            var checkLogin = blc_user.CheckDangNhap(username, password);
            if (checkLogin == null)
                return "-1";
            ThongTinNhanSu ttns = blc_user.GetTTNhansu_byID(checkLogin.IdNhansu.Value);
            int vitri = ttns.ViTri.Value;
            string strVitri = "";
            if (vitri == 1)
                strVitri = "Trưởng Phòng";
            if (vitri == 2)
                strVitri = "Phó Phòng";
            if (vitri == 3)
                strVitri = "Trưởng Nhóm";
            if (vitri == 4)
                strVitri = "Nhân Viên";
            if (vitri == 9)
                strVitri = "Trưởng Bộ Phận";
            if (vitri == 10)
                strVitri = "Phó Bộ Phận";
            if (vitri == 11)
                strVitri = "Phó Nhóm";
            //Kiểm tra lần đâu 
            var checkExist = blc_user.CheckExistDanhGiaTuyenDung(idungvien);
            int hasdata = 0;
            if (checkExist)
            {
                hasdata = 1;
            }

            string result = "0," + checkLogin.UserName + "," + strVitri + ',' + hasdata + "," + checkLogin.UserID;
            var getChucvu = blc_user.Getchucvu(checkLogin.UserID);
            if (getChucvu == 1 || getChucvu == 2 || getChucvu == 3 || getChucvu == 9 || getChucvu == 10 || getChucvu == 11)
            {
                result = "1," + checkLogin.UserName + "," + strVitri + ',' + hasdata + "," + checkLogin.UserID;
            }
            if (getChucvu == 5 || getChucvu == 6 || getChucvu == 8)
            {
                result = "2," + checkLogin.UserName + "," + strVitri + ',' + hasdata + "," + checkLogin.UserID;
            }
            return result;
        }
    }
}