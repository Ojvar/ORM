using BLL.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BaseBLL = BLL;

namespace ojvarORM.BLL.Entity
{
	class relTB : BaseBLL.Entity.Base
	{
		[BaseBLL.Base.Field (nullable = false, sqlDBType = SqlDbType.Int, validationString = "null:F", usage = EnumUsage.read | EnumUsage.update | EnumUsage.create,
			foreignField = "id", foreignLogicType = typeof (BLL.Logic.tests), foreignLogicConnection = DAL.Base.EnumConnectionType.Database1)]
		public int testId
		{
			get;
			set;
		}

		[BaseBLL.Base.Field (nullable = false, sqlDBType = SqlDbType.NVarChar, size = 50, validationString = "milen:1,mxlen:50,null:F",
			usage = EnumUsage.read | EnumUsage.update | EnumUsage.create)]
		public string data1
		{
			get;
			set;
		}
	}
}