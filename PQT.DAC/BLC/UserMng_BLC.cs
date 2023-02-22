using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PQT.API.File;

namespace PQT.DAC
{
    public class UserMng_BLC
    {
        FileManager fileMng = new FileManager();
        UserMngDataDataContext da = null;
        public UserMng_BLC()
        {
            if (da == null)
            {
                da = new UserMngDataDataContext();
            }
        }

        #region TEmailMng

        //public IList<TEmailMng> RowsEmailMng()
        //{
        //    return da.TEmailMngs.ToList();
        //}

        //public TEmailMng RowEmailMng_ByKey(string KeyWord)
        //{
        //    IList<TEmailMng> list = da.TEmailMngs.Where(z => z.KeyWord == KeyWord).ToList();
        //    if (list.Count > 0)
        //    {
        //        return list.First();
        //    }
        //    return null;
        //}

        //public TEmailMng RowEmailMng_ByID(int EmailMngID)
        //{
        //    IList<TEmailMng> list = da.TEmailMngs.Where(z => z.EmailMngID == EmailMngID).ToList();
        //    if (list.Count > 0)
        //    {
        //        return list.First();
        //    }
        //    return null;
        //}

        //public int CreateUpdateEmailMng(TEmailMng ent)
        //{
        //    TEmailMng checkEtt = RowEmailMng_ByID(ent.EmailMngID);
        //    if (checkEtt == null)
        //    {
        //        TEmailMng objEnt = ent;
        //        da.TEmailMngs.InsertOnSubmit(objEnt);
        //        da.SubmitChanges();
        //        return objEnt.EmailMngID;
        //    }
        //    else
        //    {
        //        TEmailMng objEnt = checkEtt;
        //        objEnt.EmailName = ent.EmailName;
        //        objEnt.EmailTo = ent.EmailTo;
        //        objEnt.EmailPass = ent.EmailPass;
        //        objEnt.EmailCC = ent.EmailCC;
        //        objEnt.EmailHost = ent.EmailHost;
        //        objEnt.EmailPorst = ent.EmailPorst;
        //        objEnt.EmailSSL = ent.EmailSSL;
        //        objEnt.KeyWord = ent.KeyWord;
        //        da.TEmailMngs.Context.SubmitChanges();
        //        return objEnt.EmailMngID;
        //    }
        //}
        //public bool DeleteTEmailMng(int EmailMngID)
        //{
        //    try
        //    {
        //        TEmailMng checkEtt = RowEmailMng_ByID(EmailMngID);
        //        if (checkEtt!=null)
        //        {
        //            da.TEmailMngs.DeleteOnSubmit(checkEtt);
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

        #region TUserMapLink

        public TUserMapLink RowUserMapLink_ByMenuID_UserID(int MenuID, int intUserID)
        {
            IList<TUserMapLink> list = da.TUserMapLinks.Where(z => z.MenuID == MenuID && z.UserID == intUserID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public IList<TUserMapLink> RowsUserMapLink_ByUserID(int UserID)
        {
            return da.TUserMapLinks.Where(z => z.UserID == UserID).ToList();
        }

        public TUserMapLink RowUserMapLink_ByMapID(long MapLinkID)
        {
            IList<TUserMapLink> list = da.TUserMapLinks.Where(z => z.MapLinkID == MapLinkID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public long CreateUserMapLink(TUserMapLink ent)
        {
            TUserMapLink checkEtt = RowUserMapLink_ByMapID(ent.MapLinkID);
            if (checkEtt == null)
            {
                TUserMapLink objEnt = ent;
                da.TUserMapLinks.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.MapLinkID;
            }
            else
            {
                return checkEtt.MapLinkID;
            }
            //else
            //{
            //    TUserMapLink objEnt = checkEtt;
            //    objEnt.RoleMemberID = ent.RoleMemberID;
            //    objEnt.RoleName = ent.RoleName;
            //    objEnt.StartNumber = ent.StartNumber;
            //    objEnt.EndNumber = ent.EndNumber;
            //    objEnt.KeyWord = ent.KeyWord;
            //    objEnt.status = ent.status;
            //    objEnt.SortOrder = ent.SortOrder;
            //    objEnt.Note = ent.Note;
            //    da.TUserMapLinks.Context.SubmitChanges();
            //    return objEnt.RoleMemberID;
            //}
        }

        public bool DeleteUserMapLink_byUser(int UserID)
        {
            try
            {
                IList<TUserMapLink> list = da.TUserMapLinks.Where(z => z.UserID == UserID).ToList();
                if (list.Count > 0)
                {
                    da.TUserMapLinks.DeleteAllOnSubmit(list);// .DeleteOnSubmit(list.f);
                    //da.TUserMapLinks.DeleteOnSubmit(list.First());
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

        #region TMenuAdmin

        public TMenuAdmin RowMenuAdmin_ByOption1(string @strUK)
        {
            IList<TMenuAdmin> list = da.TMenuAdmins.Where(z => z.Option1 == @strUK.Trim()).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public TMenuAdmin RowMenuAdmin_ByLink(string @linkPage)
        {
            IList<TMenuAdmin> list = da.TMenuAdmins.Where(z => z.Link == linkPage.Trim()).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public DataTable RowsMenuAdminByParentID(int @intMenuType, int @intStatus, int @intParentID, int @intUser)
        {
            string sql = string.Format("[p_TMenuAdminByParentID_Rows] @intMenuType={0}, @intStatus={1}, @intParentID={2}, @intUser={3}, @strKeyWord=''",
                @intMenuType, @intStatus, @intParentID, @intUser);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public DataTable RowsMenuAdminByParentID(int @intMenuType, int @intStatus, int @intParentID, int @intUser, string @strKeyWord)
        {
            string sql = string.Format("[p_TMenuAdminByParentID_Rows] @intMenuType={0}, @intStatus={1}, @intParentID={2}, @intUser={3}, @strKeyWord='{4}'",
                @intMenuType, @intStatus, @intParentID, @intUser, @strKeyWord);
            return (new ConnectSQL()).connect_dt(sql);
        }
        #endregion

        #region TUser
        public bool DeleteUsertokeywork_ID(int USerID, int IDkeywork)
        {
            try
            {
                IList<UsertoKeywork> list = da.UsertoKeyworks.Where(z => z.IdUser == USerID && z.IdKeywork == IDkeywork).ToList();
                if (list.Count > 0)
                {
                    //da.MapAll_IDs.DeleteAllOnSubmit(list);// .DeleteOnSubmit(list.f);
                    da.UsertoKeyworks.DeleteOnSubmit(list.First());
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
        public DataTable RowsUser_byType(int @intUserType, int @page, int @pagesize, string @strSearchText)
        {
            string sql = string.Format("[p_TUser_Rows] @intUserType={0}, @intPage={1}, @intPageSize={2},@strSearchText=N'{3}'",
                @intUserType, @page, @pagesize, @strSearchText);
            return (new ConnectSQL()).connect_dt(sql);
        }
        public IList<TUser> Listuser(int idtype)
        {
            return da.TUsers.Where(z => z.UserType == idtype).OrderBy(z => z.UserLike).ToList();
        }
        public IList<TUser> Listuser_byparentid(int parentid)
        {
            return da.TUsers.Where(z =>  z.ParentID == parentid).OrderBy(z => z.UserLike).ToList();
        }
        public IList<TUser> Listuser_byIDCty(int idCty_userlike)
        {
            return da.TUsers.Where(z => z.UserLike == idCty_userlike).OrderBy(z => z.UserID).ToList();
        }
        public IList<ThongTinNhanSu> ListttNhansu_byIDCty(int idCty_userlike)
        {
            return da.ThongTinNhanSus.Where(z => z.IDCTY == idCty_userlike).OrderBy(z => z.Id).ToList();
        }
        public IList<TUser> Listuser_like(int idtype, int iduser)
        {
            return da.TUsers.Where(z => z.UserType == idtype && z.UserID != iduser && z.Gender == 0 ).OrderBy(z => z.UserLike).ToList();
        }
        public IList<TUser> Listuser_kt(int idtype,int idtype2, int iduser)
        {
            return da.TUsers.Where(z => z.UserType == idtype && z.UserID != iduser && z.Gender == 0 || z.UserType == idtype2 && z.UserID != iduser && z.Gender == 0).OrderBy(z => z.UserLike).ToList();
        }
        public IList<TUser> ListuserXL(int idtype )
        {
            return da.TUsers.Where(z => z.UserType == idtype && z.Gender == 0 && z.UserLike != 0).OrderBy(z => z.UserName).ToList();
        }
        public IList<TUser> ListuserNghi(int idtype, int Gender)
        {
            return da.TUsers.Where(z => z.UserType == idtype && z.Gender == Gender).OrderBy(z => z.UserName).ToList();
        }
        public string ListlevelByID(string poin)
        { string namepon= "noname";
        if (da.TValueofLevels.Where(z => z.ValueOfLevel.Trim() == poin).FirstOrDefault() !=null)
        {
            return namepon = da.TValueofLevels.Where(z => z.ValueOfLevel.Trim() == poin).FirstOrDefault().LevelName;
        }
        return namepon;
        }
        public IList<TValueofLevel> ListValueoflevel()
        {
            return da.TValueofLevels.Where(z => z.ValueLevelID !=0).OrderBy(z => z.LevelName).ToList();
        }
        public IList<TMaCongViec> ListMacongviec()
        {
            return da.TMaCongViecs.Where(z => z.IDMaCV != 0).OrderBy(z => z.IDMaCV).ToList();
        }
        #endregion

        #region TUserRoleMember

        //public IList<TUserRoleMember> RowsUserRoleMember_byID(string KeyWord,int status)
        //{
        //    return da.TUserRoleMembers.Where(z => (z.KeyWord == KeyWord || KeyWord == "") && z.status == status).ToList();
        //}

        //public TUserRoleMember RowUserRoleMember_byID(int RoleMemberID)
        //{
        //    IList<TUserRoleMember> list = da.TUserRoleMembers.Where(z => z.RoleMemberID == RoleMemberID).ToList();
        //    if (list.Count > 0)
        //    {
        //        return list.First();
        //    }
        //    return null;
        //}

        //public int CreateUpdateUserRole(TUserRoleMember ent)
        //{
        //    TUserRoleMember checkEtt = RowUserRoleMember_byID(ent.RoleMemberID);
        //    if (checkEtt == null)
        //    {
        //        TUserRoleMember objEnt = ent;
        //        da.TUserRoleMembers.InsertOnSubmit(objEnt);
        //        da.SubmitChanges();
        //        return objEnt.RoleMemberID;
        //    }
        //    else
        //    {
        //        TUserRoleMember objEnt = checkEtt;
        //        objEnt.RoleMemberID = ent.RoleMemberID;
        //        objEnt.RoleName = ent.RoleName;
        //        objEnt.StartNumber = ent.StartNumber;
        //        objEnt.EndNumber = ent.EndNumber;
        //        objEnt.KeyWord = ent.KeyWord;
        //        objEnt.status = ent.status;
        //        objEnt.SortOrder = ent.SortOrder;
        //        objEnt.Note = ent.Note;
        //        da.TUserRoleMembers.Context.SubmitChanges();
        //        return objEnt.RoleMemberID;
        //    }
        //}

        #endregion

        #region MapAll_ID

        public MapAll_ID RowMapAll_ID_byUKID(string UK, long mID)
        {
            IList<MapAll_ID> list = da.MapAll_IDs.Where(z => z.KeyWord == UK && z.MapProduct == mID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public IList<MapAll_ID> RowsListImg_ByKeyID(string UK, long mID)
        {
            IList<MapAll_ID> list = da.MapAll_IDs.Where(z => z.KeyWord == UK && z.MapProduct == mID).ToList();
            return list;
        }
        public IList<NgayPhep> RowsNgayPhep_ByIDUser(int iduser)
        {
            IList<NgayPhep> list = da.NgayPheps.Where(z => z.IDNhanvien == iduser && z.TrangThaiPhep == 0).ToList();
            return list;
        }

        public long CreateUpdate_MapAll_ID(MapAll_ID ent)
        {
            //MapAll_ID checkEtt = RowMapAll_ID_byUKID(ent.KeyWord, (long)ent.MapProduct);
            //if (checkEtt == null)
            //{
                MapAll_ID objEnt = ent;
                da.MapAll_IDs.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.MapAllID;
            //}
            //else
            //{
            //    MapAll_ID objEnt = checkEtt;
            //    objEnt.MapID = ent.MapID;
            //    objEnt.MapProduct = ent.MapProduct;
            //    objEnt.KeyWord = ent.KeyWord;
            //    da.MapAll_IDs.Context.SubmitChanges();
            //    return objEnt.MapAllID;
            //}
        }
        public long Update_UseNghiPhep(NgayPhep ent)
        {
            NgayPhep ngayp = da.NgayPheps.Where(z => z.IDPhep == ent.IDPhep).FirstOrDefault();
            if (ngayp != null)
            {
                ngayp.IDCty = ent.IDCty;
                ngayp.IDBGD = ent.IDBGD;
                ngayp.IDTruongBP=ent.IDTruongBP;
                ngayp.IDPhoPhong=ent.IDPhoPhong;
                ngayp.IDTruongNhom = ent.IDTruongNhom;
                da.NgayPheps.Context.SubmitChanges();
               // da.NgayPheps.InsertOnSubmit(ngayp);
              //  da.SubmitChanges();
                return ent.IDPhep;
            }
            else return 0;
        }
        public long CreateUpdate_UserNghiPhep(UserNghiPhep ent)
        {

            UserNghiPhep objEnt = ent;
            da.UserNghiPheps.InsertOnSubmit(objEnt);
            da.SubmitChanges();
            return objEnt.IDPhep;
           
        }
        public bool DeleteMapAll_ID(Int64 MapID)
        {
            try
            {
                IList<MapAll_ID> list = da.MapAll_IDs.Where(z => z.MapID == MapID).ToList();
                if (list.Count > 0)
                {
                    //da.MapAll_IDs.DeleteAllOnSubmit(list);// .DeleteOnSubmit(list.f);
                    da.MapAll_IDs.DeleteOnSubmit(list.First());
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
        public int Create_SendTTUV_CHECKSEND(SendUngVienThuViec ent)
        {

            SendUngVienThuViec objEnt = ent;

            IList<SendUngVienThuViec> list = da.SendUngVienThuViecs.Where(z => z.DateSend == ent.DateSend).ToList();
            if (list.Count == 0)
            {
                da.SendUngVienThuViecs.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.IDSend;
            }
            else return 0;
           

        }
       
        #endregion

        #region MapAll_ID //editer new
        public IList<MapAll_ID> RowsMapList_ByMapProID(long MapProID, string Key)
        {
            return da.MapAll_IDs.Where(z => z.MapProduct == MapProID && z.KeyWord == Key).OrderBy(z => z.thu_tu).OrderByDescending(z => z.MapAllID).ToList();
        }

        //public MapAll_ID RowMapAll_ID_ByKeyID(string key, long MapProID)
        //{
        //    IList<MapAll_ID> list = da.MapAll_IDs.Where(z => z.KeyWord == key && z.MapProduct == MapProID).OrderBy(z => z.thu_tu).ToList();
        //    if (list.Count > 0)
        //    {
        //        return list.First();
        //    }
        //    return null;
        //}

        public MapAll_ID RowMapAll_ID(long idMap)
        {
            IList<MapAll_ID> list = da.MapAll_IDs.Where(z => z.MapAllID == idMap).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public void CreateImgMap(MapAll_ID ent)
        {
            da.MapAll_IDs.InsertOnSubmit(ent);
            da.MapAll_IDs.Context.SubmitChanges();
        }

        public void DeleteRowMap(long MapAllID)
        {
            MapAll_ID ent = da.MapAll_IDs.Where(p => p.MapAllID == MapAllID).First();

            fileMng.DeleteCommonFile(Utils.TryParseLong(ent.MapID, 0), true);

            da.MapAll_IDs.DeleteOnSubmit(ent);
            da.MapAll_IDs.Context.SubmitChanges();
        }

        public void Update_MapAll_ID(MapAll_ID ent)
        {
            da.MapAll_IDs.Context.SubmitChanges();            
        }

        //public void DeleteAllMap(int proID)
        //{
        //    IList<MapAll_ID> list = da.MapAll_IDs.Where(z => z.MapProduct == proID).OrderBy(z => z.thu_tu).ToList();
        //    if (list.Count > 0)
        //    {
        //        foreach (MapAll_ID item in list)
        //        {
        //            FileManager fileMng = new FileManager();
        //            fileMng.DeleteCommonFile(Utils.TryParseLong(item.MapID, 0), true);
        //        }
        //        da.MapAll_IDs.DeleteAllOnSubmit(list);
        //        da.MapAll_IDs.Context.SubmitChanges();
        //    }
        //}
        #endregion
    }
}

