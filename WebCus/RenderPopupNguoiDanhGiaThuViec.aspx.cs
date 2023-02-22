using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using UserMng.DAC;
using PQT.DAC.ViewModel;
using PQT.DAC;
using PQT.Common;
using System.Net.Mail;

namespace WebCus
{
    public partial class RenderPopupNguoiDanhGiaThuViec : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        static UserMngOther_BLC blc_user2 = new UserMngOther_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
                int type = int.Parse(Request.QueryString["type"].ToString());
                int userid= int.Parse(Request.QueryString["userid"].ToString());
                this.iduser.Value = userid.ToString();
                this.idungvien.Value = GuiId.ToString();
                UngVien uv = blc_user.GetUngVienByID(GuiId);
                this.IDNTD = GuiId;
                YeuCauTuyenDung yctd = blc_user.GetYeuCauTD_ByID(uv.IdYeuCau.Value);
                TUser tuser = blc_user.GetUser_ByIDAll(yctd.IdNguoiTao.Value);
               // this.email.Value = tuser.Email;
                NhanVien nv = blc_user.GetNhanvien_byID(tuser.IdNhansu.Value);
                string htmlContent = "";
                if (type == 1)
                {
                    this.type.Value = "1";
                    htmlContent += "Dear " + (nv.GioiTinh == "1" ? "Anh" : "Chị") + " " + nv.HoTen + ", ";
                    htmlContent += "\r\n";
                    htmlContent += "Mời " + (nv.GioiTinh == "1" ? "Anh" : "Chị") + " " + "đánh giá ứng viên " + uv.HoTen + " ";
                    this.nguoidanhgia.Value = nv.HoTen;
                    this.gioitinh.Value = nv.GioiTinh;
                    string keyword = tuser.UserID.ToString() + "-" + "1";
                    string asckeyword = Utility.Encrypt(keyword);
                    string path = HttpContext.Current.Request.Url.Host + ":8088/RenderPopupDanhGiatuyenDung.aspx?id=" + GuiId+"&type=1";
                    this.path.Value = path;
                    htmlContent += "Thông qua đường dẫn " + path;
                    //this.noidung.Value = htmlContent;
                }
                ThongTinNhanSu ttns = blc_user.GetTTNhansu_byID(tuser.IdNhansu.Value);
                ThongTinNhanSu ttns2 = blc_user.GetTruongPhong(ttns.PhongBan.Value);
                if (type == 2)
                {
                    this.type.Value = "2";
                    NhanVien nv2 = blc_user.GetNhanvien_byID(ttns2.IdNhanVien.Value);
                    TUser tuser2 = blc_user.GetUserByNhanvienId(nv2.IdNhanVien);
                    htmlContent += "Dear " + (nv2.GioiTinh == "1" ? "Anh" : "Chị") + " " + nv2.HoTen + ", ";
                    htmlContent += "\r\n";
                    htmlContent += "Mời " + (nv2.GioiTinh == "1" ? "Anh" : "Chị") + " " + "đánh giá ứng viên " + uv.HoTen + " ";
                    this.nguoidanhgia.Value = nv2.HoTen;
                    this.gioitinh.Value = nv2.GioiTinh;
                    string keyword = tuser2.UserID.ToString() + "-" + "2";
                    string asckeyword = Utility.Encrypt(keyword);
                    string path = HttpContext.Current.Request.Url.Host + ":8088/RenderPopupDanhGiatuyenDung.aspx?id=" + GuiId +"&type=2";
                    this.path.Value = path;
                    htmlContent += "Thông qua đường dẫn " + path;
                    //this.noidung.Value = htmlContent;
                }
                if (type == 4)
                {   
                    this.type.Value = "4";
                    NhanVien nv3 = blc_user.GetNhanvien_byID(1079);
                    TUser tuser3 = blc_user.GetUserByNhanvienId(nv3.IdNhanVien);
                    htmlContent += "Dear " + (nv3.GioiTinh == "1" ? "Anh" : "Chị") + " " + nv3.HoTen + ", ";
                    htmlContent += "\r\n";
                    htmlContent += "Mời " + (nv3.GioiTinh == "1" ? "Anh" : "Chị") + " " + "đánh giá ứng viên " + uv.HoTen + " ";
                    this.nguoidanhgia.Value = nv3.HoTen;
                    this.gioitinh.Value = nv3.GioiTinh;
                    string keyword = tuser3.UserID.ToString() + "-" + "4";
                    string asckeyword = Utility.Encrypt(keyword);
                    string path = HttpContext.Current.Request.Url.Host + ":8088/RenderPopupDanhGiatuyenDung.aspx?id=" + GuiId+"&type=4";
                    this.path.Value = path;
                    htmlContent += "Thông qua đường dẫn " + path;
                    //this.noidung.Value = htmlContent;
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
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            //Cập nhật status
            UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
            YeuCauTuyenDung yctd = blc_user.GetYeuCauTD_ByID(uv.IdYeuCau.Value);
            TUser tuser = blc_user.GetUser_ByIDAll(yctd.IdNguoiTao.Value);
            NhanVien nv = blc_user.GetNhanVienByEmail(this.email.Value);
            blc_user.CapnhatguimailDanhgia(this.IDNTD, int.Parse(this.type.Value));
            string contentHTML = "";
            contentHTML += "<div>";
            contentHTML += "<table>";
            contentHTML += "<tr>";
            contentHTML += "<td style='margin:5px 0px;'>";
            contentHTML += "Dear " + (this.gioitinh.Value == "1" ? "Anh" : "Chị") + " " + nv.HoTen + ", ";
            contentHTML += "</td>";
            contentHTML += "</tr>";

            contentHTML += "<tr>";
            contentHTML += "<td style='margin:5px 0px;'>";
            contentHTML += "Mời " + (this.gioitinh.Value == "1" ? "Anh" : "Chị") + " " + "đánh giá ứng viên " + uv.HoTen + " ";
            contentHTML += "</td>";
            contentHTML += "</tr>";

            contentHTML += "<tr>";
            contentHTML += "<td style='margin:5px 0px;'>"; 
            contentHTML += "Thông qua đường dẫn " + "<a href="+ Page.Request.Form["ctl00$ContentPlaceHolder1$path"] + " >" + Page.Request.Form["ctl00$ContentPlaceHolder1$path"] + "</a>";
            contentHTML += "</td>";
            contentHTML += "</tr>";

            contentHTML += "</table>";
            contentHTML += "</div>";
            sendEmail(this.email.Value,"Đánh giá thử việc", contentHTML);
            
        }
        private bool sendEmail(string to, string title, string sContent)
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
                //Send cho ung vien
                mail.To.Add(to);
                TUser tuser = blc_user.GetUser_ByIDAll(int.Parse(this.iduser.Value));
                mail.To.Add(tuser.Email);
                //Send cho người đăng nhập
                //cc 

                // mail.To.Add(user.Email);
                mail.Subject = title;
                mail.Body = sContent;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                //Cap nhật ngày phỏng vấn

                string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
                base.Response.Write(close);

                return true;

            }
            catch (System.Exception ex)
            {
                Alert.Show(ex.Message);
                //error = ex.Message;
                return false;
            }

        }

        [System.Web.Services.WebMethod]
        public static string changeemail(string email, Guid id, int type)
        {
            string htmlContent = "";
            NhanVien nv = blc_user2.GetNhanVienByEmail(email);
            string path = "";
            if (nv != null)
            {
                TUser tuser = blc_user2.GetUserByNhanvienId(nv.IdNhanVien);
                UngVien uv = blc_user2.GetUngVienByID(id);
                htmlContent += "Dear " + (nv.GioiTinh == "1" ? "Anh" : "Chị") + " " + nv.HoTen + ", ";
                htmlContent += "\r\n";
                htmlContent += "Mời " + (nv.GioiTinh == "1" ? "Anh" : "Chị") + " " + "đánh giá phỏng vấn ứng viên " + uv.HoTen + " ";
                string keyword = tuser.UserID.ToString() + "-" + type;
                string asckeyword = "";
                asckeyword = Utility.Encrypt(keyword);
                path = HttpContext.Current.Request.Url.Host + ":8088/RenderPopupDanhGiatuyenDung.aspx?id=" + uv.Id + "&type=" + asckeyword;
                htmlContent += "Thông qua đường dẫn " + path;
            }
            return htmlContent + "|" + path;
        }
    }
}