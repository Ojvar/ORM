using BaseDAL.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseDAL.Model
{
	/// <summary>
	/// Command result
	/// </summary>
	public class KeyValuePair
	{
	#region Properties
		/// <summary>
		/// Key
		/// </summary>
		public string key
		{
			get;
			set;
		}

		/// <summary>
		/// Value
		/// </summary>
		public object value
		{
			get;
			set;
		}

		/// <summary>
		/// Metadata
		/// </summary>
		public object metadata
		{
			get;
			set;
		}

		/// <summary>
		/// Structure type name
		/// </summary>
		public string stuctureTypeName
		{
			get;
			set;
		}
	#endregion

	#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="metadata"></param>
		public KeyValuePair (string key, object value, object metadata = null)
		{
			this.key		= key;
			this.value		= value;
			this.metadata	= metadata;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public KeyValuePair ()
		{
		}
	#endregion

	#region Methods
		/// <summary>
		/// To String
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.key;
		}
	#endregion
	}
}
