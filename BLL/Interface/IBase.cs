using BaseDAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BaseBLL.Interface
{
	/// <summary>
	/// Interface IBase
	/// </summary>
	public interface IBase
	{
		CommandResult	create (object iData, bool closeConnection, string exceptFilelds);
		CommandResult	read (object iData, bool closeConnection);
		CommandResult	read (object iData, string field, string criteria, bool closeConnection);
		CommandResult	update (object iData, bool closeConnection, string exceptFileld);
		CommandResult	delete (object iData, bool closeConnection);

		CommandResult	validate (object iData);
		CommandResult	loadForeignKey (object iData, string propertyName, string orderBy, bool outputAsList, bool closeConnection);

		CommandResult	rawQuery (string command, bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue);

		CommandResult	allByPaging (int pageIndex, int pageSize, string criteria, string orderBy, bool outputAsList, bool closeConnection, params KeyValuePair[] fieldValues);
		CommandResult	allData (string criteria, string orderBy, bool outputAsList , bool closeConnection, params KeyValuePair[] fieldValue);
		CommandResult   allDataByViewName (string viewName, string fields, string criteria = "", string orderBy = "", bool distinct = false, bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue);
		CommandResult	allByViewNameByPaging (string viewName, int pageIndex, int pageSize, string criteria = "", string orderBy = "", bool distinct = false, bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValues);
		CommandResult	allDataBySpecifiedFields (string[] fields, string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue);
		CommandResult	allDataBySpecifiedFieldsDistinct (string[] fields, string criteria = "", string orderBy = "", bool distinct = false, bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValue);

		CommandResult	allDataDistinct (string criteria, string orderBy, bool outputAsList , bool closeConnection, params KeyValuePair[] fieldValue);
		CommandResult	allDataDistinct (string fields, string criteria, string orderBy, bool outputAsList , bool closeConnection, params KeyValuePair[] fieldValue);
		CommandResult	allByPagingDistinct (int pageIndex, int pageSize, string criteria = "", string orderBy = "", bool outputAsList = true, bool closeConnection = true, params KeyValuePair[] fieldValues);
	}
}
