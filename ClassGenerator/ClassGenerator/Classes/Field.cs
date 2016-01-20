using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassGenerator.Classes
{
	/// <summary>
	/// Field
	/// </summary>
	public class Field : Base
	{
	#region Variables
		private Table						parent;
		private Dictionary<string, object>	properties;

		private bool	fieldIsPrimary;
		private string	fieldValidationString;

		public const string	C_Type	= "Field";
	#endregion

	#region Properties
		/// <summary>
		/// Get IsPrimary
		/// </summary>
		public bool getFieldIsPrimary ()
		{
			return fieldIsPrimary;
		}
		/// <summary>
		/// Set IsPrimary
		/// </summary>
		/// <returns></returns>
		public void setFieldIsPrimary (bool value)
		{
			fieldIsPrimary	= value;
		}

		/// <summary>
		/// Get IsPrimary
		/// </summary>
		public string getFieldValidationString ()
		{
			return fieldValidationString;
		}
		/// <summary>
		/// Set IsPrimary
		/// </summary>
		/// <returns></returns>
		public void setFieldValidationString  (string value)
		{
			fieldValidationString	= value;
		}

		/// <summary>
		/// Set parent
		/// </summary>
		/// <param name="value"></param>
		public void setParent (Table value)
		{
			parent = value;
		}
		/// <summary>
		/// Get Name
		/// </summary>
		/// <returns></returns>
		public Table getParent ()
		{
			return parent;
		}

		/// <summary>
		/// Set Properties
		/// </summary>
		/// <param fieldCsType="value"></param>
		public void setProperties (Dictionary<string, object> value)
		{
			properties	= value;
		}
		/// <summary>
		/// Get Properties
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, object> getProperties ()
		{
			return properties;
		}


		/// <summary>
		/// Get Field Name
		/// </summary>
		/// <returns></returns>
		public string getFieldName ()
		{
			string result	= (getProperty (EnumFieldProperty.COLUMN_NAME) ?? "").ToString ();

			return result;
		}

		/// <summary>
		/// Get Field Type
		/// </summary>
		/// <returns></returns>
		public SqlDbType getFieldDbType ()
		{
			SqlDbType	result;
			string		dbType;

			dbType	= (getProperty (EnumFieldProperty.DATA_TYPE) ?? "").ToString ();
			if (dbType == "")
				throw new NullReferenceException ("No type has been specified!");
			else
				result	= (SqlDbType)Enum.Parse (typeof (SqlDbType), dbType, true);

			return result;
		}

		/// <summary>
		/// Get Field CS-Type
		/// </summary>
		/// <returns></returns>
		public Type getFieldCsType ()
		{
			Type		result;
			SqlDbType	dbType;

			dbType	= getFieldDbType ();
			result	= TypeHelper.csType[dbType];

			return result;
		}

		
		/// <summary>
		/// Get Maximum Length
		/// </summary>
		/// <returns></returns>
		public int getFieldMaxLen()
		{
			int result;
			
			result	= Convert.ToInt32 (getProperty (EnumFieldProperty.CHARACTER_MAXIMUM_LENGTH) ?? "0");

			return result;
		}
		
		/// <summary>
		/// Get Nullable
		/// </summary>
		/// <returns></returns>
		public bool getFieldNullable ()
		{
			bool result;
			
			result	= (getProperty (EnumFieldProperty.IS_NULLABLE) ?? "NO").Equals ("YES");

			return result;
		}
		
		/// <summary>
		/// Get Default Value
		/// </summary>
		/// <returns></returns>
		public string getFieldDefaultValue ()
		{
			string result;
			
			result	= (getProperty (EnumFieldProperty.COLUMN_DEFAULT) ?? "").ToString ();

			return result;
		}

		/// <summary>
		/// Get Reference Table
		/// </summary>
		/// <returns></returns>
		public string getFieldRefereneTable ()
		{
			string result;
			
			result	= (getProperty (EnumFieldProperty.REFERENCED_TABLE_NAME) ?? "").ToString ();

			return result;
		}
		
		/// <summary>
		/// Get Reference Field
		/// </summary>
		/// <returns></returns>
		public string getFieldRefereneField ()
		{
			string result;
			
			result	= (getProperty (EnumFieldProperty.REFERENCED_COLUMN_NAME) ?? "").ToString ();

			return result;
		}
	#endregion

	#region Methods
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="name"></param>
		public Field (Table parent, string name, bool autoLoad = true) : base (C_Type, name, autoLoad)
		{
			if (null == parent)
				throw new NullReferenceException ("Parent is null!");

			setParent (parent);
		}

		/// <summary>
		/// Load Field Information
		/// </summary>
		public MethodResult loadInformation ()
		{
			MethodResult	result	= new MethodResult ();

			Database	dbase;
			Table		table		= getParent ();
			string		fieldName	= getName ();

			if (null != table)
				dbase	= table.getParent ();
			else
				dbase	= null;

			if ((table == null) || (dbase == null))
			{
				throw new NullReferenceException ("Table or Database is null");
				result.status	= MethodResult.Result.failed;
			}
			else
			{
				dbase	= table.getParent ();

				// Server ready?
				if (!DAL.DBaseHelper.IsServerConnected (dbase.getConnection ()))
					result.status	= MethodResult.Result.failed;
				else
				{
					string	cmd	= Resources.Field.GetInformation;

					// Clear old properties
					if (getProperties () == null)
						setProperties (new Dictionary<string, object>());
					else
						getProperties ().Clear ();

					// Prepare command
					cmd	= string.Format (cmd, dbase.getName (), table.getName (), fieldName);
				
					// Run
					CommandResult	dRes	= DAL.DBaseHelper.executeCommand (DAL.Base.EnumExecuteType.reader, dbase.getConnection (), cmd, true);

					if (dRes.status == DAL.Base.EnumCommandStatus.success)
					{
						if (dRes.model is DataTable)
						{
							DataTable	resTable	= dRes.model as DataTable;

							if (resTable.Rows.Count > 0)
								foreach (DataColumn col in resTable.Columns)
									getProperties ().Add (col.ColumnName, resTable.Rows[0][col.ColumnName]);
						}

						result.status	= MethodResult.Result.success;
					}
					else
						result.status	= MethodResult.Result.failed;
				}
			}

			return result;
		}

		/// <summary>
		/// Get Property
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public object getProperty (EnumFieldProperty property)
		{
			object						result;
			Dictionary<string, object>	properties	= getProperties ();
			
			string	propName	= Enum.GetName (property.GetType (), property);
			
			if ((null != properties) && (properties.ContainsKey (propName)) && (properties[propName].GetType () != typeof (DBNull)))
				result	= properties[propName];
			else
				result	= null;

			return result;
		}

		/// <summary>
		/// Clear Properties
		/// </summary>
		public void clearProperties ()
		{
			Dictionary<string, object>	properties	= getProperties ();

			if (properties != null)
				properties.Clear ();
		}

		/// <summary>
		/// Generate Script
		/// </summary>
		/// <returns></returns>
		public string generateScript ()
		{
			string result;
			string fieldAttr;
			string propertyBody;
			List<string>	attr;

			fieldAttr		= Resources.Field.FieldAttribute;
			propertyBody	= Resources.Field.FieldProperty;

		#region Collect Attribute
			attr	= new List<string> ();

			attr.Add (string.Format ("{0}={1}", "nullable", getFieldNullable ().ToString ().ToLower ()));
			attr.Add (string.Format ("{0}=System.Data.SqlDbType.{1}", "sqlDBType", getFieldDbType ().ToString ()));
			attr.Add (string.Format ("{0}={1}", "primary", getFieldIsPrimary ().ToString ().ToLower ()));
			attr.Add (string.Format ("{0}={1}", "usage", "BaseBLL.Base.EnumUsage.read | BaseBLL.Base.EnumUsage.update | BaseBLL.Base.EnumUsage.create"));

			if (getFieldMaxLen () > 0)
				attr.Add (string.Format ("{0}={1}", "size", getFieldMaxLen ()));

			if (!string.IsNullOrWhiteSpace (getFieldValidationString ()))
				attr.Add (string.Format ("{0}=\"{1}\"", "validationString", getFieldValidationString ()));

			if (getFieldRefereneTable () != "")
				attr.Add (string.Format ("{0}=typeof (BLL.Logic.{1})", "foreignLogicType", getFieldRefereneTable ()));

			if (getFieldRefereneField () != "")
				attr.Add (string.Format ("{0}=\"{1}\"", "foreignField", getFieldRefereneField ()));
		#endregion

			// Join array items
			fieldAttr	= string.Format (fieldAttr, string.Join (",", attr));
			result		= string.Format (propertyBody, fieldAttr, getFieldCsType ().FullName, getFieldName ()); 

			return result;
		}
	#endregion
	}
}
