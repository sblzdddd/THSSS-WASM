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
				return (int)numericUpDown1.Value;
			}
		}

		public int Para2
		{
			get
			{
				return (int)numericUpDown2.Value;
			}
		}

		public int Para3
		{
			get
			{
				return (int)numericUpDown3.Value;
			}
		}

		public int Para4
		{
			get
			{
				return (int)numericUpDown4.Value;
			}
		}

		public int Para5
		{
			get
			{
				return (int)numericUpDown5.Value;
			}
		}

		public int Para6
		{
			get
			{
				return (int)numericUpDown6.Value;
			}
		}

		public int Para7
		{
			get
			{
				return (int)numericUpDown7.Value;
			}
		}

		public int Para8
		{
			get
			{
				return (int)numericUpDown8.Value;
			}
		}

		public Form_Test()
		{
			InitializeComponent();
			folderBrowserDialog1.SelectedPath = Application.StartupPath + "\\CS";
		}

		private void Form_Test_Load(object sender, EventArgs e)
		{
			Location = new Point(Location.X + 325 + Width / 2, Location.Y);
		}

		public void SendString(string s)
		{
			var richTextBox = richTextBox1;
			richTextBox.Text += s;
			var richTextBox2 = richTextBox1;
			richTextBox2.Text += '\n';
		}

		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
			{
				listBox1.Items.Clear();
				var files = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.mbg");
				foreach (var text in files)
				{
					var fileInfo = new FileInfo(text);
					listBox1.Items.Add(fileInfo.Name);
				}
				if (listBox1.Items.Count > 0)
				{
					listBox1.SelectedIndex = 0;
				}
			}
		}
	}
}
