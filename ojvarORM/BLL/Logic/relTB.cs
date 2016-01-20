//using DAL.Base;
//using BaseBLL = BLL;

//namespace ojvarORM.BLL.Logic
//{
//	class relTB : BaseBLL.Logic.Base<BLL.Entity.relTB>
//	{
//		public relTB (EnumConnectionType type) : base (type)
//		{
//		}
//	}
//}

using BLL.Base;
using BaseBLL = BLL;

namespace ojvarORM.BLL.Logic
{
	class relTB : BaseBLL.Logic.Base<BLL.Entity.relTB>
	{
		public relTB (DAL.Base.EnumConnectionType type) : base (type)
		{
		}
	}
}
