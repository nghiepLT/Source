using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using UserMng.DAC;
using PQT.DAC.ViewModel;
using PQT.Common;
using PQT.DAC;

namespace WebCus
{
    public partial class RenderPopupCapNhatTrangThai : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
                UngVien uv = blc_user.GetUngVienByID(GuiId);
                if (uv.TrangThaiUngVien == 1)
                    rad1.Checked = true;
                if (uv.TrangThaiUngVien == 2)
                    rad2.Checked = true;
                if (uv.TrangThaiUngVien == 3)
                    rad3.Checked = true;

            }
        }
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            var rad= Page.Request.Form["ctl00$ContentPlaceHolder1$radchung"].ToString();
            int Type = 0;
            if (rad == "rad1")
                Type = 1;
            if (rad == "rad2")
                Type = 2;
            if (rad == "rad3")
                Type = 3;
            Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
            blc_user.Capnhattrangthai(GuiId, Type);
            string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
            base.Response.Write(close);
        }
    }
}