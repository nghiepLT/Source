using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using PQT.API;
using PQT.API.Connection;
using UserMng.DAC;
using UserMng.DataDefine;
using System.Data;
using PQT.DAC;
using PQT.Common;

namespace UserMng.BLC
{
    public class UserMng_BLC_TX : ModuleBLC
    {
        #region Construct
        UserMng_DAC nDAC = null;
        DBConnection dbCon = new DBConnection("WebCus");
        //DBConnection dbCon = new DBConnection("10.10.18.60", "sat.com.vn", "usatdb", "123456");
        SqlConnection m_con = null;
        public UserMng_BLC_TX()
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

        public override bool IsExistsDBElement(string p_element, DBElementType p_DBElementType)
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
        /// ∞äñδ¬à (Description)      : AddUser
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddUser(UserEntity p_user)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    UserMng_BLC blc_user = new UserMng_BLC();
                    UserEntity checkEtt = nDAC.RowUser(p_user.UserID);
                    if (checkEtt != null)
                        nDAC.UpdateUser(p_user);
                    else
                    {
                       int idusernew = nDAC.CreateUser(p_user);
                        string[] arrayid = { "172", "12" };
                        TUserMapLink ent = null;
                        foreach (string str in arrayid)
                        {
                            ent = new TUserMapLink();
                            ent.UserID = idusernew;
                            ent.MenuID = Helper.TryParseInt(str, 0);
                            blc_user.CreateUserMapLink(ent);

                        }
                       
                    }
                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }
        public bool AddCheckInOut(CheckinoutEntity checkinout)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    int idusernew = nDAC.CreateCheckInout(checkinout);
                    return true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }
        public string UpdateCheckInOut(string idcheckinout,string timein,string imgin)
        {

            string result = "";

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    string nameofuser = nDAC.UpdateCheckInout(idcheckinout, timein, imgin);
                    return nameofuser;
                }
                catch
                {
                    result = "";
                }
            }

            return result;
        }
        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-07
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateUser
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public int CreateUser(UserEntity p_user)
        {
            int result = 0;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                result = nDAC.CreateUser(p_user);
            }
            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-08-07
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateUser
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateUser(UserEntity p_user)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateUser(p_user);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteUser(p_userID);
            }
        }

        #endregion User

        #region PollOptions

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-03
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : AddPollOptions
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool AddPollOptions(PollOptionsEntity p_pollOptions)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    PollOptionsEntity checkEtt = nDAC.RowPollOptions(p_pollOptions.PollID, p_pollOptions.PollOptionID);
                    if (checkEtt != null)
                        nDAC.UpdatePollOptions(p_pollOptions);
                    else
                        nDAC.CreatePollOptions(p_pollOptions);

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
        /// 개발일자 (Created Date) : 2009-09-03
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : CreatePollOptions
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public int CreatePollOptions(PollOptionsEntity p_pollOptions)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.CreatePollOptions(p_pollOptions);
            }

        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-03
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : UpdatePollOptions
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdatePollOptions(PollOptionsEntity p_pollOptions)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdatePollOptions(p_pollOptions);
            }
        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-03
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : DeletePollOptions
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool DeletePollOptions(int p_pollID, int p_pollOptionID)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeletePollOptions(p_pollID, p_pollOptionID);
            }
        }

        #endregion PollOptions

        #region Polls

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-03
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : AddPolls
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool AddPolls(PollsEntity p_polls)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    PollsEntity checkEtt = nDAC.RowPolls(p_polls.PollID);
                    if (checkEtt != null)
                        nDAC.UpdatePolls(p_polls);
                    else
                        nDAC.CreatePolls(p_polls);

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
        /// 개발일자 (Created Date) : 2009-09-03
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : CreatePolls
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public int CreatePolls(PollsEntity p_polls)
        {
            int result = 0;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                result = nDAC.CreatePolls(p_polls);
            }
            return result;
        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-03
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : UpdatePolls
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdatePolls(PollsEntity p_polls)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdatePolls(p_polls);
            }
        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-03
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : DeletePolls
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool DeletePolls(int p_pollID)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeletePolls(p_pollID);
            }
        }

        #endregion Polls

        #region WebLink

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-04
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : AddWebLink
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool AddWebLink(WebLinkEntity p_webLink)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    WebLinkEntity checkEtt = nDAC.RowWebLink(p_webLink.LinkID);
                    if (checkEtt != null)
                        nDAC.UpdateWebLink(p_webLink);
                    else
                        nDAC.CreateWebLink(p_webLink);

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
        /// 개발일자 (Created Date) : 2009-09-04
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : CreateWebLink
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public int CreateWebLink(WebLinkEntity p_webLink)
        {
            int result = 0;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                result = nDAC.CreateWebLink(p_webLink);
            }
            return result;
        }

        /// =========================================================================
        /// 개발일자 (Created Date) : 2009-09-04
        /// 작성자 (Created By)     : GenTool
        /// 설명 (Description)      : UpdateWebLink
        /// 수정사항 (Change Log)
        ///     날자 (Date)         작성자 (Developer)           내용 (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateWebLink(WebLinkEntity p_webLink)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateWebLink(p_webLink);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteWebLink(p_linkID);
            }
        }

        #endregion WebLink

        #region BannerAdv

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : AddBannerAdv
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddBannerAdv(BannerAdvEntity p_bannerAdv)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    BannerAdvEntity checkEtt = nDAC.RowBannerAdv(p_bannerAdv.BannerID);
                    if (checkEtt != null)
                        nDAC.UpdateBannerAdv(p_bannerAdv);
                    else
                        nDAC.CreateBannerAdv(p_bannerAdv);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateBannerAdv
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public int CreateBannerAdv(BannerAdvEntity p_bannerAdv)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.CreateBannerAdv(p_bannerAdv);
            }

        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateBannerAdv
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateBannerAdv(BannerAdvEntity p_bannerAdv)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateBannerAdv(p_bannerAdv);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteBannerAdv(p_bannerID);
            }
        }

        #endregion BannerAdv

        #region BannerAdvDescription

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : AddBannerAdvDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddBannerAdvDescription(BannerAdvDescriptionEntity p_bannerAdvDescription)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    BannerAdvDescriptionEntity checkEtt = nDAC.RowBannerAdvDescription(p_bannerAdvDescription.BannerID, p_bannerAdvDescription.LanguageID);
                    if (checkEtt != null)
                        nDAC.UpdateBannerAdvDescription(p_bannerAdvDescription);
                    else
                        nDAC.CreateBannerAdvDescription(p_bannerAdvDescription);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2009-09-15
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateBannerAdvDescription
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public void CreateBannerAdvDescription(BannerAdvDescriptionEntity p_bannerAdvDescription)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                nDAC.CreateBannerAdvDescription(p_bannerAdvDescription);
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
        public bool UpdateBannerAdvDescription(BannerAdvDescriptionEntity p_bannerAdvDescription)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateBannerAdvDescription(p_bannerAdvDescription);
            }
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
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteBannerAdvDescription(p_bannerID, p_languageID);
            }
        }

        #endregion BannerAdvDescription

        #region MenuAdmin

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-12
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : AddMenuAdmin
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool AddMenuAdmin(MenuAdminEntity p_menuAdmin)
        {

            bool result = false;

            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                try
                {
                    MenuAdminEntity checkEtt = nDAC.RowMenuAdmin(p_menuAdmin.MenuID);
                    if (checkEtt != null)
                        nDAC.UpdateMenuAdmin(p_menuAdmin);
                    else
                        nDAC.CreateMenuAdmin(p_menuAdmin);

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
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-12
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : CreateMenuAdmin
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public int CreateMenuAdmin(MenuAdminEntity p_menuAdmin)
        {
            int result = 0;
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                result = nDAC.CreateMenuAdmin(p_menuAdmin);
            }
            return result;
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-12
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : UpdateMenuAdmin
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool UpdateMenuAdmin(MenuAdminEntity p_menuAdmin)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.UpdateMenuAdmin(p_menuAdmin);
            }
        }

        /// =========================================================================
        /// Ω░£δ░£∞¥╝∞₧É (Created Date) : 2010-07-12
        /// ∞₧æ∞ä▒∞₧É (Created By)     : GenTool
        /// ∞äñδ¬à (Description)      : DeleteMenuAdmin
        /// ∞êÿ∞áò∞é¼φò¡ (Change Log)
        ///     δéá∞₧É (Date)         ∞₧æ∞ä▒∞₧É (Developer)           δé┤∞Ü⌐ (Content)
        ///                                                         
        /// =========================================================================
        public bool DeleteMenuAdmin(int p_menuID)
        {
            using (m_con = dbCon.InitConnection())
            {
                m_con.Open();
                nDAC.CreateCon(m_con);
                return nDAC.DeleteMenuAdmin(p_menuID);
            }
        }

        #endregion MenuAdmin




    }
}
