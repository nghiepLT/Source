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
using System.Data.Linq;

namespace UserMng.BLC
{
    public class CartProduct_BLC : XVNET_ModuleControl
    {
        UserMngDataDataContext da = null;
        public CartProduct_BLC()
        {
            if (da == null)
            {
                da = new UserMngDataDataContext();
            }
        }

#region 
        public DataTable ListCartProduct(int p_user, int p_status, int @intLang)
        {
            string sql = string.Format("[p_TCartProductRows] {0},{1},{2}", p_user,p_status, @intLang);
            ConnectSQL cn = new ConnectSQL();
            DataTable dt = cn.connect_dt(sql);
            return dt;
        }

        public double floatTotalPrice(int p_userID)
        {
            return (double)da.TProductCarts.Where(z => z.nguoi_muaID == p_userID && z.status == 1).Sum(z=>z.price);
            //if (list.Count > 0)
            //{
            //    return list.Count;
            //}
            //return 0;

        }

        public IList<TProductCart> ViewListProCartByCustID(int p_user)
        {
            return da.TProductCarts.Where(z => z.nguoi_muaID == p_user).ToList();
        }
        public Int64 CountCart_member(int p_userid)
        {
            Int64 intcart = Utility.TryParseLong(da.TProductCarts.Where(z => z.nguoi_muaID == p_userid && z.status == 1).Sum(z => z.So_luong),0);

            return intcart;
            
        }
        public TProductCart GetCartByUser_pro(int p_user, int p_proid)
        {
            IList<TProductCart> list = da.TProductCarts.Where(z => z.nguoi_muaID == p_user && z.ProductID == p_proid && z.status == 1).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
        public TProductCart GetCartByID(Int64 p_cartID)
        {
            IList<TProductCart> list = da.TProductCarts.Where(z => z.CartID == p_cartID && z.status == 1).ToList();
            if (list.Count > 0)
            {
                return list.First();
            }
            return null;
        }
       
        public Int64 Creat(TProductCart ent)
        {
            TProductCart objEnt = new TProductCart();
            objEnt = ent;
            da.TProductCarts.InsertOnSubmit(objEnt);
            da.SubmitChanges();
            return objEnt.CartID;
        }

        public bool Update(TProductCart ent)
        {
            IList<TProductCart> list = da.TProductCarts.Where(z => z.CartID == ent.CartID).ToList();
            if (list.Count > 0)
            {
                var objVlaue = list.First();
                objVlaue = ent;
                da.TSeoDescriptions.Context.SubmitChanges();

                return true;
            }
            return false;
        }

        public bool UpdateStatus(Int64 p_cartID)
        {
            IList<TProductCart> list = da.TProductCarts.Where(z => z.CartID == p_cartID).ToList();
            if (list.Count > 0)
            {
                var objVlaue = list.First();
                objVlaue.status = 0;
                da.TSeoDescriptions.Context.SubmitChanges();

                return true;
            }
            return false;
        }

        public bool UpdateStatusAllListByUser(int p_user)
        {
            IList<TProductCart> list = da.TProductCarts.Where(z => z.nguoi_muaID == p_user && z.status == 1).ToList();
            if (list.Count > 0)
            {
                foreach(TProductCart ent in list)
                {
                    ent.status = 2;
                    da.TSeoDescriptions.Context.SubmitChanges();
                }
                return true;
            }
            return false;
        }

        //public bool DeleteSEO(Int64 MapID, int SeoType)
        //{
        //    IList<TSeo> list = da.TSeos.Where(z => z.MapID == MapID && z.SeoType == SeoType).ToList();
        //    if (list.Count > 0)
        //    {
        //        da.TSeos.DeleteOnSubmit(list.First());
        //        da.SubmitChanges();
        //    }
        //    return true;
        //}
        public bool Delete(int CartID)
        {
            IList<TProductCart> list = da.TProductCarts.Where(z => z.CartID == CartID).ToList();
            if (list.Count > 0)
            {
                da.TProductCarts.DeleteOnSubmit(list.First());
                da.SubmitChanges();
            }
            return true;
        }
#endregion
    }
}