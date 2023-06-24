using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using PQT.API;
using PQT.API.Connection;
using UserMng.DAC;
using UserMng.DataDefine;
using System.Data;
using PQT.DAC;
using PQT.Common;
using PQT.API.File;
using PQT.DAC.ViewModel;
using Newtonsoft.Json;
namespace UserMng.BLC
{
    public class UserMngOther_BLC : XVNET_ModuleControl
    {
        UserMngDataDataContext da = null;
        UserMng_BLC_NTX user_ntx = new UserMng_BLC_NTX();
        public UserMngOther_BLC()
        {
            if (da == null)
            {
                da = new UserMngDataDataContext();
            }
        }
        #region YeuCauTuyenDung
        public IList<YeuCauTuyenDung> ListYCTD()
        {
            return da.YeuCauTuyenDungs.OrderByDescending(m => m.NgayTao).ToList();
        }
        public IList<VM_YeuCauTuyenDung> ListVMYCTD()
        {
            var model = (from yctd in da.YeuCauTuyenDungs.ToList()
                         join pb in da.PhongBans.ToList()
                         on yctd.IDPhongBan equals pb.IDPhong
                         where yctd.TrangThai != 4
                         select new VM_YeuCauTuyenDung()
                         {
                             IdYeuCau = yctd.IdYeuCau,
                             TieuDe = yctd.NgayTao.Value.ToShortDateString() + " - " + pb.TenPhong + " - " + yctd.TieuDe,
                             NgayTao = yctd.NgayTao.Value,
                             TrucThuoc=yctd.TrucThuoc.Value
                         }
                       );
            return model.OrderByDescending(m => m.NgayTao).ToList();
        }
        public string GetTrangThaiTuyenDung(int IdYeuCau)
        {
            string result = "";
            //Kiem tra so luong ung vien dang tuyen dung
            YeuCauTuyenDung yctd = da.YeuCauTuyenDungs.Where(m => m.IdYeuCau == IdYeuCau).FirstOrDefault();
            int soluong = yctd.Soluong.Value;
            var lstUngVien = da.UngViens.Where(m => m.IdYeuCau == IdYeuCau).Where(m => m.TrangThaiPhongVan == 1 && m.TrangThaiNhanViec == 0 || m.TrangThaiPhongVan == 1 && m.TrangThaiNhanViec == 5).ToList();
            int soluongthucte = lstUngVien.Count();
            if(soluongthucte==0)
            {
                result = "Chưa tuyển dụng"+" "+"(0/"+soluong+")";
            }
            else
            {
                if (yctd.IsDone !=1)
                {
                    result = "Đang tuyển dụng" + " " + "(" + soluongthucte + "/" + soluong + ")";

                }
                else
                {
                    result = "<strong>Đã hoàn thành</strong>" + " " + "(" + soluongthucte + "/" + soluong + ")";

                }
            }
            return result;
        }
        public IList<VM_YeuCauTuyenDung> ListYeuCauTuyenDung(int UserMemberID, int TrucThuoc, int TrangThai)
        {
            var model = (from yc in da.YeuCauTuyenDungs.ToList()
                         join pb in da.PhongBans.ToList()
                         on yc.IDPhongBan equals pb.IDPhong
                         join us in da.TUsers
                         on yc.IdNguoiTao equals us.UserID
                         join nv in da.NhanViens
                         on us.IdNhansu equals nv.IdNhanVien
                         where (UserMemberID == 0 || (UserMemberID != 0 && yc.IdNguoiTao == UserMemberID))
                         && (TrucThuoc == -1 || (yc.TrucThuoc == TrucThuoc))
                          && ((TrangThai == -1 && yc.TrangThai != 4) || (yc.TrangThai == TrangThai))
                         select new VM_YeuCauTuyenDung()
                         {
                             Description = yc.Description,
                             Files = yc.Files,
                             IdNguoiTao = yc.IdNguoiTao.Value,
                             IdYeuCau = yc.IdYeuCau,
                             NgayTao = yc.NgayTao.Value,
                             TenPhong = pb.TenPhong,
                             TieuDe = yc.TieuDe,
                             TrangThai = yc.TrangThai.Value,
                             TrucThuoc = yc.TrucThuoc.Value,
                             HoTen = nv.HoTen,
                             Soluong = yc.Soluong.Value,
                             Reason = yc.Reason,
                             TrangThaiTuyenDung = GetTrangThaiTuyenDung(yc.IdYeuCau),
                             IsDone= yc.IsDone.HasValue? yc.IsDone.Value:0
                         }
                       ).ToList();
            return model.OrderBy(z => z.IdYeuCau).ToList();
        }
        public int CreateYeuCauTD(string TieuDe, int Soluong, string NoiDung, int IDPhongBan, int TruThuoc, string Files, int UserMemberID)
        {
            YeuCauTuyenDung objEnt = null;
            objEnt = new YeuCauTuyenDung();

            objEnt.TieuDe = TieuDe;
            objEnt.NoiDung = NoiDung;
            objEnt.NgayTao = DateTime.Now;
            objEnt.NgayCapNhat = DateTime.Now;
            objEnt.TrangThai = 1;
            objEnt.IDPhongBan = IDPhongBan;
            objEnt.TrucThuoc = TruThuoc;
            objEnt.Files = Files;
            objEnt.IdNguoiTao = UserMemberID;
            objEnt.Soluong = Soluong;
            da.YeuCauTuyenDungs.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.IdYeuCau;
        }
        public bool UpdateYeuCauTD(int IdYeuCau, string TieuDe, int Soluong, string NoiDung, int TrangThai, string Reason, int IDPhongBan, int TruThuoc, string Files, int UserMemberID)
        {
            try
            {
                IList<YeuCauTuyenDung> list = da.YeuCauTuyenDungs.Where(z => z.IdYeuCau == IdYeuCau).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.TieuDe = TieuDe;
                    objVlaue.NoiDung = NoiDung;
                    objVlaue.TrangThai = TrangThai;
                    objVlaue.NgayCapNhat = DateTime.Now;
                    objVlaue.IDPhongBan = IDPhongBan;
                    objVlaue.TrucThuoc = TruThuoc;
                    objVlaue.Files = Files;
                    objVlaue.Soluong = Soluong;
                    objVlaue.Reason = Reason;
                    if (TrangThai == 2)
                        objVlaue.IdNguoiDuyet = UserMemberID;
                    objVlaue.IDNguoicapnhat = UserMemberID;
                    da.NguonTuyenDungs.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public YeuCauTuyenDung GetYeuCauTD_ByID(int IdYeuCau)
        {
            try
            {
                da = new UserMngDataDataContext();
                IList<YeuCauTuyenDung> list = da.YeuCauTuyenDungs.Where(z => z.IdYeuCau == IdYeuCau).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public bool DeleteYeuCauTD(int IdYeuCau)
        {
            try
            {
                IList<YeuCauTuyenDung> list = da.YeuCauTuyenDungs.Where(z => z.IdYeuCau == IdYeuCau).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.IsShow = 0;
                    objVlaue.TrangThai = 4;
                    da.YeuCauTuyenDungs.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region Territory

        public IList<TTerritory> ListTerri()
        {
            return da.TTerritories.Where(z => z.Status != 3).OrderBy(z => z.SortOrder).ToList();
        }

        public IList<TTerritory> ListTerri_byArea(int AreaID)
        {
            return da.TTerritories.Where(z => z.Status != 3 && (z.AreaID == AreaID || AreaID == -1)).OrderBy(z => z.SortOrder).ToList();
        }

        public TTerritory GetTerri_ByID(int TerriID)
        {
            try
            {
                IList<TTerritory> list = da.TTerritories.Where(z => z.TerritoryID == TerriID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public int CreateTerri(string TerritoryName, int TerriParent, int Status, int AreaID, int SortOrder)
        {
            TTerritory objEnt = null;
            objEnt = new TTerritory();

            objEnt.TerritoryName = TerritoryName;
            objEnt.TerriParent = TerriParent;
            objEnt.Status = Status;
            objEnt.AreaID = AreaID;
            objEnt.SortOrder = SortOrder;

            da.TTerritories.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.TerritoryID;
        }

        public bool UpdateTerri(int TerritoryID, string TerritoryName, int TerriParent, int Status, int AreaID, int SortOrder)
        {
            try
            {
                IList<TTerritory> list = da.TTerritories.Where(z => z.TerritoryID == TerritoryID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.TerritoryName = TerritoryName;
                    objVlaue.TerriParent = TerriParent;
                    objVlaue.Status = Status;
                    objVlaue.AreaID = AreaID;
                    objVlaue.SortOrder = SortOrder;

                    da.TTerritories.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool DeleteTerri(int TerritoryID)
        {
            try
            {
                IList<TTerritory> list = da.TTerritories.Where(z => z.TerritoryID == TerritoryID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.Status = 3;

                    da.TAreas.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Area


        public DataTable ListUserOfKeywork_by_IdCase(int @keyID)
        {
            string sql = string.Format("[p_TUsertoKeyworkByIDKey_Rows] @keyID={0}",
                @keyID);
            return (new ConnectSQL()).connect_dt(sql);
        }
        public IList<UsertoKeywork> ListUserOfKeywork_by_IdUser(int iduser)
        {
            return da.UsertoKeyworks.Where(z => z.IdUser == iduser).OrderBy(z => z.id).ToList();
        }

        public IList<TMaCongViec> Listmacongviec()
        {
            return da.TMaCongViecs.Where(z => z.IDMaCV != -1).OrderBy(z => z.MaCV).ToList();
        }
        public IList<TArea> ListArea()
        {
            return da.TAreas.Where(z => z.Status != 3).OrderBy(z => z.SortOrder).ToList();
        }
        public IList<NhanVien> ListNhanvien()
        {
            return da.NhanViens.Where(z => z.IdNhanVien != 0).OrderBy(z => z.IdNhanVien).ToList();
        }
        public IList<PhongBan> ListPhongban_byCTY(int idCty)
        {
            if (idCty != -1)
                return da.PhongBans.Where(z => z.TrucThuoc == idCty).OrderBy(z => z.TrucThuoc).ToList();
            else return da.PhongBans.Where(z => z.TrucThuoc != idCty).OrderBy(z => z.TrucThuoc).ToList();
        }
        public IList<ThongTinNguoiThan> ListNguoithan_byNV(int idNV)
        {
            return da.ThongTinNguoiThans.Where(z => z.IdNhanVien == idNV).OrderBy(z => z.Id).ToList();
        }
        public IList<ThongTinKyLuatKhenThuong> ListThongtinKyLuat_byNV(int idNV)
        {
            return da.ThongTinKyLuatKhenThuongs.Where(z => z.IdNhanVien == idNV).OrderBy(z => z.Id).ToList();
        }
        public IList<PhongBan> ListPhongban()
        {
            return da.PhongBans.OrderBy(z => z.TrucThuoc).ToList();
        }
        public IList<NguonTuyenDung> ListNguonTuyenDung()
        {
            return da.NguonTuyenDungs.OrderBy(z => z.Id).ToList();
        }
        public string CountCase_by_IDUser(string idcase, int iduser, DateTime fromdate, DateTime todate)
        {

            return da.CountCase_by_IDUser(idcase, iduser, fromdate, todate).FirstOrDefault().Column1.ToString();
        }
        public IList<TPoinKM> ListPoinKM()
        {
            return da.TPoinKMs.Where(z => z.IDKm != 0).OrderBy(z => z.IDKm).ToList();
        }
        public IList<TValueofLevel> ListValueofLevel()
        {
            return da.TValueofLevels.ToList();
        }
        public IList<TMaCongViec> ListWorkValue(int idnhom)
        {
            return da.TMaCongViecs.Where(z => z.IDMaCV != -1 && (z.NhomCase == idnhom || idnhom == -1)).OrderBy(z => z.IDMaCV).ToList();
        }
        public TArea GetArea_ByID(int AreaID, int status)
        {
            try
            {
                IList<TArea> list = da.TAreas.Where(z => z.TAreaID == AreaID && (z.Status == status || status == -1)).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public PhongBan GetPhongBan_ByID(int idphong)
        {
            try
            {
                IList<PhongBan> list = da.PhongBans.Where(z => z.IDPhong == idphong).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public PhongBan GetPhongBan_ByName(string namephong, int idcty)
        {
            try
            {
                IList<PhongBan> list = da.PhongBans.Where(z => z.TenPhong.Trim().ToUpper() == namephong.Trim().ToUpper() && z.TrucThuoc == idcty).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public NguonTuyenDung GetNguonTD_ByID(int idnguontd)
        {
            try
            {
                IList<NguonTuyenDung> list = da.NguonTuyenDungs.Where(z => z.Id == idnguontd).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public NgayPhep GetNgayPhep_ByID(int IDPhep)
        {
            try
            {
                IList<NgayPhep> list = da.NgayPheps.Where(z => z.IDPhep == IDPhep).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                else return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public decimal GetNgayPhepNam_ByIDNV(int IDNV)
        {
            try
            {
                decimal tongsongaynghi = 0;
                IList<NgayPhep> list = da.NgayPheps.Where(z => z.IDNhanvien == IDNV && (DateTime.Parse("01/01/" + DateTime.Now.Year) <= z.TuNgay && z.TuNgay <= DateTime.Parse("31/12/" + DateTime.Now.Year)) && (z.LoaiPhep == 2 || z.LoaiPhep == 1)).ToList();
                if (list.Count > 0)
                {
                    foreach (NgayPhep author in list)
                    {
                        tongsongaynghi = tongsongaynghi + decimal.Parse(author.SoNgayNghi.ToString());
                    }
                    return tongsongaynghi;
                }
                else return 0;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
        public decimal GetNgayPhepThang_ByIDNV(int IDNV)
        {
            try
            {
                DateTime dtime = DateTime.Now;
                var firstDayOfMonth = new DateTime(dtime.Year, dtime.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                decimal tongsongaynghi = 0;
                IList<NgayPhep> list = da.NgayPheps.Where(z => (firstDayOfMonth.Date <= z.TuNgay && z.TuNgay <= lastDayOfMonth.Date) && z.IDNhanvien == IDNV && (z.LoaiPhep == 2 || z.LoaiPhep == 1)).ToList();
                if (list.Count > 0)
                {
                    foreach (NgayPhep author in list)
                    {
                        tongsongaynghi = tongsongaynghi + decimal.Parse(author.SoNgayNghi.ToString());
                    }
                    return tongsongaynghi;
                }
                else return 0;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
        public int GetDitreThang_ByIDNV(int IDNV)
        {
            try
            {
                DateTime dtime = DateTime.Now;
                var firstDayOfMonth = new DateTime(dtime.Year, dtime.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                //  double tongsongaynghi = 0;
                IList<NgayPhep> list = da.NgayPheps.Where(z => (firstDayOfMonth.Date <= z.TuNgay && z.TuNgay <= lastDayOfMonth.Date) && z.IDNhanvien == IDNV && (z.LoaiPhep == 3)).ToList();
                if (list.Count > 0)
                {
                    //foreach (NgayPhep author in list)
                    //{
                    //    tongsongaynghi = tongsongaynghi + double.Parse(author.SoNgayNghi.ToString());
                    //}
                    //return Utils.TryParseInt(tongsongaynghi, 0);
                    return list.Count;
                }
                else return 0;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
        public int GetVesomThang_ByIDNV(int IDNV)
        {
            try
            {
                DateTime dtime = DateTime.Now;
                var firstDayOfMonth = new DateTime(dtime.Year, dtime.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                //  double tongsongaynghi = 0;
                IList<NgayPhep> list = da.NgayPheps.Where(z => (firstDayOfMonth.Date <= z.TuNgay && z.TuNgay <= lastDayOfMonth.Date) && z.IDNhanvien == IDNV && (z.LoaiPhep == 4)).ToList();
                if (list.Count > 0)
                {
                    //foreach (NgayPhep author in list)
                    //{
                    //    tongsongaynghi = tongsongaynghi + double.Parse(author.SoNgayNghi.ToString());
                    //}
                    //return Utils.TryParseInt(tongsongaynghi, 0);
                    return list.Count;
                }
                else return 0;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
        public int GetCongtacngoaiThang_ByIDNV(int IDNV)
        {
            try
            {
                DateTime dtime = DateTime.Now;
                var firstDayOfMonth = new DateTime(dtime.Year, dtime.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                //  double tongsongaynghi = 0;
                IList<NgayPhep> list = da.NgayPheps.Where(z => (firstDayOfMonth.Date <= z.TuNgay && z.TuNgay <= lastDayOfMonth.Date) && z.IDNhanvien == IDNV && (z.LoaiPhep == 6)).ToList();
                if (list.Count > 0)
                {
                    //foreach (NgayPhep author in list)
                    //{
                    //    tongsongaynghi = tongsongaynghi + double.Parse(author.SoNgayNghi.ToString());
                    //}
                    //return Utils.TryParseInt(tongsongaynghi, 0);
                    return list.Count;
                }
                else return 0;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
        public NgayPhep GetNgayPhep_ByID_tao(int idtaophep, int loaiphep, DateTime ngaynghi, string buoinghi)
        {
            try
            {
                IList<NgayPhep> list = da.NgayPheps.Where(z => z.IDNhanvien == idtaophep && z.LoaiPhep == loaiphep && z.TuNgay == ngaynghi && z.BuoiNghi == buoinghi).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public TArea GetArea_ByName(string Areaname)
        {
            try
            {
                IList<TArea> list = da.TAreas.Where(z => z.AreaName == Areaname).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public NhanVien Getnhanvien_ByCMND(string CMND)
        {
            try
            {
                IList<NhanVien> list = da.NhanViens.Where(z => z.CMND.Trim() == CMND.Trim()).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public int GetIDTruongPhong_byIDuser(int IDuser)
        {
            int idtrp = 0;
            TUser ue = GetUse_ByID(IDuser);
            int idphongus = Utils.TryParseInt(ue.ParentID, 0);
            PhongBan pb = GetPhongBan_ByID(idphongus);
            if (pb != null)
            {
                if (Utils.TryParseInt(pb.ParenID, 0) == 0)
                {
                    idtrp = pb.IDPhong;
                }
                else idtrp = Utils.TryParseInt(pb.ParenID, 0);
            }
            ue = da.TUsers.Where(z => z.ParentID == idtrp && z.UserType == 4 && z.UserID != IDuser).OrderByDescending(z => z.ModifyDate).FirstOrDefault();
            if (ue != null)
            { return Utils.TryParseInt(ue.UserID, 0); }
            return 0;


        }
        public int GetIDTruongPhong_byIDPhong(int idphong)
        {
            int idtrp = 0;
            PhongBan pb = GetPhongBan_ByID(idphong);
            if (pb != null)
            {
                if (Utils.TryParseInt(pb.ParenID, 0) == 0)
                {
                    idtrp = pb.IDPhong;
                }
                else idtrp = Utils.TryParseInt(pb.ParenID, 0);
            }
            TUser ue = da.TUsers.Where(z => z.ParentID == idtrp && z.UserType == 4).OrderByDescending(z => z.ModifyDate).FirstOrDefault();
            if (ue != null)
            { return Utils.TryParseInt(ue.UserID, 0); }
            return 0;
        }
        public int GetIDPhoPhong_byIDPhong(int idphong)
        {
            int idtrp = 0;
            PhongBan pb = GetPhongBan_ByID(idphong);
            if (pb != null)
            {
                if (Utils.TryParseInt(pb.ParenID, 0) == 0)
                {
                    idtrp = pb.IDPhong;
                }
                else idtrp = Utils.TryParseInt(pb.ParenID, 0);
            }
            TUser ue = da.TUsers.Where(z => z.ParentID == idtrp && z.UserType == 5).OrderByDescending(z => z.ModifyDate).FirstOrDefault();
            if (ue != null)
            { return Utils.TryParseInt(ue.UserID, 0); }
            return 0;
        }
        public int GetIDTruongNhom_byIDPhong(int idphong, int iduser)
        {
            //int idtrp = 0;
            //PhongBan pb = GetPhongBan_ByID(idphong);
            //if (pb != null)
            //{
            //    if (Utils.TryParseInt(pb.ParenID, 0) == 0)
            //    {
            //        idtrp = pb.IDPhong;
            //    }
            //    else idtrp = Utils.TryParseInt(pb.ParenID, 0);
            //}
            TUser ue = da.TUsers.Where(z => z.ParentID == idphong && z.UserType == 6 && z.UserID != iduser).OrderByDescending(z => z.ModifyDate).FirstOrDefault();
            if (ue != null)
            { return Utils.TryParseInt(ue.UserID, 0); }
            return 0;
        }
        public int GetIDPhoPhong_byIDuser(int IDuser)
        {
            int idtrp = 0;
            TUser ue = GetUse_ByID(IDuser);
            int idphongus = Utils.TryParseInt(ue.ParentID, 0);
            PhongBan pb = GetPhongBan_ByID(idphongus);
            if (pb != null)
            {
                if (Utils.TryParseInt(pb.ParenID, 0) == 0)
                {
                    idtrp = pb.IDPhong;
                }
                else idtrp = Utils.TryParseInt(pb.ParenID, 0);
            }
            ue = da.TUsers.Where(z => z.ParentID == idtrp && z.UserType == 5 && z.UserID != IDuser).OrderByDescending(z => z.ModifyDate).FirstOrDefault();
            if (ue != null)
            { return Utils.TryParseInt(ue.UserID, 0); }
            return 0;


        }
        //public int GetIDPhoPhong_byIDuser(int IDuser)
        //{

        //    TUser ue = GetUse_ByID(IDuser);
        //    int idphong = Utils.TryParseInt(ue.ParentID, 0);
        //    ue = da.TUsers.Where(z => z.ParentID == idphong && z.UserType == 5).FirstOrDefault();
        //    if (ue != null)
        //    { return Utils.TryParseInt(ue.UserID, 0); }
        //    return 0;


        //}
        public int GetIDTruongNhom_byIDuser(int IDuser)
        {

            TUser ue = GetUse_ByID(IDuser);
            int idphong = Utils.TryParseInt(ue.ParentID, 0);
            ue = da.TUsers.Where(z => z.ParentID == idphong && z.UserType == 6 && z.UserID != IDuser).OrderByDescending(z => z.ModifyDate).FirstOrDefault();
            if (ue != null)
            { return Utils.TryParseInt(ue.UserID, 0); }
            else return 0;

        }
        public int GetIDBGD_byIDuser(int IDuser)
        {

            TUser ue = GetUse_ByID(IDuser);
            int idCty = Utils.TryParseInt(ue.UserLike, 0);
            ue = da.TUsers.Where(z => z.UserLike == idCty && z.UserType == 3).OrderByDescending(z => z.ModifyDate).FirstOrDefault();
            if (ue != null)
            { return Utils.TryParseInt(ue.UserID, 0); }
            return 0;


        }
        public int GetIDBGD_byIDCTY(int IDCTY)
        {

            TUser ue = da.TUsers.Where(z => z.UserLike == IDCTY && z.UserType == 3 && z.ParentID == -1).OrderByDescending(z => z.ModifyDate).FirstOrDefault();
            if (ue != null)
            { return Utils.TryParseInt(ue.UserID, 0); }
            return 0;


        }
        public int GetIDCty_byIDuser(int IDuser)
        {

            TUser ue = GetUse_ByID(IDuser);
            if (ue != null)
            { return Utils.TryParseInt(ue.UserLike, 0); }
            return 0;


        }
        public TPoinKM GetPoinKm_ByID(int poinKm)
        {
            try
            {
                IList<TPoinKM> list = da.TPoinKMs.Where(z => z.IDKm == poinKm).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public TUser GetUse_ByID(int UserID)
        {
            try
            {
                IList<TUser> list = da.TUsers.Where(z => z.UserID == UserID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public TValueofLevel GetLevelvalue_ByID(int levelID)
        {
            try
            {
                IList<TValueofLevel> list = da.TValueofLevels.Where(z => z.ValueLevelID == levelID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public TValueofLevel GetLevelvalue_ByIDNAMElevel(string levelID)
        {
            try
            {
                IList<TValueofLevel> list = da.TValueofLevels.Where(z => z.idlevel == levelID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public TMaCongViec GetWorklvalue_ByID(int levelID)
        {
            try
            {
                IList<TMaCongViec> list = da.TMaCongViecs.Where(z => z.IDMaCV == levelID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public TMaCongViec GetMACVvalue_ByID(int levelID)
        {
            try
            {
                IList<TMaCongViec> list = da.TMaCongViecs.Where(z => z.IDMaCV == levelID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }


        public int CreateArea(string AreaName, string AreaDescription, string Keyworder, int SortOrder, int Status)
        {
            TArea objEnt = null;
            objEnt = new TArea();

            objEnt.AreaName = AreaName;
            objEnt.AreaDescription = AreaDescription;
            objEnt.Keyworder = Keyworder;
            objEnt.SortOrder = SortOrder;
            objEnt.Status = Status;

            da.TAreas.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.TAreaID;
        }

        public int CreatePhongBan(string Name, int Ctytructhuoc, int parentID, string keyid)
        {
            PhongBan objEnt = null;
            objEnt = new PhongBan();

            objEnt.TenPhong = Name;
            objEnt.TrucThuoc = Ctytructhuoc;
            objEnt.ParenID = parentID;
            objEnt.KeyID = keyid;
            da.PhongBans.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.IDPhong;
        }
        public int CreateNguonTD(string Name, string linkweb)
        {
            NguonTuyenDung objEnt = null;
            objEnt = new NguonTuyenDung();

            objEnt.NameNTD = Name;
            objEnt.LinkNTD = linkweb;

            da.NguonTuyenDungs.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.Id;
        }
        public int CreatePoinkm(string khoangcach, string ghichu, int diemso)
        {
            TPoinKM objEnt = null;
            objEnt = new TPoinKM();

            objEnt.DiemKm = diemso;
            objEnt.NoteKm = ghichu;
            objEnt.SoKm = khoangcach;


            da.TPoinKMs.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.IDKm;
        }
        public int CreateLevelvalue(string levelName, string levelValue, int SortOrder, string idlevel)
        {
            TValueofLevel objEnt = null;
            objEnt = new TValueofLevel();

            objEnt.LevelName = levelName;
            objEnt.ValueOfLevel = levelValue;

            objEnt.SortOrder = SortOrder;
            objEnt.idlevel = idlevel;


            da.TValueofLevels.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.ValueLevelID;
        }
        public void CreateUsertoKeyword(int iduser, int idkeywork, string macv)
        {
            UsertoKeywork objEnt = null;
            objEnt = new UsertoKeywork();

            objEnt.IdUser = iduser;
            objEnt.IdKeywork = idkeywork;
            objEnt.IdMaCV = macv;
            objEnt.Diemlevel = GetPoinlevel(iduser);

            da.UsertoKeyworks.InsertOnSubmit(objEnt);
            da.SubmitChanges();


        }
        public int CreateWorkvalue(string workName, string MaCV, int DiemCV, string Ghichu, int nhomcase)
        {
            TMaCongViec objEnt = null;
            objEnt = new TMaCongViec();

            objEnt.TenCV = workName;
            objEnt.MaCV = MaCV;

            objEnt.DiemCV = DiemCV;
            objEnt.GhiChuCV = Ghichu;
            objEnt.NhomCase = nhomcase;
            da.TMaCongViecs.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.IDMaCV;
        }
        public int CreateNhanVien(NhanVien p_nhanvien)
        {
            p_nhanvien.NgayTao = DateTime.Now;
            p_nhanvien.NgayCapNhat = null;
            da.NhanViens.InsertOnSubmit(p_nhanvien);
            da.SubmitChanges();
            return p_nhanvien.IdNhanVien;
        }
        public int CreateTTUngvien(ThongTinTuyenDung p_nhanvien)
        {
            p_nhanvien.NgayTao = DateTime.Now;
            p_nhanvien.NgayCapNhat = null;
            da.ThongTinTuyenDungs.InsertOnSubmit(p_nhanvien);
            da.SubmitChanges();
            return p_nhanvien.Id;
        }
        public int CreateTTNhanSu(ThongTinNhanSu p_nhanvien)
        {
            p_nhanvien.NgayTao = DateTime.Now;
            p_nhanvien.NgayCapNhat = null;
            da.ThongTinNhanSus.InsertOnSubmit(p_nhanvien);
            da.SubmitChanges();
            return p_nhanvien.Id;
        }
        public int CreateTTNguoiThan(ThongTinNguoiThan p_NTnhanvien)
        {
            p_NTnhanvien.NgayTao = DateTime.Now;
            p_NTnhanvien.NgayCapNhat = null;
            da.ThongTinNguoiThans.InsertOnSubmit(p_NTnhanvien);
            da.SubmitChanges();
            return p_NTnhanvien.Id;
        }
        public int CreateTTKyLuatkhenthuong(ThongTinKyLuatKhenThuong p_NTnhanvien)
        {
            p_NTnhanvien.NgayTao = DateTime.Now;
            p_NTnhanvien.NgayCapNhat = null;
            da.ThongTinKyLuatKhenThuongs.InsertOnSubmit(p_NTnhanvien);
            da.SubmitChanges();
            return p_NTnhanvien.Id;
        }
        public bool UpdateNhanVien(NhanVien ent)
        {
            var list = da.NhanViens.Where(z => z.IdNhanVien == ent.IdNhanVien).ToList();
            if (list.Count > 0)
            {
                try
                {
                    NhanVien entCat = list.First();

                    entCat = ent;
                    entCat.NgayCapNhat = DateTime.Now;
                    da.NhanViens.Context.SubmitChanges();

                    return true;
                }
                catch (System.Exception ex)
                {

                }
            }

            return false;
        }
        public bool UpdateNhanVien_byCMnd(NhanVien ent)
        {
            var list = da.NhanViens.Where(z => z.CMND.Trim() == ent.CMND.Trim()).ToList();
            if (list.Count > 0)
            {
                try
                {
                    NhanVien entCat = list.First();

                    entCat = ent;
                    entCat.NgayCapNhat = DateTime.Now;
                    da.NhanViens.Context.SubmitChanges();

                    return true;
                }
                catch (System.Exception ex)
                {

                }
            }

            return false;
        }
        public bool UpdateTTUngVien(ThongTinTuyenDung ent)
        {
            var list = da.ThongTinTuyenDungs.Where(z => z.IdNhanvien == ent.IdNhanvien).ToList();
            if (list.Count > 0)
            {
                try
                {
                    ThongTinTuyenDung entCat = list.First();

                    entCat = ent;
                    entCat.NgayCapNhat = DateTime.Now;
                    da.ThongTinTuyenDungs.Context.SubmitChanges();

                    return true;
                }
                catch (System.Exception ex)
                {

                }
            }

            return false;
        }
        public bool UpdateTTNhanSu(ThongTinNhanSu ent)
        {
            var list = da.ThongTinNhanSus.Where(z => z.IdNhanVien == ent.IdNhanVien).ToList();
            if (list.Count > 0)
            {
                try
                {
                    ThongTinNhanSu entCat = list.First();

                    entCat = ent;
                    entCat.NgayCapNhat = DateTime.Now;
                    da.ThongTinNhanSus.Context.SubmitChanges();

                    return true;
                }
                catch (System.Exception ex)
                {

                }
            }

            return false;
        }
        public bool UpdateTTKyLuatkhenthuong(ThongTinKyLuatKhenThuong ent)
        {
            var list = da.ThongTinKyLuatKhenThuongs.Where(z => z.Id == ent.Id).ToList();
            if (list.Count > 0)
            {
                try
                {
                    ThongTinKyLuatKhenThuong entCat = list.First();

                    entCat = ent;
                    entCat.NgayCapNhat = DateTime.Now;
                    da.ThongTinKyLuatKhenThuongs.Context.SubmitChanges();

                    return true;
                }
                catch (System.Exception ex)
                {

                }
            }

            return false;
        }
        public bool UpdateTTNguoithan(ThongTinNguoiThan ent)
        {
            var list = da.ThongTinNguoiThans.Where(z => z.Id == ent.Id).ToList();
            if (list.Count > 0)
            {
                try
                {
                    ThongTinNguoiThan entCat = list.First();

                    entCat = ent;
                    entCat.NgayCapNhat = DateTime.Now;
                    da.ThongTinNguoiThans.Context.SubmitChanges();

                    return true;
                }
                catch (System.Exception ex)
                {

                }
            }

            return false;
        }
        public NhanVien GetNhanvien_byID(int IdNhanvien)
        {
            var list = da.NhanViens.Where(z => z.IdNhanVien == IdNhanvien).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;


        }
        public ThongTinTuyenDung GetTTTuyendung_byID(int IdNhanvien)
        {
            var list = da.ThongTinTuyenDungs.Where(z => z.IdNhanvien == IdNhanvien).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;


        }
        public ThongTinNhanSu GetTTNhansu_byID(int IdNhanvien)
        {
            var list = da.ThongTinNhanSus.Where(z => z.IdNhanVien == IdNhanvien).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;


        }
        public IList<ThongTinNhanSu> RowsThongTinNhanSu_ByIDLoaiNV(int p_page, int p_pageSize, int IDLoaiNV)
        {

            int preCount = (p_page - 1) * p_pageSize;
            return da.ThongTinNhanSus.Where(z => z.LoaiNV == IDLoaiNV || IDLoaiNV == -1).OrderByDescending(z => z.Id).Skip(preCount).Take(p_pageSize).ToList();


        }
        public ThongTinNguoiThan GetTTNguoithan_byID(int IdNT_Nhanvien)
        {
            var list = da.ThongTinNguoiThans.Where(z => z.Id == IdNT_Nhanvien).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;


        }
        public ThongTinKyLuatKhenThuong GetTTKyluatkhenthuong_byID(int Idklkt)
        {
            var list = da.ThongTinKyLuatKhenThuongs.Where(z => z.Id == Idklkt).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;


        }
        public ThongTinKyLuatKhenThuong GetTTKLKhenthuong_byID(int IdKLKT_Nhanvien)
        {
            var list = da.ThongTinKyLuatKhenThuongs.Where(z => z.Id == IdKLKT_Nhanvien).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;


        }
        public bool Createbill(int loaiPhep, string songaynghi, DateTime tungay, DateTime denngay, string lydo, string ghichu, int idnguoitao, int nguoithaythe, int flag, string buoinghi, int nhomduyetphep)
        {
            try
            {
                if (flag == 1)
                {
                    NgayPhep pheps = GetNgayPhep_ByID_tao(idnguoitao, loaiPhep, tungay, buoinghi);
                    if (pheps == null)
                    {

                        NgayPhep phep = new NgayPhep();
                        phep = new NgayPhep();
                        phep.BuoiNghi = buoinghi;
                        phep.DenGio = 0;
                        phep.DenNgay = denngay;
                        phep.GhiChu = ghichu;
                        phep.IDBGD = GetIDBGD_byIDuser(idnguoitao);
                        phep.IDCty = GetIDCty_byIDuser(idnguoitao);
                        phep.IDNguoiDuyet = 0;
                        phep.IdNguoiThayThe = 0;
                        phep.IDNhanvien = idnguoitao;
                        phep.IDPhoPhong = GetIDPhoPhong_byIDuser(idnguoitao);
                        phep.IDTruongBP = GetIDTruongPhong_byIDuser(idnguoitao);
                        phep.IDTruongNhom = GetIDTruongNhom_byIDuser(idnguoitao);
                        phep.LoaiPhep = loaiPhep;
                        phep.LyDoVang = lydo;
                        phep.NgayDuyetBGD = null;
                        phep.NgayDuyetBoPhan = null;
                        phep.NgayDuyetNhom = null;
                        phep.NgayDuyetPhep = null;
                        phep.NgayLapPhep = DateTime.Now;
                        phep.SoNgayNghi = Convert.ToDecimal(Convert.ToDouble(songaynghi));
                        phep.TrangThaiBGD = 0;
                        phep.TrangThaiBoPhan = 0;
                        phep.TrangThaiNhom = 0;
                        phep.TrangThaiPhep = 0;
                        phep.TuGio = 0;
                        phep.TuNgay = tungay;
                        phep.NhomDuyetPhep = nhomduyetphep;

                        da.NgayPheps.InsertOnSubmit(phep);
                        da.SubmitChanges();
                        flag = 0;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }
        public int CreateWorkKey(string MaKeyWork, string NoiDungKey, string UserKey, string SDTKey, string TenSanPhamKey, string diachikey
            , string khuvuckey, string sophieuxuat, string soluongspkey, DateTime Ngaytaokey, DateTime Ngayxulykey, string thoigianhenxulykey, string Nhanvienxulykey,
            int tongthoigianxulykey, string tinhtrangkey, DateTime ngayhoanthanhkey, int thoigiandukienxuly, string khachdanhgiakey,
            string giodixuly, string gioxulyve, int diemxulyKey, string ghichukey, string ghichuxulykey, int keyuutien, int thoigianxulykey, int diemthuongkey, int diemphatsinhkey,
            string nguoitaokey, int diemtru, string Ghichutru, int tongdiemhoanthanh, int diemcongviec, int diemdoanduong, int Idcreat, int IdFix, int IDKD, int loaiCase)
        {
            try
            {
                TKeyWork objEnt = new TKeyWork();
                objEnt = da.TKeyWorks.Where(z => z.NguoiTaoKeyWork.Trim() == nguoitaokey.Trim() && z.NoiDungKeyWork.Trim() == NoiDungKey.Trim() && z.TenKhachHangKeyWork.Trim() == UserKey.Trim() && z.DiaChiKeyWork.Trim() == diachikey.Trim()).FirstOrDefault();
                if (objEnt == null)
                {

                    objEnt = new TKeyWork();

                    objEnt.MaKeywork = MaKeyWork;
                    objEnt.NoiDungKeyWork = NoiDungKey;
                    objEnt.TenKhachHangKeyWork = UserKey;
                    objEnt.SDTkhachHangKeyWork = SDTKey;
                    objEnt.SanPhamKeyWork = TenSanPhamKey;
                    objEnt.DiaChiKeyWork = diachikey;
                    objEnt.KhuVucKeyWork = khuvuckey;
                    objEnt.SoPhieuXuatKeyWork = sophieuxuat;
                    objEnt.SoLuongSPKeyWork = soluongspkey;
                    objEnt.NgayTaoKeyWork = Ngaytaokey;
                    objEnt.NgayXulyKeyWork = Ngayxulykey;
                    objEnt.ThoiGianXulyKeyWork = thoigianxulykey;
                    objEnt.NhanvienXulyKeyWork = Nhanvienxulykey;
                    objEnt.TongThoigianXulyKeyWork = tongthoigianxulykey;
                    objEnt.TinhTrangKeyWork = tinhtrangkey;
                    objEnt.NgayHoanThanhKeyWork = ngayhoanthanhkey;
                    objEnt.ThoiGianDuKienXulyKeyWork = thoigiandukienxuly;
                    objEnt.KhachHangDanhGiaKeyWork = khachdanhgiakey;
                    objEnt.GioDiXulyKeyWork = giodixuly;
                    objEnt.GioVeXuLyKeyWork = gioxulyve;
                    objEnt.DiemXuLyKeyWork = diemxulyKey;
                    objEnt.GhiChuKeyWork = ghichukey;
                    objEnt.GhiChuXuLyKeyWork = ghichuxulykey;
                    objEnt.UuTienKeyWork = keyuutien;
                    objEnt.ThoiGianXulyKeyWork = thoigianxulykey;
                    objEnt.DiemThuongKeyWork = diemthuongkey;
                    objEnt.PhatSinhDiemKeyWork = diemphatsinhkey;
                    objEnt.NguoiTaoKeyWork = nguoitaokey;
                    objEnt.DiemTru = diemtru;
                    objEnt.GhiChuDiemTru = Ghichutru;
                    objEnt.TongDiemHoanThanh = tongdiemhoanthanh;
                    objEnt.DiemCongViec = diemcongviec;
                    objEnt.DiemDoanDuong = diemdoanduong;
                    objEnt.IDTaoCase = Idcreat;
                    objEnt.IDFixCase = IdFix;
                    objEnt.TimeUpdateCase = DateTime.Now;
                    objEnt.IDKinhDoanh = IDKD;
                    objEnt.LoaiCase = loaiCase;

                    da.TKeyWorks.InsertOnSubmit(objEnt);
                    da.SubmitChanges();

                    return objEnt.IDKeyWork;

                }
                return 0;
            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }
        public bool UpdateKeyWork(int keyID, string MaKeyWork, string NoiDungKey, string tenkhachhang, string sdtkhachhang, string TenSanPhamKey, string diachikhachhangxl
            , string khuvuckey, string sophieuxuat, string soluongspkey, DateTime Ngayxulykey, string Nhanvienxulykey,
            int thoigianxulykey, string tinhtrangkey, DateTime ngayhoanthanhkey, int thoigiandukienxuly, string khachdanhgiakey,
            string giodixuly, string gioxulyve, string ghichukey, string ghichuxulykey, int keyuutien, int diemthuongkey, int diemphatsinhkey, string ghichuphatsinh,
            string nguoitaokey, int diemtru, string Ghichutru, int IdFix, int IDKD, int loaiCase)
        {
            try
            {
                IList<TKeyWork> list = da.TKeyWorks.Where(z => z.IDKeyWork == keyID).ToList();
                if (list.Count > 0)
                {
                    var objEnt = list.First();

                    int diemcongviec = 0;
                    // string[] arrIDCV = MaKeyWork.Split('|');
                    //foreach (string id in arrIDCV)
                    //{
                    //    diemcongviec += GetPoinKeyWork(Utils.TryParseInt(id, 0));
                    //}
                    //  int diemcongviec = GetPoinKeyWork(Utils.TryParseInt(MaKeyWork, 0));
                    int diemdoanduong = GetPoinArea(Utils.TryParseInt(khuvuckey, 0));
                    int diemchuatru = 0;
                    string maCV = GetMaKeywork(Utils.TryParseInt(objEnt.MaKeywork, 0));
                    int TongSoPhuts = 0;

                    int tongdiemhoanthanh = 0;

                    objEnt.MaKeywork = MaKeyWork;

                    objEnt.NoiDungKeyWork = NoiDungKey;
                    objEnt.TenKhachHangKeyWork = tenkhachhang;
                    objEnt.SDTkhachHangKeyWork = sdtkhachhang;
                    objEnt.SanPhamKeyWork = TenSanPhamKey;
                    objEnt.DiaChiKeyWork = diachikhachhangxl;
                    objEnt.KhuVucKeyWork = khuvuckey;
                    objEnt.SoPhieuXuatKeyWork = sophieuxuat;
                    objEnt.SoLuongSPKeyWork = soluongspkey;
                    objEnt.NgayTaoKeyWork = objEnt.NgayTaoKeyWork;
                    objEnt.NgayXulyKeyWork = Ngayxulykey;
                    TimeSpan Time = ngayhoanthanhkey - Ngayxulykey;
                    int TongSoPhut = Utils.TryParseInt(Time.TotalMinutes, 0);
                    if (TongSoPhut > 0)
                    { TongSoPhuts = TongSoPhut; }

                    objEnt.NhanvienXulyKeyWork = Nhanvienxulykey;
                    objEnt.TongThoigianXulyKeyWork = TongSoPhuts;
                    objEnt.TinhTrangKeyWork = tinhtrangkey;
                    objEnt.NgayHoanThanhKeyWork = ngayhoanthanhkey;
                    objEnt.ThoiGianDuKienXulyKeyWork = thoigiandukienxuly;
                    objEnt.KhachHangDanhGiaKeyWork = khachdanhgiakey;
                    objEnt.GioDiXulyKeyWork = giodixuly;
                    objEnt.GioVeXuLyKeyWork = gioxulyve;
                    objEnt.GhiChuKeyWork = ghichukey;
                    objEnt.GhiChuXuLyKeyWork = ghichuxulykey;
                    objEnt.UuTienKeyWork = keyuutien;
                    objEnt.ThoiGianXulyKeyWork = thoigianxulykey;
                    objEnt.DiemThuongKeyWork = diemthuongkey;
                    objEnt.PhatSinhDiemKeyWork = diemphatsinhkey;
                    objEnt.NguoiTaoKeyWork = nguoitaokey;
                    objEnt.DiemCongViec = diemcongviec;
                    objEnt.DiemDoanDuong = diemdoanduong;
                    objEnt.DiemTru = 0;
                    objEnt.GhiChuDiemTru = Ghichutru;
                    objEnt.TongDiemHoanThanh = diemchuatru;
                    objEnt.DiemLevelKT = "0";
                    objEnt.DiemQuanLy = tongdiemhoanthanh;
                    objEnt.TimeUpdateCase = DateTime.Now;
                    objEnt.IDFixCase = IdFix;
                    objEnt.IDKinhDoanh = IDKD;
                    objEnt.LoaiCase = loaiCase;
                    da.TKeyWorks.Context.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool UpdateBill(int idBill, int TrangThai, string ghichu, int IdDuyet)
        {
            IList<TUser> listuser = da.TUsers.Where(z => z.UserID == IdDuyet).ToList();
            IList<NgayPhep> list = da.NgayPheps.Where(z => z.IDPhep == idBill).ToList();

            if (list.Count > 0)
            {
                var ngayp = list.First();
                int songay = Utils.TryParseInt(Convert.ToDouble(ngayp.SoNgayNghi), 0);
                int loaip = Utils.TryParseInt(ngayp.LoaiPhep, 0);

                // double tongngaynghi = 0;
                DateTime ngaydauthang = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                DataTable dss = Rows_Phep_byID(ngaydauthang, ngaydauthang.AddMonths(1).AddDays(-1), 2, Utils.TryParseInt(ngayp.IDNhanvien, 0));
                TUser user = listuser.First();
                //foreach (DataRow dr in dss.Rows)
                //{

                //    tongngaynghi += Convert.ToDouble(dr["SoNgayNghi"].ToString().Trim());

                //}
                if (ngayp.NhomDuyetPhep == 0) // nghi <= 2 ngày/tháng
                {


                    if (IdDuyet == ngayp.IDTruongNhom)
                    {
                        ngayp.TrangThaiNhom = 1;
                        ngayp.NgayDuyetNhom = DateTime.Now;
                    }
                    if (IdDuyet == ngayp.IDTruongBP || IdDuyet == ngayp.IDPhoPhong)
                    {
                        if (songay < 2)
                        {
                            ngayp.TrangThaiPhep = TrangThai;
                            ngayp.TrangThaiBoPhan = 1;
                            ngayp.NgayDuyetBoPhan = DateTime.Now;
                        }
                        else
                        {
                            ngayp.TrangThaiBoPhan = 1;
                            ngayp.NgayDuyetBoPhan = DateTime.Now;
                        }


                    }
                    if (IdDuyet == ngayp.IDBGD || user.UserType == 3)
                    {
                        ngayp.TrangThaiBGD = 1;
                        ngayp.TrangThaiPhep = TrangThai;
                        ngayp.NgayDuyetBGD = DateTime.Now;
                    }
                    if (listuser != null)
                    {
                        //   TUser user = listuser.First();
                        if (user.UserType == 4 || user.UserType == 5)
                        {
                            if (IdDuyet == ngayp.IDTruongBP || IdDuyet == ngayp.IDPhoPhong)
                            {
                                if (ngayp.TrangThaiNhom == 1 || ngayp.IDTruongNhom == 0)
                                {

                                    ngayp.TrangThaiPhep = TrangThai;
                                    ngayp.TrangThaiBoPhan = 1;
                                    ngayp.NgayDuyetBoPhan = DateTime.Now;

                                }
                                else return false;
                            }
                            else
                            {
                                if (ngayp.TrangThaiNhom == 1 || ngayp.IDTruongNhom == 0)
                                {

                                    ngayp.TrangThaiPhep = TrangThai;
                                    ngayp.TrangThaiBoPhan = 1;
                                    ngayp.NgayDuyetBoPhan = DateTime.Now;

                                }
                                else return false;
                            }
                        }
                    }
                    ngayp.IDNguoiDuyet = IdDuyet;
                    ngayp.GhiChu = ghichu;
                    ngayp.NgayDuyetPhep = DateTime.Now;
                    da.NgayPheps.Context.SubmitChanges();
                    return true;

                }
                else  // nghi > 2 ngày/tháng  or trưởng phòng or tạo phép trễ 3 ngày           
                {
                    if (IdDuyet == ngayp.IDTruongNhom)
                    {
                        ngayp.NgayDuyetNhom = DateTime.Now;
                        ngayp.TrangThaiNhom = 1;
                        ngayp.IDNguoiDuyet = IdDuyet;
                        ngayp.GhiChu = ghichu;
                        da.NgayPheps.Context.SubmitChanges();
                        return true;
                    }
                    else if (IdDuyet == ngayp.IDTruongBP || IdDuyet == ngayp.IDPhoPhong)
                    {
                        if (ngayp.TrangThaiNhom == 1 || ngayp.IDTruongNhom == 0)
                        {
                            ngayp.NgayDuyetBoPhan = DateTime.Now;
                            ngayp.TrangThaiNhom = 1;
                            ngayp.TrangThaiBoPhan = 1;
                            ngayp.GhiChu = ghichu;
                            ngayp.IDNguoiDuyet = IdDuyet;
                            da.NgayPheps.Context.SubmitChanges();
                            return true;
                        }
                        else return false;
                    }

                    if (user.UserType == 4 || user.UserType == 5)
                    {
                        if (IdDuyet == ngayp.IDTruongBP || IdDuyet == ngayp.IDPhoPhong)
                        {
                            if (ngayp.TrangThaiNhom == 1 || ngayp.IDTruongNhom == 0)
                            {


                                ngayp.TrangThaiBoPhan = 1;
                                ngayp.NgayDuyetBoPhan = DateTime.Now;
                                ngayp.IDNguoiDuyet = IdDuyet;
                                ngayp.GhiChu = ghichu;
                                ngayp.NgayDuyetPhep = DateTime.Now;
                                da.NgayPheps.Context.SubmitChanges();
                                return true;
                            }
                            else return false;
                        }
                        else
                        {
                            if (ngayp.TrangThaiNhom == 1 || ngayp.IDTruongNhom == 0)
                            {
                                ngayp.TrangThaiBoPhan = 1;
                                ngayp.NgayDuyetBoPhan = DateTime.Now;
                                ngayp.IDNguoiDuyet = IdDuyet;
                                ngayp.GhiChu = ghichu;
                                ngayp.NgayDuyetPhep = DateTime.Now;
                                da.NgayPheps.Context.SubmitChanges();
                                return true;
                            }
                            else return false;
                        }
                    }
                    else //(IdDuyet == ngayp.IDBGD)
                    {
                        if (listuser != null)
                        {
                            //  TUser user = listuser.First();
                            if (user.UserLike == -1 && user.ParentID == -1)
                            {
                                ngayp.NgayDuyetBGD = DateTime.Now;
                                ngayp.TrangThaiNhom = 1;
                                ngayp.TrangThaiBoPhan = 1;
                                ngayp.TrangThaiBGD = 1;
                                ngayp.TrangThaiPhep = TrangThai;
                                ngayp.IDNguoiDuyet = IdDuyet;
                                ngayp.NgayDuyetPhep = DateTime.Now;
                                ngayp.GhiChu = ghichu;
                                da.NgayPheps.Context.SubmitChanges();
                                return true;
                            }
                            else if (IdDuyet == ngayp.IDBGD || user.UserType == 3)
                            {
                                if (songay <= 3)
                                {
                                    if (ngayp.TrangThaiBoPhan == 1 || ngayp.IDTruongBP == 0)
                                    {
                                        ngayp.NgayDuyetBGD = DateTime.Now;
                                        ngayp.TrangThaiNhom = 1;
                                        ngayp.TrangThaiBoPhan = 1;
                                        ngayp.TrangThaiBGD = 1;
                                        ngayp.TrangThaiPhep = TrangThai;
                                        ngayp.IDNguoiDuyet = IdDuyet;
                                        ngayp.NgayDuyetPhep = DateTime.Now;
                                        ngayp.GhiChu = ghichu;
                                        da.NgayPheps.Context.SubmitChanges();
                                        return true;
                                    }
                                    else return false;
                                }
                                else return false;
                            }
                        }

                    }
                }
            }
            return false;

        }
        public int GetPoinKeyWork(int MaKeyWrok)
        {
            if (MaKeyWrok != 0)
            {
                int valuePoinKey = GetWorklvalue_ByID(MaKeyWrok).DiemCV.Value;
                return valuePoinKey;
            }
            return 0;
        }
        protected int GetPoinArea(int IdArea)
        {
            if (IdArea != 0)
            {
                int valuePoinArea = Utils.TryParseInt(GetArea_ByID(IdArea, 1).AreaDescription, 0);
                return valuePoinArea;
            }
            return 0;
        }
        protected string GetPoinlevel_by_caseID_userID(int caseID, int UserID)
        {
            UsertoKeywork usercase = new UsertoKeywork();
            string valuePoinLevel = "0";
            if (caseID != 0 && UserID != 0)
            {
                usercase = GetUserKeyWork_ByID(caseID, UserID);
                valuePoinLevel = usercase.Diemlevel;
                return valuePoinLevel;
            }
            return valuePoinLevel;
        }
        public string GetPoinlevel(int userXL)
        {

            TValueofLevel value = new TValueofLevel();
            string valuePoinLevel = "";

            if (userXL != 0)
            {
                value = GetLevelvalue_ByIDNAMElevel(GetUse_ByID(userXL).CompanyName);
                valuePoinLevel = value.ValueOfLevel;
                return valuePoinLevel;
            }
            return valuePoinLevel;
        }
        public string GetMaKeywork(int MaKey)
        {
            if (MaKey != 0)
            {
                string valuePoinLevel = GetMACVvalue_ByID(MaKey).MaCV;
                return valuePoinLevel;
            }
            return "";
        }
        public int GetPoin_MaKeywork(int MaKey)
        {
            if (MaKey != 0)
            {
                int valuePoinLevel = Utils.TryParseInt(GetMACVvalue_ByID(MaKey).DiemCV, 0);
                return valuePoinLevel;
            }
            return 0;
        }
        public string GetTenKeywork(int MaKey)
        {
            if (MaKey != 0)
            {
                string valuePoinLevel = GetMACVvalue_ByID(MaKey).TenCV;
                return valuePoinLevel;
            }
            return "";
        }
        public void Update_keywork_Status(int IDKey, string status)
        {
            var list = da.TKeyWorks.Where(z => z.IDKeyWork == IDKey).ToList();
            if (list.Count > 0)
            {
                var item = list.First();
                item.TinhTrangKeyWork = status;
                // da.TKeyWorks.InsertOnSubmit(item);
                da.SubmitChanges();
            }

        }
        public void Update_keywork_Ghichukey(int IDKey, string ghichucase)
        {
            var list = da.TKeyWorks.Where(z => z.IDKeyWork == IDKey).ToList();
            if (list.Count > 0)
            {
                var item = list.First();
                item.GhiChuKeyWork = ghichucase;
                // da.TKeyWorks.InsertOnSubmit(item);
                da.SubmitChanges();
            }

        }
        public void Update_userKey_Status(int IDUSer, int status)
        {
            var list = da.TUsers.Where(z => z.UserID == IDUSer).ToList();
            if (list.Count > 0)
            {

                var item = list.First();
                //  long socv = item.UserLike.Value;
                long ulike = item.UserLike.Value + (status);

                if (ulike <= 0)
                {
                    item.UserLike = 0;
                }
                else { item.UserLike = ulike; }


            }
            da.SubmitChanges();



        }

        public bool Update_userKey_Poin(int idkey, int IDUSer, int diemtru, string Ghichutru, int diemphatsinh, string ghichudiemphatsinh, int soluongspxl)
        {
            try
            {
                var list = da.UsertoKeyworks.Where(z => z.IdUser == IDUSer && z.IdKeywork == idkey).ToList();
                if (list.Count > 0)
                {

                    string ValueLevel = "1";
                    int diemchuatru = 0;
                    int diemphatsinhs = 0;
                    var item = list.First();
                    int diemtest = 0;
                    int diemxl = 0;
                    int diemBT = 0;
                    string MACASE = "";

                    string[] arrIDCV = item.TKeyWork.MaKeywork.Split('|');
                    foreach (string id in arrIDCV)
                    {
                        int idP = Utils.TryParseInt(id, 0);

                        MACASE = GetMaKeywork(idP).ToString().Replace(" ", "").ToUpper();

                        if (MACASE == "TEST" || MACASE == "SETUP" || MACASE == "RAPLK")
                        {
                            diemtest = GetPoin_MaKeywork(idP);
                        }
                        else if (MACASE == "BT" || MACASE == "GHXT" || MACASE == "EVENT" || MACASE == "TK")
                        {
                            diemBT = GetPoin_MaKeywork(idP);
                        }
                        else diemxl += GetPoin_MaKeywork(idP);

                    }

                    int slsp = soluongspxl;


                    int valueArea = Utils.TryParseInt(item.TKeyWork.DiemDoanDuong, 0);


                    string ValueLevels = GetPoinlevel_by_caseID_userID(idkey, IDUSer);
                    if (!string.IsNullOrEmpty(ValueLevels) && ValueLevels != "0")
                    {
                        ValueLevel = ValueLevels;
                    }
                    else ValueLevel = GetPoinlevel(Utils.TryParseInt(IDUSer, 0));
                    double ValueLevelpoin = double.Parse(ValueLevel, System.Globalization.CultureInfo.InvariantCulture);
                    int valueDiemThuong = Utils.TryParseInt(item.TKeyWork.DiemThuongKeyWork, 0);

                    int valueDiemTru = Utils.TryParseInt(diemtru, 0);
                    if (Utils.TryParseInt(item.TKeyWork.PhatSinhDiemKeyWork, 0) != 0)
                    {
                        diemphatsinhs = Utils.TryParseInt(Utils.TryParseInt(item.TKeyWork.PhatSinhDiemKeyWork, 0), 0);
                    }
                    else
                        diemphatsinhs = Utils.TryParseInt(diemphatsinh, 0);

                    int diemtestsp = Utils.TryParseInt(diemtest, 0) * Utils.TryParseInt(soluongspxl, 0);
                    double diemxlcased = diemxl * ValueLevelpoin;
                    int diemxlcase = Utils.TryParseInt(diemxlcased, 0);
                    diemchuatru = ((valueArea + diemxlcase) + diemtestsp) + ((valueDiemThuong + diemphatsinhs) + diemBT);
                    int tongdiemhoanthanh = diemchuatru - valueDiemTru;

                    item.SoSPXL = slsp.ToString();
                    item.Diemhoanthanh = diemchuatru;
                    item.DiemTru = diemtru;
                    item.GhiChuTru = Ghichutru;
                    item.DiemQL = tongdiemhoanthanh;
                    item.Diemphatsinh = diemphatsinhs;
                    item.Ghichudiemphatsinh = ghichudiemphatsinh;
                    item.Diemlevel = ValueLevel;
                    item.IdMaCV = item.TKeyWork.MaKeywork;


                    da.SubmitChanges();

                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;

            }



        }
        public void Update_keywork_Status_XulyUSer(int IDKey, string status, string iduser)
        {
            var list = da.TKeyWorks.Where(z => z.IDKeyWork == IDKey).ToList();
            if (list.Count > 0)
            {
                var item = list.First();
                item.TinhTrangKeyWork = status;
                item.NhanvienXulyKeyWork = iduser;
            }
            da.SubmitChanges();



        }
        public TKeyWork GetKeyWork_ByID(int IDKey)
        {
            try
            {
                IList<TKeyWork> list = da.TKeyWorks.Where(z => z.IDKeyWork == IDKey).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public UsertoKeywork GetUserKeyWork_ByID(int IDKey, int IDUser)
        {
            try
            {
                IList<UsertoKeywork> list = da.UsertoKeyworks.Where(z => z.IdUser == IDUser && z.IdKeywork == IDKey).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public bool UpdateArea(int AreaID, string AreaName, string AreaDescription, string Keyworder, int SortOrder, int Status)
        {
            try
            {
                IList<TArea> list = da.TAreas.Where(z => z.TAreaID == AreaID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.AreaName = AreaName;
                    objVlaue.AreaDescription = AreaDescription;
                    objVlaue.Keyworder = Keyworder;
                    objVlaue.SortOrder = SortOrder;
                    objVlaue.Status = Status;

                    da.TAreas.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool UpdatePhongban(int IDPhong, string namePhong, int Ctytructhuoc, int idparent, string keyid)
        {
            try
            {
                IList<PhongBan> list = da.PhongBans.Where(z => z.IDPhong == IDPhong).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.TenPhong = namePhong;
                    objVlaue.TrucThuoc = Ctytructhuoc;
                    objVlaue.ParenID = idparent;
                    objVlaue.KeyID = keyid;
                    da.PhongBans.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool UpdateNguonTuyenDung(int IDNguonTD, string nameNTD, string Linkweb)
        {
            try
            {
                IList<NguonTuyenDung> list = da.NguonTuyenDungs.Where(z => z.Id == IDNguonTD).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.NameNTD = nameNTD;
                    objVlaue.LinkNTD = Linkweb;

                    da.NguonTuyenDungs.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool UpdatePoinKm(int PoinID, string khoangcach, string ghichu, int diemso)
        {
            try
            {
                IList<TPoinKM> list = da.TPoinKMs.Where(z => z.IDKm == PoinID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.SoKm = khoangcach;
                    objVlaue.DiemKm = diemso;
                    objVlaue.NoteKm = ghichu;


                    da.TPoinKMs.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool UpdateValueofLevel(int levelID, string levelName, string levelvalue, int SortOrder, string idlevel)
        {
            try
            {
                IList<TValueofLevel> list = da.TValueofLevels.Where(z => z.ValueLevelID == levelID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.LevelName = levelName;
                    objVlaue.ValueOfLevel = levelvalue;

                    objVlaue.SortOrder = SortOrder;
                    objVlaue.idlevel = idlevel;

                    da.TValueofLevels.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool UpdateWorkValue(int levelID, string WorkName, string Mawork, int DiemCV, string ghichu, int nhomcase)
        {
            try
            {
                IList<TMaCongViec> list = da.TMaCongViecs.Where(z => z.IDMaCV == levelID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.TenCV = WorkName;
                    objVlaue.MaCV = Mawork;

                    objVlaue.DiemCV = DiemCV;
                    objVlaue.GhiChuCV = ghichu;
                    objVlaue.NhomCase = nhomcase;

                    da.TMaCongViecs.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteArea(int AreaID)
        {
            try
            {
                IList<TArea> list = da.TAreas.Where(z => z.TAreaID == AreaID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.Status = 3;

                    da.TAreas.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeletePhongBan(int IdPhong)
        {
            try
            {
                IList<PhongBan> list = da.PhongBans.Where(z => z.IDPhong == IdPhong).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    da.PhongBans.DeleteOnSubmit(objVlaue);

                    da.PhongBans.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool DeleteNguonTD(int IdNTD)
        {
            try
            {
                IList<NguonTuyenDung> list = da.NguonTuyenDungs.Where(z => z.Id == IdNTD).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    da.NguonTuyenDungs.DeleteOnSubmit(objVlaue);

                    da.NguonTuyenDungs.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeletePoinKm(int poinKm)
        {
            try
            {
                IList<TPoinKM> list = da.TPoinKMs.Where(z => z.IDKm == poinKm).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    //  objVlaue.Status = 3;
                    da.TPoinKMs.DeleteOnSubmit(objVlaue);
                    da.TPoinKMs.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool DeleteValueOflevel(int valueID)
        {
            try
            {
                IList<TValueofLevel> list = da.TValueofLevels.Where(z => z.ValueLevelID == valueID).ToList();
                if (list.Count > 0)
                {
                    TValueofLevel item = new TValueofLevel();
                    item = list.First();

                    da.TValueofLevels.DeleteOnSubmit(item);
                    da.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteUseOfkeywork(int IDKey, int idUSer)
        {
            try
            {
                //IList<UsertoKeywork> list = da.UsertoKeyworks.Where(z => z.IdKeywork == IDKey).ToList();
                //if (list.Count > 0)
                //{
                // UsertoKeywork item = new UsertoKeywork();
                // item = list.First();

                DataTable arrIDss = ListUserOfKeywork_by_IdCase(IDKey);

                if (arrIDss != null)
                {
                    if (idUSer == 0)
                    {
                        foreach (DataRow dr in arrIDss.Rows)
                        {

                            int idkeyU = Utils.TryParseInt(dr["IdKeywork"].ToString(), 0);
                            int idU = Utils.TryParseInt(dr["IdUser"].ToString(), 0);
                            IList<UsertoKeywork> listt = da.UsertoKeyworks.Where(z => z.IdKeywork == idkeyU).ToList();
                            UsertoKeywork item = new UsertoKeywork();
                            item = listt.First();
                            da.UsertoKeyworks.DeleteOnSubmit(item);
                            da.SubmitChanges();
                            Update_userKey_Status(idU, -1);

                        }

                        return true;
                    }
                    if (idUSer != 0)
                    {
                        IList<UsertoKeywork> listt = da.UsertoKeyworks.Where(z => z.IdKeywork == IDKey && z.IdUser == idUSer).ToList();
                        UsertoKeywork item = new UsertoKeywork();
                        item = listt.First();
                        da.UsertoKeyworks.DeleteOnSubmit(item);
                        da.SubmitChanges();

                    }

                    //}


                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteMaCV(int valueID)
        {
            try
            {
                IList<TMaCongViec> list = da.TMaCongViecs.Where(z => z.IDMaCV == valueID).ToList();
                if (list.Count > 0)
                {
                    TMaCongViec item = new TMaCongViec();
                    item = list.First();

                    da.TMaCongViecs.DeleteOnSubmit(item);
                    da.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteKeyWork_byID(int IDKeywork)
        {
            try
            {
                DeleteUseOfkeywork(IDKeywork, 0);
                IList<TKeyWork> list = da.TKeyWorks.Where(z => z.IDKeyWork == IDKeywork).ToList();
                if (list.Count > 0)
                {
                    TKeyWork item = new TKeyWork();
                    item = list.FirstOrDefault();

                    da.TKeyWorks.DeleteOnSubmit(item);
                    da.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteBill_byID(int IDPhep)
        {
            try
            {

                IList<NgayPhep> list = da.NgayPheps.Where(z => z.IDPhep == IDPhep).ToList();
                if (list.Count > 0)
                {
                    NgayPhep item = new NgayPhep();
                    item = list.FirstOrDefault();

                    da.NgayPheps.DeleteOnSubmit(item);
                    da.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteQTLV_byID(int idQTLV)
        {
            try
            {

                IList<ThongTinKyLuatKhenThuong> list = da.ThongTinKyLuatKhenThuongs.Where(z => z.Id == idQTLV).ToList();
                if (list.Count > 0)
                {
                    ThongTinKyLuatKhenThuong item = new ThongTinKyLuatKhenThuong();
                    item = list.FirstOrDefault();

                    da.ThongTinKyLuatKhenThuongs.DeleteOnSubmit(item);
                    da.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteTTTuyenDUng_byID(int IdNhanVien)
        {
            try
            {

                IList<ThongTinTuyenDung> list = da.ThongTinTuyenDungs.Where(z => z.IdNhanvien == IdNhanVien).ToList();
                if (list.Count > 0)
                {
                    ThongTinTuyenDung item = new ThongTinTuyenDung();
                    item = list.FirstOrDefault();

                    da.ThongTinTuyenDungs.DeleteOnSubmit(item);
                    da.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteTTNhanSU_byID(int IdNhanVien)
        {
            try
            {

                IList<ThongTinNhanSu> list = da.ThongTinNhanSus.Where(z => z.IdNhanVien == IdNhanVien).ToList();
                if (list.Count > 0)
                {
                    ThongTinNhanSu item = new ThongTinNhanSu();
                    item = list.FirstOrDefault();

                    da.ThongTinNhanSus.DeleteOnSubmit(item);
                    da.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteNhanvien_byID(int IDNhanvien)
        {
            try
            {


                FileManager fileMng = new FileManager();

                IList<NhanVien> list = da.NhanViens.Where(z => z.IdNhanVien == IDNhanvien).ToList();
                if (list.Count > 0)
                {
                    DeleteAllNguoiThan_byID(IDNhanvien);
                    DeleteAllQTLV_byID(IDNhanvien);
                    DeleteTTNhanSU_byID(IDNhanvien);
                    DeleteTTTuyenDUng_byID(IDNhanvien);
                    DataTable dt = user_ntx.RowsProductImages(IDNhanvien);
                    foreach (DataRow dr in dt.Rows)
                    {
                        long productImageID = Convert.ToInt64(dr["ProductImageID"]);
                        fileMng.DeleteCommonFile(productImageID, true);
                    }

                    fileMng.DeleteCommonFile(Utils.TryParseLong(list.FirstOrDefault().Image, 0), true);

                    NhanVien item = new NhanVien();
                    item = list.FirstOrDefault();

                    da.NhanViens.DeleteOnSubmit(item);
                    da.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool DeleteAllNguoiThan_byID(int ID_NhanVien)
        {
            IList<ThongTinNguoiThan> list = da.ThongTinNguoiThans.Where(z => z.IdNhanVien == ID_NhanVien).ToList();
            if (list.Count > 0)
            {
                foreach (ThongTinNguoiThan author in list)
                {
                    DeleteNguoiThan_byID(author.Id);
                }
                //for (int i = 1; i <= list.Count; i++)
                //{
                //    DeleteNguoiThan_byID(list[i].Id);
                //}
                return true;
            }
            return false;
        }
        public bool DeleteAllQTLV_byID(int ID_NhanVien)
        {
            IList<ThongTinKyLuatKhenThuong> list = da.ThongTinKyLuatKhenThuongs.Where(z => z.IdNhanVien == ID_NhanVien).ToList();
            if (list.Count > 0)
            {
                foreach (ThongTinKyLuatKhenThuong author in list)
                {
                    DeleteQTLV_byID(author.Id);
                }
                //for (int i = 1; i <= list.Count; i++)
                //{
                //    DeleteQTLV_byID(list[i].Id);
                //}
                return true;
            }
            return false;
        }

        public bool DeleteNguoiThan_byID(int IDNguoiThan_NhanVien)
        {
            try
            {
                FileManager fileMng = new FileManager();

                IList<ThongTinNguoiThan> list = da.ThongTinNguoiThans.Where(z => z.Id == IDNguoiThan_NhanVien).ToList();
                if (list.Count > 0)
                {

                    DataTable dt = user_ntx.RowsProductImages(IDNguoiThan_NhanVien);
                    foreach (DataRow dr in dt.Rows)
                    {
                        long productImageID = Convert.ToInt64(dr["ProductImageID"]);
                        fileMng.DeleteCommonFile(productImageID, true);
                    }

                    fileMng.DeleteCommonFile(Utils.TryParseLong(list.FirstOrDefault().Image, 0), true);

                    ThongTinNguoiThan item = new ThongTinNguoiThan();
                    item = list.FirstOrDefault();

                    da.ThongTinNguoiThans.DeleteOnSubmit(item);
                    da.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region MapTerritory
        public TMapTerritoryArea GetMap_terri_area_ByID(Int64 TMapID, string StrKey)
        {
            try
            {
                IList<TMapTerritoryArea> list = da.TMapTerritoryAreas.Where(z => z.TMapID == TMapID && z.Keyword.ToLower().Trim() == StrKey.ToLower().Trim()).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public Int64 CreateTerri_area_Map(int TAreaID, int TerritoryID, Int64 TMapID, string Keyword)
        {
            TMapTerritoryArea objEnt = null;
            objEnt = new TMapTerritoryArea();

            objEnt.AreaID = TAreaID;
            objEnt.TerritoryID = TerritoryID;
            objEnt.TMapID = TMapID;
            objEnt.Keyword = Keyword;

            da.TMapTerritoryAreas.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.MapTerriAreaID;
        }

        public bool UpdateTerri_Area_Map(int TAreaID, int TerritoryID, Int64 TMapID, string Keyword)
        {
            try
            {
                IList<TMapTerritoryArea> list = da.TMapTerritoryAreas.Where(z => z.TMapID == TMapID && z.Keyword.ToLower().Trim() == Keyword.ToLower().Trim()).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.TerritoryID = TerritoryID;
                    objVlaue.AreaID = TAreaID;
                    objVlaue.TMapID = TMapID;
                    objVlaue.Keyword = Keyword;

                    da.TMapTerritoryAreas.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool DeleteTerri_AreaMap(Int64 TMapID, string Keyword)
        {
            try
            {
                IList<TMapTerritoryArea> list = da.TMapTerritoryAreas.Where(z => z.TMapID == TMapID && z.Keyword.ToLower().Trim() == Keyword.ToLower().Trim()).ToList();
                if (list.Count > 0)
                {
                    da.TMapTerritoryAreas.DeleteOnSubmit(list.First());
                    da.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region MapArea
        public TAreaMap GetAreaMap_ByID(Int64 MapID, string StrKey)
        {
            try
            {
                IList<TAreaMap> list = da.TAreaMaps.Where(z => z.TMapID == MapID && z.Keyword.ToLower().Trim() == StrKey.ToLower().Trim()).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public Int64 CreateAreaMap(int TAreaID, Int64 TMapID, string Keyword)
        {
            TAreaMap objEnt = null;
            objEnt = new TAreaMap();

            objEnt.TAreaID = TAreaID;
            objEnt.TMapID = TMapID;
            objEnt.Keyword = Keyword;

            da.TAreaMaps.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.TAreaMapID;
        }

        public bool UpdateAreaMap(int TAreaID, Int64 TMapID, string Keyword)
        {
            try
            {
                IList<TAreaMap> list = da.TAreaMaps.Where(z => z.TMapID == TMapID && z.Keyword.ToLower().Trim() == Keyword.ToLower().Trim()).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.TAreaID = TAreaID;
                    objVlaue.TMapID = TMapID;
                    objVlaue.Keyword = Keyword;

                    da.TAreaMaps.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAreaMap(Int64 TMapID, string Keyword)
        {
            try
            {
                IList<TAreaMap> list = da.TAreaMaps.Where(z => z.TMapID == TMapID && z.Keyword.ToLower().Trim() == Keyword.ToLower().Trim()).ToList();
                if (list.Count > 0)
                {
                    da.TAreaMaps.DeleteOnSubmit(list.First());
                    da.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region MemberComment
        public long CountComment_NewsID(Int64 longNewsID)
        {
            IList<TComment> list = da.TComments.Where(z => z.ID_Ref == longNewsID).ToList();
            return list.Count;
        }

        public TComment GetComment_ByIDcommet(Int64 CommentID)
        {
            try
            {
                IList<TComment> list = da.TComments.Where(z => z.CommentID == CommentID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public IList<TComment> RowsComment(int p_page, int p_pageSize, Int64 Ref_ID, int Status, string strUK, Int64 ParentComment)
        {
            if (!string.IsNullOrEmpty(strUK))
            {
                int preCount = (p_page - 1) * p_pageSize;
                return da.TComments.Where(z => z.ID_Ref == Ref_ID && (z.Parent == ParentComment || ParentComment == -1) && z.UniqueKey == strUK && (z.Status == Status || Status == -1)).OrderByDescending(z => z.CreateDate).Skip(preCount).Take(p_pageSize).ToList();
            }
            else
            {
                int preCount = (p_page - 1) * p_pageSize;
                return da.TComments.Where(z => z.ID_Ref == Ref_ID && (z.Parent == ParentComment || ParentComment == -1) && (z.Status == Status || Status == -1)).OrderByDescending(z => z.CreateDate).Skip(preCount).Take(p_pageSize).ToList();
            }
        }

        public int CountComment(Int64 Ref_ID, int Status, string strUK, Int64 ParentComment)
        {
            if (!string.IsNullOrEmpty(strUK))
            {
                return da.TComments.Where(z => z.ID_Ref == Ref_ID && (z.Parent == ParentComment || ParentComment == -1) && z.UniqueKey == strUK && (z.Status == Status || Status == -1)).Count();
            }
            else
                return da.TComments.Where(z => z.ID_Ref == Ref_ID && (z.Parent == ParentComment || ParentComment == -1) && (z.Status == Status || Status == -1)).Count();
        }


        #endregion

        #region type

        public IList<TType> ListType()
        {
            try
            {
                return da.TTypes.Where(z => z.status != 3).OrderByDescending(z => z.TypeID).ToList();
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public IList<TType> ListType_byUK(string thisUK)
        {
            try
            {
                return da.TTypes.Where(z => z.status != 3 && z.status == 1 && z.Keyword == thisUK).OrderByDescending(z => z.TypeID).ToList();
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public IList<TMapType> ListMapType_byTypeID(int TypeID)
        {
            try
            {
                return da.TMapTypes.Where(z => z.TypeID == TypeID).ToList();
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public TType GetType_ByID(int TypeID)
        {
            try
            {
                IList<TType> list = da.TTypes.Where(z => z.TypeID == TypeID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public TType GetType_ByTypeKeyword_keywordmap(string keyword, string keywordmap)
        {
            try
            {
                IList<TType> list = da.TTypes.Where(z => z.TypeKeyword.ToLower() == keyword.ToLower() && z.Keyword.ToLower() == keywordmap.ToLower()).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public TMapType GetMapType_ByTypeID_ObjID(int TypeID, Int64 ObjID)
        {
            try
            {
                IList<TMapType> list = da.TMapTypes.Where(z => z.TypeID == TypeID && z.MapID == ObjID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region user
        public long CountComment_Member(Int64 intUser)
        {
            IList<TComment> list = da.TComments.Where(z => z.UserID == intUser).ToList();
            return list.Count;
        }

        public long CountNews_Member(Int64 intUser)
        {
            IList<TNew> list = da.TNews.Where(z => z.RegUser == intUser).ToList();
            return list.Count;
        }

        public long CountLike_Member(Int64 intUser)
        {
            IList<TUserMapNew> list = da.TUserMapNews.Where(z => z.MemberID == intUser).ToList();
            return list.Count;
        }

        public TUser GetUser_ByIDAll(int intUser)
        {
            var list = da.TUsers.Where(z => z.UserID == intUser).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        //public Int64 Count_LikeOfUser(int intUser)
        //{
        //    var list = da.TUsers.Where(z => z.UserID == intUser).ToList();
        //    if (list.Count >0 )
        //    {
        //        return Utility.TryParseLong(list.First().UserLike,0);
        //    }
        //    return 0;

        //}

        public TUser GetMember_ByEmail(string strEmail)
        {
            var list = da.TUsers.Where(z => z.Email == strEmail && z.UserType != 1 && z.UserType != 2).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        public TUser GetMember_ByLoginID(string strLoginID)
        {
            var list = da.TUsers.Where(z => z.LoginID == strLoginID && z.PermissionString == "1" && z.UserType != 1 && z.UserType != 2).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        public TUser GetMember_ByID(int intUser)
        {
            var list = da.TUsers.Where(z => z.UserID == intUser && z.PermissionString == "1" && z.UserType != 1 && z.UserType != 2).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        public TUser GetMember_ByLoginID_pass_otherPermission(string intUser, string pass)
        {
            var list = da.TUsers.Where(z => z.UserType != 1 && z.UserType != 2 && z.LoginID == intUser && z.Password == pass).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        public TUser GetUser_ByID_pass(int intUser, string pass)
        {
            var list = da.TUsers.Where(z => z.UserID == intUser && z.Password == pass && z.PermissionString == "1" && z.UserType != 1 && z.UserType != 2).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            else
                return null;
        }

        public DataTable List_Manager_Member(int @intPage, int @intPageSize, int @intRoleUser, int @intGender)
        {
            string sql = string.Format("[p_TUser_byMember_Rows] @intPage={0},@intPageSize={1}, @intRoleUser={2}, @intGender={3}",
                @intPage, @intPageSize, @intRoleUser, @intGender);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public int Count_Manager_Member()
        {
            string sql = string.Format("[p_TUser_byMember_Count]");
            return (new ConnectSQL()).ConnectAndExculeScala(sql);
        }

        //public int Count_Manager_Member(int @intRoleUser, int @intGender)
        //{
        //    string sql = string.Format("[p_TUser_byMember_Count] @intRoleUser={0}, @intGender={1}", @intRoleUser, @intGender);
        //    return (new ConnectSQL()).ConnectAndExculeScala(sql);
        //}

        #endregion

        #region User Map New
        public TUserMapNew GetMapUserNew_by_user(Int64 intMember, Int64 intNews)
        {
            IList<TUserMapNew> list = da.TUserMapNews.Where(z => z.MemberID == intMember && z.NewsID == intNews).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;

        }

        public Int64 CreateUserMapNew(Int64 MemberID, Int64 NewsID)
        {
            TUserMapNew objEnt = null;
            objEnt = new TUserMapNew();

            objEnt.MemberID = MemberID;
            objEnt.NewsID = NewsID;

            da.TUserMapNews.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.MapMemberNewID;
        }
        #endregion

        #region Role Member
        public TUserRoleMember GetRole_ByUK(string strKey)
        {
            IList<TUserRoleMember> list = da.TUserRoleMembers.Where(z => z.KeyWord.ToLower() == strKey.ToLower()).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public int GetRoleMemberID_ByUK(string strKey)
        {
            IList<TUserRoleMember> list = da.TUserRoleMembers.Where(z => z.KeyWord.ToLower() == strKey.ToLower()).ToList();
            if (list.Count > 0)
            {
                return list.First().RoleMemberID;
            }
            return 0;
        }

        public TUserRoleMember GetRole_ByID(int RoleMemberID)
        {
            IList<TUserRoleMember> list = da.TUserRoleMembers.Where(z => z.RoleMemberID == RoleMemberID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public IList<TUserRoleMember> ListRole()
        {
            IList<TUserRoleMember> list = da.TUserRoleMembers.ToList();

            return list;
        }

        #endregion

        #region User Map Role
        public TUserMapRole GetUserMapRole(Int64 intUser)
        {
            IList<TUserMapRole> list = da.TUserMapRoles.Where(z => z.UserID == intUser).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        #endregion

        #region newletter

        public DataTable RowsNewsLetter(int @intPage, int @intPageSize, string @strEmail, int @intStatus, int @intStatusDelete, string @strGender)
        {
            string sql = string.Format("[NewsLetter_Rows] @intPage={0},@intPageSize={1}, @strEmail='{2}', @intStatus={3}, @intStatusDelete={4}, @strGender='{5}'",
                @intPage, @intPageSize, @strEmail, @intStatus, @intStatusDelete, @strGender);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public int CountNewsLetter(string @strEmail, int @intStatus, int @intStatusDelete, string @strGender)
        {
            string sql = string.Format("[NewsLetter_Count] @strEmail='{0}', @intStatus={1}, @intStatusDelete={2}, @strGender='{3}' ",
                @strEmail, @intStatus, @intStatusDelete, @strGender);
            return (new ConnectSQL()).ConnectAndExculeScala(sql);
        }

        #endregion

        #region WebLink

        public IList<TWebLink> ListWebLink(bool viewStatus)
        {
            return da.TWebLinks.Where(z => z.IsView == viewStatus).OrderByDescending(z => z.LinkID).ToList();
        }

        #endregion

        #region MapID_All
        public MapAll_ID GetMapAll_ID(long p_proID, string p_key)
        {
            try
            {
                IList<MapAll_ID> list = da.MapAll_IDs.Where(z => z.KeyWord.ToLower().Trim() == p_key.ToLower().Trim() && z.MapProduct == p_proID).ToList();
                if (list.Count > 0)
                {
                    return list.First();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        #endregion

        /*create*/
        #region MapID_All
        public bool CreateMapAll_ID(MapAll_ID ent)
        {
            try
            {
                MapAll_ID objEnt = null;
                objEnt = new MapAll_ID();

                objEnt.MapProduct = ent.MapProduct;
                objEnt.MapID = ent.MapID;
                objEnt.KeyWord = ent.KeyWord;

                da.MapAll_IDs.InsertOnSubmit(objEnt);
                da.SubmitChanges();

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }

        public bool UpdateMapAll_ID(MapAll_ID ent)
        {
            IList<MapAll_ID> list = da.MapAll_IDs.Where(z => z.MapProduct == ent.MapProduct && z.KeyWord.ToLower().Trim() == ent.KeyWord.ToLower().Trim()).ToList();
            if (list.Count > 0)
            {
                var objVlaue = list.First();

                objVlaue.MapID = ent.MapID;

                da.MapAll_IDs.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool deleteMapAll_ID(MapAll_ID ent)
        {
            IList<MapAll_ID> list = da.MapAll_IDs.Where(z => z.MapProduct == ent.MapProduct && z.KeyWord.ToLower().Trim() == ent.KeyWord.ToLower().Trim()).ToList();
            if (list.Count > 0)
            {
                da.MapAll_IDs.DeleteOnSubmit(list.First());
                da.SubmitChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region NewsLetter

        public bool CreateNewLetter(string Email, string Gender, int Status)
        {
            try
            {
                T_NewLetter objEnt = null;
                objEnt = new T_NewLetter();

                objEnt.Email = Email;
                objEnt.Gender = Gender;
                objEnt.Status = Status;
                objEnt.AddDate = DateTime.Now;
                objEnt.AddByUserID = 0;
                objEnt.ModifiedDate = DateTime.Now;
                objEnt.ModifiedByUserID = 0;

                da.T_NewLetters.InsertOnSubmit(objEnt);
                da.SubmitChanges();

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }

        public bool UpdateNewsletter_chuaxuat(Int64 NewLetterID, int intStatus)
        {
            IList<T_NewLetter> list = da.T_NewLetters.Where(z => z.NewLetterID == NewLetterID).ToList();
            if (list.Count > 0)
            {
                var objVlaue = list.First();

                objVlaue.Status = intStatus;

                da.T_NewLetters.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool deleteNewsletter(Int64 NewLetterID)
        {
            IList<T_NewLetter> list = da.T_NewLetters.Where(z => z.NewLetterID == NewLetterID).ToList();
            if (list.Count > 0)
            {
                var objVlaue = list.First();

                objVlaue.Status = 3;

                da.T_NewLetters.Context.SubmitChanges();
                return true;
            }
            return false;
        }

        #endregion

        #region membercomment

        public bool UpdateComment(TComment entComment)
        {
            TComment ent = da.TComments.Where(z => z.CommentID == entComment.CommentID).First();
            ent.Email = entComment.Email;
            ent.ID_Ref = entComment.ID_Ref;
            ent.Name = entComment.Name;
            ent.Ref_Type = entComment.Ref_Type;
            ent.Status = entComment.Status;
            ent.Title = entComment.Title;
            ent.UserID = entComment.UserID;
            ent.Comment_Content = entComment.Comment_Content;
            da.TComments.Context.SubmitChanges();
            return true;
        }

        public bool UpdateCommentParent(TComment entComment)
        {
            TComment ent = da.TComments.Where(z => z.CommentID == entComment.CommentID).First();
            ent.Comment_Content = entComment.Comment_Content;
            da.TComments.Context.SubmitChanges();
            return true;
        }

        public bool UpdateComment_Status(Int64 longCommentID, int intStatus)
        {
            TComment ent = da.TComments.Where(z => z.CommentID == longCommentID).First();
            ent.Status = intStatus;
            da.TComments.Context.SubmitChanges();
            return true;
        }

        public bool DeleteComment(long p_commentID)
        {
            IList<TComment> list = da.TComments.Where(z => z.CommentID == p_commentID).ToList();
            if (list.Count > 0)
            {
                da.TComments.DeleteOnSubmit(list.First());
                da.SubmitChanges();
            }

            return true;
        }

        public Int64 CreateComment(Int64 UserID, Int64 ID_Ref, string Comment_Content, DateTime CreateDate, int Status, string UniqueKey)
        {
            TComment objEnt = null;
            objEnt = new TComment();

            objEnt.UserID = UserID;
            objEnt.ID_Ref = ID_Ref;
            objEnt.Comment_Content = Comment_Content;
            objEnt.CreateDate = CreateDate;
            objEnt.UniqueKey = UniqueKey;
            objEnt.Status = Status;

            da.TComments.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.CommentID;
        }
        public Int64 CreateComment_ent(TComment ent)
        {
            TComment objEnt = null;
            objEnt = new TComment();

            objEnt = ent;
            //objEnt.UserID = UserID;
            //objEnt.ID_Ref = ID_Ref;
            //objEnt.Comment_Content = Comment_Content;
            //objEnt.CreateDate = CreateDate;
            //objEnt.UniqueKey = UniqueKey;
            //objEnt.Status = Status;

            da.TComments.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.CommentID;
        }
        #endregion

        #region type Create update delete

        public int CreateType(string TypeName, int TypeParent, string TypeKeyword, int status, string Keyword)
        {
            TType objEnt = null;
            objEnt = new TType();

            objEnt.TypeName = TypeName;
            objEnt.TypeParent = TypeParent;
            objEnt.Keyword = Keyword;
            objEnt.status = status;
            objEnt.TypeKeyword = TypeKeyword;

            da.TTypes.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.TypeID;
        }

        public Int64 CreateMapType(int TypeID, Int64 MapID)
        {
            TMapType objEnt = null;
            objEnt = new TMapType();

            objEnt.TypeID = TypeID;
            objEnt.MapID = MapID;

            da.TMapTypes.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.MapTypeID;
        }

        public bool UpdateType(int TypeID, string TypeName, int TypeParent, string TypeKeyword, int status, string Keyword)
        {
            try
            {
                IList<TType> list = da.TTypes.Where(z => z.TypeID == TypeID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.TypeName = TypeName;
                    objVlaue.TypeParent = TypeParent;
                    objVlaue.Keyword = Keyword;
                    objVlaue.status = status;
                    objVlaue.TypeKeyword = TypeKeyword;
                    da.TTypes.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool DeleteType(int TypeID)
        {
            try
            {
                IList<TType> list = da.TTypes.Where(z => z.TypeID == TypeID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.status = 3;

                    da.TTypes.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool DeleteMapType(long MapTypeID)
        {
            try
            {
                IList<TMapType> list = da.TMapTypes.Where(z => z.MapTypeID == MapTypeID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();
                    da.TMapTypes.DeleteOnSubmit(objVlaue);
                    da.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }

        //public bool DeleteMapType_ByNews(long LongNewsID)
        //{
        //    try
        //    {
        //        IList<TMapType> list = da.TMapTypes.Where(z => z.MapID == LongNewsID).ToList();
        //        if (list.Count > 0)
        //        {
        //            var objVlaue = list.First();
        //            da.TMapTypes.DeleteOnSubmit(objVlaue);
        //            da.SubmitChanges();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return false;
        //    }

        //}
        #endregion

        #region UserCreate Update delete

        public int CreateUser(string LoginID, string UserName, string Password, string Tel, string Fax, string Email, string Address, string CompanyName,
            bool IsExpire, DateTime ExpireDate, string Remark, DateTime RegDate, int RegUser, DateTime ModifyDate, int ModifyUser, string PermissionString,
            int UserType, string YahooID, int Gender, long UserLike, DateTime Brithday)
        {
            TUser objEnt = new TUser();

            objEnt.LoginID = LoginID;
            objEnt.UserName = UserName;
            objEnt.Password = Password;
            objEnt.Tel = Tel;
            objEnt.Fax = Fax;
            objEnt.Email = Email;
            objEnt.Address = Address;
            objEnt.CompanyName = CompanyName;
            objEnt.IsExpire = IsExpire;
            objEnt.ExpireDate = ExpireDate;
            objEnt.Remark = Remark;
            objEnt.RegDate = RegDate;
            objEnt.RegUser = RegUser;
            objEnt.ModifyDate = ModifyDate;
            objEnt.ModifyUser = ModifyUser;
            objEnt.PermissionString = PermissionString;
            objEnt.UserType = UserType;
            objEnt.YahooID = YahooID;
            objEnt.Gender = Gender;
            objEnt.UserLike = UserLike;
            objEnt.Brithday = Brithday;

            da.TUsers.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.UserID;
        }

        public Int64 Update_UserLike(Int64 UserID)
        {
            try
            {
                IList<TUser> list = da.TUsers.Where(z => z.UserID == UserID).ToList();
                if (list.Count > 0)
                {
                    var objEnt = list.First();

                    objEnt.UserLike = Helper.TryParseInt(objEnt.UserLike != null ? objEnt.UserLike.ToString() : "0", 0) + 1;

                    da.TUsers.Context.SubmitChanges();

                    return Utility.TryParseLong(objEnt.UserLike, 0);
                }
                return 0;

            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }

        public int countofkey(string trangthaikey, int loaicase)
        {
            IList<TKeyWork> list = da.TKeyWorks.Where(z => z.TinhTrangKeyWork == trangthaikey && z.LoaiCase == loaicase).ToList();
            if (list.Count > 0)
                return list.Count;
            return 0;
        }

        public bool ChangePassword(int p_userid, string p_pass)
        {
            try
            {
                IList<TUser> list = da.TUsers.Where(z => z.UserID == p_userid).ToList();
                if (list.Count > 0)
                {
                    var objEnt = list.First();

                    objEnt.Password = Utility.Encrypt(p_pass);

                    da.TUsers.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(TUser entUser)
        {
            try
            {
                /*int UserID, string UserName, string Tel, string Fax, string Address, string CompanyName,
           bool IsExpire, DateTime ExpireDate, string Remark, DateTime ModifyDate, int ModifyUser, string PermissionString,long UserLike*/
                IList<TUser> list = da.TUsers.Where(z => z.UserID == entUser.UserID).ToList();
                if (list.Count > 0)
                {
                    var objEnt = list.First();

                    objEnt.LoginID = entUser.LoginID;
                    objEnt.UserName = entUser.UserName;
                    objEnt.Tel = entUser.Tel;
                    objEnt.Fax = entUser.Fax;
                    objEnt.Address = entUser.Address;
                    objEnt.CompanyName = entUser.CompanyName;
                    objEnt.IsExpire = entUser.IsExpire;
                    objEnt.ExpireDate = entUser.ExpireDate;
                    objEnt.Remark = entUser.Remark;
                    objEnt.ModifyDate = entUser.ModifyDate;
                    objEnt.ModifyUser = entUser.ModifyUser;
                    objEnt.PermissionString = entUser.PermissionString;
                    objEnt.UserLike = entUser.UserLike;

                    da.TUsers.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool DeleteUser(int intUserID)
        {
            try
            {
                IList<TUser> list = da.TUsers.Where(z => z.UserID == intUserID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();
                    da.TUsers.DeleteOnSubmit(objVlaue);
                    da.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Create User Map Role

        public bool Updata_UserMapRole_bylike(int intUserID)
        {
            try
            {
                IList<TUserMapRole> list = da.TUserMapRoles.Where(z => z.UserID == intUserID).ToList();
                if (list.Count > 0)
                {
                    var objEnt = list.First();
                    TUserRoleMember entRole = GetRole_ByID(Helper.TryParseInt(objEnt.RoleMemberID.ToString(), 0));
                    if (entRole != null)
                    {

                        Int64 memberlike = CountLike_Member(intUserID);
                        Int64 Thanhvien = Utility.TryParseLong(Config.GetConfigValue("TRoleThanhVien_Max"), 0);
                        Int64 ytuonggia = Utility.TryParseLong(Config.GetConfigValue("TRoleYtuongGia_Max"), 0);
                        string KeyGia = Config.GetConfigValue("TUserRoleYtuongGia");
                        string KeyVang = Config.GetConfigValue("TUserRoleYtuongVang");
                        string KeyThanhVien = Config.GetConfigValue("TUserRoleThanhVien");

                        if (memberlike > Thanhvien && memberlike <= ytuonggia)
                        {
                            objEnt.RoleMemberID = GetRoleMemberID_ByUK(KeyGia);
                        }
                        if (memberlike > ytuonggia)
                        {
                            objEnt.RoleMemberID = GetRoleMemberID_ByUK(KeyVang);
                        }
                        if (memberlike < Thanhvien)
                        {
                            objEnt.RoleMemberID = GetRoleMemberID_ByUK(KeyThanhVien);
                        }

                    }

                    da.TUserMapRoles.Context.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUSerMapRole(int intUserID, int intRoleMemberID)
        {
            try
            {
                IList<TUserMapRole> list = da.TUserMapRoles.Where(z => z.UserID == intUserID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.RoleMemberID = intRoleMemberID;

                    da.TUserMapRoles.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public int CreateMapUserRole(int RoleMemberID, int UserID)
        {
            TUserMapRole objEnt = null;
            objEnt = new TUserMapRole();

            objEnt.RoleMemberID = RoleMemberID;
            objEnt.UserID = UserID;

            da.TUserMapRoles.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.UserMapRoleID;
        }



        public bool Updata_MemberLike_MapUserRole_checkRole(Int64 UserID, bool checkKeyWord)
        {
            try
            {
                IList<TUserMapRole> list = da.TUserMapRoles.Where(z => z.UserID == UserID).ToList();
                if (list.Count > 0)
                {
                    var objEnt = list.First();
                    TUserRoleMember entRole = GetRole_ByID(Helper.TryParseInt(objEnt.RoleMemberID.ToString(), 0));
                    if (entRole != null)
                    {
                        if (checkKeyWord == true)
                        {
                            if (entRole.KeyWord != Config.GetConfigValue("TUserRoleKimCuong"))
                            {
                                Int64 memberlike = Update_UserLike(UserID);
                                Int64 Thanhvien = Utility.TryParseLong(Config.GetConfigValue("TRoleThanhVien_Max"), 0);
                                Int64 ytuonggia = Utility.TryParseLong(Config.GetConfigValue("TRoleYtuongGia_Max"), 0);
                                string KeyGia = Config.GetConfigValue("TUserRoleYtuongGia");
                                string KeyVang = Config.GetConfigValue("TUserRoleYtuongVang");

                                if (memberlike > Thanhvien && memberlike <= ytuonggia)
                                {
                                    objEnt.RoleMemberID = GetRoleMemberID_ByUK(KeyGia);
                                }
                                if (memberlike > ytuonggia)
                                {
                                    objEnt.RoleMemberID = GetRoleMemberID_ByUK(KeyVang);
                                }
                            }
                        }
                        else
                        {
                            Int64 memberlike = Update_UserLike(UserID);
                            Int64 Thanhvien = Utility.TryParseLong(Config.GetConfigValue("TRoleThanhVien_Max"), 0);
                            Int64 ytuonggia = Utility.TryParseLong(Config.GetConfigValue("TRoleYtuongGia_Max"), 0);
                            string KeyGia = Config.GetConfigValue("TUserRoleYtuongGia");
                            string KeyVang = Config.GetConfigValue("TUserRoleYtuongVang");

                            if (memberlike > Thanhvien && memberlike <= ytuonggia)
                            {
                                objEnt.RoleMemberID = GetRoleMemberID_ByUK(KeyGia);
                            }
                            if (memberlike > ytuonggia)
                            {
                                objEnt.RoleMemberID = GetRoleMemberID_ByUK(KeyVang);
                            }
                        }

                    }

                    da.TUserMapRoles.Context.SubmitChanges();

                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public DataSet Rows_TTKeyWork(int p_page, int p_pageSize, DateTime fromDate, DateTime toDate,
        int status, int p_searchType, string p_searchText, string iduser)
        {
            string sql = string.Format(@"[TTKeyWork_Rows] 
                                            @intPage = {0},                         
	                                        @intPageSize = {1},                              
	                                        @fromDate  = '{2:yyyy-MM-dd hh:mm:ss}',            
	                                        @toDate  = '{3:yyyy-MM-dd hh:mm:ss}',         
	                                        @intStatus  = {4},            
	                                        @intSearchType  = {5},            
	                                        @strSearchText  = N'{6}',@strUserID ={7}"

                , p_page, p_pageSize, fromDate, toDate, status, p_searchType, p_searchText, iduser);
            return (new ConnectSQL()).connect(sql);
        }
        public DataSet Casework_Rows(int p_page, int p_pageSize, DateTime fromDate, DateTime toDate,
       int trangthai, int loaiphep, int p_searchType, string p_searchText, int iduser, int parenid, int IdnhomDuyet, int intIDTrP, int intIDPP, int intIDTrN, int idcty)
        {
            string sql = string.Format(@"[Casework_Rows] 
                                            @intPage = {0},                         
	                                        @intPageSize = {1},                              
	                                        @fromDate  = '{2:yyyy-MM-dd hh:mm:ss}',            
	                                        @toDate  = '{3:yyyy-MM-dd hh:mm:ss}',         
	                                        @inttrangthai  = {4},            
	                                        @intloaiphep  = {5},
                                            @intsearchType  = {6},
	                                        @strSearchText  = N'{7}',
                                            @intUserID ={8},                                           
                                           @intParentID={9},@intNhomDuyet={10},@intIDTrP={11},@intIDPP={12},@intIDTrN={13},@intIDCTY={14}"

                , p_page, p_pageSize, fromDate, toDate, trangthai, loaiphep, p_searchType, p_searchText, iduser, parenid, IdnhomDuyet, intIDTrP, intIDPP, intIDTrN, idcty);
            return (new ConnectSQL()).connect(sql);
        }
        public DataTable Rows_Phep_byID(DateTime fromDate, DateTime toDate, int loaiphep, int iduser)
        {
            string sql = string.Format(@"[Rows_Phep_byID]                                                                          
	                                        @fromDate  = '{0:yyyy-MM-dd}',            
	                                        @toDate  = '{1:yyyy-MM-dd}',     
	                                        @intloaiphep  = {2},
                                            @strUserID ={3}"
                , fromDate, toDate, loaiphep, iduser);

            return (new ConnectSQL()).connect_dt(sql);
        }
        public DataTable Rows_Checkinout_byname(DateTime fromDate, DateTime toDate, int loaiphep, string nameuser)
        {
            string sql = string.Format(@"[Rows_Checkinout_byname]                                                                          
	                                        @fromDate  = '{0:yyyy-MM-dd}',            
	                                        @toDate  = '{1:yyyy-MM-dd}',     
	                                        @intloaiphep  = {2},
                                            @strUserID =N'{3}'"
                , fromDate, toDate, loaiphep, nameuser);

            return (new ConnectSQL()).connect_dt(sql);
        }

        public DataTable ThongkePhep_Rows(DateTime fromDate, DateTime toDate)
        {
            string sql = string.Format(@"[ThongkePhep_Rows]  
                                            @fromDate  = '{0:yyyy-MM-dd hh:mm:ss}',            
	                                        @toDate  = '{1:yyyy-MM-dd hh:mm:ss}'", fromDate, toDate);



            return (new ConnectSQL()).connect_dt(sql);
        }
        public DataSet Casework_Rows_byID(int p_page, int p_pageSize, DateTime fromDate, DateTime toDate,
       int trangthai, int loaiphep, int searchtype, string p_searchText, int iduser, int IDCty, int loaiNV)
        {
            string sql = string.Format(@"[Casework_Rows_byID] 
                                            @intPage = {0},                         
	                                        @intPageSize = {1},                              
	                                        @fromDate  = '{2:yyyy-MM-dd hh:mm:ss}',            
	                                        @toDate  = '{3:yyyy-MM-dd hh:mm:ss}',          
	                                        @inttrangthai  = {4},            
	                                        @intloaiphep  = {5},
                                            @intsearchType  = {6},
	                                        @strSearchText  = N'{7}',
                                            @strUserID ={8},
                                            @intIDCty = {9},                                           
                                            @intLoaiNV={10}"

                , p_page, p_pageSize, fromDate, toDate, trangthai, loaiphep, searchtype, p_searchText, iduser, IDCty, loaiNV);
            return (new ConnectSQL()).connect(sql);
        }
        public DataSet CheckInOut_Rows_byID(int p_page, int p_pageSize, DateTime fromDate, DateTime toDate,
      int trangthai, int loaiphep, int searchtype, string p_searchText, int iduser, int IDCty, int loaiNV)
        {
            string sql = string.Format(@"[CheckInOut_Rows_byID] 
                                            @intPage = {0},                         
	                                        @intPageSize = {1},                              
	                                        @fromDate  = '{2:yyyy-MM-dd hh:mm:ss}',            
	                                        @toDate  = '{3:yyyy-MM-dd hh:mm:ss}',          
	                                        @inttrangthai  = {4},            
	                                        @intloaiphep  = {5},
                                            @intsearchType  = {6},
	                                        @strSearchText  = N'{7}',
                                            @strUserID ={8},
                                            @intIDCty = {9},                                           
                                            @intLoaiNV={10}"

                , p_page, p_pageSize, fromDate, toDate, trangthai, loaiphep, searchtype, p_searchText, iduser, IDCty, loaiNV);
            return (new ConnectSQL()).connect(sql);
        }
        public DataSet NhanVien_Rows_byID(int p_page, int p_pageSize, DateTime fromDate, DateTime toDate,
     int tructhuoccty, int loaiNV, int searchtype, string p_searchText, int iduser, int IDPhong)
        {
            string sql = string.Format(@"[NhanVien_Rows_byID] 
                                            @intPage = {0},                         
	                                        @intPageSize = {1},                              
	                                        @fromDate  = '{2:yyyy-MM-dd hh:mm:ss}',            
	                                        @toDate  = '{3:yyyy-MM-dd hh:mm:ss}',         
	                                        @intCTy  = {4},            
	                                        @intloaiNV  = {5},
                                            @intsearchType  = {6},
	                                        @strSearchText  = N'{7}',
                                            @strUserID ={8},
                                            @intPhong = {9}                                           
                                            "

                , p_page, p_pageSize, fromDate, toDate, tructhuoccty, loaiNV, searchtype, p_searchText, iduser, IDPhong);
            return (new ConnectSQL()).connect(sql);
        }
        public DataSet NhanVien_Rows_byID_modify(int p_page, int p_pageSize, int tructhuoccty, int loaiNV)
        {
            string sql = string.Format(@"[NhanVien_Rows_byID_modify] 
                                            @intPage = {0},                         
	                                        @intPageSize = {1},
	                                        @intCTy  = {2},            
	                                        @intloaiNV  = {3}"
                , p_page, p_pageSize, tructhuoccty, loaiNV);
            return (new ConnectSQL()).connect(sql);
        }
        public DataSet Rows_TTKKeyWork(int p_page, int p_pageSize, DateTime fromDate, DateTime toDate,
     int status, int p_searchType, string p_searchText, int iduser)
        {
            string sql = string.Format(@"[TTKKeyWork_Rows] 
                                            @intPage = {0},                         
	                                        @intPageSize = {1},                              
	                                        @fromDate  = '{2:yyyy-MM-dd hh:mm:ss}',            
	                                        @toDate  = '{3:yyyy-MM-dd hh:mm:ss}',         
	                                        @intStatus  = {4},            
	                                        @intSearchType  = {5},            
	                                        @strSearchText  = N'{6}',@intUserID ={7} "

                , p_page, p_pageSize, fromDate, toDate, status, p_searchType, p_searchText, iduser);
            return (new ConnectSQL()).connect(sql);
        }


        #endregion

        #region Unevien
        public bool InsertUngVien(VM_UngVien vmUngVien)
        {
            try
            {
                if (vmUngVien.UngVien == null)
                {
                    Alert.Show("Null");
                }
                UngVien objEnt = GetUngVienByID(vmUngVien.UngVien.Id);
                string backupname = objEnt.HoTen;
                if (objEnt != null)
                {
                    objEnt.SendMailPV = "Chưa gửi";
                    objEnt.SendMailTuyenDung = "Chưa gửi";
                    objEnt.HoTen = vmUngVien.UngVien.HoTen;
                    objEnt.NgaySinh = vmUngVien.UngVien.NgaySinh;
                    objEnt.NoiSinh = vmUngVien.UngVien.NoiSinh;
                    objEnt.GioiTinh = vmUngVien.UngVien.GioiTinh;
                    objEnt.Email = vmUngVien.UngVien.Email;
                    objEnt.SoDt = vmUngVien.UngVien.SoDt;
                    objEnt.Dantoc = vmUngVien.UngVien.Dantoc;
                    objEnt.Tongiao = vmUngVien.UngVien.Tongiao;
                    objEnt.CMND = vmUngVien.UngVien.CMND;
                    objEnt.NgayCMND = vmUngVien.UngVien.NgayCMND;
                    objEnt.NoiCapCMND = vmUngVien.UngVien.NoiCapCMND;
                    objEnt.DCTamTru = vmUngVien.UngVien.DCTamTru;
                    objEnt.DCThuongTru = vmUngVien.UngVien.DCThuongTru;
                    if (vmUngVien.ThongTinViTri != null)
                    {
                        objEnt.UngCuViTri = JsonConvert.SerializeObject(vmUngVien.ThongTinViTri);
                    }
                    else
                    {
                        objEnt.UngCuViTri = "";
                    }
                    if (vmUngVien.GiaDinh != null)
                    {
                        objEnt.GiaDinh = JsonConvert.SerializeObject(vmUngVien.GiaDinh);
                    }
                    else
                    {
                        objEnt.GiaDinh = "";
                    }
                    objEnt.ThuongTat = false;
                    if (vmUngVien.Quatrinhdaotao != null)
                    {
                        objEnt.QuaTrinhDaoTao = JsonConvert.SerializeObject(vmUngVien.Quatrinhdaotao);
                    }
                    else
                    {
                        objEnt.QuaTrinhDaoTao = "";
                    }
                    if (vmUngVien.lstQuaTrinhLamViec != null)
                    {
                        objEnt.QuaTrinhLamViec = JsonConvert.SerializeObject(vmUngVien.lstQuaTrinhLamViec);
                    }
                    else
                    {
                        objEnt.QuaTrinhLamViec = "";
                    }
                    //
                    if (vmUngVien.KyNang != null)
                    {
                        objEnt.KyNang = JsonConvert.SerializeObject(vmUngVien.KyNang);
                    }
                    else
                    {
                        objEnt.KyNang = "";
                    }
                    if (vmUngVien.TinhCachCaNhan != null)
                    {
                        objEnt.TinhCachCaNhan = JsonConvert.SerializeObject(vmUngVien.TinhCachCaNhan);
                    }
                    else
                    {
                        objEnt.TinhCachCaNhan = "";
                    }
                    //
                    if (vmUngVien.QuyenLoiNoiLamViec != null)
                    {
                        objEnt.QuyenLoiNoiLamViec = JsonConvert.SerializeObject(vmUngVien.QuyenLoiNoiLamViec);
                    }
                    else
                    {
                        objEnt.QuyenLoiNoiLamViec = "";
                    }
                    if (vmUngVien.BanKSPhongVan != null)
                    {
                        objEnt.BanKSPhongVan = JsonConvert.SerializeObject(vmUngVien.BanKSPhongVan);
                    }
                    else
                    {
                        objEnt.QuyenLoiNoiLamViec = "";
                    }
                    //da.UngViens.InsertOnSubmit(objEnt); 
                    da.SubmitChanges();
                    //Cập nhật ngày nhập thông tin ứng viên
                    Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == objEnt.Id).FirstOrDefault();
                    if (uvtt != null)
                    {
                        uvtt.ThongTinUngVien = 1;
                        uvtt.NgayNhapThongTin = DateTime.Now;
                        da.SubmitChanges();
                    }
                    //Cap nhat dong bo nhan vien
                    //Dong bo User
                    TUser checkuser = da.TUsers.Where(m => m.IdNhansu == objEnt.IdNhanVien).FirstOrDefault();
                    if (checkuser != null)
                    {
                        checkuser.UserName = backupname;
                        checkuser.Email = objEnt.Email;
                        checkuser.Address = objEnt.DCThuongTru;
                        checkuser.Tel = objEnt.SoDt;
                        da.SubmitChanges();
                    }
                    //Dong bo nhan vien
                    NhanVien dbNhanVien = da.NhanViens.Where(m => m.IdNhanVien == objEnt.IdNhanVien).FirstOrDefault();
                    if (dbNhanVien != null)
                    {
                        dbNhanVien.HoTen = objEnt.HoTen;
                        dbNhanVien.NgaySinh = objEnt.NgaySinh.Value;
                        dbNhanVien.DCThuongTru = objEnt.DCThuongTru;
                        dbNhanVien.DCTamTru = objEnt.DCTamTru;
                        dbNhanVien.Email = objEnt.Email;
                        dbNhanVien.SoDt = objEnt.SoDt;
                        dbNhanVien.NoiCapCMND = objEnt.NoiCapCMND;
                        dbNhanVien.NgayCMND = objEnt.NgayCMND;
                        dbNhanVien.NoiSinh = objEnt.NoiSinh;
                        dbNhanVien.Tongiao = objEnt.Tongiao;
                        Quatrinhdaotao Quatrinhdaotao = JsonConvert.DeserializeObject<Quatrinhdaotao>(objEnt.QuaTrinhDaoTao);
                        if (Quatrinhdaotao != null)
                        {
                            if (Quatrinhdaotao.Vanbang == "THPT")
                                dbNhanVien.Trinhdo = "1";
                            else
                            {
                                if (Quatrinhdaotao.Vanbang == "Trung cấp")
                                    dbNhanVien.Trinhdo = "5";
                                else
                                {
                                    if (Quatrinhdaotao.Vanbang == "Cao đẳng")
                                        dbNhanVien.Trinhdo = "2";
                                    else
                                    {
                                        if (Quatrinhdaotao.Vanbang == "Đại học")
                                            dbNhanVien.Trinhdo = "3";
                                        else
                                        {
                                            dbNhanVien.Trinhdo = "4";
                                        }
                                    }
                                }
                            }
                        }
                        da.SubmitChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //Alert.Show(ex.Message.ToString());
                return false;
            }
        }
        public bool CapnhatUngVienImport(Guid id, string path)
        {
            //UngVien uv = da.UngViens.Where(m => m.Id == id).FirstOrDefault();
            //uv.IsImport = 1;
            //uv.DateImport = DateTime.Now;
            //uv.ImportPath = path;
            //da.SubmitChanges();
            return true;
        }
        public IList<UngVien> GetListUngVien()
        {
            return da.UngViens.OrderBy(m => m.CreatedDate).ToList();
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
        public IList<UngVien> GetListUngVienByKeySearch(string Name)
        {
            return da.UngViens.ToList().OrderBy(m => m.CreatedDate).Where(m=>RemoveUnicode(m.HoTen.ToLower()).Contains(RemoveUnicode(Name.ToLower()))).ToList();
        }
        public List<VM_UngvienStatus> Laydanhsachungvien(int idyeucautuyendung)
        {
            var model = (from uv in da.UngViens.ToList()
                         join uvst in da.Ungvien_Trangthais.ToList()
                         on uv.Id equals uvst.IdUngVien
                         join yctd in da.YeuCauTuyenDungs.ToList()
                         on uv.IdYeuCau equals yctd.IdYeuCau
                         join pb in da.PhongBans.ToList()
                         on yctd.IDPhongBan equals pb.IDPhong
                         join ip in da.ImportFiles 
                         on uv.Id equals ip.IdUV into ips
                         from ipss in ips.DefaultIfEmpty()
                         where (idyeucautuyendung == 0 || yctd.IdYeuCau == idyeucautuyendung)
                         select new VM_UngvienStatus()
                         {
                             CreatedDate = uv.CreatedDate.HasValue ? uv.CreatedDate.Value : DateTime.MinValue,
                             DanhGia = uvst.DanhGia.Value,
                             DongBo = uvst.DongBo.Value,
                             GuiThumoi = uvst.GuiThumoi.Value,
                             HoTen = uv.HoTen,
                             Id = uv.Id,
                             Khaosathoinhap = uvst.Khaosathoinhap.Value,
                             NgayDanhgia = uvst.NgayDanhgia.HasValue ? uvst.NgayDanhgia.Value.ToShortDateString() : "",
                             Ngaydongbo = uvst.Ngaydongbo.HasValue ? uvst.Ngaydongbo.Value.ToShortDateString() : "",
                             NgayGuithumoi = uvst.NgayGuithumoi.HasValue ? uvst.NgayGuithumoi.Value.ToShortDateString() : "",
                             NgayKhaosatHoiNhap = uvst.NgayKhaosatHoiNhap.HasValue ? uvst.NgayKhaosatHoiNhap.Value.ToShortDateString() : "",
                             NgayNhapThongTin = uvst.NgayNhapThongTin.HasValue ? uvst.NgayNhapThongTin.Value.ToShortDateString() : "",
                             NgayTomTatPhucLoi = uvst.NgayTomTatPhucLoi.HasValue ? uvst.NgayTomTatPhucLoi.Value.ToShortDateString() : "",
                             NgayTomTatQuyTrinh = uvst.NgayTomTatQuyTrinh.HasValue ? uvst.NgayTomTatQuyTrinh.Value.ToShortDateString() : "",
                             NgayPV = uvst.NgàyPV.HasValue ? uvst.NgàyPV.Value.ToShortDateString() : "",
                             PhongVan = uvst.PhongVan.Value,
                             ThongTinUngVien = uvst.ThongTinUngVien.Value,
                             TomTatPhucLoi = uvst.TomTatPhucLoi.Value,
                             TomTatQuyTrinh = uvst.TomTatQuyTrinh.Value,
                             Phongban = pb.TenPhong,
                             TrucThuoc = pb.TrucThuoc.Value,
                             TrangThai = GetTrangThaiUngVien(uv.Id),
                             TrangThaiYCTD = yctd.TrangThai.Value,
                             TieuDe = yctd.TieuDe,
                             TMNVPath= ipss!=null? ipss.ImportFilePath:"",
                             TrangThaiUngVien=uv.TrangThaiUngVien.Value,
                             TrangThaiNhanViec= uv.TrangThaiNhanViec.HasValue?uv.TrangThaiNhanViec.Value:0,
                             TrangThaiPhongVan= uv.TrangThaiPhongVan.HasValue?uv.TrangThaiPhongVan.Value:0,
                             IDPhongBan=pb.IDPhong
                         }
                       );
            return model.ToList();
        }
        public string GetTrangThaiUngVien(Guid id)
        {
            Ungvien_Trangthai Ungvien_Trangthai = da.Ungvien_Trangthais.Where(m => m.IdUngVien == id).FirstOrDefault();
            if (Ungvien_Trangthai != null)
            {
                if (Ungvien_Trangthai.ThongTinUngVien == 0)
                {
                    return "Chờ nhập thông tin";
                }
                else
                {
                    if (Ungvien_Trangthai.PhongVan == 0)
                    {
                        return "Chờ phỏng vấn";
                    }
                    else
                    {
                        if (Ungvien_Trangthai.DanhGia == 0)
                        {
                            return "Chờ đánh giá";
                        }
                        else
                        {
                           
                            if (Ungvien_Trangthai.GuiThumoi == 0)
                            {
                                DotTuyenDung dtd = da.DotTuyenDungs.Where(m => m.IDUngVien == id).FirstOrDefault();
                                DanhGiaTuyenDung dgtd = da.DanhGiaTuyenDungs.Where(m => m.IdUngVien == id).FirstOrDefault();

                                if (dgtd == null)
                                {
                                    return "";
                                }
                                if (dgtd.Ketqualan1 == 1)
                                {   
                                    return dgtd.Quyetdinhlan1;
                                }
                                else
                                {
                                    if (Ungvien_Trangthai.NgayGoiPV2.HasValue)
                                    {
                                        DanhGiaTuyenDung dgtd2 = da.DanhGiaTuyenDungs.Where(m => m.IdUngVien == id && m.DotPV == 2).FirstOrDefault();
                                        if (dgtd2 != null)
                                        {
                                            if (dgtd2.Ketqualan2 == 1)
                                                return "Đạt (Lần 2)";
                                            else
                                                return dgtd2.Quyetdinhlan2 + " (Lần 2)";
                                        }
                                        return "Chờ đánh giá (Lần 2)";
                                    }
                                    else
                                    {
                                        if (dgtd.Quyetdinhlan1 != null)
                                        {
                                            return dgtd.Quyetdinhlan1;
                                        }
                                        return "Đánh giá chưa đạt";

                                    }

                                }
                            }
                            else
                            {
                                if (Ungvien_Trangthai.DongBo == 0)
                                {
                                    return "Chờ đồng bộ";
                                }
                                else
                                {
                                    if (Ungvien_Trangthai.Khaosathoinhap == 0)
                                    {
                                        return "Chờ khảo sát";
                                    }
                                    else
                                    {
                                        if (Ungvien_Trangthai.DanhGiaThuViec == null)
                                        {
                                            return "Chờ đánh giá thử việc";
                                        }
                                        else
                                        {
                                            var dgtv = da.Danhgiathuviecs.Where(m => m.IdNhanVien == id).FirstOrDefault();
                                            if (dgtv.NgayNguoiDanhGia == null)
                                                return "Chờ người đánh giá thử việc";
                                            if (dgtv.NgayTruongPhong == null)
                                            {
                                                var getdsta = JsonConvert.DeserializeObject<VM_DanhGiaThuViec>(dgtv.DanhGia);
                                                if (getdsta.TiepnhanStatus == 1)
                                                    return "Đánh giá thử việc: Không tiếp nhận";
                                                return "Chờ trưởng phòng đánh thử việc";
                                            }
                                            if (dgtv.NgayHCNS == null)
                                                return "Chờ HCNS đánh giá thử việc";
                                            if (dgtv.NgayBanGiamDoc == null)
                                                return "Chờ ban giám đốc đánh giá thử việc";
                                            if (Ungvien_Trangthai.GuiMailChucMung == 1)
                                                return "Nhân viên chính thức";
                                            return "Chờ Gửi mail chúc mừng";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }
        public UngVien GetUngVienByID(Guid id)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == id).FirstOrDefault();
            return uv;
        }
        public UngVien GetUngVienByIDNV(int IdNhanVien)
        {
            return da.UngViens.Where(m => m.IdNhanVien == IdNhanVien).FirstOrDefault();
        }
        public Guid CreateUngVien(string HoTen, int IdYeuCau)
        {
            UngVien objEnt = null;
            objEnt = new UngVien();
            objEnt.Id = Guid.NewGuid();
            objEnt.HoTen = HoTen;
            objEnt.IsEnable = 1;
            objEnt.Status = 0;
            objEnt.IdYeuCau = IdYeuCau;
            objEnt.TrangThaiUngVien = 1;
            objEnt.TrangThaiPhongVan =0;
            objEnt.TrangThaiNhanViec = 0;
            objEnt.CreatedDate = DateTime.Now;
            da.UngViens.InsertOnSubmit(objEnt);
            da.SubmitChanges();
            //Ungvien_Trangthai
            Ungvien_Trangthai Ungvien_Trangthai = new Ungvien_Trangthai();
            Ungvien_Trangthai.IdUngVien = objEnt.Id;
            Ungvien_Trangthai.ThongTinUngVien = 0;
            Ungvien_Trangthai.PhongVan = 0;
            Ungvien_Trangthai.DanhGia = 0;
            Ungvien_Trangthai.GuiThumoi = 0;
            Ungvien_Trangthai.DongBo = 0;
            Ungvien_Trangthai.TomTatQuyTrinh = 0;
            Ungvien_Trangthai.TomTatPhucLoi = 0;
            Ungvien_Trangthai.Khaosathoinhap = 0;
            da.Ungvien_Trangthais.InsertOnSubmit(Ungvien_Trangthai);
            da.SubmitChanges();
            return objEnt.Id;
        }

        public bool CheckExistUngVien(string hoten)
        {
            //Kiểm tra trong danh sách ứng viên trước
            var checkUngVien= da.UngViens.Any(m => m.HoTen.ToLower() == hoten.ToLower());
            if (checkUngVien)
                return true;
            //Kiểm tra trong danh sách nhân viên
            var checkNhanVien = da.NhanViens.Any(m => m.HoTen.ToLower() == hoten.ToLower());
            return checkNhanVien;
        }
        public bool UpdateIdNhanVienUngVien(UngVien uv, int IdNhanVien)
        {
            var ungvien = da.UngViens.Where(m => m.Id == uv.Id).FirstOrDefault();
            ungvien.IdNhanVien = IdNhanVien;
            ungvien.Status = 3;
            da.UngViens.Context.SubmitChanges();
            //Update status
            Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == uv.Id).FirstOrDefault();
            if (uvtt != null)
            {
                uvtt.DongBo = 1;
                uvtt.Ngaydongbo = DateTime.Now;
                da.SubmitChanges();
            }
            return true;
        }
        public bool DeleteUngVien(Guid IdNTD)
        {
            try
            {
                IList<UngVien> list = da.UngViens.Where(z => z.Id == IdNTD).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    da.UngViens.DeleteOnSubmit(objVlaue);
                    da.UngViens.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public YeuCauTuyenDung GetIDPhongBanByYeuCau(int IdYeuCau)
        {
            return da.YeuCauTuyenDungs.Where(m => m.IdYeuCau == IdYeuCau).FirstOrDefault();
        }

        public bool InsertUser(TUser TUser)
        {
            da.TUsers.InsertOnSubmit(TUser);
            da.SubmitChanges();
            return true;
        }
        public bool Capnhatngayphongvan(Guid Id, DateTime datetime)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == Id).FirstOrDefault();
            uv.Status = 1;
            uv.NgayPhongVan = datetime;
            da.UngViens.Context.SubmitChanges();
            //Tạo đợt tuyển dụng
            Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == Id).FirstOrDefault();

            //Cập nhật ứng viên trạng thái

            if (uvtt != null)
            {
                if (uvtt.PhongVan == 0)
                {
                    uvtt.PhongVan = 1;
                    uvtt.NgàyPV = DateTime.Now;
                    uvtt.NgayGoiPV = datetime;
                }
                else
                {
                    if (uvtt.NgayPhongVan2 == null)
                    {
                        uvtt.NgayPhongVan2 = DateTime.Now;
                        uvtt.NgayGoiPV2 = datetime;
                    }
                    else
                    {
                        if (uvtt.NgayPhongVan3 == null)
                        {
                            uvtt.NgayPhongVan3 = DateTime.Now;
                            uvtt.NgayGoiPV3 = datetime;
                        }
                    }
                }

                da.SubmitChanges();
            }
            return true;
        }
        public bool CapnhatDanhGia(DanhGiaTuyenDung dgtd, Guid idUngVien,int type)
        {
            //Kiểm tra xem có lần nào chưa
            var chkDGTD = da.DanhGiaTuyenDungs.Where(m => m.IdUngVien == idUngVien).Count();

            DanhGiaTuyenDung item = new DanhGiaTuyenDung();
            item.NgayPV = dgtd.NgayPV;
           
            item.Vitriungtuyen = dgtd.Vitriungtuyen;
            item.NguoiPV = dgtd.NguoiPV;
            item.IdUngVien = idUngVien;
            item.Mucluonghientai = dgtd.Mucluonghientai;
            item.Mucluongdenghi = dgtd.Mucluongdenghi;
            if (type == 1)
            {
                item.DotPV = 1;
                item.Ketqualan1 = dgtd.Ketqualan1;
                item.Quyetdinhlan1 = dgtd.Quyetdinhlan1;
            }
            if (type == 2)
            {
                item.DotPV = 2;
                item.Ketqualan2 = dgtd.Ketqualan2;
                item.Quyetdinhlan2 = dgtd.Quyetdinhlan2;
            }
            if (type == 3)
            {
                item.DotPV = 3;
                item.Ketqualan3 = dgtd.Ketqualan3;
                item.Quyetdinhlan3 = dgtd.Quyetdinhlan3;
            }
            item.NgayPV = dgtd.NgayPV;
            item.NguoiPV = dgtd.NguoiPV;
            item.KienThucChuyenNganh = dgtd.KienThucChuyenNganh;
            item.ChuyenMon = dgtd.ChuyenMon;
            item.KinhNghiem = dgtd.KinhNghiem;
            item.ThanhTich = dgtd.ThanhTich;
            item.TieuChi = dgtd.TieuChi;
            item.TinhCach = dgtd.TinhCach;
            item.KhaNangHoiNhap = dgtd.KhaNangHoiNhap;
            item.TongDiem = dgtd.TongDiem;
            item.QuyetDinh = dgtd.QuyetDinh;
            //
            item.Baocaocho = dgtd.Baocaocho;
            item.Ngaynhanviec = dgtd.Ngaynhanviec;
            item.Luongthuviec = dgtd.Luongthuviec;
            item.Thoigianthuviec = dgtd.Thoigianthuviec;
            item.Luongchinh = dgtd.Luongchinh;
            item.Phucap = dgtd.Phucap;
            item.Thoathuankhac = dgtd.Thoathuankhac;
            item.Kyduyet = dgtd.Kyduyet;
            item.Ngayduyet = dgtd.Ngayduyet;
            item.DinhHuong = dgtd.DinhHuong;
            if (dgtd.DanhGiaID == 0)
            {
                da.DanhGiaTuyenDungs.InsertOnSubmit(item);
                da.SubmitChanges();
            }
            else
            {
                da.SubmitChanges();
            }
            //Cập nhật nhân viên đánh giá
            Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == idUngVien).FirstOrDefault();
            uvtt.DanhGia = 1;
            uvtt.NgayDanhgia = DateTime.Now;
            da.SubmitChanges();
            return true;
        }
        public bool CapnhatnguoiDanhGia(int userid, Guid id, int type)
        {
            Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == id).FirstOrDefault();
            if (type == 0)
            {
                uvtt.NguoiPVDanhGia = userid;
                uvtt.NgayNguoiPVDanhGia = DateTime.Now;
                uvtt.NguoiDuyetDanhGia = userid;
                uvtt.NgayNguoiDuyetDanhGia = DateTime.Now;
            }
            if (type == 1)
            {
                uvtt.NguoiPVDanhGia = userid;
                uvtt.NgayNguoiPVDanhGia = DateTime.Now;
            }
            if (type == 2)
            {
                uvtt.NguoiDuyetDanhGia = userid;
                uvtt.NgayNguoiDuyetDanhGia = DateTime.Now;
            }
            da.SubmitChanges();
            return true;
        }
        public DanhGiaTuyenDung CheckDanhGiatuyenDung(Guid idUngVien)
        {
            DanhGiaTuyenDung dgtd = da.DanhGiaTuyenDungs.Where(m => m.IdUngVien.Value == idUngVien && m.DotPV == 1).FirstOrDefault();
            return dgtd;
        }
        public DanhGiaTuyenDung CheckDanhGiatuyenDung2(Guid idUngVien)
        {
            DanhGiaTuyenDung dgtd = da.DanhGiaTuyenDungs.Where(m => m.IdUngVien.Value == idUngVien && m.DotPV == 2).FirstOrDefault();
            return dgtd;
        }
        public DanhGiaTuyenDung CheckDanhGiatuyenDung3(Guid idUngVien)
        {
            DanhGiaTuyenDung dgtd = da.DanhGiaTuyenDungs.Where(m => m.IdUngVien.Value == idUngVien && m.DotPV == 3).FirstOrDefault();
            return dgtd;
        }
        public Danhgiathuviec CheckDanhGiaThuViec(Guid idUngVien)
        {
            Danhgiathuviec dttv = da.Danhgiathuviecs.Where(m => m.IdNhanVien == idUngVien).FirstOrDefault();

            return dttv;
        }
        public void DuyetDanhGia(Guid idUngVien)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == idUngVien).FirstOrDefault();
            uv.Status = 2;
            da.SubmitChanges();
        }
        public void CapNhatEmailPVStatus(Guid idUngVien)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == idUngVien).FirstOrDefault();
            uv.SendMailPV = "Đã gửi";
            da.SubmitChanges();
        }
        public void CapNhatEmailNVStatus(Guid idUngVien, DateTime dt)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == idUngVien).FirstOrDefault();
            uv.SendMailTuyenDung = "Đã gửi";
            da.SubmitChanges();
            //Cập nhật trạng thái
            Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == idUngVien).FirstOrDefault();
            if (uvtt != null)
            {
                uvtt.GuiThumoi = 1;
                uvtt.NgayGuithumoi = DateTime.Now;
                uvtt.NgayNhanViec = dt;
                da.SubmitChanges();
            }
        }

        public IList<PhongBan> GetPhongBanByTrucThuoc(int TrucThuoc)
        {
            return da.PhongBans.Where(m => m.TrucThuoc == TrucThuoc).ToList();
        }

        public int GetTrucThuocByYCTD(int IdYeuCau)
        {
            return da.YeuCauTuyenDungs.Where(m => m.IdYeuCau == IdYeuCau).FirstOrDefault().TrucThuoc.Value;
        }
        public bool Phanquyendanhgia(TUser tuser)
        {
            TMenuAdmin tmnad = da.TMenuAdmins.Where(m => m.MenuName == "Khảo sát hội nhập").FirstOrDefault();

            TUserMapLink tusm = da.TUserMapLinks.Where(m => m.UserID == tuser.UserID && m.MenuID == tmnad.MenuID).FirstOrDefault();
            if (tusm == null)
            {
                TUserMapLink tusermap = new TUserMapLink();
                tusermap.UserID = tuser.UserID;
                tusermap.MenuID = tmnad.MenuID;
                da.TUserMapLinks.InsertOnSubmit(tusermap);
                da.SubmitChanges();
            }

            return true;
        }
        public bool CheckPhanquyendanhgia(TUser tuser)
        {
            TMenuAdmin tmnad = da.TMenuAdmins.Where(m => m.MenuName == "Khảo sát hội nhập").FirstOrDefault();
            TUserMapLink tusm = da.TUserMapLinks.Where(m => m.UserID == tuser.UserID && m.MenuID == tmnad.MenuID).FirstOrDefault();
            return tusm != null ? true : false;
        }
        public bool Capnhatkhaosat14(int IdNhanSu)
        {
            Ungvienkhaosat uvks = da.Ungvienkhaosats.Where(m => m.IdNhanSu == IdNhanSu).FirstOrDefault();
            if (uvks != null)
            {
                uvks.Step = 2;
                da.SubmitChanges();
            }
            return true;
        }
        public bool Capnhatkhaosat2thang(int IdNhanSu)
        {
            Ungvienkhaosat uvks = da.Ungvienkhaosats.Where(m => m.IdNhanSu == IdNhanSu).FirstOrDefault();
            if (uvks != null)
            {
                uvks.Step = 3;
                da.SubmitChanges();
            }
            return true;
        }
        public bool PhanQuyenungvien(TUser tuser)
        {
            int parentMenu = da.TMenuAdmins.Where(m => m.MenuName == "Thông tin tuyển dụng").FirstOrDefault().MenuID;
            int quytinhMenu = da.TMenuAdmins.Where(m => m.MenuName == "Tóm tắt quy trình").FirstOrDefault().MenuID;
            int phucloiMenu = da.TMenuAdmins.Where(m => m.MenuName == "Tóm tắt phúc lợi").FirstOrDefault().MenuID;
            int khaosathoinhap = da.TMenuAdmins.Where(m => m.MenuName == "Khảo sát hội nhập").FirstOrDefault().MenuID;
            //Danh sách ngày phép
            int ngayphepMenu = da.TMenuAdmins.Where(m => m.MenuName == "Danh Sách Ngày Phép").FirstOrDefault().MenuID;
            int thongtindnMenu = da.TMenuAdmins.Where(m => m.MenuName == "Thông tin đăng nhập").FirstOrDefault().MenuID;
            TUserMapLink tusermap = new TUserMapLink();
            tusermap.UserID = tuser.UserID;
            tusermap.MenuID = parentMenu;
            da.TUserMapLinks.InsertOnSubmit(tusermap);
            da.SubmitChanges();
            //
            TUserMapLink tusermap2 = new TUserMapLink();
            tusermap2.UserID = tuser.UserID;
            tusermap2.MenuID = quytinhMenu;
            da.TUserMapLinks.InsertOnSubmit(tusermap2);
            da.SubmitChanges();
            //
            TUserMapLink tusermap3 = new TUserMapLink();
            tusermap3.UserID = tuser.UserID;
            tusermap3.MenuID = phucloiMenu;
            da.TUserMapLinks.InsertOnSubmit(tusermap3);
            da.SubmitChanges();
            //
            //TUserMapLink tusermap4 = new TUserMapLink();
            //tusermap4.UserID = tuser.UserID;
            //tusermap4.MenuID = khaosathoinhap;
            //da.TUserMapLinks.InsertOnSubmit(tusermap4);
            //da.SubmitChanges();
            TUserMapLink tusermapDSNgayPhep = new TUserMapLink();
            tusermapDSNgayPhep.UserID = tuser.UserID;
            tusermapDSNgayPhep.MenuID = ngayphepMenu;
            da.TUserMapLinks.InsertOnSubmit(tusermapDSNgayPhep);
            da.SubmitChanges();
            TUserMapLink ttDN = new TUserMapLink();
            ttDN.UserID = tuser.UserID;
            ttDN.MenuID = thongtindnMenu;
            da.TUserMapLinks.InsertOnSubmit(ttDN);
            da.SubmitChanges();
            return true;
        }

        #endregion
        #region Quytrinhhuongdan
        public IList<Quytrinhhuongdan> GetListQuytrinhhuongdan()
        {
            return da.Quytrinhhuongdans.ToList();
        }
        public IList<Chedophucloi> GetListChedophucloi()
        {
            return da.Chedophuclois.ToList();
        }
        public void checkNhanvienquytry(int IdNhanVien, int QuytinhhuongdanID)
        {
            var item = da.UngVienQuytrinhhuongdans.Where(m => m.IdNhanVien == IdNhanVien && m.QuytinhhuongdanID == QuytinhhuongdanID).FirstOrDefault();
            if (item == null && IdNhanVien != 0)
            {
                UngVienQuytrinhhuongdan UngVienQuytrinhhuongdan = new UngVienQuytrinhhuongdan();
                UngVienQuytrinhhuongdan.QuytinhhuongdanID = QuytinhhuongdanID;
                UngVienQuytrinhhuongdan.Status = 0;
                UngVienQuytrinhhuongdan.IdNhanVien = IdNhanVien;
                da.UngVienQuytrinhhuongdans.InsertOnSubmit(UngVienQuytrinhhuongdan);
                da.SubmitChanges();
            }
        }
        public void checkNhanvienphucloi(int IdNhanVien, int ChedoId)
        {
            var item = da.UngVienChedophuclois.Where(m => m.IdNhanVien == IdNhanVien && m.ChedoId == ChedoId).FirstOrDefault();
            if (item == null && IdNhanVien != 0)
            {
                UngVienChedophucloi UngVienChedophucloi = new UngVienChedophucloi();
                UngVienChedophucloi.ChedoId = ChedoId;
                UngVienChedophucloi.Status = 0;
                UngVienChedophucloi.IdNhanVien = IdNhanVien;
                da.UngVienChedophuclois.InsertOnSubmit(UngVienChedophucloi);
                da.SubmitChanges();
            }
        }
        public bool CapnhatStatusQuytrinh(int IdNhanVien, int QuytinhhuongdanID, int status)
        {
            var item = da.UngVienQuytrinhhuongdans.Where(m => m.IdNhanVien == IdNhanVien && m.QuytinhhuongdanID == QuytinhhuongdanID).FirstOrDefault();
            item.Status = status;
            da.UngVienQuytrinhhuongdans.Context.SubmitChanges();
            //Status ung vien
            TUser tuser = da.TUsers.Where(m => m.UserID == IdNhanVien).FirstOrDefault();
            UngVien uv = da.UngViens.Where(m => m.IdNhanVien == tuser.IdNhansu).FirstOrDefault();
            if (user_ntx != null)
            {
                if (uv != null)
                {
                    Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == uv.Id).FirstOrDefault();
                    if (uvtt != null)
                    {
                        uvtt.TomTatQuyTrinh = 1;
                        uvtt.NgayTomTatQuyTrinh = DateTime.Now;
                        da.SubmitChanges();
                    }
                }
            }
            return true;
        }

        public int CheckStatusQuytrinh(int IdNhanVien, int QuytinhhuongdanID)
        {
            var item = da.UngVienQuytrinhhuongdans.Where(m => m.IdNhanVien == IdNhanVien && m.QuytinhhuongdanID == QuytinhhuongdanID).FirstOrDefault();
            return item != null ? item.Status.Value : 0;
        }

        public bool CapnhatStatusPhucloi(int IdNhanVien, int ChedoId, int status)
        {
            var item = da.UngVienChedophuclois.Where(m => m.IdNhanVien == IdNhanVien && m.ChedoId == ChedoId).FirstOrDefault();
            item.Status = status;
            da.UngVienChedophuclois.Context.SubmitChanges();
            //Status ung vien
            TUser tuser = da.TUsers.Where(m => m.UserID == IdNhanVien).FirstOrDefault();
            UngVien uv = da.UngViens.Where(m => m.IdNhanVien == tuser.IdNhansu).FirstOrDefault();
            if (user_ntx != null)
            {
                if (uv != null)
                {
                    Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == uv.Id).FirstOrDefault();
                    if (uvtt != null)
                    {
                        uvtt.TomTatPhucLoi = 1;
                        uvtt.NgayTomTatPhucLoi = DateTime.Now;
                        da.SubmitChanges();
                    }
                }
            }
            return true;
        }
        public int CheckStatusPhucloi(int IdNhanVien, int ChedoId)
        {
            var item = da.UngVienChedophuclois.Where(m => m.IdNhanVien == IdNhanVien && m.ChedoId == ChedoId).FirstOrDefault();
            return item != null ? item.Status.Value : 0;
        }
        public bool CapnhatKhaosat7Ngay(int IdNhanSu, VM_KhaoSat7Ngay ks7ngay)
        {
            var chk = da.Ungvienkhaosats.Where(m => m.IdNhanSu == IdNhanSu).FirstOrDefault();
            if (chk == null)
            {
                Ungvienkhaosat Ungvienkhaosat = new Ungvienkhaosat();
                Ungvienkhaosat.Ks7Ngay = JsonConvert.SerializeObject(ks7ngay);
                Ungvienkhaosat.IdNhanSu = IdNhanSu;
                Ungvienkhaosat.Step = 1;
                Ungvienkhaosat.Ks7NgayDate = DateTime.Now;
                da.Ungvienkhaosats.InsertOnSubmit(Ungvienkhaosat);
                da.SubmitChanges();
            }
            else
            {
                chk.Ks7Ngay = JsonConvert.SerializeObject(ks7ngay);
                da.SubmitChanges();
            }
            //
            //Status ung vien
            UngVien uv = da.UngViens.Where(m => m.IdNhanVien == IdNhanSu).FirstOrDefault();
            if (user_ntx != null)
            {
                if (uv != null)
                {
                    Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == uv.Id).FirstOrDefault();
                    if (uvtt != null)
                    {
                        uvtt.Khaosathoinhap = 1;
                        uvtt.NgayKhaosatHoiNhap = DateTime.Now;
                        da.SubmitChanges();
                    }
                }
            }
            return true;
        }
        public bool CapnhatKhaosat14Ngay(int IdNhanSu, VM_KhaoSat14Ngay ks14ngay)
        {
            var chk = da.Ungvienkhaosats.Where(m => m.IdNhanSu == IdNhanSu).FirstOrDefault();
            if (chk != null)
            {
                chk.Ks14Ngay = JsonConvert.SerializeObject(ks14ngay);
                chk.Step = 2;
                chk.Ks14NgayDate = DateTime.Now;
                da.SubmitChanges();
            }
            return true;
        }
        public bool CapnhatKhaosat2Thang(int IdNhanSu, VM_Sau2Thang ks2thang)
        {
            var chk = da.Ungvienkhaosats.Where(m => m.IdNhanSu == IdNhanSu).FirstOrDefault();
            if (chk != null)
            {
                chk.ks2Thang = JsonConvert.SerializeObject(ks2thang);
                chk.Step = 3;
                chk.KS2ThangDate = DateTime.Now;
                da.SubmitChanges();
            }
            return true;
        }
        public Ungvienkhaosat GetUngvienkhaosat(int IdNhanSu)
        {
            return da.Ungvienkhaosats.Where(m => m.IdNhanSu == IdNhanSu).FirstOrDefault();
        }
        public TUser GetUserByNhanvienId(int IdNhansu)
        {
            return da.TUsers.Where(m => m.IdNhansu == IdNhansu).FirstOrDefault();
        }
        #region Ungvien_Trangthai
        public Ungvien_Trangthai GetUngvien_TrangthaiById(Guid id)
        {
            return da.Ungvien_Trangthais.Where(m => m.IdUngVien == id).FirstOrDefault();
        }
        public bool UpdateDanhgiathuviec(Danhgiathuviec dgtv)
        {
            if (dgtv.DanhGiaThuViecId == 0)
            {
                Danhgiathuviec danhgia = new Danhgiathuviec();
                danhgia.IdNhanVien = dgtv.IdNhanVien;
                danhgia.DanhGia = dgtv.DanhGia;
                danhgia.NguoiDanhGia = dgtv.NguoiDanhGia;
                danhgia.ChucVuDanhGia = dgtv.ChucVuDanhGia;
                da.Danhgiathuviecs.InsertOnSubmit(danhgia);
                //Cap nhat status
                UngVien uv = da.UngViens.Where(m => m.Id == danhgia.IdNhanVien.Value).FirstOrDefault();
                TUser tuser = da.TUsers.Where(m => m.UserID == uv.IdNhanVien.Value).FirstOrDefault();
                if (user_ntx != null)
                {
                    if (uv != null)
                    {
                        Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == uv.Id).FirstOrDefault();
                        if (uvtt != null)
                        {
                            uvtt.DanhGiaThuViec = 1;
                            uvtt.NgayDanhGiaThuViec = DateTime.Now;
                            da.SubmitChanges();
                        }
                    }
                }
            }
            da.SubmitChanges();
            return true;
        }
        public Danhgiathuviec GetDanhgiathuviec(Guid id)
        {
            return da.Danhgiathuviecs.Where(m => m.IdNhanVien == id).FirstOrDefault();
        }
        public bool UpdateSatusChucmung(Guid id)
        {
            var item = da.Ungvien_Trangthais.Where(m => m.IdUngVien == id).FirstOrDefault();
            item.GuiMailChucMung = 1;
            item.NgayGuiMailChucMung = DateTime.Now;
            da.SubmitChanges();
            return true;
        }
        public List<MailSendUngVien> GetListEmail(bool status, int type)
        {
            var model = da.MailSendUngViens.Where(m => m.Status == status && m.Type == type).ToList();
            return model;
        }

        public DotTuyenDung GetDotTuyenDung(Guid id, int Nguoiduyet)
        {
            return da.DotTuyenDungs.Where(m => m.IDUngVien == id && m.NguoiDuyet == Nguoiduyet).FirstOrDefault();
        }

        public DanhGiaTuyenDung GetDanhGiaTuyenDung(Guid id)
        {
            var data = da.DanhGiaTuyenDungs.Where(m => m.IdUngVien == id).FirstOrDefault();
            return data;
        }

        public bool checkUngVienQuytrinhhuongdan(int idNhanVien)
        {
            return da.UngVienQuytrinhhuongdans.Any(m => m.IdNhanVien == idNhanVien);
        }
        public bool checkungVienChedophucloi(int idNhanVien)
        {
            return da.UngVienChedophuclois.Any(m => m.IdNhanVien == idNhanVien);
        }
        public bool checkungVienKhaosat(int idNhanVien)
        {
            return da.Ungvienkhaosats.Any(m => m.IdNhanSu == idNhanVien);
        }
        public ThongTinNhanSu GetTruongPhong(int idphongban)
        {
            return da.ThongTinNhanSus.Where(m => m.PhongBan == idphongban && m.ViTri != 4).FirstOrDefault();
        }

        public bool CapnhatguimailDanhgia(Guid id, int type)
        {

            Danhgiathuviec dgtv = da.Danhgiathuviecs.Where(m => m.IdNhanVien == id).FirstOrDefault();
            if (dgtv == null)
            {
                Danhgiathuviec Danhgiathuviec = new Danhgiathuviec();
                Danhgiathuviec.IdNhanVien = id;
                da.Danhgiathuviecs.InsertOnSubmit(Danhgiathuviec);
                da.SubmitChanges();
                dgtv = da.Danhgiathuviecs.Where(m => m.IdNhanVien == id).FirstOrDefault();
                //

                UngVien uv = da.UngViens.Where(m => m.Id == id).FirstOrDefault();
                TUser tuser = da.TUsers.Where(m => m.UserID == uv.IdNhanVien.Value).FirstOrDefault();
                if (user_ntx != null)
                {
                    if (uv != null)
                    {
                        Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == uv.Id).FirstOrDefault();
                        if (uvtt != null)
                        {
                            uvtt.DanhGiaThuViec = 1;
                            uvtt.NgayDanhGiaThuViec = DateTime.Now;
                            da.SubmitChanges();
                        }
                    }
                }
            }
            if (type == 1)
                dgtv.MailNguoiDanhGia = 1;
            if (type == 2)
                dgtv.MailTruongPhong = 1;
            if (type == 4)
                dgtv.MailBanGiamDoc = 1;
            da.SubmitChanges();
            return true;
        }
        public bool Capnhattrangthaidanhgia(Guid id, int type, int iduser)
        {
            Danhgiathuviec dgtv = da.Danhgiathuviecs.Where(m => m.IdNhanVien == id).FirstOrDefault();
            if (type == 1)
            {
                dgtv.IdNguoiDanhGia = iduser;
                dgtv.NgayNguoiDanhGia = DateTime.Now;
            }
            if (type == 2)
            {
                dgtv.IdTruongPhong = iduser;
                dgtv.NgayTruongPhong = DateTime.Now;
            }
            if (type == 3)
            {
                dgtv.IdNguoiDanhGia = iduser;
                dgtv.NgayNguoiDanhGia = DateTime.Now;
                dgtv.IdTruongPhong = iduser;
                dgtv.NgayTruongPhong = DateTime.Now;
            }
            if (type == 4)
            {
                dgtv.IdHCNS = iduser;
                dgtv.NgayHCNS = DateTime.Now;
            }
            if (type == 5)
            {
                dgtv.IdBanGiamDoc = iduser;
                dgtv.NgayBanGiamDoc = DateTime.Now;
                Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == id).FirstOrDefault();
                uvtt.DanhGiaThuViec = 1;
                uvtt.NgayDanhGiaThuViec = DateTime.Now;
                da.SubmitChanges();
            }

            da.SubmitChanges();

            return true;
        }

        public NhanVien GetNhanVienByEmail(string Email)
        {
            TUser user = da.TUsers.Where(m => m.Email == Email).FirstOrDefault();
            NhanVien nv = new NhanVien();
            if (user != null)
            {
                nv = da.NhanViens.Where(m => m.IdNhanVien == user.IdNhansu).FirstOrDefault();

            }
            return nv;
        }
        public bool CapnhatEmailNguoiDanhGia(Guid id, int type)
        {
            Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == id).FirstOrDefault();
            if (type == 1)
                uvtt.MailNguoiDanhGia = DateTime.Now;
            if (type == 2)
                uvtt.MailNguoiDuyetDanhGia = DateTime.Now;
            da.SubmitChanges();
            return true;
        }

        public void CapNhatNextPV(Guid id)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == id).FirstOrDefault();
            uv.NgayPhongVan = DateTime.Now;
            Ungvien_Trangthai uvtt = da.Ungvien_Trangthais.Where(m => m.IdUngVien == id).FirstOrDefault();
            uvtt.PhongVan = 1;
            uvtt.NgàyPV = DateTime.Now;
            uvtt.NgayGoiPV = DateTime.Now;
            da.SubmitChanges();
        }
        public string GetTrucThuoc(int type)
        {
            string result = "";
            if (type == 1)
                result= "Nguyên Kim";
            if (type == 2)
                result= "Chính Nhân";
            if (type == 3)
                result= "SMC";
            return result;
        }
        public IEnumerable<VM_Thongketuyendung> GetThongKeTuyenDung()
        {
            var model = (from u in da.UngViens.ToList()
                         join yctd in da.YeuCauTuyenDungs.ToList()
                         on u.IdYeuCau equals yctd.IdYeuCau
                         join pb in da.PhongBans.ToList()
                         on yctd.IDPhongBan equals pb.IDPhong
                         select new VM_Thongketuyendung()
                         {
                             TrucThuoc =GetTrucThuoc(yctd.TrucThuoc.Value),
                             PhongBan = pb.TenPhong,
                             HoTenUV = u.HoTen,
                             ViTriTuyenDung= yctd.TieuDe
                         }
                       ).ToList();
            return model;
        }
        public int Demsoluongtuyendung(int idyctd)
        {
            var count = da.UngViens.Where(m => m.IdYeuCau == idyctd).Count();
            return count;
        }
        public string GetPhongbantructhuoc(int IDPhong)
        {
            PhongBan pb = da.PhongBans.Where(m => m.IDPhong == IDPhong).FirstOrDefault();
            if (pb != null)
            {
                if (pb.TrucThuoc == 1)
                {
                    return pb.TenPhong + " (Nguyên Kim)";
                }
                if (pb.TrucThuoc == 2)
                {
                    return pb.TenPhong + " (Chính Nhân)";
                }
                if (pb.TrucThuoc == 3)
                {
                    return pb.TenPhong + " (SMC)";
                }
            }
            return "";
        }
        public IEnumerable<VM_ThongKePhongBan> GetThongKePhongBan(DateTime tungay, DateTime denngay)
        {
            var modelGb = (from yctd in da.YeuCauTuyenDungs.ToList()
                           join pb in da.PhongBans.ToList()
                           on yctd.IDPhongBan equals pb.IDPhong
                           where yctd.NgayTao >=tungay && yctd.NgayTao<=denngay
                           select new VM_ThongKePhongBan()
                           {
                               PhongBan = GetPhongbantructhuoc(pb.IDPhong),
                               SLTuyenDung = yctd.Soluong.Value,
                               Dangtuyendung = Demsoluongtuyendung(yctd.IdYeuCau)
                           }
                       ).ToList(); 
            return modelGb;
        }
        #region Logg
        public void InsertLogg(string name,int userID)
        {
            LoggHistory log = new LoggHistory();
            log.LoggName = name;
            log.UserId = userID;
            log.LoggDate = DateTime.Now;
            da.LoggHistories.InsertOnSubmit(log);
            da.SubmitChanges();
        }

        public IList<MailSendUngVien> GetListMailSend()
        {
            return da.MailSendUngViens.ToList();
        }

        public IList<TUser> GetListUser()
        {
            return da.TUsers.ToList();
        }

        public bool InsertEmail(string Email)
        {
            try
            {
                MailSendUngVien ms = new MailSendUngVien();
                ms.Email = Email;
                ms.Status = true;
                ms.Type = 2;
                da.MailSendUngViens.InsertOnSubmit(ms);
                da.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void UpdateStatusEmail(int IDMail,bool status)
        {
            MailSendUngVien ms = da.MailSendUngViens.Where(m => m.IDMail == IDMail).FirstOrDefault();
            ms.Status = status;
            da.SubmitChanges();
        }
        public void DeleteEmail(int IDMail)
        {
            MailSendUngVien ms = da.MailSendUngViens.Where(m => m.IDMail == IDMail).FirstOrDefault();
            da.MailSendUngViens.DeleteOnSubmit(ms);
            da.SubmitChanges();
        }
        public void CapnhatNgayvaolam(Guid id,DateTime ngaycapnhat)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == id).FirstOrDefault();
            ThongTinNhanSu ttns = da.ThongTinNhanSus.Where(m => m.IdNhanVien == uv.IdNhanVien).FirstOrDefault();
            ttns.NgayVaoLam = ngaycapnhat;
            da.SubmitChanges();
        }

        //ImportFile
        public bool ImportFileLog(Guid id,string path)
        {
            try
            {
                ImportFile ip = da.ImportFiles.Where(m => m.IdUV == id).FirstOrDefault();
                if (ip == null)
                    ip = new ImportFile();
                ip.DateImport = DateTime.Now;
                ip.ImportFilePath = path;
                ip.Type = 2;
                ip.IdUV = id;
                if (ip.ImportFileId == 0)
                    da.ImportFiles.InsertOnSubmit(ip);
                da.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void Capnhattrangthai(Guid id,int Type)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == id).FirstOrDefault();
            uv.TrangThaiUngVien = Type;
            da.SubmitChanges();
        }
        public void UpdateStatus(Guid id, int Type,int Type2)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == id).FirstOrDefault();
            uv.TrangThaiPhongVan = Type;
            uv.TrangThaiNhanViec = Type2;
            da.SubmitChanges();
        }
        public void UpdateStatusYCTD(int IdYeuCau,int IsDone)
        {
            YeuCauTuyenDung yctd = da.YeuCauTuyenDungs.Where(m => m.IdYeuCau == IdYeuCau).FirstOrDefault();
            yctd.IsDone = IsDone;
            da.SubmitChanges();
        }

        public void CapNhatNgayNhanViec(int idnhanvien,DateTime Ngaynhanviec)
        {
            ThongTinNhanSu ttns = da.ThongTinNhanSus.Where(m => m.IdNhanVien == idnhanvien).FirstOrDefault();
            if (ttns != null)
            {
                ttns.NgayVaoLam = Ngaynhanviec;
                da.SubmitChanges();
            }
        }
        public ImportFile GetImportFile(Guid id)
        {
            return da.ImportFiles.Where(m => m.IdUV == id).FirstOrDefault();
        }


        public void InsertMessage(string MessageContents)
        {
            tbMessage tbMessage = new tbMessage();
            tbMessage.DateCreate = DateTime.Now;
            tbMessage.IsRead = 0;
            tbMessage.MessageContents = MessageContents;
            da.tbMessages.InsertOnSubmit(tbMessage);
            da.SubmitChanges();
        }

        public int GetMessageCount()
        {
            return da.tbMessages.Where(m => m.IsRead == 0).Count();
        }

        public string GetMessageContent()
        {
            string result = "";
            var model = da.tbMessages.Where(m=>m.IsRead==0).OrderByDescending(m=>m.tbMessageId).ToList();
            foreach(var item in model)
            {
                result += "["+item.DateCreate.Value+"]"+" | "+ item.MessageContents +'_'+item.tbMessageId+ ",";
            }
            return result;
        }
        public bool HideMessage(int tbMessageId)
        {
            tbMessage _tbMessages = da.tbMessages.Where(m => m.tbMessageId == tbMessageId).FirstOrDefault();
            if (_tbMessages != null)
            {
                _tbMessages.IsRead = 1;
                da.SubmitChanges();
            }
            return true;
        }

        public void capnhat_uvyctd(Guid id, int IdYeuCau)
        {
            UngVien uv = da.UngViens.Where(m => m.Id == id).FirstOrDefault();
            if (uv != null)
            {
                uv.IdYeuCau = IdYeuCau;
                da.SubmitChanges();
            }
        }

        public double GetSoNgayLam(double IdNhanvien)
        {
            double songaylam = 0;
            ThongTinNhanSu ttns = da.ThongTinNhanSus.Where(m => m.IdNhanVien == IdNhanvien).FirstOrDefault();
            if (ttns != null)
            {
                songaylam = double.Parse((DateTime.Now - ttns.NgayVaoLam.Value).Days.ToString()) +1;
            }
            return songaylam;
        }
        public IEnumerable<VM_ThongKeKhaoSat> GetTKKhaoSat()
        {
            var model = (from uv in da.UngViens.ToList()
                         join uvks in da.Ungvienkhaosats.ToList()
                         on uv.IdNhanVien equals uvks.IdNhanSu into uvs
                         from uvs2 in uvs.DefaultIfEmpty()
                         where uv.IdNhanVien!=null
                         select new VM_ThongKeKhaoSat()
                         {
                             HoTenUngVien=uv.HoTen,
                             Ks7Ngay=(uvs2 != null && uvs2.Ks7NgayDate!=null) ?uvs2.Ks7NgayDate.Value:DateTime.MinValue,
                             Ks14Ngay=(uvs2 != null && uvs2.Ks14NgayDate!=null) ?uvs2.Ks14NgayDate.Value:DateTime.MinValue,
                             Ks2Thang= (uvs2 != null && uvs2.KS2ThangDate!=null) ?uvs2.KS2ThangDate.Value:DateTime.MinValue,
                             Step=uvs2!=null?uvs2.Step.Value:0,
                             CreateDate=uv.CreatedDate.Value,
                             SoNgayLam=GetSoNgayLam(uv.IdNhanVien.Value)
                         }).OrderByDescending(m=>m.CreateDate).ToList();
            return model;
        }
        public TUser CheckDangNhap(string username,string password)
        {
            var check = da.TUsers.ToList().Where(m => m.LoginID.ToLower() == username.ToLower() && m.Password == Utility.Encrypt(password)).FirstOrDefault();
            if (check != null)
                return check;
            return check;
        }
        public int Getchucvu(int iduser)
        {
            int chucvu = 0;
            TUser tuser = da.TUsers.Where(m => m.UserID == iduser).FirstOrDefault();
            if (tuser != null)
            {
                ThongTinNhanSu ttns = da.ThongTinNhanSus.Where(m => m.IdNhanVien == tuser.IdNhansu).FirstOrDefault();
                if (ttns != null)
                {
                    chucvu = ttns.ViTri.Value;
                }
            }
            return chucvu;
        }
        public bool CheckExistDanhgiathuviec(Guid IdNhanVien)
        {
            var check = da.Danhgiathuviecs.Any(m => m.IdNhanVien == IdNhanVien);
            return check;
        }
        public bool CheckExistDanhGiaTuyenDung(Guid IdNhanVien)
        {
            var check = da.DanhGiaTuyenDungs.Any(m => m.IdUngVien == IdNhanVien);
            return check;
        }
        #endregion
        #endregion
        #endregion
    }
}