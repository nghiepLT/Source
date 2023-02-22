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
using System.Net.Mail;

namespace WebCus
{
    public partial class ThietLapGuiMail : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        public static UserMngOther_BLC blc_user2 = new UserMngOther_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGird();
            }
        }
        private void BindGird()
        {
            IList<MailSendUngVien> list = blc_user.GetListMailSend();
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
           var email= Page.Request.Form["ctl00$MainContent$txtEmail"].ToString();
            blc_user.InsertEmail(email);
            Response.Redirect(Request.RawUrl);
        }
        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int IDMail = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "DeleteItem")
            {
                blc_user.DeleteEmail(IDMail);
                Alert.Show("Ẩn thành công!");
                Response.Redirect(Request.RawUrl);
            }
        }
        protected void chkview_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox cb = (CheckBox)sender;
            bool chk= cb.Checked;
            var id = int.Parse(cb.Attributes["CommandName"]);
            blc_user2.UpdateStatusEmail(id, chk);
            Response.Redirect(Request.RawUrl);
        }
        public bool CheckStatus(string status)
        {
            return true;
        }
        [System.Web.Services.WebMethod]
        public static string Autugetname(string key)
        {
            
           
            string html = "";
            var listUser = blc_user2.GetListUser();
            foreach (var item in listUser)
            {
                html += item.UserName + "-"+item.Email+",";
            }

            return html;
        }
    }
}