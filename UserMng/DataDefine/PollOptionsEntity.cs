using System;
namespace UserMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-09-03
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : PollOptionsEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class PollOptionsEntity
    {
		private int pollOptionID;

		public int PollOptionID
		{
			get { return pollOptionID; }
			set { pollOptionID = value; }
		}

		private int pollID;

		public int PollID
		{
			get { return pollID; }
			set { pollID = value; }
		}

		private string answer;

		public string Answer
		{
			get { return answer; }
			set { answer = value; }
		}

		private int votes;

		public int Votes
		{
			get { return votes; }
			set { votes = value; }
		}


    }
}
