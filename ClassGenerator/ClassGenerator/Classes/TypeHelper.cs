using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator.Classes
{
	/// <summary>
	/// Type Convert
	/// </summary>
	public class TypeHelper
	{
	#region Variables
		public static Dictionary<Type, SqlDbType> sqlDbType;
		public static Dictionary<SqlDbType, Type> csType;
	#endregion

	#region Methods
		/// <summary>
		/// TypeConverter Constructor
		/// </summary>
		static TypeHelper ()
		{
		#region CS To SQL Mapper
			sqlDbType = new Dictionary<Type, SqlDbType> ();

			sqlDbType[typeof (byte)]				= SqlDbType.TinyInt;
			sqlDbType[typeof (sbyte)]				= SqlDbType.TinyInt;
			sqlDbType[typeof (short)]				= SqlDbType.SmallInt;
			sqlDbType[typeof (ushort)]				= SqlDbType.SmallInt;
			sqlDbType[typeof (int)]					= SqlDbType.Int;
			sqlDbType[typeof (uint)]				= SqlDbType.Int;
			sqlDbType[typeof (long)]				= SqlDbType.BigInt;
			sqlDbType[typeof (ulong)]				= SqlDbType.BigInt;
			sqlDbType[typeof (float)]				= SqlDbType.Float;
			sqlDbType[typeof (double)]				= SqlDbType.Float;
			sqlDbType[typeof (decimal)]				= SqlDbType.Decimal;
			sqlDbType[typeof (bool)]				= SqlDbType.Bit;
			sqlDbType[typeof (string)]				= SqlDbType.Text;
			sqlDbType[typeof (char)]				= SqlDbType.Char;
			sqlDbType[typeof (Guid)]				= SqlDbType.UniqueIdentifier;
			sqlDbType[typeof (DateTime)]			= SqlDbType.DateTime;
			sqlDbType[typeof (DateTimeOffset)]		= SqlDbType.DateTimeOffset;
			sqlDbType[typeof (byte[])]				= SqlDbType.Binary;
            sqlDbType[typeof(byte[])]               = SqlDbType.VarBinary;
            sqlDbType[typeof (byte?)]				= SqlDbType.TinyInt;
			sqlDbType[typeof (sbyte?)]				= SqlDbType.TinyInt;
			sqlDbType[typeof (short?)]				= SqlDbType.SmallInt;
			sqlDbType[typeof (ushort?)]				= SqlDbType.SmallInt;
			sqlDbType[typeof (int?)]				= SqlDbType.Int;
			sqlDbType[typeof (uint?)]				= SqlDbType.Int;
			sqlDbType[typeof (long?)]				= SqlDbType.BigInt;
			sqlDbType[typeof (ulong?)]				= SqlDbType.BigInt;
			sqlDbType[typeof (float?)]				= SqlDbType.Float;
			sqlDbType[typeof (double?)]				= SqlDbType.Float;
			sqlDbType[typeof (decimal?)]			= SqlDbType.Decimal;
			sqlDbType[typeof (bool?)]				= SqlDbType.Bit;
			sqlDbType[typeof (char?)]				= SqlDbType.Char;
			sqlDbType[typeof (Guid?)]				= SqlDbType.UniqueIdentifier;
			sqlDbType[typeof (DateTime?)]			= SqlDbType.DateTime;
			sqlDbType[typeof (DateTimeOffset?)]		= SqlDbType.DateTimeOffset;
		#endregion

		#region Sql To CS Mapper
			csType = new Dictionary<SqlDbType, Type> ();
			
			csType[SqlDbType.TinyInt]			= typeof (byte);
			csType[SqlDbType.SmallInt]			= typeof (short);			
			csType[SqlDbType.Int]				= typeof (int);				
			csType[SqlDbType.BigInt]			= typeof (long);			
			//csType[SqlDbType.Float]				= typeof (float);			
			csType[SqlDbType.Float]				= typeof (double);			
			csType[SqlDbType.Decimal]			= typeof (decimal);			
			csType[SqlDbType.Bit]				= typeof (bool);			
			csType[SqlDbType.Text]				= typeof (string);			
			csType[SqlDbType.NText]				= typeof (string);			
			csType[SqlDbType.VarChar]			= typeof (string);			
			csType[SqlDbType.NVarChar]			= typeof (string);			
			csType[SqlDbType.Char]				= typeof (string);			
			csType[SqlDbType.NChar]				= typeof (string);			
			csType[SqlDbType.UniqueIdentifier]	= typeof (Guid);			
			csType[SqlDbType.Date]				= typeof (DateTime);		
			csType[SqlDbType.Time]				= typeof (TimeSpan);		
			csType[SqlDbType.DateTime]			= typeof (DateTime);		
			csType[SqlDbType.DateTime2]			= typeof (DateTime);		
			csType[SqlDbType.DateTimeOffset]	= typeof (DateTimeOffset);
			csType[SqlDbType.Binary]			= typeof (byte[]);
            csType[SqlDbType.VarBinary]         = typeof(byte[]);
            csType[SqlDbType.Timestamp]			= typeof (byte[]);
			csType[SqlDbType.Money]				= typeof (decimal);
			csType[SqlDbType.Image]				= typeof (byte[]);
            #endregion
        }
	#endregion
	}
}
