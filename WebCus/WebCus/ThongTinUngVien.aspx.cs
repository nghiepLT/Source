using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using PQT.DAC;
using PQT.DAC.ViewModel;
using PQT.Common;
using Newtonsoft.Json;
using UserMng.DAC;
using System.Web.Services;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Net.Mail;

namespace WebCus
{
    public partial class ThongTinUngVien : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        static UserMngOther_BLC blc_user2 = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                //var path = Server.MapPath("/Uploads/TuyenDung/3ttuvdutuyen.doc");
                //Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(path);
                //object missing = System.Reflection.Missing.Value;
                //var aa = doc.Content.Text; 
                //this.noidung.Value = aa;
                // doc.Content.Text += "Nội dung file";
                //app.Visible = true;    //Optional
                //doc.Save();
                //doc.Close();
                // Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                object miss = System.Reflection.Missing.Value;
                BindDropSlYeuCauTuyenDung();
                BindGird(int.Parse(drop_yeucautuyendung.SelectedValue));

                BindiDropYCTD();

                var phongban = blc_user.GetPhongBanByTrucThuoc(int.Parse(drop_tructhuoc.SelectedValue));
                BindPhongBan(phongban);
                tb_input.Visible = false;
                btnSaveBanner.Visible = false;
            }
        }
        public void BindDropSlYeuCauTuyenDung()
        {
            IList<VM_YeuCauTuyenDung> lstPhongban = blc_user.ListVMYCTD().ToList();
            VM_YeuCauTuyenDung yctd = new VM_YeuCauTuyenDung();
            List<VM_YeuCauTuyenDung> lstPhongban2 = new List<VM_YeuCauTuyenDung>();
            yctd.TieuDe = "Tất cả";
            yctd.IdYeuCau = 0;
            lstPhongban2.Add(yctd);
            foreach (var item in lstPhongban)
            {
                lstPhongban2.Add(item);
            }
            drop_yeucautuyendung.DataSource = lstPhongban2;
            drop_yeucautuyendung.DataTextField = "TieuDe";
            drop_yeucautuyendung.DataValueField = "IdYeuCau";
            drop_yeucautuyendung.DataBind();
        }
        private void BindiDropYCTD()
        {
            IList<VM_YeuCauTuyenDung> lstPhongban = blc_user.ListVMYCTD().ToList();
            dropYeuCauTuyenDung.DataSource = lstPhongban;
            dropYeuCauTuyenDung.DataTextField = "TieuDe";
            dropYeuCauTuyenDung.DataValueField = "IdYeuCau";
            dropYeuCauTuyenDung.DataBind();
        }
        private void BindGird(int idyeucautuyendung)
        {
            int userid = 0;
            int tructhuoc = int.Parse(drop_tructhuoc.SelectedValue);
            int trangthai = int.Parse(drop_trangthai.SelectedValue);
            string hoten = Page.Request.Form["ctl00$MainContent$txtTenUngVien"];
            if (hoten == null)
                hoten = "";
            string droptrangthai = dropTrangThai.SelectedItem.Text;
            //Alert.Show(trangthai.ToString());
            List<VM_UngvienStatus> list = blc_user.Laydanhsachungvien(idyeucautuyendung).OrderByDescending(m => m.CreatedDate).Where(m => m.TrucThuoc == tructhuoc && (hoten=="" || m.HoTen.ToLower().Contains(hoten.ToLower()) )).Where(m=>droptrangthai=="Tất cả" || m.TrangThai==droptrangthai).ToList();
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }

        private void BindPhongBan(IList<PhongBan> phongban)
        {
            //var get 
            IList<PhongBan> lstPhongban = phongban;
            dropPhongban.DataSource = lstPhongban;
            dropPhongban.DataTextField = "TenPhong";
            dropPhongban.DataValueField = "IDPhong";
            dropPhongban.DataBind();
            // dropPhongban.SelectedValue = tenPhongBan.IDPhong.ToString();
            // dropPhongban.SelectedIndex = blc_user.ListPhongban().IndexOf(tenPhongBan);
        }
        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid idNTD = Guid.Parse(e.CommandArgument.ToString());
            UngVien ent = blc_user.GetUngVienByID(idNTD);
            //
           
            if (e.CommandName == "DeleteItem")
            {
                if (ent != null)
                {
                    if (blc_user.DeleteUngVien(idNTD) == true)
                    {
                        Alert.Show("Xóa thành công!");
                    }
                }
                BindGird(int.Parse(drop_yeucautuyendung.SelectedValue));
                resetfield();
            }
            if (e.CommandName == "DongBoItem")
            {
                //Đồng bộ nhân viên

                if (ent != null)
                {

                    //Gui file huong dan
                    string finalPath = Server.MapPath("~/Uploads/FileHuongDan/") + "File Hướng Dẫn Nhân Viên Mới.docx";
                    string contenthtml= "";
                    contenthtml += "<table>";
                    contenthtml += "<tr style='padding:3px 3px;'>";
                    contenthtml += "<td>";
                    contenthtml += "Chào bạn " + ent.HoTen;
                    contenthtml += "</td>";
                    contenthtml += "</tr>";
                    //
                    contenthtml += "<tr style='padding:3x 3px;'>";
                    contenthtml += "<td>";
                    contenthtml += "Mời bạn tham gia khảo sát quy định của công ty thông qua file hướng dẫn đính kèm. ";
                    contenthtml += "</td>";
                    contenthtml += "</tr>";
                    // 
                    contenthtml += "<tr style='padding:3px 3px;'>";
                    contenthtml += "<td>";
                    TUser tuser = blc_user.GetUserByNhanvienId(ent.IdNhanVien.Value);
                    contenthtml += "User đăng nhập: "+ tuser.LoginID+" "+"Password: "+"1";
                    contenthtml += "</td>";
                    contenthtml += "</tr>";
                    //
                    contenthtml += "</table>";
                    sendEmail(ent.Email, "Hướng dẫn nhân viên mới", contenthtml, finalPath); 
                    NhanVien dbnv = new NhanVien();
                    if (ent.IdNhanVien == null)
                        dbnv = new NhanVien();
                    else
                        dbnv = blc_user.GetNhanvien_byID(ent.IdNhanVien.Value);

                    dbnv.HoTen = ent.HoTen;
                    dbnv.GioiTinh = ent.GioiTinh == "Nam" ? "1" : "0";
                    dbnv.NgaySinh = ent.NgaySinh.Value;
                    dbnv.CMND = ent.CMND;
                    dbnv.NgayCMND = ent.NgayCMND;
                    dbnv.NoiCapCMND = ent.NoiCapCMND;
                    dbnv.NoiSinh = ent.NoiSinh;
                    dbnv.NguyenQuan = "";
                    dbnv.DCThuongTru = ent.DCThuongTru;
                    dbnv.DCTamTru = ent.DCTamTru;
                    dbnv.Email = ent.Email;
                    dbnv.SoDt = ent.SoDt;
                    Quatrinhdaotao Quatrinhdaotao = JsonConvert.DeserializeObject<Quatrinhdaotao>(ent.QuaTrinhDaoTao);
                    if (Quatrinhdaotao != null)
                    {
                        if (Quatrinhdaotao.Vanbang == "THPT")
                            dbnv.Trinhdo = "1";
                        else
                        {
                            if (Quatrinhdaotao.Vanbang == "Trung cấp")
                                dbnv.Trinhdo = "5";
                            else
                            {
                                if (Quatrinhdaotao.Vanbang == "Cao đẳng")
                                    dbnv.Trinhdo = "2";
                                else
                                {
                                    if (Quatrinhdaotao.Vanbang == "Đại học")
                                        dbnv.Trinhdo = "3";
                                    else
                                    {
                                        dbnv.Trinhdo = "4";
                                    }
                                }
                            }
                        }

                    }
                    dbnv.Dantoc = ent.Dantoc;
                    dbnv.Tongiao = ent.Tongiao;
                    if (!string.IsNullOrEmpty(ent.GiaDinh))
                    {
                        GiaDinh GiaDinh = JsonConvert.DeserializeObject<GiaDinh>(ent.GiaDinh);
                        if (GiaDinh != null)
                        {
                            if (GiaDinh.TinhTrangHonNhan == 1)
                                dbnv.Tinhtranghonnhan = "Đã Lập Gia Đình";
                            else
                                dbnv.Tinhtranghonnhan = "Độc Thân";
                        }
                        if (GiaDinh.VoChong != null)
                        {

                        }
                    }
                    if (ent.IdNhanVien == null)
                    {
                        var result = blc_user.CreateNhanVien(dbnv);
                        if (!string.IsNullOrEmpty(ent.GiaDinh))
                        {
                            GiaDinh GiaDinh = JsonConvert.DeserializeObject<GiaDinh>(ent.GiaDinh);

                            if (GiaDinh.VoChong != null)
                            {
                                ThongTinNguoiThan ttnt = new ThongTinNguoiThan();
                                ttnt.IdNhanVien = result;
                                ttnt.TenNguoiThan = GiaDinh.VoChong.HoTenVoChong;
                                ttnt.NgaySinh = DateTime.Now;
                                ttnt.MoiQuanHe = "Vợ chồng";
                                ttnt.Gioitinh = ent.GioiTinh == "Nam" ? 2 : 1;
                                blc_user.CreateTTNguoiThan(ttnt);
                            }
                            if (GiaDinh.lstCon != null && GiaDinh.lstCon.Count() > 0)
                            {
                                foreach (var item in GiaDinh.lstCon)
                                {
                                    ThongTinNguoiThan ttnt = new ThongTinNguoiThan();
                                    ttnt.IdNhanVien = result;
                                    ttnt.TenNguoiThan = item.HoTen;
                                    ttnt.NgaySinh = DateTime.Now;
                                    ttnt.MoiQuanHe = "Con cái";
                                    ttnt.Gioitinh = 1;
                                    blc_user.CreateTTNguoiThan(ttnt);
                                }
                            }
                        }
                        ThongTinNhanSu ttuv = new ThongTinNhanSu();
                        ttuv.IdNhanVien = result;
                        ttuv.LoaiNV = 2;
                        ttuv.ViTri = 4;
                        Ungvien_Trangthai uvtt = blc_user.GetUngvien_TrangthaiById(idNTD);
                        ttuv.NgayVaoLam = uvtt.NgayNhanViec;
                        YeuCauTuyenDung yctd = blc_user.GetIDPhongBanByYeuCau(ent.IdYeuCau.Value);
                        if (yctd != null)
                        {
                            ttuv.PhongBan = yctd.IDPhongBan;
                            ttuv.IDCTY = yctd.TrucThuoc.Value;
                        }
                        blc_user.CreateTTNhanSu(ttuv);
                        //User 
                        var strName = RemoveUnicode(ent.HoTen).Split(' ');
                        var strplsu = "";
                        if (strName != null)
                        {
                            strplsu = strName[strName.Length - 1];
                            for (int i = 0; i < strName.Length - 1; i++)
                            {
                                strplsu += strName[i].Substring(0, 1).ToLower();
                            }
                        }
                        tuser.LoginID = strplsu;
                        tuser.UserName = ent.HoTen;
                        tuser.Password = "YrItVniimrc=";
                        tuser.Tel = ent.SoDt;
                        tuser.Email = ent.Email;
                        tuser.Address = ent.DCThuongTru;
                        tuser.UserType = 7;
                        tuser.IdNhansu = result;
                        blc_user.InsertUser(tuser);
                        //Cập nhật idnhanvien cho ung vien
                        blc_user.UpdateIdNhanVienUngVien(ent, result);
                        //Add quyền menu
                        blc_user.PhanQuyenungvien(tuser);
                        if (result != 0)
                        {
                            Alert.Show("Susscess!");
                        }
                    }
                    else
                    {
                        var result = blc_user.UpdateNhanVien(dbnv);
                        if (result == true)
                        {
                            Alert.Show("Susscess!");
                        }
                    }

                }
            }
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        private void resetfield()
        {
            this.IDNTD = new Guid();
            txthoTen.Text = string.Empty;
        }
        protected void btnInsertBanner_Click(object sender, EventArgs e)
        {
            if (btnInsertBanner.Text == "Tạo mới")
            {
                tb_input.Visible = true;
                btnInsertBanner.Text = "Đóng";
                btnSaveBanner.Visible = true;
            }
            else
            {
                tb_input.Visible = false;
                btnInsertBanner.Text = "Tạo mới";
                btnSaveBanner.Visible = false;
            }
        }
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            CreateUpdateType();
        }
        protected Guid IDNTD
        {
            get
            {
                if (ViewState["g_IDNTD"] != null)
                    return Guid.Parse(ViewState["g_IDNTD"].ToString());
                return new Guid();
            }
            set
            {
                ViewState["g_IDNTD"] = value;
            }
        }
        private void CreateUpdateType()
        {
            if (this.IDNTD == new Guid())
            {
                this.IDNTD = blc_user.CreateUngVien(txthoTen.Text.Trim(), int.Parse(dropYeuCauTuyenDung.SelectedValue));
                BindGird(int.Parse(drop_yeucautuyendung.SelectedValue));
                resetfield();
                tb_input.Visible = false;
                Alert.Show("Susscess!");
            }
            else
            {

            }
        }
        public string checktrangthai(string status, string id)
        {
            if (status == "-1")
            {
                return "<span style='color:red'>Không trúng tuyển</span>";
            }
            if (status == "0")
            {
                return "<span style='color:green'>Chờ phỏng vấn</span>";
                //return "<a onclick=\"ShowPopupMapLink('"+Guid.Parse(id)+"')\" style='color:blue'>Chưa duyệt</a>";
            }

            if (status == "1")
            {
                return "<span style='color:blue'>Chờ đánh giá sau PV</span>";
            }
            if (status == "2")
            {
                return "<span style='color:blue'>Đã duyệt</span>";
            }
            if (status == "3")
            {
                return "<span style='color:blue'>Đang thử việc</span>";
            }
            return "";
        }
        public string checkchucnang(string status, string id)
        {
            if (status == "3")
            {
                return "<a onclick=\"Tomtatquytrinhhuongdan('" + id + "')\" style='color:blue'>Tóm tắt quy trình hướng dẫn</a>" + "<div><a onclick=\"Tomtatphucloi('" + id + "')\" style='color:blue'>Tóm tắt phúc lợi</a></div>" + "<div><a onclick=\"Khaosathoinhap('" + id + "')\" style='color:blue'>Khảo sát hội nhập</a></div>";
            }
            return "";
        }
        public bool CheckStatus(string GuiThumoi)
        {
            if (GuiThumoi == "1")
                return true;
            return false;
        }
        public string CheckTrucThuc(int TrucThuoc)
        {
            string result = "";
            if (TrucThuoc == 1)
                result = "Nguyên Kim";
            if (TrucThuoc == 2)
                result = "Chính Nhân";
            if (TrucThuoc == 3)
                result = "SMC";
            return result;
        }
        public string CheckChucnangNhapThongtin(Guid id)
        {
            Ungvien_Trangthai uvStatus = blc_user.GetUngvien_TrangthaiById(id);
            UngVien uv = blc_user.GetUngVienByID(id);
            string strResult = "";
            strResult += " <div>";
            string input = "";
            if (uvStatus.NgayNhapThongTin.HasValue)
                input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
            else
                input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";
            string wordIcon = " <img style='width:21px; position: relative;left:11px;top: -1px;' src=\"Images/wordicon.png\"/>";
            //if (!string.IsNullOrEmpty(uv.ImportPath))
            //{
            //    wordIcon = "<a href='/Uploads/UngVien/" + uv.ImportPath+"' > <img class='iconimport' src=\"Images/wordicon.png\" /></a>";
            //}

            strResult += "<a style='position:relative' class='btnnhapthongtin' target=\"_blank\" href='thongtinungviendutuyen.aspx?id=" + id + "'>" + input + " Link nhập thông tin</a> <a class='importfile' onclick=\"importf('" + id + "')\">Import file" + wordIcon +"</a></div>";
            strResult += "<div class='NgayTaoChung'>";
            if (uvStatus.NgayNhapThongTin.HasValue)
            {
                strResult += "<div class='Ngaytaobox '><strong class='dateks'><i class='fa fa-calendar'></i>  Ngày tạo : " + uvStatus.NgayNhapThongTin.Value + " </strong></div>";
            }
            strResult += "</div>";
            return strResult;
        }

        public string CheckChucnangPhongvan(Guid id)
        {
            Ungvien_Trangthai uvStatus = blc_user.GetUngvien_TrangthaiById(id);
            UngVien uv = blc_user.GetUngVienByID(id);
            string strResult = "";
            if (uvStatus.NgayNhapThongTin.HasValue)
            {
                strResult += " <div >";
                string input = "";
                if (uvStatus.NgàyPV.HasValue)
                    input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                else
                    input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";
                if (uvStatus.NgàyPV.HasValue)
                    strResult += "<a style='position:relative;pointer-events: none;' class='btnphongvan' onclick =\"ShowPopupMapLink('" + id + "')\" >" + input + " Mời phỏng vấn</a></div>";
                else
                    strResult += "<a style='position:relative' class='btnphongvan' onclick =\"ShowPopupMapLink('" + id + "')\" >" + input + " Mời phỏng vấn</a></div>";

                strResult += "<div class='NgayTaoChung'>";
                if (uvStatus.NgàyPV.HasValue)
                {
                    strResult += "<div class='Ngaytaobox'><strong class='dateks'> Ngày tạo : " + uvStatus.NgàyPV.Value + " </strong></div>";
                    DotTuyenDung dtd = blc_user.GetDotTuyenDung(uv.Id, 1);
                    strResult += "<div class='Ngaytaobox'><strong class='dateks'> Ngày mời phỏng vấn : " + uvStatus.NgayGoiPV.Value + " </strong></div>";
                }
                //Phỏng vấn lần 2
                DanhGiaTuyenDung dgtd = blc_user.CheckDanhGiatuyenDung(id);
                if (dgtd != null)
                {
                    if (dgtd.Quyetdinhlan1 == "PV lần 2")
                    {
                        string input2 = "";
                        if (uvStatus.NgayPhongVan2 == null)
                        {
                            input2 += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" />";
                            strResult += "<a style='position:relative;' class='btnphongvan' onclick =\"ShowPopupMapLink('" + id + "')\" >" + input2 + " Mời phỏng vấn</a></div>";
                        }
                        else
                        {
                            input2 += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                            strResult += "<a style='position:relative;pointer-events: none;' class='btnphongvan' onclick =\"ShowPopupMapLink('" + id + "')\" >" + input2 + " Mời phỏng vấn lần 2</a></div>";
                        }
                        if (uvStatus.NgayPhongVan2.HasValue)
                        {
                            strResult += "<div class='Ngaytaobox'><strong class='dateks'> Ngày tạo : " + uvStatus.NgayPhongVan2.Value + " </strong></div>";
                            DotTuyenDung dtd = blc_user.GetDotTuyenDung(uv.Id, 2);
                            strResult += "<div class='Ngaytaobox'><strong class='dateks'> Ngày mời phỏng vấn : " + uvStatus.NgayGoiPV2.Value + " </strong></div>";
                        }
                    }
                }

                strResult += "</div>";
            }

            return strResult;
        }
        public string CheckChucnangDanhGia(Guid id)
        {
            string strResult = "";
            Ungvien_Trangthai uvStatus = blc_user.GetUngvien_TrangthaiById(id);
            DanhGiaTuyenDung dgtd = blc_user.CheckDanhGiatuyenDung(id);
            if (uvStatus.NgàyPV != null)
            {
                strResult += " <div>";
                string input = "";
                string inputNguoiPvDanhGia = "";
                string inputNguoiDuyetDanhGia = "";
                if (uvStatus.NgayDanhgia.HasValue)
                    input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                else
                    inputNguoiPvDanhGia += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";

                if(uvStatus.NgayNguoiPVDanhGia!=null)
                    inputNguoiPvDanhGia += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                else
                    inputNguoiPvDanhGia += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";

                if (uvStatus.NgayNguoiDuyetDanhGia != null)
                    inputNguoiDuyetDanhGia += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                else
                    inputNguoiDuyetDanhGia += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";
                strResult += "<div><a style='position:relative' class='btnDanhgia' onclick =\"ShowPopupMapLink2('" + id + "')\" >" + input + "Đánh giá </a></div>";
                strResult += "<div><a style='position:relative' class='btnDanhgia' onclick =\"ShowPopupMapLinkDanhGia1('" + id + "','" + 1 + "','" + this.UserMemberID + "')\" >" + inputNguoiPvDanhGia + "Người PV Đánh Giá "+ (uvStatus.MailNguoiDanhGia != null ? "( Đã gửi mail )" : "") +"</a></div>";
                if(uvStatus.NgayNguoiPVDanhGia!=null)
                    strResult += "<div><strong class='dateks'> Ngày tạo: " + uvStatus.NgayNguoiPVDanhGia.Value + " </strong></div>";
                strResult += "<div><a style='position:relative' class='btnDanhgia' onclick =\"ShowPopupMapLinkDanhGia2('" + id + "','" + 2 + "','" + this.UserMemberID + "')\" >" + inputNguoiDuyetDanhGia + "Người Duyệt Đánh Giá" + (uvStatus.MailNguoiDanhGia != null ? "( Đã gửi mail )" : "") +" </a></div>";
                if (uvStatus.NgayNguoiDuyetDanhGia != null)
                    strResult += "<div><strong class='dateks'> Ngày tạo: " + uvStatus.NgayNguoiDuyetDanhGia.Value + " </strong></div>";
                if (uvStatus.NgayDanhgia.HasValue)
                {
                    //strResult += "<div><strong class='dateks'> Ngày tạo: " + uvStatus.NgayDanhgia.Value + " </strong></div>";
                    
                }
            }
            return strResult;
        }

        public string CheckChucnangMoinhanviec(Guid id)
        {
            string strResult = "";
            Ungvien_Trangthai uvStatus = blc_user.GetUngvien_TrangthaiById(id);
            DanhGiaTuyenDung dgtd = blc_user.CheckDanhGiatuyenDung(id);
            DanhGiaTuyenDung dgtd2 = blc_user.CheckDanhGiatuyenDung2(id);

            if (uvStatus.NgayDanhgia != null)
            {
                if (dgtd.Ketqualan1 == 1 || (dgtd2 != null && dgtd2.Ketqualan2 == 1))
                {
                    strResult += " <div>";
                    string input = "";
                    if (uvStatus.NgayGuithumoi.HasValue)
                        input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                    else
                        input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";
                    if (uvStatus.NgayGuithumoi.HasValue)
                        strResult += "<a style='position:relative;pointer-events: none;' class='btnGuithunhanviec' onclick =\"ShowPopupMapLink3('" + id + "')\" >" + input + "Gửi thư nhận việc</a></div>";
                    else
                        strResult += "<a style='position:relative' class='btnGuithunhanviec' onclick =\"ShowPopupMapLink3('" + id + "')\" >" + input + "Gửi thư nhận việc</a></div>";

                    if (uvStatus.NgayGuithumoi.HasValue)
                    {
                        strResult += "<div><strong class='dateks'> Ngày tạo: " + uvStatus.NgayGuithumoi.Value + " </strong></div>";
                        strResult += "<div><strong class='dateks'> Ngày nhận việc: " + uvStatus.NgayNhanViec.Value + " </strong></div>";

                    }
                }

            }
            return strResult;
        }
        public string CheckChucnangDongbo(Guid id)
        {
            string strResult = "";
            Ungvien_Trangthai uvStatus = blc_user.GetUngvien_TrangthaiById(id);
            DanhGiaTuyenDung dgtd = blc_user.CheckDanhGiatuyenDung(id);

            if (uvStatus.NgayGuithumoi != null)
            {
                strResult += " <div>";
                string input = "";
                //if (uvStatus.Ngaydongbo.HasValue)
                //    input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                //else
                //    input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";

                if (uvStatus.Ngaydongbo.HasValue)
                {
                    strResult += "<div><strong class='dateks'>" + input + " Ngày đồng bộ: " + uvStatus.Ngaydongbo.Value + " </strong></div>";
                    UngVien uv = blc_user.GetUngVienByID(id);
                    ThongTinNhanSu ttns = blc_user.GetTTNhansu_byID(uv.IdNhanVien.Value);
                    strResult += "<div><strong class='dateks'>" + input + " Ngày vào làm: " + ttns.NgayVaoLam + " </strong></div>";

                }
            }
            return strResult;
        }
        public string Checkchucnangquytrinh(Guid id)
        {
            string strResult = "";
            Ungvien_Trangthai uvStatus = blc_user.GetUngvien_TrangthaiById(id);
            DanhGiaTuyenDung dgtd = blc_user.CheckDanhGiatuyenDung(id);
            UngVien uv = blc_user.GetUngVienByID(id);
            if (!uv.IdNhanVien.HasValue)
                return "";
            TUser tuser = blc_user.GetUserByNhanvienId(uv.IdNhanVien.Value);
            if (uvStatus.Ngaydongbo != null)
            {
                strResult += " <div>";

                //if (uvStatus.Ngaydongbo.HasValue)
                //    strResult += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                //else
                //    strResult += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";
                string ip1 = "", ip2 = "", ip3 = "";

                if (blc_user.checkUngVienQuytrinhhuongdan(tuser.UserID))
                    ip1 = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                if (blc_user.checkungVienChedophucloi(tuser.UserID))
                    ip2 = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";

                if (blc_user.checkungVienKhaosat(uv.IdNhanVien.Value))
                    ip3 = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";

                strResult += "<div><a style='position:relative;padding-left: 30px;' href='Tomtatquytrinh.aspx?id=" + id + "' target='_blank' class='btnTomTatQuyTrinh'>" + ip1 + "Tóm tắt quy trình</a></div>";
                strResult += "<div><a style='position:relative;padding-left: 30px;' href='Tomtatphucloi.aspx?id=" + id + "' target='_blank' class='btnTomtatphucloi'>" + ip2 + "Tóm tắt phúc lợi</a></div>";
                strResult += "<div><a style='position:relative;padding-left: 30px;' href='Khaosathoinhap.aspx?id=" + id + "' target='_blank' class='btnKhaosat'>" + ip3 + "Khảo sát</a></div>";

                Ungvienkhaosat uvks = blc_user.GetUngvienkhaosat(uv.IdNhanVien.Value);
                if (uvks != null)
                {
                    if (uvks.Ks7Ngay != null)
                    {
                        strResult += "<div><strong class='dateks'> Khảo sát 7 ngày: " + uvks.Ks7NgayDate.Value + " </strong></div>";
                    }
                    if (uvks.Ks14Ngay != null)
                    {
                        strResult += "<div><strong class='dateks'> Khảo sát 14 ngày: " + uvks.Ks14NgayDate.Value + " </strong></div>";
                    }
                    if (uvks.ks2Thang != null)
                    {
                        strResult += "<div><strong class='dateks'> Khảo sát 2 tháng: " + uvks.KS2ThangDate.Value + " </strong></div>";
                    }
                }

            }
            return strResult;
        }
        public string Checkchucnangdanhgiathuviec(Guid id)
        {
            string strResult = "";
            Ungvien_Trangthai uvStatus = blc_user.GetUngvien_TrangthaiById(id);
            DanhGiaTuyenDung dgtd = blc_user.CheckDanhGiatuyenDung(id);
            strResult += " <div>";
            string input = "";
            if (uvStatus.NgayDanhGiaThuViec.HasValue)
                input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
            else
                input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";



            if (uvStatus.NgayKhaosatHoiNhap.HasValue)
            {
                string inputnguoidanhgia = "";
                string inputtruongphong = "";
                string inputnhansu = "";
                string inputbangiamdoc = "";
                var dgtv = blc_user.CheckDanhGiaThuViec(id);

                if (dgtv != null && dgtv.NgayNguoiDanhGia != null)
                    inputnguoidanhgia = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                if (dgtv != null && dgtv.NgayTruongPhong != null)
                    inputtruongphong = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                if (dgtv != null && dgtv.NgayHCNS != null)
                    inputnhansu = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                if (dgtv != null && dgtv.NgayBanGiamDoc != null)
                    inputbangiamdoc = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                strResult += "<div style='text-align: left;'><a style='position:relative;padding-left: 32px;width: auto;' class='btnTomtatphucloi' onclick =\"ShowPopupMapLink6('" + id + "','" + 1 + "','" + this.UserMemberID + "')\" >" + inputnguoidanhgia + "Người đánh giá " + (((dgtv != null && dgtv.MailNguoiDanhGia.HasValue) && dgtv.MailNguoiDanhGia.Value == 1) ? "( Đã gửi mail )" : "") + "</a></div>";
                strResult += "<div style='text-align: left;'><a style='position:relative;padding-left: 32px;width: auto;' class='btnTomtatphucloi' onclick =\"ShowPopupMapLink6('" + id + "','" + 2 + "','" + this.UserMemberID + "')\" >" + inputtruongphong + "Trưởng phòng chuyên môn" + (((dgtv != null && dgtv.MailTruongPhong.HasValue) && dgtv.MailTruongPhong.Value == 1) ? "( Đã gửi mail )" : "") + "</a></div>";
                strResult += "<a style='position:relative;padding-left: 27px;width: auto;' class='btnTomtatphucloi' onclick =\"ShowPopupMapLink4('" + id + "')\" >" + inputnhansu + "Phòng nhân sự</a></div>";
                strResult += "<div style='text-align: left;'><a style='position:relative;padding-left: 27px;width: auto;' class='btnTomtatphucloi' onclick =\"ShowPopupMapLink6('" + id + "','" + 4 + "','" + this.UserMemberID + "')\" >" + inputbangiamdoc + "Ban giám đốc" + (((dgtv != null && dgtv.MailBanGiamDoc.HasValue) && dgtv.MailBanGiamDoc.Value == 1) ? "( Đã gửi mail )" : "") + "</a></div>";
            }
            if (uvStatus.NgayDanhGiaThuViec.HasValue)
            {
                strResult += "<div><strong class='dateks'>" + " Ngày đánh giá: " + uvStatus.NgayDanhGiaThuViec.Value + " </strong></div>";
            }
            strResult += " </div>";
            return strResult;
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
        public string Checkchucnangchucmung(Guid id)
        {
            string strResult = "";
            Ungvien_Trangthai uvStatus = blc_user.GetUngvien_TrangthaiById(id);
            DanhGiaTuyenDung dgtd = blc_user.CheckDanhGiatuyenDung(id);
            if (uvStatus.NgayDanhgia != null)
            {
                var dgtv = blc_user.GetDanhgiathuviec(id);
                if (dgtv != null && dgtv.NgayBanGiamDoc != null)
                {
                    strResult += " <div>";
                    string input = "";
                    if (uvStatus.DanhGiaThuViec != 1)
                        return "";
                    if (uvStatus.NgayGuiMailChucMung.HasValue)
                        input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                    else
                        input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";
                    if (uvStatus.NgayGuiMailChucMung.HasValue)
                        strResult += "<a style='position:relative;pointer-events: none;' class='btnGuithunhanviec' onclick =\"ShowPopupMapLink5('" + id + "')\" >" + input + "Gửi thư nhận việc</a></div>";
                    else
                        strResult += "<a style='position:relative' class='btnGuithunhanviec' onclick =\"ShowPopupMapLink5('" + id + "')\" >" + input + "Gửi thư chúc mừng</a></div>";

                    if (uvStatus.NgayGuiMailChucMung.HasValue)
                    {
                        strResult += "<div><strong class='dateks'> Ngày tạo: " + uvStatus.NgayGuiMailChucMung.Value + " </strong></div>";

                    }
                }

            }
            return strResult;
        }
        protected void drop_tructhuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGird(int.Parse(drop_yeucautuyendung.SelectedValue));
            var phongban = blc_user.GetPhongBanByTrucThuoc(int.Parse(drop_tructhuoc.SelectedValue));
            BindPhongBan(phongban);
        }

        protected void drop_trangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGird(int.Parse(drop_yeucautuyendung.SelectedValue));
        }

        protected void drop_yeucautuyendung_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGird(int.Parse(drop_yeucautuyendung.SelectedValue));
        }

        [System.Web.Services.WebMethod]
        public static bool UploadFileUngvien(string path, Guid id)
        {
          
            //string newPath =
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            string newPath = HttpContext.Current.Server.MapPath("/Uploads/UngVien/" + path);

            Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(newPath);
            object missing = System.Reflection.Missing.Value;
            var aa = doc.Content.Text;
            TinhCachCaNhan tccn = new TinhCachCaNhan();
            Quatrinhdaotao qtdt = new Quatrinhdaotao();
            bool isrun = true;
            bool isName = true;
            foreach (Microsoft.Office.Interop.Word.Table tb in doc.Tables)
            {
                
                VM_UngVien vmuv = new VM_UngVien();
                ThongTinViTri ttvt = new ThongTinViTri();
                UngVien uv = blc_user2.GetUngVienByID(id);
                VoChong vc = new VoChong();
                GiaDinh gd = new GiaDinh();
                Con c = new Con();
                Nguoilienhe nlh = new Nguoilienhe();
                KyNang kn = new KyNang();
                QuaTrinhLamViec qtlv1 = new QuaTrinhLamViec();
                QuaTrinhLamViec qtlv2 = new QuaTrinhLamViec();
                QuyenLoiNoiLamViec qlnlv = new QuyenLoiNoiLamViec();
                if (isrun==false)
                {
                    break;
                }
                if (isrun == true)
                {
                    isrun = false;
                }
                for (int row = 1; row <= 73; row++)
                {
                  
                   //Table 1
                    if(row>1 && row <= 6)
                    {
                        //Ứng cử vào vị trí
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text;
                        //Ứng cử vị trí
                        if (row == 2)
                            ttvt.ViTriungTuyen = text.Replace("\r\u0007","");
                        //Thời gian có thể bắt đầu
                        if (row == 6)
                            ttvt.Thoigianbatdau = text.Replace("\r\a", "");
                        try
                        {
                            var cell2 = tb.Cell(row, 2);
                            var text2 = cell2.Range.Text;
                            //Nguồn tin tuyển dụng
                            if (row == 2)
                            {
                                var splt = text2.Split(new string[] { "\t\r" }, StringSplitOptions.None);
                                if (splt[0].Length > 21)
                                {
                                    var splt2 = splt[0].Split(':');
                                    ttvt.Nguontin = splt2[1].Replace("\t", "").Trim();
                                    ttvt.LoaiNguontin = 1;
                                }
                                if (splt[1].Length > 10)
                                {
                                    var splt2 = splt[1].Split(':');
                                    ttvt.Nguontin = splt2[1].Replace("\t", "").Trim();
                                    ttvt.LoaiNguontin = 2;
                                }
                            }
                            if (row == 6)
                            {
                                ttvt.MucLuong = text2.Replace("\r\a", "");
                                vmuv.ThongTinViTri = ttvt;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    //Table 2
                    if (row > 7 && row<16)
                    {
                        try
                        {

                            var cell = tb.Cell(row, 1);
                            var text = cell.Range.Text;
                            var cell2 = tb.Cell(row, 2);
                            var text2 = cell2.Range.Text;
                            //1. THÔNG TIN CÁ NHÂN:
                            if (row == 9)
                            {
                              if(!text.Replace("\r\a", "").ToLower().Contains(uv.HoTen.ToLower()))
                                {
                                    isName = false;
                                    break;
                                }
                                var cell3 = tb.Cell(row, 3);
                                var text3 = cell3.Range.Text;
                                var cell4 = tb.Cell(row, 4);
                                var text4 = cell4.Range.Text;
                                uv.HoTen = text.Replace("\r\a", "");
                                uv.NgaySinh = DateTime.ParseExact(text2.Replace("\r\a", ""), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                                uv.NoiSinh = text3.Replace("\r\a", "");
                                uv.GioiTinh = text4.Replace("\r\a", "");
                            }
                            if (row == 11)
                            {
                                uv.Email = text.Replace("\r\a", "");
                                uv.SoDt = text2.Replace("\r\a", "");
                            }
                            if (row == 13)
                            {
                                var cell3 = tb.Cell(row, 3);
                                var text3 = cell3.Range.Text;
                                var cell4 = tb.Cell(row, 4);
                                var text4 = cell4.Range.Text;
                                var cell5 = tb.Cell(row, 5);
                                var text5 = cell5.Range.Text;
                                uv.Dantoc = text.Replace("\r\a", "");
                                uv.Tongiao = text2.Replace("\r\a", "");
                                uv.CMND = text3.Replace("\r\a", "");
                                uv.NgayCMND = DateTime.ParseExact(text4.Replace("\r\a", ""), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                                uv.NoiCapCMND = text5.Replace("\r\a", "");
                            }
                            if (row == 15)
                            {
                                
                                uv.DCTamTru = text.Replace("\r\a", "");
                                uv.DCThuongTru = text2.Replace("\r\a", "");
                            }
                        }
                        catch
                        {

                        }
                        
                    }
                    //Table 3
                    if (row >= 16)
                    {
                       
                        if (row == 19)
                        {
                            var cell = tb.Cell(row, 1);
                            var text = cell.Range.Text;
                            var cell2 = tb.Cell(row, 2);
                            var text2 = cell2.Range.Text;
                            var cell3 = tb.Cell(row, 3);
                            var text3 = cell3.Range.Text;
                          
                            if (text != "")
                            {
                                gd.TinhTrangHonNhan = 1;
                                vc.HoTenVoChong=text.Replace("\r\a", "");
                                vc.NamSinh = text2 != "" ? int.Parse(text2.Replace("\r\a", "")) : 0;
                                vc.CongTacTai = text3.Replace("\r\a", "");
                                gd.VoChong = vc;
                            }
                            else
                                gd.TinhTrangHonNhan = 2;
                        }
                        if (row == 21)
                        {
                            var cell = tb.Cell(row, 1);
                            var text = cell.Range.Text;
                            var cell2 = tb.Cell(row, 2);
                            var text2 = cell2.Range.Text;
                            var cell3 = tb.Cell(row, 3);
                            var text3 = cell3.Range.Text;
                            if (text != "")
                            {
                               
                                c.HoTen = text.Replace("\r\a", "");
                                c.NamSinh = text2 != "" ? int.Parse(text2.Replace("\r\a", "")) : 0;
                                c.HocTaiTruong = text3.Replace("\r\a", "");
                                gd.lstCon.Add(c);
                            }
                        }
                        if (row == 27)
                        {
                            var cell = tb.Cell(row, 1);
                            var text = cell.Range.Text.Replace("\r\a", ""); ;
                            var cell2 = tb.Cell(row, 2);
                            var text2 = cell2.Range.Text.Replace("\r\a", ""); 
                            var cell3 = tb.Cell(row, 3);
                            var text3 = cell3.Range.Text.Replace("\r\a", ""); 
                            var cell4 = tb.Cell(row,4);
                            var text4 = cell4.Range.Text.Replace("\r\a", ""); 
                            nlh.HoTen = text;
                            nlh.Moiquanhe = text2;
                            nlh.DienThoaiLienLac = text3;
                            nlh.DiaChiCuNgu = text4;
                            gd.Nguoilienhe = nlh;
                        }
                        vmuv.GiaDinh = gd;
                    }
                    //5. QUÁ TRÌNH ĐÀO TẠO 
                  
                    if (row == 32)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");  
                        var getsplTentruong= text.Split(new string[] { "Tên Trường:" }, StringSplitOptions.None);
                      
                        qtdt.Tentruong = getsplTentruong[1].Replace("\t","");
                    }
                    if (row == 33)
                    {
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        qtdt.Namtonghiep = text2;
                        qtdt.Vanbang = text3;
                    }
                    if (row == 34)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", ""); ;
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        qtdt.Nganhhoc = text.Replace("Ngành học:","");
                        qtdt.Xeploai = text2.Replace("Xếp loại:", "");
                    }
                    if (row == 36)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");
                        if (text != "")
                        {
                            var cell2 = tb.Cell(row, 2);
                            var text2 = cell2.Range.Text.Replace("\r\a", "");
                            var cell3 = tb.Cell(row, 3);
                            var text3 = cell3.Range.Text.Replace("\r\a", "");
                            var cell4 = tb.Cell(row, 4);
                            var text4 = cell4.Range.Text.Replace("\r\a", "");
                            var cell5 = tb.Cell(row,5);
                            var text5 = cell5.Range.Text.Replace("\r\a", "");
                            var cell6 = tb.Cell(row,6);
                            var text6 = cell6.Range.Text.Replace("\r\a", "");
                            Vanbangkhac vbk = new Vanbangkhac();
                            vbk.Ten = text;
                            vbk.NganhHoc = text2;
                            vbk.Thoigiandaotao = text3;
                            vbk.Namtotnghiep=text4!=""?int.Parse(text4):0;
                            vbk.XepLoai = text5;
                            vbk.NoiCap = text6;
                            qtdt.lstVanbang.Add(vbk);
                        } 
                    }
                    //Ngoại Ngữ
                    if (row == 41)
                    {
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        if (text2 != "")
                        {
                            var cell3 = tb.Cell(row, 3);
                            var text3 = cell3.Range.Text.Replace("\r\a", "");
                            var cell4 = tb.Cell(row, 4);
                            var text4 = cell4.Range.Text.Replace("\r\a", "");
                            var cell5 = tb.Cell(row, 5);
                            var text5 = cell5.Range.Text.Replace("\r\a", "");
                            var cell6 = tb.Cell(row, 6);
                            var text6 = cell6.Range.Text.Replace("\r\a", "");
                            NgoaiNgu nn = new NgoaiNgu();
                            nn.TenNgoaiNgu = "Anh";
                            nn.Nghe = text2!=""? int.Parse(text2):0;
                            nn.Noi = text3!=""?int.Parse(text3):0;
                            nn.Doc = text4 != "" ? int.Parse(text4) : 0;
                            nn.Viet = text5 != "" ? int.Parse(text5) : 0;
                            nn.GhiChu = text6;
                            qtdt.lstNgoaiNgu.Add(nn);
                        }
                       
                    }
                    vmuv.Quatrinhdaotao = qtdt;
                    //Vi tính
                   
                    if (row >= 45 && row<=51)
                    {
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        var cell4 = tb.Cell(row, 4);
                        var text4 = cell4.Range.Text.Replace("\r\a", "");
                        var cell5 = tb.Cell(row, 5);
                        var text5 = cell5.Range.Text.Replace("\r\a", "");
                        var cell6 = tb.Cell(row, 6);
                        var text6 = cell6.Range.Text.Replace("\r\a", "");
                        if (row == 45)
                        {
                            if (text2 != "")
                                kn.ViTinh = "Giỏi";
                            if (text3 != "")
                                kn.ViTinh = "Khá";
                            if (text4 != "")
                                kn.ViTinh = "TB";
                            if (text5 != "")
                                kn.ViTinh = "Yếu";
                            kn.ViTinhghiChu = text6;
                        }
                        if (row == 46)
                        {
                            if (text2 != "")
                                kn.LanhDao = "Giỏi";
                            if (text3 != "")
                                kn.LanhDao = "Khá";
                            if (text4 != "")
                                kn.LanhDao = "TB";
                            if (text5 != "")
                                kn.LanhDao = "Yếu";
                            kn.LanhDaoghiChu = text6;
                        }
                        if (row == 47)
                        {
                            if (text2 != "")
                                kn.GiaiQuyetVanDe = "Giỏi";
                            if (text3 != "")
                                kn.GiaiQuyetVanDe = "Khá";
                            if (text4 != "")
                                kn.GiaiQuyetVanDe = "TB";
                            if (text5 != "")
                                kn.GiaiQuyetVanDe = "Yếu";
                            kn.GiaiQuyetVanDeGhiChu = text6;
                        }
                        if (row == 48)
                        {
                            if (text2 != "")
                                kn.TrinhBay = "Giỏi";
                            if (text3 != "")
                                kn.TrinhBay = "Khá";
                            if (text4 != "")
                                kn.TrinhBay = "TB";
                            if (text5 != "")
                                kn.TrinhBay = "Yếu";
                            kn.TrinhBayGhiChu = text6;
                        }
                        if (row == 49)
                        {
                            if (text2 != "")
                                kn.LamViecDocLap = "Giỏi";
                            if (text3 != "")
                                kn.LamViecDocLap = "Khá";
                            if (text4 != "")
                                kn.LamViecDocLap = "TB";
                            if (text5 != "")
                                kn.LamViecDocLap = "Yếu";
                            kn.LamViecDocLapGhiChu = text6;
                        }
                        if (row == 50)
                        {
                            if (text2 != "")
                                kn.SinhHoat = "Giỏi";
                            if (text3 != "")
                                kn.SinhHoat = "Khá";
                            if (text4 != "")
                                kn.SinhHoat = "TB";
                            if (text5 != "")
                                kn.SinhHoat = "Yếu";
                            kn.SinhHoatGhiChu = text6;
                        }
                        if (row == 51)
                        {
                            if (text2 != "")
                                kn.HoatDong = "Giỏi";
                            if (text3 != "")
                                kn.HoatDong = "Khá";
                            if (text4 != "")
                                kn.HoatDong = "TB";
                            if (text5 != "")
                                kn.HoatDong = "Yếu";
                            kn.HoatDongGhiChu = text6;
                        }
                    }
                    vmuv.KyNang = kn;
                  
                    if (row == 53)
                    {
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", ""); 
                        while (text3.Contains("\r"))
                        {
                            text3= text3.Replace("\r", "");
                        }
                        tccn.NangLucVuotTroi = text3.Replace("\r", "");
                    }
                    if (row == 54)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", ""); 
                        tccn.DiemYeu = text;
                        tccn.DiemManh = text2; 
                        vmuv.TinhCachCaNhan = tccn;
                    }
                    // QUÁ TRÌNH LÀM VIỆC 
                    if (row == 56)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        if (text != "")
                        {
                            //Get tencty
                            var spldiachi= text.Split(new string[] { "Địa chỉ:" }, StringSplitOptions.None);
                            var spltencty = spldiachi[0].Split(':');
                            qtlv1.TenCongTy = spltencty[1].Replace("\t\r", "").Trim();
                            //Get dia chi
                            var spldienthoai = spldiachi[1].Split(new string[] { "Điện thoại:" }, StringSplitOptions.None);
                            qtlv1.DiaChi = spldienthoai[0].Replace("\t\r", "").Trim();
                            qtlv1.DienThoaiCongTy = spldienthoai[1].Replace("\t", "");
                            //Thoi gian lam viec
                            qtlv1.NganhNgheHoatDong = text2.Replace("Ngành nghề/ lĩnh vực Cty hoạt động :","");
                            var splden=text3.Split(new string[] { "Đến:" }, StringSplitOptions.None);
                            var splthang=splden[0].Split(new string[] { "tháng" }, StringSplitOptions.None);
                            var splthang2 = splden[1].Split(new string[] { "tháng" }, StringSplitOptions.None);
                            var lastu= splthang[1].Split(new string[] { "năm" }, StringSplitOptions.None);
                            var lastu2 = splthang2[1].Split(new string[] { "năm" }, StringSplitOptions.None);
                            qtlv1.Fromdate = lastu[0].Replace(".","").Replace("…", "")+"/"+lastu[1].Replace("\r", "").Trim(); 
                            qtlv1.Todate = lastu2[0].Replace(".", "").Replace("…", "") + "/" + lastu2[1].Replace("\r", "").Trim();

                        }
                    }
                    if (row == 57)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        qtlv1.Chucvumoivao = text.Replace("Chức vụ/Công việc mới vào:", "").Replace("\r","").Replace("\t","");
                        qtlv1.Chucvusaucung = text2.Replace("Chức vụ/ Công việc sau cùng:", "").Replace("\r", "").Replace("\t", "");
                        qtlv1.Trachnhiem = text3.Replace("Nhiệm vụ và trách nhiệm chính:", "");
                        
                    }
                    if (row == 58)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("Lương khởi điểm:", "").Trim();
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("Lương sau cùng:", "").Trim();
                        qtlv1.Luongkhoidiem =text!=""?int.Parse(text.Replace("\r\a", "")):0;
                        qtlv1.Luongsaucung = text != "" ? int.Parse(text2.Replace("\r\a", "")) : 0;
                    }
                    if (row == 59)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("Lương khởi điểm:", "").Trim();
                        //ho ten nguoi quan lý
                        var splichucvu=text.Split(new string[] { "Chức vụ:" }, StringSplitOptions.None);
                        qtlv1.HotenNQL = splichucvu[0].Replace("\t\r", "").Replace("Họ tên người quản lý trực tiếp:", "");
                        var spltdt= splichucvu[1].Split(new string[] { "Điện thoại:" }, StringSplitOptions.None);
                        var kt = spltdt[0].Trim();
                        while (kt.Contains("…"))
                        {
                            kt = kt.Replace("…", "");
                        }
                        qtlv1.ChucvuNQL = kt;
                        qtlv1.DienthoaiNQl = spltdt[1].Trim().Replace("\t\r\a", "");
                    }
                    if (row == 60)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");
                        qtlv1.LyDoNghiViec = text.Replace("Lý do nghỉ việc: ","").Trim();
                        vmuv.lstQuaTrinhLamViec.Add(qtlv1);
                    }
                    //Cong ty 2
                    if (row == 61)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        //
                        //Get tencty
                        var spldiachi = text.Split(new string[] { "Địa chỉ:" }, StringSplitOptions.None);
                        var spltencty = spldiachi[0].Split(':');
                        qtlv2.TenCongTy = spltencty[1].Replace("\t\r", "").Trim();
                        if (qtlv2.TenCongTy != "")
                        {
                            //Get dia chi
                            var spldienthoai = spldiachi[1].Split(new string[] { "Điện thoại:" }, StringSplitOptions.None);
                            qtlv2.DiaChi = spldienthoai[0].Replace("\t\r", "").Trim();
                            qtlv2.DienThoaiCongTy = spldienthoai[1].Replace("\t", "");
                            //Thoi gian lam viec
                            qtlv2.NganhNgheHoatDong = text2.Replace("Ngành nghề/ lĩnh vực Cty hoạt động :", "");
                            var splden = text3.Split(new string[] { "Đến:" }, StringSplitOptions.None);
                            var splthang = splden[0].Split(new string[] { "tháng" }, StringSplitOptions.None);
                            var splthang2 = splden[1].Split(new string[] { "tháng" }, StringSplitOptions.None);
                            var lastu = splthang[1].Split(new string[] { "năm" }, StringSplitOptions.None);
                            var lastu2 = splthang2[1].Split(new string[] { "năm" }, StringSplitOptions.None);
                            qtlv2.Fromdate = lastu[0].Replace(".", "").Replace("…", "") + "/" + lastu[1].Replace("\r", "").Trim();
                            qtlv2.Todate = lastu2[0].Replace(".", "").Replace("…", "") + "/" + lastu2[1].Replace("\r", "").Trim();

                        }
                    }
                    if (row == 62 && qtlv2.TenCongTy != "")
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        qtlv2.Chucvumoivao = text.Replace("Chức vụ/Công việc mới vào:", "").Replace("\r", "").Replace("\t", "");
                        qtlv2.Chucvusaucung = text2.Replace("Chức vụ/ Công việc sau cùng:", "").Replace("\r", "").Replace("\t", "");
                        qtlv2.Trachnhiem = text3.Replace("Nhiệm vụ và trách nhiệm chính:", "");
                    }
                    if (row == 63 && qtlv2.TenCongTy != "")
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("Lương khởi điểm:", "").Trim();
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("Lương sau cùng:", "").Trim();
                        qtlv2.Luongkhoidiem = text != "" ? int.Parse(text.Replace("\r\a", "")) : 0;
                        qtlv2.Luongsaucung = text != "" ? int.Parse(text2.Replace("\r\a", "")) : 0;
                    }
                    if (row == 64 && qtlv2.TenCongTy != "")
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("Lương khởi điểm:", "").Trim();
                        //ho ten nguoi quan lý
                        var splichucvu = text.Split(new string[] { "Chức vụ:" }, StringSplitOptions.None);
                        qtlv2.HotenNQL = splichucvu[0].Replace("\t\r", "").Replace("Họ tên người quản lý trực tiếp:","");
                        var spltdt = splichucvu[1].Split(new string[] { "Điện thoại:" }, StringSplitOptions.None);
                        var kt = spltdt[0].Trim();
                        while (kt.Contains("…"))
                        {
                            kt = kt.Replace("…", "");
                        }
                        qtlv2.ChucvuNQL = kt;
                        qtlv2.DienthoaiNQl = spltdt[1].Trim().Replace("\t\r\a", "");
                    }
                    if (row == 65 && qtlv2.TenCongTy != "")
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");
                        qtlv2.LyDoNghiViec = text.Replace("Lý do nghỉ việc: ", "").Trim();
                        vmuv.lstQuaTrinhLamViec.Add(qtlv2);
                    }
                    //Mục tieu 73
                    if (row == 67)
                    {
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        var cell4 = tb.Cell(row, 4);
                        var text4 = cell4.Range.Text.Replace("\r\a", "");
                        if (text2 != "")
                            qlnlv.DuocSuDungXe = 1;
                        if (text3 != "")
                            qlnlv.DuocSuDungXe = 2;
                        if (text4 != "")
                            qlnlv.DuocSuDungXe = 3;
                    }
                    if (row == 68)
                    {
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        var cell4 = tb.Cell(row, 4);
                        var text4 = cell4.Range.Text.Replace("\r\a", "");
                        if (text2 != "")
                            qlnlv.PhuCapDiLai = 1;
                        if (text3 != "")
                            qlnlv.PhuCapDiLai = 2;
                        if (text4 != "")
                            qlnlv.PhuCapDiLai = 3;
                    }
                    if (row == 69)
                    {
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        var cell4 = tb.Cell(row, 4);
                        var text4 = cell4.Range.Text.Replace("\r\a", "");
                        if (text2 != "")
                            qlnlv.DienThoai = 1;
                        if (text3 != "")
                            qlnlv.DienThoai = 2;
                        if (text4 != "")
                            qlnlv.DienThoai = 3;
                    }
                    if (row == 70)
                    {
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        var cell4 = tb.Cell(row, 4);
                        var text4 = cell4.Range.Text.Replace("\r\a", "");
                        if (text2 != "")
                            qlnlv.TienThuong = 1;
                        if (text3 != "")
                            qlnlv.TienThuong = 2;
                        if (text4 != "")
                            qlnlv.TienThuong = 3;
                    }
                    if (row == 71)
                    {
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        var cell3 = tb.Cell(row, 3);
                        var text3 = cell3.Range.Text.Replace("\r\a", "");
                        var cell4 = tb.Cell(row, 4);
                        var text4 = cell4.Range.Text.Replace("\r\a", "");
                        if (text2 != "")
                            qlnlv.TienVay = 1;
                        if (text3 != "")
                            qlnlv.TienVay = 2;
                        if (text4 != "")
                            qlnlv.TienVay = 3;
                    }
                    if (row == 73)
                    {
                        var cell = tb.Cell(row, 1);
                        var text = cell.Range.Text.Replace("\r\a", "");
                        var cell2 = tb.Cell(row, 2);
                        var text2 = cell2.Range.Text.Replace("\r\a", "");
                        qlnlv.MucTieuPhatTrien = text.Replace("\t", "").Replace("\r", "");
                        qlnlv.ViSaoBanMuon = text2.Replace("\t","").Replace("\r", "");
                        vmuv.QuyenLoiNoiLamViec = qlnlv;
                    }
                    // text now contains the content of the cell.
                }
                vmuv.UngVien = uv;
                blc_user2.InsertUngVien(vmuv);
                blc_user2.CapnhatUngVienImport(id,path);
                //Cập nhật status Import

            } 
           
            //doc.Content.Text += "Nội dung file";
            app.Visible = true;    //Optional
            //doc.Save(); 
            doc.Close();

            //CLEAN UP
            GC.Collect();
            GC.WaitForPendingFinalizers(); 
            app.Quit();
            if (app != null)
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(app);
            if (isName)
            return true;
            return false;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGird(int.Parse(drop_yeucautuyendung.SelectedValue));
        }

        protected void dropTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGird(int.Parse(drop_yeucautuyendung.SelectedValue));

        }

        private bool sendEmail(string to, string title, string sContent, string file)
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
                mail.To.Add(to);
                TUser user = blc_user.GetUser_ByIDAll(this.UserMemberID);
                mail.To.Add(user.Email);
                //cc
                var mailsend = blc_user.GetListEmail(1, 1).FirstOrDefault();
                var lstmailCC = blc_user.GetListEmail(1, 2).ToList();
                //
                //mail.CC.Add(new MailAddress(mailsend.Email));
                //foreach (var its in lstmailCC)
                //{
                //    mail.CC.Add(new MailAddress(its.Email));
                //}
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(file);
                mail.Attachments.Add(attachment);
                mail.Subject = title;
                mail.Body = sContent;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                //Cap nhật ngày phỏng vấn

                string close = @"<script type='text/javascript'>
                                window.opener.location.reload(true);
                                self.close();
                                </script>";
                base.Response.Write(close);

                return true;

            }
            catch (System.Exception ex)
            {
                Alert.Show(ex.Message);
                //error = ex.Message;
                return false;
            }
        }
    }
}