using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UserMng.BLC;
using UserMng.DataDefine;
using PQT.Common.Authentication;
using System.Collections.Generic;
using PQT.DAC;
using PQT.Common;

namespace WebCus
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["LoginInfo"] = null;
            //if (!IsPostBack)
            //{
            //    Label1.Text = "";
            //    Login1.Focus();
            //}

        }

        protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            e.Cancel = true;
            string loginID = Login1.UserName.ToLower();
            string pwd = Login1.Password;
            UserMng_BLC_NTX blc = new UserMng_BLC_NTX();
            UserMngOther_BLC blc_user = new UserMngOther_BLC();
            UserEntity userEnt = blc.RowUserByLoginID(loginID);
            if (userEnt != null)
            {
                //if (userEnt.UserType != 5)
                //{
                    if (pwd == Utility.Decrypt(userEnt.Password))
                    {
                    this.LoginID = loginID;
                    this.UserID = userEnt.UserID;
                    this.Password = Utility.Decrypt(userEnt.Password);
                    Session["codesecret"] = userEnt.LoginID + "&" + userEnt.Password;
                    Session["PermissionString"] = userEnt.PermissionString;
                    Session["g_UserMemberID"] = userEnt.UserID;
                    Session["g_UserMemberType"] = userEnt.UserType;
                    Session["g_UserMemberName"] = userEnt.UserName;
                    Session["g_UserMemberLoginID"] = userEnt.LoginID;
                    Session["g_UserMemberLike"] = userEnt.UserLike;
                    Session["g_UserMemberParentID"] = userEnt.Parentid;
                    UserAuthType userType = userEnt.UserType == 1 ? UserAuthType.ADMIN : UserAuthType.DATAADMIN;


                    SiteIdentity identity = null;
                        identity = new SiteIdentity(this.UserID, this.LoginID, userEnt.UserName, 1, userEnt.CompanyName, userType, string.Empty, 5, false, 0, string.Empty);
                        Session["LoginInfo"] = identity;
                        if (Config.GetConfigValue("baotri") == "1")
                        {
                            if (userEnt.UserType == 1)
                            { Response.Redirect("/Index.aspx"); }
                            else Response.Redirect("/baotri.html");
                        }
                        else
                        {
                            //if (userEnt.UserType == 4)
                            //{
                            
                             Response.Redirect("/Index.aspx");
                            //}

                            //else Response.Redirect("/Index.aspx");// nomall
                        }
                    //if (userEnt.UserType == 4)
                    //{
                    //    if (userEnt.Gender == 200 || userEnt.Gender == 300 || userEnt.Gender == 400)
                    //    { Response.Redirect("/Accepwork.aspx"); }
                    //    else Response.Redirect("/Index.aspx");
                    //}

                    //else 
                    // Response.Redirect("/Index.aspx");

                    //if (userEnt.UserType == 1)
                    //{ Response.Redirect("/Index.aspx"); }
                    //else Response.Redirect("/baotri.html");
                    if (userEnt.UserID != 568)
                        blc_user.InsertLogg("Login", userEnt.UserID);
                }
                    else
                    {
                        this.LoginID = null;
                        Label1.Text = "Password Incorrect";
                    }
               // }

            }
            else
            {
                this.LoginID = null;
                Label1.Text = "LoginID Incorrect ";
            }

        }

        #region Property

        protected string LoginID
        {
            get
            {
                if (Session["g_LoginID"] != null)
                    return Convert.ToString(Session["g_LoginID"]);
                return string.Empty;
            }
            set
            {
                Session["g_LoginID"] = value;
            }
        }

        protected int UserID
        {
            get
            {
                if (Session["g_userID"] != null)
                    return Convert.ToInt32(Session["g_userID"]);
                return 0;
            }
            set
            {
                Session["g_userID"] = value;
            }
        }

        protected string Password
        {
            get
            {
                if (Session["g_password"] != null)
                    return Convert.ToString(Session["g_password"]);
                return string.Empty;
            }
            set
            {
                Session["g_password"] = value;
            }
        }

        protected bool IsUser
        {
            get
            {
                if (Session["g_isUser"] != null)
                    return Convert.ToBoolean(Session["g_isUser"]);
                return true;
            }
            set
            {
                Session["g_isUser"] = value;
            }
        }

        #endregion

        #region File Mng

        [System.Web.Services.WebMethod]
        public static string Delete_File_Or_Folder(object p_path, object p_Is_Folder)
        {
            try
            {
                if (p_Is_Folder.ToString() == "1")
                {
                    if (System.IO.Directory.Exists(p_path.ToString()))
                    {
                        System.IO.Directory.Delete(p_path.ToString(), true);

                        return "1";
                    }
                    else
                    {
                        return "Err: Folder not exists";
                    }
                }
                else
                {
                    if (System.IO.File.Exists(p_path.ToString()))
                    {
                        System.IO.File.Delete(p_path.ToString());
                        return "1";
                    }
                    else
                    {
                        return "Err: File not exists";
                    }
                }

            }
            catch (System.Exception ex)
            {
                return "Err:" + ex.Message;

            }
        }

        [System.Web.Services.WebMethod]
        public static string Open_Folder(object p_path)
        {
            try
            {
                if (System.IO.Directory.Exists(p_path.ToString()))
                {
                    HttpContext.Current.Session["Selected_Folder"] = p_path.ToString().Replace("/", "\\");
                    return "1";
                }
                else
                {
                    return "Err: Folder not exists to open";
                }

            }
            catch (System.Exception ex)
            {
                return "Err:" + ex.Message;

            }
        }

        [System.Web.Services.WebMethod]
        public static string Create_Folder(object p_name)
        {
            try
            {
                if (HttpContext.Current.Session["Selected_Folder"] != null)
                {
                    string path = HttpContext.Current.Session["Selected_Folder"].ToString();
                    if (System.IO.Directory.Exists(path))
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(path, p_name.ToString()));
                    return "1";
                }
                else
                {
                    return "Err: Session Error";
                }

            }
            catch (System.Exception ex)
            {
                return "Err:" + ex.Message;

            }
        }

        [System.Web.Services.WebMethod]
        public static string View_Link(object p_path)
        {
            try
            {
                string path_root = HttpContext.Current.Server.MapPath(".");
                string path = p_path.ToString().Replace("/", "\\").Replace(path_root, "");
                return path.Replace("\\", "/");

            }
            catch (System.Exception ex)
            {
                return "Err:" + ex.Message;

            }
        }

        #endregion File Mng

        #region Project

        


        #endregion Project

        [System.Web.Services.WebMethod]
        public static string Change_Order_product(object Pro_id, object order)
        {
            try
            {
                ProductMng_BLC blc_pro = new ProductMng_BLC();

                TProduct ent = blc_pro.rowProduct_ByID(Convert.ToInt32(Pro_id));
                if (ent != null)
                {
                    ent.SortOrder = Convert.ToInt32(order);
                    blc_pro.Update_Sorder(Convert.ToInt32(Pro_id), Convert.ToInt32(order));
                }
                return "1";
            }
            catch (System.Exception ex)
            {
                return "0";
                //return ex.Message;

            }
        }

        [System.Web.Services.WebMethod]
        public static string Change_Order_MapImg(object MapAllID, object order)
        {
            try
            {
                UserMng_BLC blc_dac = new UserMng_BLC();

                MapAll_ID ent = blc_dac.RowMapAll_ID(Utilis.TryParseLong(MapAllID));
                if (ent != null)
                {
                    ent.thu_tu = Utilis.TryParseInt(order);
                    blc_dac.Update_MapAll_ID(ent);
                }
                return "1";
            }
            catch (System.Exception ex)
            {
                return "0";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/inoutchecker");
        }

    }
}
