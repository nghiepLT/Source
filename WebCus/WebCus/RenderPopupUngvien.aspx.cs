using PQT.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.DAC.ViewModel;
using UserMng.BLC;
using UserMng.DAC;
using Newtonsoft.Json;
using PQT.Common;
using System.Net.Mail;

namespace WebCus
{
    public partial class RenderPopupUngvien : System.Web.UI.Page
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
                    this.emailuv.InnerText = uv.Email;
                    this.sodtuv.InnerText = uv.SoDt;
                    this.diachiuv.InnerText = uv.DCTamTru;

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
          
            UngVien uv = blc_user.GetUngVienByID(this.IDNTD);

            if (uv.Email != "")
            {
                string contentHTML = "";
                contentHTML += "<table>";
                contentHTML += "<tr style='margin-bottom:20px;'>";
                contentHTML += "<td>";
                contentHTML += "<strong style='font-size:16pt'>THƯ MỜI PHỎNG VẤN</strong>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //Tên ứng viên
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<strong>Tên ứng viên: </strong><span>" + uv.HoTen.ToUpper() + "</span>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //Ngày tháng năm sinh
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<strong>Ngày tháng năm sinh: </strong><span>" + uv.NgaySinh.Value.ToShortDateString() + "</span>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //Email:
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<strong>Email: </strong><span>" + uv.Email + "</span>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //SĐT:
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<strong>SĐT: </strong><span>" + uv.SoDt + "</span>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //Địa chỉ:
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<strong>Địa chỉ: </strong><span>" + uv.DCTamTru + "</span>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //Truoc tiên
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Trước tiên, Công Ty TNHH Vi tính Nguyên Kim xin gửi tới bạn lời chúc sức khoẻ, an khang và thành đạt. </div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Sau Quá trình sàn lọc hồ sơ, phòng nhân sự trân trọng mời bạn đến Công ty tham dự buổi phỏng vấn: </div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
               var ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(uv.UngCuViTri);
                contentHTML += "<div>-<strong>Vị trí dự tuyển</strong>: "+ ThongTinViTri .ViTriungTuyen+ "</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>-Địa điểm phỏng vấn: 245B Trần Quang Khải, Phường Tân Định, Quận 1, TP.HCM. </div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>-Thời gian: Lúc <strong>" + this.uvluc.Value + "</strong> thứ " + this.uvthu.Value + " ngày " + this.uvngay.Value + " tháng " + this.uvmonth.Value + " năm " + this.uvyear.Value + "</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                //Bạn vui lòng
                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<span style='color:red'>Bạn vui lòng liên hệ tiếp tân điền vào Phiếu thông tin và Bảng khảo sát khi đến tham gia Phỏng vấn : </span>hoặc <span></span><span style='color:red'>điền thông tin vào file đính kèm và gửi lại qua email.</span>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Rất mong bạn thu xếp thời gian, nếu có sự trở ngại về mặt thời gian và vị trí ứng tuyển, bạn cũng có thể thông báo với chúng tôi theo thông tin bên dưới.</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<span>Xin chân thành cảm ơn và trân trọng kính chào!</span>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Công ty Vi Tính Nguyên Kim hiện là công ty hàng đầu trong việc phân phối sỉ sản phẩm CNTT máy tính, máy in, linh kiện, camera, server, laptop … chính hãng. </div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Khi được tuyển vào Công ty Vi Tính Nguyên Kim các bạn sẽ có dịp trải nghiệm môi trường làm việc thân thiện và chuyên nghiệp.</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Có nhiều cơ hội thăng tiến và thu nhập cao tùy theo hiệu quả và sự đóng góp của mỗi cá nhân.</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Nhiều chính sách phúc lợi hấp dẫn dành cho người lao động:</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Thưởng thâm niên 3 năm, 5 năm.</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Khám sức khỏe định kỳ</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Cơm trưa, đồng phục, nghỉ thư giản giữa giờ</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Du lịch hàng năm</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>BHXH, BHYT, BHTN, BH 24/24, Bảo hiểm cao cấp</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";

                contentHTML += "<tr style='font-style: italic;'>";
                contentHTML += "<td style='margin:2px 0px;'>";
                contentHTML += "<div>Lương thưởng tháng 13, các ngày Lễ trong năm, 12 ngày phép…</div>";
                contentHTML += "</td>";
                contentHTML += "</tr>";
                contentHTML += "</table>";
                var timespl = this.slhour.Value.ToString().Split(':');
                DateTime dt = new DateTime(int.Parse(this.uvyear.Value), int.Parse(this.uvmonth.Value)+1, int.Parse(this.uvngay.Value),int.Parse(timespl[0]), int.Parse(timespl[1]),0);
                blc_user.Capnhatngayphongvan(uv.Id,dt);
                //blc_user.CapNhatEmailPVStatus(uv.Id);
                TUser user = blc_user.GetUser_ByIDAll(this.UserMemberID);
                string titles = "Thư mời phỏng vấn " + (uv.GioiTinh == "Nam" ? "Anh" : "Chịs") + " " + uv.HoTen.ToUpper();
                sendEmail(uv.Email, titles, contentHTML);
                
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
                //Send cho ung vien
                mail.To.Add(to);
                //Send cho người đăng nhập
                //cc 
                TUser user = blc_user.GetUser_ByIDAll(this.UserMemberID);
                mail.To.Add(user.Email);
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