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
    public partial class PoinKm : XVNET_ModuleControl
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindGird();
                bidlistKm();
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
            IList<TArea> list = blc_user.ListArea();
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }
        private void bidlistKm()
        {
             IList<TPoinKM> list = blc_user.ListPoinKM();
             dr_khoangcach.DataSource = list;
             dr_khoangcach.DataTextField = "SoKm";
             dr_khoangcach.DataValueField = "DiemKm";
            dr_khoangcach.DataBind();
            dr_khoangcach.AppendDataBoundItems = true;
            dr_khoangcach.Items.Insert(0, new ListItem("--Chọn khoảng cách--", "-1"));
            dr_khoangcach.SelectedIndex = 0;

        }

        //private void BindInfor()
        //{
        //    if (this.ssAreaID != -1)
        //    {
        //        TPoinKM ent = blc_user.GetPoinKm_ByID(this.ssAreaID);
        //        if (ent != null)
        //        {
        //          //  txt_AreaName.Text = ent.SoKm;
        //            txt_Sort.Text = ent.NoteKm;
        //            txt_areades.Text = ent.DiemKm.ToString();
        //            //txtKeyword.Text = ent.TypeKeyword;
        //            //txt_keywordMap.Text = ent.Keyword;
        //            //ddlStatus.SelectedValue = ent.status != null ? ent.status.ToString() : "1";
        //        }
        //    }
        //}
        #endregion

        #region Create Update
        private void CreateUpdateType()
        {
         
            if (this.ssAreaID == -1)
            {
                this.ssAreaID = blc_user.CreateArea(txt_areades.Text, dr_khoangcach.SelectedValue,this.idUser.ToString(), 1, 1);
            }
            else
            {
                if (this.ssAreaID != -1)
                {
                    if (blc_user.UpdateArea(this.ssAreaID, txt_areades.Text, dr_khoangcach.SelectedValue, this.idUser.ToString(), 1, 1) == true)
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
            if (e.CommandName == "DeleteItem")
            {
                try
                {
                    int poinID = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                    TPoinKM ent = blc_user.GetPoinKm_ByID(poinID);
                    if (ent != null)
                    {
                        if (blc_user.DeletePoinKm(poinID) == true)
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
                this.ssAreaID = AreaID;
              //  BindInfor();
            }
        }

        protected void btnInsertBanner_Click(object sender, EventArgs e)
        {
            resetfield();
        }

        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            CreateUpdateType();
            BindGird();
         //   BindInfor();
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
            this.ssAreaID = -1;
           // txt_AreaName.Text = string.Empty;
            txt_Sort.Text = "";
            txt_areades.Text = string.Empty;
        }

        #region Property


        protected int ssAreaID
        {
            get
            {
                if (ViewState["g_PoinID"] != null)
                    return Convert.ToInt32(ViewState["g_PoinID"]);
                return -1;
            }
            set
            {
                ViewState["g_PoinID"] = value;
            }
        }


        //protected string Get_Image_Status(object p_status)
        //{
        //    if (p_status == DBNull.Value)
        //        return "";
        //    int status = Convert.ToInt32(p_status);
        //    return status == 1 ? "active.png" : "inactive.png";
        //}
        private int idUser
        {
            get { return Helper.ValidateParam("id", -1); }
        }
        #endregion

    }
}