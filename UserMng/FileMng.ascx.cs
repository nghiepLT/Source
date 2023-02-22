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

using UserMng.BLC;
using UserMng.DataDefine;
using PQT.API;
using PQT.Common;
using System.IO;
using System.Collections.Generic;

namespace UserMng
{
    public partial class FileMng : XVNET_ModuleControl
    {

        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                LoadTreeFile();

                if (treeFile.SelectedNode == null)
                    treeFile.Nodes[0].Selected = true;

                string folderPath = treeFile.SelectedNode.Value;// Path.Combine(Server.MapPath("."), Config.GetConfigValue("FileManagerPath"));

                Bind_File_And_Folder(folderPath);
            }

        }


        private void LoadTreeFile()
        {
            treeFile.Nodes.Clear();
            string folderPath = Path.Combine(Server.MapPath("."), Config.GetConfigValue("FileManagerPath"));
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            treeFile.ExpandDepth = 1;

            // Add Root Node
            TreeNode root = new TreeNode();
            root.Text = "Root";
            root.Value = folderPath;
            treeFile.Nodes.Add(root);
            if (HttpContext.Current.Session["Selected_Folder"] == null)
                root.Selected = true;

            // Bind Tree
            BindFolderTree(root, folderPath);
            root.Expand();
        }

        private void Bind_File_And_Folder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                LoadTreeFile();
                if (treeFile.SelectedNode == null)
                    treeFile.Nodes[0].Selected = true;
                Bind_File_And_Folder(treeFile.SelectedNode.Value);
            }
            else
            {
                // Sub Directories
                string[] folders = Directory.GetDirectories(folderPath);
                IList<File_And_Folder> list = new List<File_And_Folder>();
                foreach (string folder in folders)
                {
                    DirectoryInfo di = new DirectoryInfo(folder);
                    list.Add(new File_And_Folder() { Name = di.Name, Path = di.FullName, Is_Folder = 1 });
                }

                string[] files = Directory.GetFiles(folderPath, "*", SearchOption.TopDirectoryOnly);
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    list.Add(new File_And_Folder() { Name = fi.Name, Path = fi.FullName, Is_Folder = 0 });
                }

                rpt_file.DataSource = list;
                rpt_file.DataBind();
            }
        }

        private void BindFolderTree(TreeNode pNode, string folderPath)
        {
            // Sub Directories
            string[] folders = Directory.GetDirectories(folderPath);
            foreach (string folder in folders)
            {
                DirectoryInfo di = new DirectoryInfo(folder);
                int count_file = di.GetFiles().Length;
                TreeNode nNode = new TreeNode();
                nNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                nNode.Text = di.Name + (count_file > 0 ? string.Format(" <i>({0})<i>", count_file) : "");
                nNode.Value = folder;// folder + ",1," + isHaveFile;
                pNode.ChildNodes.Add(nNode);
                if (HttpContext.Current.Session["Selected_Folder"] != null)
                {
                    if (HttpContext.Current.Session["Selected_Folder"].ToString() == folder)
                    {
                        nNode.Selected = true;
                        nNode.Expand();
                        pNode.Expand();
                        if (pNode.Parent != null)
                            pNode.Parent.Expand();
                    }
                }
                nNode.ImageUrl = "/Images/icon/IcoFileExt/folder.gif";
                BindFolderTree(nNode, folder);
            }
        }

        protected string CheckFileType(object p_fileName, int type, object is_folder)
        {
            string preFile = "folder";
            if (Convert.ToInt16(is_folder) != 1)
            {
                string ext = Path.GetExtension(p_fileName.ToString());
                switch (ext)
                {
                    case ".png":
                    case ".jpg":
                        preFile = "jpg";
                        break;
                    case ".gif":
                        preFile = "gif";
                        break;
                    case ".bmp":
                        preFile = "bmp";
                        break;
                    case ".xls":
                    case ".xlsx":
                        preFile = "excel";
                        break;
                    case ".html":
                    case ".htm":
                        preFile = "html";
                        break;
                    case ".mp3":
                        preFile = "mp3";
                        break;
                    case ".pdf":
                        preFile = "pdf";
                        break;
                    case ".ppt":
                    case ".pptx":
                        preFile = "ppt";
                        break;
                    case ".txt":
                        preFile = "txt";
                        break;
                    case ".mmp":
                        preFile = "Project";
                        break;
                    case ".doc":
                    case ".docx":
                        preFile = "word";
                        break;
                    case ".zip":
                    case ".rar":
                    case ".dad":
                        preFile = "zip";
                        break;
                    case ".exe":
                        preFile = "file";
                        break;
                    default:
                        preFile = "nofile";
                        break;

                }
            }
            return string.Format("/Images/icon/{1}/icon_{0}.{2}", preFile, (type == 1 ? "IcoFileExt" : "Icon_Big"), (type == 1 ? "gif" : "png"));

        }

        protected string Get_Url_Thumb(object p_fileName, int type, object is_folder)
        {
            string img_url = "";
            string ext = Path.GetExtension(p_fileName.ToString());
            if (ext == ".gif" || ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp")
            {
                string folderPath = treeFile.SelectedNode.Value;
                folderPath = folderPath.Replace(Server.MapPath("."), "");
                folderPath = folderPath.Replace("\\", "/");
                img_url = folderPath + "/" + p_fileName;
            }
            else
            {
                img_url = CheckFileType(p_fileName, type, is_folder);
            }

            return img_url;
        }

        #region Utils

        protected UserControl ParentCtrl()
        {
            Control objParent = Parent;
            while (!(objParent is UserControl))
            {
                objParent = objParent.Parent;
            }

            return objParent as UserControl;
        }

        #endregion

        #region Events

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string value = treeFile.SelectedNode.Value;
                string path = value.Substring(0, value.Length - 4);

                if (hdfNodeValue.Value == "1")
                {
                    Directory.Delete(path);
                }
                else
                {
                    File.Delete(path);
                }

                hdfNodeValue.Value = "";
                LoadTreeFile();
            }
            catch (System.Exception ex)
            {

            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }

        protected void btnNewFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string value = treeFile.SelectedNode.Value;
                string path = value.Substring(0, value.Length - 4);
                string newPath = Path.Combine(path, hdfNodeValue.Value);

                Directory.CreateDirectory(newPath);

                hdfNodeValue.Value = "";
                LoadTreeFile();
            }
            catch (System.Exception ex)
            {

            }
        }

        protected void btnRename_Click(object sender, EventArgs e)
        {

        }


        protected void treeFile_SelectedNodeChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Session["Selected_Folder"] = treeFile.SelectedNode.Value;
            Bind_File_And_Folder(treeFile.SelectedNode.Value);
            //string[] arr = treeFile.SelectedNode.Value.Split('\\');

            //hdfNodeValue.Value = arr[arr.Length - 1];
        }

        #endregion


    }

    public class File_And_Folder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int Is_Folder { get; set; }
    }
}