using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQT.DAC.ViewModel
{
    public class VM_UngVien
    {
        public UngVien UngVien { get; set; }
        public ThongTinViTri ThongTinViTri { get; set; } = new ThongTinViTri();
        public GiaDinh GiaDinh { get; set; } = new GiaDinh();
        public Quatrinhdaotao Quatrinhdaotao { get; set; } = new Quatrinhdaotao();
        public TinhCachCaNhan TinhCachCaNhan { get; set; } = new TinhCachCaNhan();
        public List<QuaTrinhLamViec> lstQuaTrinhLamViec { get; set; } = new List<QuaTrinhLamViec>();
        public KyNang KyNang { get; set; } = new KyNang();
        public QuyenLoiNoiLamViec QuyenLoiNoiLamViec { get; set; } = new QuyenLoiNoiLamViec();
        public BanKSPhongVan BanKSPhongVan { get; set; } = new BanKSPhongVan();
    }
    public class QuyenLoiNoiLamViec
    {
        public int DuocSuDungXe { get; set; }
        public int PhuCapDiLai { get; set; }
        public int DienThoai { get; set; }
        public int TienThuong { get; set; }
        public int TienVay { get; set; }
        public string MucTieuPhatTrien { get; set; }
        public string ViSaoBanMuon { get; set; }
    }
    public class ThongTinViTri
    {
        public string ViTriungTuyen { get; set; }
        public string ViTriMongMuonKhac { get; set; }
        public string Thoigianbatdau { get; set; }
        public string Nguontin { get; set; }
        public int LoaiNguontin { get; set; }
        public string MucLuong { get; set; }
    }
    #region GiaDinh
    public class GiaDinh
    {
        public int TinhTrangHonNhan { get; set; }
        public VoChong VoChong { get; set; }
        public List<Con> lstCon { get; set; } = new List<Con>();
        public Nguoilienhe Nguoilienhe { get; set; } = new Nguoilienhe();
    }
    public class VoChong
    {
        public string HoTenVoChong { get; set; }
        public int NamSinh { get; set; }
        public string CongTacTai { get; set; }
    }
    public class Nguoilienhe
    {
        public string HoTen { get; set; }
        public string Moiquanhe { get; set; }
        public string DienThoaiLienLac { get; set; }
        public string DiaChiCuNgu { get; set; }
    }
    public class Con
    {
        public string HoTen { get; set; }
        public int NamSinh { get; set; }
        public string HocTaiTruong { get; set; }
    }
    #endregion

    #region Quá trình đào tạo
    public class Quatrinhdaotao
    {
        public string Vanbang { get; set; }
        public string Namtonghiep { get; set; }
        public string Tentruong { get; set; }
        public string Nganhhoc { get; set; }
        public string Xeploai { get; set; }
        public List<Vanbangkhac> lstVanbang { get; set; } = new List<Vanbangkhac>();
        public List<NgoaiNgu> lstNgoaiNgu { get; set; } = new List<NgoaiNgu>();
    }
    public class Vanbangkhac
    {
        public string Ten { get; set; }
        public string NganhHoc { get; set; }
        public string Thoigiandaotao { get; set; }
        public int Namtotnghiep { get; set; }
        public string XepLoai { get; set; }
        public string NoiCap { get; set; }
    }
    public class NgoaiNgu
    {
        public string TenNgoaiNgu { get; set; }
        public int Nghe { get; set; }
        public int Noi { get; set; }
        public int Doc { get; set; }
        public int Viet { get; set; }
        public string GhiChu { get; set; }
    }
    public class TinhCachCaNhan
    {
        public string DiemYeu { get; set; }
        public string DiemManh { get; set; }
        public string NangLucVuotTroi { get; set; }
    }
    public class QuaTrinhLamViec
    {
        public string TenCongTy { get; set; }
        public string DiaChi { get; set; }
        public string DienThoaiCongTy { get; set; }
        public string NganhNgheHoatDong { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public string Chucvumoivao { get; set; }
        public string Chucvusaucung { get; set; }
        public string Luongkhoidiem { get; set; }
        public string Luongsaucung { get; set; }
        public string Trachnhiem { get; set; }
        public string HotenNQL { get; set; }
        public string ChucvuNQL { get; set; }
        public string DienthoaiNQl { get; set; }
        public string LyDoNghiViec { get; set; }
    }
    public class KyNang
    {
        public string ViTinh { get; set; }
        public string ViTinhghiChu { get; set; }
        public string LanhDao { get; set; }
        public string LanhDaoghiChu { get; set; }
        public string GiaiQuyetVanDe { get; set; }
        public string GiaiQuyetVanDeGhiChu { get; set; }
        public string TrinhBay { get; set; }
        public string TrinhBayGhiChu { get; set; }
        public string LamViecDocLap { get; set; }
        public string LamViecDocLapGhiChu { get; set; }
        public string SinhHoat { get; set; }
        public string SinhHoatGhiChu { get; set; }
        public string HoatDong { get; set; }
        public string HoatDongGhiChu { get; set; }
    }

    public class BanKSPhongVan
    {
        public string Dinhhuongcongviec { get; set; }
        public string Dinhhuongkhac { get; set; }
        public string CSLuongcung { get; set; }
        public string CSThuong { get; set; }
        public string Luubanggoc { get; set; }
        public string Thuongthamnien { get; set; }
        public string ThuongXSCV { get; set; }
        public string Thuongluong13 { get; set; }
        public string CSphucloicuoihoi { get; set; }
        public string CSphucloiquatet { get; set; }
        public string Chucmungsinhnhat { get; set; }
        public string CSmuabaohiem { get; set; }
        public string CSDaotao { get; set; }
        public string CSKhamsuckhoe { get; set; }
        public string CSMuahangtragop { get; set; }
        public string CSDulichhangnam { get; set; }
        public string CSKhach { get; set; }
        public string TCCongviec { get; set; }
        public string TCMoitruong { get; set; }
        public string TCNoilamviec { get; set; }
        public string TCThunhap { get; set; }
        public string TCChinhsach { get; set; }
        public string TCKhac { get; set; }
        public string Nguyenvongcanhan { get; set; }
    }
    #endregion
}
