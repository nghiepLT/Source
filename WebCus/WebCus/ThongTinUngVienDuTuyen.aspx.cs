using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.DAC;
using PQT.DAC.ViewModel;
using UserMng.BLC;
using Newtonsoft.Json;

namespace WebCus
{
    public partial class ThongTinUngVienDuTuyen : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    this.IDNTD = Guid.Parse(Request.QueryString["id"].ToString());
                    UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
                    if (uv != null && string.IsNullOrEmpty(uv.Email))
                        SelectFrist();
                    this.radDocThan.Checked = true;
                    LoadFormData();
                }
                 
            }
            
        }
        private void SelectFrist()
        {
            radViTinh3.Checked = true;
            radLanhDao3.Checked = true;
            radGaiQuyet3.Checked = true;
            radLamViec3.Checked = true;
            radTrinhBay3.Checked = true;
            radSinhHoat3.Checked = true;
            radHoatDong3.Checked = true;
            //
            radSuDung1.Checked = true;
            radSuDung.Value = "1";
            radPhuCap.Value = "1";
            radPhuCap1.Checked = true;
            radDienThoaiDiDong.Value = "1";
            radDienThoaiDiDong1.Checked = true;
            radTienThuong.Value = "1";
            radTienThuong1.Checked = true;
            radtienVay.Value = "1";
            radtienVay1.Checked = true;
        }
        private void LoadFormData()
        { 

            UngVien ungvien = blc_user.GetUngVienByID(this.IDNTD);

            
                //Quyền lợi
                if (!string.IsNullOrEmpty(ungvien.QuyenLoiNoiLamViec))
            {
                QuyenLoiNoiLamViec QuyenLoiNoiLamViec = JsonConvert.DeserializeObject<QuyenLoiNoiLamViec>(ungvien.QuyenLoiNoiLamViec);
                if (QuyenLoiNoiLamViec.DuocSuDungXe == 1)
                {
                    this.radSuDung1.Checked = true;
                    this.radSuDung.Value = "1";
                }
                if (QuyenLoiNoiLamViec.DuocSuDungXe == 2)
                {
                    this.radSuDung2.Checked = true;
                    this.radSuDung.Value = "2";
                }
                if (QuyenLoiNoiLamViec.DuocSuDungXe == 3)
                {
                    this.radSuDung3.Checked = true;
                    this.radSuDung.Value = "3";
                }
                //
                if (QuyenLoiNoiLamViec.PhuCapDiLai == 1)
                {
                    this.radPhuCap1.Checked = true;
                    this.radPhuCap.Value = "1";
                }
                if (QuyenLoiNoiLamViec.PhuCapDiLai == 2)
                {
                    this.radPhuCap2.Checked = true;
                    this.radPhuCap.Value = "2";
                }
                if (QuyenLoiNoiLamViec.PhuCapDiLai == 3)
                {
                    this.radPhuCap3.Checked = true;
                    this.radPhuCap.Value = "3";
                }
                //
                if (QuyenLoiNoiLamViec.DienThoai == 1)
                {
                    this.radDienThoaiDiDong1.Checked = true;
                    this.radDienThoaiDiDong.Value = "1";
                }
                if (QuyenLoiNoiLamViec.DienThoai == 2)
                {
                    this.radDienThoaiDiDong2.Checked = true;
                    this.radDienThoaiDiDong.Value = "2";
                }
                if (QuyenLoiNoiLamViec.DienThoai == 3)
                {
                    this.radDienThoaiDiDong3.Checked = true;
                    this.radDienThoaiDiDong.Value = "3";
                }
                //
                if (QuyenLoiNoiLamViec.TienThuong == 1)
                {
                    this.radTienThuong1.Checked = true;
                    this.radTienThuong.Value = "1";
                }
                if (QuyenLoiNoiLamViec.TienThuong == 2)
                {
                    this.radTienThuong2.Checked = true;
                    this.radTienThuong.Value = "2";
                }
                if (QuyenLoiNoiLamViec.TienThuong == 3)
                {
                    this.radTienThuong3.Checked = true;
                    this.radTienThuong.Value = "3";
                }
                //
                if (QuyenLoiNoiLamViec.TienVay == 1)
                {
                    this.radtienVay1.Checked = true;
                    this.radtienVay.Value = "1";
                }
                if (QuyenLoiNoiLamViec.TienVay == 2)
                {
                    this.radtienVay2.Checked = true;
                    this.radtienVay.Value = "2";
                }
                if (QuyenLoiNoiLamViec.TienVay == 3)
                {
                    this.radtienVay3.Checked = true;
                    this.radtienVay.Value = "3";
                }
                this.MuctieuPhatTrien.Value = QuyenLoiNoiLamViec.MucTieuPhatTrien;
                this.ViSaoBanMuon.Value = QuyenLoiNoiLamViec.ViSaoBanMuon;
            }
            //TinhCachCaNhan

            if (!string.IsNullOrEmpty(ungvien.TinhCachCaNhan))
            {
                TinhCachCaNhan TinhCachCaNhan = JsonConvert.DeserializeObject<TinhCachCaNhan>(ungvien.TinhCachCaNhan);
                this.diemyeu.Value = TinhCachCaNhan.DiemYeu;
                this.diemmanh.Value = TinhCachCaNhan.DiemManh;
                this.nangnlucvuottroi.Value = TinhCachCaNhan.NangLucVuotTroi;
            }
          
            //Kỹ năng
           
            //Vi tính
            if (!string.IsNullOrEmpty(ungvien.KyNang))
            {
                KyNang KyNang = JsonConvert.DeserializeObject<KyNang>(ungvien.KyNang);
                if (KyNang.ViTinh == "Giỏi")
                {
                    this.radViTinh1.Checked = true;
                    this.knViTinh.Value = "Giỏi";
                }
                if (KyNang.ViTinh == "Khá")
                {
                    this.knViTinh.Value = "Khá";
                    this.radViTinh2.Checked = true;
                }
                if (KyNang.ViTinh == "TB")
                {
                    this.knViTinh.Value = "TB";
                    this.radViTinh3.Checked = true;
                }
                if (KyNang.ViTinh == "Yếu")
                {
                    this.knViTinh.Value = "Yếu";
                    this.radViTinh4.Checked = true;
                }
                this.ViTinhGhiChu.Value = KyNang.ViTinhghiChu;
                //Lãnh đạo
                if (KyNang.LanhDao == "Giỏi")
                {
                    this.knLanhDao.Value = "Giỏi";
                    this.radLanhDao1.Checked = true;
                }
                if (KyNang.LanhDao == "Khá")
                {
                    this.knLanhDao.Value = "Khá";
                    this.radLanhDao2.Checked = true;
                }

                if (KyNang.LanhDao == "TB")
                {
                    this.knLanhDao.Value = "TB";
                    this.radLanhDao3.Checked = true;
                }
                if (KyNang.LanhDao == "Yếu")
                {
                    this.knLanhDao.Value = "Yếu";
                    this.radLanhDao4.Checked = true;
                }
                this.LanhDaoGhiChu.Value = KyNang.LanhDaoghiChu;
                //Giai quyết vấn đề
                if (KyNang.GiaiQuyetVanDe == "Giỏi")
                {
                    this.knGiaiQuyet.Value = "Giỏi";
                    this.radGaiQuyet1.Checked = true;
                }
                if (KyNang.GiaiQuyetVanDe == "Khá")
                {
                    this.knGiaiQuyet.Value = "Khá";
                    this.radGaiQuyet2.Checked = true;
                }
                if (KyNang.GiaiQuyetVanDe == "TB")
                {
                    this.knGiaiQuyet.Value = "TB";
                    this.radGaiQuyet3.Checked = true;
                }
                if (KyNang.GiaiQuyetVanDe == "Yếu")
                {
                    this.knGiaiQuyet.Value = "Yếu";
                    this.radGaiQuyet4.Checked = true;
                }
                this.GiaiQuyetGhiChu.Value = KyNang.GiaiQuyetVanDeGhiChu;
                //Trình bày 
                if (KyNang.TrinhBay == "Giỏi")
                {
                    this.knTrinhBay.Value = "Giỏi";
                    this.radTrinhBay1.Checked = true;
                }
                if (KyNang.TrinhBay == "Khá")
                {
                    this.knTrinhBay.Value = "Khá";
                    this.radTrinhBay2.Checked = true;
                }
                if (KyNang.TrinhBay == "TB")
                {
                    this.knTrinhBay.Value = "TB";
                    this.radTrinhBay3.Checked = true;
                }
                if (KyNang.TrinhBay == "Yếu")
                {
                    this.knTrinhBay.Value = "Yếu";
                    this.radTrinhBay4.Checked = true;
                }
                this.TrinhBayGhiChu.Value = KyNang.TrinhBayGhiChu;
                //Làm việc độc lập 
                if (KyNang.LamViecDocLap == "Giỏi")
                {
                    this.knLamViec.Value = "Giỏi";
                    this.radLamViec1.Checked = true;
                }
                if (KyNang.LamViecDocLap == "Khá")
                {
                    this.knLamViec.Value = "Khá";
                    this.radLamViec2.Checked = true;
                }
                if (KyNang.LamViecDocLap == "TB")
                {
                    this.knLamViec.Value = "TB";
                    this.radLamViec3.Checked = true;
                }
                if (KyNang.LamViecDocLap == "Yếu")
                {
                    this.knLamViec.Value = "Yếu";
                    this.radLamViec4.Checked = true;
                }
                this.LamViecGhiChu.Value = KyNang.LamViecDocLapGhiChu;
                //Sinh hoạt tập thể
                if (KyNang.SinhHoat == "Giỏi")
                {
                    this.knSinhHoat.Value = "Giỏi";
                    this.radSinhHoat1.Checked = true;
                }
                if (KyNang.SinhHoat == "Khá")
                {
                    this.knSinhHoat.Value = "Khá";
                    this.radSinhHoat2.Checked = true;
                }
                if (KyNang.SinhHoat == "TB")
                {
                    this.knSinhHoat.Value = "TB";
                    this.radSinhHoat3.Checked = true;
                }
                if (KyNang.SinhHoat == "Yếu")
                {
                    this.knSinhHoat.Value = "Yếu";
                    this.radSinhHoat4.Checked = true;
                }
                this.knSinhHoatGhiChu.Value = KyNang.SinhHoatGhiChu;
                //Hoạt động thể thao
                if (KyNang.HoatDong == "Giỏi")
                {
                    this.knHoatDong.Value = "Giỏi";
                    this.radHoatDong1.Checked = true;
                }
                if (KyNang.HoatDong == "Khá")
                {
                    this.knHoatDong.Value = "Khá";
                    this.radHoatDong2.Checked = true;
                }
                if (KyNang.HoatDong == "TB")
                {
                    this.knHoatDong.Value = "TB";
                    this.radHoatDong3.Checked = true;
                }
                if (KyNang.HoatDong == "Yếu")
                {
                    this.knHoatDong.Value = "Yếu";
                    this.radHoatDong4.Checked = true;
                }
                this.knHoatDongGhiChu.Value = KyNang.HoatDongGhiChu;
            }
            //Thông tin ứng tuyển
           
            if (!string.IsNullOrEmpty(ungvien.UngCuViTri))
            {
                ThongTinViTri ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(ungvien.UngCuViTri);
                if (ThongTinViTri != null)
                {
                    vitriungtuyen.Value = ThongTinViTri.ViTriungTuyen;
                    vitrimongmuon.Value = ThongTinViTri.ViTriMongMuonKhac;
                    thoigianbatdau.Value = ThongTinViTri.Thoigianbatdau != null ? ThongTinViTri.Thoigianbatdau.ToString() : "";
                    if (ThongTinViTri.LoaiNguontin == 1)
                        lcinternet.Value = ThongTinViTri.Nguontin;
                    if (ThongTinViTri.LoaiNguontin == 2)
                        lcdvvl.Value = ThongTinViTri.Nguontin;
                    if (ThongTinViTri.LoaiNguontin == 3)
                        lcgioithieuboi.Value = ThongTinViTri.Nguontin;
                    if (ThongTinViTri.LoaiNguontin == 4)
                        lckhac.Value = ThongTinViTri.Nguontin;
                    mucluongdenghi.Value = ThongTinViTri.MucLuong!=null? ThongTinViTri.MucLuong.ToString():"";
                }
              
            }

            //Load thông tin ca nhân
            this.HoTen.Value = ungvien.HoTen;
            this.NgaySinh.Value = ungvien.NgaySinh.HasValue?ungvien.NgaySinh.Value.ToShortDateString().ToString():"";
            this.NoiSinh.Value = ungvien.NoiSinh;
            this.GioiTinh.Value = ungvien.GioiTinh;
            this.Email.Value = ungvien.Email;
            this.SoDt.Value = ungvien.SoDt;
            this.Dantoc.Value = ungvien.Dantoc;
            this.Tongiao.Value = ungvien.Tongiao;
            this.CMND.Value = ungvien.CMND;
            this.NgayCMND.Value = ungvien.NgayCMND.HasValue? ungvien.NgayCMND.Value.ToShortDateString():"";
            this.NoiCapCMND.Value = ungvien.NoiCapCMND;
            this.DCTamTru.Value = ungvien.DCTamTru;
            this.DCThuongTru.Value = ungvien.DCThuongTru;
            //2. GIA ĐÌNH:
           
            if (!string.IsNullOrEmpty(ungvien.GiaDinh))
            {
                GiaDinh GiaDinh = JsonConvert.DeserializeObject<GiaDinh>(ungvien.GiaDinh);

                if (GiaDinh.TinhTrangHonNhan == 1)
                {
                    radLapGiaDinh.Checked = true;
                }
                else
                {
                    radDocThan.Checked = true;
                }
                if (GiaDinh.VoChong != null)
                {
                    this.hotenvochong.Value = GiaDinh.VoChong.HoTenVoChong;
                    this.namsinhvochong.Value = GiaDinh.VoChong.NamSinh.ToString();
                    this.congtactai.Value = GiaDinh.VoChong.CongTacTai;
                }
                if (GiaDinh.lstCon != null && GiaDinh.lstCon.Count() > 0)
                {
                    var lst = GiaDinh.lstCon.OrderBy(m => m.NamSinh);
                    for (int i = 0; i < GiaDinh.lstCon.Count(); i++)
                    {
                        if (i == 0)
                        {
                            this.hotencon1.Value = GiaDinh.lstCon[i].HoTen;
                            this.namsinhcon1.Value = GiaDinh.lstCon[i].NamSinh.ToString();
                            this.hoctaitruong1.Value = GiaDinh.lstCon[i].HocTaiTruong;
                        }
                        if (i == 1)
                        {
                            this.hotencon2.Value = GiaDinh.lstCon[i].HoTen;
                            this.namsinhcon2.Value = GiaDinh.lstCon[i].NamSinh.ToString();
                            this.hoctaitruong2.Value = GiaDinh.lstCon[i].HocTaiTruong;
                        }
                        if (i == 2)
                        {
                            this.hotencon3.Value = GiaDinh.lstCon[i].HoTen;
                            this.namsinhcon3.Value = GiaDinh.lstCon[i].NamSinh.ToString();
                            this.hoctaitruong3.Value = GiaDinh.lstCon[i].HocTaiTruong;
                        }
                    }
                }
                if (GiaDinh.Nguoilienhe != null)
                {
                    this.tennlh.Value = GiaDinh.Nguoilienhe.HoTen;
                    this.moiqh.Value = GiaDinh.Nguoilienhe.Moiquanhe;
                    this.diachinlh.Value = GiaDinh.Nguoilienhe.DiaChiCuNgu;
                    this.dtnlh.Value = GiaDinh.Nguoilienhe.DienThoaiLienLac;
                }
            }
            
            //5. QUÁ TRÌNH ĐÀO TẠO
            if (!string.IsNullOrEmpty(ungvien.QuaTrinhDaoTao))
            {
                Quatrinhdaotao Quatrinhdaotao = JsonConvert.DeserializeObject<Quatrinhdaotao>(ungvien.QuaTrinhDaoTao);

                if(Quatrinhdaotao.Vanbang== "PTTH")
                {
                    this.RadioButtonList1.SelectedIndex = 0;
                }
                if (Quatrinhdaotao.Vanbang == "Cao đẳng")
                {
                    this.RadioButtonList1.SelectedIndex = 2;
                }
                if (Quatrinhdaotao.Vanbang == "Cao học")
                {
                    this.RadioButtonList1.SelectedIndex = 4;
                }
                if (Quatrinhdaotao.Vanbang == "Trung cấp")
                {
                    this.RadioButtonList1.SelectedIndex =1;
                }
                if (Quatrinhdaotao.Vanbang == "Đại học")
                {
                    this.RadioButtonList1.SelectedIndex = 3;
                }
                if (Quatrinhdaotao.Vanbang == "Tiến sĩ")
                {
                    this.RadioButtonList1.SelectedIndex =5;
                }
                this.namtotnghiep.Value = Quatrinhdaotao.Namtonghiep;
                this.vanbang.Value = Quatrinhdaotao.Vanbang;
                this.tentruong.Value = Quatrinhdaotao.Tentruong;
                this.nganhhoc.Value = Quatrinhdaotao.Nganhhoc;
                this.xeploai.Value = Quatrinhdaotao.Xeploai; 
                //
               
                if(Quatrinhdaotao.lstVanbang!=null && Quatrinhdaotao.lstVanbang.Count() > 0)
                {
                    for (int i=0;i< Quatrinhdaotao.lstVanbang.Count(); i++)
                    {
                        if (i == 0)
                        {
                            this.vpcc1.Value = Quatrinhdaotao.lstVanbang[i].Ten;
                            this.nganhhoc1.Value = Quatrinhdaotao.lstVanbang[i].NganhHoc;
                            this.thoigiandaotao1.Value = Quatrinhdaotao.lstVanbang[i].Thoigiandaotao;
                            this.namtotnghiep1.Value = Quatrinhdaotao.lstVanbang[i].Namtotnghiep.ToString();
                            this.xeploai1.Value = Quatrinhdaotao.lstVanbang[i].XepLoai;
                            this.noicap1.Value = Quatrinhdaotao.lstVanbang[i].NoiCap;
                        }
                        if (i == 1)
                        {
                            this.vpcc2.Value = Quatrinhdaotao.lstVanbang[i].Ten;
                            this.nganhhoc2.Value = Quatrinhdaotao.lstVanbang[i].NganhHoc;
                            this.thoigiandaotao2.Value = Quatrinhdaotao.lstVanbang[i].Thoigiandaotao;
                            this.namtotnghiep2.Value = Quatrinhdaotao.lstVanbang[i].Namtotnghiep.ToString();
                            this.xeploai2.Value = Quatrinhdaotao.lstVanbang[i].XepLoai;
                            this.noicap2.Value = Quatrinhdaotao.lstVanbang[i].NoiCap;
                        }
                    }
                }
                //Ngoai ngu
                if (Quatrinhdaotao.lstNgoaiNgu != null && Quatrinhdaotao.lstNgoaiNgu.Count() > 0)
                {
                    for (int i = 0; i < Quatrinhdaotao.lstNgoaiNgu.Count(); i++)
                    {
                        if (i == 0)
                        {
                            this.NgheAnh.Value = Quatrinhdaotao.lstNgoaiNgu[i].Nghe.ToString();
                            this.NoiAnh.Value = Quatrinhdaotao.lstNgoaiNgu[i].Noi.ToString();
                            this.DocAnh.Value = Quatrinhdaotao.lstNgoaiNgu[i].Doc.ToString();
                            this.VietAnh.Value = Quatrinhdaotao.lstNgoaiNgu[i].Viet.ToString();
                            this.GhiChuAnh.Value = Quatrinhdaotao.lstNgoaiNgu[i].GhiChu.ToString();
                        }
                        
                    }
                }

            }
            //
            if (!string.IsNullOrEmpty(ungvien.QuaTrinhLamViec))
            {
                List<QuaTrinhLamViec> QuaTrinhLamViec = JsonConvert.DeserializeObject<List<QuaTrinhLamViec>>(ungvien.QuaTrinhLamViec);
                if (QuaTrinhLamViec != null && QuaTrinhLamViec.Count() > 0)
                {
                    for (int i = 0; i < QuaTrinhLamViec.Count(); i++)
                    {
                        if (i == 0)
                        {
                            this.tencongty1.Value = QuaTrinhLamViec[i].TenCongTy;
                            this.linhvuchd1.Value = QuaTrinhLamViec[i].NganhNgheHoatDong;
                            this.diachicongty1.Value = QuaTrinhLamViec[i].DiaChi;
                            this.congtytu1.Value = QuaTrinhLamViec[i].Fromdate;
                            this.congtyden1.Value = QuaTrinhLamViec[i].Todate;
                            this.chuvumoivao1.Value = QuaTrinhLamViec[i].Chucvumoivao;
                            this.chucvusaucung1.Value = QuaTrinhLamViec[i].Chucvusaucung;
                            this.luongkhoidiem1.Value = QuaTrinhLamViec[i].Luongkhoidiem.ToString();
                            this.luongsaucung1.Value = QuaTrinhLamViec[i].Luongsaucung.ToString();
                            this.nhiemvuchinh1.Value = QuaTrinhLamViec[i].Trachnhiem.ToString();
                            this.nqlhoten1.Value = QuaTrinhLamViec[i].HotenNQL.ToString();
                            this.nqlchucvu1.Value = QuaTrinhLamViec[i].ChucvuNQL.ToString();
                            this.nqldienthoai.Value = QuaTrinhLamViec[i].DienthoaiNQl.ToString();
                            this.lydonghiviec1.Value = QuaTrinhLamViec[i].LyDoNghiViec.ToString();
                        }
                        if (i == 1)
                        {
                            this.tencongty2.Value = QuaTrinhLamViec[i].TenCongTy;
                            this.linhvuchoatdong2.Value = QuaTrinhLamViec[i].NganhNgheHoatDong;
                            this.diachicongty21.Value = QuaTrinhLamViec[i].DiaChi;
                            this.congtytu2.Value = QuaTrinhLamViec[i].Fromdate;
                            this.congtyden2.Value = QuaTrinhLamViec[i].Todate;
                            this.chucvumoivao2.Value = QuaTrinhLamViec[i].Chucvumoivao;
                            this.chucvusaucung2.Value = QuaTrinhLamViec[i].Chucvusaucung;
                            this.luongkhoidiem2.Value = QuaTrinhLamViec[i].Luongkhoidiem.ToString();
                            this.luongsaucung2.Value = QuaTrinhLamViec[i].Luongsaucung.ToString();
                            this.nhiemvuchinh21.Value = QuaTrinhLamViec[i].Trachnhiem.ToString();
                            this.nqlhoten2.Value = QuaTrinhLamViec[i].HotenNQL.ToString();
                            this.nqlchucvu2.Value = QuaTrinhLamViec[i].ChucvuNQL.ToString();
                            this.nqldienthoai2.Value = QuaTrinhLamViec[i].DienthoaiNQl.ToString();
                            this.lydonghiviec2.Value = QuaTrinhLamViec[i].LyDoNghiViec.ToString();
                        }
                    }
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
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {

            var data = Page.Request.Form["ctl00$ContentPlaceHolder1$knViTinh"].ToString();
            
            //Thông tin cá nhân
            UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
            if (uv == null)
                uv = new UngVien();
            VM_UngVien VMungvien = new VM_UngVien();
            //Quyền lợi
            QuyenLoiNoiLamViec QuyenLoiNoiLamViec = new QuyenLoiNoiLamViec();
            QuyenLoiNoiLamViec.DuocSuDungXe = Page.Request.Form["ctl00$ContentPlaceHolder1$radSuDung"]!=null? int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radSuDung"].ToString()):0;
            QuyenLoiNoiLamViec.PhuCapDiLai = Page.Request.Form["ctl00$ContentPlaceHolder1$radPhuCap"]!=null? int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radPhuCap"].ToString()):0;
            QuyenLoiNoiLamViec.DienThoai = Page.Request.Form["ctl00$ContentPlaceHolder1$radDienThoaiDiDong"]!=null? int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radDienThoaiDiDong"].ToString()):0;
            QuyenLoiNoiLamViec.TienThuong = Page.Request.Form["ctl00$ContentPlaceHolder1$radTienThuong"]!=null? int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radTienThuong"].ToString()):0;
            QuyenLoiNoiLamViec.TienVay = Page.Request.Form["ctl00$ContentPlaceHolder1$radtienVay"]!=null? int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$radtienVay"].ToString()):0;
            QuyenLoiNoiLamViec.MucTieuPhatTrien = Page.Request.Form["ctl00$ContentPlaceHolder1$MuctieuPhatTrien"].ToString();
            QuyenLoiNoiLamViec.ViSaoBanMuon = Page.Request.Form["ctl00$ContentPlaceHolder1$ViSaoBanMuon"].ToString();
            VMungvien.QuyenLoiNoiLamViec = QuyenLoiNoiLamViec;
            //TinhCachCaNhan
            TinhCachCaNhan TinhCachCaNhan = new TinhCachCaNhan();
            TinhCachCaNhan.DiemYeu= Page.Request.Form["ctl00$ContentPlaceHolder1$diemyeu"].ToString();
            TinhCachCaNhan.DiemManh = Page.Request.Form["ctl00$ContentPlaceHolder1$diemmanh"].ToString();
            TinhCachCaNhan.NangLucVuotTroi = Page.Request.Form["ctl00$ContentPlaceHolder1$nangnlucvuottroi"].ToString();
            VMungvien.TinhCachCaNhan = TinhCachCaNhan;
            //Kỹ năng
            KyNang KyNang = new KyNang();
            KyNang.ViTinh= Page.Request.Form["ctl00$ContentPlaceHolder1$knViTinh"].ToString();
            KyNang.ViTinhghiChu= Page.Request.Form["ctl00$ContentPlaceHolder1$ViTinhGhiChu"].ToString();
            KyNang.LanhDao= Page.Request.Form["ctl00$ContentPlaceHolder1$knLanhDao"].ToString();
            KyNang.LanhDaoghiChu = Page.Request.Form["ctl00$ContentPlaceHolder1$LanhDaoGhiChu"].ToString();
            KyNang.GiaiQuyetVanDe = Page.Request.Form["ctl00$ContentPlaceHolder1$knGiaiQuyet"].ToString();
            KyNang.GiaiQuyetVanDeGhiChu = Page.Request.Form["ctl00$ContentPlaceHolder1$GiaiQuyetGhiChu"].ToString();
            KyNang.TrinhBay = Page.Request.Form["ctl00$ContentPlaceHolder1$knTrinhBay"].ToString();
            KyNang.TrinhBayGhiChu = Page.Request.Form["ctl00$ContentPlaceHolder1$TrinhBayGhiChu"].ToString();
            KyNang.LamViecDocLap = Page.Request.Form["ctl00$ContentPlaceHolder1$knLamViec"].ToString();
            KyNang.LamViecDocLapGhiChu = Page.Request.Form["ctl00$ContentPlaceHolder1$LamViecGhiChu"].ToString();
            KyNang.SinhHoat = Page.Request.Form["ctl00$ContentPlaceHolder1$knSinhHoat"].ToString();
            KyNang.SinhHoatGhiChu = Page.Request.Form["ctl00$ContentPlaceHolder1$knSinhHoatGhiChu"].ToString();
            KyNang.HoatDong = Page.Request.Form["ctl00$ContentPlaceHolder1$knHoatDong"].ToString();
            KyNang.HoatDongGhiChu = Page.Request.Form["ctl00$ContentPlaceHolder1$knHoatDongGhiChu"].ToString();
            VMungvien.KyNang = KyNang;

            //Thông tin vị trí ứng tuyển
            if (!string.IsNullOrEmpty(Page.Request.Form["ctl00$ContentPlaceHolder1$vitriungtuyen"].ToString()))
            {
                //ThongTinViTri ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);
                ThongTinViTri ThongTinViTri = new ThongTinViTri();

                ThongTinViTri.ViTriungTuyen = Page.Request.Form["ctl00$ContentPlaceHolder1$vitriungtuyen"].ToString();
                ThongTinViTri.ViTriMongMuonKhac = Page.Request.Form["ctl00$ContentPlaceHolder1$vitrimongmuon"].ToString();
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$thoigianbatdau"].ToString() != "")
                    ThongTinViTri.Thoigianbatdau = Page.Request.Form["ctl00$ContentPlaceHolder1$thoigianbatdau"].ToString();
                ThongTinViTri.MucLuong = Page.Request.Form["ctl00$ContentPlaceHolder1$mucluongdenghi"].ToString();
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$lcinternet"].ToString() != "")
                {
                    ThongTinViTri.Nguontin = Page.Request.Form["ctl00$ContentPlaceHolder1$lcinternet"].ToString();
                    ThongTinViTri.LoaiNguontin = 1;
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$lcdvvl"].ToString() != "")
                {
                    ThongTinViTri.Nguontin = Page.Request.Form["ctl00$ContentPlaceHolder1$lcdvvl"].ToString();
                    ThongTinViTri.LoaiNguontin = 2;
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$lcgioithieuboi"].ToString() != "")
                {
                    ThongTinViTri.Nguontin = Page.Request.Form["ctl00$ContentPlaceHolder1$lcgioithieuboi"].ToString();
                    ThongTinViTri.LoaiNguontin = 3;
                }
                if (Page.Request.Form["ctl00$ContentPlaceHolder1$lckhac"].ToString() != "")
                {
                    ThongTinViTri.Nguontin = Page.Request.Form["ctl00$ContentPlaceHolder1$lckhac"].ToString();
                    ThongTinViTri.LoaiNguontin = 4;
                }
                VMungvien.ThongTinViTri = ThongTinViTri;
            }
            var idPhongban = blc_user.GetYeuCauTD_ByID(uv.IdYeuCau.Value).IDPhongBan.Value;
            uv.PhongBan = blc_user.GetPhongBan_ByID(idPhongban).TenPhong;
            uv.HoTen = Page.Request.Form["ctl00$ContentPlaceHolder1$HoTen"].ToString();
            if (Page.Request.Form["ctl00$ContentPlaceHolder1$NgaySinh"].ToString() != "")
                uv.NgaySinh = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$NgaySinh"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            uv.NoiSinh = Page.Request.Form["ctl00$ContentPlaceHolder1$NoiSinh"].ToString();
            uv.GioiTinh = Page.Request.Form["ctl00$ContentPlaceHolder1$GioiTinh"].ToString();
            uv.Email = Page.Request.Form["ctl00$ContentPlaceHolder1$Email"].ToString();
            uv.SoDt = Page.Request.Form["ctl00$ContentPlaceHolder1$SoDt"].ToString();
            uv.Dantoc = Page.Request.Form["ctl00$ContentPlaceHolder1$Dantoc"].ToString();
            uv.Tongiao = Page.Request.Form["ctl00$ContentPlaceHolder1$Tongiao"].ToString();
            uv.DCTamTru = Page.Request.Form["ctl00$ContentPlaceHolder1$DCTamTru"].ToString();
            uv.DCThuongTru = Page.Request.Form["ctl00$ContentPlaceHolder1$DCThuongTru"].ToString();
            uv.CMND = Page.Request.Form["ctl00$ContentPlaceHolder1$CMND"].ToString();
            if (Page.Request.Form["ctl00$ContentPlaceHolder1$NgayCMND"].ToString() != "")
                uv.NgayCMND = DateTime.ParseExact(Page.Request.Form["ctl00$ContentPlaceHolder1$NgayCMND"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            uv.NoiCapCMND = Page.Request.Form["ctl00$ContentPlaceHolder1$NoiCapCMND"].ToString();
            VMungvien.UngVien = uv;

            //2. GIA ĐÌNH:
            GiaDinh GiaDinh = new GiaDinh();
            GiaDinh.TinhTrangHonNhan = Page.Request.Form["ctl00$ContentPlaceHolder1$radHonnhan"].ToString() == "radLapGiaDinh" ? 1 : 2;
            VoChong vochong = new VoChong();
            if (Page.Request.Form["ctl00$ContentPlaceHolder1$hotenvochong"].ToString() != "")
            {
                vochong.HoTenVoChong = Page.Request.Form["ctl00$ContentPlaceHolder1$hotenvochong"].ToString();
                vochong.NamSinh = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$namsinhvochong"].ToString());
                vochong.CongTacTai = Page.Request.Form["ctl00$ContentPlaceHolder1$congtactai"].ToString();
                GiaDinh.VoChong = vochong;
            }

            if (hotencon1.Value != "")
            {
                Con con = new Con();
                con.HoTen = Page.Request.Form["ctl00$ContentPlaceHolder1$hotencon1"].ToString();
                con.NamSinh = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$namsinhcon1"].ToString());
                con.HocTaiTruong = Page.Request.Form["ctl00$ContentPlaceHolder1$hoctaitruong1"].ToString();
                GiaDinh.lstCon.Add(con);
            }
            if (hotencon2.Value != "")
            {
                Con con = new Con();
                con.HoTen = Page.Request.Form["ctl00$ContentPlaceHolder1$hotencon2"].ToString();
                con.NamSinh = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$namsinhcon2"].ToString());
                con.HocTaiTruong = Page.Request.Form["ctl00$ContentPlaceHolder1$hoctaitruong2"].ToString();
                GiaDinh.lstCon.Add(con);
            }
            if (hotencon3.Value != "")
            {
                Con con = new Con();
                con.HoTen = Page.Request.Form["ctl00$ContentPlaceHolder1$hotencon3"].ToString();
                con.NamSinh = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$namsinhcon3"].ToString());
                con.HocTaiTruong = Page.Request.Form["ctl00$ContentPlaceHolder1$hoctaitruong3"].ToString();
                GiaDinh.lstCon.Add(con);
            }

            Nguoilienhe nlh = new Nguoilienhe();
            nlh.HoTen = Page.Request.Form["ctl00$ContentPlaceHolder1$tennlh"].ToString();
            nlh.Moiquanhe = Page.Request.Form["ctl00$ContentPlaceHolder1$moiqh"].ToString();
            nlh.DienThoaiLienLac = Page.Request.Form["ctl00$ContentPlaceHolder1$dtnlh"].ToString();
            nlh.DiaChiCuNgu = Page.Request.Form["ctl00$ContentPlaceHolder1$diachinlh"].ToString();
            GiaDinh.Nguoilienhe = nlh;
            VMungvien.GiaDinh = GiaDinh;

            //5. QUÁ TRÌNH ĐÀO TẠO
            Quatrinhdaotao qtdt = new Quatrinhdaotao();
            qtdt.Vanbang = Page.Request.Form["ctl00$ContentPlaceHolder1$vanbang"].ToString();
            qtdt.Namtonghiep = Page.Request.Form["ctl00$ContentPlaceHolder1$namtotnghiep"].ToString();
            qtdt.Nganhhoc = Page.Request.Form["ctl00$ContentPlaceHolder1$nganhhoc"].ToString();
            qtdt.Xeploai = Page.Request.Form["ctl00$ContentPlaceHolder1$xeploai"].ToString();
            qtdt.Tentruong = Page.Request.Form["ctl00$ContentPlaceHolder1$tentruong"].ToString();
            NgoaiNgu nn = new NgoaiNgu();
            if (NgheAnh.Value != "")
            {
                nn.TenNgoaiNgu = "Anh";
                nn.Nghe = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$NgheAnh"].ToString());
                nn.Noi = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$NoiAnh"].ToString());
                nn.Doc = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$DocAnh"].ToString());
                nn.Viet = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$VietAnh"].ToString());
                nn.GhiChu = Page.Request.Form["ctl00$ContentPlaceHolder1$GhiChuAnh"].ToString();
                qtdt.lstNgoaiNgu.Add(nn);
            }
            
            // Vanbangkhac 1
           
            if (vpcc1.Value != "")
            {
                Vanbangkhac Vanbangkhac = new Vanbangkhac();
                Vanbangkhac.Ten = Page.Request.Form["ctl00$ContentPlaceHolder1$vpcc1"].ToString();
                Vanbangkhac.NganhHoc = Page.Request.Form["ctl00$ContentPlaceHolder1$nganhhoc1"].ToString();
                Vanbangkhac.Thoigiandaotao = Page.Request.Form["ctl00$ContentPlaceHolder1$thoigiandaotao1"].ToString();
                Vanbangkhac.Namtotnghiep = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$namtotnghiep1"].ToString());
                Vanbangkhac.XepLoai = Page.Request.Form["ctl00$ContentPlaceHolder1$xeploai1"].ToString();
                Vanbangkhac.NoiCap = Page.Request.Form["ctl00$ContentPlaceHolder1$noicap1"].ToString();
                qtdt.lstVanbang.Add(Vanbangkhac);
            }
           
            // Vanbangkhac2
            if (vpcc2.Value != "")
            {
                Vanbangkhac Vanbangkhac = new Vanbangkhac();
                Vanbangkhac.Ten = Page.Request.Form["ctl00$ContentPlaceHolder1$vpcc2"].ToString();
                Vanbangkhac.NganhHoc = Page.Request.Form["ctl00$ContentPlaceHolder1$nganhhoc2"].ToString();
                Vanbangkhac.Thoigiandaotao = Page.Request.Form["ctl00$ContentPlaceHolder1$thoigiandaotao2"].ToString();
                Vanbangkhac.Namtotnghiep = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$namtotnghiep2"].ToString());
                Vanbangkhac.XepLoai = Page.Request.Form["ctl00$ContentPlaceHolder1$xeploai2"].ToString();
                Vanbangkhac.NoiCap = Page.Request.Form["ctl00$ContentPlaceHolder1$noicap2"].ToString();
                qtdt.lstVanbang.Add(Vanbangkhac);
            }
            VMungvien.Quatrinhdaotao = qtdt;
            //6. QUÁ TRÌNH LÀM VIỆC
           
            if (tencongty1.Value != "")
            {
                QuaTrinhLamViec qtlv = new QuaTrinhLamViec();
                qtlv.TenCongTy = Page.Request.Form["ctl00$ContentPlaceHolder1$tencongty1"].ToString();
                qtlv.DiaChi = Page.Request.Form["ctl00$ContentPlaceHolder1$diachicongty1"].ToString() ;
                qtlv.NganhNgheHoatDong = Page.Request.Form["ctl00$ContentPlaceHolder1$linhvuchd1"].ToString();
                qtlv.Fromdate = Page.Request.Form["ctl00$ContentPlaceHolder1$congtytu1"].ToString();
                qtlv.Todate = Page.Request.Form["ctl00$ContentPlaceHolder1$congtyden1"].ToString();
                qtlv.Chucvumoivao = Page.Request.Form["ctl00$ContentPlaceHolder1$chuvumoivao1"].ToString();
                qtlv.Chucvusaucung = Page.Request.Form["ctl00$ContentPlaceHolder1$chucvusaucung1"].ToString();
                qtlv.Trachnhiem = Page.Request.Form["ctl00$ContentPlaceHolder1$nhiemvuchinh1"].ToString();
                qtlv.Luongkhoidiem = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$luongkhoidiem1"].Replace(",", "").ToString());
                qtlv.Luongsaucung = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$luongsaucung1"].Replace(",", "").ToString());
                qtlv.HotenNQL = Page.Request.Form["ctl00$ContentPlaceHolder1$nqlhoten1"].ToString();
                qtlv.ChucvuNQL = Page.Request.Form["ctl00$ContentPlaceHolder1$nqlchucvu1"].ToString();
                qtlv.DienthoaiNQl = Page.Request.Form["ctl00$ContentPlaceHolder1$nqldienthoai"].ToString();
                qtlv.LyDoNghiViec = Page.Request.Form["ctl00$ContentPlaceHolder1$lydonghiviec1"].ToString();
                VMungvien.lstQuaTrinhLamViec.Add(qtlv);
            }
            if (tencongty2.Value != "")
            {
                QuaTrinhLamViec qtlv = new QuaTrinhLamViec();
                qtlv.TenCongTy = Page.Request.Form["ctl00$ContentPlaceHolder1$tencongty2"].ToString();
                qtlv.DiaChi = Page.Request.Form["ctl00$ContentPlaceHolder1$diachicongty21"].ToString();
                qtlv.NganhNgheHoatDong = Page.Request.Form["ctl00$ContentPlaceHolder1$linhvuchoatdong2"].ToString();
                qtlv.Fromdate = Page.Request.Form["ctl00$ContentPlaceHolder1$congtytu2"].ToString();
                qtlv.Todate = Page.Request.Form["ctl00$ContentPlaceHolder1$congtyden2"].ToString();
                qtlv.Chucvumoivao = Page.Request.Form["ctl00$ContentPlaceHolder1$chucvumoivao2"].ToString();
                qtlv.Chucvusaucung = Page.Request.Form["ctl00$ContentPlaceHolder1$chucvusaucung2"].ToString();
                qtlv.Trachnhiem = Page.Request.Form["ctl00$ContentPlaceHolder1$nhiemvuchinh21"].ToString();
                qtlv.Luongkhoidiem = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$luongkhoidiem2"].Replace(",", "").ToString());
                qtlv.Luongsaucung = int.Parse(Page.Request.Form["ctl00$ContentPlaceHolder1$luongsaucung2"].Replace(",", "").ToString());
                qtlv.HotenNQL = Page.Request.Form["ctl00$ContentPlaceHolder1$nqlhoten2"].ToString();
                qtlv.ChucvuNQL = Page.Request.Form["ctl00$ContentPlaceHolder1$nqlchucvu2"].ToString();
                qtlv.DienthoaiNQl = Page.Request.Form["ctl00$ContentPlaceHolder1$nqldienthoai2"].ToString();
                qtlv.LyDoNghiViec = Page.Request.Form["ctl00$ContentPlaceHolder1$lydonghiviec2"].ToString();
                VMungvien.lstQuaTrinhLamViec.Add(qtlv);
            }
            var result = blc_user.InsertUngVien(VMungvien);
            Alert.Show("Cập nhật thành công!");
            LoadFormData();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string string1 = RadioButtonList1.SelectedItem.Text;
            vanbang.Value = string1; 
        }
    }
}