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
using System.Net.Mail;
using PQT.Common;

namespace WebCus
{
    public partial class RenderPopupThumoinhanviec2 : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
                    UngVien uv = blc_user.GetUngVienByID(GuiId);
                    this.IDNTD = GuiId;
                    this.kinhguianh.InnerHtml = uv.HoTen;
                    this.sodt.InnerHtml = uv.SoDt;
                    if (!string.IsNullOrEmpty(uv.UngCuViTri))
                    {
                        ThongTinViTri ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);
                        if (ThongTinViTri != null)
                        {
                            this.chucdanh.InnerHtml = ThongTinViTri.ViTriungTuyen;
                            this.bophanphong.InnerHtml = uv.PhongBan;
                        }
                    }
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
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
            if (uv.Email != "")
            {
                string contentHTML = "";

                contentHTML +="<table class='table table-bordered'>";
                contentHTML += "<tr>";
                contentHTML += "<td style='border: 1px solid;' colspan = '2' class='text-center'> <img style = 'width:100px;margin:0px auto' src='https://vitinhnguyenkim.vn/uploads/File/logo_NguyenKim_ao.png' class='img-responsive' /></td>";
                contentHTML += "<td style='border: 1px solid;'>";
                contentHTML += "THỎA THUẬN THỬ VIỆC";
                contentHTML += "</td>";
                contentHTML += "<td style='border: 1px solid;'>";
                contentHTML += "<div>";
                contentHTML += "Mã hiệu: 06.01-BM/HCN/NK";
                contentHTML += "</div>";
                contentHTML += "<div>";
                contentHTML += "Lần ban hành/sửa đổi: 1/0";
                contentHTML += "</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                contentHTML += "<tr>";
                contentHTML += "<td>";
                contentHTML += "Ngày hiệu lực";
                contentHTML += "</td>";
                contentHTML += "<td style='border: 1px solid;'>";
                contentHTML += "26 /04/2017";
                contentHTML += "</td>";
                contentHTML += "<td style='border: 1px solid;'>Bộ phận chịu trách nhiệm </td>";
                contentHTML += "<td style='border: 1px solid;'>Hành chính Nhân sự</td>";
                contentHTML += "</tr>";
                contentHTML += "</table>";

                sendEmail(uv.Email, "Thư mời nhận việc", contentHTML);
                string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
                base.Response.Write(close);
            }
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
                mail.To.Add(to);
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