using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQT.DAC.ViewModel
{
    public class VM_YeuCauTuyenDung
    {
        public int IdYeuCau { get; set; }
        public int IdNguoiTao { get; set; }
        public string TieuDe { get; set; }
        public DateTime NgayTao { get; set; }
        public int TrangThai { get ; set; }
        public string Description { get; set; }
        public string TenPhong { get; set; }
        public int TrucThuoc { get; set; }
        public string Files { get; set; }
        public string HoTen { get; set; }
        public int Soluong { get; set; }
        public string Reason { get; set; }
        public string TrangThaiTuyenDung { get; set; }
        public int IsDone { get; set; }
    }
}
