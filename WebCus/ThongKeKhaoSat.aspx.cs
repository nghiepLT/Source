using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;

namespace WebCus
{
    public partial class ThongKeKhaoSat : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            gvBanner.DataSource = blc_user.GetTKKhaoSat();
            gvBanner.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
        }
        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           

        }
        public string Get7Ngay(DateTime dt)
        {
            if (dt != DateTime.MinValue)
                return dt.ToString();
            return "";
        }
        public string Get14Ngay(DateTime dt)
        {
            if (dt != DateTime.MinValue)
                return dt.ToString();
            return "";
        }
        public string Get2Thang(DateTime dt)
        {
            if (dt != DateTime.MinValue)
                return dt.ToString();
            return "";
        }
    }
}