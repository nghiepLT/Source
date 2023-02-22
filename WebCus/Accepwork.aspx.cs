using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UserMng.BLC;
using UserMng.DataDefine;
using PQT.Common.Authentication;
using PQT.DAC;

namespace WebCus
{
    public partial class Accepwork : System.Web.UI.Page
    {
        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int idNghi = 0;
                UserEntity userEnt = null;
                if (Session["g_UserMemberID"].ToString() != null)
                {
                    int iduser = Utils.TryParseInt(Session["g_UserMemberID"].ToString(), 0);
                    userEnt = nBLC.RowUser(iduser);
                    if (userEnt != null)
                    {
                        userEnt.Gender = idNghi;

                        tBLC.AddUser(userEnt);
                        string message = "Chào Mừng Bạn Đã Đi Làm Trở Lại";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                }
                else Alert.Show(Session["g_UserMemberID"].ToString());
            }
        }
       
    }
}