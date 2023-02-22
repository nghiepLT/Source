using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using PQT.API;
using PQT.Common;
using System.Net.Mail;
using System.Data;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using PQT.DAC;
using UserMng.BLC;
using NewsMng.BLC;
//using ProductMng.BLC;
//using ProductMng.DataDefine;
using System.Data.SqlClient;
using PQT.API.Connection;
using System.Configuration;
using System.Web.Script.Services;
namespace WebCus
{
    public partial class jquery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [System.Web.Services.WebMethod]
        public static string JQ_AboutDetail(object p_AboutID, object p_LangID)
        {
            if (p_AboutID != null)
            {
                News_BLC blc_news = new News_BLC();
                int intLang = Utils.TryParseInt(p_LangID, 1);
                int p_about = Utils.TryParseInt(p_AboutID, 0);
                TNewsCategoryDescription entDetail2 = blc_news.GetNewsCategoryDescription(p_about, intLang);
                if (entDetail2 != null)
                {
                    return entDetail2.Description;
                }
            }

            return string.Empty;
        }

        [System.Web.Services.WebMethod]
        public static string SelectJQTourDetail(object p_TourID, object p_LangID)
        {
            TourMng_BLC blc_tour = new TourMng_BLC();
            int TourID = Helper.TryParseInt(p_TourID.ToString(), 0);
            int intLang = Helper.TryParseInt(p_LangID.ToString(), 1);

            TTourDetail ent = blc_tour.RowTourDetail(TourID);
            if (ent != null)
            {
                //if (ent.Tmp1.ToLower() != "TourDetailPhoto".ToLower())
                //{
                string html01 = "<div class='maximg_contentTour css_description clearfix defaulLink maximg_video' style='padding: 5px;'>{0}</div>";
                TTourDetailDescription entDes = blc_tour.RowTourDetailDescription(ent.TourDetailID, intLang);
                if (entDes != null)
                {
                    return string.Format(html01, entDes.Content);
                }
                //}
                //else
                //{
                //    TTourDetailDescription entDesPhoto = blc_tour.RowTourDetailDescription(TourID, intLang);
                //    if (entDesPhoto != null)
                //    {
                //        string html02 = "<div class='clearfix' style='padding: 10px 0;' ><div class='css_slider_PhotoTour'><div id='slider_home2'>{0}";
                //        html02 +="</div></div></div>";
                //        return string.Format(html02,entDesPhoto.Content.Replace("<p>", "").Replace("</p>", ""));
                //    }
                //}
            }
            return string.Empty;
        }

        [System.Web.Services.WebMethod]
        public static long LikeJQ(object p_memberid, object p_newID, object p_num)
        {
            try
            {
                long lmember = Utility.TryParseLong(p_memberid, 0);
                long lnew = Utility.TryParseLong(p_newID, 0);
                int int_num = Helper.TryParseInt(p_num.ToString(), 1);
                if (lmember == 0)
                {
                    return -2;
                }
                else
                {
                    UserMngOther_BLC blc_user = new UserMngOther_BLC();
                    News_BLC blc_new = new News_BLC();

                    TUserMapNew checkMap = blc_user.GetMapUserNew_by_user(lmember, lnew);
                    if (checkMap != null)
                    {
                        return -3;
                    }
                    else
                    {

                        TNew entNew = blc_new.GetNew_ByID(lnew);
                        long likenew = Utility.TryParseLong(entNew.NewsLike, 0) + int_num;

                        //blc_user.Update_UserLike(Convert.ToInt32(lmember));

                        blc_new.UpdateLikeNew(entNew.NewsID, likenew);
                        blc_user.CreateUserMapNew(lmember, lnew);

                        //blc_user.Updata_MemberLike_MapUserRole_checkRole(lmember,true);

                        return likenew;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return -1;
            }

        }

        [System.Web.Services.WebMethod]
        public static long CreaterCommentHoi(object p_noidung, object p_userMemberID, object p_parent)
        {
            try
            {
                int iduser = Convert.ToInt32(Utility.TryParseLong(p_userMemberID, 0));
                long ilongparent = Utility.TryParseLong(p_parent, 0);
                UserMngOther_BLC blc_user = new UserMngOther_BLC();
                TUser entuser = blc_user.GetUser_ByIDAll(iduser);
                if (entuser != null)
                {
                    TComment ent = new TComment();
                    ent.Email = entuser.Email.ToString().Trim();
                    ent.Name = entuser.UserName.ToString().Trim();
                    ent.Title = "Hỏi đáp";
                    ent.Comment_Content = p_noidung.ToString().Trim();
                    ent.CreateDate = DateTime.Now;
                    ent.UserID = Utility.TryParseLong(p_userMemberID, 0);
                    ent.ID_Ref = ilongparent;
                    ent.Ref_Type = 0;
                    ent.Status = 1;
                    ent.UniqueKey = "HoiDap";

                    return blc_user.CreateComment_ent(ent);
                }
                return -1;
            }
            catch (System.Exception ex)
            {
                return -1;
            }

        }

        [System.Web.Services.WebMethod]
        public static List<CommentJQList> ViewCommentHoi(object p_pageCurent, object p_pagesize)
        {
            int pCurent = Convert.ToInt32(Utility.TryParseLong(p_pageCurent, 1));
            int pSize = Convert.ToInt32(Utility.TryParseLong(p_pagesize, 5));

            UserMngOther_BLC blc_user = new UserMngOther_BLC();

            List<CommentJQList> list = new List<CommentJQList>();

            IList<TComment> listView = blc_user.RowsComment(pCurent, pSize, 0, 1, "HoiDap", -1);
            if (listView.Count > 0)
            {
                foreach (TComment item in listView)
                {
                    list.Add(new CommentJQList
                    {
                        CommentID = Utility.TryParseLong(item.CommentID, 0),
                        Name = item.Name,
                        Email = item.Email,
                        Title = item.Title,
                        Comment_Content = item.Comment_Content,
                        CreateDate = string.Format("{0:dd/MM/yyyy}", item.CreateDate),
                        UserID = Utility.TryParseLong(item.UserID, 0),
                        ID_Ref = Utility.TryParseLong(item.ID_Ref, 0),
                        Ref_Type = Utility.TryParseLong(item.Ref_Type, 0),
                        Status = Helper.TryParseInt(item.Status.ToString(), 1),
                        UniqueKey = item.UniqueKey,
                    });
                }
                return list;
            }
            return null;
        }

        [System.Web.Services.WebMethod]
        public static List<CommentJQList> ViewCommentTraLoi(object p_parent, object p_pageCurent, object p_pagesize)
        {
            int pCurent = Convert.ToInt32(Utility.TryParseLong(p_pageCurent, 1));
            int pSize = Convert.ToInt32(Utility.TryParseLong(p_pagesize, 5));
            long pParent = Utility.TryParseLong(p_parent, 0);

            UserMngOther_BLC blc_user = new UserMngOther_BLC();

            List<CommentJQList> list = new List<CommentJQList>();

            IList<TComment> listView = blc_user.RowsComment(pCurent, pSize, pParent, 1, "HoiDap", -1);
            if (listView.Count > 0)
            {
                foreach (TComment item in listView)
                {
                    list.Add(new CommentJQList
                    {
                        CommentID = Utility.TryParseLong(item.CommentID, 0),
                        Name = item.Name,
                        Email = item.Email,
                        Title = item.Title,
                        Comment_Content = item.Comment_Content,
                        CreateDate = string.Format("{0:dd/MM/yyyy}", item.CreateDate),
                        UserID = Utility.TryParseLong(item.UserID, 0),
                        ID_Ref = Utility.TryParseLong(item.ID_Ref, 0),
                        Ref_Type = Utility.TryParseLong(item.Ref_Type, 0),
                        Status = Helper.TryParseInt(item.Status.ToString(), 1),
                        UniqueKey = item.UniqueKey,
                    });
                }
                return list;
            }
            return null;
        }

        //[System.Web.Services.WebMethod]
        //public static List<ProductJQList> ProductJQList(object p_pageCurent, object p_pagesize, object p_StrCatePro)
        //{
        //    int pCurent = Convert.ToInt32(Utility.TryParseLong(p_pageCurent, 1));
        //    int pSize = Convert.ToInt32(Utility.TryParseLong(p_pagesize, 5));
        //    string strCatepro = p_StrCatePro.ToString().Trim();

        //    ProductMng_BLC_NTX nBLC = new ProductMng_BLC_NTX();
        //    DataSet ds = nBLC.RowsProduct_sort(pCurent, pSize, 1, 1, 0, 1, strCatepro, "", 50, 0, 0);

        //    //int recordCount = nBLC.CountTProduct_Count_sort(1, 1, 1, strCatepro, 0, "", 0, 0);
        //    //string lbl_NumMin = ds.Tables[1].Rows[0][0].ToString();
        //    //string lbl_NumMax = ds.Tables[2].Rows[0][0].ToString();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];

        //        //if (dt.Rows.Count % 8 != 0)
        //        //{
        //        //    for (int i = 0; i < dt.Rows.Count % 8; i++)
        //        //    {
        //        //        DataRow dr = null;
        //        //        dr = dt.NewRow();
        //        //        dr["Name"] = "-";
        //        //        dt.Rows.Add(dr);
        //        //    }
        //        //}

        //        List<ProductJQList> list = new List<ProductJQList>();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            list.Add(new ProductJQList
        //            {
        //                ProductID = Helper.TryParseInt(dr["ProductID"].ToString(), 0),
        //                CategoryID = Helper.TryParseInt(dr["CategoryID"].ToString(), 0),
        //                Image = GetImageUrl_pro(dr["Image"], 3),
        //                Name = dr["Name"].ToString(),
        //                NameLink = DACHelper.ConvertUrlText(dr["Name"].ToString()),
        //            });
        //        }
        //        return list;
        //    }
        //    return null;
        //}
        //[System.Web.Services.WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string[] Getproname(string prefix)
        //{
        //    ProductMng_BLC_NTX blc_pro = new ProductMng_BLC_NTX();
        //    List<string> customers = new List<string>();

        //    //DataTable dt = blc_pro.RowsProduct(1,int.MaxValue,1,0, 1, 1,prefix, 0);
        //    //foreach (DataRow row in dt.Rows)
        //    //{

        //    //    customers.Add(string.Format("{0}-{1}", row["Name"], row["ProductID"]));

        //    //}

        //    using (SqlConnection conn = new SqlConnection())
        //    {
        //        conn.ConnectionString = ConfigurationManager
        //                .ConnectionStrings["constr"].ConnectionString;
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.CommandText = @"select TD.Name, TD.ProductID from TProductDescription as TD INNER JOIN TProduct as TP ON TP.ProductID=TD.ProductID WHERE ((TP.[Status] = @Status)) and ((TD.Name like N'%'+ @SearchText + N'%')) and ((TD.Name like N'%'+ @SearchText + N'%'))";

        //            cmd.Parameters.AddWithValue("@SearchText", prefix);
        //            cmd.Parameters.AddWithValue("@Status", 1);

        //            cmd.Connection = conn;
        //            conn.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    customers.Add(string.Format("{0}-{1}", sdr["Name"], sdr["ProductID"]));
        //                }
        //            }
        //            conn.Close();
        //        }
        //        return customers.ToArray();
        //    }
        //}
        //[System.Web.Services.WebMethod]
        //public static List<ProductCateJQList> ProductCateJQList(object p_StrCatePro)
        //{
        //    int strCatepro = Utilis.TryParseInt(p_StrCatePro);
        //    if (strCatepro > 0)
        //    {
        //        ProductMng_BLC_NTX nBLC = new ProductMng_BLC_NTX();
        //        DataTable dt = nBLC.RowsProductCategoryByParentID(1, int.MaxValue, strCatepro, 1);
        //        List<ProductCateJQList> list = new List<ProductCateJQList>();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            list.Add(new ProductCateJQList
        //            {
        //                CategoryID = Helper.TryParseInt(dr["CategoryID"].ToString(), 0),
        //                Image = GetImageUrl_proCate(dr["Image"], 3),
        //                Name = dr["Name"].ToString(),
        //                NameLink = DACHelper.ConvertUrlText(dr["Name"].ToString()),
        //            });
        //        }
        //        return list;
        //    }
        //    return null;
        //}
    
       
      

        #region image

        private static string GetImageUrl_pro(object p_fileID, int p_type)
        {
            try
            {
                string str = GetImageUrl(Utility.TryParseLong(p_fileID, 0), "ProductImagePath", p_type == 1 ? ImageSizeType.Big : (p_type == 2 ? ImageSizeType.Medium : ImageSizeType.Small));
                //if (System.IO.File.Exists(MapPath(str)))
                if (System.IO.File.Exists(str))
                {
                    return str;
                }
                return "/Images/NoImage.jpg";
            }
            catch (System.Exception ex)
            {
                return "/Images/NoImage.jpg";
            }
        }

        protected static string GetImageUrl_proCate(object p_fileID, int p_type)
        {
            try
            {
                string str = GetImageUrl(Utility.TryParseLong(p_fileID, 0), "ProductCategoryImagePath", p_type == 1 ? ImageSizeType.Big : (p_type == 2 ? ImageSizeType.Medium : ImageSizeType.Small));
                //if (System.IO.File.Exists("~"+str))
                //{
                return str;
                //}
                //return "/Images/NoImage.jpg";
            }
            catch (System.Exception ex)
            {
                return "/Images/NoImage.jpg";
            }
        }

        private static string GetImageUrl(long p_fileID, string p_configPath, PQT.API.File.ImageSizeType p_imageSizeType)
        {
            PQT.API.File.FileManager fileMng = new PQT.API.File.FileManager();
            return fileMng.GetImageUrl(p_fileID, p_configPath, p_imageSizeType);
        }


        #endregion
        //#region newsImgJquery
        //        [System.Web.Services.WebMethod]
        //        public static string jquerysliderNewsIMG(object p_VideoID)
        //        {

        //        }
        //#endregion
        #region cart
        //[WebMethod]
        //public static string Edit_Cart_Quantity(int quantity, int product_id)
        //{
        //    try
        //    {
        //        DataTable dtCartList = (DataTable)HttpContext.Current.Session["TableCart"];

        //        string selectQuery = string.Format("ProductID='{0}'", product_id);
        //        DataRow[] arrRow = dtCartList.Select(selectQuery);
        //        arrRow[0]["Quantity"] = quantity;
        //        //int currencyID = Convert.ToInt32(arrRow[0]["currencyID"]);
        //        //string currency = GetCurrency(currencyID);
        //        ProductMng_BLC_NTX nBLC = new ProductMng_BLC_NTX();
        //        TProduct enp = nBLC.GetProduct(product_id);

        //        decimal price = enp.Price;
        //        decimal sum = price * quantity;
        //        //arrRow[0]["Sum"] = sum;

        //        decimal total = 0;
        //        int cart_count = 0;
        //        int Qty_item = 0;
        //        foreach (DataRow dr in dtCartList.Rows)
        //        {
        //             Qty_item = Convert.ToInt32(dr["Quantity"]);
                   
        //            decimal price_item = Convert.ToDecimal(dr["Price"]);
        //            total += Qty_item * price_item;
        //            dr["Sum"] = Qty_item * price_item;
        //            cart_count += Qty_item;
        //        }

               

        //        HttpContext.Current.Session["TableCart"] = dtCartList;
        //        HttpContext.Current.Session["CartCount"] = cart_count;

        //        string equal = string.Format("{0:N0} {1}|{2:N0} {3}/{4}", sum, "VNĐ", total, "VNĐ", cart_count);
        //        return equal.Replace(',', '.');

        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}

        //private static string GetCurrency(object curr_id)
        //{
        //    ProductMng_BLC_NTX pBLC = new ProductMng_BLC_NTX();
        //    CurrencyEntity entCurrency = pBLC.RowCurrency(Convert.ToInt32(curr_id));
        //    string current = "";
        //    return current = entCurrency != null ? entCurrency.SymbolRight.ToUpper() : string.Empty;
        //}
        [WebMethod]
        public static string Delete_Cart(int product_id)
        {
            try
            {
                
                DataTable dtCartList = (DataTable)HttpContext.Current.Session["TableCart"];
                string selectQuery = string.Format("ProductID='{0}'", product_id);
                DataRow[] arrRow = dtCartList.Select(selectQuery);
                arrRow[0].Delete();
               
              
                return "Done";
            }
            catch
            {
                return "";
            }
        }
        #endregion
        #region tour
        [WebMethod]
        public static string bindCateTourByParentID(int TourcategorrID,int LangID)
        {
            try
            {
                TourMng_BLC TourBLC = new TourMng_BLC();

                int parentID = 0;
                if (TourcategorrID > 0)
                {
                    parentID = TourcategorrID;
                }

                DataTable dt = TourBLC.TTourCategoryByParentID_Rows(1, int.MaxValue, TourcategorrID, LangID);

                string html = "";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        html += "<option value='"+r["TourCategoryID"]+"'>";
                        html += r["Name"];
                        html += "</option>";
                    }
                }
                else
                {
                    html = "<option value='-1'>Chọn Loại Tour</option>";
                }

                return html;
            }
            catch
            {
                return "";
            }
        }

        #endregion
    }
}