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
using PQT.API;
using PQT.Common;
using System.Collections.Generic;
using PQT.DAC;
using PQT.API.File;
using PQT.API.DataDefine.Sys;

namespace UserMng
{
    public partial class UserView : XVNET_ModuleControl
    {

        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC blc_user = new UserMng_BLC();
        UserMngOther_BLC blc_usr = new UserMngOther_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindUserInfo();
               // Bindddr_value();
            }
            InitClientScript();
           
        }

        //protected void Bindddr_value()
        //{
        //    IList<TValueofLevel> dt = blc_user.ListValueoflevel();
        //    dr_levelUser.DataSource = dt;
        //    dr_levelUser.DataTextField = "LevelName";
        //    dr_levelUser.DataValueField = "idlevel";
        //    dr_levelUser.DataBind();

        //}


        #region MainMethods

        private void BindUserInfo()
        {
            UserMng_BLC_NTX blc = new UserMng_BLC_NTX();
            UserEntity reEnt = blc.RowUser(this.UserID);
            if ( btnSave.Text.Trim().Replace(" ","").Replace("/","").ToUpperInvariant() == "LƯU")
            {
                if (reEnt != null)
                {
                    txtUsername.Text = reEnt.UserName;
                    string pwd = Utility.Decrypt(reEnt.Password);
                    txtPassword.Attributes.Add("value", pwd);
                    txtConfirmPassword.Attributes.Add("value", pwd);
                    txtAddress.Text = reEnt.Address;
                    txtTel.Text = reEnt.Tel;
                    txtFax.Text = reEnt.Fax;
                    lblIsExpire.Text = reEnt.IsExpire ? "Yes" : "No";
                    lblExpireDate.Text = reEnt.ExpireDate.ToString("MM-dd-yyyy");
                    txtEmail.Text = reEnt.Email;
                    txt_loginid.Text = reEnt.LoginID;
                    btnSave.Text = "Change PassWord";
                    txtUsername.Focus();
                    //txtUsername.Enabled = false;
                    //txtPassword.Enabled = false;
                    //txtConfirmPassword.Enabled = false;
                    //txtEmail.Enabled = false;
                    //txtAddress.Enabled = false;
                    //txtTel.Enabled = false;
                    //txtFax.Enabled = false;
                    NhanVien ent = new NhanVien();
                    ent = blc_usr.GetNhanvien_byID(reEnt.IdNhansu);

                    if (ent != null)
                    {
                        tbttNhanvien.Visible = true;
                        img_user.ImageUrl = GetUSerImagePath(Utils.TryParseLong(ent.Image, 0));
                     
                        txt_chuyenmon.Text = ent.ChuyenMon;
                        txt_socmnd.Text = ent.CMND;
                        txt_dctamtru.Text = ent.DCTamTru;
                        txt_dcthuongtru.Text = ent.DCThuongTru;
                        txt_email.Text = ent.Email;
                       // txt_ghichunhanvien.Text = ent.GhiChuNV;
                        dr_gioitinh.SelectedValue = ent.GioiTinh;
                        txt_hoten.Text = ent.HoTen;
                        txt_kinhnghiem.Text = ent.KinhNghiem;
                        txt_masothue.Text = ent.MSThue;
                        txt_ngaycapcmnd.Text = Convert.ToDateTime(ent.NgayCMND).ToString("dd/MM/yyy");
                        txt_ngaysinh.Text =  Convert.ToDateTime(ent.NgaySinh).ToString("dd/MM/yyy");
                        
                        txt_nguyenQuan.Text = ent.NguyenQuan;
                        txt_noisinh.Text = ent.NoiSinh;
                        txt_sdt.Text = ent.SoDt;
                        txtsotknganhang.Text = ent.SoTkNganhang;
                        txt_tknganhang.Text = ent.TkNganHang;
                        dr_trinhdonhanvien.SelectedValue = ent.Trinhdo;
                        txt_dantoc.Text = ent.Dantoc;
                        txt_tongiao.Text = ent.Tongiao;
                        txt_tinhtranghonnhan.Text = ent.Tinhtranghonnhan;
                        txt_noicapcmnd.Text = ent.NoiCapCMND;

                    }
                    else tbttNhanvien.Visible = true;
                }
            }
           
          
        }
        public string GetUSerImagePath(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);
            if (fileEnt != null)
                return string.Format("/{0}{1}", Config.GetConfigValue("UserImagePath").Replace("\\", "/"), fileEnt.ServerFileName);
            return string.Empty;
        }
        private void InitClientScript()
        {
            btnSave.OnClientClick = "return CheckValidate();";
        }

        #endregion

        #region Utils

        protected UserControl ParentCtrl()
        {
            Control objParent = Parent;
            while (!(objParent is UserControl))
            {
                objParent = objParent.Parent;
            }

            return objParent as UserControl;
        }

        #endregion

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (btnSave.Text.Trim().Replace(" ", "").Replace("/", "").ToLower() == "changepassword")
                {
                    
                //    txtUsername.Enabled = true;
                //    txtPassword.Enabled = true;
                //    txtConfirmPassword.Enabled = true;
                //    txtEmail.Enabled = true;
                //    txtAddress.Enabled = true;
                //    txtTel.Enabled = true;
                //    txtFax.Enabled = true;
                //    txtUsername.Focus();
                      btnSave.Text = "Cập Nhật";
                      txtPassword.Focus();
                      
                }
                else
                    //(btnSave.Text.Trim().Replace(" ", "").Replace("/", "").ToLower() == "cậpnhật")
                {
                    UserEntity userEnt = nBLC.RowUser(this.UserID);

                    userEnt.UserName = txtUsername.Text;
                    userEnt.Password = Utility.Encrypt(txtPassword.Text);
                    userEnt.Email = txtEmail.Text;
                    userEnt.Fax = txtFax.Text;
                    userEnt.Tel = txtTel.Text;
                    userEnt.Address = txtAddress.Text;

                    tBLC.AddUser(userEnt);
                    BindUserInfo();
                    Alert.Show("Lưu thành công");
               }
            }
            catch (System.Exception ex)
            {
                Alert.Show(string.Format("Lưu thất bại: {0}", ex.Message));
            }
        }

        #endregion


    }
}