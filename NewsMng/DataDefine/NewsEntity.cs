using System;
namespace NewsMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-08-03
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : NewsEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class NewsEntity
    {
		private long newsID;

		public long NewsID
		{
			get { return newsID; }
			set { newsID = value; }
		}

		private int newsSourceID;

		public int NewsSourceID
		{
			get { return newsSourceID; }
			set { newsSourceID = value; }
		}

		private string author;

		public string Author
		{
			get { return author; }
			set { author = value; }
		}

		private int newsStatus;

		public int NewsStatus
		{
			get { return newsStatus; }
			set { newsStatus = value; }
		}

		private long defaultPic;

		public long DefaultPic
		{
			get { return defaultPic; }
			set { defaultPic = value; }
		}

		private int countView;

		public int CountView
		{
			get { return countView; }
			set { countView = value; }
		}

		private DateTime dateMng;

		public DateTime DateMng
		{
			get { return dateMng; }
			set { dateMng = value; }
		}

		private string iPAdd;

		public string IPAdd
		{
			get { return iPAdd; }
			set { iPAdd = value; }
		}

		private DateTime regDate;

		public DateTime RegDate
		{
			get { return regDate; }
			set { regDate = value; }
		}

		private int regUser;

		public int RegUser
		{
			get { return regUser; }
			set { regUser = value; }
		}

		private DateTime modifyDate;

		public DateTime ModifyDate
		{
			get { return modifyDate; }
			set { modifyDate = value; }
		}

		private int modifyUser;

		public int ModifyUser
		{
			get { return modifyUser; }
			set { modifyUser = value; }
		}


    }
}
