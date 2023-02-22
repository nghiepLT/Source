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
    public partial class TypeMng : XVNET_ModuleControl
    {

        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {                
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
            IList<TType> list = blc_user.ListType();
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }

        private void BindInfor()
        {
            if (this.ssTypeID!=-1)
            {
                TType ent = blc_user.GetType_ByID(this.ssTypeID);
                if (ent != null)
                {
                    txt_typeName.Text = ent.TypeName;
                    txtKeyword.Text = ent.TypeKeyword;
                    txt_keywordMap.Text = ent.Keyword;
                    ddlStatus.SelectedValue = ent.status != null ? ent.status.ToString() : "1";
                }
            }
        }
#endregion

#region Create Update
        private void CreateUpdateType()
        {
            string TypeName = txt_typeName.Text;
            string TypeKeyword = txtKeyword.Text.Trim();
            string keywordmap = txt_keywordMap.Text.Trim();
            int intStatus = Helper.TryParseInt(ddlStatus.SelectedValue,1);
            if (this.ssTypeID == -1)
            {
                this.ssTypeID = blc_user.CreateType(TypeName, 0, TypeKeyword, intStatus, keywordmap);
            }
            else
            {
                if(this.ssTypeID != -1)
                {
                    if (blc_user.UpdateType(this.ssTypeID, TypeName, 0, TypeKeyword, intStatus, keywordmap) == true)
                    {
                        Alert.Show("Cập nhật thành công!");
                    }
                    else
                    {
                        Alert.Show("Error!");
                    }
                }
            }
        }
#endregion

        #region Events

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                try
                {
                    int intType = Helper.TryParseInt(e.CommandArgument.ToString(),0);
                    TType ent = blc_user.GetType_ByID(intType);
                    if(ent!=null)
                    {
                        if (blc_user.DeleteType(intType) == true)
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
                int intType = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                this.ssTypeID = intType;
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
            this.ssTypeID = -1;
            lblFileID.Text = string.Empty;
            txt_typeName.Text = string.Empty;
            txtKeyword.Text = string.Empty;
        }

        #region Property


        protected int ssTypeID
        {
            get
            {
                if (ViewState["g_TypeID"] != null)
                    return Convert.ToInt32(ViewState["g_TypeID"]);
                return -1;
            }
            set
            {
                ViewState["g_TypeID"] = value;
            }
        }


        protected string Get_Image_Status(object p_status)
        {
            if (p_status == DBNull.Value)
                return "";
            int status = Convert.ToInt32(p_status);
            return status == 1 ? "active.png" : "inactive.png";
        }

        #endregion

    }
}