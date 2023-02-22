using PQT.API;
using PQT.Common;
using PQT.DAC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;

namespace UserMng
{
    public partial class SendMail : XVNET_ModuleControl
    {
        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGridUser();
        }

        private void BindGridUser()
        {
            UserMng_BLC_NTX blc = new UserMng_BLC_NTX();
            //DataTable dt = blc.RowsUser(this.MRI.IsAdmin == true ? 1 : 2);
            //if (dt.Rows.Count < 5)
            //{
            //    for (int i = dt.Rows.Count; i < 10; i++)
            //    {
            //        DataRow drNew = dt.NewRow();
            //        dt.Rows.Add(drNew);
            //    }
            //}

            List<TUser> dt = blc.ListUser();
            gvUser.DataSource = dt;
            gvUser.DataBind();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string[] arrIDs = hdnIDs.Value.Split(',');
            foreach (var item in arrIDs)
            {
                if (item != "")
                {
                    int id = Convert.ToInt32(item);
                    var user = nBLC.RowUser(id);
                    EmailContent(txtTitle.Text, user.Email, txtContent.Text);
                }
            }
        }

        public void EmailContent(string title, string email, string strcontent)
        {
            try
            {
                string toMail = Config.GetConfigValue("EmailTo");

                string content = string.Format("<div style='font-family:Arial;font-size:9pt;'><b>Thông tin liên hệ từ website: {0} </b>", "http://" + Request.Url.Host.ToString());

                content += "<br/>==============================================================<br/>";
                content += "Email: " + email + "<br/>";
                content += "Nội dung:";
                content += "<div style='color:#070e6e; padding-left:10px;'>";
                content += strcontent.Replace("\r\n", "<br/>");
                content += "<div>";

                content += "<br/>==============================================================<br/>";
                content += @"<span style='font-size:8pt;'>Đây là email gửi từ hệ thống Website. Quý Công ty có nhu cầu liên hệ với Khách hàng, Quý Công ty 
vui lòng không Reply lại địa chỉ email này. <br/>
Để liên lạc với Khách hàng của mình, xin vui lòng tạo Email mới để trả lời theo địa chỉ được cung cấp trong nội dung bên trên.<span></div>
";

                string fromMail = email;
                string error = "";
                if (sendservice(toMail, fromMail, content, title, ref error))
                    Alert.Show("Email da duoc gui thanh cong!");
                else
                    Alert.Show("Send mail failed:" + error);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private bool sendservice(string to, string from, string sContent, string subject, ref string error)
        {
            try
            {
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential("hoangnhan6879@gmail.com", "srzvgnlstsuoxmqy");
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("Sever@gmail.com", "KythuatXvnet", System.Text.Encoding.UTF8);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = sContent;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //mail.ReplyTo = new MailAddress(from);
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (System.Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}