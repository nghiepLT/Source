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
using PQT.DAC;
namespace WebCus
{
    public partial class Tomtatquytrinh : System.Web.UI.Page
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
                //Insert nếu chua có 
            }
           
            int selectid = 0;
            if (this.IDNTD == Guid.Empty)
            { 
                var listQuytrinh = blc_user.GetListQuytrinhhuongdan();
                foreach (var item in listQuytrinh)
                {
                    //Kiểm tra tồn tại
                    blc_user.checkNhanvienquytry(this.UserMemberID, item.QuytinhhuongdanID);
                }

                selectid = this.UserMemberID;
            }
            else
            { 
                UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
                //Giờ làm việc
                TUser tus = blc_user.GetUserByNhanvienId(uv.IdNhanVien.Value);
                selectid = tus.UserID;
                this.chkgiolamviec.Disabled = true;
                this.chkChamcong.Disabled = true;
                this.chkLamphep.Disabled = true;
                this.chkDirangoai.Disabled = true;
                this.chkTrangphuc.Disabled = true;
                this.chkSinhhoat.Disabled = true;
                this.chkTraodoi.Disabled = true;
                this.chkThoigian.Disabled = true;
            } 
            //Giờ làm việc
            if (blc_user.CheckStatusQuytrinh(selectid, 1) == 0)
            {
                this.chkgiolamviec.Checked = false;
            }
            else
            {
                this.chkgiolamviec.Checked = true;
            }
            //Chấm công
            if (blc_user.CheckStatusQuytrinh(selectid, 2) == 0)
            {
                this.chkChamcong.Checked = false;
            }
            else
            {
                this.chkChamcong.Checked = true;
            }
            //Làm phép
            if (blc_user.CheckStatusQuytrinh(selectid, 3) == 0)
            {
                this.chkLamphep.Checked = false;
            }
            else
            {
                this.chkLamphep.Checked = true;
            }
            //Đi ra ngoài
            if (blc_user.CheckStatusQuytrinh(selectid, 4) == 0)
            {
                this.chkDirangoai.Checked = false;
            }
            else
            {
                this.chkDirangoai.Checked = true;
            }
            //Trang phục
            if (blc_user.CheckStatusQuytrinh(selectid, 5) == 0)
            {
                this.chkTrangphuc.Checked = false;
            }
            else
            {
                this.chkTrangphuc.Checked = true;
            }
            //Sinh hoạt
            if (blc_user.CheckStatusQuytrinh(selectid, 6) == 0)
            {
                this.chkSinhhoat.Checked = false;
            }
            else
            {
                this.chkSinhhoat.Checked = true;
            }
            //Trao đổi
            if (blc_user.CheckStatusQuytrinh(selectid, 7) == 0)
            {
                this.chkTraodoi.Checked = false;
            }
            else
            {
                this.chkTraodoi.Checked = true;
            }
            //Thời gian
            if (blc_user.CheckStatusQuytrinh(selectid, 8) == 0)
            {
                this.chkThoigian.Checked = false;
            }
            else
            {
                this.chkThoigian.Checked = true;
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
            var data = blc_user.GetListQuytrinhhuongdan();
            this.giolamviec.Value = HtmlToPlainText(data.Where(m => m.QuytinhhuongdanID == 1).FirstOrDefault().Description);
            this.chamcong.Value = HtmlToPlainText(data.Where(m => m.QuytinhhuongdanID == 2).FirstOrDefault().Description);
            this.lamphep.Value = HtmlToPlainText(data.Where(m => m.QuytinhhuongdanID == 3).FirstOrDefault().Description);
            this.dirangoai.Value = HtmlToPlainText(data.Where(m => m.QuytinhhuongdanID == 4).FirstOrDefault().Description);
            this.trangphuc.Value = HtmlToPlainText(data.Where(m => m.QuytinhhuongdanID == 5).FirstOrDefault().Description);
            this.sinhhoat.Value = HtmlToPlainText(data.Where(m => m.QuytinhhuongdanID == 6).FirstOrDefault().Description);
            this.traodoi.Value = HtmlToPlainText(data.Where(m => m.QuytinhhuongdanID == 7).FirstOrDefault().Description);
            this.thoigian.Value = HtmlToPlainText(data.Where(m => m.QuytinhhuongdanID == 8).FirstOrDefault().Description);
           
        } 
        [WebMethod]
        public static string UDGiolamviec(string name)
        { 
            if (name== "true")
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,1, 1);
            }
            else
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,1, 0);
            }
            return "Hello " + name;
        }
        [WebMethod]
        public static string UDChamCong(string name)
        {
            Alert.Show(UserMemberIDStatic.ToString());
            if (name == "true")
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,2, 1);
            }
            else
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,2, 0);
            }
            return "Hello " + name;
        }
        [WebMethod]
        public static string UDLamphep(string name)
        {
            Alert.Show(UserMemberIDStatic.ToString());
            if (name == "true")
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,3, 1);
            }
            else
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,3, 0);
            }
            return "Hello " + name;
        }

        [WebMethod]
        public static string UDDirangoai(string name)
        {
            Alert.Show(UserMemberIDStatic.ToString());
            if (name == "true")
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,4, 1);
            }
            else
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,4, 0);
            }
            return "Hello " + name;
        }
        [WebMethod]
        public static string UDTrangphuc(string name)
        {
            Alert.Show(UserMemberIDStatic.ToString());
            if (name == "true")
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,5, 1);
            }
            else
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,5, 0);
            }
            return "Hello " + name;
        }
        [WebMethod]
        public static string UDSinhhoat(string name)
        {
            Alert.Show(UserMemberIDStatic.ToString());
            if (name == "true")
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,6, 1);
            }
            else
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,6, 0);
            }
            return "Hello " + name;
        }
        [WebMethod]
        public static string UDTraodoi(string name)
        {
            Alert.Show(UserMemberIDStatic.ToString());
            if (name == "true")
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,7, 1);
            }
            else
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,7, 0);
            }
            return "Hello " + name;
        }
        [WebMethod]
        public static string UDThoigian(string name)
        {
            if (name == "true")
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,8, 1);
            }
            else
            {
                blc_user2.CapnhatStatusQuytrinh(UserMemberIDStatic,8, 0);
            }
            return "Hello " + name;
        }
    }
}