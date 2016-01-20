using BLL.Base;
using System.Collections.Generic;
using System.Reflection;

namespace BLL.Common
{
	/// <summary>
	/// Validation Helper
	/// </summary>
	public class ValidationHelper
	{
	#region Constants
		public const string C_FIELD_NAME	= "validationString";
	#endregion

	#region Methods
		/// <summary>
		/// Validate Field
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="value"></param>
		/// <param name="attr"></param>
		/// <returns></returns>
		public static ValidationResult validateField (string fieldName, object value, FieldAttribute attr, string validationFieldName = C_FIELD_NAME)
		{
			ValidationResult result = new ValidationResult ();

			// Set TRUE as default
			result.isValid = true;
			result.message = new List<string> ();

			if (null != attr)
			{
				// Get Property info
				PropertyInfo pInfo = attr.GetType ().GetProperty (validationFieldName, BindingFlags.Public | BindingFlags.Instance);

				if (null != pInfo)
				{
					List<ValidationResult> vItems = new List<ValidationResult> ();
					object fieldValue = null;

					// Get value
					fieldValue = pInfo.GetValue (attr, null);

					if (null != fieldValue)
					{
						string[] valueOptions = fieldValue.ToString ().ToLower ().Split (',');

						if (valueOptions != null)
							foreach (string val in valueOptions)
							{
								ValidationResult vRes = ValidationItem.validate (fieldName, value, val);

								if ((null != vRes) && (null != vRes.message) && (0 < vRes.message.Count))
									result.message.AddRange (vRes.message);
							}

						result.isValid = (result.message.Count == 0);
					}
				}
			}

			return result;
		} 
	#endregion
	}
}
