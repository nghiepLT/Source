using PQT.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using UserMng.DAC;
namespace WebCus
{
    public partial class TTDanhgiaungvien : System.Web.UI.UserControl
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGird();
            }
        }
        private void BindGird()
        {
            int userid = 0;

            IList<UngVien> list = blc_user.GetListUngVien().Where(m => m.Status == 1).ToList();
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        public string checkchucnang(string status, string id)
        {
            if (status == "-1")
            {
                return "<a style='color:red'>Không trúng tuyển</a>";
            }
            if (status == "0")
            {
                return "<a onclick=\"ShowPopupMapLink('" + Guid.Parse(id) + "')\" style='color:blue'>Duyệt</a>";
            }

            if (status == "1")
            {
                return "<a onclick=\"ShowPopupMapLink2('" + Guid.Parse(id) + "')\" style='color:blue'>Đánh giá</a>";
            }
            if (status == "2")
            {
                return "<a style='color:blue'>Đã duyệt</a>";
            }
            return "";
        }
    }
}