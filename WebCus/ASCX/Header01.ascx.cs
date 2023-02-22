using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.Common;
using PQT.Common.Authentication;

namespace WebCus.ASCX
{
    public partial class Header01 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMenuPermission();
        }

        public string WebsiteName
        {
            get;
            set;
        }

        public string UserLogin
        {
            get;
            set;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["LoginInfo"] = null;
            Response.Redirect(Config.HTTPServer);
        }

        private void SetMenuPermission()
        {
            if (Session["PermissionString"] != null)
            {
                string permisString = Session["PermissionString"].ToString();
                tdProduct.Visible = permisString[0] == '1';
                tdProductList.Visible = permisString[0] == '1';
                tdProductCategory.Visible = permisString[1] == '1';
                tdNews.Visible = permisString[3] == '1';

                string paramUser = permisString[5] == '1' ? "UserInfo" : "UserView";
                string link = string.Format("/RenderModule.aspx?smid=UserMng&md=UserControl.ascx&muid={0}", paramUser);
                hplUserInfo.NavigateUrl = link;


            }
            if (Session["LoginInfo"] != null)
            {
                SiteIdentity identity = (SiteIdentity)Session["LoginInfo"];
                bool isAdmin = identity.UserAuth == (int)UserAuthType.ADMIN;
                tdLanguage.Visible = isAdmin;
            }
        }

    }
}