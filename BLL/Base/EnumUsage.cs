namespace BaseBLL.Base
{
	/// <summary>
	/// Field Usage Enum
	/// </summary>
	public enum EnumUsage
	{
		create			= 1,
		read			= 2,
		readCriteria	= 4,
		update			= 8,
		updateCriteria	= 16,
		delete			= 32,
		all				= 256
	}
}