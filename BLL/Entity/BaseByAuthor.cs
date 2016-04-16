using BaseBLL.Base;
using System;

namespace BaseBLL.Entity
{
	/// <summary>
	/// Base By Author Entity
	/// </summary>
	[Serializable]
	public class BaseByAuthor : Base
	{
		[Field (nullable =false, sqlDBType =System.Data.SqlDbType.Int, usage =EnumUsage.create | EnumUsage.read)]
		public int	insertedBy
		{
			get;
			set;
		}

		[Field (nullable =true, sqlDBType =System.Data.SqlDbType.Int, usage =EnumUsage.update | EnumUsage.read)]
		public int	updatedBy
		{
			get;
			set;
		}

		[Field (nullable =false, sqlDBType =System.Data.SqlDbType.DateTime2, usage =EnumUsage.create | EnumUsage.read)]
		public DateTime inserteDate
		{
			get;
			set;
		}

		[Field (nullable =false, sqlDBType =System.Data.SqlDbType.DateTime2, usage =EnumUsage.update | EnumUsage.read)]
		public DateTime updateDate
		{
			get;
			set;
		}
	}
}
