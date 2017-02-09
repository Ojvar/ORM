using BaseBLL.Base;
using System;

namespace BaseBLL.Entity
{
	/// <summary>
	/// Base By Author Entity
	/// </summary>
	[Serializable]
	public class BaseByViewId : Base
	{
		[Field (nullable =false, sqlDBType =System.Data.SqlDbType.UniqueIdentifier, usage =EnumUsage.read)]
		public Guid viewId
		{
			get;
			set;
		}
	}
}
