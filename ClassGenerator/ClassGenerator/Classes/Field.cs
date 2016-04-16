using BaseDAL.Model;
using ClassGenerator.Models;
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
		public List<ForeignKeyModel> referentialColumn
		{
			get;
			set;
		}

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
			
			result	= (getProperty (EnumFieldProperty.REFERENCED_TABLE_NAME) ?? "").ToString ().Replace (".", "__");

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
				if (!BaseDAL.DBaseHelper.IsServerConnected (dbase.getConnection ()))
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
					CommandResult	dRes	= BaseDAL.DBaseHelper.executeCommand (BaseDAL.Base.EnumExecuteType.reader, dbase.getConnection (), cmd, true);

					if (dRes.status == BaseDAL.Base.EnumCommandStatus.success)
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
		/// Load Field Referntial Information
		/// </summary>
		public MethodResult loadReferentialInformation ()
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
				if (!BaseDAL.DBaseHelper.IsServerConnected (dbase.getConnection ()))
					result.status	= MethodResult.Result.failed;
				else
				{
					string	cmd	= Resources.Field.GetReferentialInformation;
					
					// Clear last information
					if (null != referentialColumn)
						referentialColumn.Clear ();
					else
						referentialColumn	= new List<ForeignKeyModel> ();

					// Prepare command
					cmd	= string.Format (cmd, dbase.getName (), table.getName (), fieldName);
				
					// Run
					CommandResult	dRes	= BaseDAL.DBaseHelper.executeCommand (BaseDAL.Base.EnumExecuteType.reader, dbase.getConnection (), cmd, true);

					if (dRes.status == BaseDAL.Base.EnumCommandStatus.success)
					{
						if (dRes.model is DataTable)
						{
							DataTable	resTable	= dRes.model as DataTable;

							if (resTable.Rows.Count > 0)
								foreach (DataRow row in resTable.Rows)
									referentialColumn.Add (
										new ForeignKeyModel (row["primaryTable"].ToString (), row["primaryColumn"].ToString (), row["foreignTable"].ToString (), row["foreignColumn"].ToString ())
										);
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
			string	fieldType;
			string	cFT;

			fieldType	= getFieldCsType ().FullName;
			cFT			= fieldType.ToLower ();
			if (getFieldNullable ())
				if ((cFT != typeof(string).ToString ().ToLower()) &&
					(cFT != typeof(byte[]).ToString ().ToLower()) &&
					(cFT != typeof(char).ToString ().ToLower())
					)
				fieldType	= "Nullable<" + fieldType + ">";

			// Create field attr and definition
			fieldAttr	= string.Format (fieldAttr, string.Join (",", attr));
			result		= string.Format (propertyBody, fieldAttr, fieldType, getFieldName ());

		#region Create Foreign key property if need
			if (getFieldRefereneTable() != "")
			{
				string	def;
				string	refEntityName;
				string	refLogicName;
				string	refFieldName;
				string	refFieldValue;
				string	refFieldType;
				string	refForeignKey;
				string	refConnection;
				string	refNullSign;

				if (getFieldNullable ())
					def				= Resources.Field.FieldForeignKeyNullable;
				else
					def				= Resources.Field.FieldForeignKey;
				refEntityName	= getFieldRefereneTable ();						// {0} EntityName
				refLogicName	= refEntityName;								// {1} LogicName
				refFieldName	= refEntityName;								// {2} FieldName
				refFieldValue	= getFieldName ();								// {3} FieldValue - Current Entity Property
				refFieldType	= getFieldCsType ().ToString ();				// {4} FieldType
				refForeignKey	= getFieldRefereneField ();						// {5} ForeignKeyField
				refConnection	= parent.getParent ().getName ();				// {6} Connection
				refNullSign		= ""; //(getFieldNullable () ? "?" : "");		// {7} IsNUll?
			
				// Generate property string
				def = string.Format(def, refEntityName, refLogicName, refFieldName, refFieldValue, refFieldType, refForeignKey, refConnection, refNullSign);

				//result	+= string.Format ("\r\n{1}", refFieldName, def);
				result	+= string.Format ("\r\n{0}", def);
			}
		#endregion

		#region Create Referential properties if need
			loadReferentialInformation ();

			if (null != referentialColumn)
				foreach (Models.ForeignKeyModel key in referentialColumn)
				{
					string	def;
					
					def	= string.Format (Resources.Field.FieldReferentialKey, key.foreignTable.Replace (".", "__"), key.foreignColumn, key.primaryColumn, parent.getParent ().getName ());

					// Append to result
					result	+= string.Format ("\r\n{0}", def);
				}
		#endregion

			return result;
		}
	#endregion
	}
}