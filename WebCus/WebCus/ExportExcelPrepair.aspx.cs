using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainProject
{
    public partial class ExportExcelPrepair : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Session["HtmlDataExport"] = hdnData.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenExportEnd", "OpenExportEnd();", true);
            //Response.Redirect("/ExportExcel.aspx");

        }
    }
}