﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassGenerator.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Field {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Field() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ClassGenerator.Resources.Field", typeof(Field).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [BaseBLL.Base.Field({0})].
        /// </summary>
        internal static string FieldAttribute {
            get {
                return ResourceManager.GetString("FieldAttribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //
        ///	// Genereted Property of {3}
        ///	//
        ///	#region Referenced Property - {2}
        ///		BLL.Entity{8}.{0}{7} _{2}_{3};
        ///		public BLL.Entity{8}.{0}{7} {2}_{3}
        ///		{{
        ///			get
        ///			{{
        ///				if ((null == _{2}_{3}) &amp;&amp; (AutoLoadForeignKeys))
        ///					load_{2}_{3} ();
        ///				return _{2}_{3};
        ///			}}
        ///			set
        ///			{{
        ///				_{2}_{3}	= value;
        ///			}}
        ///		}}
        ///
        ///		public void load_{2}_{3} ()
        ///		{{ 
        ///			BLL.Entity{8}.{0}	entity;
        ///			BLL.Logic{8}.{1}	logic;
        ///
        ///			entity	= new BLL.Entity{8}.{0} () {{ {5} = {3} }};
        ///			logic	= new BLL.Logic{8}.{1 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string FieldForeignKey {
            get {
                return ResourceManager.GetString("FieldForeignKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //
        ///	// Genereted Property of {3}
        ///	//
        ///	#region Referenced Property - {2}
        ///		BLL.Entity{8}.{0}{7} _{2}_{3};
        ///		public BLL.Entity{8}.{0}{7} {2}_{3}
        ///		{{
        ///			get
        ///			{{
        ///				if ((null == _{2}_{3}) &amp;&amp; ({3}.HasValue) &amp;&amp; (AutoLoadForeignKeys))
        ///					load_{2}_{3} ();
        ///				return _{2}_{3};
        ///			}}
        ///			set
        ///			{{
        ///				_{2}_{3}	= value;
        ///			}}
        ///		}}
        ///
        ///		public void load_{2}_{3} ()
        ///		{{ 
        ///			BLL.Entity{8}.{0}	entity;
        ///			BLL.Logic{8}.{1}	logic;
        ///
        ///			entity	= new BLL.Entity{8}.{0} () {{ {5} = {3}.Value }};
        ///			log [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string FieldForeignKeyNullable {
            get {
                return ResourceManager.GetString("FieldForeignKeyNullable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///		{0}
        ///		public {1} {2}
        ///		{{
        ///			get;
        ///			set;
        ///		}}.
        /// </summary>
        internal static string FieldProperty {
            get {
                return ResourceManager.GetString("FieldProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //
        ///	// Genereted Property of {0}
        ///	//
        ///	#region Relation - {0} (Has-Many relation)
        ///		private System.Data.DataTable _get_{0}_{1};
        ///		public System.Data.DataTable get{0}_{1}
        ///		{{
        ///			get
        ///			{{
        ///				if ((_get_{0}_{1} == null) &amp;&amp; (AutoLoadForeignKeys))
        ///					load{0}_{1} ();
        ///
        ///				return _get_{0}_{1};
        ///			}}
        ///			set
        ///			{{
        ///				_get_{0}_{1}	= value;
        ///			}}
        ///		}}
        ///
        ///		public void load{0}_{1} (int pageIndex = -1, int pageSize = 100)
        ///		{{
        ///			CommandResult	opResult;
        ///
        ///			BLL.Logic{4}.{0}	logic	= new BLL.Logi [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string FieldReferentialKey {
            get {
                return ResourceManager.GetString("FieldReferentialKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///WITH FK AS
        ///	(
        ///	SELECT  
        ///			RC.CONSTRAINT_CATALOG
        ///			,RC.CONSTRAINT_SCHEMA 
        ///			,KCU1.CONSTRAINT_NAME AS FK_CONSTRAINT_NAME 
        ///			,KCU1.TABLE_NAME AS FK_TABLE_NAME 
        ///			,KCU1.COLUMN_NAME AS FK_COLUMN_NAME 
        ///			,KCU1.ORDINAL_POSITION AS FK_ORDINAL_POSITION 
        ///			,KCU2.CONSTRAINT_NAME AS REFERENCED_CONSTRAINT_NAME 
        ///			,KCU2.TABLE_NAME AS REFERENCED_TABLE_NAME 
        ///			,KCU2.COLUMN_NAME AS REFERENCED_COLUMN_NAME 
        ///			,KCU2.ORDINAL_POSITION AS REFERENCED_ORDINAL_POSITION 
        ///			,RC.UPDATE_RULE
        ///			,RC.DELETE_RULE [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetInformation {
            get {
                return ResourceManager.GetString("GetInformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///WITH base AS
        ///(
        ///	SELECT
        ///			OBJECT_NAME (FK.referenced_object_id) AS primaryTable,
        ///			COL_NAME (FK.referenced_object_id, FKC.referenced_column_id) AS primaryColumn,
        ///			OBJECT_NAME (FK.parent_object_id) AS foreignTable,
        ///			COL_NAME (FK.parent_object_id, FKC.parent_column_id) AS foreignColumn
        ///	FROM
        ///			[{0}].sys.foreign_keys AS FK
        ///		INNER JOIN [{0}].sys.foreign_key_columns AS FKC ON (FKC.constraint_object_id = FK.object_id)
        ///)
        ///
        ///SELECT
        ///		*
        ///FROM
        ///		base
        ///WHERE
        ///		([primaryTable] = &apos;{1}&apos;) AND
        ///		([pr [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GetReferentialInformation {
            get {
                return ResourceManager.GetString("GetReferentialInformation", resourceCulture);
            }
        }
    }
}
