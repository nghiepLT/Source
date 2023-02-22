using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using PQT.API;
using PQT.Controls;
using PQT.API.File;
using PQT.API.DataDefine.Sys;
using PQT.Common;
using UserMng.BLC;
using UserMng.DataDefine;
using PQT.DAC;
using NewsMng.BLC;
//using ProductMng.BLC;

namespace WebCus
{
    public partial class EditCurrentProfile : CommonPage
    {
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        Seo_BLC blc_seo = new Seo_BLC();
        News_BLC blc_news = new News_BLC();
        protected void Page_Load(object sender, EventArgs e)
        {
            /*BindSeo();*/
            if (this.UserMemberID == 0)
            {
                Response.Redirect("/dang-nhap");
            }
            if (!IsPostBack)
            {
                BindInfo();
                BindGirdView();
                BindText();
            }
        }

        private void BindText()
        {
            rdoGender_Male.Text = this.ClientLanguageMsg("lngMale");
            rdoGender_Female.Text = this.ClientLanguageMsg("lngFemale");
            RequiredFieldValidator4.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputName"));
            RequiredFieldValidator5.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputTel"));
            RequiredFieldValidator6.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputAdd"));
            RequiredFieldValidator2.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputPass"));
            RequiredFieldValidator1.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputNewPass"));
            CustomValidator1.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputconfirmPass"));
            RegularExpressionValidator5.Attributes.Add("ErrorMessage", this.ClientLanguageMsg("lngMsgInputPassErrorType"));



        }
        /*
        #region SEO
        protected string MetaTitle
        {
            get;
            set;
        }
        protected string MetaKey
        {
            get;
            set;
        }
        protected string MetaDes
        {
            get;
            set;
        }
        private void BindSeo()
        {
            TNewsCategory ent = blc_news.GetNewCategoryByUniqueKey("Banner03");
            if (ent != null)
            {
                TSeo entSeo = blc_seo.GetTSeo_By(ent.NewsCategoryID, 1);
                if (ent != null)
                {
                    TSeoDescription entDes = blc_seo.GetTSeoDescription(entSeo.SeoID, this.LangID);
                    if (entDes != null)
                    {
                        this.MetaTitle = entDes.SeoTitle;
                        this.MetaKey = entDes.SeoKeyWord;
                        this.MetaDes = entDes.SeoDescription;
                    }
                }
            }
        }
        #endregion
        */
        private void BindInfo()
        {
            TUser ent = blc_user.GetMember_ByID(this.UserMemberID);
            if (ent != null)
            {
                lblEmail.Text = ent.Email;
                lbl_tendangnhap.Text = ent.LoginID;
                txtUserName.Text = ent.UserName;
                txtTel.Text = ent.Tel;
                txtAddress.Text = ent.Address;
                //txtBirthday.SelectedDate = ent.Brithday!=null ? (DateTime)ent.Brithday : DateTime.Now;
                if (ent.Gender == 1)
                {
                    rdoGender_Male.Checked = true;
                    rdoGender_Female.Checked = false;
                }
                else
                {
                    rdoGender_Male.Checked = false;
                    rdoGender_Female.Checked = true;
                }
                
            }
        }


        private bool SaveCustomer()
        {
            try
            {
                TUser ent = blc_user.GetMember_ByID(this.UserMemberID);
                if (ent != null)
                {
                    if (rdoGender_Male.Checked == true)
                    {
                        ent.Gender = 1;
                    }
                    else
                    {
                        ent.Gender = 2;
                    }
                    ent.Brithday = DateTime.Now;//txtBirthday.SelectedDate;
                    ent.Address = txtAddress.Text;
                    ent.UserName = txtUserName.Text;
                    ent.Tel = txtTel.Text;
                    return blc_user.UpdateUser(ent);
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtVerifyCode.Text.ToUpper() == SoftGenImage.genStr)
            {
                if (SaveCustomer())
                {
                    MessageBox.Show("Bạn cập nhật thông tin thành công", false);
                }
                else
                {
                    Alert.Show("Save failed");
                }
            }
            else
            {
                Alert.Show("Mã xác nhận không đúng!");
            }
        }

        protected void btn_changepass_Click(object sender, EventArgs e)
        {
            if (txtVerifyCode02.Text.ToUpper() == SoftGenImage.genStr)
            {
                TUser ent = blc_user.GetUser_ByID_pass(this.UserMemberID, Utility.Encrypt(txt_passold.Text));
                if (ent != null)
                {
                    bool b = blc_user.ChangePassword(this.UserMemberID, txt_passnew.Text);
                    if (b == true)
                    {
                        Alert.Show("Thay đổi mật khẩu thành công!");
                        Session.Remove("g_LoginMemberID");
                        Session.Remove("g_UserMemberID");
                        Response.Redirect("/dang-nhap");
                    }
                }
            }
        }


        protected void Pager_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            this.CurrentPage = e.NewPageIndex + 1;
            BindGirdView();
        }
        protected void btnhome_Click(object sender, EventArgs e)
        {
            Response.Redirect("/trang-chu");
        }

        protected void gvList_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "SortItem")
            //{
            //    LinkButton linkSort = e.CommandSource as LinkButton;
            //    this.ActiveColumn_S = linkSort.ID;
            //    this.ActiveCss_S = linkSort.CssClass != "Active_Down" ? "Active_Down" : "Active_Up";
            //    string[] arr = e.CommandArgument.ToString().Split('-');
            //    this.SortOption_S = Convert.ToInt32(arr[0]);
            //    this.SortType_S = this.ActiveCss_S == "Active_Down" ? 1 : 2;
            //    BindGirdView();
            //}
        }


        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    LinkButton linkSort = e.Row.FindControl(this.ActiveColumn_S) as LinkButton;
            //    if (linkSort != null)
            //    {
            //        linkSort.CssClass = this.ActiveCss_S;
            //    }
            //}
        }
        private void BindGirdView()
        {
          //  int pagesize = 100;
          ////  ProductMng_BLC_NTX blc = new ProductMng_BLC_NTX();
          //  DataTable ds = blc.getOrdersByUserID(this.UserMemberID).Tables[0];
          //  if (ds.Rows.Count > 0)
          //  {
          //      gvList.DataSource = ds;
          //      gvList.DataBind();

          //      //int total = Utilis.TryParseInt(ds.Tables[1].Rows[0][0]);

          //      //lbl_Total_Count.Text = string.Format("Có {0} đơn hàng được tìm thấy", total);
          //      int total = Utilis.TryParseInt(ds.Rows.Count);

          //      PQTPager1.RecordCount = total;

          //      PQTPager1.PageSize = pagesize;
          //      PQTPager1.PagerButtonCount = 5;

          //      PQTPager1.CurrentPageIndex = this.CurrentPage - 1;
          //  }
          //  else
          //  {
          //      gvList.DataSource = null;
          //      gvList.DataBind();
          //      lblNoOrders.Visible = true;
          //      PQTPager1.RecordCount = 0;

          //  }

        }

        private int CurrentPage
        {
            get
            {
                if (ViewState["g_CurrentPage"] != null)
                    return Convert.ToInt32(ViewState["g_CurrentPage"]);
                return 1;
            }
            set
            {
                ViewState["g_CurrentPage"] = value;
            }
        }


        #region UserLogin

        protected string LoginMemberID
        {
            get
            {
                if (Session["g_LoginMemberID"] != null)
                    return Convert.ToString(Session["g_LoginMemberID"]);
                return string.Empty;
            }
            set
            {
                Session["g_LoginMemberID"] = value;
            }
        }

        protected int UserMemberID
        {
            get
            {
                if (Session["g_UserMemberID"] != null)
                    return Convert.ToInt32(Session["g_UserMemberID"]);
                return 0;
            }
            set
            {
                Session["g_UserMemberID"] = value;
            }
        }

        protected string PasswordMember
        {
            get
            {
                if (Session["g_PasswordMember"] != null)
                    return Convert.ToString(Session["g_PasswordMember"]);
                return string.Empty;
            }
            set
            {
                Session["g_PasswordMember"] = value;
            }
        }

        protected bool IsUserMember
        {
            get
            {
                if (Session["g_IsUserMember"] != null)
                    return Convert.ToBoolean(Session["g_IsUserMember"]);
                return true;
            }
            set
            {
                Session["g_IsUserMember"] = value;
            }
        }
        #endregion
    }
}
