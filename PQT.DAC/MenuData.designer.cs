﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PQT.DAC
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="justintime_18_10_16")]
	public partial class MenuDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertTMenu(TMenu instance);
    partial void UpdateTMenu(TMenu instance);
    partial void DeleteTMenu(TMenu instance);
    #endregion
		
		public MenuDataDataContext() :
        base(global::PQT.DAC.Properties.Settings.Default.Quanlynhansu_nkcn_2018ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MenuDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MenuDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MenuDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MenuDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TMenu> TMenus
		{
			get
			{
				return this.GetTable<TMenu>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TMenu")]
	public partial class TMenu : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Menu_ID;
		
		private string _Name;
		
		private int _Type;
		
		private System.Nullable<long> _Map_ID;
		
		private int _Status;
		
		private bool _Require_Login;
		
		private int _Sort_Order;
		
		private int _Parent_ID;
		
		private string _Keyword;
		
		private string _Alias_Url;
		
		private System.DateTime _Reg_Date;
		
		private System.DateTime _Update_Date;
		
		private int _Reg_User;
		
		private int _Modify_User;
		
		private string _Image;
		
		private string _Option_1;
		
		private string _Option_2;
		
		private string _Option_3;
		
		private string _Option_4;
		
		private string _Option_5;
		
		private string _Option_6;
		
		private string _Name_2;
		
		private string _Name_3;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMenu_IDChanging(int value);
    partial void OnMenu_IDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnTypeChanging(int value);
    partial void OnTypeChanged();
    partial void OnMap_IDChanging(System.Nullable<long> value);
    partial void OnMap_IDChanged();
    partial void OnStatusChanging(int value);
    partial void OnStatusChanged();
    partial void OnRequire_LoginChanging(bool value);
    partial void OnRequire_LoginChanged();
    partial void OnSort_OrderChanging(int value);
    partial void OnSort_OrderChanged();
    partial void OnParent_IDChanging(int value);
    partial void OnParent_IDChanged();
    partial void OnKeywordChanging(string value);
    partial void OnKeywordChanged();
    partial void OnAlias_UrlChanging(string value);
    partial void OnAlias_UrlChanged();
    partial void OnReg_DateChanging(System.DateTime value);
    partial void OnReg_DateChanged();
    partial void OnUpdate_DateChanging(System.DateTime value);
    partial void OnUpdate_DateChanged();
    partial void OnReg_UserChanging(int value);
    partial void OnReg_UserChanged();
    partial void OnModify_UserChanging(int value);
    partial void OnModify_UserChanged();
    partial void OnImageChanging(string value);
    partial void OnImageChanged();
    partial void OnOption_1Changing(string value);
    partial void OnOption_1Changed();
    partial void OnOption_2Changing(string value);
    partial void OnOption_2Changed();
    partial void OnOption_3Changing(string value);
    partial void OnOption_3Changed();
    partial void OnOption_4Changing(string value);
    partial void OnOption_4Changed();
    partial void OnOption_5Changing(string value);
    partial void OnOption_5Changed();
    partial void OnOption_6Changing(string value);
    partial void OnOption_6Changed();
    partial void OnName_2Changing(string value);
    partial void OnName_2Changed();
    partial void OnName_3Changing(string value);
    partial void OnName_3Changed();
    #endregion
		
		public TMenu()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Menu_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Menu_ID
		{
			get
			{
				return this._Menu_ID;
			}
			set
			{
				if ((this._Menu_ID != value))
				{
					this.OnMenu_IDChanging(value);
					this.SendPropertyChanging();
					this._Menu_ID = value;
					this.SendPropertyChanged("Menu_ID");
					this.OnMenu_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(150) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="Int NOT NULL")]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Map_ID", DbType="BigInt")]
		public System.Nullable<long> Map_ID
		{
			get
			{
				return this._Map_ID;
			}
			set
			{
				if ((this._Map_ID != value))
				{
					this.OnMap_IDChanging(value);
					this.SendPropertyChanging();
					this._Map_ID = value;
					this.SendPropertyChanged("Map_ID");
					this.OnMap_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="Int NOT NULL")]
		public int Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Require_Login", DbType="Bit NOT NULL")]
		public bool Require_Login
		{
			get
			{
				return this._Require_Login;
			}
			set
			{
				if ((this._Require_Login != value))
				{
					this.OnRequire_LoginChanging(value);
					this.SendPropertyChanging();
					this._Require_Login = value;
					this.SendPropertyChanged("Require_Login");
					this.OnRequire_LoginChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sort_Order", DbType="Int NOT NULL")]
		public int Sort_Order
		{
			get
			{
				return this._Sort_Order;
			}
			set
			{
				if ((this._Sort_Order != value))
				{
					this.OnSort_OrderChanging(value);
					this.SendPropertyChanging();
					this._Sort_Order = value;
					this.SendPropertyChanged("Sort_Order");
					this.OnSort_OrderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Parent_ID", DbType="Int NOT NULL")]
		public int Parent_ID
		{
			get
			{
				return this._Parent_ID;
			}
			set
			{
				if ((this._Parent_ID != value))
				{
					this.OnParent_IDChanging(value);
					this.SendPropertyChanging();
					this._Parent_ID = value;
					this.SendPropertyChanged("Parent_ID");
					this.OnParent_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Keyword", DbType="NVarChar(150) NOT NULL", CanBeNull=false)]
		public string Keyword
		{
			get
			{
				return this._Keyword;
			}
			set
			{
				if ((this._Keyword != value))
				{
					this.OnKeywordChanging(value);
					this.SendPropertyChanging();
					this._Keyword = value;
					this.SendPropertyChanged("Keyword");
					this.OnKeywordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Alias_Url", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string Alias_Url
		{
			get
			{
				return this._Alias_Url;
			}
			set
			{
				if ((this._Alias_Url != value))
				{
					this.OnAlias_UrlChanging(value);
					this.SendPropertyChanging();
					this._Alias_Url = value;
					this.SendPropertyChanged("Alias_Url");
					this.OnAlias_UrlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Reg_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Reg_Date
		{
			get
			{
				return this._Reg_Date;
			}
			set
			{
				if ((this._Reg_Date != value))
				{
					this.OnReg_DateChanging(value);
					this.SendPropertyChanging();
					this._Reg_Date = value;
					this.SendPropertyChanged("Reg_Date");
					this.OnReg_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Update_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Update_Date
		{
			get
			{
				return this._Update_Date;
			}
			set
			{
				if ((this._Update_Date != value))
				{
					this.OnUpdate_DateChanging(value);
					this.SendPropertyChanging();
					this._Update_Date = value;
					this.SendPropertyChanged("Update_Date");
					this.OnUpdate_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Reg_User", DbType="Int NOT NULL")]
		public int Reg_User
		{
			get
			{
				return this._Reg_User;
			}
			set
			{
				if ((this._Reg_User != value))
				{
					this.OnReg_UserChanging(value);
					this.SendPropertyChanging();
					this._Reg_User = value;
					this.SendPropertyChanged("Reg_User");
					this.OnReg_UserChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Modify_User", DbType="Int NOT NULL")]
		public int Modify_User
		{
			get
			{
				return this._Modify_User;
			}
			set
			{
				if ((this._Modify_User != value))
				{
					this.OnModify_UserChanging(value);
					this.SendPropertyChanging();
					this._Modify_User = value;
					this.SendPropertyChanged("Modify_User");
					this.OnModify_UserChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Image", DbType="NVarChar(250)")]
		public string Image
		{
			get
			{
				return this._Image;
			}
			set
			{
				if ((this._Image != value))
				{
					this.OnImageChanging(value);
					this.SendPropertyChanging();
					this._Image = value;
					this.SendPropertyChanged("Image");
					this.OnImageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Option_1", DbType="NVarChar(250)")]
		public string Option_1
		{
			get
			{
				return this._Option_1;
			}
			set
			{
				if ((this._Option_1 != value))
				{
					this.OnOption_1Changing(value);
					this.SendPropertyChanging();
					this._Option_1 = value;
					this.SendPropertyChanged("Option_1");
					this.OnOption_1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Option_2", DbType="NVarChar(250)")]
		public string Option_2
		{
			get
			{
				return this._Option_2;
			}
			set
			{
				if ((this._Option_2 != value))
				{
					this.OnOption_2Changing(value);
					this.SendPropertyChanging();
					this._Option_2 = value;
					this.SendPropertyChanged("Option_2");
					this.OnOption_2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Option_3", DbType="NVarChar(250)")]
		public string Option_3
		{
			get
			{
				return this._Option_3;
			}
			set
			{
				if ((this._Option_3 != value))
				{
					this.OnOption_3Changing(value);
					this.SendPropertyChanging();
					this._Option_3 = value;
					this.SendPropertyChanged("Option_3");
					this.OnOption_3Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Option_4", DbType="NVarChar(250)")]
		public string Option_4
		{
			get
			{
				return this._Option_4;
			}
			set
			{
				if ((this._Option_4 != value))
				{
					this.OnOption_4Changing(value);
					this.SendPropertyChanging();
					this._Option_4 = value;
					this.SendPropertyChanged("Option_4");
					this.OnOption_4Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Option_5", DbType="NVarChar(250)")]
		public string Option_5
		{
			get
			{
				return this._Option_5;
			}
			set
			{
				if ((this._Option_5 != value))
				{
					this.OnOption_5Changing(value);
					this.SendPropertyChanging();
					this._Option_5 = value;
					this.SendPropertyChanged("Option_5");
					this.OnOption_5Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Option_6", DbType="NVarChar(250)")]
		public string Option_6
		{
			get
			{
				return this._Option_6;
			}
			set
			{
				if ((this._Option_6 != value))
				{
					this.OnOption_6Changing(value);
					this.SendPropertyChanging();
					this._Option_6 = value;
					this.SendPropertyChanged("Option_6");
					this.OnOption_6Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name_2", DbType="NVarChar(150)")]
		public string Name_2
		{
			get
			{
				return this._Name_2;
			}
			set
			{
				if ((this._Name_2 != value))
				{
					this.OnName_2Changing(value);
					this.SendPropertyChanging();
					this._Name_2 = value;
					this.SendPropertyChanged("Name_2");
					this.OnName_2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name_3", DbType="NVarChar(150)")]
		public string Name_3
		{
			get
			{
				return this._Name_3;
			}
			set
			{
				if ((this._Name_3 != value))
				{
					this.OnName_3Changing(value);
					this.SendPropertyChanging();
					this._Name_3 = value;
					this.SendPropertyChanged("Name_3");
					this.OnName_3Changed();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591