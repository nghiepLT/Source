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
    public partial class TerritoryMng : XVNET_ModuleControl
    {

        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindArea();
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

        private void BindArea()
        {
            IList<TArea> list = blc_user.ListArea();
            ddl_area.DataSource = list;
            ddl_area.DataTextField = "AreaName";
            ddl_area.DataValueField = "TAreaID";
            ddl_area.DataBind();
            ddl_area.Items.Insert(0,new ListItem("Chọn khu vực", "-1"));
        }

        private void BindGird()
        {
            IList<TTerritory> list = blc_user.ListTerri();
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }

        private void BindInfor()
        {
            if (this.ssTerritoryID != -1)
            {
                TTerritory ent = blc_user.GetTerri_ByID(this.ssTerritoryID);
                if (ent != null)
                {
                    txt_TerriName.Text = ent.TerritoryName;
                    txt_Sort.Text = ent.SortOrder != null ? ent.SortOrder.ToString() : "1";
                    try
                    {
                        ddl_area.SelectedValue = ent.AreaID.ToString();
                    }
                    catch (System.Exception ex)
                    {
                        ddl_area.SelectedValue = "-1";
                    }
                }
            }
        }
#endregion

        protected string BindAreaName(object p_value)
        {
            if (p_value!=null)
            {
                int int_value = Helper.TryParseInt(p_value.ToString(), 0);
                TArea ent = blc_user.GetArea_ByID(int_value, 1);
                if (ent!=null)
                {
                    return ent.AreaName;
                }
            }
            return string.Empty;
        }

#region Create Update
        private void CreateUpdateType()
        {
            string TerriName = txt_TerriName.Text.Trim();
            int TerriSort = Helper.TryParseInt(txt_Sort.Text.Trim(), 1);
            int TAreaID = Helper.TryParseInt(ddl_area.SelectedValue, -1);
            int intStatus = 1;

            if (this.ssTerritoryID == -1)
            {
                this.ssTerritoryID = blc_user.CreateTerri(TerriName, 0, intStatus, TAreaID, TerriSort);
            }
            else
            {
                if (this.ssTerritoryID != -1)
                {
                    if (blc_user.UpdateTerri(this.ssTerritoryID, TerriName, 0, intStatus, TAreaID, TerriSort) == true)
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
                    int TerriID = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                    TTerritory ent = blc_user.GetTerri_ByID(TerriID);
                    if(ent!=null)
                    {
                        if (blc_user.DeleteTerri(TerriID) == true)
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
                int TerriID = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                this.ssTerritoryID = TerriID;
                BindInfor();
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
            this.ssTerritoryID = -1;
            txt_TerriName.Text = string.Empty;
            txt_Sort.Text = "1";
            ddl_area.SelectedValue = "-1";
        }

        #region Property


        protected int ssTerritoryID
        {
            get
            {
                if (ViewState["g_TerritoryID"] != null)
                    return Convert.ToInt32(ViewState["g_TerritoryID"]);
                return -1;
            }
            set
            {
                ViewState["g_TerritoryID"] = value;
            }
        }


        //protected string Get_Image_Status(object p_status)
        //{
        //    if (p_status == DBNull.Value)
        //        return "";
        //    int status = Convert.ToInt32(p_status);
        //    return status == 1 ? "active.png" : "inactive.png";
        //}

        #endregion

    }
}