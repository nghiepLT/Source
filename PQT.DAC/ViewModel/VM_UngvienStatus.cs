using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQT.DAC.ViewModel
{
    public class VM_UngvienStatus
    {
        public Guid Id { get; set; }
        public string HoTen { get; set; }
        public DateTime CreatedDate { get; set; } 
        public int ThongTinUngVien { get; set; }
        public string NgayNhapThongTin { get; set; }
        public int PhongVan { get; set; }
        public string NgayPV { get; set; }
        public int DanhGia { get; set; }
        public string NgayDanhgia { get; set; }
        public int GuiThumoi { get; set; }
        public string NgayGuithumoi { get; set; }
        public int DongBo { get; set; }
        public string Ngaydongbo { get; set; }
        public int TomTatQuyTrinh { get; set; }
        public string NgayTomTatQuyTrinh { get; set; }
        public int TomTatPhucLoi { get; set; }
        public string NgayTomTatPhucLoi { get; set; }
        public int Khaosathoinhap { get; set; }
        public string NgayKhaosatHoiNhap { get; set; }
        public string Phongban { get; set; }
        public int TrucThuoc { get; set; }
        public string TrangThai { get; set; }
        public int TrangThaiYCTD { get; set; }
        public string TieuDe { get; set; }
        public string TMNVPath { get; set; }
        public int TrangThaiUngVien { get; set; }
        public int TrangThaiPhongVan { get; set; }
        public int TrangThaiNhanViec { get; set; }
    }
}
