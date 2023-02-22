using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PQT.API;
using PQT.API.Connection;
using UserMng.DAC;
using UserMng.DataDefine;
using PQT.DAC;

namespace UserMng.BLC
{
    public class UserMng_BLC_NTX : ModuleBLC
    {
        #region Construct
        UserMng_DAC nDAC = null;
        DBConnection dbCon = new DBConnection("WebCus");
        SqlConnection m_con = null;
        UserMngDataDataContext UserContext = new UserMngDataDataContext ();
        public UserMng_BLC_NTX()
        {
            if (nDAC == null)
            {
                nDAC = new UserMng_DAC();
            }
        }

        public override bool ExecuteSql(string p_filePath)
        {
            bool result = false;
            UserMng_DAC ptDAC = new UserMng_DAC();

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
            UserMng_DAC nDAC = new UserMng_DAC();

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();

                nDAC.CreateCon(m_con);
                result = nDAC.IsExistsDBElement(p_element, p_DBElementType);
            }

            return result;
        } // end of method IsExistsDBElement

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
        /// 
        public List<TUser> ListUser()
        {
            var item = UserContext.TUsers.Where(w => w.UserType == 2 );
            return item.ToList();
        }
        public UserEntity RowUser(int p_userID)
        {
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowUser(p_userID);
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
        public DataTable RowsProductImages(int p_productID)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = RowsProductImage(p_productID);
            }

            return dt;
        }
        public DataTable RowsProductImage(int p_productID)
        {
            DataTable dt = null;
            SqlCommand objCmd = null;
            SqlDataAdapter da = null;
            SqlParameter[] param = {
                    DBUtil.ParamIn("@intProductID", SqlDbType.Int, p_productID)
		        };

            DBUtil.SPCmdCreate(m_con, ref objCmd, "p_TProductImage_Rows", param);

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
        public UserEntity RowUserByLoginID(string p_loginID)
        {
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowUserByLoginID(p_loginID);
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
        public UserEntity RowUserByMaNV(string p_loginID)
        {
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowUserByMaNV(p_loginID);
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

        public UserEntity RowUserByEmail(string p_email)
        {
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowUserByEmail(p_email);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsUser(p_userType);
            }

            return dt;
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowPollOptions(p_pollID, p_pollOptionID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsPollOptions();
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsPollOptionsByPollID(p_pollID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsPollOptionsByPollOptionID(p_pollOptionID);
            }

            return dt;
        }

        public int SumVoteByPollID(int p_pollID)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.SumVoteByPollID(p_pollID);
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowPolls(p_pollID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsPolls();
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowWebLink(p_linkID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsWebLink();
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowBannerAdv(p_bannerID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsBannerAdv();
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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowBannerAdvDescription(p_bannerID, p_languageID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsBannerAdvDescription();
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsBannerAdvDescriptionByBannerID(p_bannerID);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsBannerAdvDescriptionByLanguageID(p_languageID);
            }

            return dt;
        }

        #endregion BannerAdvDescription

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
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowMenuAdmin(p_menuID);
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


        public MenuAdminEntity RowMenuAdminByKeyWord(string p_keyWord)
        {
            try
            {
                m_con = dbCon.InitConnection();
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.RowMenuAdminByKeyWord(p_keyWord);
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsMenuAdmin(p_menuType, p_status, p_keyWord);
            }

            return dt;
        }

        public DataTable RowsMenuAdminByParentID(int p_menuType, int p_status, int p_parentID)
        {
            DataTable dt = null;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                dt = nDAC.RowsMenuAdminByParentID(p_menuType, p_status, p_parentID);
            }

            return dt;
        }


        #endregion MenuAdmin

       

    }
}
