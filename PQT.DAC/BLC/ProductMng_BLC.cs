using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PQT.DAC
{
    public class ProductMng_BLC
    {
        ProductDataDataContext da = null;
        public ProductMng_BLC()
        {
            if (da == null)
            {
                da = new ProductDataDataContext();
            }
        }

        #region Order Product

        public TOrder GetOrder_ByID(int CartID)
        {
            IList<TOrder> list = da.TOrders.Where(z => z.OrderID == CartID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public void Update_StatusPay(int CartID, int statusPay)
        {
            IList<TOrder> list = da.TOrders.Where(z => z.OrderID == CartID).ToList();
            if (list.Count > 0)
            {

                TOrder objEnt = list.First();
                objEnt.Status = statusPay;

                da.TOrders.Context.SubmitChanges();

            }
        }
        #endregion

        #region Product
        public TProduct rowProduct_ByID(int productID)
        {
            IList<TProduct> list = da.TProducts.Where(z => z.ProductID == productID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        public TProductImage rowProductIMG_ByIDIMG(int productIMGID)
        {
            IList<TProductImage> list = da.TProductImages.Where(z => z.ProductImageID == productIMGID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        public DataTable RowsProductCategory_ByProductID_KeyDeferent(int @intProductID, string @strKey)
        {
            string sql = string.Format(@"[p_TProductCategory_ByProductID_Key] @intProductID={0},@strKey='{1}'",
                @intProductID,@strKey);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public DataTable RowsProductCategory_ByProductID(int @intProductID)
        {
            string sql = string.Format(@"[p_TProductCategory_ByProductID] @intProductID={0}",
                @intProductID);
            return (new ConnectSQL()).connect_dt(sql);
        }
        public void Update_Sorder(int productID, int sortOrder)
        {
            IList<TProduct> list = da.TProducts.Where(z => z.ProductID == productID).ToList();
            if (list.Count > 0)
            {
                TProduct objEnt = list.First();
                objEnt.SortOrder = sortOrder;
                da.TProducts.Context.SubmitChanges();
            }
        }
        #endregion

        #region size

        public TProductSize rowProductSize_ByKey_proID(string key, int ProID)
        {
            IList<TProductSize> list = da.TProductSizes.Where(z => z.Keyword == key && z.ProductID == ProID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        public TProductSizeDescription rowProductSizeDescription(Int64 SizeID,int langID)
        {
            IList<TProductSizeDescription> list = da.TProductSizeDescriptions.Where(z => z.SizeID == SizeID && z.LanguageID == langID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        public DataTable RowsProductDescriptionSize_BySizeID(long @longSize)
        {
            string sql = string.Format(@"[p_TProductSizeDescriptionBySizeID_Rows] @longSize={0}",
                @longSize);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public DataTable RowsProductSize_ByProductID(int @intPage, int @intPageSize, int @intLangID, int @intProductID,Int64 @intOtherID)
        {
            string sql = string.Format(@"[p_TProductSizeRows] @intPage={0},@intPageSize={1},@intLangID={2},@intProductID={3}, @intOtherID={4}",
                @intPage, @intPageSize, @intLangID, @intProductID, @intOtherID);
            DataSet ds = (new ConnectSQL()).connect(sql);
            return ds.Tables[0];
        }

        public int CountProductSize_ByProductID(int @intPage, int @intPageSize, int @intLangID, int @intProductID)
        {
            string sql = string.Format(@"[p_TProductSizeRows] @intPage={0},@intPageSize={1},@intLangID={2},@intProductID={3}",
                @intPage, @intPageSize, @intLangID, @intProductID);
            DataSet ds = (new ConnectSQL()).connect(sql);
            return Utils.TryParseInt(ds.Tables[1].Rows[0][0],0);
        }

        public TProductSize GetProSize_ByID(long SizeID)
        {
            IList<TProductSize> list = da.TProductSizes.Where(z => z.SizeID == SizeID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }


        public TProductSizeDescription GetProSize_Des_ByID(long SizeID,int lang)
        {
            IList<TProductSizeDescription> list = da.TProductSizeDescriptions.Where(z => z.SizeID == SizeID && z.LanguageID == lang).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public long Create_Update_ProSize(TProductSize ent)
        {
            TProductSize entCheck = GetProSize_ByID(ent.SizeID);
            if (entCheck == null)
            {
                TProductSize objEnt = ent;
                da.TProductSizes.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.SizeID;
            }
            else
            {
                TProductSize objEnt = entCheck;
                objEnt.ProductID = ent.ProductID;
                objEnt.Keyword = ent.Keyword;
                objEnt.ParentID = ent.ParentID;
                objEnt.SortOrder = ent.SortOrder;
                objEnt.Price = ent.Price;
                da.TProductSizes.Context.SubmitChanges();
                return objEnt.SizeID;
            }
        }

        public long Create_Update_ProSize_Des(TProductSizeDescription ent)
        {
            TProductSizeDescription entCheck = GetProSize_Des_ByID((long)ent.SizeID, (int)ent.LanguageID);
            if (entCheck == null)
            {
                TProductSizeDescription objEnt = ent;
                da.TProductSizeDescriptions.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.SizeDesID;
            }
            else
            {
                TProductSizeDescription objEnt = entCheck;
                objEnt.SizeID = ent.SizeID;
                objEnt.LanguageID = ent.LanguageID;
                objEnt.Name = ent.Name;
                objEnt.Description = ent.Description;
                objEnt.SubContent = ent.SubContent;
                da.TProductSizeDescriptions.Context.SubmitChanges();
                return objEnt.SizeDesID;
            }
        }

        public bool DeleteProSize(Int64 SizeID)
        {
            TProductSize ent = GetProSize_ByID(SizeID);
            if (ent != null)
            {
                DeleteSizeDes(ent.SizeID);
                da.TProductSizes.DeleteOnSubmit(ent);
                da.SubmitChanges();
            }
            return true;
        }

        public bool DeleteSizeDes(Int64 SizeID)
        {
            IList<TProductSizeDescription> list = da.TProductSizeDescriptions.Where(z => z.SizeID == SizeID).ToList();
            if (list.Count > 0)
            {
                foreach (TProductSizeDescription entDes in list)
                {
                    da.TProductSizeDescriptions.DeleteOnSubmit(entDes);
                    da.SubmitChanges();
                }
            }
            return true;
        }

        #endregion

        #region Product Img
        public IList<TProductImage> ListProductImg_By_Pro_ColorID(int p_page, int p_pageSize, int ProID)
        {
            int preCount = (p_page - 1) * p_pageSize;
            return da.TProductImages.Where(z => z.ProductID == ProID).OrderByDescending(z => z.ProductImageID).Skip(preCount).Take(p_pageSize).ToList();
        }
        #endregion

        #region ProductToCategory
        public DataTable RowsTProductToCategoryByProductID_Key(int @intProductID, string @strKeyWord)
        {
            string sql = string.Format(@"[p_TProductToCategoryByProductID_Key_Rows] @intProductID={0}, @strKeyWord='{1}'",
                @intProductID,@strKeyWord);
            return (new ConnectSQL()).connect_dt(sql);
        }
         
        #endregion

        #region TProductRelated
        public DataTable RowsTProductRelatedByProductID(int @intProductID)
        {
            string sql = string.Format(@"[p_TProductRelatedByProductID_Rows] @intProductID={0}",
                @intProductID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        #endregion
        /*
        #region color

        public DataTable RowsProductDescriptionColor_ByColorID(long @longColor)
        {
            string sql = string.Format(@"[p_TProductColorDescriptionByColorID_Rows] @longColor={0}",
                @longColor);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public DataTable RowsProductColor_ByProductID(int @intPage, int @intPageSize, int @intLangID)
        {
            string sql = string.Format(@"[p_TProductColorRows] @intPage={0},@intPageSize={1},@intLangID={2}",
                @intPage, @intPageSize, @intLangID);
            DataSet ds = (new ConnectSQL()).connect(sql);
            return ds.Tables[0];
        }

        public int CountProductColor_ByProductID(int @intPage, int @intPageSize, int @intLangID)
        {
            string sql = string.Format(@"[p_TProductColorRows] @intPage={0},@intPageSize={1},@intLangID={2}",
                @intPage, @intPageSize, @intLangID);
            DataSet ds = (new ConnectSQL()).connect(sql);
            return Utils.TryParseInt(ds.Tables[1].Rows[0][0], 0);
        }

        public TProductColor GetProColor_ByID(long ColorID)
        {
            IList<TProductColor> list = da.TProductColors.Where(z => z.ColorID == ColorID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public TProductColorDescription GetProColor_Des_ByID(long ColorID, int lang)
        {
            IList<TProductColorDescription> list = da.TProductColorDescriptions.Where(z => z.ColorID == ColorID && z.LanguageID == lang).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }

        public long Create_Update_ProColor(TProductColor ent)
        {
            TProductColor entCheck = GetProColor_ByID(ent.ColorID);
            if (entCheck == null)
            {
                TProductColor objEnt = ent;
                da.TProductColors.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.ColorID;
            }
            else
            {
                TProductColor objEnt = entCheck;
                objEnt.ProductID = ent.ProductID;
                objEnt.Keyword = ent.Keyword;
                objEnt.ParentID = ent.ParentID;
                objEnt.SortOrder = ent.SortOrder;
                objEnt.Image = ent.Image;
                objEnt.CodeColor = ent.CodeColor;

                da.TProductColors.Context.SubmitChanges();
                return objEnt.ColorID;
            }
        }

        public long Create_Update_ProColor_Des(TProductColorDescription ent)
        {
            TProductColorDescription entCheck = GetProColor_Des_ByID((long)ent.ColorID, (int)ent.LanguageID);
            if (entCheck == null)
            {
                TProductColorDescription objEnt = ent;
                da.TProductColorDescriptions.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.ColorDesID;
            }
            else
            {
                TProductColorDescription objEnt = entCheck;
                objEnt.ColorID = ent.ColorID;
                objEnt.LanguageID = ent.LanguageID;
                objEnt.Name = ent.Name;
                objEnt.Description = ent.Description;
                objEnt.SubContent = ent.SubContent;
                da.TProductColorDescriptions.Context.SubmitChanges();
                return objEnt.ColorDesID;
            }
        }

        public bool DeleteProColor(Int64 ColorID)
        {
            TProductColor ent = GetProColor_ByID(ColorID);
            if (ent != null)
            {
                DeleteColorDes(ent.ColorID);
                da.TProductColors.DeleteOnSubmit(ent);
                da.SubmitChanges();
            }
            return true;
        }

        public bool DeleteColorDes(Int64 ColorID)
        {
            IList<TProductColorDescription> list = da.TProductColorDescriptions.Where(z => z.ColorID == ColorID).ToList();
            if (list.Count > 0)
            {
                foreach (TProductColorDescription entDes in list)
                {
                    da.TProductColorDescriptions.DeleteOnSubmit(entDes);
                    da.SubmitChanges();
                }
            }
            return true;
        }

        #endregion
        */

        /*
        #region ImagePro

        public IList<TProductImage> ListProductImg_By_Pro_ColorID(int p_page, int p_pageSize, int ProID, Int64 ColorID)
        {
            int preCount = (p_page - 1) * p_pageSize;
            return da.TProductImages.Where(z => z.ProductID == ProID && (z.TempID1 == ColorID || ColorID == -1)).OrderByDescending(z=>z.ProductImageID).Skip(preCount).Take(p_pageSize).ToList();
        }

        public DataTable RowProductImageTempID1_ByProID(long @intProductID, int @intLangID)
        {
            string sql = string.Format(@"[p_TProductImageTempID1_ByProID] @intProductID={0},@intLangID = {1}",
                @intProductID, @intLangID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        public DataTable RowsProductImage(long @intProductID)
        {
            string sql = string.Format(@"[p_TProductImage_Rows] @intProductID={0}",
                @intProductID);
            return (new ConnectSQL()).connect_dt(sql);
        }

        //public DataTable RowsProductColor_ByProductID(int @intPage, int @intPageSize, int @intLangID)
        //{
        //    string sql = string.Format(@"[p_TProductColorRows] @intPage={0},@intPageSize={1},@intLangID={2}",
        //        @intPage, @intPageSize, @intLangID);
        //    DataSet ds = (new ConnectSQL()).connect(sql);
        //    return ds.Tables[0];
        //}

        //public int CountProductColor_ByProductID(int @intPage, int @intPageSize, int @intLangID)
        //{
        //    string sql = string.Format(@"[p_TProductColorRows] @intPage={0},@intPageSize={1},@intLangID={2}",
        //        @intPage, @intPageSize, @intLangID);
        //    DataSet ds = (new ConnectSQL()).connect(sql);
        //    return Utils.TryParseInt(ds.Tables[1].Rows[0][0], 0);
        //}

        public TProductImage GetProImg_ByID(long ProductImageID)
        {
            IList<TProductImage> list = da.TProductImages.Where(z => z.ProductImageID == ProductImageID).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }


        //public TProductColorDescription GetProColor_Des_ByID(long ColorID, int lang)
        //{
        //    IList<TProductColorDescription> list = da.TProductColorDescriptions.Where(z => z.ColorID == ColorID && z.LanguageID == lang).ToList();
        //    if (list.Count > 0)
        //    {
        //        return list.First();
        //    }
        //    return null;
        //}

        public long Create_Update_ProImg(TProductImage ent)
        {
            TProductImage entCheck = GetProImg_ByID(ent.ProductImageID);
            if (entCheck == null)
            {
                TProductImage objEnt = ent;
                da.TProductImages.InsertOnSubmit(objEnt);
                da.SubmitChanges();
                return objEnt.ProductImageID;
            }
            else
            {
                TProductImage objEnt = entCheck;
                objEnt.ProductID = ent.ProductID;
                objEnt.ProductImageID = ent.ProductImageID;
                objEnt.TempID1 = ent.TempID1;
                objEnt.TempID2 = ent.TempID2;

                da.TProductImages.Context.SubmitChanges();
                return objEnt.ProductImageID;
            }
        }

        //public long Create_Update_ProColor_Des(TProductColorDescription ent)
        //{
        //    TProductColorDescription entCheck = GetProColor_Des_ByID((long)ent.ColorID, (int)ent.LanguageID);
        //    if (entCheck == null)
        //    {
        //        TProductColorDescription objEnt = ent;
        //        da.TProductColorDescriptions.InsertOnSubmit(objEnt);
        //        da.SubmitChanges();
        //        return objEnt.ColorDesID;
        //    }
        //    else
        //    {
        //        TProductColorDescription objEnt = entCheck;
        //        objEnt.ColorID = ent.ColorID;
        //        objEnt.LanguageID = ent.LanguageID;
        //        objEnt.Name = ent.Name;
        //        objEnt.Description = ent.Description;
        //        objEnt.SubContent = ent.SubContent;
        //        da.TProductColorDescriptions.Context.SubmitChanges();
        //        return objEnt.ColorDesID;
        //    }
        //}

        public bool DeleteProductImage(Int64 ProductImageID)
        {
            TProductImage ent = GetProImg_ByID(ProductImageID);
            if (ent != null)
            {
                DeleteColorDes(ent.ProductImageID);
                da.TProductImages.DeleteOnSubmit(ent);
                da.SubmitChanges();
            }
            return true;
        }

        //public bool DeleteColorDes(Int64 ColorID)
        //{
        //    IList<TProductColorDescription> list = da.TProductColorDescriptions.Where(z => z.ColorID == ColorID).ToList();
        //    if (list.Count > 0)
        //    {
        //        foreach (TProductColorDescription entDes in list)
        //        {
        //            da.TProductColorDescriptions.DeleteOnSubmit(entDes);
        //            da.SubmitChanges();
        //        }
        //    }
        //    return true;
        //}

        #endregion
        */
    }
}

