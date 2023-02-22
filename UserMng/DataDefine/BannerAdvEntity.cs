using System;
namespace UserMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-09-15
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : BannerAdvEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class BannerAdvEntity
    {
		private int bannerID;

		public int BannerID
		{
			get { return bannerID; }
			set { bannerID = value; }
		}

		private string url;

		public string Url
		{
			get { return url; }
			set { url = value; }
		}

		private long fileID;

		public long FileID
		{
			get { return fileID; }
			set { fileID = value; }
		}

		private int position;

		public int Position
		{
			get { return position; }
			set { position = value; }
		}

		private bool isActive;

		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; }
		}

		private DateTime regDate;

		public DateTime RegDate
		{
			get { return regDate; }
			set { regDate = value; }
		}

        private int sortorder;

        public int SortOrder
        {
            get { return sortorder; }
            set { sortorder = value; }
        }

    }
}
