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
    public partial class RenderPopupNguoiDanhGia : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        static UserMngOther_BLC blc_user2 = new UserMngOther_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                string htmlContent = "";
                Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
                int type = int.Parse(Request.QueryString["type"].ToString());
                int userid = int.Parse(Request.QueryString["userid"].ToString());
                this.iduser.Value = userid.ToString();
                this.IDNTD = GuiId;
                this.idungvien.Value = GuiId.ToString();
                UngVien uv = blc_user.GetUngVienByID(GuiId);
                YeuCauTuyenDung yctd = blc_user.GetYeuCauTD_ByID(uv.IdYeuCau.Value);
                TUser tuser = blc_user.GetUser_ByIDAll(yctd.IdNguoiTao.Value);
               // this.email.Value = tuser.Email;
                NhanVien nv = blc_user.GetNhanvien_byID(tuser.IdNhansu.Value); 
                if (type == 1)
                {
                    //this.noidung.Value = htmlContent;
                }

                this.type.Value = type.ToString();
                htmlContent += "Dear " + (nv.GioiTinh == "1" ? "Anh" : "Chị") + " " + nv.HoTen + ", ";
                htmlContent += "\r\n";
                htmlContent += "Mời " + (nv.GioiTinh == "1" ? "Anh" : "Chị") + " " + "đánh giá phỏng vấn ứng viên " + uv.HoTen + " ";
                this.nguoidanhgia.Value = nv.HoTen;
                this.gioitinh.Value = nv.GioiTinh;
                string keyword = tuser.UserID.ToString() + "-" + "1";
                string asckeyword = "";
                //asckeyword = Utility.Encrypt(keyword);
                string path = HttpContext.Current.Request.Url.Host + ":8088/RenderPopupDanhGia.aspx?id=" + GuiId + "&type=" + asckeyword;
                this.path.Value = path;
                htmlContent += "Thông qua đường dẫn " + path;
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
            //Cập nhật status
            UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
           
            YeuCauTuyenDung yctd = blc_user.GetYeuCauTD_ByID(uv.IdYeuCau.Value);
            TUser tuser = blc_user.GetUser_ByIDAll(yctd.IdNguoiTao.Value);
            NhanVien nv = blc_user2.GetNhanVienByEmail(this.email.Value);
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
            contentHTML += "Thông qua đường dẫn " + "<a href=" + Page.Request.Form["ctl00$ContentPlaceHolder1$path"] + " >" + Page.Request.Form["ctl00$ContentPlaceHolder1$path"] + "</a>";
            contentHTML += "</td>";
            contentHTML += "</tr>";

            contentHTML += "</table>";
            contentHTML += "</div>";
            sendEmail(this.email.Value, "Đánh giá phỏng vấn", contentHTML);
            blc_user.CapnhatEmailNguoiDanhGia(this.IDNTD,int.Parse(this.type.Value));
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
                if (tuser.Email != to)
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
        public static string changeemail(string email, Guid id,int type)
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
                path = HttpContext.Current.Request.Url.Host + ":8088/RenderPopupDanhGia.aspx?id=" + uv.Id + "&type=" + asckeyword;
                htmlContent += "Thông qua đường dẫn " + path;
            }
            return htmlContent+"|"+path;
        }
    }
}