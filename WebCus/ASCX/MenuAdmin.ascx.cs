using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.Common;
using PQT.API;
using PQT.Common.Authentication;
using UserMng.BLC;
using UserMng.DataDefine;
using PQT.DAC;

namespace WebCus.ASCX
{
    public partial class MenuAdmin : CommonUserControl
    {
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC blc_user = new UserMng_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Label1.Text = string.Format("{0:N0}", Application["visitors_online"]);
            //Label2.Text = string.Format("{0:N0}", Application["count_visit"]);
            //Label3.Text = string.Format("{0:N0}", Application["count_Today"]);
            lbl_nameUSer.Text = "Đăng nhập TK: <br/>"+ Session["g_UserMemberName"].ToString();
            BindMenu();
            if (this.IntIsAdmin == (int)UserAuthType.ADMIN)
                menuSupperAdmin.Visible = true;//this.IsAdmin;
            else
            {
                menuSupperAdmin.Visible = false;//this.IsAdmin;
            }
            if (!this.HttpServer.Contains("test."))
            {
            }
        }


        #region BindMenu

        private void BindMenu()
        {
            DataTable dt = new DataTable();

            if (this.IntIsAdmin == (int)UserAuthType.ADMIN)
            {
                dt = blc_user.RowsMenuAdminByParentID(0, 1, 0, -1);
            }

            if (this.IntIsAdmin == (int)UserAuthType.DATAADMIN)
            {
                dt = blc_user.RowsMenuAdminByParentID(0, 1, 0, this.UserID);
            }
            //dt = blc_user.RowsMenuAdminByParentID(0, 1, 0, -1);//login x
            rptMenu.DataSource = dt;
            rptMenu.DataBind();
        }
        public string changerlink(object url) {

            if (url.ToString().Contains("?code"))
            {                
               //http://192.168.117.111:8080/?code=7do0pgA9HjUtZlqqHVZU9NoaABXaYRpA
                string code = Utility.Encrypt(Session["codesecret"].ToString());
                string urltrans = url.ToString().Replace("?code","?code="+code);
                //    // "http://192.168.117.111:8080/?code".Replace("?code", "?code=" + code)
                //    Response.Redirect(urltrans);

                return urltrans;
            }
            else return url.ToString();           
        }

        #endregion

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

        private int IntIsAdmin
        {
            get
            {
                if (Session["LoginInfo"] != null)
                {
                    SiteIdentity identity = (SiteIdentity)Session["LoginInfo"];
                    return identity.UserAuth;
                }
                return 0;
            }
        }

        private bool IsAdmin
        {
            get
            {
                if (Session["LoginInfo"] != null)
                {
                    SiteIdentity identity = (SiteIdentity)Session["LoginInfo"];
                    return identity.UserAuth == (int)UserAuthType.ADMIN;
                }
                return false;
            }
        }

        #region Events

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                int menuID = Convert.ToInt32(dr["MenuID"]);
                Repeater rptSubMenu = e.Item.FindControl("rptSubMenu") as Repeater;

                DataTable dt = new DataTable();

                if (this.IntIsAdmin == (int)UserAuthType.ADMIN)
                {
                    dt = blc_user.RowsMenuAdminByParentID(0, 1, menuID, -1);
                }

                if (this.IntIsAdmin == (int)UserAuthType.DATAADMIN)
                {
                    dt = blc_user.RowsMenuAdminByParentID(0, 1, menuID, this.UserID);
                }
                //dt = blc_user.RowsMenuAdminByParentID(0, 1, menuID, -1);//login x
                //DataTable dtChild = blc_user.RowsMenuAdminByParentID(0, 1, menuID);

                rptSubMenu.DataSource = dt;
                rptSubMenu.DataBind();

                Control menuSub = e.Item.FindControl("menuSub");
                if (menuSub != null && dt.Rows.Count == 0)
                    menuSub.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["LoginInfo"] = null;
            Response.Redirect(Config.HTTPServer);
        }

        #endregion


    }
}