using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using PQT.API;
using PQT.Common;

namespace NewsMng
{
    public partial class NewsControl : XVNET_ModuleControl
    {
        #region Member Variable

        bool m_error = false;

        #endregion

        #region Properties

        public string CurrentLoadPage
        {
            get
            {
                if (this.ViewState["_currLoadCtl"] != null)
                    return this.ViewState["_currLoadCtl"].ToString();
                else
                    return CurrentLoadPage = LoadPage;
            }
            private set
            {
                this.ViewState["_currLoadCtl"] = value;
            }
        }

        public string PrevLoadPage
        {
            get
            {
                if (this.ViewState["_prevLoadCtl"] != null)
                    return this.ViewState["_prevLoadCtl"].ToString();
                else
                {
                    return string.Empty;
                }
            }
            private set
            {
                this.ViewState["_prevLoadCtl"] = value;
            }
        }

        private string LoadPage
        {
            get
            {
                if (Request.QueryString["muid"] != null)
                {
                    string muid = Request.QueryString["muid"].ToString();
                    return PageInfo(muid);
                }
                else
                {
                    string md = Request.QueryString["md"].ToString();
                    return PageInfo(md);
                }
            }

        }

        #endregion

        #region Page Event

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            PrevLoadPage = CurrentLoadPage;
        }

        protected override void OnInit(EventArgs e)
        {
            DBSetting();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!m_error)
                LoadCurrentControl();
        }

        #endregion

        #region Private Method

        private bool DBSetting()
        {
            bool returnVal = false;
            return returnVal;
        }

        private void LoadCurrentControl()
        {
            string controlPath = CurrentLoadPage;
            Control currCotrol = LoadControl(controlPath);
            ((XVNET_ModuleControl)currCotrol).InitModule(MRI);
            this.Controls.Add(currCotrol);
        }

        #endregion

        #region Internal Method

        internal void SetViewState(string p_key, object p_value)
        {
            ViewState[p_key] = p_value;
        } 

        internal object GetViewState(string p_key)
        {
            if (ViewState[p_key] != null)
                return this.ViewState[p_key];
            else
                return null;
        }

        internal void SetEvent(string page)
        {
            bool changed = false;
            if (CurrentLoadPage != page)
            {
                CurrentLoadPage = page;
                changed = true;
            }

            if (changed)
            {
                this.Controls.Clear();
                LoadCurrentControl();
            }
        }

        #endregion
    }
}