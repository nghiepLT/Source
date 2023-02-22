using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PQT.API;
using PQT.API.Connection;
using PQT.DAC;
using System.Data.Linq;
using HotelMng.BLC;

namespace HotelMng.BLC
{

    public class HotelMng_BLC
    {
        #region Construct
        CommonDataDataContext nDAC = null;
        public HotelMng_BLC()
        {
            if (nDAC == null)
            {
                nDAC = new CommonDataDataContext();
            }
        }

        #endregion

        #region HotelDetail

        public DataTable RowsHotelDetail(int @intPage, int @intPageSize, int @intLangID, int @intStatus, int @intHotelID)
        {
            string sql = string.Format(@"[p_THotelDetail_Rows] @intPage={0}, @intPageSize={1}, @intLangID={2},@intStatus={3},@intHotelID={4}",
                @intPage, @intPageSize, @intLangID, @intStatus, @intHotelID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public THotelDetail RowHotelDetail(int p_HotelDetailID)
        {
            IList<THotelDetail> list = nDAC.THotelDetails.Where(x => x.HotelDetailID == p_HotelDetailID).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
        }

        public int AddHotelDetail(THotelDetail p_HotelDetail)
        {

            THotelDetail checkEtt = RowHotelDetail(p_HotelDetail.HotelDetailID);
            if (checkEtt != null)
                return UpdateHotelDetail(p_HotelDetail);
            else
                return CreateHotelDetail(p_HotelDetail);

        }

        public int CreateHotelDetail(THotelDetail p_HotelDetail)
        {
            nDAC.THotelDetails.InsertOnSubmit(p_HotelDetail);
            nDAC.THotelDetails.Context.SubmitChanges();
            return p_HotelDetail.HotelDetailID;
        }

        public int UpdateHotelDetail(THotelDetail p_HotelDetail)
        {
            THotelDetail ent = nDAC.THotelDetails.Where(z => z.HotelDetailID == p_HotelDetail.HotelDetailID).First();
            ent.Image = p_HotelDetail.Image;
            ent.SortOrder = p_HotelDetail.SortOrder;
            ent.Status = p_HotelDetail.Status;
            ent.Tmp1 = p_HotelDetail.Tmp1;
            ent.Tmp2 = p_HotelDetail.Tmp2;
            ent.HotelID = p_HotelDetail.HotelID;
            nDAC.THotelDetails.Context.SubmitChanges();
            return ent.HotelDetailID;
        }
        public bool DeleteHotelDetail(int p_HotelDetailID)
        {
            THotelDetail checkEtt = RowHotelDetail(p_HotelDetailID);
            nDAC.THotelDetails.DeleteOnSubmit(checkEtt);
            nDAC.THotelDetails.Context.SubmitChanges();
            return true;
        }

        public bool DeleteHotelDetailList(int hotelID)
        {
            try
            {
                IList<THotelDetail> list = nDAC.THotelDetails.Where(z => z.HotelID == hotelID).ToList();
                if (list.Count > 0)
                {
                    DeleteHotelDetailDescription(list.First().HotelDetailID);
                    nDAC.THotelDetails.DeleteAllOnSubmit(list);// .DeleteOnSubmit(list.f);
                    nDAC.SubmitChanges();

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

        #region HotelDetailDescription
        public THotelDetailDescription RowHotelDetailDescription(int p_HotelDetailID, int p_langID)
        {
            return nDAC.THotelDetailDescriptions.Where(x => x.HotelDetailID == p_HotelDetailID && x.LanguageID == p_langID).First();
        }

        public DataTable RowsHotelDetailDescription(int @intHotelDetailID)
        {
            string sql = string.Format(@"[p_THotelDetailDescription_Rows] @intHotelDetailID={0}",
                @intHotelDetailID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public void AddHotelDetailDescription(THotelDetailDescription ent)
        {

            int count = nDAC.THotelDetailDescriptions.Where(p => p.HotelDetailID == ent.HotelDetailID && p.LanguageID == ent.LanguageID).ToList().Count;
            if (count == 1)
                UpdateHotelDetailDes(ent);
            else
                CreateHotelDetailDes(ent);
        }

        public int CreateHotelDetailDes(THotelDetailDescription ent)
        {
            nDAC.THotelDetailDescriptions.InsertOnSubmit(ent);
            nDAC.THotelDetailDescriptions.Context.SubmitChanges();
            return ent.HotelDetailID;
        }


        public bool UpdateHotelDetailDes(THotelDetailDescription ent)
        {
            THotelDetailDescription obent = nDAC.THotelDetailDescriptions.Where(z => z.HotelDetailID == ent.HotelDetailID && z.LanguageID == ent.LanguageID).First();
            obent.Name = ent.Name;
            obent.Content = ent.Content;
            obent.Tmp1 = ent.Tmp1;
            nDAC.THotelDetailDescriptions.Context.SubmitChanges();
            return true;
        }

        public bool DeleteHotelDetailDescription(int p_HotelDetailID)
        {
            IList<THotelDetailDescription> list = nDAC.THotelDetailDescriptions.Where(p => p.HotelDetailID == p_HotelDetailID).ToList();
            foreach (THotelDetailDescription ent in list)
            {
                nDAC.THotelDetailDescriptions.DeleteOnSubmit(ent);
            }
            nDAC.THotelDetailDescriptions.Context.SubmitChanges();
            return true;
        }

        #endregion

        #region HotelCategory

        public THotelCategory RowHotelCategory(int p_HotelCategoryID)
        {
            return nDAC.THotelCategories.Where(p => p.HotelCategoryID == p_HotelCategoryID).First();
        }

        public DataSet RowsHotelCategory(int @intPage, int @intPageSize, int @intLangID, int @intStatus, int @intParent)
        {
            string sql = string.Format(@"[p_THotelCategory_Rows] @intPage={0}, @intPageSize={1}, @intLangID={2},@intStatus={3},@intParent={4}",
                @intPage, @intPageSize, @intLangID, @intStatus, @intParent);
            return (new ConnectSQL()).connect(sql);
        }

        public IList<THotelCategory> RowsHotelCategory(int p_parentID)
        {
            return nDAC.THotelCategories.Where(p => p.ParentID == p_parentID).ToList();

        }

        public THotelCategory RowHotelCategoryByUniqueKey(string p_uniqueKey, int p_langID)
        {
            return nDAC.THotelCategories.Where(p => p.UniqueKey == p_uniqueKey).First();
        }

        #region Lang

        public string GetDetail_HotelCategory(int p_HotelCategoryID, int p_langID, ContentDetail p_contentDetail)
        {
            THotelCategoryDescription ent = nDAC.THotelCategoryDescriptions.Where(p => p.HotelCategoryID == p_HotelCategoryID && p.LanguageID == p_langID).First();
            if (ent != null)
            {
                return p_contentDetail == ContentDetail.Name ? ent.Name : ent.Description;
            }
            return "";
        }

        public THotelCategoryDescription RowHotelCategoryDescription(int p_HotelCategoryID, int p_langID)
        {
            return nDAC.THotelCategoryDescriptions.Where(p => p.HotelCategoryID == p_HotelCategoryID && p.LanguageID == p_langID).First();
        }

        public IList<THotelCategoryDescription> RowHotelCategoryDescription(int p_HotelCategoryID)
        {
            return nDAC.THotelCategoryDescriptions.Where(p => p.HotelCategoryID == p_HotelCategoryID).ToList();
        }

        public void DeleteHotelCategory(int p_HotelCategoryID)
        {
            DeleteHotelCategoryDescription(p_HotelCategoryID);
            THotelCategory ent = nDAC.THotelCategories.Where(p => p.HotelCategoryID == p_HotelCategoryID).First();
            nDAC.THotelCategories.DeleteOnSubmit(ent);
            nDAC.SubmitChanges();
        }

        public bool DeleteHotelCategoryDescription(int p_HotelCategoryID)
        {
            IList<THotelCategoryDescription> list = nDAC.THotelCategoryDescriptions.Where(p => p.HotelCategoryID == p_HotelCategoryID).ToList();
            foreach (THotelCategoryDescription ent in list)
            {
                nDAC.THotelCategoryDescriptions.DeleteOnSubmit(ent);
            }
            nDAC.SubmitChanges();
            return true;


        }

        public int CreateHotelCategory(THotelCategory ent)
        {
            nDAC.THotelCategories.InsertOnSubmit(ent);
            nDAC.SubmitChanges();
            return ent.HotelCategoryID;
        }

        public void UpdateHotelCategory(THotelCategory ent)
        {
            THotelCategory entCat = nDAC.THotelCategories.Where(z => z.HotelCategoryID == ent.HotelCategoryID).First();
            entCat.HotelCategoryID = ent.HotelCategoryID;
            entCat.Image = ent.Image;
            entCat.ParentID = ent.ParentID;
            entCat.Status = ent.Status;
            entCat.UniqueKey = ent.UniqueKey;
            nDAC.THotelCategories.Context.SubmitChanges();
        }

        public void AddHotelCategoryDescription(THotelCategoryDescription ent)
        {
            var list = nDAC.THotelCategoryDescriptions.Where(p => p.HotelCategoryID == ent.HotelCategoryID && p.LanguageID == ent.LanguageID).ToList();
            if (list.Count == 1)
            {
                THotelCategoryDescription entCheck = list[0];
                entCheck.Description = ent.Description;
                entCheck.HotelCategoryID = ent.HotelCategoryID;
                entCheck.LanguageID = ent.LanguageID;
                entCheck.Name = ent.Name;
                entCheck.SubContent = ent.SubContent;
            }
            else
                nDAC.THotelCategoryDescriptions.InsertOnSubmit(ent);
            nDAC.SubmitChanges();
        }

        #endregion

        #endregion HotelCategory

        #region Language

        public IList<TLanguage> RowsLanguage()
        {
            return nDAC.TLanguages.ToList();
        }

        #endregion Language

        #region Hotel

        public THotel RowHotel(int p_HotelID)
        {
            return nDAC.THotels.Where(x => x.HotelID == p_HotelID).First();
        }


        public THotel RowHotelByUniqueKey(string p_uniqueKey)
        {
            return nDAC.THotels.Where(x => x.UniqueKey == p_uniqueKey).First();
        }


        public IList<THotel> RowsHotel(int p_HotelCategoryID)
        {
            return nDAC.THotels.Where(x => (x.HotelCategoryID == p_HotelCategoryID || p_HotelCategoryID == 0)).ToList();

        }

        public DataSet RowsHotel(int @intPage, int @intPageSize, int @intLangID, int @intStatus, int @intStarNum, string @strSearchText, string @strCatID, int @intSorder)
        {
            string sql = string.Format(@"[p_THotel_Rows] @intPage={0}, @intPageSize={1}, @intLangID={2},@intStatus={3},@intStarNum={4},@strSearchText='{5}',@strCatID='{6}',@intSorder={7}",
                @intPage, @intPageSize, @intLangID, @intStatus, @intStarNum, @strSearchText, @strCatID, @intSorder);
            return (new ConnectSQL()).connect(sql);
        }

        public DataSet RowsHotel(int @intPage, int @intPageSize, int @intLangID, int @intStatus, int @intStarNum, string @strSearchText, string @strCatID, int @intSorder, int @intHotelID)
        {
            string sql = string.Format(@"[p_THotel_Rows] @intPage={0}, @intPageSize={1}, @intLangID={2},@intStatus={3},@intStarNum={4},@strSearchText='{5}',@strCatID='{6}',@intSorder={7}, @intHotelID={8}",
                @intPage, @intPageSize, @intLangID, @intStatus, @intStarNum, @strSearchText, @strCatID, @intSorder, @intHotelID);
            return (new ConnectSQL()).connect(sql);
        }

        //public Array RowsHotel(int p_page, int p_pageSize, int p_langID, int p_status, int p_starNum, int p_HotelCategoryID, string p_searchText)
        //{
        //    return nDAC.p_THotel_Rows(p_page, p_pageSize, p_langID, p_status, p_starNum, p_searchText, p_HotelCategoryID).ToArray();
        //}

        public IList<THotel> RowsHotel(string p_listID)
        {

            return nDAC.THotels.Where(x => (p_listID.Contains(string.Format(",{0},", x.HotelCategoryID)) || p_listID == "")).ToList();

        }

        public bool AddHotel(THotel p_Hotel)
        {

            bool result = false;

            try
            {
                THotel checkEtt = RowHotel(p_Hotel.HotelID);
                if (checkEtt != null)
                    UpdateHotel(p_Hotel);
                else
                    CreateHotel(p_Hotel);

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }


        public int CreateHotel(THotel p_Hotel)
        {
            nDAC.THotels.InsertOnSubmit(p_Hotel);
            nDAC.SubmitChanges();
            return p_Hotel.HotelID;
        }


        public bool UpdateHotel(THotel p_Hotel)
        {
            THotel ent = nDAC.THotels.Where(z => z.HotelID == p_Hotel.HotelID).First();
            ent.CountView = p_Hotel.CountView;
            ent.Fax = p_Hotel.Fax;
            ent.HotelCategoryID = p_Hotel.HotelCategoryID;
            ent.HotelID = p_Hotel.HotelID;
            ent.Image = p_Hotel.Image;
            ent.ModifyDate = p_Hotel.ModifyDate;
            ent.SortOrder = p_Hotel.SortOrder;
            ent.StarNum = p_Hotel.StarNum;
            ent.Status = p_Hotel.Status;
            ent.Tel = p_Hotel.Tel;
            ent.UniqueKey = p_Hotel.UniqueKey;
            nDAC.THotels.Context.SubmitChanges();
            return true;
        }

        public void AddHotelDescription(THotelDescription ent)
        {
            IList<THotelDescription> list = nDAC.THotelDescriptions.Where(p => p.HotelID == ent.HotelID && p.LanguageID == ent.LanguageID).ToList();
            if (list.Count == 1)
            {
                THotelDescription entCheck = list[0];
                entCheck.Address = ent.Address;
                entCheck.Description = ent.Description;
                entCheck.HotelID = ent.HotelID;
                entCheck.LanguageID = ent.LanguageID;
                entCheck.Name = ent.Name;
                entCheck.SubDescription = ent.SubDescription;
            }
            else
                nDAC.THotelDescriptions.InsertOnSubmit(ent);
            nDAC.SubmitChanges();
        }

        public bool DeleteHotel(int p_HotelID)
        {
            THotel checkEtt = RowHotel(p_HotelID);
            nDAC.THotels.DeleteOnSubmit(checkEtt);
            nDAC.SubmitChanges();
            return true;
        }

        public bool DeleteHotelDescription(int p_HotelID)
        {
            IList<THotelDescription> list = nDAC.THotelDescriptions.Where(p => p.HotelID == p_HotelID).ToList();
            foreach (THotelDescription ent in list)
            {
                nDAC.THotelDescriptions.DeleteOnSubmit(ent);
            }
            nDAC.SubmitChanges();
            return true;
        }

        public THotelDescription RowHotelDescription(int p_HotelID, int p_langID)
        {
            return nDAC.THotelDescriptions.Where(x => x.HotelID == p_HotelID && x.LanguageID == p_langID).First();
        }

        public IList<THotelDescription> RowsHotelDescriptionByHotelID(int p_HotelID)
        {
            return nDAC.THotelDescriptions.Where(x => x.HotelID == p_HotelID).ToList();
        }
        public string GetDetail_Hotel(int p_HotelID, int p_langID, ContentDetail p_contentDetail)
        {
            THotelDescription ent = nDAC.THotelDescriptions.Where(p => p.HotelID == p_HotelID && p.LanguageID == p_langID).First();
            if (ent != null)
            {
                switch (p_contentDetail)
                {
                    case ContentDetail.Address:
                        return ent.Address;
                    case ContentDetail.Name:
                        return ent.Name;
                    case ContentDetail.Description:
                        return ent.Description;
                    case ContentDetail.SubContent:
                        return ent.Description;
                    case ContentDetail.SubDescription:
                        return ent.SubDescription;
                    default:
                        return ent.Name;
                }
            }
            return "";
        }

        #endregion Hotel

        #region HotelOrder
        
        public THotelOrder RowHotelOrder(int p_HotelOrderID)
        {
            IList<THotelOrder> list = nDAC.THotelOrders.Where(x => x.HotelOrderID == p_HotelOrderID).ToList();
            return list.Count != 0 ? list.First() : null;
        }


        public THotelOrder RowHotelOrderByEmail(string p_email)
        {
            IList<THotelOrder> list = nDAC.THotelOrders.Where(x => x.Email == p_email).ToList();
            return list.Count > 0 ? list.First() : null;
        }


        //public Array RowsHotelOrder(int p_page, int p_pageSize, int p_langID, int p_status, int p_paymentType, DateTime p_fromDate, DateTime p_toDate, string p_searchText, int p_searchType)
        //{
        //    return nDAC.p_THotelOrder_Rows(p_page, p_pageSize, p_langID, p_status, p_paymentType, p_fromDate, p_toDate, p_searchText, p_searchType).ToArray();

        //}

        //public int CountHotelOrder(int p_langID, int p_status, int p_paymentType, DateTime p_fromDate, DateTime p_toDate, string p_searchText, int p_searchType)
        //{
        //    return nDAC.p_THotelOrder_Count(p_langID, p_status, p_paymentType, p_fromDate, p_toDate, p_searchText, p_searchType).First().Column1.Value;
        //}

        public int CreateHotelOrder(THotelOrder p_HotelOrder)
        {
            nDAC.THotelOrders.InsertOnSubmit(p_HotelOrder);
            nDAC.SubmitChanges();
            return p_HotelOrder.HotelOrderID;
        }

        public bool DeleteHotelOrder(int p_HotelOrderID)
        {
            THotelOrder checkEtt = RowHotelOrder(p_HotelOrderID);
            nDAC.THotelOrders.DeleteOnSubmit(checkEtt);
            nDAC.THotelOrders.Context.SubmitChanges();
            return true;
        }

        #endregion HotelOrder

    }
}
