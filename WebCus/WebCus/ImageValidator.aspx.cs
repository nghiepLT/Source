using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PQT.Common;

namespace WebCus
{
    public partial class ImageValidator : System.Web.UI.Page
    {
        SoftGenImage cc = new SoftGenImage();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            RenderImage();
        }



        public void RenderImage()
        {
            int index = Helper.ValidateParam("code", 0);

          //  string[] arrText = new string[] { genString(5), genString(5), genString(5), genString(5), genString(5) };
            string[] arrText = new string[] { genString(4), genString(4), genString(4), genString(4)};
            SoftGenImage.setGenString(arrText[index]);
            SoftGenImage.CreateImage(arrText[index]).Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public string genString(int strLength)
        {
            string str = "";
            Random rand = new Random();
            //str += Convert.ToChar(rand.Next(69,93));
            for (int i = 0; i < strLength; i++)
            {
               // str += Convert.ToChar(rand.Next(65, 90));
                str += Convert.ToChar(rand.Next(48, 57));
            }
            //System.Web.HttpContext.Current.Session["GenStr"] = str;
            return str;
        }
    }
}
