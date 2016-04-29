using BaseBLL.Base;
using System;
using System.Collections.Generic;
using System.Reflection;
using BaseBLL;

namespace BaseBLL.Common
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
		public static ValidationResult validateField (PropertyInfo prop, object value)
		{
			ValidationResult result = new ValidationResult ();

			// Set TRUE as default
			result.isValid = true;
			result.message = new List<string> ();

			if (null != prop)
			{
				// Get Property info
				object[]		attrs	= prop.GetCustomAttributes (typeof (FieldAttribute), true);

				foreach (FieldAttribute attr in attrs)
				{
					object	nullable	= attr.GetType ().GetProperty ("nullable", BindingFlags.Public | BindingFlags.Instance).GetValue (attr, null);
					object	size		= attr.GetType ().GetProperty ("size", BindingFlags.Public | BindingFlags.Instance).GetValue (attr, null);
					object	noValidate	= attr.GetType ().GetProperty ("noValidate", BindingFlags.Public | BindingFlags.Instance).GetValue (attr, null);

					if (noValidate is bool)
					{
						bool noValidateValue	= Convert.ToBoolean (noValidate);

						if (noValidateValue)
						{
							if (nullable is bool)
							{
								bool isNull	= Convert.ToBoolean (nullable);

								if (!isNull && (value == null))
									result.message.Add(string.Format("{0} value can't be null", prop.Name));
							}

							decimal	fieldSize;

							if (Decimal.TryParse (size.ToString (), out fieldSize))
								if ((fieldSize > 0) && (value != null) && (value.ToString().Length > fieldSize))
									result.message.Add(string.Format("{0} size error, maximum length is {1}", prop.Name, fieldSize));
						}
					}
				}

				result.isValid	= (result.message.Count == 0);
			}

			return result;
		} 
	#endregion
	}
}
