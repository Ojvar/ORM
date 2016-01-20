using BLL.Base;
using System;

namespace BLL.Entity
{
	/// <summary>
	/// BaseByTimestamp Entity
	/// </summary>
	public class BaseByTimestamp : Base
	{
	#region Properties
		[Field(nullable=false, sqlDBType=System.Data.SqlDbType.Timestamp, usage=EnumUsage.create | EnumUsage.read)]
		public virtual byte[] timestamp
		{
			get;
			set;
		}
	#endregion
	}
}
