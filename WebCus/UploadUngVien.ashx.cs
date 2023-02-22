using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace WebCus
{
    /// <summary>
    /// Summary description for UploadUngVien
    /// </summary>
    public class UploadUngVien : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/Uploads/UngVien/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        str_image = DateTime.Now.ToString("yyyyMMddhhmmssff") + "_" + fileName;
                        string pathToSave_100 = HttpContext.Current.Server.MapPath("~/Uploads/UngVien/") + str_image;
                        file.SaveAs(pathToSave_100);
                    }
                }
                //  database record update logic here  ()

                context.Response.Write(str_image);
            }
            catch (Exception ex)
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}