using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using PQT.API;
using PQT.API.Connection;
using NewsMng.DAC;
using NewsMng.DataDefine;
using System.Data;

namespace NewsMng.BLC
{
    public class NewsMng_BLC_TX : ModuleBLC
    {
        #region Construct
        NewsMng_DAC nDAC = null;
        DBConnection dbCon = new DBConnection("WebCus");
        SqlConnection m_con = null;
        public NewsMng_BLC_TX()
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

        public override bool IsExistsDBElement(string p_element, DBElementType p_DBElementType)
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
        /// ∞äñδ¬à (Description)      : AddNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddNews(NewsEntity p_news)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    NewsEntity checkEtt = nDAC.RowNews(p_news.NewsID);
                    if (checkEtt != null)
                        nDAC.UpdateNews(p_news);
                    else
                        nDAC.CreateNews(p_news);

                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public long CreateNews(NewsEntity p_news)
        {
            long result = 0;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                result = nDAC.CreateNews(p_news);
            }
            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNews
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNews(NewsEntity p_news)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateNews(p_news);
            }
        }

        public bool UpdateCountViewNews(long p_newsID)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateCountViewNews(p_newsID);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteNews(p_newsID);
            }
        }

        #endregion News

        #region NewsCategory

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : AddNewsCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddNewsCategory(NewsCategoryEntity p_newsCategory)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    NewsCategoryEntity checkEtt = nDAC.RowNewsCategory(p_newsCategory.NewsCategoryID);
                    if (checkEtt != null)
                        nDAC.UpdateNewsCategory(p_newsCategory);
                    else
                        nDAC.CreateNewsCategory(p_newsCategory);

                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNewsCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public int CreateNewsCategory(NewsCategoryEntity p_newsCategory)
        {
            int result = 0;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                result = nDAC.CreateNewsCategory(p_newsCategory);
            }
            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNewsCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsCategory(NewsCategoryEntity p_newsCategory)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateNewsCategory(p_newsCategory);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteNewsCategory(p_newsCategoryID);
            }
        }

        #endregion NewsCategory

        #region NewsCategoryDescription

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : AddNewsCategoryDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddNewsCategoryDescription(NewsCategoryDescriptionEntity p_newsCategoryDescription)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    NewsCategoryDescriptionEntity checkEtt = nDAC.RowNewsCategoryDescription(p_newsCategoryDescription.LanguageID, p_newsCategoryDescription.NewsCategoryID);
                    if (checkEtt != null)
                        nDAC.UpdateNewsCategoryDescription(p_newsCategoryDescription);
                    else
                        nDAC.CreateNewsCategoryDescription(p_newsCategoryDescription);

                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-25
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNewsCategoryDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsCategoryDescription(NewsCategoryDescriptionEntity p_newsCategoryDescription)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                nDAC.CreateNewsCategoryDescription(p_newsCategoryDescription);
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
        public bool UpdateNewsCategoryDescription(NewsCategoryDescriptionEntity p_newsCategoryDescription)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateNewsCategoryDescription(p_newsCategoryDescription);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteNewsCategoryDescription(p_languageID, p_newsCategoryID);
            }
        }

        #endregion NewsCategoryDescription

        #region NewsDescription

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : AddNewsDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddNewsDescription(NewsDescriptionEntity p_newsDescription)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    NewsDescriptionEntity checkEtt = nDAC.RowNewsDescription(p_newsDescription.LanguageID, p_newsDescription.NewsID);
                    if (checkEtt != null)
                        nDAC.UpdateNewsDescription(p_newsDescription);
                    else
                        nDAC.CreateNewsDescription(p_newsDescription);

                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNewsDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsDescription(NewsDescriptionEntity p_newsDescription)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                nDAC.CreateNewsDescription(p_newsDescription);
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
        public bool UpdateNewsDescription(NewsDescriptionEntity p_newsDescription)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateNewsDescription(p_newsDescription);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteNewsDescription(p_languageID, p_newsID);
            }
        }

        #endregion NewsDescription

        #region NewsSource

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : AddNewsSource
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddNewsSource(NewsSourceEntity p_newsSource)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    NewsSourceEntity checkEtt = nDAC.RowNewsSource(p_newsSource.LanguageID, p_newsSource.NewsSourceID);
                    if (checkEtt != null)
                        nDAC.UpdateNewsSource(p_newsSource);
                    else
                        nDAC.CreateNewsSource(p_newsSource);

                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNewsSource
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsSource(NewsSourceEntity p_newsSource)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                nDAC.CreateNewsSource(p_newsSource);
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
        public bool UpdateNewsSource(NewsSourceEntity p_newsSource)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateNewsSource(p_newsSource);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteNewsSource(p_languageID, p_newsSourceID);
            }
        }

        #endregion NewsSource

        #region NewsToCategory

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : AddNewsToCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddNewsToCategory(NewsToCategoryEntity p_newsToCategory)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    NewsToCategoryEntity checkEtt = nDAC.RowNewsToCategory(p_newsToCategory.NewsCategoryID, p_newsToCategory.NewsID);
                    if (checkEtt != null)
                        nDAC.UpdateNewsToCategory(p_newsToCategory);
                    else
                        nDAC.CreateNewsToCategory(p_newsToCategory);

                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-03
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNewsToCategory
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsToCategory(NewsToCategoryEntity p_newsToCategory)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                nDAC.CreateNewsToCategory(p_newsToCategory);
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
        public bool UpdateNewsToCategory(NewsToCategoryEntity p_newsToCategory)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateNewsToCategory(p_newsToCategory);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteNewsToCategory(p_newsCategoryID, p_newsID);
            }
        }

        #endregion NewsToCategory

        #region NewsReleated

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-05
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : AddNewsReleated
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddNewsReleated(NewsReleatedEntity p_newsReleated)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    NewsReleatedEntity checkEtt = nDAC.RowNewsReleated(p_newsReleated.NewsID, p_newsReleated.ReleatedID);
                    if (checkEtt != null)
                        nDAC.UpdateNewsReleated(p_newsReleated);
                    else
                        nDAC.CreateNewsReleated(p_newsReleated);

                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-05
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateNewsReleated
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsReleated(NewsReleatedEntity p_newsReleated)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                nDAC.CreateNewsReleated(p_newsReleated);
            }

        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-05
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateNewsReleated
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsReleated(NewsReleatedEntity p_newsReleated)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateNewsReleated(p_newsReleated);
            }
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-05
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteNewsReleated
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNewsReleated(long p_newsID, long p_releatedID)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteNewsReleated(p_newsID, p_releatedID);
            }
        }

        #endregion NewsReleated

        #region NewsComment

        /// =========================================================================
        /// 개발일자 (Created Date) : 2010-07-06
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : AddNewsComment
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool AddNewsComment(NewsCommentEntity p_newsComment)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    NewsCommentEntity checkEtt = nDAC.RowNewsComment(p_newsComment.CommentID, p_newsComment.NewsID);
                    if (checkEtt != null)
                        nDAC.UpdateNewsComment(p_newsComment);
                    else
                        nDAC.CreateNewsComment(p_newsComment);

                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2010-07-06
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : CreateNewsComment
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public void CreateNewsComment(NewsCommentEntity p_newsComment)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                nDAC.CreateNewsComment(p_newsComment);
            }

        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2010-07-06
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : UpdateNewsComment
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateNewsComment(NewsCommentEntity p_newsComment)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateNewsComment(p_newsComment);
            }
        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2010-07-06
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : DeleteNewsComment
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteNewsComment(string p_commentID, long p_newsID)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteNewsComment(p_commentID, p_newsID);
            }
        }

        #endregion NewsComment





    }
}
