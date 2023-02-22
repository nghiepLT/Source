using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;


namespace WebCus
{
    public partial class testgetjson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }
        protected void click_check(object sender, EventArgs e)
        {
            Justintime obj = GetFacebookUserData(txt_idkey.Text.Trim());
            if (obj != null)
            {
                lbl_type.Text = obj.BLType;
                lbl_no.Text = obj.BLNo;
                lbl_pie.Text = obj.Pieces;
                lbl_gw.Text = obj.GW;
                lbl_cont.Text = obj.Conts;
                lbl_cbm.Text = obj.Cbm;

                List<histList> arr = obj.Histories.ToList();
                
                rep.DataSource = arr;
                rep.DataBind();
               
            }
        }
        protected Justintime GetFacebookUserData(string code)
        {
            try
            {

                Uri targetUserUri = new Uri("http://210.211.109.180:9999/api/bltracking/" + code);
                HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

                // Read the returned JSON object response
                StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
                string jsonResponse = string.Empty;
                jsonResponse = userInfo.ReadToEnd();

                // Deserialize and convert the JSON object to the Facebook.User object type
                JavaScriptSerializer sr = new JavaScriptSerializer();
                string jsondata = jsonResponse;
                Justintime converted = sr.Deserialize<Justintime>(jsondata);

                // Write the user data to a List
                // List<Facebook.User> currentUser = new List<Facebook.User>();
                //  currentUser.Add(converted);

                // Return the current Facebook user
                // return currentUser;
                return converted;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public string a { get; set; }
    }
}