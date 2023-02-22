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

namespace WebCus
{
    public static class  Utilis
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

        public static int TryParseInt(object p_source)
        {
            int result = 0;
            if (p_source == DBNull.Value || p_source == null)
                return result;
            int.TryParse(p_source.ToString(), out result);
            return result;
        }

        public static DateTime TryParseDate(object p_source)
        {
            DateTime result = DateTime.Now;
            if (p_source == DBNull.Value || p_source == null)
                return result;
            DateTime.TryParse(p_source.ToString(), out result);
            return result;
        }

        public static Boolean TryParseBool(object p_source)
        {
            Boolean result = false;
            if (p_source == DBNull.Value || p_source == null)
                return result;
            Boolean.TryParse(p_source.ToString(), out result);
            return result;
        }

        public static long TryParseLong(object p_source)
        {
            long result = 0;
            if (p_source == DBNull.Value || p_source == null)
                return result;
            long.TryParse(p_source.ToString(), out result);
            return result;
        }

        public static decimal TryParseDecimal(object p_source)
        {
            decimal result = 0;
            if (p_source == DBNull.Value || p_source == null)
                return result;
            decimal.TryParse(p_source.ToString(), out result);
            return result;
        }

        public static double TryParseDouble(object p_source)
        {
            double result = 0;
            if (p_source == DBNull.Value || p_source == null)
                return result;
            double.TryParse(p_source.ToString(), out result);
            return result;
        }

        public static string TrimText(object p_content, int p_num)
        {
            string content = p_content.ToString();
            if (content.Length > p_num)
            {
                content = content.Substring(0, p_num);
                content = content.Substring(0, content.LastIndexOf(" ")) + "...";
            }
            return content;
        }

    }

    public enum PaymentType : int
    {
        Giao_hang_nhan_tien = 10,
        Chuyen_khoan = 20,
        Thanh_Toan_Tai_Kho=25,
        TTTT_OnePay = 31,
        TTTT_NganLuong = 32,
        TTTT_BaoKim = 33,
    }

    public enum Transaction_Status : int
    {
        Chua_thanh_toan = 0,
        Da_thanh_toan = 1,
        Da_giao_hang = 4,
        Khong_hop_le = 5,
        Huy = 6
    }
}
