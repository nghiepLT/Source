using System;
namespace NewsMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-08-03
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : NewsSourceEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class NewsSourceEntity
    {
		private int newsSourceID;

		public int NewsSourceID
		{
			get { return newsSourceID; }
			set { newsSourceID = value; }
		}

		private int languageID;

		public int LanguageID
		{
			get { return languageID; }
			set { languageID = value; }
		}

		private string newsSourceName;

		public string NewsSourceName
		{
			get { return newsSourceName; }
			set { newsSourceName = value; }
		}


    }
}
