using Newtonsoft.Json;
using PQT.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using UserMng.DAC;
using PQT.DAC.ViewModel;
using PQT.Common;
using System.Net.Mail;

namespace WebCus
{
    public partial class RenderPopupThuChucMung : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
                UngVien uv = blc_user.GetUngVienByID(GuiId);
                NhanVien nv = blc_user.GetNhanvien_byID(uv.IdNhanVien.Value);
                ThongTinViTri ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);

                this.IDNTD = GuiId;
                ThongTinNhanSu ttns = blc_user.GetTTNhansu_byID(uv.IdNhanVien.Value);
                string Gioitinh = "";
                if (nv.GioiTinh == "1")
                    Gioitinh = "Anh";
                else
                    Gioitinh = "Chị";
                this.nhanvien.InnerText = ThongTinViTri.ViTriungTuyen;
                this.nv1.InnerHtml = Gioitinh + " " + nv.HoTen;
                this.nv2.InnerHtml = Gioitinh + " " + nv.HoTen;
                this.nv3.InnerHtml = Gioitinh + " " + nv.HoTen;
                this.nv4.InnerHtml = Gioitinh + " " + nv.HoTen;
                this.nv5.InnerHtml = Gioitinh + " " + nv.HoTen;
                this.nv6.InnerHtml = Gioitinh + " " + nv.HoTen;
                this.batdautu.Value = DateTime.Now.ToShortDateString();
                this.homnay.Value= DateTime.Now.ToShortDateString();
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
            UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
            string anhChi = "";
            if (uv.GioiTinh == "Nam")
            {
                anhChi = "Anh";
            }
            else
            {
                anhChi = "Chị";
            } 
            if (uv.Email != "")
            {
               
                ThongTinViTri ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);
               
                //End  

                string contentHTMLtb = "";
                contentHTMLtb += "<table>";
                contentHTMLtb += "<tr>";
                contentHTMLtb += "<td style='padding:2px 0px;'>";
                contentHTMLtb += "<span style='font-style: italic;'>Chào : </span><strong>" + anhChi + " " + uv.HoTen + "</strong>";
                contentHTMLtb += "</td>"; 
                contentHTMLtb += "</tr>";

                contentHTMLtb += "<tr>";
                contentHTMLtb += "<td style='padding:2px 0px;'>";
                ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);
                contentHTMLtb += "<span style='font-style: italic;'>Nhân viên : </span><strong>" + ThongTinViTri.ViTriungTuyen + "</strong>";
                contentHTMLtb += "</td>";
                contentHTMLtb += "</tr>";

                contentHTMLtb += "<tr style='margin-top:20px;'>";
                contentHTMLtb += "<td style='padding:5px 0px;'>";
                contentHTMLtb += "<span style='font-style: italic;'>Chúc mừng : </span><strong>" + anhChi + " " + uv.HoTen + "</strong>" + " đã chính thức tham gia vào đội ngũ nhân viên của Công ty TNHH Vi Tính Nguyên Kim.";
                contentHTMLtb += "</td>";
                contentHTMLtb += "</tr>";

                contentHTMLtb += "<tr style='margin-top:2px;'>";
                contentHTMLtb += "<td style='padding:5px 0px;'>";
                contentHTMLtb += "<span>Theo đánh giá của Ban Giám đốc, </span><strong>" + anhChi + " " + uv.HoTen + "</strong>" + " trong thời gian thử việc đã có nhiều nỗ lực hoàn thành những công việc được giao và đạt được những thành công nhất định.";
                contentHTMLtb += "</td>";
                contentHTMLtb += "</tr>";

                contentHTMLtb += "<tr style='margin-top:2px;'>";
                contentHTMLtb += "<td style='padding:5px 0px;'>";
                contentHTMLtb += "<span >Ban Giám đốc công ty mong muốn  </span><strong>" + anhChi + " " + uv.HoTen + "</strong>" + " sẽ phát huy tiềm năng của mình trong thời gian tới tại Công ty TNHH Vi Tính Nguyên Kim.";
                contentHTMLtb += "</td>";
                contentHTMLtb += "</tr>";

                contentHTMLtb += "<tr style='margin-top:2px;'>";
                contentHTMLtb += "<td style='padding:5px 0px;'>";
                contentHTMLtb += "Hy vọng những đóng góp của cá nhân Chị sẽ đem lại thu nhập cho chính Chị cũng như đội ngũ nhân viên Công ty TNHH Vi Tính Nguyên Kim. ";
                contentHTMLtb += "</td>";
                contentHTMLtb += "</tr>";

                contentHTMLtb += "<tr style='margin-top:2px;'>";
                contentHTMLtb += "<td style='padding:5px 0px;'>";
                contentHTMLtb += "Theo quyết định của Ban Giám đốc, thời gian tiếp nhận chính thức của " + "<strong>" + anhChi + " " + uv.HoTen + "</strong>" + " bắt đầu từ " + DateTime.Now.ToShortDateString();
                contentHTMLtb += "</td>";
                contentHTMLtb += "</tr>";

                contentHTMLtb += "<tr style='margin-top:20px;'>";
                contentHTMLtb += "<td style='text-align:right'>";
                contentHTMLtb += "<strong>TM BAN GIÁM ĐỐC</strong>";
                contentHTMLtb += "</td>";
                contentHTMLtb += "</tr>";

                contentHTMLtb += "<tr style='margin-top:2px;margin-bottom:20px'>";
                contentHTMLtb += "<td style='text-align:right'>";
                contentHTMLtb += "<strong>PHÒNG HCNS</strong>";
                contentHTMLtb += "</td>";
                contentHTMLtb += "</tr>";

                contentHTMLtb += "<tr style='margin-top:2px;'>";
                contentHTMLtb += "<td style='padding:5px 0px;'>";
                contentHTMLtb += "<strong>@mời " + anhChi + " " + uv.HoTen + " liên hệ P. HCNS vào lúc 17h hôm nay "+DateTime.Now.ToShortDateString()+ " để làm thủ tục nhân viên chính thức (BHXH, Đồng phục, ký HĐLĐ, làm thẻ ngân hàng "+this.thenganhang.Value+").</strong>";
                contentHTMLtb += "</td>";
                contentHTMLtb += "</tr>";

                contentHTMLtb += "</table>";
                blc_user.UpdateSatusChucmung(this.IDNTD);
                sendEmail(uv.Email, "Chúc mừng Nv chính thức", contentHTMLtb, ""); 
                //End End
            }
        }
      
        private bool sendEmail(string to, string title, string sContent,string file)
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
                TUser user = blc_user.GetUser_ByIDAll(this.UserMemberID);
                mail.To.Add(user.Email);
                //cc
                var mailsend = blc_user.GetListEmail(1, 1).FirstOrDefault();
                var lstmailCC = blc_user.GetListEmail(1, 2).ToList();
                //
                // mail.CC.Add(new MailAddress(mailsend.Email));
                //foreach (var its in lstmailCC)
                //{
                //    mail.CC.Add(new MailAddress(its.Email));
                //}
                if (file != "")
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(file);
                    mail.Attachments.Add(attachment);
                }
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
    }
}