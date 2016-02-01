using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseBLL = BLL;
namespace ojvarORM
{
	public partial class MainForm : Form
	{
		public MainForm ()
		{
			InitializeComponent ();
		}

		private void Form1_Load (object sender, EventArgs e)
		{
			BLL.Logic.HC__Contracts	bC	= new BLL.Logic.HC__Contracts (DAL.Base.EnumConnectionType.Database1);
			var	x	= bC.allData ();

			BLL.Entity.HC__Contracts	hcC	= new BLL.Entity.HC__Contracts ();

			hcC.code		= 100;
			hcC.description	= "Test";
			hcC.contractType	= "CType";
			x	= bC.create (hcC);
		}

		/// <summary>
		///  My Logic
		/// </summary>
		BLL.Logic.tests	t	= new BLL.Logic.tests (DAL.Base.EnumConnectionType.Database1);

		/// <summary>
		/// Create Record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click (object sender, EventArgs e)
		{
			BLL.Entity.test	d	= new BLL.Entity.test ();

			BaseBLL.General.FormModelHelper<BLL.Entity.test>.fillModel (groupBox2, d);
			if (d.id == 0)
				t.create (d);
			else
				t.udpate (d);
		}

		/// <summary>
		/// Read Record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click (object sender, EventArgs e)
		{
			BLL.Entity.test	d	= new BLL.Entity.test (){ id  = Convert.ToInt32 (txtId.Text)};
			CommandResult	res	= t.read (d);
			BaseBLL.General.FormModelHelper<BLL.Entity.test>.fillControl (groupBox2, d);
		}

		private void button3_Click (object sender, EventArgs e)
		{
			BLL.Entity.test	d	= new BLL.Entity.test (){ id  = Convert.ToInt32 (txtId.Text)};
			CommandResult	res	= t.delete (d);
		}

		private void button3_Click_1 (object sender, EventArgs e)
		{
			BLL.Logic.relTB	rt	= new BLL.Logic.relTB(DAL.Base.EnumConnectionType.Database1);
			BLL.Entity.relTB	rtE	= new BLL.Entity.relTB () { testId= Convert.ToInt32 (txtDataId.Text) };

			CommandResult res	= rt.loadForeignKey (rtE, "testId");
			List<BLL.Entity.test>	dataList = res.model as List<BLL.Entity.test>;

			BaseBLL.General.FormModelHelper<BLL.Entity.test>.fillControl (groupBox2, dataList[0]);
		}
	}
}
