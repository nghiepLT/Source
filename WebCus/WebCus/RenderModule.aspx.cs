using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.API;
using PQT.API.Module;
using PQT.Common;
using PQT.Common.Authentication;
using PQT.DAC;

namespace WebCus
{
    public partial class RenderModule : CommonPage
    {
        UserMng_BLC blc_user = new UserMng_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!CheckAdminPage() && !CheckPermission())
            //{
            //    Response.Redirect(Config.HTTPServer + "Login.aspx");
            //}
        }

        public string TitlePage
        {
            get
            {
                string result = string.Empty;
                string smid = string.Empty;
                string muid = string.Empty;

                if (Request.QueryString["muid"] != null)
                    muid = Request.QueryString["muid"];
                if (Request.QueryString["smid"] != null)
                    smid = Request.QueryString["smid"];

                ModulePageInfo modulePageInfo = ModulePageInfo.PageInfoLoad(smid);
                foreach (PageInfo p in modulePageInfo.pageInfos)
                {
                    if (p.key == muid)
                    {
                        result = p.description;
                        break;
                    }
                }
                return result;
            }
        }

        protected override void CreateChildControls()
        {
            int IntIsAdmin = 0;
            if (Session["LoginInfo"] != null)
            {
                SiteIdentity identity = (SiteIdentity)Session["LoginInfo"];
                IntIsAdmin = identity.UserAuth;
                //== (int)UserAuthType.ADMIN;
            }

            if (CheckUserPage() == true || IntIsAdmin == (int)UserAuthType.ADMIN || Request.QueryString["muid"]=="PoinKm")
            {
                string moduleController = String.Empty;
                string smid = String.Empty;
                string muid = string.Empty;

                if (Request.QueryString["muid"] != null)
                    muid = Request.QueryString["muid"];

                if (Request.QueryString["md"] != null)
                    moduleController = Request.QueryString["md"];
                if (Request.QueryString["smid"] != null)
                    smid = Request.QueryString["smid"];

                #region MRI Setting

                ModuleRenderInfo mri = new ModuleRenderInfo();
                mri.UseModuleID = 1;
                mri.ModuleTitle = "ModuleName";
                mri.ModulePath = smid;//smid
                // SiteIdentity
                if (Session["LoginInfo"] != null)
                {
                    SiteIdentity identity = (SiteIdentity)Session["LoginInfo"];

                    mri.IsAdmin = identity.UserAuth == (int)UserAuthType.ADMIN;
                    mri.IsDataAdmin = identity.UserAuth == (int)UserAuthType.DATAADMIN;
                    if (identity.UserAuth == (int)UserAuthType.USER && !this.HttpServer.Contains("test.") || identity.UserAuth == (int)UserAuthType.USER && !this.HttpServer.Contains("ktht."))
                    {
                        Response.Redirect(this.HttpAuthServer);
                    }
                }
                else if (!this.HttpServer.Contains("test.") && !this.HttpServer.Contains("ktht."))
                {
                    Response.Redirect(this.HttpAuthServer);
                    mri.IsViewPage = true;
                    mri.ModuleDataKey = null;
                    mri.IsConfigPage = false;//Fix
                }
                #endregion

                string controlPath = Config.ModulesDir + mri.ModulePath + "/" + moduleController;
                Control control = LoadControl(controlPath);
                ((XVNET_ModuleControl)control).InitModule(mri);
                control.EnableViewState = true;
                Panel_Modules.Controls.Add(control);
            }//login x

            else
            {
                Label lbms = new Label();
                lbms.Text = "<div style='text-align:center;margin-top:8px;font-weight:bold;font-size:12pt;'>Trang bạn yêu cầu không tồn tại</div>";
                Panel_Modules.Controls.Add(lbms);

                //string textReaderText = "<div style='text-align:center;margin-top:8px;'>Trang bạn yêu cầu không tồn tại</div>";
                //Console.WriteLine(textReaderText);
            }//login x
        }


        private bool CheckUserPage()
        {
            string strUrl = Request.RawUrl.TrimStart('/');
            TMenuAdmin ent = blc_user.RowMenuAdmin_ByLink(strUrl);
            if (ent != null)
            {
                int Menuid = ent.MenuID;
                TUserMapLink entMap = blc_user.RowUserMapLink_ByMenuID_UserID(Menuid, this.UserID);
                if (entMap != null)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
