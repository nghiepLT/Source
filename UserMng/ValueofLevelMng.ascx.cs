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
using PQT.API;
using UserMng.BLC;
using System.Collections.Generic;
using PQT.DAC;
using PQT.Common;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;


namespace UserMng
{
    public partial class ValueofLevelMng : XVNET_ModuleControl
    {

        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {                
                BindGird();   
            }
        }

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

#region bind

        private void BindGird()
        {
            IList<TValueofLevel> list = blc_user.ListValueofLevel();
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }

        private void BindInfor()
        {
            if (this.ssLevelID != -1)
            {
                TValueofLevel ent = blc_user.GetLevelvalue_ByID(this.ssLevelID);
                if (ent != null)
                {
                    txt_levelName.Text = ent.LevelName;
                    txt_Sort.Text = ent.SortOrder != null ? ent.SortOrder.ToString() : "1";
                    txt_valueoflevel.Text = ent.ValueOfLevel.ToString();
                    //txtKeyword.Text = ent.TypeKeyword;
                    //txt_keywordMap.Text = ent.Keyword;
                    //ddlStatus.SelectedValue = ent.status != null ? ent.status.ToString() : "1";
                }
            }
        }
#endregion

#region Create Update
        private void CreateUpdateType()
        {
            string lelvelName = txt_levelName.Text.Trim();
            int levelSort =  Helper.TryParseInt(txt_Sort.Text.Trim(),1);
            string valueoflevel = txt_valueoflevel.Text.Trim();
            string idlevel = ConvertUrlText(lelvelName.Trim());

            if (this.ssLevelID == -1)
            {
                this.ssLevelID = blc_user.CreateLevelvalue(lelvelName, valueoflevel, levelSort, idlevel);
            }
            else
            {
                if (this.ssLevelID != -1)
                {
                    if (blc_user.UpdateValueofLevel(this.ssLevelID, lelvelName, valueoflevel, levelSort, idlevel) == true)
                    {
                        Alert.Show("Cập nhật thành công!");
                    }
                    else
                    {
                        Alert.Show("Error!");
                    }
                }
            }
            resetfield();
        }
#endregion

        #region Events

        protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                try
                {
                    int AreaID = Helper.TryParseInt(e.CommandArgument.ToString(),0);
                    TValueofLevel ent = blc_user.GetLevelvalue_ByID(AreaID);
                    if(ent!=null)
                    {
                        if (blc_user.DeleteValueOflevel(AreaID) == true)
                        {
                            Alert.Show("Xóa thành công!");
                        }
                    }
                    BindGird();
                    resetfield();
                }
                catch
                {
                    Alert.Show("Xóa lỗi!");
                }
            }
            if (e.CommandName == "EditItem")
            {
                int AreaID = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                this.ssLevelID = AreaID;
                BindInfor();
            }
        }

        protected void btnInsertBanner_Click(object sender, EventArgs e)
        {
            resetfield();
        }

        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            CreateUpdateType();
            BindGird();
            BindInfor();
        }

        protected void btnDeleteBanner_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
                Alert.Show("Xóa lỗi!");
            }
        }

        #endregion

        private void resetfield()
        {
            this.ssLevelID = -1;
            txt_valueoflevel.Text = string.Empty;
            txt_Sort.Text = "1";
            txt_levelName.Text = string.Empty;
        }

        #region Property


        protected int ssLevelID
        {
            get
            {
                if (ViewState["g_LevelID"] != null)
                    return Convert.ToInt32(ViewState["g_LevelID"]);
                return -1;
            }
            set
            {
                ViewState["g_LevelID"] = value;
            }
        }


        //protected string Get_Image_Status(object p_status)
        //{
        //    if (p_status == DBNull.Value)
        //        return "";
        //    int status = Convert.ToInt32(p_status);
        //    return status == 1 ? "active.png" : "inactive.png";
        //}

        #endregion
        public static string StripDiacritics(object accented)
        {
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = accented.ToString().Normalize(NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static string ConvertUrlText(object p_urlText)
        {
            string urlText = StripDiacritics(p_urlText);
            Regex purifyUrlRegex = new Regex("[^-a-zA-Z0-9_ ]");
            Regex dashesRegex = new Regex("[-_ ]+", RegexOptions.Compiled);

            urlText = purifyUrlRegex.Replace(urlText, "");
            urlText = urlText.Trim();
            urlText = dashesRegex.Replace(urlText, "-");
            return urlText;

        }
    }
}