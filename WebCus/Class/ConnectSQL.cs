using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PQT.API;
using PQT.Common;
using System.Configuration;

namespace WebCus
{
    public class ConnectSQL
    {
        public DataSet connect(string sql)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            string firstSql = null;

            connetionString = ConfigurationManager.ConnectionStrings["PQT"].ConnectionString;
            firstSql = sql; // "p_TSallingPharmacy_Report12_DoctorSale 0";
            connection = new SqlConnection(connetionString);
            command = new SqlCommand(firstSql, connection);

            try
            {
                connection.Open();
                //connection.ConnectionTimeout = 3600;
                command.CommandTimeout = 3600;
                adapter.SelectCommand = command;
                adapter.Fill(ds);

                adapter.Dispose();
                command.Dispose();
                connection.Close();

                return ds;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                adapter.Dispose();
                command.Dispose();

                if (connection.State == ConnectionState.Connecting || connection.State == ConnectionState.Open)
                    connection.Close();
            }

            //return ds;
        }

        public bool InsertDbByDataTable(DataTable tb, String tableName)
        {

            DataSet ds = new DataSet();
            string connetionString = Config.GetConfigValue("connectionString");
            //String sql = "select * from " + tableName;
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connection))
                {
                    //Set destination table name
                    //to table previously created.
                    //bulkcopy.ColumnMappings
                    bulkcopy.DestinationTableName = tableName;
                    switch (tableName.Trim())
                    {
                        case "Temp_Salling_AgentSoft":
                            {
                                bulkcopy.ColumnMappings.Add("NUM", "NUM");
                                bulkcopy.ColumnMappings.Add("PharSalesID", "PharSalesID");
                                bulkcopy.ColumnMappings.Add("InvoiceNumber", "InvoiceNumber");
                                bulkcopy.ColumnMappings.Add("InvoiceDate", "InvoiceDate");
                                bulkcopy.ColumnMappings.Add("SectorID", "SectorID");
                                bulkcopy.ColumnMappings.Add("ProductID", "ProductID");
                                bulkcopy.ColumnMappings.Add("AgentID", "AgentID");
                                bulkcopy.ColumnMappings.Add("PatientName", "PatientName");
                                bulkcopy.ColumnMappings.Add("Quantity", "Quantity");
                                bulkcopy.ColumnMappings.Add("Price", "Price");
                                bulkcopy.ColumnMappings.Add("STD_Price", "STD_Price");
                                bulkcopy.ColumnMappings.Add("CIF_Price", "CIF_Price");
                                bulkcopy.ColumnMappings.Add("Sample", "Sample");
                                bulkcopy.ColumnMappings.Add("Lot", "Lot");
                                bulkcopy.ColumnMappings.Add("Status", "Status");
                                bulkcopy.ColumnMappings.Add("SalesType", "SalesType");
                                bulkcopy.ColumnMappings.Add("ResonReturn", "ResonReturn");
                                bulkcopy.ColumnMappings.Add("Address", "Address");
                                bulkcopy.ColumnMappings.Add("SubTerID", "SubTerID");
                                bulkcopy.ColumnMappings.Add("TerID", "TerID");
                                bulkcopy.ColumnMappings.Add("PromotionCampaign", "PromotionCampaign");
                                bulkcopy.ColumnMappings.Add("Fromdate", "Fromdate");
                                bulkcopy.ColumnMappings.Add("ToDate", "ToDate");
                                bulkcopy.ColumnMappings.Add("VoucherCode", "VoucherCode");
                                bulkcopy.ColumnMappings.Add("Notes", "Notes");
                                bulkcopy.ColumnMappings.Add("C_CustomerID", "C_CustomerID");
                                bulkcopy.ColumnMappings.Add("C_CustomerName", "C_CustomerName");
                                bulkcopy.ColumnMappings.Add("C_ParentID", "C_ParentID");
                                bulkcopy.ColumnMappings.Add("C_CustomerTypeID", "C_CustomerTypeID");
                                bulkcopy.ColumnMappings.Add("C_SectorID", "C_SectorID");
                                bulkcopy.ColumnMappings.Add("C_BrickID", "C_BrickID");
                                bulkcopy.ColumnMappings.Add("C_Tel", "C_Tel");
                                bulkcopy.ColumnMappings.Add("C_Fax", "C_Fax");
                                bulkcopy.ColumnMappings.Add("C_ContactName", "C_ContactName");
                                bulkcopy.ColumnMappings.Add("C_Address", "C_Address");
                                bulkcopy.ColumnMappings.Add("C_RegionID", "C_RegionID");
                                bulkcopy.ColumnMappings.Add("C_AreaID", "C_AreaID");
                                bulkcopy.ColumnMappings.Add("C_TerritoryID", "C_TerritoryID");
                                bulkcopy.ColumnMappings.Add("C_SubTerritory", "C_SubTerritory");
                                bulkcopy.ColumnMappings.Add("C_DiscountLevel", "C_DiscountLevel");
                                bulkcopy.ColumnMappings.Add("C_LocalGroupID", "C_LocalGroupID");
                                bulkcopy.ColumnMappings.Add("C_BeginDate", "C_BeginDate");
                                bulkcopy.ColumnMappings.Add("C_EndDate", "C_EndDate");
                                bulkcopy.ColumnMappings.Add("C_Code_Servier", "C_Code_Servier");
                                bulkcopy.ColumnMappings.Add("CustomerCode", "CustomerCode");
                                bulkcopy.ColumnMappings.Add("C_CustOfAgent", "C_CustOfAgent");
                                bulkcopy.ColumnMappings.Add("C_Note", "C_Note");
                            }
                            break;
                        case "TSalesMoving":
                            {
                                bulkcopy.ColumnMappings.Add("CUS_ID", "CUS_ID");
                                bulkcopy.ColumnMappings.Add("CUS_NAME", "CUS_NAME");
                                bulkcopy.ColumnMappings.Add("Date", "Date");
                                bulkcopy.ColumnMappings.Add("PRODUCT", "PRODUCT");
                                bulkcopy.ColumnMappings.Add("PRODUCT_ID", "PRODUCT_ID");
                                bulkcopy.ColumnMappings.Add("UNIT", "UNIT");
                            }
                            break;
                    }
                    try
                    {
                        bulkcopy.WriteToServer(tb);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Connecting || connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return true;
        }

        public int ConnectAndExculeScala(string sql)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            int result = 0;
            string firstSql = null;

            connetionString = Config.GetConfigValue("connectionString");
            firstSql = sql;
            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();

                command = new SqlCommand(firstSql, connection);
                result = (int)command.ExecuteScalar();

                command.Dispose();
                connection.Close();

                return result;
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                if (connection.State == ConnectionState.Connecting || connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return result;
        }

        public void ConnectAndExcule(string sql)
        {
            string connetionString = null;
            SqlCommand command;

            string firstSql = null;

            //connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            //firstSql = "Your First SQL Statement Here";
            //secondSql = "Your Second SQL Statement Here";
            connetionString = Config.GetConfigValue("connectionString"); //"Data Source=PHUOCMINH-PC1;Initial Catalog=SERVIER;User ID=sa;Password=1234";
            firstSql = sql;// "p_TSallingPharmacy_Report12_DoctorSale 0";

            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = firstSql;
                command.ExecuteNonQuery();
                connection.Close();
            }

            //if (connection.State == ConnectionState.Connecting || connection.State == ConnectionState.Open)
            //    connection.Close();
        }

        public void isConnectAndExcule(string sql)
        {
            string connetionString = Config.GetConfigValue("connectionString");
            SqlConnection connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandTimeout = 3600;
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                if (connection.State == ConnectionState.Connecting || connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void isConnectAndExcule_Dynamic(string sql, string serverName, string databaseName, string loginName, string loginPass)
        {
            string connetionString = null;
            connetionString = Config.GetConfigValue("connectionStringDynamic");
            connetionString = connetionString.Replace("{SERVER_NAME}", serverName);
            connetionString = connetionString.Replace("{DATABASE_NAME}", databaseName);
            connetionString = connetionString.Replace("{LOGIN_USER}", loginName);
            connetionString = connetionString.Replace("{LOGIN_PASS}", loginPass);

            SqlConnection connection;
            connection = new SqlConnection(connetionString);
            string firstSql = null;
            firstSql = sql;
            SqlCommand command;
            command = new SqlCommand(firstSql, connection);
            try
            {

                connection.Open();
                command.CommandTimeout = 3600;
                command.ExecuteNonQuery();
                connection.Close();
            }
            finally
            {
                command.Dispose();

                if (connection.State == ConnectionState.Connecting || connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public DataSet ExcecuteDS_Dynamic(string sql, string serverName, string databaseName, string loginName, string loginPass)
        {
            string connetionString = null;
            connetionString = Config.GetConfigValue("connectionStringDynamic");
            connetionString = connetionString.Replace("{SERVER_NAME}", serverName);
            connetionString = connetionString.Replace("{DATABASE_NAME}", databaseName);
            connetionString = connetionString.Replace("{LOGIN_USER}", loginName);
            connetionString = connetionString.Replace("{LOGIN_PASS}", loginPass);

            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            string firstSql = null;
            firstSql = sql;

            bool isSuccess = false;
            int countfail = 0;
            while (!isSuccess && countfail < 3)
            {

                connection = new SqlConnection(connetionString);
                command = new SqlCommand(firstSql, connection);

                try
                {
                    connection.Open();
                    //connection.ConnectionTimeout = 3600;
                    command.CommandTimeout = 3600;
                    adapter.SelectCommand = command;
                    adapter.Fill(ds);
                    adapter.Dispose();
                    command.Dispose();

                    connection.Close();
                    isSuccess = true;

                    return ds;
                }
                catch
                {
                    countfail++;
                    //MessageBox.Show(ex.Message, false);
                }
                finally
                {
                    adapter.Dispose();
                    command.Dispose();

                    if (connection.State == ConnectionState.Connecting || connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return ds;
        }

    }

}