using Newtonsoft.Json;
using PQT.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using UserMng.DAC;
using PQT.DAC.ViewModel;
namespace WebCus
{
    public partial class TTDongbonhanvien : System.Web.UI.UserControl
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGird();
            }
        }
        private void BindGird()
        {
            int userid = 0;

            IList<UngVien> list = blc_user.GetListUngVien().Where(m => m.Status == 2 && m.IdNhanVien==null).ToList();
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }
        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid idNTD = Guid.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "DongBoItem")
            {
                UngVien ent = blc_user.GetUngVienByID(idNTD);
                if (ent != null)
                {
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
                        //Ngay moi nhan viec
                        Ungvien_Trangthai uvtt = blc_user.GetUngvien_TrangthaiById(idNTD);
                        if(uvtt!=null)
                        ttuv.NgayVaoLam = uvtt.NgayGuithumoi;
                        YeuCauTuyenDung yctd = blc_user.GetIDPhongBanByYeuCau(ent.IdYeuCau.Value);
                        if (yctd != null)
                        {
                            ttuv.PhongBan = yctd.IDPhongBan;
                            ttuv.IDCTY = yctd.TrucThuoc.Value;
                        }
                        blc_user.CreateTTNhanSu(ttuv);
                        //User
                        TUser tuser = new TUser();
                        var strName = ent.HoTen.Split(' ');
                        var strplsu = "";
                        if (strName != null)
                        {
                            tuser.LoginID = strName[strName.Length - 1];

                            for (int i = strName.Length - 1; i > 0; i--)
                            {
                                if (i == strName.Length - 1)
                                {
                                    strplsu += strName[strName.Length - 1];
                                }
                                else
                                {
                                    strplsu += strName[strName.Length - 1].Substring(0, 1);
                                }
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
        public string checkchucnang(string status, string id)
        {
            if (status == "-1")
            {
                return "<a style='color:red'>Không trúng tuyển</a>";
            }
            if (status == "0")
            {
                return "<a onclick=\"ShowPopupMapLink('" + Guid.Parse(id) + "')\" style='color:blue'>Duyệt</a>";
            }

            if (status == "1")
            {
                return "<a onclick=\"ShowPopupMapLink2('" + Guid.Parse(id) + "')\" style='color:blue'>Đánh giá</a>";
            }
            if (status == "2")
            {
                return "<a onclick=\"ShowPopupMapLink3('" + Guid.Parse(id) + "')\" style='color:blue'>Gửi mail</a>";
            }
            return "";
        }
    }
}