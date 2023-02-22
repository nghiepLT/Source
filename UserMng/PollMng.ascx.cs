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

namespace UserMng
{
    public partial class PollMng : XVNET_ModuleControl
    {

        UserMng_BLC_TX tBLC = new UserMng_BLC_TX();
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                BindGridPoll();
                BindGridPollOption();
                ResetPoll();
            }

        }

        #region MainMethods

        private void BindGridPoll()
        {
            UserMng_BLC_NTX blc = new UserMng_BLC_NTX();
            DataTable dt = blc.RowsPolls();
            if (dt.Rows.Count < 5)
            {
                for (int i = dt.Rows.Count; i < 5; i++)
                {
                    DataRow drNew = dt.NewRow();
                    dt.Rows.Add(drNew);
                }
            }
            gvPoll.DataSource = dt;
            gvPoll.DataBind();
        }

        private void BindGridPollOption()
        {
            UserMng_BLC_NTX blc = new UserMng_BLC_NTX();
            DataTable dt = blc.RowsPollOptionsByPollID(this.PollID);
            if (dt.Rows.Count < 5)
            {
                for (int i = dt.Rows.Count; i < 5; i++)
                {
                    DataRow drNew = dt.NewRow();
                    dt.Rows.Add(drNew);
                }
            }
            gvPollOption.DataSource = dt;
            gvPollOption.DataBind();
        }

        private void BindPollInfo()
        {
            PollsEntity ent = nBLC.RowPolls(this.PollID);
            if (ent!=null)
            {
                lblPollID.Text = ent.PollID.ToString();
                txtQuestion.Text = ent.Question;
                rdoPollActive.Checked = ent.Active;
                rdoPollUnActive.Checked = !ent.Active;
                BindGridPollOption();
            }
        }


        private void BindPollOptionInfo()
        {
            PollOptionsEntity ent = nBLC.RowPollOptions(this.PollID, this.PollOptionID);
            if (ent != null)
            {
                lblPollOptionID.Text = ent.PollOptionID.ToString();
                txtAnswer.Text = ent.Answer;
                txtVote.Text = ent.Votes.ToString();
            }
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

        private void ResetPoll()
        {
            this.PollID = 0;
            txtQuestion.Text = string.Empty;
            lblPollID.Text = string.Empty;
            ResetPollOption();
        }

        private void ResetPollOption()
        {
            this.PollOptionID = 0;
            txtAnswer.Text = string.Empty;
            lblPollOptionID.Text = string.Empty;
            txtVote.Text = "10";
        }

        #endregion

        #region Events

        protected void gvPoll_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.PollID = Convert.ToInt32(e.CommandArgument);
            BindPollInfo();
            ResetPollOption();
        }

        protected void btnInsertPoll_Click(object sender, EventArgs e)
        {
            ResetPoll();
        }

        protected void btnSavePoll_Click(object sender, EventArgs e)
        {
            try
            {
                PollsEntity ent = new PollsEntity();
                ent.Active = rdoPollActive.Checked;
                ent.PollID = this.PollID;
                ent.Question = txtQuestion.Text;

                tBLC.AddPolls(ent);

                BindGridPoll();
                ResetPoll();

            }
            catch 
            {
                Alert.Show("Error");
            }

        }

        protected void btnDeletePoll_Click(object sender, EventArgs e)
        {
            try
            {
                tBLC.DeletePollOptions(this.PollID, -1);
                tBLC.DeletePolls(this.PollID);
                ResetPoll();
                BindGridPoll();
                BindGridPollOption();
            }
            catch
            {
                Alert.Show("Xóa thất bại!");
            }
        }


        protected void btnInsertPollOption_Click(object sender, EventArgs e)
        {
            ResetPollOption();
        }

        protected void btnSavePollOption_Click(object sender, EventArgs e)
        {
            try
            {
                PollOptionsEntity ent = new PollOptionsEntity();
                ent.Answer = txtAnswer.Text;
                ent.PollID = this.PollID;
                ent.PollOptionID = this.PollOptionID;
                ent.Votes = Convert.ToInt32(txtVote.Text);

                tBLC.AddPollOptions(ent);
                ResetPollOption();
                BindGridPollOption();
            }
            catch (System.Exception ex)
            {
                Alert.Show(string.Format("Lưu thất bại: {0}", ex.Message));
            }
        }

        protected void btnDeletePollOption_Click(object sender, EventArgs e)
        {
            try
            {
                tBLC.DeletePollOptions(this.PollID, this.PollOptionID);
                BindGridPollOption();
                ResetPollOption();
            }
            catch (System.Exception ex)
            {
                Alert.Show("Xóa thất bại");

            }
        }

        protected void gvPollOption_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.PollOptionID= Convert.ToInt32(e.CommandArgument);
            BindPollOptionInfo();
        }

        #endregion

        #region Property

        protected int PollID
        {
            get
            {
                if (ViewState["g_PollID"] != null)
                    return Convert.ToInt32(ViewState["g_PollID"]);
                return 0;
            }
            set
            {
                ViewState["g_PollID"] = value;
            }
        }

        protected int PollOptionID
        {
            get
            {
                if (ViewState["g_PollOptionID"] != null)
                    return Convert.ToInt32(ViewState["g_PollOptionID"]);
                return 0;
            }
            set
            {
                ViewState["g_PollOptionID"] = value;
            }
        }

        #endregion

    }
}