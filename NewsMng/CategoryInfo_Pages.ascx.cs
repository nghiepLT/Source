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
using System.Text.RegularExpressions;
using System.Text;
using PQT.DAC;
using UserMng.BLC;
using UserMng;
using PQT.Common.Authentication;

namespace NewsMng
{
    public partial class CategoryInfo_Pages : XVNET_ModuleControl
    {
        NewsMng_BLC_TX tBLC = new NewsMng_BLC_TX();
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();
        News_BLC blc_new = new News_BLC();
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        string keyimgother = "IconCate";
        Seo_BLC blc_seo = new Seo_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IntIsAdmin == (int)UserAuthType.ADMIN)
                tr_txtKeyWordad.Visible = true;//this.IsAdmin;
            else
            {
                tr_txtKeyWordad.Visible = false;//this.IsAdmin;
            }
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                if (!string.IsNullOrEmpty(Helper.ValidateParam("divmeta", string.Empty)))
                {
                    divmeta.Visible = true;
                }
                else
                {
                    divmeta.Visible = false;
                }
                if (!string.IsNullOrEmpty(Helper.ValidateParam("stt", string.Empty)))
                {
                    str_stt.Visible = true;
                }
                else
                {
                    str_stt.Visible = false;
                }
                if (!string.IsNullOrEmpty(str_styleMota()))
                {
                    if (str_styleMota() == "2")
                    {
                        //fckmlMetaDescription.Visible = true;
                        fckmlMetaDescription_CK.Visible = true;
                        tbmlMetaDescription.Visible = false;
                    }
                    else
                    {
                        tbmlMetaDescription.Visible = true;
                       // fckmlMetaDescription.Visible = false;
                        fckmlMetaDescription_CK.Visible = false;

                    }
                }
                else
                {
                    tbmlMetaDescription.Visible = false;
                  //  fckmlMetaDescription.Visible = false;
                    fckmlMetaDescription_CK.Visible = false;
                }

                if (!string.IsNullOrEmpty(Helper.ValidateParam("tr_imgIcon", string.Empty)))
                {
                    tr_imgIcon.Visible = true;
                }
                else
                    tr_imgIcon.Visible = false;
               
                BindParent();
                BindCategoryInfo();
                BindRootCategory();
                changetitle();
                SetPermission();
                this.NewsCategoryID = -1;

                if (!string.IsNullOrEmpty(Helper.ValidateParam("sn_parent", string.Empty)))
                {
                    sn_parent.Visible = true;
                }
                else
                {
                    sn_parent.Visible = false;
                }

                if (!string.IsNullOrEmpty(Helper.ValidateParam("tr_img", string.Empty)))
                {
                    tr_img.Visible = true;
                }
                else
                {
                    tr_img.Visible = false;
                }

                if (!string.IsNullOrEmpty(Helper.ValidateParam("div_editor", string.Empty)))
                {
                    div_editor.Visible = false;
                }
                else
                {
                    div_editor.Visible = true;
                }
            }
        }

        private int IntIsAdmin
        {
            get
            {
                if (Session["LoginInfo"] != null)
                {
                    SiteIdentity identity = (SiteIdentity)Session["LoginInfo"];
                    return identity.UserAuth;
                }
                return 0;
            }
        }

        private bool IsAdmin
        {
            get
            {
                if (Session["LoginInfo"] != null)
                {
                    SiteIdentity identity = (SiteIdentity)Session["LoginInfo"];
                    return identity.UserAuth == (int)UserAuthType.ADMIN;
                }
                return false;
            }
        }


        private void changetitle()
        {
            if (this.UK == "ykienkhachhang")
            {
                
                tbmlName.Title = "Tên";
             //   fckmlDescription.Title = "Ý kiến";
                fckmlDescription_CK.Title = "Ý kiến";
            }
        }
        private string str_styleMota()
        {
            if (!string.IsNullOrEmpty(Helper.ValidateParam("styleMota", string.Empty)))
            {
                return Helper.ValidateParam("styleMota", string.Empty);
            }
            return string.Empty;
        }

        #region img mng type

        private void BindMapIMG(Image p_img, Label p_label, int p_product, string keymap)
        {
            p_label.Text = string.Empty;
            MapAll_ID ent = blc_user.GetMapAll_ID(p_product, keymap);
            if (ent != null)
            {
                FileManager fileMng = new FileManager();
                CommonFileEntity fileEnt = fileMng.RowCommonFile(Utility.TryParseLong(ent.MapID, 0));
                if (fileEnt != null)
                {
                    p_label.Text = fileEnt.RealFileName;
                    p_img.ImageUrl = GetImagePathIcon(Utility.TryParseLong(fileEnt.CommonFileID, 0));
                }
                else
                {
                    p_label.Text = string.Empty;
                }
            }
        }

        private void BindCreateImgOther(FileUpload fileImage, int int_productID, string keyMapID)
        {
            if (int_productID != -1)
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

              //  fileEnt = fileMng.UploadImageFile(fileImage, filePath, swidth, sheight, mwidth, mheight, width, height, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));
                fileEnt = fileMng.UploadImageFile(fileImage, filePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));
                MapAll_ID ent = null;
                ent = new MapAll_ID();
                ent.MapProduct = int_productID;
                ent.KeyWord = keyMapID;
                MapAll_ID ent2 = blc_user.GetMapAll_ID(int_productID, keyMapID);
                if (ent2 != null)
                {
                    if (fileEnt != null)
                    {
                        fileMng.DeleteCommonFile(Utility.TryParseLong(ent2.MapID, 0), true);
                        ent.MapID = fileEnt.CommonFileID;
                        blc_user.UpdateMapAll_ID(ent);
                    }
                }
                else
                {
                    if (fileEnt != null)
                    {
                        ent.MapID = fileEnt.CommonFileID;
                        blc_user.CreateMapAll_ID(ent);
                    }
                }
            }
        }

        private void BindDeleteImgOther(int int_productID, string keyMapID)
        {
            MapAll_ID ent = new MapAll_ID();
            ent.MapProduct = int_productID;
            ent.KeyWord = keyMapID;
            blc_user.deleteMapAll_ID(ent);
        }
        #endregion

       

        #region parent

        private void BindParent()
        {
            int intparent = 0;
            if (!string.IsNullOrEmpty(Helper.ValidateParam("sn_parent", string.Empty)))
            {
                intparent = Helper.TryParseInt(Helper.ValidateParam("sn_parent", string.Empty), 0);
            }

            NewsCategoryEntity ent = nBLC.RowNewsCategoryByUniqueKey(this.UK);
            if (ent != null)
            {
                DataTable dt = nBLC.RowsNewsCategoryByParentID(ent.NewsCategoryID, 1);
                ddl_parent.Items.Clear();

                ListItem item = null;
                item = new ListItem();

                string ids = string.Empty;
                //ids = categoryID.ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    item = new ListItem();
                    item.Text = dr["Name"].ToString();
                    item.Value = dr["NewsCategoryID"].ToString();
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
                ddl_parent.Items.Insert(0, new ListItem("Root", ent.NewsCategoryID.ToString()));
            }
        }

        private void BindCategoryByParentID(ListItem p_item, int intparent)
        {
            int subParent = intparent - 1;
            if (subParent > 0)
            {
                int parentID = Convert.ToInt32(p_item.Value);
                DataTable dt = nBLC.RowsNewsCategoryByParentID(parentID, this.LangID);

                string ids = string.Empty;
                //ids += parentID.ToString() + ",";

                ListItem item = null;
                foreach (DataRow dr in dt.Rows)
                {
                    item = new ListItem();
                    item.Text = p_item.Text + " >> " + dr["Name"].ToString();
                    item.Value = dr["NewsCategoryID"].ToString();
                    ddl_parent.Items.Add(item);
                    //ids += dr["NewsCategoryID"].ToString() + ",";
                    //this.CategoryIDs += dr["NewsCategoryID"].ToString() + ",";
                    BindCategoryByParentID(item, subParent);
                }
            }
        }

        #endregion

        #region check delete keyword
        private bool checkDelete()
        {
            string proDelete = Config.GetConfigValue("NewDelete");
            string[] strarr = proDelete.Split('|');
            for (int i = 0; i < strarr.Count(); i++)
            {
                NewsCategoryEntity entP = nBLC.RowNewsCategory(this.NewsCategoryID);
                if (entP != null)
                {
                    if (entP.UniqueKey.ToLower().Trim() == strarr[i].ToLower().Trim())
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        #endregion

        #region Main

        private void BindRootCategory()
        {
            treeCategory.Nodes.Clear();
            if (!string.IsNullOrEmpty(this.StrProID))
            {
                pro_list.Visible = true;
                txtKeyWord.Text = this.StrProID.Trim();
                DataTable dt = nBLC.RowsNewsCategoryByParentID_andUK(this.ParentID, this.StrProID.Trim(), 1);
                int stt = dt.Rows.Count + 1;
                txtSortOrder.Text = stt.ToString();
                TreeNode node = null;
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        node = new TreeNode();
                        /*node.Text = dr["NewsCategoryID"].ToString() + "-" + dr["Name"].ToString();*/
                        node.Text = dr["Name"].ToString();
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
            else
            {
                DataTable dt = nBLC.RowsNewsCategoryByParentID(this.ParentID, 1);
                int stt = dt.Rows.Count + 1;
                txtSortOrder.Text = stt.ToString();
                TreeNode node = null;
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        node = new TreeNode();
                        //node.Text = dr["NewsCategoryID"].ToString() + "-" + dr["Name"].ToString();
                        node.Text = dr["SortOrder"].ToString() + "-" + dr["Name"].ToString();
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


        }

        private void BindNewsCategoryByParentID(TreeNode p_node)
        {
            int parentID = Convert.ToInt32(p_node.Value);
            DataTable dt = nBLC.RowsNewsCategoryByParentID(parentID, 1);

            TreeNode node = null;
            foreach (DataRow dr in dt.Rows)
            {
                node = new TreeNode();
                //node.Text = dr["NewsCategoryID"].ToString() + "-" + dr["Name"].ToString();
                node.Text = dr["SortOrder"].ToString() + "-" + dr["Name"].ToString();
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
               // CommonFileEntity fileEnt = fileMng.UploadImageFile(fileImage, filePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));

               // CommonFileEntity fileEnt = null;
                CommonFileEntity fileEnt = fileMng.UploadImageFile(fileImage, filePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));

                string[] arrImageSize = Config.GetConfigValue("NewsCategoryImageSize").Split(',');
                int swidth = Convert.ToInt32(arrImageSize[0]);
                int sheight = Convert.ToInt32(arrImageSize[1]);
                int mwidth = Convert.ToInt32(arrImageSize[2]);
                int mheight = Convert.ToInt32(arrImageSize[3]);
                int width = Convert.ToInt32(arrImageSize[4]);
                int height = Convert.ToInt32(arrImageSize[5]);


               // fileEnt = fileMng.UploadImageFile(fileImage, filePath, swidth, sheight, mwidth, mheight, width, height, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));

              //  fileMng.UploadImageFile(fileImage, filePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));


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
               
                int idcate = Helper.TryParseInt(ddl_parent.SelectedValue, 0);
                ent.ParentID = Convert.ToInt32(idcate);
                ent.IsView = rdoIsViewY.Checked ? true : false;
                ent.UniqueKey = txtKeyWord.Text;
                ent.Keyword = txtKeyWordad.Text;

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

                TSeo entSeo = new TSeo();
                long SeoIDCre = 0;
                if (divmeta.Visible == true)
                {
                    entSeo.SeoType = 1;
                    entSeo.MapID = this.NewsCategoryID;
                    entSeo.Status = 1;
                    entSeo.Uniquekey = UniKeySeo();
                    entSeo.KeyOther = "";

                    SeoIDCre = blc_seo.CreateSEO(entSeo);
                }

                for (int i = 0; i < tbmlName.DataText.Rows.Count; i++)
                {
                    DataRow drName = tbmlName.DataText.Rows[i];
                  //  DataRow drDescriptiom = fckmlDescription.DataText.Rows[i];

                 //   DataRow drMetaDescription2 = fckmlMetaDescription.DataText.Rows[i];
                    DataRow drDescriptiom = fckmlDescription_CK.DataText.Rows[i];

                    DataRow drMetaDescription2 = fckmlMetaDescription_CK.DataText.Rows[i];
                    DataRow drMetaDescription = tbmlMetaDescription.DataText.Rows[i];

                    NewsCategoryDescriptionEntity ent1 = new NewsCategoryDescriptionEntity();
                    ent1.NewsCategoryID = this.NewsCategoryID;
                    ent1.LanguageID = Convert.ToInt32(drName["LangID"]);
                    ent1.CategoryName = drName["Text"].ToString();
                    ent1.Description = drDescriptiom["Text"].ToString();
                    if (str_styleMota() == "2")
                    {
                        ent1.SubDescription = drMetaDescription2["Text"].ToString();
                    }
                    else
                    {
                        ent1.SubDescription = drMetaDescription["Text"].ToString();
                    }
                    tBLC.AddNewsCategoryDescription(ent1);

                    DataRow drTitleTag = txt_title_tag.DataText.Rows[i];
                    DataRow drKeyTag = txt_key_tag.DataText.Rows[i];
                    DataRow drDesTag = txt_des_tag.DataText.Rows[i];
                    if (divmeta.Visible == true)
                    {
                        TSeoDescription entSeoDes = new TSeoDescription();
                        entSeoDes.SeoID = SeoIDCre;
                        entSeoDes.LanguageID = Convert.ToInt32(drName["LangID"]);
                        string strSeo = !string.IsNullOrEmpty(drTitleTag["Text"].ToString()) ? drTitleTag["Text"].ToString() : drName["Text"].ToString();
                        entSeoDes.SeoTitle = strSeo;
                        entSeoDes.SeoDescription = drDesTag["Text"].ToString();
                        entSeoDes.SeoKeyWord = drKeyTag["Text"].ToString();
                        blc_seo.CreateSEODescription(entSeoDes);
                    }
                }

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
                //this.NewsCategoryID = ent1 != null ? ent1.NewsCategoryID : 0;
                //DataTable dt = nBLC.RowsNewsCategoryByParentID(this.NewsCategoryID, 1);
                int sn_newcate = ent1 != null ? ent1.NewsCategoryID : 0;
                int sn_cate = this.NewsCategoryID != -1 ? this.NewsCategoryID : sn_newcate;
                DataTable dt = nBLC.RowsNewsCategoryByParentID(sn_newcate, 1);

                this.ParentID = ent1.NewsCategoryID;
                ddl_parent.SelectedValue = this.ParentID.ToString();


                //lblRootID.Text = string.Format(" (RootID = {0})", this.ParentID);
                //txtParentID.Text = this.ParentID.ToString();


            }
            if (treeCategory.SelectedNode != null || this.IsAlone == 1)
            {

                this.NewsCategoryID = this.IsAlone == 0 ? Convert.ToInt32(treeCategory.SelectedValue) : this.NewsCategoryID;
                NewsCategoryEntity ent = nBLC.RowNewsCategory(this.NewsCategoryID);
                if (ent != null)
                {
                    this.ParentNewsCategoryID = ent.ParentID;
                    //txtParentID.Text = ent.ParentID.ToString();
                    ddl_parent.SelectedValue = ent.ParentID.ToString();

                    txtSortOrder.Text = ent.SortOrder.ToString();
                    string isView = ent.IsView.ToString();

                    rdoIsViewY.Checked = ent.IsView;
                    rdoIsViewN.Checked = !rdoIsViewY.Checked;


                    txtKeyWord.Text = ent.UniqueKey;
                    txtKeyWordad.Text = ent.Keyword;

                    DataTable dt = nBLC.RowsNewsCategoryDescriptionByNewsCategoryID(this.NewsCategoryID);
                    if (dt.Rows.Count != 0)
                    {
                        tbmlName.DataValue = FormatForMultiLanguageControl(dt, "CategoryName");
                        tbmlName.Reload();

                        if (str_styleMota() == "2")
                        {
                           // fckmlMetaDescription.DataSource = FormatForMultiLanguageControl(dt, "SubDescription");
                            //fckmlMetaDescription.DataBind();
                            fckmlMetaDescription_CK.DataSource = FormatForMultiLanguageControl(dt, "SubDescription");
                            fckmlMetaDescription_CK.DataBind();
                        }
                        else
                        {
                            tbmlMetaDescription.DataValue = FormatForMultiLanguageControl(dt, "SubDescription");
                            tbmlMetaDescription.Reload();
                        }



                        //fckmlDescription.DataSource = FormatForMultiLanguageControl(dt, "Description");
                       // fckmlDescription.DataBind();
                        fckmlDescription_CK.DataSource = FormatForMultiLanguageControl(dt, "Description");
                        fckmlDescription_CK.DataBind();
                    }

                    imgProduct.ImageUrl = GetImagePath(ent.Image);
                    LinkImg.Text = GetImageName(ent.Image);

                    BindMapIMG(imgIcon, lbl_imgIcon, this.NewsCategoryID, this.keyimgother);


                }
            }
            if (divmeta.Visible == true)
            {
                TSeo entSEO = blc_seo.GetTSeo_ByUniKeyMapID(this.NewsCategoryID, UniKeySeo());
                if (entSEO != null)
                {
                    DataTable dtSeo = blc_seo.ListSeoDescription(entSEO.SeoID);
                    if (dtSeo.Rows.Count != 0)
                    {
                        txt_title_tag.DataValue = FormatForMultiLanguageControl(dtSeo, "SeoTitle");
                        txt_title_tag.Reload();

                        txt_key_tag.DataValue = FormatForMultiLanguageControl(dtSeo, "SeoKeyWord");
                        txt_key_tag.Reload();

                        txt_des_tag.DataValue = FormatForMultiLanguageControl(dtSeo, "SeoDescription");
                        txt_des_tag.Reload();
                    }
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

        private string GetImagePathIcon(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);
            if (fileEnt != null)
                return "/" + Config.GetConfigValue("ImageIconPath").Replace("\\", "/") + "/" + fileEnt.ServerFileName;
            //return "/" + Config.NewsImagePath.Replace("\\", "/") + "/" + fileEnt.ServerFileName;
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
            //btnInsertSub.Visible = this.MRI.IsAdmin;


        }

        #endregion

        #region Events

        protected void change_sort(object sender, EventArgs e)
        { 
           if (ddl_parent.SelectedIndex != 0)
                {
                    DataTable dt1 = nBLC.RowsNewsCategoryByParentID(Helper.TryParseInt(ddl_parent.SelectedValue, 0), 1);
                    int stt = dt1.Rows.Count + 1;
                   txtSortOrder.Text= stt.ToString();
                }
              
        }

        protected void treeCategory_SelectedNodeChanged(object sender, EventArgs e)
        {
            txt_title_tag.DataValue = InitTable();
            txt_title_tag.Reload();
            txt_key_tag.DataValue = InitTable();
            txt_key_tag.Reload();
            txt_des_tag.DataValue = InitTable();
            txt_des_tag.Reload();

            this.NewsCategoryID = Convert.ToInt32(treeCategory.SelectedValue);
            BindCategoryInfo();

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            this.NewsCategoryID = -1;
            tbmlName.DataValue = InitTable();
            tbmlName.Reload();
            DataTable dt1 = nBLC.RowsNewsCategoryByParentID(this.ParentID, 1);
            int stt = dt1.Rows.Count + 1;
            txtSortOrder.Text = stt.ToString();
           
            txtKeyWord.Text = string.Empty;
            txtKeyWordad.Text = this.UK == "Blogs" ? "blogs" : "pages";
          //  txtKeyWordad.Text = string.Empty;
            LinkImg.Text = string.Empty;
            imgProduct.ImageUrl = string.Empty;

            DataTable dt = new DataTable();
           // fckmlDescription.DataSource = FormatForMultiLanguageControl(dt, "Description");
            //fckmlDescription.DataBind();
            fckmlDescription_CK.DataSource = FormatForMultiLanguageControl(dt, "Description");
            fckmlDescription_CK.DataBind();


            txt_title_tag.DataValue = InitTable();
            txt_title_tag.Reload();
            txt_key_tag.DataValue = InitTable();
            txt_key_tag.Reload();
            txt_des_tag.DataValue = InitTable();
            txt_des_tag.Reload();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (AddNewsCategory())
            {
                BindCreateImgOther(fileIcon, this.NewsCategoryID, keyimgother);

                BindParent();
                BindCategoryInfo();
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

        protected void btnDeleteImg_Click(object sender, EventArgs e)
        {
            NewsCategoryEntity ent = nBLC.RowNewsCategory(this.NewsCategoryID);
            if (ent != null)
            {
                FileManager fileMng = new FileManager();
                if (ent.Image != 0)
                {
                    fileMng.DeleteCommonFile(ent.Image, true);
                    Alert.Show("Xoá ảnh thành công");
                    BindCategoryInfo();
                }
            }
            Alert.Show("Vui lòng chọn lại danh mục!");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt1 = nBLC.RowsNewsCategoryByParentID(this.NewsCategoryID, 1);
                if (dt1.Rows.Count > 0 || (txtKeyWordad.Text != "blogs" && txtKeyWordad.Text != "pages"))
                {
                    if(this.LangID == 1 )
                        Alert.Show("Phần tử này chứa nhóm con hoặc là thành phần đặc biệt , không thể xóa ! ");
                   else Alert.Show("This category have children category or this is special name , can not delete! ");
                   
                }

                else
                {
                    if (checkDelete() == true)
                    {
                        NewsCategoryEntity ent = nBLC.RowNewsCategory(this.NewsCategoryID);
                        if (ent != null)
                        {
                            BindDeleteImgOther(this.NewsCategoryID, this.keyimgother);

                            FileManager fileMng = new FileManager();
                            if (ent.Image != 0)
                            {
                                fileMng.DeleteCommonFile(ent.Image, true);
                            }
                            blc_seo.DeleteSEO(this.NewsCategoryID, UniKeySeo());
                            tBLC.DeleteNewsCategory(this.NewsCategoryID);
                            Alert.Show("Xóa thành công");
                            BindRootCategory();
                            this.NewsCategoryID = -1;
                            tbmlName.DataValue = InitTable();
                            tbmlName.Reload();

                           // fckmlDescription.DataSource = null;
                           // fckmlDescription.DataBind();
                            fckmlDescription_CK.DataSource = null;
                            fckmlDescription_CK.DataBind();
                        }
                    }
                    else
                    {
                        Alert.Show("Danh mục tin tức đặc biệt không thể xóa chỉ được đổi tên !");
                    }

                }
            }
            catch (Exception ex)
            {

                Alert.Show(ex.ToString());
            }
        }

        #endregion

        #region property
        private string UniKeySeo()
        {
            return Config.GetConfigValue("SeoNewCate");
        }

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

        public string UK
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

        protected string StrProID
        {
            get { return Helper.ValidateParam("ProID", string.Empty); }
        }

        #endregion

    }
}