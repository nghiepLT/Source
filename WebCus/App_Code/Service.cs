using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;

/// <summary>
/// Summary description for Service_CS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService {

    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetCustomers(string prefix) 
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["constr"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Name, ProductID from TProductDescription where " +
                "Name like N'%'+ @SearchText + N'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["Name"], sdr["ProductID"]));
                    }
                }
                conn.Close();
            }
            return customers.ToArray();
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] Getproname(string prefix, int cateID)
    { 
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["constr"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
               // cmd.CommandText = @"select TD.Name, TD.ProductID from TProductDescription as TD INNER JOIN TProduct as TP ON TP.ProductID=TD.ProductID WHERE ((TP.[Status] = @Status)) and ((TD.Name like N'%'+ @SearchText + N'%')) and ((TD.Name like N'%'+ @SearchText + N'%'))";

                cmd.CommandText = ("p_TProduct_Rows @intPage, @intPageSize, @intLangID, @intStatus, @intProductID, @intSearchType, @intCategoryID, @strSearchText");
                cmd.Parameters.AddWithValue("@intPage",1); 
                cmd.Parameters.AddWithValue("@intPageSize",int.MaxValue);
                cmd.Parameters.AddWithValue("@intLangID",1);
                cmd.Parameters.AddWithValue("@intStatus",1);
                cmd.Parameters.AddWithValue("@intProductID",0);
                cmd.Parameters.AddWithValue("@intSearchType",0);
                cmd.Parameters.AddWithValue("@intCategoryID",cateID);
                cmd.Parameters.AddWithValue("@strSearchText",prefix);
                

                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}-{2}", sdr["Name"], sdr["ProductID"], sdr["Price_Text"]));
                    }
                }
                conn.Close();
            }
            return customers.ToArray();
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetUser(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["constr"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                //cmd.CommandText = "select UserName, UserID from TUser where " +
               // "UserName like N'%'+ @SearchText + N'%'";
                cmd.CommandText = "select UserName, MaNV, UserID from TUser INNER JOIN ThongTinNhanSu ON ThongTinNhanSu.IdNhanVien=TUser.IdNhansu where " +
                    "UserName like N'%'+ @SearchText + N'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}-{2}", sdr["UserName"], sdr["MaNV"], sdr["UserID"]));
                    }
                }
                conn.Close();
            }
            return customers.ToArray();
        }
    }
}
