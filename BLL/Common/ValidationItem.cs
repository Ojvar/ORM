using BLL.Base;
using System.Collections.Generic;
using System.Reflection;

namespace BLL.Common
{
	/// <summary>
	/// Validation Item
	/// </summary>
	public class ValidationItem
	{
	#region Methods
		/// <summary>
		/// Validate
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="validationString"></param>
		/// <returns></returns>
		public static ValidationResult validate (string name, object value, string validationString)
		{
			ValidationResult	result	= new ValidationResult ();
			string[]			opts	= validationString.ToLower ().Split (':');

			// set Default to TRUE
			result.message	= new List<string> ();
			result.isValid	= true;

		#region validate
			if ((null != opts) && (opts.Length > 1))
			{
				if (opts[0] == "mxlen")
				{
					int len;
					bool res	= (int.TryParse (opts[1], out len) && ((value ?? "").ToString ().Length > len));

					if (res)
						result.message.Add (string.Format ("{0}:{1}", name, "Maximum length"));
				}
				else if (opts[0] == "milen")
				{
					int len;
					bool res	= (int.TryParse (opts[1], out len) && ((value ?? "").ToString ().Length < len));

					if (res)
						result.message.Add (string.Format ("{0}:{1}", name, "Minimum length"));
				}
				else if (opts[0] == "null")
				{
					bool res	= (opts[1] == "f") && (null == value);

					if (res)
						result.message.Add (string.Format ("{0}:{1}", name, "NULL value"));
				}
			}
		#endregion

			return result;
		}
	#endregion
	}
}
