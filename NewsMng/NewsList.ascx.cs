using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


using PQT.API;
using PQT.Common;
using PQT.Controls;
using NewsMng.BLC;
using NewsMng.DataDefine;
using PQT.API.File;
using PQT.DAC;
using UserMng.BLC;
using UserMng;

namespace NewsMng
{
    public partial class NewsList : XVNET_ModuleControl
    {
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();
        NewsMng_BLC_TX tBLC = new NewsMng_BLC_TX();
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        News_BLC blc_new = new News_BLC();
        Seo_BLC blc_seo = new Seo_BLC();
        int pageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindCategotyList();
                BindGridNews();
            }
            checkComment();
            checkLike();
        }

#region 
        protected void checkLike()
        {
            if (!string.IsNullOrEmpty(Helper.ValidateParam("islike", string.Empty)))
            {
                gvUser.Columns[7].Visible = true;
            }
            else
                gvUser.Columns[7].Visible = false;
        }
        protected void checkComment()
        {
            if (!string.IsNullOrEmpty(Helper.ValidateParam("iscomment", string.Empty)))
            {
                gvUser.Columns[8].Visible = true;
            }
            else
                gvUser.Columns[8].Visible = false;
        }

        protected string GetAreaName(object p_value)
        {
            if (p_value!=null)
            {
                Int64 longValue = Utility.TryParseLong(p_value, 0);
                TAreaMap AreMap = blc_user.GetAreaMap_ByID(longValue, this.KeyWord);
                if (AreMap!=null)
                {
                    TArea AreaName = blc_user.GetArea_ByID(Helper.TryParseInt(AreMap.TAreaID.ToString(),0),1);
                    if (AreaName!=null)
                    {

                    }
                }
            }
            return string.Empty;
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

        #endregion

        #region Main

        private void BindGridNews()
        {
            int status = Convert.ToInt32(ddlStatus.SelectedValue);
            int searchType = Convert.ToInt32(ddlSearchType.SelectedValue);
            int sortOption = Convert.ToInt32(ddlSearchType.SelectedValue);
            //DataTable dt = nBLC.RowsNewsByListCategory(this.CurrentPage, pageSize, 1, status, txtSearchText.Text, this.CategoryIDs);
            //for (int i = dt.Rows.Count; i < pageSize; i++)
            //{
            //    dt.Rows.Add(dt.NewRow());
            //}
            DataTable dt = blc_new.RowsNewsByListCategory_byType_like(this.CurrentPage, pageSize, 1, status, txtSearchText.Text, this.CategoryIDs,-1,false,-1,"",-1,-1);
            gvUser.DataSource = dt;
            gvUser.DataBind();

            //int recordCount = nBLC.CountNewsByListCategory(1, status, txtSearchText.Text, this.CategoryIDs);
            int recordCount = blc_new.Count_NewsByListCategory_byType_like(1, status, txtSearchText.Text, this.CategoryIDs, -1, false, -1, "",-1,-1);
            PQTPager1.RecordCount = recordCount;

            if (recordCount > 0)
            {
                TotalPage = (recordCount - 1) / pageSize + 1;
                PQTPager1.PageSize = pageSize;
                PQTPager1.PagerButtonCount = 5;
                PQTPager1.CurrentPageIndex = this.CurrentPage - 1;
                txtCurrentPage.Text = this.CurrentPage.ToString();
            }
            else
            {
                txtCurrentPage.Text = string.Empty;
                TotalPage = 1;
            }
            lblTotalPage.Text = TotalPage.ToString();

        }

        protected string GetStatusName(object p_status)
        {
            if (p_status == DBNull.Value)
                return "";
            int i = Convert.ToInt32(p_status);
            string status;
            if (i == 1)
            {
                status = "Hiện";
            }
            else
            {
                status = "Ẩn";
            }
            return status;
        }

        private void BindCategotyList()
        {
            //int categoryID = -1;
            //if (this.CatID != 0)
            //    categoryID = this.CatID;
            //else
            //{
            //    NewsCategoryEntity ent = nBLC.RowNewsCategoryByUniqueKey(this.KeyWord);
            //    categoryID = ent != null ? ent.NewsCategoryID : -1;
            //}

            //DataTable dt = nBLC.RowsNewsCategoryByParentID(categoryID, 1);
            //ddlCategory.Items.Clear();

            //ListItem item = null;
            //item = new ListItem();
            //item.Text = "ALL";
            //item.Value = this.CategoryIDs;
            //ddlCategory.Items.Insert(0, item);

            //string ids = string.Empty;
            //ids += categoryID.ToString() + ",";
            //foreach (DataRow dr in dt.Rows)
            //{
            //    ddlCategory.Items.Add(new ListItem(dr["Name"].ToString(), dr["NewsCategoryID"].ToString()));
            //    DataTable dtc = nBLC.RowsNewsCategoryByParentID(Convert.ToInt32(dr["NewsCategoryID"]), 1);
            //    if (dtc.Rows.Count > 0)
            //    {
            //        foreach (DataRow drc in dtc.Rows)
            //        {
            //            ddlCategory.Items.Add(new ListItem(dr["Name"].ToString() + ">>" + drc["Name"].ToString(), dr["NewsCategoryID"].ToString() + ">>" + drc["NewsCategoryID"].ToString()));
            //            ids += drc["NewsCategoryID"].ToString() + ",";
            //        }
            //    }

            //    ids += dr["NewsCategoryID"].ToString() + ",";
            //}
            //ids = ids.TrimEnd(',');
            //this.CategoryIDs = ids;

            //son 02_26_2012

            int categoryID = -1;
            if (this.CatID != 0)
                categoryID = this.CatID;
            else
            {
                NewsCategoryEntity ent = nBLC.RowNewsCategoryByUniqueKey(this.KeyWord);
                categoryID = ent != null ? ent.NewsCategoryID : -1;
            }

            DataTable dt = nBLC.RowsNewsCategoryByParentID(categoryID, 1);
            ddlCategory.Items.Clear();

            ListItem item = null;
            item = new ListItem();
            //item.Text = "ALL";
            //item.Value = this.CategoryIDs;
            //ddlCategory.Items.Insert(0, item);

            string ids = string.Empty;
            //ids += categoryID.ToString() + ",";
            ids = categoryID.ToString();
            foreach (DataRow dr in dt.Rows)
            {
                item = new ListItem();
                item.Text = dr["Name"].ToString();
                item.Value = dr["NewsCategoryID"].ToString();
                ddlCategory.Items.Add(item);

                BindCategoryByParentID(item);

                //ids += dr["NewsCategoryID"].ToString() + ",";
                this.CategoryIDs += dr["NewsCategoryID"].ToString() + ",";
                
            }
            ids = ids + "," + this.CategoryIDs;
            this.CategoryIDs = ids.Trim(',');
            ddlCategory.Items.Insert(0, new ListItem("All", this.CategoryIDs));
        }
#region child news son

        private void BindCategoryByParentID(ListItem p_item)
        {
            int parentID = Convert.ToInt32(p_item.Value);
            DataTable dt = nBLC.RowsNewsCategoryByParentID(parentID, this.LangID);

            string ids = string.Empty;
            ids += parentID.ToString() + ",";

            ListItem item = null;
            foreach (DataRow dr in dt.Rows)
            {
                item = new ListItem();
                item.Text = p_item.Text + " >> " + dr["Name"].ToString();
                item.Value = dr["NewsCategoryID"].ToString();
                ddlCategory.Items.Add(item);
                //ids += dr["NewsCategoryID"].ToString() + ",";
                this.CategoryIDs += dr["NewsCategoryID"].ToString() + ",";
                BindCategoryByParentID(item);
            }
        }
#endregion
        private bool DeleteNews(int p_ID)
        {
            try
            {
                NewsEntity ent = nBLC.RowNews(p_ID);
                FileManager fileMng = new FileManager();
                if (ent != null)
                {
                    blc_seo.DeleteSEO(ent.NewsID, UniKeySeo());
                    tBLC.DeleteNews(p_ID);

                }

                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        private bool ActiveNews(int p_ID)
        {
            try
            {
                NewsEntity entNews = nBLC.RowNews(p_ID);
                if (entNews == null) return false;
                entNews.NewsStatus = 1;
                return tBLC.UpdateNews(entNews);
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        #endregion

        #region Event

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] arrIDs = hdnIDs.Value.TrimEnd('|').Split('|');
                foreach (string id in arrIDs)
                {
                    int idP = Convert.ToInt32(id);
                    DeleteNews(idP);
                }
                Alert.Show("Xóa thành công");
                BindGridNews();
            }
            catch (System.Exception ex)
            {
                Alert.Show("Xóa thất bại");

            }

        }

        protected void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                string[] arrIDs = hdnIDs.Value.TrimEnd('|').Split('|');
                foreach (string id in arrIDs)
                {
                    int idP = Convert.ToInt32(id);
                    ActiveNews(idP);
                }
                Alert.Show("Cập nhật thành công");
                BindGridNews();
            }
            catch (System.Exception ex)
            {
                Alert.Show("Xóa thất bại");

            }

        }

        protected void Pager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.CurrentPage = e.NewPageIndex + 1;
            BindGridNews();
        }

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ParentCtrl().SetViewState("g_NewsID", e.CommandArgument);
            ParentCtrl().SetEvent(PageInfo("NewsEntry"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.CurrentPage = 1;
            this.CategoryIDs = ddlCategory.SelectedValue;
            BindGridNews();
        }

        protected void btnPageMove_Click(object sender, EventArgs e)
        {
            CurrentPage = Convert.ToInt32(txtCurrentPage.Text);
            BindGridNews();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ParentCtrl().SetViewState("g_NewsID", -1);
            ParentCtrl().SetEvent(PageInfo("NewsEntry"));
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CategoryIDs = ddlCategory.SelectedValue;
            BindGridNews();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridNews();
        }

        #endregion

        #region Property
        private string UniKeySeo()
        {
            return Config.GetConfigValue("SeoNewList");
        }
        private int CurrentPage
        {
            get
            {
                if (ViewState["g_CurrentPage"] != null)
                    return Convert.ToInt32(ViewState["g_CurrentPage"]);
                return 1;
            }
            set
            {
                ViewState["g_CurrentPage"] = value;
            }
        }

        public int TotalPage
        {
            get
            {
                if (ViewState["g_TotalPage"] != null)
                    return Convert.ToInt32(ViewState["g_TotalPage"]);
                return 0;
            }
            set
            {
                ViewState["g_TotalPage"] = value;
            }
        }

        protected string KeyWord
        {
            get
            {
                return Helper.ValidateParam("UK", "News");
            }
        }

        private int CatID
        {
            get
            {
                return Helper.ValidateParam("id", 0);
            }
        }

        private int IsStandAlone
        {
            get
            {
                return Helper.ValidateParam("IS", 0);
            }
        }



        private string CategoryIDs
        {
            get
            {
                if (ViewState["g_CategoryIDs"] != null)
                    return Convert.ToString(ViewState["g_CategoryIDs"]);
                return string.Empty;
            }
            set
            {
                ViewState["g_CategoryIDs"] = value;
            }
        }

        #endregion

        protected string BindCateName(object p_value)
        {
            if (p_value!=null)
            {
                int intVlaue = Helper.TryParseInt(p_value.ToString(), 0);
                NewsCategoryDescriptionEntity ent = nBLC.RowNewsCategoryDescription(this.LangID, intVlaue);
                return ent != null ? ent.CategoryName : string.Empty;
            }
            return string.Empty;
        }

        protected string MemberName(object p_MemberReg)
        {
            if (p_MemberReg != null)
            {
                int p_value = Helper.TryParseInt(p_MemberReg.ToString(), 0);

                TUser ent = blc_user.GetUser_ByIDAll(p_value);
                if (ent != null)
                {
                    return ent.LoginID != null ? ent.LoginID.ToString() : string.Empty;
                }
                return string.Empty;
            }
            return string.Empty;
        }

        protected string SoBinhluan(object p_value)
        {
            if (p_value != null)
            {
                long long_value = Utility.TryParseLong(p_value, 0);
                long ent = blc_user.CountComment_NewsID(long_value);
                return ent.ToString("N0");
            }

            return string.Empty;
        }
    }
}