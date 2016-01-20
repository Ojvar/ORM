using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Interface
{
	/// <summary>
	/// Interface IBase
	/// </summary>
	public interface IBase
	{
		CommandResult	create (object iData, bool closeConnection);
		CommandResult	read (object iData, bool closeConnection);
		CommandResult	udpate (object iData, bool closeConnection);
		CommandResult	delete (object iData, bool closeConnection);
		CommandResult	validate (object iData);
		CommandResult	loadForeignKey (object iData, string propertyName, string orderBy, bool outputAsList, bool closeConnection);
		CommandResult	allByPaging (int pageIndex, int pageSize, string criteria, string orderBy, bool outputAsList, bool closeConnection);
		CommandResult	allData (string criteria, string orderBy, bool outputAsList , bool closeConnection, params KeyValuePair[] fieldValue);
	}
}
