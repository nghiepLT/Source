using System;
namespace NewsMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2010-07-06
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : NewsCommentEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class NewsCommentEntity
    {
		private long newsID;

		public long NewsID
		{
			get { return newsID; }
			set { newsID = value; }
		}

		private string commentID;

		public string CommentID
		{
			get { return commentID; }
			set { commentID = value; }
		}

		private string email;

		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		private string author;

		public string Author
		{
			get { return author; }
			set { author = value; }
		}

		private bool status;

		public bool Status
		{
			get { return status; }
			set { status = value; }
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

		private string option1;

		public string Option1
		{
			get { return option1; }
			set { option1 = value; }
		}

		private DateTime regDate;

		public DateTime RegDate
		{
			get { return regDate; }
			set { regDate = value; }
		}


    }
}
