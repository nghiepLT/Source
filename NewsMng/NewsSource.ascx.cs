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
    public partial class NewsSource : XVNET_ModuleControl
    {
        NewsMng_BLC_TX tBLC = new NewsMng_BLC_TX();
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindGridNewsSource();
                BindLanguageList();
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

        #region MainMethods

        private void BindGridNewsSource()
        {
            DataTable dt = nBLC.RowsNewsSource();
            gvUser.DataSource = dt;
            gvUser.DataBind();
        }

        private void BindStockStatusINfo(int p_newsSourceID, int p_langID)
        {
            ddlLanguage.Enabled = false;
            NewsSourceEntity ent = nBLC.RowNewsSource(p_langID, p_newsSourceID);
            if (ent!=null)
            {
                lblNewsSourceID.Text = ent.NewsSourceID.ToString();
                txtSourceName.Text = ent.NewsSourceName;
                ddlLanguage.SelectedValue = ent.LanguageID.ToString();
            }
        }

        private void BindLanguageList()
        {
            DataTable dt = nBLC.RowsLanguage();
            ddlLanguage.DataSource = dt;
            ddlLanguage.DataTextField = "Name";
            ddlLanguage.DataValueField = "LanguageID";
            ddlLanguage.DataBind();
        }

        private bool AddNewsSource()
        {
            try
            {

                NewsSourceEntity ent = null;
                int newsSourceID;
                if (string.IsNullOrEmpty(lblNewsSourceID.Text))
                {
                    ent = new NewsSourceEntity();
                }
                else
                {
                    newsSourceID = Convert.ToInt32(lblNewsSourceID.Text);
                    ent = nBLC.RowNewsSource(Convert.ToInt32(ddlLanguage.SelectedValue.ToString()) ,newsSourceID);
                }
                ent.NewsSourceName = txtSourceName.Text;
                ent.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue.ToString());


                if (string.IsNullOrEmpty(lblNewsSourceID.Text))
                {
                   tBLC.CreateNewsSource(ent);
                }
                else
                {
                    tBLC.UpdateNewsSource(ent);
                }


                BindGridNewsSource();

            return true;

            }
            catch(Exception ex)
            {
                ex.ToString();
                return false;
            }

        }

        private void DelNewsSource(int p_NewsSourceID)
        {
            try
            {
                DataTable dt = nBLC.RowsNewsByNewsSourceID(p_NewsSourceID);
                if (dt.Rows.Count > 0)
                {
                    Alert.Show("This source have news(s) , can not delete.");
                }
                else
                {
                    tBLC.DeleteNewsSource(Convert.ToInt32(ddlLanguage.SelectedValue.ToString()),p_NewsSourceID);
                    Alert.Show("Xóa thành công!");
                    BindGridNewsSource();

                }

            }
            catch (Exception ex)
            {
                Alert.Show(ex.ToString());
            }
        }

        private void ResetAll()
        {
            this.lblNewsSourceID.Text = string.Empty;
            this.txtSourceName.Text = string.Empty;
            ddlLanguage.Enabled = true;
        }

        #endregion

        #region Events
        //protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    
        //}

        

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    DelManufacture(Convert.ToInt32(lblManufacture.Text));
        //}

        protected void btnsave_click(object sender, EventArgs e)
        {
            AddNewsSource();
            ResetAll();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arr = e.CommandArgument.ToString().Split(',');
            int newsSourceID = Convert.ToInt32(arr[0]);
            int langID = Convert.ToInt32(arr[1]);
            BindStockStatusINfo(newsSourceID, langID);

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DelNewsSource(Convert.ToInt32(this.lblNewsSourceID.Text));

            ResetAll();
            BindGridNewsSource();
        }

        #endregion

        
    }
}