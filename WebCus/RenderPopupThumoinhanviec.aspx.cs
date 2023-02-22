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
using System.IO;

namespace WebCus
{
    public partial class RenderPopupThumoinhanviec : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
                    UngVien uv = blc_user.GetUngVienByID(GuiId);
                    this.IDNTD = GuiId;
                    this.tenuv.InnerText = uv.HoTen;
                    this.tenuv2.InnerText = uv.HoTen;
                    this.tenuv3.InnerText = uv.HoTen;
                    this.sodtuv.InnerText = uv.SoDt;
                    if (!string.IsNullOrEmpty(uv.UngCuViTri))
                    {
                        ThongTinViTri ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);
                        if (ThongTinViTri != null)
                        {
                            this.vitriutuv.InnerText = ThongTinViTri.ViTriungTuyen;
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
            string Files = "/Uploads/TuyenDung/ThuMoiNhanViec/" + this.Filess.Value;
            string anhChi = "";
            if (uv.GioiTinh == "Nam")
            {
                anhChi = "Anh";
            }
            else
            {
                anhChi = "Chị";
            }
            var link = Server.MapPath(Files);
            if (uv.Email != "")
            {
                string contentHTML = "";


                contentHTML += "<table>";
                //Tên ứng viên
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:8px 0px;'>";
                contentHTML += "<span>Chào "+anhChi+ " : </span><span>" + uv.HoTen.ToUpper() + "</span>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //Điện thoại
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:8px 0px;'>";
                contentHTML += "<span>Điện thoại : </span><strong>" + uv.SoDt + "</strong>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //sau khi
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:8px 0px;'>";
                contentHTML += "<span>Sau khi tham gia các vòng phỏng vấn, phòng HCNS trân trọng thông báo "+anhChi+" </span><strong>" + uv.HoTen.ToUpper() + "</strong>";
                string vitri = "";
                ThongTinViTri ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);
                if (ThongTinViTri != null)
                {
                    vitri = ThongTinViTri.ViTriungTuyen;
                }
                contentHTML += " đã trúng tuyển vị trí <strong>Nhân viên " +vitri+"</strong>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //sau khi
                //Khi gia nhập 
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:8px 0px;'>";
                contentHTML += "Khi gia nhập Công ty TNHH Vi Tính Nguyên Kim, "+anhChi+" sẽ được hưởng đầy đủ các phúc lợi, quyền lợi, chính sách như các nhân viên khác. ";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //Khi gia nhập 
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:8px 0px;'>";
                contentHTML += "Phòng HCNS trân trọng mời "+anhChi+" <span>"+ uv.HoTen.ToUpper() + "</span> có mặt tại công ty " + "<strong style='color:#0070C0' >địa chỉ 245B Trần Quang Khải, phường Tân Định, quận 1, TP.HCM</strong>";
                contentHTML += "<strong style='color:red'> vào lúc "+ this.slhour.Value+" ngày "+this.ngaythangnam.Value + "</strong> để nhận việc.";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<div>Trường hợp Anh/Chị không thể thu xếp đến nhận việc đúng thời gian, xin vui lòng liên hệ với chúng tôi theo số điện thoại <span style='font-style: italic;' >0938 808 660</span> để xác nhận lại.</div>";
                contentHTML += "</table>";
                string tmnv= "THƯ MỜI NHẬN VIỆC " + anhChi.ToUpper() + " " + uv.HoTen.ToUpper();
                 
                DateTime dt= DateTime.ParseExact(this.ngaynhanviec.Value.ToString(), "yyyy/MM/dd", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                blc_user.CapNhatEmailNVStatus(uv.Id, dt);
                sendEmail(uv.Email,tmnv, contentHTML,link);

                string close = @"<script type='text/javascript'>
                                window.opener.location.reload(true);
                                self.close();
                                </script>";
                base.Response.Write(close);
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
                var mailsend = blc_user.GetListEmail(true, 1).FirstOrDefault();
                var lstmailCC = blc_user.GetListEmail(true, 2).ToList();
                //
                //mail.CC.Add(new MailAddress(mailsend.Email));
                //foreach (var its in lstmailCC)
                //{
                //    mail.CC.Add(new MailAddress(its.Email));
                //}
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(file);
                mail.Attachments.Add(attachment);
                mail.Subject = title;
                mail.Body = sContent;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                //Cap nhật ngày phỏng vấn

                string close = @"<script type='text/javascript'>
                                window.opener.location.reload(true);
                                self.close();
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

        protected void Click_uploadexcel(object sender, EventArgs e)
        {
            if (filesUpload.HasFile)
            {
                string filename = Path.GetFileName(filesUpload.FileName).Replace(" ", "_");
                string fileType = Path.GetExtension(filename);
                var splitdot = filename.Split('.');
                stringfilePath = splitdot[0]+ fileType;
               
                string finalPath = Server.MapPath("~/Uploads/TuyenDung/ThuMoiNhanViec/") + stringfilePath;
                filesUpload.SaveAs(finalPath);
                lbFiles.Text = "File";
                lbFiles.NavigateUrl = "~/Uploads/TuyenDung/ThuMoiNhanViec/" + filename;
                spFile.Visible = true;
                Alert.Show("Import thành công");
            }
        }
        protected string stringfilePath
        {
            get
            {
                if (ViewState["g_filePath"] != null)
                    return ViewState["g_filePath"].ToString();
                return "";
            }
            set
            {
                ViewState["g_filePath"] = value;
            }
        }
        protected void filesUpload_Load(object sender, EventArgs e)
        {
           
        }

        protected void filesUpload_DataBinding(object sender, EventArgs e)
        {
            Alert.Show("Loaded");
        }
    }
}