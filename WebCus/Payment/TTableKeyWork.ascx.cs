
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.Controls;
using PQT.API;
using System.Data;
using PQT.Common;
using System.Net.Mail;
//using ProductMng.BLC;
using UserMng.BLC;
using System.IO;
using System.Data.SqlClient;
using DocumentFormat.OpenXml;
using ExcelStream;
using OfficeOpenXml;
using System.Configuration;
using PQT.DAC;
using System.Web.Services;
using UserMng.DataDefine;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Globalization;
using System.Threading;
using Telerik.WebControls;

namespace WebCus
{
    public partial class TTableKeyWork : CommonUserControl
    {

        int findex, lindex;
        
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_BLC blc_users = new UserMng_BLC();
        PagedDataSource pgsource = new PagedDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Action = "/listcase";
                btnSave.Visible = false;
                btnhuy.Visible = false;
                this.SortOption_S = 1;
                this.SortType_S = 1;
                // txt_tungay.Text = DateTime.Now.AddDays(-Utilis.TryParseInt(Config.GetConfigValue("Days_View_Order"))).ToString();
                
                txtDateFrom.SelectedDate = DateTime.Now.AddDays(-Utilis.TryParseInt(Config.GetConfigValue("Days_View_Order")));
                txtDateTo.SelectedDate = DateTime.Now.AddDays(+Utilis.TryParseInt(30));
                rad_tungay.SelectedDate = DateTime.Now.AddDays(+Utilis.TryParseInt(3));
                rad_denngay.SelectedDate = DateTime.Now.AddDays(+Utilis.TryParseInt(3));
                rad_ngay.SelectedDate = DateTime.Now;
                rad_ngaynghibuoi.SelectedDate = DateTime.Now;

               

                dr_dspb.Visible = false;
                txtSearch.Visible = false;
                IList<PhongBan> dt = blc_user.ListPhongban();
                dr_dspb.DataSource = dt;
                dr_dspb.DataTextField = "TenPhong";
                dr_dspb.DataValueField = "IDPhong";
                dr_dspb.DataBind();
                dr_dspb.AppendDataBoundItems = true;
                dr_dspb.Items.Insert(0, new ListItem("--Chọn Phòng Ban--", "-1"));
                dr_dspb.SelectedIndex = 0;

                Literal1.Text = DateTimeFormatInfo.CurrentInfo.GetDayName(DateTime.Now.DayOfWeek) + " Ngày " + DateTime.Now.ToString("dd/MM/yyyy ");

                th_buoi.Visible = false;
                td_buoi.Visible = false;
                th_songay.Visible = true;
                td_Ngaynghi.Visible = true;
                th_ngay.Visible = false;
                td_ngay.Visible = false;
                BindGirdView();
                if (this.UserMemberType == 2 || this.UserMemberType == 3 && this.UserMemberParentID == -1) { tbn_action.Visible = false;}
                if (this.UserMemberType == 2 || this.UserMemberType == 1 || this.UserMemberType == 3 && this.UserMemberParentID == -1) { td_searchname.Visible = true; }
                
                    CheckTTUVandSendemail();
               
            }
          
        }
        protected void CheckTTUVandSendemail()
        {
             
            string nameuv = "Danh sách Ứng Viên Đến Hạn Thử Việc <br/>";
            string uvdata = "";
            int i = 0;
            //string EmailToHCNS = Config.GetConfigValue("EmailToHCNS");
            string EmailToHCNS = "lethanhnghiep1993@gmail.com";
            DateTime ngayoff = DateTime.Now;// DateTime.Parse("2018-12-01");
            
             IList<ThongTinNhanSu> listTTNhanSu = blc_user.RowsThongTinNhanSu_ByIDLoaiNV(1, int.MaxValue, 2);
             foreach (var item in listTTNhanSu)
             {
                
                 ngayoff = DateTime.Parse(item.NgayVaoLam.ToString());
                 TimeSpan thoigiansd = DateTime.Now - ngayoff;
                 if (thoigiansd.Days >= 60)
                 {
                     i = i+1;
                     uvdata += i + "/ " + item.NhanVien.HoTen + "-" + string.Format("{0:dd/MM/yyyy}", item.NgayVaoLam) + "<br/>";
                   //  Send_Mail("Ứng Viên Đến Hạn Thử Việc", "", EmailToHCNS);
                 }
                
             }

          
            SendUngVienThuViec ent= new SendUngVienThuViec();
            ent.DataSend = nameuv + "<br/>" + uvdata;
            ent.DateSend = DateTime.Now;
            ent.IDUserSend = this.UserID;
            ent.ValueSend = 1;
            int idsend =blc_users.Create_SendTTUV_CHECKSEND(ent); 
            if (idsend != 0 && !string.IsNullOrEmpty(uvdata))
            {
                // Send_Mail("Ứng Viên Đến Hạn Thử Việc", nameuv + "<br/>" + uvdata, EmailToHCNS);
                //Kiểm tra nếu ứng viên đến hạn 7 ngày
                foreach (var item in listTTNhanSu)
                { 
                    ngayoff = DateTime.Parse(item.NgayVaoLam.ToString());
                    TimeSpan thoigiansd = DateTime.Now - ngayoff; 
                    if (thoigiansd.Days == 7)
                    {
                        i = i + 1;
                        // uvdata += i + "/ " + item.NhanVien.HoTen + "-" + string.Format("{0:dd/MM/yyyy}", item.NgayVaoLam) + "<br/>";

                        //TUser tuser = blc_user.
                        TUser tuser = blc_user.GetUserByNhanvienId(item.IdNhanVien.Value);
                        blc_user.Phanquyendanhgia(tuser);
                        string contentHtml = "";
                        contentHtml += "<div>";
                        contentHtml += "<table>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "<div>Chào bạn "+ "<span style='color:blue'>"+ item.NhanVien.HoTen.ToUpper()+ "</span>,</div>";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Kỹ thuật đã tạo mã portal, tài khoản đào tạo. lấy dấu vân tay cho bạn. Hàng ngày, bạn cần scan vân tay trên cả 2 máy (máy cũ + máy mới) để chấm công nhé.";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Nếu gặp khó khăn trong quá trình đăng nhập, bạn có thể liên hệ trực tiếp:";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "1-Web portal: Chương (0902.013.243)";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "2- Phần mềm Nhân sự, đào tạo: Nhân (0334.435.608).";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Có thắc mắc về các cách sử dụng, bạn có thể liên hệ trực tiếp Phòng Nhân sự để được hỗ trợ kịp thời nhé!";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        //I/ HƯỚNG DẪN TẠO PHÉP ONLINE:
                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "I/ HƯỚNG DẪN TẠO PHÉP ONLINE:";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Hiện tại, Công ty đang có 2 phần mềm đăng ký phép online. Sau khi đã lấy dấu vân tay, bất cứ phát sinh đi trễ, về sớm hay nghỉ phép, bạn đều phải tạo phép trên cả 2 phần mềm (File hướng dẫn).";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Hiện tại, Công ty đang có 2 phần mềm đăng ký phép online. Sau khi đã lấy dấu vân tay, bất cứ phát sinh đi trễ, về sớm hay nghỉ phép, bạn đều phải tạo phép trên cả 2 phần mềm (File hướng dẫn).";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "1.	Web portal: http://192.168.117.212:8011/";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Thông tin đăng nhập đã gửi qua email Công ty của bạn bao gồm tên đăng nhập và mật khẩu. ";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Tên đăng nhập: 070322NK0272";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Pass: (pass đã được gửi qua email và sẽ được yêu cầu thay đổi khi bạn đăng nhập lần đầu tiên)";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "2.	Phần mềm Nhân sự: http://nhansu.nguyenkimvn.com.vn/ hoặc đường link: http://192.168.117.250:800/login";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Tên đăng nhập: Lamtm";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Pass: 1 (pass này mặc định giống nhau, bạn có thể thay đổi pass theo nhu cầu)";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "II/ TÀI KHOẢN ĐÀO TẠO QUI TRÌNH, QUI ĐỊNH CỦA CÔNG TY:";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Đăng nhập tài khoản đào tạo qui trình, qui định của công ty, đường link: daotao.nguyenkimvn.com.vn hoặc đường link: http://192.168.117.250:801";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Tên đăng nhập: Lamtm";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "Pass: 1 (pass này mặc định giống nhau, bạn có thể thay đổi pass theo nhu cầu)";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>Sau 07 ngày làm việc, bạn vui lòng đăng nhập vào hệ thống để đánh giá.</td>";
                        contentHtml += "</tr>";
                        contentHtml += "</table>";
                        contentHtml += "</div>";
                        sendEmail7ngay(item.NhanVien.Email, "Khảo sát đánh giá sau 07 ngày làm việc - " + item.NhanVien.HoTen.ToUpper(), contentHtml);
                    }
                    if (thoigiansd.Days == 14)
                    {
                        i = i + 1;
                        // uvdata += i + "/ " + item.NhanVien.HoTen + "-" + string.Format("{0:dd/MM/yyyy}", item.NgayVaoLam) + "<br/>";

                        //TUser tuser = blc_user.
                        TUser tuser = blc_user.GetUserByNhanvienId(item.IdNhanVien.Value);
                        blc_user.Capnhatkhaosat14(tuser.IdNhansu.Value);
                        string contentHtml = "";
                        contentHtml += "<div>";
                        contentHtml += "<table>";
                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "<div>Chào bạn " + "<span style='color:blue'>" + item.NhanVien.HoTen.ToUpper() + "</span>,</div>";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>Sau 14 ngày làm việc, bạn vui lòng đăng nhập vào hệ thống để đánh giá.</td>";
                        contentHtml += "</tr>";
                        contentHtml += "</table>";
                        contentHtml += "</div>";
                        sendEmail7ngay(item.NhanVien.Email, "Khảo sát đánh giá sau 14 ngày làm việc - " + item.NhanVien.HoTen.ToUpper(), contentHtml);
                    }
                    if (thoigiansd.Days == 60)
                    {
                        i = i + 1;
                        // uvdata += i + "/ " + item.NhanVien.HoTen + "-" + string.Format("{0:dd/MM/yyyy}", item.NgayVaoLam) + "<br/>";

                        //TUser tuser = blc_user.
                        TUser tuser = blc_user.GetUserByNhanvienId(item.IdNhanVien.Value);
                        blc_user.Capnhatkhaosat2thang(tuser.IdNhansu.Value);
                        string contentHtml = "";
                        contentHtml += "<div>";
                        contentHtml += "<table>";
                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>";
                        contentHtml += "<div>Chào bạn " + "<span style='color:blue'>" + item.NhanVien.HoTen.ToUpper() + "</span>,</div>";
                        contentHtml += "</td>";
                        contentHtml += "</tr>";

                        contentHtml += "<tr style='padding:5px 0px;'>";
                        contentHtml += "<td>Sau 2 tháng làm việc, bạn vui lòng đăng nhập vào hệ thống để đánh giá.</td>";
                        contentHtml += "</tr>";
                        contentHtml += "</table>";
                        contentHtml += "</div>";
                        sendEmail7ngay(item.NhanVien.Email, "Khảo sát đánh giá sau 2 tháng làm việc - " + item.NhanVien.HoTen.ToUpper(), contentHtml);
                    }
                }
                
            }

            // Alert.Show(nameuv );

        }
        private bool sendEmail7ngay(string to, string title, string sContent)
        {
            try
            {
                string AdminEmail = Config.GetConfigValue("AdminEmailTo");
                string AdminPass = Config.GetConfigValue("AdminPass");
                string MailHost = Config.GetConfigValue("MailHost");
                string PortMailHost = Config.GetConfigValue("PortMailHost");
                int intPort = Helper.TryParseInt(PortMailHost, 25);
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
                SmtpServer.Port = intPort;
                SmtpServer.Host = MailHost;
                if (intPort == 25)
                    SmtpServer.EnableSsl = false;
                else
                    SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress(AdminEmail, Request.Url.Host.ToString(), System.Text.Encoding.UTF8);
                mail.To.Add(to);
                mail.Subject = title;
                mail.Body = sContent;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail); 
                return true;
            }
            catch (System.Exception ex)
            {
                Alert.Show(ex.Message);
                //error = ex.Message;
                return false;
            }

        }
        protected string removeduplicate(string str)
        {

            string assd = "";
            char[] arrI = str.Distinct().ToArray();
            for (int i = 0; i < arrI.Length; i++)
            {
                assd += arrI[i].ToString() + "|";

            }
            return assd;
        }


        private int CurrentPage
        {
            get
            {
                if (ViewState["g_CurrentPage"] != null)
                    return Convert.ToInt32(ViewState["g_CurrentPage"]);
                return 1;
            }
            set
            {
                ViewState["g_CurrentPage"] = value;
            }
        }

        private void BindGirdView()
        {

            int idcty = -1;// this.UserMemberLike;
            int iduserchoose = this.UserMemberID;
            int PageInt = 50;
            tbttkt.Visible = false;
            int trangthai = Utilis.TryParseInt(drop_trangthai.SelectedValue);
            int loaiphep = Utilis.TryParseInt(drop_loaipheps.SelectedValue);
           int loaisearh=Utilis.TryParseInt(dropSearchtype.SelectedValue);
            int idtrPhong = blc_user.GetIDTruongPhong_byIDuser(this.UserID);
            int idPhoPhong = blc_user.GetIDPhoPhong_byIDuser(this.UserID);
            int idtrNhom = blc_user.GetIDTruongNhom_byIDuser(this.UserID);
            int idparentPhongban = this.UserMemberParentID;

            DateTime fromDate = txtDateFrom.SelectedDate;// Convert.ToDateTime(txt_tungay.Text);
           // Alert.Show(fromDate.ToString());
            DateTime toDate =  txtDateTo.SelectedDate;//Convert.ToDateTime(txt_denngay.Text);
            if (this.UserMemberType == 1 || this.UserMemberType == 2)
            {
                idcty = -1;
                iduserchoose = -1;
                div_ngaynghithang.Visible = false;
            }
            if (this.UserMemberType == 3)
            {
                iduserchoose = -1;
                div_ngaynghithang.Visible = false;
              
            }
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                div_ngaynghithang.Visible = true;
            }
            DateTime ngaydauthang = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
           // DateTime tungay = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" +"01");
         // DataSet dss = blc_user.Casework_Rows_byID(1, int.MaxValue, ngaydauthang, ngaydauthang.AddMonths(1).AddDays(-1), -1, 2, -1, "", this.UserMemberID, this.UserMemberLike, this.UserMemberType);
            DataTable dss = blc_user.Rows_Phep_byID(ngaydauthang, ngaydauthang.AddMonths(1).AddDays(-1), 2, this.UserMemberID);
            DataSet ds = blc_user.Casework_Rows_byID(this.CurrentPage, int.MaxValue, fromDate, toDate, trangthai, loaiphep, loaisearh, txtSearch.Text.Trim().ToUpper(), iduserchoose, idcty, this.UserMemberType);
            int total = Utilis.TryParseInt(ds.Tables[0].Rows.Count);
            int chuaxl = blc_user.Casework_Rows_byID(this.CurrentPage, int.MaxValue, fromDate, toDate,0, loaiphep, loaisearh, txtSearch.Text.Trim(), iduserchoose, idcty, this.UserMemberType).Tables[0].Rows.Count;
           
            lbl_Total_Count.Text = string.Format("Có <span style='color:Blue;'> {0} </span> phép đã tìm thấy | Có <span style='color:Red;'> {1} </span> phép chưa duyệt", total, chuaxl);

            double tongngaynghi = 0;
            double ngaynghi = 0;
            foreach (DataRow dr in dss.Rows)
            {

                tongngaynghi += Convert.ToDouble(dr["SoNgayNghi"].ToString().Trim());

            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                ngaynghi += Convert.ToDouble(dr["SoNgayNghi"].ToString().Trim());

            }
            lbl_thang.Text = DateTime.Now.Month.ToString();

            //lbl_sngaynghi.Text = Utils.TryParseInt(tongngaynghi,0).ToString();
            lbl_tungay.Text = fromDate.ToShortDateString();
            lbl_denngay.Text = toDate.ToShortDateString();
            lbl_songaynghitim.Text = ngaynghi.ToString();
            lblngaynghi.Text = blc_user.GetNgayPhepNam_ByIDNV(Utilis.TryParseInt(iduserchoose)).ToString();//ngaynghi.ToString();
            lbl_sngaynghi.Text = tongngaynghi.ToString();
            this.tongngaynghitrongthang = Utils.TryParseInt(tongngaynghi, 0);
            
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                int cout = Utils.TryParseInt(ds.Tables.Count, 0);

                

                System.Data.DataView dv = new System.Data.DataView(dt);
                pgsource.DataSource = dv;
                pgsource.AllowPaging = true;
                if (dt.Rows.Count > PageInt)
                {
                    pgsource.PageSize = PageInt;
                }
                else pgsource.PageSize = dt.Rows.Count;
                pgsource.CurrentPageIndex = PageNumber;
                ViewState["totpage"] = pgsource.PageCount;
                lnkPrevious.Enabled = !pgsource.IsFirstPage;
                lnkNext.Enabled = !pgsource.IsLastPage;
                lnkFirst.Enabled = !pgsource.IsFirstPage;
                lnkLast.Enabled = !pgsource.IsLastPage;
                gvList.DataSource = pgsource;
                gvList.DataBind();
                doPaging();
     
            }
            else
            {
                gvList.DataSource = null;
                gvList.DataBind();
                //  PQTPager1.RecordCount = 0;
                lbl_Total_Count.Text = "";
            }

        }

        protected string Bind_Payment_Type(object p_status)
        {
            return ((PaymentType)Utilis.TryParseInt(p_status)).ToString().Replace("_", " ");
        }

        protected string Bind_Transaction_Type(object p_status)
        {
            return ((Transaction_Status)Utilis.TryParseInt(p_status)).ToString().Replace("_", " ");
        }

        private void Update_Status(string status_trans)
        {

            try
            {
                if (status_trans == "0")//xóa
                {
                    NgayPhep p = blc_user.GetNgayPhep_ByID(this.BillPhepID);

                    int idtao = Utils.TryParseInt(p.IDNhanvien,0);
                    if (this.UserMemberID == idtao)
                    {
                        if (blc_user.DeleteBill_byID(this.BillPhepID))
                        {
                            Alert.Show("Xóa thành công");
                            BindGirdView();
                        }
                        else Alert.Show("Xóa Thất Bại !");
                    }
                    else MessageBox.Show("Bạn Không Tạo Phép Này, Không Thể Xóa !", false, "/listcase");
                }
                if (status_trans == "1")//duyet
                {                    
                        if (blc_user.UpdateBill(this.BillPhepID, 1, txt_ghichunghu.Text, this.UserMemberID))
                        {
                            Alert.Show("Success !");
                            BindGirdView();
                        }
                        else Alert.Show("Error !");
                                    }
                if (status_trans == "2")//koo duyet
                {
                   
                        if (blc_user.UpdateBill(this.BillPhepID, 2, txt_ghichunghu.Text, this.UserMemberID))
                        {
                            Alert.Show("Success !");
                            BindGirdView();
                        }
                        else Alert.Show("Error!");                    
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region Send Mail

        private bool sendservice( string subjectEmail, string sContent,string to, string mailCC)
        {
            try
            {
                string AdminEmail = Config.GetConfigValue("AdminEmailTo");
                string AdminPass = Config.GetConfigValue("AdminPass");
                string MailHost = Config.GetConfigValue("MailHost");
                string PortMailHost = Config.GetConfigValue("PortMailHost");
                int intPort = Helper.TryParseInt(PortMailHost, 25);
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
                SmtpServer.Port = intPort;
                SmtpServer.Host = MailHost;
                if (intPort == 25)
                    SmtpServer.EnableSsl = false;
                else
                    SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(AdminEmail, Request.Url.Host.ToString(), System.Text.Encoding.UTF8);
                mail.To.Add(to);
                if (!string.IsNullOrEmpty(mailCC))
                {
                    mail.CC.Add(mailCC);
                }
                mail.Subject = subjectEmail;//"Thông tin từ website: " + Request.Url.Host.ToString();
                mail.Body = sContent;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public static bool Send_Mail(string subject, string sContent, string emailTo)
        {
            bool bFlag = false;
            try
            {
                string AdminEmail = Config.GetConfigValue("AdminEmailTo");
                string AdminPass = Config.GetConfigValue("AdminPass");
                string MailHost = Config.GetConfigValue("MailHost");
                string PortMailHost = Config.GetConfigValue("PortMailHost");
                int intPort = Helper.TryParseInt(PortMailHost, 25);
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
                SmtpServer.Port = intPort;
                SmtpServer.Host = MailHost;
                if (intPort == 25)
                    SmtpServer.EnableSsl = false;
                else
                    SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage myMessage = new MailMessage();
                myMessage.Subject = subject;
                myMessage.Body = sContent;
                myMessage.From = new MailAddress(AdminEmail,"NKCN-Nghĩ Phép");
                myMessage.To.Add(new MailAddress(emailTo));
                myMessage.IsBodyHtml = true;

                SmtpServer.Send(myMessage);
                bFlag = true;
            }
            catch (Exception ex)
            {
                bFlag = false;
            }
            return bFlag;
        }

        #endregion
        public int numpage()
        {
            int page = this.CurrentPage;
            int pagesize = 10;

            if (page > 1)
                return ((Convert.ToInt16(page) * pagesize - pagesize) > 0) ? (Convert.ToInt16(page) * pagesize - pagesize) : 0;
            return 0;
        }

        protected void Pager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.CurrentPage = e.NewPageIndex + 1;
            BindGirdView();
        }

        protected void gvList_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SortItem")
            {
                LinkButton linkSort = e.CommandSource as LinkButton;
                this.ActiveColumn_S = linkSort.ID;
                this.ActiveCss_S = linkSort.CssClass != "Active_Down" ? "Active_Down" : "Active_Up";
                string[] arr = e.CommandArgument.ToString().Split('-');
                this.SortOption_S = Convert.ToInt32(arr[0]);
                this.SortType_S = this.ActiveCss_S == "Active_Down" ? 1 : 2;
                BindGirdView();
            }
            if (e.CommandName == "capnhatphep")
            {

                this.BillPhepID = Convert.ToInt32(e.CommandArgument);

                BindKeyInfo();

            }
        }
        
        private void BindKeyInfo()
        {
            try
            {
                btnInsert.Visible = false;
                btnSave.Visible = false;
                btnhuy.Visible = false;
                NgayPhep keys = new NgayPhep();
                keys = blc_user.GetNgayPhep_ByID(this.BillPhepID);

                if (keys != null)
                {
                    if (keys.TrangThaiPhep == 1 || keys.TrangThaiPhep == 2)
                    {
                        MessageBox.Show("Phép Này Đã Được Xữ Lý !", false, "/listcase");
                    }
                    
                    tbttkt.Visible = true;
                    int tinhtrang = Utils.TryParseInt(keys.TrangThaiPhep, 0);
                    txt_songaynghi.Text = keys.SoNgayNghi.ToString();
                    rad_tungay.SelectedDate = DateTime.Parse(keys.TuNgay.ToString());
                    rad_denngay.SelectedDate = DateTime.Parse(keys.DenNgay.ToString());
                    ddlmaphep.SelectedValue = keys.LoaiPhep.ToString();

                    txtlydonghi.Text = keys.LyDoVang;
                    txt_ghichunghu.Text = keys.GhiChu;
                    rad_tungay.Enabled = false;
                    rad_denngay.Enabled = false;
                    //  txt_ghichunghu.Enabled = false;
                    txt_songaynghi.Enabled = false;
                    txtlydonghi.Enabled = false;
                    ddlmaphep.Enabled = false;
                    rad_ngaynghibuoi.Enabled = false;
                    dr_buoinghi.Enabled = false;
                    rad_ngay.Enabled = false;
                    if (keys.LoaiPhep == 1 || keys.LoaiPhep == 6)
                    {
                        th_buoi.Visible = true;
                        td_buoi.Visible = true;
                        th_songay.Visible = false;
                        td_Ngaynghi.Visible = false;
                        th_ngay.Visible = false;
                        td_ngay.Visible = false;
                        dr_buoinghi.SelectedValue = keys.BuoiNghi.Trim();
                        rad_ngaynghibuoi.SelectedDate = DateTime.Parse(keys.TuNgay.ToString());

                    }
                    if (keys.LoaiPhep == 2)
                    {
                        th_buoi.Visible = false;
                        td_buoi.Visible = false;
                        th_songay.Visible = true;
                        td_Ngaynghi.Visible = true;
                        th_ngay.Visible = false;
                        td_ngay.Visible = false;
                    }
                    else if (keys.LoaiPhep == 3 || keys.LoaiPhep == 4 || keys.LoaiPhep == 5)
                    {
                        th_buoi.Visible = false;
                        td_buoi.Visible = false;
                        th_songay.Visible = false;
                        td_Ngaynghi.Visible = false;
                        th_ngay.Visible = true;
                        td_ngay.Visible = true;
                        rad_ngay.SelectedDate = DateTime.Parse(keys.TuNgay.ToString());
                    }
                }
                else Response.Redirect("/listcase");

            }
            catch (System.Exception ex)
            {

            }
        }
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                LinkButton linkSort = e.Row.FindControl(this.ActiveColumn_S) as LinkButton;
                if (linkSort != null)
                {
                    linkSort.CssClass = this.ActiveCss_S;
                }
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime fromDate =txtDateFrom.SelectedDate;

            DateTime toDate =txtDateTo.SelectedDate;
            if (fromDate > toDate)
            {
                Alert.Show("Khoảng Thời gian chọn không phù hơp !");
                txtDateTo.Focus();

            }
            else
            {
               
                BindGirdView();
            }
        }


        protected void btnUpdate_Status_Click(object sender, EventArgs e)
        {

            Update_Status(((Button)sender).CommandArgument);

        }


        #region Property STMS

        protected string ActiveColumn_S
        {
            get
            {
                if (ViewState["g_ActiveColumn_S"] != null)
                    return Convert.ToString(ViewState["g_ActiveColumn_S"]);
                return "";
            }
            set
            {
                ViewState["g_ActiveColumn_S"] = value;
            }
        }

        protected string ActiveCss_S
        {
            get
            {
                if (ViewState["g_ActiveCss_S"] != null)
                    return Convert.ToString(ViewState["g_ActiveCss_S"]);
                return "";
            }
            set
            {
                ViewState["g_ActiveCss_S"] = value;
            }
        }


        protected int SortType_S
        {
            get
            {
                if (ViewState["g_SortType_S"] != null)
                    return Convert.ToInt32(ViewState["g_SortType_S"]);
                return 1;
            }
            set
            {
                ViewState["g_SortType_S"] = value;
            }
        }

        protected int SortOption_S
        {
            get
            {
                if (ViewState["g_SortOption_S"] != null)
                    return Convert.ToInt32(ViewState["g_SortOption_S"]);
                return 1;
            }
            set
            {
                ViewState["g_SortOption_S"] = value;
            }
        }

        #endregion

        protected void Click_upload(object sender, EventArgs e)
        {
            if (IsPostBack && Upload.HasFile)
            {
                if (Path.GetExtension(Upload.FileName).Equals(".xlsx"))
                {
                    var excel = new ExcelPackage(Upload.FileContent);

                    var dt = excel.ToDataTable();
                    var table = "Contacts";
                    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

                    // using (var conn = new SqlConnection("Server=.;Database=test;Integrated Security=SSPI"))
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        var bulkCopy = new SqlBulkCopy(conn);
                        bulkCopy.DestinationTableName = table;
                        conn.Open();
                        var schema = conn.GetSchema("Columns", new[] { null, null, table, null });
                        foreach (DataColumn sourceColumn in dt.Columns)
                        {
                            foreach (DataRow row in schema.Rows)
                            {
                                if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
                                {
                                    bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                                    break;
                                }
                            }
                        }
                        bulkCopy.WriteToServer(dt);
                    }
                }
                string script = "alert(\"Complete!\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            secsionchose = 0;
            tblData.Visible = false;
            pager_div.Visible = false;
            div_lbl.Visible = false;
            tbl_search.Visible = false;
            tbttkt.Visible = true;
            btnSave.Visible = true;
            btnhuy.Visible = true;
            btnInsert.Visible = false;
            div_button_active.Visible = false;
            btn_bosungphep.Visible = false;
            resettext();
            //ListItem removeItem = ddlmaphep.Items.FindByValue("5");
            //ddlmaphep.Items.Remove(removeItem);
            //removeItem = ddlmaphep.Items.FindByValue("4");
           // ddlmaphep.Items.Remove(removeItem);
            //removeItem = ddlmaphep.Items.FindByValue("3");
            //ddlmaphep.Items.Remove(removeItem);
        }

        protected void btnInsertUpdate_Click(object sender, EventArgs e)
        {
            this.secsionchose = 1;
            
            tblData.Visible = false;
            pager_div.Visible = false;
            div_lbl.Visible = false;
            tbl_search.Visible = false;
            tbttkt.Visible = true;
            btnSave.Visible = true;
            btnhuy.Visible = true;
            btnInsert.Visible = false;
            div_button_active.Visible = false;
            btn_bosungphep.Visible = false;
            
            resettext();
           // ListItem removeItem = ddlmaphep.Items.FindByValue("4");
          //  ddlmaphep.Items.Remove(removeItem);
          //  ddl1.Items[1].Attributes.Add("disabled", "disabled");
         
        }
        protected void resettext()
        {
            rad_tungay.Enabled = true;
            rad_denngay.Enabled = true;
            txt_ghichunghu.Enabled = true;
            txt_songaynghi.Enabled = true;
            txtlydonghi.Enabled = true;
            ddlmaphep.Enabled = true;


        }
        protected void bindetexbox()
        {

        }
        protected void hidetext()
        {

        }
        protected void btn_refrestData(object sender, EventArgs e)
        {
            Response.Redirect("/listcase");

        }

        protected void btnhuy_Click(object sender, EventArgs e)
        {
            Response.Redirect("listcase");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlmaphep.SelectedValue == "2" && string.IsNullOrEmpty(txt_songaynghi.Text))
            {

                Alert.Show("Nhập số ngày nghỉ !");
                txt_songaynghi.Focus();

            }
           else if (string.IsNullOrEmpty(txtlydonghi.Text))
            {
                Alert.Show("Lý Do Nghĩ Không Được Để Trống !");
                txtlydonghi.Focus();
            }
            else
            {
                if (ddlmaphep.SelectedValue == "3" && string.IsNullOrEmpty(txt_thoigiansomtre.Text) || ddlmaphep.SelectedValue == "4" && string.IsNullOrEmpty(txt_thoigiansomtre.Text))
                {

                    if (string.IsNullOrEmpty(txt_thoigiansomtre.Text))
                    {
                        Alert.Show("Thời Gian Đi Trễ, Về SỚm Không Được Để Trống !");
                        txt_thoigiansomtre.Focus();
                    }
                   
                }
                else
                {
                    int ditre = blc_user.GetDitreThang_ByIDNV(Utilis.TryParseInt(this.UserMemberID));
                   // int vesom = blc_user.GetVesomThang_ByIDNV(Utilis.TryParseInt(this.UserMemberID));

                    if (Utils.TryParseInt(txt_thoigiansomtre.Text, 0) > 60 && ddlmaphep.SelectedValue == "3")
                    {
                      //  Alert.Show("Bạn Phải Làm Phép Nghĩ Buổi, Không Được làm phép đi trễ !");
                        MessageBox.Show("Bạn Phải Làm Phép Nghĩ Buổi, Không Được làm phép đi trễ !", false, "/TableKeyWork.aspx");
                        //ddlmaphep.Focus();
                    }
                    else if (ditre >= 4 && ddlmaphep.SelectedValue == "3")
                    {
                        MessageBox.Show("Bạn Đã Hết Số Lần Đi Trễ Quy Định, Không Thể Tạo Phép !", false, "/TableKeyWork.aspx");
                        //Alert.Show("Bạn Đã Hết Số Lần Đi Trễ, Về Sớm Theo Quy Định, Không Thể Tạo Phép !");
                    }
                    else
                    {
                        int nguoithaythe = 0;
                        string buoinghi = "";
                        string lydonghi = txtlydonghi.Text.Trim();
                        DateTime ngay = rad_tungay.SelectedDate;
                        DateTime denngay = rad_denngay.SelectedDate;
                        string songay = txt_songaynghi.Text.Trim().Replace(".", ",");
                        int nhomduyetphep = 0;
                        int idbgd = blc_user.GetIDBGD_byIDuser(this.UserMemberID);
                        int idtrPhong = blc_user.GetIDTruongPhong_byIDuser(this.UserMemberID);
                        int idPhoPhong = blc_user.GetIDPhoPhong_byIDuser(this.UserMemberID);
                        int idtrNhom = blc_user.GetIDTruongNhom_byIDuser(this.UserMemberID);
                        string emailtrphong = blc_user.GetUse_ByID(idtrPhong) != null ? blc_user.GetUse_ByID(idtrPhong).Email.Trim() : "";
                        string emailtrNhom = blc_user.GetUse_ByID(idtrNhom) != null ? blc_user.GetUse_ByID(idtrNhom).Email.Trim() : "";
                        string emailBGD = blc_user.GetUse_ByID(idbgd) != null ? blc_user.GetUse_ByID(idbgd).Email.Trim() : "";
                        string emailphophong = blc_user.GetUse_ByID(idPhoPhong) != null ? blc_user.GetUse_ByID(idPhoPhong).Email.Trim() : "";
                        string emailhcns = "";// "thanhdtl@nguyenkimvn.vn";
                        //if (this.UserMemberLike == 2)
                        //{
                        //    emailhcns = "minh.duong@chinhnhan.vn";
                        //}
                        // Alert.Show(Utils.TryParseInt(Convert.ToDouble(songay), 0).ToString());

                        int songays = Utils.TryParseInt(songay, 0) + tongngaynghitrongthang;

                        if (rad_tungay.SelectedDate.Month != DateTime.Now.Month)
                        {
                            DateTime ngaythangselect = new DateTime(DateTime.Now.Year, rad_tungay.SelectedDate.Month, 1);
                            DataTable dss = blc_user.Rows_Phep_byID(ngaythangselect, ngaythangselect.AddMonths(1).AddDays(-1), 2, this.UserMemberID);
                            double tongngaynghi = 0;
                            foreach (DataRow dr in dss.Rows)
                            {

                                tongngaynghi += Convert.ToDouble(dr["SoNgayNghi"].ToString().Trim());

                            }

                            int ngaydanghi = Convert.ToInt32(tongngaynghi);
                            songays = Utils.TryParseInt(songay, 0) + ngaydanghi;
                        }

                        if (ddlmaphep.SelectedValue == "1" || ddlmaphep.SelectedValue == "2")
                        {
                            if (songays > 1)
                                nhomduyetphep = 1;
                        }
                        if (Utils.TryParseInt(ddlmaphep.SelectedValue.ToString(), 0) == 2)
                        {

                            if (Utils.TryParseInt(Convert.ToDouble(songay), 0) > 1)
                            {
                                nhomduyetphep = 1;
                            }
                        }
                        if (ddlmaphep.SelectedValue == "1")
                        {
                            ngay = rad_ngaynghibuoi.SelectedDate;
                            songay = "0,5";
                            buoinghi = dr_buoinghi.SelectedValue;
                            lydonghi = lydonghi + "-" + buoinghi;
                            denngay = rad_ngaynghibuoi.SelectedDate;
                        }
                        if (ddlmaphep.SelectedValue == "3" || ddlmaphep.SelectedValue == "4" || ddlmaphep.SelectedValue == "5" || ddlmaphep.SelectedValue == "6")
                        {

                            lydonghi = lydonghi + "-" + txt_thoigiansomtre.Text + " Phút";
                            ngay = rad_ngay.SelectedDate;
                            denngay = rad_ngay.SelectedDate;
                            songay = "0";
                            if (ddlmaphep.SelectedValue == "6")
                            {
                                buoinghi = dr_buoinghi.SelectedValue;
                                lydonghi = lydonghi + "-" + buoinghi;
                                ngay = rad_ngaynghibuoi.SelectedDate;
                                // ngay = rad_ngay.SelectedDate;
                                denngay = rad_ngaynghibuoi.SelectedDate;
                                songay = "0";
                                if (dr_buoinghi.SelectedValue == "Cả Ngày")
                                {
                                    ngay = rad_tungay.SelectedDate;
                                    denngay = rad_denngay.SelectedDate;
                                    lydonghi = txtlydonghi.Text.Trim() + " " + txt_songaynghi.Text.Trim().Replace(".", ",") + " ngày";

                                }
                            }
                        }

                        TimeSpan thoigiansd = DateTime.Now - denngay;
                        if (thoigiansd.Days > 4)
                        {
                            nhomduyetphep = 1;
                        }
                        if (this.UserMemberType == 4)
                        {
                            nhomduyetphep = 1;
                        }
                        int flag = 1;
                        if (blc_user.Createbill(Utils.TryParseInt(ddlmaphep.SelectedValue, 0), songay, ngay, denngay, lydonghi, txt_ghichunghu.Text.Trim(), this.UserMemberID, nguoithaythe, flag, buoinghi, nhomduyetphep))
                        {
                            string mgse = "";
                            if (ddlmaphep.SelectedValue == "1" || ddlmaphep.SelectedValue == "2")
                                mgse = "Xin Nghĩ Phép";
                            if (ddlmaphep.SelectedValue == "3")
                                mgse = "Xin Phép Đi trễ";
                            if (ddlmaphep.SelectedValue == "4")
                                mgse = "Xin Phép Về Sớm";
                            if (ddlmaphep.SelectedValue == "5")
                                mgse = "Quên Quẹt Thẻ";
                            if (ddlmaphep.SelectedValue == "6")
                                mgse = "Công Tác Ngoài";
                            flag = 0;
                            string content = string.Format("<b>" + mgse + "</b>");
                            content += "<br/>=========================<br/>";
                            content += "Họ Tên: " + "<br/>" + this.g_UserMemberName + "<br/>";
                            content += "Thời gian: " + buoinghi + "<br/>" + ngay.ToShortDateString() + " Đến Ngày " + denngay.ToShortDateString() + "<br/>";
                            content += "Lý Do:";
                            content += "<span style='padding-left:10px;'>";
                            content += txtlydonghi.Text.Trim().Replace("\r\n", "<br/>");
                            //  content += "<br/> Ghi Chú :" + txt_ghichunghu.Text;
                            content += "<span>";

                            content += "<br/>========================<br/>";
                            content += @"<span style='font-size:8pt;'>Đây là email gửi từ hệ thống, vui lòng không Reply lại địa chỉ email này.<span>";
                            if (songay == "0")
                            {
                                if (this.UserMemberType != 4 && this.UserMemberType != 5)
                                { sendservice(this.g_UserMemberName, content, emailtrphong, emailhcns); }
                                else
                                { sendservice(this.g_UserMemberName, content, emailBGD, string.Empty); }

                            }
                            else
                            {
                                if (Utils.TryParseInt(songay, 0) < 2)
                                {
                                    if (this.UserMemberType != 4 && this.UserMemberType != 5)
                                    { sendservice(this.g_UserMemberName, content, emailtrphong, emailhcns); }
                                    else Send_Mail(this.g_UserMemberName, content, emailBGD);

                                }
                                else
                                {
                                    if (this.UserMemberType != 4 && this.UserMemberType != 5)
                                    { sendservice(this.g_UserMemberName, content, emailtrphong, emailhcns); }
                                    else Send_Mail(this.g_UserMemberName, content, emailBGD);
                                }
                            }
                            // sendmail(songay, ngay.ToString("dd/MM/yyyy"), denngay.ToString("dd/MM/yyyy "), txtlydonghi.Text, txt_ghichunghu.Text, buoinghi, this.UserMemberID);
                            BindGirdView();
                            tblData.Visible = true;
                            pager_div.Visible = true;
                            div_lbl.Visible = true;
                            tbl_search.Visible = true;
                            MessageBox.Show("Sussess!", false, "/listcase");
                        }
                        else Alert.Show("Error! không thể tạo phép, vui lòng kiểm tra lại ngày phép hoặc thông tin chưa đúng !");
                    }
                }

            }
        }
        protected void sendmail(string songaynghi, string ngay,string denngay,string lydonghi,string ghichu, string buoinghi,int iduser)
        {
            try
            {
              //  string nameuser = blc_user.GetUse_ByID(iduser).UserName;
               

                //string toMailtruongphong = blc_user.GetUse_ByID(idtrPhong).Email.Trim();
                //string toMailphophong = blc_user.GetUse_ByID(idPhoPhong).Email.Trim();
                //string toMailtruongnhom = blc_user.GetUse_ByID(idtrNhom).Email.Trim();
                //string toMailBGD = blc_user.GetUse_ByID(idbgd).Email.Trim();

               
                // string toMail = Config.GetConfigValue("EmailTo");

                string content = string.Format("<b>Đơn Xin Nghĩ Phép</b>");
                content += "<br/>========================<br/>";
                content += "Họ Tên: " + "<br/>" + this.g_UserMemberName + "<br/>";
                content += "Thời gian Nghĩ: " + "<br/>"  + ngay + " Đến Ngày " + denngay + "<br/>";
                content += "Lý Do:";
                content += "<span style='padding-left:10px;'>";
                content += lydonghi.Replace("\r\n", "<br/>");
                content += "<br/> Ghi Chú :" + ghichu;
                content += "<span>";

                content += "<br/>========================<br/>";
                content += @"<span style='font-size:8pt;'>Đây là email gửi từ hệ thống, vui lòng không Reply lại địa chỉ email này.<span>";
                //int idbgd = blc_user.GetIDBGD_byIDuser(iduser);
                //int idtrPhong = blc_user.GetIDTruongPhong_byIDuser(iduser);
                //int idPhoPhong = blc_user.GetIDPhoPhong_byIDuser(iduser);
                //int idtrNhom = blc_user.GetIDTruongNhom_byIDuser(iduser);
                //if (songaynghi == "0")
                //{
                //    if (iduser != idtrPhong)
                //    { Send_Mail(this.g_UserMemberName, content, blc_user.GetUse_ByID(idtrPhong).Email.Trim()); }
                // // Send_Mail(nameuser, content, toMailphophong);
                //    if (iduser != idtrNhom)
                //   {Send_Mail(this.g_UserMemberName, content, blc_user.GetUse_ByID(idtrNhom).Email.Trim());}

                //}
                //if (songaynghi != "0" && songaynghi == "1" || songaynghi != "0" && songaynghi == "0,5")
                //{
                //    if (iduser != idtrPhong)
                //    { Send_Mail(this.g_UserMemberName, content, blc_user.GetUse_ByID(idtrPhong).Email.Trim()); }
                //  //  Send_Mail(nameuser, content, toMailBGD);
                //    if (iduser != idPhoPhong)
                //    {
                //        Send_Mail(this.g_UserMemberName, content, blc_user.GetUse_ByID(idPhoPhong).Email.Trim());
                //    }
                //}
                //else
                //{
                //    if (iduser != idtrPhong)
                //    { Send_Mail(this.g_UserMemberName, content, blc_user.GetUse_ByID(idtrPhong).Email.Trim()); }
                //    Send_Mail(this.g_UserMemberName, content, blc_user.GetUse_ByID(idbgd).Email.Trim());
                //}

            }
            catch (Exception ex)
            {
                ex.ToString();
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
        public int secsionchose
        {
            get
            {
                if (Session["g_secsionchose"] != null)
                    return Convert.ToInt32(Session["g_secsionchose"]);
                return 0;
            }
            set
            {
                Session["g_secsionchose"] = value;
            }
        }
        public int UserMemberType
        {
            get
            {
                if (Session["g_UserMemberType"] != null)
                    return Convert.ToInt32(Session["g_UserMemberType"]);
                return 0;
            }
            set
            {
                Session["g_UserMemberType"] = value;
            }
        }
        protected string g_UserMemberName
        {
            get
            {
                if (Session["g_UserMemberName"] != null)
                    return Session["g_UserMemberName"].ToString();
                return string.Empty;
            }
            set
            {
                Session["g_UserMemberName"] = value;
            }
        }
        protected string UserMemberLoginID
        {
            get
            {
                if (Session["g_UserMemberLoginID"] != null)
                    return Session["g_UserMemberLoginID"].ToString();
                return string.Empty;
            }
            set
            {
                Session["g_UserMemberLoginID"] = value;
            }
        }
        public int UserMemberLike
        {
            get
            {
                if (Session["g_UserMemberLike"] != null)
                    return Convert.ToInt32(Session["g_UserMemberLike"]);
                return -1;
            }
            set
            {
                Session["g_UserMemberLike"] = value;
            }
        }


        protected int BillPhepID
        {
            get
            {
                if (ViewState["g_BillID"] != null)
                    return Convert.ToInt32(ViewState["g_BillID"]);
                return -1;
            }
            set
            {
                ViewState["g_BillID"] = value;
            }
        }


        protected void btnDeleteKey(object sender, EventArgs e)
        {
            try
            {

                if (blc_user.DeleteBill_byID(this.BillPhepID))
                {

                    Alert.Show("Xóa thành công");
                    BindGirdView();
                }
            }
            catch (System.Exception ex)
            {
                Alert.Show("Xóa thất bại" + ex.Message);

            }

        }

        private void doPaging()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            findex = PageNumber - 5;
            if (PageNumber > 5)
            {
                lindex = PageNumber + 5;
            }
            else
            {
                lindex = 10;
            }
            if (lindex > Convert.ToInt32(ViewState["totpage"]))
            {
                lindex = Convert.ToInt32(ViewState["totpage"]);
                findex = lindex - 10;
            }

            if (findex < 0)
            {
                findex = 0;
            }
            for (int i = findex; i < lindex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPages.DataSource = dt;

            rptPages.DataBind();

            if (dt.Rows.Count > 1)

                pager_div.Visible = true;

            else
                pager_div.Visible = false;



        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            PageNumber -= 1;

            BindGirdView();
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            PageNumber = 0;
            BindGirdView();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            PageNumber = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindGirdView();
        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            PageNumber += 1;


            BindGirdView();
        }
        protected void rptPages_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {

            PageNumber = Convert.ToInt32(e.CommandArgument);

            BindGirdView();

        }
        protected void RepeaterPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnkPage = (LinkButton)e.Item.FindControl("btnPage");
            if (lnkPage.CommandArgument.ToString() == PageNumber.ToString())
            {
                lnkPage.Enabled = false;
                lnkPage.BackColor = System.Drawing.Color.FromName("#FFCC01");
            }
        }
        public int PageNumber
        {

            get
            {

                if (ViewState["PageNumber"] != null)

                    return Convert.ToInt32(ViewState["PageNumber"]);

                else

                    return 0;

            }

            set
            {

                ViewState["PageNumber"] = value;

            }

        }

        protected void dropSearchtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropSearchtype.SelectedValue == "1")
            {
                dr_dspb.Visible = true;
                txtSearch.Visible = false;
            }
            if (dropSearchtype.SelectedValue == "2")
            {
                txtSearch.Visible = true;
                dr_dspb.Visible = false;
            }
        }
        protected string Binstatus(object trangthaiphep,object ttnhom,object ttbophan)
        { string stus="";
        if (trangthaiphep.ToString() == "0")
        {
            if (ttnhom.ToString() != "0" || ttbophan.ToString() != "0")
                return "<span style='font-weight: bold;color:#9a8803'>Đã Xác Nhận</span>";
            return "<span style='font-weight: bold;color:Red'>Chưa duyệt</span>";
        }
        if (trangthaiphep.ToString() == "2")
            return "<span style='font-weight: bold;color:Orange'>Không duyệt</span>";
        if (trangthaiphep.ToString() == "1")
            return "<span style='font-weight: bold;color:Green'>Đã duyệt</span>";

        return stus;
        }
        protected string checkdisplay(object idnhanvien)
        {
            if (Utilis.TryParseInt(idnhanvien) == this.UserID)
                return "";
            else
            return "none";
        }
        protected void ddlmaphep_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (secsionchose != 0)
            {
                rad_ngaynghibuoi.MaxDate = DateTime.Now;
                rad_ngay.MaxDate = DateTime.Now;
                rad_tungay.MaxDate = DateTime.Now;
                rad_denngay.MaxDate = DateTime.Now;
                rad_tungay.SelectedDate = DateTime.Now;
                rad_denngay.SelectedDate = DateTime.Now;
                rad_ngay.SelectedDate = DateTime.Now;
                rad_ngaynghibuoi.SelectedDate = DateTime.Now;
            }
            else {
                rad_ngaynghibuoi.MinDate = DateTime.Now.AddDays( - 1);
                rad_ngay.MinDate = DateTime.Now.AddDays(-1);
                rad_tungay.MinDate = DateTime.Now.AddDays(-1);
                rad_denngay.MinDate = DateTime.Now.AddDays(-1);
            }

            if (ddlmaphep.SelectedValue == "1")
            {
                th_buoi.Visible = true;
                td_buoi.Visible = true;
                th_songay.Visible = false;
                td_Ngaynghi.Visible = false;
                th_ngay.Visible = false;
                td_ngay.Visible = false;
                ListItem removeItem = dr_buoinghi.Items.FindByValue("Cả Ngày");
                dr_buoinghi.Items.Remove(removeItem);
            }
            if (ddlmaphep.SelectedValue == "2")
            {
                th_buoi.Visible = false;
                td_buoi.Visible = false;
                th_songay.Visible = true;
                td_Ngaynghi.Visible = true;
                th_ngay.Visible = false;
                td_ngay.Visible = false;
            }
            
            else if (ddlmaphep.SelectedValue == "3" || ddlmaphep.SelectedValue == "4" || ddlmaphep.SelectedValue == "5" || ddlmaphep.SelectedValue == "6")
            {
                th_buoi.Visible = false;
                td_buoi.Visible = false;
                th_songay.Visible = false;
                td_Ngaynghi.Visible = false;
                th_ngay.Visible = true;
                td_ngay.Visible = true;
                if (ddlmaphep.SelectedValue == "6")
                {
                    th_buoi.Visible = true;
                    td_buoi.Visible = true;
                    th_songay.Visible = false;
                    td_Ngaynghi.Visible = false;
                    th_ngay.Visible = false;
                    td_ngay.Visible = false;
                    
                   
                }
               

            }
        }

        public string GetNameNhanVien(object IDUSer)
        {
            int iduser = Utils.TryParseInt(IDUSer.ToString(), 0);

            TUser us = blc_user.GetUser_ByIDAll(iduser);
            if (us != null)
            {
                return us.UserName;
            }
            else return "---";
        }

        public int UserMemberParentID
        {
            get
            {
                if (Session["g_UserMemberParentID"] != null)
                    return Convert.ToInt32(Session["g_UserMemberParentID"]);
                return -1;
            }
            set
            {
                Session["g_UserMemberParentID"] = value;
            }
        }
        public int tongngaynghitrongthang
        {
            get
            {
                if (Session["tongngaynghitrongthang"] != null)
                    return Convert.ToInt32(Session["tongngaynghitrongthang"]);
                return 0;
            }
            set
            {
                Session["tongngaynghitrongthang"] = value;
            }
        }

        protected void dr_buoinghi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dr_buoinghi.SelectedValue == "Cả Ngày")
            {
                th_buoi.Visible = false;
                td_buoi.Visible = false;
                th_songay.Visible = true;
                td_Ngaynghi.Visible = true;
                th_ngay.Visible = false;
                td_ngay.Visible = false;
            }
        }
        //OleDbConnection oledbConn;
        //private DataTable GenerateExcelData(string SlnoAbbreviation)
        //{
        //    DataTable data = null;
        //    //try
        //    //{

        //        string filename = Path.GetFileName(filesUpload1.FileName);
        //        filesUpload1.SaveAs(Server.MapPath("~/") + filename);
        //        string path = System.IO.Path.GetFullPath(Server.MapPath("~/" + filename));
        //        // need to pass relative path after deploying on server
        //        //  string path = System.IO.Path.GetFullPath(Server.MapPath("~/InformationNew.xlsx"));
        //        /* connection string  to work with excel file. HDR=Yes - indicates 
        //           that the first row contains columnnames, not data. HDR=No - indicates 
        //           the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
        //           (numbers, dates, strings etc) data columns as text. 
        //        Note that this option might affect excel sheet write access negative. */

        //        if (Path.GetExtension(path) == ".xls")
        //        {
        //            oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
        //        }
        //        else if (Path.GetExtension(path) == ".xlsx")
        //        {
        //            oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
        //        }
        //        oledbConn.Open();
        //        OleDbCommand cmd = new OleDbCommand();
        //        OleDbDataAdapter oleda = new OleDbDataAdapter();
        //        DataSet ds = new DataSet();

        //        // passing list to drop-down list

        //        // selecting distict list of Slno 
        //        cmd.Connection = oledbConn;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "SELECT distinct([Name]) FROM [Sheet1$]";
        //        oleda = new OleDbDataAdapter(cmd);
        //        oleda.Fill(ds, "dsdiem");
        //        //  ddlSlno.DataSource = ds.Tables["dsSlno"].DefaultView;
        //        //if (!IsPostBack)
        //        //{
        //        //    ddlSlno.DataTextField = "Name";
        //        //    ddlSlno.DataValueField = "Name";
        //        //    ddlSlno.DataBind();
        //        //}
        //        // by default we will show form data for all states but if any state is selected then show data accordingly
        //        if (!String.IsNullOrEmpty(SlnoAbbreviation) && SlnoAbbreviation != "Select")
        //        {
        //            cmd.CommandText = "SELECT [Name], [Poin]" +
        //                "  FROM [Sheet1$] ";
        //            // cmd.Parameters.AddWithValue("@Slno_Abbreviation", SlnoAbbreviation);
        //        }
        //        else
        //        {
        //            cmd.CommandText = "SELECT [Name], [Poin] FROM [Sheet1$]";
        //        }
        //        oleda = new OleDbDataAdapter(cmd);
        //        oleda.Fill(ds);

        //        // binding form data with grid view
        //        data = ds.Tables[1].DataSet.Tables[1];

        //    //}
        //    // need to catch possible exceptions
        //    //catch (Exception ex)
        //    //{
        //    //    Alert.Show(ex.ToString());
        //    //}
        //   //finally
        //   // {
        //        oledbConn.Close();
        //   // }
        //    return data;
        //}

    }
}
