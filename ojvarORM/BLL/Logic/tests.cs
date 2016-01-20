using DAL.Base;
using BaseBLL = BLL;

namespace ojvarORM.BLL.Logic
{
	class tests : BaseBLL.Logic.Base<BLL.Entity.test>
	{
		public tests (EnumConnectionType type) : base (type)
		{
		}
	}
}
