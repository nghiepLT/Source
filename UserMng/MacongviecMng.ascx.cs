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
using PQT.API;
using UserMng.BLC;
using System.Collections.Generic;
using PQT.DAC;
using PQT.Common;
using System.IO;


namespace UserMng
{
    public partial class MacongviecMng : XVNET_ModuleControl
    {

        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_BLC blc_users = new UserMng_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
               
                if (Session["g_UserMemberType"].ToString() == "2" || Session["g_UserMemberType"].ToString() == "1")
                {
                    tb_button.Visible = true;
                    tb_input.Visible = true;
                }
                else
                {
                    tb_button.Visible = false;
                    tb_input.Visible = false;
                }
                BindGird();   
            }
        }

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

#region bind

        private void BindGird()
        {
            int idnhom = this.UserMemberType;
            if (this.UserMemberType == 1 || this.UserMemberType == 2)
            {
                idnhom=-1;
            }
            IList<TMaCongViec> list = blc_user.ListWorkValue(idnhom);
            gvBanner.DataSource = list;
            gvBanner.DataBind();

            //IList<TUser> dts = blc_users.Listuser(3);
            //dr_parent.DataSource = dts;

            //dr_parent.DataTextField = "UserName";
            //dr_parent.DataValueField = "UserID";
            //dr_parent.DataBind();
            //dr_parent.AppendDataBoundItems = true;
            //dr_parent.Items.Insert(0, new ListItem("--Chọn Nhóm--", "0"));
            //dr_parent.SelectedIndex = 0;
        }

        private void BindInfor()
        {
            if (this.ssLevelID != -1)
            {
                TMaCongViec ent = blc_user.GetWorklvalue_ByID(this.ssLevelID);
                if (ent != null)
                {
                    txt_worklName.Text = ent.TenCV;
                   
                    txt_maWork.Text = ent.MaCV.ToString();
                    txt_diemsoWork.Text = ent.DiemCV.ToString();
                    txt_ghichu.Text = ent.GhiChuCV.ToString();
                    dr_parent.SelectedValue = ent.NhomCase.ToString();
                    //txtKeyword.Text = ent.TypeKeyword;
                    //txt_keywordMap.Text = ent.Keyword;
                    //ddlStatus.SelectedValue = ent.status != null ? ent.status.ToString() : "1";
                }
            }
        }
#endregion

#region Create Update
        private void CreateUpdateType()
        {
            string worklName = txt_worklName.Text.Trim();
            int levelSort =  Helper.TryParseInt(txt_Sort.Text.Trim(),1);
            //int valueoflevel = Helper.TryParseFloat(txt_valueoflevel.Text.Trim(), 1);
             int diemvc= Helper.TryParseInt(txt_diemsoWork.Text.Trim(),1);

            if (this.ssLevelID == -1)
            {
                this.ssLevelID = blc_user.CreateWorkvalue(worklName, txt_maWork.Text.Trim(), diemvc, txt_ghichu.Text.Trim(),Utils.TryParseInt(dr_parent.SelectedValue,4));
            }
            else
            {
                if (this.ssLevelID != -1)
                {
                    if (blc_user.UpdateWorkValue(this.ssLevelID, worklName, txt_maWork.Text.Trim(), diemvc, txt_ghichu.Text.Trim(), Utils.TryParseInt(dr_parent.SelectedValue, 4)) == true)
                    {
                        Alert.Show("Cập nhật thành công!");
                    }
                    else
                    {
                        Alert.Show("Error!");
                    }
                }
            }
            resetfield();
        }
#endregion

        #region Events

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["g_UserMemberType"].ToString() == "2" || Session["g_UserMemberType"].ToString() == "1")
            {
                if (e.CommandName == "DeleteItem")
                {
                    try
                    {
                        int AreaID = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                        TMaCongViec ent = blc_user.GetMACVvalue_ByID(AreaID);
                        if (ent != null)
                        {
                            if (blc_user.DeleteMaCV(AreaID) == true)
                            {
                                Alert.Show("Xóa thành công!");
                            }
                        }
                        BindGird();
                        resetfield();
                    }
                    catch
                    {
                        Alert.Show("Xóa lỗi!");
                    }
                }
                if (e.CommandName == "EditItem")
                {
                    int AreaID = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                    this.ssLevelID = AreaID;
                    BindInfor();
                }
            }
            else Alert.Show("NO Action !");
        }

        protected void btnInsertBanner_Click(object sender, EventArgs e)
        {
            resetfield();
        }

        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            CreateUpdateType();
            BindGird();
            BindInfor();
        }

        protected void btnDeleteBanner_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
                Alert.Show("Xóa lỗi!");
            }
        }

        #endregion

        private void resetfield()
        {
            this.ssLevelID = -1;
            txt_diemsoWork.Text = string.Empty;
            txt_Sort.Text = "1";
            txt_maWork.Text = string.Empty;
            txt_worklName.Text = string.Empty;
        }

        #region Property


        protected int ssLevelID
        {
            get
            {
                if (ViewState["g_WorkID"] != null)
                    return Convert.ToInt32(ViewState["g_WorkID"]);
                return -1;
            }
            set
            {
                ViewState["g_WorkID"] = value;
            }
        }


        //protected string Get_Image_Status(object p_status)
        //{
        //    if (p_status == DBNull.Value)
        //        return "";
        //    int status = Convert.ToInt32(p_status);
        //    return status == 1 ? "active.png" : "inactive.png";
        //}
        public int UserMemberType
        {
            get
            {
                if (Session["g_UserMemberType"] != null)
                    return Convert.ToInt32(Session["g_UserMemberType"]);
                return 0;
            }
            set
            {
                Session["g_UserMemberType"] = value;
            }
        }
        #endregion

    }
}