using System;
namespace UserMng.DataDefine
{
    /// =========================================================================
    /// 개발일자 (Created Date) : 2009-08-07
    /// 작성자 (Created By)     : GenTool
    /// 설명 (Description)      : UserEntity
    /// 수정사항 (Change Log)
    ///     날자 (Date)         작성자 (Developer)           내용 (Content)
    ///                                                         
    /// =========================================================================
    public class UserEntity
    {
        private int userLike;
        public int UserLike
        {
            get { return userLike; }
            set { userLike = value; }
        }
		private int userID;

		public int UserID
		{
			get { return userID; }
			set { userID = value; }
		}

        public int UserType
        {
            get;
            set;
        }

		private string loginID;

		public string LoginID
		{
			get { return loginID; }
			set { loginID = value; }
		}

		private string userName;

		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

		private string password;

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		private string tel;

		public string Tel
		{
			get { return tel; }
			set { tel = value; }
		}

		private string fax;

		public string Fax
		{
			get { return fax; }
			set { fax = value; }
		}

		private string email;

		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		private string address;

		public string Address
		{
			get { return address; }
			set { address = value; }
		}

		private string companyName;

		public string CompanyName
		{
			get { return companyName; }
			set { companyName = value; }
		}

		private bool isExpire;

		public bool IsExpire
		{
			get { return isExpire; }
			set { isExpire = value; }
		}

		private DateTime expireDate;

		public DateTime ExpireDate
		{
			get { return expireDate; }
			set { expireDate = value; }
		}

		private string remark;

		public string Remark
		{
			get { return remark; }
			set { remark = value; }
		}

		private DateTime regDate;

		public DateTime RegDate
		{
			get { return regDate; }
			set { regDate = value; }
		}

		private int regUser;

		public int RegUser
		{
			get { return regUser; }
			set { regUser = value; }
		}

		private DateTime modifyDate;

		public DateTime ModifyDate
		{
			get { return modifyDate; }
			set { modifyDate = value; }
		}

		private int modifyUser;

		public int ModifyUser
		{
			get { return modifyUser; }
			set { modifyUser = value; }
		}

		private string permissionString;

		public string PermissionString
		{
			get { return permissionString; }
			set { permissionString = value; }
		}
        private int parentid;
        public int Parentid
        {
            get { return parentid; }
            set { parentid = value; }
        }
        private int gender;
        public int Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private int idNhansu;
        public int IdNhansu
        {
            get { return idNhansu; }
            set { idNhansu = value; }
        }
    }

}
