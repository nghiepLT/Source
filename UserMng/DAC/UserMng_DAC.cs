using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PQT.API;
using PQT.API.Connection;
using PQT.API.Module;
using System.Data;
using System.Data.SqlClient;
using UserMng.DataDefine;


namespace UserMng.DAC
{
    public class UserMng_DAC : ModuleDAC
    {
        #region Construct

        #region SQLConnectInit
        private SqlConnection m_con = null;
        #endregion

        #region Constructor
        public UserMng_DAC() { } // end of constructor #1
        #endregion

        #region Connection

        public void CreateCon(SqlConnection p_con)
        {
            m_con = p_con;
        }

        private void ConnectionOpen()
        {
            if (m_con.State == ConnectionState.Closed)
                m_con.Open();
        }

        #endregion


        /// <summary>
        /// DB요소가 존재하는지 여부를 반환합니다.
        /// </summary>
        /// <param name="p_element">검색할 요소 이름</param>
        /// <param name="p_DBElementType">요소 타입</param>
        /// <returns></returns>
        public override bool IsExistsDBElement(string p_element, DBElementType p_DBElementType)
        {
            bool result = false;
            SqlCommand objCmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();
            sb.Append("	SELECT COUNT(*) FROM sys.objects");
            if (p_DBElementType == DBElementType.Table)
                sb.Append("	WHERE object_id = OBJECT_ID(N'[dbo].[" + p_element + "]') AND type in (N'U')");
            else
                sb.Append("	WHERE object_id = OBJECT_ID(N'[dbo].[" + p_element + "]') AND type in (N'P', N'PC')");

            objCmd.Connection = m_con;
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = sb.ToString();

            try
            {
                int elementCnt = Convert.ToInt32(objCmd.ExecuteScalar());

                if (elementCnt > 0)
                    result = true;
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                objCmd.Dispose();
            }

            return result;
        } // end of method IsExistsDBElement

        /// <summary>
        /// 폴더에 있는 모든 *.sql파일을 실행합니다.
        /// </summary>
        /// <param name="p_filePath">폴더 경로</param>
        public override void ExecuteSql(string p_filePath)
        {
            string tableScript = string.Empty;
            SqlCommand objCmd = new SqlCommand();

            objCmd.Connection = m_con;
            objCmd.CommandType = CommandType.Text;

            DirectoryInfo directoryInfo = new DirectoryInfo(p_filePath);
            FileInfo[] fi = directoryInfo.GetFiles("*.sql");
            StreamReader dr = null;
            string script = string.Empty;

            for (int i = 0; i < fi.Length; i++)
            {
                dr = fi[i].OpenText();
                script = dr.ReadToEnd();
                dr.Close();
                dr.Dispose();

                objCmd.CommandText = script;

                try
                {
                    objCmd.ExecuteNonQuery();
                }
                catch (SqlException se)
                {
                    //ErrorWrite.Write(se as Exception);
                    throw new Exception(se.Message, se.InnerException);
                }
                catch (Exception ex)
                {
                    //ErrorWrite.Write(ex);
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }

            objCmd.Dispose();
        } // end of method ExecuteSql

        #endregion

        #region User

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-07
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowUser
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public UserEntity RowUser(int p_userID)
        {
            UserEntity userEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intUserID", SqlDbType.Int, p_userID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TUser_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    userEntity = new UserEntity();
                    userEntity.UserID = (DBNull.Value != dr["UserID"]) ? Convert.ToInt32(dr["UserID"]) : 0;
                    userEntity.LoginID = (DBNull.Value != dr["LoginID"]) ? Convert.ToString(dr["LoginID"]) : string.Empty;
                    userEntity.UserName = (DBNull.Value != dr["UserName"]) ? Convert.ToString(dr["UserName"]) : string.Empty;
                    userEntity.Password = (DBNull.Value != dr["Password"]) ? Convert.ToString(dr["Password"]) : string.Empty;
                    userEntity.Tel = (DBNull.Value != dr["Tel"]) ? Convert.ToString(dr["Tel"]) : string.Empty;
                    userEntity.Fax = (DBNull.Value != dr["Fax"]) ? Convert.ToString(dr["Fax"]) : string.Empty;
                    userEntity.Email = (DBNull.Value != dr["Email"]) ? Convert.ToString(dr["Email"]) : string.Empty;
                    userEntity.Address = (DBNull.Value != dr["Address"]) ? Convert.ToString(dr["Address"]) : string.Empty;
                    userEntity.CompanyName = (DBNull.Value != dr["CompanyName"]) ? Convert.ToString(dr["CompanyName"]) : string.Empty;
                    userEntity.IsExpire = (DBNull.Value != dr["IsExpire"]) ? Convert.ToBoolean(dr["IsExpire"]) : false;
                    userEntity.ExpireDate = (DBNull.Value != dr["ExpireDate"]) ? Convert.ToDateTime(dr["ExpireDate"]) : DateTime.Now;
                    userEntity.Remark = (DBNull.Value != dr["Remark"]) ? Convert.ToString(dr["Remark"]) : string.Empty;
                    userEntity.RegDate = (DBNull.Value != dr["RegDate"]) ? Convert.ToDateTime(dr["RegDate"]) : DateTime.Now;
                    userEntity.RegUser = (DBNull.Value != dr["RegUser"]) ? Convert.ToInt32(dr["RegUser"]) : 0;
                    userEntity.ModifyDate = (DBNull.Value != dr["ModifyDate"]) ? Convert.ToDateTime(dr["ModifyDate"]) : DateTime.Now;
                    userEntity.ModifyUser = (DBNull.Value != dr["ModifyUser"]) ? Convert.ToInt32(dr["ModifyUser"]) : 0;
                    userEntity.UserType = (DBNull.Value != dr["UserType"]) ? Convert.ToInt32(dr["UserType"]) : 2;
                    userEntity.PermissionString = (DBNull.Value != dr["PermissionString"]) ? Convert.ToString(dr["PermissionString"]) : string.Empty;
                    userEntity.UserLike = (DBNull.Value != dr["UserLike"]) ? Convert.ToInt32(dr["UserLike"]) : 0;
                     userEntity.Parentid = (DBNull.Value != dr["ParentID"]) ? Convert.ToInt32(dr["ParentID"]) : 0;
                     userEntity.Gender = (DBNull.Value != dr["Gender"]) ? Convert.ToInt32(dr["Gender"]) : 0;
                     userEntity.IdNhansu = (DBNull.Value != dr["IdNhansu"]) ? Convert.ToInt32(dr["IdNhansu"]) : 0;
                   
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return userEntity;

        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-07
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateUser
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public int CreateUser(UserEntity p_userEntity)
        {
            int userID = 0;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamOut("@intUserID", SqlDbType.Int),
			DBUtil.ParamIn("@strLoginID", SqlDbType.NVarChar, 50, p_userEntity.LoginID),
			DBUtil.ParamIn("@strUserName", SqlDbType.NVarChar, 250, p_userEntity.UserName),
			DBUtil.ParamIn("@strPassword", SqlDbType.NVarChar, 50, p_userEntity.Password),
			DBUtil.ParamIn("@strTel", SqlDbType.NVarChar, 50, p_userEntity.Tel),
			DBUtil.ParamIn("@strFax", SqlDbType.NVarChar, 50, p_userEntity.Fax),
			DBUtil.ParamIn("@strEmail", SqlDbType.NVarChar, 50, p_userEntity.Email),
			DBUtil.ParamIn("@strAddress", SqlDbType.NVarChar, 400, p_userEntity.Address),
			DBUtil.ParamIn("@strCompanyName", SqlDbType.NVarChar, 200, p_userEntity.CompanyName),
			DBUtil.ParamIn("@bitIsExpire", SqlDbType.Bit, p_userEntity.IsExpire),
			DBUtil.ParamIn("@dateExpireDate", SqlDbType.DateTime, p_userEntity.ExpireDate),
			DBUtil.ParamIn("@strRemark", SqlDbType.NVarChar, 500, p_userEntity.Remark),
			DBUtil.ParamIn("@intRegUser", SqlDbType.Int, p_userEntity.RegUser),
			DBUtil.ParamIn("@intModifyUser", SqlDbType.Int, p_userEntity.ModifyUser),
			DBUtil.ParamIn("@intUserType", SqlDbType.Int, p_userEntity.UserType),
			DBUtil.ParamIn("@strPermissionString", SqlDbType.NVarChar, 15, p_userEntity.PermissionString),
             DBUtil.ParamIn("@intUserlike", SqlDbType.Int, p_userEntity.UserLike),
            DBUtil.ParamIn("@intParent", SqlDbType.Int, p_userEntity.Parentid),
             DBUtil.ParamIn("@intGender", SqlDbType.Int, p_userEntity.Gender),
             DBUtil.ParamIn("@intIdNhansu", SqlDbType.Int, p_userEntity.IdNhansu)

		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TUser_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();
                userID = Convert.ToInt32(param[0].Value);
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return userID;
        }
        public int CreateCheckInout(CheckinoutEntity Checkinout_Entity)
        {
            int IDCheckinout = 0;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamOut("@IDCheck", SqlDbType.Int),
			DBUtil.ParamIn("@IDuser", SqlDbType.Int, Checkinout_Entity.IDuser),
			DBUtil.ParamIn("@NameUser", SqlDbType.NVarChar, 500, Checkinout_Entity.NameUser),
			DBUtil.ParamIn("@BarCodeUser", SqlDbType.NVarChar, 500, Checkinout_Entity.BarCodeUser),
			DBUtil.ParamIn("@DateCheck", SqlDbType.DateTime, Checkinout_Entity.DateCheck),
			DBUtil.ParamIn("@TimesOut", SqlDbType.NVarChar, 500, Checkinout_Entity.TimesOut),
			DBUtil.ParamIn("@TimesIn", SqlDbType.NVarChar, 500, Checkinout_Entity.TimesIn),
			DBUtil.ParamIn("@LyDoCheck", SqlDbType.NVarChar, 500, Checkinout_Entity.LyDoCheck),
			DBUtil.ParamIn("@Status", SqlDbType.Int, Checkinout_Entity.Status),
            DBUtil.ParamIn("@ImgCheck", SqlDbType.NVarChar, 500, Checkinout_Entity.Imgcheck),
			

		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_CheckInOut_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();
                IDCheckinout = Convert.ToInt32(param[0].Value);
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return IDCheckinout;
        }
        public string UpdateCheckInout(string Checkinout_ID, string timein, string imgin)
        {
            string IDCheckinout = "";
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			
			DBUtil.ParamIn("@BarCodeUser", SqlDbType.NVarChar, 500, Checkinout_ID),
            DBUtil.ParamIn("@Timein", SqlDbType.NVarChar, 500, timein),
            DBUtil.ParamIn("@imgin", SqlDbType.NVarChar, 500, imgin)
			
			

		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_UpdateCheckInOut", param);
           
            try
            {
                objCmd.ExecuteNonQuery();
                IDCheckinout = param[0].Value.ToString();
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return IDCheckinout;
        }
        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-07
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateUser
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateUser(UserEntity p_userEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intUserID", SqlDbType.Int, p_userEntity.UserID),
			DBUtil.ParamIn("@strLoginID", SqlDbType.NVarChar, 50, p_userEntity.LoginID),
			DBUtil.ParamIn("@strUserName", SqlDbType.NVarChar, 250, p_userEntity.UserName),
			DBUtil.ParamIn("@strPassword", SqlDbType.NVarChar, 50, p_userEntity.Password),
			DBUtil.ParamIn("@strTel", SqlDbType.NVarChar, 50, p_userEntity.Tel),
			DBUtil.ParamIn("@strFax", SqlDbType.NVarChar, 50, p_userEntity.Fax),
			DBUtil.ParamIn("@strEmail", SqlDbType.NVarChar, 50, p_userEntity.Email),
			DBUtil.ParamIn("@strAddress", SqlDbType.NVarChar, 400, p_userEntity.Address),
			DBUtil.ParamIn("@strCompanyName", SqlDbType.NVarChar, 200, p_userEntity.CompanyName),
			DBUtil.ParamIn("@bitIsExpire", SqlDbType.Bit, p_userEntity.IsExpire),
			DBUtil.ParamIn("@dateExpireDate", SqlDbType.DateTime, p_userEntity.ExpireDate),
			DBUtil.ParamIn("@strRemark", SqlDbType.NVarChar, 500, p_userEntity.Remark),
			DBUtil.ParamIn("@intRegUser", SqlDbType.Int, p_userEntity.RegUser),
			DBUtil.ParamIn("@intModifyUser", SqlDbType.Int, p_userEntity.ModifyUser),
			DBUtil.ParamIn("@intUserType", SqlDbType.Int, p_userEntity.UserType),
			DBUtil.ParamIn("@strPermissionString", SqlDbType.NVarChar, 15, p_userEntity.PermissionString),
            DBUtil.ParamIn("@intUserlike", SqlDbType.Int, p_userEntity.UserLike),
            DBUtil.ParamIn("@intParent", SqlDbType.Int, p_userEntity.Parentid),
            DBUtil.ParamIn("@intGender", SqlDbType.Int, p_userEntity.Gender),
            DBUtil.ParamIn("@intIdNhansu", SqlDbType.Int, p_userEntity.IdNhansu)


		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TUser_Update", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-07
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteUser
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteUser(int p_userID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intUserID", SqlDbType.Int, p_userID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TUser_Delete", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-07
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsUser
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsUser(int p_userType)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intUserType", SqlDbType.Int, p_userType),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TUser_Rows", param);

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }


        public UserEntity RowUserByLoginID(string p_LoginID)
        {
            UserEntity userEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@strLoginID", SqlDbType.NVarChar, 50, p_LoginID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TUserByLoginID_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    userEntity = new UserEntity();
                    userEntity.UserID = (DBNull.Value != dr["UserID"]) ? Convert.ToInt32(dr["UserID"]) : 0;
                    userEntity.LoginID = (DBNull.Value != dr["LoginID"]) ? Convert.ToString(dr["LoginID"]) : string.Empty;
                    userEntity.UserName = (DBNull.Value != dr["UserName"]) ? Convert.ToString(dr["UserName"]) : string.Empty;
                    userEntity.Password = (DBNull.Value != dr["Password"]) ? Convert.ToString(dr["Password"]) : string.Empty;
                    userEntity.Tel = (DBNull.Value != dr["Tel"]) ? Convert.ToString(dr["Tel"]) : string.Empty;
                    userEntity.Fax = (DBNull.Value != dr["Fax"]) ? Convert.ToString(dr["Fax"]) : string.Empty;
                    userEntity.Email = (DBNull.Value != dr["Email"]) ? Convert.ToString(dr["Email"]) : string.Empty;
                    userEntity.Address = (DBNull.Value != dr["Address"]) ? Convert.ToString(dr["Address"]) : string.Empty;
                    userEntity.CompanyName = (DBNull.Value != dr["CompanyName"]) ? Convert.ToString(dr["CompanyName"]) : string.Empty;
                    userEntity.IsExpire = (DBNull.Value != dr["IsExpire"]) ? Convert.ToBoolean(dr["IsExpire"]) : false;
                    userEntity.ExpireDate = (DBNull.Value != dr["ExpireDate"]) ? Convert.ToDateTime(dr["ExpireDate"]) : DateTime.Now;
                    userEntity.Remark = (DBNull.Value != dr["Remark"]) ? Convert.ToString(dr["Remark"]) : string.Empty;
                    userEntity.RegDate = (DBNull.Value != dr["RegDate"]) ? Convert.ToDateTime(dr["RegDate"]) : DateTime.Now;
                    userEntity.RegUser = (DBNull.Value != dr["RegUser"]) ? Convert.ToInt32(dr["RegUser"]) : 0;
                    userEntity.ModifyDate = (DBNull.Value != dr["ModifyDate"]) ? Convert.ToDateTime(dr["ModifyDate"]) : DateTime.Now;
                    userEntity.ModifyUser = (DBNull.Value != dr["ModifyUser"]) ? Convert.ToInt32(dr["ModifyUser"]) : 0;
                    userEntity.UserType = (DBNull.Value != dr["UserType"]) ? Convert.ToInt32(dr["UserType"]) : 2;
                    userEntity.PermissionString = (DBNull.Value != dr["PermissionString"]) ? Convert.ToString(dr["PermissionString"]) : string.Empty;
                    userEntity.UserLike = (DBNull.Value != dr["UserLike"]) ? Convert.ToInt32(dr["UserLike"]) : 0;
                    userEntity.Parentid = (DBNull.Value != dr["ParentID"]) ? Convert.ToInt32(dr["ParentID"]) : 0;
                    userEntity.Gender = (DBNull.Value != dr["Gender"]) ? Convert.ToInt32(dr["Gender"]) : 0;
                    userEntity.IdNhansu = (DBNull.Value != dr["IdNhansu"]) ? Convert.ToInt32(dr["IdNhansu"]) : 0;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return userEntity;

        }
        public UserEntity RowUserByMaNV(string p_LoginID)
        {
            UserEntity userEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@strLoginID", SqlDbType.NVarChar, 50, p_LoginID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TUserByManv_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    userEntity = new UserEntity();
                    userEntity.UserID = (DBNull.Value != dr["UserID"]) ? Convert.ToInt32(dr["UserID"]) : 0;
                    userEntity.LoginID = (DBNull.Value != dr["LoginID"]) ? Convert.ToString(dr["LoginID"]) : string.Empty;
                    userEntity.UserName = (DBNull.Value != dr["UserName"]) ? Convert.ToString(dr["UserName"]) : string.Empty;
                    userEntity.Password = (DBNull.Value != dr["Password"]) ? Convert.ToString(dr["Password"]) : string.Empty;
                    userEntity.Tel = (DBNull.Value != dr["Tel"]) ? Convert.ToString(dr["Tel"]) : string.Empty;
                    userEntity.Fax = (DBNull.Value != dr["Fax"]) ? Convert.ToString(dr["Fax"]) : string.Empty;
                    userEntity.Email = (DBNull.Value != dr["Email"]) ? Convert.ToString(dr["Email"]) : string.Empty;
                    userEntity.Address = (DBNull.Value != dr["Address"]) ? Convert.ToString(dr["Address"]) : string.Empty;
                    userEntity.CompanyName = (DBNull.Value != dr["CompanyName"]) ? Convert.ToString(dr["CompanyName"]) : string.Empty;
                    userEntity.IsExpire = (DBNull.Value != dr["IsExpire"]) ? Convert.ToBoolean(dr["IsExpire"]) : false;
                    userEntity.ExpireDate = (DBNull.Value != dr["ExpireDate"]) ? Convert.ToDateTime(dr["ExpireDate"]) : DateTime.Now;
                    userEntity.Remark = (DBNull.Value != dr["Remark"]) ? Convert.ToString(dr["Remark"]) : string.Empty;
                    userEntity.RegDate = (DBNull.Value != dr["RegDate"]) ? Convert.ToDateTime(dr["RegDate"]) : DateTime.Now;
                    userEntity.RegUser = (DBNull.Value != dr["RegUser"]) ? Convert.ToInt32(dr["RegUser"]) : 0;
                    userEntity.ModifyDate = (DBNull.Value != dr["ModifyDate"]) ? Convert.ToDateTime(dr["ModifyDate"]) : DateTime.Now;
                    userEntity.ModifyUser = (DBNull.Value != dr["ModifyUser"]) ? Convert.ToInt32(dr["ModifyUser"]) : 0;
                    userEntity.UserType = (DBNull.Value != dr["UserType"]) ? Convert.ToInt32(dr["UserType"]) : 2;
                    userEntity.PermissionString = (DBNull.Value != dr["PermissionString"]) ? Convert.ToString(dr["PermissionString"]) : string.Empty;
                    userEntity.UserLike = (DBNull.Value != dr["UserLike"]) ? Convert.ToInt32(dr["UserLike"]) : 0;
                    userEntity.Parentid = (DBNull.Value != dr["ParentID"]) ? Convert.ToInt32(dr["ParentID"]) : 0;
                    userEntity.Gender = (DBNull.Value != dr["Gender"]) ? Convert.ToInt32(dr["Gender"]) : 0;
                    userEntity.IdNhansu = (DBNull.Value != dr["IdNhansu"]) ? Convert.ToInt32(dr["IdNhansu"]) : 0;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return userEntity;

        }

        public UserEntity RowUserByEmail(string p_email)
        {
            UserEntity userEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@strEmail", SqlDbType.NVarChar, 150, p_email)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TUserByEmail_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    userEntity = new UserEntity();
                    userEntity.UserID = (DBNull.Value != dr["UserID"]) ? Convert.ToInt32(dr["UserID"]) : 0;
                    userEntity.LoginID = (DBNull.Value != dr["LoginID"]) ? Convert.ToString(dr["LoginID"]) : string.Empty;
                    userEntity.UserName = (DBNull.Value != dr["UserName"]) ? Convert.ToString(dr["UserName"]) : string.Empty;
                    userEntity.Password = (DBNull.Value != dr["Password"]) ? Convert.ToString(dr["Password"]) : string.Empty;
                    userEntity.Tel = (DBNull.Value != dr["Tel"]) ? Convert.ToString(dr["Tel"]) : string.Empty;
                    userEntity.Fax = (DBNull.Value != dr["Fax"]) ? Convert.ToString(dr["Fax"]) : string.Empty;
                    userEntity.Email = (DBNull.Value != dr["Email"]) ? Convert.ToString(dr["Email"]) : string.Empty;
                    userEntity.Address = (DBNull.Value != dr["Address"]) ? Convert.ToString(dr["Address"]) : string.Empty;
                    userEntity.CompanyName = (DBNull.Value != dr["CompanyName"]) ? Convert.ToString(dr["CompanyName"]) : string.Empty;
                    userEntity.IsExpire = (DBNull.Value != dr["IsExpire"]) ? Convert.ToBoolean(dr["IsExpire"]) : false;
                    userEntity.ExpireDate = (DBNull.Value != dr["ExpireDate"]) ? Convert.ToDateTime(dr["ExpireDate"]) : DateTime.Now;
                    userEntity.Remark = (DBNull.Value != dr["Remark"]) ? Convert.ToString(dr["Remark"]) : string.Empty;
                    userEntity.RegDate = (DBNull.Value != dr["RegDate"]) ? Convert.ToDateTime(dr["RegDate"]) : DateTime.Now;
                    userEntity.RegUser = (DBNull.Value != dr["RegUser"]) ? Convert.ToInt32(dr["RegUser"]) : 0;
                    userEntity.ModifyDate = (DBNull.Value != dr["ModifyDate"]) ? Convert.ToDateTime(dr["ModifyDate"]) : DateTime.Now;
                    userEntity.ModifyUser = (DBNull.Value != dr["ModifyUser"]) ? Convert.ToInt32(dr["ModifyUser"]) : 0;
                    userEntity.UserType = (DBNull.Value != dr["UserType"]) ? Convert.ToInt32(dr["UserType"]) : 2;
                    userEntity.PermissionString = (DBNull.Value != dr["PermissionString"]) ? Convert.ToString(dr["PermissionString"]) : string.Empty;
                    userEntity.UserLike = (DBNull.Value != dr["UserLike"]) ? Convert.ToInt32(dr["UserLike"]) : 0;
                    userEntity.Parentid = (DBNull.Value != dr["ParentID"]) ? Convert.ToInt32(dr["ParentID"]) : 0;
                    userEntity.Gender = (DBNull.Value != dr["Gender"]) ? Convert.ToInt32(dr["Gender"]) : 0;
                    userEntity.IdNhansu = (DBNull.Value != dr["IdNhansu"]) ? Convert.ToInt32(dr["IdNhansu"]) : 0;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return userEntity;

        }

        #endregion User

        #region PollOptions

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowPollOptions
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public PollOptionsEntity RowPollOptions(int p_pollID, int p_pollOptionID)
        {
            PollOptionsEntity pollOptionsEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intPollID", SqlDbType.Int, p_pollID),
			DBUtil.ParamIn("@intPollOptionID", SqlDbType.Int, p_pollOptionID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPollOptions_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    pollOptionsEntity = new PollOptionsEntity();
                    pollOptionsEntity.PollOptionID = (DBNull.Value != dr["PollOptionID"]) ? Convert.ToInt32(dr["PollOptionID"]) : 0;
                    pollOptionsEntity.PollID = (DBNull.Value != dr["PollID"]) ? Convert.ToInt32(dr["PollID"]) : 0;
                    pollOptionsEntity.Answer = (DBNull.Value != dr["Answer"]) ? Convert.ToString(dr["Answer"]) : string.Empty;
                    pollOptionsEntity.Votes = (DBNull.Value != dr["Votes"]) ? Convert.ToInt32(dr["Votes"]) : 0;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return pollOptionsEntity;

        }

        /// =========================================================================
        /// Created Date            : 2009-09-03
        /// Created By              : PQT_GenTool
        /// Description             : CreatePollOptions
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public int CreatePollOptions(PollOptionsEntity p_pollOptionsEntity)
        {
            int pollOptionID = 0;

            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamOut("@intPollOptionID", SqlDbType.Int),
			DBUtil.ParamIn("@intPollID", SqlDbType.Int, p_pollOptionsEntity.PollID),
			DBUtil.ParamIn("@strAnswer", SqlDbType.NVarChar, 250, p_pollOptionsEntity.Answer),
			DBUtil.ParamIn("@intVotes", SqlDbType.Int, p_pollOptionsEntity.Votes),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPollOptions_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();
                pollOptionID = Convert.ToInt32(param[0].Value);
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return pollOptionID;

        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdatePollOptions
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdatePollOptions(PollOptionsEntity p_pollOptionsEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPollOptionID", SqlDbType.Int, p_pollOptionsEntity.PollOptionID),
			DBUtil.ParamIn("@intPollID", SqlDbType.Int, p_pollOptionsEntity.PollID),
			DBUtil.ParamIn("@strAnswer", SqlDbType.NVarChar, 250, p_pollOptionsEntity.Answer),
			DBUtil.ParamIn("@intVotes", SqlDbType.Int, p_pollOptionsEntity.Votes),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPollOptions_Update", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeletePollOptions
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeletePollOptions(int p_pollID, int p_pollOptionID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPollID", SqlDbType.Int, p_pollID),
			DBUtil.ParamIn("@intPollOptionID", SqlDbType.Int, p_pollOptionID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPollOptions_Delete", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsPollOptions
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsPollOptions()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPollOptions_Rows");

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }


        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsPollOptionsByPollID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsPollOptionsByPollID(int p_pollID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPollID", SqlDbType.Int, p_pollID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPollOptionsByPollID_Rows", param);

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }


        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsPollOptionsByPollOptionID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsPollOptionsByPollOptionID(int p_pollOptionID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPollOptionID", SqlDbType.Int, p_pollOptionID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPollOptionsByPollOptionID_Rows", param);

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }

        public int SumVoteByPollID(int p_pollID)
        {
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			        DBUtil.ParamIn("@intPollID", SqlDbType.Int, p_pollID)
		        };

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPollOptionsSumVotesByPollID_Row", param);

            try
            {
                return Convert.ToInt32(objCmd.ExecuteScalar());
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                objCmd.Dispose();
            }

        }


        #endregion PollOptions

        #region Polls

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowPolls
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public PollsEntity RowPolls(int p_pollID)
        {
            PollsEntity pollsEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intPollID", SqlDbType.Int, p_pollID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPolls_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    pollsEntity = new PollsEntity();
                    pollsEntity.PollID = (DBNull.Value != dr["PollID"]) ? Convert.ToInt32(dr["PollID"]) : 0;
                    pollsEntity.Question = (DBNull.Value != dr["Question"]) ? Convert.ToString(dr["Question"]) : string.Empty;
                    pollsEntity.Active = (DBNull.Value != dr["Active"]) ? Convert.ToBoolean(dr["Active"]) : false;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return pollsEntity;

        }

        /// =========================================================================
        /// Created Date            : 2009-09-03
        /// Created By              : PQT_GenTool
        /// Description             : CreatePolls
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public int CreatePolls(PollsEntity p_pollsEntity)
        {
            int pollID = 0;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamOut("@intPollID", SqlDbType.Int),
			DBUtil.ParamIn("@strQuestion", SqlDbType.NVarChar, 200, p_pollsEntity.Question),
			DBUtil.ParamIn("@bitActive", SqlDbType.Bit, p_pollsEntity.Active),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPolls_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();
                pollID = Convert.ToInt32(param[0].Value);
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return pollID;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdatePolls
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdatePolls(PollsEntity p_pollsEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPollID", SqlDbType.Int, p_pollsEntity.PollID),
			DBUtil.ParamIn("@strQuestion", SqlDbType.NVarChar, 200, p_pollsEntity.Question),
			DBUtil.ParamIn("@bitActive", SqlDbType.Bit, p_pollsEntity.Active),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPolls_Update", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeletePolls
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeletePolls(int p_pollID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPollID", SqlDbType.Int, p_pollID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPolls_Delete", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsPolls
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsPolls()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TPolls_Rows");

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }




        #endregion Polls

        #region WebLink

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-04
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : RowWebLink
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public WebLinkEntity RowWebLink(int p_linkID)
        {
            WebLinkEntity webLinkEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intLinkID", SqlDbType.Int, p_linkID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TWebLink_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    webLinkEntity = new WebLinkEntity();
                    webLinkEntity.LinkID = (DBNull.Value != dr["LinkID"]) ? Convert.ToInt32(dr["LinkID"]) : 0;
                    webLinkEntity.LinkName = (DBNull.Value != dr["LinkName"]) ? Convert.ToString(dr["LinkName"]) : string.Empty;
                    webLinkEntity.Url = (DBNull.Value != dr["Url"]) ? Convert.ToString(dr["Url"]) : string.Empty;
                    webLinkEntity.IsView = (DBNull.Value != dr["IsView"]) ? Convert.ToBoolean(dr["IsView"]) : false;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return webLinkEntity;

        }

        /// =========================================================================
        /// Created Date            : 2009-09-04
        /// Created By              : PQT_GenTool
        /// Description             : CreateWebLink
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public int CreateWebLink(WebLinkEntity p_webLinkEntity)
        {
            int linkID = 0;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamOut("@intLinkID", SqlDbType.Int),
			DBUtil.ParamIn("@strLinkName", SqlDbType.NVarChar, 250, p_webLinkEntity.LinkName),
			DBUtil.ParamIn("@strUrl", SqlDbType.NVarChar, 250, p_webLinkEntity.Url),
			DBUtil.ParamIn("@bitIsView", SqlDbType.Bit, p_webLinkEntity.IsView),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TWebLink_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();
                linkID = Convert.ToInt32(param[0].Value);
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return linkID;
        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-04
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : UpdateWebLink
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateWebLink(WebLinkEntity p_webLinkEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLinkID", SqlDbType.Int, p_webLinkEntity.LinkID),
			DBUtil.ParamIn("@strLinkName", SqlDbType.NVarChar, 250, p_webLinkEntity.LinkName),
			DBUtil.ParamIn("@strUrl", SqlDbType.NVarChar, 250, p_webLinkEntity.Url),
			DBUtil.ParamIn("@bitIsView", SqlDbType.Bit, p_webLinkEntity.IsView),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TWebLink_Update", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-04
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : DeleteWebLink
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteWebLink(int p_linkID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLinkID", SqlDbType.Int, p_linkID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TWebLink_Delete", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-04
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : RowsWebLink
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsWebLink()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TWebLink_Rows");

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }




        #endregion WebLink

        #region BannerAdv

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowBannerAdv
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public BannerAdvEntity RowBannerAdv(int p_bannerID)
        {
            BannerAdvEntity bannerAdvEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intBannerID", SqlDbType.Int, p_bannerID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdv_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    bannerAdvEntity = new BannerAdvEntity();
                    bannerAdvEntity.BannerID = (DBNull.Value != dr["BannerID"]) ? Convert.ToInt32(dr["BannerID"]) : 0;
                    bannerAdvEntity.Url = (DBNull.Value != dr["Url"]) ? Convert.ToString(dr["Url"]) : string.Empty;
                    bannerAdvEntity.FileID = (DBNull.Value != dr["FileID"]) ? Convert.ToInt64(dr["FileID"]) : 0;
                    bannerAdvEntity.Position = (DBNull.Value != dr["Position"]) ? Convert.ToInt32(dr["Position"]) : 0;
                    bannerAdvEntity.IsActive = (DBNull.Value != dr["IsActive"]) ? Convert.ToBoolean(dr["IsActive"]) : false;
                    bannerAdvEntity.RegDate = (DBNull.Value != dr["RegDate"]) ? Convert.ToDateTime(dr["RegDate"]) : DateTime.Now;
                    bannerAdvEntity.SortOrder = (DBNull.Value != dr["SortOrder"]) ? Convert.ToInt32(dr["SortOrder"]) : 0;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return bannerAdvEntity;

        }

        /// =========================================================================
        /// Created Date            : 2009-09-16
        /// Created By              : PQT_GenTool
        /// Description             : CreateBannerAdv
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public int CreateBannerAdv(BannerAdvEntity p_bannerAdvEntity)
        {
            int bannerID = 0;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamOut("@intBannerID", SqlDbType.Int),
			DBUtil.ParamIn("@strUrl", SqlDbType.NVarChar, 250, p_bannerAdvEntity.Url),
			DBUtil.ParamIn("@longFileID", SqlDbType.BigInt, p_bannerAdvEntity.FileID),
			DBUtil.ParamIn("@intPosition", SqlDbType.Int, p_bannerAdvEntity.Position),
			DBUtil.ParamIn("@bitIsActive", SqlDbType.Bit, p_bannerAdvEntity.IsActive),
            DBUtil.ParamIn("@intSortOrder", SqlDbType.Int, p_bannerAdvEntity.SortOrder),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdv_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();
                bannerID = Convert.ToInt32(param[0].Value);
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return bannerID;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateBannerAdv
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateBannerAdv(BannerAdvEntity p_bannerAdvEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intBannerID", SqlDbType.Int, p_bannerAdvEntity.BannerID),
			DBUtil.ParamIn("@strUrl", SqlDbType.NVarChar, 250, p_bannerAdvEntity.Url),
			DBUtil.ParamIn("@longFileID", SqlDbType.BigInt, p_bannerAdvEntity.FileID),
			DBUtil.ParamIn("@intPosition", SqlDbType.Int, p_bannerAdvEntity.Position),
			DBUtil.ParamIn("@bitIsActive", SqlDbType.Bit, p_bannerAdvEntity.IsActive),
            DBUtil.ParamIn("@intSortOrder", SqlDbType.Int, p_bannerAdvEntity.SortOrder),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdv_Update", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteBannerAdv
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteBannerAdv(int p_bannerID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intBannerID", SqlDbType.Int, p_bannerID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdv_Delete", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsBannerAdv
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsBannerAdv()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdv_Rows");

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }




        #endregion BannerAdv

        #region BannerAdvDescription

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowBannerAdvDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public BannerAdvDescriptionEntity RowBannerAdvDescription(int p_bannerID, int p_languageID)
        {
            BannerAdvDescriptionEntity bannerAdvDescriptionEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intBannerID", SqlDbType.Int, p_bannerID),
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdvDescription_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    bannerAdvDescriptionEntity = new BannerAdvDescriptionEntity();
                    bannerAdvDescriptionEntity.BannerID = (DBNull.Value != dr["BannerID"]) ? Convert.ToInt32(dr["BannerID"]) : 0;
                    bannerAdvDescriptionEntity.LanguageID = (DBNull.Value != dr["LanguageID"]) ? Convert.ToInt32(dr["LanguageID"]) : 0;
                    bannerAdvDescriptionEntity.Name = (DBNull.Value != dr["Name"]) ? Convert.ToString(dr["Name"]) : string.Empty;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return bannerAdvDescriptionEntity;

        }

        /// =========================================================================
        /// Created Date            : 2009-09-15
        /// Created By              : PQT_GenTool
        /// Description             : CreateBannerAdvDescription
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public void CreateBannerAdvDescription(BannerAdvDescriptionEntity p_bannerAdvDescriptionEntity)
        {

            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intBannerID", SqlDbType.Int, p_bannerAdvDescriptionEntity.BannerID),
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_bannerAdvDescriptionEntity.LanguageID),
			DBUtil.ParamIn("@strName", SqlDbType.NVarChar, 150, p_bannerAdvDescriptionEntity.Name),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdvDescription_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();

            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }


        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateBannerAdvDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateBannerAdvDescription(BannerAdvDescriptionEntity p_bannerAdvDescriptionEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intBannerID", SqlDbType.Int, p_bannerAdvDescriptionEntity.BannerID),
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_bannerAdvDescriptionEntity.LanguageID),
			DBUtil.ParamIn("@strName", SqlDbType.NVarChar, 150, p_bannerAdvDescriptionEntity.Name),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdvDescription_Update", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteBannerAdvDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteBannerAdvDescription(int p_bannerID, int p_languageID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intBannerID", SqlDbType.Int, p_bannerID),
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdvDescription_Delete", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsBannerAdvDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsBannerAdvDescription()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdvDescription_Rows");

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }


        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsBannerAdvDescriptionByBannerID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsBannerAdvDescriptionByBannerID(int p_bannerID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intBannerID", SqlDbType.Int, p_bannerID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdvDescriptionByBannerID_Rows", param);

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }


        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsBannerAdvDescriptionByLanguageID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsBannerAdvDescriptionByLanguageID(int p_languageID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TBannerAdvDescriptionByLanguageID_Rows", param);

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }




        #endregion BannerAdvDescription


        #region Language

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-07-24
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsLanguage
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsLanguage()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TLanguage_Rows");

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }

        #endregion Language

        #region MenuAdmin

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-06-18
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowMenuAdmin
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public MenuAdminEntity RowMenuAdmin(int p_menuID)
        {
            MenuAdminEntity menuAdminEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intMenuID", SqlDbType.Int, p_menuID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TMenuAdmin_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    menuAdminEntity = new MenuAdminEntity();
                    menuAdminEntity.MenuID = (DBNull.Value != dr["MenuID"]) ? Convert.ToInt32(dr["MenuID"]) : 0;
                    menuAdminEntity.MenuName = (DBNull.Value != dr["MenuName"]) ? Convert.ToString(dr["MenuName"]) : string.Empty;
                    menuAdminEntity.Link = (DBNull.Value != dr["Link"]) ? Convert.ToString(dr["Link"]) : string.Empty;
                    menuAdminEntity.MenuType = (DBNull.Value != dr["MenuType"]) ? Convert.ToInt32(dr["MenuType"]) : 0;
                    menuAdminEntity.Option1 = (DBNull.Value != dr["Option1"]) ? Convert.ToString(dr["Option1"]) : string.Empty;
                    menuAdminEntity.Option2 = (DBNull.Value != dr["Option2"]) ? Convert.ToInt32(dr["Option2"]) : 0;
                    menuAdminEntity.Option3 = (DBNull.Value != dr["Option3"]) ? Convert.ToBoolean(dr["Option3"]) : false;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return menuAdminEntity;

        }

        public MenuAdminEntity RowMenuAdminByKeyWord(string p_keyWord)
        {
            MenuAdminEntity menuAdminEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@strKeyWord", SqlDbType.NVarChar, p_keyWord)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TMenuAdminByKeyWord_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    menuAdminEntity = new MenuAdminEntity();
                    menuAdminEntity.MenuID = (DBNull.Value != dr["MenuID"]) ? Convert.ToInt32(dr["MenuID"]) : 0;
                    menuAdminEntity.MenuName = (DBNull.Value != dr["MenuName"]) ? Convert.ToString(dr["MenuName"]) : string.Empty;
                    menuAdminEntity.Link = (DBNull.Value != dr["Link"]) ? Convert.ToString(dr["Link"]) : string.Empty;
                    menuAdminEntity.MenuType = (DBNull.Value != dr["MenuType"]) ? Convert.ToInt32(dr["MenuType"]) : 0;
                    menuAdminEntity.Option1 = (DBNull.Value != dr["Option1"]) ? Convert.ToString(dr["Option1"]) : string.Empty;
                    menuAdminEntity.Option2 = (DBNull.Value != dr["Option2"]) ? Convert.ToInt32(dr["Option2"]) : 0;
                    menuAdminEntity.Option3 = (DBNull.Value != dr["Option3"]) ? Convert.ToBoolean(dr["Option3"]) : false;
                }
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                objCmd.Dispose();
            }

            return menuAdminEntity;

        }

        /// =========================================================================
        /// Created Date            : 2010-06-18
        /// Created By              : PQT_GenTool
        /// Description             : CreateMenuAdmin
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public int CreateMenuAdmin(MenuAdminEntity p_menuAdminEntity)
        {
            int menuID = 0;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamOut("@intMenuID", SqlDbType.Int),
			DBUtil.ParamIn("@strMenuName", SqlDbType.NVarChar, 150, p_menuAdminEntity.MenuName),
			DBUtil.ParamIn("@strLink", SqlDbType.NVarChar, 250, p_menuAdminEntity.Link),
			DBUtil.ParamIn("@intMenuType", SqlDbType.Int, p_menuAdminEntity.MenuType),
			DBUtil.ParamIn("@strOption1", SqlDbType.NVarChar, 250, p_menuAdminEntity.Option1),
			DBUtil.ParamIn("@intOption2", SqlDbType.Int, p_menuAdminEntity.Option2),
			DBUtil.ParamIn("@bitOption3", SqlDbType.Bit, p_menuAdminEntity.Option3),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TMenuAdmin_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();
                menuID = Convert.ToInt32(param[0].Value);
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return menuID;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-06-18
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateMenuAdmin
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateMenuAdmin(MenuAdminEntity p_menuAdminEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intMenuID", SqlDbType.Int, p_menuAdminEntity.MenuID),
			DBUtil.ParamIn("@strMenuName", SqlDbType.NVarChar, 150, p_menuAdminEntity.MenuName),
			DBUtil.ParamIn("@strLink", SqlDbType.NVarChar, 250, p_menuAdminEntity.Link),
			DBUtil.ParamIn("@intMenuType", SqlDbType.Int, p_menuAdminEntity.MenuType),
			DBUtil.ParamIn("@strOption1", SqlDbType.NVarChar, 250, p_menuAdminEntity.Option1),
			DBUtil.ParamIn("@intOption2", SqlDbType.Int, p_menuAdminEntity.Option2),
			DBUtil.ParamIn("@bitOption3", SqlDbType.Bit, p_menuAdminEntity.Option3),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TMenuAdmin_Update", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-06-18
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteMenuAdmin
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteMenuAdmin(int p_menuID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intMenuID", SqlDbType.Int, p_menuID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TMenuAdmin_Delete", param);

            try
            {
                result = objCmd.ExecuteNonQuery() != 0;
            }

            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }

            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }

            finally
            {
                objCmd.Dispose();
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-06-18
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsMenuAdmin
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsMenuAdmin(int p_menuType, int p_status, string p_keyWord)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            SqlParameter[] param = {
			    DBUtil.ParamIn("@intMenuType", SqlDbType.Int, p_menuType),
			    DBUtil.ParamIn("@intStatus", SqlDbType.Int, p_status),
			    DBUtil.ParamIn("@strKeyWord", SqlDbType.NVarChar, p_keyWord),
		    };

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TMenuAdmin_Rows", param);

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }

        public DataTable RowsMenuAdminByParentID(int p_menuType, int p_status, int p_parentID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            SqlParameter[] param = {
			    DBUtil.ParamIn("@intMenuType", SqlDbType.Int, p_menuType),
			    DBUtil.ParamIn("@intStatus", SqlDbType.Int, p_status),
			    DBUtil.ParamIn("@intParentID", SqlDbType.Int, p_parentID),
		    };

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TMenuAdminByParentID_Rows", param);

            try
            {
                da = new SqlDataAdapter(objCmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException se)
            {
                //ErrorWrite.Write(se as Exception);
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                //ErrorWrite.Write(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                if (dt != null)
                    dt.Dispose();
                objCmd.Dispose();
            }

            return dt;
        }


        #endregion MenuAdmin

    }
}
