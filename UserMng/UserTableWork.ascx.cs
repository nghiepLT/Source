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
using PQT.DAC;
using System.Collections.Generic;

namespace UserMng
{
    public partial class UserTableWork : XVNET_ModuleControl
    {

        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC blc_user = new UserMng_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindGridUser();
                ResetControl();
                if (this.MRI.IsDataAdmin)
                    ddlUserType.Items.RemoveAt(0);
               // BindUserInfo();
                InitClientScript();
                //trUserType.Visible = false;
                Bindddr_value();
                tbttkt.Visible = false;
            }

         
          //  
           
        }

        #region MainMethods

        private void BindGridUser()
        {
           // DataTable dt = blc_user.RowsUser_byType(this.MRI.IsAdmin == true ? 1 : 2);

            IList<TUser> dt = blc_user.Listuser(4);
           
        
            gvUser.DataSource = dt;
            gvUser.DataBind();
            IList<TUser> dts = blc_user.Listuser(40);
            gr_kththong.DataSource = dts;
            gr_kththong.DataBind();
             IList<TUser> dta = blc_user.Listuser(50);
            GR_ktsc.DataSource = dta;
            GR_ktsc.DataBind();

            
 IList<TUser> dtsr = blc_user.Listuser(70);
 gr_ktshowroom.DataSource = dtsr;
 gr_ktshowroom.DataBind();

 IList<TUser> dtbh = blc_user.Listuser(60);
 gr_ktbh.DataSource = dtbh;
 gr_ktbh.DataBind();


            IList<TUser> dtt = blc_user.Listuser(3);
            dr_parent.DataSource = dtt;

            dr_parent.DataTextField = "UserName";
            dr_parent.DataValueField = "UserID";
            dr_parent.DataBind();
            dr_parent.AppendDataBoundItems = true;
            dr_parent.Items.Insert(0, new ListItem("KT Hỗ Trợ", "0"));
            dr_parent.SelectedIndex = 0;
           
        }

        private void BindUserInfo()
        {
            try
            {
                btnInsert.Visible = false;
                btnSave.Text = "Cập Nhật";
                tbttkt.Visible = true;
                UserMng_BLC_NTX blc = new UserMng_BLC_NTX();
                UserEntity reEnt = blc.RowUser(this.UserID);
                if (reEnt != null)
                {
                   
                    txtLoginID.Text = reEnt.LoginID;
                    txtUsername.Text = reEnt.UserName;
                    string pwd = Utility.Decrypt(reEnt.Password);
                    txtPassword.Attributes.Add("value", pwd);
                    txtConfirmPassword.Attributes.Add("value", pwd);
                    txtAddress.Text = reEnt.Address;
                    txtTel.Text = reEnt.Tel;
                    txtFax.Text = reEnt.Fax;
                    rdoIsExpireY.Checked = reEnt.IsExpire;
                    rdoIsExpireN.Checked = !reEnt.IsExpire;
                    txtExpireDate.Text = reEnt.ExpireDate.ToString("MM-dd-yyyy");
                    txtEmail.Text = reEnt.Email;
                    txtRemark.Text = reEnt.Remark;
                    ddlUserType.SelectedValue = reEnt.UserType.ToString();
                    rdoActiveYes.Checked = reEnt.PermissionString == "1";
                    rdoActiveNo.Checked = reEnt.PermissionString != "1";
                    dr_levelUser.SelectedValue = reEnt.CompanyName.Trim().ToString();
                    if(reEnt.Gender==200)
                    { rd_buoisang.Checked = true; }
                    if (reEnt.Gender == 300)
                    { rd_buoichieu.Checked = true; }
                    if (reEnt.Gender == 400)
                    { rd_phepngay.Checked = true; }
                }
                else
                    txtExpireDate.Text = DateTime.Now.AddMonths(1).ToString("MM-dd-yyyy");
            }
            catch (System.Exception ex)
            {

            }

        }
       

        #endregion

        #region Other

        private void InitClientScript()
        {
            btnDelete.OnClientClick = "return confirm('Do you want delete!');";
            btnSave.OnClientClick = "return CheckValidate();";
        }

        private void ResetControl()
        {
            this.UserID = 0;
            txtLoginID.ReadOnly = false;
            txtLoginID.Text = string.Empty;
            txtPassword.Attributes.Add("value", string.Empty);
            txtConfirmPassword.Attributes.Add("value", string.Empty);
            //comUserType.SelectedIndex = 2;
            txtLoginID.Focus();
            txtEmail.Text = string.Empty;
            dr_levelUser.SelectedIndex = 0;
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

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.UserID = Convert.ToInt32(e.CommandArgument);
            txtLoginID.ReadOnly = true;
            BindUserInfo();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
            tbttkt.Visible = true;
        }
        protected void Bindddr_value()
        {
            IList<TValueofLevel> dt = blc_user.ListValueoflevel();
            dr_levelUser.DataSource = dt;
            dr_levelUser.DataTextField = "LevelName";
            dr_levelUser.DataValueField = "idlevel";
            dr_levelUser.DataBind();

        }
        public string GetNameoflevel(object valuelevel)
        {
            string namelevel = "none";
            if (valuelevel != null)
            {
                return namelevel = blc_user.ListlevelByID(valuelevel.ToString());
            }
           else return namelevel;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
           try
           {

               int idNghi = 0;
                UserEntity userEnt = null;
                if (this.UserID > 0)
                {
                    userEnt = nBLC.RowUser(this.UserID);
                }
                else
                {
                    userEnt = nBLC.RowUserByLoginID(txtLoginID.Text);
                    if (userEnt == null)
                    {
                        userEnt = new UserEntity();
                        userEnt.RegUser = this.UserID;
                    }
                    else
                    {
                        Alert.Show("LoginID is exists");
                        return;
                    }
                }

                userEnt.UserID = this.UserID;
                userEnt.LoginID = txtLoginID.Text;
                userEnt.UserName = txtUsername.Text;
                userEnt.Password = Utility.Encrypt(txtPassword.Text);
                userEnt.Email = txtEmail.Text;
                userEnt.Fax = txtFax.Text;
                userEnt.ModifyUser = this.UserID;
                userEnt.Tel = txtTel.Text;
               
                if (rd_buoisang.Checked)
                { idNghi = 200; }
                if (rd_buoichieu.Checked)
                { idNghi = 300; }
                if (rd_phepngay.Checked)
                { idNghi = 400; }
                userEnt.UserLike = 0;
                if (userEnt.UserLike != 0)
                {
                    userEnt.UserLike = userEnt.UserLike;
                }
                userEnt.Remark = txtRemark.Text;
                userEnt.IsExpire = rdoIsExpireY.Checked;
                userEnt.ExpireDate = DateTime.Now.AddYears(1);
                userEnt.Address = txtAddress.Text;
                userEnt.CompanyName = dr_levelUser.SelectedValue.ToString().Trim() ;
                userEnt.PermissionString = rdoActiveYes.Checked ? "1" : "0";
               // userEnt.UserType = userEnt.UserType;
                userEnt.UserType = Convert.ToInt32(ddlUserType.SelectedValue);
                userEnt.Parentid = 0;
                userEnt.Gender = idNghi;
                
                tBLC.AddUser(userEnt);

                if (rd_buoisang.Checked || rd_buoichieu.Checked || rd_phepngay.Checked)
                {
                    UserNghiPhep usern = new UserNghiPhep();
                    usern.BuoiNghi = idNghi.ToString();
                    usern.IDUser = this.UserID;
                    usern.LyDo = txt_lydonghi.Text;
                    usern.Songaynghi = Utils.TryParseInt(txt_songaynghi.Text, 0);
                    usern.NgayNghi = DateTime.Now;
                    blc_user.CreateUpdate_UserNghiPhep(usern);
                }

                BindGridUser();
                BindUserInfo();
                Alert.Show("Lưu thành công");
                btnInsert.Visible = false;
            }
            catch (System.Exception ex)
           {
               Alert.Show(string.Format("Lưu thất bại: {0}", ex.Message));
           }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
                tBLC.DeleteUser(this.UserID);
                Alert.Show("Delete succecced");
                BindGridUser();
                ResetControl();
            }
            catch (System.Exception ex)
            {
                Alert.Show("Xóa thất bại");

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridUser();
        }

        protected void comSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridUser();
        }

        #endregion

        #region Property

        protected int UserID
        {
            get
            {
                if (ViewState["g_UserID"] != null)
                    return Convert.ToInt32(ViewState["g_UserID"]);
                return 0;
            }
            set
            {
                ViewState["g_UserID"] = value;
            }
        }

        #endregion

    }
}