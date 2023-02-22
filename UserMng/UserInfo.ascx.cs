using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using UserMng.BLC;
using UserMng.DataDefine;
using PQT.API;
using PQT.Common;
using PQT.DAC;
using System.Collections.Generic;
using PQT.Controls;

namespace UserMng
{
    public partial class UserInfo : XVNET_ModuleControl
    {

        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC blc_user = new UserMng_BLC();
        UserMngOther_BLC blcU = new UserMngOther_BLC();
        PagedDataSource pgsource = new PagedDataSource();
        int paretnid = 0;
        int findex, lindex;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                dr_dspb.Visible = false;
                txtSearch.Visible = false;
                BindGridUser();
                ResetControl();
                if (this.MRI.IsDataAdmin)
                    ddlUserType.Items.RemoveAt(0);
                //  BindUserInfo();
                // BindPhongBan(Utils.TryParseInt(dr_cty.SelectedValue, 0));
                trUserType.Visible = this.MRI.IsAdmin || this.MRI.IsDataAdmin;
                tbttkt.Visible = false;
                BindDSNhanVien();
                tr_parentuser.Visible = false;

            }

            InitClientScript();

        }

        #region MainMethods

        private void BindPhongBan(int IDCTY)
        {
            dr_parent.Items.Clear();
            IList<PhongBan> phong = blcU.ListPhongban_byCTY(IDCTY);
            dr_parent.DataSource = phong;
            dr_parent.DataTextField = "TenPhong";
            dr_parent.DataValueField = "IDPhong";
            dr_parent.DataBind();
            dr_parent.AppendDataBoundItems = true;
            dr_parent.Items.Insert(0, new ListItem("--Chọn Phòng Ban--", "-1"));
            dr_parent.SelectedIndex = 0;
        }


        private void BindDSNhanVien()
        {
            dr_dsnhanvien.Items.Clear();
            IList<NhanVien> dsnv = blcU.ListNhanvien();
            dr_dsnhanvien.DataSource = dsnv;
            dr_dsnhanvien.DataTextField = "HoTen";
            dr_dsnhanvien.DataValueField = "IdNhanVien";
            dr_dsnhanvien.DataBind();
            dr_dsnhanvien.AppendDataBoundItems = true;
            dr_dsnhanvien.Items.Insert(0, new ListItem("--Chọn Nhân Sự--", "-1"));
            dr_dsnhanvien.SelectedIndex = 0;
        }

        private void BindGridUser()
        {
           // Alert.Show(txtSearch.Text.Trim());
            int PageInt = 15;

            DataTable dt = blc_user.RowsUser_byType(this.MRI.IsAdmin == true ? 1 : 2, this.CurrentPage, int.MaxValue, txtSearch.Text.Trim().ToUpper());
            System.Data.DataView dv = new System.Data.DataView(dt);
            pgsource.DataSource = dv;
            pgsource.AllowPaging = true;
            pgsource.PageSize = PageInt;
            pgsource.CurrentPageIndex = PageNumber;
            ViewState["totpage"] = pgsource.PageCount;
            lnkPrevious.Enabled = !pgsource.IsFirstPage;
            lnkNext.Enabled = !pgsource.IsLastPage;
            lnkFirst.Enabled = !pgsource.IsFirstPage;
            lnkLast.Enabled = !pgsource.IsLastPage;
            gvUser.DataSource = pgsource;
            gvUser.DataBind();
            doPaging();

        }
        private void doPaging()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            findex = PageNumber - 5;
            if (PageNumber > 5)
            {
                lindex = PageNumber + 5;
            }
            else
            {
                lindex = 10;
            }
            if (lindex > Convert.ToInt32(ViewState["totpage"]))
            {
                lindex = Convert.ToInt32(ViewState["totpage"]);
                findex = lindex - 10;
            }

            if (findex < 0)
            {
                findex = 0;
            }
            for (int i = findex; i < lindex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPages.DataSource = dt;

            rptPages.DataBind();

            if (dt.Rows.Count > 1)

                pager_div.Visible = true;

            else
                pager_div.Visible = false;



        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {

            PageNumber -= 1;

            BindGridUser();
        }
        protected void lnkFirst_Click(object sender, EventArgs e)
        {

            PageNumber = 0;
            BindGridUser();
        }
        protected void lnkLast_Click(object sender, EventArgs e)
        {

            PageNumber = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindGridUser();
        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {

            PageNumber += 1;


            BindGridUser();
        }
        protected void rptPages_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {

            PageNumber = Convert.ToInt32(e.CommandArgument);

            BindGridUser();

        }
        protected void RepeaterPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lnkPage = (LinkButton)e.Item.FindControl("btnPage");
            if (lnkPage.CommandArgument.ToString() == PageNumber.ToString())
            {
                lnkPage.Enabled = false;
                lnkPage.BackColor = System.Drawing.Color.FromName("#FFCC01");
            }
        }
        private void BindUserInfo(int iduser)
        {
            try
            {
                tbttkt.Visible = true;

                UserMng_BLC_NTX blc = new UserMng_BLC_NTX();
                UserEntity reEnt = blc.RowUser(iduser);
                if (reEnt != null)
                {


                    txtLoginID.Text = reEnt.LoginID;
                    txtUsername.Text = reEnt.UserName;
                    string pwd = Utility.Decrypt(reEnt.Password);
                    txtPassword.Attributes.Add("value", pwd);
                    txtConfirmPassword.Attributes.Add("value", pwd);
                    if (MRI.IsAdmin)
                    {
                    //    Alert.Show(pwd);
                        txtPassword.TextMode =TextBoxMode.SingleLine;
                        txtConfirmPassword.TextMode = TextBoxMode.SingleLine;
                    }
                    txtAddress.Text = reEnt.Address;
                    txtTel.Text = reEnt.Tel;
                    txtFax.Text = reEnt.Fax;
                    rdoIsExpireY.Checked = reEnt.IsExpire;
                    rdoIsExpireN.Checked = !reEnt.IsExpire;
                    txtExpireDate.Text = reEnt.ExpireDate.ToString("MM-dd-yyyy");
                    txtEmail.Text = reEnt.Email;
                    txtRemark.Text = reEnt.Remark;
                    ddlUserType.SelectedValue = reEnt.UserType.ToString();
                    rdoActiveYes.Checked = reEnt.PermissionString == "1";
                    rdoActiveNo.Checked = reEnt.PermissionString != "1";
                    dr_cty.SelectedValue = reEnt.UserLike.ToString();
                    if (Utils.TryParseInt(reEnt.IdNhansu, 0) != 0)
                    { dr_dsnhanvien.SelectedValue = reEnt.IdNhansu.ToString(); }
                    else dr_dsnhanvien.SelectedIndex = 0;
                    // BindPhongBan(reEnt.UserLike);
                    //BindPhongBan(Utils.TryParseInt(dr_cty.SelectedValue, 0));
                   
                   
                    //  else tr_parentuser.Visible = false;

                    if (reEnt.UserType == 1 || reEnt.UserType == 2)
                    {
                        tr_parentuser.Visible = false;
                        tr_cty.Visible = false;
                        tr_lkns.Visible = false;
                    }
                    if (reEnt.UserType == 3)
                    {
                        tr_parentuser.Visible = false;
                        tr_cty.Visible = true;
                        tr_lkns.Visible = false;
                    }
                    BindPhongBan(reEnt.UserLike);
                    if (reEnt.Parentid != 0)
                    {
                        tr_parentuser.Visible = true;
                        dr_parent.SelectedValue = reEnt.Parentid.ToString();

                    }
                    else
                    {
                        tr_parentuser.Visible = false;
                        dr_parent.SelectedIndex = 0;
                    }
                }
                else Alert.Show("NO DATA !");
                // txtExpireDate.Text = DateTime.Now.AddMonths(1).ToString("MM-dd-yyyy");
            }
            catch (System.Exception ex)
            {
                Alert.Show(ex.Message);
            }

        }

        #endregion

        #region Other

        private void InitClientScript()
        {
            btnDelete.OnClientClick = "return confirm('Do you want delete!');";
            btnSave.OnClientClick = "return CheckValidate();";
        }

        private void ResetControl()
        {
            this.UserID = 0;
            txtLoginID.ReadOnly = false;
            txtLoginID.Text = string.Empty;
            txtPassword.Attributes.Add("value", string.Empty);
            txtConfirmPassword.Attributes.Add("value", string.Empty);
            //comUserType.SelectedIndex = 2;
            txtLoginID.Focus();
            txtEmail.Text = string.Empty;
        }

        #endregion

        #region Utils

        protected UserControl ParentCtrl()
        {
            Control objParent = Parent;
            while (!(objParent is UserControl))
            {
                objParent = objParent.Parent;
            }

            return objParent as UserControl;
        }

        #endregion

        #region Events

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.UserID = Convert.ToInt32(e.CommandArgument);
            txtLoginID.ReadOnly = true;
            BindUserInfo(Convert.ToInt32(e.CommandArgument));
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ResetControl();
            tbttkt.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UserEntity userEnt = null;
                if (this.UserID > 0)
                {
                    userEnt = nBLC.RowUser(this.UserID);
                }
                else
                {
                    userEnt = nBLC.RowUserByLoginID(txtLoginID.Text);
                    if (userEnt == null)
                    {
                        userEnt = new UserEntity();
                        userEnt.RegUser = this.UserID;
                    }
                    else
                    {
                        Alert.Show("LoginID is exists");
                        return;
                    }
                }

                userEnt.UserID = this.UserID;
                userEnt.LoginID = txtLoginID.Text;
                userEnt.UserName = txtUsername.Text;
                userEnt.Password = Utility.Encrypt(txtPassword.Text);
                userEnt.Email = txtEmail.Text;
                userEnt.Fax = txtFax.Text;
                userEnt.ModifyUser = this.UserID;
                userEnt.Tel = txtTel.Text;
                userEnt.Remark = txtRemark.Text;
                userEnt.IsExpire = rdoIsExpireY.Checked;
                userEnt.ExpireDate = DateTime.Now.AddYears(1);
                userEnt.Address = txtAddress.Text;
                userEnt.PermissionString = rdoActiveYes.Checked ? "1" : "0";
                userEnt.UserType = Convert.ToInt32(ddlUserType.SelectedValue);
                userEnt.CompanyName = "";
                userEnt.IdNhansu = Utils.TryParseInt(dr_dsnhanvien.SelectedValue, 0);
                if (Convert.ToInt32(ddlUserType.SelectedValue) == 1 || Convert.ToInt32(ddlUserType.SelectedValue) == 2)
                {
                    userEnt.Parentid = 0;
                    userEnt.UserLike = 0;
                }
                else
                {
                    userEnt.Parentid = Utils.TryParseInt(dr_parent.SelectedValue, 0);
                    userEnt.UserLike = Utils.TryParseInt(dr_cty.SelectedValue, 0);
                }
                if (tBLC.AddUser(userEnt))
                {
                   IList<NgayPhep> listngayphep = blc_user.RowsNgayPhep_ByIDUser(this.UserID);
                    foreach (var item in listngayphep)
                {
                    NgayPhep ngayp = blcU.GetNgayPhep_ByID(item.IDPhep);
                    ngayp.IDCty=Utils.TryParseInt(dr_cty.SelectedValue,0);
                    ngayp.IDBGD = blcU.GetIDBGD_byIDCTY(Utils.TryParseInt(dr_cty.SelectedValue, 0));
                    ngayp.IDTruongBP = blcU.GetIDTruongPhong_byIDPhong(Utils.TryParseInt(dr_parent.SelectedValue, 0));
                    ngayp.IDPhoPhong = blcU.GetIDPhoPhong_byIDPhong(Utils.TryParseInt(dr_parent.SelectedValue, 0));
                    ngayp.IDTruongNhom = blcU.GetIDTruongNhom_byIDPhong(Utils.TryParseInt(dr_parent.SelectedValue, 0), Utils.TryParseInt(ngayp.IDNhanvien, 0));                    
                    blc_user.Update_UseNghiPhep(ngayp);
                }
                    
                    BindGridUser();                  
                    Alert.Show("Lưu thành công");
                    tbttkt.Visible = false;
                }
                else Alert.Show("Error !");
            }
            catch (System.Exception ex)
            {
                Alert.Show(string.Format("Lưu thất bại: {0}", ex.Message));
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
                tBLC.DeleteUser(this.UserID);
                Alert.Show("Delete succecced");
                BindGridUser();
                ResetControl();
                tbttkt.Visible = false;
            }
            catch (System.Exception ex)
            {
                Alert.Show("Xóa thất bại" + ex.Message);

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridUser();
        }

        protected void comSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridUser();
        }

        #endregion

        #region Property
        public string PageSize
        {
            get;
            set;
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
        protected int UserID
        {
            get
            {
                if (ViewState["g_UserID"] != null)
                    return Convert.ToInt32(ViewState["g_UserID"]);
                return 0;
            }
            set
            {
                ViewState["g_UserID"] = value;
            }
        }
        public int PageNumber
        {

            get
            {

                if (ViewState["PageNumber"] != null)

                    return Convert.ToInt32(ViewState["PageNumber"]);

                else

                    return 0;

            }

            set
            {

                ViewState["PageNumber"] = value;

            }

        }
        #endregion

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlUserType.SelectedValue == "4" || ddlUserType.SelectedValue == "5" || ddlUserType.SelectedValue == "6" || ddlUserType.SelectedValue == "7")
            {

                tr_parentuser.Visible = true;
                tr_cty.Visible = true;
            }
            else
                if (ddlUserType.SelectedValue == "2" || ddlUserType.SelectedValue == "1")
                {

                    tr_parentuser.Visible = false;
                    tr_cty.Visible = false;
                }
            if (ddlUserType.SelectedValue == "3")
            {
                tr_parentuser.Visible = false;
                tr_cty.Visible = true;
            }

        }
        protected void dr_cty_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindPhongBan(Utils.TryParseInt(dr_cty.SelectedValue, 0));
            ddlUserType.SelectedValue = ddlUserType.SelectedValue;


        }

        protected void dr_dsnhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            NhanVien nv = blcU.GetNhanvien_byID(Utils.TryParseInt(dr_dsnhanvien.SelectedValue, 0));
            if (nv != null)
            {
                txtLoginID.Text = refullname(nv.HoTen);
                txtUsername.Text = nv.HoTen;
                string pwd = "1";
                 txtPassword.Attributes.Add("value", pwd);
                 txtConfirmPassword.Attributes.Add("value", pwd);
                txtAddress.Text = nv.DCThuongTru;
                txtTel.Text = nv.SoDt;
                //txtFax.Text = reEnt.Fax;             

                txtEmail.Text = nv.Email;
            }
        }
        private static readonly string[] VietnameseSigns = new string[]

    {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

    };



        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }
        public static string refullname(string fullname)
        {

            //string[] names = fullName.Split(' ');
            //string name = names.First();
            //string midlename = names.First() ;
            //string lasName = names.Last();
            //Alert.Show("Tên :" + lasName + "_Họ" + name+ "_Đệm" + midlename);

            string fistchar = "";
            string name = fullname.Substring(fullname.LastIndexOf(' ') + 1);
            // string firstname = fullName.Substring(0,fullName.IndexOf(' '));
            // string midlename = fullName.Substring(fullName.IndexOf(' '),fullName.Length-name.Length);
            //   string midlename = fullName.Remove(fullName.Length - name.Length);
            string[] names = fullname.Split(' ');
            for (int i = 0; i < names.Length - 1; i++)
            {
                fistchar += names[i].Substring(0, 1);
            }
            //fullName = fullName.Remove(fullName.Length - lastid.Length);

            return RemoveSign4VietnameseString(name).ToLower() + fistchar.ToLower();
        }
        protected void dr_dspb_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridUser();
        }
        protected void dropSearchtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropSearchtype.SelectedValue == "1")
            {
                dr_dspb.Visible = true;
                txtSearch.Visible = false;
            }
            if (dropSearchtype.SelectedValue == "2")
            {
                txtSearch.Visible = true;
                dr_dspb.Visible = false;
            }
        }
        public string GetPhongban(object idPhongban)
        {
            int idLoai = Utils.TryParseInt(idPhongban.ToString(), 0);
            PhongBan p = blcU.GetPhongBan_ByID(idLoai);
            if(p !=null){
                if(p.ParenID==0)
                return p.TenPhong;
                else return p.TenPhong + "</br>" + blcU.GetPhongBan_ByID(Utils.TryParseInt(p.ParenID,0)).TenPhong;
            }
           else return "";
        }
    }
}