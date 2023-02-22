using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using PQT.DAC;
using PQT.DAC.ViewModel;
using PQT.Common;
using Newtonsoft.Json;
using UserMng.DAC;
using System.Web.Services;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Net.Mail;
namespace WebCus
{
    public partial class RenderPopupCapnhatngaylam : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
                    UngVien uv = blc_user.GetUngVienByID(GuiId);
                    ThongTinNhanSu ttns = blc_user.GetTTNhansu_byID(uv.IdNhanVien.Value);
                    txtDateTo.SelectedDate = ttns.NgayVaoLam.Value;
                }
            }
        }
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            Guid GuiId = Guid.Parse(Request.QueryString["id"].ToString());
            blc_user.CapnhatNgayvaolam(GuiId, this.txtDateTo.SelectedDate);
            string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
            base.Response.Write(close);
        }
    }
}