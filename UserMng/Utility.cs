using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using PQT.Common;
using System.Threading;

using System.Collections.Generic;

namespace UserMng
{
    public class Utility
    {
        
        private static Table AddRow(Table p_table, System.Collections.Generic.List<string> p_strCellText)
        {
            TableRow tbRow = new TableRow();
            for (int i = 0; i < p_strCellText.Count; i++)
            {
                TableCell tbCell = new TableCell();
                tbCell.Text = p_strCellText[i];
                tbRow.Cells.Add(tbCell);
            }
            p_table.Rows.Add(tbRow);
            return p_table;
        }

        public static string Encrypt(string stringToEncrypt)
        {
            string sEncryptionKey = "!#$a54?3";
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length)
            try
            {
                key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static string Decrypt(string stringToDecrypt)
        {
            string sEncryptionKey = "!#$a54?3";
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            //Private IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

            byte[] inputByteArray = new byte[stringToDecrypt.Length];
            try
            {
                key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetResourceXML(string p_pathFile)
        {
            DataTable dt = new DataTable();
            dt.ReadXml(p_pathFile);
            return dt;
        }

        public static void WriteResourceXML(DataTable p_source, string p_pathFile)
        {
            p_source.TableName = "Resource";
            p_source.WriteXml(p_pathFile, XmlWriteMode.WriteSchema);
        }

        public static string TrimString(string p_source, int p_length, string p_pattern)
        {
            if (p_source.Length > p_length)
            {
                return string.Format("{0}{1}", p_source.Substring(0, p_length - p_pattern.Length), p_pattern);
            }
            return p_source;
        }

        public static string TrimString(string p_source, int p_length)
        {
            return TrimString(p_source, p_length, "...");
        }

        public static string TextToHtmlText(string p_text)
        {
            return p_text.Replace("\r\n", "<br />");
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

        private static bool ResponseFile(string filepath, HttpResponse _Response)
        {
            bool result = false;
            string filename = Path.GetFileName(filepath);
            System.IO.Stream stream = null;
            try
            {
                // Open the file into a stream. 
                stream = new FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                // Total bytes to read: 
                long bytesToRead = stream.Length;
                _Response.ContentType = "application/octet-stream";
                _Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                // Read the bytes from the stream in small portions. 
                while (bytesToRead > 0)
                {
                    // Make sure the client is still connected. 
                    if (_Response.IsClientConnected)
                    {
                        // Read the data into the buffer and write into the 
                        // output stream. 
                        byte[] buffer = new Byte[10000];
                        int length = stream.Read(buffer, 0, 10000);
                        _Response.OutputStream.Write(buffer, 0, length);
                        _Response.Flush();
                        // We have already read some bytes.. need to read 
                        // only the remaining. 
                        bytesToRead = bytesToRead - length;
                    }
                    else
                    {
                        // Get out of the loop, if user is not connected anymore.. 
                        bytesToRead = -1;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                _Response.Write(ex.Message);
                // An error occurred.. 
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return result;
        }

        public static string GetContentType(string p_fileName)
        {
            string ext = Path.GetExtension(p_fileName);
            switch (ext)
            {
                case "wml":
                    return "text/vnd.wap.wml";
                case "wmlc":
                    return "application/vnd.wap.wmlc";
                case "wmls":
                    return "application/vnd.wap.wmlscriptc";
                case "jar":
                    return "application/java-archive"; // Or application/x-java-archive
                case "jad":
                    return "text/vnd.sun.j2me.app-descriptor;charset=UTF-8";
                case "sis":
                case "sisx":
                    return "application/vnd.symbian.install";
                case "mmf":
                    return "application/vnd.smaf";// Or application/x-smaf
                case "mpn":
                    return "application/vnd.mophun.application";
                case "mpc":
                    return "application/vnd.mophun.application";
                case "thm":
                    return "application/vnd.eri.thm";
                case "nth":
                    return "application/vnd.nok-s40theme";
                default:
                    return "application/java-archive";
            }
        }

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

        public static long TryParseLong(object p_value,int p_default)
        {
            try
            {
                if (p_value!=null)
                {
                    long longValue = Convert.ToInt64(p_value);
                    return longValue;
                }
                return p_default;
                
            }
            catch (System.Exception ex)
            {
                return p_default;
            }
        }

#region export

        public static bool IsDatetime(object obj, bool p_is_VN_DateTime)
        {
            try
            {
                if (obj.ToString().Length > 7)
                {
                    System.Globalization.CultureInfo _cultureInfo = new System.Globalization.CultureInfo(p_is_VN_DateTime ? "vi-vn" : "en-us");
                    DateTime a = Convert.ToDateTime(obj, _cultureInfo);
                    return true;
                }
                return false;

            }
            catch
            {
                return false;
            }
        }

        public static void Export(string fileName, DataTable dt)
        {
            string attachment = string.Format("attachment; filename={0}", fileName);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = Encoding.Unicode;
            HttpContext.Current.Response.BinaryWrite(Encoding.Unicode.GetPreamble());

            HttpContext.Current.Response.AddHeader("content-disposition", attachment);

            HttpContext.Current.Response.ContentType = "application/csv";
            // HttpContext.Current.Response.ContentType = "application/vnd.ms-excel [official], application/msexcel, application/x-msexcel, application/x-ms-excel, application/vnd.ms-excel, application/x-excel, application/x-dos_ms_excel, application/xls";

            string tab = "";
            //HttpContext.Current.Response.Write("hello aaaa\n");
            foreach (DataColumn dc in dt.Columns)
            {

                HttpContext.Current.Response.Write(tab + dc.ColumnName);

                tab = "\t";

            }

            HttpContext.Current.Response.Write("\n");
            HttpContext.Current.Response.Flush();

            int i;

            foreach (DataRow dr in dt.Rows)
            {

                tab = "";

                for (i = 0; i < dt.Columns.Count; i++)
                {
                    string currentText = dr[i].ToString();
                    currentText = currentText.Trim();
                    if (dr[i].ToString().ToLower() == "true")
                        currentText = "Yes";
                    else if (dr[i].ToString().ToLower() == "false")
                        currentText = "No";

                    else if (IsDatetime(currentText, false))
                    {
                        System.Globalization.CultureInfo _cultureInfo = new System.Globalization.CultureInfo("en-us");
                        currentText = Convert.ToDateTime(currentText, _cultureInfo).ToString("yyyy/MM/dd");
                    }

                    HttpContext.Current.Response.Write(tab + currentText);

                    tab = "\t";

                }

                HttpContext.Current.Response.Write("\n");
                HttpContext.Current.Response.Flush();
            }

            HttpContext.Current.Response.End();

        }

        public static void Export(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();

                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        } 
#endregion

        

    }


    
}
