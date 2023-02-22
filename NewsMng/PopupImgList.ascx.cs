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
using PQT.API.DataDefine.Sys;

namespace NewsMng
{
    public partial class PopupImgList : XVNET_ModuleControl
    {
        UserMng_BLC blc_dac = new UserMng_BLC();
        int pageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindImgUpload();
                BindListImg();
            }
        }
        #region ImgUpload
        private void BindImgUpload()
        {
            int page = 5;
            IList<int> list = new List<int>() { };
            for (int i = 0; i < page; i++)
            {
                list.Add(i);
            }
            rpt_img.DataSource = list;
            rpt_img.DataBind();
        }

        private void BindListImg()
        { 
            IList<MapAll_ID> list = blc_dac.RowsMapList_ByMapProID(this.MapProID,this.Key);
            rpt_listImg.DataSource = list;
            rpt_listImg.DataBind();

        }

        private void CreateUpdateImg()
        {
            if (this.MapProID >= 0)
            {
                String filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[PathImg]);
                string[] arrImageSize = Config.GetConfigValue(ConfigSizeImg).Split(',');
                int swidth = Convert.ToInt32(arrImageSize[0]);
                int sheight = Convert.ToInt32(arrImageSize[1]);
                int mwidth = Convert.ToInt32(arrImageSize[2]);
                int mheight = Convert.ToInt32(arrImageSize[3]);
                int width = Convert.ToInt32(arrImageSize[4]);
                int height = Convert.ToInt32(arrImageSize[5]);
                
                foreach (RepeaterItem item in rpt_img.Items)
                {
                    FileUpload FileUpload1 = item.FindControl("FileUpload1") as FileUpload;
                    if (FileUpload1.HasFile)
                    {
                        MapAll_ID proHinhAnh = new MapAll_ID();
                        FileManager fileMng = new FileManager();
                        CommonFileEntity fileEnt = null;
                        fileEnt = fileMng.UploadImageFile(FileUpload1, filePath, swidth, sheight, mwidth, mheight, width, height, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));

                        proHinhAnh.MapID = fileEnt != null ? fileEnt.CommonFileID : 0;
                        proHinhAnh.MapProduct = this.MapProID;
                        proHinhAnh.thu_tu = 100;
                        proHinhAnh.KeyWord = this.Key;
                        blc_dac.CreateImgMap(proHinhAnh);
                    }
                }
            }
            BindListImg();
        }

        protected void rpt_listImg_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                long idDelete = Utils.TryParseLong(e.CommandArgument,0);
                blc_dac.DeleteRowMap(idDelete);
                BindListImg();
            }
        }
        
        protected string GetImagePathImg(object fileID, int index)
        {
            try
            {
                string str = this.GetImageUrl(Utils.TryParseLong(fileID, 0), this.PathImg, index == 3 ? ImageSizeType.Small : ImageSizeType.Big);
                if (System.IO.File.Exists(MapPath(str)))
                {
                    return str;
                }
                return "/Images/NoImage.jpg";
            }
            catch (System.Exception ex)
            {
                return "/Images/NoImage.jpg";
            }
        }

        #endregion

        #region event

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CreateUpdateImg();
        }

        #endregion

        #region
        protected long MapProID
        {
            get
            {
                return Helper.ValidateParam("ID", -1);
            }
        }

        protected string PathImg
        {
            get
            {
                return Helper.ValidateParam("PathImg", string.Empty);
            }
        }

        public string ConfigSizeImg
        {
            get
            {
                return Helper.ValidateParam("ConfigSizeImg", string.Empty);
            }
        }

        public string Key
        {
            get
            {
                return Helper.ValidateParam("Key", string.Empty);
            }
        }
        #endregion
    }
}