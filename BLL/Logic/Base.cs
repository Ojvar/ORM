﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using BaseBLL.Base;
using BaseBLL.Common;
using BaseBLL.Interface;
using BaseDAL.Base;
using BaseDAL.Model;
using BaseBLL;

namespace BaseBLL.Logic
{
	/// <summary>
	/// Base Logic
	/// </summary>
	public class Base<T> : IBase where T : new()
	{
		#region Constants
		public const string C_FIELD_SEPARATOR = ";,\t\n";
		#endregion

		#region Variables
		/// <summary>
		/// Transaction
		/// </summary>
		public SqlTransaction transactionObject
		{
			get;
			set;
		}

		public static string	C_TOTAL_ROWS {
			get
			{
				return "__totalRows";
			}
		}
		public static string	C_ROW_NUMBER
		{
			get
			{
				return "__rowNumber";
			}
		}
		
		public SqlConnection sqlConnection 
		{
			get
			{
				return connection;
			}
		}

		protected SqlConnection	connection	= null;
		protected string		datasource;
	#endregion

	#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="type"></param>
		public Base (object dataSource, SqlTransaction transactionObject = null)
		{
			// Save Connection type
			if (null == dataSource)
				throw new NullReferenceException ();

			this.datasource	= dataSource.ToString ();

			// Create SqlConnection
			this.connection			= BaseDAL.Base.Connection.generateConnection (this.datasource);
			this.transactionObject	= transactionObject;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="type"></param>
		public Base (SqlConnection connection, SqlTransaction transactionObject = null)
		{
			// Save Connection type
			if (null == connection)
				throw new NullReferenceException ();

			this.datasource	= null;

			// Create SqlConnection
			this.connection			= connection;
			this.transactionObject	= transactionObject;
		}
	#endregion

	#region Public - CRUD
		/// <summary>
		/// Create command
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual CommandResult create (object iData, bool closeConnection = true, string exceptField = "")
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
					string[] exceptFields	= exceptField.Split (C_FIELD_SEPARATOR.ToCharArray (), StringSplitOptions.RemoveEmptyEntries);

					foreach (PropertyInfo info in properties)
					{
						bool addToFields = true;

						// Get value
						object	infoData	= info.GetValue (data, null);

						if (null != exceptFields) 
							addToFields	= (Array.IndexOf (exceptFields, info.Name) == -1);

						if (addToFields)
						{
							// Make fieldName & fieldValue string
							fieldName			+= ",[" + info.Name + "]";
							fieldValueString	+= ",@" + info.Name;

							// Get DB type
							SqlDbType	dbType;
							string		sDbType;

							// Get SqlDBType
							sDbType	= getAttrField (info, "sqlDBType").ToString ();

							if (!sDbType.isNullOrEmptyOrWhiteSpaces ())
							{
								dbType	= (SqlDbType)Enum.Parse (typeof (SqlDbType), sDbType);
								fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData), dbType));
							}
							// NO DBTypeFound
							else
								fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData)));
						}
					}

					if (!fieldName.isNullOrEmptyOrWhiteSpaces ())
						fieldName = fieldName.Remove (0, 1);

					if (!fieldValueString.isNullOrEmptyOrWhiteSpaces ())
						fieldValueString = fieldValueString.Remove (0, 1);
				#endregion

				#region Run Command
					if (null != connection)
					{
						// Setup command string
						command	= string.Format (command, tableName, fieldName, fieldValueString);

						// Run Command
						result	= BaseDAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, transactionObject, closeConnection, fieldValue.ToArray ());

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
				result.status	= BaseDAL.Base.EnumCommandStatus.executeFailed;
				result.message	= "Error: NULL Data";
			}

			return result;
		}

		/// <summary>
		/// Update Command
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual CommandResult update (object iData, bool closeConnection = true, string exceptField = "")
		{
			CommandResult	result	= new CommandResult ();

			if ((null != iData) && (iData is T))
			{
				T data	= (T)iData;

				string[]	exceptFields	= exceptField.Split (C_FIELD_SEPARATOR.ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
				string		command			= "";
				string		updateStr		= "";
				string		updateCriteria	= "";
				string		tableName		= "";

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
						bool addToFields = true;

						// Get value
						object	infoData	= info.GetValue (data, null);

						if (null != exceptFields) 
							addToFields	= (Array.IndexOf (exceptFields, info.Name) == -1);

						if (addToFields)
						{
							// Make Update string
							updateStr	+= string.Format (",[{0}] = @{0}", info.Name);

							// Get DB type
							SqlDbType	dbType;

							dbType	= (SqlDbType)Enum.Parse (typeof (SqlDbType), getAttrField (info, "sqlDBType").ToString ());
							fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData), dbType));
						}
					}

					if (!updateStr.isNullOrEmptyOrWhiteSpaces ())
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
							if (null == infoData)
								updateCriteria	+= string.Format (" AND ([{0}] IS NULL)", info.Name);
							else
								updateCriteria	+= string.Format (" AND ([{0}] = @{0})", info.Name);

							// Get DB type
							SqlDbType	dbType;

							dbType	= (SqlDbType)Enum.Parse (typeof (SqlDbType), getAttrField (info, "sqlDBType").ToString ());
							fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData), dbType));
						}

						if (!updateCriteria.isNullOrEmptyOrWhiteSpaces ())
							updateCriteria = " WHERE " + updateCriteria.Remove (0, 4);
					}
				#endregion

				#region Run Command
					if (null != connection)
					{
						// Setup command string
						command	= string.Format (command, tableName, updateStr, updateCriteria);

						// Run Command
						result	= BaseDAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, transactionObject, closeConnection, fieldValue.ToArray ());

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
				result.status	= BaseDAL.Base.EnumCommandStatus.executeFailed;
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
							if (null == infoData)
								deleteCriteria	+= string.Format (" AND ([{0}] IS NULL)", info.Name);
							else
								deleteCriteria	+= string.Format (" AND ([{0}] = @{0})", info.Name);
							
							// Get DB type
							SqlDbType	dbType;

							dbType	= (SqlDbType)Enum.Parse (typeof (SqlDbType), getAttrField (info, "sqlDBType").ToString ());
							fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData), dbType));
						}

						if (!deleteCriteria.isNullOrEmptyOrWhiteSpaces ())
							deleteCriteria = " WHERE " + deleteCriteria.Remove (0, 4);
					}
					#endregion

					#region Run Command
					if (null != connection)
					{
						// Setup command string
						command	= string.Format (command, tableName, deleteCriteria);

						// Run Command
						result	= BaseDAL.DBaseHelper.executeCommand (EnumExecuteType.scaler, connection, command, transactionObject, closeConnection, fieldValue.ToArray ());
					}
					#endregion
				}
			}
			else
			{
				result.status	= BaseDAL.Base.EnumCommandStatus.executeFailed;
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

					if (!readStr.isNullOrEmptyOrWhiteSpaces ())
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
							if (null == infoData)
								readCriteria	+= string.Format (" AND ([{0}] IS NULL)", info.Name);
							else
								readCriteria	+= string.Format (" AND ([{0}] = @{0})", info.Name);
							
							// Get DB type
							SqlDbType	dbType;

							dbType	= (SqlDbType)Enum.Parse (typeof (SqlDbType), getAttrField (info, "sqlDBType").ToString ());
							fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData), dbType));
						}

						if (!readCriteria.isNullOrEmptyOrWhiteSpaces ())
							readCriteria = " WHERE " + readCriteria.Remove (0, 4);
					}
				#endregion

				#region Run Command
					if (null != connection)
					{
						// Setup command string
						command	= string.Format (command, tableName, readStr, readCriteria);

						// Run Command
						result	= BaseDAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, transactionObject, closeConnection, fieldValue.ToArray ());

					#region Read updated Record data & save into "data"
						if (result.status == EnumCommandStatus.success)
						{
							DataTable	dt	= result.model as DataTable;

							// update data
							if ((dt != null) && (dt.Rows.Count > 0))
								parseInline (data, dt.Rows[0]);

							result.model	= dt;
						}
					#endregion
					}
				#endregion
				}
			}
			else
			{
				result.status	= BaseDAL.Base.EnumCommandStatus.executeFailed;
				result.message	= "Error: NULL Data";
			}

			return result;
		}
		
		/// <summary>
		/// Read Command
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual CommandResult read (object data, string field, string criteria = "", bool closeConnection = true)
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

					if (!readStr.isNullOrEmptyOrWhiteSpaces ())
						readStr = readStr.Remove (0, 1);
				#endregion

				#region Create Criteria Command
					if (null != properties)
					{
						string[]	fields	= field.Split (new string[] {",", ";", ""}, StringSplitOptions.RemoveEmptyEntries);

						foreach (string fieldItem in fields)
						{
							PropertyInfo	info	= data.GetType ().GetProperty (fieldItem.Trim ());

							if (null != info)
							{
								// Get value
								object	infoData	= info.GetValue (data, null);

								// Make Update string
								if (null == infoData)
									readCriteria	+= string.Format (" AND ([{0}] IS NULL)", info.Name);
								else
									readCriteria	+= string.Format (" AND ([{0}] = @{0})", info.Name);
							
								// Get DB type
								SqlDbType	dbType;

								dbType	= (SqlDbType)Enum.Parse (typeof (SqlDbType), getAttrField (info, "sqlDBType").ToString ());
								fieldValue.Add (new KeyValuePair ("@" + info.Name, (null == infoData ? DBNull.Value : infoData), dbType));
							}
						}

						// Add User Criterial
						if (!criteria.isNullOrEmptyOrWhiteSpaces ())
							readCriteria	= " AND " + criteria + " " + readCriteria;
						if (!readCriteria.isNullOrEmptyOrWhiteSpaces ())
							readCriteria = " WHERE " + readCriteria.Remove (0, 4);
					}
				#endregion

				#region Run Command
					if (null != connection)
					{
						// Setup command string
						command	= string.Format (command, tableName, readStr, readCriteria);

						// Run Command
						result	= BaseDAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, transactionObject, closeConnection, fieldValue.ToArray ());

					#region Read updated Record data & save into "data"
						if (result.status == EnumCommandStatus.success)
						{
							DataTable	dt	= result.model as DataTable;

							// update data
							if ((dt != null) && (dt.Rows.Count > 0))
								parseInline (data, dt.Rows[0]);

							result.model	= dt;
						}
					#endregion
					}
				#endregion
				}
			}
			else
			{
				result.status	= BaseDAL.Base.EnumCommandStatus.executeFailed;
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
		public virtual CommandResult allByPaging (int pageIndex, int pageSize, string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValues)
		{
			CommandResult result = null;
            string tableName = this.GetType().Name;

            result = allByViewNameByPaging(tableName, pageIndex, pageSize, criteria, orderBy, false, outputAsList, closeConnection, fieldValues);

            return result;
		}

		/// <summary>
		/// Read by Paging - Distinct
		/// </summary>
		/// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
		/// <param name="pageSize"></param>
		/// <param name="criteria"></param>
		/// <param name="outputAsList"></param>
		/// <returns></returns>
		public virtual CommandResult allByPagingDistinct (int pageIndex, int pageSize, string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValues)
		{
			CommandResult result = null;
            string tableName = this.GetType().Name;

            result = allByViewNameByPaging(tableName, pageIndex, pageSize, criteria, orderBy, true, outputAsList, closeConnection, fieldValues);

            return result;
		}

        /// <summary>
		/// Read by viewname by Paging
		/// </summary>
		/// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
		/// <param name="pageSize"></param>
		/// <param name="criteria"></param>
		/// <param name="outputAsList"></param>
		/// <returns></returns>
		public virtual CommandResult allByViewNameByPaging(string viewName, int pageIndex, int pageSize, string criteria = "",
			string orderBy = "", bool distinct = false, bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValues)
        {
            CommandResult	result			= new CommandResult();
            string			command			= "";
            string			commandDistinct = "";
            string			tableName		= "";

        #region Setup parameters
            tableName = viewName.Replace("__", ".");
            command = "SELECT TOP 100 PERCENT base.* FROM " +
                " (SELECT {6}, {5} FROM [{0}] {1}) AS base WHERE ({7} BETWEEN {2} AND {3}) {4}";
            commandDistinct = "SELECT TOP 100 PERCENT base.* FROM " +
                " (SELECT {6}, {5} FROM (SELECT DISTINCT * FROM [{0}] {1}) AS distBase) AS base WHERE ({7} BETWEEN {2} AND {3}) {4}";
        #endregion

        #region Prepare command
			pageIndex	= Math.Max (1, pageIndex);
			pageSize	= Math.Max (1, pageSize);

			int	startRow	= ((pageIndex - 1) * pageSize) + 1;
			int	endRow		= startRow + (pageSize - 1);

            command = string.Format((distinct ? commandDistinct : command ),
				tableName,
                (criteria.isNullOrEmptyOrWhiteSpaces () ? "" : " WHERE (" + criteria + ")"),
                startRow, 
				endRow,
                (orderBy.isNullOrEmptyOrWhiteSpaces () ? "" : " ORDER BY " + orderBy),
                (pageIndex > -1 ? " ROW_NUMBER() OVER (ORDER BY ID) AS " + C_ROW_NUMBER + ", *" : "*"),
				" COUNT (*) OVER () AS " + C_TOTAL_ROWS, 
				C_ROW_NUMBER
            );
        #endregion

        #region Run Command
            if (null != connection)
            {
                result = BaseDAL.DBaseHelper.executeCommand(EnumExecuteType.reader, connection, command, transactionObject, closeConnection, fieldValues);

            #region Add total rows count & remove this column from result
                if ((null != result) && (result.status == EnumCommandStatus.success))
                {
                    DataTable table = result.model as DataTable;

                    if (null != table)
                    {
                        if ((table.Rows.Count > 0) && (table.Columns.Contains (C_TOTAL_ROWS)))
						{
							Hashtable extraData;
							extraData		= new Hashtable ();
							result.extra	= extraData;
							extraData.Add (C_TOTAL_ROWS, Convert.ToInt32 (table.Rows[0][C_TOTAL_ROWS]));
						}

                    #region Remove controll fields
                        if (table.Columns.IndexOf(C_TOTAL_ROWS) > -1)
                            table.Columns.Remove(C_TOTAL_ROWS);
                        if (table.Columns.IndexOf(C_ROW_NUMBER) > -1)
                            table.Columns.Remove(C_ROW_NUMBER);
                    #endregion

                    #region Convert to List
                        if (outputAsList)
                        {
                            List<T> resultRows = new List<T>();

                            foreach (DataRow row in table.Rows)
                                resultRows.Add(parse(row));

                            // Clear Table Model
                            table.Dispose();

                            // Add List Model
                            result.model = resultRows;
                        }
                    #endregion
                    }
                }
            #endregion
            }
            else
            {
                result.status = EnumCommandStatus.executeFailed;
                result.message = "Error: Connection null";
            }
        #endregion

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
        public virtual CommandResult rawQuery (string command, bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue)
		{
			CommandResult	result = new CommandResult ();

		#region Run Command
			if (null != connection)
			{
				result	= BaseDAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, transactionObject, closeConnection, fieldValue);

			#region Add total rows count & remove this column from result
				if ((null != result) && (result.status == EnumCommandStatus.success))
				{
					DataTable	table	= result.model as DataTable;

					if (null != table)
					{
						if ((table.Rows.Count > 0) && (table.Columns.Contains (C_TOTAL_ROWS)))
						{
							Hashtable extraData;
							extraData		= new Hashtable ();
							result.extra	= extraData;
							extraData.Add (C_TOTAL_ROWS, Convert.ToInt32 (table.Rows[0][C_TOTAL_ROWS]));
						}

					#region Remove controll fields
						if (table.Columns.IndexOf (C_TOTAL_ROWS) > -1)
							table.Columns.Remove (C_TOTAL_ROWS);
						if (table.Columns.IndexOf (C_ROW_NUMBER) > -1)
							table.Columns.Remove (C_ROW_NUMBER);
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
        /// All data
        /// </summary>
        /// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="outputAsList"></param>
        /// <returns></returns>
        public virtual CommandResult allDataByViewName (string viewName, string fields, string criteria = "", string orderBy = "", bool distinct = false, bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue)
		{
			CommandResult	result			= new CommandResult ();
			string			command			= "";
			string			commandDistinct	= "";
			string			tableName		= "";

		#region Setup parameters
			tableName		= viewName.Replace ("__", ".");
			command			= "SELECT {3} FROM [{0}] {1} {2}"; 
			commandDistinct = "SELECT * FROM (SELECT DISTINCT {3} FROM [{0}] {1}) AS base {2}"; 
		#endregion

		#region Prepare command
			command	= string.Format ((distinct ? commandDistinct : command),
				tableName, 
				(criteria.isNullOrEmptyOrWhiteSpaces () ? "" : " WHERE (" + criteria + ")"), 
				(orderBy.isNullOrEmptyOrWhiteSpaces () ? "" : " ORDER BY " + orderBy),
				fields
			);
		#endregion

		#region Run Command
			if (null != connection)
			{
				result	= BaseDAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, transactionObject, closeConnection, fieldValue);

			#region Add total rows count & remove this column from result
				if ((null != result) && (result.status == EnumCommandStatus.success))
				{
					DataTable	table	= result.model as DataTable;

					if (null != table)
					{
						if ((table.Rows.Count > 0) && (table.Columns.Contains (C_TOTAL_ROWS)))
						{
							Hashtable extraData;
							extraData		= new Hashtable ();
							result.extra	= extraData;
							extraData.Add (C_TOTAL_ROWS, Convert.ToInt32 (table.Rows[0][C_TOTAL_ROWS]));
						}

					#region Remove controll fields
						if (table.Columns.IndexOf (C_TOTAL_ROWS) > -1)
							table.Columns.Remove (C_TOTAL_ROWS);
						if (table.Columns.IndexOf (C_ROW_NUMBER) > -1)
							table.Columns.Remove (C_ROW_NUMBER);
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
        /// All data
        /// </summary>
        /// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="outputAsList"></param>
        /// <returns></returns>
        public virtual CommandResult allData (string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue)
		{
            CommandResult   result    = null;
			string          tableName = this.GetType ().Name.Replace ("__", ".");

            result = allDataByViewName(tableName, "*", criteria, orderBy, false, outputAsList, closeConnection, fieldValue);

			return result;
		}


        /// <summary>
        /// All data - Distinct
        /// </summary>
        /// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="outputAsList"></param>
        /// <returns></returns>
        public virtual CommandResult allDataDistinct (string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue)
		{
            CommandResult   result    = null;
			string          tableName = this.GetType ().Name.Replace ("__", ".");

            result = allDataByViewName(tableName, "*", criteria, orderBy, true, outputAsList, closeConnection, fieldValue);

			return result;
		}

        /// <summary>
        /// All data - Distinct
        /// </summary>
        /// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="outputAsList"></param>
        /// <returns></returns>
        public virtual CommandResult allDataDistinct (string field = "*", string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue)
		{
            CommandResult   result    = null;
			string          tableName = this.GetType ().Name.Replace ("__", ".");

            result = allDataByViewName(tableName, field, criteria, orderBy, true, outputAsList, closeConnection, fieldValue);

			return result;
		}

		/// <summary>
		/// All Data
		/// </summary>
		/// <param name="fields"></param>
		/// <param name="criteria"></param>
		/// <param name="orderBy"></param>
		/// <param name="distinct"></param>
		/// <param name="outputAsList"></param>
		/// <param name="closeConnection"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
		public virtual CommandResult allDataBySpecifiedFields (string fields, string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue)
		{
			CommandResult	result	= null;

			result	= allDataBySpecifiedFieldsDistinct (fields, criteria, orderBy, false, outputAsList, closeConnection, fieldValue);

			return result;
		}

        /// <summary>
        /// All data Distinct or not
        /// </summary>
        /// <param name="pageIndex">-1 : returns all rows, otherwise : results by paging</param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="outputAsList"></param>
        /// <returns></returns>
        public virtual CommandResult allDataBySpecifiedFieldsDistinct (string fields, string criteria = "", string orderBy = "", bool distinct = false, bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue)
		{
			CommandResult	result			= new CommandResult ();
			string			command			= "";
			string			commandDistinct	= "";
			string			tableName		= "";
			string[]		fieldsArr		= fields.Split (C_FIELD_SEPARATOR.ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
			
		#region Validation
			if ((null == fieldsArr) || (fieldsArr.Length == 0))
			{
				result.status	= EnumCommandStatus.operationFailed;
				result.message	= "Field(s) data can't be null or empty!";
				result.id		= 1;

				// Return
				return result;
			}
		#endregion

		#region Setup parameters
			tableName   = GetType ().Name.Replace ("__", ".");
			command				= "SELECT {3} FROM [{0}] {1} {2}"; 
			commandDistinct     = "SELECT * FROM (SELECT DISTINCT {3} FROM [{0}] {1}) AS base {2}"; 
		#endregion

		#region Prepare command
			command	= string.Format ((distinct ? commandDistinct : command ),  tableName, 
				(criteria.isNullOrEmptyOrWhiteSpaces () ? "" : " WHERE (" + criteria + ")"), 
				(orderBy.isNullOrEmptyOrWhiteSpaces () ? "" : " ORDER BY " + orderBy),
				string.Join (",", fieldsArr)
				);
		#endregion

		#region Run Command
			if (null != connection)
			{
				result	= BaseDAL.DBaseHelper.executeCommand (EnumExecuteType.reader, connection, command, transactionObject, closeConnection, fieldValue);

			#region Add total rows count & remove this column from result
				if ((null != result) && (result.status == EnumCommandStatus.success))
				{
					DataTable	table	= result.model as DataTable;

					if (null != table)
					{
						if ((table.Rows.Count > 0) && (table.Columns.Contains (C_TOTAL_ROWS)))
						{
							Hashtable extraData;
							extraData		= new Hashtable ();
							result.extra	= extraData;
							extraData.Add (C_TOTAL_ROWS, Convert.ToInt32 (table.Rows[0][C_TOTAL_ROWS]));
						}

					#region Remove controll fields
						if (table.Columns.IndexOf (C_TOTAL_ROWS) > -1)
							table.Columns.Remove (C_TOTAL_ROWS);
						if (table.Columns.IndexOf (C_ROW_NUMBER) > -1)
							table.Columns.Remove (C_ROW_NUMBER);
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

			#region Default
				result.id = 0;
				result.status = EnumCommandStatus.success;
				result.message = "Success"; 
			#endregion

				if (null != properties)
				{
					bool			isValid	= true;
					List<string>	valResult	= new List<string> ();
					
					// Validate fields
					foreach (PropertyInfo info in properties)
					{
						ValidationResult	res	= ValidationHelper.validateField (info, info.GetValue (data, null));

						if ((res != null) && (res.message != null) && (res.message.Count > 0))
							valResult.AddRange (res.message);
					}

					// Set output message
					isValid	= (valResult.Count == 0);
					result.model	= valResult;

					if (!isValid)
					{
						result.id		= 2;
						result.message	= "Error";
						result.status	= EnumCommandStatus.operationFailed;
					}
				}
			}
			else
			{
				result.id		= 1;
				result.status	= EnumCommandStatus.operationFailed;
				result.message	= "Error :NULL data";
			}

			return result;
		}

        /// <summary>
        /// Validate size
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool validateSize (object model, string propertyName)
        {
            bool	result	= false;
			int		len		= 0;

			// Get Property
			PropertyInfo	prop	= model.GetType ().GetProperty (propertyName, BindingFlags.Public | BindingFlags.Instance);

			if (null != prop)
			{
				object[]	cProp	= prop.GetCustomAttributes (typeof (BaseBLL.Base.FieldAttribute), true);
				object		data	= prop.GetValue (model, null);

				// Get data value
				if (data is Array)
					len		= ((Array)data).Length;
				else if (data is string)
					len		= data.ToString ().Length;
				else
					throw new InvalidCastException ();

				// Valdate size
				if (null != prop)
					foreach (FieldAttribute attr in cProp)
						result	|= (attr.size > len);
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

		#region Stop Transaction
		/// <summary>
		/// Begin Transaction
		/// </summary>
		/// <param name="commitOldTransaction"></param>
		public void beginTransaction (bool commitOldTransaction = false)
		{
			// Old Transaction
			stopTransaction (commitOldTransaction);

			if (sqlConnection?.State != ConnectionState.Open)
				sqlConnection.Open ();

			transactionObject   = this.sqlConnection?.BeginTransaction ();
		}

		/// <summary>
		/// Stop Transaction
		/// </summary>
		/// <param name="commitOldTransaction"></param>
		/// <returns></returns>
		public CommandResult stopTransaction (bool commitOldTransaction = false)
		{
			CommandResult result;

			try
			{
				if (commitOldTransaction)
					commitTransaction ();
				else
					rollBackTransaction ();

				transactionObject?.Dispose ();

				result  = CommandResult.makeSuccessResult ("OK");
			}
			catch (Exception ex)
			{
				result  = CommandResult.makeErrorResult (ex.Message, ex);
			}

			return result;
		}

		/// <summary>
		/// Rollback Transaction
		/// </summary>
		/// <param name="commitOldTransaction"></param>
		public CommandResult rollBackTransaction ()
		{
			CommandResult result;

			try
			{
				transactionObject?.Rollback ();

				result  = CommandResult.makeSuccessResult ("OK", transactionObject);
			}
			catch (Exception ex)
			{
				result  = CommandResult.makeErrorResult (ex.Message, ex, transactionObject);
			}

			return result;
		}

		/// <summary>
		/// Commit Transaction
		/// </summary>
		/// <param name="commitOldTransaction"></param>
		public CommandResult commitTransaction ()
		{
			CommandResult result;

			try
			{
				transactionObject?.Commit ();
				result  = CommandResult.makeSuccessResult ("OK", transactionObject);
			}
			catch (Exception ex)
			{
				result  = CommandResult.makeErrorResult (ex.Message, ex, transactionObject);
			}

			return result;
		}
		#endregion

		#region Class Methods
		/// <summary>
		/// Get Field Attribute value
		/// </summary>
		/// <returns></returns>
		public object getFieldAttributeValue (string propertyName, string attrName)
		{
			object			result	= null;
			PropertyInfo	prop	= typeof (T).GetProperty (propertyName, BindingFlags.Public | BindingFlags.Instance);

			if (null != prop)
			{
				object[]	attrs	= prop.GetCustomAttributes (typeof (BaseBLL.Base.FieldAttribute), true);

				if (null != attrs)
					foreach (BaseBLL.Base.FieldAttribute attr in attrs)
					{
						PropertyInfo	pInfo	= attr.GetType ().GetProperty (attrName, BindingFlags.Public | BindingFlags.Instance);

						if (null != pInfo)
							result	= pInfo.GetValue (attr, null);
					}
			}

			return result;
		}

		/// <summary>
		/// Copy model
		/// </summary>
		/// <param name="mode"></param>
		public void copyTo (ref object model)
		{
			if ((null != model) && (model is T))
			{
				PropertyInfo	sourceInfo;
				PropertyInfo[]	targetProp;
				T				tModel;

				// Init
				tModel	= (T)model;
				targetProp	= tModel.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance);

				// Try to copy
				foreach (PropertyInfo destInfo in targetProp)
				{
					// Init
					sourceInfo	= this.GetType ().GetProperty (destInfo.Name);

					// try to copy
					destInfo.SetValue (tModel, sourceInfo.GetValue (this, null), null);
				}
			}
		}

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

				try
				{
					foreach (PropertyInfo inf in info)
						if (table.Columns.IndexOf (inf.Name) > -1)
							inf.SetValue (result, (row[inf.Name].GetType () == typeof (DBNull) ? null : row[inf.Name]), null);
				}
				catch (Exception ex)
				{
					/// TODO: Log Error
				}
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
					try
					{
						if (table.Columns.IndexOf (inf.Name) > -1)
							inf.SetValue (data, (row[inf.Name].GetType () == typeof (DBNull) ? null : row[inf.Name]), null);
					}
					catch (Exception ex)
					{
						/// TODO: Log Error
					}
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

		/// <summary>
		/// Get an attribute value
		/// </summary>
		/// <param name="attrName"></param>
		/// <returns></returns>
		private object getAttrField (PropertyInfo data, string attrName)
		{
			object		result;
			object[]	fieldAttr;

			// Init
			fieldAttr	= data.GetCustomAttributes (typeof (FieldAttribute), true);
			result		= null;

			if (null != fieldAttr)
				foreach (FieldAttribute attr in fieldAttr)
					result	= attr.GetType ().GetProperty (attrName).GetValue (attr, null);

			return result;
		}
		

		/// <summary>
		/// Convert datatable to list
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public List<T> tableToList (DataTable table)
		{
			List<T> result = new List<T> ();

			if (null != table)
				foreach (DataRow row in table.Rows)
				{
					T	entity	= parse (row);

					if (null != entity)
						result.Add (entity);
				}

			return result;
		}
	#endregion
	}
}