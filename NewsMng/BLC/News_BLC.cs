using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using PQT.API;
using PQT.API.Connection;

using System.Data;
using PQT.DAC;

namespace NewsMng.BLC
{
    public class News_BLC : XVNET_ModuleControl
    {
        UserMngDataDataContext da = null;
        ProductDataDataContext dapro = null;
        public News_BLC()
        {
            if (da == null)
            {
                da = new UserMngDataDataContext();
            }
            if (dapro == null)
            {
                dapro = new ProductDataDataContext();
            }
        }

#region 
        public TProductDescription GetProDetail(int proId,int lang)
        {
            IList<TProductDescription> list = dapro.TProductDescriptions.Where(z => z.ProductID == proId && z.LanguageID == lang).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
#endregion

#region News
        public TNew GetNew_ByID(Int64 LongNewID)
        {
            IList<TNew> list = da.TNews.Where(z => z.NewsID == LongNewID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        //public DataTable RowsNewsListMemberCreate_byType_like_Rows(int @intPage, int @intPageSize, int @intLangID, int @intStatus, string @strSearchText, string @strCategoryIDList, int @intType, bool @TopLike, int @intMemberID)
        //{
        //    string sql = string.Format("[p_TNewsListMemberCreate_byType_like_Rows] @intPage={0},@intPageSize={1},@intLangID={2},@intStatus={3},@strSearchText='{4}',@strCategoryIDList='{5}',@intType={6},@TopLike={7},@intMemberID={8}",
        //        @intPage, @intPageSize, @intLangID, @intStatus, @strSearchText, @strCategoryIDList, @intType, @TopLike, @intMemberID);
        //    return (new ConnectSQL()).connect_dt(sql);
        //}

        //public int CountNewsListMemberCreate_byType_like_Rows(int @intLangID, int @intStatus, string @strSearchText, string @strCategoryIDList, int @intType, bool @TopLike, int @intMemberID)
        //{
        //    string sql = string.Format("p_TNewsListMemberCreate_byType_like_Count @intLangID={0},@intStatus={1},@strSearchText='{2}',@strCategoryIDList='{3}',@intType={4},@TopLike={5},@intMemberID={6}",
        //        @intLangID, @intStatus, @strSearchText, @strCategoryIDList, @intType, @TopLike,@intMemberID);
        //    return (new ConnectSQL()).ConnectAndExculeScala(sql);
        //}
        public DataTable RowsNewsByListCategory_byType_like_MapAreaTerri(int @intPage, int @intPageSize, int @intLangID, int @intStatus, string @strSearchText, string @strCategoryIDList, int @intType, bool @TopLike, int @intMember, string @strMember, string @strTerri)
        {
            string sql = string.Format("[p_TNewsByPositionAndCategoryIDList_byType_like_area_terri_Rows] @intPage={0},@intPageSize={1},@intLangID={2},@intStatus={3},@strSearchText='{4}',@strCategoryIDList='{5}',@intType={6},@TopLike={7},@intMember={8},@strMember='{9}',@strTerri='{10}'",
                @intPage, @intPageSize, @intLangID, @intStatus, @strSearchText, @strCategoryIDList, @intType, @TopLike, @intMember, @strMember, @strTerri);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public int Count_NewsByListCategory_byType_like_MapAreaTerri(int @intLangID, int @intStatus, string @strSearchText, string @strCategoryIDList, int @intType, bool @TopLike, int @intMember, string @strMember, string @strTerri)
        {
            string sql = string.Format("[p_TNewsByPositionAndCategoryIDList_byType_like_area_terri_Count] @intLangID={0},@intStatus={1},@strSearchText='{2}',@strCategoryIDList='{3}',@intType={4},@TopLike={5},@intMember={6},@strMember='{7}',@strTerri='{8}'",
                @intLangID, @intStatus, @strSearchText, @strCategoryIDList, @intType, @TopLike, @intMember, @strMember, @strTerri);
            return (new ConnectSQL()).ConnectAndExculeScala(sql);
        }
        public DataTable RowsStatics()
        {
            string sql = string.Format("[spThongKe_Edit]");
            return (new ConnectSQL()).connect_dt(sql);
        }
        public DataTable RowsNewsByListCategory_byType_like(int @intPage, int @intPageSize, int @intLangID, int @intStatus, string @strSearchText, string @strCategoryIDList, int @intType, bool @TopLike, int @intMember, string @strMember, int @intArea, Int64 @intNewsIDOther)
        {
            string sql = string.Format("[p_TNewsByPositionAndCategoryIDList_byType_like_Rows] @intPage={0},@intPageSize={1},@intLangID={2},@intStatus={3},@strSearchText='{4}',@strCategoryIDList='{5}',@intType={6},@TopLike={7},@intMember={8},@strMember='{9}',@intArea={10},@intNewsIDOther={11}",
                @intPage, @intPageSize, @intLangID, @intStatus, @strSearchText, @strCategoryIDList, @intType, @TopLike, @intMember, @strMember, @intArea, @intNewsIDOther);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public int Count_NewsByListCategory_byType_like(int @intLangID, int @intStatus, string @strSearchText, string @strCategoryIDList, int @intType, bool @TopLike, int @intMember, string @strMember, int @intArea, Int64 @intNewsIDOther)
        {
            string sql = string.Format("[p_TNewsByPositionAndCategoryIDList_byType_like_Count] @intLangID={0},@intStatus={1},@strSearchText='{2}',@strCategoryIDList='{3}',@intType={4},@TopLike={5},@intMember={6},@strMember='{7}',@intArea={8}, @intNewsIDOther={9}",
                @intLangID, @intStatus, @strSearchText, @strCategoryIDList, @intType, @TopLike, @intMember, @strMember, @intArea, @intNewsIDOther);
            return (new ConnectSQL()).ConnectAndExculeScala(sql);
        }

        public DataTable RowsNews_byType(int intPage, int intPageSize, int intLangID, int intStatus, int intSortOption, int intSearchType, string strSearchText, int intCategory, int intType, bool boolLike, int @intMember, long @intNewOther)
        {
            string sql = string.Format("[p_TNews_byType_like_Rows] @intPage={0}, @intPageSize={1}, @intLangID={2}, @intStatus={3}, @intSortOption={4}, @intSearchType={5}, @strSearchText='{6}', @intCategory={7}, @intType={8}, @TopLike={9}, @intMember={10}, @intNewOther={11}",
                intPage, intPageSize, intLangID, intStatus, 0, intSearchType, strSearchText, intCategory, intType, boolLike, @intMember, @intNewOther);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public int CountNews_byType(int intLangID, int intStatus, int intSortOption, int intSearchType, string strSearchText, int intCategory, int intType, bool boolLike, int @intMember, long @intNewOther)
        {
            string sql = string.Format("[p_TNews_byType_like_Count] @intLangID={0}, @intStatus={1}, @intSortOption={2}, @intSearchType={3}, @strSearchText='{4}', @intCategory={5}, @intType={6}, @TopLike={7}, @intMember={8}, @intNewOther={9}",
                intLangID, intStatus, 0, intSearchType, strSearchText, intCategory, intType, boolLike, @intMember, @intNewOther);
            return (new ConnectSQL()).ConnectAndExculeScala(sql);
        }

#endregion

#region NewsDescription
        public DataTable RowsNewsDescriptionByNewsID(Int64 P_newsID)
        {
            string sql = string.Format("p_TNewsDescriptionByNewsID_Rows @longNewsID={0}", P_newsID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public TNewsDescription GetNewsDescription(Int64 @longNewsID, int @intLanguageID)
        {
            IList<TNewsDescription> list = da.TNewsDescriptions.Where(z => z.NewsID == @longNewsID && z.LanguageID == @intLanguageID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
#endregion

#region NewsCategory
        public TNewsCategory GetNewCategoryByUniqueKey_AND_KEYGROUP(string Keyword, string KeyGroup)
        {
            int idParentgroup = da.TNewsCategories.Where(x => x.UniqueKey.ToLower() == KeyGroup.ToLower()).FirstOrDefault().NewsCategoryID;
            IList<TNewsCategory> list = da.TNewsCategories.Where(z => z.UniqueKey.ToLower() == Keyword.ToLower() && z.ParentID == idParentgroup).ToList();
            if (list.Count > 0)
            {
                return list.FirstOrDefault();
            }
            return null;
        }
        public TNewsCategory GetNewCategoryByUniqueKey(string Keyword)
        {
            IList<TNewsCategory> list = da.TNewsCategories.Where(z => z.UniqueKey.ToLower() == Keyword.ToLower()).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        public TNewsCategory GetNewCategoryByUniqueKey_AND_KeyWord(string Keyword, string keKeyWordAD)
        {
            IList<TNewsCategory> list = da.TNewsCategories.Where(z => z.UniqueKey.ToLower() == Keyword.ToLower() && z.Keywork == keKeyWordAD).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        public TNewsCategory GetNewCategoryByKeyWord(string Keyword)
        {
            IList<TNewsCategory> list = da.TNewsCategories.Where(z => z.Keywork.ToLower() == Keyword.ToLower()).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        public TNewsCategory GetNewCategoryByID(int intcate)
        {
            IList<TNewsCategory> list = da.TNewsCategories.Where(z => z.NewsCategoryID == intcate).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public DataTable dt_RowsNewsCategoryByParentID(int intcateID, int intlang, int intCateOrder, int @intStatus)
        {
            string sql = string.Format("[p_TNewsCategoryByParentID_Rows] @intParentID={0},@intLangID={1},@intCateOrder={2}, @intStatus = {3}", intcateID, intlang, intCateOrder, @intStatus);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public DataTable dt_RowsNewsCategoryBy_strParentID(string @strParentID, int intlang, int intCateOrder, int @intStatus)
        {
            string sql = string.Format("[p_TNewsCategoryByStrParentID_Rows] @strParentID='{0}',@intLangID={1},@intCateOrder={2}, @intStatus = {3}", @strParentID, intlang, intCateOrder, @intStatus);
            return (new ConnectSQL()).connect_dt(sql);
        }

#endregion
        
#region newscategorydescription
        public TNewsCategoryDescription GetNewsCategoryDescription(int @intNewsCategoryID, int @intLanguageID)
        {
            IList<TNewsCategoryDescription> list = da.TNewsCategoryDescriptions.Where(z => z.NewsCategoryID == @intNewsCategoryID && z.LanguageID == @intLanguageID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

#endregion

#region NewsToCategory

        public IList<TNewsToCategory> RowsNewsToCategoryByNewsID(Int64 LongNewID)
        {
            IList<TNewsToCategory> list = da.TNewsToCategories.Where(z => z.NewsID == LongNewID).ToList();
            return list;
        }

        public TNewsToCategory GetNewsToCategory_byNewID_CategoryID(Int64 LongNewID,int CategoryID)
        {
            IList<TNewsToCategory> list = da.TNewsToCategories.Where(z => z.NewsID == LongNewID && z.NewsCategoryID == CategoryID).ToList();
            if (list.Count>0)
            {
                return list.First();
            }
            return null;
        }

#endregion

        /****Create****/

#region CreateNew or Update OR delete

        public Int64 CreateNews(TNew news)
        {
            TNew objEnt = news;

            da.TNews.InsertOnSubmit(objEnt);
            da.SubmitChanges();

            return objEnt.NewsID;
        }

        public bool UpdateLikeNew(Int64 intNew,Int64 intlike)
        {
            try
            {
                IList<TNew> list = da.TNews.Where(z => z.NewsID == intNew).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();
                    objVlaue.NewsLike = intlike;

                    da.TNews.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool UpdateNews_CountView(Int64 NewsID, int CountView)
        {
            try
            {
                IList<TNew> list = da.TNews.Where(z => z.NewsID == NewsID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.CountView = (Utils.TryParseInt(objVlaue.CountView,0) + CountView);

                    da.TNews.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool UpdateNews(TNew news)
        {
            try
            {
                IList<TNew> list = da.TNews.Where(z => z.NewsID == news.NewsID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();
                    objVlaue.NewsSourceID = news.NewsSourceID;
                    objVlaue.Author = news.Author;
                    objVlaue.NewsStatus = news.NewsStatus;
                    objVlaue.DefaultPic = news.DefaultPic;
                    objVlaue.CountView = news.CountView;
                    objVlaue.DateMng = news.DateMng;
                    objVlaue.IPAdd = news.IPAdd;
                    objVlaue.RegUser = news.RegUser;
                    objVlaue.ModifyDate = DateTime.Now;
                    objVlaue.ModifyUser = news.ModifyUser;
                    objVlaue.SortOrder = news.SortOrder;
                    da.TNews.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public void DeleteNews(Int64 P_newsID)
        {
            string sql = string.Format("[p_TNews_Delete] @longNewsID={0}", P_newsID);
            (new ConnectSQL()).ConnectAndExcule(sql);
        }
#endregion
        
#region Create or update NewsDescription

        public bool CreateNewsDescription(TNewsDescription p_newsDescription)
        {
            try
            {
                TNewsDescription objEnt = p_newsDescription;

                da.TNewsDescriptions.InsertOnSubmit(objEnt);
                da.SubmitChanges();

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            
        }

        public bool UpdateNewsDescription(TNewsDescription p_newsDescriptionEntity)
        {
            try
            {
                IList<TNewsDescription> list = da.TNewsDescriptions.Where(z => z.NewsID == p_newsDescriptionEntity.NewsID && z.LanguageID == p_newsDescriptionEntity.LanguageID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.Title = p_newsDescriptionEntity.Title;
                    objVlaue.Content = p_newsDescriptionEntity.Content;
                    objVlaue.SubTitle = p_newsDescriptionEntity.SubTitle;
                    objVlaue.SubContent = p_newsDescriptionEntity.SubContent;
                    objVlaue.Comment = p_newsDescriptionEntity.Comment;

                    da.TNewsDescriptions.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool AddNewsDescription(TNewsDescription p_newsDescription)
        {
            try
            {
                TNewsDescription checkEtt = GetNewsDescription(p_newsDescription.NewsID,p_newsDescription.LanguageID);
                if (checkEtt != null)
                    UpdateNewsDescription(p_newsDescription);
                else
                    CreateNewsDescription(p_newsDescription);

                return true;
            }
            catch
            {
                return false;
            }
        }


#endregion

#region Create NewToCategory or update or delete

        public bool CreateNewsToCategory(TNewsToCategory p_newsToCategory)
        {
            try
            {
                TNewsToCategory objEnt = p_newsToCategory;

                da.TNewsToCategories.InsertOnSubmit(objEnt);
                da.SubmitChanges();

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }

        public bool UpdateNewsToCategory(TNewsToCategory p_newsToCategory)
        {
            try
            {
                IList<TNewsToCategory> list = da.TNewsToCategories.Where(z => z.NewsCategoryID == p_newsToCategory.NewsCategoryID && z.NewsID == p_newsToCategory.NewsID).ToList();
                if (list.Count > 0)
                {
                    var objVlaue = list.First();

                    objVlaue.NewsCategoryID = p_newsToCategory.NewsCategoryID;
                    objVlaue.NewsID = p_newsToCategory.NewsID;

                    da.TNewsToCategories.Context.SubmitChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool AddNewsToCategory(TNewsToCategory p_newsToCategory)
        {
            try
            {
                if (UpdateNewsToCategory(p_newsToCategory) == true)
                {
                    return true;
                }
                else
                {
                    CreateNewsToCategory(p_newsToCategory);
                    return true;
                }
                //TNewsDescription checkEtt = GetNewsDescription(p_newsDescription.NewsID, p_newsDescription.LanguageID);
                //if (checkEtt != null)
                //    UpdateNewsDescription(p_newsDescription);
                //else
                //    CreateNewsDescription(p_newsDescription);

                //return true;
            }
            catch
            {
                return false;
            }
        }

        public void DeleteNewsToCategory(int @intNewsCategoryID, long @longNewsID)
        {
            string sql = string.Format("p_TNewsToCategory_Delete @intNewsCategoryID={0}, @longNewsID={1}", @intNewsCategoryID, @longNewsID);
            new ConnectSQL().ConnectAndExcule(sql);
        }

#endregion

    }
}