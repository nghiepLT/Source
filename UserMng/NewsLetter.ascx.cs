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
using System.IO;
using PQT.Controls;

namespace UserMng
{
    public partial class NewsLetter : XVNET_ModuleControl
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        int pageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindGridUser();
            }
        }

        #region MainMethods

        private void BindGridUser()
        {
            int intstatus = Helper.TryParseInt(ddlStatus.SelectedValue,0);
            DataTable list = blc_user.RowsNewsLetter(this.CurrentPage, pageSize, txtSearchText.Text.Trim(), intstatus,3, ddlGender.SelectedValue);
            gvBanner.DataSource = list;
            gvBanner.DataBind();

            int recordCount = blc_user.CountNewsLetter(txtSearchText.Text.Trim(), intstatus,3, ddlGender.SelectedValue);
            PQTPager1.RecordCount = recordCount;
            if (recordCount > 0)
            {
                PQTPager1.PageSize = pageSize;
                PQTPager1.PagerButtonCount = 5;
                PQTPager1.CurrentPageIndex = this.CurrentPage - 1;
            }
        }

        //private void BindUserDetailInfo()
        //{
        //    TUser ent = blc_user.GetMember_ByID(this.UserDetail);
        //    if(ent!=null)
        //    {
        //        lbl_fullname.Text = ent.UserName;
        //        lblEmail.Text = ent.Email;
        //        lbl_loginid.Text = ent.LoginID;
        //        lblYahooID.Text = ent.YahooID;
        //        lbl_phone.Text = ent.Tel;
        //        lbl_brithday.Text = ent.Brithday!=null?ent.Brithday.Value.ToString("dd/MM/yyyy"):string.Empty;
        //        lbl_gioitinh.Text = StrGender(ent.Gender);
        //        lblAddress.Text = ent.Address;
        //        lblRegDate.Text = ent.RegDate != null ? ent.RegDate.Value.ToString("dd/MM/yyyy") : string.Empty;
        //    }
        //}

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

        protected UserControl ParentCtrl()
        {
            Control objParent = Parent;
            while (!(objParent is UserControl))
            {
                objParent = objParent.Parent;
            }

            return objParent as UserControl;
        }

        private void ResetUser()
        {
            //this.UserDetail = -1;
            //lbl_fullname.Text = string.Empty;
            //lblEmail.Text = string.Empty;
            //lbl_loginid.Text = string.Empty;
            //lblYahooID.Text = string.Empty;
            //lbl_phone.Text = string.Empty;
            //lbl_brithday.Text = string.Empty;
            //lbl_gioitinh.Text = string.Empty;
            //lblAddress.Text = string.Empty;
        }

        private bool DeleteNewsLetter(Int64 p_value)
        {
            return blc_user.deleteNewsletter(p_value);
        }

        private bool UpdateNewsLetter_chuaxuat(Int64 p_value, int intStatus)
        {
            return blc_user.UpdateNewsletter_chuaxuat(p_value, intStatus);
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
        #region Events

        

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                 try
                 {
                     if (DeleteNewsLetter(Utility.TryParseLong(e.CommandArgument,0)) == true)
                     {
                         Alert.Show("Delete success!");
                         ResetUser();
                         BindGridUser();
                     }
                 }
                 catch
                 {
                     Alert.Show("Delete failed!");
                 }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.CurrentPage = 1;
            BindGridUser();
        }

        protected void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                string[] arrIDs = hdnIDs.Value.TrimEnd('|').Split('|');
                foreach (string id in arrIDs)
                {
                    long idP = Utility.TryParseLong(id,0);
                    UpdateNewsLetter_chuaxuat(idP, 0);
                }
                Alert.Show("Cập nhật thành công");
                this.CurrentPage = 1;
                BindGridUser();
            }
            catch (System.Exception ex)
            {
                Alert.Show("Cập nhật thất bại");
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
                    DeleteNewsLetter(idP);
                }
                Alert.Show("Xóa thành công");
                this.CurrentPage = 1;
                BindGridUser();
            }
            catch (System.Exception ex)
            {
                Alert.Show("Xóa thất bại");
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (exportFile() == true)
            {
                Alert.Show("Xuất file thanh công");
            }
            this.CurrentPage = 1;
            BindGridUser();
        }

        #endregion

        #region export

        private void EventColumnRemove(DataTable dt, string ColumnName)
        {
            if (dt.Columns.Contains(ColumnName))
                dt.Columns.Remove(ColumnName);
        }

        private void EventColumn(DataTable dt, string ColumnName, string NewNameColumn)
        {
            if (dt.Columns.Contains(ColumnName))
                dt.Columns[ColumnName].ColumnName = NewNameColumn;
        }

        private void AddColumn(DataTable dt, string ColumnName)
        {
            DataColumn dc = new DataColumn(ColumnName);
            dc.DataType = typeof(string);
            dc.DefaultValue = dt.TableName;
            dt.Columns.Add(dc);
        }

        //DataTable BindDataTable(DataTable dt)
        //{
        //    if (dt.Rows.Count == 0) return dt;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        if (dr["AgentID"] != null)
        //        {
        //            dr["Đại lý"] = BindAgent(dr["AgentID"]).ToString();
        //        }
        //        if (dr["ProductID"] != null)
        //            dr["Sản phẩm"] = ProductName(dr["ProductID"]).ToString();
        //        //if (dt.Columns.Contains("AreaID"))
        //        //    dr["AreaID"] = EvalAreaIDOld(dr["AreaID"]).ToString();
        //    }
        //    return dt;
        //}

        private bool exportFile()
        {
            try
            {
                int intStatus = Helper.TryParseInt(ddlStatus.SelectedValue, 0);

                DataTable ds = null;

                ds = blc_user.RowsNewsLetter(1, int.MaxValue, txtSearchText.Text.Trim(), intStatus, 3, ddlGender.SelectedValue);

                DataTable dt = new DataTable();
                if (ds.Rows.Count > 0)
                {
                    dt = ds;
                    foreach (DataRow item in ds.Rows)
                    {
                        UpdateNewsLetter_chuaxuat(Utility.TryParseLong(item["NewLetterID"], 0), 1);
                    }
                    EventColumnRemove(dt, "NewLetterID");
                    EventColumnRemove(dt, "Status");
                    EventColumnRemove(dt, "AddByUserID");
                    EventColumnRemove(dt, "ModifiedByUserID");
                    //dt = BindDataTable(dt);

                    EventColumn(dt, "NUM", "STT");
                    EventColumn(dt, "Email", "Email");
                    EventColumn(dt, "Gender", "Giới tính");
                    EventColumn(dt, "AddDate", "Ngày nhập");
                    Utility.Export(string.Format("Newsletter_{0}.xls", DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")), dt);

                }
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            
        }

        #endregion

        #region Property

        protected string StrStatus(object p_value)
        {
            if (p_value!=null)
            {
                int intPvaule = Helper.TryParseInt(p_value.ToString(), 3);
                if (intPvaule == 0)
                {
                    return "Chưa xuất";
                }
                if (intPvaule == 1)
                {
                    return "Đã xuất";
                }
                return string.Empty;
            }
            return string.Empty;
        }
        #endregion

    }
}