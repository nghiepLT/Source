
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
using PQT.API.DataDefine.Sys;
using PQT.API.File;
using ClosedXML.Excel;


namespace WebCus
{
    public partial class TTNhanvien : CommonUserControl
    {

        int findex, lindex;
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_BLC blc_users = new UserMng_BLC();
        PagedDataSource pgsource = new PagedDataSource();
        DataTable dataexport = new DataTable();
        OleDbConnection oledbConn;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDateFrom.SelectedDate = DateTime.Now.AddDays(-Utilis.TryParseInt(Config.GetConfigValue("Days_View_Order")));
                txtDateTo.SelectedDate = DateTime.Now;
                Literal1.Text = DateTimeFormatInfo.CurrentInfo.GetDayName(DateTime.Now.DayOfWeek) + " Ngày " + DateTime.Now.ToString("dd/MM/yyyy ");

                IList<NguonTuyenDung> dts = blc_user.ListNguonTuyenDung();
                dr_nguontuyendung.DataSource = dts;
                dr_nguontuyendung.DataTextField = "NameNTD";
                dr_nguontuyendung.DataValueField = "Id";
                dr_nguontuyendung.DataBind();
                dr_nguontuyendung.AppendDataBoundItems = true;
                dr_nguontuyendung.Items.Insert(0, new ListItem("--Chọn Nguồn Tuyển Dụng--", "-1"));
                dr_nguontuyendung.SelectedIndex = 0;
                //  Alert.Show(refullname("Nguyễn Hà Hoàng Nhân"));
                btnSave.Visible = false;
                this.SortOption_S = 1;
                this.SortType_S = 1;               
                Bidphongban(this.IDCty);
                BindGirdView();

            }

        }

        private static readonly string[] VietnameseSigns = new string[]

    {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

    };



        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }
        public static string refullname(string fullname)
        {

            //string[] names = fullName.Split(' ');
            //string name = names.First();
            //string midlename = names.First() ;
            //string lasName = names.Last();
            //Alert.Show("Tên :" + lasName + "_Họ" + name+ "_Đệm" + midlename);

            string fistchar = "";
            string name = fullname.Substring(fullname.LastIndexOf(' ') + 1);
            // string firstname = fullName.Substring(0,fullName.IndexOf(' '));
            // string midlename = fullName.Substring(fullName.IndexOf(' '),fullName.Length-name.Length);
            //   string midlename = fullName.Remove(fullName.Length - name.Length);
            string[] names = fullname.Split(' ');
            for (int i = 0; i < names.Length - 1; i++)
            {
                fistchar += names[i].Substring(0, 1);
            }
            //fullName = fullName.Remove(fullName.Length - lastid.Length);

            return RemoveSign4VietnameseString(name).ToLower() + fistchar.ToLower();
        }
        protected void Bidphongban(int idcty)
        {
            dropSearchtype.SelectedIndex = 0;
            dr_dspb.Visible = false;
            txtSearch.Visible = false;
            dr_dspb.Items.Clear();
            IList<PhongBan> dt = blc_user.ListPhongban_byCTY(idcty);
            dr_dspb.DataSource = dt;
            dr_dspb.DataTextField = "TenPhong";
            dr_dspb.DataValueField = "IDPhong";
            dr_dspb.DataBind();
            dr_dspb.AppendDataBoundItems = true;
            dr_dspb.Items.Insert(0, new ListItem("--Chọn Phòng Ban--", "-1"));
            dr_dspb.SelectedIndex = 0;
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

            int idcty = this.UserMemberLike;
            int iduserchoose = this.UserMemberID;
            int PageInt = 50;
            tbttkt.Visible = false;
            int tructhuocCty = Utilis.TryParseInt(drop_tructhuoc.SelectedValue);
            int loaiNV = Utilis.TryParseInt(drop_loainv.SelectedValue);
            int loaisearh = Utilis.TryParseInt(dropSearchtype.SelectedValue);


            DateTime fromDate = txtDateFrom.SelectedDate;
            DateTime toDate = txtDateTo.SelectedDate;
            if (this.UserMemberType == 1 || this.UserMemberType == 2)
            {
                idcty = -1;
                iduserchoose = -1;

            }
            if (this.UserMemberType == 3)
            {
                iduserchoose = -1;

            }
            DataSet ds = blc_user.NhanVien_Rows_byID(this.CurrentPage, int.MaxValue, fromDate, toDate, tructhuocCty, loaiNV, loaisearh, txtSearch.Text.Trim(), iduserchoose, Utils.TryParseInt(dr_dspb.SelectedValue, 0));
            dataexport = ds.Tables[0];
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                int cout = Utils.TryParseInt(ds.Tables.Count, 0);


                System.Data.DataView dv = new System.Data.DataView(dt);
                pgsource.DataSource = dv;
                pgsource.AllowPaging = true;
                if (dt.Rows.Count > PageInt)
                { pgsource.PageSize = PageInt; }
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
                int total = Utilis.TryParseInt(ds.Tables[0].Rows.Count);
                lbl_Total_Count.Text = string.Format("Có <span style='color:Blue;'> {0} </span>Nhân viên ", total);

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

        private void Update_Status(string status_trans)
        {

            try
            {

                if (status_trans == "0")//xóa
                {
                    if (blc_user.DeleteNhanvien_byID(this.IDNhanVien))
                    {
                        Alert.Show("Xóa thành công");
                        BindGirdView();
                    }
                    else Alert.Show("Xóa Thất Bại !");
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
        protected void gvList_RowCommand3(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "capnhatqtlv")
            {
                this.IDQTLV_NhanVien = Convert.ToInt32(e.CommandArgument);
                BindInfoKyLuatkhenthuong(this.IDQTLV_NhanVien);
                BindGirdViewQuatrinhlamviec();
            }
            if (e.CommandName == "DeleteQtlv")
            {
                this.IDQTLV_NhanVien = Convert.ToInt32(e.CommandArgument);
                blc_user.DeleteQTLV_byID(this.IDQTLV_NhanVien);
                this.IDQTLV_NhanVien = -1;
                Alert.Show("Sucess !");
                BindGirdViewQuatrinhlamviec();
            }


        }
        protected void gvList_RowCommand2(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "capnhatnguoithan")
            {

                this.IDNT_NhanVien = Convert.ToInt32(e.CommandArgument);
                BindInfoNguoithan(this.IDNT_NhanVien);
                BindGirdViewNguoiThan();
            }
            if (e.CommandName == "Deletenguoithan")
            {

                this.IDNT_NhanVien = Convert.ToInt32(e.CommandArgument);
                blc_user.DeleteNguoiThan_byID(this.IDNT_NhanVien);
                this.IDNT_NhanVien = -1;
                Alert.Show("Sucess !");
                BindGirdViewNguoiThan();
            }

        }

        protected void BindInfoNguoithan(int idnguoithan)
        {

            ThongTinNguoiThan ents = blc_user.GetTTNguoithan_byID(idnguoithan);
            if (ents != null)
            {
                btn_capnhanguoithan.Text = "Cập Nhật";
                Image_nguoithan.ImageUrl = GetUSerImagePath(Utils.TryParseLong(ents.Image, 0));
                BindImage_NguoiThan(Utils.TryParseLong(ents.Image, 0));
                txt_diachiNt.Text = ents.DiaChiNguoiThan;
                txt_ghichunt.Text = ents.Ghichu;
                dr_gioitinhnguoithan.SelectedValue = ents.Gioitinh.ToString();

                txt_moiquanhe.Text = ents.MoiQuanHe;
                rad_ngaysinhnt.SelectedDate = (DateTime)ents.NgaySinh;
                txt_ngheNghiepnt.Text = ents.NgheNghiep;
                txt_quequannt.Text = ents.Quequan;
                txt_soDTnt.Text = ents.SoDTNguoiThan;
                txt_hotennguoithan.Text = ents.TenNguoiThan;
            }
        }
        protected void BindInfoKyLuatkhenthuong(int idklkt)
        {

            ThongTinKyLuatKhenThuong ents = blc_user.GetTTKyluatkhenthuong_byID(idklkt);
            if (ents != null)
            {
                Button_capnhatkl.Text = "Cập Nhật";
                rad_ngaythuchien.SelectedDate = (DateTime)ents.NgayThucHien;
                dr_loaikytluatkhenthuong.SelectedValue = ents.Loai.ToString();
                txt_lyDokhenthuong.Text = ents.LyDo;
                txt_ghichukhenthuong.Text = ents.GhiChu;

            }
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
            if (e.CommandName == "capnhat")
            {

                this.IDNhanVien = Convert.ToInt32(e.CommandArgument);
                BindKeyInfo();

            }
        }
        public string GetUSerImagePath(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);
            if (fileEnt != null)
                return string.Format("/{0}{1}", Config.GetConfigValue("UserImagePath").Replace("\\", "/"), fileEnt.ServerFileName);
            return string.Empty;
        }
        protected string GetUSerImageUrl(object p_fileID)
        {
            try
            {
                string str = this.GetImageUrl(Convert.ToInt64(p_fileID), "UserImagePath", ImageSizeType.Big);
                if (System.IO.File.Exists(MapPath("~" + str)))
                {
                    return str;
                }
                return "/Images/noimage.png";
            }
            catch (System.Exception)
            {
                return "/Images/noimage.png";
            }
        }
        private void BindImage(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);
            if (fileEnt != null)
            {
                lblImage.Text = fileEnt.RealFileName;
            }
            else
            {
                lblImage.Text = string.Empty;
            }
        }
        private void BindImage_NguoiThan(long p_fileID)
        {
            FileManager fileMng = new FileManager();
            CommonFileEntity fileEnt = fileMng.RowCommonFile(p_fileID);
            if (fileEnt != null)
            {
                lbl_Imagettngthan.Text = fileEnt.RealFileName;
            }
            else
            {
                lbl_Imagettngthan.Text = string.Empty;
            }
        }
        private void BindKeyInfo()
        {
            try
            {
                div_button_active.Visible = true;
                btnInsert.Visible = true;
                btnSave.Visible = false;
                tbttUngVien.Visible = false;
                tbttNhansu.Visible = false;
                tbttNhanvien.Visible = true;
                tablettnt.Visible = false;
                tableqtlv.Visible = false;
                NhanVien ent = new NhanVien();
                ent = blc_user.GetNhanvien_byID(this.IDNhanVien);

                if (ent != null)
                {
                    tbttkt.Visible = true;
                    imgProduct.ImageUrl = GetUSerImagePath(Utils.TryParseLong(ent.Image, 0));
                    BindImage(Utils.TryParseLong(ent.Image, 0));
                    txt_chuyenmon.Text = ent.ChuyenMon;
                    txt_socmnd.Text = ent.CMND;
                    txt_dctamtru.Text = ent.DCTamTru;
                    txt_dcthuongtru.Text = ent.DCThuongTru;
                    txt_email.Text = ent.Email;
                    txt_ghichunhanvien.Text = ent.GhiChuNV;
                    dr_gioitinh.SelectedValue = ent.GioiTinh;
                    txt_hoten.Text = ent.HoTen;
                    txt_kinhnghiem.Text = ent.KinhNghiem;
                    txt_masothue.Text = ent.MSThue;
                    rad_ngaycapcmnd.SelectedDate = (DateTime)ent.NgayCMND;
                    rad_ngaysinh.SelectedDate = (DateTime)ent.NgaySinh;

                    txt_nguyenQuan.Text = ent.NguyenQuan;
                    txt_noisinh.Text = ent.NoiSinh;
                    txt_sdt.Text = ent.SoDt;
                    txtsotknganhang.Text = ent.SoTkNganhang;
                    txt_tknganhang.Text = ent.TkNganHang;
                    dr_trinhdonhanvien.SelectedValue = ent.Trinhdo;
                    txt_dantoc.Text = ent.Dantoc;
                    txt_tongiao.Text = ent.Tongiao;
                    txt_tinhtranghonnhan.Text = ent.Tinhtranghonnhan;
                    txt_noicapcmnd.Text = ent.NoiCapCMND;

                }
                else MessageBox.Show("Nhân viên không tồn tại", false, "/nhanvien");


            }
            catch (System.Exception ex)
            {

            }
        }
        protected void gr_dsnguoithan_RowDataBound(object sender, GridViewRowEventArgs e)
        { }
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
            tbttNhanvien.Visible = true;
            tbttUngVien.Visible = false;
            tbttNhansu.Visible = false;
            tablettnt.Visible = false;
            tableqtlv.Visible = false;

            this.IDNhanVien = -1;
            resettext();
        }

        protected void resettext()
        {

            txt_chuyenmon.Text = string.Empty;
            txt_socmnd.Text.Trim();
            txt_dctamtru.Text = string.Empty;
            txt_dcthuongtru.Text = string.Empty;
            txt_email.Text = string.Empty;
            txt_ghichunhanvien.Text = string.Empty;
            dr_gioitinh.SelectedIndex = 0;
            txt_hoten.Text = string.Empty;

            txt_kinhnghiem.Text = string.Empty;
            txt_masothue.Text = string.Empty;
            rad_ngaycapcmnd.SelectedDate = DateTime.Now;
            rad_ngaysinh.SelectedDate = DateTime.Now;
            txt_nguyenQuan.Text = string.Empty;
            txt_noisinh.Text = string.Empty;
            txt_sdt.Text = string.Empty;
            txtsotknganhang.Text = string.Empty;
            txt_tknganhang.Text = string.Empty;
            dr_trinhdonhanvien.SelectedIndex = 0;
        }


        protected void btn_refrestData(object sender, EventArgs e)
        {
            Response.Redirect("/nhanvien");

        }
        protected void btncnnv_Click(object sender, EventArgs e)
        {

            tbttNhanvien.Visible = true;
            tbttUngVien.Visible = false;
            tbttNhansu.Visible = false;
            tablettnt.Visible = false;
            tableqtlv.Visible = false;
        }
        protected void btncnuv_Click(object sender, EventArgs e)
        {

            tbttNhanvien.Visible = false;
            tbttUngVien.Visible = true;
            tbttNhansu.Visible = false;
            tablettnt.Visible = false;
            tableqtlv.Visible = false;
            ThongTinTuyenDung ent = blc_user.GetTTTuyendung_byID(this.IDNhanVien);
            if (ent != null)
            {
                dr_chapnhancv.SelectedValue = ent.ChapNhanCV.ToString();
                dr_goilai.SelectedValue = ent.CoTheGoiLai.ToString();
                txt_ghichugoilai.Text = ent.GhiChuGoiLai;
                txt_ghichuphongvan.Text = ent.GhiChuPV;
                dr_kql1.SelectedValue = ent.KetQuaPVL1.ToString();
                dr_kql2.SelectedValue = ent.KetQuaPVL2.ToString();
                dr_kql3.SelectedValue = ent.KetQuaPVL3.ToString();
                dr_kqlai.SelectedValue = ent.KetQuaPVLai.ToString();
                rad_ngayduyeths.SelectedDate = (DateTime)ent.NgayDuyetHS;
                rad_ngaypvl1.SelectedDate = (DateTime)ent.NgayPVL1;
                rad_ngaypvl2.SelectedDate = (DateTime)ent.NgayPVL2;
                rad_ngaypvl3.SelectedDate = (DateTime)ent.NgayPVL3;
                rad_ngaypvlai.SelectedDate = (DateTime)ent.NgayPVLai;
                rad_ngayvaolam.SelectedDate = (DateTime)ent.NgayVaoLam;
                dr_nguontuyendung.SelectedValue = ent.NguonTuyenDung.ToString();
                txt_vitrituyendung.Text = ent.ViTriTuyenDung;
            }
            else
            {
                dr_chapnhancv.SelectedIndex = 0;
                dr_goilai.SelectedIndex = 0;
                txt_ghichugoilai.Text = string.Empty;
                txt_ghichuphongvan.Text = string.Empty;
                dr_kql1.SelectedIndex = 0;
                dr_kql2.SelectedIndex = 0;
                dr_kql3.SelectedIndex = 0;
                dr_kqlai.SelectedIndex = 0;
                rad_ngayduyeths.SelectedDate = DateTime.Now;
                rad_ngaypvl1.SelectedDate = DateTime.Now;
                rad_ngaypvl2.SelectedDate = DateTime.Now;
                rad_ngaypvl3.SelectedDate = DateTime.Now;
                rad_ngaypvlai.SelectedDate = DateTime.Now;
                rad_ngayvaolam.SelectedDate = DateTime.Now;
                dr_nguontuyendung.SelectedIndex = 0;
                txt_vitrituyendung.Text = string.Empty;
            }
        }
        protected void btncnns_Click(object sender, EventArgs e)
        {

            tbttNhanvien.Visible = false;
            tbttUngVien.Visible = false;
            tbttNhansu.Visible = true;
            tablettnt.Visible = false;
            tableqtlv.Visible = false;
            DateTime ngayvaolam = DateTime.Parse("27 / 12 / 2014");
            TimeSpan thamnien = DateTime.Now - ngayvaolam;
            // txt_thamnien.Text = thamnien.TotalDays.ToString();
            TimeSpan t = new TimeSpan();
            DateTime ngayhientai = DateTime.Now, ngayvaolamlai = new DateTime(2010, 3, 10);
            DateTime ngaytamnghi = new DateTime(2010, 2, 10), ngaykyhd = new DateTime(2009, 1, 11);
            t = ngayhientai - ngayvaolamlai;
            DateTime d5 = new DateTime(Math.Abs(t.Ticks));
            t = ngaytamnghi - ngaykyhd;
            DateTime d6 = new DateTime(Math.Abs(t.Ticks));

            int ngay, thang, nam;
            nam = (d5.Year + d6.Year) - 2;
            thang = (d5.Month + d6.Month) - 2;
            ngay = (d5.Day + d6.Day) - 2;
            if (ngay >= 30)
            {
                ngay = ngay - 30;
                thang = thang + 1;
            }
            if (thang >= 12)
            {
                thang = thang - 12;
                nam = nam + 1;
            }

            txt_thamnien.Text = nam + " năm " + thang + " tháng " + ngay + " ngày";
            ThongTinNhanSu ents = blc_user.GetTTNhansu_byID(this.IDNhanVien);
            if (ents != null)
            {
                dr_tructhuoccty.SelectedValue = ents.IDCTY.ToString();
                dr_phongban.Items.Clear();
                IList<PhongBan> phong = blc_user.ListPhongban_byCTY(Utils.TryParseInt(ents.IDCTY, 0));
                dr_phongban.DataSource = phong;
                dr_phongban.DataTextField = "TenPhong";
                dr_phongban.DataValueField = "IDPhong";                
                dr_phongban.DataBind();
                dr_phongban.AppendDataBoundItems = true;
                dr_phongban.Items.Insert(0, new ListItem("--Chọn Phòng Ban--", "0"));
               
                txtsosobh.Text = ents.IDSoBH;
                txt_manv.Text = ents.MaNV;
                if (!string.IsNullOrEmpty(ents.NgayKyHD.ToString()))
                { rad_ngaykyhd.SelectedDate = (DateTime)ents.NgayKyHD; }
                if (!string.IsNullOrEmpty(ents.NgayVaoLam.ToString()))
                {
                rad_ngayvaonhanviec.SelectedDate = (DateTime)ents.NgayVaoLam;}
                dr_phongban.SelectedValue = ents.PhongBan.ToString().Trim();
                txt_thamnien.Text = ents.ThamNien;
                txtngaybhcc.Text = ents.ThangBHCC;
                txtngaybhtn.Text = ents.ThangBHTN;
                txtngaybhxh.Text = ents.ThangBHXH;
                txtngaybhyt.Text = ents.ThangBHYT;
                dr_vitri.SelectedValue = ents.ViTri.ToString();
                dr_loainv.SelectedValue = Utils.TryParseInt(ents.LoaiNV, 0).ToString();
            }
            else
            {
                dr_tructhuoccty.SelectedIndex = 0;
                dr_phongban.Items.Clear();
                txtsosobh.Text = string.Empty;
                txt_manv.Text = string.Empty;
                rad_ngaykyhd.SelectedDate = DateTime.Now;
                rad_ngayvaonhanviec.SelectedDate = DateTime.Now;
                txt_thamnien.Text = string.Empty;
                txtngaybhcc.Text = string.Empty;
                txtngaybhtn.Text = string.Empty;
                txtngaybhxh.Text = string.Empty;
                txtngaybhyt.Text = string.Empty;
                dr_vitri.SelectedIndex = 0;
                dr_loainv.SelectedIndex = 0;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //     DateTime ngay = rad_tungay.SelectedDate;
            //     DateTime denngay = rad_denngay.SelectedDate;
            if (tbttNhanvien.Visible == true)
            {
                if (AddNhanVien())
                {
                    Alert.Show("Lưu thành công");
                    BindGirdView();
                }
                else Alert.Show("Error !");
            }
            else tbttNhanvien.Visible = true;
        }
        protected void btnSaveUV_Click(object sender, EventArgs e)
        {


            if (AddUngVien())
            {
                Alert.Show("Lưu thành công");
                BindGirdView();
            }
            else Alert.Show("Error !");

        }

        protected void btnSaveNS_Click(object sender, EventArgs e)
        {


            if (AddNhansu())
            {
                Alert.Show("Lưu thành công");
                BindGirdView();
            }
            else Alert.Show("Error !");

        }
        protected void btncnnt_Click(object sender, EventArgs e)
        {


            tbttNhanvien.Visible = false;
            tbttUngVien.Visible = false;
            tbttNhansu.Visible = false;
            tablettnt.Visible = true;
            tableqtlv.Visible = false;

            BindGirdViewNguoiThan();
            //if (tbttNhanvien.Visible == true)
            //{
            //    if (AddNhanVien())
            //    {
            //        Alert.Show("Lưu thành công");
            //        BindGirdView();
            //    }
            //    else Alert.Show("Error !");
            //}
            //else tbttNhanvien.Visible = true;
        }
        protected void BindGirdViewNguoiThan()
        {
            IList<ThongTinNguoiThan> dsnt = blc_user.ListNguoithan_byNV(IDNhanVien);
            gr_dsnguoithan.DataSource = dsnt;
            gr_dsnguoithan.DataBind();
        }

        protected void BindGirdViewQuatrinhlamviec()
        {
            IList<ThongTinKyLuatKhenThuong> dsnt = blc_user.ListThongtinKyLuat_byNV(IDNhanVien);
            gr_kyluatkhenthuong.DataSource = dsnt;
            gr_kyluatkhenthuong.DataBind();
        }
        protected void btnSaveNT_Click(object sender, EventArgs e)
        {
            if (AddNguoiThan())
            {
                Alert.Show("Lưu thành công");
                BindGirdViewNguoiThan();
            }
            else Alert.Show("Error !");

        }
        protected void btnaddNT_Click(object sender, EventArgs e)
        {
            this.IDNT_NhanVien = -1;
            txt_diachiNt.Text = string.Empty;
            txt_ghichunt.Text = string.Empty;
            dr_gioitinhnguoithan.SelectedIndex = 0;

            txt_moiquanhe.Text = string.Empty;
            rad_ngaysinhnt.SelectedDate = DateTime.Now;
            txt_ngheNghiepnt.Text = string.Empty;
            txt_quequannt.Text = string.Empty;
            txt_soDTnt.Text = string.Empty;
            txt_hotennguoithan.Text = string.Empty;
            btn_capnhanguoithan.Text = "Lưu";

        }
        protected void btnAdd_qtlv_Click(object sender, EventArgs e)
        {
            this.IDQTLV_NhanVien = -1;
            rad_ngaythuchien.SelectedDate = DateTime.Now;
            dr_loaikytluatkhenthuong.SelectedIndex = 0;
            txt_lyDokhenthuong.Text = string.Empty;
            txt_ghichukhenthuong.Text = string.Empty;
            Button_capnhatkl.Text = "Lưu";
        }
        protected void btnSaveQTLV_Click(object sender, EventArgs e)
        {


            if (AddQTLV())
            {
                Alert.Show("Success !");
                BindGirdViewQuatrinhlamviec();
            }
            else Alert.Show("Error !");

        }
        protected void btncnqtlv_Click(object sender, EventArgs e)
        {


            tbttNhanvien.Visible = false;
            tbttUngVien.Visible = false;
            tbttNhansu.Visible = false;
            tablettnt.Visible = false;
            tableqtlv.Visible = true;
            BindGirdViewQuatrinhlamviec();

        }
        private bool AddNhanVien()
        {
            FileManager fileMng = new FileManager();

            //  string pathLogo = Server.MapPath(Config.GetConfigValue("ProductLogoImagePath"));
            string filePath = System.IO.Path.Combine(Server.MapPath("."), Config.GetConfigValue("UserImagePath")).TrimEnd('\\');
            CommonFileEntity fileEnt = null;
            string[] arrImageSize = Config.GetConfigValue("UserImageSize").Split(',');
            int swidth = Convert.ToInt32(arrImageSize[0]);
            int sheight = Convert.ToInt32(arrImageSize[1]);
            int mwidth = Convert.ToInt32(arrImageSize[2]);
            int mheight = Convert.ToInt32(arrImageSize[3]);
            int width = Convert.ToInt32(arrImageSize[4]);
            int height = Convert.ToInt32(arrImageSize[5]);

            //string[] arrImageSizeL = Config.GetConfigValue("ProductLogoImageSize").Split(',');
            //int swidthL = Convert.ToInt32(arrImageSizeL[0]);
            //int sheightL = Convert.ToInt32(arrImageSizeL[1]);
            //int mwidthL = Convert.ToInt32(arrImageSizeL[2]);
            //int mheightL = Convert.ToInt32(arrImageSizeL[3]);
            //int widthL = Convert.ToInt32(arrImageSizeL[4]);
            //int heightL = Convert.ToInt32(arrImageSizeL[5]);
            fileEnt = fileMng.UploadImageFile(fileImage, filePath, swidth, sheight);


            // fileEnt = fileMng.UploadImageFile(fileImage, filePath, swidth, sheight, mwidth, mheight, width, height, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));
            //  fileEnt = fileMng.UploadImageFile_With_Logo(fileImage, filePath, swidth, sheight, mwidth, mheight, width, height, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"), pathLogo, swidthL, sheightL, mwidthL, mheightL, widthL, heightL);
            NhanVien ent = null;
            if (this.IDNhanVien == -1)
            {
                ent = new NhanVien();
                ent.Image = fileEnt != null ? fileEnt.CommonFileID : 0;
            }

            else
            {
                ent = blc_user.GetNhanvien_byID(this.IDNhanVien);
                if (fileEnt != null)
                {
                    fileMng.DeleteCommonFile(Utils.TryParseLong(ent.Image, 0), true);
                    ent.Image = fileEnt.CommonFileID;
                }
            }
            ent.AnhDaiDien = fileImage.FileName;
            ent.ChuyenMon = txt_chuyenmon.Text;
            ent.CMND = txt_socmnd.Text.Trim();
            ent.DCTamTru = txt_dctamtru.Text;
            ent.DCThuongTru = txt_dcthuongtru.Text;
            ent.Email = txt_email.Text;
            ent.GhiChuNV = txt_ghichunhanvien.Text;
            ent.GioiTinh = dr_gioitinh.SelectedValue;
            ent.HoTen = txt_hoten.Text;
            ent.IDNguoiTao = this.UserMemberID;
            ent.KinhNghiem = txt_kinhnghiem.Text;
            ent.MSThue = txt_masothue.Text;
            ent.NgayCapNhat = null;
            ent.NgayCMND = rad_ngaycapcmnd.SelectedDate;
            ent.NgaySinh = rad_ngaysinh.SelectedDate;
            ent.NguoiPhuThuoc = "";
            ent.NguyenQuan = txt_nguyenQuan.Text;
            ent.NoiSinh = txt_noisinh.Text;
            ent.SoDt = txt_sdt.Text.Trim().Replace(".", "");
            ent.SoTkNganhang = txtsotknganhang.Text;
            ent.TkNganHang = txt_tknganhang.Text;
            ent.Trinhdo = dr_trinhdonhanvien.SelectedValue;
            ent.Dantoc = txt_dantoc.Text;
            ent.Tongiao = txt_tongiao.Text;
            ent.Tinhtranghonnhan = txt_tinhtranghonnhan.Text;
            ent.NoiCapCMND = txt_noicapcmnd.Text;
            DataSet ds = blc_user.NhanVien_Rows_byID(this.CurrentPage, int.MaxValue, DateTime.Now.AddYears(-1), DateTime.Now, -1, -1, -1, txtSearch.Text.Trim(), -1, Utils.TryParseInt(dr_dspb.SelectedValue, 0));
            ent.SttNhanVien = ds.Tables[0].Rows.Count + 1;

            if (this.IDNhanVien == -1)
            {
                ent.NgayTao = DateTime.Now;
                this.IDNhanVien = blc_user.CreateNhanVien(ent);

                //MessageBox.Show("Sussess!", false, "/listcase");
                //return true;
            }
            else
            {
                ent.NgayCapNhat = DateTime.Now;
                blc_user.UpdateNhanVien(ent);
            }

            try
            {

                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
        private bool AddUngVien()
        {



            ThongTinTuyenDung ent = blc_user.GetTTTuyendung_byID(this.IDNhanVien);
            ThongTinTuyenDung ents = new ThongTinTuyenDung();
            ents.ChapNhanCV = Utils.TryParseInt(dr_chapnhancv.SelectedValue, 0);
            ents.CoTheGoiLai = Utils.TryParseInt(dr_goilai.SelectedValue, 0);
            ents.GhiChuGoiLai = txt_ghichugoilai.Text;
            ents.GhiChuPV = txt_ghichuphongvan.Text;
            ents.IDCapNhat = this.UserMemberID;
            ents.IdNhanvien = this.IDNhanVien;
            // ent.KetQua=
            ents.KetQuaPVL1 = Utils.TryParseInt(dr_kql1.SelectedValue, 0);
            ents.KetQuaPVL2 = Utils.TryParseInt(dr_kql2.SelectedValue, 0);
            ents.KetQuaPVL3 = Utils.TryParseInt(dr_kql3.SelectedValue, 0);
            ents.KetQuaPVLai = Utils.TryParseInt(dr_kqlai.SelectedValue, 0);
            ents.NgayDuyetHS = rad_ngayduyeths.SelectedDate;
            ents.NgayPVL1 = rad_ngaypvl1.SelectedDate;
            ents.NgayPVL2 = rad_ngaypvl2.SelectedDate;
            ents.NgayPVL3 = rad_ngaypvl3.SelectedDate;
            ents.NgayPVLai = rad_ngaypvlai.SelectedDate;
            ents.NgayVaoLam = rad_ngayvaolam.SelectedDate;
            ents.NguonTuyenDung = Utils.TryParseInt(dr_nguontuyendung.SelectedValue, 0);
            ents.ViTriTuyenDung = txt_vitrituyendung.Text;

            if (ent == null)
            {

                ents.NgayTao = DateTime.Now;
                blc_user.CreateTTUngvien(ents);
            }

            if (ent != null)
            {
                ent.ChapNhanCV = Utils.TryParseInt(dr_chapnhancv.SelectedValue, 0);
                ent.CoTheGoiLai = Utils.TryParseInt(dr_goilai.SelectedValue, 0);
                ent.GhiChuGoiLai = txt_ghichugoilai.Text;
                ent.GhiChuPV = txt_ghichuphongvan.Text;
                ent.IDCapNhat = this.UserMemberID;
                ent.IdNhanvien = this.IDNhanVien;
                // ent.KetQua=
                ent.KetQuaPVL1 = Utils.TryParseInt(dr_kql1.SelectedValue, 0);
                ent.KetQuaPVL2 = Utils.TryParseInt(dr_kql2.SelectedValue, 0);
                ent.KetQuaPVL3 = Utils.TryParseInt(dr_kql3.SelectedValue, 0);
                ent.KetQuaPVLai = Utils.TryParseInt(dr_kqlai.SelectedValue, 0);
                ent.NgayDuyetHS = rad_ngayduyeths.SelectedDate;
                ent.NgayPVL1 = rad_ngaypvl1.SelectedDate;
                ent.NgayPVL2 = rad_ngaypvl2.SelectedDate;
                ent.NgayPVL3 = rad_ngaypvl3.SelectedDate;
                ent.NgayPVLai = rad_ngaypvlai.SelectedDate;
                ent.NgayVaoLam = rad_ngayvaolam.SelectedDate;
                ent.NguonTuyenDung = Utils.TryParseInt(dr_nguontuyendung.SelectedValue, 0);
                ent.ViTriTuyenDung = txt_vitrituyendung.Text;
                ent.NgayCapNhat = DateTime.Now;
                blc_user.UpdateTTUngVien(ent);

            }


            try
            {

                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
        private bool AddNhansu()
        {


            ThongTinNhanSu ents = new ThongTinNhanSu();
            ents.Idcapnhat = this.UserMemberID;
            ents.IDCTY = Utils.TryParseInt(dr_tructhuoccty.SelectedValue, 0);
            ents.IdNhanVien = this.IDNhanVien;
            ents.IDSoBH = txtsosobh.Text;
            ents.MaNV = txt_manv.Text;
            ents.NgayKyHD = rad_ngaykyhd.SelectedDate;
            ents.NgayVaoLam = rad_ngayvaonhanviec.SelectedDate;
            ents.PhongBan = Utils.TryParseInt(dr_phongban.SelectedValue, 0);
            ents.ThamNien = txt_thamnien.Text;
            ents.ThangBHCC = txtngaybhcc.Text;
            ents.ThangBHTN = txtngaybhtn.Text;
            ents.ThangBHXH = txtngaybhxh.Text;
            ents.ThangBHYT = txtngaybhyt.Text;
            ents.ViTri = Utils.TryParseInt(dr_vitri.SelectedValue, 0);
            ents.LoaiNV = Utils.TryParseInt(dr_loainv.SelectedValue, 0);
            ThongTinNhanSu ent = blc_user.GetTTNhansu_byID(this.IDNhanVien);
            if (ent == null)
            {
                ents.NgayTao = DateTime.Now;
                blc_user.CreateTTNhanSu(ents);
            }

            if (ent != null)
            {
                ent.Idcapnhat = this.UserMemberID;
                ent.IDCTY = Utils.TryParseInt(dr_tructhuoccty.SelectedValue, 0);
                ent.IdNhanVien = this.IDNhanVien;
                ent.IDSoBH = txtsosobh.Text;
                ent.MaNV = txt_manv.Text;
                ent.NgayKyHD = rad_ngaykyhd.SelectedDate;
                ent.NgayVaoLam = rad_ngayvaonhanviec.SelectedDate;
                ent.PhongBan = Utils.TryParseInt(dr_phongban.SelectedValue, 0);
                ent.ThamNien = txt_thamnien.Text;
                ent.ThangBHCC = txtngaybhcc.Text;
                ent.ThangBHTN = txtngaybhtn.Text;
                ent.ThangBHXH = txtngaybhxh.Text;
                ent.ThangBHYT = txtngaybhyt.Text;
                ent.ViTri = Utils.TryParseInt(dr_vitri.SelectedValue, 0);
                ent.NgayCapNhat = DateTime.Now;
                ent.LoaiNV = Utils.TryParseInt(dr_loainv.SelectedValue, 0);
                blc_user.UpdateTTNhanSu(ent);

            }

            try
            {

                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
        private bool AddNguoiThan()
        {
            FileManager fileMng = new FileManager();

            string filePath = System.IO.Path.Combine(Server.MapPath("."), Config.GetConfigValue("UserImagePath")).TrimEnd('\\');
            CommonFileEntity fileEnt = null;
            string[] arrImageSize = Config.GetConfigValue("UserImageSize").Split(',');
            int swidth = Convert.ToInt32(arrImageSize[0]);
            int sheight = Convert.ToInt32(arrImageSize[1]);
            int mwidth = Convert.ToInt32(arrImageSize[2]);
            int mheight = Convert.ToInt32(arrImageSize[3]);
            int width = Convert.ToInt32(arrImageSize[4]);
            int height = Convert.ToInt32(arrImageSize[5]);

            fileEnt = fileMng.UploadImageFile(FileUpload1, filePath, swidth, sheight);
            // fileEnt = fileMng.UploadImageFile(fileImage, filePath, swidth, sheight, mwidth, mheight, width, height, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"));
            //  fileEnt = fileMng.UploadImageFile_With_Logo(fileImage, filePath, swidth, sheight, mwidth, mheight, width, height, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff"), pathLogo, swidthL, sheightL, mwidthL, mheightL, widthL, heightL);

            ThongTinNguoiThan ents = new ThongTinNguoiThan();
            ents.DiaChiNguoiThan = txt_diachiNt.Text;
            ents.Ghichu = txt_ghichunt.Text;
            ents.Gioitinh = Utils.TryParseInt(dr_gioitinhnguoithan.SelectedValue, 0);
            ents.IdCapNhat = this.UserMemberID;
            ents.IdNhanVien = this.IDNhanVien;
            ents.IdTao = this.UserMemberID;
            ents.MoiQuanHe = txt_moiquanhe.Text;
            ents.NgaySinh = rad_ngaysinhnt.SelectedDate;
            ents.NgheNghiep = txt_ngheNghiepnt.Text;
            ents.Quequan = txt_quequannt.Text;
            ents.SoDTNguoiThan = txt_soDTnt.Text;
            ents.TenNguoiThan = txt_hotennguoithan.Text;

            ThongTinNguoiThan ent = blc_user.GetTTNguoithan_byID(IDNT_NhanVien);
            if (ent == null)
            {
                ents.Image = fileEnt != null ? fileEnt.CommonFileID : 0;
                ents.NgayTao = DateTime.Now;
                blc_user.CreateTTNguoiThan(ents);
            }

            if (ent != null)
            {

                if (fileEnt != null)
                {
                    fileMng.DeleteCommonFile(Utils.TryParseLong(ent.Image, 0), true);
                    ent.Image = fileEnt.CommonFileID;
                }

                ent.IdCapNhat = this.UserMemberID;
                ent.NgayCapNhat = DateTime.Now;
                ent.DiaChiNguoiThan = txt_diachiNt.Text;
                ent.Ghichu = txt_ghichunt.Text;
                ent.Gioitinh = Utils.TryParseInt(dr_gioitinhnguoithan.SelectedValue, 0);
                ent.IdCapNhat = this.UserMemberID;
                ent.IdNhanVien = this.IDNhanVien;
                ent.IdTao = this.UserMemberID;
                ent.MoiQuanHe = txt_moiquanhe.Text;
                ent.NgaySinh = rad_ngaysinhnt.SelectedDate;
                ent.NgheNghiep = txt_ngheNghiepnt.Text;
                ent.Quequan = txt_quequannt.Text;
                ent.SoDTNguoiThan = txt_soDTnt.Text;
                ent.TenNguoiThan = txt_hotennguoithan.Text;
                blc_user.UpdateTTNguoithan(ent);

            }

            try
            {

                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
        private bool AddQTLV()
        {


            ThongTinKyLuatKhenThuong ents = new ThongTinKyLuatKhenThuong();
            ents.GhiChu = txt_ghichukhenthuong.Text;
            ents.IdCapNhat = this.UserMemberID;
            ents.IdNhanVien = this.IDNhanVien;
            ents.IdTao = this.UserMemberID;
            ents.Loai = Utils.TryParseInt(dr_loaikytluatkhenthuong.SelectedValue, 0);
            ents.LyDo = txt_lyDokhenthuong.Text;
            ents.NgayThucHien = rad_ngaythuchien.SelectedDate;

            ThongTinKyLuatKhenThuong ent = blc_user.GetTTKLKhenthuong_byID(this.IDQTLV_NhanVien);
            if (ent == null)
            {
                ents.NgayTao = DateTime.Now;
                blc_user.CreateTTKyLuatkhenthuong(ents);
            }

            if (ent != null)
            {
                ent.NgayCapNhat = DateTime.Now;
                ent.GhiChu = txt_ghichukhenthuong.Text;
                ent.IdCapNhat = this.UserMemberID;
                ent.IdNhanVien = this.IDNhanVien;
                ent.IdTao = this.UserMemberID;
                ent.Loai = Utils.TryParseInt(dr_loaikytluatkhenthuong.SelectedValue, 0);
                ent.LyDo = txt_lyDokhenthuong.Text;
                ent.NgayThucHien = rad_ngaythuchien.SelectedDate;
                blc_user.UpdateTTKyLuatkhenthuong(ent);

            }

            try
            {

                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
        protected int IDNhanVien
        {
            get
            {
                if (Session["g_IDNhanVien"] != null)
                    return Convert.ToInt32(Session["g_IDNhanVien"]);
                return -1;

            }
            set
            {
                Session["g_IDNhanVien"] = value;
            }
        }
        protected int IDCty
        {
            get
            {
                if (Session["g_IDCty"] != null)
                    return Convert.ToInt32(Session["g_IDCty"]);
                return -1;

            }
            set
            {
                Session["g_IDCty"] = value;
            }
        }
        protected int IDNT_NhanVien
        {
            get
            {
                if (Session["g_IDNT_NhanVien"] != null)
                    return Convert.ToInt32(Session["g_IDNT_NhanVien"]);
                return -1;

            }
            set
            {
                Session["g_IDNT_NhanVien"] = value;
            }
        }
        protected int IDQTLV_NhanVien
        {
            get
            {
                if (Session["g_IDQTLV_NhanVien"] != null)
                    return Convert.ToInt32(Session["g_IDQTLV_NhanVien"]);
                return -1;

            }
            set
            {
                Session["g_IDQTLV_NhanVien"] = value;
            }
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




        protected void btnDeleteKey(object sender, EventArgs e)
        {
            try
            {

                if (blc_user.DeleteNhanvien_byID(this.IDNhanVien))
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
            }
            if (dropSearchtype.SelectedValue == "2")
            {
                txtSearch.Visible = true;
                dr_dspb.Visible = false;
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
        public string GetNameLoai(object idloai)
        {
            int idLoai = Utils.TryParseInt(idloai.ToString(), 0);

            if (idLoai == 1)

                return "Khen Thưởng";

            else if (idLoai == 2)

                return "Kỷ luật";
            else if (idLoai == 3)

                return "Đào tạo nội bộ";
            else if (idLoai == 4)

                return "Đào tạo bên ngoài";
            else if (idLoai == 5)

                return "Thay đỗi vị trí";


            else return "---";
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

        protected void dr_tructhuoccty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThongTinNhanSu ent = blc_user.GetTTNhansu_byID(this.IDNhanVien);
            if (dr_tructhuoccty.SelectedValue == "1")
            {

                if (ent != null && ent.IDCTY == 1)
                {
                    txt_manv.Text = ent.MaNV;
                }
                else
                {
                    int idofuser = blc_users.ListttNhansu_byIDCty(1).Count + 1;
                    txt_manv.Text = "NK" + DateTime.Now.Month + DateTime.Now.Year + idofuser;
                }
            }
            if (dr_tructhuoccty.SelectedValue == "2")
            {
                if (ent != null && ent.IDCTY == 2)
                {
                    txt_manv.Text = ent.MaNV;
                }
                else
                {
                    int idofuser = blc_users.ListttNhansu_byIDCty(2).Count + 1;
                    txt_manv.Text = "CN" + DateTime.Now.Month + DateTime.Now.Year + idofuser;
                }
            }

            if (dr_tructhuoccty.SelectedValue == "3")
            {
                if (ent != null && ent.IDCTY == 3)
                {
                    txt_manv.Text = ent.MaNV;
                }
                else
                {
                    int idofuser = blc_users.ListttNhansu_byIDCty(3).Count + 1;
                    txt_manv.Text = "SMC" + DateTime.Now.Month + DateTime.Now.Year + idofuser;
                }
            }


            dr_phongban.Items.Clear();
            IList<PhongBan> phong = blc_user.ListPhongban_byCTY(Utils.TryParseInt(dr_tructhuoccty.SelectedValue, 0));
            dr_phongban.DataSource = phong;
            dr_phongban.DataTextField = "TenPhong";
            dr_phongban.DataValueField = "IDPhong";
            dr_phongban.DataBind();
            dr_phongban.AppendDataBoundItems = true;
            dr_phongban.Items.Insert(0, new ListItem("--Chọn Phòng Ban--", "0"));
        }

        protected void drop_tructhuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IDCty = Utils.TryParseInt(drop_tructhuoc.SelectedValue, -1);
            Bidphongban(IDCty);
            BindGirdView();
        }

        protected void dr_dspb_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGirdView();
        }

        protected void btn_export_Click(object sender, EventArgs e)
        {
            //Get the data from database into datatable
            int idcty = this.UserMemberLike;
            int iduserchoose = this.UserMemberID;

            tbttkt.Visible = false;
            int tructhuocCty = Utilis.TryParseInt(drop_tructhuoc.SelectedValue);
            int loaiNV = Utilis.TryParseInt(drop_loainv.SelectedValue);
            int loaisearh = Utilis.TryParseInt(dropSearchtype.SelectedValue);


            DateTime fromDate = txtDateFrom.SelectedDate;
            DateTime toDate = txtDateTo.SelectedDate;
            if (this.UserMemberType == 1 || this.UserMemberType == 2)
            {
                idcty = -1;
                iduserchoose = -1;

            }
            if (this.UserMemberType == 3)
            {
                iduserchoose = -1;

            }


            DataSet ds = blc_user.NhanVien_Rows_byID(this.CurrentPage, int.MaxValue, fromDate, toDate, tructhuocCty, loaiNV, loaisearh, txtSearch.Text.Trim(), iduserchoose, Utils.TryParseInt(dr_dspb.SelectedValue, 0));
            dataexport = ds.Tables[0];
            DataTable dt = dataexport;
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("AnhDaiDien");
                dt.Columns.Remove("NgayTao");
                dt.Columns.Remove("IDNguoiTao");
                dt.Columns.Remove("NgayCapNhat");
                dt.Columns.Remove("Image");
                dt.Columns.Remove("NguoiPhuThuoc");
                dt.Columns.Remove("IdNhanVien");
                dt.Columns.Remove("NUM");
                // dt.Columns["NUM"].ColumnName = "Stt";
                dt.Columns["SttNhanVien"].ColumnName = "Số TTNV";
                dt.Columns["CMND"].ColumnName = "SoCMND";
                dt.Columns["PhongBan"].ColumnName = "Phòng Ban";
                dt.Columns["HoTen"].ColumnName = "Họ Tên Nhân Viên";
                dt.Columns["ViTri"].ColumnName = "Vị Trí";
                dt.Columns["MaNV"].ColumnName = "Mã Số NV";
                dt.Columns["NgaySinh"].ColumnName = "Ngày Tháng Năm Sinh";
                dt.Columns["GioiTinh"].ColumnName = "Giới Tính";
                dt.Columns["NgayCMND"].ColumnName = "Ngày Cấp CMND";
                dt.Columns["NoiCapCMND"].ColumnName = "Nơi Cấp CMND";
                dt.Columns["NoiSinh"].ColumnName = "Nơi Sinh";
                dt.Columns["NguyenQuan"].ColumnName = "Nguyên Quán";
                dt.Columns["DCTamTru"].ColumnName = "Địa Chỉ Tạm Trú";
                dt.Columns["DCThuongTru"].ColumnName = "Địa Chỉ Thường Trú";
                dt.Columns["SoDT"].ColumnName = "Số Điện Thoại";
                dt.Columns["Trinhdo"].ColumnName = "Trình Độ";
                dt.Columns["ChuyenMon"].ColumnName = "Chuyên Môn";
                dt.Columns["KinhNghiem"].ColumnName = "kinh Nghiệm";
                dt.Columns["MSThue"].ColumnName = "Mã Số Thuế";
                dt.Columns["TkNganHang"].ColumnName = "Ngân Hàng";
                dt.Columns["SoTkNganhang"].ColumnName = "Số Tài Khoản Ngân Hàng";
                dt.Columns["GhiChuNV"].ColumnName = "Ghi Chú Nhân Viên";
                dt.Columns["Dantoc"].ColumnName = "Dân Tộc";
                dt.Columns["Tongiao"].ColumnName = "Tôn Giáo";
                dt.Columns["Tinhtranghonnhan"].ColumnName = "Tình Trạng Hôn Nhân";
                dt.Columns["NgayVaoLam"].ColumnName = "Ngày Vào Làm";
                dt.Columns["NgayKyHD"].ColumnName = "Ngày Ký HĐ";
                //Create a dummy GridView
                //GridView GridView1 = new GridView();
                //GridView1.AllowPaging = false;
                //GridView1.DataSource = dt;
                //GridView1.DataBind();

                //Response.Clear();
                //Response.Buffer = true;
                //Response.AddHeader("content-disposition",
                // "attachment;filename=DataTable.xlsx");
                //Response.Charset = "";
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);

                //for (int i = 0; i < GridView1.Rows.Count; i++)
                //{
                //    //Apply text style to each Row
                //    GridView1.Rows[i].Attributes.Add("class", "textmode");
                //}
                //GridView1.RenderControl(hw);

                ////style to format numbers to string
                //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                //Response.Write(style);
                //Response.Output.Write(sw.ToString());
                //Response.Flush();
                //Response.End();
                string filename = this.g_UserMemberName + "_" + "DanhSachNhanVien" + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Millisecond;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Worksheets.Add(dt, "Sheet1");


                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else MessageBox.Show("No Data Export !");
        }
        protected void Click_uploadexcel(object sender, EventArgs e)
        {
            
                //try
                //{
                    if (filesUpload.HasFile)
                    {
                    // DataTable data = ReadDataFromExcelFile();
                    DataTable data = GenerateExcelData("Select");
                    if (data != null)
                    // Import dữ liệu đọc được vào database
                    {
                        //  grvData.DataSource = data.DefaultView;
                        //  grvData.DataBind();
                        //  Alert.Show("Data OK !");
                        ImportIntoDatabase(data);

                    }
                    else Alert.Show("Không có data !");
                    }
                     else Alert.Show("chưa chọn files !");
                //}
                //catch (Exception ex)
                //{
                //    Alert.Show(ex.ToString());
                //}            
        }
      
        private DataTable GenerateExcelData(string SlnoAbbreviation)
        {
          
            DataTable data = null;
            try
            {

                string filename = Path.GetFileName(filesUpload.FileName);
                filesUpload.SaveAs(Server.MapPath("~/") + filename);
                string path = System.IO.Path.GetFullPath(Server.MapPath("~/" + filename));
                // need to pass relative path after deploying on server
                //  string path = System.IO.Path.GetFullPath(Server.MapPath("~/InformationNew.xlsx"));
                /* connection string  to work with excel file. HDR=Yes - indicates 
                   that the first row contains columnnames, not data. HDR=No - indicates 
                   the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
                   (numbers, dates, strings etc) data columns as text. 
                Note that this option might affect excel sheet write access negative. */

                if (Path.GetExtension(path) == ".xls")
                {
                    oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                }
                else if (Path.GetExtension(path) == ".xlsx")
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                }
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();

                // passing list to drop-down list

                // selecting distict list of Slno 
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT distinct([SoCMND]) FROM [Sheet1$]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "DSNhanVien");
                //  ddlSlno.DataSource = ds.Tables["dsSlno"].DefaultView;
                //if (!IsPostBack)
                //{
                //    ddlSlno.DataTextField = "Name";
                //    ddlSlno.DataValueField = "Name";
                //    ddlSlno.DataBind();
                //}
                // by default we will show form data for all states but if any state is selected then show data accordingly
                if (!String.IsNullOrEmpty(SlnoAbbreviation) && SlnoAbbreviation != "Select")
                {
                    // cmd.CommandText = "SELECT [SoCMND], [PhongBan], [HoTenNhanVien], [ViTri], [MaSoNV], [NgayThangNamSinh], [GioiTinh], [NgayCapCMND], [NoiCapCMND], [NoiSinh], [NguyenQuan], [DCTamTru], [DCThuongTru], [Email], [SoDienThoai], [TrinhDo], [ChuyenMon], [KinhNghiem], [MaSoThue], [NganHang], [SoTKNganHang], [GhiChuNhanVien], [SoTTNV]" + "  FROM [Sheet1$] ";
                    // cmd.Parameters.AddWithValue("@Slno_Abbreviation", SlnoAbbreviation);
                    cmd.CommandText = "SELECT [SoCMND], [Phòng Ban], [Họ Tên Nhân Viên], [Vị Trí], [Ngày Vào Làm], [Ngày Ký HĐ], [Mã Số NV], [Ngày Tháng Năm Sinh], [Giới Tính], [Ngày Cấp CMND], [Nơi Cấp CMND], [Nơi Sinh], [Nguyên Quán], [Địa Chỉ Tạm Trú], [Địa Chỉ Thường Trú], [Email], [Số Điện Thoại], [Trình Độ], [Chuyên Môn], [kinh Nghiệm], [Mã Số Thuế], [Ngân Hàng], [Số Tài Khoản Ngân Hàng], [Ghi Chú Nhân Viên], [Số TTNV], [Dân Tộc], [Tôn Giáo], [Tình Trạng Hôn Nhân] FROM [Sheet1$]";
                }
                else
                {
                    cmd.CommandText = "SELECT [SoCMND], [Phòng Ban], [Họ Tên Nhân Viên], [Vị Trí], [Ngày Vào Làm], [Ngày Ký HĐ], [Mã Số NV], [Ngày Tháng Năm Sinh], [Giới Tính], [Ngày Cấp CMND], [Nơi Cấp CMND], [Nơi Sinh], [Nguyên Quán], [Địa Chỉ Tạm Trú], [Địa Chỉ Thường Trú], [Email], [Số Điện Thoại], [Trình Độ], [Chuyên Môn], [kinh Nghiệm], [Mã Số Thuế], [Ngân Hàng], [Số Tài Khoản Ngân Hàng], [Ghi Chú Nhân Viên], [Số TTNV], [Dân Tộc], [Tôn Giáo], [Tình Trạng Hôn Nhân] FROM [Sheet1$]";
                    //  cmd.CommandText = "SELECT [SoCMND], [PhongBan], [HoTenNhanVien], [ViTri], [MaSoNV], [NgayThangNamSinh], [GioiTinh], [NgayCapCMND], [NoiCapCMND], [NoiSinh], [NguyenQuan], [DCTamTru], [DCThuongTru], [Email], [SoDienThoai], [TrinhDo], [ChuyenMon], [KinhNghiem], [MaSoThue], [NganHang], [SoTKNganHang], [GhiChuNhanVien], [SoTTNV] FROM [Sheet1$] ";
                }
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds);

                // binding form data with grid view
                data = ds.Tables[1].DataSet.Tables[1];

            }
            // need to catch possible exceptions
            catch (Exception ex)
            {
                Alert.Show(ex.ToString());
            }
            finally
            {
                oledbConn.Close();
            }
            return data;
        }
        private void ImportIntoDatabase(DataTable data)
        {
            if (data == null || data.Rows.Count == 0)
            {
                Alert.Show("Không có dữ liệu để import");
                return;
            }
            else
            {

                //  DataNhanVienResource.NhanVienDataTable adapter = new DataNhanVienResource.NhanVienDataTable();
                // HumanResourceTableAdapters.EmployeeInfoTableAdapter adapter = new HumanResourceTableAdapters.EmployeeInfoTableAdapter();
                //  HumanResourceTableAdapters.InforEmployTableAdapter adapter2 = new HumanResourceTableAdapters.InforEmployTableAdapter();
                string Phongban = "", Vitri = "", Masonv = "";
                //  string Noisinh = "", Nguyenquan = "", Dctamtru = "", DCthuongtru = "", Sdt = "", Trinhdo = "", Chuyenmon = "", KinhNghiem = "", Msothue = "", Nganhang = "", SotkNganhang = "", Ghichu = "";

                try
                {

                    for (int i = 0; i < data.Rows.Count; i++)
                    {

                        //AreaName = data.Rows[i]["Name"].ToString().Trim();
                        //areades = data.Rows[i]["Poin"].ToString().Trim();                        
                        NhanVien ent = new NhanVien();                       
                        ent.SttNhanVien = Utils.TryParseInt(data.Rows[i]["Số TTNV"].ToString().Trim(), 0);
                        ent.CMND = data.Rows[i]["SoCMND"].ToString().Trim();
                        Phongban = data.Rows[i]["Phòng Ban"].ToString().Trim();
                        ent.HoTen = data.Rows[i]["Họ Tên Nhân Viên"].ToString().Trim();
                        Vitri = data.Rows[i]["Vị Trí"].ToString().Trim();
                        Masonv = data.Rows[i]["Mã Số NV"].ToString().Trim();
                        // DateTime.ParseExact(data.Rows[i]["Ngày Tháng Năm Sinh"].ToString().Replace("/", "-"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        ent.NgaySinh = getdatetime(data.Rows[i]["Ngày Tháng Năm Sinh"].ToString().Replace("/", "-"));
                        ent.GioiTinh = getGioitinh(data.Rows[i]["Giới Tính"].ToString().Trim());
                        ent.NgayCMND = getdatetime(data.Rows[i]["Ngày Cấp CMND"].ToString().Replace("/", "-"));
                        ent.NoiCapCMND = data.Rows[i]["Nơi Cấp CMND"].ToString().Trim();
                        ent.NoiSinh = data.Rows[i]["Nơi Sinh"].ToString().Trim();
                        ent.NguyenQuan = data.Rows[i]["Nguyên Quán"].ToString().Trim();
                        ent.DCTamTru = data.Rows[i]["Địa Chỉ Tạm Trú"].ToString().Trim();
                        ent.DCThuongTru = data.Rows[i]["Địa Chỉ Thường Trú"].ToString().Trim();
                        ent.SoDt = data.Rows[i]["Số Điện Thoại"].ToString().Trim();
                        ent.Email = data.Rows[i]["Email"].ToString().Trim();
                        ent.Trinhdo = gettrinhdoNV(data.Rows[i]["Trình Độ"].ToString().Trim());
                        ent.ChuyenMon = data.Rows[i]["Chuyên Môn"].ToString().Trim();
                        ent.KinhNghiem = data.Rows[i]["kinh Nghiệm"].ToString().Trim();
                        ent.MSThue = data.Rows[i]["Mã Số Thuế"].ToString().Trim();
                        ent.TkNganHang = data.Rows[i]["Ngân Hàng"].ToString().Trim();
                        ent.SoTkNganhang = data.Rows[i]["Số Tài Khoản Ngân Hàng"].ToString().Trim();
                        ent.GhiChuNV = data.Rows[i]["Ghi Chú Nhân Viên"].ToString().Trim();
                        ent.Dantoc = data.Rows[i]["Dân Tộc"].ToString().Trim();
                        ent.Tongiao = data.Rows[i]["Tôn Giáo"].ToString().Trim();
                        ent.Tinhtranghonnhan = data.Rows[i]["Tình Trạng Hôn Nhân"].ToString().Trim();
                        ent.IDNguoiTao = this.UserMemberID;
                        // DataNhanVienResource.NhanVienDataTable existingEmployee = adapter.GetDataBy_CMND(ent.CMND);
                        NhanVien nhanviennew = blc_user.Getnhanvien_ByCMND(data.Rows[i]["SoCMND"].ToString().Trim());
                        ThongTinNhanSu ttns = new ThongTinNhanSu();
                        ttns.MaNV = data.Rows[i]["Mã Số NV"].ToString().Trim();
                        ttns.ViTri = getvitriNV(data.Rows[i]["Vị Trí"].ToString().Trim());
                       
                        ttns.Idcapnhat = this.UserMemberID;
                        ttns.NgayVaoLam = !string.IsNullOrEmpty(data.Rows[i]["Ngày Vào Làm"].ToString()) ? Convert.ToDateTime(data.Rows[i]["Ngày Vào Làm"].ToString().Trim().Replace("/", "-")) : DateTime.Now;
                        ttns.NgayKyHD = !string.IsNullOrEmpty(data.Rows[i]["Ngày Ký HĐ"].ToString()) ? Convert.ToDateTime(data.Rows[i]["Ngày Ký HĐ"].ToString().Trim().Replace("/", "-")) : Convert.ToDateTime(data.Rows[i]["Ngày Vào Làm"].ToString().Trim().Replace("/", "-"));
                        if (ttns.MaNV.Contains("NK"))
                            ttns.IDCTY = 1;
                        else ttns.IDCTY = 2;
                        ttns.PhongBan = getphongban(data.Rows[i]["Phòng Ban"].ToString().Trim(), Utils.TryParseInt(ttns.IDCTY,0));
                        if (!string.IsNullOrEmpty(ttns.MaNV))
                        {
                            ttns.LoaiNV = 1;

                        }
                        else ttns.LoaiNV = 2;
                        
                        //  HumanResource.EmployeeInfoDataTable existingEmployee = adapter.GetEmployeeInfoByCode(code);
                        //  HumanResource.InforEmployDataTable existingInfoloyee = adapter2.GetDataBy(code);
                        // Nếu nhân viên chưa tồn tại trong DB thì thêm mới
                        if (nhanviennew == null)
                        {
                           
                          int idns =  blc_user.CreateNhanVien(ent);
                          ttns.IdNhanVien = idns;   
                            if(!string.IsNullOrEmpty(ttns.MaNV))
                            { blc_user.CreateTTNhanSu(ttns);}
                            //  adapter.InsertEmployee(code, fullName, workingYears);
                        }
                        // Ngược lại, nhân viên đã tồn tại trong DB thì update
                        if (nhanviennew != null)
                        {
                            ent.IdNhanVien = nhanviennew.IdNhanVien;
                            blc_user.UpdateNhanVien(ent);
                             if(!string.IsNullOrEmpty(ttns.MaNV))
                             {  ttns.IdNhanVien = nhanviennew.IdNhanVien;
                            blc_user.UpdateTTNhanSu(ttns);}

                            //   adapter.UpdateEmployeeInfoByCode(fullName, workingYears, code);
                        }
                        //if (existingInfoloyee == null || existingInfoloyee.Rows.Count == 0)
                        //{
                        //    adapter2.InsertQuery(code, phongban, chucvu);
                        //}
                        //// Ngược lại, nhân viên đã tồn tại trong DB thì update
                        //else
                        //{
                        //    adapter2.UpdateQuery(code, phongban, chucvu);
                        //}
                    }
                    Alert.Show("Upload data success !");
                    BindGirdView();
                }
                catch (Exception ex)
                {
                    Alert.Show("error" + ex.ToString());
                }

            }
        }
        public DateTime getdatetime(string datestring)
        {
            DateTime dt = DateTime.Now ;           
            if (!string.IsNullOrEmpty(datestring))
                datestring = datestring.ToString().Replace("-", "/");
            // Alert.Show(datestring);
            // dt = DateTime.ParseExact(datestring, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            // dt = DateTime.Parse(datestring);
            //Alert.Show(dt.ToShortDateString());
             dt = DateTime.ParseExact(datestring, "dd/MM/yyyy", CultureInfo.InvariantCulture);
           // dt = Convert.ToDateTime(datestring);
            return dt;
        }
        public string gettrinhdoNV(string name)
        {
            string thpt = "thpt";
            string cd = "Cao Đẳng";
            string dh = "Đại Học";
            string trungcap = "Trung Cấp";

            if (name.Trim().ToUpper() == thpt.Trim().ToUpper())
            {
                return "1";
            }
            else if (name.Trim().ToUpper() == cd.Trim().ToUpper())
            {
                return "2";
            }
            else if (name.Trim().ToUpper() == dh.Trim().ToUpper())
            {
                return "3";
            }
            else if (name.Trim().ToUpper() == trungcap.Trim().ToUpper())
            {
                return "5";
            }
            else return "4";
        }
        public int getphongban(string namephong,int idcty)
        {

            PhongBan pb = blc_user.GetPhongBan_ByName(namephong,idcty);
            if (pb != null)
            {
                return pb.IDPhong;
            }
            else
                return 0;
        }
        public int getvitriNV(string vitri)
        {
            string trphong = "Trưởng Phòng";
            string phophong = "Phó Phòng";
            string trnhom = "Trưởng Nhóm";
            string pnhom = "Phó Nhóm";
            string GD = "Giám Đốc";
            string pGD = "Phó Giám Đốc";
            string tlGD = "Trợ Lý Giám Đốc";
            string nvtt = "NV Thực Tập";
            string tbp = "Trưởng Bộ Phận";
            string pbp = "Phó Bộ Phận";
            

            //string saudh = "Sau Đại Học";

            if (vitri.Trim().ToUpper() == trphong.Trim().ToUpper())
            {
                return 1;
            }
            else if (vitri.Trim().ToUpper() == phophong.Trim().ToUpper())
            {
                return 2;
            }
            else if (vitri.Trim().ToUpper() == trnhom.Trim().ToUpper())
            {
                return 3;
            }
            else if (vitri.Trim().ToUpper() == GD.Trim().ToUpper())
            {
                return 5;
            }
            else if (vitri.Trim().ToUpper() == pGD.Trim().ToUpper())
            {
                return 6;
            }
            else if (vitri.Trim().ToUpper() == nvtt.Trim().ToUpper())
            {
                return 7;
            }
            else if (vitri.Trim().ToUpper() == tlGD.Trim().ToUpper())
            {
                return 8;
            }
            else if (vitri.Trim().ToUpper() == tbp.Trim().ToUpper())
            {
                return 9;
            }
            else if (vitri.Trim().ToUpper() == pbp.Trim().ToUpper())
            {
                return 10;
            }
            else if (vitri.Trim().ToUpper() == pnhom.Trim().ToUpper())
            {
                return 11;
            }
            else return 4;
        }
        public string getGioitinh(string name)
        {
            string nu = "Nữ";

            if (name.Trim().ToUpper() == nu.Trim().ToUpper())
            {
                return "2";
            }

            else return "1";
        }
        //public string nameofdscase()
        //{
        //    if (dr_dsnv.SelectedValue != "0")
        //    {
        //        return DACHelper.ConvertUrlText(dr_dsnv.SelectedItem.ToString().Replace(" ", "_"));
        //    }
        //    return DACHelper.ConvertUrlText(g_UserMemberName.Replace(" ", "_")).Replace("-", "_");
        //}

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
