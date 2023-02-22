using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.API;
using PQT.Controls;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using PQT.Common;
using ProductMng.BLC;
using ProductMng.DataDefine;
using System.Net.Mail;
using UserMng.BLC;
using PQT.DAC;
using NewsMng.BLC;

namespace WebCus
{
    public partial class FavoriteProduct : PQT.API.CommonPage
    {
        ProductMng_BLC_TX tBLC = new ProductMng_BLC_TX();
        ProductMng_BLC_NTX nBLC = new ProductMng_BLC_NTX();
        Seo_BLC blc_seo = new Seo_BLC();
        CartProduct_BLC blc_cart = new CartProduct_BLC();
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        News_BLC blc_news = new News_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.UserMemberID == 0)
            {
                MessageBox.Show("Bạn chưa đăng nhập!",false,"/trang-chu");
            }
            else
            {
                BindDatapro();
            }
        }
        private void BindDatapro()
        {
            IList<TProductCart> list = blc_cart.ViewListProCartByCustID(this.UserMemberID);
            GridCart.DataSource = list;
            GridCart.DataBind();
        }
        protected string BindProName(object p_value)
        {
            if (p_value!=null)
            {
                int int_value = Helper.TryParseInt(p_value.ToString(), 0);
                
                int catepro = nBLC.Get_ProductCategory_By_ProductID(int_value);
                
                ProductDescriptionEntity ent = nBLC.RowProductDescription(this.LangID, int_value);
                if (ent!=null )
                {
                    string str = string.Format("<a href='/chi-tiet-san-pham/{0}/{1}/{2}.html'>{3}</a>", int_value, catepro, WebCus.DACHelper.ConvertUrlText(ent.Name), ent.Name);
                    return str;
                }
            }
            return string.Empty;
        }
        protected string BindProPrice(object p_value)
        {
            if (p_value != null)
            {
                int int_value = Helper.TryParseInt(p_value.ToString(), 0);
                TProduct enp = nBLC.GetProduct(int_value);
                if (enp!=null)
                {
                    return string.Format("{0:N0}",enp.Price);
                }
            }
            return string.Empty;
        }
        protected int UserMemberID
        {
            get
            {
                if (Session["g_UserMemberID"] != null)
                    return Convert.ToInt32(Session["g_UserMemberID"]);
                return 0;
            }
            set
            {
                Session["g_UserMemberID"] = value;
            }
        }

        protected void GridCart_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int cartid = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                blc_cart.Delete(cartid);
                BindDatapro();
            }
        }
    }
}
