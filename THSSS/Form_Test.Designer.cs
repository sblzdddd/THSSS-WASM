namespace Shooting
{
	// Token: 0x02000109 RID: 265
	public partial class Form_Test : global::System.Windows.Forms.Form
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x00041FD4 File Offset: 0x000401D4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0004200C File Offset: 0x0004020C
		private void InitializeComponent()
		{
			this.numericUpDown1 = new global::System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new global::System.Windows.Forms.NumericUpDown();
			this.numericUpDown3 = new global::System.Windows.Forms.NumericUpDown();
			this.numericUpDown4 = new global::System.Windows.Forms.NumericUpDown();
			this.numericUpDown5 = new global::System.Windows.Forms.NumericUpDown();
			this.numericUpDown6 = new global::System.Windows.Forms.NumericUpDown();
			this.richTextBox1 = new global::System.Windows.Forms.RichTextBox();
			this.numericUpDown7 = new global::System.Windows.Forms.NumericUpDown();
			this.numericUpDown8 = new global::System.Windows.Forms.NumericUpDown();
			this.listBox1 = new global::System.Windows.Forms.ListBox();
			this.checkBox1 = new global::System.Windows.Forms.CheckBox();
			this.folderBrowserDialog1 = new global::System.Windows.Forms.FolderBrowserDialog();
			this.treeView1 = new global::System.Windows.Forms.TreeView();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown2).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown3).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown4).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown5).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown6).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown7).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown8).BeginInit();
			base.SuspendLayout();
			this.numericUpDown1.Location = new global::System.Drawing.Point(0, 0);
			global::System.Windows.Forms.NumericUpDown numericUpDown = this.numericUpDown1;
			int[] array = new int[4];
			array[0] = 255;
			numericUpDown.Maximum = new decimal(array);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new global::System.Drawing.Size(128, 21);
			this.numericUpDown1.TabIndex = 0;
			this.numericUpDown1.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown2.Location = new global::System.Drawing.Point(0, 22);
			global::System.Windows.Forms.NumericUpDown numericUpDown2 = this.numericUpDown2;
			array = new int[4];
			array[0] = 255;
			numericUpDown2.Maximum = new decimal(array);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new global::System.Drawing.Size(128, 21);
			this.numericUpDown2.TabIndex = 1;
			this.numericUpDown2.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown3.Location = new global::System.Drawing.Point(0, 44);
			global::System.Windows.Forms.NumericUpDown numericUpDown3 = this.numericUpDown3;
			array = new int[4];
			array[0] = 255;
			numericUpDown3.Maximum = new decimal(array);
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new global::System.Drawing.Size(128, 21);
			this.numericUpDown3.TabIndex = 2;
			this.numericUpDown3.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			global::System.Windows.Forms.NumericUpDown numericUpDown4 = this.numericUpDown4;
			array = new int[4];
			array[0] = 500;
			numericUpDown4.Increment = new decimal(array);
			this.numericUpDown4.Location = new global::System.Drawing.Point(0, 73);
			global::System.Windows.Forms.NumericUpDown numericUpDown5 = this.numericUpDown4;
			array = new int[4];
			array[0] = 100000;
			numericUpDown5.Maximum = new decimal(array);
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new global::System.Drawing.Size(128, 21);
			this.numericUpDown4.TabIndex = 3;
			this.numericUpDown4.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			global::System.Windows.Forms.NumericUpDown numericUpDown6 = this.numericUpDown5;
			array = new int[4];
			array[0] = 10;
			numericUpDown6.Increment = new decimal(array);
			this.numericUpDown5.Location = new global::System.Drawing.Point(0, 94);
			global::System.Windows.Forms.NumericUpDown numericUpDown7 = this.numericUpDown5;
			array = new int[4];
			array[0] = 10000;
			numericUpDown7.Maximum = new decimal(array);
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new global::System.Drawing.Size(128, 21);
			this.numericUpDown5.TabIndex = 4;
			this.numericUpDown5.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			global::System.Windows.Forms.NumericUpDown numericUpDown8 = this.numericUpDown6;
			array = new int[4];
			array[0] = 10;
			numericUpDown8.Increment = new decimal(array);
			this.numericUpDown6.Location = new global::System.Drawing.Point(0, 115);
			global::System.Windows.Forms.NumericUpDown numericUpDown9 = this.numericUpDown6;
			array = new int[4];
			array[0] = 10000;
			numericUpDown9.Maximum = new decimal(array);
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new global::System.Drawing.Size(128, 21);
			this.numericUpDown6.TabIndex = 5;
			this.numericUpDown6.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.richTextBox1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left;
			this.richTextBox1.Location = new global::System.Drawing.Point(0, 209);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new global::System.Drawing.Size(128, 272);
			this.richTextBox1.TabIndex = 8;
			this.richTextBox1.Text = "";
			global::System.Windows.Forms.NumericUpDown numericUpDown10 = this.numericUpDown7;
			array = new int[4];
			array[0] = 10;
			numericUpDown10.Increment = new decimal(array);
			this.numericUpDown7.Location = new global::System.Drawing.Point(0, 136);
			global::System.Windows.Forms.NumericUpDown numericUpDown11 = this.numericUpDown7;
			array = new int[4];
			array[0] = 10000;
			numericUpDown11.Maximum = new decimal(array);
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.Size = new global::System.Drawing.Size(128, 21);
			this.numericUpDown7.TabIndex = 6;
			this.numericUpDown7.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			global::System.Windows.Forms.NumericUpDown numericUpDown12 = this.numericUpDown8;
			array = new int[4];
			array[0] = 100;
			numericUpDown12.Increment = new decimal(array);
			this.numericUpDown8.Location = new global::System.Drawing.Point(0, 157);
			global::System.Windows.Forms.NumericUpDown numericUpDown13 = this.numericUpDown8;
			array = new int[4];
			array[0] = 10000;
			numericUpDown13.Maximum = new decimal(array);
			this.numericUpDown8.Name = "numericUpDown8";
			this.numericUpDown8.Size = new global::System.Drawing.Size(128, 21);
			this.numericUpDown8.TabIndex = 7;
			this.numericUpDown8.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.listBox1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.HorizontalScrollbar = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new global::System.Drawing.Point(134, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new global::System.Drawing.Size(183, 196);
			this.listBox1.TabIndex = 9;
			this.listBox1.DoubleClick += new global::System.EventHandler(this.listBox1_DoubleClick);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new global::System.Drawing.Point(0, 187);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new global::System.Drawing.Size(96, 16);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "显示判定范围";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.treeView1.Anchor = global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right;
			this.treeView1.Location = new global::System.Drawing.Point(134, 209);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new global::System.Drawing.Size(183, 272);
			this.treeView1.TabIndex = 11;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(318, 480);
			base.Controls.Add(this.treeView1);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.listBox1);
			base.Controls.Add(this.numericUpDown8);
			base.Controls.Add(this.numericUpDown7);
			base.Controls.Add(this.richTextBox1);
			base.Controls.Add(this.numericUpDown4);
			base.Controls.Add(this.numericUpDown5);
			base.Controls.Add(this.numericUpDown6);
			base.Controls.Add(this.numericUpDown3);
			base.Controls.Add(this.numericUpDown2);
			base.Controls.Add(this.numericUpDown1);
			base.Name = "Form_Test";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Test";
			base.Load += new global::System.EventHandler(this.Form_Test_Load);
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown2).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown3).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown4).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown5).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown6).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown7).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown8).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000282 RID: 642
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000283 RID: 643
		private global::System.Windows.Forms.NumericUpDown numericUpDown1;

		// Token: 0x04000284 RID: 644
		private global::System.Windows.Forms.NumericUpDown numericUpDown2;

		// Token: 0x04000285 RID: 645
		private global::System.Windows.Forms.NumericUpDown numericUpDown3;

		// Token: 0x04000286 RID: 646
		private global::System.Windows.Forms.NumericUpDown numericUpDown4;

		// Token: 0x04000287 RID: 647
		private global::System.Windows.Forms.NumericUpDown numericUpDown5;

		// Token: 0x04000288 RID: 648
		private global::System.Windows.Forms.NumericUpDown numericUpDown6;

		// Token: 0x04000289 RID: 649
		private global::System.Windows.Forms.RichTextBox richTextBox1;

		// Token: 0x0400028A RID: 650
		private global::System.Windows.Forms.NumericUpDown numericUpDown7;

		// Token: 0x0400028B RID: 651
		private global::System.Windows.Forms.NumericUpDown numericUpDown8;

		// Token: 0x0400028C RID: 652
		public global::System.Windows.Forms.ListBox listBox1;

		// Token: 0x0400028D RID: 653
		public global::System.Windows.Forms.CheckBox checkBox1;

		// Token: 0x0400028E RID: 654
		public global::System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

		// Token: 0x0400028F RID: 655
		public global::System.Windows.Forms.TreeView treeView1;
	}
}
