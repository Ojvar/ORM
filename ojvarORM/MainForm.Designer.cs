﻿namespace ojvarORM
{
	partial class MainForm
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtId = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtDataId = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(68, 37);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(178, 22);
			this.textBox1.TabIndex = 0;
			this.textBox1.Tag = "name";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(68, 65);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(178, 22);
			this.textBox2.TabIndex = 1;
			this.textBox2.Tag = "family";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(68, 93);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(178, 22);
			this.textBox3.TabIndex = 2;
			this.textBox3.Tag = "age";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(68, 121);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(93, 31);
			this.button1.TabIndex = 3;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(370, 113);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(93, 31);
			this.button2.TabIndex = 4;
			this.button2.Text = "Read";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Name";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Family";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 98);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(33, 17);
			this.label3.TabIndex = 3;
			this.label3.Text = "Age";
			// 
			// txtId
			// 
			this.txtId.Location = new System.Drawing.Point(282, 37);
			this.txtId.Name = "txtId";
			this.txtId.Size = new System.Drawing.Size(178, 22);
			this.txtId.TabIndex = 0;
			this.txtId.Tag = "id";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(282, 68);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(91, 31);
			this.button4.TabIndex = 5;
			this.button4.Text = "delete";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button3_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtDataId);
			this.groupBox1.Controls.Add(this.textBox5);
			this.groupBox1.Controls.Add(this.textBox6);
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Location = new System.Drawing.Point(514, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(457, 152);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// txtDataId
			// 
			this.txtDataId.Location = new System.Drawing.Point(21, 28);
			this.txtDataId.Name = "txtDataId";
			this.txtDataId.Size = new System.Drawing.Size(178, 22);
			this.txtDataId.TabIndex = 0;
			this.txtDataId.Tag = "id";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(21, 56);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(178, 22);
			this.textBox5.TabIndex = 1;
			this.textBox5.Tag = "testId";
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(21, 84);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(178, 22);
			this.textBox6.TabIndex = 2;
			this.textBox6.Tag = "data1";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(32, 112);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(93, 31);
			this.button3.TabIndex = 4;
			this.button3.Text = "Read";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click_1);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.textBox3);
			this.groupBox2.Controls.Add(this.textBox2);
			this.groupBox2.Controls.Add(this.txtId);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Location = new System.Drawing.Point(9, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(494, 162);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "groupBox2";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1007, 177);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtDataId;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button button3;
	}
}

