namespace BaseDAL.Base
{
	/// <summary>
	/// EnumExecuteType Enum
	/// </summary>
	public enum EnumExecuteType
	{
		nonQuery			= 1,
		scaler				= 2,
		reader				= 4,
		xmlReader			= 8,
		procedureReader		= 16,
		procedureNonQuery	= 32
	}
}