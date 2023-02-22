using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PQT.API;
using PQT.API.Connection;
using NewsMng.DAC;
using NewsMng.DataDefine;

namespace NewsMng.BLC
{
    public class NewsMng_BLC_NTX : ModuleBLC
    {
        #region Construct
        NewsMng_DAC nDAC = null;
        DBConnection dbCon = new DBConnection("WebCus");
        SqlConnection m_con = null;
        public NewsMng_BLC_NTX()
        {
            if (nDAC == null)
            {
                nDAC = new NewsMng_DAC();
            }
        }

        public override bool ExecuteSql(string p_filePath)
        {
            bool result = false;
            NewsMng_DAC ptDAC = new NewsMng_DAC();

            using (m_con = dbCon.InitConnection())
            {
                try
                {
                    m_con.Open();
                    ptDAC.CreateCon(m_con);
                    ptDAC.ExecuteSql(p_filePath);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        } // end of method ExecuteSql

        public override bool IsExistsDBElement(string p_element, PQT.API.DBElementType p_DBElementType)
        {
            bool result = false;
            NewsMng_DAC nDAC = new NewsMng_DAC();

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();

                nDAC.CreateCon(m_con);
                result = nDAC.IsExistsDBElement(p_element, p_DBElementType);
            }

            return result;
        } // end of method IsExistsDBElement

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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNews(p_newsID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNews(int p_page, int p_pageSize, int p_langID, int p_status, int p_searchType, string p_searchText, int p_sortOption, int p_categoty)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNews(p_page, p_pageSize,p_langID,p_status,p_searchType,p_searchText,p_sortOption,p_categoty);
            }

            return dt;
        }

        public int CountNews(int p_page, int p_pageSize, int p_langID, int p_status, int p_searchType, string p_searchText, int p_sortOption, int p_categoryID)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.CountNews(p_page, p_pageSize, p_langID, p_status, p_searchType, p_searchText, p_sortOption, p_categoryID);
            }
        }

        public DataTable RowsNewsByNewsSourceID(int p_newsSourceID)
        {
            DataTable dt = null;
            using(m_con=dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsByNewsSourceID(p_newsSourceID);
            }
            return dt;
        }

        public DataTable RowsNewsByPosition(int p_startIndex, int p_pageSize, int p_langID, int p_newsCategoryID, long p_NewID,int p_sortOption)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsByPosition(p_startIndex, p_pageSize, p_langID, p_newsCategoryID, p_NewID, p_sortOption);
            }

            return dt;
        }

        public DataTable RowsNewsByPosition(int p_startIndex, int p_pageSize, int p_langID, int p_newsCategoryID, long p_NewID)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsByPosition(p_startIndex, p_pageSize, p_langID, p_newsCategoryID, p_NewID, 1);
            }

            return dt;
        }

        public DataTable RowsNewsByPositionAndCategoryIDList(int p_startIndex, int p_pageSize, int p_langID, string p_newsCategoryIDList)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsByPositionAndCategoryIDList(p_startIndex, p_pageSize, p_langID, p_newsCategoryIDList);
            }

            return dt;
        }

        public DataTable RowsNewsStatistic(int p_page, int p_pageSize, int p_langID, int p_status, int p_searchType, string p_searchText,int p_category)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsStatistic(p_page, p_pageSize, p_langID, p_status, p_searchType, p_searchText,p_category);
            }

            return dt;
        }

        #endregion News

        #region NewsCategory

        public NewsCategoryEntity RowNewsCategory(int p_newsCategoryID)
        {
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNewsCategory(p_newsCategoryID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
        }

        public NewsCategoryEntity RowNewsCategoryByUniqueKey(string p_keyword)
        {
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNewsCategoryByUniqueKey(p_keyword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
        }
        
        public NewsCategoryEntity RowNewsCategoryByKeyWord(string p_keyword)
        {
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNewsCategoryByKeyword(p_keyword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
        }

        public DataTable RowsNewsCategory()
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsCategory();
            }

            return dt;
        }

        public DataTable RowsNewsCategoryByParentID(int p_parentID, int p_langID)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsCategoryByParentID(p_parentID, p_langID);
            }

            return dt;
        }
        /**/
        public DataTable RowsNewsCategoryByParentID_pageSize(int p_page, int p_pagesize, int p_parentID, int p_langID)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsCategoryByParentID_pageSize(p_page, p_pagesize, p_parentID, p_langID);
            }

            return dt;
        }

        public int CountNewsCategoryByParentID_pageSize(int p_parentID, int p_langID)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.CountNewsCategoryByParentID_pageSize(p_parentID, p_langID);
            }
        }

        /*son creat store [p_TCategoryNews_SearchUKey] 07_04_2012*/

        public DataTable RowsNewsCategoryByParentID_andUK(int p_parentID, string p_UK, int p_langID)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsCategoryByParentID_andUK(p_parentID, p_UK,p_langID);
            }

            return dt;
        }
        /**/
        public DataTable RowsNewsByListCategory(int p_page, int p_pageSize, int p_langID, int p_status, string p_searchText, string p_newsCategoryIDs)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsByListCategory(p_page, p_pageSize, p_langID, p_status, p_searchText, p_newsCategoryIDs);
            }

            return dt;
        }

        public int CountNewsByListCategory(int p_langID, int p_status, string p_searchText, string p_categoryIDs)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.CountNewsByListCategory(p_langID, p_status, p_searchText, p_categoryIDs);
            }
        }

        
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNewsCategoryDescription(p_languageID, p_newsCategoryID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsCategoryDescription();
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsCategoryDescriptionByLanguageID(p_languageID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsCategoryDescriptionByNewsCategoryID(p_newsCategoryID);
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNewsDescription(p_languageID, p_newsID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsDescription();
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsDescriptionByLanguageID(p_languageID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsDescriptionByNewsID(p_newsID);
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNewsSource(p_languageID, p_newsSourceID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsSource();
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsSourceByLanguageID(p_languageID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsSourceByNewsSourceID(p_newsSourceID);
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNewsToCategory(p_newsCategoryID, p_newsID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsToCategory();
            }

            return dt;
        }

        public DataTable RowsNewsToCategory(int p_newsCategoryID)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsToCategory(p_newsCategoryID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsToCategoryByNewsCategoryID(p_newsCategoryID, p_langID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsToCategoryByNewsID(p_newsID);
            }

            return dt;
        }

        #endregion NewsToCategory

        #region Language
       
        public DataTable RowsLanguage()
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsLanguage();
            }

            return dt;
        }

        #endregion Language

        #region NewsReleated
        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-05
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowNewsReleated
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public NewsReleatedEntity RowNewsReleated(long p_newsID, long p_releatedID)
        {
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNewsReleated(p_newsID, p_releatedID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-05
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsReleated
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsReleated()
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsReleated();
            }

            return dt;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-05
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsReleatedByNewsID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsReleatedByNewsID(long p_newsID)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsReleatedByNewsID(p_newsID);
            }

            return dt;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-05
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : RowsNewsReleatedByReleatedID
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public DataTable RowsNewsReleatedByReleatedID(long p_releatedID)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsReleatedByReleatedID(p_releatedID);
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowNewsComment(p_commentID, p_newsID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                m_con.Close();
                m_con.Dispose();
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsComment();
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsCommentByCommentID(p_commentID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsNewsCommentByNewsID(p_newsID);
            }

            return dt;
        }

        #endregion NewsComment






    }
}
