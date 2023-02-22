using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NewsMng.BLC;
using NewsMng.DataDefine;
using FredCK;

namespace NewsMng
{
    public partial class FCKMultiLanguage : System.Web.UI.UserControl
    {
        NewsMng_BLC_NTX nBLC = new NewsMng_BLC_NTX();

        protected void Page_Load(object sender, EventArgs e)
        {
            rptText.DataSource = this.LanguageTable;
            rptText.DataBind();
        }

        protected DataTable LanguageTable
        {
            get
            {
                return nBLC.RowsLanguage();
            }
        }

        public void DataBind()
        {

            rptText.DataSource = this.LanguageTable;
            rptText.DataBind();
        }

        private DataTable InitTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("LangID", typeof(string));

            return dt;
        }

        protected string GetText(object p_langID)
        {
            try
            {
                string result = "";
                int langID = Convert.ToInt32(p_langID);
                if (this.DataSource != null)
                {
                    DataRow[] arrRow = DataSource.Select("LangID=" + langID);
                    result = arrRow[0]["Text"].ToString();
                }
                return result;
            }
            catch (System.Exception e)
            {
                return string.Empty;
            }

        }

        public DataTable DataSource
        {
            get;
            set;
        }

        public DataTable DataText
        {
            get
            {
                DataTable dt = InitTable();
                DataRow dr = null;
                foreach (RepeaterItem item in rptText.Items)
                {
                    FredCK.FCKeditorV2.FCKeditor txtName = item.FindControl("txtName") as FredCK.FCKeditorV2.FCKeditor;
                    HiddenField hdnLangID = item.FindControl("hdnLangID") as HiddenField;
                    if (txtName != null)
                    {
                        dr = dt.NewRow();
                        dr["Text"] = txtName.Value;
                        dr["LangID"] = hdnLangID.Value;
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }

        }

        private TextBoxMode modeText = TextBoxMode.SingleLine;

        public TextBoxMode TextMode
        {
            get
            {
                return modeText;
            }
            set
            {
                modeText = value;
            }
        }

        public Unit TextWidth
        {
            get;
            set;
        }

        public Unit TextHeight
        {
            get;
            set;
        }

        public Unit TitleWidth
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string ToolbarSet
        {
            get;
            set;
        }

    }
}