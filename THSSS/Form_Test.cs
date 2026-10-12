using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Shooting
{
	public partial class Form_Test : Form
	{
		public int Para1
		{
			get
			{
				return (int)this.numericUpDown1.Value;
			}
		}

		public int Para2
		{
			get
			{
				return (int)this.numericUpDown2.Value;
			}
		}

		public int Para3
		{
			get
			{
				return (int)this.numericUpDown3.Value;
			}
		}

		public int Para4
		{
			get
			{
				return (int)this.numericUpDown4.Value;
			}
		}

		public int Para5
		{
			get
			{
				return (int)this.numericUpDown5.Value;
			}
		}

		public int Para6
		{
			get
			{
				return (int)this.numericUpDown6.Value;
			}
		}

		public int Para7
		{
			get
			{
				return (int)this.numericUpDown7.Value;
			}
		}

		public int Para8
		{
			get
			{
				return (int)this.numericUpDown8.Value;
			}
		}

		public Form_Test()
		{
			this.InitializeComponent();
			this.folderBrowserDialog1.SelectedPath = Application.StartupPath + "\\CS";
		}

		private void Form_Test_Load(object sender, EventArgs e)
		{
			base.Location = new Point(base.Location.X + 325 + base.Width / 2, base.Location.Y);
		}

		public void SendString(string s)
		{
			RichTextBox richTextBox = this.richTextBox1;
			richTextBox.Text += s;
			RichTextBox richTextBox2 = this.richTextBox1;
			richTextBox2.Text += '\n';
		}

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
