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
using PQT.API;
using UserMng.BLC;
using System.Collections.Generic;
using PQT.DAC;
using PQT.Common;
using System.IO;


namespace UserMng
{
    public partial class MnSeoList : XVNET_ModuleControl
    {
        Seo_BLC blc_seo = new Seo_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                BindGird();
            }
            if (!MRI.IsDataAdmin)
            {
                titlePage.Visible = true;
                btnInsertBanner.Visible = true;
                btnSaveBanner.Visible = true;
                btnDeleteBanner.Visible = true;
                keyword.Visible = true;
                gvBanner.Columns[1].Visible = true;
            }
        }

#region bind

        private void BindGird()
        {
            int inttype = 2;
            if (!MRI.IsDataAdmin)
            {
                inttype = -1;
            }
            DataTable dt = blc_seo.RowsListSeoMnSeo(-1,1,inttype,"","");
            gvBanner.DataSource = dt;
            gvBanner.DataBind();
        }

#endregion

        #region Events

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                this.LongSeoID = Utility.TryParseLong(e.CommandArgument,-1);
                TSeo entSEO = blc_seo.GetTSeo_ByID(this.LongSeoID);
                if (entSEO != null)
                {
                    txtKetWord.Text = entSEO.KeyOther;
                    lbl_mota.Text = entSEO.Mota;
                    txt_MoTa.Text = entSEO.Mota;
                    DataTable dtSeo = blc_seo.ListSeoDescription(entSEO.SeoID);
                    if (dtSeo.Rows.Count != 0)
                    {
                        txt_title_tag.DataValue = Utility.FormatForMultiLanguageControl(dtSeo, "SeoTitle");
                        txt_title_tag.Reload();

                        txt_key_tag.DataValue = Utility.FormatForMultiLanguageControl(dtSeo, "SeoKeyWord");
                        txt_key_tag.Reload();

                        txt_des_tag.DataValue = Utility.FormatForMultiLanguageControl(dtSeo, "SeoDescription");
                        txt_des_tag.Reload();
                    }
                }
            }

        }

        protected void btnInsertBanner_Click(object sender, EventArgs e)
        {
            resetfield();
        }

        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            if (this.LongSeoID == -1)
            {
                TSeo entSeo = new TSeo();
                long SeoIDCre = 0;

                entSeo.SeoType = 2;
                entSeo.MapID = 0;
                entSeo.Status = 1;
                entSeo.Uniquekey = "";
                entSeo.KeyOther = txtKetWord.Text.Trim();
                entSeo.Mota = txt_MoTa.Text.Trim();
                SeoIDCre = blc_seo.CreateSEOList(entSeo);
                for (int i = 0; i < txt_title_tag.DataText.Rows.Count; i++)
                {
                    DataRow drTitleTag = txt_title_tag.DataText.Rows[i];
                    DataRow drKeyTag = txt_key_tag.DataText.Rows[i];
                    DataRow drDesTag = txt_des_tag.DataText.Rows[i];
                    TSeoDescription entSeoDes = new TSeoDescription();
                    entSeoDes.SeoID = SeoIDCre;
                    entSeoDes.LanguageID = Convert.ToInt32(drTitleTag["LangID"]);
                    entSeoDes.SeoTitle = drTitleTag["Text"].ToString();
                    entSeoDes.SeoDescription = drDesTag["Text"].ToString();
                    entSeoDes.SeoKeyWord = drKeyTag["Text"].ToString();
                    blc_seo.CreateSEODescription(entSeoDes);
                }
                
            }
            else
            {
                Alert.Show("Erro Create new seo!");
            }
            resetfield();
            BindGird();
        }

        protected void btnUpdateBanner_Click(object sender, EventArgs e)
        {
            if (this.LongSeoID==-1)
            {
                Alert.Show("Vui lòng chọn Item cần chỉnh sửa");
            }
            else
            {
                blc_seo.UpdateSEOList(this.LongSeoID,txtKetWord.Text.Trim(),txt_MoTa.Text.Trim());
                for (int i = 0; i < txt_title_tag.DataText.Rows.Count; i++)
                {
                    DataRow drTitleTag = txt_title_tag.DataText.Rows[i];
                    DataRow drKeyTag = txt_key_tag.DataText.Rows[i];
                    DataRow drDesTag = txt_des_tag.DataText.Rows[i];
                    TSeoDescription entSeoDes = new TSeoDescription();
                    entSeoDes.SeoID = this.LongSeoID;
                    entSeoDes.LanguageID = Convert.ToInt32(drTitleTag["LangID"]);
                    entSeoDes.SeoTitle = drTitleTag["Text"].ToString();
                    entSeoDes.SeoDescription = drDesTag["Text"].ToString();
                    entSeoDes.SeoKeyWord = drKeyTag["Text"].ToString();
                    blc_seo.CreateSEODescription(entSeoDes);
                }
                Alert.Show("Thành công!");
                BindGird();
            }
            resetfield();
        }

        protected void btnDeleteBanner_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.LongSeoID!=-1)
                {
                    blc_seo.DeleteSEOList(this.LongSeoID);
                    Alert.Show("Thành công!");
                    resetfield();
                    BindGird();
                }
                else
                {
                    Alert.Show("Chưa chọn Item cần xóa!");
                }
                
            }
            catch
            {
                Alert.Show("Xóa lỗi!");
            }
        }



        #endregion

        private DataTable InitTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("LangID", typeof(string));

            return dt;
        }

        private void resetfield()
        {
            this.LongSeoID = -1;
            txt_title_tag.DataValue = InitTable();
            txt_title_tag.Reload();

            txt_key_tag.DataValue = InitTable();
            txt_key_tag.Reload();

            txt_des_tag.DataValue = InitTable();
            txt_des_tag.Reload();

            txt_MoTa.Text = "";
            lbl_mota.Text = "";
            txtKetWord.Text = "";
        }

        #region Property

        private long LongSeoID
        {
            get
            {
                if (ViewState["g_LongSeoID"] != null)
                    return Convert.ToInt64(ViewState["g_LongSeoID"]);
                return -1;
            }
            set
            {
                ViewState["g_LongSeoID"] = value;
            }
        }
        #endregion

    }
}