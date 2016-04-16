using BaseDAL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseBLL
{
	/// <summary>
	/// Extensions
	/// </summary>
	public static class Extensions
	{
	#region Methods
		/// <summary>
		/// Convert To List<typeparamref name=">"/>
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static object itemAt (this CommandResult data, int index)
		{
			object result	= null;

			if ((data != null) && (data.model is IList))
            {
                IList	item	= data.model as IList;
				
				if ((item != null) && (item.Count > 0) && (index < item.Count))
					result	= item[index];
			}

			return result;
		}

		/// <summary>
		/// Is Null Or Empty Or WhiteSpaces
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static bool isNullOrEmptyOrWhiteSpaces (this string data)
		{
			bool result	= false;

			// Check input data
			result	= (data == null) || (data.Trim ().Length == 0);

			return result;
		}
	#endregion
	}
}
