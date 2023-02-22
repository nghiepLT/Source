using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Threading;

using PQT.Common;
using UserMng.BLC;
using UserMng.DataDefine;
using PQT.DAC;

namespace WebCus
{
    public partial class ConfirmActiveAccount : System.Web.UI.Page
    {
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfirmAccount();
        }

        private void ConfirmAccount()
        {
            string id = Helper.ValidateParam("id", "");
            if (!string.IsNullOrEmpty(id))
            {
                id = Utility.Decrypt(id.ToString());
                UserEntity ent = nBLC.RowUser(Convert.ToInt32(id));
                if (ent!=null)
                {
                    ent.PermissionString = "1";
                    tBLC.UpdateUser(ent);
                    MessageBox.Show("Xác nhận thành công",false, "/trang-chu");
                }
                else
                    MessageBox.Show("Xác nhận không thành công", false, "/trang-chu");
            }
            else
                MessageBox.Show("Xác nhận không thành công", false, "/trang-chu");

        }

     
    }
}
