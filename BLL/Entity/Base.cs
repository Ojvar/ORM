using BLL.Base;

namespace BLL.Entity
{
	/// <summary>
	/// Base Entity
	/// </summary>
	public class Base
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
