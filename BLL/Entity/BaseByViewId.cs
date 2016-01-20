using BLL.Base;
using System;

namespace BLL.Entity
{
	/// <summary>
	/// Base By Author Entity
	/// </summary>
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
