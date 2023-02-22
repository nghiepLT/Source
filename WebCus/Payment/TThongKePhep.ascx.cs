
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
using System.ComponentModel;

namespace WebCus
{
    public partial class TThongKePhep : CommonUserControl
    {

        int findex, lindex;
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_BLC blc_users = new UserMng_BLC();
        PagedDataSource pgsource = new PagedDataSource();
        Image sortImage = new Image();
        public string SortDireaction
        {
            get
            {
                if (ViewState["SortDireaction"] == null)
                    return string.Empty;
                else
                    return ViewState["SortDireaction"].ToString();
            }
            set
            {
                ViewState["SortDireaction"] = value;
            }
        }
        private string _sortDirection;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnSave.Visible = false;
                this.SortOption_S = 1;
                this.SortType_S = 1;

                txtDateFrom.SelectedDate = DateTime.Now.AddDays(-Utilis.TryParseInt(Config.GetConfigValue("Days_View_Order")));
                txtDateTo.SelectedDate = DateTime.Now.AddDays(+Utilis.TryParseInt(30));
                rad_tungay.SelectedDate = DateTime.Now.AddDays(+Utilis.TryParseInt(3));
                rad_denngay.SelectedDate = DateTime.Now.AddDays(+Utilis.TryParseInt(3));
                rad_ngay.SelectedDate = DateTime.Now;
                rad_ngaynghibuoi.SelectedDate = DateTime.Now;

                rad_ttFormDate.SelectedDate = DateTime.Now;
                rad_ttToDate.SelectedDate = DateTime.Now;

              //  BindGirdView();
                tbttkt.Visible = false;
                tblData.Visible = false;
                dstonghop.Visible = false;
                dr_dspb.Visible = false;
                txtSearch.Visible = false;
                tbl_ctcontrol.Visible = false;
                tbl_Tongcontrl.Visible = false;

                Literal1.Text = DateTimeFormatInfo.CurrentInfo.GetDayName(DateTime.Now.DayOfWeek) + " Ngày " + DateTime.Now.ToString("dd/MM/yyyy ");

                th_buoi.Visible = false;
                td_buoi.Visible = false;
                th_songay.Visible = true;
                td_Ngaynghi.Visible = true;
                th_ngay.Visible = false;
                td_ngay.Visible = false;

                IList<PhongBan> dt = blc_user.ListPhongban_byCTY(Utils.TryParseInt(dr_cty.SelectedValue, 0));

                //DataTable tsb = ConvertToDataTable(dt);
                //tsb.Columns.Add(new DataColumn("Title", System.Type.GetType("System.String"), "TenPhong + ' - ' 1" ));
                //dr_dspb.DataSource = tsb;
                //dr_dspb.DataTextField = "Title";
                //dr_dspb.DataTextField = "TenPhong" +"-"+ string.Format("{0}","TrucThuoc" == "1" ? "NK":"CN");
                dr_dspb.Items.Clear();
                dr_dspb.DataSource = dt;
                dr_dspb.DataTextField = "TenPhong";
                dr_dspb.DataValueField = "IDPhong";
                dr_dspb.DataBind();
                dr_dspb.AppendDataBoundItems = true;
                dr_dspb.Items.Insert(0, new ListItem("--Chọn Phòng Ban--", "-1"));
                dr_dspb.SelectedIndex = 0;
            }
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

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
            tblData.Visible = true;
            int idcty = this.UserMemberLike;
            int iduserchoose = this.UserMemberID;
            int PageInt = 50;
           // int songaynghi = 0;
            int idnhomduyet = -1;
            tbttkt.Visible = false;
            int trangthai = Utilis.TryParseInt(drop_trangthai.SelectedValue);
            int loaiphep = Utilis.TryParseInt(drop_loaipheps.SelectedValue);
            int loaisearch = Utilis.TryParseInt(dropSearchtype.SelectedValue);
            int idtrPhong = blc_user.GetIDTruongPhong_byIDuser(this.UserID);
            int idPhoPhong = blc_user.GetIDPhoPhong_byIDuser(this.UserID);
            int idtrNhom = blc_user.GetIDTruongNhom_byIDuser(this.UserID);
            int idparentPhongban = this.UserMemberParentID;
          
            DateTime fromDate = txtDateFrom.SelectedDate;
            DateTime toDate = txtDateTo.SelectedDate;
            if (this.UserMemberType == 1 || this.UserMemberType == 2)
            {
                idcty = -1;
                iduserchoose = -1;
                idtrPhong = -1;
                idPhoPhong = -1;
                idtrNhom = -1;
             //   songaynghi = 2;
                idparentPhongban = -1;
            }
            if (this.UserMemberType == 3)
            {
                iduserchoose = -1;
                idtrPhong = -1;
                idPhoPhong = -1;
                idtrNhom = -1;
              //  songaynghi = 2;
                idparentPhongban = -1;
               // idnhomduyet = 1;
            }
            if (this.UserMemberType == 4)
            {

                idPhoPhong = -1;
                idtrNhom = -1;
            }
            if (this.UserMemberType == 5)
            {
                idtrNhom = -1;
            }
            string KEYID = "";
            PhongBan sp = blc_user.GetPhongBan_ByID(idparentPhongban);
            if (sp != null && !string.IsNullOrEmpty(sp.KeyID))
            {
                KEYID = sp.KeyID;
                if (KEYID == "NK-HCNS" || KEYID == "CN-HCNS")
                {
                    if (this.UserMemberType != 4)
                    {
                        idcty = -1;
                       
                    }
                    iduserchoose = -1;
                    idtrPhong = -1;
                    idPhoPhong = -1;
                    idtrNhom = -1;
                    idparentPhongban = Utils.TryParseInt(dr_dspb.SelectedValue, -1);
                  //  div_button_active.Visible = false;
                }
            }
            if (Utils.TryParseInt(dr_dspb.SelectedValue, -1) != -1)
            {
                idcty =  Utils.TryParseInt(dr_cty.SelectedValue,-1);
                idparentPhongban = Utils.TryParseInt(dr_dspb.SelectedValue, -1);
            }
            
            DataSet ds = blc_user.Casework_Rows(this.CurrentPage, int.MaxValue, fromDate, toDate, trangthai, loaiphep, loaisearch, txtSearch.Text.Trim().ToUpper(), iduserchoose, idparentPhongban, idnhomduyet, idtrPhong, idPhoPhong, idtrNhom, idcty);
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
               // pgsource.PageSize = PageInt;
                pgsource.CurrentPageIndex = PageNumber;
                ViewState["totpage"] = pgsource.PageCount;
                lnkPrevious.Enabled = !pgsource.IsFirstPage;
                lnkNext.Enabled = !pgsource.IsLastPage;
                lnkFirst.Enabled = !pgsource.IsFirstPage;
                lnkLast.Enabled = !pgsource.IsLastPage;
               
                gvList.DataSource = pgsource;
                gvList.DataBind();
                doPaging();

                

                int total = Utilis.TryParseInt(ds.Tables[0].Rows.Count);

                int chuaxl = blc_user.Casework_Rows(this.CurrentPage, int.MaxValue, fromDate, toDate, 0, loaiphep, loaisearch, txtSearch.Text.Trim(), iduserchoose, idparentPhongban, idnhomduyet, idtrPhong, idPhoPhong, idtrNhom, idcty).Tables[0].Rows.Count;
                lbl_Total_Count.Text = string.Format("Có <span style='color:Blue;'> {0} </span> phép đã tìm thấy | Có <span style='color:Red;'> {1} </span> phép chưa duyệt", total, chuaxl);

            }
            else
            {
                gvList.DataSource = null;
                gvList.DataBind();
                //  PQTPager1.RecordCount = 0;
                lbl_Total_Count.Text = "";
            }

        }

        private void BindAllGirdTongHop()
        {
            dstonghop.Visible = true;
            DateTime fromDate = rad_ttFormDate.SelectedDate;
            DateTime toDate = rad_ttToDate.SelectedDate;
            DataTable dts = blc_user.ThongkePhep_Rows(fromDate, toDate);
            Gr_dstonghop.DataSource = dts;
            Gr_dstonghop.DataBind();
        }
        protected string Bind_Payment_Type(object p_status)
        {
            return ((PaymentType)Utilis.TryParseInt(p_status)).ToString().Replace("_", " ");
        }

        protected string Bind_Transaction_Type(object p_status)
        {
            return ((Transaction_Status)Utilis.TryParseInt(p_status)).ToString().Replace("_", " ");
        }

        private void Update_Status(string status_trans)
        {

            try
            {
                if (status_trans == "0")//xóa
                {
                    if (blc_user.DeleteBill_byID(this.BillPhepID))
                    {
                        Alert.Show("Xóa thành công");
                        BindGirdView();
                    }
                    else Alert.Show("Xóa Thất Bại !");
                }
                if (status_trans == "1")//duyet
                {
                  
                    if (blc_user.UpdateBill(this.BillPhepID, 1, txt_ghichunghu.Text, this.UserMemberID))
                    {
                        Alert.Show("Success !");
                        BindGirdView();
                    }
                    else Alert.Show("Phép Chưa Được Xác Nhận Hoặc Quyền Giới Hạn !");
                }
                if (status_trans == "2")//khong duyet
                {
                    if (blc_user.UpdateBill(this.BillPhepID, 2, txt_ghichunghu.Text, this.UserMemberID))
                    {
                        Alert.Show("Success !");
                        BindGirdView();
                    }
                    else Alert.Show("Error!");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region Send Mail

        public static bool Send_Mail(string subject, string sContent, string emailTo)
        {
            bool bFlag = false;
            try
            {
                string AdminEmail = Config.GetConfigValue("AdminEmailTo");
                string AdminPass = Config.GetConfigValue("AdminPass");
                string MailHost = Config.GetConfigValue("MailHost");
                string PortMailHost = Config.GetConfigValue("PortMailHost");
                int intPort = Helper.TryParseInt(PortMailHost, 25);
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
                SmtpServer.Port = intPort;
                SmtpServer.Host = MailHost;
                if (intPort == 25)
                    SmtpServer.EnableSsl = false;
                else
                    SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage myMessage = new MailMessage();
                myMessage.Subject = subject;
                myMessage.Body = sContent;
                myMessage.From = new MailAddress(AdminEmail);
                myMessage.To.Add(new MailAddress(emailTo));
                myMessage.IsBodyHtml = true;

                SmtpServer.Send(myMessage);
                bFlag = true;
            }
            catch (Exception ex)
            {
                bFlag = false;
            }
            return bFlag;
        }

        #endregion
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

        protected void gvList_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SortItem")
            {
                LinkButton linkSort = e.CommandSource as LinkButton;
                this.ActiveColumn_S = linkSort.ID;
                this.ActiveCss_S = linkSort.CssClass != "Active_Down" ? "Active_Down" : "Active_Up";
                string[] arr = e.CommandArgument.ToString().Split('-');
                this.SortOption_S = Convert.ToInt32(arr[0]);
                this.SortType_S = this.ActiveCss_S == "Active_Down" ? 1 : 2;
                BindGirdView();
            }
            if (e.CommandName == "capnhatphep")
            {
                if (this.UserMemberType != 7)
                {
                    this.BillPhepID = Convert.ToInt32(e.CommandArgument);
                    BindKeyInfo();
                }
                else { MessageBox.Show("Quyền Giới Hạn !", false, "/listaccept"); }
            }
        }
        private void BindKeyInfo()
        {
            try
            {
                btnInsert.Visible = false;
                btnSave.Visible = false;
                NgayPhep keys = new NgayPhep();
                keys = blc_user.GetNgayPhep_ByID(this.BillPhepID);
               
                if (keys != null)
                {
                    lbltennv.Text = GetNameNhanVien(keys.IDNhanvien);
                    lbl_nghinamnv.Text = blc_user.GetNgayPhepNam_ByIDNV(Utilis.TryParseInt(keys.IDNhanvien)).ToString();
                    lbl_nghithangnv.Text = blc_user.GetNgayPhepThang_ByIDNV(Utilis.TryParseInt(keys.IDNhanvien)).ToString();
                    lbl_ditrenv.Text = blc_user.GetDitreThang_ByIDNV(Utilis.TryParseInt(keys.IDNhanvien)).ToString();
                    lbl_vesomnv.Text = blc_user.GetVesomThang_ByIDNV(Utilis.TryParseInt(keys.IDNhanvien)).ToString();
                    lbl_congtacngoainv.Text = blc_user.GetCongtacngoaiThang_ByIDNV(Utilis.TryParseInt(keys.IDNhanvien)).ToString();
                    int songay = Utils.TryParseInt(keys.SoNgayNghi, 0);
                    //  Alert.Show(songay.ToString());
                    if (this.UserMemberType != 1 && this.UserMemberType != 2 && this.UserMemberType != 3)
                    {
                        if (keys.TrangThaiPhep == 1 || keys.TrangThaiPhep == 2)
                        {
                            MessageBox.Show("Phép Này Đã Được Xữ Lý !", false, "/listaccept");
                        }
                    }
                    if (keys.IDNhanvien == this.UserID)
                    {
                        MessageBox.Show("Bạn Không Được Phép!", false, "/listaccept");
                    }
                    tbttkt.Visible = true;
                    int tinhtrang = Utils.TryParseInt(keys.TrangThaiPhep, 0);
                    txt_songaynghi.Text = keys.SoNgayNghi.ToString();
                    rad_tungay.SelectedDate = DateTime.Parse(keys.TuNgay.ToString());
                    rad_denngay.SelectedDate = DateTime.Parse(keys.DenNgay.ToString());
                    ddlmaphep.SelectedValue = keys.LoaiPhep.ToString();

                    txtlydonghi.Text = keys.LyDoVang;
                    txt_ghichunghu.Text = keys.GhiChu;
                    rad_tungay.Enabled = false;
                    rad_denngay.Enabled = false;
                    //  txt_ghichunghu.Enabled = false;
                    txt_songaynghi.Enabled = false;
                    txtlydonghi.Enabled = false;
                    ddlmaphep.Enabled = false;
                    rad_ngaynghibuoi.Enabled = false;
                    dr_buoinghi.Enabled = false;
                    rad_ngay.Enabled = false;
                    if (keys.LoaiPhep == 1 || keys.LoaiPhep == 6)
                    {
                        th_buoi.Visible = true;
                        td_buoi.Visible = true;
                        th_songay.Visible = false;
                        td_Ngaynghi.Visible = false;
                        th_ngay.Visible = false;
                        td_ngay.Visible = false;
                        dr_buoinghi.SelectedValue = keys.BuoiNghi.Trim();
                        rad_ngaynghibuoi.SelectedDate = DateTime.Parse(keys.TuNgay.ToString());

                    }
                    if (keys.LoaiPhep == 2)
                    {
                        th_buoi.Visible = false;
                        td_buoi.Visible = false;
                        th_songay.Visible = true;
                        td_Ngaynghi.Visible = true;
                        th_ngay.Visible = false;
                        td_ngay.Visible = false;
                    }
                    else if (keys.LoaiPhep == 3 || keys.LoaiPhep == 4 || keys.LoaiPhep == 5)
                    {
                        th_buoi.Visible = false;
                        td_buoi.Visible = false;
                        th_songay.Visible = false;
                        td_Ngaynghi.Visible = false;
                        th_ngay.Visible = true;
                        td_ngay.Visible = true;
                        rad_ngay.SelectedDate = DateTime.Parse(keys.TuNgay.ToString());
                    }
                }
                else Response.Redirect("/listaccept");

            }
            catch (System.Exception ex)
            {
                Alert.Show(ex.ToString());
            }
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
            DateTime fromDate = txtDateFrom.SelectedDate;

            DateTime toDate = txtDateTo.SelectedDate;
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
        protected void btnSearchTotal_Click(object sender, EventArgs e)
        {
            DateTime fromDate = rad_ttFormDate.SelectedDate;

            DateTime toDate = rad_ttToDate.SelectedDate;
            if (fromDate > toDate)
            {
                Alert.Show("Khoảng Thời gian chọn không phù hơp !");
                rad_ttFormDate.Focus();

            }
            else
            {
                BindAllGirdTongHop();
            }
        }

        protected void btnUpdate_Status_Click(object sender, EventArgs e)
        {

            Update_Status(((Button)sender).CommandArgument);

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

        protected void Click_upload(object sender, EventArgs e)
        {
            if (IsPostBack && Upload.HasFile)
            {
                if (Path.GetExtension(Upload.FileName).Equals(".xlsx"))
                {
                    var excel = new ExcelPackage(Upload.FileContent);

                    var dt = excel.ToDataTable();
                    var table = "Contacts";
                    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

                    // using (var conn = new SqlConnection("Server=.;Database=test;Integrated Security=SSPI"))
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        var bulkCopy = new SqlBulkCopy(conn);
                        bulkCopy.DestinationTableName = table;
                        conn.Open();
                        var schema = conn.GetSchema("Columns", new[] { null, null, table, null });
                        foreach (DataColumn sourceColumn in dt.Columns)
                        {
                            foreach (DataRow row in schema.Rows)
                            {
                                if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
                                {
                                    bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                                    break;
                                }
                            }
                        }
                        bulkCopy.WriteToServer(dt);
                    }
                }
                string script = "alert(\"Complete!\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            tbttkt.Visible = true;
            btnSave.Visible = true;
            btnInsert.Visible = false;
            div_button_active.Visible = false;
            resettext();
        }

        protected void resettext()
        {
            rad_tungay.Enabled = true;
            rad_denngay.Enabled = true;
            txt_ghichunghu.Enabled = true;
            txt_songaynghi.Enabled = true;
            txtlydonghi.Enabled = true;
            ddlmaphep.Enabled = true;


        }
        protected void bindetexbox()
        {

        }
        protected void hidetext()
        {

        }
        protected void btn_refrestData(object sender, EventArgs e)
        {
            Response.Redirect("/listaccept");

        }
        protected void btn_rebackData(object sender, EventArgs e)
        {
            Response.Redirect("/thongkengayphep.aspx");

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int nguoithaythe = 0;
            string buoinghi = "";
            DateTime ngay = rad_tungay.SelectedDate;
            DateTime denngay = rad_denngay.SelectedDate;
            string songay = txt_songaynghi.Text.Trim();
            if (ddlmaphep.SelectedValue == "1")
            {
                ngay = rad_ngaynghibuoi.SelectedDate;
                songay = "0.5";
                buoinghi = dr_buoinghi.SelectedValue;
            }
            if (ddlmaphep.SelectedValue == "3" || ddlmaphep.SelectedValue == "4" || ddlmaphep.SelectedValue == "5" || ddlmaphep.SelectedValue == "6")
            {
                ngay = rad_ngay.SelectedDate;
                denngay = rad_ngay.SelectedDate;
                songay = "0";
            }
            int flag = 1;
            if (blc_user.Createbill(Utils.TryParseInt(ddlmaphep.SelectedValue, 0), songay, ngay, denngay, txtlydonghi.Text.Trim(), txt_ghichunghu.Text.Trim(), this.UserMemberID, nguoithaythe, flag, buoinghi, 0))
            {
                flag = 0;
                MessageBox.Show("Sussess!", false, "/listaccept");

            }
            else Alert.Show("Error! Phép đã được tạo");
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


        protected void btnDeleteKey(object sender, EventArgs e)
        {
            try
            {

                if (blc_user.DeleteBill_byID(this.BillPhepID))
                {

                    Alert.Show("Xóa thành công");
                    BindGirdView();
                }
            }
            catch (System.Exception ex)
            {
                Alert.Show("Xóa thất bại" + ex.Message);

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
                dr_cty.Visible = true;

            }
            if (dropSearchtype.SelectedValue == "2")
            {
                txtSearch.Visible = true;
                dr_dspb.Visible = false;
                dr_cty.Visible = false;
                //   dr_cty.SelectedIndex = 0;
                //  dr_dspb.SelectedIndex = 0;

            }
        }

        protected void ddlmaphep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlmaphep.SelectedValue == "1")
            {
                th_buoi.Visible = true;
                td_buoi.Visible = true;
                th_songay.Visible = false;
                td_Ngaynghi.Visible = false;
                th_ngay.Visible = false;
                td_ngay.Visible = false;
            }
            if (ddlmaphep.SelectedValue == "2")
            {
                th_buoi.Visible = false;
                td_buoi.Visible = false;
                th_songay.Visible = true;
                td_Ngaynghi.Visible = true;
                th_ngay.Visible = false;
                td_ngay.Visible = false;
            }
            if (ddlmaphep.SelectedValue == "6")
            {
                th_buoi.Visible = true;
                td_buoi.Visible = true;
                th_songay.Visible = false;
                td_Ngaynghi.Visible = false;
                th_ngay.Visible = false;
                td_ngay.Visible = false;
            }
            else if (ddlmaphep.SelectedValue == "3" || ddlmaphep.SelectedValue == "4" || ddlmaphep.SelectedValue == "5")
            {
                th_buoi.Visible = false;
                td_buoi.Visible = false;
                th_songay.Visible = false;
                td_Ngaynghi.Visible = false;
                th_ngay.Visible = true;
                td_ngay.Visible = true;
            }
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

        protected void dr_cty_SelectedIndexChanged(object sender, EventArgs e)
        {

            IList<PhongBan> dt = blc_user.ListPhongban_byCTY(Utils.TryParseInt(dr_cty.SelectedValue, 0));

            //DataTable tsb = ConvertToDataTable(dt);
            //tsb.Columns.Add(new DataColumn("Title", System.Type.GetType("System.String"), "TenPhong + ' - ' 1" ));
            //dr_dspb.DataSource = tsb;
            // dr_dspb.DataTextField = "Title";
            //  dr_dspb.DataTextField = "TenPhong" +"-"+ string.Format("{0}","TrucThuoc" == "1" ? "NK":"CN");
            dr_dspb.Items.Clear();
            dr_dspb.DataSource = dt;
            dr_dspb.DataTextField = "TenPhong";
            dr_dspb.DataValueField = "IDPhong";
            dr_dspb.DataBind();
            dr_dspb.AppendDataBoundItems = true;
            dr_dspb.Items.Insert(0, new ListItem("--Chọn Phòng Ban--", "-1"));
            dr_dspb.SelectedIndex = 0;
        }

        protected void dr_dspb_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGirdView();
        }

        protected void rd_2_CheckedChanged(object sender, EventArgs e)
        {
            tbl_ctcontrol.Visible = false;
            tbl_Tongcontrl.Visible = true;
            tblData.Visible = false;
        }

        protected void rd_1_CheckedChanged(object sender, EventArgs e)
        {
            tbl_ctcontrol.Visible = true;
            tbl_Tongcontrl.Visible = false;
            dstonghop.Visible = false;
        }

        protected void Gr_dstonghop_Sorting(object sender, GridViewSortEventArgs e)
        {
            SetSortDirection(SortDireaction);
            dstonghop.Visible = true;
            DateTime fromDate = rad_ttFormDate.SelectedDate;
            DateTime toDate = rad_ttToDate.SelectedDate;
            DataTable dts = blc_user.ThongkePhep_Rows(fromDate, toDate);
            
            if (dts != null)
            {
               
                //Sort the data.
                dts.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                Gr_dstonghop.DataSource = dts;
                Gr_dstonghop.DataBind();
                SortDireaction = _sortDirection;
                int columnIndex = 0;
                foreach (DataControlFieldHeaderCell headerCell in Gr_dstonghop.HeaderRow.Cells)
                {
                    if (headerCell.ContainingField.SortExpression == e.SortExpression)
                    {
                        columnIndex = Gr_dstonghop.HeaderRow.Cells.GetCellIndex(headerCell);
                    }
                }
               // BindAllGirdTongHop();
                Gr_dstonghop.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
            }
        }
        protected void SetSortDirection(string sortDirection)
        {
            if (sortDirection == "ASC")
            {
                _sortDirection = "DESC";
                sortImage.ImageUrl = "view_sort_ascending.png";

            }
            else
            {
                _sortDirection = "ASC";
                sortImage.ImageUrl = "view_sort_descending.png";
            }
        }
        //OleDbConnection oledbConn;
        //private DataTable GenerateExcelData(string SlnoAbbreviation)
        //{
        //    DataTable data = null;
        //    //try
        //    //{
        //        string filename = Path.GetFileName(filesUpload1.FileName);
        //        filesUpload1.SaveAs(Server.MapPath("~/") + filename);
        //        string path = System.IO.Path.GetFullPath(Server.MapPath("~/" + filename));
        //        // need to pass relative path after deploying on server
        //        //  string path = System.IO.Path.GetFullPath(Server.MapPath("~/InformationNew.xlsx"));
        //        /* connection string  to work with excel file. HDR=Yes - indicates 
        //           that the first row contains columnnames, not data. HDR=No - indicates 
        //           the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
        //           (numbers, dates, strings etc) data columns as text. 
        //        Note that this option might affect excel sheet write access negative. */

        //        if (Path.GetExtension(path) == ".xls")
        //        {
        //            oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
        //        }
        //        else if (Path.GetExtension(path) == ".xlsx")
        //        {
        //            oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
        //        }
        //        oledbConn.Open();
        //        OleDbCommand cmd = new OleDbCommand();
        //        OleDbDataAdapter oleda = new OleDbDataAdapter();
        //        DataSet ds = new DataSet();

        //        // passing list to drop-down list

        //        // selecting distict list of Slno 
        //        cmd.Connection = oledbConn;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "SELECT distinct([Name]) FROM [Sheet1$]";
        //        oleda = new OleDbDataAdapter(cmd);
        //        oleda.Fill(ds, "dsdiem");
        //        //  ddlSlno.DataSource = ds.Tables["dsSlno"].DefaultView;
        //        //if (!IsPostBack)
        //        //{
        //        //    ddlSlno.DataTextField = "Name";
        //        //    ddlSlno.DataValueField = "Name";
        //        //    ddlSlno.DataBind();
        //        //}
        //        // by default we will show form data for all states but if any state is selected then show data accordingly
        //        if (!String.IsNullOrEmpty(SlnoAbbreviation) && SlnoAbbreviation != "Select")
        //        {
        //            cmd.CommandText = "SELECT [Name], [Poin]" +
        //                "  FROM [Sheet1$] ";
        //            // cmd.Parameters.AddWithValue("@Slno_Abbreviation", SlnoAbbreviation);
        //        }
        //        else
        //        {
        //            cmd.CommandText = "SELECT [Name], [Poin] FROM [Sheet1$]";
        //        }
        //        oleda = new OleDbDataAdapter(cmd);
        //        oleda.Fill(ds);

        //        // binding form data with grid view
        //        data = ds.Tables[1].DataSet.Tables[1];

        //    //}
        //    // need to catch possible exceptions
        //    //catch (Exception ex)
        //    //{
        //    //    Alert.Show(ex.ToString());
        //    //}
        //   //finally
        //   // {
        //        oledbConn.Close();
        //   // }
        //    return data;
        //}

    }
}
