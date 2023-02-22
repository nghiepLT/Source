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
using System.Data.OleDb;



namespace UserMng
{
    public partial class AreaMng :  XVNET_ModuleControl
    {

        UserMngOther_BLC blc_user = new UserMngOther_BLC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ParentCtrl().PrevLoadPage != ParentCtrl().CurrentLoadPage)
            {
                if (Session["g_UserMemberType"].ToString() == "2" || Session["g_UserMemberType"].ToString() == "1")
                {
                    tb_button.Visible = true;
                    tb_input.Visible = true;
                }
                else
                {
                    tb_button.Visible = false;
                    tb_input.Visible = false;
                }
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
            IList<TArea> list = blc_user.ListArea();
            gvBanner.DataSource = list;
            gvBanner.DataBind();
        }

        private void BindInfor()
        {
            if (this.ssAreaID != -1)
            {
                TArea ent = blc_user.GetArea_ByID(this.ssAreaID,-1);
                if (ent != null)
                {
                    txt_AreaName.Text = ent.AreaName;
                    txt_Sort.Text = ent.SortOrder != null ? ent.SortOrder.ToString() : "1";
                    txt_areades.Text = ent.AreaDescription;
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
            string AreaName = txt_AreaName.Text.Trim();
            int AreaSort =  Helper.TryParseInt(txt_Sort.Text.Trim(),1);
            int intStatus = 1;

            if (this.ssAreaID == -1)
            {
                this.ssAreaID = blc_user.CreateArea(AreaName,txt_areades.Text,"",AreaSort, intStatus);
            }
            else
            {
                if (this.ssAreaID != -1)
                {
                    if (blc_user.UpdateArea(this.ssAreaID, AreaName,txt_areades.Text, "", AreaSort, intStatus) == true)
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
            if (Session["g_UserMemberType"].ToString() == "2" || Session["g_UserMemberType"].ToString() == "1")
            {
                if (e.CommandName == "DeleteItem")
                {
                    try
                    {
                        int AreaID = Helper.TryParseInt(e.CommandArgument.ToString(), 0);
                        TArea ent = blc_user.GetArea_ByID(AreaID, -1);
                        if (ent != null)
                        {
                            if (blc_user.DeleteArea(AreaID) == true)
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
                    this.ssAreaID = AreaID;
                    BindInfor();
                }
            }
            else Alert.Show("NO Action !");
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
            this.ssAreaID = -1;
            txt_AreaName.Text = string.Empty;
            txt_Sort.Text = "1";
            txt_areades.Text = string.Empty;
        }

        #region Property


        protected int ssAreaID
        {
            get
            {
                if (ViewState["g_AreaID"] != null)
                    return Convert.ToInt32(ViewState["g_AreaID"]);
                return -1;
            }
            set
            {
                ViewState["g_AreaID"] = value;
            }
        }

        private DataTable ReadDataFromExcelFile()
        {  
            string path = System.IO.Path.GetFullPath(Server.MapPath("~/Uploads/diemdoanduong_chuanimport.xlsx"));
           
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=Excel 8.0";
            // Tạo đối tượng kết nối
            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            DataTable data = null;
            try
            {

                 
                // Mở kết nối
                oledbConn.Open();

                // Tạo đối tượng OleDBCommand và query data từ sheet có tên "Sheet1"
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);

                // Tạo đối tượng OleDbDataAdapter để thực thi việc query lấy dữ liệu từ tập tin excel
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                // Tạo đối tượng DataSet để hứng dữ liệu từ tập tin excel
               DataSet ds = new DataSet();

                // Đổ đữ liệu từ tập excel vào DataSet
                oleda.Fill(ds);

               data = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Alert.Show(ex.ToString());
            }
            finally
            {
                // Đóng chuỗi kết nối
                oledbConn.Close();
            }
            Alert.Show("ok");
            return data;
        }
        private void ImportIntoDatabase(DataTable data)
        {
            if (data == null || data.Rows.Count == 0)
            {
                Alert.Show("Không có dữ liệu để import");
                return;
            }
            else
            {
                dataArearesource.TAreaDataTable adapter = new dataArearesource.TAreaDataTable();              
                // HumanResourceTableAdapters.EmployeeInfoTableAdapter adapter = new HumanResourceTableAdapters.EmployeeInfoTableAdapter();
                //  HumanResourceTableAdapters.InforEmployTableAdapter adapter2 = new HumanResourceTableAdapters.InforEmployTableAdapter();
                string AreaName = "", areades = "";

                try
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        AreaName = data.Rows[i]["Name"].ToString().Trim();
                        areades = data.Rows[i]["Poin"].ToString().Trim();


                        dataArearesource.TAreaDataTable existingEmployee = adapter.GetDataBy_NAME(AreaName);
                        TArea area1 = blc_user.GetArea_ByName(AreaName);

                        //  HumanResource.EmployeeInfoDataTable existingEmployee = adapter.GetEmployeeInfoByCode(code);
                        //  HumanResource.InforEmployDataTable existingInfoloyee = adapter2.GetDataBy(code);
                        // Nếu nhân viên chưa tồn tại trong DB thì thêm mới
                        if (existingEmployee == null || existingEmployee.Rows.Count == 0)
                        {
                            blc_user.CreateArea(AreaName, areades, "", 1, 1);
                            //  adapter.InsertEmployee(code, fullName, workingYears);
                        }
                        // Ngược lại, nhân viên đã tồn tại trong DB thì update
                        else
                        {
                            blc_user.UpdateArea(area1.TAreaID, AreaName, areades, "", 1, 1);
                            //   adapter.UpdateEmployeeInfoByCode(fullName, workingYears, code);
                        }
                        //if (existingInfoloyee == null || existingInfoloyee.Rows.Count == 0)
                        //{
                        //    adapter2.InsertQuery(code, phongban, chucvu);
                        //}
                        //// Ngược lại, nhân viên đã tồn tại trong DB thì update
                        //else
                        //{
                        //    adapter2.UpdateQuery(code, phongban, chucvu);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Alert.Show("error" + ex.ToString());
                }
                Alert.Show("Upload data success !");
                BindGird();
            }
        }
        //protected string Get_Image_Status(object p_status)
        //{
        //    if (p_status == DBNull.Value)
        //        return "";
        //    int status = Convert.ToInt32(p_status);
        //    return status == 1 ? "active.png" : "inactive.png";
        //}
        protected void Click_upload(object sender, EventArgs e)
        {
            if ( filesUpload.HasFile)
            {
                try
                {
                   
                 // DataTable data = ReadDataFromExcelFile();
                    DataTable data = GenerateExcelData("Select");
                    if (data != null)
                    // Import dữ liệu đọc được vào database
                    {
                      //  grvData.DataSource = data.DefaultView;
                      //  grvData.DataBind();
                        ImportIntoDatabase(data);
                        
                    }
                    else Alert.Show("Không có data !");
                }
                catch (Exception ex)
                {
                    Alert.Show(ex.ToString());
                }
                
            }
            else Alert.Show("chưa chọn files !");
        }
        #endregion
        OleDbConnection oledbConn;
        private DataTable GenerateExcelData(string SlnoAbbreviation)
        {
            DataTable data = null;
            try
            {
                
                string filename = Path.GetFileName(filesUpload.FileName);
                filesUpload.SaveAs(Server.MapPath("~/") + filename);
                string path = System.IO.Path.GetFullPath(Server.MapPath("~/" + filename));
                // need to pass relative path after deploying on server
                //  string path = System.IO.Path.GetFullPath(Server.MapPath("~/InformationNew.xlsx"));
                /* connection string  to work with excel file. HDR=Yes - indicates 
                   that the first row contains columnnames, not data. HDR=No - indicates 
                   the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
                   (numbers, dates, strings etc) data columns as text. 
                Note that this option might affect excel sheet write access negative. */

                if (Path.GetExtension(path) == ".xls")
                {
                    oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                }
                else if (Path.GetExtension(path) == ".xlsx")
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                }
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();

                // passing list to drop-down list

                // selecting distict list of Slno 
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT distinct([Name]) FROM [Sheet1$]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "dsdiem");
              //  ddlSlno.DataSource = ds.Tables["dsSlno"].DefaultView;
                //if (!IsPostBack)
                //{
                //    ddlSlno.DataTextField = "Name";
                //    ddlSlno.DataValueField = "Name";
                //    ddlSlno.DataBind();
                //}
                // by default we will show form data for all states but if any state is selected then show data accordingly
                if (!String.IsNullOrEmpty(SlnoAbbreviation) && SlnoAbbreviation != "Select")
                {
                    cmd.CommandText = "SELECT [Name], [Poin]" +
                        "  FROM [Sheet1$] ";
                    // cmd.Parameters.AddWithValue("@Slno_Abbreviation", SlnoAbbreviation);
                }
                else
                {
                    cmd.CommandText = "SELECT [Name], [Poin] FROM [Sheet1$]";
                }
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds);
                
                // binding form data with grid view
                data = ds.Tables[1].DataSet.Tables[1];
               
            }
            // need to catch possible exceptions
            catch (Exception ex)
            {
                 Alert.Show(ex.ToString());
            }
            finally
            {
                oledbConn.Close();
            }
            return data;
        }
    }
}