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
using UserMng.BLC;
using PQT.DAC;

namespace NewsMng
{
    public partial class CategoryInfo01 : XVNET_ModuleControl
    {
        NewsMng_BLC_TX tBLC = new NewsMng_BLC_TX();
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();
        Seo_BLC blc_seo = new Seo_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                tr_emailad.Visible = false;
                BindCategoryInfo();
                if (!string.IsNullOrEmpty(Helper.ValidateParam("divmeta", string.Empty)))
                {
                    divmeta.Visible = true;
                }
                else
                {
                    divmeta.Visible = false;
                }
                if (!string.IsNullOrEmpty(str_styleMota()))
                {
                    if (str_styleMota() == "2")
                    {
                        fckmlMetaDescription.Visible = true;
                        tbmlMetaDescription.Visible = false;
                    }
                    else
                    {
                        tbmlMetaDescription.Visible = true;
                        fckmlMetaDescription.Visible = false;
                    }
                }
                else
                {
                    tbmlMetaDescription.Visible = false;
                    fckmlMetaDescription.Visible = false;
                }

                if (!string.IsNullOrEmpty(str_styleUpload()))
                {
                    btn_upload.Visible = true;
                }
                else
                {
                    btn_upload.Visible = false;
                }

                if (!string.IsNullOrEmpty(str_styleIMG()))
                {
                    tr_img.Visible = true;
                }
                else
                {
                    tr_img.Visible = false;
                }

                if (!string.IsNullOrEmpty(Helper.ValidateParam("divnd", "")))
                {
                    fckmlDescription.Visible = false;
                }
                else
                {
                    fckmlDescription.Visible = true;
                }
                if (this.UK == "EmailAdmin")
                {
                    tr_emailad.Visible = true;
                    fckmlDescription.Visible = false;

                    tbmlMetaDescription.Visible = false;
                    fckmlMetaDescription.Visible = false;
                    divmeta.Visible = false;
                    tr_img.Visible = false;
                    tbmlName.Visible = false;
                    btn_upload.Visible = false;
                }

            }
        }

        protected void btnDeleteImg_Click(object sender, EventArgs e)
        {
            NewsCategoryEntity ent = nBLC.RowNewsCategory(this.NewsCategoryID);
            if (ent != null)
            {
                FileManager fileMng = new FileManager();
                if (ent.Image != null)
                {
                    fileMng.DeleteCommonFile(ent.Image, true);
                    Alert.Show("Xoá ảnh thành công");
                    BindCategoryInfo();
                }
            }
            Alert.Show("Vui lòng chọn lại danh mục!");
        }

        private string str_styleIMG()
        {
            if (!string.IsNullOrEmpty(Helper.ValidateParam("tr_img", string.Empty)))
            {
                return Helper.ValidateParam("tr_img", string.Empty);
            }
            return string.Empty;
        }

        private string str_styleMota()
        {
            if (!string.IsNullOrEmpty(Helper.ValidateParam("styleMota", string.Empty)))
            {
                return Helper.ValidateParam("styleMota", string.Empty);
            }
            return string.Empty;
        }

        private string str_styleUpload()
        {
            if (!string.IsNullOrEmpty(Helper.ValidateParam("styleUpload", string.Empty)))
            {
                return Helper.ValidateParam("styleUpload", string.Empty);
            }
            return string.Empty;
        }

        #region Main

        private void BindCategoryInfo()
        {

            NewsCategoryEntity ent = nBLC.RowNewsCategoryByUniqueKey(this.UK);
            if (ent != null)
            {
                this.NewsCategoryID = ent.NewsCategoryID;
                string isView = ent.IsView.ToString();

                rdoIsViewY.Checked = ent.IsView;
                rdoIsViewN.Checked = !rdoIsViewY.Checked;
                NewsCategoryDescriptionEntity entd = nBLC.RowNewsCategoryDescription(1, this.NewsCategoryID);
                emailad.Text = entd.Description;


                DataTable dt = nBLC.RowsNewsCategoryDescriptionByNewsCategoryID(this.NewsCategoryID);
                if (dt.Rows.Count != 0)
                {
                    tbmlName.DataValue = FormatForMultiLanguageControl(dt, "CategoryName");
                    tbmlName.Reload();

                    if (str_styleMota() == "2")
                    {
                        fckmlMetaDescription.DataSource = FormatForMultiLanguageControl(dt, "SubDescription");
                        fckmlMetaDescription.DataBind();
                    }
                    else
                    {
                        tbmlMetaDescription.DataValue = Ultils.FormatForMultiLanguageControl(dt, "SubDescription");
                        tbmlMetaDescription.Reload();
                    }

                    fckmlDescription.DataSource = FormatForMultiLanguageControl(dt, "Description");
                    fckmlDescription.DataBind();
                   
                }

                imgProduct.ImageUrl = GetImagePath(ent.Image);
                LinkImg.Text = GetImageName(ent.Image);
               
                if (divmeta.Visible == true)
                {
                    TSeo entSEO = blc_seo.GetTSeo_ByUniKeyMapID(ent.NewsCategoryID, UniKeySeo());
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



        }

        private bool AddNewsCategory()
        {
            try
            {
                NewsCategoryEntity ent = nBLC.RowNewsCategory(this.NewsCategoryID);
                if (ent != null)
                {
                    FileManager fileMng = new FileManager();
                    String filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["NewsCategoryImagePath"]);
                    CommonFileEntity fileEnt = fileMng.UploadImageFile(fileImage, filePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));

                    if (fileImage.HasFile == true)
                    {
                        if (fileEnt != null)
                        {
                            fileMng.DeleteCommonFile(ent.Image, true);
                            ent.Image = fileEnt.CommonFileID;
                        }
                    }
                    ent.Keyword = string.Empty;
                    ent.IsView = rdoIsViewY.Checked ? true : false;
                    tBLC.UpdateNewsCategory(ent);

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
                        DataRow drDescriptiom = fckmlDescription.DataText.Rows[i];

                        DataRow drMetaDescription2 = fckmlMetaDescription.DataText.Rows[i];
                        DataRow drMetaDescription = tbmlMetaDescription.DataText.Rows[i];

                        NewsCategoryDescriptionEntity ent1 = new NewsCategoryDescriptionEntity();
                        ent1.NewsCategoryID = this.NewsCategoryID;
                        ent1.LanguageID = Convert.ToInt32(drName["LangID"]);
                        ent1.CategoryName = drName["Text"].ToString();
                        
                        if (this.UK == "EmailAdmin")
                            ent1.Description = emailad.Text.Trim();
                        else ent1.Description = drDescriptiom["Text"].ToString();
                        

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
                }

                return true;

            }
            catch
            {
                return false;
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


        #endregion

        #region Events


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (AddNewsCategory())
            {
                BindCategoryInfo();

            }
            else
            {
                Alert.Show("Error");
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


        private string UK
        {
            get { return Helper.ValidateParam("UK", ""); }
        }


        #endregion

    }
}