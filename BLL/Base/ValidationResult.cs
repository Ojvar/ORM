using System;
using System.Collections.Generic;
using System.Data;

namespace BaseBLL.Base
{
	/// <summary>
	/// Field Attributes
	/// </summary>
	public class ValidationResult
	{
	#region Properties
		/// <summary>
		/// Messages List
		/// </summary>
		public List<string> message
		{
			get;
			set;
		}

		/// <summary>
		/// IsValid
		/// </summary>
		public bool isValid
		{
			get;
			set;
		}
	#endregion

	#region Methods
		/// <summary>
		/// Constructor
		/// </summary>
		public ValidationResult ()
		{
		}
	#endregion
	}
}
