
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.Controls;
using PQT.API;
using System.Data;
using PQT.Common;
using System.Net.Mail;
//using ProductMng.BLC;
using UserMng.BLC;
using System.IO;
using System.Data.SqlClient;
using DocumentFormat.OpenXml;
using ExcelStream;
using OfficeOpenXml;
using System.Configuration;
using PQT.DAC;
using System.Web.Services;
using UserMng.DataDefine;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Globalization;
using System.Threading;
using Telerik.WebControls;

namespace WebCus
{
    public partial class historycheckinout : CommonUserControl
    {

        int findex, lindex;
        
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_BLC blc_users = new UserMng_BLC();
        PagedDataSource pgsource = new PagedDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Action = "/inoutcheckhistory";               
                this.SortOption_S = 1;
                this.SortType_S = 1;
               // txt_tungay.Text = DateTime.Now.AddDays(-Utilis.TryParseInt(Config.GetConfigValue("Days_View_Order"))).ToString();

                txtDateFrom.SelectedDate = DateTime.Now;// DateTime.Now.AddDays(-Utilis.TryParseInt(Config.GetConfigValue("Days_View_Order")));
                txtDateTo.SelectedDate = DateTime.Now;//DateTime.Now.AddDays(+Utilis.TryParseInt(30));
                //rad_tungay.SelectedDate = DateTime.Now.AddDays(+Utilis.TryParseInt(3)); 

                dr_dspb.Visible = false;
                txtSearch.Visible = false;
                IList<PhongBan> dt = blc_user.ListPhongban();
                dr_dspb.DataSource = dt;
                dr_dspb.DataTextField = "TenPhong";
                dr_dspb.DataValueField = "IDPhong";
                dr_dspb.DataBind();
                dr_dspb.AppendDataBoundItems = true;
                dr_dspb.Items.Insert(0, new ListItem("--Chọn Phòng Ban--", "-1"));
                dr_dspb.SelectedIndex = 0;
                Literal1.Text = DateTimeFormatInfo.CurrentInfo.GetDayName(DateTime.Now.DayOfWeek) + " Ngày " + DateTime.Now.ToString("dd/MM/yyyy ");                               
                BindGirdView();               
                if (this.UserMemberType == 2 || this.UserMemberType == 1 || this.UserMemberType == 3 && this.UserMemberParentID == -1) { td_searchname.Visible = true; }
                
            }
          
        }        
        protected string removeduplicate(string str)
        {

            string assd = "";
            char[] arrI = str.Distinct().ToArray();
            for (int i = 0; i < arrI.Length; i++)
            {
                assd += arrI[i].ToString() + "|";

            }
            return assd;
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

        private void BindGirdView()
        {

            int idcty = -1;// this.UserMemberLike;
            int iduserchoose = this.UserMemberID;
            int PageInt = 50;            
            int trangthai = Utilis.TryParseInt(drop_trangthai.SelectedValue);
            int loaiphep = Utilis.TryParseInt(drop_loaipheps.SelectedValue);
            int loaisearh=Utilis.TryParseInt(dropSearchtype.SelectedValue);
            int idtrPhong = blc_user.GetIDTruongPhong_byIDuser(this.UserID);
            int idPhoPhong = blc_user.GetIDPhoPhong_byIDuser(this.UserID);
            int idtrNhom = blc_user.GetIDTruongNhom_byIDuser(this.UserID);
            int idparentPhongban = this.UserMemberParentID;

            DateTime fromDate = txtDateFrom.SelectedDate;// Convert.ToDateTime(txt_tungay.Text);
           // Alert.Show(fromDate.ToString());
            DateTime toDate =  txtDateTo.SelectedDate;//Convert.ToDateTime(txt_denngay.Text);
            if (this.UserMemberType == 1 || this.UserMemberType == 2)
            {
                idcty = -1;
                iduserchoose = -1;
                div_ngaynghithang.Visible = false;
            }
            if (this.UserMemberType == 3)
            {
                iduserchoose = -1;
                div_ngaynghithang.Visible = false;
              
            }
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                div_ngaynghithang.Visible = true;
            }
            DateTime ngaydauthang = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
           // DateTime tungay = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" +"01");
           // DataSet dss = blc_user.Casework_Rows_byID(1, int.MaxValue, ngaydauthang, ngaydauthang.AddMonths(1).AddDays(-1), -1, 2, -1, "", this.UserMemberID, this.UserMemberLike, this.UserMemberType);
            DataTable dss = blc_user.Rows_Checkinout_byname(ngaydauthang, ngaydauthang.AddMonths(1).AddDays(-1), 2, txtSearch.Text);

            DataSet dsnv = blc_user.NhanVien_Rows_byID_modify(1, int.MaxValue,2,1);
             int totalnghi = 0;
             int totalrangoai = 0;
             string manvra = "";
             string manvnghi = "";
            DataSet ds = blc_user.CheckInOut_Rows_byID(this.CurrentPage, int.MaxValue, fromDate, toDate, trangthai, loaiphep, loaisearh, txtSearch.Text.Trim().ToUpper(), iduserchoose, idcty, this.UserMemberType);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["Status"].ToString() == "1" || dr["Status"].ToString() == "2" || dr["Status"].ToString() == "3" || dr["Status"].ToString() == "4")
                {
                    if (!manvra.Equals(dr["BarCodeUser"].ToString()) && string.IsNullOrEmpty(dr["TimesIn"].ToString()))
                    { totalrangoai = totalrangoai + 1;
                    manvra = dr["BarCodeUser"].ToString();
                    }
                }
                if (dr["Status"].ToString() == "5" || dr["Status"].ToString() == "6" || dr["Status"].ToString() == "7" || dr["Status"].ToString() == "8")
                {
                    if (!manvnghi.Equals(dr["BarCodeUser"].ToString()))
                    { totalnghi = totalnghi + 1;
                    manvnghi = dr["BarCodeUser"].ToString();
                    }
                }
            }           


            int total = Utilis.TryParseInt(ds.Tables[0].Rows.Count);
            string totalthang = dss.Rows.Count.ToString();
            lbl_Total_Count.Text = string.Format("Có <span style='color:Blue;'> {0} </span> lượt đã tìm thấy", total);

            lbl_thang.Text = DateTime.Now.Month.ToString();
            
            lbl_tungay.Text = fromDate.ToShortDateString();
            lbl_denngay.Text = toDate.ToShortDateString();
            lbl_songaynghitim.Text = total.ToString();
            int conlai = 0;
            conlai = dsnv.Tables[0].Rows.Count - (totalnghi + totalrangoai);
           // lblngaynghi.Text = blc_user.GetNgayPhepNam_ByIDNV(Utilis.TryParseInt(iduserchoose)).ToString();//ngaynghi.ToString();
            lbl_sngaynghi.Text = total.ToString();
            lbt_totalnhanvien.Text = string.Format("Tổng nhân viên : <span style='color:Blue;'> {0} </span> | Ra ngoài hôm nay: <span style='color:Red;'> {1} </span>| Nghĩ hôm nay : <span style='color:Red;'> {2} </span> | Còn lại : <span style='color:Red;'> {3} </span>", dsnv.Tables[0].Rows.Count, totalrangoai, totalnghi, conlai);

            this.tongngaynghitrongthang = Utils.TryParseInt(totalthang, 0);
            
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                int cout = Utils.TryParseInt(ds.Tables.Count, 0);

                

                System.Data.DataView dv = new System.Data.DataView(dt);
                pgsource.DataSource = dv;
                pgsource.AllowPaging = true;
                if (dt.Rows.Count > PageInt)
                {
                    pgsource.PageSize = PageInt;
                }
                else pgsource.PageSize = dt.Rows.Count;
                pgsource.CurrentPageIndex = PageNumber;
                ViewState["totpage"] = pgsource.PageCount;
                lnkPrevious.Enabled = !pgsource.IsFirstPage;
                lnkNext.Enabled = !pgsource.IsLastPage;
                lnkFirst.Enabled = !pgsource.IsFirstPage;
                lnkLast.Enabled = !pgsource.IsLastPage;
                gvList.DataSource = pgsource;
                gvList.DataBind();
                doPaging();
     
            }
            else
            {
                gvList.DataSource = null;
                gvList.DataBind();
                //  PQTPager1.RecordCount = 0;
                lbl_Total_Count.Text = "";
            }

        }

        protected string Bind_Payment_Type(object p_status)
        {
            return ((PaymentType)Utilis.TryParseInt(p_status)).ToString().Replace("_", " ");
        }

        protected string Bind_Transaction_Type(object p_status)
        {
            return ((Transaction_Status)Utilis.TryParseInt(p_status)).ToString().Replace("_", " ");
        }       

        
        public int numpage()
        {
            int page = this.CurrentPage;
            int pagesize = 10;

            if (page > 1)
                return ((Convert.ToInt16(page) * pagesize - pagesize) > 0) ? (Convert.ToInt16(page) * pagesize - pagesize) : 0;
            return 0;
        }

        protected void Pager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.CurrentPage = e.NewPageIndex + 1;
            BindGirdView();
        }          
        
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                LinkButton linkSort = e.Row.FindControl(this.ActiveColumn_S) as LinkButton;
                if (linkSort != null)
                {
                    linkSort.CssClass = this.ActiveCss_S;
                }
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime fromDate =txtDateFrom.SelectedDate;

            DateTime toDate =txtDateTo.SelectedDate;
            if (fromDate > toDate)
            {
                Alert.Show("Khoảng Thời gian chọn không phù hơp !");
                txtDateTo.Focus();

            }
            else
            {
               
                BindGirdView();
            }
        }


        #region Property STMS

        protected string ActiveColumn_S
        {
            get
            {
                if (ViewState["g_ActiveColumn_S"] != null)
                    return Convert.ToString(ViewState["g_ActiveColumn_S"]);
                return "";
            }
            set
            {
                ViewState["g_ActiveColumn_S"] = value;
            }
        }

        protected string ActiveCss_S
        {
            get
            {
                if (ViewState["g_ActiveCss_S"] != null)
                    return Convert.ToString(ViewState["g_ActiveCss_S"]);
                return "";
            }
            set
            {
                ViewState["g_ActiveCss_S"] = value;
            }
        }


        protected int SortType_S
        {
            get
            {
                if (ViewState["g_SortType_S"] != null)
                    return Convert.ToInt32(ViewState["g_SortType_S"]);
                return 1;
            }
            set
            {
                ViewState["g_SortType_S"] = value;
            }
        }

        protected int SortOption_S
        {
            get
            {
                if (ViewState["g_SortOption_S"] != null)
                    return Convert.ToInt32(ViewState["g_SortOption_S"]);
                return 1;
            }
            set
            {
                ViewState["g_SortOption_S"] = value;
            }
        }

        #endregion

        protected void bindetexbox()
        {

        }
        protected void hidetext()
        {

        }
        protected void btn_refrestData(object sender, EventArgs e)
        {
            Response.Redirect("/inoutcheckhistory");

        }

        protected void btnhuy_Click(object sender, EventArgs e)
        {
            Response.Redirect("inoutcheckhistory");
        }
       
       
        public int UserMemberID
        {
            get
            {
                if (Session["g_UserMemberID"] != null)
                    return Convert.ToInt32(Session["g_UserMemberID"]);
                return 0;
            }
            set
            {
                Session["g_UserMemberID"] = value;
            }
        }
        public int secsionchose
        {
            get
            {
                if (Session["g_secsionchose"] != null)
                    return Convert.ToInt32(Session["g_secsionchose"]);
                return 0;
            }
            set
            {
                Session["g_secsionchose"] = value;
            }
        }
        public int UserMemberType
        {
            get
            {
                if (Session["g_UserMemberType"] != null)
                    return Convert.ToInt32(Session["g_UserMemberType"]);
                return 0;
            }
            set
            {
                Session["g_UserMemberType"] = value;
            }
        }
        protected string g_UserMemberName
        {
            get
            {
                if (Session["g_UserMemberName"] != null)
                    return Session["g_UserMemberName"].ToString();
                return string.Empty;
            }
            set
            {
                Session["g_UserMemberName"] = value;
            }
        }
        protected string UserMemberLoginID
        {
            get
            {
                if (Session["g_UserMemberLoginID"] != null)
                    return Session["g_UserMemberLoginID"].ToString();
                return string.Empty;
            }
            set
            {
                Session["g_UserMemberLoginID"] = value;
            }
        }
        public int UserMemberLike
        {
            get
            {
                if (Session["g_UserMemberLike"] != null)
                    return Convert.ToInt32(Session["g_UserMemberLike"]);
                return -1;
            }
            set
            {
                Session["g_UserMemberLike"] = value;
            }
        }


        protected int BillPhepID
        {
            get
            {
                if (ViewState["g_BillID"] != null)
                    return Convert.ToInt32(ViewState["g_BillID"]);
                return -1;
            }
            set
            {
                ViewState["g_BillID"] = value;
            }
        }
                

        private void doPaging()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            findex = PageNumber - 5;
            if (PageNumber > 5)
            {
                lindex = PageNumber + 5;
            }
            else
            {
                lindex = 10;
            }
            if (lindex > Convert.ToInt32(ViewState["totpage"]))
            {
                lindex = Convert.ToInt32(ViewState["totpage"]);
                findex = lindex - 10;
            }

            if (findex < 0)
            {
                findex = 0;
            }
            for (int i = findex; i < lindex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPages.DataSource = dt;

            rptPages.DataBind();

            if (dt.Rows.Count > 1)

                pager_div.Visible = true;

            else
                pager_div.Visible = false;



        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            PageNumber -= 1;

            BindGirdView();
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            PageNumber = 0;
            BindGirdView();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            PageNumber = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindGirdView();
        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            PageNumber += 1;


            BindGirdView();
        }
        protected void rptPages_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {

            PageNumber = Convert.ToInt32(e.CommandArgument);

            BindGirdView();

        }
        protected void RepeaterPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnkPage = (LinkButton)e.Item.FindControl("btnPage");
            if (lnkPage.CommandArgument.ToString() == PageNumber.ToString())
            {
                lnkPage.Enabled = false;
                lnkPage.BackColor = System.Drawing.Color.FromName("#FFCC01");
            }
        }
        public int PageNumber
        {

            get
            {

                if (ViewState["PageNumber"] != null)

                    return Convert.ToInt32(ViewState["PageNumber"]);

                else

                    return 0;

            }

            set
            {

                ViewState["PageNumber"] = value;

            }

        }

        protected void dropSearchtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropSearchtype.SelectedValue == "1")
            {
                dr_dspb.Visible = true;
                txtSearch.Visible = false;
            }
            if (dropSearchtype.SelectedValue == "2")
            {
                txtSearch.Visible = true;
                dr_dspb.Visible = false;
            }
        }        
        protected string checkdisplay(object idnhanvien)
        {
            if (Utilis.TryParseInt(idnhanvien) == this.UserID)
                return "";
            else
            return "none";
        }        

        public string GetNameNhanVien(object IDUSer)
        {
            int iduser = Utils.TryParseInt(IDUSer.ToString(), 0);

            TUser us = blc_user.GetUser_ByIDAll(iduser);
            if (us != null)
            {
                return us.UserName;
            }
            else return "---";
        }
        public string Gettime(object tu, object den)
        {

            if (!string.IsNullOrEmpty(tu.ToString()) && !string.IsNullOrEmpty(den.ToString()))
            {
                return "Từ : " + tu.ToString() + " - đến : " + den.ToString();
            }
            else if (!string.IsNullOrEmpty(tu.ToString()) && string.IsNullOrEmpty(den.ToString()))
            {
                return " <span style='color:blue;'> Ra : " + tu.ToString() + "</span> - <span style='color:red;'> Chưa vào </span>";
            }
            else return "";
        }

        public int UserMemberParentID
        {
            get
            {
                if (Session["g_UserMemberParentID"] != null)
                    return Convert.ToInt32(Session["g_UserMemberParentID"]);
                return -1;
            }
            set
            {
                Session["g_UserMemberParentID"] = value;
            }
        }
        public int tongngaynghitrongthang
        {
            get
            {
                if (Session["tongngaynghitrongthang"] != null)
                    return Convert.ToInt32(Session["tongngaynghitrongthang"]);
                return 0;
            }
            set
            {
                Session["tongngaynghitrongthang"] = value;
            }
        }
        
    }
}
