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
    public partial class NewsStatistics : XVNET_ModuleControl
    {
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();
        NewsMng_BLC_TX tBLC = new NewsMng_BLC_TX();
        int pageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindCategotyList();
                BindGridNews();
            }
        }

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
            int category = Convert.ToInt32(ddlCategory.SelectedValue);
            int sortOption = Convert.ToInt32(ddlSearchType.SelectedValue);
            DataTable dt = nBLC.RowsNewsStatistic(this.CurrentPage, pageSize, 1, status, searchType, txtSearchText.Text, category);
            for (int i = dt.Rows.Count; i < pageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            gvUser.DataSource = dt;
            gvUser.DataBind();

            int recordCount = nBLC.CountNews(this.CurrentPage, pageSize, 1, status, searchType, txtSearchText.Text, sortOption, category);
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

        private void BindCategotyList()
        {
            DataTable dt = nBLC.RowsNewsCategoryByParentID(0, 1);
            DataRow dr = dt.NewRow();
            dr["Name"] = "All";
            dr["NewsCategoryID"] = -1;
            dt.Rows.InsertAt(dr, 0);

            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "NewsCategoryID";
            ddlCategory.DataBind();
        }

        #endregion

        #region Event
        protected void Pager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.CurrentPage = e.NewPageIndex + 1;
            BindGridNews();
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

        #endregion

    }
}