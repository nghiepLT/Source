using System;
namespace NewsMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-08-03
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : NewsToCategoryEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class NewsToCategoryEntity
    {
		private int newsCategoryID;

		public int NewsCategoryID
		{
			get { return newsCategoryID; }
			set { newsCategoryID = value; }
		}

		private long newsID;

		public long NewsID
		{
			get { return newsID; }
			set { newsID = value; }
		}


    }
}
