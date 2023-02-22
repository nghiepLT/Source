using System;
namespace NewsMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-08-25
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : NewsCategoryEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class NewsCategoryEntity
    {
		private int newsCategoryID;

		public int NewsCategoryID
		{
			get { return newsCategoryID; }
			set { newsCategoryID = value; }
		}

		private int parentID;

		public int ParentID
		{
			get { return parentID; }
			set { parentID = value; }
		}

        public int SortOrder
        {
            get;
            set;
        }

		private bool isView;

		public bool IsView
		{
			get { return isView; }
			set { isView = value; }
		}

		private long image;

		public long Image
		{
			get { return image; }
			set { image = value; }
		}

        private string uniqueKey;

        public string UniqueKey
        {
            get { return uniqueKey; }
            set { uniqueKey = value; }
        }
        private string keyword;
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
    }
}
