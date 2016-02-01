using System;
using BLL.Base;
using System.Data;
using BaseBLL = BLL;

namespace ojvarORM.BLL.Entity
{
	class HC__Contracts : BaseBLL.Entity.Base
	{
		[BaseBLL.Base.Field (nullable = false, sqlDBType = System.Data.SqlDbType.NVarChar, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create, size = 50)]
		public System.String contractType
		{
			get;
			set;
		}
		[BaseBLL.Base.Field (nullable = true, sqlDBType = System.Data.SqlDbType.Int, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create)]
		public Nullable<System.Int32> code
		{
			get;
			set;
		}
		[BaseBLL.Base.Field (nullable = true, sqlDBType = System.Data.SqlDbType.NVarChar, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create, size = 50)]
		public System.String description
		{
			get;
			set;
		}
		[BaseBLL.Base.Field (nullable = true, sqlDBType = System.Data.SqlDbType.Char, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create, size = 10)]
		public Nullable<System.Char> t1
		{
			get;
			set;
		}
		[BaseBLL.Base.Field (nullable = true, sqlDBType = System.Data.SqlDbType.BigInt, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create)]
		public Nullable<System.Int64> t2
		{
			get;
			set;
		}
		[BaseBLL.Base.Field (nullable = true, sqlDBType = System.Data.SqlDbType.DateTime, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create)]
		public Nullable<System.DateTime> d1
		{
			get;
			set;
		}
	}
}