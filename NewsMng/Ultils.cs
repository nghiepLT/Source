using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using PQT.API;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using System.Threading;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

namespace NewsMng
{
    public static class Ultils
    {

        public static DataTable InitTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("LangID", typeof(string));
            return dt;
        }

        public static DataTable FormatForMultiLanguageControl(DataTable p_source, string p_currentColumnName)
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

        public static void DeleteFileInServer(long p_fileID)
        {
            try
            {
                FileManager fileMng = new FileManager();
                CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);

                if (fileEnt != null)
                {
                    CommonFileCollection fileColl = new CommonFileCollection();
                    fileColl.Remove(fileEnt);
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileEnt.ServerFilePath);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }
                }
            }
            catch// (System.Exception e)
            {

            }

        }
#region conver url
        public static string StripDiacritics(object accented)
        {
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = accented.ToString().Normalize(NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ConvertUrlText(object p_urlText)
        {
            string urlText = StripDiacritics(p_urlText);
            Regex purifyUrlRegex = new Regex("[^-a-zA-Z0-9_ ]");
            Regex dashesRegex = new Regex("[-_ ]+", RegexOptions.Compiled);

            urlText = purifyUrlRegex.Replace(urlText, "");
            urlText = urlText.Trim();
            urlText = dashesRegex.Replace(urlText, "-");
            return urlText;

        }
#endregion
        
    }
}
