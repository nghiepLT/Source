using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.Common.Authentication;
using System.Data;
using PQT.API;
using PQT.API.DataDefine.Sys;
using PQT.API.File;
using PQT.Common;
using UserMng.BLC;
using UserMng.DataDefine;
using System.Drawing;
using PQT.DAC;
using NewsMng.BLC;
using System.Configuration;

namespace WebCus
{
    public partial class MemberLogin : CommonPage
    {
        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        Seo_BLC blc_seo = new Seo_BLC();
        News_BLC blc_news = new News_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserID.Attributes.Add("placeholder", this.ClientLanguageMsg("lngUser"));
            txtPass.Attributes.Add("placeholder", this.ClientLanguageMsg("lngPass"));
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
        protected void btnReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("/dang-ky");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string txtloginid = txtUserID.Text;
            string pwd = txtPass.Text;


            UserEntity userEnt = nBLC.RowUserByLoginID(txtloginid);
            if (userEnt != null)
            {
                if (userEnt.PermissionString == "1")
                {
                    if (pwd == Utility.Decrypt(userEnt.Password))
                    {
                        this.LoginMemberID = txtloginid;
                        this.UserMemberID = userEnt.UserID;
                        this.PasswordMember = userEnt.Password;//Utility.Decrypt(userEnt.Password);
                        //Session["PermissionString"] = userEnt.PermissionString;

                        //UserAuthType userType = userEnt.UserType == 1 ? UserAuthType.ADMIN : UserAuthType.DATAADMIN;


                        //SiteIdentity identity = null;
                        //identity = new SiteIdentity(this.UserID, this.LoginID, userEnt.UserName, 1, userEnt.CompanyName, UserAuthType.USER, string.Empty, 5, false, 0, string.Empty);
                        //Session["LoginInfo"] = identity;
                        //Session["Client_Login_ID"] = this.UserID;


                        string redirect_url = Request.Params["url"] == "checkout" ? "/trang-chu" : "/mua-hang";
                        Session["g_UserMemberID"] = userEnt.UserID;

                        Response.Redirect(redirect_url);
                    }
                    else
                    {
                        this.LoginMemberID = null;
                        lblAlert.Text = this.ClientLanguageMsg("lngWrongPass");
                        lblAlert.Visible = true;
                    }
                }
                else
                {
                    this.LoginMemberID = null;
                    lblAlert.Text = this.ClientLanguageMsg("lngUserNotReg");
                    lblAlert.Visible = true;
                }
            }
            else
            {
                this.LoginMemberID = null;
                lblAlert.Text = "Tên đăng nhập không tồn tại";
                lblAlert.Visible = true;
            }
        }



        #region Property

        protected string LoginMemberID
        {
            get
            {
                if (Session["g_LoginMemberID"] != null)
                    return Convert.ToString(Session["g_LoginMemberID"]);
                return string.Empty;
            }
            set
            {
                Session["g_LoginMemberID"] = value;
            }
        }

        protected int UserMemberID
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

        protected string PasswordMember
        {
            get
            {
                if (Session["g_PasswordMember"] != null)
                    return Convert.ToString(Session["g_PasswordMember"]);
                return string.Empty;
            }
            set
            {
                Session["g_PasswordMember"] = value;
            }
        }

        protected bool IsUserMember
        {
            get
            {
                if (Session["g_IsUserMember"] != null)
                    return Convert.ToBoolean(Session["g_IsUserMember"]);
                return true;
            }
            set
            {
                Session["g_IsUserMember"] = value;
            }
        }

        #endregion


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string str = "https://www.facebook.com/v2.0/dialog/oauth/?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/Member/RegisAccount.aspx&response_type=code&state=1";
            Response.Redirect(str);
        }
    }
}
