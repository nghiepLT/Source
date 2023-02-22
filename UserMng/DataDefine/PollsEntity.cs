using System;
namespace UserMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-09-03
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : PollsEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class PollsEntity
    {
		private int pollID;

		public int PollID
		{
			get { return pollID; }
			set { pollID = value; }
		}

		private string question;

		public string Question
		{
			get { return question; }
			set { question = value; }
		}

		private bool active;

		public bool Active
		{
			get { return active; }
			set { active = value; }
		}


    }
}
