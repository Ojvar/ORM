using System.Data;
using System.Data.SqlClient;

namespace DAL.Base
{
	/// <summary>
	/// Connection Class
	/// </summary>
	public static class Connection
	{
	#region Properties
		/// <summary>
		/// Base String
		/// </summary>
		private static string	baseString	= "";
	#endregion

	#region Constructor
		static Connection ()
		{
			baseString	= "Data source={0}; Initial Catalog={1}; User Id={2}; Password={3};";
		}
	#endregion

	#region Public Methods
		/// <summary>
		/// Generate SqlConnection
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static SqlConnection generateConnection (Base.EnumConnectionType type, string otherParam = "")
		{
		#region Variables
			SqlConnection result = null;
			string userName = "";
			string password = "";
			string database = "";
			string dataSource = ""; 
		#endregion

		#region Select database Parameters
			switch (type)
			{
				case EnumConnectionType.Database1:
					userName	= "sa";
					password	= "1365";
					database	= "testDB";
					dataSource	= ".";
					break;
			}
		#endregion

		#region Create Connection
			result	= new SqlConnection (string.Format (baseString + otherParam, dataSource, database, userName, password));
		#endregion

			return result;
		} 
	#endregion
	}
}
