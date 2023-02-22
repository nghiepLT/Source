using System;
namespace NewsMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-08-03
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : NewsDescriptionEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class NewsDescriptionEntity
    {
		private long newsID;

		public long NewsID
		{
			get { return newsID; }
			set { newsID = value; }
		}

		private int languageID;

		public int LanguageID
		{
			get { return languageID; }
			set { languageID = value; }
		}

		private string title;

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		private string content;

		public string Content
		{
			get { return content; }
			set { content = value; }
		}

		private string subTitle;

		public string SubTitle
		{
			get { return subTitle; }
			set { subTitle = value; }
		}

		private string subContent;

		public string SubContent
		{
			get { return subContent; }
			set { subContent = value; }
		}

		private string comment;

		public string Comment
		{
			get { return comment; }
			set { comment = value; }
		}


    }
}
