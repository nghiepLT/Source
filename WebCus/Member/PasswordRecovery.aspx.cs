using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using PQT.API;
using PQT.Controls;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using PQT.Common;
using UserMng.BLC;
using UserMng.DataDefine;

using System.Net.Mail;
using PQT.DAC;
using NewsMng.BLC;


namespace WebCus
{
    public partial class PasswordRecovery : CommonPage
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        Seo_BLC blc_seo = new Seo_BLC();
        News_BLC blc_news = new News_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            //BindSeo();
            if (!IsPostBack)
            {
            }
        }
/*
        #region SEO
        protected string MetaTitle
        {
            get;
            set;
        }
        protected string MetaKey
        {
            get;
            set;
        }
        protected string MetaDes
        {
            get;
            set;
        }
        private void BindSeo()
        {
            TNewsCategory ent = blc_news.GetNewCategoryByUniqueKey("Banner03");
            if (ent != null)
            {
                TSeo entSeo = blc_seo.GetTSeo_By(ent.NewsCategoryID, 1);
                if (ent != null)
                {
                    TSeoDescription entDes = blc_seo.GetTSeoDescription(entSeo.SeoID, this.LangID);
                    if (entDes != null)
                    {
                        this.MetaTitle = entDes.SeoTitle;
                        this.MetaKey = entDes.SeoKeyWord;
                        this.MetaDes = entDes.SeoDescription;
                    }
                }
            }
        }
        #endregion
        */
        #region random
        private Random random = new Random();
        private string GenerateRandomCode(int length)
        {
            string s = "";
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        #endregion

        private bool RecoverPass()
        {
            try
            {
                TUser ent = new TUser();
                ent = blc_user.GetMember_ByLoginID(txt_loginID.Text);
                if (ent == null)
                {
                    lblMsgLogin.Text = "Tên đăng nhập không tồn tại!";
                    lblMsgLogin.Visible = true;
                    return false;
                }
                else
                {
                    if (ent.Email != txtEmail.Text.Trim())
                    {
                        lblMsg.Text = "Địa chỉ email không đúng!";
                        lblMsg.Visible = true;
                        return false;
                    }
                    else
                    {
                        if (ent.PermissionString == "1")
                        {
                            string newPass = GenerateRandomCode(8);
                            if (blc_user.ChangePassword(ent.UserID, newPass))
                            {
                                if (CreateTemplateMailAndSendMail(Config.GetConfigValue("EmailTo"), txt_loginID.Text, txtEmail.Text, newPass))
                                    MessageBox.Show("Thông tin mật khẩu đã gửi vào mail của bạn", false, "/trang-chu");
                                return true;
                            }
                            lblMsgLogin.Text = "Lỗi xảy ra!";
                            lblMsgLogin.Visible = true;
                            return false;
                        }
                        else
                        {
                            lblMsgLogin.Text = "Tài khoản chưa được kích hoạt!";
                            lblMsgLogin.Visible = true;
                            return false;
                        }

                    }
                }
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtVerifyCode.Text.ToUpper() == SoftGenImage.genStr)
            {
                RecoverPass();
            }
            else
            {
                lblMsgCode.Text = "Mã xác nhận không đúng";
                lblMsgCode.Visible = true;
            }
        }

        #region SendMail

        private bool sendservice(string to, string title, string sContent)
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
                mail.Subject = title;
                mail.Body = sContent;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                return true;

            }
            catch (System.Exception ex)
            {
                //error = ex.Message;
                return false;
            }

        }

        private bool CreateTemplateMailAndSendMail(string sFrom, string strLogin, string stremail, string strpassword)
        {
            try
            {

                ////string senderEmail = System.Configuration.ConfigurationManager.AppSettings["EmailFrom"];
                string toMail = stremail.Trim();//Config.GetConfigValue("EmailTo");

                string content = string.Format("<div style='font-family:Arial;font-size:9pt;'><b>Recover pass from website: {0} </b>", "http://" + Request.Url.Host.ToString());
                content += "<br/>==============================================================<br/>";
                content += "Your login information:<br/>";
                content += "Login: " + strLogin + "<br/>";
                content += "Email: " + stremail + "<br/>";
                content += "Password: " + strpassword + "<br/>";
                content += "<br/>==============================================================<br/></div>";

                sendservice(txtEmail.Text, "Recover pass from website " + "http://" + Request.Url.Host.ToString(), content);
                return true;


            }
            catch (Exception ex)
            {
                return false;
            }

        }




        #endregion
    }
}
