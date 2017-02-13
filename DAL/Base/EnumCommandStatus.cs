namespace BaseDAL.Base
{
	/// <summary>
	/// Command Result Enum
	/// </summary>
	public enum EnumCommandStatus
	{
		success					= 1,
		executeFailed			= 2,
		connectionFailed		= 4,
		unknown					= 8,
		operationFailed			= 16,
		authenticaitonFailed	= 32
	}
}