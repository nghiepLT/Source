using PQT.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using UserMng.DAC;

namespace WebCus
{
    public partial class Chedo_phucloi : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        static UserMngOther_BLC blc_user2 = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        public int UserMemberID
        {
            get
            {
                if (HttpContext.Current.Session["g_UserMemberID"] != null)
                    return Convert.ToInt32(HttpContext.Current.Session["g_UserMemberID"]);
                return 0;
            }
            set
            {
                HttpContext.Current.Session["g_UserMemberID"] = value;
            }
        }
        protected Guid IDNTD
        {
            get
            {
                if (ViewState["g_IDNTD"] != null)
                    return Guid.Parse(ViewState["g_IDNTD"].ToString());
                return new Guid();
            }
            set
            {
                ViewState["g_IDNTD"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    this.IDNTD = Guid.Parse(Request.QueryString["id"]);

                }
                BindingData();
               
            }
            int selectid = 0;
            if (this.IDNTD == Guid.Empty)
            {
                selectid = this.UserMemberID;
                var listQuytrinh = blc_user.GetListChedophucloi();
                foreach (var item in listQuytrinh)
                {
                    //Kiểm tra tồn tại
                    blc_user.checkNhanvienphucloi(selectid, item.ChedoId);
                } 
            }
            //Insert nếu chua có 
            else
            {
                UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
                //Giờ làm việc
                TUser tus = blc_user.GetUserByNhanvienId(uv.IdNhanVien.Value);
                selectid = tus.UserID;
                this.chkchedobaohiem.Disabled = true;
                this.chkchinhsachthamnien.Disabled = true;
                this.chkchedothuong.Disabled = true;
                this.chkphucloi.Disabled = true;
            }
            //Chế độ bảo hiểm
            if (blc_user.CheckStatusPhucloi(selectid, 1) == 0)
            {
                this.chkchedobaohiem.Checked = false;
            }
            else
            {
                this.chkchedobaohiem.Checked = true;
            }
            //Chính sách thêm niên
            if (blc_user.CheckStatusPhucloi(selectid, 2) == 0)
            {
                this.chkchinhsachthamnien.Checked = false;
            }
            else
            {
                this.chkchinhsachthamnien.Checked = true;
            }
            //Chế độ thưởng
            if (blc_user.CheckStatusPhucloi(selectid, 3) == 0)
            {
                this.chkchedothuong.Checked = false;
            }
            else
            {
                this.chkchedothuong.Checked = true;
            }
            //Phúc lợi
            if (blc_user.CheckStatusPhucloi(selectid, 4) == 0)
            {
                this.chkphucloi.Checked = false;
            }
            else
            {
                this.chkphucloi.Checked = true;
            }
        }
        public static int UserMemberIDStatic
        {
            get
            {
                if (HttpContext.Current.Session["g_UserMemberID"] != null)
                    return Convert.ToInt32(HttpContext.Current.Session["g_UserMemberID"]);
                return 0;
            }
            set
            {
                HttpContext.Current.Session["g_UserMemberID"] = value;
            }
        }
        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
        public void BindingData()
        {
            var data = blc_user.GetListChedophucloi();
            this.chedobaohiem.Value = HtmlToPlainText(data.Where(m => m.ChedoId == 1).FirstOrDefault().Description);
            this.chinhsachthamnien.Value = HtmlToPlainText(data.Where(m => m.ChedoId == 2).FirstOrDefault().Description);
            this.chedothuong.Value = HtmlToPlainText(data.Where(m => m.ChedoId == 3).FirstOrDefault().Description);
            this.phucloi.Value = HtmlToPlainText(data.Where(m => m.ChedoId == 4).FirstOrDefault().Description);
        }
        [WebMethod]
        public static string UDChedobaohiem(string name,int status)
        { 
            if (name == "true")
            {
                blc_user2.CapnhatStatusPhucloi(UserMemberIDStatic,status, 1);
            }
            else
            {
                blc_user2.CapnhatStatusPhucloi(UserMemberIDStatic, status, 0);
            }
            return "Hello " + name;
        }
    }
}