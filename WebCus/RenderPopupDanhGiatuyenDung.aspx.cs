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
    public partial class RenderPopupDanhGiatuyenDung : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
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
                this.typeChucvu.Value = "0";
                this.idungvien.Value = (Guid.Parse(Request.QueryString["id"].ToString())).ToString();
                var idUser = this.UserMemberID;
                if (idUser == 275 || idUser == 277 || idUser == 478 || idUser == 276 || idUser == 568 || idUser == 6)
                {
                    this.typePhanquyen.Value = "1";
                    this.typeChucvu.Value = "2";
                    this.type.Value = "4";
                }
                else
                {
                    this.typePhanquyen.Value = "2";
                }
                //Kiểm tra type
                //1 Trưởng. phó các bộ phận
                //2 Phòng nhân sự
                //3 ban giám đốc
                
                this.iduser.Value = idUser.ToString();
                Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
                UngVien uv = blc_user.GetUngVienByID(GuiId);
                this.IDNTD = GuiId;
                // RenderPopupDanhGiatuyenDung.aspx ? id = 0578d596 - afcc - 40e9 - acc9 - 61eb161e7618 & type = 9StmH0IFKNw % 3d

                ThongTinNhanSu ttns = blc_user.GetTTNhansu_byID(uv.IdNhanVien.Value);
                //Kiểm tra tồn tại
                Danhgiathuviec dgtv = blc_user.CheckDanhGiaThuViec(GuiId);
                if (dgtv != null)
                {
                    this.ipNguoiDanhGia.Value = dgtv.NguoiDanhGia;
                    this.ipChucVuDanhGia.Value = dgtv.ChucVuDanhGia;
                }
               

                if (Request.QueryString["type"] != null)
                {
                    var encrypt = Utility.Decrypt(Request.QueryString["type"].ToString());
                    var getsplit = encrypt.Split('-');
                   // this.type.Value = getsplit[1];
                   // this.iduser.Value = getsplit[0];
                    TUser tuser = blc_user.GetUser_ByIDAll(int.Parse(getsplit[0]));
                    //this.nguoidanhgia.InnerText = tuser.UserName;
                    //if (tuser.UserType == 7)
                    //    this.chucvu.InnerText = "Nhân Viên";
                    //if (tuser.UserType == 3)
                    //    this.chucvu.InnerText = "Ban Giám Đốc";
                    //if (tuser.UserType == 4)
                    //    this.chucvu.InnerText = "Trưởng phòng";
                    //if (tuser.UserType == 5)
                    //    this.chucvu.InnerText = "Phó phòng";
                    //if (tuser.UserType == 6)
                    //    this.chucvu.InnerText = "Trưởng Nhóm";
                }
                else
                {
                    //this.type.Value = "3";
                  
                }
                if (ttns != null)
                {
                    this.masonhanvien.InnerText = ttns.MaNV +" Giới tính: "+uv.GioiTinh;
                }
                this.hotennhanvien.InnerText = uv.HoTen;
                ThongTinViTri ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);
                this.vitrilamviec.InnerText = ThongTinViTri != null ? ThongTinViTri.ViTriungTuyen : "";
                YeuCauTuyenDung yctd = blc_user.GetYeuCauTD_ByID(uv.IdYeuCau.Value);
                PhongBan pb = blc_user.GetPhongBan_ByID(yctd.IDPhongBan.Value);
                this.bophanphong.InnerText = pb.TenPhong;

                //Điền thông tin
                var getitem = blc_user.GetDanhgiathuviec(GuiId) ;
                if (getitem != null && getitem.DanhGia!=null)
                {
                    VM_DanhGiaThuViec dguvs= JsonConvert.DeserializeObject<VM_DanhGiaThuViec>(getitem.DanhGia);
                    this.TinhThanTrachNhiemPercent.Value = dguvs.TinhThanTrachNhiemPercent;
                    this.TinhThanTrachNhiemCongViec.Value = dguvs.TinhThanTrachNhiemCongViec;
                    if (dguvs.TinhThanTrachNhiemDanhGia == 1)
                        this.TinhThanTrachNhiemDanhGia1.Checked = true;
                    if (dguvs.TinhThanTrachNhiemDanhGia == 2)
                        this.TinhThanTrachNhiemDanhGia2.Checked = true;
                    if (dguvs.TinhThanTrachNhiemDanhGia == 3)
                        this.TinhThanTrachNhiemDanhGia3.Checked = true;
                    if (dguvs.TinhThanTrachNhiemDanhGia == 4)
                        this.TinhThanTrachNhiemDanhGia4.Checked = true;
                    //
                    this.MucDoHoanthanhPercent.Value = dguvs.MucDoHoanthanhPercent;
                    this.MucDoHoanthanhCongViec.Value = dguvs.MucDoHoanthanhCongViec;
                    if (dguvs.MucDoHoanthanhDanhGia == 1)
                        this.MucDoHoanthanhCongViec1.Checked = true;
                    if (dguvs.MucDoHoanthanhDanhGia == 2)
                        this.MucDoHoanthanhCongViec2.Checked = true;
                    if (dguvs.MucDoHoanthanhDanhGia == 3)
                        this.MucDoHoanthanhCongViec3.Checked = true;
                    if (dguvs.MucDoHoanthanhDanhGia == 4)
                        this.MucDoHoanthanhCongViec4.Checked = true;
                    //
                    this.ThoiGianHoanThanhPercent.Value = dguvs.ThoiGianHoanThanhPercent;
                    this.ThoiGianHoanThanhCongViec.Value = dguvs.ThoiGianHoanThanhCongViec;
                    if (dguvs.ThoiGianHoanThanhDanhGia == 1)
                        this.ThoiGianHoanThanhDanhGia1.Checked = true;
                    if (dguvs.ThoiGianHoanThanhDanhGia == 2)
                        this.ThoiGianHoanThanhDanhGia2.Checked = true;
                    if (dguvs.ThoiGianHoanThanhDanhGia == 3)
                        this.ThoiGianHoanThanhDanhGia3.Checked = true;
                    if (dguvs.ThoiGianHoanThanhDanhGia == 4)
                        this.ThoiGianHoanThanhDanhGia4.Checked = true;
                    //
                    this.SuHieuBietPercent.Value = dguvs.SuHieuBietPercent;
                    this.SuHieuBietCongViec.Value = dguvs.SuHieuBietCongViec;
                    if (dguvs.SuHieuBietDanhGia == 1)
                        this.SuHieuBietDanhGia1.Checked = true;
                    if (dguvs.SuHieuBietDanhGia == 2)
                        this.SuHieuBietDanhGia2.Checked = true;
                    if (dguvs.SuHieuBietDanhGia == 3)
                        this.SuHieuBietDanhGia3.Checked = true;
                    if (dguvs.SuHieuBietDanhGia == 4)
                        this.SuHieuBietDanhGia4.Checked = true;
                    //
                    this.KyNangChuyenMonPercent.Value = dguvs.KyNangChuyenMonPercent;
                    this.KyNangChuyenMonCongViec.Value = dguvs.KyNangChuyenMonCongViec;
                    if (dguvs.KyNangChuyenMonDanhGia == 1)
                        this.KyNangChuyenMonDanhGia1.Checked = true;
                    if (dguvs.KyNangChuyenMonDanhGia == 2)
                        this.KyNangChuyenMonDanhGia2.Checked = true;
                    if (dguvs.KyNangChuyenMonDanhGia == 3)
                        this.KyNangChuyenMonDanhGia3.Checked = true;
                    if (dguvs.KyNangChuyenMonDanhGia == 4)
                        this.KyNangChuyenMonDanhGia4.Checked = true;
                    //
                    this.SuChuDongPercent.Value = dguvs.SuChuDongPercent;
                    this.SuChuDongCongViec.Value = dguvs.SuChuDongCongViec;
                    if (dguvs.SuChuDongDanhGia == 1)
                        this.SuChuDongDanhGia1.Checked = true;
                    if (dguvs.SuChuDongDanhGia == 2)
                        this.SuChuDongDanhGia2.Checked = true;
                    if (dguvs.SuChuDongDanhGia == 3)
                        this.SuChuDongDanhGia3.Checked = true;
                    if (dguvs.SuChuDongDanhGia == 4)
                        this.SuChuDongDanhGia4.Checked = true;
                    //
                    this.KhaNangLamViecPercent.Value = dguvs.KhaNangLamViecPercent;
                    this.KhaNangLamViecCongViec.Value = dguvs.KhaNangLamViecCongViec;
                    if (dguvs.KhaNangLamViecDanhGia == 1)
                        this.KhaNangLamViecDanhGia1.Checked = true;
                    if (dguvs.KhaNangLamViecDanhGia == 2)
                        this.KhaNangLamViecDanhGia2.Checked = true;
                    if (dguvs.KhaNangLamViecDanhGia == 3)
                        this.KhaNangLamViecDanhGia3.Checked = true;
                    if (dguvs.KhaNangLamViecDanhGia == 4)
                        this.KhaNangLamViecDanhGia4.Checked = true;
                    //
                    this.TinhThanhoTroPercent.Value = dguvs.TinhThanhoTroPercent;
                    this.TinhThanhoTroCongViec.Value = dguvs.TinhThanhoTroCongViec;
                    if (dguvs.TinhThanhoTroDanhGia == 1)
                        this.TinhThanhoTroDanhGia1.Checked = true;
                    if (dguvs.TinhThanhoTroDanhGia == 2)
                        this.TinhThanhoTroDanhGia2.Checked = true;
                    if (dguvs.TinhThanhoTroDanhGia == 3)
                        this.TinhThanhoTroDanhGia3.Checked = true;
                    if (dguvs.TinhThanhoTroDanhGia == 4)
                        this.TinhThanhoTroDanhGia4.Checked = true;
                    //
                    this.HocHoiPercent.Value = dguvs.HocHoiPercent;
                    this.HocHoiTroCongViec.Value = dguvs.HocHoiTroCongViec;
                    if (dguvs.HocHoiTroDanhGia == 1)
                        this.HocHoiTroDanhGia1.Checked = true;
                    if (dguvs.HocHoiTroDanhGia == 2)
                        this.HocHoiTroDanhGia2.Checked = true;
                    if (dguvs.HocHoiTroDanhGia == 3)
                        this.HocHoiTroDanhGia3.Checked = true;
                    if (dguvs.HocHoiTroDanhGia == 4)
                        this.HocHoiTroDanhGia4.Checked = true;
                    //
                    this.GiaoTiepVoiKHPercent.Value = dguvs.GiaoTiepVoiKHPercent;
                    this.GiaoTiepVoiKHCongViec.Value = dguvs.GiaoTiepVoiKHCongViec;
                    if (dguvs.GiaoTiepVoiKHDanhGia == 1)
                        this.GiaoTiepVoiKHDanhGia1.Checked = true;
                    if (dguvs.GiaoTiepVoiKHDanhGia == 2)
                        this.GiaoTiepVoiKHDanhGia2.Checked = true;
                    if (dguvs.GiaoTiepVoiKHDanhGia == 3)
                        this.GiaoTiepVoiKHDanhGia3.Checked = true;
                    if (dguvs.GiaoTiepVoiKHDanhGia == 4)
                        this.GiaoTiepVoiKHDanhGia4.Checked = true;
                    //
                    this.MoiQuanHeDNPercent.Value = dguvs.MoiQuanHeDNPercent;
                    this.MoiQuanHeDNCongViec.Value = dguvs.MoiQuanHeDNCongViec;
                    if (dguvs.MoiQuanHeDNDanhGia == 1)
                        this.MoiQuanHeDNDanhGia1.Checked = true;
                    if (dguvs.MoiQuanHeDNDanhGia == 2)
                        this.MoiQuanHeDNDanhGia2.Checked = true;
                    if (dguvs.MoiQuanHeDNDanhGia == 3)
                        this.MoiQuanHeDNDanhGia3.Checked = true;
                    if (dguvs.MoiQuanHeDNDanhGia == 4)
                        this.MoiQuanHeDNDanhGia4.Checked = true;
                    //
                    this.XuLyTinhHuongPercent.Value = dguvs.XuLyTinhHuongPercent;
                    this.XuLyTinhHuongCongViec.Value = dguvs.XuLyTinhHuongCongViec;
                    if (dguvs.XuLyTinhHuongDanhGia == 1)
                        this.XuLyTinhHuongDanhGia1.Checked = true;
                    if (dguvs.XuLyTinhHuongDanhGia == 2)
                        this.XuLyTinhHuongDanhGia2.Checked = true;
                    if (dguvs.XuLyTinhHuongDanhGia == 3)
                        this.XuLyTinhHuongDanhGia3.Checked = true;
                    if (dguvs.XuLyTinhHuongDanhGia == 4)
                        this.XuLyTinhHuongDanhGia4.Checked = true;
                    //
                    this.KhaNangSangTaoPercent.Value = dguvs.KhaNangSangTaoPercent;
                    this.KhaNangSangTaoCongViec.Value = dguvs.KhaNangSangTaoCongViec;
                    if (dguvs.KhaNangSangTaoDanhGia == 1)
                        this.KhaNangSangTaoDanhGia1.Checked = true;
                    if (dguvs.KhaNangSangTaoDanhGia == 2)
                        this.KhaNangSangTaoDanhGia2.Checked = true;
                    if (dguvs.KhaNangSangTaoDanhGia == 3)
                        this.KhaNangSangTaoDanhGia3.Checked = true;
                    if (dguvs.KhaNangSangTaoDanhGia == 4)
                        this.KhaNangSangTaoDanhGia4.Checked = true;
                    //
                    this.ChapHanhMenhLenhPercent.Value = dguvs.ChapHanhMenhLenhPercent;
                    this.ChapHanhMenhLenhCongViec.Value = dguvs.ChapHanhMenhLenhCongViec;
                    if (dguvs.ChapHanhMenhLenhDanhGia == 1)
                        this.ChapHanhMenhLenhDanhGia1.Checked = true;
                    if (dguvs.ChapHanhMenhLenhDanhGia == 2)
                        this.ChapHanhMenhLenhDanhGia2.Checked = true;
                    if (dguvs.ChapHanhMenhLenhDanhGia == 3)
                        this.ChapHanhMenhLenhDanhGia3.Checked = true;
                    if (dguvs.ChapHanhMenhLenhDanhGia == 4)
                        this.ChapHanhMenhLenhDanhGia4.Checked = true;
                    //
                    this.DaoDucPercent.Value = dguvs.DaoDucPercent;
                    this.DaoDucCongViec.Value = dguvs.DaoDucCongViec;
                    if (dguvs.DaoDucDanhGia == 1)
                        this.DaoDucDanhGia1.Checked = true;
                    if (dguvs.DaoDucDanhGia == 2)
                        this.DaoDucDanhGia2.Checked = true;
                    if (dguvs.DaoDucDanhGia == 3)
                        this.DaoDucDanhGia3.Checked = true;
                    if (dguvs.DaoDucDanhGia == 4)
                        this.DaoDucDanhGia4.Checked = true;
                    //
                    this.DaoDucPercent.Value = dguvs.DaoDucPercent;
                    this.DaoDucCongViec.Value = dguvs.DaoDucCongViec;
                    if (dguvs.DaoDucDanhGia == 1)
                        this.DaoDucDanhGia1.Checked = true;
                    if (dguvs.DaoDucDanhGia == 2)
                        this.DaoDucDanhGia2.Checked = true;
                    if (dguvs.DaoDucDanhGia == 3)
                        this.DaoDucDanhGia3.Checked = true;
                    if (dguvs.DaoDucDanhGia == 4)
                        this.DaoDucDanhGia4.Checked = true;
                    //
                    this.HieuRoPercent.Value = dguvs.HieuRoPercent;
                    this.HieuRoCongViec.Value = dguvs.HieuRoCongViec;
                    if (dguvs.HieuRoDanhGia == 1)
                        this.HieuRoDanhGia1.Checked = true;
                    if (dguvs.HieuRoDanhGia == 2)
                        this.HieuRoDanhGia2.Checked = true;
                    if (dguvs.HieuRoDanhGia == 3)
                        this.HieuRoDanhGia3.Checked = true;
                    if (dguvs.HieuRoDanhGia == 4)
                        this.HieuRoDanhGia4.Checked = true;
                    //
                    this.DamBaoNgayCongPercent.Value = dguvs.DamBaoNgayCongPercent;
                    this.DamBaoNgayCongCongViec.Value = dguvs.DamBaoNgayCongCongViec;
                    if (dguvs.DamBaoNgayCongDanhGia == 1)
                        this.DamBaoNgayCongDanhGia1.Checked = true;
                    if (dguvs.DamBaoNgayCongDanhGia == 2)
                        this.DamBaoNgayCongDanhGia2.Checked = true;
                    if (dguvs.DamBaoNgayCongDanhGia == 3)
                        this.DamBaoNgayCongDanhGia3.Checked = true;
                    if (dguvs.DamBaoNgayCongDanhGia == 4)
                        this.DamBaoNgayCongDanhGia4.Checked = true;
                    //Danh gia chung
                    if (dguvs.Danhgiachung == 1)
                        this.DanhgiachungDanhgia1.Checked = true;
                    if (dguvs.Danhgiachung == 2)
                        this.DanhgiachungDanhgia2.Checked = true;
                    if (dguvs.Danhgiachung == 3)
                        this.DanhgiachungDanhgia3.Checked = true;
                    if (dguvs.Danhgiachung == 4)
                        this.DanhgiachungDanhgia4.Checked = true;
                    //
                    this.Nhanxetnguoidanhgia.Value = dguvs.Nhanxetnguoidnahgia;
                    if (dguvs.TiepnhanStatus == 1)
                    {
                        this.radKhongtiepnhan.Checked = true;
                    }
                    if (dguvs.TiepnhanStatus == 2)
                    {
                        this.radTaithuviec.Checked = true;
                        this.TaithuviecTu.Value = dguvs.TaiThuViecTu;
                        this.TaithuviecDen.Value = dguvs.TaiThuViecDen; 
                    }
                    if (dguvs.TiepnhanStatus == 3)
                    {
                        this.radTiepnhanchinhthuc.Checked = true;
                        this.TiepnhanchinhthucTu.Value = dguvs.TiepNhanChinhThucTu;
                        this.TiepnhanchinhthucDen.Value = dguvs.TiepNhanChinhThucDen;
                    }

                    this.Text1.Value = dguvs.Kyten;
                    this.Text2.Value = dguvs.Khac;
                    this.Text3.Value = dguvs.NgayDanhGia;
                    //
                    this.truongphongkyten.Value = dguvs.TruongPhongKyTen;
                    this.TruongPhongHoTen.Value = dguvs.TruongPhongHoTen;
                    this.truongphongkyten.Value = dguvs.TruongPhongKyTen;
                    this.Truongphongngay.Value = dguvs.TruongPhongNgay;
                    this.truongphongnhanxet.Value = dguvs.TruongPhongNhanXet;
                    //
                    this.Hanhchanhkyten.Value = dguvs.Hanhchanhkyten;
                    this.Hanhchanhngay.Value = dguvs.Hanhchanhngay;
                    this.Hanhchanhnhanxet.Value = dguvs.Hanhchanhnhanxet;
                     

                    this.Bangiamdocnhanxet.Value = dguvs.Bangiamdocnhanxet;
                    this.BangiamdocKyten.Value = dguvs.BangiamdocKyten;
                    this.Bangiamdochoten.Value = dguvs.Bangiamdochoten;
                    this.Bangiamdocngay.Value = dguvs.Bangiamdocngay;
                    this.Bangiamdockhac.Value = dguvs.Bangiamdockhac;
                    this.Bangiamdocluong.Value = dguvs.Bangiamdocluong;
                    if (dguvs.BangiamdocdongytiepnhanStatus == 1)
                    {
                        BangiamdocdongytiepnhanTu.Value = dguvs.BangiamdocdongytiepnhanTu;
                        BangiamdocdongytiepnhanDen.Value = dguvs.BangiamdocdongytiepnhanDen;
                        this.Bangiamdocdongytiepnhan.Checked = true;
                    }
                    else
                    {
                        this.Bangiamdocykienkhac.Checked = true;
                    }
                    //
                }
                else
                {
                    this.TinhThanTrachNhiemDanhGia3.Checked = true;
                    this.MucDoHoanthanhCongViec3.Checked = true;
                    this.ThoiGianHoanThanhDanhGia3.Checked = true;
                    this.SuHieuBietDanhGia3.Checked = true;
                    this.KyNangChuyenMonDanhGia3.Checked = true;
                    this.SuChuDongDanhGia3.Checked = true;
                    this.KhaNangLamViecDanhGia3.Checked = true;
                    this.TinhThanhoTroDanhGia3.Checked = true;
                    this.HocHoiTroDanhGia3.Checked = true;
                    this.GiaoTiepVoiKHDanhGia3.Checked = true;
                    this.MoiQuanHeDNDanhGia3.Checked = true;
                    this.XuLyTinhHuongDanhGia3.Checked = true;
                    this.KhaNangLamViecDanhGia3.Checked = true;
                    this.ChapHanhMenhLenhDanhGia3.Checked = true;
                    this.DaoDucDanhGia3.Checked = true;
                    this.HieuRoDanhGia3.Checked = true;
                    this.DamBaoNgayCongDanhGia3.Checked = true;
                    this.KhaNangSangTaoDanhGia3.Checked = true;
                    this.DanhgiachungDanhgia3.Checked = true;
                }
            }
        }
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

        [WebMethod]
        public static string Login(string username, string password,Guid idungvien)
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
            var checkExist = blc_user.CheckExistDanhgiathuviec(idungvien);
            int hasdata = 0;
            if (checkExist)
            {
                hasdata = 1;
            }
            
            string result = "0," + checkLogin.UserName+","+ strVitri+','+ hasdata+","+ checkLogin.UserID;
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
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            //Type=1 Nhân viên thường
            //Type =2 Trưởng phòng 
            //Type=3 Trưởng phòng full
            //Type=4 PHCNS
            //Type=5 Ban giám đốc
            int type = int.Parse(this.type.Value);
            VM_DanhGiaThuViec vmDG = new VM_DanhGiaThuViec(); 
            Danhgiathuviec Danhgiathuviec = blc_user.CheckDanhGiaThuViec(this.IDNTD);
            if (Danhgiathuviec == null)
            {
                Danhgiathuviec = new Danhgiathuviec();
                vmDG = new VM_DanhGiaThuViec();
            }
            else
            {
                if (Danhgiathuviec.DanhGia != null)
                    vmDG = JsonConvert.DeserializeObject<VM_DanhGiaThuViec>(Danhgiathuviec.DanhGia);
            }
            Danhgiathuviec.NguoiDanhGia= Page.Request.Form["ctl00$ContentPlaceHolder1$ipNguoiDanhGia"].ToString();
            Danhgiathuviec.ChucVuDanhGia= Page.Request.Form["ctl00$ContentPlaceHolder1$ipChucVuDanhGia"].ToString();
            Danhgiathuviec.IdNhanVien = this.IDNTD;
            vmDG.TinhThanTrachNhiemPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$TinhThanTrachNhiemPercent"].ToString();
            vmDG.TinhThanTrachNhiemCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$TinhThanTrachNhiemCongViec"].ToString();
            vmDG.TinhThanTrachNhiemDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radTinhthan"].ToString().Replace("TinhThanTrachNhiemDanhGia", ""));
            //

            vmDG.MucDoHoanthanhPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$MucDoHoanthanhPercent"].ToString();
            vmDG.MucDoHoanthanhCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$MucDoHoanthanhCongViec"].ToString();
            vmDG.MucDoHoanthanhDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radMucDo"].ToString().Replace("MucDoHoanthanhCongViec", ""));

            //
            vmDG.ThoiGianHoanThanhPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$ThoiGianHoanThanhPercent"].ToString();
            vmDG.ThoiGianHoanThanhCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$ThoiGianHoanThanhCongViec"].ToString();
            vmDG.ThoiGianHoanThanhDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radThoiGian"].ToString().Replace("ThoiGianHoanThanhDanhGia", ""));

            //
            vmDG.SuHieuBietPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$SuHieuBietPercent"].ToString();
            vmDG.SuHieuBietCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$SuHieuBietCongViec"].ToString();
            vmDG.SuHieuBietDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radSuHieuBiet"].ToString().Replace("SuHieuBietDanhGia", ""));
            //
            vmDG.KyNangChuyenMonPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$KyNangChuyenMonPercent"].ToString();
            vmDG.KyNangChuyenMonCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$KyNangChuyenMonCongViec"].ToString();
            vmDG.KyNangChuyenMonDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radKyNang"].ToString().Replace("KyNangChuyenMonDanhGia", ""));
            //
            vmDG.KyNangChuyenMonPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$KyNangChuyenMonPercent"].ToString();
            vmDG.KyNangChuyenMonCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$KyNangChuyenMonCongViec"].ToString();
            vmDG.KyNangChuyenMonDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radKyNang"].ToString().Replace("KyNangChuyenMonDanhGia", ""));
            //
            vmDG.SuChuDongPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$SuChuDongPercent"].ToString();
            vmDG.SuChuDongCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$SuChuDongCongViec"].ToString();
            vmDG.SuChuDongDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radsuChuDong"].ToString().Replace("SuChuDongDanhGia", ""));
            //
            vmDG.KhaNangLamViecPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$KhaNangLamViecPercent"].ToString();
            vmDG.KhaNangLamViecCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$KhaNangLamViecCongViec"].ToString();
            vmDG.KhaNangLamViecDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radKhaNangLam"].ToString().Replace("KhaNangLamViecDanhGia", ""));
            //
            vmDG.TinhThanhoTroPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$TinhThanhoTroPercent"].ToString();
            vmDG.TinhThanhoTroCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$TinhThanhoTroCongViec"].ToString();
            vmDG.TinhThanhoTroDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radTinhThanHoTro"].ToString().Replace("TinhThanhoTroDanhGia", ""));
            //
            vmDG.HocHoiPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$HocHoiPercent"].ToString();
            vmDG.HocHoiTroCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$HocHoiTroCongViec"].ToString();
            vmDG.HocHoiTroDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radHocHoi"].ToString().Replace("HocHoiTroDanhGia", ""));
            //
            vmDG.GiaoTiepVoiKHPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$GiaoTiepVoiKHPercent"].ToString();
            vmDG.GiaoTiepVoiKHCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$GiaoTiepVoiKHCongViec"].ToString();
            vmDG.GiaoTiepVoiKHDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radGiaoTiepVoiKH"].ToString().Replace("GiaoTiepVoiKHDanhGia", ""));
            //
            vmDG.MoiQuanHeDNPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$MoiQuanHeDNPercent"].ToString();
            vmDG.MoiQuanHeDNCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$MoiQuanHeDNCongViec"].ToString();
            vmDG.MoiQuanHeDNDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radMoiQuanHe"].ToString().Replace("MoiQuanHeDNDanhGia", ""));
            //
            vmDG.XuLyTinhHuongPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$XuLyTinhHuongPercent"].ToString();
            vmDG.XuLyTinhHuongCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$XuLyTinhHuongCongViec"].ToString();
            vmDG.XuLyTinhHuongDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radXulytinhhuong"].ToString().Replace("XuLyTinhHuongDanhGia", ""));
            //
            vmDG.KhaNangSangTaoPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$KhaNangSangTaoPercent"].ToString();
            vmDG.KhaNangSangTaoCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$KhaNangSangTaoCongViec"].ToString();
            vmDG.KhaNangSangTaoDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radKhanangsangtao"].ToString().Replace("KhaNangSangTaoDanhGia", ""));
            //
            vmDG.ChapHanhMenhLenhPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$ChapHanhMenhLenhPercent"].ToString();
            vmDG.ChapHanhMenhLenhCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$ChapHanhMenhLenhCongViec"].ToString();
            vmDG.ChapHanhMenhLenhDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radChapHanhMenhLenh"].ToString().Replace("ChapHanhMenhLenhDanhGia", ""));
            //
            vmDG.DaoDucPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$DaoDucPercent"].ToString();
            vmDG.DaoDucCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$DaoDucCongViec"].ToString();
            vmDG.DaoDucDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radDaoDuc"].ToString().Replace("DaoDucDanhGia", ""));
            //
            vmDG.HieuRoPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$HieuRoPercent"].ToString();
            vmDG.HieuRoCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$HieuRoCongViec"].ToString();
            vmDG.HieuRoDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radHieuRo"].ToString().Replace("HieuRoDanhGia", ""));
            //
            vmDG.DamBaoNgayCongPercent = Page.Request.Form["ctl00$ContentPlaceHolder1$DamBaoNgayCongPercent"].ToString();
            vmDG.DamBaoNgayCongCongViec = Page.Request.Form["ctl00$ContentPlaceHolder1$DamBaoNgayCongCongViec"].ToString();
            vmDG.DamBaoNgayCongDanhGia = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radDambaongaycong"].ToString().Replace("DamBaoNgayCongDanhGia", ""));
            //

            vmDG.Danhgiachung = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radDanhgiachung"].ToString().Replace("DanhgiachungDanhgia", ""));
            //
            vmDG.Nhanxetnguoidnahgia = Page.Request.Form["ctl00$ContentPlaceHolder1$Nhanxetnguoidanhgia"].ToString();
            //
            if (Page.Request.Form["ctl00$ContentPlaceHolder1$radDiLamDungGio"].ToString() == "radKhongtiepnhan")
                vmDG.TiepnhanStatus = 1;
            if (Page.Request.Form["ctl00$ContentPlaceHolder1$radDiLamDungGio"].ToString() == "radTaithuviec")
            {
                vmDG.TiepnhanStatus = 2;
                vmDG.TaiThuViecTu = Page.Request.Form["ctl00$ContentPlaceHolder1$TaithuviecTu"].ToString();
                vmDG.TaiThuViecDen = Page.Request.Form["ctl00$ContentPlaceHolder1$TaithuviecDen"].ToString();
            }
            if (Page.Request.Form["ctl00$ContentPlaceHolder1$radDiLamDungGio"].ToString() == "radTiepnhanchinhthuc")
            {
                vmDG.TiepnhanStatus = 3;
                vmDG.TiepNhanChinhThucTu = Page.Request.Form["ctl00$ContentPlaceHolder1$TiepnhanchinhthucTu"].ToString();
                vmDG.TiepNhanChinhThucDen = Page.Request.Form["ctl00$ContentPlaceHolder1$TiepnhanchinhthucDen"].ToString();
            }

            vmDG.Kyten = Page.Request.Form["ctl00$ContentPlaceHolder1$Text1"].ToString();
            vmDG.Khac = Page.Request.Form["ctl00$ContentPlaceHolder1$Text2"].ToString();
            vmDG.NgayDanhGia = Page.Request.Form["ctl00$ContentPlaceHolder1$Text3"].ToString();
            vmDG.TruongPhongKyTen = Page.Request.Form["ctl00$ContentPlaceHolder1$truongphongkyten"].ToString();
            vmDG.TruongPhongHoTen = Page.Request.Form["ctl00$ContentPlaceHolder1$TruongPhongHoTen"].ToString();
            vmDG.TruongPhongNgay = Page.Request.Form["ctl00$ContentPlaceHolder1$Truongphongngay"].ToString();
            vmDG.TruongPhongNhanXet = Page.Request.Form["ctl00$ContentPlaceHolder1$truongphongnhanxet"].ToString();
            //Hanh chanh
            vmDG.Hanhchanhkyten = Page.Request.Form["ctl00$ContentPlaceHolder1$Hanhchanhkyten"].ToString();
            vmDG.Hanhchanhngay = Page.Request.Form["ctl00$ContentPlaceHolder1$Hanhchanhngay"].ToString();
            vmDG.Hanhchanhnhanxet = Page.Request.Form["ctl00$ContentPlaceHolder1$Hanhchanhnhanxet"].ToString();
            //
            if (Page.Request.Form["ctl00$ContentPlaceHolder1$radBangiamdoc"]!=null && Page.Request.Form["ctl00$ContentPlaceHolder1$radBangiamdoc"].ToString() == "Bangiamdocdongytiepnhan")
            {
                vmDG.BangiamdocdongytiepnhanStatus = 1;
                vmDG.BangiamdocdongytiepnhanTu = Page.Request.Form["ctl00$ContentPlaceHolder1$Text2"].ToString();
            }
            else
                vmDG.BangiamdocdongytiepnhanStatus = 2;
            vmDG.Bangiamdockhac = Page.Request.Form["ctl00$ContentPlaceHolder1$Bangiamdockhac"].ToString();
            vmDG.Bangiamdocnhanxet = Page.Request.Form["ctl00$ContentPlaceHolder1$Bangiamdocnhanxet"].ToString();
            vmDG.BangiamdocKyten = Page.Request.Form["ctl00$ContentPlaceHolder1$BangiamdocKyten"].ToString();
            vmDG.Bangiamdochoten = Page.Request.Form["ctl00$ContentPlaceHolder1$Bangiamdochoten"].ToString();
            vmDG.Bangiamdocngay = Page.Request.Form["ctl00$ContentPlaceHolder1$Bangiamdocngay"].ToString();
            vmDG.Bangiamdocluong = Page.Request.Form["ctl00$ContentPlaceHolder1$Bangiamdocluong"].ToString();
            vmDG.BangiamdocdongytiepnhanTu = Page.Request.Form["ctl00$ContentPlaceHolder1$BangiamdocdongytiepnhanTu"].ToString();
            vmDG.BangiamdocdongytiepnhanDen = Page.Request.Form["ctl00$ContentPlaceHolder1$BangiamdocdongytiepnhanDen"].ToString();
            //  
            Danhgiathuviec.DanhGia = JsonConvert.SerializeObject(vmDG);
            blc_user.UpdateDanhgiathuviec(Danhgiathuviec);
            //Cập nhật đánh giá thử việc
            var iduser = 0;
            iduser = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$iduser"].ToString());
            if (iduser==null)
            {
                type = 3;
                iduser = int.Parse(this.UserMemberID.ToString());
            }
            blc_user.Capnhattrangthaidanhgia(this.IDNTD, type, iduser);
            Alert.Show("Cập nhật thành công");
            string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
            base.Response.Write(close);
        }
    }
}