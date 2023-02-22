using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PQT.DAC;
using System.Data.Linq;

namespace PQT.DAC
{

    public class TourMng_BLC
    {
        #region Construct
        CommonDataDataContext nDAC = null;
        public TourMng_BLC()
        {
            if (nDAC == null)
            {
                nDAC = new CommonDataDataContext();
            }
        }

        #endregion

        #region TourCategory

        public TTourCategory RowTourCategory(int p_TourCategoryID)
        {
            return nDAC.TTourCategories.Where(p => p.TourCategoryID == p_TourCategoryID).First();
        }

        public IList<TTourCategory> RowsTourCategory(int p_parentID)
        {
            return nDAC.TTourCategories.Where(p => p.ParentID == p_parentID).OrderBy(z=>z.SortOder).ToList();

        }

        public TTourCategory RowTourCategoryByUniqueKey(string p_uniqueKey, int p_langID)
        {
            IList<TTourCategory> list = nDAC.TTourCategories.Where(p => p.UniqueKey == p_uniqueKey).ToList();
            return list.Count > 0 ? list.First() : null;
        }

        public int CreateTourCategory(TTourCategory ent)
        {
            nDAC.TTourCategories.InsertOnSubmit(ent);
            nDAC.TTourCategories.Context.SubmitChanges();
            return ent.TourCategoryID;
        }

        public void UpdateTourCategory(TTourCategory ent)
        {
            TTourCategory entCat = nDAC.TTourCategories.Where(z => z.TourCategoryID == ent.TourCategoryID).First();
            entCat.TourCategoryID = ent.TourCategoryID;
            entCat.Image = ent.Image;
            entCat.ParentID = ent.ParentID;
            entCat.Status = ent.Status;
            entCat.UniqueKey = ent.UniqueKey;
            entCat.SortOder = ent.SortOder;
            nDAC.TTourCategories.Context.SubmitChanges();
        }

        public void DeleteTourCategory(int p_TourCategoryID)
        {
            DeleteTourCategoryDescription(p_TourCategoryID);

            TTourCategory ent = nDAC.TTourCategories.Where(p => p.TourCategoryID == p_TourCategoryID).First();
            nDAC.TTourCategories.DeleteOnSubmit(ent);
            nDAC.TTourCategories.Context.SubmitChanges();
        }

        #region Lang

        public DataTable RowsTourCategoryByParentID(int @intPage, int @intPageSize, int @intParentID, int @intLangID)
        {
            string sql = string.Format(@"[p_TTourCategoryByParentID_Rows] @intPage={0}, @intPageSize={1}, @intParentID={2},@intLangID={3}",
                @intPage, @intPageSize, @intParentID, @intLangID);
            return (new ConnectSQL()).connect_dt(sql);
        }
        public int CountTourCategoryByParentID(int @intParentID, int @intLangID, int @intCateOrder, int @intStatus)
        {
            string sql = string.Format(@"[p_TTourCategoryByParentID_Rows] @intPage={0}, @intPageSize={1}, @intParentID={2},@intLangID={3},@intCateOrder={4},@intStatus={5}",
                1, int.MaxValue, @intParentID, @intLangID, @intCateOrder, @intStatus);
            return (new ConnectSQL()).connect_dt(sql).Rows.Count;
        }

        public string GetDetail_TourCategory(int p_TourCategoryID, int p_langID, ContentDetail p_contentDetail)
        {
            IList<TTourCategoryDescription> list = nDAC.TTourCategoryDescriptions.Where(p => p.TourCategoryID == p_TourCategoryID && p.LanguageID == p_langID).ToList();
            if (list.Count > 0)
            {
                TTourCategoryDescription ent = list.First();
                if (p_contentDetail == ContentDetail.Name)
                    return ent.Name;
                if (p_contentDetail == ContentDetail.Description)
                    return ent.Description;
                if (p_contentDetail == ContentDetail.SubContent)
                    return ent.SubContent;
                
            }
            return "";
        }

        public TTourCategoryDescription RowTourCategoryDescription(int p_TourCategoryID, int p_langID)
        {
            try
            {
                return nDAC.TTourCategoryDescriptions.Where(p => p.TourCategoryID == p_TourCategoryID && p.LanguageID == p_langID).First();
            }
            catch {
                return null;
            }
        }

        public IList<TTourCategoryDescription> RowTourCategoryDescription(int p_TourCategoryID)
        {
            return nDAC.TTourCategoryDescriptions.Where(p => p.TourCategoryID == p_TourCategoryID).ToList();
        }

        public bool DeleteTourCategoryDescription(int p_TourCategoryID)
        {
            IList<TTourCategoryDescription> list = nDAC.TTourCategoryDescriptions.Where(p => p.TourCategoryID == p_TourCategoryID).ToList();
            foreach (TTourCategoryDescription ent in list)
            {
                nDAC.TTourCategoryDescriptions.DeleteOnSubmit(ent);
            }
            nDAC.TTourCategoryDescriptions.Context.SubmitChanges();
            return true;
        }

        public void AddTourCategoryDescription(TTourCategoryDescription ent)
        {
            IList<TTourCategoryDescription> list = nDAC.TTourCategoryDescriptions.Where(p => p.TourCategoryID == ent.TourCategoryID && p.LanguageID == ent.LanguageID).ToList();
            if (list.Count == 0)
            {
                TTourCategoryDescription objEnt = ent;
                nDAC.TTourCategoryDescriptions.InsertOnSubmit(objEnt);
                nDAC.SubmitChanges();
            }
            else
            {
                TTourCategoryDescription objEnt = list.First();
                objEnt.Name = ent.Name;
                objEnt.Description = ent.Description;
                objEnt.LanguageID = ent.LanguageID;
                objEnt.SubContent = ent.SubContent;
                objEnt.TourCategoryID = ent.TourCategoryID;
                nDAC.TTourCategoryDescriptions.Context.SubmitChanges();
            }
        }

        #endregion

        #endregion TourCategory

        #region Language

        public IList<TLanguage> RowsLanguage()
        {
            return nDAC.TLanguages.ToList();
        }

        #endregion Language

        #region Tour

        public TTour RowTour(int p_TourID)
        {
            IList<TTour> list = nDAC.TTours.Where(x => x.TourID == p_TourID).ToList();
            return list.Count != 0 ? list.First() : null;
        }


        public TTour RowTourByUniqueKey(string p_uniqueKey)
        {
            return nDAC.TTours.Where(x => x.UniqueKey == p_uniqueKey).First();
        }


        public IList<TTour> RowsTour(int p_TourCategoryID)
        {
            return nDAC.TTours.Where(x => (x.TourCategoryID == p_TourCategoryID || p_TourCategoryID == 0)).ToList();

        }
       
        public DataSet RowsTour(int p_page, int p_pageSize, int p_langID, int p_status, int p_transportation, string p_searchText, string p_TourCategoryID, int @intSorder)
        {
            string sql = string.Format(@"[p_TTour_Rows] @intPage={0}, @intPageSize={1}, @intLangID={2},@intStatus={3},@intTransportation={4},@strSearchText='{5}',@strCatID='{6}', @intSorder ={7}",
                p_page, p_pageSize, p_langID, p_status, p_transportation, p_searchText, p_TourCategoryID, @intSorder);
            return (new ConnectSQL()).connect(sql);
        }
        public DataSet RowsTour_option(int p_page, int p_pageSize, int p_langID, int p_status, int p_transportation, string p_searchText, string p_TourCategoryID, int @intSorder, int @intTourID, int @intDayout, int @intDayin, string p_TourDays, int @intprice)
        {
            string sql = string.Format(@"[p_TTour_Rows_option] @intPage={0}, @intPageSize={1}, @intLangID={2},@intStatus={3},@intTransportation={4},@strSearchText='{5}',@strCatID='{6}', @intSorder ={7},@intTourID={8},@intDayout={9},@intDayin={10},@p_TourDays='{11}',@intprice={12}",
                p_page, p_pageSize, p_langID, p_status, p_transportation, p_searchText, p_TourCategoryID, @intSorder, @intTourID, @intDayout, @intDayin, p_TourDays, @intprice);
            return (new ConnectSQL()).connect(sql);
        }
        public DataSet RowsTour(int p_page, int p_pageSize, int p_langID, int p_status, int p_transportation, string p_searchText, string p_TourCategoryID, int @intSorder, int @intTourID)
        {
            string sql = string.Format(@"[p_TTour_Rows] @intPage={0}, @intPageSize={1}, @intLangID={2},@intStatus={3},@intTransportation={4},@strSearchText='{5}',@strCatID='{6}', @intSorder ={7}, @intTourID={8}",
                p_page, p_pageSize, p_langID, p_status, p_transportation, p_searchText, p_TourCategoryID, @intSorder, @intTourID);
            return (new ConnectSQL()).connect(sql);
        }
        public DataTable TTourCategoryByParentID_Rows(int p_Page, int p_PageSize, int p_parentID, int p_langID)
        {
            string sql = string.Format("[p_TTourCategoryByParentID_Rows] @intPage={0},@intPageSize = {1}, @intParentID={2}, @intLangID={3}",
                 p_Page, p_PageSize, p_parentID, p_langID);
            return (new ConnectSQL()).connect_dt(sql);
        }
        public IList<TTour> RowsTour(string p_listID)
        {

            return nDAC.TTours.Where(x => (p_listID.Contains(string.Format(",{0},", x.TourCategoryID)) || p_listID == "")).ToList();

        }

        public bool AddTour(TTour p_Tour)
        {

            bool result = false;

            try
            {
                TTour checkEtt = RowTour(p_Tour.TourID);
                if (checkEtt != null)
                    UpdateTour(p_Tour);
                else
                    CreateTour(p_Tour);

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }


        public int CreateTour(TTour p_Tour)
        {
            nDAC.TTours.InsertOnSubmit(p_Tour);
            nDAC.TTours.Context.SubmitChanges();
            return p_Tour.TourID;
        }


        public bool UpdateTour(TTour p_Tour)
        {

            TTour ent = nDAC.TTours.Where(z => z.TourID == p_Tour.TourID).First();
            ent.CountView = p_Tour.CountView;
            ent.TourCategoryID = p_Tour.TourCategoryID;
            ent.TourID = p_Tour.TourID;
            ent.Image = p_Tour.Image;
            ent.ModifyDate = p_Tour.ModifyDate;
            ent.SortOrder = p_Tour.SortOrder;
            ent.Status = p_Tour.Status;
            ent.UniqueKey = p_Tour.UniqueKey;
            ent.AvaiablePosition = p_Tour.AvaiablePosition;
            ent.DayNum = p_Tour.DayNum;
            ent.EndDate = p_Tour.EndDate;
            ent.ExpectedHours = p_Tour.ExpectedHours;
            ent.IsHot = p_Tour.IsHot;
            ent.ModifyDate = DateTime.Now;
            ent.Price = p_Tour.Price;
            ent.StartTime = p_Tour.StartTime;
            ent.Transportation = p_Tour.Transportation;
            ent.StartDate = p_Tour.StartDate;
            nDAC.TTours.Context.SubmitChanges();
            return true;
        }

        public void AddTourDescription(TTourDescription ent)
        {
            IList<TTourDescription> list = nDAC.TTourDescriptions.Where(p => p.TourID == ent.TourID && p.LanguageID == ent.LanguageID).ToList();
            if (list.Count == 0)
            {
                TTourDescription objEnt = ent;
                nDAC.TTourDescriptions.InsertOnSubmit(objEnt);
                nDAC.SubmitChanges();
            }
            else
            {
                TTourDescription objEnt = list.First();
                objEnt.Name = ent.Name;
                objEnt.Content = ent.Content;
                objEnt.Destination = ent.Destination;
                objEnt.LanguageID = ent.LanguageID;
                objEnt.PriceInfo = ent.PriceInfo;
                objEnt.Tmp1 = ent.Tmp1;
                nDAC.TTourDescriptions.Context.SubmitChanges();
            }
        }

        public bool DeleteTour(int p_TourID)
        {
            TTour checkEtt = RowTour(p_TourID);
            nDAC.TTours.DeleteOnSubmit(checkEtt);
            nDAC.TTours.Context.SubmitChanges();
            return true;
        }

        public bool DeleteTourDescription(int p_TourID)
        {
            IList<TTourDescription> list = nDAC.TTourDescriptions.Where(p => p.TourID == p_TourID).ToList();
            foreach (TTourDescription ent in list)
            {
                nDAC.TTourDescriptions.DeleteOnSubmit(ent);
            }
            nDAC.TTourDescriptions.Context.SubmitChanges();
            return true;
        }

        public TTourDescription RowTourDescription(int p_TourID, int p_langID)
        {
            return nDAC.TTourDescriptions.Where(x => x.TourID == p_TourID && x.LanguageID == p_langID).First();
        }

        public IList<TTourDescription> RowsTourDescriptionByTourID(int p_TourID)
        {
            return nDAC.TTourDescriptions.Where(x => x.TourID == p_TourID).ToList();
        }
        public string GetDetail_Tour(int p_TourID, int p_langID, ContentDetail p_contentDetail)
        {
            IList<TTourDescription> list = nDAC.TTourDescriptions.Where(p => p.TourID == p_TourID && p.LanguageID == p_langID).ToList();
            if (list.Count > 0)
            {
                TTourDescription ent = list.First();
                switch (p_contentDetail)
                {
                    case ContentDetail.Address:
                    case ContentDetail.Destination:
                        return ent.Destination;
                    case ContentDetail.Name:
                        return ent.Name;
                    case ContentDetail.Description:
                        return ent.Content;
                    case ContentDetail.SubContent:
                        return ent.PriceInfo;
                    case ContentDetail.Comment:
                        return ent.Tmp1;
                    default:
                        return ent.Name;
                }
            }
            return "";
        }

        #endregion Tour

        #region TourOrder

        public TTourOrder RowTourOrder(int p_TourOrderID)
        {
            IList<TTourOrder> list = nDAC.TTourOrders.Where(x => x.TourOrderID == p_TourOrderID).ToList();
            return list.Count != 0 ? list.First() : null;
        }


        public TTourOrder RowTourOrderByEmail(string p_email)
        {
            IList<TTourOrder> list = nDAC.TTourOrders.Where(x => x.Email == p_email).ToList();
            return list.Count > 0 ? list.First() : null;
        }


        public Array RowsTourOrder(int p_page, int p_pageSize, int p_langID, int p_status, int p_paymentType, DateTime p_fromDate, DateTime p_toDate, string p_searchText, int p_searchType)
        {
            return nDAC.p_TTourOrder_Rows(p_page, p_pageSize, p_langID, p_status, p_paymentType, p_fromDate, p_toDate, p_searchText, p_searchType).ToArray();

        }

        public int CountTourOrder(int p_langID, int p_status, int p_paymentType, DateTime p_fromDate, DateTime p_toDate, string p_searchText, int p_searchType)
        {
            return nDAC.p_TTourOrder_Count(p_langID, p_status, p_paymentType, p_fromDate, p_toDate, p_searchText, p_searchType).First().Column1.Value;
        }

        public bool AddTourOrder(TTourOrder p_TourOrder)
        {

            bool result = false;

            try
            {
                TTourOrder checkEtt = RowTourOrder(p_TourOrder.TourOrderID);
                if (checkEtt != null)
                    UpdateTourOrder(p_TourOrder);
                else
                    CreateTourOrder(p_TourOrder);

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }


        public int CreateTourOrder(TTourOrder p_TourOrder)
        {
            nDAC.TTourOrders.InsertOnSubmit(p_TourOrder);
            nDAC.SubmitChanges();
            return p_TourOrder.TourOrderID;
        }


        public bool UpdateTourOrder(TTourOrder p_TourOrder)
        {
            TTourOrder ent = nDAC.TTourOrders.Where(z => z.TourOrderID == p_TourOrder.TourOrderID).First();
            ent.Address = p_TourOrder.Address;
            ent.CompanyName = p_TourOrder.CompanyName;
            ent.Email = p_TourOrder.Email;
            ent.Fax = p_TourOrder.Fax;
            ent.Note = p_TourOrder.Note;
            ent.PaymentType = p_TourOrder.PaymentType;
            ent.Phone = p_TourOrder.Phone;
            ent.RegDate = p_TourOrder.RegDate;
            ent.Status = p_TourOrder.Status;
            ent.TaxCode = p_TourOrder.TaxCode;
            ent.Tel = p_TourOrder.Tel;
            ent.TotalCus = p_TourOrder.TotalCus;
            ent.TotalCus1 = p_TourOrder.TotalCus1;
            ent.TotalCus2 = p_TourOrder.TotalCus2;
            ent.TotalCus3 = p_TourOrder.TotalCus3;
            ent.TourID = p_TourOrder.TourID;
            ent.TourOrderID = p_TourOrder.TourOrderID;
            ent.YourName = p_TourOrder.YourName;
            nDAC.TTourOrders.Context.SubmitChanges(); 
            nDAC.SubmitChanges();
            return true;
        }

        public bool DeleteTourOrder(int p_TourOrderID)
        {
            TTourOrder checkEtt = RowTourOrder(p_TourOrderID);
            nDAC.TTourOrders.DeleteOnSubmit(checkEtt);
            nDAC.TTourOrders.Context.SubmitChanges();
            return true;
        }


        #endregion TourOrder

        #region TourDetail

        public DataTable RowsTourDetail(int @intPage, int @intPageSize, int @intLangID, int @intStatus, int @intTourID)
        {
            string sql = string.Format(@"[p_TTourDetail_Rows] @intPage={0}, @intPageSize={1}, @intLangID={2},@intStatus={3},@intTourID={4}",
                @intPage, @intPageSize, @intLangID, @intStatus, @intTourID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public TTourDetail RowTourDetail(int p_TourDetailID)
        {
            IList<TTourDetail> list = nDAC.TTourDetails.Where(x => x.TourDetailID == p_TourDetailID).ToList();
            if (list.Count > 0)
                return list.First();
            return null;
            //return nDAC.TTourDetails.Where(x => x.TourDetailID == p_TourDetailID).First();
        }


        public IList<TTourDetail> RowsTourDetail(int p_TourID,int p_status)
        {
            IList<TTourDetail> list = nDAC.TTourDetails.Where(x => (x.TourID == p_TourID) && (x.Status == p_status || p_status==-1)).ToList();
            list = list.OrderBy(x => x.SortOrder).ToList();
            return list;

        }


        public int AddTourDetail(TTourDetail p_TourDetail)
        {

            TTourDetail checkEtt = RowTourDetail(p_TourDetail.TourDetailID);
            if (checkEtt != null)
                return UpdateTourDetail(p_TourDetail);
            else
                return CreateTourDetail(p_TourDetail);

        }


        public int CreateTourDetail(TTourDetail p_TourDetail)
        {
            nDAC.TTourDetails.InsertOnSubmit(p_TourDetail);
            nDAC.TTourDetails.Context.SubmitChanges();
            return p_TourDetail.TourDetailID;
        }


        public int UpdateTourDetail(TTourDetail p_TourDetail)
        {
            TTourDetail ent = nDAC.TTourDetails.Where(z => z.TourDetailID == p_TourDetail.TourDetailID).First();
            ent.Image = p_TourDetail.Image;
            ent.SortOrder = p_TourDetail.SortOrder;
            ent.Status = p_TourDetail.Status;
            ent.Tmp1 = p_TourDetail.Tmp1;
            ent.Tmp2 = p_TourDetail.Tmp2;
            ent.TourID = p_TourDetail.TourID;
            nDAC.TTourDetails.Context.SubmitChanges();
            return ent.TourDetailID;
        }

        public DataTable RowsTourDetailDescription(int @intTourDetailID)
        {
            string sql = string.Format(@"[p_TTourDetailDescription_Rows] @intTourDetailID={0}",
                @intTourDetailID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public void AddTourDetailDescription(TTourDetailDescription ent)
        {

            int count = nDAC.TTourDetailDescriptions.Where(p => p.TourDetailID == ent.TourDetailID && p.LanguageID == ent.LanguageID).ToList().Count;
            if (count == 1)
                UpdateTourDetailDes(ent);
            else
                CreateTourDetailDes(ent);
        }

        public int CreateTourDetailDes(TTourDetailDescription ent)
        {
            nDAC.TTourDetailDescriptions.InsertOnSubmit(ent);
            nDAC.TTourDetailDescriptions.Context.SubmitChanges();
            return ent.TourDetailID;
        }


        public bool UpdateTourDetailDes(TTourDetailDescription ent)
        {
            TTourDetailDescription obent = nDAC.TTourDetailDescriptions.Where(z => z.TourDetailID == ent.TourDetailID && z.LanguageID == ent.LanguageID).First();
            obent.Name = ent.Name;
            obent.Content = ent.Content;
            obent.Tmp1 = ent.Tmp1;
            nDAC.TTourDetailDescriptions.Context.SubmitChanges();
            return true;
        }

        public bool DeleteTourDetail(int p_TourDetailID)
        {
            TTourDetail checkEtt = RowTourDetail(p_TourDetailID);
            nDAC.TTourDetails.DeleteOnSubmit(checkEtt);
            nDAC.TTourDetails.Context.SubmitChanges();
            return true;
        }

        public bool DeleteTourDetailDescription(int p_TourDetailID)
        {
            IList<TTourDetailDescription> list = nDAC.TTourDetailDescriptions.Where(p => p.TourDetailID == p_TourDetailID).ToList();
            foreach (TTourDetailDescription ent in list)
            {
                nDAC.TTourDetailDescriptions.DeleteOnSubmit(ent);
            }
            nDAC.TTourDetailDescriptions.Context.SubmitChanges();
            return true;
        }

        public TTourDetailDescription RowTourDetailDescription(int p_TourDetailID, int p_langID)
        {
            return nDAC.TTourDetailDescriptions.Where(x => x.TourDetailID == p_TourDetailID && x.LanguageID == p_langID).First();
        }

        public IList<TTourDetailDescription> RowsTourDetailDescriptionByTourDetailID(int p_TourDetailID)
        {
            return nDAC.TTourDetailDescriptions.Where(x => x.TourDetailID == p_TourDetailID).ToList();
        }

        public string GetDetail_TourDetail(int p_TourDetailID, int p_langID, ContentDetail p_contentDetail)
        {
            IList<TTourDetailDescription> list = nDAC.TTourDetailDescriptions.Where(p => p.TourDetailID == p_TourDetailID && p.LanguageID == p_langID).ToList();
            if (list.Count > 0)
            {
                TTourDetailDescription ent = list.First();
                if (ent != null)
                {
                    switch (p_contentDetail)
                    {

                        case ContentDetail.Name:
                            return ent.Name;
                        case ContentDetail.Description:
                            return ent.Content;
                        case ContentDetail.SubContent:
                            return ent.Tmp1;
                        default:
                            return ent.Name;
                    }
                }
            }
            return "";
        }

        #endregion Tour
        #region tourimage
         public TTourImage RowTourImage(long p_TourImageID)
        {
            IList<TTourImage> list = nDAC.TTourImages.Where(x => x.TourImageID == p_TourImageID).ToList();
            return list.Count != 0 ? list.First() : null;
        }
        public DataTable RowsTourImage(int p_TourID)
        {
            string sql = string.Format("[p_TTourImage_Rows] @intTourID={0}",
                 p_TourID);
            return (new ConnectSQL()).connect_dt(sql);
        }
        
        public bool AddTourImage(TTourImage p_TourImage)
        {

            bool result = false;

            try
            {
                TTourImage checkEtt = RowTourImage(p_TourImage.TourImageID);
                if (checkEtt != null)
                    UpdateTourImage(p_TourImage);
                else
                    CreateTourImage(p_TourImage);

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
        public long CreateTourImage(TTourImage p_TourImage)
        {
            nDAC.TTourImages.InsertOnSubmit(p_TourImage);
            nDAC.SubmitChanges();
            return p_TourImage.TourImageID;
        }

        public bool UpdateTourImage(TTourImage p_TourImage)
        {
            TTourImage ent = nDAC.TTourImages.Where(z => z.TourImageID == p_TourImage.TourImageID).First();
            ent.TourImageID = p_TourImage.TourImageID;

            ent.TourID = p_TourImage.TourID;
            ent.TempID2 = p_TourImage.TempID2;
            ent.TempID1 = p_TourImage.TempID1;
            ent.NameIMG = p_TourImage.NameIMG;
            ent.LinkIMG = p_TourImage.LinkIMG;
            nDAC.TTourImages.Context.SubmitChanges();
            nDAC.SubmitChanges();
            return true;
        }
        public bool DeleteTourImage(int p_TourImageID)
        {
            TTourImage checkEtt = RowTourImage(p_TourImageID);
            nDAC.TTourImages.DeleteOnSubmit(checkEtt);
            nDAC.TTourImages.Context.SubmitChanges();
            return true;
        }
        public TTourImage rowTourIMG_ByIDIMG(int tourIMGID)
        {
            IList<TTourImage> list = nDAC.TTourImages.Where(z => z.TourImageID == tourIMGID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        public IList<TTourImage> ListTourImg_ByTourID(int p_page, int p_pageSize, int tourID)
        {
            int preCount = (p_page - 1) * p_pageSize;
            return nDAC.TTourImages.Where(z => z.TourID == tourID).OrderByDescending(z => z.TourImageID).Skip(preCount).Take(p_pageSize).ToList();
        }
        #endregion tourimage
    }
}
