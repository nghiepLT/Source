using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.API;
using UserMng.BLC;
using UserMng.DataDefine;
using PQT.Common;
using System.Net.Mail;
using PQT.DAC;
using NewsMng.BLC;

namespace WebCus
{
    public partial class RegisAccount : CommonPage
    {
        UserMng_BLC_NTX user_blc = new UserMng_BLC_NTX();
        UserMng_BLC_TX user_blc_t = new UserMng_BLC_TX();
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        Seo_BLC blc_seo = new Seo_BLC();
        News_BLC blc_news = new News_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //rdoGender_Male.Text = this.ClientLanguageMsg("lngMale");
            //rdoGender_Female.Text = this.ClientLanguageMsg("lngFemale");

            RequiredFieldValidator4.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputName"));
            RequiredFieldValidator5.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputTel"));
            //RequiredFieldValidator6.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputAdd"));
            RequiredFieldValidator2.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputPass"));
            RegularExpressionValidator5.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputPassErrorType"));
            RequiredFieldValidator1.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputUserName"));
            CompareValidator1.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputconfirmPass"));
            RegularExpressionValidator2.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgEmailInvalid"));
            RequiredFieldValidator3.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputEmail"));
            RegularExpressionValidator1.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputUserErrorType"));

            txtUserName.Attributes.Add("placeholder", this.ClientLanguageMsg("lngFullname"));
            txt_loginid.Attributes.Add("placeholder", this.ClientLanguageMsg("lngUser"));
            txtPassword.Attributes.Add("placeholder", this.ClientLanguageMsg("lngPass"));
            txtPasswordConfirm.Attributes.Add("placeholder", this.ClientLanguageMsg("lngPassConfirm"));
            txtEmail.Attributes.Add("placeholder", "Email");
            txtTel.Attributes.Add("placeholder", this.ClientLanguageMsg("lngTel"));
            txtAddress.Attributes.Add("placeholder", this.ClientLanguageMsg("lngAddress"));
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
            if (ent!=null)
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //TUserRoleMember entRole = blc_user.GetRole_ByUK(Config.GetConfigValue("TUserRoleThanhVien"));
           // if (txtVerifyCode.Text.ToUpper() == SoftGenImage.genStr)
           // {

            if (user_blc.RowUserByLoginID(txt_loginid.Text.Trim()) == null)
                {
                    if (user_blc.RowUserByEmail(txtEmail.Text.Trim()) == null)
                    {
                        int intgender = 1;
                        //if (rdoGender_Male.Checked == true)
                        //{
                       //     intgender = 1;
                       // }
                       // else
                        //{
                       //     intgender = 2;
                       // }
                        int userID = blc_user.CreateUser(txt_loginid.Text.Trim(), txtUserName.Text.Trim(), Utility.Encrypt(txtPassword.Text), txtTel.Text.Trim(), "", txtEmail.Text.Trim(), txtAddress.Text.Trim(), "", false, DateTime.Now, "", DateTime.Now, 0, DateTime.Now, 0, "1", 3, "", intgender, 0, DateTime.Now);

                        //if (entRole != null)
                        //{
                        //    blc_user.CreateMapUserRole(entRole.RoleMemberID, userID);
                        //}

                        string id = HttpUtility.UrlEncode(Utility.Encrypt(userID.ToString()));

                        string link = string.Format("{0}/Member/ConfirmActiveAccount.aspx?id={1}", this.HttpServer, id);

                        CreateTemplateMailAndSendMail(link);
                        string redirect_url = Request.Params["url"] == "checkout" ? "/trang-chu" : "/mua-hang";
                        Session["g_UserMemberID"] = userID;
                        MessageBox.Show("Cảm ơn bạn đã đang ký thành viên, 1 email xác nhận gửi tới mail bạn!", false, redirect_url);
                    }
                    else
                    {
                        Alert.Show("Địa chỉ email này đã được đăng ký!");

                    }

                }
                else
                {
                    Alert.Show("Tên đăng nhập đã tồn tại !");

                }
            //}
            //else
            //{
           //     Alert.Show("Mã xác nhận không đúng !");
           // }

        }

        private bool CreateTemplateMailAndSendMail(string p_link)
        {

            try
            {

                //string fromMail = Config.GetConfigValue("EmailTo");

                string content = string.Format("<div style='font-family:Arial;font-size:9pt;'><b>Thông tin tài khoản bạn đã đăng ký tại {0} </b><br/><br/>", "http://" + Request.Url.Host.ToString());

                content += string.Format("Chào bạn : {0}  <br/><br/>", txtUserName.Text);
                content += string.Format(@"Bạn đã đăng ký tài khoản trên website {0}.<br/>

Vui lòng click vào liên kết kích hoạt bên dưới để hoàn thành việc đăng ký.<br/>

<a href='[ConfirmLink]'> Kích hoạt </a>

<br/><br/>
Thân ái<br/>

", "http://" + Request.Url.Host.ToString());

                content = content.Replace("[ConfirmLink]", p_link);
                content += "</div>";

                //SendMail_Gmail(txtEmail.Text.Trim(), "Welcome Email for New Register at " + Config.GetConfigValue("HTTPServer"), content);
                sendservice(txtEmail.Text.Trim(), content);
                return true;


            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private bool sendservice(string to, string sContent)
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
                mail.Subject = "Thông tin liên hệ từ website: " + Request.Url.Host.ToString();
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
    }
}