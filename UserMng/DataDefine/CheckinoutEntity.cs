using System;
namespace UserMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-08-07
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : UserEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class CheckinoutEntity
    {
        private int iDCheck;
        public int IDCheck
        {
            get { return iDCheck; }
            set { iDCheck = value; }
        }
        private int iDuser;

        public int IDuser
		{
            get { return iDuser; }
            set { iDuser = value; }
		}

        private string nameUser;

        public string NameUser
		{
            get { return nameUser; }
            set { nameUser = value; }
		}

        private string barCodeUser;

        public string BarCodeUser
		{
            get { return barCodeUser; }
            set { barCodeUser = value; }
		}

        private DateTime dateCheck;

        public DateTime DateCheck
		{
            get { return dateCheck; }
            set { dateCheck = value; }
		}

        private string timesIn;

        public string TimesIn
		{
            get { return timesIn; }
            set { timesIn = value; }
		}
        private string timesOut;

        public string TimesOut
        {
            get { return timesOut; }
            set { timesOut = value; }
        }


        private string lyDoCheck;

        public string LyDoCheck
		{
            get { return lyDoCheck; }
            set { lyDoCheck = value; }
		}
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private string imgcheck;
        public string Imgcheck
        {
            get { return imgcheck; }
            set { imgcheck = value; }
        }  
        
    }

}
