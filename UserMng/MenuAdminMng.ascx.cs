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

namespace UserMng
{
    public partial class MenuAdminMng : XVNET_ModuleControl
    {

        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC blc_user = new UserMng_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                ResetMenu();
                BindRootCategory();
            }

        }

        #region MainMethods

        private void BindRootCategory()
        {
            treeCategory.Nodes.Clear();

            DataTable dt = blc_user.RowsMenuAdminByParentID(0, 2, 0,-1);

            TreeNode node = null;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    node = new TreeNode();
                    node.Text = string.Format("{0}-{1}-{2}", dr["MenuID"], dr["MenuName"], dr["MenuType"]);
                    node.Value = dr["MenuID"].ToString();
                    treeCategory.Nodes.Add(node);
                    node.Expand();
                    BindMenuAdminByParentID(node);
                }
            }
            catch (System.Exception e)
            {
                e.ToString();
            }
        }

        private void BindMenuAdminByParentID(TreeNode p_node)
        {
            int parentID = Convert.ToInt32(p_node.Value);
            DataTable dt = blc_user.RowsMenuAdminByParentID(0, 2, parentID,-1);

            TreeNode node = null;
            foreach (DataRow dr in dt.Rows)
            {
                node = new TreeNode();
                node.Text = string.Format("{0}-{1}-{2}", dr["MenuID"], dr["MenuName"], dr["MenuType"]);
                node.Value = dr["MenuID"].ToString();
                p_node.ChildNodes.Add(node);
                BindMenuAdminByParentID(node);
            }
        }

        private void BindMenuInfo()
        {
            MenuAdminEntity ent = nBLC.RowMenuAdmin(this.MenuID);
            if (ent != null)
            {
                lblMenuID.Text = ent.MenuID.ToString();
                txtKeyWord.Text = ent.Option1;
                rdoMenuActive.Checked = ent.Option3;
                rdoMenuUnActive.Checked = !ent.Option3;
                txtUrl.Text = ent.Link;
                txtParentID.Text = ent.Option2.ToString();
                txtMenuName.Text = ent.MenuName;
                txtSortOrder.Text= ent.MenuType.ToString();
            }
        }

      
        #endregion



        #region Utils

        private string GetImageName(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);

            if (fileEnt != null)
            {
                return fileEnt.RealFileName.ToString();
            }
            return string.Empty;
        }

        protected string GetNameMenuType(object p_menuType)
        {
            string configMenuName = Config.GetConfigValue("ConfigAdminMenuType");
            int index = configMenuName.IndexOf(p_menuType.ToString());
            if (index!=-1)
            {
                string configMenuName_after = configMenuName.Substring(index);
                int indexNext = configMenuName_after.IndexOf("|");
                if (indexNext!=-1)
                    return configMenuName_after.Substring(2, indexNext - 2);
                return configMenuName_after.Substring(2);
            }
         
            return "";

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

        private void ResetMenu()
        {
            this.MenuID = -1;
            txtKeyWord.Text = string.Empty;
            txtMenuName.Text = string.Empty;
        }

        #endregion

        #region Events

        protected void treeCategory_SelectedNodeChanged(object sender, EventArgs e)
        {
            this.MenuID = Convert.ToInt32(treeCategory.SelectedValue);
            BindMenuInfo();

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ResetMenu();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CommonFileEntity fileEnt = null;
            FileManager fileMng = new FileManager();
            try
            {
                MenuAdminEntity ent = null;
                if (this.MenuID == -1)
                    ent = new MenuAdminEntity();

                else
                    ent = nBLC.RowMenuAdmin(this.MenuID);

                ent.Link = txtUrl.Text.TrimStart('/').Trim();
                ent.Option1 = txtKeyWord.Text;
                ent.Option2 = Convert.ToInt32(txtParentID.Text);
                ent.Option3 = rdoMenuActive.Checked;
                ent.MenuID = this.MenuID;
                ent.MenuName = txtMenuName.Text;
                ent.MenuType = Convert.ToInt32(txtSortOrder.Text);


                if (this.MenuID == -1)
                {
                    this.MenuID = tBLC.CreateMenuAdmin(ent);
                }
                else
                {
                    tBLC.UpdateMenuAdmin(ent);
                }

                BindRootCategory();
                ResetMenu();

            }
            catch
            {
                if (fileEnt != null)
                    fileMng.DeleteCommonFile(fileEnt.CommonFileID, true);
                Alert.Show("Error");
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                tBLC.DeleteMenuAdmin(this.MenuID);
                ResetMenu();
                BindRootCategory();
            }
            catch
            {
                Alert.Show("Xóa thất bại!");
            }
        }

        #endregion

        #region Property

        protected int MenuID
        {
            get
            {
                if (ViewState["g_MenuID"] != null)
                    return Convert.ToInt32(ViewState["g_MenuID"]);
                return -1;
            }
            set
            {
                ViewState["g_MenuID"] = value;
            }
        }

        #endregion

       

    }
}