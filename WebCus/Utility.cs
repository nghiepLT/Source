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

using UserMng.BLC;
using UserMng.DataDefine;
using PQT.API.Connection;
using System.Collections.Generic;
//using ProductMng.BLC;


namespace WebCus
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
#region trim

        public static string TrimText(object p_content, int p_num)
        {
            string content = p_content.ToString();
            if (content.Length > p_num)
            {
                content = content.Substring(0, p_num) + "...";
            }
            return content;
        }

#endregion
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

        public static bool ResponseFileInstallMobile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                if (!string.IsNullOrEmpty(_fileName))
                {

                    if (File.Exists(_fullPath))
                    {


                        FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        BinaryReader br = new BinaryReader(myFile);
                        try
                        {
                            _Response.AddHeader("Accept-Ranges", "bytes");
                            _Response.Buffer = false;
                            long fileLength = myFile.Length;
                            long startBytes = 0;

                            int pack = 10240; //10K bytes
                            int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
                            if (_Request.Headers["Range"] != null)
                            {
                                _Response.StatusCode = 206;
                                string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                                startBytes = Convert.ToInt64(range[1]);
                            }
                            _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                            if (startBytes != 0)
                            {
                                _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                            }
                            _Response.AddHeader("Connection", "Keep-Alive");
                            _Response.ContentType = GetContentType(_fileName);
                            _Response.AddHeader("Content-Length", "100");
                            //_Response.ContentType = "application/octet-stream";
                            _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                            br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                            int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                            for (int i = 0; i < maxCount; i++)
                            {
                                if (_Response.IsClientConnected)
                                {
                                    _Response.BinaryWrite(br.ReadBytes(pack));
                                    Thread.Sleep(sleep);
                                }
                                else
                                {
                                    i = maxCount;
                                }
                            }

                        }
                        catch
                        {
                            return false;
                        }
                        finally
                        {
                            br.Close();
                            myFile.Close();
                        }
                    }
                    else
                    {
                        Alert.Show("File not exists");
                        return false;
                    }
                }
                else
                {
                    Alert.Show("Please check PhoneBrand and Model have suppost for this game");
                    return false;
                }

            }
            catch
            {
                return false;
            }
            return true;
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
        public static string getID_of_URL(string url)
        {
            int lastDot = url.IndexOf(".html");
            int lastHyphen = url.LastIndexOf('-');
            int len = lastDot - lastHyphen;
            return url.Substring(url.LastIndexOf('-') + 1, len - 1);

        }

        #region UserInfo

        public static UserEntity GetUserInfo(int p_userID)
        {
            UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
            return nBLC.RowUser(p_userID);
        }

        public static string GetUserName(int p_userID)
        {
            UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
            UserEntity ent = nBLC.RowUser(p_userID);
            return ent != null ? ent.LoginID : "";
        }

        public static string GetUserEmail(int p_userID)
        {
            UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
            UserEntity ent = nBLC.RowUser(p_userID);
            return ent != null ? ent.Email : "";
        }

        #endregion

        #region
        public static long TryParseLong(object p_value, int p_default)
        {
            try
            {
                if (p_value != null)
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
        #endregion

        #region
        public static decimal TryParseDecimal(object p_value, int p_default)
        {
            try
            {
                if (p_value != null)
                {
                    decimal longValue = Convert.ToDecimal(p_value);
                    return longValue;
                }
                return p_default;

            }
            catch (System.Exception ex)
            {
                return p_default;
            }
        }
        #endregion
    }

    public static class Alert
    {

        /// <summary> 
        /// Shows a client-side JavaScript alert in the browser. 
        /// </summary> 
        /// <param name="message">The message to appear in the alert.</param> 
        public static void Show(string message)
        {
            // Cleans the message to allow single quotation marks 
            string cleanMessage = message.Replace("'", "/'");
            cleanMessage = message.Replace("\r\n", " ");
            string script = string.Format("alert('{0}');", cleanMessage);
            //string script = "<script type='text/javascript'>alert('" + cleanMessage + "');</script>";

            // Gets the executing web page 
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page 
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(Alert), "alert", script, true);
            }
        }

    }

    public static class MessageBox
    {
        /// <summary> 
        /// Shows a client-side JavaScript alert in the browser. 
        /// </summary> 
        /// <param name="message">The message to appear in the alert.</param> 
        public static void Show(string message, bool p_htmlEncode)
        {
            try
            {
                // Cleans the message to allow single quotation marks 
                string messages = message.Replace("'", "/'");
                messages = message.Replace("\r\n", " ");
                if (p_htmlEncode)
                    messages = HttpContext.Current.Server.HtmlEncode(messages);
                string script = string.Format("alert('{0}');", messages);
                //string script = "<script type='text/javascript'>alert('" + cleanMessage + "');</script>";

                // Gets the executing web page 
                Page page = HttpContext.Current.CurrentHandler as Page;

                // Checks if the handler is a Page and that the script isn't allready on the Page 
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(typeof(MessageBox), "alert", script, true);
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        public static void Show(string message, bool p_htmlEncode, string link)
        {
            // Cleans the message to allow single quotation marks 
          string  messages = message.Replace("'", "/'");
          messages = message.Replace("\r\n", " ");
            if (p_htmlEncode)
                messages = HttpContext.Current.Server.HtmlEncode(messages);
            string script = string.Format("alert('{0}');", messages);
            string redirectLink = string.Format("window.location = '{0}';", link);
            //string script = "<script type='text/javascript'>alert('" + cleanMessage + "');</script>";

            // Gets the executing web page 
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page 
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(MessageBox), "alert", script, true);
                page.ClientScript.RegisterClientScriptBlock(typeof(MessageBox), "window.location", redirectLink, true);
            }
        }

        public static void Show(string message)
        {
            try
            {
                // Cleans the message to allow single quotation marks 
             string messages = message.Replace("'", "/'");
                messages = message.Replace("\r\n", " ");
                messages = HttpContext.Current.Server.HtmlEncode(message);
                string script = string.Format("alert('{0}');", messages);
                //string script = "<script type='text/javascript'>alert('" + cleanMessage + "');</script>";

                // Gets the executing web page 
                Page page = HttpContext.Current.CurrentHandler as Page;

                // Checks if the handler is a Page and that the script isn't allready on the Page 
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(typeof(MessageBox), "alert", script, true);
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        public static void Show(string p_message, string p_moveLocation)
        {
            HttpContext.Current.Response.Write(ClientMessageBoxAfterMove(p_message, p_moveLocation));
        }


        private static string ClientMessageBox(string p_message)
        {
            StringBuilder sb = new StringBuilder();
          string  p_messages = p_message.Replace("'", "''");
          p_messages = HttpContext.Current.Server.HtmlEncode(p_messages);

            sb.Append("<script language='javascript'>");
            string script = string.Format("alert(\"{0}\")", p_messages);
            sb.Append(script);
            sb.Append("</script>");

            return sb.ToString();
        } // end of mehotd ClientMessageBox


        private static string ClientMessageBoxAfterMove(string p_message, string p_moveLocation)
        {
            StringBuilder sb = new StringBuilder();
            string p_messages = p_message.Replace("'", "''");
            p_messages = HttpContext.Current.Server.HtmlEncode(p_messages);

            sb.Append("<script language='javascript'>");
            string script = string.Format("alert('{0}'); document.location='{1}';", p_messages, p_moveLocation);
            sb.Append(script);
            sb.Append("</script>");

            return sb.ToString();
        } // end of mehotd ClientMessageBoxAfterMove


    }

#region jquerylist
    public class CommentJQList
    {
        public Int64 CommentID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Comment_Content { get; set; }
        public string CreateDate { get; set; }
        public Int64 UserID { get; set; }
        public Int64 ID_Ref { get; set; }
        public Int64 Ref_Type { get; set; }
        public int Status { get; set; }
        public string UniqueKey { get; set; }
    }

    public class ProductJQList
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string NameLink { get; set; }
    }

    public class ProductCateJQList
    {
        public int CategoryID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string NameLink { get; set; }
    }
   
#endregion

    
}
