using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BaseBLL.General
{
	/// <summary>
	/// Form Model Helper
	/// </summary>
	public class FormModelHelper<T> where T : new ()
	{
	#region Methods
		/// <summary>
		/// Fill Controls with Model
		/// </summary>
		public static bool fillControl (Control baseControl, T data, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public)
		{
			bool		result		= true;
			Control[]	controls	= null;

			if ((null != baseControl) && (null != data))
			{
				PropertyInfo[]	properties	= null;

				// Get all controls
				controls	= getControls (baseControl);
				properties	= data.GetType ().GetProperties (flags);

				if (null != properties)
				{
					foreach (PropertyInfo info in properties)
					{
						Control	ctl	= (from c in controls where ((c.Tag ?? "").ToString ().ToLower () == info.Name.ToLower ()) select c).FirstOrDefault<Control> ();

						if (null != ctl)
						{
							object	iValue	= info.GetValue (data, null);

							try
							{
								if ((ctl is TextBox) || (ctl is Label))
									ctl.Text	= (iValue ?? (object)"").ToString ();
								else if (ctl is NumericUpDown)
								{
									NumericUpDown	numCtl	= (NumericUpDown)ctl;
									decimal			valueN	= Convert.ToDecimal (iValue ?? (object)0);

									valueN	= Math.Max (valueN, numCtl.Minimum);
									valueN	= Math.Min (valueN, numCtl.Maximum);
									numCtl.Value	= valueN;
								}
								else if (ctl is ComboBox)
								{
									if (iValue == null)
										((ComboBox)ctl).SelectedIndex	= -1;
									else
										((ComboBox)ctl).SelectedValue	= iValue;
								}
							}
							catch
                            {
								result	= false;
							}
						}
					}
				}
			}

			return result;
		} 

		/// <summary>
		/// Fill Model with Form Controls
		/// </summary>
		public static bool fillModel (Control baseControl, T data, BindingFlags flags = BindingFlags.Instance | BindingFlags.Public)
		{
			bool		result		= true;
			Control[]	controls	= null;

			if ((null != baseControl) && (null != data))
			{
				PropertyInfo[]	properties	= null;

				// Get all controls
				controls	= getControls (baseControl);
				properties	= data.GetType ().GetProperties (flags);

				if (null != properties)
					foreach (PropertyInfo info in properties)
					{
						Control	ctl	= (from c in controls where ((c.Tag ?? "").ToString ().ToLower () == info.Name.ToLower ()) select c).FirstOrDefault<Control> ();

						if (null != ctl)
							try
							{
								object	value	= null;

								if ((ctl is TextBox) || (ctl is Label))
									value	= ctl.Text;
								else if (ctl is NumericUpDown)
									value	= ((NumericUpDown)ctl).Value;
								else if (ctl is ComboBox)
									value	= ((ComboBox)ctl).SelectedValue;

								if (Nullable.GetUnderlyingType (info.PropertyType) != null)
									info.SetValue (data, value, null);
								else 
									info.SetValue (data, Convert.ChangeType (value, info.PropertyType), null);
							}
							catch
							{
								result	= false;
							}
					}
			}

			return result;
		} 

		/// <summary>
		/// Get all controls 
		/// </summary>
		/// <param name="baseControl"></param>
		/// <returns></returns>
		public static Control[] getControls (Control baseControl)
		{
			List<Control>	result	= new List<Control> ();

			if (null != baseControl)
			{
				List<Control>	stack	= new List<Control> ();

				// Add to stack
				foreach (Control control in baseControl.Controls)
					stack.Add (control);

				// Travers in stack
				while (stack.Count > 0)
				{
					Control	ctl	= stack[0];

					// Add childrens to stack
					if (ctl.HasChildren)
						foreach (Control control in ctl.Controls)	// Add to stack
							stack.Add (control);
					
					// Add to result
					result.Add (ctl);

					// Remove first item
					stack.RemoveAt (0);
				}
			}

			return result.ToArray ();
		}
	#endregion
	}
}
