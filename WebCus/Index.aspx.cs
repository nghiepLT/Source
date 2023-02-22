using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCus
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            //Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(@"D:\SourceCode\Source\WebCus\Uploads\TuyenDung\3ttuvdutuyen.doc");
            //object missing = System.Reflection.Missing.Value;
            //if (doc == null)
            //{
            //    Alert.Show("Null");
            //    return;
            //}
            //var aa = doc.Content.Text;
            //Alert.Show(aa);
            //doc.Close();
        }
    }
}