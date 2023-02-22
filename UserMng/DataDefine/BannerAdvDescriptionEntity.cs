using System;
namespace UserMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-09-15
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : BannerAdvDescriptionEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class BannerAdvDescriptionEntity
    {
		private int bannerID;

		public int BannerID
		{
			get { return bannerID; }
			set { bannerID = value; }
		}

		private int languageID;

		public int LanguageID
		{
			get { return languageID; }
			set { languageID = value; }
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}


    }
}
