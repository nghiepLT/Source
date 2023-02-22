using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using PQT.API;
using PQT.Common;
using NewsMng.BLC;
using NewsMng.DataDefine;

namespace NewsMng
{
    public partial class SelectCategoryPopup : XVNET_ModuleControl
    {
        NewsMng_BLC_TX tBLC = new NewsMng_BLC_TX();
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRootCategory();
                SelectedNode();
            }
        }

        #region Main

        private void BindRootCategory()
        {
            treeCategory.Nodes.Clear();

            DataTable dt = nBLC.RowsNewsCategoryByParentID(0, 1);

            TreeNode node = null;
            foreach (DataRow dr in dt.Rows)
            {
                node = new TreeNode();
                node.Text = dr["Name"].ToString();
                node.Value = dr["NewsCategoryID"].ToString();
                treeCategory.Nodes.Add(node);
                node.Expand();
                BindCategoryByParentID(node);
            }

        }

        private void BindCategoryByParentID(TreeNode p_node)
        {
            int parentID = Convert.ToInt32(p_node.Value);
            DataTable dt = nBLC.RowsNewsCategoryByParentID(parentID, 1);

            TreeNode node = null;
            foreach (DataRow dr in dt.Rows)
            {
                node = new TreeNode();
                node.Text = dr["Name"].ToString();
                node.Value = dr["NewsCategoryID"].ToString();
                p_node.ChildNodes.Add(node);
                BindCategoryByParentID(node);
            }
        }

        private void SelectedNode()
        {
            foreach (TreeNode node in treeCategory.Nodes)
            {
                SelectedChildNodes(node, "," + this.NewsCategoryIDs + ",");
            }
        }

        private void SelectedChildNodes(TreeNode node, string inValue)
        {
            if (inValue.IndexOf("," + node.Value + ",") > -1)
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

        #region Events

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (TreeNode node in treeCategory.CheckedNodes)
            {
                ids += node.Value + ",";
            }
            ids = ids.TrimEnd(',');
            ScriptManager.RegisterStartupScript(btnSelect, btnSelect.GetType(), "ReloadCategory", string.Format("window.close(); window.opener.ReloadCategory('{0}')", ids), true);

        }


        protected void treeCategory_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }



        #endregion

        #region property


        protected int NewsCategoryID
        {
            get
            {
                if (ViewState["g_NewsCategoryID"] != null)
                    return Convert.ToInt32(ViewState["g_NewsCategoryID"]);
                return -1;
            }
            set
            {
                ViewState["g_NewsCategoryID"] = value;
            }

        }

        private string NewsCategoryIDs
        {
            get
            {
                return Helper.ValidateParam("CategoryIDs", string.Empty);
            }
        }

        #endregion


    }
}