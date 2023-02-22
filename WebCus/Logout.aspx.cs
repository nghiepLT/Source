using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.Common;

namespace WebCus
{
    public partial class Logout : PQT.API.CommonPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Helper.ValidateParam("p", 0) == 0)
            {
                Session["LoginInfo"] = null;
                Response.Redirect("/login");
            }
            else
            {
                Session.Remove("Client_Login_ID");//["Client_Login_ID"] = null;
                Response.Redirect("/home.aspx");
            }
        }
    }
}
