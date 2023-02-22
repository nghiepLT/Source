using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQT.DAC.ViewModel
{
    public class VM_Thongketuyendung
    {
        public int STT { get; set; }
        public string  TrucThuoc { get; set; }  
        public string PhongBan { get; set; }
        public string HoTenUV { get; set; }
        public string ViTriTuyenDung { get; set; }
    }
    public class VM_ThongKePhongBan
    {
        public string PhongBan { get; set; }
        public int SLTuyenDung { get; set; }
        public int Dangtuyendung { get; set; }
        public string TenYCTD { get; set; }
        public int TuyenThatBai { get; set; }
        public int TuyenThanhCong { get; set; }
    }

    public class VM_ThongKeTheoNguonTD
    {
        public string TenNguon { get; set; }
        public int SoLuong { get; set; }
    }
}
