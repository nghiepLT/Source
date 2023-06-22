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

namespace WebCus
{
    public partial class TTThongtinUngvien : System.Web.UI.UserControl
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
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
            foreach(var item in lstPhongban)
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
            
            //Alert.Show(trangthai.ToString());
            List<VM_UngvienStatus> list = blc_user.Laydanhsachungvien(idyeucautuyendung).OrderByDescending(m=>m.CreatedDate).Where(m=>m.TrucThuoc==tructhuoc).ToList();
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
                    NhanVien dbnv=new NhanVien();
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
                        if(Quatrinhdaotao.Vanbang=="THPT")
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
                            if(GiaDinh.lstCon!=null && GiaDinh.lstCon.Count() > 0)
                            {
                                foreach(var item in GiaDinh.lstCon)
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
                        TUser tuser = new TUser();
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
                        if (result ==true)
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
        public string checktrangthai(string status,string  id)
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
            if(GuiThumoi=="1")
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
            string strResult = "";
            strResult += " <div>";
            string input = "";
            if (uvStatus.NgayNhapThongTin.HasValue)
                input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
            else
                input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";
            strResult += "<a style='position:relative' class='btnnhapthongtin' target=\"_blank\" href='thongtinungviendutuyen.aspx?id=" + id + "'>"+ input+ " Link nhập thông tin</a> <a onclick=\"importf('" + id + "')\">Import file</a></div>";
            strResult+="<div class='NgayTaoChung'>";
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
                if(uvStatus.NgàyPV.HasValue)
                strResult += "<a style='position:relative;pointer-events: none;' class='btnphongvan' onclick =\"ShowPopupMapLink('" + id + "')\" >"+ input+" Mời phỏng vấn</a></div>";
                else
                strResult += "<a style='position:relative' class='btnphongvan' onclick =\"ShowPopupMapLink('" + id + "')\" >"+ input+" Mời phỏng vấn</a></div>";

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
                if (uvStatus.NgayDanhgia.HasValue)
                    input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                else
                    input += "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\"/>";

                strResult += "<a style='position:relative' class='btnDanhgia' onclick =\"ShowPopupMapLink2('" + id + "')\" >"+ input+"Đánh giá </a></div>";
                strResult += "<a style='position:relative' class='btnDanhgia' onclick =\"ShowPopupMapLinkDanhGia1('" + id + "','" + 1 + "','" + this.UserMemberID + "')\" >" + input+"Người PV Đánh Giá </a></div>";

                if (uvStatus.NgayDanhgia.HasValue)
                {
                    strResult += "<div><strong class='dateks'> Ngày tạo: " + uvStatus.NgayDanhgia.Value + " </strong></div>";
                    //if (dgtd.Ketqualan1 == 1)
                    //    strResult += "Trang thái :  <strong>Đạt</strong>";
                    //else
                    //{
                    //    if (dgtd.Quyetdinhlan1 != null)
                    //        strResult += "<strong>"+dgtd.Quyetdinhlan1+"</strong>";
                    //    else
                    //        strResult += "Trang thái :  <strong>Chưa Đạt</strong>";
                    //}
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
                    strResult += "<div><strong class='dateks'>" + input+" Ngày đồng bộ: " + uvStatus.Ngaydongbo.Value + " </strong></div>";
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
                string ip1 = "", ip2 = "", ip3="";

                if (blc_user.checkUngVienQuytrinhhuongdan(tuser.UserID))
                    ip1 = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                if (blc_user.checkungVienChedophucloi(tuser.UserID))
                    ip2 = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";

                if (blc_user.checkungVienKhaosat(uv.IdNhanVien.Value))
                    ip3 = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";

                strResult += "<div><a style='position:relative;padding-left: 30px;' href='Tomtatquytrinh.aspx?id=" + id+"' target='_blank' class='btnTomTatQuyTrinh'>"+ ip1+"Tóm tắt quy trình</a></div>";
                strResult += "<div><a style='position:relative;padding-left: 30px;' href='Tomtatphucloi.aspx?id=" + id + "' target='_blank' class='btnTomtatphucloi'>"+ ip2+"Tóm tắt phúc lợi</a></div>";
                strResult += "<div><a style='position:relative;padding-left: 30px;' href='Khaosathoinhap.aspx?id=" + id + "' target='_blank' class='btnKhaosat'>"+ip3+"Khảo sát</a></div>";
               
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
            Danhgiathuviec dgtv = blc_user.GetDanhgiathuviec(id);
            if (uvStatus.NgayKhaosatHoiNhap.HasValue)
            {
                string inputnguoidanhgia = "";
                string inputtruongphong = "";
                string inputnhansu = "";
                string inputbangiamdoc = "";
               

                if (dgtv!=null && dgtv.NgayNguoiDanhGia!=null)
                    inputnguoidanhgia= "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                if (dgtv != null && dgtv.NgayTruongPhong != null)
                    inputtruongphong = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                if (dgtv != null && dgtv.NgayHCNS != null)
                    inputnhansu = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                if (dgtv != null && dgtv.NgayBanGiamDoc != null)
                    inputbangiamdoc = "<input class='inputStatus' style='pointer-events:none;' type=\"checkbox\" checked />";
                strResult += "<div style='text-align: left;'><a style='position:relative;padding-left: 32px;width: auto;' class='btnTomtatphucloi' onclick =\"ShowPopupMapLink6('" + id + "','"+1+ "','" + this.UserMemberID + "')\" >" + inputnguoidanhgia + "Người đánh giá "+ (((dgtv != null && dgtv.MailNguoiDanhGia.HasValue) && dgtv.MailNguoiDanhGia.Value == 1) ? "( Đã gửi mail )" : "") +"</a></div>";
                strResult += "<div style='text-align: left;'><a style='position:relative;padding-left: 32px;width: auto;' class='btnTomtatphucloi' onclick =\"ShowPopupMapLink6('" + id + "','" + 2 + "','" + this.UserMemberID + "')\" >" + inputtruongphong + "Trưởng phòng chuyên môn" + (((dgtv != null && dgtv.MailTruongPhong.HasValue) && dgtv.MailTruongPhong.Value == 1) ? "( Đã gửi mail )" : "") + "</a></div>";
                strResult += "<a style='position:relative;padding-left: 27px;width: auto;' class='btnTomtatphucloi' onclick =\"ShowPopupMapLink4('" + id + "')\" >" + inputnhansu + "Phòng nhân sự</a></div>";
                strResult += "<div style='text-align: left;'><a style='position:relative;padding-left: 27px;width: auto;' class='btnTomtatphucloi' onclick =\"ShowPopupMapLink6('" + id + "','" + 4 + "','" + this.UserMemberID + "')\" >" + inputbangiamdoc + "Ban giám đốc" + (((dgtv != null && dgtv.MailBanGiamDoc.HasValue) && dgtv.MailBanGiamDoc.Value == 1) ? "( Đã gửi mail )" : "") + "</a></div>";
            }
           
            if (dgtv!=null)
            {
                if (dgtv.NgayNguoiDanhGia != null)
                {
                    TUser ndg = blc_user.GetUser_ByIDAll(dgtv.IdNguoiDanhGia.Value);
                    if (ndg != null)
                    {
                        strResult += "<div><strong class='dateks'>" + " Người đánh giá: " + ndg.UserName+" "+ dgtv.NgayNguoiDanhGia.Value + " </strong></div>";
                    }
                    
                }
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
                if (dgtv!=null && dgtv.NgayBanGiamDoc != null)
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
        public static bool UploadFileUngvien(string path)
        {
            return true;
        }
    }
}