using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.Common.Authentication;

namespace WebCus
{
    public partial class MasterAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DateTime ngayoff= DateTime.Parse("2021-12-30");
            //TimeSpan thoigiansd = DateTime.Now - ngayoff;
            //if (thoigiansd.Days <= 0)
            //{
                Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                Response.Cache.SetValidUntilExpires(false);
                Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                GetUserName();//login x
            //}
            //else MessageBox.Show("Error 79 !",false,"/login.aspx");
        }
        protected void Page_Init(object sender, EventArgs e)
        {

            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        private void GetUserName()
        {
            if (Session["LoginInfo"] != null)
            {
                SiteIdentity identity = (SiteIdentity)Session["LoginInfo"];
                //lblAdminName.Text = identity.Name;
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["LoginInfo"] = null;
            Response.Redirect("/login.aspx");
        }



    }
}
