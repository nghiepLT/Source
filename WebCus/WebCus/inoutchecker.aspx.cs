using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.DataDefine;
using UserMng.BLC;
using PQT.Common;
using PQT.API;
using PQT.API.DataDefine.Sys;
using PQT.API.File;
using System.Drawing;
using System.Net;
using System.IO;
using System.Configuration;
using System.Drawing.Imaging;

namespace WebCus
{
    public partial class inoutchecker : CommonPage
    {
        UserMng_BLC_NTX nBLC = new UserMng_BLC_NTX();
        UserMng_BLC_TX BLC = new UserMng_BLC_TX();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Action = "/inoutchecker";
                string linkcamera = ConfigurationManager.AppSettings["linkipcamera"];
                if (linkcamera != "0")
                linkcam = linkcamera;           
           // txtUserID.Attributes.Add("placeholder", this.ClientLanguageMsg("lngUser"));
          //  txtPass.Attributes.Add("placeholder", this.ClientLanguageMsg("lngPass"));
        }
        protected void LinkButton3_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string txtloginid = txtUserID.Text.Trim();
                // string pwd = txtPass.Text;
                string lydo = "";
                int status = 1;
                switch (e.CommandName)
                {
                    case "gapkhachhang":
                        lydo = "Gặp Khách Hàng";
                        status = 2;
                        break;
                    case "gapdoitac":
                        lydo = "Gặp Đối Tác";
                        status = 3;
                        break;
                    case "giaovanthu":
                        lydo = "Giao Văn Thư";
                        status = 4;
                        break;
                    case "khac":
                        lydo = txt_lydo.Text;
                        break;
                }


                if (!string.IsNullOrEmpty(txtloginid))
                {
                    UserEntity userEnt = nBLC.RowUserByMaNV(txtloginid);
                    CheckinoutEntity checkentity = new CheckinoutEntity();
                    if (userEnt != null)
                    {
                        //if (userEnt.PermissionString == "1")
                        //{
                        //    if (pwd == Utility.Decrypt(userEnt.Password))
                        //    {
                        //  this.LoginMemberID = txtloginid;
                        //  this.UserMemberID = userEnt.UserID;
                        //  this.PasswordMember = userEnt.Password;//Utility.Decrypt(userEnt.Password);
                        // string redirect_url = Request.Params["url"] == "checkout" ? "/trang-chu" : "/mua-hang";
                        //   Session["g_UserMemberID"] = userEnt.UserID;

                        // Response.Redirect(redirect_url);
                        //    }
                        //    else
                        //    {
                        //        this.LoginMemberID = null;
                        //        lblAlert.Text = this.ClientLanguageMsg("lngWrongPass");
                        //        lblAlert.Visible = true;
                        //    }
                        //}
                        //else
                        //{
                        //    this.LoginMemberID = null;
                        //    lblAlert.Text = this.ClientLanguageMsg("lngUserNotReg");
                        //    lblAlert.Visible = true;
                        //}
                        checkentity.BarCodeUser = txtloginid;
                        checkentity.DateCheck = DateTime.Now;
                        checkentity.IDuser = userEnt.UserID;
                        checkentity.LyDoCheck = lydo;
                        checkentity.NameUser = userEnt.UserName;
                        checkentity.Status = status;
                        checkentity.TimesIn = ""; //DateTime.Now.ToLongTimeString();// txt_totimes.Text;// đến
                        checkentity.TimesOut = DateTime.Now.ToLongTimeString(); //txt_fromtimes.Text;   // từ                 
                        string file_name = Server.MapPath(".") + "\\img_take_camera\\" + txtloginid + "_" + DateTime.Now.ToString().Replace(":", "_").Replace(".", "_").Replace("/", "_").Replace("-", "_").Replace(" ", "_") + ".JPG";
                        // Alert.Show(imgtake.ImageUrl);                        
                       // SaveImage(file_name, ImageFormat.Png, linkcamera);
                        checkentity.Imgcheck = file_name;
                        string linkcamera = ConfigurationManager.AppSettings["linkipcamera"];
                        if (linkcamera != "0")
                        save_file_from_url(file_name, linkcamera);
                        if (BLC.AddCheckInOut(checkentity))
                        {

                            lblAlert.Text = "Nhân Viên :  " + userEnt.UserName + " - Đã Xác Nhận !";
                            lblAlert.ForeColor = Color.Green;
                            lblAlert.Visible = true;
                            txtUserID.Text = string.Empty;
                            txt_idcheckvao.Text = string.Empty;
                            txt_lydo.Text = string.Empty;
                        }
                        else
                        {
                            this.LoginMemberID = null;
                            lblAlert.Text = "Bạn Chưa Check Vào, Vui lòng check vào trước Khi tạo lượt mới !";
                            lblAlert.Visible = true;
                            lblAlert.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        this.LoginMemberID = null;
                        lblAlert.Text = "Mã Nhân viên Không Đúng !";
                        lblAlert.Visible = true;
                        lblAlert.ForeColor = Color.Red;                        
                    }
                }
                else
                {
                    lblAlert.Text = "Nhập Mã Nhân Viên !";
                    lblAlert.Visible = true;
                    lblAlert.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error!!");
            }
        }        
        protected void btn_takepicture_Click(object sender, EventArgs e)
        {

            try
            {
                string txtloginid = txt_idcheckvao.Text.Trim();
                //string file_name = Server.MapPath(".") + "\\img_take_camera\\camerasnapshot.jpg";
                //save_file_from_url(file_name, "http://192.168.117.208/cgi-bin/snapshot.cgi");
                string file_name = Server.MapPath(".") + "\\img_take_camera\\" + txtloginid + "_" + DateTime.Now.ToString().Replace(":", "_").Replace(".", "_").Replace("/", "_").Replace("-", "_").Replace(" ", "_") + ".JPG";
                // Alert.Show(imgtake.ImageUrl);                        
                // SaveImage(file_name, ImageFormat.Png, linkcamera);                
                string linkcamera = ConfigurationManager.AppSettings["linkipcamera"];
                if (linkcamera != "0")
                save_file_from_url(file_name, linkcamera);
                string sss = BLC.UpdateCheckInOut(txtloginid, DateTime.Now.ToString(), file_name);
              if (sss != "0")
                {
                    lblAlert.Text = " Đã Xác Nhận !";
                    lblAlert.ForeColor = Color.Green;
                    lblAlert.Visible = true;
                    txtUserID.Text = string.Empty;
                    txt_idcheckvao.Text = string.Empty;
                    txt_lydo.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error!!");
            }

        }
        public void SaveImage(string path_filename, ImageFormat format, string imageUrl)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
            {
                bitmap.Save(path_filename, format);
            }

            stream.Flush();
            stream.Close();
            client.Dispose();
        }

        public void save_file_from_url(string file_name, string url)
        {
            byte[] content;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                content = br.ReadBytes(500000);
                br.Close();
            }
            response.Close();

            FileStream fs = new FileStream(file_name, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(content);
            }
            finally
            {
                fs.Close();
                bw.Close();
            }
        }
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
        protected string linkcam
        {
            get
            {
                if (Session["linkcam"] != null)
                    return Convert.ToString(Session["linkcam"]);
                return "0";
            }
            set
            {
                Session["linkcam"] = value;
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
        
    }
}