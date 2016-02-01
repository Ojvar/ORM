using DAL.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Model
{
	/// <summary>
	/// Command result
	/// </summary>
	public class CommandResult
	{
	#region Properties
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
		public EnumCommandStatus status
		{
			get;
			set;
		}

		/// <summary>
		/// Model
		/// </summary>
		public object model
		{
			get;
			set;
		} 

		/// <summary>
		/// Extra data
		/// </summary>
		public object extra
		{
			get;
			set;
		}
		#endregion

	#region Methods
		/// <summary>
		/// Constructor
		/// </summary>
		public CommandResult ()
		{
		}
	#endregion
	}
}
