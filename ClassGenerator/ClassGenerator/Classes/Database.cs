using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Classes
{
	/// <summary>
	/// Database Class
	/// </summary>
	public class Database : Base
	{
	#region Variables
		private SqlConnection	connection;
		private List<Table>		tables;

		public const string	C_Type	= "Database";
	#endregion

	#region Properties Methods
		/// <summary>
		/// Set Connection
		/// </summary>
		/// <param name="value"></param>
		public void setConnection (SqlConnection value)
		{
			connection	= value;
		}
		/// <summary>
		/// Get Connection
		/// </summary>
		/// <returns></returns>
		public SqlConnection getConnection ()
		{
			return connection;
		}
	#endregion

	#region Methods
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="autoLoad"></param>
		public Database (SqlConnection connection, string name, bool autoLoad = true) : base (C_Type, name, autoLoad)
		{
			if (null == connection)
				throw new NullReferenceException ("Connection is null!");
			
			setConnection (connection);
		}

		/// <summary>
		/// Get Table by name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Table this[string name]
		{
			get
			{
				List<Table>	tables	= getTables ();

				return (from t in tables where (t.getName ().Equals (name)) select t).FirstOrDefault ();
			}
		}
		/// <summary>
		/// Get Table by index
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Table this[int index]
		{
			get
			{
				List<Table>	tables	= getTables ();

				if (index >= tables.Count)
					throw new IndexOutOfRangeException ();

				return tables[index];
			}
		}

		/// <summary>
		/// Load information
		/// </summary>
		public MethodResult loadTables ()
		{
			MethodResult	result		= new MethodResult ();
			SqlConnection	connection	= getConnection ();

			// Check Connection
			if (null == connection)
				result.status	= MethodResult.Result.failed;
			else
			{
				// Server ready?
				if (!DAL.DBaseHelper.IsServerConnected (connection))
					result.status	= MethodResult.Result.failed;
				else
				{
					if (null == tables)
						tables	= new List<Table> ();	// Create new instance of list
					else
						clearTables ();					// Clear current tables list

				#region Load tables information
					// Generate command
					string	cmd	= string.Format (Resources.Database.GetTables, getName ());

					// Run
					CommandResult	dRes	= DAL.DBaseHelper.executeCommand (DAL.Base.EnumExecuteType.reader, connection, cmd, true);

					if (dRes.status == DAL.Base.EnumCommandStatus.success)
					{
						if (dRes.model is DataTable)
						{
							DataTable	table	= dRes.model as DataTable;

							foreach (DataRow row in table.Rows)
							{
								string	tableName;
								Table	newTable;

								tableName	= row["TABLE_NAME"].ToString ();
								newTable	= new Table (this, tableName, getAutoload ());
								tables.Add (newTable);
							}
						}

						result.status	= MethodResult.Result.success;
					}
					else
						result.status	= MethodResult.Result.failed;
				#endregion
				}
			}

			return result;
		}

		/// <summary>
		/// Get Tables
		/// </summary>
		/// <returns></returns>
		public List<Table> getTables ()
		{
			if ((null == tables) && autoLoad)
				loadTables ();
			
			return tables;
		}

		/// <summary>
		/// Clear tables
		/// </summary>
		private void clearTables ()
		{
			if (null != tables)
				tables.Clear ();
		}
	#endregion
	}
}
