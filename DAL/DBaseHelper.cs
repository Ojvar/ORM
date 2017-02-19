using BaseDAL.Base;
using BaseDAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BaseDAL
{
	/// <summary>
	/// Database Helper
	/// </summary>
	public static class DBaseHelper
	{
	    #region Variables
		/// <summary>
		/// Sync Object
		/// </summary>
		private static object objSync = new object ();
		#endregion

		#region Methods
		/// <summary>
		/// Check Connection
		/// </summary>
		/// <param name="connection"></param>
		/// <returns></returns>
		public static bool IsServerConnected (SqlConnection connection)
		{
			bool	result;

			try
			{
				if (null == connection)
					result	= false;
				else
				{
					connection.Open ();
					connection.Close ();
					result	= true;
				}
			}
			catch (Exception ex)
			{
				result = false;
			}

			return result;
		}
		/// <summary>
		/// Check Connection -by String
		/// </summary>
		/// <param name="connection"></param>
		/// <returns></returns>
		public static bool IsServerConnected (string connectionString)
		{
			bool	result;

			result = IsServerConnected (new SqlConnection (connectionString));

			return result;
		}

		/// <summary>
		/// Execute Scaler
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="command"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static CommandResult executeCommand (EnumExecuteType executeType, SqlConnection connection, string commandString, bool closeConnection = true, params KeyValuePair[] parameters)
		{
			return executeCommand (executeType, connection, commandString, closeConnection, null, parameters);
		}

		public static CommandResult executeCommand (EnumExecuteType executeType, SqlConnection connection, string commandString, bool closeConnection = true, SqlTransaction transactionObject = null, params KeyValuePair[] parameters)
		{
			CommandResult	result	= new CommandResult ();

			try
			{
				lock (objSync)
				{
					const string	C_RET_VALUE	= "@__retValue";

					// Disable close connection within transactional mode
					closeConnection	= (transactionObject == null) && closeConnection;

					if (null != connection)
					{
						#region Run Command
						try
						{
							SqlCommand	command		= null;

							#region Prepare command
							// Create command
							if (null != transactionObject)
								command	= new SqlCommand (commandString, transactionObject.Connection);
                            else
								command	= new SqlCommand (commandString, connection);
                            command.Transaction	= transactionObject;

							// Add parameters
							if (null != parameters)
								foreach (KeyValuePair key in parameters)
									if (null != key)
									{
										key.value	= (key.value == null ? DBNull.Value : key.value);

										if (!(key.metadata is SqlDbType))
											command.Parameters.Add (new SqlParameter (key.key, key.value)
											{
												Direction	= key.direction
											});
										else
										{
											SqlDbType	dbType	= (SqlDbType)key.metadata;

											if ((dbType == SqlDbType.Structured) && (key.stuctureTypeName != null))
												command.Parameters.Add (new SqlParameter (key.key, (SqlDbType)key.metadata)
												{
													Value		= key.value,
													TypeName	= key.stuctureTypeName.ToString (),
													Direction	= key.direction
												});
											else
												command.Parameters.Add (new SqlParameter (key.key, (SqlDbType)key.metadata)
												{
													Value		= key.value,
													Direction	= key.direction
												});
										}
									}

					
							// Add ReturnValue Paramter
							command.Parameters.Add (new SqlParameter () {
								DbType			= DbType.Object,
								Direction		= ParameterDirection.ReturnValue,
								ParameterName	= C_RET_VALUE
							});
							#endregion

							#region Open Connection
							if (connection.State != System.Data.ConnectionState.Open)
								connection.Open ();
							#endregion

							#region Execute
							switch (executeType)
							{
								default:
								case EnumExecuteType.nonQuery :
									command.CommandType = CommandType.Text;
									result.model	= command.ExecuteNonQuery ();
									break;

								case EnumExecuteType.scaler :
									command.CommandType = CommandType.Text;
									result.model	= command.ExecuteScalar ();
									break;
							
								case EnumExecuteType.reader :
									command.CommandType = CommandType.Text;
									result.model	= readerToTable (command.ExecuteReader ());
									break;

								case EnumExecuteType.xmlReader :
									command.CommandType = CommandType.Text;
									result.model	= command.ExecuteXmlReader ();
									break;

								case EnumExecuteType.procedureReader :
									command.CommandType = CommandType.StoredProcedure;
									result.model	= command.ExecuteReader ();
									break;

								case EnumExecuteType.procedureNonQuery :
									command.CommandType = CommandType.StoredProcedure;
									result.model	= command.ExecuteNonQuery ();
									break;
							}

							// create result
							result.extra	= command.Parameters[C_RET_VALUE].Value;
							if (result.extra is DBNull)
								result.extra	= null;
							result.status	= EnumCommandStatus.success;
							if (result.model is SqlDataReader)
							{
								DataTable	dt	= new DataTable ();
						
								dt.Load ((SqlDataReader)result.model);
								result.model	= dt;
							}

							// Set output values
							foreach (SqlParameter param in command.Parameters)
							{
								if ((param.Direction == ParameterDirection.Output) ||
									(param.Direction == ParameterDirection.InputOutput))
								{
									KeyValuePair key = parameters.Where (x => x.key == param.ParameterName).FirstOrDefault ();

									if (key != null)
										key.value	= param.Value;
								}
							}
							#endregion
						}
						catch (Exception ex)
						{
							result.status	= EnumCommandStatus.executeFailed;
							result.model	= ex;
							result.message	= "Error: " + ex.Message;
						}
						finally
						{
							// Close Connection
							if (closeConnection)
								connection.Close ();
						}
						#endregion
					}
					else
					{
						result.status	= EnumCommandStatus.connectionFailed;
						result.model	= null;
						result.message	= "Error: Connection failed";
					}
				}
			}
			catch (Exception ex)
			{
				/// TODO: HANDLE ERROR
			}

			return result;
		}

		/// <summary>
		/// Convert dataReader to dataTable
		/// </summary>
		/// <param name="datareader"></param>
		/// <returns></returns>
		public static DataTable readerToTable (object datareader)
		{
			DataTable	result	= new DataTable ();

			if (datareader is SqlDataReader)
				result.Load ((SqlDataReader)datareader);

			return result;
		}
	#endregion
	}
}
