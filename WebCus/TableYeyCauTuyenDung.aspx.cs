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
using PQT.API;
using UserMng.BLC;
using System.Collections.Generic;
using PQT.DAC;
using PQT.Common;
using System.IO;
using System.Data.OleDb;
using System.Linq;
using PQT.DAC.ViewModel;
using System.Net.Mail;

namespace WebCus
{
    public partial class TableYeyCauTuyenDung : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["g_UserMemberType"]== null)
                {
                    Response.Redirect("/");
                }
                var pathdelete = HttpContext.Current.Server.MapPath("~/Uploads/TuyenDung/FileMau/");
                DirectoryInfo dir = new DirectoryInfo(pathdelete);
                foreach (FileInfo fi in dir.GetFiles())
                {
                    fileimport.HRef = "/Uploads/TuyenDung/FileMau/"+fi.Name;
                }
               
                int type = 0;
                if (Request.QueryString["type"] != null)
                {
                    type = int.Parse(Request.QueryString["type"].ToString());

                }
                else
                {
                    type = 1;
                }
                if (type == 1)
                {
                    this.a1.Style.Add("background-color", "#005399");
                    this.a1.Style.Add("border-bottom", "4px solid #033c74");
                    this.a1.Style.Add("color", "white");
                }
                if (type == 2)
                {
                    this.a2.Style.Add("background-color", "#005399");
                    this.a2.Style.Add("color", "white");
                    this.a2.Style.Add("border-bottom", "4px solid #033c74");
                }

                BindGird(type);
                var idUser = this.UserMemberID;
                var IdPhongban = blc_user.GetUser_ByIDAll(idUser).ParentID;
                var tenPhongBan = blc_user.GetPhongBan_ByID(IdPhongban.Value);
                BindPhongBan(tenPhongBan);
                spFile.Visible = false;
                tb_input.Visible = false;
                btnSaveBanner.Visible = false;
                if (Session["g_UserMemberType"].ToString() == "3" || idUser == 275 || idUser == 277 || idUser == 478 || idUser == 276 || idUser == 568 || idUser==6)
                {
                    trTrangthai.Visible = true;
                    trLydo.Visible = true;
                }
                else
                {
                    trTrangthai.Visible = false;
                    trLydo.Visible = false;
                }

                // 
            }
        }
        #region Property
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
        #endregion
        protected UserControl ParentCtrl()
        {
            Control objParent = Parent;
            while (!(objParent is UserControl))
            {
                objParent = objParent.Parent;
            }
            return objParent as UserControl;
        }

        protected int TruThuoc { get; set; }
        private void BindPhongBan(PhongBan tenPhongBan)
        {
            if (tenPhongBan != null)
            {
                //var get
                int tructhuoc = int.Parse(dropTructhuoc.SelectedValue);
                this.TruThuoc = tenPhongBan.TrucThuoc.Value;
                IList<PhongBan> lstPhongban = blc_user.ListPhongban().Where(m => m.TrucThuoc == tructhuoc).ToList();
                dropPhongban.DataSource = lstPhongban;
                dropPhongban.DataTextField = "TenPhong";
                dropPhongban.DataValueField = "IDPhong";
                dropPhongban.DataBind();
                if (tructhuoc == this.TruThuoc)
                    dropPhongban.SelectedValue = tenPhongBan.IDPhong.ToString();
            }
          
            // dropPhongban.SelectedIndex = blc_user.ListPhongban().IndexOf(tenPhongBan);
            //dropPhongban.Enabled = false;
        }
        protected void btnInsertBanner_Click(object sender, EventArgs e)
        {
            if (btnInsertBanner.Text == "Tạo mới")
            {
                resetfield();
                tb_input.Visible = true;
                btnInsertBanner.Text = "Đóng";
                btnSaveBanner.Visible = true;
            }
            else
            {
                this.spFile.Visible = false;
                tb_input.Visible = false;
                btnInsertBanner.Text = "Tạo mới";
                btnSaveBanner.Visible = false;
            }
        }

        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            CreateUpdateType();

        }
        protected void btnDeleteBanner_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
                Alert.Show("Xóa lỗi!");
            }
        }
        protected string GetText()
        {
            return "";
        }
        protected int IDNTD
        {
            get
            {
                if (ViewState["g_IDNTD"] != null)
                    return Convert.ToInt32(ViewState["g_IDNTD"]);
                return -1;
            }
            set
            {
                ViewState["g_IDNTD"] = value;
            }
        }


        #region Create Update
        private void CreateUpdateType()
        {
            bool isCheck = true;
            if (Page.Request.Form["ctl00$MainContent$TTYeuCauTuyenDung$isSend"] != null)
            {
                isCheck = true;
            }
            else
            {
                isCheck = false;
            }
            var idUser = this.UserMemberID;
            TUser tuser = blc_user.GetUser_ByIDAll(idUser);
            // var IdPhongban = blc_user.GetUser_ByIDAll(idUser).ParentID;
            var idPhongBan = int.Parse(Page.Request.Form["ctl00$MainContent$dropPhongban"].ToString());
            var tenPhongBan = blc_user.GetPhongBan_ByID(idPhongBan);
            //var get
            this.TruThuoc = tenPhongBan.TrucThuoc.Value;
            //string path
            //string Files = "/Uploads/TuyenDung/" + stringfilePath;
            string Files = this.Filess.Value;

            if (this.IDNTD == -1)
            {
                var link = Server.MapPath(Files);
                this.IDNTD = blc_user.CreateYeuCauTD(txtTieuDe.Text.Trim(), int.Parse(txtSoLuong.Text), txtNoiDung.Text.Trim(), int.Parse(dropPhongban.SelectedValue), this.TruThuoc, Files, UserMemberID);

                try
                {
                    //sendEmail  
                   // sendEmail(tuser.Email, getEmailtitle(tuser.UserName, tenPhongBan.TenPhong), txtNoiDung.Text.Trim(), link, 1);
                }
                catch (Exception ex)
                {
                    Alert.Show(ex.Message);
                }
                BindGird(1);
                Alert.Show("Tạo thành công!");
            }
            else
            {
                if (this.IDNTD != -1)
                {
                    YeuCauTuyenDung yctd = blc_user.GetYeuCauTD_ByID(this.IDNTD);

                    if (blc_user.UpdateYeuCauTD(this.IDNTD, txtTieuDe.Text.Trim(), int.Parse(txtSoLuong.Text), txtNoiDung.Text.Trim(), int.Parse(dropTrangThai.SelectedValue), txtLyDo.Text, int.Parse(dropPhongban.SelectedValue), this.TruThuoc, Files, UserMemberID) == true)
                    {
                        Alert.Show("Cập nhật thành công!");
                        tb_input.Visible = false;
                        BindGird(1);
                        //Gửi email cho trưởng phòng tạo yêu cầu
                        if (yctd != null)
                        {
                            TUser tu = blc_user.GetUser_ByIDAll(yctd.IdNguoiTao.Value);
                            NhanVien nv = blc_user.GetNhanvien_byID(tu.IdNhansu.Value);
                            var contentduyet = "";
                            string gioitinh = "";
                            if (nv.GioiTinh == "1")
                                gioitinh = "Anh";
                            else
                                gioitinh = "Chị";
                            contentduyet += "<div>Dear " + gioitinh + " " + nv.HoTen + ",</div>";
                            contentduyet += "<div>Phòng hành chánh đã duyệt " + yctd.TieuDe + "</div>";
                            if (isCheck)
                                sendEmail(tu.Email, "Phòng hành chánh duyệt yêu cầu tuyển dụng", contentduyet, "", 2);
                        }
                    }
                    else
                    {
                        Alert.Show("Error!");
                    }
                }
            }
            resetfield();
        }
        #endregion

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                try
                {
                    int idNTD = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                    YeuCauTuyenDung ent = blc_user.GetYeuCauTD_ByID(idNTD);
                    if (ent != null)
                    {
                        if (blc_user.DeleteYeuCauTD(idNTD) == true)
                        {
                            Alert.Show("Ẩn thành công!");
                        }
                    }
                    BindGird(1);
                    resetfield();
                }
                catch
                {
                    Alert.Show("Xóa lỗi!");
                }
            }
            if (e.CommandName == "EditItem")
            {
                spFile.Visible = false;
                btnInsertBanner.Text = "Đóng";
                //
                tb_input.Visible = true;
                btnSaveBanner.Visible = true;
                this.IDNTD = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                BindInfor();
            }
            //if (Session["g_UserMemberType"].ToString() == "2" || Session["g_UserMemberType"].ToString() == "1")
            //{

            //}
            //else Alert.Show("NO Action !");
        }
        public string checktructhuoc(int id)
        {
            string result = "";
            if(id==1)
                result= "Nguyên Kim";
            if (id == 2)
                result = "Chính Nhân";
            if (id == 3)
                result = "SMC";
            return result;
        }
        public string checktrangthai(string status, int IdYeuCau)
        {
            string Reason = "";
            YeuCauTuyenDung yctd = blc_user.GetYeuCauTD_ByID(IdYeuCau);
            if (yctd != null && yctd.Reason != null)
            {
                Reason = yctd.Reason;
            }
            if (status == "2")
            {
                return "<span style='color:blue;font-weight: bold'>Duyệt</span>";
            }
            if (status == "1")
            {
                return "<span style='color:green;font-weight: bold;'>Chưa Duyệt</span>";
            }
            if (status == "3")
            {
                return "<span style='color:red;font-weight: bold'>Hủy</span> <div><strong>Lý do :" + Reason + "</strong></div>";
            }
            if (status == "4")
            {
                return "<span style='color:red;font-weight: bold;'>Đã ẩn</span>";
            }
            return "";
        }

        private void BindInfor()
        {
            if (this.IDNTD != -1)
            {
                YeuCauTuyenDung ent = blc_user.GetYeuCauTD_ByID(this.IDNTD);
                if (ent != null)
                {
                    txtTieuDe.Text = ent.TieuDe;
                    txtNoiDung.Text = ent.NoiDung;
                    dropTrangThai.SelectedValue = ent.TrangThai.Value.ToString();
                    var tenPhongBan = blc_user.GetPhongBan_ByID(ent.IDPhongBan.Value);
                    BindPhongBan(tenPhongBan);
                    stringfilePath = ent.Files;
                    spFile.Visible = true;
                    this.Filess.Value = ent.Files;
                    this.filepath.InnerText = ent.Files.Replace("/Uploads/TuyenDung/", "");
                    this.filepath.HRef = ent.Files;
                    lbFiles.Text = ent.Files.Replace("/Uploads/TuyenDung/", "");
                    lbFiles.NavigateUrl = ent.Files;
                    txtSoLuong.Text = ent.Soluong.Value.ToString();
                    txtLyDo.Text = ent.Reason;
                }
            }
        }
        private void resetfield()
        {
            this.IDNTD = -1;
            txtTieuDe.Text = string.Empty;
            txtNoiDung.Text = string.Empty;
            spFile.Visible = false;
            this.filepath.InnerText = "";
        }


        protected string stringfilePath
        {
            get
            {
                if (ViewState["g_filePath"] != null)
                    return ViewState["g_filePath"].ToString();
                return "";
            }
            set
            {
                ViewState["g_filePath"] = value;
            }
        }
        protected void Click_uploadexcel(object sender, EventArgs e)
        {
            if (filesUpload.HasFile)
            {
                string filename = Path.GetFileName(filesUpload.FileName);
                string fileType = Path.GetExtension(filename);
                var splitdot = filename.Split('.');
                stringfilePath = splitdot[0] + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")) + fileType;
                Alert.Show(stringfilePath);
                string finalPath = Server.MapPath("~/Uploads/TuyenDung/") + stringfilePath;
                filesUpload.SaveAs(finalPath);
                lbFiles.Text = filename;
                lbFiles.NavigateUrl = "~/Uploads/TuyenDung/" + filename;
                spFile.Visible = true;
            }
        }


        public string getEmailtitle(string Username, string PhongBan)
        {
            string str = "";
            str = "[ " + Username + " ]" + "[ " + " " + PhongBan + " ] " + "Yêu cầu tuyển dụng nhân sự";
            return str;
        }
        public string getEmailContent(string title)
        {
            string str = "";
            str = "Nội dung" + " " + title;
            return str;
        }
        private bool sendEmail(string to, string title, string sContent, string file, int type)
        {
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

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress(AdminEmail, Request.Url.Host.ToString(), System.Text.Encoding.UTF8);
                //Lấy danh sách gửi mail
                //var mailsend = blc_user.GetListEmail(1, 1).FirstOrDefault();
                var lstmailCC = blc_user.GetListEmail(true, 2).ToList();
                // mail.To.Add(mailsend.Email);
                if (type == 1)
                {
                    TUser user = blc_user.GetUser_ByIDAll(this.UserMemberID);
                    mail.To.Add(user.Email);
                }
                if (type == 2)
                {
                    mail.To.Add(to);
                }
                if (file != "")
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(file);
                    mail.Attachments.Add(attachment);
                }
                foreach (var its in lstmailCC)
                {
                    if (its.Email != to)
                        mail.CC.Add(new MailAddress(its.Email));
                }
                mail.Subject = title;
                mail.Body = sContent;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (System.Exception ex)
            {
                Alert.Show(ex.Message);
                //error = ex.Message;
                return false;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
        private void BindGird(int type)
        {
            int userid = 0;
            var UserID = this.UserMemberID;
            if (Session["g_UserMemberType"].ToString() != "3" && UserID != 275 && UserID != 277 && UserID != 478 && UserID != 276 && UserID != 568 && UserID!=6)
            {
                userid = UserMemberID;
            }
            int TrucThuoc = int.Parse(drop_tructhuoc.SelectedValue.ToString());
            int TrangThai = int.Parse(drop_trangthai.SelectedValue.ToString());
            IList<VM_YeuCauTuyenDung> list = blc_user.ListYeuCauTuyenDung(userid, TrucThuoc, TrangThai).OrderByDescending(m => m.NgayTao).ToList();
            this.rs1.InnerText = list.Where(m => m.IsDone == 0).Count().ToString();
            this.rs2.InnerText = list.Where(m => m.IsDone == 1).Count().ToString();
            if (type == 1)
            {
                list = list.Where(m => m.IsDone == 0).ToList();
               
            }
            else
            {
                list = list.Where(m => m.IsDone == 1).ToList();
               
            }

            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }
        protected void drop_tructhuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGird(1);
        }

        protected void drop_trangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGird(1);
        }

        protected void filesUpload_Load(object sender, EventArgs e)
        {

        }

        protected void dropTructhuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idUser = this.UserMemberID;
            var IdPhongban = blc_user.GetUser_ByIDAll(idUser).ParentID;
            var tenPhongBan = blc_user.GetPhongBan_ByID(IdPhongban.Value);
            BindPhongBan(tenPhongBan);
        }
        protected void btnSaveBanner_Click2(object sender, EventArgs e)
        {
            var chk = Page.Request.Form["ctl00$MainContent$tttd"].ToString();
            if (chk == "radDangTuyendung")
            {
                blc_user.UpdateStatusYCTD(int.Parse(this.txtIdYeuCau.Value), 0);
            }
            else
            {
                blc_user.UpdateStatusYCTD(int.Parse(this.txtIdYeuCau.Value),1);
            }
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }

        [System.Web.Services.WebMethod]
        public static int GetStatus(int IdYeuCau)
        {
            UserMngOther_BLC blc_user2 = new UserMngOther_BLC();
            var getStatus = blc_user2.GetYeuCauTD_ByID(IdYeuCau);
            blc_user2.Dispose();
            return getStatus.IsDone.HasValue? getStatus.IsDone.Value:0;
        }
    }
}