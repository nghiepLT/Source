using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PQT.API;

using NewsMng.BLC;
using NewsMng.DataDefine;
using PQT.Common;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using System.IO;
using UserMng.BLC;
using PQT.DAC;


namespace WebCus
{
    public partial class CustomerDownload : CommonPage
    {
        FileUpload_BLC blc_file = new FileUpload_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Client_Login_ID != 0)
            {
                Binddownload();
            }
            else
            {
                Response.Redirect("/dang-nhap");
            }
        }

#region download
        
        private void Binddownload()
        {
            TFileUpload ent = blc_file.GetFile_ByID(this.FileID);
            if (ent!=null)
            {
                string filename = ent.FileName.ToString().Trim();
                string pathfile = Server.MapPath(Config.GetConfigValue("PathFileUpload"));
                string fileDow = pathfile+ent.ConvertFileName;
                FileInfo fileInfo = new FileInfo(fileDow);
                if (fileInfo.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name.Replace(fileInfo.Name, filename));
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.Flush();
                    Response.WriteFile(fileInfo.FullName);
                }
                else
                {
                    Alert.Show("File không tồn tại!");
                    lblmss.Text = "File không tồn tại";
                    //string prevPage = Request.UrlReferrer.ToString();
                    //Response.Redirect(prevPage);
                }
            }
            else
            {
                string prevPage = Request.UrlReferrer.ToString();
                Response.Redirect(prevPage);
            }
            Dispose();
        }
#endregion

        private long FileID
        {
            get {
                string strQ = Helper.ValidateParam("FileID", "0");
                long Lfile = Convert.ToInt64(strQ);
                return Lfile;
            }
        }

        protected int Client_Login_ID
        {
            get
            {
                if (Session["g_UserMemberID"] != null)
                    return Convert.ToInt32(Session["g_UserMemberID"]);
                return 0;
            }
            set
            {
                Session["g_UserMemberID"] = value;
            }
        }
    }
}
