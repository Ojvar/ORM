using System;
using System.Data;

namespace BaseBLL.Base
{
	/// <summary>
	/// Field Attributes
	/// </summary>
	public class FieldAttribute : Attribute
	{
		/// <summary>
		/// No Validate apply to that field
		/// </summary>
		/// <returns></returns>
		public bool noValidate { get; set; }

		/// <summary>
		/// Field Name
		/// </summary>
		public string name
		{
			get;
			set;
		}

		/// <summary>
		/// Field Description
		/// </summary>
		public string description
		{
			get;
			set;
		}

		/// <summary>
		/// Field SqlDBType
		/// </summary>
		public SqlDbType sqlDBType
		{
			get;
			set;
		}

		/// <summary>
		/// Field Size
		/// </summary>
		public int size
		{
			get;
			set;
		}

		/// <summary>
		/// Field isNull
		/// </summary>
		public bool nullable
		{
			get;
			set;
		}

		/// <summary>
		/// Field Usage
		/// </summary>
		public EnumUsage usage
		{
			get;
			set;
		}

		/// <summary>
		/// Field Primary
		/// </summary>
		public bool primary
		{
			get;
			set;
		}

		/// <summary>
		/// Field AutoIncreament
		/// </summary>
		public bool autoInc
		{
			get;
			set;
		}

		/// <summary>
		/// Foreign Key Table	-- Logic Type
		/// </summary>
		public Type foreignLogicType
		{
			get;
			set;
		}

		/// <summary>
		/// Foreign Logic Connection
		/// </summary>
		public string foreignLogicConnection
		{
			get;
			set;
		}

		/// <summary>
		/// Foreign Key Field
		/// </summary>
		public string foreignField
		{
			get;
			set;
		}

		/// <summary>
		/// Field validationString
		/// </summary>
		public string validationString
		{
			get;
			set;
		}

		/// <summary>
		/// Field String Quote
		/// </summary>
		public string stringQuote
		{
			get
			{
				string quote	= "";

				if ((this.sqlDBType == SqlDbType.NChar) || (this.sqlDBType == SqlDbType.Char) || 
					(this.sqlDBType == SqlDbType.NVarChar) || (this.sqlDBType == SqlDbType.VarChar) || 
					(this.sqlDBType == SqlDbType.NText) || (this.sqlDBType == SqlDbType.Text))
					quote	= "'";
				
				return quote;
			}
		}

		/// <summary>
		/// Field String Quote
		/// </summary>
		public string stringNChar
		{
			get
			{
				string nvarchar	= "";

				if ((this.sqlDBType == SqlDbType.NChar) || (this.sqlDBType == SqlDbType.NVarChar) || (this.sqlDBType == SqlDbType.NText))
					nvarchar = "N";

				return nvarchar;
			}
		}
	}
}
