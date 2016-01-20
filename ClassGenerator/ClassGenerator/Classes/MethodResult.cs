using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Classes
{
	public class MethodResult
	{
	#region Properties
		/// <summary>
		/// Result enum
		/// </summary>
		public enum Result
		{
			success = 1,
			failed = 2,
			unknown = 4
		}

		/// <summary>
		/// Message
		/// </summary>
		public string message
		{
			get;
			set;
		}

		/// <summary>
		/// Status
		/// </summary>
		public Result status
		{
			get;
			set;
		}

		/// <summary>
		/// Meta data
		/// </summary>
		public object data
		{
			get;
			set;
		} 
	#endregion
	}
}
