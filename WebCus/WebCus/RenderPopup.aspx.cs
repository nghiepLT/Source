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
using PQT.API.Module;
using PQT.Common;
using PQT.Common.Authentication;
using PQT.DAC;

namespace WebCus
{
    public partial class RenderPopup : CommonPage//Page //PageControl
    {
        UserMng_BLC blc_user = new UserMng_BLC();

        int m_umid = 0;
        string m_sc = string.Empty;
        string m_smid = string.Empty;
        string m_renderPage = string.Empty;
        string m_moduleDir = string.Empty;
        bool m_isWrite = false;
        bool m_isAdmin = false;
        bool m_isDataAdmin = false;

        private int _userID;

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        public string JavascriptPath
        {
            get { return Config.JavascriptPath; }
        }

        protected override void CreateChildControls()
        {
            int IntIsAdmin = 0;
            if (Session["LoginInfo"] != null)
            {
                SiteIdentity identity = (SiteIdentity)Session["LoginInfo"];
                IntIsAdmin = identity.UserAuth;

                if (CheckUserPage() == true || IntIsAdmin == (int)UserAuthType.ADMIN || IntIsAdmin==(int)UserAuthType.DATAADMIN)
                {
                    m_sc = Helper.ValidateParam("SC", string.Empty);
                    m_moduleDir = Helper.ValidateParam("moduleDir", string.Empty);
                    m_umid = Helper.ValidateParam("umid", 0);
                    m_renderPage = Helper.ValidateParam("renderPage", string.Empty);
                    m_smid = Helper.ValidateParam("smid", string.Empty);

                    #region Fake API

                    //Guid userGUID = (Guid)Membership.GetUser().ProviderUserKey;
                    //_userID = _repository.GetStemUserID(userGUID);
                    //m_isAdmin = _repository.isSiteAdmin(_userID, 1);

                    // SiteIdentity
                    if (Session["LoginInfo"] != null)
                    {
                        PopupRender(identity);
                    }
                    else
                        PopupRender();

                    #endregion

                    #region <Original Code>

                    //ILoginInfo iLoginInfo = new LoginInfo();
                    //UserAuthType userAuthType = iLoginInfo.UserAuth;
                    //if (userAuthType != UserAuthType.USER)
                    //    m_isAdmin = true;

                    #endregion
                }
                else
                {
                    Label lbms = new Label();
                    lbms.Text = "<div style='text-align:center;margin-top:8px;font-weight:bold;font-size:12pt;'>Trang bạn yêu cầu không tồn tại !</div>";
                    RenderCell.Controls.Add(lbms);
                }
            }
            else
            {
                Label lbms = new Label();
                lbms.Text = "<div style='text-align:center;margin-top:8px;font-weight:bold;font-size:12pt;'>Vui lòng đăng nhập</div>";
                RenderCell.Controls.Add(lbms);
            }            
        }

        private bool CheckUserPage()
        {
            string strUrl = Helper.ValidateParam("md",string.Empty);
            
                TMenuAdmin ent = blc_user.RowMenuAdmin_ByOption1(strUrl);
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

        private void PopupRender(SiteIdentity p_identity)
        {
            Control popupControl = null;
            string moduleDir = (string.IsNullOrEmpty(m_moduleDir) ? Config.ModulesDir : m_moduleDir); 
            popupControl = LoadControl(moduleDir + m_smid + "/" + m_renderPage);
            ModuleRenderInfo mri = new ModuleRenderInfo();

            mri.UseModuleID = m_umid;
            mri.ModuleTitle = Helper.ValidateParam("moduletitle", Config.PageSubTitle);
            mri.ModulePath = m_smid;
            mri.SiteMenuID = string.Empty;
            mri.IsConfigPage = Helper.IsConfigMode(m_umid);
            mri.IsViewPage = Helper.IsViewMode(m_umid);

            mri.IsAdmin = p_identity.UserAuth == (int)UserAuthType.ADMIN;

            mri.IsDataAdmin = true;

            ((XVNET_ModuleControl)popupControl).InitModule(mri);

            RenderCell.Controls.Add(popupControl);
        }


        private void PopupRender()
        {
            Control popupControl = null;
            string moduleDir = (string.IsNullOrEmpty(m_moduleDir) ? Config.ModulesDir : m_moduleDir);

            popupControl = LoadControl(moduleDir + m_smid + "/" + m_renderPage);
            ModuleRenderInfo mri = new ModuleRenderInfo();

            mri.UseModuleID = m_umid;
            mri.ModuleTitle = Helper.ValidateParam("moduletitle", Config.PageSubTitle);
            mri.ModulePath = m_smid;
            mri.SiteMenuID = string.Empty;
            mri.IsConfigPage = Helper.IsConfigMode(m_umid);
            mri.IsViewPage = Helper.IsViewMode(m_umid);

            ((XVNET_ModuleControl)popupControl).InitModule(mri);

            RenderCell.Controls.Add(popupControl);
        }

    }
}
