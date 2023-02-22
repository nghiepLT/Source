using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.DAC;
using UserMng.BLC;

namespace WebCus
{
    public partial class sinhnhatnv : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime ngayoff = DateTime.Now;// DateTime.Parse("2018-12-01");
            string uvdata = "";
            IList<ThongTinNhanSu> listTTNhanSu = blc_user.RowsThongTinNhanSu_ByIDLoaiNV(1, int.MaxValue, -1);
            var ordered = from dt in listTTNhanSu
                          orderby DateTime.Parse(dt.NhanVien.NgaySinh.ToString()).Day
                          select dt;
            foreach (var item in ordered)
             {
                
                 ngayoff = DateTime.Parse(item.NhanVien.NgaySinh.ToString());                
                 if (ngayoff.Month == DateTime.Now.Month)
                 {
                     
                     uvdata += "<b>" + item.NhanVien.HoTen + "</b> - " + string.Format("{0:dd/MM/yyyy}", item.NhanVien.NgaySinh) + "<br/>";
                   //  Send_Mail("Ứng Viên Đến Hạn Thử Việc", "", EmailToHCNS);
                 }
                
             }
             lbl_name.Text = uvdata;
        }
    }
}