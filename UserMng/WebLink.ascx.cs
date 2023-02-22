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

namespace UserMng
{
    public partial class WebLink : XVNET_ModuleControl
    {
        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindRootLink();
        }
        #region Main

        private void BindRootLink()
        {
            treeWebLink.Nodes.Clear();

            DataTable dt = nBLC.RowsWebLink();

            TreeNode node = null;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    node = new TreeNode();
                    node.Text = dr["LinkName"].ToString();
                    node.Value = dr["LinkID"].ToString();
                    treeWebLink.Nodes.Add(node);
                    node.Expand();
                }
            }
            catch (System.Exception e)
            {
                e.ToString();
            }


        }

        private bool AddLink()
        {
            try
            {

                WebLinkEntity ent = null;
                int weblinkID;
                if (string.IsNullOrEmpty(lblID.Text))
                {
                    ent = new WebLinkEntity();
                }
                else
                {
                    weblinkID = Convert.ToInt32(lblID.Text);
                    ent = nBLC.RowWebLink(weblinkID);
                }
                ent.LinkName = txtName.Text;
                ent.Url = txtUrl.Text;
                ent.IsView = rdoIsViewY.Checked ? true : false;


                if (string.IsNullOrEmpty(lblID.Text))
                {
                    tBLC.CreateWebLink(ent);
                }
                else
                {
                    tBLC.UpdateWebLink(ent);
                }


                BindRootLink();

                return true;

            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        private void BindInfo()
        {
            WebLinkEntity ent = nBLC.RowWebLink(Convert.ToInt32(lblID.Text));
            if (ent != null)
            {
                string isView = ent.IsView.ToString();

                rdoIsViewY.Checked = ent.IsView;
                rdoIsViewN.Checked = !rdoIsViewY.Checked;

                txtName.Text = ent.LinkName;
                txtUrl.Text = ent.Url;

            }

        }

        public void ResetAll()
        {
            this.lblID.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtUrl.Text = string.Empty;
        }

        #endregion

        #region Event
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblID.Text==null)
            {
                Alert.Show("Nothing selected");
            } 
            else
            {
                try
                {
                    tBLC.DeleteWebLink(Convert.ToInt32(lblID.Text));
                    Alert.Show("Xóa thành công");
                    BindRootLink();
                    ResetAll();
                }
                catch (System.Exception ee)
                {
                    ee.ToString();
                }
            }
            
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddLink();
            ResetAll();
        }

        protected void treeWebLink_SelectedNodeChanged(object sender, EventArgs e)
        {
            this.lblID.Text = treeWebLink.SelectedValue;
            BindInfo();
        }
        #endregion

        

    }
}