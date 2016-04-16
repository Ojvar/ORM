using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Models
{
	public class ForeignKeyModel
	{
	#region Properties
		public string primaryTable
		{
			get;
			set;
		}

		public string primaryColumn
		{
			get;
			set;
		}

		public string foreignTable
		{
			get;
			set;
		}

		public string foreignColumn
		{
			get;
			set;
		}
	#endregion

	#region Methods
		/// <summary>
		/// ForeignKey Relation
		/// </summary>
		/// <param name="pkTable"></param>
		/// <param name="pkCol"></param>
		/// <param name="fkTable"></param>
		/// <param name="fkCol"></param>
		public ForeignKeyModel (string pkTable, string pkCol, string fkTable, string fkCol)
		{
			this.primaryTable	= pkTable;
			this.primaryColumn	= pkCol;
			this.foreignTable	= fkTable;
			this.foreignColumn	= fkCol;
		}
	#endregion
	}
}
