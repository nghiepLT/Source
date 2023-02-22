using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.Drawing;


namespace WebCus
{
    public partial class baotri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getvalue();
        }

        protected void btn_updatabaotri_Click(object sender, EventArgs e)
        {
            string valuebt = "1";
            if (btn_updatabaotri.Text == "OFF")
            {
                valuebt = "0";
            }
            else
            {
                valuebt = "1";
                

            }
            AddOrUpdateAppSettings("baotri", valuebt);
        }
        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                string oldValue = config.AppSettings.Settings[key].Value;
                config.AppSettings.Settings[key].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                Response.Redirect("/login.aspx");
                
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
        public void getvalue()
        {

           // lal_tile.Text = ConfigurationManager.AppSettings["baotri"] == "0" ? "Mode = Using" : "Mode = Update";
           // lal_tile.ForeColor = Color.Red;
            if (ConfigurationManager.AppSettings["baotri"] == "1")
            {
                btn_updatabaotri.Text = "OFF";
                btn_updatabaotri.BackColor = Color.Red;
                btn_updatabaotri.ForeColor = Color.White;
            }
            else { btn_updatabaotri.Text = "ON MODE UPDATE";
            btn_updatabaotri.BackColor = Color.Green;
            btn_updatabaotri.ForeColor = Color.White;
            }
        }
      }
}