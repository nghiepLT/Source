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
    public partial class Content : PQT.API.CommonUserControl
    {
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindInfo();
        }

        private void BindInfo()
        {
            //this.NewsCatID = Helper.ValidateParam("id", 0);
            //if (this.UK != null)
            //{
            //    NewsCategoryEntity entCat = nBLC.RowNewsCategoryByUniqueKey(this.UK);
            //    this.NewsCatID = entCat != null ? entCat.NewsCategoryID : Helper.ValidateParam("id", 0);
            //}
            NewsCategoryEntity entCat = nBLC.RowNewsCategoryByUniqueKey(this.UK);
            if (entCat != null)
            {
                this.NewsCatID = entCat != null ? entCat.NewsCategoryID : 0;
                NewsCategoryDescriptionEntity ent = nBLC.RowNewsCategoryDescription(this.LangID, this.NewsCatID);
                NewsCategoryEntity entCatContent = nBLC.RowNewsCategory(this.NewsCatID);

                if (ent != null)
                {
                    if (this.RemoveTagP)
                        ltrContent.Text = ent.Description.Replace("<p>", "").Replace("</p>", "");
                   else if (this.InserLi)
                        ltrContent.Text = ent.Description.Replace("<p>", "<li>").Replace("</p>", "</li>");
                    else if (this.InserDiv)
                        ltrContent.Text = ent.Description.Replace("<p>", "<div>").Replace("</p>", "</div>");
                    else if (this.InserDivSpan)
                        ltrContent.Text = ent.Description.Replace("<p>", "<div><span>").Replace("</p>", "</span></div>");
                    else
                        ltrContent.Text = ent.Description;
                    this.Visible = entCatContent.IsView;

                }
                else
                {
                    this.Visible = false;
                }
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

        public bool RemoveTagP
        {
            get;
            set;
        }
        public bool InserLi
        {
            get;
            set;
        }
        public bool InserDiv
        {
            get;
            set;
        }
        public bool InserDivSpan
        {
            get;
            set;
        }


        #endregion



    }
}