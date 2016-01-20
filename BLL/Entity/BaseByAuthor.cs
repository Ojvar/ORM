using BLL.Base;
using System;

namespace BLL.Entity
{
	/// <summary>
	/// Base By Author Entity
	/// </summary>
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
