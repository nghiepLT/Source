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

namespace NewsMng
{
    public partial class PopupNewsReleated : XVNET_ModuleControl
    {
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();
        NewsMng_BLC_TX tBLC = new NewsMng_BLC_TX();
        int pageSize = 16;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategotyList();
                BindGridNews();
                BindGridNewsReleated();
            }
        }


        #region Main

        private void BindGridNews()
        {
            int status = Convert.ToInt32(ddlStatus.SelectedValue);
            int searchType = Convert.ToInt32(ddlSearchType.SelectedValue);
            int sortOption = Convert.ToInt32(ddlSearchType.SelectedValue);
            DataTable dt = nBLC.RowsNewsByListCategory(this.CurrentPage, pageSize, 1, status, txtSearchText.Text, this.CategoryIDs);
            for (int i = dt.Rows.Count; i < pageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            gvUser.DataSource = dt;
            gvUser.DataBind();

            int recordCount = nBLC.CountNewsByListCategory(1, status, txtSearchText.Text, this.CategoryIDs);
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

        private void BindGridNewsReleated()
        {
            DataTable dt = nBLC.RowsNewsReleatedByNewsID(this.NewsID);
            for (int i = dt.Rows.Count; i <= pageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            gvReleated.DataSource = dt;
            gvReleated.DataBind();
        }

        protected string GetStatusName(object p_status)
        {
            if (p_status == DBNull.Value)
                return "";
            int i = Convert.ToInt32(p_status);
            string status;
            if (i == 1)
            {
                status = "True";
            }
            else
            {
                status = "Fasle";
            }
            return status;
        }

        private void BindCategotyList()
        {

            NewsCategoryEntity ent = nBLC.RowNewsCategoryByUniqueKey(this.KeyWord);
            if (ent != null)
            {
                DataTable dt = nBLC.RowsNewsCategoryByParentID(ent.NewsCategoryID, 1);
                ddlCategory.Items.Clear();

                ListItem item = null;
                string ids = string.Empty;
                ids += ent.NewsCategoryID.ToString() + ",";
                foreach (DataRow dr in dt.Rows)
                {
                    item = new ListItem();
                    item.Text = dr["Name"].ToString();
                    item.Value = dr["NewsCategoryID"].ToString();
                    ddlCategory.Items.Add(item);
                    ids += dr["NewsCategoryID"].ToString() + ",";
                }
                ids = ids.TrimEnd(',');
                this.CategoryIDs = ids;
                item = new ListItem();
                item.Text = "ALL";
                item.Value = this.CategoryIDs;
                ddlCategory.Items.Insert(0, item);
            }
        }

        private void UpdateNewsReleated()
        {
            NewsReleatedEntity ent = null;
            string[] arrIDNews = hdnIDs.Value.TrimEnd(',').Split(',');
            foreach (string id in arrIDNews)
            {
                ent = new NewsReleatedEntity();
                ent.ReleatedID = Convert.ToInt32(id);
                ent.NewsID = this.NewsID;
                ent.SortID = 0;
                tBLC.AddNewsReleated(ent);
            }
        }

        #endregion

        #region Event

        protected void Pager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.CurrentPage = e.NewPageIndex + 1;
            BindGridNews();
        }

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvReleated_RowCommand(object source, GridViewCommandEventArgs e)
        {
            long releatedID = Convert.ToInt32(e.CommandArgument);
            bool result = tBLC.DeleteNewsReleated(this.NewsID, releatedID);

            if (result)
            {
                BindGridNewsReleated();
            }
            else
                Alert.Show("Xóa thất bại");

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridNews();
        }

        protected void btnPageMove_Click(object sender, EventArgs e)
        {
            CurrentPage = Convert.ToInt32(txtCurrentPage.Text);
            BindGridNews();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            UpdateNewsReleated();
            BindGridNewsReleated();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(btnSelect, btnSelect.GetType(), "ReloadNewsReleated", string.Format("window.close(); window.opener.ReloadNewsReleated()"), true);
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

        #region Utils

        protected bool IsVisible(object p_id)
        {
            return p_id.ToString().Length>0;

        }

        protected string IsView(object p_id)
        {
            return p_id.ToString().Length > 0 ? "inline" : "none";

        }


        #endregion

        #region Property

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

        private string KeyWord
        {
            get
            {
                return Helper.ValidateParam("UK", "News");
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

        private long NewsID
        {
            get
            {
                return Helper.ValidateParam("id", 0);
            }
        }

        #endregion



    }
}