using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;


namespace WebCus
{
    public partial class BasePage : System.Web.UI.Page
    {

        protected int LangID
        {
            get
            {
                if (Session["_langID"] != null)
                    return Convert.ToInt32(Session["_langID"]);
                return 1;
            }
            set
            {
                Session["_langID"] = value;
            }
        }

        protected override void InitializeCulture()
        {
            string ctrlname = this.Request.Params["__EVENTTARGET"];
            //Response.Write("--- " + ctrlname + " ---");
            if (ctrlname != null)
            {
                if (ctrlname.Equals("VN"))
                {
                    SetCulture("vi-VN", "vi-VN");
                    this.LangID = 1;
                }
                else if (ctrlname.Equals("EN"))
                {
                    SetCulture("en-US", "en-US");
                    this.LangID = 2;
                }
            }

            if (Session["MyUICulture"] != null && Session["MyCulture"] != null)
            {
                Thread.CurrentThread.CurrentUICulture = (CultureInfo)Session["MyUICulture"];
                Thread.CurrentThread.CurrentCulture = (CultureInfo)Session["MyCulture"];
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            }

            base.InitializeCulture();
        }

        protected void SetCulture(string name, string locale)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);

            Session["MyUICulture"] = Thread.CurrentThread.CurrentUICulture;
            Session["MyCulture"] = Thread.CurrentThread.CurrentCulture;
        }
    }
}
