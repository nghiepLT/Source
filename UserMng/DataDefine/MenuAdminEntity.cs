using System;
namespace UserMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2010-06-18
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : MenuAdminEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class MenuAdminEntity
    {
		private int menuID;

		public int MenuID
		{
			get { return menuID; }
			set { menuID = value; }
		}

		private string menuName;

		public string MenuName
		{
			get { return menuName; }
			set { menuName = value; }
		}

		private string link;

		public string Link
		{
			get { return link; }
			set { link = value; }
		}

		private int menuType;

		public int MenuType
		{
			get { return menuType; }
			set { menuType = value; }
		}

		private string option1;

		public string Option1
		{
			get { return option1; }
			set { option1 = value; }
		}

		private int option2;

		public int Option2
		{
			get { return option2; }
			set { option2 = value; }
		}

		private bool option3;

		public bool Option3
		{
			get { return option3; }
			set { option3 = value; }
		}


    }
}
