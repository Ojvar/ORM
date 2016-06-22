namespace ClassGenerator
{
	partial class SqlGenerator
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.messageLabel = new System.Windows.Forms.Label();
			this.connectToDbButton = new System.Windows.Forms.Button();
			this.passwordTextbox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.usernameTextbox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.initialiCatalogTextbox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.dataSourceTextbox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pageTabControl = new System.Windows.Forms.TabControl();
			this.connectionTabPage = new System.Windows.Forms.TabPage();
			this.databaseTabPage = new System.Windows.Forms.TabPage();
			this.loadFieldsButton = new System.Windows.Forms.Button();
			this.tablesRefreshButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tablesGrid = new System.Windows.Forms.DataGridView();
			this.fieldsTabPage = new System.Windows.Forms.TabPage();
			this.saveToFileCheckbox = new System.Windows.Forms.CheckBox();
			this.savePathTextbox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.postfixNamespaceTextbox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.namespaceTextbox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.entityBaseCombobox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.fieldsRefreshButton = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.fieldsGrid = new System.Windows.Forms.DataGridView();
			this.generateScriptButton = new System.Windows.Forms.Button();
			this.scriptTabPage = new System.Windows.Forms.TabPage();
			this.scriptTextbox = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			this.pageTabControl.SuspendLayout();
			this.connectionTabPage.SuspendLayout();
			this.databaseTabPage.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tablesGrid)).BeginInit();
			this.fieldsTabPage.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fieldsGrid)).BeginInit();
			this.scriptTabPage.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.messageLabel);
			this.groupBox1.Controls.Add(this.connectToDbButton);
			this.groupBox1.Controls.Add(this.passwordTextbox);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.usernameTextbox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.initialiCatalogTextbox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.dataSourceTextbox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(7, 8);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Size = new System.Drawing.Size(777, 161);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection Parameters";
			// 
			// messageLabel
			// 
			this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.messageLabel.AutoSize = true;
			this.messageLabel.ForeColor = System.Drawing.Color.Maroon;
			this.messageLabel.Location = new System.Drawing.Point(17, 123);
			this.messageLabel.Name = "messageLabel";
			this.messageLabel.Size = new System.Drawing.Size(95, 15);
			this.messageLabel.TabIndex = 5;
			this.messageLabel.Text = "Last Message : -";
			// 
			// connectToDbButton
			// 
			this.connectToDbButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.connectToDbButton.Location = new System.Drawing.Point(647, 112);
			this.connectToDbButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.connectToDbButton.Name = "connectToDbButton";
			this.connectToDbButton.Size = new System.Drawing.Size(124, 41);
			this.connectToDbButton.TabIndex = 4;
			this.connectToDbButton.Text = "Connect";
			this.connectToDbButton.UseVisualStyleBackColor = true;
			// 
			// passwordTextbox
			// 
			this.passwordTextbox.Location = new System.Drawing.Point(401, 61);
			this.passwordTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.passwordTextbox.Name = "passwordTextbox";
			this.passwordTextbox.Size = new System.Drawing.Size(193, 23);
			this.passwordTextbox.TabIndex = 3;
			this.passwordTextbox.Text = "1365";
			this.passwordTextbox.UseSystemPasswordChar = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(337, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(58, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Password";
			// 
			// usernameTextbox
			// 
			this.usernameTextbox.Location = new System.Drawing.Point(112, 62);
			this.usernameTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.usernameTextbox.Name = "usernameTextbox";
			this.usernameTextbox.Size = new System.Drawing.Size(193, 23);
			this.usernameTextbox.TabIndex = 2;
			this.usernameTextbox.Text = "sa";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(48, 66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "User Name";
			// 
			// initialiCatalogTextbox
			// 
			this.initialiCatalogTextbox.Location = new System.Drawing.Point(401, 30);
			this.initialiCatalogTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.initialiCatalogTextbox.Name = "initialiCatalogTextbox";
			this.initialiCatalogTextbox.Size = new System.Drawing.Size(193, 23);
			this.initialiCatalogTextbox.TabIndex = 1;
			this.initialiCatalogTextbox.Text = "PMOld";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(311, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "Initial Catalog";
			// 
			// dataSourceTextbox
			// 
			this.dataSourceTextbox.Location = new System.Drawing.Point(112, 31);
			this.dataSourceTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dataSourceTextbox.Name = "dataSourceTextbox";
			this.dataSourceTextbox.Size = new System.Drawing.Size(193, 23);
			this.dataSourceTextbox.TabIndex = 0;
			this.dataSourceTextbox.Text = ".";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server Address";
			// 
			// pageTabControl
			// 
			this.pageTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pageTabControl.Controls.Add(this.connectionTabPage);
			this.pageTabControl.Controls.Add(this.databaseTabPage);
			this.pageTabControl.Controls.Add(this.fieldsTabPage);
			this.pageTabControl.Controls.Add(this.scriptTabPage);
			this.pageTabControl.Font = new System.Drawing.Font("Tahoma", 9F);
			this.pageTabControl.Location = new System.Drawing.Point(14, 43);
			this.pageTabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.pageTabControl.Name = "pageTabControl";
			this.pageTabControl.SelectedIndex = 0;
			this.pageTabControl.Size = new System.Drawing.Size(798, 411);
			this.pageTabControl.TabIndex = 1;
			// 
			// connectionTabPage
			// 
			this.connectionTabPage.Controls.Add(this.groupBox1);
			this.connectionTabPage.Location = new System.Drawing.Point(4, 24);
			this.connectionTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.connectionTabPage.Name = "connectionTabPage";
			this.connectionTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.connectionTabPage.Size = new System.Drawing.Size(790, 383);
			this.connectionTabPage.TabIndex = 0;
			this.connectionTabPage.Text = "Connection";
			this.connectionTabPage.UseVisualStyleBackColor = true;
			// 
			// databaseTabPage
			// 
			this.databaseTabPage.Controls.Add(this.loadFieldsButton);
			this.databaseTabPage.Controls.Add(this.tablesRefreshButton);
			this.databaseTabPage.Controls.Add(this.groupBox2);
			this.databaseTabPage.Location = new System.Drawing.Point(4, 24);
			this.databaseTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.databaseTabPage.Name = "databaseTabPage";
			this.databaseTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.databaseTabPage.Size = new System.Drawing.Size(790, 383);
			this.databaseTabPage.TabIndex = 1;
			this.databaseTabPage.Text = "Database Details";
			this.databaseTabPage.UseVisualStyleBackColor = true;
			// 
			// loadFieldsButton
			// 
			this.loadFieldsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.loadFieldsButton.Location = new System.Drawing.Point(660, 331);
			this.loadFieldsButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.loadFieldsButton.Name = "loadFieldsButton";
			this.loadFieldsButton.Size = new System.Drawing.Size(124, 41);
			this.loadFieldsButton.TabIndex = 1;
			this.loadFieldsButton.Text = "Load Fields";
			this.loadFieldsButton.UseVisualStyleBackColor = true;
			// 
			// tablesRefreshButton
			// 
			this.tablesRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tablesRefreshButton.Location = new System.Drawing.Point(530, 331);
			this.tablesRefreshButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tablesRefreshButton.Name = "tablesRefreshButton";
			this.tablesRefreshButton.Size = new System.Drawing.Size(124, 41);
			this.tablesRefreshButton.TabIndex = 1;
			this.tablesRefreshButton.Text = "Refresh";
			this.tablesRefreshButton.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.tablesGrid);
			this.groupBox2.Location = new System.Drawing.Point(7, 8);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox2.Size = new System.Drawing.Size(777, 315);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Table(s) List";
			// 
			// tablesGrid
			// 
			this.tablesGrid.AllowUserToAddRows = false;
			this.tablesGrid.AllowUserToDeleteRows = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tablesGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			this.tablesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tablesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.tablesGrid.BackgroundColor = System.Drawing.Color.White;
			this.tablesGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.tablesGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.tablesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tablesGrid.Location = new System.Drawing.Point(7, 28);
			this.tablesGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tablesGrid.MultiSelect = false;
			this.tablesGrid.Name = "tablesGrid";
			this.tablesGrid.ReadOnly = true;
			this.tablesGrid.RowTemplate.Height = 24;
			this.tablesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.tablesGrid.Size = new System.Drawing.Size(763, 279);
			this.tablesGrid.TabIndex = 0;
			// 
			// fieldsTabPage
			// 
			this.fieldsTabPage.Controls.Add(this.saveToFileCheckbox);
			this.fieldsTabPage.Controls.Add(this.savePathTextbox);
			this.fieldsTabPage.Controls.Add(this.label7);
			this.fieldsTabPage.Controls.Add(this.postfixNamespaceTextbox);
			this.fieldsTabPage.Controls.Add(this.label8);
			this.fieldsTabPage.Controls.Add(this.namespaceTextbox);
			this.fieldsTabPage.Controls.Add(this.label6);
			this.fieldsTabPage.Controls.Add(this.entityBaseCombobox);
			this.fieldsTabPage.Controls.Add(this.label5);
			this.fieldsTabPage.Controls.Add(this.fieldsRefreshButton);
			this.fieldsTabPage.Controls.Add(this.groupBox3);
			this.fieldsTabPage.Controls.Add(this.generateScriptButton);
			this.fieldsTabPage.Location = new System.Drawing.Point(4, 24);
			this.fieldsTabPage.Name = "fieldsTabPage";
			this.fieldsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.fieldsTabPage.Size = new System.Drawing.Size(790, 383);
			this.fieldsTabPage.TabIndex = 3;
			this.fieldsTabPage.Text = "Fields";
			this.fieldsTabPage.UseVisualStyleBackColor = true;
			// 
			// saveToFileCheckbox
			// 
			this.saveToFileCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.saveToFileCheckbox.AutoSize = true;
			this.saveToFileCheckbox.Location = new System.Drawing.Point(211, 363);
			this.saveToFileCheckbox.Name = "saveToFileCheckbox";
			this.saveToFileCheckbox.Size = new System.Drawing.Size(92, 19);
			this.saveToFileCheckbox.TabIndex = 3;
			this.saveToFileCheckbox.Text = "Save To File";
			this.saveToFileCheckbox.UseVisualStyleBackColor = true;
			// 
			// savePathTextbox
			// 
			this.savePathTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.savePathTextbox.Location = new System.Drawing.Point(134, 337);
			this.savePathTextbox.Name = "savePathTextbox";
			this.savePathTextbox.Size = new System.Drawing.Size(169, 23);
			this.savePathTextbox.TabIndex = 2;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(66, 341);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(62, 15);
			this.label7.TabIndex = 5;
			this.label7.Text = "Save Path";
			// 
			// postfixNamespaceTextbox
			// 
			this.postfixNamespaceTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.postfixNamespaceTextbox.Location = new System.Drawing.Point(457, 337);
			this.postfixNamespaceTextbox.Name = "postfixNamespaceTextbox";
			this.postfixNamespaceTextbox.Size = new System.Drawing.Size(122, 23);
			this.postfixNamespaceTextbox.TabIndex = 5;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(340, 341);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(111, 15);
			this.label8.TabIndex = 5;
			this.label8.Text = "PostFix-Namespace";
			// 
			// namespaceTextbox
			// 
			this.namespaceTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.namespaceTextbox.Location = new System.Drawing.Point(457, 308);
			this.namespaceTextbox.Name = "namespaceTextbox";
			this.namespaceTextbox.Size = new System.Drawing.Size(122, 23);
			this.namespaceTextbox.TabIndex = 4;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(382, 312);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(69, 15);
			this.label6.TabIndex = 5;
			this.label6.Text = "Namespace";
			// 
			// entityBaseCombobox
			// 
			this.entityBaseCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.entityBaseCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.entityBaseCombobox.FormattingEnabled = true;
			this.entityBaseCombobox.Location = new System.Drawing.Point(134, 308);
			this.entityBaseCombobox.Name = "entityBaseCombobox";
			this.entityBaseCombobox.Size = new System.Drawing.Size(169, 23);
			this.entityBaseCombobox.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(32, 311);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 15);
			this.label5.TabIndex = 1;
			this.label5.Text = "EntityBase Type";
			// 
			// fieldsRefreshButton
			// 
			this.fieldsRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.fieldsRefreshButton.Location = new System.Drawing.Point(659, 302);
			this.fieldsRefreshButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.fieldsRefreshButton.Name = "fieldsRefreshButton";
			this.fieldsRefreshButton.Size = new System.Drawing.Size(124, 34);
			this.fieldsRefreshButton.TabIndex = 6;
			this.fieldsRefreshButton.Text = "Refresh";
			this.fieldsRefreshButton.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.fieldsGrid);
			this.groupBox3.Location = new System.Drawing.Point(7, 8);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox3.Size = new System.Drawing.Size(776, 293);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Field(s) List";
			// 
			// fieldsGrid
			// 
			this.fieldsGrid.AllowUserToAddRows = false;
			this.fieldsGrid.AllowUserToDeleteRows = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
			this.fieldsGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.fieldsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fieldsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.fieldsGrid.BackgroundColor = System.Drawing.Color.White;
			this.fieldsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.fieldsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.fieldsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.fieldsGrid.Location = new System.Drawing.Point(7, 28);
			this.fieldsGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.fieldsGrid.MultiSelect = false;
			this.fieldsGrid.Name = "fieldsGrid";
			this.fieldsGrid.RowTemplate.Height = 24;
			this.fieldsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.fieldsGrid.Size = new System.Drawing.Size(762, 257);
			this.fieldsGrid.TabIndex = 0;
			// 
			// generateScriptButton
			// 
			this.generateScriptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.generateScriptButton.Location = new System.Drawing.Point(660, 342);
			this.generateScriptButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.generateScriptButton.Name = "generateScriptButton";
			this.generateScriptButton.Size = new System.Drawing.Size(124, 34);
			this.generateScriptButton.TabIndex = 7;
			this.generateScriptButton.Text = "Generate";
			this.generateScriptButton.UseVisualStyleBackColor = true;
			// 
			// scriptTabPage
			// 
			this.scriptTabPage.Controls.Add(this.scriptTextbox);
			this.scriptTabPage.Location = new System.Drawing.Point(4, 24);
			this.scriptTabPage.Name = "scriptTabPage";
			this.scriptTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.scriptTabPage.Size = new System.Drawing.Size(790, 383);
			this.scriptTabPage.TabIndex = 2;
			this.scriptTabPage.Text = "Script";
			this.scriptTabPage.UseVisualStyleBackColor = true;
			// 
			// scriptTextbox
			// 
			this.scriptTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.scriptTextbox.Font = new System.Drawing.Font("Gadugi", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.scriptTextbox.Location = new System.Drawing.Point(6, 6);
			this.scriptTextbox.Multiline = true;
			this.scriptTextbox.Name = "scriptTextbox";
			this.scriptTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.scriptTextbox.Size = new System.Drawing.Size(778, 368);
			this.scriptTextbox.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F);
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
			this.menuStrip1.Size = new System.Drawing.Size(826, 25);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitMenu});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 19);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// quitMenu
			// 
			this.quitMenu.Name = "quitMenu";
			this.quitMenu.Size = new System.Drawing.Size(97, 22);
			this.quitMenu.Text = "Quit";
			// 
			// SqlGenerator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(826, 470);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.pageTabControl);
			this.Font = new System.Drawing.Font("Tahoma", 10F);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SqlGenerator";
			this.Text = "Class Generator";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.pageTabControl.ResumeLayout(false);
			this.connectionTabPage.ResumeLayout(false);
			this.databaseTabPage.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tablesGrid)).EndInit();
			this.fieldsTabPage.ResumeLayout(false);
			this.fieldsTabPage.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.fieldsGrid)).EndInit();
			this.scriptTabPage.ResumeLayout(false);
			this.scriptTabPage.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button connectToDbButton;
		private System.Windows.Forms.TextBox passwordTextbox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox usernameTextbox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox initialiCatalogTextbox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl pageTabControl;
		private System.Windows.Forms.TabPage connectionTabPage;
		private System.Windows.Forms.TabPage databaseTabPage;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quitMenu;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView tablesGrid;
		private System.Windows.Forms.Button tablesRefreshButton;
		private System.Windows.Forms.TextBox dataSourceTextbox;
		private System.Windows.Forms.Label messageLabel;
		private System.Windows.Forms.TabPage scriptTabPage;
		private System.Windows.Forms.TextBox scriptTextbox;
		private System.Windows.Forms.Button loadFieldsButton;
		private System.Windows.Forms.TabPage fieldsTabPage;
		private System.Windows.Forms.Button fieldsRefreshButton;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView fieldsGrid;
		private System.Windows.Forms.Button generateScriptButton;
		private System.Windows.Forms.ComboBox entityBaseCombobox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox namespaceTextbox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox savePathTextbox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox saveToFileCheckbox;
		private System.Windows.Forms.TextBox postfixNamespaceTextbox;
		private System.Windows.Forms.Label label8;
	}
}

