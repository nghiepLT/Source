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
using PQT.API.DataDefine.Sys;
using PQT.API.File;
using PQT.Common;
using PQT.DAC;
using System.Collections.Generic;
using System.IO;
using PQT.Controls;

namespace UserMng
{
    public partial class UserDetailMng : XVNET_ModuleControl
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        int pageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                //bindRole();
                BindGridUser();
            }
        }

        //private void bindRole()
        //{
        //    IList<TUserRoleMember> list = blc_user.ListRole();
        //    ddl_role.DataSource = list;
        //    ddl_role.DataTextField = "RoleName";
        //    ddl_role.DataValueField = "RoleMemberID";
        //    ddl_role.DataBind();
        //}

        #region MainMethods

        private void BindGridUser()
        {
            DataTable dt = blc_user.List_Manager_Member(this.CurrentPage, pageSize, -1,-1);
            gvBanner.DataSource = dt;
            gvBanner.DataBind();

            int recordCount = blc_user.Count_Manager_Member();
            PQTPager1.RecordCount = recordCount;
            if (recordCount > 0)
            {
                PQTPager1.PageSize = pageSize;
                PQTPager1.PagerButtonCount = 5;
                PQTPager1.CurrentPageIndex = this.CurrentPage - 1;
            }
        }

        private void BindUserDetailInfo()
        {
            TUser ent = blc_user.GetMember_ByID(this.UserDetail);
            if(ent!=null)
            {
                lbl_fullname.Text = ent.UserName;
                lblEmail.Text = ent.Email;
                lbl_loginid.Text = ent.LoginID;
                lblYahooID.Text = ent.YahooID;
                lbl_phone.Text = ent.Tel;
                lbl_brithday.Text = ent.Brithday!=null?ent.Brithday.Value.ToString("dd/MM/yyyy"):string.Empty;
                lbl_gioitinh.Text = StrGender(ent.Gender);
                lblAddress.Text = ent.Address;
                lblRegDate.Text = ent.RegDate != null ? ent.RegDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                lbl_like.Text = SoLike(ent.UserID);
                lbl_comment.Text = SoBinhluan(ent.UserID);
                lbl_baiviet.Text = SoBaiViet(ent.UserID);
                lbl_chucdanh.Text = RoleName(ent.UserID);
                rbt_KCYes.Checked = CheckRoleName(ent.UserID);
                if (rbt_KCYes.Checked == true)
                {
                    rbt_KCNo.Checked = false;
                }
                else
                {
                    rbt_KCNo.Checked = true;
                }
            }
        }

        #endregion

        #region Utils

        private string GetImageName(object p_fileID)
        {
            string strp_value = p_fileID!=null ? (!string.IsNullOrEmpty(p_fileID.ToString())? p_fileID.ToString() : "0") : "0";
            Int64 longValue = Convert.ToInt64(strp_value);
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(longValue);

            if (fileEnt != null)
            {
                return fileEnt.RealFileName.ToString();
            }
            return string.Empty;
        }

        protected string GetImagePath_Paint(object p_fileID, int p_type)
        {
            if (p_fileID == DBNull.Value || p_fileID == null)
                return "";
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(Convert.ToInt64(p_fileID));
            if (fileEnt != null)
                return this.GetImageUrl(Convert.ToInt64(p_fileID), "CartImagePath", p_type == 1 ? ImageSizeType.Big : (p_type == 2 ? ImageSizeType.Medium : ImageSizeType.Small));
            return string.Empty;
        }

        protected string GetImagePath_Frame(object p_fileID, int p_type)
        {
            if (p_fileID == DBNull.Value || p_fileID == null)
                return "";
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(Convert.ToInt64(p_fileID));
            if (fileEnt != null)
                return this.GetImageUrl(Convert.ToInt64(p_fileID), "FrameImagePath", p_type == 1 ? ImageSizeType.Big : (p_type == 2 ? ImageSizeType.Medium : ImageSizeType.Small));
            return string.Empty;
        }

        protected UserControl ParentCtrl()
        {
            Control objParent = Parent;
            while (!(objParent is UserControl))
            {
                objParent = objParent.Parent;
            }

            return objParent as UserControl;
        }

        private void ResetUser()
        {
            this.UserDetail = -1;
            lbl_fullname.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lbl_loginid.Text = string.Empty;
            lblYahooID.Text = string.Empty;
            lbl_phone.Text = string.Empty;
            lbl_brithday.Text = string.Empty;
            lbl_gioitinh.Text = string.Empty;
            lblAddress.Text = string.Empty;
            lbl_like.Text = string.Empty;
            lbl_comment.Text = string.Empty;
            lbl_baiviet.Text = string.Empty;
            lbl_chucdanh.Text = string.Empty;
        }

        private bool DeleteUser(int p_value)
        {
            return blc_user.DeleteUser(p_value);
        }

        #endregion

#region page
        protected void Pager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.CurrentPage = e.NewPageIndex + 1;
            BindGridUser();
        }
        private int CurrentPage
        {
            get
            {
                if (ViewState["g_CurrentPage"] != null)
                    return Convert.ToInt32(ViewState["g_CurrentPage"]);
                return 1;
            }
            set
            {
                ViewState["g_CurrentPage"] = value;
            }
        }
        public int numpage()
        {
            int page = this.CurrentPage;

            if (page > 1)
                return ((Convert.ToInt32(page) * pageSize - pageSize) > 0) ? (Convert.ToInt32(page) * pageSize - pageSize) : 0;
            return 0;
        }

#endregion
        #region Events

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                ResetUser();
                this.UserDetail = Convert.ToInt32(e.CommandArgument);
                BindUserDetailInfo();
            }
            if (e.CommandName == "DeleteItem")
            {
                 try
                 {
                     if (DeleteUser(Convert.ToInt32(e.CommandArgument)) == true)
                     {
                         Alert.Show("Delete success!");
                         ResetUser();
                         BindGridUser();
                     }
                 }
                 catch
                 {
                     Alert.Show("Delete failed!");
                 }
            }
        }

        protected void btnInsertBanner_Click(object sender, EventArgs e)
        {
            ResetUser();
        }

        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            if (rbt_KCYes.Checked == true)
            {
                string strUKRole = Config.GetConfigValue("TUserRoleKimCuong");
                TUserRoleMember ent = blc_user.GetRole_ByUK(strUKRole);
                if (ent != null)
                {
                    if (blc_user.UpdateUSerMapRole(this.UserDetail, ent.RoleMemberID) == true)
                    {
                        Alert.Show("Cập nhật thành công");

                        BindUserDetailInfo();
                    }
                }
            }
            else
            {
                if (blc_user.Updata_UserMapRole_bylike(this.UserDetail) == true)
                {
                    Alert.Show("Cập nhật thành công");

                    BindUserDetailInfo();
                }
            }
            
            //int SizeID = Helper.TryParseInt(ddl_size.SelectedValue,0);
            //float Price = Helper.TryParseFloat(txtPrice.Text.ToString().Trim(),0);
            //float Weight = Helper.TryParseFloat(txtWeight.Text.ToString().Trim(), 0);
            //int FrameModelID = Helper.TryParseInt(ddl_FrameModel.SelectedValue,0);
            //string FrameName = txtFrameName.Text;
            //int SortOrder = Helper.TryParseInt(txtPrice.Text, 0);
            
            //if (this.FrameID != -1 && !string.IsNullOrEmpty(lblFrameID.Text.Trim()))
            //{
            //    if (blc_frame.UpdateFrame(this.FrameID, addimg("Update"), SizeID, Price, Weight, FrameModelID, FrameName, SortOrder) == true)
            //    {
            //        Alert.Show("Cập nhật thành công!");
            //        BindBannerInfo();
            //        BindGridFrame();
            //    }
            //    else
            //        Alert.Show("Có lỗi!");
            //}

            //if (this.FrameID == -1 && string.IsNullOrEmpty(lblFrameID.Text.Trim()))
            //{
            //    try
            //    {
            //        int intSize = blc_frame.CreateFrame(addimg("insert"), SizeID, Price, Weight, FrameModelID, FrameName, SortOrder);
            //        this.FrameID = intSize;
            //        lblFrameID.Text = intSize.ToString();
            //        BindBannerInfo();
            //        BindGridFrame();
            //        Alert.Show("Tạo mới thành công!");
            //    }
            //    catch (System.Exception ex)
            //    {
            //        Alert.Show("Có lỗi!");
            //    }
                
            //}
            
        }

        protected void btnDeleteBanner_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.UserDetail!=0)
                {
                    DeleteUser(this.UserDetail);
                    ResetUser();
                    BindGridUser();
                    Alert.Show("Delete success!");
                }
                else
                {
                    Alert.Show("Delete failed!");
                }
            }
            catch
            {
                Alert.Show("Error!");
            }
        }



        #endregion

        #region Property

        protected int UserDetail
        {
            get
            {
                if (ViewState["UserDetail"] != null)
                    return Convert.ToInt32(ViewState["UserDetail"]);
                return -1;
            }
            set
            {
                ViewState["UserDetail"] = value;
            }
        }

        protected string StrGender(object p_value)
        {
            if (p_value!=null)
            {
                int intPvaule = Helper.TryParseInt(p_value.ToString(), 0);
                return intPvaule != 0 ? (intPvaule == 1 ? "Nam" : "Nữ") : string.Empty;
            }
            return string.Empty;
        }

        protected string SoLike(object p_MemberReg)
        {
            if (p_MemberReg != null)
            {
                long p_value = Utility.TryParseLong(p_MemberReg, 0);
                long ent = blc_user.CountLike_Member(p_value);
                return ent.ToString("N0");
            }

            return string.Empty;
        }

        protected string SoBinhluan(object p_MemberReg)
        {
            if (p_MemberReg != null)
            {
                long p_value = Utility.TryParseLong(p_MemberReg, 0);
                long ent = blc_user.CountComment_Member(p_value);
                return ent.ToString("N0");
            }

            return string.Empty;
        }

        protected string SoBaiViet(object p_MemberReg)
        {
            if (p_MemberReg != null)
            {
                long p_value = Utility.TryParseLong(p_MemberReg, 0);
                long ent = blc_user.CountNews_Member(p_value);
                return ent.ToString("N0");
            }

            return string.Empty;
        }

        protected string RoleName(object p_MemberReg)
        {
            if (p_MemberReg != null)
            {
                long p_value = Utility.TryParseLong(p_MemberReg, 0);
                TUserMapRole ent = blc_user.GetUserMapRole(p_value);
                if (ent != null)
                {
                    TUserRoleMember entRole = blc_user.GetRole_ByID(Helper.TryParseInt(ent.RoleMemberID.ToString(), 0));
                    return entRole != null ? entRole.RoleName.ToString() : string.Empty;
                }
            }

            return string.Empty;
        }

        protected bool CheckRoleName(object p_MemberReg)
        {
            if (p_MemberReg != null)
            {
                long p_value = Utility.TryParseLong(p_MemberReg, 0);
                TUserMapRole ent = blc_user.GetUserMapRole(p_value);
                string strUKRole = Config.GetConfigValue("TUserRoleKimCuong");
                if (ent != null)
                {
                    TUserRoleMember entRole = blc_user.GetRole_ByID(Helper.TryParseInt(ent.RoleMemberID.ToString(), 0));
                    if (entRole!=null)
                    {
                        return entRole.KeyWord == strUKRole ? true : false;
                    }
                }
            }

            return false;
        }

        #endregion

    }
}