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
    public partial class FileUploadMng : XVNET_ModuleControl
    {

        FileUpload_BLC blc_file = new FileUpload_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            //{
            txtKeyWord.Text = this.UK;
            if (!MRI.IsAdmin)
            {
                tr_key.Visible = true;
            }
            if (!IsPostBack)
            {
               
                BindGird();
            }
                
                
            //}
        }

        #region Utils

        //protected UserControl ParentCtrl()
        //{
        //    Control objParent = Parent;
        //    while (!(objParent is UserControl))
        //    {
        //        objParent = objParent.Parent;
        //    }

        //    return objParent as UserControl;
        //}

        #endregion

#region bind

        private void BindGird()
        {
            IList<TFileUpload> list = blc_file.ListFile_byKey(this.UK);
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }

#endregion

#region uploadFile
        private long CreateFileUpload(string strTitle,FileUpload fileupload)
        {
            try
            {
                string pathfile = Server.MapPath(Config.GetConfigValue("PathFileUpload"));
                if (!Directory.Exists(pathfile))
                {
                    Directory.CreateDirectory(pathfile);
                }
                if (fileupload.HasFile)
                {
                    //txtUrl01.Text = Path.GetExtension(fileUrl01.PostedFile.FileName);
                    //txtUrl02.Text = fileUrl01.FileName;

                    string extension = Path.GetExtension(fileupload.PostedFile.FileName);
                    string ConvertName = string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
                    ConvertName = ConvertName + extension;
                    fileupload.SaveAs(pathfile + ConvertName);

                    long intFile = blc_file.CreateFile(strTitle, fileupload.FileName, ConvertName, pathfile, txtKeyWord.Text.Trim(), DateTime.Now);

                    return intFile;
                }
                return 0;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
#endregion

        #region Events

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                try
                {
                    string pathfile = Server.MapPath(Config.GetConfigValue("PathFileUpload"));

                    long intfile = Convert.ToInt64(e.CommandArgument);
                    TFileUpload ent = blc_file.GetFile_ByID(intfile);
                    if(ent!=null)
                    {
                        File.Delete(pathfile+ent.ConvertFileName);
                        blc_file.Delete(intfile);
                    }
                    BindGird();
                    resetfield();
                }
                catch
                {
                    Alert.Show("Xóa lỗi!");
                }
            }

        }

        protected void btnInsertBanner_Click(object sender, EventArgs e)
        {
            resetfield();
        }

        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            CreateFileUpload(txt_title1.Text, fileUrl01);
            CreateFileUpload(txt_title2.Text,fileUrl02);
            CreateFileUpload(txt_title3.Text,fileUrl03);
            CreateFileUpload(txt_title4.Text,fileUrl04);
            
            BindGird();
        }

        protected void btnDeleteBanner_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
                Alert.Show("Xóa lỗi!");
            }
        }



        #endregion

        private void resetfield()
        {
            lblFileID.Text = string.Empty;
            txt_title1.Text = string.Empty;
            txt_title2.Text = string.Empty;
            txt_title3.Text = string.Empty;
            txt_title4.Text = string.Empty;
        }

        #region Property

        protected string BindLinkUrl(object p_value)
        {
            if(p_value!=null)
            {
                long intfile = Convert.ToInt64(p_value);
                string LinkUrl = Config.GetConfigValue("LinkUrlFile");
                string url = HttpContext.Current.Request.Url.Host;
                string str = "/FileDowload.html";
                return string.Format("{0}{1}{2}{3}",url,LinkUrl,intfile,str);
            }
            return string.Empty;
        }

        private string UK
        {
            get
            {
                return Helper.ValidateParam("UK", string.Empty);
            }
        }

        protected int FileID
        {
            get
            {
                if (ViewState["g_FileID"] != null)
                    return Convert.ToInt32(ViewState["g_FileID"]);
                return -1;
            }
            set
            {
                ViewState["g_FileID"] = value;
            }
        }
       
        #endregion

    }
}