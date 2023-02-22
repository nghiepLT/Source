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
    public partial class PopupMemberEdit : CommonPage
    {
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        Seo_BLC blc_seo = new Seo_BLC();
        News_BLC blc_news = new News_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.UserMemberID == 0)
            {
                Response.Redirect("/dang-nhap");
            }
            if (!IsPostBack)
            {
                BindInfo();
            }
        }
        private void BindInfo()
        {
            TUser ent = blc_user.GetMember_ByID(this.UserMemberID);
            if (ent != null)
            {
                lblEmail.Text = ent.Email;
                lbl_tendangnhap.Text = ent.LoginID;
                txtUserName.Text = ent.UserName;
                txtTel.Text = ent.Tel;
                txtAddress.Text = ent.Address;
                //txtBirthday.SelectedDate = ent.Brithday!=null ? (DateTime)ent.Brithday : DateTime.Now;
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


        private bool SaveCustomer()
        {
            try
            {
                TUser ent = blc_user.GetMember_ByID(this.UserMemberID);
                if (ent != null)
                {
                    if (ent.Password == Utility.Encrypt(txt_passold.Text))
                    {
                        if (rdoGender_Male.Checked == true)
                        {
                            ent.Gender = 1;
                        }
                        else
                        {
                            ent.Gender = 2;
                        }
                        ent.Brithday = DateTime.Now;//txtBirthday.SelectedDate;
                        ent.Address = txtAddress.Text;
                        ent.UserName = txtUserName.Text;
                        ent.Tel = txtTel.Text;

                        if (!string.IsNullOrEmpty(txt_passnew.Text))
                        {
                            if (txt_passnew.Text == txt_passnewconfirm.Text)
                            {
                                ent.Password = Utility.Encrypt(txt_passnew.Text);
                            }
                            else
                            {
                                Alert.Show("Mật khẩu xác nhận không đúng!");
                                return false;
                            }
                        }

                        return blc_user.UpdateUser(ent);
                    }
                    else
                    {
                        Alert.Show("Mật khẩu không đúng!");
                        return false;
                    }
                }
                else
                {
                    Alert.Show("Cập nhật thông tin thất bại!");
                    return false;
                }
            }
            catch (System.Exception e)
            {
                Alert.Show("Cập nhật thông tin thất bại!");
                return false;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtVerifyCode.Text.ToUpper() == SoftGenImage.genStr)
            {
                if (SaveCustomer())
                {
                    MessageBox.Show("Bạn cập nhật thông tin thành công", false);
                }
            }
            else
            {
                Alert.Show("Mã xác nhận không đúng!");
            }
        }

        protected void btn_changepass_Click(object sender, EventArgs e)
        {
            //if (txtVerifyCode02.Text.ToUpper() == SoftGenImage.genStr)
            //{
            //    TUser ent = blc_user.GetUser_ByID_pass(this.UserMemberID, Utility.Encrypt(txt_passold.Text));
            //    if (ent != null)
            //    {
            //        bool b = blc_user.ChangePassword(this.UserMemberID, txt_passnew.Text);
            //        if (b == true)
            //        {
            //            Alert.Show("Thay đổi mật khẩu thành công!");
            //            Session.Remove("g_LoginMemberID");
            //            Session.Remove("g_UserMemberID");
            //            Response.Redirect("/MemberPopup/PopupMemberLogin.aspx");
            //        }
            //    }
            //}
        }

        protected void btnhome_Click(object sender, EventArgs e)
        {
            Response.Redirect("/trang-chu");
        }

        #region UserLogin

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
