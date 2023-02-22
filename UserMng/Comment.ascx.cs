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
using PQT.API.DataDefine.Sys;
using PQT.API.File;
using PQT.Common;
using PQT.DAC;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using PQT.Controls;
using System.Text.RegularExpressions;

namespace UserMng
{
    public partial class Comment : XVNET_ModuleControl
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        int pageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            //{
            if (!IsPostBack)
            {
                if (this.ReplycommentID != -1)
                {
                    divRely.Visible = true;
                }
                else
                {
                    divRely.Visible = false;
                }
                BindGridUser();
            }
            //}
        }

        protected string BindNum(object p_value)
        {
            if (p_value != null)
            {
                int intvalue = Helper.TryParseInt(p_value.ToString(), 0);
                int recordCount = blc_user.CountComment(this.LongNewsID, -1, this.KeyWord, intvalue);
                return recordCount.ToString();
            }
            return "0";
        }

        protected UserControl ParentCtrl()
        {
            Control objParent = Parent;
            while (!(objParent is UserControl))
            {
                objParent = objParent.Parent;
            }

            return objParent as UserControl;
        }

#region bind
        protected string Bind_Email(object p_value)
        {
            if (p_value != null)
            {
                int intValue = Helper.TryParseInt(p_value.ToString(), 0);
                TUser ent = blc_user.GetUser_ByIDAll(intValue);
                if (ent != null)
                {
                    return ent.Email;
                }
            }
            return string.Empty;
        }
        protected string Bind_Name(object p_value)
        {
            if (p_value!=null)
            {
                int intValue = Helper.TryParseInt(p_value.ToString(), 0);
                TUser ent = blc_user.GetUser_ByIDAll(intValue);
                if (ent!=null)
                {
                    return ent.LoginID;
                }
            }
            return string.Empty;
        }
#endregion

        #region MainMethods

        private void BindGridUser()
        {
            IList<TComment> list = blc_user.RowsComment(this.CurrentPage, pageSize, this.LongNewsID, -1, this.KeyWord,0);
            gvBanner.DataSource = list;
            gvBanner.DataBind();

            int recordCount = blc_user.CountComment(this.LongNewsID, -1, this.KeyWord,0);
            PQTPager1.RecordCount = recordCount;
            if (recordCount > 0)
            {
                PQTPager1.PageSize = pageSize;
                PQTPager1.PagerButtonCount = 5;
                PQTPager1.CurrentPageIndex = this.CurrentPage - 1;
            }
        }

        private void BindGridTraLoi(int idcomment)
        {
            if (idcomment != 0)
            {
                IList<TComment> list = blc_user.RowsComment(this.CurrentPage, pageSize, this.LongNewsID, -1, this.KeyWord, idcomment);
                GridView1.DataSource = list;
                GridView1.DataBind();
            }
            else
            {
                IList<TComment> list = null;
                GridView1.DataSource = list;
                GridView1.DataBind();
            }
        }

        private void BindUserDetailInfo(Int64 longCommentID)
        {
            //if (this.ReplycommentID != -1)
            //{
            //    divRely.Visible = true;
            //}
            //else
            //{
            //    divRely.Visible = false;
            //}

            TComment ent = blc_user.GetComment_ByIDcommet(longCommentID);
            if (ent != null)
            {
                ddlStatus.SelectedValue = ent.Status.ToString();
                string str = "";
                str = "<div><span>Họ Tên:</span> " + ent.Name + "</div>";
                str += "<div><span>Email:</span> " + ent.Email + "</div>";
                //str += "<div><span>Điện thoại:</span> " + ent.str01 + "</div>";
                //str += "<div><span>Tiêu đề:</span> " + ent.Title + "</div>";
                str += "<div><span>Nội Dung:</span></div>";
                ltlContent.Text = str + ent.Comment_Content;

                if (!string.IsNullOrEmpty(this.StrReply))
                {
                    IList<TComment> list = blc_user.RowsComment(1, int.MaxValue, this.LongNewsID, -1, this.KeyWord, ent.CommentID);
                    if (list.Count > 0)
                    {
                        this.ReplycommentID = list.First().CommentID;
                        txtContent.Value = list.First().Comment_Content;
                    }
                }
            }
        }

        #endregion

        #region Utils

        private string GetImageName(object p_fileID)
        {
            string strp_value = p_fileID!=null ? (!string.IsNullOrEmpty(p_fileID.ToString())? p_fileID.ToString() : "0") : "0";
            Int64 longValue = Convert.ToInt64(strp_value);
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(longValue);

            if (fileEnt != null)
            {
                return fileEnt.RealFileName.ToString();
            }
            return string.Empty;
        }

        protected string GetImagePath_Paint(object p_fileID, int p_type)
        {
            if (p_fileID == DBNull.Value || p_fileID == null)
                return "";
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(Convert.ToInt64(p_fileID));
            if (fileEnt != null)
                return this.GetImageUrl(Convert.ToInt64(p_fileID), "CartImagePath", p_type == 1 ? ImageSizeType.Big : (p_type == 2 ? ImageSizeType.Medium : ImageSizeType.Small));
            return string.Empty;
        }

        protected string GetImagePath_Frame(object p_fileID, int p_type)
        {
            if (p_fileID == DBNull.Value || p_fileID == null)
                return "";
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(Convert.ToInt64(p_fileID));
            if (fileEnt != null)
                return this.GetImageUrl(Convert.ToInt64(p_fileID), "FrameImagePath", p_type == 1 ? ImageSizeType.Big : (p_type == 2 ? ImageSizeType.Medium : ImageSizeType.Small));
            return string.Empty;
        }

        

        private void ResetUser()
        {
            this.longcommentID = -1;
            this.ReplycommentID = -1;
            txtContent.Value = string.Empty;
            divRely.Visible = false;
            ltlContent.Text = string.Empty;
        }

        #endregion

#region page
        protected void Pager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.CurrentPage = e.NewPageIndex + 1;
            BindGridUser();
        }
        private int CurrentPage
        {
            get
            {
                if (ViewState["g_CurrentPage"] != null)
                    return Convert.ToInt32(ViewState["g_CurrentPage"]);
                return 1;
            }
            set
            {
                ViewState["g_CurrentPage"] = value;
            }
        }
        public int numpage()
        {
            int page = this.CurrentPage;

            if (page > 1)
                return ((Convert.ToInt32(page) * pageSize - pageSize) > 0) ? (Convert.ToInt32(page) * pageSize - pageSize) : 0;
            return 0;
        }

#endregion

        #region trim

        protected string TrimText_Br(object p_content, int p_num)
        {
            if (p_content != null)
            {
                string content = p_content.ToString();

                if (content.Length > p_num)
                {
                    content = content.Substring(0, p_num) + "...";
                }
                return content.Replace("\r\n", "<br/>");
            }
            return string.Empty;
        }

        protected string TrimText(object p_content, int p_num)
        {
            if (p_content != null)
            {
                string content = p_content.ToString();
                string rm = RemoveHtml(content);
                if (rm.Length > p_num)
                {
                    rm = rm.Substring(0, p_num) + "...";
                }
                return rm;
            }
            return string.Empty;
        }

        protected string RemoveHtml(string p_content)
        {
            return Server.HtmlDecode(Regex.Replace(p_content.Trim(), @"<(.|\n)*?>", string.Empty));
        }
        #endregion

#region Events

        private void DeleteComment(Int64 longCommentID)
        {
            blc_user.DeleteComment(longCommentID);
        }

        private void UpdateStatusComment(Int64 longCommentID,int intstatus)
        {
           blc_user.UpdateComment_Status(longCommentID, intstatus);
        }

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewItem")
            {
                this.longcommentID = Utility.TryParseLong(e.CommandArgument, 0);
                BindUserDetailInfo(Utility.TryParseLong(e.CommandArgument, 0));
                if (!string.IsNullOrEmpty(this.StrReply))
                {
                    divRely.Visible = true;
                }
                else
                {
                    divRely.Visible = false;
                }
            }
            if (e.CommandName == "ViewTraLoi")
            {
                this.longcommentID = Utility.TryParseLong(e.CommandArgument, 0);
                BindUserDetailInfo(Utility.TryParseLong(e.CommandArgument, 0));
                if (!string.IsNullOrEmpty(this.StrReply))
                {
                    divRely.Visible = true;
                }
                else
                {
                    divRely.Visible = false;
                }
                //divRely.Visible = true;
                int iCom = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                this.ReplycommentID = Utility.TryParseLong(e.CommandArgument, -1);
                BindGridTraLoi(iCom);
                BindUserDetailInfo(Utility.TryParseLong(e.CommandArgument, 0));
            }
        }
        protected void gvBanner_RowCommand2(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewItem")
            {
                this.longcommentID = Utility.TryParseLong(e.CommandArgument, 0);
                BindUserDetailInfo(Utility.TryParseLong(e.CommandArgument, 0));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.CurrentPage = 1;
            BindGridUser();
        }

        protected void btnActive_Click(object sender, EventArgs e)
        {
            if (this.longcommentID != -1)
            {
                
                TComment ent = new TComment();
                ent.Comment_Content = txtContent.Value;
                ent.CreateDate = DateTime.Now;
                ent.Email = string.Empty;
                ent.ID_Ref = 0;
                ent.Name = "UKAdmin";
                ent.Ref_Type = 0;
                ent.Status = 1;
                ent.Title = "";
                ent.UniqueKey = this.KeyWord;
                ent.UserID = 0;
                ent.Parent = this.longcommentID;

                if (!string.IsNullOrEmpty(this.StrReply))
                {
                    if (this.ReplycommentID == -1)
                    {
                        blc_user.CreateComment_ent(ent);
                    }
                    else
                    {
                        ent.CommentID = this.ReplycommentID;
                        blc_user.UpdateCommentParent(ent);
                    }
                }
                int intstatus = Helper.TryParseInt(ddlStatus.SelectedValue, 0);
                UpdateStatusComment(this.longcommentID, intstatus);
                Alert.Show("Cập nhật thành công");
                this.CurrentPage = 1;
                BindGridUser();
                ResetUser();
            }
            else
            {
                Alert.Show("Chưa chọn bình luận để cập nhật!");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ReplycommentID != -1)
            {
                TComment ent = new TComment();
                ent.Comment_Content = txtContent.Value;
                ent.CreateDate = DateTime.Now;
                ent.Email = string.Empty;
                ent.ID_Ref = 0;
                ent.Name = "UKAdmin";
                ent.Ref_Type = 0;
                ent.Status = 1;
                ent.Title = "";
                ent.UniqueKey = this.KeyWord;
                ent.UserID = 0;
                ent.Parent = this.ReplycommentID;
                blc_user.CreateComment_ent(ent);

                PQT.API.Alert.Show("Trả lời bình luận thành công!");
                BindGridTraLoi(Helper.TryParseInt(this.ReplycommentID.ToString(),0));
                txtContent.Value = string.Empty;
            }
            else
            {
                Alert.Show("Chưa chọn bình luận để cập nhật!");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                
                string[] arrIDs = hdnIDs.Value.TrimEnd('|').Split('|');
                foreach (string id in arrIDs)
                {
                    long idP = Utility.TryParseLong(id, 0);
                    DeleteComment(idP);
                }
                Alert.Show("Xóa thành công");
                this.CurrentPage = 1;
                BindGridUser();
                BindGridTraLoi(0);
                divRely.Visible = false;
                this.longcommentID = -1;
                this.ReplycommentID = -1;
                ResetUser();
            }
            catch (System.Exception ex)
            {
                Alert.Show("Xóa thất bại");
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Property

        protected string KeyWord
        {
            get
            {
                return Helper.ValidateParam("UK", "News");
            }
        }

        private long longcommentID
        {
            get
            {
                if (ViewState["longcommentID"] != null)
                    return Utility.TryParseLong(ViewState["longcommentID"],0);
                return -1;
            }
            set
            {
                ViewState["longcommentID"] = value;
            }
        }

        private long ReplycommentID
        {
            get
            {
                if (ViewState["p_ReplycommentID"] != null)
                    return Utility.TryParseLong(ViewState["p_ReplycommentID"], 0);
                return -1;
            }
            set
            {
                ViewState["p_ReplycommentID"] = value;
            }
        }

        private long LongNewsID
        {
            get
            {
                return Helper.ValidateParam("id", 0);
            }
        }

        private string StrReply
        {
            get
            {
                return Helper.ValidateParam("Reply", string.Empty);
            }
        }

        protected string StrStatus(object p_value)
        {
            if (p_value!=null)
            {
                int intPvaule = Helper.TryParseInt(p_value.ToString(), 3);
                if (intPvaule == 1)
                {
                    return "Hiện"; 
                }
                else
                {
                    return "Ẩn";
                }
            }
            return string.Empty;
        }

        protected string MemberName(object p_MemberReg)
        {
            if (p_MemberReg != null)
            {
                int p_value = Helper.TryParseInt(p_MemberReg.ToString(), 0);

                TUser ent = blc_user.GetUser_ByIDAll(p_value);
                if (ent != null)
                {
                    return ent.LoginID != null ? ent.LoginID.ToString() : string.Empty;
                }
                return string.Empty;
            }
            return string.Empty;
        }
        #endregion

    }
}