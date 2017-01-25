using System;
using System.Data.SqlClient;

namespace ojvarORM.BLL.Logic
{
	public class ExceptionLog : BaseBLL.Logic.Base<BLL.Entity.ExceptionLog>
	{
	#region Constants
	#endregion
	
	#region Properties
	#endregion
	
	#region Variables
	#endregion
	
	#region Constructor
		public ExceptionLog (object type) : base (type)
		{
		}

		public ExceptionLog (SqlConnection conn) : base (conn)
		{
		}
	#endregion		
		
	#region Custom Methods
	#endregion
	}
}