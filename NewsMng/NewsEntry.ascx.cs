using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using PQT.API;
using PQT.API.DataDefine.Sys;
using PQT.API.File;
using PQT.Common;
using NewsMng.BLC;
using NewsMng.DataDefine;
using UserMng.BLC;
using PQT.DAC;
using UserMng;

namespace NewsMng
{
    public partial class NewsEntry : XVNET_ModuleControl
    {

        //NewsMng_BLC_TX tBLC = new NewsMng_BLC_TX();
        //NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        News_BLC blc_news = new News_BLC();
        Seo_BLC blc_seo = new Seo_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Helper.ValidateParam("divmeta", string.Empty)))
            {
                divmeta.Visible = true;
            }
            else
            {
                divmeta.Visible = false;
            }
            if (!string.IsNullOrEmpty(Helper.ValidateParam("div_ImgList", string.Empty)))
            {
                if (this.NewsID >= 0)
                {
                    btnSelectImgMap.Visible = true;
                }
                div_img.Visible = false;
            }
            else
            {
                div_img.Visible = true;
                btnSelectImgMap.Visible = false;
            }
            if (Helper.ValidateParam("div_ImgList", string.Empty) == "2")
            {
                div_img.Visible = false;
                btnSelectImgMap.Visible = false;
            }

            if (!string.IsNullOrEmpty(Helper.ValidateParam("sn_like",string.Empty)))
            {
                sn_like.Visible = true;
            }
            else
            {
                sn_like.Visible = false;
            }
            if (!string.IsNullOrEmpty(Helper.ValidateParam("div_downloadTitle", string.Empty)))
            {
                div_downloadTitle.Visible = true;
            }
            else
            {
                div_downloadTitle.Visible = false;
            }
            if (!string.IsNullOrEmpty(Helper.ValidateParam("divnoidung", string.Empty)))
            {
                divnoidung.Visible = true;
            }
            else
            {
                divnoidung.Visible = false;
            }

            if (!string.IsNullOrEmpty(Helper.ValidateParam("div_Area", string.Empty)))
            {
                div_Area.Visible = true;
                GetArea_terri = "true";
            }
            else
            {
                div_Area.Visible = false;
                GetArea_terri = "false";
            }

            string str_styleMota = Helper.ValidateParam("styleMota", string.Empty);
            if (str_styleMota=="1")
            {
                tbmlSubContent.Visible = false;
               // fckmlMetaDescription.Visible = true;
            }
            else
            {
                tbmlSubContent.Visible = true;
              //  fckmlMetaDescription.Visible = false;
                fckmlMetaDescription_CK.Visible = false;
            }
            if (str_styleMota == "2")
            {
                tbmlSubContent.Visible = false;
             //   fckmlMetaDescription.Visible = false;

                fckmlMetaDescription_CK.Visible = false;
            }
            

            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindType();
                txtDateMng.Text = DateTime.Now.ToString("MM-dd-yyyy");
                BindNewsInfo(NewsID);
                BindArea();
                //BindSourceList();
                BindRootCategory();
                SelectedNode();
                treeCategory.ExpandAll();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "startup_script", string.Format("selectedTabOnLoad();"), true);
            btnDelete.Visible = this.NewsID != -1;
        }

        protected string GetArea_terri
        {
            get;set;
        }

#region BindArea
        private void BindArea()
        {
            IList<TArea> list = blc_user.ListArea();
            list.Insert(0, new TArea() { TAreaID = -1, AreaName = "Chọn khu vực" });
            ddl_area.DataSource = list;
            ddl_area.DataTextField = "AreaName";
            ddl_area.DataValueField = "TAreaID";
            ddl_area.DataBind();

            if (this.NewsID != -1)
            {
                TMapTerritoryArea Amap = blc_user.GetMap_terri_area_ByID(this.NewsID, this.UK);
                if (Amap != null)
                {
                    try
                    {
                        ddl_area.SelectedValue = Amap.AreaID.ToString();
                    }
                    catch (System.Exception ex)
                    {
                        ddl_area.SelectedValue = "-1";
                    }
                }
            }

            int AreaID = Helper.TryParseInt(ddl_area.SelectedValue, -1);
            BindTerritory(AreaID);
        }
#endregion

        #region BindTerritory
        private void BindTerritory(int AreaID)
        {
            int areID = Helper.TryParseInt(AreaID.ToString(), -1);
            IList<TTerritory> list = blc_user.ListTerri_byArea(areID);
            list.Insert(0, new TTerritory() { TerritoryID = -1, TerritoryName = "Chọn quận/huyện" });
            ddl_territory.DataSource = list;
            ddl_territory.DataTextField = "TerritoryName";
            ddl_territory.DataValueField = "TerritoryID";
            ddl_territory.DataBind();

            if (this.NewsID != -1)
            {
                TMapTerritoryArea Amap = blc_user.GetMap_terri_area_ByID(this.NewsID, this.UK);
                if (Amap != null)
                {
                    try
                    {
                        ddl_territory.SelectedValue = Amap.TerritoryID.ToString();
                    }
                    catch (System.Exception ex)
                    {
                        ddl_territory.SelectedValue = "-1";
                    }
                }
            }
        }
        protected void ddl_area_SelectedIndexChanged(object sender, EventArgs e)
        {
            int AreaID = Helper.TryParseInt(ddl_area.SelectedValue, 0);
            BindTerritory(AreaID);
        }
        #endregion

#region  Bind Type
        private void BindType()
        {
            IList<TType> list = blc_user.ListType_byUK(this.UK);
            if (list.Count > 0)
            {
                cb_list.DataSource = list;
                cb_list.DataTextField = "TypeName";
                cb_list.DataValueField = "TypeID";
                cb_list.DataBind();
                listType.Visible = true;
            }
            else
            {
                listType.Visible = false;
            }
        }

        private void Creat_update_MapTypeNews(Int64 intNewsID)
        {
            string str = string.Empty;
            TMapType ent = null;
            foreach (ListItem item in cb_list.Items)
            {
                int intType = Helper.TryParseInt(item.Value, 0);
                ent = blc_user.GetMapType_ByTypeID_ObjID(intType, intNewsID);

                if (item.Selected == true)
                {
                    if (ent == null)
                    {
                        if (intType != 0)
                        {
                            blc_user.CreateMapType(intType, intNewsID);
                        }
                    }
                }
                else
                {
                    if (ent != null)
                    {
                        blc_user.DeleteMapType(ent.MapTypeID);
                    }
                }

            }
        }

        private void Delete_MapTypeNews(Int64 intNewsID)
        {
            string str = string.Empty;
            TMapType ent = null;
            foreach (ListItem item in cb_list.Items)
            {
                int intType = Helper.TryParseInt(item.Value, 0);
                ent = blc_user.GetMapType_ByTypeID_ObjID(intType, intNewsID);
                
                if (ent != null)
                {    
                    blc_user.DeleteMapType(ent.MapTypeID);
                }
                
            }
        }
#endregion
        #region Mains

        private void BindNewsInfo(Int64 p_NewsID)
        {
            TNew ent = blc_news.GetNew_ByID(Convert.ToInt64(p_NewsID));
            if (ent != null)
            {
                ddlNewsStatus.SelectedValue = ent.NewsStatus.ToString();
                txtAuthor.Text = ent.Author;
                txtDateMng.Text = string.Format("{0:MM-dd-yyyy}", ent.DateMng);
                lblRegUser.Text = ent.RegUser.ToString();
                lblRegDate.Text = string.Format("{0:MM-dd-yyyy}", ent.RegDate);
                lblModifyDate.Text = string.Format("{0:MM-dd-yyyy}", ent.ModifyDate);
                lblModifyUser.Text = ent.ModifyUser.ToString();
                lblIPAdd.Text = ent.IPAdd.ToString();
                lblCountView.Text = ent.CountView.ToString();
                lbl_like.Text = ent.NewsLike !=null ? ent.NewsLike.Value.ToString("N0") : "0";
                txtSortOrder.Text = ent.SortOrder !=null ? ent.SortOrder.ToString() : "100";
                //ddlNewsSource.SelectedValue = ent.NewsSourceID.ToString();

                imgProduct.ImageUrl = GetImagePath(Utility.TryParseLong(ent.DefaultPic,0));

                LinkImg.Text = GetImageName(Utility.TryParseLong(ent.DefaultPic,0));
                //LinkImg.NavigateUrl = GetImageUrl(ent.DefaultPic);

            }
            BindContent(p_NewsID);

            IList<TNewsToCategory> listToCatenews = blc_news.RowsNewsToCategoryByNewsID(this.NewsID);
            string ids = string.Empty;
            foreach (var itemlist in listToCatenews)
            {
                ids += itemlist.NewsCategoryID.ToString() + ",";
            }

            hdnCategoryIDs.Value = ids.TrimEnd(',');

            if (Helper.ValidateParam("mc", "") == "")
            {
                newcate.Visible = false;
                TNewsCategory ent_newcate = blc_news.GetNewCategoryByUniqueKey(Helper.ValidateParam("UK", ""));
                hdnCategoryIDs.Value = ent_newcate != null ? ent_newcate.NewsCategoryID.ToString() : "";
            }
            /**/
            TMapType ent_map = null;
            foreach (ListItem item in cb_list.Items)
            {
                int intType = Helper.TryParseInt(item.Value, 0);
                ent_map = blc_user.GetMapType_ByTypeID_ObjID(intType, this.NewsID);
                if (ent_map!=null)
                {
                    item.Selected = true;
                }
            }
            //BindNewsReleatedFirst();
            if (divmeta.Visible == true)
            {
                TSeo entSEO = blc_seo.GetTSeo_ByUniKeyMapID(this.NewsID, UniKeySeo());
                if (entSEO != null)
                {
                    DataTable dtSeo = blc_seo.ListSeoDescription(entSEO.SeoID);
                    if (dtSeo.Rows.Count != 0)
                    {
                        txt_title_tag.DataValue = Ultils.FormatForMultiLanguageControl(dtSeo, "SeoTitle");
                        txt_title_tag.Reload();

                        txt_key_tag.DataValue = Ultils.FormatForMultiLanguageControl(dtSeo, "SeoKeyWord");
                        txt_key_tag.Reload();

                        txt_des_tag.DataValue = Ultils.FormatForMultiLanguageControl(dtSeo, "SeoDescription");
                        txt_des_tag.Reload();
                    }
                }
            }
        }

        //private void BindNewsReleatedFirst()
        //{
        //    DataTable dt = nBLC.RowsNewsReleatedByNewsID(this.NewsID);
        //    rptReleatedNews.DataSource = dt;
        //    rptReleatedNews.DataBind();
        //}

        private void BindContent(Int64 P_newsID)
        {
            DataTable dt = blc_news.RowsNewsDescriptionByNewsID(P_newsID);

            if (dt.Rows.Count != 0)
            {
                tbmlTitle.DataValue = Ultils.FormatForMultiLanguageControl(dt, "Title");
                tbmlTitle.Reload();

                tbmlSubTitle.DataValue = Ultils.FormatForMultiLanguageControl(dt, "SubTitle");
                tbmlSubTitle.Reload();

             //   tbmlContent.DataSource = Ultils.FormatForMultiLanguageControl(dt, "Content");
             //   tbmlContent.DataBind();
                tbmlContent_CK.DataSource = Ultils.FormatForMultiLanguageControl(dt, "Content");
                tbmlContent_CK.DataBind();

                string str_styleMota = Helper.ValidateParam("styleMota", string.Empty);
                if (str_styleMota == "1")
                {
                   // fckmlMetaDescription.DataSource = Ultils.FormatForMultiLanguageControl(dt, "SubContent");
                  //  fckmlMetaDescription.DataBind();
                    fckmlMetaDescription_CK.DataSource = Ultils.FormatForMultiLanguageControl(dt, "SubContent");
                    fckmlMetaDescription_CK.DataBind();
                }
                else
                {
                    tbmlSubContent.DataValue = Ultils.FormatForMultiLanguageControl(dt, "SubContent");
                    tbmlSubContent.Reload();
                }

                

                tbmlComment.DataValue = Ultils.FormatForMultiLanguageControl(dt, "Comment");
                tbmlComment.Reload();

            }
        }

        //private void BindSourceList()
        //{
        //    DataTable dt = nBLC.RowsNewsSource();
        //    ddlNewsSource.DataSource = dt;
        //    ddlNewsSource.DataTextField = "NewsSourceName";
        //    ddlNewsSource.DataValueField = "NewsSourceID";
        //    ddlNewsSource.DataBind();
        //}

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
                return "/" + System.Configuration.ConfigurationManager.AppSettings["NewsImagePath"].Replace("\\", "/") + "/" + fileEnt.ServerFileName;
            //return "/" + Config.NewsImagePath.Replace("\\", "/") + "/" + fileEnt.ServerFileName;
            return string.Empty;
        }

        private bool AddNews()
        {

            FileManager fileMng = new FileManager();
            String filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["NewsImagePath"]);

            string[] arrImageSize = Config.GetConfigValue("NewsImageSize").Split(',');
            int swidth = Convert.ToInt32(arrImageSize[0]);
            int sheight = Convert.ToInt32(arrImageSize[1]);
            int mwidth = Convert.ToInt32(arrImageSize[2]);
            int mheight = Convert.ToInt32(arrImageSize[3]);
            int width = Convert.ToInt32(arrImageSize[4]);
            int height = Convert.ToInt32(arrImageSize[5]);

            CommonFileEntity fileEnt = fileMng.UploadImageFile(fileImage, filePath, swidth, sheight, mwidth, mheight, width, height, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));

            TNew ent = null;
            if (this.NewsID == -1)
            {
                ent = new TNew();
                ent.RegUser = this.UserID;
                ent.RegDate = DateTime.Now;
                ent.IPAdd = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                ent.DefaultPic = fileEnt != null ? fileEnt.CommonFileID : 0;
                ent.NewsLike = 0;
            }
            else
            {
                ent = blc_news.GetNew_ByID(this.NewsID);
                ent.ModifyUser = this.UserID;
                ent.ModifyDate = DateTime.Now;
                if (fileImage.HasFile == true)
                {
                    if (fileEnt != null)
                    {
                        fileMng.DeleteCommonFile(Utility.TryParseLong(ent.DefaultPic,0), true);
                        ent.DefaultPic = fileEnt.CommonFileID;
                    }
                }
            }
            ent.SortOrder = Helper.TryParseInt(txtSortOrder.Text, 100);
            ent.NewsID = this.NewsID;
            ent.NewsStatus = Convert.ToInt32(ddlNewsStatus.SelectedValue.ToString());
            ent.Author = txtAuthor.Text;
            //ent.DateMng = string.IsNullOrEmpty(txtDateMng.Text) ? DateTime.Now : Convert.ToDateTime(txtDateMng.Text);
            ent.DateMng = DateTime.Now;
            ent.NewsSourceID = 1;// Convert.ToInt32(ddlNewsSource.SelectedValue.ToString());

            if (this.NewsID == -1)
            {
                this.NewsID = Utility.TryParseLong(blc_news.CreateNews(ent),0);
                if (!string.IsNullOrEmpty(Helper.ValidateParam("div_Area", string.Empty)))
                {
                    blc_user.CreateTerri_area_Map(Helper.TryParseInt(ddl_area.SelectedValue, -1), Helper.TryParseInt(ddl_territory.SelectedValue, -1), this.NewsID, this.UK);
                }
            }
            else
            {
                blc_news.UpdateNews(ent);
                
                if (!string.IsNullOrEmpty(Helper.ValidateParam("div_Area", string.Empty)))
                {
                    TMapTerritoryArea Amap = blc_user.GetMap_terri_area_ByID(this.NewsID, this.UK);
                    if (Amap!=null)
                        blc_user.UpdateTerri_Area_Map(Helper.TryParseInt(ddl_area.SelectedValue, -1), Helper.TryParseInt(ddl_territory.SelectedValue, -1), this.NewsID, this.UK);
                    else
                        blc_user.CreateTerri_area_Map(Helper.TryParseInt(ddl_area.SelectedValue, -1), Helper.TryParseInt(ddl_territory.SelectedValue, -1), this.NewsID, this.UK);
                }
            }

            TSeo entSeo = new TSeo();
            long SeoIDCre = 0;
            if (divmeta.Visible == true)
            {
                entSeo.SeoType = 1;
                entSeo.MapID = this.NewsID;
                entSeo.Status = 1;
                entSeo.Uniquekey = UniKeySeo();
                entSeo.KeyOther = "";

                SeoIDCre = blc_seo.CreateSEO(entSeo);
            }

            for (int i = 0; i < tbmlTitle.DataText.Rows.Count; i++)
            {
                DataRow drTitle = tbmlTitle.DataText.Rows[i];
                DataRow drSubTitle = tbmlSubTitle.DataText.Rows[i];
               // DataRow drContent = tbmlContent.DataText.Rows[i];
                DataRow drContent = tbmlContent_CK.DataText.Rows[i];

                DataRow drSubContent = tbmlSubContent.DataText.Rows[i];
               // DataRow drSubContent2 = fckmlMetaDescription.DataText.Rows[i];
                DataRow drSubContent2 = fckmlMetaDescription_CK.DataText.Rows[i];

                DataRow drComment = tbmlComment.DataText.Rows[i];

                TNewsDescription ent1 = new TNewsDescription();

                ent1.NewsID = this.NewsID;
                ent1.LanguageID = Convert.ToInt32(drTitle["LangID"]);
                ent1.Title = drTitle["Text"].ToString();
                ent1.SubTitle = drSubTitle["Text"].ToString();
                ent1.Content = drContent["Text"].ToString();

                string str_styleMota = Helper.ValidateParam("styleMota", string.Empty);
                if (str_styleMota == "1")
                {
                    ent1.SubContent = drSubContent2["Text"].ToString();
                }
                else
                {
                    ent1.SubContent = drSubContent["Text"].ToString();
                }
                
                ent1.Comment = drComment["Text"].ToString();
                try
                {
                    blc_news.AddNewsDescription(ent1);
                }
                catch (System.Exception e)
                {
                    e.ToString();
                }
                if (divmeta.Visible == true)
                {
                    DataRow drTitleTag = txt_title_tag.DataText.Rows[i];
                    DataRow drKeyTag = txt_key_tag.DataText.Rows[i];
                    DataRow drDesTag = txt_des_tag.DataText.Rows[i];
                    
                    TSeoDescription entSeoDes = new TSeoDescription();
                    entSeoDes.SeoID = SeoIDCre;
                    entSeoDes.LanguageID = Convert.ToInt32(drTitle["LangID"]);
                    string strTitle = !string.IsNullOrEmpty(drTitleTag["Text"].ToString()) ? drTitleTag["Text"].ToString() : drTitle["Text"].ToString();
                    entSeoDes.SeoTitle = strTitle;
                    entSeoDes.SeoDescription = drDesTag["Text"].ToString();
                    entSeoDes.SeoKeyWord = drKeyTag["Text"].ToString();
                    blc_seo.CreateSEODescription(entSeoDes);
                }
            }

            UpdateNewsToCategory();


            return true;
            try
            {
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }

        }

        private void UpdateNewsToCategory()
        {
            if (treeCategory.CheckedNodes.Count > 0)
            {
                blc_news.DeleteNewsToCategory(-1, this.NewsID);

                TNewsToCategory ent = null;
                foreach (TreeNode node in treeCategory.CheckedNodes)
                {
                    ent = new TNewsToCategory();
                    ent.NewsCategoryID = Convert.ToInt32(node.Value);
                    ent.NewsID = this.NewsID;
                    blc_news.AddNewsToCategory(ent);
                }
            }
            else
            {
                blc_news.DeleteNewsToCategory(-1, this.NewsID);
                string uk = Helper.ValidateParam("uk", "");
                TNewsCategory entCat = blc_news.GetNewCategoryByUniqueKey(uk);
                if (entCat != null)
                {
                    TNewsToCategory ent = new TNewsToCategory();
                    ent.NewsCategoryID = Convert.ToInt32(entCat.NewsCategoryID);
                    ent.NewsID = this.NewsID;
                    blc_news.AddNewsToCategory(ent);
                }
                //tBLC.DeleteNewsToCategory(-1, this.NewsID);

                //NewsToCategoryEntity ent = null;
                //foreach (TreeNode node in treeCategory.CheckedNodes)
                //{
                //    ent = new NewsToCategoryEntity();
                //    ent.NewsCategoryID = Convert.ToInt32(node.Value);
                //    ent.NewsID = this.NewsID;
                //    tBLC.AddNewsToCategory(ent);
                //}
            }
        }

        private void BindRootCategory()
        {
            treeCategory.Nodes.Clear();

            int catID = 0;

            string uk = Helper.ValidateParam("uk", "");
            TNewsCategory entCat = blc_news.GetNewCategoryByUniqueKey(uk);

            if (entCat != null)
                catID = entCat.NewsCategoryID;

            DataTable dt = blc_news.dt_RowsNewsCategoryByParentID(catID, 1,-1,-1);

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
            DataTable dt = blc_news.dt_RowsNewsCategoryByParentID(parentID, 1,-1,-1);

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
            if (!string.IsNullOrEmpty(hdnCategoryIDs.Value))
            {
                foreach (TreeNode node in treeCategory.Nodes)
                {
                    SelectedChildNodes(node, "," + hdnCategoryIDs.Value + ",");
                }
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

        #region Utils

        private string GetCategoryName(int p_NewscategoryID)
        {

            TNewsCategoryDescription ent = blc_news.GetNewsCategoryDescription(p_NewscategoryID,1);
            return ent != null ? ent.CategoryName : string.Empty;
        }

        private string GetNewsName(long p_NewsID)
        {

            TNewsDescription ent = blc_news.GetNewsDescription(p_NewsID,1);
            return ent != null ? ent.Title : string.Empty;
        }

        protected string GetStatusName(object p_status)
        {
            if (p_status == DBNull.Value)
                return "";
            int i = Convert.ToInt32(p_status);
            string status;
            if (i == 1)
            {
                status = "Đã duyệt";
            }
            else
            {
                status = "Chưa duyệt";
            }
            return status;
        }

        protected string GetButtonName(object p_status)
        {
            if (p_status == DBNull.Value)
                return "";
            int i = Convert.ToInt32(p_status);
            string status;
            if (i == 1)
            {
                status = "Hủy duyệt";
            }
            else
            {
                status = "Duyệt";
            }
            return status;
        }

        protected string TextToHTML(object p_source)
        {
            return p_source.ToString().Replace("\r\n", "<br/>");
        }

        protected NewsControl ParentCtrl()
        {
            Control objParent = Parent;
            while (!(objParent is NewsControl))
            {
                objParent = objParent.Parent;
            }

            return objParent as NewsControl;
        }

        #endregion

        #region Property
        private string UniKeySeo()
        {
            return Config.GetConfigValue("SeoNewList");
        }

        protected Int64 NewsID
        {
            get
            {
                if (ParentCtrl().GetViewState("g_NewsID") != null)
                    return Convert.ToInt64(ParentCtrl().GetViewState("g_NewsID"));
                return -1;
            }
            set
            {
                ParentCtrl().SetViewState("g_NewsID", value);
            }
        }

        #endregion

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (AddNews())
            {
                if (this.NewsID!=-1)
                {
                    TNew ent = blc_news.GetNew_ByID(this.NewsID);
                    if (ent != null)
                    {
                        Creat_update_MapTypeNews(this.NewsID);
                    }
                }
                Alert.Show("Lưu thành công");
                //ParentCtrl().SetEvent(PageInfo("NewsList"));
                BindNewsInfo(NewsID);
            }
            else
                Alert.Show("Lưu thất bại");

        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            ParentCtrl().SetEvent(PageInfo("NewsList"));
        }

        protected void btnRefesh_Click(object sender, EventArgs e)
        {
            //SelectedNode();
            //BindNewsReleatedFirst();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            TNew ent = blc_news.GetNew_ByID(this.NewsID);
            if (ent != null)
            {
                blc_seo.DeleteSEO(ent.NewsID, UniKeySeo());
                Delete_MapTypeNews(ent.NewsID);
                FileManager fileMng = new FileManager();
                fileMng.DeleteCommonFile(Utility.TryParseLong(ent.DefaultPic,0), true);
                blc_news.DeleteNews(this.NewsID);
                Alert.Show("Deleted success!");
                ParentCtrl().SetEvent(PageInfo("NewsList"));
            }
        }

        protected void rptReleatedNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //long releatedID = Convert.ToInt32(e.CommandArgument);
            //bool result = tBLC.DeleteNewsReleated(this.NewsID, releatedID);

            //if (result)
            //{
            //    //BindNewsReleatedFirst();
            //}
            //else
            //    Alert.Show("Xóa thất bại");

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected string UK
        {
            get {
                return Helper.ValidateParam("UK", string.Empty);
            }
        }

        #endregion

    }
}