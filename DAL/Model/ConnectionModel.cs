using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDAL.Model
{
    /// <summary>
    /// Connection Model
    /// </summary>
    public class ConnectionModel
    {
    #region Properties
        public string userId
        {
            get;
            set;
        }
        public string password
        {
            get;
            set;
        }
        public string dataSource
        {
            get;
            set;
        }
        public string initCatalog
        {
            get;
            set;
        }
        public bool? integratedSec
        {
            get;
            set;
        }
        public int? timeOut
        {
            get;
            set;
        } 

        /// <summary>
        /// Get Connection string
        /// </summary>
        public string connectionString
        {
            get
            {
                string result;

				string datasourceStr	= (string.IsNullOrEmpty (dataSource) ? "" : string.Format (";Data Source={0}", dataSource));
				string initCatStr		= (string.IsNullOrEmpty (initCatalog) ? "" : string.Format (";Initial Catalog={0}", initCatalog));
				string integStr			= (integratedSec.HasValue ? string.Format (";Integrated Security={0}", integratedSec.Value) : "");
				string timeOutStr		= (timeOut.HasValue ? string.Format (";Timeout={0}", timeOut.Value) : "");
				string userIdStr		= (string.IsNullOrEmpty (userId) ? "" : string.Format (";User id={0}", userId));
				string passwordStr		= (string.IsNullOrEmpty (password) ? "" : string.Format (";Password={0}", password));

				result	= datasourceStr + initCatStr + integStr + timeOutStr + userIdStr + passwordStr;

                return result;
            }
        }
    #endregion
    }
}
