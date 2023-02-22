using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PQT.API;
using PQT.Controls;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using PQT.Common;
using NewsMng.BLC;
using NewsMng.DataDefine;
namespace WebCus.ASCX
{
    public partial class ContentWithID : PQT.API.CommonUserControl
    {
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindInfo();
        }

        private void BindInfo()
        {
            this.NewsCatID = Helper.ValidateParam("id", 0);
            if (this.UK != null)
            {
                NewsCategoryEntity entCat = nBLC.RowNewsCategoryByUniqueKey(this.UK);
                this.NewsCatID = entCat != null ? entCat.NewsCategoryID : Helper.ValidateParam("id", 0);
            }

            NewsCategoryDescriptionEntity ent = nBLC.RowNewsCategoryDescription(this.LangID, this.NewsCatID);

            if (ent != null)
            {
                ltrContent.Text = ent.Description;
            }
        }


        #region Property

        public string UK
        {
            get;
            set;
        }

        private int NewsCatID
        {
            get;
            set;
        }


        #endregion



    }
}