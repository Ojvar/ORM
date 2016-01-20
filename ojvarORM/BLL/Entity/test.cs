using BLL.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BaseBLL = BLL;

namespace ojvarORM.BLL.Entity
{
	class test : BaseBLL.Entity.Base
	{
		[BaseBLL.Base.Field (nullable=false, sqlDBType=SqlDbType.NVarChar, size=100, validationString="milen:5,mxlen:100,null:F",
			usage=EnumUsage.read | EnumUsage.update | EnumUsage.create)]
		public string name
		{
			get;
			set;
		}
		
		[BaseBLL.Base.Field (nullable=false, sqlDBType=SqlDbType.NVarChar, size=255, validationString="milen:5,mxlen:255,null:T",
			usage=EnumUsage.read | EnumUsage.update | EnumUsage.create)]
		public string family
		{
			get;
			set;
		}

		[BaseBLL.Base.Field (nullable=false, sqlDBType=SqlDbType.Int, size=255, usage=EnumUsage.read | EnumUsage.update | EnumUsage.create)]
		public int age
		{
			get;
			set;
		}
	}
}
