using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Classes
{
	/// <summary>
	/// Table Class
	/// </summary>
	public class Table : Base
	{
	#region Variables
		private Database	parent;
		private List<Field>	fields;

		public const string	C_Type	= "Table";
	#endregion

	#region Properties
		/// <summary>
		/// Set parent
		/// </summary>
		/// <param name="value"></param>
		public void setParent (Database value)
		{
			parent = value;
		}
		/// <summary>
		/// Get Name
		/// </summary>
		/// <returns></returns>
		public Database getParent ()
		{
			return parent;
		}
	#endregion

	#region Methods
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="name"></param>
		public Table (Database parent, string name, bool autoLoad = true) : base (C_Type, name, autoLoad)
		{
			if (null == parent)
				throw new NullReferenceException ("Parent is null!");

			setParent (parent);
		}

		/// <summary>
		/// Get Field by name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Field this[string name]
		{
			get
			{
				List<Field>	fields	= getFields ();

				return (from t in fields where (t.getName ().Equals (name)) select t).FirstOrDefault ();
			}
		}
		/// <summary>
		/// Get Field by index
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Field this[int index]
		{
			get
			{
				List<Field>	fields	= getFields ();

				if (index >= fields.Count)
					throw new IndexOutOfRangeException ();

				return fields[index];
			}
		}

		/// <summary>
		/// Load information
		/// </summary>
		public MethodResult loadFields ()
		{
			MethodResult	result	= new MethodResult ();
			Database		parent	= getParent ();

			// Check Connection
			if (null == parent)
			{
				throw new NullReferenceException ("Parent is null!");
				result.status	= MethodResult.Result.failed;
			}
			else
			{
				// Server ready?
				if (!DAL.DBaseHelper.IsServerConnected (parent.getConnection ()))
					result.status	= MethodResult.Result.failed;
				else
				{
					if (fields == null)
						fields = new List<Field> ();	// Create new instance
					else
						clearFields ();					// Clear current tables list

				#region Load tables information
					// Generate command
					string	cmd	= string.Format (Resources.Table.GetFields, parent.getName (), getName ());

					// Run
					CommandResult	dRes	= DAL.DBaseHelper.executeCommand (DAL.Base.EnumExecuteType.reader, parent.getConnection (), cmd, true);

					if (dRes.status == DAL.Base.EnumCommandStatus.success)
					{
						if (dRes.model is DataTable)
						{
							DataTable	table	= dRes.model as DataTable;

							foreach (DataRow row in table.Rows)
							{
								string	fieldName;
								Field	field;

								fieldName	= row["COLUMN_NAME"].ToString ();
								field		= new Field (this, fieldName, getAutoload ());
								fields.Add (field);
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
		public List<Field> getFields ()
		{
			if ((null == fields) && autoLoad)
				loadFields ();
			
			return fields;
		}

		/// <summary>
		/// Clear tables
		/// </summary>
		private void clearFields ()
		{
			if (null != fields)
				fields.Clear ();
		}
	#endregion
	}
}
