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
using NewsMng.BLC;
using PQT.DAC;

namespace WebCus
{
    public partial class CurrentProfile : CommonPage
    {
        News_BLC blc_news = new News_BLC();
        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUserLogin();
            if (!IsPostBack)
            {
                BindUserInfo();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtVerifyCode.Text.ToUpper() == SoftGenImage.genStr)
            {
                lbl_codeimage.Text = "*";
                if (this.MemberID != 0)
                {

                    TUser ent = blc_user.GetUser_ByIDAll(this.MemberID);
                    if (ent != null)
                    {
                        if (ent.Password == Utility.Encrypt(txtPasswordold.Text))
                        {
                            lbl_Mass_passOld.Text = "*";
                            ent.UserName = txtUserName.Text.Trim();
                            ent.YahooID = txtYahoo.Text.Trim();
                            //ent.Email = txtEmail.Text.Trim();
                            ent.Address = txtAddress.Text.Trim();
                            ent.Tel = txtTel.Text.Trim();
                            int intgender = 0;
                            if (rdoGender_Male.Checked == true)
                            {
                                intgender = 1;
                            }
                            else
                            {
                                intgender = 2;
                            }
                            ent.Gender = intgender;

                            if (!string.IsNullOrEmpty(txtPasswordNew.Text))
                            {
                                if (txtPasswordNew.Text == txtPasswordConfirm.Text)
                                {
                                    lbl_mspass.Text = "*";
                                    ent.Password = Utility.Encrypt(txtPasswordNew.Text);
                                }
                                else
                                {
                                    Alert.Show("Mật khẩu xác nhận không đúng!");
                                    lbl_mspass.Text = "Mật khẩu xác nhận không đúng!";
                                }
                            }
                            if (blc_user.UpdateUser(ent) == true)
                            {
                                Alert.Show("Cập nhật thông tin thành công!");
                            }
                            else
                            {
                                Alert.Show("Cập nhật thông tin thất bại!");
                            }
                        }
                        else
                        {
                            Alert.Show("Mật khẩu không đúng!");
                            lbl_Mass_passOld.Text = "Mật khẩu không đúng!";
                        }
                    }
                }
            }
            else
            {
                Alert.Show("Mã xác nhận không đúng!");
                lbl_codeimage.Text = "Mã xác nhận không đúng!";
            }
        }

        private void BindUserInfo()
        {
            TUser ent = blc_user.GetUser_ByIDAll(this.MemberID);
            if (ent!=null)
            {
                txtUserName.Text = ent.UserName;
                lbl_loginID.Text = ent.LoginID;
                txtYahoo.Text = ent.YahooID;
                lbl_email.Text = ent.Email;
                txtTel.Text = ent.Tel;
                txtAddress.Text = ent.Address;
                if (ent.Gender == 1)
                {
                    rdoGender_Male.Checked = true;
                    rdoGender_Female.Checked = false;
                }
                else
                {
                    rdoGender_Male.Checked = false;
                    rdoGender_Female.Checked = true;
                }
            }
        }
        protected int Client_Login_ID
        {
            get
            {
                if (Session["Client_Login_ID"] != null)
                    return Convert.ToInt32(Session["Client_Login_ID"]);
                return 0;
            }
        }


        #region UserLogin

        private void CheckUserLogin()
        {
            if (this.MemberID == 0)
            {
                Response.Redirect("/trang-chu");
            }
            
        }

        protected string RoleName
        {
            get
            {
                TUserMapRole ent = blc_user.GetUserMapRole(this.MemberID);
                if (ent != null)
                {
                    TUserRoleMember entRole = blc_user.GetRole_ByID(Helper.TryParseInt(ent.RoleMemberID.ToString(), 0));
                    return entRole != null ? entRole.RoleName.ToString() : string.Empty;
                }
                return string.Empty;
            }
        }

        protected int MemberID
        {
            get
            {
                if (Session["g_MemnerID"] != null)
                    return Convert.ToInt32(Session["g_MemnerID"]);
                return 0;
            }
            set
            {
                Session["g_MemnerID"] = value;
            }
        }

        protected string MemberName
        {
            get
            {
                if (Session["g_MemnerName"] != null)
                    return Session["g_MemnerName"].ToString();
                return string.Empty;
            }
            set
            {
                Session["g_MemnerName"] = value;
            }
        }
        #endregion

     
    }
}
