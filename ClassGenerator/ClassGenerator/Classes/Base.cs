using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Classes
{
	public class Base
	{
	#region Variables
		protected string	name;
		protected string	type;
		protected bool		autoLoad;
	#endregion

	#region Properties Methods
		/// <summary>
		/// Set Name
		/// </summary>
		/// <param name="value"></param>
		public void setName (string value)
		{
			name = value;
		}
		/// <summary>
		/// Get Name
		/// </summary>
		/// <returns></returns>
		public string getName ()
		{
			return name;
		}

		/// <summary>
		/// Set Type
		/// </summary>
		/// <param name="value"></param>
		public void setType (string value)
		{
			type = value;
		}
		/// <summary>
		/// Get Type
		/// </summary>
		/// <returns></returns>
		public string getType ()
		{
			return type;
		}

		/// <summary>
		/// Set autoload
		/// </summary>
		/// <param name="value"></param>
		public void setAutoload (bool value)
		{
			autoLoad = value;
		}
		/// <summary>
		/// Get Name
		/// </summary>
		/// <returns></returns>
		public bool getAutoload ()
		{
			return autoLoad;
		}
	#endregion

	#region Methods
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="autoLoad"></param>
		public Base (string type, string name, bool autoLoad = true)
		{
			if (string.IsNullOrWhiteSpace (type))
				throw new Exception ("Type is empty!");
			if (string.IsNullOrWhiteSpace (name))
				throw new Exception ("Name is empty!");

			setType (type);
			setName (name);
			setAutoload	(autoLoad);
		}

		/// <summary>
		/// ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString ()
		{
			return string.Format ("{0} : {1}", getType (), getName ());
		}
		#endregion
	}
}
