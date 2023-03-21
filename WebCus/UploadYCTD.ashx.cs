﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebCus
{
    /// <summary>
    /// Summary description for UploadYCTD
    /// </summary>
    public class UploadYCTD : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/Uploads/TuyenDung/FileMau");
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
                        var pathdelete = HttpContext.Current.Server.MapPath("~/Uploads/TuyenDung/FileMau/");
                        DirectoryInfo dir = new DirectoryInfo(pathdelete);

                        foreach (FileInfo fi in dir.GetFiles())
                        {
                            fi.Delete();
                        }
                        string pathToSave_100 = HttpContext.Current.Server.MapPath("~/Uploads/TuyenDung/FileMau/") + str_image;
                      
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