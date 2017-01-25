using BaseDAL.Model;
using System;
using System.Data;

namespace ojvarORM.BLL.Entity
{
	[Serializable]
	public class ExceptionLog : BaseBLL.Entity.BaseByViewId
	{

		[BaseBLL.Base.Field (nullable = false, sqlDBType = System.Data.SqlDbType.VarChar, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create, size = 50)]
		public System.String clientIP
		{
			get;
			set;
		}

		[BaseBLL.Base.Field (nullable = false, sqlDBType = System.Data.SqlDbType.VarChar, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create, size = 50)]
		public System.String clientName
		{
			get;
			set;
		}

		[BaseBLL.Base.Field (nullable = false, sqlDBType = System.Data.SqlDbType.Text, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create, size = 2147483647)]
		public System.String exception
		{
			get;
			set;
		}

		[BaseBLL.Base.Field (nullable = false, sqlDBType = System.Data.SqlDbType.DateTime2, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create)]
		public System.DateTime insertDate
		{
			get;
			set;
		}

		[BaseBLL.Base.Field (nullable = true, sqlDBType = System.Data.SqlDbType.Int, primary = false, usage = BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create)]
		public Nullable<System.Int32> insertById
		{
			get;
			set;
		}
	}
}