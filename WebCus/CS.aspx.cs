using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using PQT.Common;

public partial class CS : System.Web.UI.Page
{
    [WebMethod]
    public static void Rate(int rating , int IDPro)
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO UserRatings (Rating, IDPro) VALUES(@Rating , @IDPro)"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                { 
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Rating", rating);
                    cmd.Parameters.AddWithValue("@IDPro", IDPro);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }

    [WebMethod]
    public static string GetRating(int IDPro)
    {
        string sql = "SELECT ROUND(ISNULL(CAST(SUM(Rating) AS NUMERIC(5, 2)) / COUNT(Rating), 0), 1) Average";
        sql += ", COUNT(Rating) Total FROM UserRatings where IDPro ='"+IDPro+"'";
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                string json = string.Empty;
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    json += "[ {";
                    json += string.Format("Average: {0}, Total: {1}", sdr["Average"], sdr["Total"]);
                    json += "} ]";
                    sdr.Close();
                }
                con.Close();
                return json;
            }
        }

    }
    
}