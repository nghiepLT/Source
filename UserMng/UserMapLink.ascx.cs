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
    public partial class UserMapLink : XVNET_ModuleControl
    {
        UserMng_BLC blc_user = new UserMng_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.idUser != -1)
                {
                    BindListMap();
                    BindRootMenu();
                    SelectedNode();
                    treeMenuAdmin.ExpandAll();
                }
                //BindListMenuAmdin();
            }
        }

        #region bind list

        private void BindListMap()
        {
            string strMapLinkID = "";
            IList<TUserMapLink> list = blc_user.RowsUserMapLink_ByUserID(this.idUser);
            foreach (TUserMapLink item in list)
            {
                strMapLinkID += item.MenuID + ";";
            }
            hdnCategoryIDs.Value = strMapLinkID.TrimEnd(';');
        }

        private void BindRootMenu()
        {
            treeMenuAdmin.Nodes.Clear();
            DataTable dt = blc_user.RowsMenuAdminByParentID(0, 1, 0,-1);
            TreeNode node = null;
            foreach (DataRow dr in dt.Rows)
            {
                node = new TreeNode();
                node.Text = dr["MenuName"].ToString();
                node.Value = dr["MenuID"].ToString();
                treeMenuAdmin.Nodes.Add(node);
                node.Expand();
                BindMenuAdminByParentID(node);
            }

           // dt = blc_user.RowsMenuAdminByParentID(0, 1, 0, -1, "PagePopupMng");
            dt = blc_user.RowsMenuAdminByParentID(0, 1, 0, -1, "NONULL");
            foreach (DataRow dr in dt.Rows)
            {
                node = new TreeNode();
                node.Text = dr["MenuName"].ToString();
                node.Value = dr["MenuID"].ToString();
                treeMenuAdmin.Nodes.Add(node);
                node.Expand();
                BindMenuAdminByParentID(node);
            }
        }

        private void BindMenuAdminByParentID(TreeNode p_node)
        {
            int parentID = Convert.ToInt32(p_node.Value);
            DataTable dt = blc_user.RowsMenuAdminByParentID(0, 1, parentID,-1);

            TreeNode node = null;
            foreach (DataRow dr in dt.Rows)
            {
                node = new TreeNode();
                node.Text = dr["MenuName"].ToString();
                node.Value = dr["MenuID"].ToString();
                p_node.ChildNodes.Add(node);
                BindMenuAdminByParentID(node);
            }
        }

        private void SelectedNode()
        {
            if (!string.IsNullOrEmpty(hdnCategoryIDs.Value))
            {
                foreach (TreeNode node in treeMenuAdmin.Nodes)
                {
                    SelectedChildNodes(node, ";" + hdnCategoryIDs.Value + ";");
                }
            }
        }

        private void SelectedChildNodes(TreeNode node, string inValue)
        {
            if (inValue.IndexOf(";" + node.Value + ";") > -1)
            {
                node.Checked = true;
            }

            if (node.ChildNodes.Count > 0)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    SelectedChildNodes(node.ChildNodes[i], inValue);
                }
            }
        }

        #endregion

        #region event Button

        protected void btnSave_Click(object a, EventArgs e)
        { 
            bool checkSave = false;
            if (treeMenuAdmin.CheckedNodes.Count > 0)
            {
                blc_user.DeleteUserMapLink_byUser(this.idUser);

                TUserMapLink ent = null;
                foreach (TreeNode node in treeMenuAdmin.CheckedNodes)
                {
                    ent = new TUserMapLink();
                    ent.UserID = this.idUser;
                    ent.MenuID = Helper.TryParseInt(node.Value,0);
                    blc_user.CreateUserMapLink(ent);
                    checkSave = true;
                }
            }
            if (checkSave == true)
            {
                Alert.Show("Cập nhật thành công!");
            }
            //else
            //{
            //    blc_new.DeleteNewsToCategory(-1, this.NewsID);
            //    string uk = Helper.ValidateParam("uk", "");
            //    TNewsCategory entCat = blc_new.RowNewsCategory_ByUK(uk);
            //    if (entCat != null)
            //    {
            //        TNewsToCategory ent = new TNewsToCategory();
            //        ent.NewsCategoryID = Convert.ToInt32(entCat.NewsCategoryID);
            //        ent.NewsID = this.NewsID;
            //        blc_new.AddNewsToCategory(ent);
            //    }
            //}
        }

        //private void HandleOnTreeViewAfterCheck(Object sender, TreeNodeEventArgs e)
        //{
        //    CheckTreeViewNode(e.Node, e.Node.Checked);
        //}

        //private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
        //{
        //    foreach (TreeNode item in node.ChildNodes)
        //    {
        //        item.Checked = isChecked;

        //        if (item.ChildNodes.Count > 0)
        //        {
        //            this.CheckTreeViewNode(item, isChecked);
        //        }
        //    }
        //}

        #endregion

        #region protected

        private int idUser
        {
            get { return Helper.ValidateParam("id", -1); }
        }

        #endregion

    }
}