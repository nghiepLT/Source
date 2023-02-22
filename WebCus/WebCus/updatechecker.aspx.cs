using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.DataDefine;
using UserMng.BLC;
using PQT.Common;
using PQT.API;
using PQT.API.DataDefine.Sys;
using PQT.API.File;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Configuration;

namespace WebCus
{
    public partial class updatechecker : CommonPage
    {
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC_TX BLC = new UserMng_BLC_TX();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Action = "/updatechecker";
            if (UserMemberID == 0)
            {
                div_login.Visible = true;
                div_update.Visible = !div_login.Visible;
            }
            else {
                div_login.Visible = false;
                div_update.Visible = !div_login.Visible;
            }
            string query = Request.QueryString["id"];
            if (query != "")
                txtUserID.Text = query;
           // txtUserID.Attributes.Add("placeholder", this.ClientLanguageMsg("lngUser"));
           // txtPass.Attributes.Add("placeholder", this.ClientLanguageMsg("lngPass"));
        }
        protected void LinkButton3_Command(object sender, CommandEventArgs e)
        {
            string txtloginid = HiddenField1.Value.Trim();
            // string pwd = txtPass.Text;
            string lydo = "";
            int status = 5;
            switch (e.CommandName)
            {
                case "nghibuoisang":
                    lydo = "Nghỉ Buổi Sáng";
                    status = 6;
                    break;
                case "nghibuoichieu":
                    lydo = "Nghỉ Buổi Chiều";
                    status = 7;
                    break;
                case "nghingay":
                    lydo = "Nghỉ 1 Ngày";
                    status = 8;
                    break;
                case "nghikhac":
                    lydo = txt_lydo.Text;
                    break;
            }


            if (!string.IsNullOrEmpty(txtloginid))
            {
               // UserEntity userEnt = nBLC.RowUserByMaNV(txtloginid);
                CheckinoutEntity checkentity = new CheckinoutEntity();
                //if (userEnt != null)
                //{
                    //if (userEnt.PermissionString == "1")
                    //{
                    //    if (pwd == Utility.Decrypt(userEnt.Password))
                    //    {
                    //  this.LoginMemberID = txtloginid;
                    //  this.UserMemberID = userEnt.UserID;
                    //  this.PasswordMember = userEnt.Password;//Utility.Decrypt(userEnt.Password);
                    // string redirect_url = Request.Params["url"] == "checkout" ? "/trang-chu" : "/mua-hang";
                    //   Session["g_UserMemberID"] = userEnt.UserID;

                    // Response.Redirect(redirect_url);
                    //    }
                    //    else
                    //    {
                    //        this.LoginMemberID = null;
                    //        lblAlert.Text = this.ClientLanguageMsg("lngWrongPass");
                    //        lblAlert.Visible = true;
                    //    }
                    //}
                    //else
                    //{
                    //    this.LoginMemberID = null;
                    //    lblAlert.Text = this.ClientLanguageMsg("lngUserNotReg");
                    //    lblAlert.Visible = true;
                    //}
                    checkentity.BarCodeUser = txtloginid;
                    checkentity.DateCheck = DateTime.Now;
                    checkentity.IDuser = Utilis.TryParseInt(HiddenField2.Value);
                    checkentity.LyDoCheck = lydo;
                    checkentity.NameUser = HiddenField.Value;
                    checkentity.Status = status;
                    checkentity.TimesIn = txt_totimes.Text;// đến
                    checkentity.TimesOut = txt_fromtimes.Text;   // từ                 
                    checkentity.Imgcheck = "No";
                    if (BLC.AddCheckInOut(checkentity))
                    {

                        lblAlert.Text = "Nhân Viên :  " + HiddenField.Value + " - Đã Xác Nhận !";
                        lblAlert.ForeColor = Color.Green;
                        lblAlert.Visible = true;
                        txtUserID.Text = string.Empty;
                    }
                }
                else
                {
                    this.LoginMemberID = null;
                    lblAlert.Text = "Mã Nhân viên Không Đúng !";
                    lblAlert.Visible = true;
                    lblAlert.ForeColor = Color.Red;
                }
            //}
            //else
            //{
            //    lblAlert.Text = "Nhập Mã Nhân Viên !";
            //    lblAlert.Visible = true;
            //    lblAlert.ForeColor = Color.Red;
            //}
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string txtloginid = txtUsername.Text.Trim();
            string pwd = txtPass.Text;  
            if (!string.IsNullOrEmpty(txtloginid))
            {               
              //  UserEntity userEnt = nBLC.RowUserByMaNV(txtloginid);
              //  CheckinoutEntity checkentity = new CheckinoutEntity();               
                UserEntity userEnt = nBLC.RowUserByLoginID(txtloginid);                
                if (userEnt != null)
                {
                    if (userEnt.PermissionString == "1" || userEnt.UserType == 2 || userEnt.UserType == 3)
                    {
                        if (pwd == Utility.Decrypt(userEnt.Password))
                       {
                    //  this.LoginMemberID = txtloginid;
                    //  this.UserMemberID = userEnt.UserID;
                    //  this.PasswordMember = userEnt.Password;//Utility.Decrypt(userEnt.Password);
                    // string redirect_url = Request.Params["url"] == "checkout" ? "/trang-chu" : "/mua-hang";
                       Session["g_UserMemberID"] = userEnt.UserID;
                       div_login.Visible = false;
                       div_update.Visible = !div_login.Visible;
                    // Response.Redirect("/updatecheckers");
                        }
                       else
                       {
                            this.LoginMemberID = null;
                            lbl_thongbao.Text = "Mật khẩu Không Đúng !";
                            lbl_thongbao.Visible = true;
                            lbl_thongbao.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        this.LoginMemberID = null;
                        lbl_thongbao.Text = "Tài khoản Không Đúng !";
                        lbl_thongbao.Visible = true;
                        lbl_thongbao.ForeColor = Color.Red;
                    }                    
                }
                else
                {
                    this.LoginMemberID = null;
                    lbl_thongbao.Text = "Tài khoản Không tồn tại !";
                    lbl_thongbao.Visible = true;
                    lbl_thongbao.ForeColor = Color.Red;
                }
            }
            else {
                lbl_thongbao.Text = "Nhập tài khoản & mật khẩu !";
                lbl_thongbao.Visible = true;
                lbl_thongbao.ForeColor = Color.Red;
            }
        }
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
       
    }

}