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
		CommandResult	create (object iData, bool closeConnection);
		CommandResult	read (object iData, bool closeConnection);
		CommandResult	read (object iData, string field, bool closeConnection);
		CommandResult	update (object iData, bool closeConnection);
		CommandResult	delete (object iData, bool closeConnection);
		CommandResult	validate (object iData);
		CommandResult	loadForeignKey (object iData, string propertyName, string orderBy, bool outputAsList, bool closeConnection);
		CommandResult	allByPaging (int pageIndex, int pageSize, string criteria, string orderBy, bool outputAsList, bool closeConnection, params KeyValuePair[] fieldValues);
		CommandResult	allByViewNameByPaging (string viewName, int pageIndex, int pageSize, string criteria, string orderBy, bool outputAsList, bool closeConnection, params KeyValuePair[] fieldValues);
		CommandResult	allData (string criteria, string orderBy, bool outputAsList , bool closeConnection, params KeyValuePair[] fieldValue);
		CommandResult   allDataByViewName (string viewName, string criteria, string orderBy, bool outputAsList , bool closeConnection, params KeyValuePair[] fieldValue);
	}
}
