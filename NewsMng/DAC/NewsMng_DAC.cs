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
using NewsMng.DataDefine;


namespace NewsMng.DAC
{
    public class NewsMng_DAC : ModuleDAC
    {
        #region Construct

        #region SQLConnectInit
        private SqlConnection m_con = null;
        #endregion

        #region Constructor
        public NewsMng_DAC() { } // end of constructor #1
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

        #region News

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public NewsEntity RowNews(long p_newsID)
        {
            NewsEntity newsEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID)

		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNews_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsEntity = new NewsEntity();
                    newsEntity.NewsID = (DBNull.Value != dr["NewsID"]) ? Convert.ToInt64(dr["NewsID"]) : 0;
                    newsEntity.NewsSourceID = (DBNull.Value != dr["NewsSourceID"]) ? Convert.ToInt32(dr["NewsSourceID"]) : 0;
                    newsEntity.Author = (DBNull.Value != dr["Author"]) ? Convert.ToString(dr["Author"]) : string.Empty;
                    newsEntity.NewsStatus = (DBNull.Value != dr["NewsStatus"]) ? Convert.ToInt32(dr["NewsStatus"]) : 0;
                    newsEntity.DefaultPic = (DBNull.Value != dr["DefaultPic"]) ? Convert.ToInt64(dr["DefaultPic"]) : 0;
                    newsEntity.CountView = (DBNull.Value != dr["CountView"]) ? Convert.ToInt32(dr["CountView"]) : 0;
                    newsEntity.DateMng = (DBNull.Value != dr["DateMng"]) ? Convert.ToDateTime(dr["DateMng"]) : DateTime.Now;
                    newsEntity.IPAdd = (DBNull.Value != dr["IPAdd"]) ? Convert.ToString(dr["IPAdd"]) : string.Empty;
                    newsEntity.RegDate = (DBNull.Value != dr["RegDate"]) ? Convert.ToDateTime(dr["RegDate"]) : DateTime.Now;
                    newsEntity.RegUser = (DBNull.Value != dr["RegUser"]) ? Convert.ToInt32(dr["RegUser"]) : 0;
                    newsEntity.ModifyDate = (DBNull.Value != dr["ModifyDate"]) ? Convert.ToDateTime(dr["ModifyDate"]) : DateTime.Now;
                    newsEntity.ModifyUser = (DBNull.Value != dr["ModifyUser"]) ? Convert.ToInt32(dr["ModifyUser"]) : 0;
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

            return newsEntity;

        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public long CreateNews(NewsEntity p_newsEntity)
        {
            long newsID = 0;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamOut("@longNewsID", SqlDbType.BigInt),
			DBUtil.ParamIn("@intNewsSourceID", SqlDbType.Int, p_newsEntity.NewsSourceID),
			DBUtil.ParamIn("@strAuthor", SqlDbType.NVarChar, 300, p_newsEntity.Author),
			DBUtil.ParamIn("@intNewsStatus", SqlDbType.Int, p_newsEntity.NewsStatus),
			DBUtil.ParamIn("@longDefaultPic", SqlDbType.BigInt, p_newsEntity.DefaultPic),
			DBUtil.ParamIn("@intCountView", SqlDbType.Int, p_newsEntity.CountView),
			DBUtil.ParamIn("@dateDateMng", SqlDbType.DateTime, p_newsEntity.DateMng),
			DBUtil.ParamIn("@strIPAdd", SqlDbType.NVarChar, 50, p_newsEntity.IPAdd),
			DBUtil.ParamIn("@intRegUser", SqlDbType.Int, p_newsEntity.RegUser),
			DBUtil.ParamIn("@intModifyUser", SqlDbType.Int, p_newsEntity.ModifyUser),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNews_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();
                newsID = Convert.ToInt32(param[0].Value);
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

            return newsID;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNews(NewsEntity p_newsEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsEntity.NewsID),
			DBUtil.ParamIn("@intNewsSourceID", SqlDbType.Int, p_newsEntity.NewsSourceID),
			DBUtil.ParamIn("@strAuthor", SqlDbType.NVarChar, 300, p_newsEntity.Author),
			DBUtil.ParamIn("@intNewsStatus", SqlDbType.Int, p_newsEntity.NewsStatus),
			DBUtil.ParamIn("@longDefaultPic", SqlDbType.BigInt, p_newsEntity.DefaultPic),
			DBUtil.ParamIn("@intCountView", SqlDbType.Int, p_newsEntity.CountView),
			DBUtil.ParamIn("@dateDateMng", SqlDbType.DateTime, p_newsEntity.DateMng),
			DBUtil.ParamIn("@strIPAdd", SqlDbType.NVarChar, 50, p_newsEntity.IPAdd),
			DBUtil.ParamIn("@intRegUser", SqlDbType.Int, p_newsEntity.RegUser),
			DBUtil.ParamIn("@intModifyUser", SqlDbType.Int, p_newsEntity.ModifyUser),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNews_Update", param);

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

        public bool UpdateCountViewNews(long p_newsID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCountView_Update", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNews(long p_newsID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNews_Delete", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNews(int p_page, int p_pageSize, int p_langID, int p_status, int p_searchType, string p_searchText, int p_sortOption, int p_category)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPage", SqlDbType.Int, p_page),
			DBUtil.ParamIn("@intPageSize", SqlDbType.Int, p_pageSize),
			DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
			DBUtil.ParamIn("@intStatus", SqlDbType.Int, p_status),
			DBUtil.ParamIn("@intSortOption", SqlDbType.Int, p_sortOption),
			DBUtil.ParamIn("@intSearchType", SqlDbType.Int, p_searchType),
            DBUtil.ParamIn("@intCategory", SqlDbType.Int, p_category),
			DBUtil.ParamIn("@strSearchText", SqlDbType.NVarChar, 100, p_searchText)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNews_Rows", param);

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

        public int CountNews(int p_page, int p_pageSize, int p_langID, int p_status, int p_searchType, string p_searchText, int p_sortOption, int p_categoryID)
        {
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPage", SqlDbType.Int, p_page),
			DBUtil.ParamIn("@intPageSize", SqlDbType.Int, p_pageSize),
			DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
			DBUtil.ParamIn("@intStatus", SqlDbType.Int, p_status),
			DBUtil.ParamIn("@intSortOption", SqlDbType.Int, p_sortOption),
			DBUtil.ParamIn("@intSearchType", SqlDbType.Int, p_searchType),
			DBUtil.ParamIn("@intCategory", SqlDbType.Int, p_categoryID),
			DBUtil.ParamIn("@strSearchText", SqlDbType.NVarChar, 100, p_searchText)
		    };
            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNews_Count", param);

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

        public DataTable RowsNewsByNewsSourceID(int p_newsSourceID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsSourceID", SqlDbType.Int,p_newsSourceID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsByNewsSourceID_Rows", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsByPosition(int p_startIndex, int p_pageSize, int p_langID, int p_newsCategoryID, long p_NewID, int p_sortOption)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intStartIndex", SqlDbType.Int, p_startIndex),
			DBUtil.ParamIn("@intPageSize", SqlDbType.Int, p_pageSize),
			DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
			DBUtil.ParamIn("@intSortOption", SqlDbType.Int, p_sortOption),
            DBUtil.ParamIn("@intNewID", SqlDbType.BigInt, p_NewID),
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsByPosition_Rows", param);

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

        public DataTable RowsNewsByPositionAndCategoryIDList(int p_startIndex, int p_pageSize, int p_langID, string p_newsCategoryIDList)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intStartIndex", SqlDbType.Int, p_startIndex),
			DBUtil.ParamIn("@intPageSize", SqlDbType.Int, p_pageSize),
			DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
			DBUtil.ParamIn("@strCategoryIDList", SqlDbType.VarChar, 200, p_newsCategoryIDList)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsByPositionAndCategoryIDList_Rows", param);

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

        public DataTable RowsNewsStatistic(int p_page, int p_pageSize, int p_langID, int p_status, int p_searchType, string p_searchText, int p_category)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPage", SqlDbType.Int, p_page),
			DBUtil.ParamIn("@intPageSize", SqlDbType.Int, p_pageSize),
			DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
			DBUtil.ParamIn("@intStatus", SqlDbType.Int, p_status),
			DBUtil.ParamIn("@intCategory", SqlDbType.Int, p_category),
			DBUtil.ParamIn("@intSearchType", SqlDbType.Int, p_searchType),
			DBUtil.ParamIn("@strSearchText", SqlDbType.NVarChar, 100, p_searchText)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsStatistic_Rows", param);

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

        #endregion News

        #region NewsCategory

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowNewsCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public NewsCategoryEntity RowNewsCategory(int p_newsCategoryID)
        {
            NewsCategoryEntity newsCategoryEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategory_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsCategoryEntity = new NewsCategoryEntity();
                    newsCategoryEntity.NewsCategoryID = (DBNull.Value != dr["NewsCategoryID"]) ? Convert.ToInt32(dr["NewsCategoryID"]) : 0;
                    newsCategoryEntity.ParentID = (DBNull.Value != dr["ParentID"]) ? Convert.ToInt32(dr["ParentID"]) : 0;
                    newsCategoryEntity.SortOrder = (DBNull.Value != dr["SortOrder"]) ? Convert.ToInt32(dr["SortOrder"]) : 0;
                    newsCategoryEntity.IsView = (DBNull.Value != dr["IsView"]) ? Convert.ToBoolean(dr["IsView"]) : false;
                    newsCategoryEntity.Image = (DBNull.Value != dr["Image"]) ? Convert.ToInt64(dr["Image"]) : 0;
                    newsCategoryEntity.UniqueKey = (DBNull.Value != dr["UniqueKey"]) ? Convert.ToString(dr["UniqueKey"]) : null;
                    newsCategoryEntity.Keyword = (DBNull.Value != dr["Keywork"]) ? Convert.ToString(dr["Keywork"]) : null;
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

            return newsCategoryEntity;

        }

        public NewsCategoryEntity RowNewsCategoryByUniqueKey(string p_keyword)
        {
            NewsCategoryEntity newsCategoryEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@strUniqueKey", SqlDbType.NVarChar,50, p_keyword),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryByUniqueKey_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsCategoryEntity = new NewsCategoryEntity();
                    newsCategoryEntity.NewsCategoryID = (DBNull.Value != dr["NewsCategoryID"]) ? Convert.ToInt32(dr["NewsCategoryID"]) : 0;
                    newsCategoryEntity.ParentID = (DBNull.Value != dr["ParentID"]) ? Convert.ToInt32(dr["ParentID"]) : 0;
                    newsCategoryEntity.IsView = (DBNull.Value != dr["IsView"]) ? Convert.ToBoolean(dr["IsView"]) : false;
                    newsCategoryEntity.SortOrder = (DBNull.Value != dr["SortOrder"]) ? Convert.ToInt32(dr["SortOrder"]) : 0;
                    newsCategoryEntity.Image = (DBNull.Value != dr["Image"]) ? Convert.ToInt64(dr["Image"]) : 0;
                    newsCategoryEntity.UniqueKey = (DBNull.Value != dr["UniqueKey"]) ? Convert.ToString(dr["UniqueKey"]) : null;
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

            return newsCategoryEntity;

        }

        public NewsCategoryEntity RowNewsCategoryByKeyword(string p_keyword)
        {
            NewsCategoryEntity newsCategoryEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@strKeyword", SqlDbType.NVarChar,50, p_keyword),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryByKeyword_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsCategoryEntity = new NewsCategoryEntity();
                    newsCategoryEntity.NewsCategoryID = (DBNull.Value != dr["NewsCategoryID"]) ? Convert.ToInt32(dr["NewsCategoryID"]) : 0;
                    newsCategoryEntity.ParentID = (DBNull.Value != dr["ParentID"]) ? Convert.ToInt32(dr["ParentID"]) : 0;
                    newsCategoryEntity.IsView = (DBNull.Value != dr["IsView"]) ? Convert.ToBoolean(dr["IsView"]) : false;
                    newsCategoryEntity.SortOrder = (DBNull.Value != dr["SortOrder"]) ? Convert.ToInt32(dr["SortOrder"]) : 0;
                    newsCategoryEntity.Image = (DBNull.Value != dr["Image"]) ? Convert.ToInt64(dr["Image"]) : 0;
                    newsCategoryEntity.UniqueKey = (DBNull.Value != dr["UniqueKey"]) ? Convert.ToString(dr["UniqueKey"]) : null;
                    newsCategoryEntity.Keyword = (DBNull.Value != dr["Keywork"]) ? Convert.ToString(dr["Keywork"]) : null;
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

            return newsCategoryEntity;

        }
        /// =========================================================================
        /// Created Date            : 2009-08-25
        /// Created By              : PQT_GenTool
        /// Description             : CreateNewsCategory
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public int CreateNewsCategory(NewsCategoryEntity p_newsCategoryEntity)
        {
            int newsCategoryID = 0;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamOut("@intNewsCategoryID", SqlDbType.Int),
			DBUtil.ParamIn("@intParentID", SqlDbType.Int, p_newsCategoryEntity.ParentID),
			DBUtil.ParamIn("@intSortOrder", SqlDbType.Int, p_newsCategoryEntity.SortOrder),
			DBUtil.ParamIn("@bitIsView", SqlDbType.Bit, p_newsCategoryEntity.IsView),
			DBUtil.ParamIn("@longImage", SqlDbType.BigInt, p_newsCategoryEntity.Image),
			DBUtil.ParamIn("@strUniqueKey", SqlDbType.NVarChar,50, p_newsCategoryEntity.UniqueKey),
            DBUtil.ParamIn("@strKeyword", SqlDbType.NVarChar,50, p_newsCategoryEntity.Keyword),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategory_Create", param);

            try
            {
                objCmd.ExecuteNonQuery();
                newsCategoryID = Convert.ToInt32(param[0].Value);
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

            return newsCategoryID;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNewsCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsCategory(NewsCategoryEntity p_newsCategoryEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryEntity.NewsCategoryID),
			DBUtil.ParamIn("@intParentID", SqlDbType.Int, p_newsCategoryEntity.ParentID),
			DBUtil.ParamIn("@intSortOrder", SqlDbType.Int, p_newsCategoryEntity.SortOrder),
			DBUtil.ParamIn("@bitIsView", SqlDbType.Bit, p_newsCategoryEntity.IsView),
			DBUtil.ParamIn("@longImage", SqlDbType.BigInt, p_newsCategoryEntity.Image),
			DBUtil.ParamIn("@strUniqueKey", SqlDbType.NVarChar,50, p_newsCategoryEntity.UniqueKey),
            DBUtil.ParamIn("@strKeyword", SqlDbType.NVarChar,50, p_newsCategoryEntity.Keyword),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategory_Update", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteNewsCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNewsCategory(int p_newsCategoryID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategory_Delete", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsCategory()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategory_Rows");

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

        public DataTable RowsNewsByListCategory(int p_page, int p_pageSize, int p_langID, int p_status, string p_searchText, string p_newsCategoryIDs)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPage", SqlDbType.Int, p_page),
			DBUtil.ParamIn("@intPageSize", SqlDbType.Int, p_pageSize),
			DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
			DBUtil.ParamIn("@intStatus", SqlDbType.Int, p_status),
			DBUtil.ParamIn("@strSearchText", SqlDbType.NVarChar, 200, p_searchText),
			DBUtil.ParamIn("@strCategoryIDList", SqlDbType.NVarChar, 200, p_newsCategoryIDs)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsByPositionAndCategoryIDList_Rows", param);

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

        public int CountNewsByListCategory(int p_langID, int p_status, string p_searchText, string p_categoryIDs)
        {
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
			DBUtil.ParamIn("@intStatus", SqlDbType.Int, p_status),
			DBUtil.ParamIn("@strSearchText", SqlDbType.NVarChar, 200, p_searchText),
			DBUtil.ParamIn("@strCategoryIDList", SqlDbType.VarChar, 100, p_categoryIDs),
		    };
            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsByPositionAndCategoryIDList_Count", param);

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

        public DataTable Rows_News_By_RegUser(int p_page, int p_pageSize, int p_langID, int p_status, int p_userID, string p_searchText, string p_newsCategoryIDs)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intPage", SqlDbType.Int, p_page),
			DBUtil.ParamIn("@intPageSize", SqlDbType.Int, p_pageSize),
			DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
			DBUtil.ParamIn("@intStatus", SqlDbType.Int, p_status),
			DBUtil.ParamIn("@intUserID", SqlDbType.Int, p_userID),
			DBUtil.ParamIn("@strSearchText", SqlDbType.NVarChar, 200, p_searchText),
			DBUtil.ParamIn("@strCategoryIDList", SqlDbType.NVarChar, 200, p_newsCategoryIDs)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNews_By_RegUser_Rows", param);

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

        public DataTable RowsNewsCategoryByParentID(int p_parentID, int p_langID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            SqlParameter[] param = {
			    DBUtil.ParamIn("@intParentID", SqlDbType.Int, p_parentID),
			    DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
		    };

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryByParentID_Rows", param);

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

        /*son [p_TNewsCategoryByParentID_pageSize_Rows] - [p_TNewsCategoryByParentID_pageSize_Count]*/
        public DataTable RowsNewsCategoryByParentID_pageSize(int p_page, int p_pagesize, int p_parentID, int p_langID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            SqlParameter[] param = {
                DBUtil.ParamIn("@intPage", SqlDbType.Int, p_page),
                DBUtil.ParamIn("@intPageSize", SqlDbType.Int, p_pagesize),
			    DBUtil.ParamIn("@intParentID", SqlDbType.Int, p_parentID),
			    DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
		    };

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryByParentID_pageSize_Rows", param);

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

        public int CountNewsCategoryByParentID_pageSize(int p_parentID, int p_langID)
        {
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			    DBUtil.ParamIn("@intParentID", SqlDbType.Int, p_parentID),
			    DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
		    };
            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryByParentID_pageSize_Count", param);

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

        /*son creat store [p_TCategoryNews_SearchUKey] 07_04_2012*/

        public DataTable RowsNewsCategoryByParentID_andUK(int p_parentID,string p_UK ,int p_langID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            SqlParameter[] param = {
			    DBUtil.ParamIn("@intParentID", SqlDbType.Int, p_parentID),
			    DBUtil.ParamIn("@strUK", SqlDbType.NVarChar,50, p_UK),
                DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
		    };

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TCategoryNews_SearchUKey", param);

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

        //public int CountNewsByListCategory(int p_langID, int p_status, string p_searchText, string p_categoryIDs)
        //{
        //    SqlCommand objCmd = null;
        //    SqlParameter[] param = {
        //    DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID),
        //    DBUtil.ParamIn("@intStatus", SqlDbType.Int, p_status),
        //    DBUtil.ParamIn("@strSearchText", SqlDbType.NVarChar, 200, p_searchText),
        //    DBUtil.ParamIn("@strCategoryIDList", SqlDbType.VarChar, 100, p_categoryIDs),
        //    };
        //    DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsByPositionAndCategoryIDList_Count", param);

        //    try
        //    {
        //        return Convert.ToInt32(objCmd.ExecuteScalar());
        //    }
        //    catch (SqlException se)
        //    {
        //        //ErrorWrite.Write(se as Exception);
        //        throw new Exception(se.Message, se.InnerException);
        //    }
        //    catch (Exception ex)
        //    {
        //        //ErrorWrite.Write(ex);
        //        throw new Exception(ex.Message, ex.InnerException);
        //    }
        //    finally
        //    {
        //        objCmd.Dispose();
        //    }
        //}


        #endregion NewsCategory

        #region NewsCategoryDescription

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowNewsCategoryDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public NewsCategoryDescriptionEntity RowNewsCategoryDescription(int p_languageID, int p_newsCategoryID)
        {
            NewsCategoryDescriptionEntity newsCategoryDescriptionEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID),
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryDescription_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsCategoryDescriptionEntity = new NewsCategoryDescriptionEntity();
                    newsCategoryDescriptionEntity.NewsCategoryID = (DBNull.Value != dr["NewsCategoryID"]) ? Convert.ToInt32(dr["NewsCategoryID"]) : 0;
                    newsCategoryDescriptionEntity.LanguageID = (DBNull.Value != dr["LanguageID"]) ? Convert.ToInt32(dr["LanguageID"]) : 0;
                    newsCategoryDescriptionEntity.CategoryName = (DBNull.Value != dr["CategoryName"]) ? Convert.ToString(dr["CategoryName"]) : string.Empty;
                    newsCategoryDescriptionEntity.Description = (DBNull.Value != dr["Description"]) ? Convert.ToString(dr["Description"]) : string.Empty;
                    newsCategoryDescriptionEntity.SubDescription = (DBNull.Value != dr["SubDescription"]) ? Convert.ToString(dr["SubDescription"]) : string.Empty;
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

            return newsCategoryDescriptionEntity;

        }

        /// =========================================================================
        /// Created Date            : 2009-08-25
        /// Created By              : PQT_GenTool
        /// Description             : CreateNewsCategoryDescription
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsCategoryDescription(NewsCategoryDescriptionEntity p_newsCategoryDescriptionEntity)
        {

            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryDescriptionEntity.NewsCategoryID),
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_newsCategoryDescriptionEntity.LanguageID),
			DBUtil.ParamIn("@strCategoryName", SqlDbType.NVarChar, 350, p_newsCategoryDescriptionEntity.CategoryName),
			DBUtil.ParamIn("@strDescription", SqlDbType.NText, 1073741823, p_newsCategoryDescriptionEntity.Description),
            DBUtil.ParamIn("@strSubDescription", SqlDbType.NText, 1073741823, p_newsCategoryDescriptionEntity.SubDescription),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryDescription_Create", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNewsCategoryDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsCategoryDescription(NewsCategoryDescriptionEntity p_newsCategoryDescriptionEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryDescriptionEntity.NewsCategoryID),
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_newsCategoryDescriptionEntity.LanguageID),
			DBUtil.ParamIn("@strCategoryName", SqlDbType.NVarChar, 350, p_newsCategoryDescriptionEntity.CategoryName),
			DBUtil.ParamIn("@strDescription", SqlDbType.NText, 1073741823, p_newsCategoryDescriptionEntity.Description),
            DBUtil.ParamIn("@strSubDescription", SqlDbType.NText, 1073741823, p_newsCategoryDescriptionEntity.SubDescription),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryDescription_Update", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteNewsCategoryDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNewsCategoryDescription(int p_languageID, int p_newsCategoryID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID),
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryDescription_Delete", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsCategoryDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsCategoryDescription()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryDescription_Rows");

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsCategoryDescriptionByLanguageID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsCategoryDescriptionByLanguageID(int p_languageID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryDescriptionByLanguageID_Rows", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsCategoryDescriptionByNewsCategoryID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsCategoryDescriptionByNewsCategoryID(int p_newsCategoryID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCategoryDescriptionByNewsCategoryID_Rows", param);

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




        #endregion NewsCategoryDescription

        #region NewsDescription

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowNewsDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public NewsDescriptionEntity RowNewsDescription(int p_languageID, long p_newsID)
        {
            NewsDescriptionEntity newsDescriptionEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID),
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsDescription_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsDescriptionEntity = new NewsDescriptionEntity();
                    newsDescriptionEntity.NewsID = (DBNull.Value != dr["NewsID"]) ? Convert.ToInt64(dr["NewsID"]) : 0;
                    newsDescriptionEntity.LanguageID = (DBNull.Value != dr["LanguageID"]) ? Convert.ToInt32(dr["LanguageID"]) : 0;
                    newsDescriptionEntity.Title = (DBNull.Value != dr["Title"]) ? Convert.ToString(dr["Title"]) : string.Empty;
                    newsDescriptionEntity.Content = (DBNull.Value != dr["Content"]) ? Convert.ToString(dr["Content"]) : string.Empty;
                    newsDescriptionEntity.SubTitle = (DBNull.Value != dr["SubTitle"]) ? Convert.ToString(dr["SubTitle"]) : string.Empty;
                    newsDescriptionEntity.SubContent = (DBNull.Value != dr["SubContent"]) ? Convert.ToString(dr["SubContent"]) : string.Empty;
                    newsDescriptionEntity.Comment = (DBNull.Value != dr["Comment"]) ? Convert.ToString(dr["Comment"]) : string.Empty;
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

            return newsDescriptionEntity;

        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNewsDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsDescription(NewsDescriptionEntity p_newsDescriptionEntity)
        {
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsDescriptionEntity.NewsID),
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_newsDescriptionEntity.LanguageID),
			DBUtil.ParamIn("@strTitle", SqlDbType.NVarChar, 500, p_newsDescriptionEntity.Title),
			DBUtil.ParamIn("@strContent", SqlDbType.NText, 1073741823, p_newsDescriptionEntity.Content),
			DBUtil.ParamIn("@strSubTitle", SqlDbType.NVarChar, 500, p_newsDescriptionEntity.SubTitle),
			DBUtil.ParamIn("@strSubContent", SqlDbType.NText, 1073741823, p_newsDescriptionEntity.SubContent),
			DBUtil.ParamIn("@strComment", SqlDbType.NText, 1073741823, p_newsDescriptionEntity.Comment),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsDescription_Create", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNewsDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsDescription(NewsDescriptionEntity p_newsDescriptionEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsDescriptionEntity.NewsID),
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_newsDescriptionEntity.LanguageID),
			DBUtil.ParamIn("@strTitle", SqlDbType.NVarChar, 500, p_newsDescriptionEntity.Title),
			DBUtil.ParamIn("@strContent", SqlDbType.NText, 1073741823, p_newsDescriptionEntity.Content),
			DBUtil.ParamIn("@strSubTitle", SqlDbType.NVarChar, 500, p_newsDescriptionEntity.SubTitle),
			DBUtil.ParamIn("@strSubContent", SqlDbType.NText, 1073741823, p_newsDescriptionEntity.SubContent),
			DBUtil.ParamIn("@strComment", SqlDbType.NText, 1073741823, p_newsDescriptionEntity.Comment),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsDescription_Update", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteNewsDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNewsDescription(int p_languageID, long p_newsID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID),
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsDescription_Delete", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsDescription()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsDescription_Rows");

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsDescriptionByLanguageID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsDescriptionByLanguageID(int p_languageID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsDescriptionByLanguageID_Rows", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsDescriptionByNewsID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsDescriptionByNewsID(long p_newsID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsDescriptionByNewsID_Rows", param);

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




        #endregion NewsDescription

        #region NewsSource

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowNewsSource
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public NewsSourceEntity RowNewsSource(int p_languageID, int p_newsSourceID)
        {
            NewsSourceEntity newsSourceEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID),
			DBUtil.ParamIn("@intNewsSourceID", SqlDbType.Int, p_newsSourceID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsSource_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsSourceEntity = new NewsSourceEntity();
                    newsSourceEntity.NewsSourceID = (DBNull.Value != dr["NewsSourceID"]) ? Convert.ToInt32(dr["NewsSourceID"]) : 0;
                    newsSourceEntity.LanguageID = (DBNull.Value != dr["LanguageID"]) ? Convert.ToInt32(dr["LanguageID"]) : 0;
                    newsSourceEntity.NewsSourceName = (DBNull.Value != dr["NewsSourceName"]) ? Convert.ToString(dr["NewsSourceName"]) : string.Empty;
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

            return newsSourceEntity;

        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNewsSource
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsSource(NewsSourceEntity p_newsSourceEntity)
        {
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_newsSourceEntity.LanguageID),
			DBUtil.ParamIn("@strNewsSourceName", SqlDbType.NVarChar, 250, p_newsSourceEntity.NewsSourceName),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsSource_Create", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNewsSource
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsSource(NewsSourceEntity p_newsSourceEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsSourceID", SqlDbType.Int, p_newsSourceEntity.NewsSourceID),
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_newsSourceEntity.LanguageID),
			DBUtil.ParamIn("@strNewsSourceName", SqlDbType.NVarChar, 250, p_newsSourceEntity.NewsSourceName),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsSource_Update", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteNewsSource
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNewsSource(int p_languageID, int p_newsSourceID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID),
			DBUtil.ParamIn("@intNewsSourceID", SqlDbType.Int, p_newsSourceID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsSource_Delete", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsSource
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsSource()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsSource_Rows");

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsSourceByLanguageID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsSourceByLanguageID(int p_languageID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intLanguageID", SqlDbType.Int, p_languageID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsSourceByLanguageID_Rows", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsSourceByNewsSourceID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsSourceByNewsSourceID(int p_newsSourceID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsSourceID", SqlDbType.Int, p_newsSourceID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsSourceByNewsSourceID_Rows", param);

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




        #endregion NewsSource

        #region NewsToCategory

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowNewsToCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public NewsToCategoryEntity RowNewsToCategory(int p_newsCategoryID, long p_newsID)
        {
            NewsToCategoryEntity newsToCategoryEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID),
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsToCategory_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsToCategoryEntity = new NewsToCategoryEntity();
                    newsToCategoryEntity.NewsCategoryID = (DBNull.Value != dr["NewsCategoryID"]) ? Convert.ToInt32(dr["NewsCategoryID"]) : 0;
                    newsToCategoryEntity.NewsID = (DBNull.Value != dr["NewsID"]) ? Convert.ToInt64(dr["NewsID"]) : 0;
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

            return newsToCategoryEntity;

        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNewsToCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsToCategory(NewsToCategoryEntity p_newsToCategoryEntity)
        {
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsToCategoryEntity.NewsCategoryID),
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsToCategoryEntity.NewsID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsToCategory_Create", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNewsToCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsToCategory(NewsToCategoryEntity p_newsToCategoryEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsToCategoryEntity.NewsCategoryID),
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsToCategoryEntity.NewsID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsToCategory_Update", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteNewsToCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNewsToCategory(int p_newsCategoryID, long p_newsID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID),
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsToCategory_Delete", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsToCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsToCategory()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsToCategory_Rows");

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

        public DataTable RowsNewsToCategory(int p_newsCategoryID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            SqlParameter[] param = {
			    DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID),
		    };

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsToCategoryByNewsCategoryID_Rows", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsToCategoryByNewsCategoryID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsToCategoryByNewsCategoryID(int p_newsCategoryID, int p_langID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@intNewsCategoryID", SqlDbType.Int, p_newsCategoryID),
			DBUtil.ParamIn("@intLangID", SqlDbType.Int, p_langID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsToCategoryByNewsCategoryID_Rows", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsToCategoryByNewsID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsToCategoryByNewsID(long p_newsID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsToCategoryByNewsID_Rows", param);

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




        #endregion NewsToCategory

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

        #region NewsReleated

        /// =========================================================================
        /// 개발일자 (Created Date) : 2010-07-05
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : RowNewsReleated
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public NewsReleatedEntity RowNewsReleated(long p_newsID, long p_releatedID)
        {
            NewsReleatedEntity newsReleatedEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID),
			DBUtil.ParamIn("@longReleatedID", SqlDbType.BigInt, p_releatedID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsReleated_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsReleatedEntity = new NewsReleatedEntity();
                    newsReleatedEntity.NewsID = (DBNull.Value != dr["NewsID"]) ? Convert.ToInt64(dr["NewsID"]) : 0;
                    newsReleatedEntity.ReleatedID = (DBNull.Value != dr["ReleatedID"]) ? Convert.ToInt64(dr["ReleatedID"]) : 0;
                    newsReleatedEntity.SortID = (DBNull.Value != dr["SortID"]) ? Convert.ToInt32(dr["SortID"]) : 0;
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

            return newsReleatedEntity;

        }

        /// =========================================================================
        /// Created Date            : 2010-07-05
        /// Created By              : PQT_GenTool
        /// Description             : CreateNewsReleated
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsReleated(NewsReleatedEntity p_newsReleatedEntity)
        {

            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsReleatedEntity.NewsID),
			DBUtil.ParamIn("@longReleatedID", SqlDbType.BigInt, p_newsReleatedEntity.ReleatedID),
			DBUtil.ParamIn("@intSortID", SqlDbType.Int, p_newsReleatedEntity.SortID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsReleated_Create", param);

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
        /// 개발일자 (Created Date) : 2010-07-05
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : UpdateNewsReleated
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsReleated(NewsReleatedEntity p_newsReleatedEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsReleatedEntity.NewsID),
			DBUtil.ParamIn("@longReleatedID", SqlDbType.BigInt, p_newsReleatedEntity.ReleatedID),
			DBUtil.ParamIn("@intSortID", SqlDbType.Int, p_newsReleatedEntity.SortID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsReleated_Update", param);

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
        /// 개발일자 (Created Date) : 2010-07-05
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : DeleteNewsReleated
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNewsReleated(long p_newsID, long p_releatedID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID),
			DBUtil.ParamIn("@longReleatedID", SqlDbType.BigInt, p_releatedID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsReleated_Delete", param);

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
        /// 개발일자 (Created Date) : 2010-07-05
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : RowsNewsReleated
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsReleated()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsReleated_Rows");

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
        /// 개발일자 (Created Date) : 2010-07-05
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : RowsNewsReleatedByNewsID
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsReleatedByNewsID(long p_newsID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsReleatedByNewsID_Rows", param);

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
        /// 개발일자 (Created Date) : 2010-07-05
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : RowsNewsReleatedByReleatedID
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsReleatedByReleatedID(long p_releatedID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longReleatedID", SqlDbType.BigInt, p_releatedID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsReleatedByReleatedID_Rows", param);

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




        #endregion NewsReleated

        #region NewsComment

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-06
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowNewsComment
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public NewsCommentEntity RowNewsComment(string p_commentID, long p_newsID)
        {
            NewsCommentEntity newsCommentEntity = null;
            SqlDataReader dr = null;
            SqlCommand objCmd = null;

            SqlParameter[] param = {
			DBUtil.ParamIn("@strCommentID", SqlDbType.NVarChar, p_commentID),
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsComment_Row", param);

            try
            {
                dr = objCmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    newsCommentEntity = new NewsCommentEntity();
                    newsCommentEntity.NewsID = (DBNull.Value != dr["NewsID"]) ? Convert.ToInt64(dr["NewsID"]) : 0;
                    newsCommentEntity.CommentID = (DBNull.Value != dr["CommentID"]) ? Convert.ToString(dr["CommentID"]) : string.Empty;
                    newsCommentEntity.Email = (DBNull.Value != dr["Email"]) ? Convert.ToString(dr["Email"]) : string.Empty;
                    newsCommentEntity.Author = (DBNull.Value != dr["Author"]) ? Convert.ToString(dr["Author"]) : string.Empty;
                    newsCommentEntity.Status = (DBNull.Value != dr["Status"]) ? Convert.ToBoolean(dr["Status"]) : false;
                    newsCommentEntity.Title = (DBNull.Value != dr["Title"]) ? Convert.ToString(dr["Title"]) : string.Empty;
                    newsCommentEntity.Content = (DBNull.Value != dr["Content"]) ? Convert.ToString(dr["Content"]) : string.Empty;
                    newsCommentEntity.Option1 = (DBNull.Value != dr["Option1"]) ? Convert.ToString(dr["Option1"]) : string.Empty;
                    newsCommentEntity.RegDate = (DBNull.Value != dr["RegDate"]) ? Convert.ToDateTime(dr["RegDate"]) : DateTime.Now;
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

            return newsCommentEntity;

        }

        /// =========================================================================
        /// Created Date            : 2010-07-06
        /// Created By              : PQT_GenTool
        /// Description             : CreateNewsComment
        /// Change Log
        ///      (Date)          (Developer)            (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsComment(NewsCommentEntity p_newsCommentEntity)
        {

            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsCommentEntity.NewsID),
			DBUtil.ParamIn("@strCommentID", SqlDbType.NVarChar, 50, p_newsCommentEntity.CommentID),
			DBUtil.ParamIn("@strEmail", SqlDbType.NVarChar, 50, p_newsCommentEntity.Email),
			DBUtil.ParamIn("@strAuthor", SqlDbType.NVarChar, 50, p_newsCommentEntity.Author),
			DBUtil.ParamIn("@bitStatus", SqlDbType.Bit, p_newsCommentEntity.Status),
			DBUtil.ParamIn("@strTitle", SqlDbType.NVarChar, 250, p_newsCommentEntity.Title),
			DBUtil.ParamIn("@strContent", SqlDbType.NVarChar, 500, p_newsCommentEntity.Content),
			DBUtil.ParamIn("@strOption1", SqlDbType.NVarChar, 150, p_newsCommentEntity.Option1),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsComment_Create", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-06
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNewsComment
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsComment(NewsCommentEntity p_newsCommentEntity)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsCommentEntity.NewsID),
			DBUtil.ParamIn("@strCommentID", SqlDbType.NVarChar, 50, p_newsCommentEntity.CommentID),
			DBUtil.ParamIn("@strEmail", SqlDbType.NVarChar, 50, p_newsCommentEntity.Email),
			DBUtil.ParamIn("@strAuthor", SqlDbType.NVarChar, 50, p_newsCommentEntity.Author),
			DBUtil.ParamIn("@bitStatus", SqlDbType.Bit, p_newsCommentEntity.Status),
			DBUtil.ParamIn("@strTitle", SqlDbType.NVarChar, 250, p_newsCommentEntity.Title),
			DBUtil.ParamIn("@strContent", SqlDbType.NVarChar, 500, p_newsCommentEntity.Content),
			DBUtil.ParamIn("@strOption1", SqlDbType.NVarChar, 150, p_newsCommentEntity.Option1),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsComment_Update", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-06
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteNewsComment
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNewsComment(string p_commentID, long p_newsID)
        {
            bool result = false;
            SqlCommand objCmd = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@strCommentID", SqlDbType.NVarChar, 50, p_commentID),
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID),
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsComment_Delete", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-06
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsComment
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsComment()
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsComment_Rows");

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-06
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsCommentByCommentID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsCommentByCommentID(string p_commentID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@strCommentID", SqlDbType.NVarChar, p_commentID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCommentByCommentID_Rows", param);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-06
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsCommentByNewsID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsCommentByNewsID(long p_newsID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
			DBUtil.ParamIn("@longNewsID", SqlDbType.BigInt, p_newsID)
		};

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TNewsCommentByNewsID_Rows", param);

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




        #endregion NewsComment

    }
}
