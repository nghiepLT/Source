using System;
namespace NewsMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2010-07-05
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : NewsReleatedEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class NewsReleatedEntity
    {
		private long newsID;

		public long NewsID
		{
			get { return newsID; }
			set { newsID = value; }
		}

		private long releatedID;

		public long ReleatedID
		{
			get { return releatedID; }
			set { releatedID = value; }
		}

		private int sortID;

		public int SortID
		{
			get { return sortID; }
			set { sortID = value; }
		}


    }
}
