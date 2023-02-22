using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using PQT.API;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using PQT.Common;
using NewsMng.BLC;
using NewsMng.DataDefine;

namespace NewsMng
{
    public partial class CategoryInfo : XVNET_ModuleControl
    {
        NewsMng_BLC_TX tBLC = new NewsMng_BLC_TX();
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();
        News_BLC blc_new = new News_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindCategoryInfo();
                BindRootCategory();
                SetPermission();
            }
        }




        #region Main

        private void BindRootCategory()
        {
            treeCategory.Nodes.Clear();

            DataTable dt = blc_new.dt_RowsNewsCategoryByParentID(this.ParentID, this.LangID, -1,-1);//nBLC.RowsNewsCategoryByParentID(this.ParentID, 1);

            TreeNode node = null;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    node = new TreeNode();
                    node.Text = dr["NewsCategoryID"].ToString() + "-" + dr["Name"].ToString();
                    node.Value = dr["NewsCategoryID"].ToString();
                    treeCategory.Nodes.Add(node);
                    node.Expand();
                    BindNewsCategoryByParentID(node);
                }
            }
            catch (System.Exception e)
            {
                e.ToString();
            }


        }

        private void BindNewsCategoryByParentID(TreeNode p_node)
        {
            int parentID = Convert.ToInt32(p_node.Value);
            DataTable dt = blc_new.dt_RowsNewsCategoryByParentID(parentID, this.LangID, -1,-1);//nBLC.RowsNewsCategoryByParentID(parentID, 1);

            TreeNode node = null;
            foreach (DataRow dr in dt.Rows)
            {
                node = new TreeNode();
                node.Text = dr["NewsCategoryID"].ToString() + "-" + dr["Name"].ToString();
                node.Value = dr["NewsCategoryID"].ToString();
                p_node.ChildNodes.Add(node);
                BindNewsCategoryByParentID(node);
            }
        }

        private bool AddNewsCategory()
        {
            try
            {
                FileManager fileMng = new FileManager();
                String filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["NewsCategoryImagePath"]);
                CommonFileEntity fileEnt = fileMng.UploadImageFile(fileImage, filePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));

                NewsCategoryEntity ent = null;
                if (this.NewsCategoryID == -1)
                {
                    ent = new NewsCategoryEntity();
                    ent.Image = fileEnt != null ? fileEnt.CommonFileID : 0;

                }
                else
                {
                    ent = nBLC.RowNewsCategory(this.NewsCategoryID);
                    if (fileImage.HasFile == true)
                    {
                        if (fileEnt != null)
                        {
                            fileMng.DeleteCommonFile(ent.Image, true);
                            ent.Image = fileEnt.CommonFileID;
                        }
                    }
                }

                ent.SortOrder = Convert.ToInt32(txtSortOrder.Text);
                ent.ParentID = Convert.ToInt32(txtParentID.Text);
                ent.IsView = rdoIsViewY.Checked ? true : false;
                ent.UniqueKey = txtKetWord.Text;
                ent.Keyword = txtKetWordad.Text;

                if (this.NewsCategoryID == -1)
                {
                    this.NewsCategoryID = tBLC.CreateNewsCategory(ent);
                }
                else if (ent.NewsCategoryID != ent.ParentID)
                {
                    tBLC.UpdateNewsCategory(ent);
                }
                else
                    Alert.Show("ParentID invalid");

                for (int i = 0; i < tbmlName.DataText.Rows.Count; i++)
                {
                    DataRow drName = tbmlName.DataText.Rows[i];
                    DataRow drDescriptiom = fckmlDescription.DataText.Rows[i];

                    NewsCategoryDescriptionEntity ent1 = new NewsCategoryDescriptionEntity();
                    ent1.NewsCategoryID = this.NewsCategoryID;
                    ent1.LanguageID = Convert.ToInt32(drName["LangID"]);
                    ent1.CategoryName = drName["Text"].ToString();
                    ent1.Description = drDescriptiom["Text"].ToString();
                    ent1.SubDescription = string.Empty;
                    tBLC.AddNewsCategoryDescription(ent1);
                }

                BindCategoryInfo();

                return true;

            }
            catch
            {
                return false;
            }

        }


        private void BindCategoryInfo()
        {
            if (this.UK != "")
            {
                NewsCategoryEntity ent1 = nBLC.RowNewsCategoryByUniqueKey(this.UK);
                this.NewsCategoryID = ent1 != null ? ent1.NewsCategoryID : 0;
                DataTable dt = blc_new.dt_RowsNewsCategoryByParentID(this.NewsCategoryID, this.LangID, -1,-1);//nBLC.RowsNewsCategoryByParentID(this.NewsCategoryID, 1);
                if (!this.MRI.IsAdmin)
                {
                    //if (dt.Rows.Count == 0)
                    //{
                    //    tdCategory.Visible = false;
                    //    tblInfo.Visible = false;
                    //}
                    //else
                    //{
                    //    this.ParentID = this.NewsCategoryID;
                    //}
                    if (this.IsAlone == 0)
                    {

                        this.ParentID = this.NewsCategoryID;
                        this.NewsCategoryID = -1;
                    }
                    else
                    {
                        this.ParentID = ent1.ParentID; ;
                        tdCategory.Visible = false;
                        tblInfo.Visible = false;
                    }
                    lblRootID.Text = string.Format(" (RootID = {0})", this.ParentID);
                    txtParentID.Text = this.NewsCategoryID.ToString();
                }

            }
            if (treeCategory.SelectedNode != null || this.IsAlone == 1)
            {

                this.NewsCategoryID = this.IsAlone == 0 ? Convert.ToInt32(treeCategory.SelectedValue) : this.NewsCategoryID;
                NewsCategoryEntity ent = nBLC.RowNewsCategory(this.NewsCategoryID);
                if (ent != null)
                {
                    this.ParentNewsCategoryID = ent.ParentID;
                    txtParentID.Text = ent.ParentID.ToString();
                    txtSortOrder.Text = ent.SortOrder.ToString();
                    string isView = ent.IsView.ToString();

                    rdoIsViewY.Checked = ent.IsView;
                    rdoIsViewN.Checked = !rdoIsViewY.Checked;



                    txtKetWord.Text = ent.UniqueKey;

                    DataTable dt = nBLC.RowsNewsCategoryDescriptionByNewsCategoryID(this.NewsCategoryID);
                    if (dt.Rows.Count != 0)
                    {
                        tbmlName.DataValue = FormatForMultiLanguageControl(dt, "CategoryName");
                        tbmlName.Reload();

                        fckmlDescription.DataSource = FormatForMultiLanguageControl(dt, "Description");
                        fckmlDescription.DataBind();
                    }

                    imgProduct.ImageUrl = GetImagePath(ent.Image);
                    LinkImg.Text = GetImageName(ent.Image);

                }
            }


        }

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

        private string GetImagePath(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);
            if (fileEnt != null)
                return "/" + Config.GetConfigValue("NewsCategoryImagePath").Replace("\\", "/") + "/" + fileEnt.ServerFileName;
            //return "/" + Config.NewsImagePath.Replace("\\", "/") + "/" + fileEnt.ServerFileName;
            return string.Empty;
        }

        private DataTable FormatForMultiLanguageControl(DataTable p_source, string p_currentColumnName)
        {
            DataTable dt = InitTable();
            DataRow drNew = null;
            foreach (DataRow dr in p_source.Rows)
            {
                drNew = dt.NewRow();
                drNew["Text"] = dr[p_currentColumnName];
                drNew["LangID"] = dr["LanguageID"];
                dt.Rows.Add(drNew);
            }

            return dt;
        }

        private DataTable InitTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("LangID", typeof(string));

            return dt;
        }

        #endregion

        #region Utils

        protected NewsControl ParentCtrl()
        {
            Control objParent = Parent;
            while (!(objParent is NewsControl))
            {
                objParent = objParent.Parent;
            }

            return objParent as NewsControl;
        }

        private void SetPermission()
        {
            //trKeyWord.Visible = this.MRI.IsAdmin;
            //btnDelete.Visible = this.MRI.IsAdmin;
            //btnInsert.Visible = this.MRI.IsAdmin;
            btnInsertSub.Visible = this.MRI.IsAdmin;


        }

        #endregion

        #region Events



        protected void treeCategory_SelectedNodeChanged(object sender, EventArgs e)
        {
            this.NewsCategoryID = Convert.ToInt32(treeCategory.SelectedValue);
            BindCategoryInfo();

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            this.NewsCategoryID = -1;
            tbmlName.DataValue = InitTable();
            tbmlName.Reload();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (AddNewsCategory())
            {
                BindRootCategory();

            }
            else
            {
                Alert.Show("Error");
            }
        }

        protected void btnInsertSub_Click(object sender, EventArgs e)
        {
            this.ParentNewsCategoryID = this.NewsCategoryID;
            this.NewsCategoryID = -1;
            tbmlName.DataValue = InitTable();
            tbmlName.Reload();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                NewsCategoryEntity ent = nBLC.RowNewsCategory(this.NewsCategoryID);
                if (ent != null)
                {
                    FileManager fileMng = new FileManager();
                    fileMng.DeleteCommonFile(ent.Image, true);
                    tBLC.DeleteNewsCategory(this.NewsCategoryID);
                    BindRootCategory();
                }
            }
            catch (Exception ex)
            {

                Alert.Show(ex.ToString());
            }
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

        private int IsAlone
        {
            get
            {
                return Helper.ValidateParam("IS", 0);
            }
        }

        protected int ParentNewsCategoryID
        {
            get
            {
                if (ViewState["g_ParentNewsCategoryID"] != null)
                    return Convert.ToInt32(ViewState["g_ParentNewsCategoryID"]);
                return 0;
            }
            set
            {
                ViewState["g_ParentNewsCategoryID"] = value;
            }

        }

        private string UK
        {
            get { return Helper.ValidateParam("UK", ""); }
        }

        private int ParentID
        {
            get
            {
                if (ViewState["g_ParentID"] != null)
                    return Convert.ToInt32(ViewState["g_ParentID"]);
                return 0;
            }
            set
            {
                ViewState["g_ParentID"] = value;
            }
        }

        #endregion

    }
}