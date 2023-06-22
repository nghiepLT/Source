using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PQT.DAC.ViewModel
{
    public class VM_ThongKeKhaoSat
    {
        public string HoTenUngVien { get; set; }

        public DateTime Ks7Ngay { get; set; }
        public DateTime Ks14Ngay { get; set; }
        public DateTime Ks2Thang { get; set; }
        public int Step { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public double SoNgayLam { get; set; }
    }
}
