using System;
using System.Data;
using System.Data.SqlClient;

namespace BaseDAL.Base
{
	/// <summary>
	/// Connection Class
	/// </summary>
	public static class Connection
	{
	#region Properties
        /// <summary>
        /// Sql datasource
        /// </summary>
        public static System.Collections.Generic.Dictionary<string, Model.ConnectionModel> dataSources
        {
            get;
            set;
        }
	#endregion

	#region Constructor
		static Connection ()
		{
            dataSources = new System.Collections.Generic.Dictionary<string, Model.ConnectionModel>();
		}
	#endregion

	#region Public Methods
		/// <summary>
		/// Generate SqlConnection
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static SqlConnection generateConnection (string type, string otherParam = "")
		{
			SqlConnection result = null;

			if ((dataSources != null) && (dataSources.ContainsKey (type)))
				result	= new SqlConnection (dataSources[type].connectionString);

			return result;
		} 
	#endregion
	}
}
