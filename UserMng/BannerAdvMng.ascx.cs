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

namespace UserMng
{
    public partial class BannerAdvMng : XVNET_ModuleControl
    {

        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindGridBanner();
                ResetBanner();
            }

        }

        #region MainMethods

        private void BindGridBanner()
        {
            UserMng_BLC_NTX blc = new UserMng_BLC_NTX();
            DataTable dt = blc.RowsBannerAdv();
            gvBanner.DataSource = dt;
            gvBanner.DataBind();
        }

        private void BindBannerInfo()
        {
            BannerAdvEntity ent = nBLC.RowBannerAdv(this.BannerID);
            if (ent != null)
            {
                lblBannerID.Text = ent.BannerID.ToString();
                rdoBannerActive.Checked = ent.IsActive;
                rdoBannerUnActive.Checked = !ent.IsActive;
                txtUrl.Text = ent.Url;
                txtSortOrder.Text = !string.IsNullOrEmpty(ent.SortOrder.ToString()) ? ent.SortOrder.ToString() : "0";
                ddlPosition.SelectedValue = ent.Position.ToString();
                imgBanner.ImageUrl = GetImagePath(ent.FileID);
                LinkImg.Text = GetImageName(ent.FileID);

                DataTable dt = nBLC.RowsBannerAdvDescriptionByBannerID(this.BannerID);
                if (dt.Rows.Count != 0)
                {
                    tbmlName.DataValue = Utility.FormatForMultiLanguageControl(dt, "Name");
                    tbmlName.Reload();
                }
            }
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

        private string GetImagePath(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);
            if (fileEnt != null)
                return string.Format("/{0}{1}", Config.GetConfigValue("BannerImagePath").Replace("\\", "/"), fileEnt.ServerFileName);
            return string.Empty;
        }

        protected string GetImagePath(object p_fileID)
        {
            if (p_fileID == DBNull.Value || p_fileID == null)
                return "";
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(Convert.ToInt64(p_fileID));
            if (fileEnt != null)
                return string.Format("/{0}{1}", Config.GetConfigValue("BannerImagePath").Replace("\\", "/"), fileEnt.ServerFileName);
            return string.Empty;
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

        private void ResetBanner()
        {
            this.BannerID = -1;
            txtUrl.Text = string.Empty;
            txtSortOrder.Text = "0";
            lblBannerID.Text = string.Empty;
            LinkImg.Text = String.Empty;
        }

        private void DeleteBanner()
        {
            BannerAdvEntity ent = nBLC.RowBannerAdv(this.BannerID);
            if (ent != null)
            {

                FileManager fileMng = new FileManager();
                fileMng.DeleteCommonFile(ent.FileID, true);
                tBLC.DeleteBannerAdv(this.BannerID);
                ResetBanner();
                BindGridBanner();
            }
        }

        #endregion

        #region Events

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                this.BannerID = Convert.ToInt32(e.CommandArgument);
                BindBannerInfo();
            }
            else if (e.CommandName == "DeleteItem")
            {
                try
                {
                    this.BannerID = Convert.ToInt32(e.CommandArgument);
                    DeleteBanner();
                }
                catch
                {
                    Alert.Show("Xóa lỗi!");
                }
            }

        }

        protected void btnInsertBanner_Click(object sender, EventArgs e)
        {
            ResetBanner();
        }

        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            CommonFileEntity fileEnt = null;
            FileManager fileMng = new FileManager();
            try
            {
                String filePath = Server.MapPath(Config.GetConfigValue("BannerImagePath"));
                fileEnt = fileMng.UploadImageFile(fileImage, filePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));

                BannerAdvEntity ent = null;
                if (this.BannerID == -1)
                {
                    ent = new BannerAdvEntity();
                    ent.FileID = fileEnt != null ? fileEnt.CommonFileID : 0;

                }
                else
                {
                    ent = nBLC.RowBannerAdv(this.BannerID);
                    if (fileImage.HasFile == true)
                    {
                        if (fileEnt != null)
                        {
                            fileMng.DeleteCommonFile(ent.FileID, true);
                            ent.FileID = fileEnt.CommonFileID;
                        }
                    }
                }

                ent.Position = Convert.ToInt32(ddlPosition.SelectedValue);
                ent.Url = txtUrl.Text;
                ent.SortOrder = Helper.TryParseInt(txtSortOrder.Text,100);
                ent.IsActive = rdoBannerActive.Checked;
                ent.BannerID = this.BannerID;


                if (this.BannerID == -1)
                {
                    this.BannerID = tBLC.CreateBannerAdv(ent);
                }
                else
                {
                    tBLC.UpdateBannerAdv(ent);
                }

                for (int i = 0; i < tbmlName.DataText.Rows.Count; i++)
                {
                    DataRow drName = tbmlName.DataText.Rows[i];

                    BannerAdvDescriptionEntity ent1 = new BannerAdvDescriptionEntity();

                    ent1.BannerID = this.BannerID;
                    ent1.LanguageID = Convert.ToInt32(drName["LangID"]);
                    ent1.Name = drName["Text"].ToString();

                    tBLC.AddBannerAdvDescription(ent1);



                }

                BindGridBanner();
                ResetBanner();

            }
            catch
            {
                if (fileEnt != null)
                    fileMng.DeleteCommonFile(fileEnt.CommonFileID, true);
                Alert.Show("Error");
            }

        }

        protected void btnDeleteBanner_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteBanner();
            }
            catch
            {
                Alert.Show("Xóa lỗi!");
            }
        }



        #endregion

        #region Property

        protected int BannerID
        {
            get
            {
                if (ViewState["g_BannerID"] != null)
                    return Convert.ToInt32(ViewState["g_BannerID"]);
                return -1;
            }
            set
            {
                ViewState["g_BannerID"] = value;
            }
        }

        #endregion

    }
}