using BLL.Base;
using BLL.Common;
using BLL.Interface;
using DAL.Base;
using DAL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace BLL.Logic
{
	/// <summary>
	/// Base Logic
	/// </summary>
	public class Base<T> : IBase where T : new()
	{
	#region Variables
		protected SqlConnection	connection	= null;
	#endregion

	#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="type"></param>
		public Base (DAL.Base.EnumConnectionType type)
		{
			// Create SqlConnection
			this.connection	= DAL.Base.Connection.generateConnection (type);
		}
	#endregion

	#region Public - CRUD
		/// <summary>
		/// Create command
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual CommandResult create (object iData, bool closeConnection = true)
		{
			CommandResult	result	= new CommandResult ();

			if ((null != iData) && (iData is T))
			{
				T data	= (T)iData;

				string	command				= "";
				string	fieldName			= "";
				string	fieldValueString	= "";
				string	tableName			= "";
				string	uniqueCol			= getUniqueColumn (iData);

				PropertyInfo[]		properties	= null;
				List<KeyValuePair>	fieldValue	= new List<KeyValuePair> ();

			#region Set Command String
				tableName	= this.GetType ().Name.Replace ("__", ".");
				command	= "INSERT INTO [{0}] ({1}) VALUES ({2}); SELECT * FROM [{0}] " +
					(uniqueCol == "" ? "" : "WHERE ([" + uniqueCol + "] = SCOPE_IDENTITY());");
			#endregion

				// Filter Properties by Usage
				properties	= filterProperties (data.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance), EnumUsage.create);

				if (null != properties)
				{
				#region Prepare insert command parameters
					foreach (PropertyInfo info in properties)
					{
						// Get value
						object	infoData	= info.GetValue (data, null);

						// Make fieldName & fieldValue string
						fieldName			+= ",[" + info.Name + "]";
						fieldValueString	+= ",@" + info.Name;

						fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData)));
					}

					if (!string.IsNullOrWhiteSpace (fieldName))
						fieldName = fieldName.Remove (0, 1);

					if (!string.IsNullOrWhiteSpace (fieldValueString))
						fieldValueString = fieldValueString.Remove (0, 1);
				#endregion

				#region Run Command
					if (null != connection)
					{
						// Setup command string
						command	= string.Format (command, tableName, fieldName, fieldValueString);

						// Run Command
						result	= DAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, closeConnection, fieldValue.ToArray ());

					#region Read new Record data & save into "data"
						if (result.status == EnumCommandStatus.success)
						{
							DataTable	dt	= result.model as DataTable;

							// update data
							if ((dt != null) && (dt.Rows.Count > 0))
								parseInline (data, dt.Rows[0]);
						}
					#endregion
					}
				#endregion
				}
			}
			else
			{
				result.status	= DAL.Base.EnumCommandStatus.executeFailed;
				result.message	= "Error: NULL Data";
			}

			return result;
		}

		/// <summary>
		/// Update Command
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual CommandResult udpate (object iData, bool closeConnection = true)
		{
			CommandResult	result	= new CommandResult ();

			if ((null != iData) && (iData is T))
			{
				T data	= (T)iData;

				string	command			= "";
				string	updateStr		= "";
				string	updateCriteria	= "";
				string	tableName		= "";

				PropertyInfo[]		properties	= null;
				List<KeyValuePair>	fieldValue	= new List<KeyValuePair> ();

			#region Set Command String
				tableName	= this.GetType ().Name.Replace ("__", ".");
				command	= "UPDATE [{0}] SET {1} {2}; SELECT * FROM [{0}] {2};";
			#endregion

				// Filter Properties by Usage
				properties	= filterProperties (data.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance), EnumUsage.update);

				if (null != properties)
				{
				#region Prepare Update command parameters
					foreach (PropertyInfo info in properties)
					{
						// Get value
						object	infoData	= info.GetValue (data, null);

						// Make Update string
						updateStr	+= string.Format (",[{0}] = @{0}", info.Name);
						fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData)));
					}

					if (!string.IsNullOrWhiteSpace (updateStr))
						updateStr = updateStr.Remove (0, 1);
				#endregion

				#region Create Criteria Command
					// Filter Properties by Usage
					properties	= filterProperties (data.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance), EnumUsage.updateCriteria);

					if (null != properties)
					{
						foreach (PropertyInfo info in properties)
						{
							// Get value
							object	infoData	= info.GetValue (data, null);

							// Make Update string
							updateCriteria	+= string.Format ("AND ([{0}] = @{0})", info.Name);
							fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData)));
						}

						if (!string.IsNullOrWhiteSpace (updateCriteria))
							updateCriteria = " WHERE " + updateCriteria.Remove (0, 3);
					}
				#endregion

				#region Run Command
					if (null != connection)
					{
						// Setup command string
						command	= string.Format (command, tableName, updateStr, updateCriteria);

						// Run Command
						result	= DAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, closeConnection, fieldValue.ToArray ());

					#region Read updated Record data & save into "data"
						if (result.status == EnumCommandStatus.success)
						{
							DataTable	dt	= result.model as DataTable;

							// update data
							if ((dt != null) && (dt.Rows.Count > 0))
								parseInline (data, dt.Rows[0]);
						}
					#endregion
					}
				#endregion
				}
			}
			else
			{
				result.status	= DAL.Base.EnumCommandStatus.executeFailed;
				result.message	= "Error: NULL Data";
			}

			return result;
		}

		/// <summary>
		/// Delete Command
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual CommandResult delete (object data, bool closeConnection = true)
		{
			CommandResult	result	= new CommandResult ();

			if (null != data)
			{
				string	command			= "";
				string	deleteCriteria	= "";
				string	tableName		= "";

				PropertyInfo[]		properties	= null;
				List<KeyValuePair>	fieldValue	= new List<KeyValuePair> ();

			#region Set Command String
				tableName	= this.GetType ().Name.Replace ("__", ".");
				command	= "DELETE FROM [{0}] {1}; SELECT @@ROWCOUNT;";
			#endregion

				// Filter Properties by Usage
				properties	= filterProperties (data.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance), EnumUsage.delete);

				if (null != properties)
				{
				#region Create Criteria Command
					if (null != properties)
					{
						foreach (PropertyInfo info in properties)
						{
							// Get value
							object	infoData	= info.GetValue (data, null);

							// Make Update string
							deleteCriteria	+= string.Format ("AND ([{0}] = @{0})", info.Name);
							fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData)));
						}

						if (!string.IsNullOrWhiteSpace (deleteCriteria))
							deleteCriteria = " WHERE " + deleteCriteria.Remove (0, 3);
					}
				#endregion

				#region Run Command
					if (null != connection)
					{
						// Setup command string
						command	= string.Format (command, tableName, deleteCriteria);

						// Run Command
						result	= DAL.DBaseHelper.executeCommand (EnumExecuteType.scaler, connection, command, closeConnection, fieldValue.ToArray ());
					}
				#endregion
				}
			}
			else
			{
				result.status	= DAL.Base.EnumCommandStatus.executeFailed;
				result.message	= "Error: NULL Data";
			}

			return result;
		}

		/// <summary>
		/// Read Command
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual CommandResult read (object data, bool closeConnection = true)
		{
			CommandResult	result	= new CommandResult ();

			if (null != data)
			{
				string	command			= "";
				string	readStr			= "";
				string	readCriteria	= "";
				string	tableName		= "";

				PropertyInfo[]		properties	= null;
				List<KeyValuePair>	fieldValue	= new List<KeyValuePair> ();

			#region Set Command String
				tableName	= this.GetType ().Name.Replace ("__", ".");
				command	= "SELECT {1} FROM [{0}] {2}";
			#endregion

				// Filter Properties by Usage
				properties	= filterProperties (data.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance), EnumUsage.read);

				if (null != properties)
				{
				#region Prepare Read command parameters
					foreach (PropertyInfo info in properties)
						// Make Update string
						readStr	+= string.Format (",[{0}]", info.Name);

					if (!string.IsNullOrWhiteSpace (readStr))
						readStr = readStr.Remove (0, 1);
				#endregion

				#region Create Criteria Command
					// Filter Properties by Usage
					properties	= filterProperties (data.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance), EnumUsage.updateCriteria);

					if (null != properties)
					{
						foreach (PropertyInfo info in properties)
						{
							// Get value
							object	infoData	= info.GetValue (data, null);

							// Make Update string
							readCriteria	+= string.Format ("AND ([{0}] = @{0})", info.Name);
							fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData)));
						}

						if (!string.IsNullOrWhiteSpace (readCriteria))
							readCriteria = " WHERE " + readCriteria.Remove (0, 3);
					}
				#endregion

				#region Run Command
					if (null != connection)
					{
						// Setup command string
						command	= string.Format (command, tableName, readStr, readCriteria);

						// Run Command
						result	= DAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, closeConnection, fieldValue.ToArray ());

					#region Read updated Record data & save into "data"
						if (result.status == EnumCommandStatus.success)
						{
							DataTable	dt	= result.model as DataTable;

							// update data
							if ((dt != null) && (dt.Rows.Count > 0))
								parseInline (data, dt.Rows[0]);
						}
					#endregion
					}
				#endregion
				}
			}
			else
			{
				result.status	= DAL.Base.EnumCommandStatus.executeFailed;
				result.message	= "Error: NULL Data";
			}

			return result;
		}
	#endregion
		
	#region Public - Paging
		/// <summary>
		/// Read by Paging
		/// </summary>
		/// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
		/// <param name="pageSize"></param>
		/// <param name="criteria"></param>
		/// <param name="outputAsList"></param>
		/// <returns></returns>
		public virtual CommandResult allByPaging (string viewName, int pageIndex = 1, int pageSize = 100, string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true)
		{
			CommandResult	result		= new CommandResult ();
			string			command		= "";
			string			tableName	= "";

		#region Setup parameters
			tableName = viewName;
			command = "SELECT TOP 100 PERCENT COUNT (*) OVER () AS totalRows, base.* FROM " +
				" (SELECT {5} FROM [{0}] {1}) AS base WHERE (rowNumber BETWEEN {2} AND {3}) {4}"; 
		#endregion

		#region Prepare command
			command	= string.Format (command,  tableName, 
				(string.IsNullOrWhiteSpace (criteria) ? "" : " WHERE (" + criteria + ")"), 
				((pageIndex - 1) * pageSize), ((pageIndex - 1) * pageSize) + pageSize,
				(string.IsNullOrWhiteSpace (orderBy) ? "" : " ORDER BY " + orderBy),
				(pageIndex > -1 ? " ROW_NUMBER() OVER (ORDER BY ID) AS rowNumber, *" : "*")
				);
		#endregion

		#region Run Command
			if (null != connection)
			{
				result	= DAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, closeConnection);

			#region Add total rows count & remove this column from result
				if ((null != result) && (result.status == EnumCommandStatus.success))
				{
					DataTable	table	= result.model as DataTable;

					if (null != table)
					{
						result.extra	= new Hashtable ();
						result.extra.Add ("TotalRows", (table.Rows.Count > 0 ? 0 : Convert.ToInt32 (table.Rows[0]["totalRows"])));

					#region Remove controll fields
						if (table.Columns.IndexOf ("totalRows") > -1)
							table.Columns.Remove ("totalRows");
						if (table.Columns.IndexOf ("rowNumber") > -1)
							table.Columns.Remove ("rowNumber");
					#endregion

					#region Convert to List
						if (outputAsList)
						{
							List<T>	resultRows	= new List<T>();

							foreach (DataRow row in table.Rows)
								resultRows.Add (parse (row));
							
							// Clear Table Model
							table.Dispose ();

							// Add List Model
							result.model	= resultRows;
						}
					#endregion
					}
				}
			#endregion
			}
			else
			{
				result.status	= EnumCommandStatus.executeFailed;
				result.message	= "Error: Connection null";
			}
		#endregion

			return result;
		}
	
		/// <summary>
		/// Read by Paging
		/// </summary>
		/// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
		/// <param name="pageSize"></param>
		/// <param name="criteria"></param>
		/// <param name="outputAsList"></param>
		/// <returns></returns>
		public virtual CommandResult allByPaging (int pageIndex = 1, int pageSize = 100, string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true)
		{
			CommandResult	result;
			string			viewName;

			// Prepare
			result		= new CommandResult ();
			viewName	= this.GetType ().Name.Replace ("__", ".");

			// Run Query
			result	= allByPaging (viewName, pageIndex, pageSize, criteria, orderBy, outputAsList);

			// Return result
			return result;
		}
	#endregion

	#region Public - Other methods
		/// <summary>
		/// All data
		/// </summary>
		/// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
		/// <param name="pageSize"></param>
		/// <param name="criteria"></param>
		/// <param name="outputAsList"></param>
		/// <returns></returns>
		public virtual CommandResult allData (string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue)
		{
			CommandResult	result		= new CommandResult ();
			string			command		= "";
			string			tableName	= "";

		#region Setup parameters
			tableName = this.GetType ().Name.Replace ("__", ".");
			command = "SELECT * FROM [{0}] {1} {2}"; 
		#endregion

		#region Prepare command
			command	= string.Format (command,  tableName, 
				(string.IsNullOrWhiteSpace (criteria) ? "" : " WHERE (" + criteria + ")"), 
				(string.IsNullOrWhiteSpace (orderBy) ? "" : " ORDER BY " + orderBy)
				);
		#endregion

		#region Run Command
			if (null != connection)
			{
				result	= DAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, closeConnection, fieldValue);

			#region Add total rows count & remove this column from result
				if ((null != result) && (result.status == EnumCommandStatus.success))
				{
					DataTable	table	= result.model as DataTable;

					if (null != table)
					{
						if (table.Rows.Count > 0)
						{
							result.extra	= new Hashtable ();
							result.extra.Add ("TotalRows", (table.Rows.Count > 0 ? 0 : Convert.ToInt32 (table.Rows[0]["totalRows"])));
						}

					#region Remove controll fields
						if (table.Columns.IndexOf ("totalRows") > -1)
							table.Columns.Remove ("totalRows");
						if (table.Columns.IndexOf ("rowNumber") > -1)
							table.Columns.Remove ("rowNumber");
					#endregion

					#region Convert to List
						if (outputAsList)
						{
							List<T>	resultRows	= new List<T>();

							foreach (DataRow row in table.Rows)
								resultRows.Add (parse (row));
							
							// Clear Table Model
							table.Dispose ();

							// Add List Model
							result.model	= resultRows;
						}
					#endregion
					}
				}
			#endregion
			}
			else
			{
				result.status	= EnumCommandStatus.executeFailed;
				result.message	= "Error: Connection null";
			}
		#endregion

			return result;
		}
	#endregion

	#region Public - Validation
		/// <summary>
		/// Validate
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual CommandResult validate (object data)
		{
			CommandResult	result	= new CommandResult ();

			if (data != null)
			{
				// Get Properties
				PropertyInfo[]	properties	= data.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance);

				result.status	= EnumCommandStatus.success;
				result.message	= "Success";

				if (null != properties)
				{
					bool isValid	= true;
					result.model	= new List<string> ();

					foreach (PropertyInfo info in properties)
					{
						object[] fieldAttr	= info.GetCustomAttributes (typeof (FieldAttribute), true);

						if (null != fieldAttr)
						{
							foreach (FieldAttribute attr in fieldAttr)
							{
								ValidationResult	res	= ValidationHelper.validateField (info.Name, info.GetValue (data, null), attr);

								if ((res != null) && (res.message != null) && (res.message.Count > 0))
									((List<string>)result.model).AddRange (res.message);

								isValid	= isValid && (res.message.Count == 0);
							}
						}
					}

					if (!isValid)
					{
						result.message	= "Error";
						result.status	= EnumCommandStatus.operationFailed;
					}
				}
			}
			else
			{
				result.status	= EnumCommandStatus.operationFailed;
				result.message	= "Error :NULL data";
			}

			return result;
		}
	#endregion

	#region Foreign Field
		/// <summary>
		/// Load Foreign Key
		/// </summary>
		/// <param name="iData"></param>
		/// <param name="foreignKey"></param>
		/// <returns></returns>
		public CommandResult loadForeignKey (object iData, string foreignKey, string orderBy = "", bool outputAsList = true, bool closeConnection = true)
        {
			CommandResult	result	= new CommandResult ();

			if ((null != iData) && (iData is T))
			{
				T				data		= (T)iData;
				PropertyInfo	property	= data.GetType ().GetProperty (foreignKey);

				if (null != property)
				{
				#region Create Logic and run query
					object[]	fieldAttr	= property.GetCustomAttributes (typeof (FieldAttribute), true);

					if (null != fieldAttr)
						foreach (FieldAttribute attr in fieldAttr)
							if (null != attr.foreignLogicType) 
							{
								IBase	logic		= Activator.CreateInstance (attr.foreignLogicType, new object[] { attr.foreignLogicConnection }) as IBase;
								string	fieldName	= attr.foreignField;

								if (null != logic)
									result	= logic.allData (string.Format ("([{0}] = @fieldValue)", fieldName), orderBy, outputAsList, closeConnection, new KeyValuePair ("@fieldValue", property.GetValue (data, null)));
							}
				#endregion
				}
			}

			return result;
		}
	#endregion

	#region Class Methods
		/// <summary>
		/// Parse datarow
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public T parse (DataRow row)
		{
			T	result	= new T ();

			if (null != row)
			{
				DataTable		table	= row.Table;
				PropertyInfo[]	info	= result.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance);

				foreach (PropertyInfo inf in info)
					if (table.Columns.IndexOf (inf.Name) > -1)
						inf.SetValue (result, (row[inf.Name].GetType () == typeof (DBNull) ? null : row[inf.Name]), null);
			}

			return result;
		}

		/// <summary>
		/// Parse datarow
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public void parseInline (object data, DataRow row)
		{
			if (null != row)
			{
				DataTable		table	= row.Table;
				PropertyInfo[]	info	= data.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance);

				foreach (PropertyInfo inf in info)
					if (table.Columns.IndexOf (inf.Name) > -1)
						inf.SetValue (data, (row[inf.Name].GetType () == typeof (DBNull) ? null : row[inf.Name]), null);
			}
		}

		/// <summary>
		/// Filter Properties
		/// </summary>
		/// <param name="peroperties"></param>
		/// <param name="usage"></param>
		/// <returns></returns>
		private PropertyInfo[] filterProperties (PropertyInfo[] peroperties, EnumUsage type)
		{
			List<PropertyInfo>	result	= new List<PropertyInfo> ();

			if (null != peroperties)
			{
			#region Filter Properties
				bool addToList	= false;

				foreach (PropertyInfo info in peroperties)
				{
					addToList	= false;

				#region Check Field Attribute
					object[]	filedAttr	= info.GetCustomAttributes (typeof (FieldAttribute), true);

					if (null != filedAttr)
					{
						// Search in FieldAttribute
						foreach (FieldAttribute attr in filedAttr)
							if ((attr.usage & type) == type)
							{
								addToList	= true;
								break;
							}

						// Add to list
						if (addToList)
							result.Add (info);
					}
				#endregion
				}
			#endregion
			}

			return result.ToArray ();
		}

		/// <summary>
		/// Get Unique Column
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public string getUniqueColumn (object data)
		{
			string	result	= "";

			PropertyInfo[]	properties	= filterProperties (data.GetType().GetProperties (BindingFlags.Public | BindingFlags.Instance), EnumUsage.readCriteria);

			if (null != properties)
			{
			#region Get Column
				foreach (PropertyInfo info in properties)
					result	+= "," + info.Name;
			#endregion

				if (result.Length > 0)
					result	= result.Remove (0, 1);
			}

			return result;
		}
	#endregion
	}
}
