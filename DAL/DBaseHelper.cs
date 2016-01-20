using DAL.Base;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
	/// <summary>
	/// Database Helper
	/// </summary>
	public static class DBaseHelper
	{
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
			CommandResult	result	= new CommandResult ();

			if (null != connection)
			{
			#region Run Command
				try
				{
					SqlCommand	command		= null;

				#region Prepare command
					// Create command
					command	= new SqlCommand (commandString, connection);

					// Add parameters
					if (null != parameters)
						foreach (KeyValuePair key in parameters)
							if (null != key)
								command.Parameters.Add (new SqlParameter (key.key, key.value));
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
							result.model	= command.ExecuteNonQuery ();
							break;

						case EnumExecuteType.scaler :
							result.model	= command.ExecuteScalar ();
							break;
							
						case EnumExecuteType.reader :
							result.model	= readerToTable (command.ExecuteReader ());
							break;

						case EnumExecuteType.xmlReader :
							result.model	= command.ExecuteXmlReader ();
							break;
					}

					result.status	= EnumCommandStatus.success;
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
	}
}
