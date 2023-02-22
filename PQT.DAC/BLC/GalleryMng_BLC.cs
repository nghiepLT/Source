using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PQT.DAC
{
    public class GalleryMng_BLC
    {
        AlbumGalleryDataDataContext da = null;
        public GalleryMng_BLC()
        {
            if (da == null)
            {
                da = new AlbumGalleryDataDataContext();
            }
        }

        //#region

        //public long CreateLike(int NumLike, string IPAddress, long GalleryID, int TypeID)
        //{
        //    IList<TGalleryLike> list = da.TGalleryLikes.Where(z => z.GalleryID == GalleryID && z.IPAddress.Trim() == IPAddress.Trim() && z.TypeID == TypeID).ToList();
        //    if (list.Count > 0)
        //    {
        //        return list.First().LikeID;
        //    }
        //    else
        //    {
        //        TGalleryLike objEnt = null;
        //        objEnt = new TGalleryLike();

        //        objEnt.NumLike = NumLike;
        //        objEnt.IPAddress = IPAddress;
        //        objEnt.GalleryID = GalleryID;
        //        objEnt.TypeID = TypeID;
        //        objEnt.CreateDate = DateTime.Now;
        //        objEnt.UpdateDate = DateTime.Now;
        //        da.TGalleryLikes.InsertOnSubmit(objEnt);
        //        da.SubmitChanges();

        //        return objEnt.LikeID;
        //    }
        //}

        //#endregion

        #region GalleryLike

        //public long GetCountGalleryLike_ByID(Int64 GalleryID, int typelike)
        //{
        //    IList<TGalleryLike> list = da.TGalleryLikes.Where(z => z.GalleryID == GalleryID && z.TypeID == typelike).ToList();
        //    return Utils.TryParseLong(list.Sum(z=>z.NumLike),0);
        //    //return list.Count;
        //}

        //public bool DeleteGalleryLike(Int64 GalleryID)
        //{
        //    try
        //    {
        //        IList<TGalleryLike> list = da.TGalleryLikes.Where(z => z.GalleryID == GalleryID).ToList();
        //        if (list.Count > 0)
        //        {
        //            da.TGalleryLikes.DeleteAllOnSubmit(list);// .DeleteOnSubmit(list.f);
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

        #region Gallery

        public TGallery GetGallery_ByID(Int64 GalleryID)
        {
            IList<TGallery> list = da.TGalleries.Where(z => z.GalleryID == GalleryID).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }
        public long Create_Update_Gallery(TGallery ent)
        {
            TGallery entCheck = GetGallery_ByID(ent.GalleryID);
            if (entCheck == null)
            {
                TGallery objEnt = ent;
                da.TGalleries.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.GalleryID;
            }
            else
            {
                TGallery objEnt = entCheck;
                objEnt.AdminLike = ent.AdminLike;
                objEnt.Img= ent.Img;
                objEnt.Resolution= ent.Resolution;
                objEnt.Size = ent.Size ;
                objEnt.FileSize = ent.FileSize;
                
                da.TGalleries.Context.SubmitChanges();
                return objEnt.GalleryID;
            }
        }

        // 10 :GalleryID- ASC; 20 :SortOrder - ASC; 30 :TotalLike - ASC

        public DataTable RowsGallery(int @intPage, int @intPageSize, int @intLangID, int @intAlbumID, bool @bitIsAdmin, int @intSorder)
        {
            string sql = string.Format(@"[p_TGallery_Rows] @intPage = {0}, @intPageSize={1}, @intLangID={2}, @intAlbumID={3}, @bitIsAdmin={4}, @intSorder={5}",
                @intPage, @intPageSize, @intLangID, @intAlbumID, @bitIsAdmin, @intSorder);
            return (new ConnectSQL()).connect(sql).Tables[0];
        }

        public int CountRowsGallery(int @intPage, int @intPageSize, int @intLangID, int @intAlbumID, bool @bitIsAdmin, int @intSorder)
        {
            string sql = string.Format(@"[p_TGallery_Rows] @intPage = {0}, @intPageSize={1}, @intLangID={2}, @intAlbumID={3}, @bitIsAdmin={4}, @intSorder={5}",
                @intPage, @intPageSize, @intLangID, @intAlbumID, @bitIsAdmin, @intSorder);
            return Utils.TryParseInt((new ConnectSQL()).connect(sql).Tables[1].Rows[0][0],0);
        }

        public DataTable DeleteGallery(long @longGalleryID)
        {
            string sql = string.Format(@"[p_TGallery_Delete] @longGalleryID = {0}",
                @longGalleryID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        
        #endregion

        #region GalleryDescription

        public TGalleryDescription GetGalleryDescription_ByID(Int64 GalleryID, int langID)
        {
            IList<TGalleryDescription> list = da.TGalleryDescriptions.Where(z => z.GalleryID == GalleryID && z.LanguageID == langID).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }

        public DataTable RowsGalleryDescription_ByGalleryID(long @longGalleryID)
        {
            string sql = string.Format(@"[p_TGalleryDescriptionByGalleryID_Rows] @longGalleryID={0}",
                @longGalleryID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public long Create_Update_GalleryDescription(TGalleryDescription ent)
        {
            TGalleryDescription entCheck = GetGalleryDescription_ByID(ent.GalleryID, ent.LanguageID);
            if (entCheck == null)
            {
                TGalleryDescription objEnt = ent;
                da.TGalleryDescriptions.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.GalleryID;
            }
            else
            {
                TGalleryDescription objEnt = entCheck;
                objEnt.Name = ent.Name;
                objEnt.MoTa = ent.MoTa;
                objEnt.Description = ent.Description;
                da.TGalleryDescriptions.Context.SubmitChanges();
                return objEnt.GalleryID;
            }
        }

        public DataTable DeleteGalleryDescription(long @longGalleryID, int @intLanguageID)
        {
            string sql = string.Format(@"[p_TGalleryDescription_Delete] @longGalleryID = {0}, @intLanguageID={1}",
                @longGalleryID, @intLanguageID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        #endregion

        #region GalleryToAlbum

        public TGalleryToAlbum GetGalleryToAlbum_byGalleryID_AlbumID(Int64 @longGalleryID, int @intAlbumID)
        {
            IList<TGalleryToAlbum> list = null;
            if (@intAlbumID != -1)
            {
                list = da.TGalleryToAlbums.Where(z => z.GalleryID == @longGalleryID && z.AlbumID == @intAlbumID).ToList();
            }
            else
            {
                list = da.TGalleryToAlbums.Where(z => z.GalleryID == @longGalleryID).ToList();
            }

            if (list.Count > 0)
                return list.First();
            return null;
        }

        public DataTable RowsGalleryToAlbumByGalleryID(long @longGalleryID)
        {
            string sql = string.Format(@"[p_TGalleryToAlbumByGalleryID_Rows] @longGalleryID={0}",
                @longGalleryID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public DataTable DeleteGalleryToAlbum(int @intAlbumID, long @longGalleryID)
        {
            string sql = string.Format(@"[p_TGalleryToAlbum_Delete] @intAlbumID = {0}, @longGalleryID={1}",
                @intAlbumID, @longGalleryID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public long Create_Update_GalleryToAlbum(TGalleryToAlbum ent)
        {
            TGalleryToAlbum entCheck = GetGalleryToAlbum_byGalleryID_AlbumID(ent.GalleryID, ent.AlbumID);
            if (entCheck == null)
            {
                TGalleryToAlbum objEnt = ent;
                da.TGalleryToAlbums.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.GalleryID;
            }
            else
            {
                TGalleryToAlbum objEnt = entCheck;
                objEnt.AlbumID = ent.AlbumID;
                objEnt.GalleryID = ent.GalleryID;
                da.TGalleryToAlbums.Context.SubmitChanges();
                return objEnt.GalleryID;
            }
        }

        #endregion

        #region Album

        public TAlbum GetAlbum_ByID(int AlbumID)
        {
            IList<TAlbum> list = da.TAlbums.Where(z => z.AlbumID == AlbumID).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }

        public TAlbum GetAlbum_ByUK(string UK)
        {
            IList<TAlbum> list = da.TAlbums.Where(z => z.UniqueKey.Trim().ToLower() == UK.Trim().ToLower()).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }

        public DataTable RowsAlbum(int @intPage, int @intPageSize, int @intAlbumCategoryID, int @intStatus, int @intLangID, bool @bitIsAdmin)
        {
            string sql = string.Format(@"[p_TAlbum_Rows] @intPage={0},@intPageSize={1},@intAlbumCategoryID={2},@intStatus={3},@intLangID={4},@bitIsAdmin={5}",
                @intPage, @intPageSize, @intAlbumCategoryID, @intStatus, @intLangID, @bitIsAdmin);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public int Create_Update_Album(TAlbum ent)
        {
            TAlbum entCheck = GetAlbum_ByID(ent.AlbumID);
            if (entCheck == null)
            {
                TAlbum objEnt = ent;
                da.TAlbums.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.AlbumID;
            }
            else
            {
                TAlbum objEnt = entCheck;
                objEnt.Image = ent.Image;
                objEnt.UniqueKey = ent.UniqueKey;
                objEnt.SortOrder = ent.SortOrder;
                objEnt.CountView = ent.CountView;
                objEnt.Status = ent.Status;
                

                da.TAlbums.Context.SubmitChanges();
                return objEnt.AlbumID;
            }
        }

        public void DeleteAlbum(int @intAlbumID)
        {
            string sql = string.Format("[p_TAlbum_Delete] @intAlbumID={0}", @intAlbumID);
            (new ConnectSQL()).ConnectAndExcule(sql);
        }
        #endregion

        #region AlbumDescription

        public DataTable RowsAlbumDescriptionByAlbumID(int @intAlbumID)
        {
            string sql = string.Format(@"[p_TAlbumDescriptionByAlbumID_Rows] @intAlbumID={0}",
                @intAlbumID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public TAlbumDescription GetAlbumDescription_ByID(int AlbumID,int langID)
        {
            IList<TAlbumDescription> list = da.TAlbumDescriptions.Where(z => z.AlbumID == AlbumID && z.LanguageID == langID).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }

        public int Create_Update_AlbumDescription(TAlbumDescription ent)
        {
            TAlbumDescription entCheck = GetAlbumDescription_ByID(ent.AlbumID, ent.LanguageID);
            if (entCheck == null)
            {
                TAlbumDescription objEnt = ent;
                da.TAlbumDescriptions.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.AlbumID;
            }
            else
            {
                TAlbumDescription objEnt = entCheck;
                objEnt.Description = ent.Description;
                objEnt.Name = ent.Name;

                da.TAlbumDescriptions.Context.SubmitChanges();
                
                return objEnt.AlbumID;
            }
        }
        #endregion

        #region AlbumToCategory

        public TAlbumToCategory GetAlbumToCategory_ByID(int AlbumCategoryID, int AlbumID)
        {
            IList<TAlbumToCategory> list = da.TAlbumToCategories.Where(z => z.AlbumCategoryID == AlbumCategoryID && z.AlbumID == AlbumID).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }

        public DataTable RowsAlbumToCategoryByAlbumID(int @intAlbumID)
        {
            string sql = string.Format(@"[p_TAlbumToCategoryByAlbumID_Rows] @intAlbumID={0}",
               @intAlbumID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public int Create_Update_AlbumToCategory(TAlbumToCategory ent)
        {
            TAlbumToCategory entCheck = GetAlbumToCategory_ByID(ent.AlbumCategoryID, ent.AlbumID);
            if (entCheck == null)
            {
                TAlbumToCategory objEnt = ent;
                da.TAlbumToCategories.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.AlbumID;
            }
            else
            {
                TAlbumToCategory objEnt = entCheck;
                objEnt.AlbumCategoryID = ent.AlbumCategoryID;
                objEnt.AlbumID = ent.AlbumID;
                da.TAlbumToCategories.Context.SubmitChanges();
                return objEnt.AlbumID;
            }
        }

        public void DeleteAlbumToCategory(int @intAlbumCategoryID, int @intAlbumID)
        {
            string sql = string.Format("[p_TAlbumToCategory_Delete] @intAlbumCategoryID={0},@intAlbumID={1}", @intAlbumCategoryID, @intAlbumID);
            (new ConnectSQL()).ConnectAndExcule(sql);
        }
        //public bool DeleteAlbumToCategory(Int64 TMapID, string Keyword)
        //{
        //    try
        //    {
        //        IList<TMapTerritoryArea> list = da.TMapTerritoryAreas.Where(z => z.TMapID == TMapID && z.Keyword.ToLower().Trim() == Keyword.ToLower().Trim()).ToList();
        //        if (list.Count > 0)
        //        {
        //            da.TMapTerritoryAreas.DeleteOnSubmit(list.First());
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

        #region AlbumCategory
        public TAlbumCategory GetAlbumCate_ByID(int AlbumCategoryID)
        {
            IList<TAlbumCategory> list = da.TAlbumCategories.Where(z => z.AlbumCategoryID == AlbumCategoryID).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }

        public TAlbumCategory GetAlbumCate_UK(string uk)
        {
            IList<TAlbumCategory> list = da.TAlbumCategories.Where(z => z.UniqueKey.Trim().ToLower() == uk.Trim().ToLower()).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }

        public DataTable RowsAlbumCategodyByParentID(int @AlbumCategoryID, int @intLangID)
        {
            string sql = string.Format("[p_TAlbumCategory_Rows] @intParentID={0},@intLangID={1}",
                @AlbumCategoryID, @intLangID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public int Create_Update_AlbumCategory(TAlbumCategory ent)
        {
            TAlbumCategory entCheck = GetAlbumCate_ByID(ent.AlbumCategoryID);
            if (entCheck == null)
            {
                TAlbumCategory objEnt = ent;
                da.TAlbumCategories.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.AlbumCategoryID;
            }
            else
            {
                TAlbumCategory objEnt = entCheck;
                objEnt.Image = ent.Image;
                objEnt.ParentID = ent.ParentID;
                objEnt.Status = ent.Status;
                objEnt.UniqueKey = ent.UniqueKey;

                da.TAlbumCategories.Context.SubmitChanges();
                return objEnt.AlbumCategoryID;
            }
        }

        public void DeleteAlbumCategory(int AlbumCateID)
        {
            string sql = string.Format("[p_TAlbumCategory_Delete] @intAlbumCategoryID={0}", AlbumCateID);
            (new ConnectSQL()).ConnectAndExcule(sql);
        }
        
#endregion

#region AlbumCategoryDescription
        public TAlbumCategoryDescription GetAlbumCategoryDescription_ByID(int AlbumCategoryID,int langID)
        {
            IList<TAlbumCategoryDescription> list = da.TAlbumCategoryDescriptions.Where(z => z.AlbumCategoryID == AlbumCategoryID && z.LanguageID == langID).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }

        public DataTable RowsAlbumCategoryDescriptionByAlbumCategoryID(int @AlbumCategoryID)
        {
            string sql = string.Format("[p_TAlbumCategoryDescriptionByAlbumCategoryID_Rows] @intAlbumCategoryID={0}",
                @AlbumCategoryID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public int Create_Update_AlbumCategoryDescription(TAlbumCategoryDescription ent)
        {
            TAlbumCategoryDescription entCheck = GetAlbumCategoryDescription_ByID(ent.AlbumCategoryID,ent.LanguageID);
            if (entCheck == null)
            {
                TAlbumCategoryDescription objEnt = ent;
                da.TAlbumCategoryDescriptions.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.AlbumCategoryID;
            }
            else
            {
                TAlbumCategoryDescription objEnt = entCheck;
                objEnt.Name = ent.Name;
                objEnt.Description = ent.Description;

                da.TAlbumCategoryDescriptions.Context.SubmitChanges();
                return objEnt.AlbumCategoryID;
            }
        }
#endregion

    }
}

