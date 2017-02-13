using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ClassGenerator.Classes;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.IO;

namespace ClassGenerator
{
	public partial class SqlGenerator : Form
	{
	#region Variables
		/// <summary>
		/// Data Source
		/// </summary>
		string	dataSource
		{
			get
			{
				return dataSourceTextbox.Text.Trim ();
			}
		}

		/// <summary>
		/// Initil Catalog
		/// </summary>
		string	initialCatalog
		{
			get
			{
				return initialiCatalogTextbox.Text.Trim ();
			}
		}

		/// <summary>
		/// User Id
		/// </summary>
		string	userId
		{
			get
			{
				return usernameTextbox.Text.Trim ();
			}
		}

		/// <summary>
		/// Password
		/// </summary>
		string	password
		{
			get
			{
				return passwordTextbox.Text.Trim ();
			}
		}

		/// <summary>
		/// Connection Message
		/// </summary>
		string	connectionMessage
		{
			get
			{
				return messageLabel.Text.Trim ();
			}
			set
			{
				messageLabel.Text	= value;
			}
		}

		/// <summary>
		/// Current DBase Connection
		/// </summary>
		SqlConnection	currentConnection;

		/// <summary>
		/// Current DB
		/// </summary>
		Database	currentDB;

		/// <summary>
		/// Current Table
		/// </summary>
		Table		currentTable;
	#endregion

	#region Constructor
		/// <summary>
		/// Constrcutor
		/// </summary>
		public SqlGenerator ()
		{
			InitializeComponent ();

			init ();		// Initliaze
			bindEvents ();	// Bind Events
		}
	#endregion

	#region Methods
		/// <summary>
		/// Initialize
		/// </summary>
		void init ()
		{
		#region Fill EntityBase
			entityBaseCombobox.Items.Clear ();
			entityBaseCombobox.Items.AddRange (new string[] {
				"BaseBLL.Entity.Base",
				"BaseBLL.Entity.BaseByViewId",
				"BaseBLL.Entity.BaseByAuthor",
				"BaseBLL.Entity.BaseByTimestamp",
				"BaseBLL.Entity.BaseEmpty",
			}); 
		#endregion
		}

		/// <summary>
		/// Bind Events
		/// </summary>
		void bindEvents ()
		{
			quitMenu.Click	+= (x, y) =>
            {
				Close ();
			};

			connectToDbButton.Click += (x, y) =>
			{
				connectToDB (dataSource, initialCatalog, userId, password);
			};
			
			tablesRefreshButton.Click	+= (x, y) =>
			{
				refreshTableList ();
			};

			generateScriptButton.Click	+= (x, y) =>
			{
				generateScript ();
			};

			loadFieldsButton.Click	+= (x, y) =>
			{
				loadFields ();
			};
			
			fieldsRefreshButton.Click += (x, y) =>
			{
				refreshFieldList ();
			};
		}
	#endregion

	#region Functions
		/// <summary>
		/// Connect To DB
		/// </summary>
		/// <param name="dataSource"></param>
		/// <param name="initCatalog"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		void connectToDB (string dataSource, string initCatalog, string username, string password)
		{
			string	C_ConnStr	= string.Format ("Persist Security Info=True; Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}",
				dataSource, initialCatalog, userId, password);

			// Check Connection
			bool conn	= BaseDAL.DBaseHelper.IsServerConnected (C_ConnStr);

			if (conn)
			{
				// Dispose old connection
				if ((null != currentConnection) && (currentConnection.State != ConnectionState.Closed))
				{
					currentConnection.Close ();
					currentConnection.Dispose ();
				}

				currentConnection	= new SqlConnection (C_ConnStr);	// Create new connection

				pageTabControl.SelectedTab	= databaseTabPage;			// Change Tab
				refreshTableList ();									// Refresh table list
				connectionMessage	= "Connection Success!";			// Show Message
			}
			else
				connectionMessage	= "Connection Failed!";
		}

		/// <summary>
		/// Show Message for Connection
		/// </summary>
		/// <param name="msg"></param>
		void showConnectionMessage (string msg)
		{
			connectionMessage	= msg;
		}

		/// <summary>
		/// Refresh Table List
		/// </summary>
		void refreshTableList ()
		{
			if (null != currentConnection)
			{
				// Prepare Database
				currentDB	= new Database (currentConnection, initialCatalog);
				currentDB.loadTables ();

				// Show in grid
				if (tablesGrid.DataSource is DataTable)
					((DataTable)tablesGrid.DataSource).Dispose ();
				tablesGrid.DataSource	= tablesToDatatable (currentDB.getTables ());
			}
			else
				MessageBox.Show (this, "Connect to database", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// Load Fields
		/// </summary>
		void loadFields ()
		{
			if ((currentDB != null) && (null != tablesGrid.CurrentRow))
				refreshFieldList ();
			else
				MessageBox.Show (this, "Please select a table!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// Refresh Field List
		/// </summary>
		void refreshFieldList ()
		{
			if ((currentDB != null) && (null != tablesGrid.CurrentRow))
			{
				string	tableName	= tablesGrid.CurrentRow.Cells["Table"].Value.ToString ();

				currentTable	= currentDB[tableName];
				currentTable.loadFields ();

				// Show in grid
				if (fieldsGrid.DataSource is DataTable)
					((DataTable)fieldsGrid.DataSource).Dispose ();
				fieldsGrid.DataSource	= fieldsToDatatable (currentTable.getFields ());

				pageTabControl.SelectedTab	= fieldsTabPage;	// Change Tab
			}
			else
				MessageBox.Show (this, "Please select a table", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// Generate Scripts
		/// </summary>
		void generateScript ()
		{
		#region Variables
			string entityResult;
			string logicResult;
			string classEntityDef;
			string classLogicDef;
			string baseClass;
			string namespaceValue;
			string postfixNamespace;
			string fieldStr;
			string entityName;
			string logicName;
			List<string> fieldsScript = new List<string> (); 

			string	savePath;
			bool	saveToFile;
		#endregion

			// Prepare
			namespaceValue		= namespaceTextbox.Text.Trim ();
			postfixNamespace	= postfixNamespaceTextbox.Text.Trim ();
			saveToFile			= saveToFileCheckbox.Checked;
			scriptTextbox.Text	= "";

			if(postfixNamespace.Length > 0)
				postfixNamespace	= "." + postfixNamespace;


			if (string.IsNullOrWhiteSpace (namespaceValue))
			{
				MessageBox.Show (this, "Specify a namespace!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			else if (null != currentTable)
			{
			#region Collect Field data
				foreach (DataGridViewRow row in fieldsGrid.Rows)
					if (((bool)row.Cells["checked"].Value) == true)
					{
						Field field = currentTable.getFields ().Find (x => x.getName () == row.Cells["FieldName"].Value.ToString ());

						if (null != field)
						{
							field.postfixNamespace	= postfixNamespaceTextbox.Text.Trim ();
							fieldsScript.Add (field.generateScript ());
						}
					}
			#endregion

			#region Prepare data
				classEntityDef = Resources.Class.ClassEntityDefinition;
				classLogicDef = Resources.Class.ClassLogicDefinition;
				logicName = currentTable.getName ().Replace (".", "__");
				baseClass = entityBaseCombobox.Text;
				fieldStr = string.Join ("\r\n", fieldsScript); 
			#endregion

			#region Singularized entity name
				PluralizationService pService = PluralizationService.CreateService (new CultureInfo ("en-US"));
				entityName = pService.Singularize (logicName);
			#endregion

			#region Generate definition
				entityResult = string.Format (classEntityDef, namespaceValue, postfixNamespace, entityName, baseClass, fieldStr);
				logicResult = string.Format (classLogicDef, namespaceValue, postfixNamespace, logicName, entityName);
			#endregion

			#region Save to file
				savePath	= savePathTextbox.Text.Trim ();
				if (saveToFile)
					if (string.IsNullOrWhiteSpace (savePath))
						MessageBox.Show (this, "Save path is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					else if (!Directory.Exists (savePath))
						MessageBox.Show (this, "Save path not exists!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					else
					{
						string	logicPath;
						string	entityPath;

						logicPath	= Path.Combine (savePath, "Logic");
						entityPath	= Path.Combine (savePath, "Entity");

						if (!Directory.Exists (logicPath))
							Directory.CreateDirectory (logicPath);
						if (!Directory.Exists (entityPath))
							Directory.CreateDirectory (entityPath);

						File.WriteAllText (Path.Combine (entityPath, entityName + ".cs"), entityResult);
						File.WriteAllText (Path.Combine (logicPath, logicName + ".cs"), logicResult);
					}
			#endregion

				// Show in output
				scriptTextbox.Text	= string.Format ("/// AUTO-GENERATE, OJVAR\r\n\r\n/// ENTITY CLASS\r\n{0}\r\n\r\n/// LOGIC CLASS\r\n{1}", entityResult, logicResult);
			}
			else
			{
				// Empty definition
				entityResult	= "";
				logicResult		= "";
			}

			// Swap tab
			pageTabControl.SelectedTab	= scriptTabPage;
		}
	#endregion

	#region Data Converter
		/// <summary>
		/// Convert list of tables to datatable
		/// </summary>
		/// <returns></returns>
		DataTable tablesToDatatable (List<Table> tables)
		{
			DataTable result	= new DataTable ();

			if (null != tables)
			{
				// Prepare columns
				result.Columns.Add ("Table");

				// Collect data
				foreach (Table table in tables)
					result.Rows.Add (table.getName ());
			}

			return result;
		}

		/// <summary>
		/// Convert list of fields to datatable
		/// </summary>
		/// <returns></returns>
		DataTable fieldsToDatatable (List<Field> fields)
		{
			DataTable result	= (new DataTemplate.DefaultDataTemplate.FieldsDataTable());

			if (null != fields)
				// Collect data
				foreach (Field field in fields)
				{
					// Load Field Information
					field.loadInformation ();

				#region Collect data
					string		fieldName		= field.getFieldName ();
					SqlDbType	fieldDbType		= field.getFieldDbType ();
					Type		fieldCsType		= field.getFieldCsType ();
					int			fieldMaxLen		= field.getFieldMaxLen ();
					bool		fieldNullable	= field.getFieldNullable ();
					string		fieldDefValue	= field.getFieldDefaultValue ();
					string		fieldRefTable	= field.getFieldRefereneTable ();
					string		fieldRefKey		= field.getFieldRefereneField (); 
				#endregion

					// Add Row
					result.Rows.Add (true, fieldName, fieldCsType.ToString (), fieldDbType.ToString (), fieldMaxLen, fieldNullable, false, fieldDefValue, fieldRefTable, fieldRefKey);
				}

			return result;
		}
		#endregion
	}
}
