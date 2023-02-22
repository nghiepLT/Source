using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using PQT.API;
using PQT.API.Connection;
using UserMng.DAC;
using UserMng.DataDefine;
using System.Data;
using PQT.DAC;

namespace UserMng.BLC
{
    public class FileUpload_BLC : XVNET_ModuleControl
    {
        UploadFileDataContext da = null;
        public FileUpload_BLC()
        {
            if (da == null)
            {
                da = new UploadFileDataContext();
            }
        }

#region list
        public IList<TFileUpload> ListFile_byKey(string Keyword)
        {
            try
            {
                return da.TFileUploads.Where(z => z.KeyWord.ToLower().Trim() == Keyword.ToLower().Trim()).OrderByDescending(z => z.UploadFileID).ToList();
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
#endregion

#region get
        public TFileUpload GetFile_ByID(long UploadFileID)
        {
            var list = da.TFileUploads.Where(z => z.UploadFileID == UploadFileID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }
#endregion

        #region Create
        public long CreateFile(string strTitle,string FileName, string ConvertFileName, string ServerFilePath, string KeyWord, DateTime CreateDate)
        {
            TFileUpload objEnt = null;
            objEnt = new TFileUpload();

            objEnt.Title = strTitle;
            objEnt.FileName = FileName;
            objEnt.ConvertFileName = ConvertFileName;
            objEnt.ServerFilePath = ServerFilePath;
            objEnt.KeyWord = KeyWord;
            objEnt.CreateDate = CreateDate;

            da.TFileUploads.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.UploadFileID;
        }
        #endregion

        public bool Delete(long UploadFileID)
        {
            try
            {
                IList<TFileUpload> list = da.TFileUploads.Where(z => z.UploadFileID == UploadFileID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();
                    da.TFileUploads.DeleteOnSubmit(objVlaue);
                    da.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }

        }
    }
}