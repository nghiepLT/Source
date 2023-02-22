using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PQT.API;

using NewsMng.BLC;
using NewsMng.DataDefine;
using PQT.Common;
using AjaxControlToolkit;
using PQT.DAC;
using System.Text.RegularExpressions;
using PQT.API.File;
using UserMng.BLC;
using System.Net.Mail;
using UserMng.DataDefine;

namespace WebCus
{
    public partial class PopupMemberLogin : CommonPage
    {
        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        Seo_BLC blc_seo = new Seo_BLC();
        News_BLC blc_news = new News_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
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
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "closeFancyBox()", true);
                        //Response.Redirect("/trang-chu");
                    }
                    else
                    {
                        this.LoginMemberID = null;
                        lblAlert.Text = "Mật khẩu không đúng";
                    }
                }
                else
                {
                    this.LoginMemberID = null;
                    lblAlert.Text = "Thành viên chưa được kích hoạt";
                }
            }
            else
            {
                this.LoginMemberID = null;
                lblAlert.Text = "Tên đăng nhập không tồn tại";
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
    }
}
