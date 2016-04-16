using BaseDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
			
			BaseDAL.Base.Connection.dataSources.Add (Common.Enum.DAL.enumConnectionType.Test.ToString (), 
				new BaseDAL.Model.ConnectionModel () { dataSource=".", initCatalog="ProjectManagement", integratedSec=true });
			
			BLL.Logic.Document	lDoc	= new BLL.Logic.Document (Common.Enum.DAL.enumConnectionType.Test.ToString ());
			CommandResult	cR	= lDoc.create (new BLL.Entity.Document () {
				areaId			= 1,
				assumed			= "",
				condominium		= "",
				dateArchives	= DateTime.Now,
				folderName		= "XXX",
				implicitPlaque	= 20,
				mainPlaque		= 100,
				operatorId		= 1,
				piece			= "",
				recordsUnitId	= 1,
				regDate			= DateTime.Now,
				remaining		= true,
				sectionId		= 1,
				subPlaque		= 200,
				Znumber			= 999,
				resume			= true
			});

			
		}
	}
}
