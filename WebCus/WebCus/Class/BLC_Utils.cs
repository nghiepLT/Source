using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebCus
{
    public class BLC_Utils
    {
        public BLC_Utils()
        {
        }

        public DataSet Product_Search(int p_page, int p_pageSize, int intLangID, int intStatus, int intCategoryID, decimal decPriceMin, decimal decPriceMax)
        {
            string sql = string.Format(@"[p_TProduct_Search_Price] @intPage = {0}, @intPageSize = {1},  @intLangID = {2},  @intStatus = {3}, 
                                        @intCategoryID = {4}, @decPriceMin = {5}, @decPriceMax = {6}"
                                      , p_page, p_pageSize, intLangID, intStatus, intCategoryID, decPriceMin, decPriceMax);

            return (new ConnectSQL()).connect(sql);
        }

    }
}