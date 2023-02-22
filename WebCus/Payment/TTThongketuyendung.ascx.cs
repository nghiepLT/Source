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
using PQT.API;
using UserMng.BLC;
using System.Collections.Generic;
using PQT.DAC;
using PQT.Common;
using System.IO;
using System.Data.OleDb;
using System.Linq;
using PQT.DAC.ViewModel;

namespace WebCus
{
    public partial class TTThongketuyendung : System.Web.UI.UserControl
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drop_yeucautuyendung.DataSource = blc_user.ListYCTD();
                drop_yeucautuyendung.DataTextField = "TieuDe";
                drop_yeucautuyendung.DataValueField = "IdYeuCau";
                drop_yeucautuyendung.DataBind();
                //Thoi gian
                txtDateFrom.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month,1);
                var lastDate= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                txtDateTo.SelectedDate=new DateTime(DateTime.Now.Year, DateTime.Now.Month, lastDate);
            }
        }
       
        protected void Click_upload(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (dropTructhuoc.SelectedValue == "1")
            {
                gvBanner.DataSource = blc_user.GetThongKePhongBan(txtDateFrom.SelectedDate, txtDateTo.SelectedDate);
                gvBanner.DataBind();
            }
           
        }

        protected void dropTructhuoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}