using System;
namespace UserMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-09-04
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : WebLinkEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class WebLinkEntity
    {
		private int linkID;

		public int LinkID
		{
			get { return linkID; }
			set { linkID = value; }
		}

		private string linkName;

		public string LinkName
		{
			get { return linkName; }
			set { linkName = value; }
		}

		private string url;

		public string Url
		{
			get { return url; }
			set { url = value; }
		}

        private bool isView;

        public bool IsView
        {
            get { return isView; }
            set { isView = value; }
        }
    }
}
