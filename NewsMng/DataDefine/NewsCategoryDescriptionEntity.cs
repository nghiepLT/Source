using System;
namespace NewsMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-08-25
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : NewsCategoryDescriptionEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class NewsCategoryDescriptionEntity
    {
		private int newsCategoryID;

		public int NewsCategoryID
		{
			get { return newsCategoryID; }
			set { newsCategoryID = value; }
		}

		private int languageID;

		public int LanguageID
		{
			get { return languageID; }
			set { languageID = value; }
		}

		private string categoryName;

		public string CategoryName
		{
			get { return categoryName; }
			set { categoryName = value; }
		}

		private string description;

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

        private string subdescription;

        public string SubDescription
        {
            get { return subdescription; }
            set { subdescription = value; }
        }


    }
}
