using BaseBLL.Base;
using System;

namespace BaseBLL.Entity
{
	/// <summary>
	/// BaseByTimestamp Entity
	/// </summary>
	[Serializable]
	public class BaseByTimestamp : Base
	{
	#region Properties
		[Field(nullable=false, sqlDBType=System.Data.SqlDbType.Timestamp, usage=EnumUsage.read)]
		public byte[] timestamp
		{
			get;
			set;
		}
	#endregion
	}
}
