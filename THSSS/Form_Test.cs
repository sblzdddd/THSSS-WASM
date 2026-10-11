using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Shooting
{
	// Token: 0x02000109 RID: 265
	public partial class Form_Test : Form
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00042904 File Offset: 0x00040B04
		public int Para1
		{
			get
			{
				return (int)this.numericUpDown1.Value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00042928 File Offset: 0x00040B28
		public int Para2
		{
			get
			{
				return (int)this.numericUpDown2.Value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0004294C File Offset: 0x00040B4C
		public int Para3
		{
			get
			{
				return (int)this.numericUpDown3.Value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00042970 File Offset: 0x00040B70
		public int Para4
		{
			get
			{
				return (int)this.numericUpDown4.Value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00042994 File Offset: 0x00040B94
		public int Para5
		{
			get
			{
				return (int)this.numericUpDown5.Value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x000429B8 File Offset: 0x00040BB8
		public int Para6
		{
			get
			{
				return (int)this.numericUpDown6.Value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x000429DC File Offset: 0x00040BDC
		public int Para7
		{
			get
			{
				return (int)this.numericUpDown7.Value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00042A00 File Offset: 0x00040C00
		public int Para8
		{
			get
			{
				return (int)this.numericUpDown8.Value;
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00042A22 File Offset: 0x00040C22
		public Form_Test()
		{
			this.InitializeComponent();
			this.folderBrowserDialog1.SelectedPath = Application.StartupPath + "\\CS";
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00042A58 File Offset: 0x00040C58
		private void Form_Test_Load(object sender, EventArgs e)
		{
			base.Location = new Point(base.Location.X + 325 + base.Width / 2, base.Location.Y);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00042A9D File Offset: 0x00040C9D
		public void SendString(string s)
		{
			RichTextBox richTextBox = this.richTextBox1;
			richTextBox.Text += s;
			RichTextBox richTextBox2 = this.richTextBox1;
			richTextBox2.Text += '\n';
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00042AD8 File Offset: 0x00040CD8
		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			if (DialogResult.OK == this.folderBrowserDialog1.ShowDialog())
			{
				this.listBox1.Items.Clear();
				string[] files = Directory.GetFiles(this.folderBrowserDialog1.SelectedPath, "*.mbg");
				foreach (string text in files)
				{
					FileInfo fileInfo = new FileInfo(text);
					this.listBox1.Items.Add(fileInfo.Name);
				}
				if (this.listBox1.Items.Count > 0)
				{
					this.listBox1.SelectedIndex = 0;
				}
			}
		}
	}
}
