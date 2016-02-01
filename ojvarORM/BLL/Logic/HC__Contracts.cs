using System;
using BLL.Base;
using BaseBLL = BLL;

namespace ojvarORM.BLL.Logic
{
	class HC__Contracts : BaseBLL.Logic.Base<BLL.Entity.HC__Contracts>
	{
		public HC__Contracts (DAL.Base.EnumConnectionType type) : base (type)
		{
		}
	}
}