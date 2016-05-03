using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BaseBLL.Entity
{
	/// <summary>
	/// Base Entity Mode
	/// </summary>
	[Serializable]
	public class BaseModel
	{
	#region Properties
		public bool AutoLoadForeignKeys;
	#endregion

	#region Methods
		/// <summary>
		/// Copy model
		/// </summary>
		/// <param name="mode"></param>
		public void copyTo (object target)
		{
			if ((null != target) && (this.GetType () == target.GetType ()))
			{
				object			source;
				PropertyInfo	sourceInfo;
				PropertyInfo[]	targetProp;

				// Init
				source		= this;
				targetProp	= target.GetType ().GetProperties (BindingFlags.Public | BindingFlags.Instance);

				// Try to copy
				foreach (PropertyInfo destInfo in targetProp)
				{
					// Init
					sourceInfo	= this.GetType ().GetProperty (destInfo.Name);

					// try to copy
					destInfo.SetValue (target, sourceInfo.GetValue (this, null), null);
				}
			}
		}
	#endregion
	}
}
