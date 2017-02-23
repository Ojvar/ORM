using BaseDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ojvarORM
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);
			//Application.Run (new MainForm ());

			BaseDAL.Base.Connection.dataSources.Add ("x", new BaseDAL.Model.ConnectionModel ()
			{
				dataSource  = "nod7251",
				initCatalog = "Account",
				password    = "1365",
				userId      = "sa"
			});

			BLL.Logic.ExceptionLog l = new BLL.Logic.ExceptionLog ("x");
			BLL.Entity.ExceptionLog e = new BLL.Entity.ExceptionLog ();

			e.clientIP		= "ip3333333333";
			e.clientName    = "name33333333";
			e.exception     = "exceptio33333333333n";

			CommandResult res;

			l.beginTransaction (false);

			res = l.create (e, true, "insertDate");

			e.id = 23;
			res = l.delete (e);

			res =  l.commitTransaction ();
			//res =  l.rollBackTransaction ();
		}
	}
}
