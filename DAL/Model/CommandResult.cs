using BaseDAL.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseDAL.Model
{
	/// <summary>
	/// Command result
	/// </summary>
	public class CommandResult
	{
	#region Properties
		/// <summary>
		/// Message
		/// </summary>
		public string message
		{
			get;
			set;
		}

		/// <summary>
		/// Status
		/// </summary>
		public EnumCommandStatus status
		{
			get;
			set;
		}

		/// <summary>
		/// Model
		/// </summary>
		public object model
		{
			get;
			set;
		} 
		
		/// <summary>
		/// Extra data
		/// </summary>
		public object extra
		{
			get;
			set;
		}

		/// <summary>
		/// Message/Error Number
		/// </summary>
		public int id
		{
			get;
			set;
		}
        #endregion

    #region Variables
        public const int    C_ID_SUCCESS    = 0;
        public const int    C_ID_NULLDATA   = 1;
        public const int    C_ID_ERR        = 999;
    #endregion

    #region Methods
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandResult ()
		{
		}

        /// <summary>
        /// Make a new CommandResult
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <param name="model"></param>
        /// <param name="extra"></param>
        public static CommandResult makeResult (int id = 0, EnumCommandStatus status = EnumCommandStatus.unknown, string message = "", object model = null, object extra = null)
        {
            CommandResult   result  = new CommandResult ()
            {
                id      = id,
                message = message,
                status  = status,
                model   = model,
                extra   = extra
            };

            return result;
        }

        /// <summary>
        /// Make a success CommandResult
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <param name="model"></param>
        /// <param name="extra"></param>
        public static CommandResult makeSuccessResult (string message = "", object model = null, object extra = null)
        {
            CommandResult   result  = new CommandResult ()
            {
                id      = C_ID_SUCCESS,
                message = message,
                status  = EnumCommandStatus.success,
                model   = model,
                extra   = extra
            };

            return result;
        }
        
        /// <summary>
        /// Make a error CommandResult
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <param name="model"></param>
        /// <param name="extra"></param>
        public static CommandResult makeErrorResult (string message = "", object model = null, object extra = null)
        {
            CommandResult   result  = new CommandResult ()
            {
                id      = C_ID_ERR,
                message = message,
                status  = EnumCommandStatus.operationFailed,
                model   = model,
                extra   = extra
            };

            return result;
        }
        
        /// <summary>
        /// Make a null-data CommandResult
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <param name="model"></param>
        /// <param name="extra"></param>
        public static CommandResult makeNullDataResult (string message = "", object model = null, object extra = null)
        {
            CommandResult   result  = new CommandResult ()
            {
                id      = C_ID_NULLDATA,
                message = message,
                status  = EnumCommandStatus.operationFailed,
                model   = model,
                extra   = extra
            };

            return result;
        }
	#endregion
	}
}
