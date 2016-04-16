using BaseBLL.Base;
using System;
using System.Reflection;

namespace BaseBLL.Entity
{
	/// <summary>
	/// Base Entity
	/// </summary>
	[Serializable]
	public class Base : BaseModel
	{
	#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public Base ()
		{
		}
	#endregion

	#region Properties
		[Field(nullable=false, sqlDBType=System.Data.SqlDbType.Int, primary=true, usage=EnumUsage.delete | EnumUsage.read | EnumUsage.readCriteria | EnumUsage.updateCriteria)]
		public virtual int id
		{
			get;
			set;
		}
	#endregion
	}
}
