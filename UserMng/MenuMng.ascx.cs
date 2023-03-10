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
    public partial class MenuMng : XVNET_ModuleControl
    {

        MenuMng_BLC blc_menu = new MenuMng_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                ResetMenu();
                BindParent();
                if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
                {
                    BindRootCategory();
                }
                Bind_Map_Item("");
                if (!string.IsNullOrEmpty(Helper.ValidateParam("div_img", string.Empty)))
                
                    div_img.Visible = true;
                
                else
                
                    div_img.Visible = false;
                
            }

        }

        #region MainMethods

        private void BindRootCategory()
        {
            treeCategory.Nodes.Clear();
            int cID = 0;
            TMenu ent = blc_menu.RowMenu_By_Key("menupage");
            if (!MRI.IsAdmin)
            {
                cID = ent.Menu_ID;
            }

            DataTable dt = blc_menu.RowsMenu(cID, -1, -1, "");
            int stt = dt.Rows.Count + 1;
            txtSortOrder.Text = stt.ToString();
            TreeNode node = null;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    node = new TreeNode();
                    node.Text = string.Format("{0}-{1}-{2}", dr["Menu_ID"], dr["Name"], dr["Sort_Order"]);
                    node.Value = dr["Menu_ID"].ToString();
                    treeCategory.Nodes.Add(node);
                    node.Expand();
                    BindMenuByParentID(node);
                }
            }
            catch (System.Exception e)
            {
                e.ToString();
            }
        }

        private void BindMenuByParentID(TreeNode p_node)
        {
            int parentID = Convert.ToInt32(p_node.Value);
            DataTable dt = blc_menu.RowsMenu(parentID, -1, -1, "");

            TreeNode node = null;
            foreach (DataRow dr in dt.Rows)
            {
                node = new TreeNode();
                node.Text = string.Format("{0}-{1}-{2}", dr["Menu_ID"], dr["Name"], dr["Sort_Order"]);
                node.Value = dr["Menu_ID"].ToString();
                p_node.ChildNodes.Add(node);
                node.Expand();
                BindMenuByParentID(node);
            }
        }

        private void BindMenuInfo()
        {
            TMenu ent = blc_menu.RowMenu(this.MenuID);
            if (ent != null)
            {
                txtKeyWord.Text = ent.Keyword;
                rdoMenuActive.Checked = ent.Status == 1;
                rdoMenuUnActive.Checked = ent.Status == 0;
                txtUrl.Text = ent.Alias_Url;
               // txtParentID.Text = ent.Parent_ID.ToString();
                if (ent.Parent_ID != 0)
                    ddl_parent.SelectedValue = ent.Parent_ID.ToString();
                else Alert.Show("Không thể chọn mục này!");
                txtMenuName.Text = ent.Name;
                txtMenuName2.Text = ent.Name_2;
                txtMenuName3.Text = ent.Name_3;

                txtOption1.Text = ent.Option_1;
                txtOption2.Text = ent.Option_2;
                txtOption3.Text = ent.Option_3;
                txtOption4.Text = ent.Option_4;

                txtSortOrder.Text = ent.Sort_Order.ToString();

                rdoMenu_NoiBo_Active.Checked = ent.Require_Login;
                rdoMenu_NoiBo_UnActive.Checked = !ent.Require_Login;

                dl_type.SelectedValue = ent.Type.ToString();
                Bind_Map_Item(ent.Alias_Url);
                BindImage(Utils.TryParseLong( ent.Image,0));
              //  imgProduct.ImageUrl = GetImagePath(Utils.TryParseLong(ent.Image, 0));
                imgProduct.ImageUrl = GetImageUrl_icon(Utils.TryParseLong(ent.Image, 0),1);
            }
        }
        protected string GetImageUrl_icon(object p_fileID, int p_type)
        {
            return this.GetImageUrl(Convert.ToInt64(p_fileID), "ImageIconPath", p_type == 1 ? ImageSizeType.Big : (p_type == 2 ? ImageSizeType.Medium : ImageSizeType.Small));
        }
        private string GetImagePath(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);
            if (fileEnt != null)
                return string.Format("/{0}{1}", Config.ProductCategoryImagePath.Replace("\\", "/"), fileEnt.ServerFileName);
            return string.Empty;
        }
        private void Bind_Map_Item(string value_map_item)
        {
            int type = Convert.ToInt16(dl_type.SelectedValue);
            string sql = string.Format("[p_TMenu_Get_Map_Item_Rows] {0}", type);
            DataTable dt = (new ConnectSQL()).connect_dt(sql);

            dl_map_item.DataSource = dt;
            dl_map_item.DataTextField = "Name";
            dl_map_item.DataValueField = "Alias_URL";
            dl_map_item.DataBind();

            //txtUrl.Enabled = (type == 8 || type == 9);

            try
            {
                if (value_map_item != "")
                {
                    dl_map_item.SelectedValue = value_map_item;
                }
                else
                {
                    dl_map_item.SelectedIndex = 0;
                    if (dl_map_item.SelectedValue != "#")
                        txtUrl.Text = dl_map_item.SelectedValue;
                }
            }
            catch (Exception ex) { }
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
            if (index != -1)
            {
                string configMenuName_after = configMenuName.Substring(index);
                int indexNext = configMenuName_after.IndexOf("|");
                if (indexNext != -1)
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

            FileManager fileMng = new FileManager();

            string filePath = System.IO.Path.Combine(Server.MapPath("."), Config.GetConfigValue("ImageIconPath")).TrimEnd('\\');
            CommonFileEntity fileEnt = null;

            string[] arrImageSize = Config.GetConfigValue("ImageIconSize").Split(',');
            int swidth = Convert.ToInt32(arrImageSize[0]);
            int sheight = Convert.ToInt32(arrImageSize[1]);
            int mwidth = Convert.ToInt32(arrImageSize[2]);
            int mheight = Convert.ToInt32(arrImageSize[3]);
            int width = Convert.ToInt32(arrImageSize[4]);
            int height = Convert.ToInt32(arrImageSize[5]);

           // fileEnt = fileMng.UploadImageFile(fileImage, filePath, swidth, sheight, mwidth, mheight, width, height, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));
            fileEnt = fileMng.UploadImageFile(fileImage, filePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));
           



            TMenu ent = null;
            if (this.MenuID == -1)
            {
                ent = new TMenu();
                ent.Image = string.Format("{0}", fileEnt != null ? fileEnt.CommonFileID : 0);
            }

            else
            {
                ent = blc_menu.RowMenu(this.MenuID);
                ent.Image = string.Format("{0}",fileEnt != null ? fileEnt.CommonFileID : Utils.TryParseLong( ent.Image,0));
            }
            ent.Name = txtMenuName.Text;
            ent.Type = Convert.ToInt16(dl_type.SelectedValue);
            ent.Map_ID = 0;
            ent.Status = rdoMenuActive.Checked ? 1 : 0;
            ent.Require_Login = rdoMenu_NoiBo_Active.Checked;
            ent.Sort_Order = Convert.ToInt32(txtSortOrder.Text);
           // ent.Parent_ID = Convert.ToInt32(txtParentID.Text); 
            ent.Parent_ID = Convert.ToInt32(ddl_parent.SelectedValue.ToString());
            ent.Keyword = txtKeyWord.Text;
            ent.Alias_Url = txtUrl.Text;
            ent.Reg_Date = DateTime.Now;
            ent.Update_Date = DateTime.Now;
            ent.Reg_User = this.UserID;
            ent.Modify_User = this.UserID;
           // ent.Image = "";

            ent.Option_1 = txtOption1.Text;
            ent.Option_2 = txtOption2.Text;
            ent.Option_3 = txtOption3.Text;
            ent.Option_4 = txtOption4.Text;
            ent.Option_5 = "";
            ent.Option_6 = "";
            ent.Name_2 = txtMenuName2.Text;
            ent.Name_3 = txtMenuName3.Text;

            this.MenuID = blc_menu.AddMenu(ent);

            BindRootCategory();
            BindParent();
            ResetMenu();
            try
            {
            }
            catch
            {
                Alert.Show("Error");
            }

        }
        private void BindImage(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);
            if (fileEnt != null)
            {
                lblImage.Text = fileEnt.RealFileName;
                btnDeleteImg.Visible = true;
            }
            else
            {
                lblImage.Text = string.Empty;

                btnDeleteImg.Visible = false;
            }

        }
        protected void btnDeleteImg_Click(object sender, EventArgs e)
        {
            TMenu ent = null;
            if (this.MenuID != -1)
            {
                ent = blc_menu.RowMenu(this.MenuID);
                if (ent != null)
                {
                    ent.Image = "";
                    blc_menu.AddMenu(ent);
                    lblImage.Text = string.Empty;
                    Alert.Show("Đã xóa !");
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                blc_menu.DeleteMenu(this.MenuID);
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

        protected void dl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_Map_Item("");
        }

        private void BindParent()
        {
            int intparent = 0;
            if (!string.IsNullOrEmpty(Helper.ValidateParam("div_parent", string.Empty)))
            {
                intparent = Helper.TryParseInt(Helper.ValidateParam("div_parent", string.Empty), 0);
            }

            TMenu ent = blc_menu.RowMenu_By_Key("menupage");
            if (ent != null)
            {
                DataTable dt = blc_menu.RowsMenu(ent.Menu_ID, -1, -1, "");
                ddl_parent.Items.Clear();

                ListItem item = null;
                item = new ListItem();

                string ids = string.Empty;
                //ids = categoryID.ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    item = new ListItem();
                    item.Text = dr["Name"].ToString();
                    item.Value = dr["Menu_ID"].ToString();
                    ddl_parent.Items.Add(item);
                    if (intparent > 1)
                    {
                        BindCategoryByParentID(item, intparent);
                    }
                    //ids += dr["NewsCategoryID"].ToString() + ",";
                    //this.CategoryIDs += dr["NewsCategoryID"].ToString() + ",";

                }
                //ids = ids + "," + this.CategoryIDs;
                //this.CategoryIDs = ids.Trim(',');
                ddl_parent.Items.Insert(0, new ListItem("Root", ent.Menu_ID.ToString()));
            }
        }
        private void BindCategoryByParentID(ListItem p_item, int intparent)
        {
            int subParent = intparent - 1;
            if (subParent > 0)
            {
                int parentID = Convert.ToInt32(p_item.Value);
                DataTable dt = blc_menu.RowsMenu(parentID, -1, -1, "");

                string ids = string.Empty;
                //ids += parentID.ToString() + ",";

                ListItem item = null;
                foreach (DataRow dr in dt.Rows)
                {
                    item = new ListItem();
                    item.Text = p_item.Text + " >> " + dr["Name"].ToString();
                    item.Value = dr["Menu_ID"].ToString();
                    ddl_parent.Items.Add(item);
                    //ids += dr["NewsCategoryID"].ToString() + ",";
                    //this.CategoryIDs += dr["NewsCategoryID"].ToString() + ",";
                    BindCategoryByParentID(item, subParent);
                }
            }
        }

    }
}